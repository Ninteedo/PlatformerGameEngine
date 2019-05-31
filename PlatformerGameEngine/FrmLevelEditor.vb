'Richard Holmes
'29/03/2019
'Level editor for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmLevelEditor

	'initialisation

    Dim delayTimer As New Timer With {.Interval = 1, .Enabled = False}

    Private Sub FrmLevelEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
    End Sub

    Private Sub Initialisation()
        LayoutInitialisation()
    End Sub

    Private Sub LayoutInitialisation()

    End Sub

	'save load
	
	Private Sub LoadRoom(fileLocation As String)
	
	End Sub
	
	Private Sub SaveRoom(saveLocation As String)
	
	End Sub

    Private Function LoadEntityTemplate(fileLocation As String) As PRE2.Entity

    End Function

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click

    End Sub

    Private Sub btnSaveAs_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub



    'render

    Dim renderer As New PRE2
	Dim thisLevel As FrmGame.Level
	Dim thisRoom As FrmGame.Room
	
	Private Sub RenderLevel()
	
	End Sub


    'control



    Private Sub AddEntityInstance()

    End Sub

    Private Sub RemoveEntityInstance()
	
	End Sub

    Private Sub btnInstanceCreate_Click(sender As Object, e As EventArgs) Handles btnInstanceCreate.Click

    End Sub

    Private Sub btnInstanceDuplicate_Click(sender As Object, e As EventArgs) Handles btnInstanceDuplicate.Click

    End Sub

    Private Sub btnInstanceDelete_Click(sender As Object, e As EventArgs) Handles btnInstanceDelete.Click

    End Sub

    Private Sub lstTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTemplates.SelectedIndexChanged

    End Sub

    Private Sub lstInstances_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstInstances.SelectedIndexChanged

    End Sub


End Class