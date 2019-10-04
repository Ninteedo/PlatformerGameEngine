'Richard Holmes
'24/03/2019
'Entity creator for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmEntityMaker

#Region "Initialisation"

    Dim delayTimer As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub FrmEntityMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        delayTimer.Stop()
        delayTimer.Dispose()

        renderer = New PRE2 With {.renderPanel = pnlFramePreview}

        LayoutInitialisation()
        GetFolderLocations()
        ent = New PRE2.Entity(Nothing, renderer)
    End Sub

    Private Sub LayoutInitialisation()
        'moves everything around as appropriate

        tblControlLayout.Location = New Point(pnlFramePreview.Right + 10, pnlFramePreview.Top)
        Me.Size = New Size(tblControlLayout.Right + 20, tblControlLayout.Bottom + 50)

        RefreshControlsEnabled()
    End Sub

#End Region

#Region "Save/Load"

    Dim ent As PRE2.Entity          'the user's created entity
    Dim saveLocation As String = ""
    'Dim gameLocation As String = ""

    Private ReadOnly Property EntityString
        Get
            'Return EntityStringHandler.CreateEntityString(ent, renderer.spriteFolderLocation)
            Return ent.ToString(renderer.spriteFolderLocation)
        End Get
    End Property

    Private Sub GetFolderLocations()
        'asks the user to select the game loader file

        MsgBox("Please select the loader file for the game")
        Using openDialog As New OpenFileDialog With {.Filter = "Loader File (*.ldr)|*.ldr", .Title = "Select Loader File"}
            If openDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                If IO.File.Exists(openDialog.FileName) = True Then
                    Dim loaderText As String = PRE2.ReadFile(openDialog.FileName)

                    Dim topLevelFolder As String = openDialog.FileName.Remove(openDialog.FileName.LastIndexOf("\") + 1)
                    renderer.entityFolderLocation = topLevelFolder & renderer.FindProperty(loaderText, "entityFolder")
                    renderer.spriteFolderLocation = topLevelFolder & renderer.FindProperty(loaderText, "spriteFolder")
                Else
                    PRE2.DisplayError("Couldn't find file " & openDialog.FileName)
                    Me.Close()
                End If
            Else
                Me.Close()
            End If
        End Using
    End Sub

    Private Sub BtnOpen_Click(sender As Button, e As EventArgs) Handles btnOpen.Click
        'asks the user to select a .sprt file and reads it

        Using openDialog As New OpenFileDialog With {.Filter = "Entity file (*.ent)|*.ent", .Multiselect = False, .CheckFileExists = True, .InitialDirectory = renderer.entityFolderLocation}
            If openDialog.ShowDialog = DialogResult.OK Then
                ReadEntityFromFile(openDialog.FileName)
            End If
        End Using
    End Sub

    Private Sub BtnSaveAs_Click(sender As Button, e As EventArgs) Handles btnSaveAs.Click
        'asks the user to select a save location, then saves the sprite there and enables the regular save button

        Using saveDialog As New SaveFileDialog With {.Filter = "Entity file (*.ent)|*.ent", .InitialDirectory = renderer.entityFolderLocation}
            If saveDialog.ShowDialog = DialogResult.OK Then
                saveLocation = saveDialog.FileName
                PRE2.WriteFile(saveLocation, EntityString)

                btnSave.Enabled = True
            End If
        End Using
    End Sub

    Private Sub BtnSave_Click(sender As Button, e As EventArgs) Handles btnSave.Click
        'saves the file to the already selected location

        If IO.File.Exists(saveLocation) Then
            PRE2.WriteFile(saveLocation, EntityString)
        Else
            PRE2.DisplayError("Couldn't find file at " & saveLocation)
        End If
    End Sub

    Private Sub ReadEntityFromFile(fileLocation As String)
        'reads the entity stored in a given file

        If IO.File.Exists(fileLocation) = True Then
            Dim fileText As String = PRE2.ReadFile(fileLocation)
            Dim successfulLoad As Boolean = False

            renderer.loadedSprites = Nothing
            ent = EntityStringHandler.ReadEntityString(fileText, renderer, successfulLoad)

            If successfulLoad = True Then
                txtName.Text = ent.name
                saveLocation = fileLocation
            Else
                saveLocation = ""
            End If

            RefreshFramesList()
            RefreshTagsList()
            RefreshSpritesList()
            RefreshControlsEnabled()
        Else
            PRE2.DisplayError("Couldn't find file " & fileLocation)
        End If
    End Sub

    Private Sub UserCloseForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'displays a warning to the user if they have unsaved work when they close the form

        Dim unsavedChanges As Boolean = False

        If Not IsNothing(saveLocation) AndAlso IO.File.Exists(saveLocation) Then
            Dim savedEntityString As String = PRE2.ReadFile(saveLocation)

            If savedEntityString <> EntityString Then
                unsavedChanges = True
            End If
        ElseIf Not IsNothing(renderer.entityFolderLocation) Then     'no level folder location if form isnt finished loading
            unsavedChanges = True
        End If

        'if there are unsaved changes then warns the user
        If unsavedChanges Then
            If MsgBox("There are unsaved changes, do wish to close anyway?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Sprites"

    Private Sub RefreshSpritesList()
        'empties and refills the sprites list

        lstSprites.Items.Clear()

        If IsNothing(renderer.loadedSprites) = False Then
            For index As Integer = 0 To UBound(renderer.loadedSprites)
                lstSprites.Items.Add(renderer.loadedSprites(index).fileName.Remove(0, Len(renderer.spriteFolderLocation)))
            Next
        End If
    End Sub

    Private Sub BtnSpriteLoad_Click(sender As Object, e As EventArgs) Handles btnSpriteLoad.Click
        'loads the user selected sprites

        Using openDialog As New OpenFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt", .Multiselect = True, .InitialDirectory = renderer.spriteFolderLocation}
            If openDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                For index As Integer = 0 To UBound(openDialog.FileNames)
                    renderer.LoadSprite(openDialog.FileNames(index))
                Next index

                RefreshSpritesList()
            End If
        End Using
    End Sub

    Private Sub LstSprites_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSprites.SelectedIndexChanged

        If lstSprites.SelectedIndex > -1 Then
            btnFrameAddSprite.Enabled = True
        Else
            btnFrameAddSprite.Enabled = False
        End If
    End Sub

#End Region

#Region "Render"
    Dim renderer As PRE2

    Private Sub DrawFramePreview(frameToDraw As PRE2.Frame)
        'draws the given frame in the preview box

        If IsNothing(frameToDraw.sprites) = False Then
            Dim previewTags() As PRE2.Tag = {New PRE2.Tag("name", "FramePreviewEntity")} '= {New PRE2.Tag("location", {frameToDraw.Centre.ToString})}
            Dim previewEntity As New PRE2.Entity({frameToDraw}, previewTags, renderer.spriteFolderLocation, New PointF(0, 0)) With {
                .location = New PointF(frameToDraw.Centre.X, frameToDraw.Centre.Y)
            } 'New PointF(renderer.panelCanvasGameArea.ClipRectangle.Width / 2, renderer.panelCanvasGameArea.ClipRectangle.Height / 2))
            'New PointF(frameToDraw.Centre.X, frameToDraw.Centre.Y)

            renderer.renderResolution = frameToDraw.Dimensions
            'renderer.renderResolution = New Size(frameToDraw.Dimensions.Width * renderer.renderScaleFactor, frameToDraw.Dimensions.Height * renderer.renderScaleFactor)
            renderer.renderPixelPerfect = False
            renderer.ResizeRenderWindow()
            LayoutInitialisation()

            renderer.DoGameRender({previewEntity})
        Else
            renderer.DoGameRender({})       'renders nothing
        End If
    End Sub

#End Region

#Region "Frames"

    Private Sub RefreshFramesList()
        'resets frame list and adds them all back

        lstFrames.Items.Clear()

        If IsNothing(ent.Frames) = False Then
            For frameIndex As Integer = 0 To UBound(ent.Frames)
                lstFrames.Items.Add("Frame " & Trim(Str(frameIndex + 1)))
            Next frameIndex
        End If

        RefreshTagsList()
    End Sub

    Private Sub BtnFrameNew_Click(sender As Object, e As EventArgs) Handles btnFrameNew.Click
        'adds a new, empty frame

        If IsNothing(ent.Frames) = True Then
            ReDim ent.Frames(0)
        Else
            ReDim Preserve ent.Frames(UBound(ent.Frames) + 1)
        End If

        ent.Frames(UBound(ent.Frames)) = New PRE2.Frame(Nothing, renderer.spriteFolderLocation)
        RefreshFramesList()
    End Sub

    Private Sub BtnFrameRemove_Click(sender As Object, e As EventArgs) Handles btnFrameRemove.Click
        'removes the selected frame

        Dim removeIndex As Integer = lstFrames.SelectedIndex

        For index As Integer = removeIndex + 1 To UBound(ent.Frames)
            ent.Frames(index - 1) = ent.Frames(index)
        Next index

        'reduces the ent.Frames array length by 1
        If UBound(ent.Frames) > 0 Then
            ReDim Preserve ent.Frames(UBound(ent.Frames) - 1)
        Else
            ent.Frames = Nothing
        End If

        RefreshFramesList()
    End Sub

    Private Sub LstFrames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFrames.SelectedIndexChanged
        'enables or disable the remove frame button

        If lstFrames.SelectedIndex > -1 Then
            btnFrameRemove.Enabled = True
            DrawFramePreview(ent.Frames(lstFrames.SelectedIndex))
            'RefreshFramesList()

            If lstSprites.SelectedIndex > -1 Then
                btnFrameAddSprite.Enabled = True
            Else
                btnFrameAddSprite.Enabled = False
            End If
        Else
            btnFrameRemove.Enabled = False
            btnFrameAddSprite.Enabled = False
        End If
    End Sub

    Private Sub BtnFrameAddSprite_Click(sender As Object, e As EventArgs) Handles btnFrameAddSprite.Click
        'adds the selected sprite and asks the user for the offset

        If lstFrames.SelectedIndex > -1 Then
            Dim spriteIndex As Integer = lstSprites.SelectedIndex
            Dim frameIndex As Integer = lstFrames.SelectedIndex
            Dim offset As Point
            Dim userInput As String = InputBox("Enter offset for current sprite e.g. (10,5)", "Enter Offset", "0,0")
            Dim inputSplit() As String = userInput.Split(",")

            If userInput <> "" Then
                If inputSplit.Length = 2 AndAlso IsNumeric(Trim(inputSplit(0))) = True And IsNumeric(Trim(inputSplit(1))) = True Then
                    offset = New Point(Int(Trim(inputSplit(0))), Int(Trim(inputSplit(1))))
                    ent.Frames(frameIndex).AddSprite(renderer.loadedSprites(spriteIndex), offset)
                    ent.RefreshFramesTag()

                    DrawFramePreview(ent.Frames(frameIndex))
                Else
                    PRE2.DisplayError("Offsets need to be provided in the form x,y e.g. 10,5")
                End If
            End If
        End If

        RefreshFramesList()
    End Sub

#End Region

#Region "Tags"

    Private Sub RefreshTagsList()
        'empties and refills the tags list

        lstTags.Items.Clear()

        If IsNothing(ent.tags) = False Then
            For Each thisTag As PRE2.Tag In ent.tags
                If IsNothing(thisTag.name) = False Then
                    lstTags.Items.Add(thisTag.ToString)
                End If
            Next
        End If

        'ReloadFrames()
    End Sub

    Private Sub BtnTagsNew_Click(sender As Object, e As EventArgs) Handles btnTagsNew.Click
        'opens FrmTagMaker to allow the user to create tags

        Using tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()

            If tagMaker.userFinished = True Then
                If IsNothing(ent.tags) = True Then
                    ReDim ent.tags(0)
                Else
                    ReDim Preserve ent.tags(UBound(ent.tags) + 1)
                End If

                ent.tags(UBound(ent.tags)) = tagMaker.CreatedTag

                'lstTags.Items.Add(tagMaker.createdTag.name)
                RefreshTagsList()
            End If
        End Using
    End Sub

    Private Sub BtnTagsEdit_Click(sender As Object, e As EventArgs) Handles btnTagsEdit.Click
        'opens FrmTagMaker with the selected tag already loaded

        Dim tagIndex As Integer = lstTags.SelectedIndex

        If tagIndex > -1 Then
            Using tagMaker As New FrmTagMaker(ent.tags(tagIndex))

                tagMaker.ShowDialog()

                If tagMaker.userFinished = True Then
                    ent.SetTag(tagIndex, tagMaker.CreatedTag)
                    RefreshTagsList()
                    'lstTags.Items(tagIndex) = tagMaker.createdTag.name
                End If
            End Using
        End If
    End Sub

    Private Sub BtnTagRemove_Click(sender As Object, e As EventArgs) Handles btnTagRemove.Click
        'removes the currently selected tag

        If lstTags.SelectedIndex > -1 Then
            If UBound(ent.tags) > lstTags.SelectedIndex Then
                For index As Integer = lstTags.SelectedIndex To UBound(ent.tags) - 1
                    ent.tags(index) = ent.tags(index + 1)
                Next index
            Else
                If UBound(ent.tags) > 0 Then
                    ReDim Preserve ent.tags(UBound(ent.tags) - 1)
                Else
                    ent.tags = Nothing
                End If
            End If

            RefreshTagsList()
        End If
    End Sub

    Private Sub LstTags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTags.SelectedIndexChanged
        'enables or disables the edit tag button

        If lstTags.SelectedIndex > -1 Then
            btnTagsEdit.Enabled = True
            btnTagRemove.Enabled = True
        Else
            btnTagsEdit.Enabled = False
            btnTagRemove.Enabled = False
        End If
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.LostFocus
        'changes the name of the entity if the name is valid

        Dim newName As String = txtName.Text

        If Not IsNothing(newName) Then
            ent.name = AddQuotes(newName)
            RefreshTagsList()
        Else    'no name provided so restores previous name
            txtName.Text = RemoveQuotes(ent.name)
        End If
    End Sub

#End Region

#Region "Other Controls"

    Private Sub BtnRedraw_Click(sender As Object, e As EventArgs) Handles btnRedraw.Click
        If IsNothing(ent.Frames) = False Then
            If lstFrames.SelectedIndex > -1 AndAlso IsNothing(ent.Frames(lstFrames.SelectedIndex)) = False Then
                DrawFramePreview(ent.Frames(lstFrames.SelectedIndex))
            End If
        Else
            renderer.DoGameRender({})       'if there are no frames then clears the render
        End If
    End Sub

#End Region

#Region "General Procedures"

    Private Sub AnySelectionChanged(sender As Object, e As EventArgs) Handles _
        lstTags.SelectedIndexChanged, lstFrames.SelectedIndexChanged, lstSprites.SelectedIndexChanged

        RefreshControlsEnabled()
    End Sub

    Private Sub RefreshControlsEnabled()
        'enables or disables controls as appropriate

        Dim tagSelected As Boolean = lstTags.SelectedIndex > -1
        Dim frameSelected As Boolean = lstFrames.SelectedIndex > -1
        Dim spriteSelected As Boolean = lstSprites.SelectedIndex > -1
        Dim saveLocationSelected As Boolean = Not IsNothing(saveLocation) AndAlso IO.File.Exists(saveLocation)

        Dim controlsDefaultDisabled() As Control = {btnTagsEdit, btnTagRemove, btnFrameRemove, btnFrameAddSprite, btnSave, btnRedraw}
        Dim controlsDefaultEnabled() As Control = {btnTagsNew, btnFrameNew, btnSpriteLoad, txtName, btnSaveAs, btnOpen}

        For Each ctrl As Control In controlsDefaultDisabled
            ctrl.Enabled = False
        Next
        For Each ctrl As Control In controlsDefaultEnabled
            ctrl.Enabled = True
        Next

        If tagSelected Then
            btnTagsEdit.Enabled = True
            btnTagRemove.Enabled = True
        End If
        If frameSelected Then
            btnFrameRemove.Enabled = True
            btnRedraw.Enabled = True
            If spriteSelected Then
                btnFrameAddSprite.Enabled = True
            End If
        End If

        If saveLocationSelected Then
            btnSave.Enabled = True
        End If
    End Sub

#End Region
End Class