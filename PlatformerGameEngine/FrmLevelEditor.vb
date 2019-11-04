'Richard Holmes
'29/03/2019
'Level editor for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmLevelEditor

#Region "Initialisation"
    'initialisation

    ReadOnly delayTimer As New Timer With {.Interval = 1, .Enabled = False}

    Private Sub FrmLevelEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        delayTimer.Stop()

        renderer = New PRE2 With {.renderPanel = PnlRender}

        LayoutInitialisation()
        'ControlInitialisation()
        LoadInitialisation()
        RefreshControlsEnabled()
    End Sub

    'Private Sub LayoutInitialisation()
    '    'TblControlsOverall.Location = New Point(PnlRender.Right + 10, PnlRender.Top)
    '    'TblRooms.Location = New Point(PnlRender.Left, PnlRender.Bottom + 10)

    '    'Me.Size = New Size(TblControlsOverall.Right + 20, TblControlsOverall.Bottom + 40)

    '    'flwSaveLoad.Location = New Point(pnlRender.Right + 10, pnlRender.Top)
    '    'tblActors.Location = New Point(flwSaveLoad.Left, flwSaveLoad.Bottom + 5)
    '    'tblTagsSummary.Location = New Point(tblActors.Right + 10, tblActors.Top)
    '    'tblTagsDetailed.Location = New Point(tblTagsSummary.Left, tblTagsSummary.Bottom + 5)
    'End Sub

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
                renderer.actorFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "actorFolder")
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
            RefreshActorsList()
            'RefreshTemplatesList()
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

    Private Sub btnLevelOpen_Click(sender As Object, e As EventArgs) Handles
        Using openDialog As New OpenFileDialog With {.Filter = "Level file (*.lvl)|*.lvl", .InitialDirectory = renderer.levelFolderLocation}
            If openDialog.ShowDialog() = DialogResult.OK Then
                LoadLevel(openDialog.FileName)
            End If
        End Using
    End Sub

    Private Sub BtnLevelSaveAs_Click(sender As Object, e As EventArgs) Handles
        Dim fileName As String = InputBox("Enter file name for level")

        If fileName.Length >= 1 Then        'checks that the user actually entered something
            levelSaveLocation = renderer.levelFolderLocation & fileName & ".lvl"
            SaveLevel(thisLevel, levelSaveLocation)
            btnLevelSave.Enabled = True
        End If
    End Sub

    Private Sub BtnLevelSave_Click(sender As Object, e As EventArgs)
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



    Private Sub RenderCurrentRoom()
        'renders the current room

        renderer.DoGameRender(SelectedRoom.actors)
    End Sub

#End Region

#Region "Actors"

