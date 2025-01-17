﻿Public Class TagContainer

    Public Tags() As Tag

#Region "Constructors"

    Protected Sub New(Optional tags() As Tag = Nothing)
        If IsNothing(tags) Then
            Me.Tags = {}
        Else
            Me.Tags = tags
        End If
    End Sub

#End Region

#Region "Tags List Handling"

    Public Function HasTag(tagName As String) As Boolean
        'returns whether or not this container has a tag which matches a given name

        Return Not IsNothing(FindTag(tagName))
    End Function

    Public Function FindTag(tagName As String) As Tag
        'returns the first tag this container has which matches the given name

        Dim matchingTags() As Tag = FindTags(tagName)

        If Not IsNothing(matchingTags) AndAlso UBound(matchingTags) > -1 Then
            'returns first matching tag
            Return matchingTags(0)
        Else
            'returns nothing if no matching tags found
            Return Nothing
        End If
    End Function

    Public Function FindTags(tagName As String) As Tag()
        'returns all the tags this container has which match a given name
        'useful for things such as listeners where there are multiple tags with the same name

        Dim result() As Tag = {}

        If Not IsNothing(Tags) Then
            For index As Integer = 0 To UBound(Tags)
                If LCase(Tags(index).Name) = LCase(tagName) Then
                    result = InsertItem(result, Tags(index))
                End If
            Next
        End If

        Return result
    End Function

    Public Sub AddTag(newTag As Tag)
        'adds the given tag to the end of the tag list

        Tags = InsertItem(Tags, newTag)
    End Sub

    Public Sub SetTag(newTag As Tag)
        'changes the argument of any tags with a name matching the new tag, adds tag instead if tag previously missing

        Dim tagName As String = newTag.Name
        Dim tagSet As Boolean = False
        For Each currentTag As Tag In Tags
            If currentTag.Name = tagName Then
                currentTag.Argument = newTag.Argument
                tagSet = True
            End If
        Next

        If Not tagSet Then
            AddTag(newTag)
        End If
    End Sub

    Public Sub RemoveTag(tagName As String)
        'removes all tags with the given name

        If Not IsNothing(Tags) AndAlso UBound(Tags) >= 0 Then
            Dim tagIndex As Integer = 0
            Do While tagIndex <= UBound(Tags)
                If Tags(tagIndex).Name = tagName Then
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

#End Region

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
        SetTag(New Tag(tagName, argument))
    End Sub

#End Region

End Class
