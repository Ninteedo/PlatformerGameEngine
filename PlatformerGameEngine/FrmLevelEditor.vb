'Richard Holmes
'29/03/2019
'Level editor for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmLevelEditor

#Region "Initialisation"
    'initialisation

    Dim delayTimer As New Timer With {.Interval = 1, .Enabled = False}

    Private Sub FrmLevelEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        delayTimer.Stop()

        renderer = New PRE2 With {.renderPanel = pnlRender}

        LayoutInitialisation()
        'ControlInitialisation()
        LoadInitialisation()
        RefreshControlsEnabled()
    End Sub

    Private Sub LayoutInitialisation()
        tblControlsOverall.Location = New Point(pnlRender.Right + 10, pnlRender.Top)
        tblLevel.Location = New Point(pnlRender.Left, pnlRender.Bottom + 10)

        Me.Size = New Size(tblControlsOverall.Right + 20, tblControlsOverall.Bottom + 40)

        'flwSaveLoad.Location = New Point(pnlRender.Right + 10, pnlRender.Top)
        'tblEntities.Location = New Point(flwSaveLoad.Left, flwSaveLoad.Bottom + 5)
        'tblTagsSummary.Location = New Point(tblEntities.Right + 10, tblEntities.Top)
        'tblTagsDetailed.Location = New Point(tblTagsSummary.Left, tblTagsSummary.Bottom + 5)
    End Sub

#End Region

#Region "Save/Load"
    'save load

    Dim levelSaveLocation As String = Nothing

    Private Sub LoadInitialisation()
        'gets the folder locations from the loader file

        Using openDialog As New OpenFileDialog With {.Filter = "Loader file (*.ldr)|*.ldr", .Multiselect = False}
            MsgBox("Please select the loader file for the game")

            If openDialog.ShowDialog() = DialogResult.OK Then
                Dim loaderFileText As String = PRE2.ReadFile(openDialog.FileName)

                'loads locations of each folder
                Dim topLevelFolder As String = openDialog.FileName.Remove(openDialog.FileName.LastIndexOf("\") + 1)
                renderer.levelFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "levelFolder")
                renderer.entityFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "entityFolder")
                renderer.spriteFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "spriteFolder")
                'renderer.roomFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "roomFolder")
            Else
                Me.Close()     'might need to change this
            End If
        End Using
    End Sub



    'level save/load

    Private Sub LoadLevel(fileLocation As String)
        'loads a level and sets the interface up

        If IO.File.Exists(fileLocation) Then
            levelSaveLocation = fileLocation
            'thisLevel = FrmGame.LoadLevelFile(levelSaveLocation, renderer, FrmGame.levelDelimiters, FrmGame.roomDelimiters)
            thisLevel = FrmGame.LoadLevelFile(levelSaveLocation, renderer)

            RefreshRoomsList()
            RefreshInstancesList()
            RefreshTemplatesList()
            RefreshParameterList()

            RefreshControlsEnabled()
        Else
            PRE2.DisplayError("No file found at " & fileLocation)
        End If
    End Sub

    Private Sub SaveLevel(levelToSave As Level, saveLocation As String)
        'saves a level (not the rooms) to a file

        'PRE2.WriteFile(saveLocation, levelToSave.ToString(FrmGame.levelDelimiters, FrmGame.roomDelimiters))
        PRE2.WriteFile(saveLocation, levelToSave.ToString)
    End Sub

    Private Sub btnLevelOpen_Click(sender As Object, e As EventArgs) Handles btnLevelOpen.Click
        Using openDialog As New OpenFileDialog With {.Filter = "Level file (*.lvl)|*.lvl", .InitialDirectory = renderer.levelFolderLocation}
            If openDialog.ShowDialog() = DialogResult.OK Then
                LoadLevel(openDialog.FileName)
            End If
        End Using
    End Sub

    Private Sub BtnLevelSaveAs_Click(sender As Object, e As EventArgs) Handles btnLevelSaveAs.Click
        Dim fileName As String = InputBox("Enter file name for level")

        If fileName.Length >= 1 Then        'checks that the user actually entered something
            levelSaveLocation = renderer.levelFolderLocation & fileName & ".lvl"
            SaveLevel(thisLevel, levelSaveLocation)
            btnLevelSave.Enabled = True
        End If
    End Sub

    Private Sub BtnLevelSave_Click(sender As Object, e As EventArgs) Handles btnLevelSave.Click
        SaveLevel(thisLevel, levelSaveLocation)
    End Sub


    Private Sub UserCloseForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'displays a warning to the user if they have unsaved work when they close the form

        Dim unsavedChanges As Boolean = False

        If Not IsNothing(levelSaveLocation) AndAlso IO.File.Exists(levelSaveLocation) Then
            Dim savedLevelString As String = PRE2.ReadFile(levelSaveLocation)

            If savedLevelString <> thisLevel.ToString Then
                unsavedChanges = True
            End If
        ElseIf Not IsNothing(renderer.levelFolderLocation) Then     'no level folder location if form isnt finished loading
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