#Region "Templates"

    'Private Sub LoadActorTemplate(fileLocation As String)
    '    'loads an actor saved to a file for a template

    '    Dim actorString As String = PRE2.ReadFile(fileLocation)
    '    If Not IsNothing(actorString) Then
    '        Dim successfulLoad As Boolean = False
    '        Dim newTemplate As Actor = ActorStringHandler.ReadActorString(actorString, renderer, successfulLoad)

    '        If successfulLoad Then
    '            If Not IsNothing(thisLevel.templates) Then      'makes name unique
    '                Dim templateNames(UBound(thisLevel.templates)) As String
    '                For index As Integer = 0 To UBound(templateNames)
    '                    templateNames(index) = thisLevel.templates(index).Name
    '                Next index
    '                newTemplate.Name = FrmGame.MakeNameUnique(newTemplate.Name, templateNames, True)
    '            End If

    '            newTemplate.AddTag(New Tag("fileName", AddQuotes(fileLocation.Remove(0, Len(renderer.actorFolderLocation)))))

    '            If IsNothing(thisLevel.templates) = True Then
    '                ReDim thisLevel.templates(0)
    '            Else
    '                ReDim Preserve thisLevel.templates(UBound(thisLevel.templates) + 1)
    '            End If
    '            thisLevel.templates(UBound(thisLevel.templates)) = newTemplate
    '        End If

    '        RefreshTemplatesList()
    '    End If
    'End Sub

    'Private Sub RemoveActorTemplate(templateIndex As Integer)
    '    'removes the template of an actor and all instances made from that template from the level

    '    If Not IsNothing(thisLevel.templates) Then
    '        If templateIndex >= 0 And templateIndex <= UBound(thisLevel.templates) Then
    '            'first removes all instances with the templateName tag which is the same as the name of the template
    '            Dim templateName As String = thisLevel.templates(templateIndex).Name

    '            If Not IsNothing(thisLevel.rooms) Then
    '                'goes through each room
    '                For roomIndex As Integer = 0 To UBound(thisLevel.rooms)
    '                    Dim currentRoom As Room = thisLevel.rooms(roomIndex)
    '                    LstRooms.SelectedIndex = roomIndex

    '                    If Not IsNothing(currentRoom.actors) AndAlso UBound(currentRoom.actors) >= 0 Then
    '                        Dim instanceIndex As Integer = 0
    '                        Do
    '                            Dim currentInstance As Actor = currentRoom.actors(instanceIndex)

    '                            If currentInstance.FindTag("templateName").InterpretArgument = templateName Then
    '                                'removes current instance if the templateName matches
    '                                RemoveActorInstance(instanceIndex)

    '                                'For index As Integer = instanceIndex To UBound(currentRoom.instances) - 1
    '                                '    currentRoom.instances(index) = currentRoom.instances(index + 1)
    '                                'Next index

    '                                'ReDim Preserve currentRoom.instances(UBound(currentRoom.instances) - 1)
    '                            Else
    '                                instanceIndex += 1
    '                            End If
    '                        Loop Until instanceIndex > UBound(currentRoom.actors) - 1

    '                        thisLevel.rooms(roomIndex) = currentRoom
    '                    End If
    '                Next roomIndex
    '            End If

    '            'then removes the template itself
    '            For index As Integer = templateIndex To UBound(thisLevel.templates) - 1
    '                thisLevel.templates(index) = thisLevel.templates(index + 1)
    '            Next index

    '            If UBound(thisLevel.templates) > 0 Then
    '                ReDim Preserve thisLevel.templates(UBound(thisLevel.templates) - 1)
    '            Else
    '                thisLevel.templates = Nothing
    '            End If
    '        Else
    '            PRE2.DisplayError("Provided template index (" & templateIndex & ") was an out of bounds")
    '        End If
    '    Else
    '        PRE2.DisplayError("There is currently no templates")
    '    End If

    '    RefreshActorsList()
    '    RefreshTemplatesList()

    'End Sub


    'Private Sub btnLoadActor_Click(sender As Object, e As EventArgs)
    '    Using openDialog As New OpenFileDialog With {.Filter = "Actor files (*.ent)|*.ent", .Multiselect = True, .InitialDirectory = renderer.actorFolderLocation}

    '        If openDialog.ShowDialog() = DialogResult.OK Then
    '            For Each fileName As String In openDialog.FileNames
    '                LoadActorTemplate(fileName)
    '            Next
    '        End If
    '    End Using
    'End Sub

    'Private Sub btnRemoveActor_Click(sender As Object, e As EventArgs)
    '    If lstTemplates.SelectedIndex > -1 Then
    '        If MsgBox("Are you sure you wish to remove this template?" & Environment.NewLine &
    '              "This will also remove all instances which use this template") = DialogResult.OK Then
    '            RemoveActorTemplate(lstTemplates.SelectedIndex)
    '        End If
    '    End If
    'End Sub


    'Private Sub RefreshTemplatesList()
    '    If IsNothing(thisLevel.templates) = False Then
    '        Dim names(UBound(thisLevel.templates)) As String

    '        For index As Integer = 0 To UBound(thisLevel.templates)
    '            names(index) = thisLevel.templates(index).Name
    '        Next

    '        RefreshList(lstTemplates, names)
    '    Else
    '        RefreshList(lstTemplates, Nothing)
    '    End If
    'End Sub

    'Private Sub LstTemplates_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    'deselects whatever is selected in lstInstances

    '    If lstTemplates.SelectedIndex > -1 Then     'checks that this isn't being unselected
    '        LstActors.SelectedIndex = -1

    '        'ToggleTagControls(True)
    '        ShowActorTags(thisLevel.templates(lstTemplates.SelectedIndex), True)
    '    Else
    '        If LstActors.SelectedIndex = -1 Then
    '            'ToggleTagControls(False)    'disables tag controls as there is no selected instance or template
    '            ShowActorTags(Nothing, False)
    '        End If
    '    End If
    'End Sub



#End Region

