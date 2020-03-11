Module GeneralProcedures

#Region "Array Modifying"

    Public Function RemoveItem(Of t)(array As t(), removeIndex As Integer) As t()
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
                array = {}
            End If
        End If

        Return array
    End Function

    Public Function InsertItem(Of t)(array As t(), newItem As t, Optional insertIndex As Integer = -1) As t()
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

    Public Function ReadFile(fileLocation As String) As String
        'returns the contents of a text file at a given location

        If IO.File.Exists(fileLocation) Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd()
            reader.Close()
            Return fileText
        Else
            DisplayError("Couldn't find file " & fileLocation)
            Return Nothing
        End If
    End Function

    Public Sub WriteFile(fileLocation As String, message As String)
        'writes a message to a file

        Dim writer As New IO.StreamWriter(fileLocation)
        For Each c As Char In message
            writer.Write(c)
        Next c
        writer.Close()
    End Sub

#End Region

#Region "Error Handling"

    Public Sub DisplayError(message As String)
        'displays a given error message to the user

        MsgBox(message, MsgBoxStyle.Exclamation)
    End Sub

#End Region

#Region "Other"

    Public Sub RefreshList(list As ListBox, values() As String)
        'empties a list and fills it with given values

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
    End Sub

    Public Function MakeNameUnique(name As String, otherNames() As String, removeUnnecessary As Boolean) As String
        'returns a name with a number appended to it so the name is unique

        name = RemoveQuotes(name)

        If Not IsNothing(otherNames) Then
            Dim copyNumber As Integer = 0           'used to find which number needs to added to the end of the instance name so there aren't any duplicate names
            Dim generatedName As String = name
            Dim nameUnique As Boolean 

            Do
                copyNumber += 1
                If Not removeUnnecessary Or copyNumber > 1 Then
                    generatedName = name & "-" & Trim(Str(copyNumber))
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
