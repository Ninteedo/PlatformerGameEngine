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
        Me.name = newTag.name
        Me.argument = newTag.argument
    End Sub

    Public Overrides Function ToString() As String
        Return TagToJSON(Me)
    End Function

    Public Function InterpretArgument(Of t)() As t
        Return InterpretValue(argument, GetType(t))

        'If GetType(t).IsArray Then
        '    Return CTypeDynamic(InterpretValueArray(Of Object)(argument), GetType(t))
        'Else
        '    Return InterpretValue(Of t)(argument)
        'End If

        'if subTagName is provided then searches the argument for a tag with the same name
        ' If Not IsNothing(subTagName) Then
        '     If IsArray(result) Then
        '         For index As Integer = 0 To UBound(result)
        '             If Not IsNothing(result(index)) AndAlso result(index).name = subTagName Then
        '                 Return result(index)
        '             End If
        '         Next
        '     ElseIf Not IsNothing(result) AndAlso result.name = subTagName Then
        '         Return result
        '     End If

        '     'couldn't find a matching sub tag
        '     Return Nothing
        ' End If

        'Return result
    End Function

    Public Sub SetArgument(newValue As Object)
        'sets the argument of the given tag to this new value

        argument = ArrayToString(newValue)
    End Sub

    Public Function FindSubTag(subTagName As String) As Tag
        'returns a subtag in this tag's argument which has a name which matches the provided subtagname

        Dim subTags As Object() = InterpretValue(argument, GetType(Tag()))

        If Not IsNothing(subTags) Then
            For index As Integer = 0 To UBound(subTags)
                Dim subTag As Tag = subTags(index)
                If Not IsNothing(subTag) AndAlso subTag.name = subTagName Then
                    Return subTag
                End If
            Next
        End If

        'couldn't find a matching sub tag
        Return Nothing
    End Function

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

    Public Shared Widening Operator CType(ByVal s As String) As Tag
        Return New Tag(s)
    End Operator

    Public Shared Widening Operator CType(ByVal t As Tag) As String
        Return t.ToString
    End Operator

End Class