#Region "Instances"

    Private Property Actors As Actor()
        Get
            Return SelectedRoom.actors
        End Get
        Set(value As Actor())
            SelectedRoom.actors = value

            RefreshActorsList()
            RefreshActorTagsList()

            RenderCurrentRoom()
        End Set
    End Property

    Private Property SelectedActor As Actor
        Get
            If LstActors.SelectedIndex > -1 Then
                Return SelectedRoom.actors(LstActors.SelectedIndex)
            Else
                Return New Actor With {.Name = "UnselectedActor"}
            End If
        End Get
        Set(value As Actor)
            If LstActors.SelectedIndex > -1 Then
                SelectedRoom.actors(LstActors.SelectedIndex) = value
            Else
                PRE2.DisplayError("Tried to modify an actor but none were selected")
            End If
        End Set
    End Property

    Private Sub BtnCreateActor_Click(sender As Object, e As EventArgs) Handles BtnCreateActor.Click
        'opens Actor Maker for user and adds created actor to room

        Using actorMaker As New FrmActorMaker(renderEngine:=renderer)
            actorMaker.ShowDialog()

            If actorMaker.userFinished Then
                AddActor(actorMaker.result)
                'ReDim Preserve CurrentRoom.instances(UBound(CurrentRoom.instances) + 1)
                'CurrentRoom.instances(UBound(CurrentRoom.instances)) = actorMaker.result
            End If
        End Using
    End Sub

    Private Sub ItmActorDelete_Click(sender As Object, e As EventArgs) Handles ItmActorDelete.Click
        RemoveItem(SelectedRoom.actors, LstActors.SelectedIndex)
        RefreshActorsList()
    End Sub

    Private Sub ItmActorEdit_Click(sender As Object, e As EventArgs) Handles ItmActorEdit.Click
        Using actorMaker As New FrmActorMaker(SelectedActor, renderer)
            actorMaker.ShowDialog()

            If actorMaker.userFinished Then
                SelectedActor = actorMaker.result
            End If
        End Using
    End Sub

    Private Sub ItmActorDuplicate_Click(sender As Object, e As EventArgs) Handles ItmActorDuplicate.Click
        AddActor(SelectedActor)
    End Sub

    Private Sub AddActor(ByVal template As Actor)
        'creates a new instance from the given actor

        'Dim templateName As String = template.name

        Dim newActor As Actor = template.Clone
        'If Not newInstance.HasTag("templateName") Then      'doesn't add template name if template is an instance of a template
        '    newInstance.AddTag(New Tag("templateName", template.Name))       'instance stores the name of its template so the instance can be created from the correct template when loading
        'End If

        Dim usedNames(UBound(Actors)) As String
        For index As Integer = 0 To UBound(usedNames)
            usedNames(index) = Actors(index).Name
        Next
        newActor.Name = FrmGame.MakeNameUnique(newActor.Name, usedNames, True)

        AppendItem(Actors, newActor)

        'checks if there are any instances with the same name yet, and numbers the instance accordingly
        'If IsNothing(SelectedRoom.instances) Then
        '    newInstance.Name = RemoveQuotes(template.Name) & "-1"

        '    ReDim thisLevel.rooms(LstRooms.SelectedIndex).instances(0)
        '    SelectedRoom.instances(0) = newInstance
        'Else
        '    Dim instanceNames(UBound(SelectedRoom.instances)) As String

        '    For index As Integer = 0 To UBound(SelectedRoom.instances)
        '        instanceNames(index) = SelectedRoom.instances(index).Name
        '    Next index

        '    Dim newName As String = FrmGame.MakeNameUnique(newInstance.Name, instanceNames, False)

        '    newInstance.Name = newName

        '    ReDim Preserve thisLevel.rooms(LstRooms.SelectedIndex).instances(UBound(SelectedRoom.instances) + 1)
        '    SelectedRoom.instances(UBound(SelectedRoom.instances)) = newInstance
        'End If

        'template.name = templateName

        'RefreshActorsList()
        'RefreshTemplatesList()
    End Sub

    'Private Sub RemoveActorInstance(instanceIndex As Integer)
    '    'deletes the instance with the given index

    '    'removes the instance from the room
    '    If instanceIndex >= 0 And instanceIndex <= UBound(SelectedRoom.instances) Then
    '        For index As Integer = instanceIndex To UBound(SelectedRoom.instances) - 1
    '            SelectedRoom.instances(index) = SelectedRoom.instances(index + 1)
    '        Next index

    '        If UBound(SelectedRoom.instances) Then
    '            ReDim Preserve thisLevel.rooms(LstRooms.SelectedIndex).instances(UBound(SelectedRoom.instances) - 1)
    '        Else
    '            thisLevel.rooms(LstRooms.SelectedIndex).instances = Nothing
    '        End If
    '    Else
    '        PRE2.DisplayError("Tried to remove an instance at index " & instanceIndex & " in an array with a max index of " & UBound(SelectedRoom.instances))
    '    End If

    '    RefreshInstancesList()
    'End Sub


    'Private Sub btnInstanceCreate_Click(sender As Object, e As EventArgs) Handles BtnCreateActor.Click
    '    'creates a new instance of the currently selected template

    '    'checks that a template is selected
    '    If lstTemplates.SelectedIndex > -1 Then
    '        AddActorInstance(thisLevel.templates(lstTemplates.SelectedIndex))
    '    Else
    '        PRE2.DisplayError("No selected template to create an instance from")
    '    End If
    'End Sub

    'Private Sub btnInstanceDuplicate_Click(sender As Object, e As EventArgs)
    '    'creates a new instance as a copy of the currently selected instance

    '    'checks that an instance is selected
    '    If LstActors.SelectedIndex > -1 Then
    '        AddActorInstance(SelectedRoom.instances(LstActors.SelectedIndex))
    '    Else
    '        PRE2.DisplayError("No selected instance to duplicate")
    '    End If
    'End Sub

    'Private Sub btnInstanceDelete_Click(sender As Object, e As EventArgs)
    '    'deletes the currently selected instance

    '    'checks that an instance is selected
    '    If LstActors.SelectedIndex > -1 Then
    '        'asks the user to confirm deleting the instance
    '        If MsgBox("Are you sure you wish to delete instance " & SelectedRoom.instances(LstActors.SelectedIndex).Name, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
    '            RemoveActorInstance(LstActors.SelectedIndex)
    '        End If
    '    End If
    'End Sub


    Private Sub RefreshActorsList()
        If IsNothing(SelectedRoom.actors) = False Then
            Dim names(UBound(SelectedRoom.actors)) As String

            For index As Integer = 0 To UBound(SelectedRoom.actors)
                names(index) = SelectedRoom.actors(index).Name
            Next

            RefreshList(LstActors, names)
        Else
            RefreshList(LstActors, Nothing)
        End If
    End Sub

    'Private Sub lstInstances_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstActors.SelectedIndexChanged
    '    'deselects whatever is selected in lstTemplates

    '    If LstActors.SelectedIndex > -1 Then     'checks that this isn't being unselected
    '        lstTemplates.SelectedIndex = -1

    '        'ToggleTagControls(True)     'enables tag controls as an instance has been selected
    '        ShowActorTags(SelectedRoom.instances(LstActors.SelectedIndex), False)

    '        RenderCurrentRoom()
    '    Else
    '        If lstTemplates.SelectedIndex = -1 Then
    '            'ToggleTagControls(False)    'disables tag controls as there is no selected instance or template
    '            ShowActorTags(Nothing, False)
    '        End If
    '    End If
    'End Sub

