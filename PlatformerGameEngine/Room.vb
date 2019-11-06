Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class Room
    'a room is a collection of actors which are all rendered at once

    Public actors() As Actor    'the actors which are used in the game, modified copies of the defaults
    Public name As String

    Public Sub New(Optional actors As Actor() = Nothing)
        Me.actors = actors
    End Sub

    Public Sub New(roomTag As Tag, renderEngine As PRE2)
        Dim tagStrings() As Object = roomTag.InterpretArgument
        If Not IsNothing(tagStrings) Then
            Dim tags(UBound(tagStrings)) As Tag
            For tagIndex As Integer = 0 To UBound(tags)
                tags(tagIndex) = New Tag(tagStrings(tagIndex).ToString)
            Next

            For Each thisTag As Tag In tags
                If Not IsNothing(thisTag) Then
                    Select Case thisTag.name
                        Case "instances"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim actors(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    actors(index) = New Actor(temp(index).ToString, renderEngine)
                                Next
                            End If
                            'Case "tags"
                            '    Dim temp() As Object = thisTag.InterpretArgument
                            '    If Not IsNothing(temp) Then
                            '        ReDim tags(UBound(temp))
                            '        For index As Integer = 0 To UBound(temp)
                            '            tags(index) = temp(index)
                            '        Next
                            '    End If
                        Case Else
                            PRE2.DisplayError("Unknown tag in room tag: " & thisTag.name)
                    End Select
                End If
            Next
        End If
    End Sub

    Public Overrides Function ToString() As String
        'updated version of room tostring which makes better use of tags

        Return New Tag(name, New Tag("instances", ArrayToString(actors)).ToString).ToString
    End Function

End Class
