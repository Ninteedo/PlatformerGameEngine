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
                For index As Integer = insertIndex To UBound(array) - 1
                    array(index + 1) = array(index)
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

    Public Sub Log(message As String, level As WarnLevel)
        'logs something noteworthy, eg an error or a warning or a debug
        'warn levels are 0:info, 1:warn, 2:error, 3:fatal
    End Sub

    Public Enum WarnLevel As Integer
        info
        warn
        err
        fatal
    End Enum

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

#End Region

End Module
