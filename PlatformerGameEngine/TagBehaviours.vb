'Richard Holmes
'28/08/2019
'Some predefined behaviours for specific tags

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module TagBehaviours

    Dim errorMessageArgumentInvalid As String = " has an invalid argument"

    Public Sub ProcessTag(ByRef ent As PRE2.Entity, tagIndex As Integer)
        'processes a single tag and modifes the entity accordingly

        If Not IsNothing(ent) AndAlso Not IsNothing(ent.tags) AndAlso tagIndex >= 0 AndAlso tagIndex <= UBound(ent.tags) Then
            Dim tag As PRE2.Tag = ent.tags(tagIndex)

            Select Case tag.name
                Case "xVel"
                    TagXVel(ent, tagIndex)
                Case "yVel"
                    TagYVel(ent, tagIndex)
            End Select
        End If
    End Sub

    Private Sub TagXVel(ByRef ent As PRE2.Entity, tagIndex As Integer)
        'changes the location of the entity by its x velocity

        'If IsANumber(ent.tags(tagIndex).args(0)) Then
        ent.location = New PointF(ent.location.X + ent.tags(tagIndex).args(0), ent.location.Y)
        'Else
        '    PRE2.DisplayError(ent.tags(tagIndex).ToString & errorMessageArgumentInvalid)
        'End If
    End Sub

    Private Sub TagYVel(ByRef ent As PRE2.Entity, tagIndex As Integer)
        'changes the location of the entity by its y velocity

        'If IsANumber(ent.tags(tagIndex).args(0)) Then
        ent.location = New PointF(ent.location.X, ent.location.Y + ent.tags(tagIndex).args(0))
        'Else
        '    PRE2.DisplayError(ent.tags(tagIndex).ToString & errorMessageArgumentInvalid)
        'End If
    End Sub


    Private Function IsANumber(value As Object) As Boolean
        'used for validating tags

        If Not IsNothing(value) AndAlso IsNumeric(value) Then
            Return True
        Else
            Return False
        End If
    End Function
End Module