#Region "Render"
    'render

    Dim renderer As New PRE2
    Dim thisLevel As Level
    'Dim thisRoom As Room

    Private ReadOnly Property SelectedRoom As Room
        Get
            If lstRooms.SelectedIndex > -1 And Not IsNothing(thisLevel.rooms) AndAlso lstRooms.SelectedIndex <= UBound(thisLevel.rooms) Then
                Return thisLevel.rooms(lstRooms.SelectedIndex)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private Sub RenderCurrentRoom()
        'renders the current room

        renderer.DoGameRender(SelectedRoom.instances)
    End Sub

#End Region

#Region "Entities"

#Region "Templates"

    Private Sub LoadEntityTemplate(fileLocation As String)
        'loads an entity saved to a file for a template

        Dim entityString As String = PRE2.ReadFile(fileLocation)
        If IsNothing(entityString) = False Then
            Dim successfulLoad As Boolean = False
            Dim newTemplate As Entity = EntityStringHandler.ReadEntityString(entityString, renderer, successfulLoad)

            If successfulLoad Then
                If Not IsNothing(thisLevel.templates) Then      'makes name unique
                    Dim templateNames(UBound(thisLevel.templates)) As String
                    For index As Integer = 0 To UBound(templateNames)
                        templateNames(index) = thisLevel.templates(index).name
                    Next index
                    newTemplate.name = FrmGame.MakeNameUnique(newTemplate.name, templateNames, True)
                End If

                newTemplate.AddTag(New Tag("fileName", AddQuotes(fileLocation.Remove(0, Len(renderer.entityFolderLocation)))))

                If IsNothing(thisLevel.templates) = True Then
                    ReDim thisLevel.templates(0)
                Else
                    ReDim Preserve thisLevel.templates(UBound(thisLevel.templates) + 1)
                End If
                thisLevel.templates(UBound(thisLevel.templates)) = newTemplate
            End If

            RefreshTemplatesList()
        End If
    End Sub

    Private Sub RemoveEntityTemplate(templateIndex As Integer)
        'removes the template of an entity and all instances made from that template from the level

        If Not IsNothing(thisLevel.templates) Then
            If templateIndex >= 0 And templateIndex <= UBound(thisLevel.templates) Then
                'first removes all instances with the templateName tag which is the same as the name of the template
                Dim templateName As String = thisLevel.templates(templateIndex).name

                If Not IsNothing(thisLevel.rooms) Then
                    'goes through each room
                    For roomIndex As Integer = 0 To UBound(thisLevel.rooms)
                        Dim currentRoom As Room = thisLevel.rooms(roomIndex)
                        lstRooms.SelectedIndex = roomIndex

                        If Not IsNothing(currentRoom.instances) AndAlso UBound(currentRoom.instances) >= 0 Then
                            Dim instanceIndex As Integer = 0
                            Do
                                Dim currentInstance As Entity = currentRoom.instances(instanceIndex)

                                If currentInstance.FindTag("templateName").GetArgument = templateName Then
                                    'removes current instance if the templateName matches
                                    RemoveEntityInstance(instanceIndex)

                                    'For index As Integer = instanceIndex To UBound(currentRoom.instances) - 1
                                    '    currentRoom.instances(index) = currentRoom.instances(index + 1)
                                    'Next index

                                    'ReDim Preserve currentRoom.instances(UBound(currentRoom.instances) - 1)
                                Else
                                    instanceIndex += 1
                                End If
                            Loop Until instanceIndex > UBound(currentRoom.instances) - 1

                            thisLevel.rooms(roomIndex) = currentRoom
                        End If
                    Next roomIndex
                End If

                'then removes the template itself
                For index As Integer = templateIndex To UBound(thisLevel.templates) - 1
                    thisLevel.templates(index) = thisLevel.templates(index + 1)
                Next index

                If UBound(thisLevel.templates) > 0 Then
                    ReDim Preserve thisLevel.templates(UBound(thisLevel.templates) - 1)
                Else
                    thisLevel.templates = Nothing
                End If
            Else
                PRE2.DisplayError("Provided template index (" & templateIndex & ") was an out of bounds")
            End If
        Else
            PRE2.DisplayError("There is currently no templates")
        End If

        RefreshInstancesList()
        RefreshTemplatesList()

    End Sub


    Private Sub btnLoadEntity_Click(sender As Object, e As EventArgs) Handles btnLoadEntity.Click
        Using openDialog As New OpenFileDialog With {.Filter = "Entity files (*.ent)|*.ent", .Multiselect = True, .InitialDirectory = renderer.entityFolderLocation}

            If openDialog.ShowDialog() = DialogResult.OK Then
                For Each fileName As String In openDialog.FileNames
                    LoadEntityTemplate(fileName)
                Next
            End If
        End Using
    End Sub

    Private Sub btnRemoveEntity_Click(sender As Object, e As EventArgs) Handles btnRemoveEntity.Click
        If lstTemplates.SelectedIndex > -1 Then
            If MsgBox("Are you sure you wish to remove this template?" & Environment.NewLine &
                  "This will also remove all instances which use this template") = DialogResult.OK Then
                RemoveEntityTemplate(lstTemplates.SelectedIndex)
            End If
        End If
    End Sub


    Private Sub RefreshTemplatesList()
        If IsNothing(thisLevel.templates) = False Then
            Dim names(UBound(thisLevel.templates)) As String

            For index As Integer = 0 To UBound(thisLevel.templates)
                names(index) = thisLevel.templates(index).name
            Next

            RefreshList(lstTemplates, names)
        Else
            RefreshList(lstTemplates, Nothing)
        End If
    End Sub

    Private Sub lstTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTemplates.SelectedIndexChanged
        'deselects whatever is selected in lstInstances

        If lstTemplates.SelectedIndex > -1 Then     'checks that this isn't being unselected
            lstInstances.SelectedIndex = -1

            'ToggleTagControls(True)
            ShowEntityTags(thisLevel.templates(lstTemplates.SelectedIndex), True)
        Else
            If lstInstances.SelectedIndex = -1 Then
                'ToggleTagControls(False)    'disables tag controls as there is no selected instance or template
                ShowEntityTags(Nothing, False)
            End If
        End If
    End Sub

