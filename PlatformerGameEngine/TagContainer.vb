'A template used for Room and Actor classes

Public Class TagContainer
    Public tags() As Tag

    Public Sub New(Optional startTags() As Tag = Nothing)
        tags = startTags
    End Sub

    Public Function HasTag(ByVal tagName As String) As Boolean
        'returns whether or not this container has a tag which matches a given name

        Return Not IsNothing(FindTag(tagName))
    End Function

    Public Function FindTag(ByVal tagName As String) As Tag
        'returns the first tag this container has which matches the given name

        If IsNothing(tags) = False Then
            For index As Integer = 0 To UBound(tags)
                If LCase(tags(index).name) = LCase(tagName) Then
                    Return tags(index)
                End If
            Next index
        End If

        'returns nothing if no matching tag found
        Return Nothing
    End Function

    Public Sub AddTag(ByVal newTag As Tag, Optional ByVal removeDuplicates As Boolean = False)
        'adds the given tag to the end of the tag list

        If removeDuplicates Then
            RemoveTag(newTag.name)
        End If

        If IsNothing(tags) Then
            ReDim tags(0)
        Else
            ReDim Preserve tags(UBound(tags) + 1)
        End If
        tags(UBound(tags)) = newTag
    End Sub

    Public Sub RemoveTag(ByVal tagName As String)
        'removes all tags with the given name

        Dim tagIndex As Integer = 0

        If Not IsNothing(tags) Then
            Do While tagIndex <= UBound(tags)
                If tags(tagIndex).name = tagName Then
                    For removeIndex As Integer = tagIndex To UBound(tags) - 1
                        tags(removeIndex) = tags(removeIndex + 1)
                    Next removeIndex

                    ReDim Preserve tags(UBound(tags) - 1)
                Else
                    tagIndex += 1       'tag index isn't incremented when a tag with matching name is found so none are skipped
                End If
            Loop
        End If
    End Sub
End Class