#End Region

#End Region

#Region "Tags"
    'tags

    Dim disableTagChangedEvent As Boolean = False

    'Private Sub ControlInitialisation()
    '    tagControls = {txtTagName, numTagLocX, numTagLocY, numTagLayer, numTagScale, lstTags, btnTagAdd, btnTagEdit, btnTagRemove}

    '    ToggleTagControls(False)
    'End Sub

    Private Sub RefreshActorTagsList()
        Dim items(UBound(SelectedActor.tags)) As String
        For index As Integer = 0 To UBound(items)
            items(index) = SelectedActor.tags(index).ToString
        Next
        RefreshList(LstActorTags, items)

        ShowActorTags(SelectedActor)
    End Sub

    Private Sub ToggleTagControls(enabled As Boolean)
        'enables or disables all controls for tags, depending on whether provided True or False

        Dim tagControls() As Control = {TxtActorName, NumActorLocX, NumActorLocY, NumActorLayer, NumActorScale, LstActorTags, BtnAddActorTag}

        For Each ctrl As Object In tagControls
            ctrl.Enabled = enabled
        Next
    End Sub

    Private Sub ShowActorTags(ByVal displayActor As Actor)
        'changes the values displayed in the controls for tags to show values of the current actor

        disableTagChangedEvent = True

        If IsNothing(displayActor) Then      'if no actor provided then uses an empty actor
            displayActor = New Actor With {.Name = "No Actor Selected"}       'this doesn't work as actors have some default properties
            'ToggleTagControls(False)
        End If

        TxtActorName.Text = RemoveQuotes(displayActor.Name)
        NumActorLocX.Value = displayActor.Location.X
        NumActorLocY.Value = displayActor.Location.Y
        If TxtActorName.Enabled Then
            NumActorLocX.Enabled = True
            NumActorLocY.Enabled = True
        End If
        NumActorLayer.Value = displayActor.Layer
        NumActorScale.Value = displayActor.Scale

        'updates lstTags
        LstActorTags.Items.Clear()
        If IsNothing(displayActor.tags) = False Then
            For Each thisTag As Tag In displayActor.tags
                If IsNothing(thisTag.name) = False Then
                    LstActorTags.Items.Add(thisTag.ToString)
                End If
            Next thisTag
        End If

        disableTagChangedEvent = False
    End Sub

    Private Sub KeyTagChanged(sender As Object, e As EventArgs) Handles NumActorLocX.ValueChanged, NumActorLocY.ValueChanged,
        NumActorLayer.ValueChanged, NumActorScale.ValueChanged ', txtTagName.TextChanged
        'updates the key tags (location, layer, scale) of selected actor using the key tags controls' values

        If Not disableTagChangedEvent Then
            'selects the actor to update, currently selected instance or template
            Dim actorToUpdate As Actor
            If LstActors.SelectedIndex > -1 Then
                actorToUpdate = SelectedRoom.actors(LstActors.SelectedIndex)
            ElseIf lstTemplates.SelectedIndex > -1 Then
                actorToUpdate = thisLevel.templates(lstTemplates.SelectedIndex)
            Else
                Exit Sub
            End If

            'actorToUpdate.name = txtTagName.Text
            actorToUpdate.Location = New PointF(NumActorLocX.Value, NumActorLocY.Value)
            actorToUpdate.Layer = NumActorLayer.Value
            actorToUpdate.Scale = NumActorScale.Value

            If LstActors.SelectedIndex > -1 Then
                thisLevel.rooms(LstRooms.SelectedIndex).actors(LstActors.SelectedIndex) = actorToUpdate
            Else
                thisLevel.templates(lstTemplates.SelectedIndex) = actorToUpdate
            End If

            ShowActorTags(actorToUpdate, lstTemplates.SelectedIndex > -1)

            RenderCurrentRoom()
        End If
    End Sub

    'Private Sub btnTagAdd_Click(sender As Object, e As EventArgs) Handles BtnAddActorTag.Click
    '    'adds a tag created by the user using FrmTagMaker

    '    Using tagMaker As New FrmTagMaker
    '        tagMaker.ShowDialog()

    '        If tagMaker.userFinished = True Then
    '            If LstActors.SelectedIndex > -1 Then
    '                SelectedRoom.actors(LstActors.SelectedIndex).AddTag(tagMaker.CreatedTag)
    '                ShowActorTags(SelectedRoom.actors(LstActors.SelectedIndex), False)
    '            ElseIf lstTemplates.SelectedIndex > -1 Then
    '                thisLevel.templates(lstTemplates.SelectedIndex).AddTag(tagMaker.CreatedTag)
    '                ShowActorTags(thisLevel.templates(lstTemplates.SelectedIndex), True)
    '            End If
    '        End If
    '    End Using
    'End Sub

    'Private Sub btnTagEdit_Click(sender As Object, e As EventArgs)
    '    'allows the user to edit a tag using FrmTagMaker

    '    Dim tagIndex As Integer = LstActorTags.SelectedIndex
    '    Dim instanceIndex As Integer = LstActors.SelectedIndex
    '    Dim templateIndex As Integer = lstTemplates.SelectedIndex

    '    Dim tagMaker As FrmTagMaker = New FrmTagMaker
    '    If tagIndex > -1 Then
    '        If instanceIndex > -1 Then
    '            tagMaker = New FrmTagMaker(SelectedRoom.actors(instanceIndex).tags(tagIndex))
    '        ElseIf templateIndex > -1 Then
    '            tagMaker = New FrmTagMaker(thisLevel.templates(templateIndex).tags(tagIndex))
    '        Else
    '            Exit Sub
    '        End If
    '        tagMaker.ShowDialog()

    '        If tagMaker.userFinished = True Then
    '            If instanceIndex > -1 Then
    '                SelectedRoom.actors(instanceIndex).SetTag(tagIndex, tagMaker.CreatedTag)
    '                ShowActorTags(SelectedRoom.actors(instanceIndex), False)
    '            ElseIf templateIndex > -1 Then
    '                thisLevel.templates(templateIndex).SetTag(tagIndex, tagMaker.CreatedTag)
    '                ShowActorTags(thisLevel.templates(templateIndex), True)
    '            End If
    '        End If
    '    End If
    '    tagMaker.Dispose()
    'End Sub

    'Private Sub BtnTagRemove_Click(sender As Object, e As EventArgs)
    '    'removes the selected tag

    '    Dim newTags() As Tag
    '    If LstActors.SelectedIndex > -1 Then
    '        newTags = SelectedRoom.actors(LstActors.SelectedIndex).tags
    '    ElseIf lstTemplates.SelectedIndex > -1 Then
    '        newTags = thisLevel.templates(lstTemplates.SelectedIndex).tags
    '    Else
    '        Exit Sub
    '    End If

    '    For index As Integer = LstActorTags.SelectedIndex To UBound(newTags) - 1
    '        newTags(index) = newTags(index + 1)
    '    Next

    '    If UBound(newTags) > 0 Then
    '        ReDim Preserve newTags(UBound(newTags) - 1)
    '    Else
    '        newTags = Nothing
    '    End If

    '    If LstActors.SelectedIndex > -1 Then
    '        SelectedRoom.actors(LstActors.SelectedIndex).tags = newTags
    '        ShowActorTags(SelectedRoom.actors(LstActors.SelectedIndex), False)
    '    ElseIf lstTemplates.SelectedIndex > -1 Then
    '        thisLevel.templates(lstTemplates.SelectedIndex).tags = newTags
    '        ShowActorTags(thisLevel.templates(lstTemplates.SelectedIndex), True)
    '    End If
    'End Sub

    Private Sub TxtTagName_Leave(sender As Object, e As EventArgs) Handles TxtActorName.Leave
        'changes the name of an instance or template

        'If lstTemplates.SelectedIndex > -1 Then
        '    thisLevel.templates(lstTemplates.SelectedIndex).Name = TxtActorName.Text
        'ElseIf LstActors.SelectedIndex > -1 Then
        SelectedRoom.actors(LstActors.SelectedIndex).Name = TxtActorName.Text
        'End If
    End Sub


