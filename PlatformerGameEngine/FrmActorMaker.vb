Imports PlatformerGameEngine.My.Resources

Public Class FrmActorMaker

#Region "Constructors"

    Public Sub New(actorToModify As Actor)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _renderer = New RenderEngine(PnlPreview)
        If Not IsNothing(actorToModify) Then
            CreatedActor = actorToModify.Clone()
        Else
            CreatedActor = New Actor
        End If
        _originalString = CreatedActor.ToString
    End Sub

#End Region

#Region "Finishing"

    Public Finished As Boolean = False      'remains false until the user presses done
    ReadOnly _originalString As String       'used to see if any changes have been made

    Private Sub BtnCancel_Click(sender As Button, e As EventArgs) Handles BtnCancel.Click
        'displays a warning if there are unsaved changes, giving the user the option to cancel the cancel
        If CreatedActor.ToString <> _originalString Then
            If MsgBox("Are you sure you wish to cancel with unsaved work?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If

        Close()
    End Sub

    Private Sub BtnDone_Click(sender As Button, e As EventArgs) Handles BtnDone.Click
        Finished = True
        Close()
    End Sub

    Private Sub UserCloseForm(sender As FrmActorMaker, e As FormClosingEventArgs) Handles Me.FormClosing
        'displays a warning to the user if they have unsaved work when they close the form

        'checks if user hasn't finished the created actor is identical to the original one
        If Not Finished And CreatedActor.ToString = _originalString Then
            If MsgBox("There are unsaved changes, do wish to close anyway?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Actor"

    Dim _createdActor As Actor          'the user's created actor

    Public Property CreatedActor As Actor
        Get
            Return _createdActor
        End Get
        Set
            _createdActor = Value
            RefreshSpritesList()
        End Set
    End Property

#Region "Sprites"

    Private Sub RefreshSpritesList()
        'empties and refills the sprites list

        Dim items() As String = {}
        If Not IsNothing(ActorSprites) Then
            ReDim items(UBound(ActorSprites))
            For index As Integer = 0 To UBound(ActorSprites)
                items(index) = "Sprite " & index
            Next
        End If

        RefreshList(LstSprites, items)
    End Sub

    Private Sub LstSprites_SelectedIndexChanged(sender As ListBox, e As EventArgs) Handles LstSprites.SelectedIndexChanged
        If LstSprites.SelectedIndex > -1 Then
            DrawSpritePreview(ActorSprites(LstSprites.SelectedIndex))
        Else
            DrawSpritePreview(Nothing)
        End If
    End Sub

#Region "Loading"

    Private Property ActorSprites As Sprite()
        Get
            Return CreatedActor.Sprites
        End Get
        Set
            CreatedActor.Sprites = Value

            RefreshSpritesList()
        End Set
    End Property

    Private Sub LoadSprite(fileLocation As String)
        'loads a sprite from a given location

        If IO.File.Exists(fileLocation) Then
            Dim newSprite As New Sprite(fileLocation)
            ActorSprites = InsertItem(ActorSprites, newSprite)
        End If
    End Sub

#End Region

#Region "List Manipulation"

    Private Sub BtnSpriteLoad_Click(sender As Button, e As EventArgs) Handles BtnSpriteLoad.Click
        'loads the user selected sprites

        Using openDialog As New OpenFileDialog With {.Filter = SpriteFileFilter, .Multiselect = True}
            If openDialog.ShowDialog = DialogResult.OK Then
                For index As Integer = 0 To UBound(openDialog.FileNames)
                    LoadSprite(openDialog.FileNames(index))
                Next index
            End If
        End Using
    End Sub

    Private Sub NumSpriteIndex_ValueChanged(sender As NumericUpDown, e As EventArgs) Handles NumSpriteIndex.ValueChanged
        'swaps the locations in loadedSprites of the selected sprite and the entered new sprite index

        If LstSprites.SelectedIndex > -1 Then
            Dim oldIndex As Integer = LstSprites.SelectedIndex
            Dim newIndex As Integer = NumSpriteIndex.Value

            Dim thisSprite As Sprite = ActorSprites(oldIndex)
            ActorSprites = RemoveItem(ActorSprites, oldIndex)       'removes sprite from old index
            ActorSprites = InsertItem(ActorSprites, thisSprite, newIndex)   'inserts sprite to new index

            RefreshSpritesList()
        End If
    End Sub

    Private Sub MenuLstSpriteDelete_Click(sender As ToolStripMenuItem, e As EventArgs) Handles MenuLstSpriteDelete.Click
        'deletes the sprite that the user has selected

        If LstSprites.SelectedIndex > -1 Then
            ActorSprites = RemoveItem(ActorSprites, LstSprites.SelectedIndex)
        End If
    End Sub

#End Region

#End Region

#Region "Render"

    ReadOnly _renderer As RenderEngine

    Private Sub DrawSpritePreview(spriteToDraw As Sprite)
        'draws the given frame in the preview box

        If Not IsNothing(spriteToDraw) Then
            Dim previewActor As New Actor() With {
                .Name = "SpritePreview",
                .Sprites = {spriteToDraw}
                }
            _renderer.RenderResolution = spriteToDraw.Dimensions
            _renderer.DoGameRender({previewActor})
        Else
            _renderer.DoGameRender({})       'renders nothing
        End If
    End Sub

#End Region

#End Region

End Class
