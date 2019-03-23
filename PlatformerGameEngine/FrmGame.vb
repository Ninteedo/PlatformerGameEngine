﻿'Richard Holmes
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








    Public Sub EntityBehaviour(ent As PRE2.Entity, behaviour As PRE2.Tag)
        Select Case behaviour.name
            Case "xVel"
                Dim start As PointF = ent.location

                ent.location.X += behaviour.args(0)
            Case "yVel"
                ent.location.Y += behaviour.args(0)
            Case "xAcc"
                ent.FindTag("xVel").args(0) += behaviour.args(0)
            Case "yAcc"
                ent.FindTag("yVel").args(0) += behaviour.args(0)
        End Select
    End Sub
End Class