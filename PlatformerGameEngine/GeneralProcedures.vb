Imports System.Configuration
Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports PlatformerGameEngine.My

Module GeneralProcedures

#Region "Array Modifying"

    Public Function RemoveItem(Of t)(ByVal array As t(), ByVal removeIndex As Integer) As t()
        'removes the item at the given index from a given array of a given type

        If IsNothing(array) Then
            DisplayError("Attempted to remove something from an empty array")
        ElseIf removeIndex < 0 Or removeIndex > UBound(array) Then
            DisplayError("Attempted to remove something from an array at an invalid index")
        Else
            If UBound(array) > 0 Then
                For index As Integer = removeIndex To UBound(array) - 1
                    array(index) = array(index + 1)
                Next
                ReDim Preserve array(UBound(array) - 1)
            Else        'if array length is 0 then removing the last item will remove last element of array
                array = Nothing
            End If
        End If

        Return array
    End Function

    Public Function InsertItem(Of t)(ByVal array As t(), ByVal newItem As t, Optional ByVal insertIndex As Integer = -1) As t()
        'inserts the given item to the end of the given array of a given type

        If Not IsNothing(array) Then
            ReDim Preserve array(UBound(array) + 1)

            If insertIndex >= 0 Then        'inserts into middle of array
                For index As Integer = UBound(array) To insertIndex + 1 Step -1
                    array(index) = array(index - 1)
                Next
                array(insertIndex) = newItem
            Else    'if no value (default -1) given then adds item to end of array
                array(UBound(array)) = newItem
            End If
        Else        'previously empty array
            array = {newItem}
        End If

        Return array
    End Function

#End Region

#Region "File Handling"

    Public Function ReadFile(ByVal fileLocation As String) As String
        'returns the contents of a text file at a given location

        If IO.File.Exists(fileLocation) = True Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd()
            reader.Close()

            Return fileText
        Else
            DisplayError("Couldn't find file " & fileLocation)
            Return Nothing
        End If
    End Function

    Public Sub WriteFile(ByVal fileLocation As String, ByVal message As String)
        'writes a message to a file

        Dim writer As New IO.StreamWriter(fileLocation)

        For Each c As Char In message
            writer.Write(c)
        Next c

        writer.Close()
    End Sub

    Public Function FindProperty(ByVal fileText As String, ByVal propertyName As String) As String     'returns the property in a file with a given name, property: value
        'TODO: is it possible to make this redundant

        Dim lines() As String = fileText.Split(Environment.NewLine)

        For Each line As String In lines
            Dim currentProperty As String = line.Split(":")(0).Replace(vbLf, "")

            If currentProperty = propertyName Then
                Return Trim(line.Split(":")(1))         'issue: cant have colons anywhere else in the line
            End If
        Next line

        Return Nothing
    End Function

#End Region

#Region "Error Handling"

    Public Sub DisplayError(ByVal message As String)
        'displays a given error message to the user

        MsgBox(message, MsgBoxStyle.Exclamation)
    End Sub

#End Region

#Region "Other"

    Public Sub RefreshList(list As ListBox, values() As String)
        'empties a list and fills it with given values

        If Not IsNothing(list) Then
            Dim startSelectedIndex As Integer = list.SelectedIndex
            list.SelectedIndex = -1
            list.Items.Clear()

            If Not IsNothing(values) Then
                For Each value As String In values
                    If Not IsNothing(value) Then
                        list.Items.Add(value)
                    End If
                Next value

                If startSelectedIndex < list.Items.Count Then
                    list.SelectedIndex = startSelectedIndex
                End If
            End If
        Else
            'DisplayError("A list tried to refresh but doesn't exist")
        End If
    End Sub

    Public Function MakeNameUnique(ByVal name As String, otherNames() As String, removeUnnecessary As Boolean) As String
        'returns a name with a number appended to it so the name is unique

        name = RemoveQuotes(name)

        If Not IsNothing(otherNames) Then
            Dim copyNumber As Integer = 0           'used to find which number needs to added to the end of the instance name so there aren't any duplicate names
            Dim generatedName As String = name
            Dim nameUnique As Boolean = False

            Do
                copyNumber += 1
                If Not removeUnnecessary Or copyNumber > 1 Then
                    generatedName = AddQuotes(name & "-" & Trim(Str(copyNumber)))
                End If
                nameUnique = True

                'checks if name is unique
                For Each otherName As String In otherNames
                    If otherName = generatedName Then
                        nameUnique = False
                        Exit For
                    End If
                Next
            Loop Until nameUnique

            Return generatedName
        Else
            Return name & If(Not removeUnnecessary, "-1", "")
        End If
    End Function

#End Region