#Region "Mouse Location Control"

    Dim heldInstanceIndex As Integer = -1           'index of the actor being held by the user
    Dim relativeHoldLocation As PointF = Nothing    'used so that the mouse holds a specific location on the instance

    Private Sub PnlRenderMouseDown(sender As Object, e As MouseEventArgs) Handles PnlRender.MouseDown
        'mouse starts holding the instance underneath it

        'gets the relative mouse location in the game render
        Dim mouseLocationInRender As New PointF(e.X / renderer.RenderScale.Width, e.Y / renderer.RenderScale.Height)

        'finds which instances the mouse is over
        Dim possibleInstanceIndices() As Integer = Nothing
        For index As Integer = 0 To UBound(SelectedRoom.actors)
            Dim instanceArea As RectangleF = SelectedRoom.actors(index).GetActorHitbox()

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
                If SelectedRoom.actors(possibleInstanceIndices(index)).Layer > SelectedRoom.actors(topMostInstanceIndex).Layer Then
                    topMostInstanceIndex = possibleInstanceIndices(index)
                End If
            Next

            heldInstanceIndex = topMostInstanceIndex
            relativeHoldLocation = New PointF(SelectedRoom.actors(heldInstanceIndex).GetActorHitbox.Left - mouseLocationInRender.X,
                                              SelectedRoom.actors(heldInstanceIndex).GetActorHitbox.Top - mouseLocationInRender.Y)
            LstActors.SelectedIndex = heldInstanceIndex
        End If
    End Sub

    Private Sub PnlRenderMouseDrag(sender As Object, e As MouseEventArgs) Handles PnlRender.MouseMove
        'mouse moves the held instance

        If heldInstanceIndex >= 0 Then
            SelectedRoom.actors(heldInstanceIndex).Location = New PointF(e.X / renderer.RenderScale.Width + relativeHoldLocation.X,
                                                                            e.Y / renderer.RenderScale.Height + relativeHoldLocation.Y)
            RenderCurrentRoom()
            ShowActorTags(SelectedRoom.actors(heldInstanceIndex), False)
        End If
    End Sub

    Private Sub PnlRenderMouseUp(sender As Object, e As MouseEventArgs) Handles PnlRender.MouseUp
        'mouse lets go of the held instance

        heldInstanceIndex = -1
    End Sub

