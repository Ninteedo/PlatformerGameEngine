Public Structure Tag
    Public name As String
    Public argument As String

    Public Sub New(tagName As String, Optional argument As String = Nothing)
        Me.name = tagName
        Me.argument = argument
    End Sub

    Public Sub New(ByVal tagString As String)
        'creates a tag from a string

        Dim newTag As Tag = JsonToTag(tagString)
        name = newTag.name
        argument = newTag.argument
    End Sub

    Public Overrides Function ToString() As String
        'turns this tag into a string

        Return TagToJSON(Me)
    End Function

    Public Function InterpretArgument(Optional subTagName As String = Nothing) As Object
        Dim temp As Object = InterpretValue(argument)

        'if subTagName is provided then searches the argument for a tag with the same name
        If Not IsNothing(subTagName) Then
            If IsArray(temp) Then
                For index As Integer = 0 To UBound(temp)
                    If Not IsNothing(temp(index)) AndAlso temp(index).name = subTagName Then
                        Return temp(index)
                    End If
                Next
            ElseIf Not IsNothing(temp) AndAlso temp.name = subTagName Then
                Return temp
            End If

            'couldn't find a matching sub tag
            Return Nothing
        End If

        Return temp
    End Function

    Public Sub SetArgument(newValue As Object)
        argument = ArrayToString(newValue)
    End Sub

    Public Shared Function AreTagsIdentical(tag1 As Tag, tag2 As Tag) As Boolean
        'used for = and <> operators

        Return LCase(tag1.name) = LCase(tag2.name) AndAlso tag1.argument = tag2.argument
    End Function

    Public Shared Operator =(tag1 As Tag, tag2 As Tag) As Boolean
        Return AreTagsIdentical(tag1, tag2)
    End Operator

    Public Shared Operator <>(tag1 As Tag, tag2 As Tag) As Boolean
        Return Not AreTagsIdentical(tag1, tag2)
    End Operator
End Class