#Region "Database Connections"

    Public Sub SQLTest()
        Try
            Dim sqlReader As OleDbDataReader
            'creates and opens connection to database
            'Dim conType As String = "Provider=Microsoft.ACE.OLEDB.12.0;"
            'Dim fileLocation As String = $"Data Source=F:\School\Higher\Computing\Visual Basic\PlatformerGameEngine\resources\games\Robotic Escape\Test.accdb"
            Dim conn As New OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0;Data Source=""F:\School\Higher\Computing\Visual Basic\PlatformerGameEngine\resources\games\Robotic Escape\Test.mdb""")
            conn.Open()

            Dim query As String = "SELECT * FROM [Customers]"
            Dim command As New OleDbCommand(query, conn)
            sqlReader = command.ExecuteReader

            If sqlReader.HasRows Then
                Dim output As String = ""
                While sqlReader.Read
                    output += sqlReader("ID") & " " & sqlReader("Firstname") & " " & sqlReader("Surname") & vbCrLf
                End While
                MsgBox(output)
            Else
                DisplayError("No Results Returned")
            End If

            conn.Close()
        Catch ex As Exception
            DisplayError(ex.ToString)
        End Try
    End Sub

    Public Sub MySqlTest()
        Try
            Dim sqlReader As MySqlDataReader
            'creates and opens connection to database
            Dim conn As New MySqlConnection(Settings.ProjectScoresConnectionString)
            conn.Open()

            Dim query As String = InputBox("Enter SQL Query") '"SELECT * FROM Score;"
            Dim command As New MySqlCommand(query, conn)
            sqlReader = command.ExecuteReader()

            If sqlReader.HasRows Then
                Dim output As String = ""
                While sqlReader.Read
                    output += sqlReader("ID") & " " & sqlReader("initials") & " " & sqlReader("points") & vbCrLf
                End While
                MsgBox(output)
            Else
                MsgBox("No results returned")
            End If


            'closes connection
            conn.Close()
        Catch ex As Exception
            DisplayError(ex.ToString())
        End Try
    End Sub

    Public Sub SaveScore(points As Integer, gameName As String)
        'asks the user for initials and saves their score to the Score table
        'uses a MySQL server running from XAMPP

        Try
            'creates and opens connection to database
            Dim conn As New MySqlConnection(Settings.ProjectScoresConnectionString)
            conn.Open()

            'gets initials from the user
            Dim initials As String = Nothing
            Do While IsNothing(initials) OrElse Len(initials) <> 3
                initials = InputBox("Enter initials for scoreboard" & vbCrLf _
                             & "Press cancel if you don't want your score saved",
                        "Enter Initials")

                If IsNothing(initials) Then
                    'closes connection and doesn't insert score if user presses cancel
                    conn.Close()
                    Exit Sub
                ElseIf Len(initials) <> 3 Then
                    DisplayError("Initials must be 3 characters long")
                End If
            Loop

            'sql query
            Dim query = "INSERT INTO Score (initials, points, gameName) VALUES (@initials, @points, @gameName);"

            'using preparing to prevent SQL injection attacks (injection possible for game name and possibly initials)
            Using cmd As New MySqlCommand(query, conn)
                'prepares query
                cmd.Parameters.Add("@initials", MySqlDbType.String, 3)
                cmd.Parameters.Add("@points", MySqlDbType.Int16)
                cmd.Parameters.Add("@gameName", MySqlDbType.String, 64)
                cmd.Prepare()

                'binds values to parameters and executes with values
                cmd.Parameters(0).Value = initials
                cmd.Parameters(1).Value = points
                cmd.Parameters(2).Value = gameName
                cmd.ExecuteNonQuery()
            End Using

            'closes connection
            conn.Close()
        Catch ex As Exception
            DisplayError("An error has occured so your score couldn't be saved" & vbCrLf & ex.ToString())
        End Try
    End Sub

#End Region

#Region "Scaling"

    'these are used to simply code elsewhere

    Public Function ScaleSize(s As SizeF, factor As Single) As SizeF
        'multiplies the width and height of a size by a scalar

        Return New SizeF(s.Width * factor, s.Height * factor)
    End Function

    Public Function ScaleSize(s1 As SizeF, s2 As SizeF) As SizeF
        'multiples two sizes together by individually multiplying their width and height

        Return New SizeF(s1.Width * s2.Width, s1.Height * s2.Height)
    End Function

    Public Function ScalePoint(p As PointF, s As SizeF) As PointF
        'multiples the X and Y of the point by the width and height of the size

        Return New PointF(p.X * s.Width, p.Y * s.Height)
    End Function

    Public Function ScaleRect(r As RectangleF, s As SizeF) As RectangleF
        'scales the rectangle with the size

        Return New RectangleF(ScalePoint(r.Location, s), ScaleSize(r.Size, s))
    End Function

#End Region

End Module