#End Region

#End Region

#Region "Parameters"
    'parameters

    Private Property Parameters As Tag()
        Get
            Return thisLevel.parameters
        End Get
        Set(value As Tag())
            thisLevel.parameters = value

            RefreshParameterList()

            RenderCurrentRoom()
        End Set
    End Property

    Private Sub RefreshParameterList()
        'refreshes LstLevelParams

        If Not IsNothing(Parameters) Then
            Dim items(UBound(Parameters)) As String

            For index As Integer = 0 To UBound(items)
                items(index) = Parameters(index).ToString
            Next index
            RefreshList(LstLevelParams, items)
        Else
            RefreshList(LstLevelParams, Nothing)
        End If

    End Sub

    'Private Sub AddParameter(newParam As Tag, ByRef parameterList() As Tag)
    '    'adds a given parameter to the level

    '    If IsNothing(parameterList) = True Then
    '        ReDim parameterList(0)
    '    Else
    '        ReDim Preserve parameterList(UBound(parameterList) + 1)
    '    End If
    '    parameterList(UBound(parameterList)) = newParam

    '    RefreshParameterList()
    'End Sub

    'Private Sub ReplaceParameter(paramIndex As Integer, newParam As Tag, parameterList() As Tag)
    '    'replaces the parameter in the given index with the given parameter

    '    parameterList(paramIndex) = newParam

    '    RefreshParameterList()
    'End Sub

    'Private Sub RemoveParameter(paramIndex As Integer, ByRef parameterList() As Tag)
    '    'removes the parameter at the given index

    '    For index As Integer = paramIndex To UBound(parameterList) - 1
    '        parameterList(index) = parameterList(index + 1)
    '    Next index

    '    If UBound(parameterList) > 0 Then
    '        ReDim Preserve parameterList(UBound(parameterList) - 1)
    '    Else
    '        parameterList = Nothing
    '    End If

    '    RefreshParameterList()
    'End Sub

    'level params

    Private Sub LstLevelParams_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstLevelParams.SelectedIndexChanged
        'a different parameter is selected for editing/removing

        If LstLevelParams.SelectedIndex > -1 Then

        End If
    End Sub

    Private Sub BtnAddLevelParam_Click(sender As Object, e As EventArgs) Handles BtnAddLevelParam.Click
        'user creates a new parameter using FrmTagMaker

        'gets the user to create a parameter (same as a tag)
        Using tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()
            If tagMaker.userFinished Then
                AppendItem(Parameters, tagMaker.CreatedTag)
            End If
        End Using
    End Sub

    'Private Sub btnLevelParamEdit_Click(sender As Object, e As EventArgs)
    '    'user changes an existing parameter using FrmTagMaker

    '    If LstLevelParams.SelectedIndex > -1 Then
    '        'gets the user to create a parameter (same as a tag)
    '        Using tagMaker As New FrmTagMaker(thisLevel.parameters(LstLevelParams.SelectedIndex))
    '            tagMaker.ShowDialog()
    '            If tagMaker.userFinished = True Then
    '                ReplaceParameter(LstLevelParams.SelectedIndex, tagMaker.CreatedTag, thisLevel.parameters)
    '            End If
    '        End Using
    '    End If
    'End Sub

    'Private Sub btnLevelParamRemove_Click(sender As Object, e As EventArgs)
    '    'user deletes an existing parameter

    '    If LstLevelParams.SelectedIndex > -1 Then
    '        If MsgBox("Are you sure you wish to delete parameter " & thisLevel.parameters(LstLevelParams.SelectedIndex).name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
    '            RemoveParameter(LstLevelParams.SelectedIndex, thisLevel.parameters)
    '        End If
    '    End If
    'End Sub

    'room params

    'Private Sub lstRoomParams_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstLevelParams.SelectedIndexChanged
    '    'a different parameter is selected for editing/removing

    '    If LstLevelParams.SelectedIndex > -1 Then
    '        btnEditRoomParam.Enabled = True
    '        btnRemoveRoomParam.Enabled = True
    '    Else
    '        btnEditRoomParam.Enabled = False
    '        btnRemoveRoomParam.Enabled = False
    '    End If
    'End Sub

    'Private Sub btnAddRoomParam_Click(sender As Object, e As EventArgs) Handles BtnAddLevelParam.Click
    '    'user creates a new parameter using FrmTagMaker

    '    'gets the user to create a parameter (same as a tag)
    '    Using tagMaker As New FrmTagMaker
    '        tagMaker.ShowDialog()
    '        If tagMaker.userFinished = True Then
    '            AddParameter(tagMaker.CreatedTag, thisLevel.rooms(LstRooms.SelectedIndex).parameters)
    '        End If
    '    End Using
    'End Sub

    'Private Sub btnRemoveRoomParam_Click(sender As Object, e As EventArgs)
    '    'user deletes an existing parameter

    '    If LstLevelParams.SelectedIndex > -1 Then
    '        If MsgBox("Are you sure you wish to delete parameter " & SelectedRoom.parameters(LstLevelParams.SelectedIndex).name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
    '            RemoveParameter(LstLevelParams.SelectedIndex, thisLevel.rooms(LstRooms.SelectedIndex).parameters)
    '        End If
    '    End If
    'End Sub

    'Private Sub btnEditRoomParam_Click(sender As Object, e As EventArgs)
    '    'user changes an existing parameter using FrmTagMaker

    '    If LstLevelParams.SelectedIndex > -1 Then
    '        'gets the user to create a parameter (same as a tag)
    '        Using tagMaker As New FrmTagMaker(SelectedRoom.parameters(LstLevelParams.SelectedIndex))
    '            tagMaker.ShowDialog()
    '            If tagMaker.userFinished = True Then
    '                ReplaceParameter(LstLevelParams.SelectedIndex, tagMaker.CreatedTag, thisLevel.rooms(LstRooms.SelectedIndex).parameters)
    '            End If
    '        End Using
    '    End If
    'End Sub

