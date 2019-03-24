'Richard Holmes
'23/03/2019
'Platformer engine, actual game executor

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmGame

    Dim delayTimer As New Timer With {.Interval = 1, .Enabled = False}

    Private Sub FrmGame_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'used because issues can arise if using the form load event for lots of code

        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        'executed once the form loads

        delayTimer.Stop()

        renderer = New PRE2 With {.panelCanvasGameArea = New PaintEventArgs(pnlGame.CreateGraphics, New Rectangle(New Point(0, 0), pnlGame.Size))}
    End Sub




    Dim renderer As PRE2            'panel renderer engine 2





    Public Sub EntityEvent(ent As PRE2.Entity, behaviour As PRE2.Tag, Optional entTarget As PRE2.Entity = Nothing)

    End Sub

    Public Sub EntityBehaviour(ent As PRE2.Entity, behaviour As PRE2.Tag, Optional ent2 As PRE2.Entity = Nothing)
        'does the behaviour for the entity, based on the tag given

        Select Case behaviour.name
            Case "xVel"         'args(0):float
                Dim start As PointF = ent.location

                ent.location.X += behaviour.args(0)
            Case "yVel"         'args(0):float
                ent.location.Y += behaviour.args(0)
            Case "xAcc"         'args(0):float
                ent.FindTag("xVel").args(0) += behaviour.args(0)
            Case "yAcc"         'args(0):float
                ent.FindTag("yVel").args(0) += behaviour.args(0)

        End Select
    End Sub

    Public Function EntityWithName(entityName As String) As PRE2.Entity
        Select Case entityName
            Case "other"

            Case Else
                For index As Integer = 0 To UBound(renderer.entities)
                    If renderer.entities(index).name = entityName Then
                        Return renderer.entities(index)
                    End If
                Next index
        End Select
    End Function
End Class