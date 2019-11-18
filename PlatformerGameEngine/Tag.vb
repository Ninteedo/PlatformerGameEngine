Public Class Tag
    Public name As String
    Public argument As String

    Public Sub New(name As String, Optional argument As String = Nothing)
        Me.name = name
        Me.argument = argument
    End Sub

    Public Sub New(ByVal tagString As String)
        'creates a tag from a string

        Dim newTag As Tag = JsonToTag(tagString)
        name = newTag.name
        argument = newTag.argument
    End Sub

    Public Overrides Function ToString() As String
        Return TagToJSON(Me)
    End Function

    Public Function InterpretArgument(Optional subTagName As String = Nothing) As Object
        If IsNothing(subTagName) Then
            Return InterpretValue(argument)
        Else
            Return FindSubTag(subTagName)
        End If
    End Function

    Public Function FindSubTag(subTagName As String) As Tag
        'returns a subtag in this tag's argument which has a name which matches the provided subtagname

        Dim subTagsTemp() As Object = InterpretArgument()

        If Not IsNothing(subTagsTemp) Then
            For index As Integer = 0 To UBound(subTagsTemp)
                Dim subTag As Tag = TryCast(subTagsTemp(index), Tag)
                If Not IsNothing(subTag) AndAlso subTag.name = subTagName Then
                    Return subTag
                End If
            Next
        End If

        'couldn't find a matching sub tag
        Return Nothing
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
