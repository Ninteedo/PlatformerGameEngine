Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class Room
    'a room is a collection of actors which are all rendered at once

    Public actors() As Actor    'the actors which are used in the game, modified copies of the defaults
    Public name As String

    Private Const actorsTagName As String = "actors"

#Region "Constructors"

    Public Sub New(Optional actors As Actor() = Nothing)
        Me.actors = actors
    End Sub

    Public Sub New(roomString As String, renderEngine As PRE2)
        If Not IsNothing(roomString) Then
            'Try
            Dim roomTag As New Tag(roomString)
            If Not IsNothing(roomTag) Then
                'loads the room name
                name = roomTag.name

                'loads each actor
                Dim actorsTag As Tag = roomTag.FindSubTag(actorsTagName)
                If Not IsNothing(actorsTag) Then
                    Dim temp As Object = actorsTag.InterpretArgument
                    If Not IsNothing(temp) Then
                        ReDim actors(UBound(temp))
                        For index As Integer = 0 To UBound(temp)
                            actors(index) = New Actor(temp(index).ToString, renderEngine)
                        Next
                    End If
                End If
            End If
            'Catch ex As Exception
            '    DisplayError("An error occured whilst loading an room" & vbCrLf & ex.ToString)
            'End Try
        End If
    End Sub

#End Region

#Region "Other"

    Public Overrides Function ToString() As String
        'updated version of room ToString which makes better use of tags

        Return New Tag(name, ArrayToString({New Tag(actorsTagName, ArrayToString(actors)).ToString})).ToString
    End Function

#End Region

End Class