#End Region

#Region "Rooms"

    Private Property Rooms As Room()
        Get
            Return thisLevel.rooms
        End Get
        Set(value As Room())
            thisLevel.rooms = value

            RefreshRoomsList()
            RefreshActorsList()

            RenderCurrentRoom()
        End Set
    End Property

    Private ReadOnly Property SelectedRoom As Room
        Get
            If LstRooms.SelectedIndex > -1 And Not IsNothing(thisLevel.rooms) AndAlso LstRooms.SelectedIndex <= UBound(thisLevel.rooms) Then
                Return thisLevel.rooms(LstRooms.SelectedIndex)
            Else
                Return New Room With {.Name = "UnselectedRoom"}
            End If
        End Get
    End Property

    Private Sub RefreshRoomsList()
        If IsNothing(Rooms) = False Then
            Dim roomNames(UBound(Rooms)) As String

            For index As Integer = 0 To UBound(Rooms)
                roomNames(index) = Rooms(index).Name
            Next

            RefreshList(LstRooms, roomNames)
        End If
    End Sub

    Private Sub AddRoom(newRoom As Room)
        'adds a new room to the level

        'checks that name isn't already being used
        AppendItem(Rooms, newRoom)
    End Sub

    Private Sub RemoveRoom(roomIndex As Integer)
        'removes the room at the given index

        RemoveItem(Rooms, roomIndex)
    End Sub

    'Private Sub SetRoomCoords(roomIndex As Integer, roomCoords As Point)
    '    'sets the coordinates of the room at the given index to the given coords

    '    If IsNothing(thisLevel.rooms) = False AndAlso roomIndex >= 0 And roomIndex <= UBound(thisLevel.rooms) Then
    '        'checks if the new coords are being used by another room
    '        Dim uniqueCoords As Boolean = True
    '        'For Each coords As Point In thisLevel.roomCoords
    '        '    If coords = roomCoords Then
    '        '        uniqueCoords = False
    '        '        Exit For
    '        '    End If
    '        'Next

    '        If uniqueCoords = True Then
    '            'thisLevel.roomCoords(roomIndex) = roomCoords

    '            RefreshRoomsList()
    '        Else
    '            PRE2.DisplayError("These coordinates are already being used by another room")
    '        End If
    '    End If
    'End Sub

    Private Sub LstRooms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstRooms.SelectedIndexChanged
        'currently selected room is changed

        If LstRooms.SelectedIndex > -1 Then
            'thisRoom = thisLevel.rooms(lstRooms.SelectedIndex)

            'lstTemplates.SelectedIndex = -1
            LstActors.SelectedIndex = -1
            RefreshActorsList()
            RefreshParameterList()
            RenderCurrentRoom()

            'btnLevelRoomRemove.Enabled = True
        Else
            'btnLevelRoomRemove.Enabled = False

        End If
    End Sub

    Private Sub BtnRoomAdd_Click(sender As Object, e As EventArgs) Handles BtnRoomAdd.Click
        'a new room is added to the level

        Dim roomName As String = InputBox("Please enter a name for the new room")

        If Not IsNothing(roomName) AndAlso roomName.Length > 0 Then
            AddRoom(New Room With {.Name = roomName})
        End If
    End Sub

    'Private Sub btnLevelRoomRemove_Click(sender As Object, e As EventArgs)
    '    'an existing room is removed from the level

    '    'checks that a room is selected
    '    If LstRooms.SelectedIndex > -1 Then
    '        'asks the user to confirm deleting the room
    '        If MsgBox("Are you sure you wish to delete room " & SelectedRoom.Name, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
    '            RemoveRoom(LstRooms.SelectedIndex)
    '        End If
    '    End If
    'End Sub

    'Private Sub btnRoomEditCoords_Click(sender As Object, e As EventArgs)
    '    'prompts the user to enter new coords for the selected room

    '    If LstRooms.SelectedIndex > -1 Then
    '        Dim userInput As String = InputBox("Please enter the new coordinates for " & thisLevel.rooms(LstRooms.SelectedIndex).Name & vbCrLf & "Form x,y eg '2,1'")

    '        If userInput.Length > 0 Then
    '            'checks that input is valid
    '            If IsNothing(userInput.Split(",")(0)) = False And IsNothing(userInput.Split(",")(1)) = False AndAlso IsNumeric(userInput.Split(",")(0)) = True And IsNumeric(userInput.Split(",")(1)) = True Then
    '                Dim newCoords As New Point(Int(userInput.Split(",")(0)), Int(userInput.Split(",")(1)))

    '                SetRoomCoords(LstRooms.SelectedIndex, newCoords)
    '            Else
    '                PRE2.DisplayError(userInput & " is not valid input for coordinates")
    '            End If
    '        End If
    '    End If
    'End Sub

