﻿'Richard Holmes
'29/03/2019
'Level editor for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmLevelEditor
	
	
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
        ControlInitialisation()
        LoadInitialisation()
        RefreshControlsEnabled()
    End Sub

    Private Sub LayoutInitialisation()
        tblRoom.Location = New Point(pnlRender.Right + 10, pnlRender.Top)
        tblLevel.Location = New Point(pnlRender.Left, pnlRender.Bottom + 10)

        Me.Size = New Size(tblRoom.Right + 20, tblLevel.Bottom + 40)

        'flwSaveLoad.Location = New Point(pnlRender.Right + 10, pnlRender.Top)
        'tblEntities.Location = New Point(flwSaveLoad.Left, flwSaveLoad.Bottom + 5)
        'tblTagsSummary.Location = New Point(tblEntities.Right + 10, tblEntities.Top)
        'tblTagsDetailed.Location = New Point(tblTagsSummary.Left, tblTagsSummary.Bottom + 5)
    End Sub



    'save load

    Dim roomSaveLocation As String = ""
    Dim levelSaveLocation As String = ""

    Private Sub LoadInitialisation()
        'gets the folder locations from the loader file

        Dim openDialog As New OpenFileDialog With {.Filter = "Loader file (*.ldr)|*.ldr", .Multiselect = False}
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
    End Sub


	
    'levels

    Private Sub LoadLevel(fileLocation As String)
        'loads a level and sets the interface up

        thisLevel = FrmGame.LoadLevelFile(fileLocation, renderer)

        RefreshRoomsList()
        RefreshInstancesList()
        RefreshTemplatesList()
        RefreshParameterList()
    End Sub

    Private Sub SaveLevel(levelToSave As FrmGame.Level, saveLocation As String)
        'saves a level (not the rooms) to a file

        PRE2.WriteFile(saveLocation, CreateLevelString(levelToSave))
    End Sub

    Private Sub btnLevelOpen_Click(sender As Object, e As EventArgs) Handles btnLevelOpen.Click
        Dim openDialog As New OpenFileDialog With {.Filter = "Level file (*.lvl)|*.lvl"}

        If openDialog.ShowDialog() = DialogResult.OK Then
            LoadLevel(openDialog.FileName)
        End If
    End Sub

    Private Sub btnLevelSaveAs_Click(sender As Object, e As EventArgs) Handles btnLevelSaveAs.Click
        Dim fileName As String = InputBox("Enter file name for level")

        If fileName.Length >= 1 Then        'checks that the user actually entered something
            levelSaveLocation = renderer.levelFolderLocation & fileName & ".lvl"
            SaveLevel(thisLevel, levelSaveLocation)
            btnLevelSave.Enabled = True
        End If
    End Sub

    Private Sub btnLevelSave_Click(sender As Object, e As EventArgs) Handles btnLevelSave.Click
        SaveLevel(thisLevel, levelSaveLocation)
    End Sub

    Private Function CreateLevelString(level As FrmGame.Level)
        'creates a string of a level so it can be saved and loaded

        Dim levelString As String = ""

        'adds an addParam line for each parameter
        If Not IsNothing(level.globalParameters) Then
            For Each param As PRE2.Tag In level.globalParameters
                levelString += "addParam: " & param.ToString & Environment.NewLine
            Next param
        End If

        'adds a loadEnt line for each template
        If Not IsNothing(level.templates) Then
            For Each template As PRE2.Entity In level.templates
                If template.HasTag("fileName") = True Then
                    Dim line As String = "loadEnt: " & template.FindTag("fileName").args(0) & "/" & template.name

                    'adds each tag
                    For Each thisTag As PRE2.Tag In template.tags
                        line += "/" & thisTag.ToString
                    Next

                    levelString += line & Environment.NewLine
                Else
                    PRE2.DisplayError("Template " & template.name & " is missing tag 'fileName' so couldn't be saved")
                End If
            Next
        End If

        'adds a loadRoom line for each room
        If Not IsNothing(level.rooms) Then
            For Each currentRoom As FrmGame.Room In level.rooms
                Dim line As String = "loadRoom: "
                line += thisRoom.fileLocation.Remove(renderer.roomFolderLocation)       'the saved part is the location relative to the room folder
                levelString += line & Environment.NewLine
            Next currentRoom
        End If

        Return levelString
    End Function

    'rooms save/load

    Private Sub LoadRoom(fileLocation As String)
        'loads a room from a file (is this necessary?)

        Dim newRoom As FrmGame.Room = FrmGame.LoadRoomFile(fileLocation, thisLevel)

        If IsNothing(thisLevel.rooms) = True Then
            ReDim thisLevel.rooms(0)
        Else
            ReDim Preserve thisLevel.rooms(UBound(thisLevel.rooms) + 1)
        End If
        thisLevel.rooms(UBound(thisLevel.rooms)) = newRoom

        RefreshRoomsList()
        lstRooms.SelectedIndex = UBound(thisLevel.rooms)        'automatically selects the loaded room
    End Sub

    Private Sub SaveRoom(levelOfRoom As FrmGame.Level, roomToSave As FrmGame.Room, saveLocation As String)
        'saves a single room to a file, can only be loaded when the level is loaded

        PRE2.WriteFile(saveLocation, CreateRoomString(levelOfRoom, roomToSave))
    End Sub

    Private Sub LoadEntityTemplate(fileLocation As String)
        'loads an entity saved to a file for a template
		
		Dim entityString As String = PRE2.ReadFile(fileLocation)
		If IsNothing(entityString) = False Then
            Dim newTemplate As PRE2.Entity = EntityStringHandler.ReadEntityString(entityString, renderer)

            If IsNothing(thisLevel.templates) = True Then
                ReDim thisLevel.templates(0)
            Else
                ReDim Preserve thisLevel.templates(UBound(thisLevel.templates) + 1)
            End If
            thisLevel.templates(UBound(thisLevel.templates)) = newTemplate

            RefreshTemplatesList()
        End If
    End Sub

    Private Sub btnRoomOpen_Click(sender As Object, e As EventArgs) Handles btnRoomOpen.Click
        Dim openDialog As New OpenFileDialog With {.Filter = "Room file (*.room)|*.room", .Multiselect = True, .InitialDirectory = roomSaveLocation}

        If openDialog.ShowDialog() = DialogResult.OK Then
            For Each fileLocation As String In openDialog.FileNames
                LoadRoom(fileLocation)
            Next
        End If
    End Sub

    Private Sub btnRoomSaveAs_Click(sender As Object, e As EventArgs) Handles btnRoomSaveAs.Click
        Dim fileName As String = InputBox("Enter file name for room. For example: " & vbCrLf & "- 1,3" & vbCrLf & "- pillars" & vbCrLf & "- room3")

        If fileName.Length >= 1 Then        'checks that the user actually entered something
            roomSaveLocation = renderer.roomFolderLocation & fileName & ".room"
            SaveRoom(thisLevel, thisRoom, roomSaveLocation)
            btnRoomSave.Enabled = True
        End If
    End Sub

    Private Sub btnRoomSave_Click(sender As Object, e As EventArgs) Handles btnRoomSave.Click
        SaveRoom(thisLevel, thisRoom, roomSaveLocation)
    End Sub


    Private Function CreateRoomString(levelOfRoom As FrmGame.Level, roomForString As FrmGame.Room) As String
        'returns a string to save the given room

        Dim roomString As String = ""

        'adds an addEnt line for each instance
        For Each instance As PRE2.Entity In roomForString.instances
            Dim templateOfInstance As PRE2.Entity           'used so that tags can be compared and identical ones can be ignored
            For Each template As PRE2.Entity In levelOfRoom.templates
                If template.name = instance.FindTag("templateName").args(0) Then
                    templateOfInstance = template
                    Exit For
                End If
            Next template

            If IsNothing(templateOfInstance) = True Then
                PRE2.DisplayError("Could not find a template called " & instance.FindTag("templateName").args(0) & " for instance " & instance.name)
            Else
                Dim line As String = "addEnt: " & instance.FindTag("templateName").args(0) & "/" & instance.name

                'adds each added tag to the line, which is not identical to one which the template has
                For Each thisTag As PRE2.Tag In instance.tags
                    If IsNothing(templateOfInstance.FindTag(thisTag.name)) = False AndAlso templateOfInstance.FindTag(thisTag.name) <> thisTag Then
                        line += "/" & thisTag.ToString
                    End If
                Next thisTag

                roomString += line & Environment.NewLine
            End If
        Next instance

        Return roomString
    End Function


	
    'render
	
    Dim renderer As New PRE2
    Dim thisLevel As FrmGame.Level
    Dim thisRoom As FrmGame.Room

    Private Sub RenderCurrentRoom()
        'renders the current room

        renderer.DoGameRender(thisRoom.instances)
    End Sub



    'entities

    Private Sub AddEntityInstance(template As PRE2.Entity)
        'creates a new instance from the given entity

        'Dim templateName As String = template.name

        Dim newInstance As PRE2.Entity = template.Clone
        If Not newInstance.HasTag("templateName") Then      'doesn't add template name if template is an instance of a template
            newInstance.AddTag(New PRE2.Tag("templateName", {template.name}))       'instance stores the name of its template so the instance can be created from the correct template when loading
        End If

        'checks if there are any instances with the same name yet, and numbers the instance accordingly
        If IsNothing(thisRoom.instances) = True Then
            newInstance.name = template.name & "-1"

            ReDim thisRoom.instances(0)
            thisRoom.instances(0) = newInstance
        Else
            Dim copyNumber As Integer = 0           'used to find which number needs to added to the end of the instance name so there aren't any duplicate names
            Dim nameUnique As Boolean = False
            Dim generatedName As String = ""

            Do
                copyNumber += 1
                generatedName = template.name & "-" & Trim(Str(copyNumber))
                nameUnique = True

                'checks if name is unique
                For Each instance As PRE2.Entity In thisRoom.instances
                    If instance.name = generatedName Then
                        nameUnique = False
                        Exit For
                    End If
                Next
            Loop Until nameUnique = True

            newInstance.name = generatedName

            ReDim Preserve thisRoom.instances(UBound(thisRoom.instances) + 1)
            thisRoom.instances(UBound(thisRoom.instances)) = newInstance
        End If

        'template.name = templateName

        RefreshInstancesList()
        RefreshTemplatesList()
    End Sub

    Private Sub RemoveEntityInstance(instanceIndex As Integer)
        'deletes the instance with the given index

        'removes the instance from the room
        If instanceIndex >= 0 And instanceIndex <= UBound(thisRoom.instances) Then
            For index As Integer = instanceIndex To UBound(thisRoom.instances) - 1
                thisRoom.instances(index) = thisRoom.instances(index + 1)
            Next index

            ReDim Preserve thisRoom.instances(UBound(thisRoom.instances) - 1)

        Else
            PRE2.DisplayError("Tried to remove an instance at index " & instanceIndex & " in an array with a max index of " & UBound(thisRoom.instances))
        End If

        RefreshInstancesList()
    End Sub

    Private Sub btnLoadEntity_Click(sender As Object, e As EventArgs) Handles btnLoadEntity.Click
        Dim openDialog As New OpenFileDialog With {.Filter = "Entity files (*.ent)|*.ent", .Multiselect = True, .InitialDirectory = renderer.entityFolderLocation}

        If openDialog.ShowDialog() = DialogResult.OK Then
            For Each fileName As String In openDialog.FileNames
                LoadEntityTemplate(fileName)
            Next
        End If
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
            AddEntityInstance(thisRoom.instances(lstInstances.SelectedIndex))
        Else
            PRE2.DisplayError("No selected instance to duplicate")
        End If
    End Sub

    Private Sub btnInstanceDelete_Click(sender As Object, e As EventArgs) Handles btnInstanceDelete.Click
        'deletes the currently selected instance

        'checks that an instance is selected
        If lstInstances.SelectedIndex > -1 Then
            'asks the user to confirm deleting the instance
            If MsgBox("Are you sure you wish to delete instance " & thisRoom.instances(lstInstances.SelectedIndex).name, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RemoveEntityInstance(lstInstances.SelectedIndex)
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
		End If
    End Sub

    Private Sub RefreshInstancesList()
        If IsNothing(thisRoom.instances) = False Then
            Dim names(UBound(thisRoom.instances)) As String

            For index As Integer = 0 To UBound(thisRoom.instances)
                names(index) = thisRoom.instances(index).name
            Next

            RefreshList(lstInstances, names)
        Else
            RefreshList(lstInstances, Nothing)
        End If
    End Sub

    Private Sub lstTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTemplates.SelectedIndexChanged
        'deselects whatever is selected in lstInstances

        If lstTemplates.SelectedIndex > -1 Then     'checks that this isn't being unselected
            lstInstances.SelectedIndex = -1

            ToggleTagControls(True)
            ShowEntityTags(thisLevel.templates(lstTemplates.SelectedIndex), True)
            btnInstanceCreate.Enabled = True
            btnInstanceDuplicate.Enabled = False
        Else
            If lstInstances.SelectedIndex = -1 Then
                ToggleTagControls(False)    'disables tag controls as there is no selected instance or template
            End If
            btnInstanceCreate.Enabled = False
            btnInstanceDuplicate.Enabled = True
        End If
    End Sub

    Private Sub lstInstances_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstInstances.SelectedIndexChanged
        'deselects whatever is selected in lstTemplates

        If lstInstances.SelectedIndex > -1 Then     'checks that this isn't being unselected
            lstTemplates.SelectedIndex = -1

            ToggleTagControls(True)     'enables tag controls as an instance has been selected
            ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)

            RenderCurrentRoom()
        Else
            If lstTemplates.SelectedIndex = -1 Then
                ToggleTagControls(False)    'disables tag controls as there is no selected instance or template
            End If
        End If
    End Sub



    'tags

    Dim tagControls() As Object

    Private Sub ControlInitialisation()
        tagControls = {txtTagName, numTagLocX, numTagLocY, numTagLayer, numTagScale, lstTags, btnTagAdd, btnTagEdit, btnTagRemove}

        ToggleTagControls(False)
    End Sub

    Private Sub ToggleTagControls(enable As Boolean)
        'enables or disables all controls for tags, depending on whether provided True or False

        For Each ctrl As Object In tagControls
            ctrl.Enabled = enable
        Next
    End Sub

    Private Sub ShowEntityTags(ent As PRE2.Entity, isTemplate As Boolean)
        'changes the values displayed in the controls for tags to show values of the current entity

        txtTagName.Text = ent.name
        If isTemplate = False Then      'templates dont have x and y values
            numTagLocX.Value = ent.location.X
            numTagLocY.Value = ent.location.Y
            numTagLocX.Enabled = True
            numTagLocY.Enabled = True
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
            For Each thisTag As PRE2.Tag In ent.tags
                If IsNothing(thisTag.name) = False Then
                    lstTags.Items.Add(thisTag.ToString)
                End If
            Next thisTag
        End If

    End Sub

    Private Sub numTagLocX_ValueChanged(sender As Object, e As EventArgs) Handles numTagLocX.ValueChanged
        'x position of instance changed

        If lstInstances.SelectedIndex > -1 Then
            thisRoom.instances(lstInstances.SelectedIndex).location = New PointF(numTagLocX.Value, thisRoom.instances(lstInstances.SelectedIndex).location.Y)
            RenderCurrentRoom()
            ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)
        End If
    End Sub

    Private Sub numTagLocY_ValueChanged(sender As Object, e As EventArgs) Handles numTagLocY.ValueChanged
        'y position of instance changed

        If lstInstances.SelectedIndex > -1 Then
            thisRoom.instances(lstInstances.SelectedIndex).location = New PointF(thisRoom.instances(lstInstances.SelectedIndex).location.X, numTagLocY.Value)
            RenderCurrentRoom()
            ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)
        End If
    End Sub

    Private Sub numTagLayer_ValueChanged(sender As Object, e As EventArgs) Handles numTagLayer.ValueChanged
        'z position (layer) of instance changed

        If lstInstances.SelectedIndex > -1 Then
            thisRoom.instances(lstInstances.SelectedIndex).layer = numTagLayer.Value
            RenderCurrentRoom()
            ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)
        End If
    End Sub

    Private Sub numTagScale_ValueChanged(sender As Object, e As EventArgs) Handles numTagScale.ValueChanged
        'scale of instance changed

        If lstInstances.SelectedIndex > -1 Then
            thisRoom.instances(lstInstances.SelectedIndex).scale = numTagScale.Value
            RenderCurrentRoom()
            ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)
        End If
    End Sub

    Private Sub btnTagAdd_Click(sender As Object, e As EventArgs) Handles btnTagAdd.Click
        'adds a tag created by the user using FrmTagMaker

        If lstInstances.SelectedIndex > -1 Then
            Dim tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()

            If tagMaker.userFinished = True Then
                thisRoom.instances(lstInstances.SelectedIndex).AddTag(tagMaker.createdTag)
                ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)
            End If
        End If
    End Sub

    Private Sub btnTagEdit_Click(sender As Object, e As EventArgs) Handles btnTagEdit.Click
        'allows the user to edit a tag using FrmTagMaker

        If lstInstances.SelectedIndex > -1 And lstTags.SelectedIndex > -1 Then
            Dim tagMaker As New FrmTagMaker(thisRoom.instances(lstInstances.SelectedIndex).tags(lstTags.SelectedIndex))
            tagMaker.ShowDialog()

            If tagMaker.userFinished = True Then
                thisRoom.instances(lstInstances.SelectedIndex).tags(lstTags.SelectedIndex) = tagMaker.createdTag
                ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)
            End If
        End If
    End Sub

    Private Sub btnTagRemove_Click(sender As Object, e As EventArgs) Handles btnTagRemove.Click
        'removes the selected tag

        If lstInstances.SelectedIndex > -1 And lstTags.SelectedIndex > -1 Then
            Dim newTags() As PRE2.Tag = thisRoom.instances(lstInstances.SelectedIndex).tags
            For index As Integer = lstTags.SelectedIndex To UBound(newTags) - 1
                newTags(index) = newTags(index + 1)
            Next

            If UBound(newTags) > 0 Then
                ReDim Preserve newTags(UBound(newTags) - 1)
            Else
                newTags = Nothing
            End If

            thisRoom.instances(lstInstances.SelectedIndex).tags = newTags
            ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)
        End If
    End Sub



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
        If IsNothing(thisRoom.parameters) = False Then
            ReDim items(UBound(thisRoom.parameters))

            For index As Integer = 0 To UBound(items)
                items(index) = thisRoom.parameters(index).ToString
            Next index
        End If
        RefreshList(lstRoomParams, items)
    End Sub

    Private Sub AddParameter(newParam As PRE2.Tag, ByRef parameterList() As PRE2.Tag)
        'adds a given parameter to the level

        'If IsNothing(thisLevel.globalParameters) = True Then
        '    ReDim thisLevel.globalParameters(0)
        'Else
        '    ReDim Preserve thisLevel.globalParameters(UBound(thisLevel.globalParameters) + 1)
        'End If
        'thisLevel.globalParameters(UBound(thisLevel.globalParameters)) = newParam

        If IsNothing(parameterList) = True Then
            ReDim parameterList(0)
        Else
            ReDim Preserve parameterList(UBound(parameterList) + 1)
        End If
        parameterList(UBound(parameterList)) = newParam

        RefreshParameterList()
    End Sub

    Private Sub ReplaceParameter(paramIndex As Integer, newParam As PRE2.Tag, parameterList() As PRE2.Tag)
        'replaces the parameter in the given index with the given parameter

        parameterList(paramIndex) = newParam

        RefreshParameterList()
    End Sub

    Private Sub RemoveParameter(paramIndex As Integer, parameterList() As PRE2.Tag)
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
        Dim tagMaker As New FrmTagMaker
        tagMaker.ShowDialog()
        If tagMaker.userFinished = True Then
            AddParameter(tagMaker.createdTag, thisLevel.globalParameters)
        End If
    End Sub

    Private Sub btnLevelParamEdit_Click(sender As Object, e As EventArgs) Handles btnLevelParamEdit.Click
        'user changes an existing parameter using FrmTagMaker

        If lstLevelParams.SelectedIndex > -1 Then
            'gets the user to create a parameter (same as a tag)
            Dim tagMaker As New FrmTagMaker(thisLevel.globalParameters(lstLevelParams.SelectedIndex))
            tagMaker.ShowDialog()
            If tagMaker.userFinished = True Then
                ReplaceParameter(lstLevelParams.SelectedIndex, tagMaker.createdTag, thisLevel.globalParameters)
            End If
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
        Dim tagMaker As New FrmTagMaker
        tagMaker.ShowDialog()
        If tagMaker.userFinished = True Then
            AddParameter(tagMaker.createdTag, thisRoom.parameters)
        End If
    End Sub

    Private Sub btnRemoveRoomParam_Click(sender As Object, e As EventArgs) Handles btnRemoveRoomParam.Click
        'user deletes an existing parameter

        If lstRoomParams.SelectedIndex > -1 Then
            If MsgBox("Are you sure you wish to delete parameter " & thisRoom.parameters(lstRoomParams.SelectedIndex).name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RemoveParameter(lstRoomParams.SelectedIndex, thisRoom.parameters)
            End If
        End If
    End Sub

    Private Sub btnEditRoomParam_Click(sender As Object, e As EventArgs) Handles btnEditRoomParam.Click
        'user changes an existing parameter using FrmTagMaker

        If lstRoomParams.SelectedIndex > -1 Then
            'gets the user to create a parameter (same as a tag)
            Dim tagMaker As New FrmTagMaker(thisRoom.parameters(lstRoomParams.SelectedIndex))
            tagMaker.ShowDialog()
            If tagMaker.userFinished = True Then
                ReplaceParameter(lstRoomParams.SelectedIndex, tagMaker.createdTag, thisRoom.parameters)
            End If
        End If
    End Sub

    Private Sub txtTagName_Leave(sender As Object, e As EventArgs) Handles txtTagName.Leave
        'changes the name of an instance

        Dim newName As String = txtTagName.Text
        Dim oldName As String = thisRoom.instances(lstInstances.SelectedIndex).name

        If newName = "" Then        'resets the name if nothing was entered
            txtTagName.Text = oldName
        Else
            'checks that the name is unique
            Dim nameUnique As Boolean = True
            For Each instance As PRE2.Entity In thisRoom.instances
                If newName = instance.name Then
                    nameUnique = False
                    Exit For
                End If
            Next instance

            If nameUnique = True Then
                thisRoom.instances(lstInstances.SelectedIndex).name = newName
                lstInstances.Items(lstInstances.SelectedIndex) = newName
            Else
                PRE2.DisplayError("This name is already being used")
                txtTagName.Text = oldName
            End If
            ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)
        End If
    End Sub

    

    'rooms 

    Private Sub RefreshRoomsList()	
        If IsNothing(thisLevel.rooms) = False Then
			Dim names(UBound(thisLevel.rooms)) As String
		
            For index As Integer = 0 To UBound(thisLevel.rooms)
                names(index) = thisLevel.rooms(index).name
            Next
			
			RefreshList(lstRooms, names)
        End If
    End Sub

    Private Sub AddRoom(newRoom As FrmGame.Room)
        'adds a new room to the level

        'checks that name isn't already being used
		If IsNothing(thisLevel.rooms) = False Then
			For Each currentRoom As FrmGame.Room In thisLevel.rooms
				If currentRoom.name = newRoom.name Then
					PRE2.DisplayError("Room name (" & newRoom.name & ") is in use, please use a different one")
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
            For Each coords As Point In thisLevel.roomCoords
                If coords = roomCoords Then
                    uniqueCoords = False
                    Exit For
                End If
            Next

            If uniqueCoords = True Then
                thisLevel.roomCoords(roomIndex) = roomCoords

                RefreshRoomsList()
            Else
                PRE2.DisplayError("These coordinates are already being used by another room")
            End If
        End If
    End Sub

    Private Sub lstRooms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRooms.SelectedIndexChanged
        'currently selected room is changed

        If lstRooms.SelectedIndex > -1 Then
            thisRoom = thisLevel.rooms(lstRooms.SelectedIndex)

            lstTemplates.SelectedIndex = -1
            lstInstances.SelectedIndex = -1
            RefreshInstancesList()
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
            AddRoom(New FrmGame.Room With {.name = roomName})
        End If
    End Sub

    Private Sub btnLevelRoomRemove_Click(sender As Object, e As EventArgs) Handles btnLevelRoomRemove.Click
        'an existing room is removed from the level

        'checks that a room is selected
        If lstRooms.SelectedIndex > -1 Then
            'asks the user to confirm deleting the room
            If MsgBox("Are you sure you wish to delete room " & thisRoom.name, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RemoveRoom(lstRooms.SelectedIndex)
            End If
        End If
    End Sub

    Private Sub btnRoomEditCoords_Click(sender As Object, e As EventArgs) Handles btnRoomEditCoords.Click
        'prompts the user to enter new coords for the selected room

        If lstRooms.SelectedIndex > -1 Then
            Dim userInput As String = InputBox("Please enter the new coordinates for " & thisLevel.rooms(lstRooms.SelectedIndex).name & vbCrLf & "Form x,y eg '2,1'")

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
	
	
	
	'general procedures
	
	Private Sub RefreshList(list As ListBox, values() As String)
        'empties a list and fills it with given values

        list.Items.Clear()

        If IsNothing(list) = False Then
            If IsNothing(values) = False Then
                For Each value As String In values
                    list.Items.Add(value)
                Next value
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

        Dim templateSelected As Boolean = If(lstTemplates.SelectedIndex > -1, True, False)
        Dim instanceSelected As Boolean = If(lstInstances.SelectedIndex > -1, True, False)
        Dim roomSelected As Boolean = If(lstRooms.SelectedIndex > -1, True, False)
        Dim entityTagSelected As Boolean = If(lstTags.SelectedIndex > -1, True, False)
        Dim roomParamSelected As Boolean = If(lstRoomParams.SelectedIndex > -1, True, False)
        Dim levelParamSelected As Boolean = If(lstLevelParams.SelectedIndex > -1, True, False)

        Dim controlsDefaultDisabled() As Control = {
            btnInstanceCreate, btnInstanceDuplicate, btnInstanceDelete, btnRemoveEntity, btnLevelSave, btnRoomSaveAs, btnRoomSave,
            btnTagAdd, btnTagEdit, btnTagRemove, btnAddRoomParam, btnEditRoomParam, btnRemoveRoomParam, btnLevelParamRemove, btnLevelParamEdit,
            btnRoomEditCoords, btnLevelRoomRemove
        }
        Dim controlsDefaultEnabled() As Control = {
            btnRoomOpen, btnLevelOpen, btnLevelRoomAdd, btnLoadEntity, btnCreateEntity, btnLevelParamAdd
        }
        For Each ctrl As Control In controlsDefaultDisabled
            ctrl.Enabled = False
        Next ctrl
        For Each ctrl As Control In controlsDefaultEnabled
            ctrl.Enabled = True
        Next

        If roomSelected Then
            btnLevelRoomRemove.Enabled = True
            btnRoomEditCoords.Enabled = True
            btnRoomSaveAs.Enabled = True
            btnAddRoomParam.Enabled = True

            If templateSelected Then
                btnInstanceCreate.Enabled = True
                btnTagAdd.Enabled = True

                If entityTagSelected Then
                    btnTagEdit.Enabled = True
                    btnTagRemove.Enabled = True
                End If
            End If

            If instanceSelected Then
                btnInstanceDuplicate.Enabled = True
                btnInstanceDelete.Enabled = True
            End If

            If roomParamSelected Then
                btnEditRoomParam.Enabled = True
                btnRemoveRoomParam.Enabled = True
            End If
        End If

        If templateSelected Then
            btnRemoveEntity.Enabled = True
        End If

        If levelParamSelected Then
            btnLevelParamEdit.Enabled = True
            btnLevelParamRemove.Enabled = True
        End If
    End Sub


End Class