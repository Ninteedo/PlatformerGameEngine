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

        rendererEngine = New PRE2
        RefreshPanelCanvas()

        LayoutInitialisation()

        GetFolderLocations()
    End Sub

    Private Sub RefreshPanelCanvas()
        Dim panelCanvas As New PaintEventArgs(pnlFramePreview.CreateGraphics, New Rectangle(New Point(0, 0), pnlFramePreview.Size))
        rendererEngine.panelCanvasGameArea = panelCanvas
    End Sub

    Private Sub LayoutInitialisation()
        'moves everything around as appropriate

        pnlFramePreview.Location = New Point(5, 5)

        btnOpen.Location = New Point(pnlFramePreview.Right + 10, 5)
        btnSaveAs.Location = New Point(btnOpen.Right + 5, 5)
        btnSave.Location = New Point(btnSaveAs.Right + 5, 5)

        btnRedraw.Location = New Point(btnSave.Right + 20, 5)

        lblTagListTitle.Location = New Point(btnOpen.Left, btnOpen.Bottom + 10)
        lstTags.Location = New Point(lblTagListTitle.Left, lblTagListTitle.Bottom + 5)
        btnTagsNew.Location = New Point(lstTags.Left, lstTags.Bottom + 5)
        btnTagsEdit.Location = New Point(btnTagsNew.Left, btnTagsNew.Bottom + 5)

        lblFrameListTitle.Location = New Point(lstTags.Right + 5, lblTagListTitle.Top)
        lstFrames.Location = New Point(lblFrameListTitle.Left, lblFrameListTitle.Bottom + 5)
        btnFrameNew.Location = New Point(lstFrames.Left, lstFrames.Bottom + 5)
        btnFrameRemove.Location = New Point(btnFrameNew.Left, btnFrameNew.Bottom + 5)
        btnFrameAddSprite.Location = New Point(btnFrameRemove.Left, btnFrameRemove.Bottom + 5)
        lblFrameIndex.Location = New Point(btnFrameAddSprite.Left, btnFrameAddSprite.Bottom + 5)
        numFrameIndex.Location = New Point(lblFrameIndex.Right + 5, lblFrameIndex.Top)

        lblSpriteListTitle.Location = New Point(lstFrames.Right + 5, lblFrameListTitle.Top)
        lstSprites.Location = New Point(lblSpriteListTitle.Left, lblSpriteListTitle.Bottom + 5)
        btnSpriteLoad.Location = New Point(lstSprites.Left, lstSprites.Bottom + 5)
    End Sub

    'save load

    Dim saveLocation As String
    Dim gameLocation As String = ""

    Private ReadOnly Property EntityString
        Get
            'Dim frames() As PRE2.Frame
            'Dim tags() As PRE2.Tag

            Dim ent As New PRE2.Entity With {
                .frames = frames,
                .tags = tags
            }

            Return EntityStringHandler.CreateEntityString(ent)
        End Get
    End Property

    Private Sub GetFolderLocations()
        'asks the user to select the location of the folders
        'TRY TO REMOVE THE NEED FOR THIS

        MsgBox("Please select the top level folder for the game", MsgBoxStyle.OkOnly)
        Dim openDialog As New FolderBrowserDialog

        Do While gameLocation = Nothing OrElse gameLocation = ""
            'PRE2.DisplayError("Game folder location MUST be selected")
            openDialog.ShowDialog()

            gameLocation = openDialog.SelectedPath

            If gameLocation = Nothing OrElse gameLocation = "" Then
                If MsgBox("Game folder location MUST be selected", MsgBoxStyle.RetryCancel) = MsgBoxResult.Cancel Then
                    End
                End If
            End If
        Loop

        MsgBox("Please select the sprite folder", MsgBoxStyle.OkOnly)

        Do While rendererEngine.spriteFolderLocation = Nothing OrElse rendererEngine.spriteFolderLocation = ""
            'PRE2.DisplayError("Game folder location MUST be selected")
            openDialog.ShowDialog()

            rendererEngine.spriteFolderLocation = openDialog.SelectedPath

            If rendererEngine.spriteFolderLocation = Nothing OrElse rendererEngine.spriteFolderLocation = "" Then
                If MsgBox("Sprite folder location MUST be selected", MsgBoxStyle.RetryCancel) = MsgBoxResult.Cancel Then
                    End
                End If
            End If
        Loop

        MsgBox("Please select the entity folder", MsgBoxStyle.OkOnly)

        Do While rendererEngine.entityFolderLocation = Nothing OrElse rendererEngine.entityFolderLocation = ""
            'PRE2.DisplayError("Game folder location MUST be selected")
            openDialog.ShowDialog()

            rendererEngine.entityFolderLocation = openDialog.SelectedPath

            If rendererEngine.entityFolderLocation = Nothing OrElse rendererEngine.entityFolderLocation = "" Then
                If MsgBox("Entity folder location MUST be selected", MsgBoxStyle.RetryCancel) = MsgBoxResult.Cancel Then
                    End
                End If
            End If
        Loop
    End Sub

    Private Sub btnOpen_Click(sender As Button, e As EventArgs) Handles btnOpen.Click
        'asks the user to select a .sprt file and reads it

        Dim openDialog As New OpenFileDialog With {.Filter = "Entity file (*.ent)|*.ent", .Multiselect = False, .CheckFileExists = True}

        If openDialog.ShowDialog = DialogResult.OK Then
            saveLocation = openDialog.FileName
            ReadEntityFromFile(saveLocation)

            btnSave.Enabled = True
        End If
    End Sub

    Private Sub btnSaveAs_Click(sender As Button, e As EventArgs) Handles btnSaveAs.Click
        'asks the user to select a save location, then saves the sprite there and enables the regular save button

        Dim saveDialog As New SaveFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt"}

        If saveDialog.ShowDialog = DialogResult.OK Then
            saveLocation = saveDialog.FileName
            SaveTextToFile(EntityString, saveLocation)

            btnSave.Enabled = True
        End If
    End Sub

    Private Sub btnSave_Click(sender As Button, e As EventArgs) Handles btnSave.Click
        'saves the file to the already selected location

        If IO.File.Exists(saveLocation) Then
            SaveTextToFile(EntityString, saveLocation)
        Else
            PRE2.DisplayError("Couldn't find file at " & saveLocation)
        End If
    End Sub

    Private Sub SaveTextToFile(text As String, fileLocation As String)
        Dim writer As New IO.StreamWriter(fileLocation)
        Dim toWrite As String = text

        For Each c As Char In toWrite
            writer.Write(c)
        Next c

        writer.Close()
    End Sub

    Private Sub ReadEntityFromFile(fileLocation As String)
        'reads the entity stored in a given file

        If IO.File.Exists(fileLocation) = True Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd

            reader.Close()

            ent = EntityStringHandler.ReadEntityString(fileText, rendererEngine, rendererEngine.spriteFolderLocation)

            frames = ent.frames
            tags = ent.tags
        Else
            PRE2.DisplayError("Couldn't find file " & fileLocation)
        End If
    End Sub


    Dim ent As PRE2.Entity          'the user's created entity

    
    'sprites

    Dim sprites() As PRE2.Sprite

    Private Sub btnSpriteLoad_Click(sender As Object, e As EventArgs) Handles btnSpriteLoad.Click
        'loads the user selected sprites

        Dim openDialog As New OpenFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt", .Multiselect = True}

        If openDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            For index As Integer = 0 To UBound(openDialog.FileNames)
                rendererEngine.LoadSprite(openDialog.FileNames(index))
                lstSprites.Items.Add(rendererEngine.loadedSprites(UBound(rendererEngine.loadedSprites)).fileName)       'THIS WILL CAUSE LOGIC ERRORS
            Next index
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

    Dim frames() As PRE2.Frame
    Dim rendererEngine As PRE2

    Private Sub DrawFramePreview(frameToDraw As PRE2.Frame)
        'draws the given frame in the preview box

        If IsNothing(frameToDraw.sprites) = False Then
            Dim previewEntity As New PRE2.Entity({frameToDraw}, {}, New PointF(0, 0)) 'New PointF(rendererEngine.panelCanvasGameArea.ClipRectangle.Width / 2, rendererEngine.panelCanvasGameArea.ClipRectangle.Height / 2))

            ReDim rendererEngine.entities(0)
            rendererEngine.entities(0) = previewEntity

            Dim newPanelDimensions As New Size(frameToDraw.Dimensions.Width * rendererEngine.renderScale, frameToDraw.Dimensions.Height * rendererEngine.renderScale)
            pnlFramePreview.MaximumSize = newPanelDimensions
            pnlFramePreview.MinimumSize = newPanelDimensions
            pnlFramePreview.Size = newPanelDimensions
            LayoutInitialisation()

            RefreshPanelCanvas()

            rendererEngine.DoGameRender()
        End If
    End Sub

    Private Sub RefreshFramesList()
        'resets frame list and adds them all back

        Dim previousSelectedIndex As Integer = lstFrames.SelectedIndex
        lstFrames.Items.Clear()

        For frameIndex As Integer = 0 To UBound(frames)
            lstFrames.Items.Add("Frame " & Trim(Str(frameIndex + 1)))
        Next frameIndex

        lstFrames.SelectedIndex = previousSelectedIndex
    End Sub

    Private Sub btnFrameNew_Click(sender As Object, e As EventArgs) Handles btnFrameNew.Click
        'adds a new, empty frame

        Try
            ReDim Preserve frames(UBound(frames) + 1)
        Catch ex As Exception
            ReDim frames(0)
        End Try

        frames(UBound(frames)) = New PRE2.Frame
        lstFrames.Items.Add("Frame " & frames.Length)
    End Sub

    Private Sub btnFrameRemove_Click(sender As Object, e As EventArgs) Handles btnFrameRemove.Click
        'removes the selected frame

        Dim removeIndex As Integer = lstFrames.SelectedIndex

        For index As Integer = removeIndex + 1 To UBound(frames)
            frames(index - 1) = frames(index)
        Next index

        'reduces the frames array length by 1
        If UBound(frames) > 0 Then
            ReDim Preserve frames(UBound(frames) - 1)
        Else
            frames = Nothing
        End If

        lstFrames.Items.RemoveAt(lstFrames.Items.Count - 1)
    End Sub

    Private Sub lstFrames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFrames.SelectedIndexChanged
        'enables or disable the remove frame button

        If lstFrames.SelectedIndex > -1 Then
            btnFrameRemove.Enabled = True
            DrawFramePreview(frames(lstFrames.SelectedIndex))
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
                frames(frameIndex).AddSprite(rendererEngine.loadedSprites(spriteIndex), offset)

                DrawFramePreview(frames(frameIndex))
            Else
                PRE2.DisplayError("Offsets need to be provided in the form x,y e.g. 10,5")
            End If
		End If
    End Sub

    'tags

    Dim tags() As PRE2.Tag

    Private Sub btnTagsNew_Click(sender As Object, e As EventArgs) Handles btnTagsNew.Click
        'opens FrmTagMaker to allow the user to create tags

        Dim tagMaker As New FrmTagMaker
        tagMaker.ShowDialog()

        If tagMaker.userFinished = True Then
            Try
                ReDim Preserve tags(UBound(tags) + 1)
            Catch ex As Exception
                ReDim tags(0)
            End Try

            tags(UBound(tags)) = tagMaker.createdTag

            lstTags.Items.Add(tagMaker.createdTag.name)
        End If
    End Sub

    Private Sub btnTagsEdit_Click(sender As Object, e As EventArgs) Handles btnTagsEdit.Click
        'opens FrmTagMaker with the selected tag already loaded

        Dim tagIndex As Integer = lstTags.SelectedIndex

        If tagIndex > -1 Then
            Dim tagMaker As New FrmTagMaker With {
                .createdTag = tags(tagIndex)
            }

            For argIndex As Integer = 0 To UBound(tagMaker.createdTag.args)
                Dim currentArg As Object = tagMaker.createdTag.args(argIndex)
                Dim dataTypeIndex As Integer

                Try
                    Dim test As PRE2.Tag = currentArg
                    dataTypeIndex = 2   'tag
                Catch ex As Exception
                    If IsNumeric(currentArg) = True Then
                        dataTypeIndex = 0   'number
                    Else
                        dataTypeIndex = 1   'text
                    End If
                End Try

                tagMaker.AddArgument(currentArg, dataTypeIndex)
            Next argIndex

            tagMaker.txtName.Text = tagMaker.createdTag.name
            tagMaker.ShowDialog()

            If tagMaker.userFinished = True Then
                tags(tagIndex) = tagMaker.createdTag
                lstTags.Items(tagIndex) = tagMaker.createdTag.name
            End If
        End If
    End Sub

    Private Sub lstTags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTags.SelectedIndexChanged
        'enables or disables the edit tag button

        If lstTags.SelectedIndex > -1 Then
            btnTagsEdit.Enabled = True
        Else
            btnTagsEdit.Enabled = False
        End If
    End Sub

    Private Sub btnRedraw_Click(sender As Object, e As EventArgs) Handles btnRedraw.Click
        If IsNothing(frames(lstFrames.SelectedIndex)) = False Then
            DrawFramePreview(frames(lstFrames.SelectedIndex))
        End If
    End Sub
End Class