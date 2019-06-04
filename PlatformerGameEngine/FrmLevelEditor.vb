'Richard Holmes
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
            End     'might need to change this
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

        Dim levelString As String = ""

        'adds an addParam line for each parameter
        For Each param As PRE2.Tag In levelToSave.parameters
            levelString += "addParam: " & param.ToString
        Next param



        'adds a loadEnt line for each template
        For Each template As PRE2.Entity In levelToSave.templates
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

        'adds a loadRoom line for each room
        For Each currentRoom As FrmGame.Room In levelToSave.rooms

        Next currentRoom

        'saves level string to file
        PRE2.WriteFile(saveLocation, levelString)
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


    'rooms

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
        Dim openDialog As New OpenFileDialog With {.Filter = "Room file (*.room)|*.room", .Multiselect = True}

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

    Private Sub AddEntityInstance(ByVal template As PRE2.Entity)
        'creates a new instance from the given entity

        Dim newInstance As PRE2.Entity = template
        newInstance.AddTag(New PRE2.Tag("templateName", {template.name}))       'instance stores the name of its template so the instance can be created from the correct template when loading

        'checks that there are any instances yet
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

        RefreshInstancesList()
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
        Dim openDialog As New OpenFileDialog With {.Filter = "Entity files (*.ent)|*.ent", .Multiselect = True}

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
        'clears lstTemplates then adds all templates names back again

        lstTemplates.Items.Clear()

        For Each template As PRE2.Entity In thisLevel.templates
            lstTemplates.Items.Add(template.name)
        Next
    End Sub

    Private Sub RefreshInstancesList()
        'clears lstInstances then adds all instances names back again

        lstInstances.Items.Clear()

        For Each instance As PRE2.Entity In thisRoom.instances
            lstInstances.Items.Add(instance.name)
        Next
    End Sub

    Private Sub lstTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTemplates.SelectedIndexChanged
        'deselects whatever is selected in lstInstances

        If lstTemplates.SelectedIndex > -1 Then     'checks that this isn't being unselected
            lstInstances.SelectedIndex = -1

            ToggleTagControls(True)
            ShowEntityTags(thisLevel.templates(lstTemplates.SelectedIndex), True)
            btnInstanceCreate.Enabled = True
        Else
            If lstInstances.SelectedIndex = -1 Then
                ToggleTagControls(False)    'disables tag controls as there is no selected instance or template
            End If
            btnInstanceCreate.Enabled = False
        End If
    End Sub

    Private Sub lstInstances_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstInstances.SelectedIndexChanged
        'deselects whatever is selected in lstTemplates

        If lstInstances.SelectedIndex > -1 Then     'checks that this isn't being unselected
            lstTemplates.SelectedIndex = -1

            ToggleTagControls(True)     'enables tag controls as an instance has been selected
            ShowEntityTags(thisRoom.instances(lstInstances.SelectedIndex), False)
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
            If ent.HasTag("x") Then
                numTagLocX.Value = ent.FindTag("x").args(0)
            End If
            If ent.HasTag("y") Then
                numTagLocY.Value = ent.FindTag("y").args(0)
            End If
            numTagLocX.Enabled = True
                numTagLocY.Enabled = True
            Else
                numTagLocX.Value = 0
            numTagLocY.Value = 0
            numTagLocX.Enabled = False
            numTagLocY.Enabled = False
        End If
        If ent.HasTag("layer") Then
            numTagLayer.Value = ent.FindTag("layer").args(0)
        End If
        If ent.HasTag("scale") Then
            numTagScale.Value = ent.FindTag("scale").args(0)
        End If

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




    'parameters

    Private Sub RefreshParameterList()
        Dim items(UBound(thisLevel.parameters)) As String

        For index As Integer = 0 To UBound(items)
            items(index) = thisLevel.parameters(index).ToString
        Next index

        RefreshList(lstParams, items)
    End Sub

    Private Sub AddParameter(newParam As PRE2.Tag)
        'adds a given parameter to the level

        If IsNothing(thisLevel.parameters) = True Then
            ReDim thisLevel.parameters(0)
        Else
            ReDim Preserve thisLevel.parameters(UBound(thisLevel.parameters) + 1)
        End If
        thisLevel.parameters(UBound(thisLevel.parameters)) = newParam

        RefreshParameterList()
    End Sub

    Private Sub ReplaceParameter(paramIndex As Integer, newParam As PRE2.Tag)
        'replaces the parameter in the given index with the given parameter

        thisLevel.parameters(paramIndex) = newParam

        RefreshParameterList()
    End Sub

    Private Sub RemoveParameter(paramIndex As Integer)
        'removes the parameter at the given index

        For index As Integer = paramIndex To UBound(thisLevel.parameters) - 1
            thisLevel.parameters(index) = thisLevel.parameters(index + 1)
        Next index

        If UBound(thisLevel.parameters) > 0 Then
            ReDim Preserve thisLevel.parameters(UBound(thisLevel.parameters) - 1)
        Else
            thisLevel.parameters = {}
        End If
    End Sub

    Private Sub lstParams_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstParams.SelectedIndexChanged
        'a different parameter is selected for editing/removing

        If lstParams.SelectedIndex > -1 Then
            btnParamEdit.Enabled = True
            btnParamRemove.Enabled = True
        Else
            btnParamEdit.Enabled = False
            btnParamRemove.Enabled = False
        End If
    End Sub

    Private Sub btnParamAdd_Click(sender As Object, e As EventArgs) Handles btnParamAdd.Click
        'user creates a new parameter using FrmTagMaker

        'gets the user to create a parameter (same as a tag)
        Dim tagMaker As New FrmTagMaker
        tagMaker.ShowDialog()
        If tagMaker.userFinished = True Then
            AddParameter(tagMaker.createdTag)
        End If
    End Sub

    Private Sub btnParamEdit_Click(sender As Object, e As EventArgs) Handles btnParamEdit.Click
        'user changes an existing parameter using FrmTagMaker

        If lstParams.SelectedIndex > -1 Then
            'gets the user to create a parameter (same as a tag)
            Dim tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()
            If tagMaker.userFinished = True Then
                ReplaceParameter(lstParams.SelectedIndex, tagMaker.createdTag)
            End If
        End If
    End Sub

    Private Sub btnParamRemove_Click(sender As Object, e As EventArgs) Handles btnParamRemove.Click
        'user deletes an existing parameter

        If lstParams.SelectedIndex > -1 Then
            If MsgBox("Are you sure you wish to delete parameter " & thisLevel.parameters(lstParams.SelectedIndex).name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RemoveParameter(lstParams.SelectedIndex)
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
        End If
    End Sub

    Private Sub numTagLocX_ValueChanged(sender As Object, e As EventArgs) Handles numTagLocX.ValueChanged
        'x position of instance changed

        If lstInstances.SelectedIndex > -1 Then
            thisRoom.instances(lstInstances.SelectedIndex).location.X = numTagLocX.Value
            RenderCurrentRoom()
        End If
    End Sub

    Private Sub numTagLocY_ValueChanged(sender As Object, e As EventArgs) Handles numTagLocY.ValueChanged
        'y position of instance changed

        If lstInstances.SelectedIndex > -1 Then
            thisRoom.instances(lstInstances.SelectedIndex).location.Y = numTagLocY.Value
            RenderCurrentRoom()
        End If
    End Sub

    Private Sub numTagLayer_ValueChanged(sender As Object, e As EventArgs) Handles numTagLayer.ValueChanged
        'z position (layer) of instance changed

        If lstInstances.SelectedIndex > -1 Then
            thisRoom.instances(lstInstances.SelectedIndex).layer = numTagLayer.Value
            RenderCurrentRoom()
        End If
    End Sub

    Private Sub numTagScale_ValueChanged(sender As Object, e As EventArgs) Handles numTagScale.ValueChanged
        'scale of instance changed

        If lstInstances.SelectedIndex > -1 Then
            thisRoom.instances(lstInstances.SelectedIndex).scale = numTagScale.Value
            RenderCurrentRoom()
        End If
    End Sub



    'rooms

    Private Sub RefreshList(list As ListBox, values() As String)
        'empties a list and fills it with given values

        list.Items.Clear()

        If IsNothing(values) = False Then
            For Each value As String In values
                list.Items.Add(value)
            Next value
        End If
    End Sub

    Private Sub RefreshRoomsList()
        'empties lstRooms and fills it again

        lstRooms.Items.Clear()

        If IsNothing(thisLevel.rooms) = False Then
            For Each currentRoom As FrmGame.Room In thisLevel.rooms
                lstRooms.Items.Add(currentRoom.name)
            Next
        End If
    End Sub

    Private Sub AddRoom(newRoom As FrmGame.Room)
        'adds a new room to the level

        'checks that name isn't already being used
        For Each currentRoom As FrmGame.Room In thisLevel.rooms
            If currentRoom.name = newRoom.name Then
                PRE2.DisplayError("This room name is already being used, please use a different name")
                Exit Sub
            End If
        Next

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
            thisLevel.rooms = {}
        End If
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

        Dim roomName As String = MsgBox("Please enter a name for the new room")

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


End Class