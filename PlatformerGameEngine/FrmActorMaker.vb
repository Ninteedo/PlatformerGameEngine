'Richard Holmes
'24/03/2019
'Actor creator for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmActorMaker

#Region "Initialisation"

    Dim delayTimer As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub FrmActorMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        delayTimer.Stop()
        delayTimer.Dispose()

        renderer = New PRE2 With {.renderPanel = pnlFramePreview}

        LayoutInitialisation()
        'GetFolderLocations()
        result = New Actor(Nothing, renderer)
    End Sub

    Private Sub LayoutInitialisation()
        'moves everything around as appropriate

        'tblControlLayout.Location = New Point(pnlFramePreview.Right + 10, pnlFramePreview.Top)
        'Me.Size = New Size(tblControlLayout.Right + 20, tblControlLayout.Bottom + 50)

        'RefreshControlsEnabled()
    End Sub

#End Region

#Region "Constructors"

    Public result As Actor          'the user's created actor
    Private original As Actor       'used to see if any changes have been made
    'Dim saveLocation As String = ""
    'Dim gameLocation As String = ""


    Public Sub New(Optional actorToModify As Actor = Nothing, Optional renderEngine As PanelRenderEngine2 = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.result = actorToModify
        Me.renderer = renderEngine
    End Sub

    Private Sub UserCloseForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'displays a warning to the user if they have unsaved work when they close the form

        Dim unsavedChanges As Boolean = result = original   'checks if the created actor is identical to the original one

        'if there are unsaved changes then warns the user
        If unsavedChanges Then
            If MsgBox("There are unsaved changes, do wish to close anyway?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Finishing"

    Public userFinished As Boolean = False

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        If result <> original Then
            If MsgBox("Are you sure you wish to cancel with unsaved work?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            Else
                result = original
            End If
        End If

        userFinished = False
        Me.Close()
    End Sub

    Private Sub BtnDone_Click(sender As Object, e As EventArgs) Handles BtnDone.Click
        userFinished = True
        original = result

        Me.Close()
    End Sub

#End Region

#Region "Sprites"

    Private Sub RefreshSpritesList()
        'empties and refills the sprites list

        LstSprites.Items.Clear()

        If Not IsNothing(loadedSprites) Then
            For index As Integer = 0 To UBound(loadedSprites)
                LstSprites.Items.Add(loadedSprites(index).fileName.Remove(0, Len(renderer.spriteFolderLocation)))
            Next
        End If
    End Sub

    Private Sub LstSprites_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstSprites.SelectedIndexChanged
        If LstSprites.SelectedIndex > -1 Then
            DrawSpritePreview(loadedSprites(LstSprites.SelectedIndex))
        Else
            DrawSpritePreview(Nothing)
        End If
    End Sub

#Region "Loading"

    Dim loadedSprites() As Sprite

    Private Sub LoadSprite(fileLocation As String)
        'loads a sprite from a given location

        If IO.File.Exists(fileLocation) Then
            Dim newSprite As New Sprite(fileLocation)
            'Dim fileName As String = newSprite.fileName

            If IsNothing(FindLoadedSprite(newSprite.fileName).fileName) Then      'checks that the same sprite isn't already loaded
                If IsNothing(loadedSprites) = True Then
                    ReDim loadedSprites(0)
                Else
                    ReDim Preserve loadedSprites(UBound(loadedSprites) + 1)
                End If
                loadedSprites(UBound(loadedSprites)) = newSprite
            End If
        End If
    End Sub

    Private Function FindLoadedSprite(fileLocation As String) As Sprite
        'returns a loaded sprite with the given file name if it is already loaded

        If IsNothing(loadedSprites) = False Then
            For index As Integer = 0 To UBound(loadedSprites)
                If loadedSprites(index).fileName = fileLocation Then
                    Return loadedSprites(index)
                End If
            Next index
        End If

        Return Nothing
    End Function

#End Region

#Region "List Manipulation"

    Private Sub BtnSpriteLoad_Click(sender As Object, e As EventArgs) Handles BtnSpriteLoad.Click
        'loads the user selected sprites

        Using openDialog As New OpenFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt", .Multiselect = True, .InitialDirectory = renderer.spriteFolderLocation}
            If openDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                For index As Integer = 0 To UBound(openDialog.FileNames)
                    LoadSprite(openDialog.FileNames(index))
                Next index

                RefreshSpritesList()
            End If
        End Using
    End Sub

    Private Sub NumSpriteIndex_ValueChanged(sender As Object, e As EventArgs) Handles NumSpriteIndex.ValueChanged
        'swaps the locations in loadedSprites of the selected sprite and the entered new sprite index

        If LstSprites.SelectedIndex > -1 Then
            Dim oldIndex As Integer = LstSprites.SelectedIndex
            Dim newIndex As Integer = NumSpriteIndex.Value

            Dim thisSprite As Sprite = loadedSprites(oldIndex)
            loadedSprites(oldIndex) = loadedSprites(newIndex)
            loadedSprites(newIndex) = thisSprite

            LstSprites.SelectedIndex = newIndex
        End If
    End Sub

    Private Sub MenuLstSpriteDelete_Click(sender As Object, e As EventArgs) Handles MenuLstSpriteDelete.Click
        'deletes the sprite that the user has selected

        If LstSprites.SelectedIndex > -1 Then
            For index As Integer = LstSprites.SelectedIndex To UBound(loadedSprites) - 1
                loadedSprites(index) = loadedSprites(index + 1)
            Next
            ReDim Preserve loadedSprites(UBound(loadedSprites) - 1)

            RefreshSpritesList()
        End If
    End Sub

#End Region

#End Region

#Region "Render"
    Dim renderer As PRE2

    Private Sub DrawSpritePreview(spriteToDraw As Sprite)
        'draws the given frame in the preview box

        If Not IsNothing(spriteToDraw) Then
            Dim previewTags() As Tag = {New Tag("name", "SpritePreview")} '= {New Tag("location", {frameToDraw.Centre.ToString})}
            Dim previewActor As New Actor({spriteToDraw}, previewTags, renderer.spriteFolderLocation, New PointF(0, 0)) With {
                .Location = New PointF(spriteToDraw.Centre.X, spriteToDraw.Centre.Y)
            } 'New PointF(renderer.panelCanvasGameArea.ClipRectangle.Width / 2, renderer.panelCanvasGameArea.ClipRectangle.Height / 2))
            'New PointF(frameToDraw.Centre.X, frameToDraw.Centre.Y)
            renderer.renderResolution = spriteToDraw.Dimensions
            LayoutInitialisation()

            renderer.DoGameRender({previewActor})
        Else
            renderer.DoGameRender({})       'renders nothing
        End If
    End Sub

#End Region

#Region "Other Controls"



#End Region






End Class
