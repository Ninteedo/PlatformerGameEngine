''' <summary>
''' A superclass used for Level and Actor classes which contains a list of Tags and ways to manipulate them
''' </summary>

Public Class TagContainer

    Public Tags() As Tag

#Region "Constructors"

    Protected Sub New(Optional tags() As Tag = Nothing)
        Me.Tags = tags
    End Sub

#End Region

    Public Function HasTag(tagName As String) As Boolean
        'returns whether or not this container has a tag which matches a given name

        Return Not IsNothing(FindTag(tagName))
    End Function

    Public Function FindTag(tagName As String) As Tag
        'returns the first tag this container has which matches the given name

        Dim matchingTags() As Tag = FindTags(tagName)

        If Not IsNothing(matchingTags) Then
            'returns first matching tag
            Return matchingTags(0)
        Else
            'returns nothing if no matching tags found
            Return Nothing
        End If
    End Function

    Private Function FindTags(tagName As String) As Tag()
        'returns all the tags this container has which match a given name
        'useful for things such as listeners where there are multiple tags with the same name

        Dim result() As Tag = Nothing

        If Not IsNothing(Tags) Then
            For index As Integer = 0 To UBound(Tags)
                If LCase(Tags(index).name) = LCase(tagName) Then
                    result = InsertItem(result, Tags(index))
                End If
            Next
        End If

        Return result
    End Function

    Public Sub AddTag(newTag As Tag, Optional removeDuplicates As Boolean = False)
        'adds the given tag to the end of the tag list

        If removeDuplicates Then
            RemoveTag(newTag.name)
        End If

        Tags = InsertItem(Tags, newTag)
    End Sub

    Public Sub RemoveTag(tagName As String)
        'removes all tags with the given name

        If Not IsNothing(Tags) AndAlso UBound(Tags) >= 0 Then
            Dim tagIndex As Integer = 0
            Do While tagIndex <= UBound(Tags)
                If Tags(tagIndex).name = tagName Then
                    Tags = RemoveItem(Tags, tagIndex)

                    'breaks out of for if there are no elements left in the tag array
                    If IsNothing(Tags) Then
                        Exit Do
                    End If
                Else        'tag index is only incremented if a tag isn't removed as the list shifts
                    tagIndex += 1
                End If
            Loop
        End If
    End Sub

#Region "Key Properties"

    Protected Function GetProperty(Of t)(tagName As String, defaultVal As t) As t
        'returns the value of a property with a given name, or if it doesn't have a value then the default value
        If HasTag(tagName) Then
            Return FindTag(tagName).InterpretArgument()
        Else
            Return defaultVal
        End If
    End Function

    Protected Sub SetProperty(tagName As String, argument As String)
        AddTag(New Tag(tagName, argument), removeDuplicates:=True)
    End Sub

#End Region

End Class
