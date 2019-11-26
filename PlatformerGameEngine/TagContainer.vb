﻿'A superclass used for Room and Actor classes

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

        Dim matchingTags() As Tag = FindTags(tagName)

        If Not IsNothing(matchingTags) Then
            'returns first matching tag
            Return matchingtags(0)
        Else
            'returns nothing if no matching tags found
            Return Nothing
        End if
    End Function

    Public Function FindTags(ByVal tagName As String) As Tag()
        'returns all the tags this container has which match a given name
        'useful for things such as listeners where there are multiple tags with the same name

        Dim result() As Tag = Nothing

        If Not IsNothing(tags) Then
            For index As Integer = 0 To UBound(tags)
                If LCase(tags(index).name) = LCase(tagName) Then
                    result = InsertItem(result, tags(index))
                End If
            Next
        End If

        Return result
    End Function

    Public Sub AddTag(ByVal newTag As Tag, Optional ByVal removeDuplicates As Boolean = False)
        'adds the given tag to the end of the tag list

        If removeDuplicates Then
            RemoveTag(newTag.name)
        End If

        tags = InsertItem(tags, newTag)
    End Sub

    Public Sub RemoveTag(ByVal tagName As String)
        'removes all tags with the given name

        If Not IsNothing(tags) AndAlso UBound(tags) >= 0 Then
            Dim tagIndex As Integer = 0
            Do While tagIndex <= UBound(tags)
                If tags(tagIndex).name = tagName Then
                    tags = RemoveItem(tags, tagIndex)

                    'breaks out of for if there are no elements left in the tag array
                    If IsNothing(tags) Then
                        Exit Do
                    End If
                Else        'tag index is only incremented if a tag isn't removed as the list shifts
                    tagIndex += 1
                End If
            Loop
        End If
    End Sub
End Class