#End Region

#Region "General Procedures"
    'general procedures

    Private Sub RefreshList(list As ListBox, values() As String)
        'empties a list and fills it with given values

        If Not IsNothing(list) Then
            Dim startSelectedIndex As Integer = list.SelectedIndex
            list.SelectedIndex = -1
            list.Items.Clear()

            If Not IsNothing(values) Then
                For Each value As String In values
                    If Not IsNothing(value) Then
                        list.Items.Add(value)
                    End If
                Next value

                If startSelectedIndex < list.Items.Count Then
                    list.SelectedIndex = startSelectedIndex
                End If
            End If
        Else
            'PRE2.DisplayError("A list tried to refresh but doesn't exist")
        End If
    End Sub

    Private Sub AnySelectionChanged(sender As Object, e As EventArgs) Handles _
        LstActors.SelectedIndexChanged, LstLevelParams.SelectedIndexChanged, LstLevelParams.SelectedIndexChanged,
        LstRooms.SelectedIndexChanged, LstActorTags.SelectedIndexChanged, lstTemplates.SelectedIndexChanged

        RefreshControlsEnabled()
    End Sub

    Private Sub RefreshControlsEnabled()
        'enables or disables controls based on current condition

        Dim templateSelected As Boolean = lstTemplates.SelectedIndex > -1
        Dim instanceSelected As Boolean = LstActors.SelectedIndex > -1
        Dim roomSelected As Boolean = LstRooms.SelectedIndex > -1
        Dim actorTagSelected As Boolean = LstActorTags.SelectedIndex > -1
        Dim roomParamSelected As Boolean = LstLevelParams.SelectedIndex > -1
        Dim levelParamSelected As Boolean = LstLevelParams.SelectedIndex > -1
        Dim levelSaveLocationSelected As Boolean = Not IsNothing(levelSaveLocation) AndAlso IO.File.Exists(levelSaveLocation)

        Dim controlsDefaultDisabled() As Control = {
            BtnCreateActor, btnInstanceDuplicate, btnInstanceDelete, btnRemoveActor, btnLevelSave,
            BtnAddActorTag, btnTagEdit, btnTagRemove, BtnAddLevelParam, btnEditRoomParam, btnRemoveRoomParam, btnLevelParamRemove, btnLevelParamEdit,
            btnRoomEditCoords, btnLevelRoomRemove
        }
        Dim controlsDefaultEnabled() As Control = {
            btnLevelOpen, BtnRoomAdd, btnLoadActor, BtnCreateActor, btnLevelParamAdd
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
            BtnAddLevelParam.Enabled = True

            If templateSelected Then
                BtnCreateActor.Enabled = True
                ToggleTagControls(True)
            End If

            If instanceSelected Then
                ToggleTagControls(True)
                btnInstanceDuplicate.Enabled = True
                btnInstanceDelete.Enabled = True

                BtnAddActorTag.Enabled = True
                If actorTagSelected Then
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
            btnRemoveActor.Enabled = True

            BtnAddActorTag.Enabled = True
            If actorTagSelected Then
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

    Private Sub RemoveItem(ByRef array As Object, ByVal removeIndex As Integer)
        'removes the item at the given index from a given array

        If IsArray(array) Then
            If UBound(array) > 0 Then
                For index As Integer = removeIndex To UBound(array) - 1
                    array(index) = array(index + 1)
                Next
                ReDim Preserve array(UBound(array) - 1)
            Else
                array = Nothing
            End If
        End If
    End Sub

    Private Sub AppendItem(ByRef array As Object, ByVal newItem As Object)
        'adds the given item to the end of the given array

        If IsArray(array) Then
            If UBound(array) > 0 Then
                ReDim Preserve array(UBound(array) + 1)
                array(UBound(array)) = newItem
            Else
                array = {newItem}
            End If
        End If
    End Sub

#End Region




End Class