#End Region

#Region "Instances"

    Private Sub AddEntityInstance(template As Entity)
        'creates a new instance from the given entity

        'Dim templateName As String = template.name

        Dim newInstance As Entity = template.Clone
        If Not newInstance.HasTag("templateName") Then      'doesn't add template name if template is an instance of a template
            newInstance.AddTag(New Tag("templateName", template.name))       'instance stores the name of its template so the instance can be created from the correct template when loading
        End If

        'checks if there are any instances with the same name yet, and numbers the instance accordingly
        If IsNothing(SelectedRoom.instances) = True Then
            newInstance.name = RemoveQuotes(template.name) & "-1"

            ReDim thisLevel.rooms(lstRooms.SelectedIndex).instances(0)
            SelectedRoom.instances(0) = newInstance
        Else
            Dim instanceNames(UBound(SelectedRoom.instances)) As String

            For index As Integer = 0 To UBound(SelectedRoom.instances)
                instanceNames(index) = SelectedRoom.instances(index).name
            Next index

            Dim newName As String = FrmGame.MakeNameUnique(newInstance.name, instanceNames, False)

            newInstance.name = newName

            ReDim Preserve thisLevel.rooms(lstRooms.SelectedIndex).instances(UBound(SelectedRoom.instances) + 1)
            SelectedRoom.instances(UBound(SelectedRoom.instances)) = newInstance
        End If

        'template.name = templateName

        RefreshInstancesList()
        RefreshTemplatesList()
    End Sub

    Private Sub RemoveEntityInstance(instanceIndex As Integer)
        'deletes the instance with the given index

        'removes the instance from the room
        If instanceIndex >= 0 And instanceIndex <= UBound(SelectedRoom.instances) Then
            For index As Integer = instanceIndex To UBound(SelectedRoom.instances) - 1
                SelectedRoom.instances(index) = SelectedRoom.instances(index + 1)
            Next index

            If UBound(SelectedRoom.instances) Then
                ReDim Preserve thisLevel.rooms(lstRooms.SelectedIndex).instances(UBound(SelectedRoom.instances) - 1)
            Else
                thisLevel.rooms(lstRooms.SelectedIndex).instances = Nothing
            End If
        Else
            PRE2.DisplayError("Tried to remove an instance at index " & instanceIndex & " in an array with a max index of " & UBound(SelectedRoom.instances))
        End If

        RefreshInstancesList()
    End Sub


    Private Sub btnInstanceCreate_Click(sender As Object, e As EventArgs) Handles btnInstanceCreate.Click
        'creates a new instance of the currently selected template

        'checks that a template is selected
        If lstTemplates.SelectedIndex > -1 Then
            AddEntityInstance(thisLevel.templates(lstTemplates.SelectedIndex))
        Else
            PRE2.DisplayError("No selected template to create an instance from")
        End If
    End Sub

    Private Sub btnInstanceDuplicate_Click(sender As Object, e As EventArgs) Handles btnInstanceDuplicate.Click
        'creates a new instance as a copy of the currently selected instance

        'checks that an instance is selected
        If lstInstances.SelectedIndex > -1 Then
            AddEntityInstance(SelectedRoom.instances(lstInstances.SelectedIndex))
        Else
            PRE2.DisplayError("No selected instance to duplicate")
        End If
    End Sub

    Private Sub btnInstanceDelete_Click(sender As Object, e As EventArgs) Handles btnInstanceDelete.Click
        'deletes the currently selected instance

        'checks that an instance is selected
        If lstInstances.SelectedIndex > -1 Then
            'asks the user to confirm deleting the instance
            If MsgBox("Are you sure you wish to delete instance " & SelectedRoom.instances(lstInstances.SelectedIndex).name, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RemoveEntityInstance(lstInstances.SelectedIndex)
            End If
        End If
    End Sub


    Private Sub RefreshInstancesList()
        If IsNothing(SelectedRoom.instances) = False Then
            Dim names(UBound(SelectedRoom.instances)) As String

            For index As Integer = 0 To UBound(SelectedRoom.instances)
                names(index) = SelectedRoom.instances(index).name
            Next

            RefreshList(lstInstances, names)
        Else
            RefreshList(lstInstances, Nothing)
        End If
    End Sub

    Private Sub lstInstances_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstInstances.SelectedIndexChanged
        'deselects whatever is selected in lstTemplates

        If lstInstances.SelectedIndex > -1 Then     'checks that this isn't being unselected
            lstTemplates.SelectedIndex = -1

            'ToggleTagControls(True)     'enables tag controls as an instance has been selected
            ShowEntityTags(SelectedRoom.instances(lstInstances.SelectedIndex), False)

            RenderCurrentRoom()
        Else
            If lstTemplates.SelectedIndex = -1 Then
                'ToggleTagControls(False)    'disables tag controls as there is no selected instance or template
                ShowEntityTags(Nothing, False)
            End If
        End If
    End Sub

#End Region

#End Region

#Region "Tags"
    'tags

    Dim disableTagChangedEvent As Boolean = False

    'Private Sub ControlInitialisation()
    '    tagControls = {txtTagName, numTagLocX, numTagLocY, numTagLayer, numTagScale, lstTags, btnTagAdd, btnTagEdit, btnTagRemove}

    '    ToggleTagControls(False)
    'End Sub

    Private Sub ToggleTagControls(enabled As Boolean)
        'enables or disables all controls for tags, depending on whether provided True or False

        Dim tagControls() As Control = {txtTagName, numTagLocX, numTagLocY, numTagLayer, numTagScale, lstTags, btnTagAdd, btnTagEdit, btnTagRemove}

        For Each ctrl As Object In tagControls
            ctrl.Enabled = enabled
        Next
    End Sub

    Private Sub ShowEntityTags(ByVal ent As Entity, isTemplate As Boolean)
        'changes the values displayed in the controls for tags to show values of the current entity

        disableTagChangedEvent = True

        If IsNothing(ent) Then      'if no entity provided then uses an empty entity
            ent = New Entity With {.name = "No Entity Selected"}       'this doesn't work as entities have some default properties
            'ToggleTagControls(False)
        End If

        txtTagName.Text = RemoveQuotes(ent.name)
        If Not isTemplate Then      'templates dont have x and y values
            numTagLocX.Value = ent.location.X
            numTagLocY.Value = ent.location.Y
            If txtTagName.Enabled Then
                numTagLocX.Enabled = True
                numTagLocY.Enabled = True
            End If
        Else
            numTagLocX.Value = 0
            numTagLocY.Value = 0
            numTagLocX.Enabled = False
            numTagLocY.Enabled = False
        End If
        numTagLayer.Value = ent.layer
        numTagScale.Value = ent.scale

        'updates lstTags
        lstTags.Items.Clear()
        If IsNothing(ent.tags) = False Then
            For Each thisTag As Tag In ent.tags
                If IsNothing(thisTag.name) = False Then
                    lstTags.Items.Add(thisTag.ToString)
                End If
            Next thisTag
        End If

        disableTagChangedEvent = False
    End Sub

    Private Sub KeyTagChanged(sender As Object, e As EventArgs) Handles numTagLocX.ValueChanged, numTagLocY.ValueChanged,
        numTagLayer.ValueChanged, numTagScale.ValueChanged ', txtTagName.TextChanged
        'updates the key tags (location, layer, scale) of selected entity using the key tags controls' values

        If Not disableTagChangedEvent Then
            'selects the entity to update, currently selected instance or template
            Dim entityToUpdate As Entity
            If lstInstances.SelectedIndex > -1 Then
                entityToUpdate = SelectedRoom.instances(lstInstances.SelectedIndex)
            ElseIf lstTemplates.SelectedIndex > -1 Then
                entityToUpdate = thisLevel.templates(lstTemplates.SelectedIndex)
            Else
                Exit Sub
            End If

            'entityToUpdate.name = txtTagName.Text
            entityToUpdate.location = New PointF(numTagLocX.Value, numTagLocY.Value)
            entityToUpdate.layer = numTagLayer.Value
            entityToUpdate.scale = numTagScale.Value

            If lstInstances.SelectedIndex > -1 Then
                thisLevel.rooms(lstRooms.SelectedIndex).instances(lstInstances.SelectedIndex) = entityToUpdate
            Else
                thisLevel.templates(lstTemplates.SelectedIndex) = entityToUpdate
            End If

            ShowEntityTags(entityToUpdate, lstTemplates.SelectedIndex > -1)

            RenderCurrentRoom()
        End If
    End Sub

    Private Sub btnTagAdd_Click(sender As Object, e As EventArgs) Handles btnTagAdd.Click
        'adds a tag created by the user using FrmTagMaker

        Using tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()

            If tagMaker.userFinished = True Then
                If lstInstances.SelectedIndex > -1 Then
                    SelectedRoom.instances(lstInstances.SelectedIndex).AddTag(tagMaker.CreatedTag)
                    ShowEntityTags(SelectedRoom.instances(lstInstances.SelectedIndex), False)
                ElseIf lstTemplates.SelectedIndex > -1 Then
                    thisLevel.templates(lstTemplates.SelectedIndex).AddTag(tagMaker.CreatedTag)
                    ShowEntityTags(thisLevel.templates(lstTemplates.SelectedIndex), True)
                End If
            End If
        End Using
    End Sub

    Private Sub btnTagEdit_Click(sender As Object, e As EventArgs) Handles btnTagEdit.Click
        'allows the user to edit a tag using FrmTagMaker

        Dim tagIndex As Integer = lstTags.SelectedIndex
        Dim instanceIndex As Integer = lstInstances.SelectedIndex
        Dim templateIndex As Integer = lstTemplates.SelectedIndex

        Dim tagMaker As FrmTagMaker = New FrmTagMaker
        If tagIndex > -1 Then
            If instanceIndex > -1 Then
                tagMaker = New FrmTagMaker(SelectedRoom.instances(instanceIndex).tags(tagIndex))
            ElseIf templateIndex > -1 Then
                tagMaker = New FrmTagMaker(thisLevel.templates(templateIndex).tags(tagIndex))
            Else
                Exit Sub
            End If
            tagMaker.ShowDialog()

            If tagMaker.userFinished = True Then
                If instanceIndex > -1 Then
                    SelectedRoom.instances(instanceIndex).SetTag(tagIndex, tagMaker.CreatedTag)
                    ShowEntityTags(SelectedRoom.instances(instanceIndex), False)
                ElseIf templateIndex > -1 Then
                    thisLevel.templates(templateIndex).SetTag(tagIndex, tagMaker.CreatedTag)
                    ShowEntityTags(thisLevel.templates(templateIndex), True)
                End If
            End If
        End If
        tagMaker.Dispose()
    End Sub

    Private Sub btnTagRemove_Click(sender As Object, e As EventArgs) Handles btnTagRemove.Click
        'removes the selected tag

        Dim newTags() As Tag
        If lstInstances.SelectedIndex > -1 Then
            newTags = SelectedRoom.instances(lstInstances.SelectedIndex).tags
        ElseIf lstTemplates.SelectedIndex > -1 Then
            newTags = thisLevel.templates(lstTemplates.SelectedIndex).tags
        Else
            Exit Sub
        End If

        For index As Integer = lstTags.SelectedIndex To UBound(newTags) - 1
            newTags(index) = newTags(index + 1)
        Next

        If UBound(newTags) > 0 Then
            ReDim Preserve newTags(UBound(newTags) - 1)
        Else
            newTags = Nothing
        End If

        If lstInstances.SelectedIndex > -1 Then
            SelectedRoom.instances(lstInstances.SelectedIndex).tags = newTags
            ShowEntityTags(SelectedRoom.instances(lstInstances.SelectedIndex), False)
        ElseIf lstTemplates.SelectedIndex > -1 Then
            thisLevel.templates(lstTemplates.SelectedIndex).tags = newTags
            ShowEntityTags(thisLevel.templates(lstTemplates.SelectedIndex), True)
        End If
    End Sub

    Private Sub txtTagName_Leave(sender As Object, e As EventArgs) Handles txtTagName.Leave
        'changes the name of an instance or template

        If lstTemplates.SelectedIndex > -1 Then
            thisLevel.templates(lstTemplates.SelectedIndex).name = txtTagName.Text
        ElseIf lstInstances.SelectedIndex > -1 Then
            SelectedRoom.instances(lstInstances.SelectedIndex).name = txtTagName.Text
        End If
    End Sub


#Region "Mouse Location Control"

    Dim heldInstanceIndex As Integer = -1           'index of the entity being held by the user
    Dim relativeHoldLocation As PointF = Nothing    'used so that the mouse holds a specific location on the instance

    Private Sub PnlRenderMouseDown(sender As Object, e As MouseEventArgs) Handles pnlRender.MouseDown
        'mouse starts holding the instance underneath it

        'gets the relative mouse location in the game render
        Dim mouseLocationInRender As New PointF(e.X / renderer.RenderScale.Width, e.Y / renderer.RenderScale.Height)

        'finds which instances the mouse is over
        Dim possibleInstanceIndices() As Integer = Nothing
        For index As Integer = 0 To UBound(SelectedRoom.instances)
            Dim instanceArea As RectangleF = SelectedRoom.instances(index).GetEntityHitbox()

            If mouseLocationInRender.X <= instanceArea.Right And mouseLocationInRender.X >= instanceArea.Left _
                And mouseLocationInRender.Y >= instanceArea.Top And mouseLocationInRender.Y <= instanceArea.Bottom Then
                If IsNothing(possibleInstanceIndices) Then
                    possibleInstanceIndices = {index}
                Else
                    ReDim Preserve possibleInstanceIndices(UBound(possibleInstanceIndices) + 1)
                    possibleInstanceIndices(UBound(possibleInstanceIndices)) = index
                End If
            End If
        Next

        'finds which instance has the highest index
        If Not IsNothing(possibleInstanceIndices) Then
            Dim topMostInstanceIndex As Integer = possibleInstanceIndices(0)
            For index As Integer = 0 To UBound(possibleInstanceIndices)
                If SelectedRoom.instances(possibleInstanceIndices(index)).layer > SelectedRoom.instances(topMostInstanceIndex).layer Then
                    topMostInstanceIndex = possibleInstanceIndices(index)
                End If
            Next

            heldInstanceIndex = topMostInstanceIndex
            relativeHoldLocation = New PointF(SelectedRoom.instances(heldInstanceIndex).GetEntityHitbox.Left - mouseLocationInRender.X,
                                              SelectedRoom.instances(heldInstanceIndex).GetEntityHitbox.Top - mouseLocationInRender.Y)
            lstInstances.SelectedIndex = heldInstanceIndex
        End If
    End Sub

    Private Sub PnlRenderMouseDrag(sender As Object, e As MouseEventArgs) Handles pnlRender.MouseMove
        'mouse moves the held instance

        If heldInstanceIndex >= 0 Then
            SelectedRoom.instances(heldInstanceIndex).location = New PointF(e.X / renderer.RenderScale.Width + relativeHoldLocation.X,
                                                                            e.Y / renderer.RenderScale.Height + relativeHoldLocation.Y)
            RenderCurrentRoom()
            ShowEntityTags(SelectedRoom.instances(heldInstanceIndex), False)
        End If
    End Sub

    Private Sub PnlRenderMouseUp(sender As Object, e As MouseEventArgs) Handles pnlRender.MouseUp
        'mouse lets go of the held instance

        heldInstanceIndex = -1
    End Sub

#End Region

#End Region

#Region "Parameters"
    'parameters

    Private Sub RefreshParameterList()
        'refreshes both lstLevelParams and lstRoomParams
        Dim items() As String = {}
        If IsNothing(thisLevel.globalParameters) = False Then
            ReDim items(UBound(thisLevel.globalParameters))

            For index As Integer = 0 To UBound(items)
                items(index) = thisLevel.globalParameters(index).ToString
            Next index
        End If
        RefreshList(lstLevelParams, items)

        items = {}
        If IsNothing(SelectedRoom.parameters) = False Then
            ReDim items(UBound(SelectedRoom.parameters))

            For index As Integer = 0 To UBound(items)
                items(index) = SelectedRoom.parameters(index).ToString
            Next index
        End If
        RefreshList(lstRoomParams, items)
    End Sub

    Private Sub AddParameter(newParam As Tag, ByRef parameterList() As Tag)
        'adds a given parameter to the level

        If IsNothing(parameterList) = True Then
            ReDim parameterList(0)
        Else
            ReDim Preserve parameterList(UBound(parameterList) + 1)
        End If
        parameterList(UBound(parameterList)) = newParam

        RefreshParameterList()
    End Sub

    Private Sub ReplaceParameter(paramIndex As Integer, newParam As Tag, parameterList() As Tag)
        'replaces the parameter in the given index with the given parameter

        parameterList(paramIndex) = newParam

        RefreshParameterList()
    End Sub

    Private Sub RemoveParameter(paramIndex As Integer, ByRef parameterList() As Tag)
        'removes the parameter at the given index

        For index As Integer = paramIndex To UBound(parameterList) - 1
            parameterList(index) = parameterList(index + 1)
        Next index

        If UBound(parameterList) > 0 Then
            ReDim Preserve parameterList(UBound(parameterList) - 1)
        Else
            parameterList = Nothing
        End If

        RefreshParameterList()
    End Sub

    'level params

    Private Sub lstLevelParams_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLevelParams.SelectedIndexChanged
        'a different parameter is selected for editing/removing

        If lstLevelParams.SelectedIndex > -1 Then
            btnLevelParamEdit.Enabled = True
            btnLevelParamRemove.Enabled = True
        Else
            btnLevelParamEdit.Enabled = False
            btnLevelParamRemove.Enabled = False
        End If
    End Sub

    Private Sub btnLevelParamAdd_Click(sender As Object, e As EventArgs) Handles btnLevelParamAdd.Click
        'user creates a new parameter using FrmTagMaker

        'gets the user to create a parameter (same as a tag)
        Using tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()
            If tagMaker.userFinished = True Then
                AddParameter(tagMaker.CreatedTag, thisLevel.globalParameters)
            End If
        End Using
    End Sub

    Private Sub btnLevelParamEdit_Click(sender As Object, e As EventArgs) Handles btnLevelParamEdit.Click
        'user changes an existing parameter using FrmTagMaker

        If lstLevelParams.SelectedIndex > -1 Then
            'gets the user to create a parameter (same as a tag)
            Using tagMaker As New FrmTagMaker(thisLevel.globalParameters(lstLevelParams.SelectedIndex))
                tagMaker.ShowDialog()
                If tagMaker.userFinished = True Then
                    ReplaceParameter(lstLevelParams.SelectedIndex, tagMaker.CreatedTag, thisLevel.globalParameters)
                End If
            End Using
        End If
    End Sub

    Private Sub btnLevelParamRemove_Click(sender As Object, e As EventArgs) Handles btnLevelParamRemove.Click
        'user deletes an existing parameter

        If lstLevelParams.SelectedIndex > -1 Then
            If MsgBox("Are you sure you wish to delete parameter " & thisLevel.globalParameters(lstLevelParams.SelectedIndex).name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RemoveParameter(lstLevelParams.SelectedIndex, thisLevel.globalParameters)
            End If
        End If
    End Sub

    'room params

    Private Sub lstRoomParams_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRoomParams.SelectedIndexChanged
        'a different parameter is selected for editing/removing

        If lstLevelParams.SelectedIndex > -1 Then
            btnEditRoomParam.Enabled = True
            btnRemoveRoomParam.Enabled = True
        Else
            btnEditRoomParam.Enabled = False
            btnRemoveRoomParam.Enabled = False
        End If
    End Sub

    Private Sub btnAddRoomParam_Click(sender As Object, e As EventArgs) Handles btnAddRoomParam.Click
        'user creates a new parameter using FrmTagMaker

        'gets the user to create a parameter (same as a tag)
        Using tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()
            If tagMaker.userFinished = True Then
                AddParameter(tagMaker.CreatedTag, thisLevel.rooms(lstRooms.SelectedIndex).parameters)
            End If
        End Using
    End Sub

    Private Sub btnRemoveRoomParam_Click(sender As Object, e As EventArgs) Handles btnRemoveRoomParam.Click
        'user deletes an existing parameter

        If lstRoomParams.SelectedIndex > -1 Then
            If MsgBox("Are you sure you wish to delete parameter " & SelectedRoom.parameters(lstRoomParams.SelectedIndex).name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RemoveParameter(lstRoomParams.SelectedIndex, thisLevel.rooms(lstRooms.SelectedIndex).parameters)
            End If
        End If
    End Sub

    Private Sub btnEditRoomParam_Click(sender As Object, e As EventArgs) Handles btnEditRoomParam.Click
        'user changes an existing parameter using FrmTagMaker

        If lstRoomParams.SelectedIndex > -1 Then
            'gets the user to create a parameter (same as a tag)
            Using tagMaker As New FrmTagMaker(SelectedRoom.parameters(lstRoomParams.SelectedIndex))
                tagMaker.ShowDialog()
                If tagMaker.userFinished = True Then
                    ReplaceParameter(lstRoomParams.SelectedIndex, tagMaker.CreatedTag, thisLevel.rooms(lstRooms.SelectedIndex).parameters)
                End If
            End Using
        End If
    End Sub

#End Region

#Region "Rooms"
    'rooms 

    Private Sub RefreshRoomsList()
        If IsNothing(thisLevel.rooms) = False Then
            Dim names(UBound(thisLevel.rooms)) As String

            For index As Integer = 0 To UBound(thisLevel.rooms)
                names(index) = thisLevel.rooms(index).Name
            Next

            RefreshList(lstRooms, names)
        End If
    End Sub

    Private Sub AddRoom(newRoom As Room)
        'adds a new room to the level

        'checks that name isn't already being used
        If Not IsNothing(thisLevel.rooms) Then
            For Each currentRoom As Room In thisLevel.rooms
                If currentRoom.Name = AddQuotes(newRoom.Name) Then
                    PRE2.DisplayError("Room name " & AddQuotes(newRoom.Name, True) & " is in use, please use a different one")
                    Exit Sub
                End If
            Next
        End If

        If IsNothing(thisLevel.rooms) = True Then
            ReDim thisLevel.rooms(0)
        Else
            ReDim Preserve thisLevel.rooms(UBound(thisLevel.rooms) + 1)
        End If
        thisLevel.rooms(UBound(thisLevel.rooms)) = newRoom

        RefreshRoomsList()
    End Sub

    Private Sub RemoveRoom(roomIndex As Integer)
        'removes the room at the given index

        For index As Integer = roomIndex To UBound(thisLevel.rooms) - 1
            thisLevel.rooms(index) = thisLevel.rooms(index + 1)
        Next index

        If UBound(thisLevel.rooms) > 0 Then
            ReDim Preserve thisLevel.rooms(UBound(thisLevel.rooms) - 1)
        Else
            thisLevel.rooms = Nothing
        End If

        RefreshRoomsList()
    End Sub

    Private Sub SetRoomCoords(roomIndex As Integer, roomCoords As Point)
        'sets the coordinates of the room at the given index to the given coords

        If IsNothing(thisLevel.rooms) = False AndAlso roomIndex >= 0 And roomIndex <= UBound(thisLevel.rooms) Then
            'checks if the new coords are being used by another room
            Dim uniqueCoords As Boolean = True
            'For Each coords As Point In thisLevel.roomCoords
            '    If coords = roomCoords Then
            '        uniqueCoords = False
            '        Exit For
            '    End If
            'Next

            If uniqueCoords = True Then
                'thisLevel.roomCoords(roomIndex) = roomCoords

                RefreshRoomsList()
            Else
                PRE2.DisplayError("These coordinates are already being used by another room")
            End If
        End If
    End Sub

    Private Sub lstRooms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRooms.SelectedIndexChanged
        'currently selected room is changed

        If lstRooms.SelectedIndex > -1 Then
            'thisRoom = thisLevel.rooms(lstRooms.SelectedIndex)

            'lstTemplates.SelectedIndex = -1
            lstInstances.SelectedIndex = -1
            RefreshInstancesList()
            RefreshParameterList()
            RenderCurrentRoom()

            btnLevelRoomRemove.Enabled = True
        Else
            btnLevelRoomRemove.Enabled = False
        End If
    End Sub

    Private Sub btnLevelRoomAdd_Click(sender As Object, e As EventArgs) Handles btnLevelRoomAdd.Click
        'a new room is added to the level

        Dim roomName As String = InputBox("Please enter a name for the new room")

        If roomName.Length > 0 Then
            AddRoom(New Room With {.Name = roomName})
        End If
    End Sub

    Private Sub btnLevelRoomRemove_Click(sender As Object, e As EventArgs) Handles btnLevelRoomRemove.Click
        'an existing room is removed from the level

        'checks that a room is selected
        If lstRooms.SelectedIndex > -1 Then
            'asks the user to confirm deleting the room
            If MsgBox("Are you sure you wish to delete room " & SelectedRoom.Name, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RemoveRoom(lstRooms.SelectedIndex)
            End If
        End If
    End Sub

    Private Sub btnRoomEditCoords_Click(sender As Object, e As EventArgs) Handles btnRoomEditCoords.Click
        'prompts the user to enter new coords for the selected room

        If lstRooms.SelectedIndex > -1 Then
            Dim userInput As String = InputBox("Please enter the new coordinates for " & thisLevel.rooms(lstRooms.SelectedIndex).Name & vbCrLf & "Form x,y eg '2,1'")

            If userInput.Length > 0 Then
                'checks that input is valid
                If IsNothing(userInput.Split(",")(0)) = False And IsNothing(userInput.Split(",")(1)) = False AndAlso IsNumeric(userInput.Split(",")(0)) = True And IsNumeric(userInput.Split(",")(1)) = True Then
                    Dim newCoords As New Point(Int(userInput.Split(",")(0)), Int(userInput.Split(",")(1)))

                    SetRoomCoords(lstRooms.SelectedIndex, newCoords)
                Else
                    PRE2.DisplayError(userInput & " is not valid input for coordinates")
                End If
            End If
        End If
    End Sub

#End Region

#Region "General Procedures"
    'general procedures

    Private Sub RefreshList(list As ListBox, values() As String)
        'empties a list and fills it with given values

        Dim startSelectedIndex As Integer = list.SelectedIndex

        list.SelectedIndex = -1
        list.Items.Clear()

        If IsNothing(list) = False Then
            If IsNothing(values) = False Then
                For Each value As String In values
                    If Not IsNothing(value) Then
                        list.Items.Add(value)
                    End If
                Next value

                If list.Items.Count > startSelectedIndex Then
                    list.SelectedIndex = startSelectedIndex
                End If
            End If
        Else
            'PRE2.DisplayError("A list tried to refresh but doesn't exist")
        End If
    End Sub

    Private Sub AnySelectionChanged(sender As Object, e As EventArgs) Handles _
        lstInstances.SelectedIndexChanged, lstLevelParams.SelectedIndexChanged, lstRoomParams.SelectedIndexChanged,
        lstRooms.SelectedIndexChanged, lstTags.SelectedIndexChanged, lstTemplates.SelectedIndexChanged

        RefreshControlsEnabled()
    End Sub

    Private Sub RefreshControlsEnabled()
        'enables or disables controls based on current condition

        Dim templateSelected As Boolean = lstTemplates.SelectedIndex > -1
        Dim instanceSelected As Boolean = lstInstances.SelectedIndex > -1
        Dim roomSelected As Boolean = lstRooms.SelectedIndex > -1
        Dim entityTagSelected As Boolean = lstTags.SelectedIndex > -1
        Dim roomParamSelected As Boolean = lstRoomParams.SelectedIndex > -1
        Dim levelParamSelected As Boolean = lstLevelParams.SelectedIndex > -1
        Dim levelSaveLocationSelected As Boolean = Not IsNothing(levelSaveLocation) AndAlso IO.File.Exists(levelSaveLocation)

        Dim controlsDefaultDisabled() As Control = {
            btnInstanceCreate, btnInstanceDuplicate, btnInstanceDelete, btnRemoveEntity, btnLevelSave,
            btnTagAdd, btnTagEdit, btnTagRemove, btnAddRoomParam, btnEditRoomParam, btnRemoveRoomParam, btnLevelParamRemove, btnLevelParamEdit,
            btnRoomEditCoords, btnLevelRoomRemove
        }
        Dim controlsDefaultEnabled() As Control = {
            btnLevelOpen, btnLevelRoomAdd, btnLoadEntity, btnCreateEntity, btnLevelParamAdd
        }
        For Each ctrl As Control In controlsDefaultDisabled
            ctrl.Enabled = False
        Next ctrl
        For Each ctrl As Control In controlsDefaultEnabled
            ctrl.Enabled = True
        Next
        ToggleTagControls(False)

        If roomSelected Then
            btnLevelRoomRemove.Enabled = True
            btnRoomEditCoords.Enabled = True
            'btnRoomSaveAs.Enabled = True
            btnAddRoomParam.Enabled = True

            If templateSelected Then
                btnInstanceCreate.Enabled = True
                ToggleTagControls(True)
            End If

            If instanceSelected Then
                ToggleTagControls(True)
                btnInstanceDuplicate.Enabled = True
                btnInstanceDelete.Enabled = True

                btnTagAdd.Enabled = True
                If entityTagSelected Then
                    btnTagEdit.Enabled = True
                    btnTagRemove.Enabled = True
                End If
            End If

            If roomParamSelected Then
                btnEditRoomParam.Enabled = True
                btnRemoveRoomParam.Enabled = True
            End If
        End If

        If templateSelected Then
            btnRemoveEntity.Enabled = True

            btnTagAdd.Enabled = True
            If entityTagSelected Then
                btnTagEdit.Enabled = True
                btnTagRemove.Enabled = True
            End If
        End If

        If levelParamSelected Then
            btnLevelParamEdit.Enabled = True
            btnLevelParamRemove.Enabled = True
        End If

        If levelSaveLocationSelected Then
            btnLevelSave.Enabled = True
        End If
    End Sub

#End Region

End Class