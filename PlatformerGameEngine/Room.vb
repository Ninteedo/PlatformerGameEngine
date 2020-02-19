Public Class Room

    Public Actors() As Actor    'the Actors which are used in the game, modified copies of the defaults
    Public Name As String


#Region "Constructors"

    Public Sub New(actors() As Actor, Optional name As String = "Unnamed")
        Me.Actors = actors
        Me.Name = name
    End Sub

    Public Sub New(roomString As String)
        If Not IsNothing(roomString) Then
            'Try
            Dim roomTag As New Tag(roomString)
            If Not IsNothing(roomTag) Then
                'loads the room name
                Name = roomTag.Name

                'loads each actor
                Dim actorsTag As Tag = roomTag.FindSubTag(My.Resources.ActorTagName)
                If Not IsNothing(actorsTag) Then
                    Dim temp As Object = actorsTag.InterpretArgument
                    If IsArray(temp) Then
                        ReDim Actors(UBound(temp))
                        For index As Integer = 0 To UBound(temp)
                            Actors(index) = New Actor(temp(index).ToString)
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

        Return New Tag(Name, ArrayToString({New Tag(My.Resources.ActorTagName, ArrayToString(Actors)).ToString})).ToString
    End Function

#End Region

End Class
