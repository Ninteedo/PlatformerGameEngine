'Richard Holmes
'24/03/2019
'Entity creator for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmEntityMaker

    Dim delayTimer As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub FrmEntityMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        delayTimer.Stop()

        renderer = New PRE2 With {.renderPanel = pnlFramePreview}

        LayoutInitialisation()

        GetFolderLocations()
    End Sub

    Private Sub LayoutInitialisation()
        'moves everything around as appropriate

        tblControlLayout.Location = New Point(pnlFramePreview.Right + 10, pnlFramePreview.Top)
        Me.Size = New Size(tblControlLayout.Right + 20, tblControlLayout.Bottom + 50)
    End Sub

    'save load

    Dim saveLocation As String
    Dim gameLocation As String = ""

    Private ReadOnly Property EntityString
        Get
            Return EntityStringHandler.CreateEntityString(ent, renderer.spriteFolderLocation)
        End Get
    End Property

    Private Sub GetFolderLocations()
        'asks the user to select the game loader file

        Dim openDialog As New OpenFileDialog With {.Filter = "Loader File (*.ldr)|*.ldr", .Title = "Select Loader File"}
        If openDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            If IO.File.Exists(openDialog.FileName) = True Then
                Dim loaderText As String = PRE2.ReadFile(openDialog.FileName)

                Dim topLevelFolder As String = openDialog.FileName.Remove(openDialog.FileName.LastIndexOf("\") + 1)
                renderer.entityFolderLocation = topLevelFolder & renderer.FindProperty(loaderText, "entityFolder")
                renderer.spriteFolderLocation = topLevelFolder & renderer.FindProperty(loaderText, "spriteFolder")
            Else
                PRE2.DisplayError("Couldn't find file " & openDialog.FileName)
            End If
        End If
    End Sub

    Private Sub btnOpen_Click(sender As Button, e As EventArgs) Handles btnOpen.Click
        'asks the user to select a .sprt file and reads it

        Dim openDialog As New OpenFileDialog With {.Filter = "Entity file (*.ent)|*.ent", .Multiselect = False, .CheckFileExists = True, .InitialDirectory = renderer.entityFolderLocation}

        If openDialog.ShowDialog = DialogResult.OK Then
            saveLocation = openDialog.FileName
            ReadEntityFromFile(saveLocation)

            btnSave.Enabled = True
        End If
    End Sub

    Private Sub btnSaveAs_Click(sender As Button, e As EventArgs) Handles btnSaveAs.Click
        'asks the user to select a save location, then saves the sprite there and enables the regular save button

        Dim saveDialog As New SaveFileDialog With {.Filter = "Entity file (*.ent)|*.ent", .InitialDirectory = renderer.entityFolderLocation}

        If saveDialog.ShowDialog = DialogResult.OK Then
            saveLocation = saveDialog.FileName
            PRE2.WriteFile(saveLocation, EntityString)

            btnSave.Enabled = True
        End If
    End Sub

    Private Sub btnSave_Click(sender As Button, e As EventArgs) Handles btnSave.Click
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

            ent = EntityStringHandler.ReadEntityString(fileText, renderer)

            txtName.Text = ent.name
            RefreshFramesList()
            RefreshTagsList()
            RefreshSpritesList()
        Else
            PRE2.DisplayError("Couldn't find file " & fileLocation)
        End If
    End Sub


    Dim ent As PRE2.Entity          'the user's created entity


    'sprites

    Private Sub RefreshSpritesList()
        'empties and refills the sprites list

        lstSprites.Items.Clear()

        If IsNothing(renderer.loadedSprites) = False Then
            For index As Integer = 0 To UBound(renderer.loadedSprites)
                lstSprites.Items.Add(renderer.loadedSprites(index).fileName.Remove(0, Len(renderer.spriteFolderLocation)))
            Next
        End If
    End Sub

    Private Sub btnSpriteLoad_Click(sender As Object, e As EventArgs) Handles btnSpriteLoad.Click
        'loads the user selected sprites

        Dim openDialog As New OpenFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt", .Multiselect = True, .InitialDirectory = renderer.spriteFolderLocation}

        If openDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            For index As Integer = 0 To UBound(openDialog.FileNames)
                renderer.LoadSprite(openDialog.FileNames(index))
            Next index

            RefreshSpritesList()
        End If
    End Sub

    Private Sub lstSprites_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSprites.SelectedIndexChanged

        If lstSprites.SelectedIndex > -1 Then
            btnFrameAddSprite.Enabled = True
        Else
            btnFrameAddSprite.Enabled = False
        End If
    End Sub


    'frames

    'Dim frames() As PRE2.Frame
    Dim renderer As PRE2

    Private Sub DrawFramePreview(frameToDraw As PRE2.Frame)
        'draws the given frame in the preview box

        If IsNothing(frameToDraw.sprites) = False Then
            Dim previewEntity As New PRE2.Entity({frameToDraw}, {}, New PointF(0, 0)) 'New PointF(renderer.panelCanvasGameArea.ClipRectangle.Width / 2, renderer.panelCanvasGameArea.ClipRectangle.Height / 2))

            Dim newPanelDimensions As New Size(frameToDraw.Dimensions.Width * renderer.renderScale, frameToDraw.Dimensions.Height * renderer.renderScale)
            pnlFramePreview.MaximumSize = newPanelDimensions
            pnlFramePreview.MinimumSize = newPanelDimensions
            pnlFramePreview.Size = newPanelDimensions
            LayoutInitialisation()

            renderer.DoGameRender({previewEntity})
        Else
            renderer.DoGameRender({})       'renders nothing
        End If
    End Sub

    Private Sub RefreshFramesList()
        'resets frame list and adds them all back

        lstFrames.Items.Clear()

        If IsNothing(ent.frames) = False Then
            For frameIndex As Integer = 0 To UBound(ent.frames)
                lstFrames.Items.Add("Frame " & Trim(Str(frameIndex + 1)))
            Next frameIndex
        End If
    End Sub

    Private Sub btnFrameNew_Click(sender As Object, e As EventArgs) Handles btnFrameNew.Click
        'adds a new, empty frame

        If IsNothing(ent.frames) = True Then
            ReDim ent.frames(0)
        Else
            ReDim Preserve ent.frames(UBound(ent.frames) + 1)
        End If

        ent.frames(UBound(ent.frames)) = New PRE2.Frame
        RefreshFramesList()
    End Sub

    Private Sub btnFrameRemove_Click(sender As Object, e As EventArgs) Handles btnFrameRemove.Click
        'removes the selected frame

        Dim removeIndex As Integer = lstFrames.SelectedIndex

        For index As Integer = removeIndex + 1 To UBound(ent.frames)
            ent.frames(index - 1) = ent.frames(index)
        Next index

        'reduces the ent.frames array length by 1
        If UBound(ent.frames) > 0 Then
            ReDim Preserve ent.frames(UBound(ent.frames) - 1)
        Else
            ent.frames = Nothing
        End If

        RefreshFramesList()
    End Sub

    Private Sub lstFrames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFrames.SelectedIndexChanged
        'enables or disable the remove frame button

        If lstFrames.SelectedIndex > -1 Then
            btnFrameRemove.Enabled = True
            DrawFramePreview(ent.frames(lstFrames.SelectedIndex))
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

    Private Sub btnFrameAddSprite_Click(sender As Object, e As EventArgs) Handles btnFrameAddSprite.Click
        'adds the selected sprite and asks the user for the offset

        Dim spriteIndex As Integer = lstSprites.SelectedIndex
        Dim frameIndex As Integer = lstFrames.SelectedIndex
        Dim offset As Point
        Dim userInput As String = InputBox("Enter offset for current sprite e.g. (10,5)", "Enter Offset", "0,0")
        Dim inputSplit() As String = userInput.Split(",")

        If userInput <> "" Then
            If inputSplit.Length = 2 AndAlso IsNumeric(Trim(inputSplit(0))) = True And IsNumeric(Trim(inputSplit(1))) = True Then
                offset = New Point(Int(Trim(inputSplit(0))), Int(Trim(inputSplit(1))))
                ent.frames(frameIndex).AddSprite(renderer.loadedSprites(spriteIndex), offset)

                DrawFramePreview(ent.frames(frameIndex))
            Else
                PRE2.DisplayError("Offsets need to be provided in the form x,y e.g. 10,5")
            End If
        End If
    End Sub




    'tags

    Private Sub RefreshTagsList()
        'empties and refills the tags list

        lstTags.Items.Clear()

        If IsNothing(ent.tags) = False Then
            For Each thisTag As PRE2.Tag In ent.tags
                If IsNothing(thisTag.name) = False Then
                    lstTags.Items.Add(thisTag.name)
                End If
            Next
        End If
    End Sub

    Private Sub btnTagsNew_Click(sender As Object, e As EventArgs) Handles btnTagsNew.Click
        'opens FrmTagMaker to allow the user to create tags

        Dim tagMaker As New FrmTagMaker
        tagMaker.ShowDialog()

        If tagMaker.userFinished = True Then
            If IsNothing(ent.tags) = True Then
                ReDim ent.tags(0)
            Else
                ReDim Preserve ent.tags(UBound(ent.tags) + 1)
            End If

            ent.tags(UBound(ent.tags)) = tagMaker.createdTag

            'lstTags.Items.Add(tagMaker.createdTag.name)
            RefreshTagsList()
        End If
    End Sub

    Private Sub btnTagsEdit_Click(sender As Object, e As EventArgs) Handles btnTagsEdit.Click
        'opens FrmTagMaker with the selected tag already loaded

        Dim tagIndex As Integer = lstTags.SelectedIndex

        If tagIndex > -1 Then
            Dim tagMaker As New FrmTagMaker(ent.tags(tagIndex))
            
            tagMaker.ShowDialog()

            If tagMaker.userFinished = True Then
                ent.tags(tagIndex) = tagMaker.createdTag
                RefreshTagsList()
                'lstTags.Items(tagIndex) = tagMaker.createdTag.name
            End If
        End If
    End Sub

    Private Sub btnTagRemove_Click(sender As Object, e As EventArgs) Handles btnTagRemove.Click
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

    Private Sub lstTags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTags.SelectedIndexChanged
        'enables or disables the edit tag button

        If lstTags.SelectedIndex > -1 Then
            btnTagsEdit.Enabled = True
            btnTagRemove.Enabled = True
        Else
            btnTagsEdit.Enabled = False
            btnTagRemove.Enabled = False
        End If
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.LostFocus
        'changes the name of the entity if the name is valid

        Dim newName As String = txtName.Text

        If Not IsNothing(newName) Then
            ent.name = newName
            RefreshTagsList()
        Else    'no name provided so restores previous name
            txtName.Text = ent.name
        End If
    End Sub


    'other

    Private Sub btnRedraw_Click(sender As Object, e As EventArgs) Handles btnRedraw.Click
        If IsNothing(ent.frames) = False Then
            If lstFrames.SelectedIndex > -1 AndAlso IsNothing(ent.frames(lstFrames.SelectedIndex)) = False Then
                DrawFramePreview(ent.frames(lstFrames.SelectedIndex))
            End If
        Else
            renderer.DoGameRender({})       'if there are no frames then clears the render
        End If
    End Sub


End Class