'Richard Holmes
'29/03/2019
'Level editor for platformer game engine

Public Class FrmLevelEditor

#Region "Initialisation"
    'initialisation

    Private ReadOnly delayTimer As New Timer With {.Interval = 1, .Enabled = False}

    Private Sub FrmLevelEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        delayTimer.Stop()
        delayTimer.Dispose()

        renderEngine = New PanelRenderEngine2 With {.renderPanel = PnlRender}
        createdLevel = New Level

        'LayoutInitialisation()
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

    Dim createdLevel As Level
    Dim levelSaveLocation As String = Nothing

    Private Sub LoadInitialisation()
        'gets the folder locations from the loader file

        Using openDialog As New OpenFileDialog With {.Filter = "Loader file (*.ldr)|*.ldr", .Multiselect = False}
            MsgBox("Please select the loader file for the game")

            If openDialog.ShowDialog() = DialogResult.OK Then
                Dim loaderFileText As String = ReadFile(openDialog.FileName)

                'loads locations of each folder
                Dim topLevelFolder As String = openDialog.FileName.Remove(openDialog.FileName.LastIndexOf("\") + 1)
                renderEngine.levelFolderLocation = topLevelFolder & FindProperty(loaderFileText, "levelFolder")
                'renderer.actorFolderLocation = topLevelFolder & FindProperty(loaderFileText, "actorFolder")
                renderEngine.spriteFolderLocation = topLevelFolder & FindProperty(loaderFileText, "spriteFolder")
                'renderer.roomFolderLocation = topLevelFolder & FindProperty(loaderFileText, "roomFolder")
            Else
                Me.Close()     'might need to change this
            End If
        End Using
    End Sub

    Private Sub LoadLevel(fileLocation As String)
        'loads a level and sets the interface up

        If IO.File.Exists(fileLocation) Then
            levelSaveLocation = fileLocation
            Dim levelString As String = ReadFile(levelSaveLocation)
            createdLevel = New Level(levelString, renderEngine)

            RefreshEverything()

            RefreshControlsEnabled()
        Else
            DisplayError("No file found at " & fileLocation)
        End If
    End Sub

    Private Sub SaveLevel(levelToSave As Level, saveLocation As String)
        'saves a level to a file

        WriteFile(saveLocation, levelToSave.ToString)
    End Sub

    Private Sub SaveAsPrompt()
        'asks the user for a name to save the level

        Dim fileName As String = InputBox("Enter file name for level")

        If fileName.Length >= 1 Then        'checks that the user actually entered something
            levelSaveLocation = renderEngine.levelFolderLocation & fileName & ".lvl"
            SaveLevel(createdLevel, levelSaveLocation)
        End If
    End Sub

    Private Sub ToolBarFileOpen_Click(sender As Object, e As EventArgs) Handles ToolBarFileOpen.Click
        'opens a level file selected by the user

        Using openDialog As New OpenFileDialog With {.Filter = "Level file (*.lvl)|*.lvl", .InitialDirectory = renderEngine.levelFolderLocation}
            If openDialog.ShowDialog() = DialogResult.OK Then
                LoadLevel(openDialog.FileName)
            End If
        End Using
    End Sub

    Private Sub ToolBarFileSaveAs_Click(sender As Object, e As EventArgs) Handles ToolBarFileSaveAs.Click
        SaveAsPrompt()
    End Sub

    Private Sub ToolBarFileSave_Click(sender As Object, e As EventArgs) Handles ToolBarFileSave.Click
        'saves the level if there is a valid save location already or asks the user to save as

        If Not IsNothing(levelSaveLocation) AndAlso IO.File.Exists(levelSaveLocation) Then
            SaveLevel(createdLevel, levelSaveLocation)
        Else
            SaveAsPrompt()
        End If
    End Sub

    Private Sub UserCloseForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'displays a warning to the user if they have unsaved work when they close the form

        Dim unsavedChanges As Boolean = False

        If Not IsNothing(levelSaveLocation) AndAlso IO.File.Exists(levelSaveLocation) Then
            Dim savedLevelString As String = ReadFile(levelSaveLocation)

            If savedLevelString <> createdLevel.ToString Then
                unsavedChanges = True
            End If
        ElseIf Not IsNothing(renderEngine.levelFolderLocation) Then     'no level folder location if form isnt finished loading
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

    Dim renderEngine As PanelRenderEngine2

    Private Sub RenderCurrentRoom()
        'renders the current room
        If Not IsNothing(renderEngine) Then
            renderEngine.renderPanel = PnlRender
            renderEngine.DoGameRenderNoSort(SelectedRoom.actors)
        End If
    End Sub

    Private Sub FrmLevelEditor_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        RenderCurrentRoom()
    End Sub

#End Region

#Region "Actors"

    Private ReadOnly unselectedActor As New Actor With {.Name = "UnselectedActor"}

    Private Property Actors As Actor()
        Get
            Return SelectedRoom.actors
        End Get
        Set(value As Actor())
            SelectedRoom.actors = value

            RefreshActorsList()
            RefreshTagsList()

            RenderCurrentRoom()
        End Set
    End Property

    Private Property SelectedActor As Actor
        Get
            If LstActors.SelectedIndex > -1 Then
                Return SelectedRoom.actors(LstActors.SelectedIndex)
            Else
                Return unselectedActor
            End If
        End Get
        Set(value As Actor)
            If LstActors.SelectedIndex > -1 Then
                SelectedRoom.actors(LstActors.SelectedIndex) = value
            Else
                DisplayError("Tried to modify an actor but none were selected")
            End If
        End Set
    End Property

    Private Sub BtnCreateActor_Click(sender As Object, e As EventArgs) Handles BtnCreateActor.Click
        'opens Actor Maker for user and adds created actor to room

        Using actorMaker As New FrmActorMaker(Nothing, renderEngine.spriteFolderLocation)
            actorMaker.ShowDialog()

            If actorMaker.userFinished Then
                AddActor(actorMaker.createdActor)
            End If
        End Using
    End Sub

    Private Sub ItmActorDelete_Click(sender As Object, e As EventArgs) Handles ItmActorDelete.Click
        Actors = RemoveItem(Actors, LstActors.SelectedIndex)
        RefreshActorsList()
    End Sub

    Private Sub ItmActorEdit_Click(sender As Object, e As EventArgs) Handles ItmActorEdit.Click
        Using actorMaker As New FrmActorMaker(SelectedActor, renderEngine.spriteFolderLocation)
            actorMaker.ShowDialog()

            If actorMaker.userFinished Then
                SelectedActor = actorMaker.createdActor
            End If
        End Using
    End Sub

    Private Sub ItmActorDuplicate_Click(sender As Object, e As EventArgs) Handles ItmActorDuplicate.Click
        AddActor(SelectedActor)
    End Sub

    Private Sub AddActor(ByRef template As Actor)
        'creates a new instance from the given actor

        Dim newActor As Actor = template.Clone

        If Not IsNothing(Actors) Then
            Dim usedNames(UBound(Actors)) As String
            For index As Integer = 0 To UBound(usedNames)
                usedNames(index) = Actors(index).Name
            Next
            newActor.Name = FrmGame.MakeNameUnique(newActor.Name, usedNames, True)
        End If

        Actors = InsertItem(Actors, newActor)
        LstActors.SelectedIndex = UBound(Actors)
    End Sub

    Private Sub RefreshActorsList()
        If Not IsNothing(Actors) Then
            Dim names(UBound(Actors)) As String

            For index As Integer = 0 To UBound(Actors)
                names(index) = Actors(index).Name
            Next

            RefreshList(LstActors, names)
        Else
            RefreshList(LstActors, Nothing)
        End If
    End Sub

    Private Sub LstActors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstActors.SelectedIndexChanged
        RefreshTagsList()
    End Sub

#End Region

#Region "Tags"
    'tags

    Dim disableTagChangedEvent As Boolean = False

    Private Property Tags As Tag()
        Get
            Return SelectedActor.tags
        End Get
        Set(value As Tag())
            SelectedActor.tags = value
            RefreshTagsList()
        End Set
    End Property

    Private Property SelectedTag As Tag
        Get
            If LstActorTags.SelectedIndex > -1 And LstActorTags.SelectedIndex <= UBound(SelectedActor.tags) Then
                Return SelectedActor.tags(LstActorTags.SelectedIndex)
            Else
                Return New Tag("UnselectedTag", Nothing)
            End If
        End Get
        Set(value As Tag)
            If LstActorTags.SelectedIndex > -1 Then
                Tags(LstActorTags.SelectedIndex) = value
            End If
        End Set
    End Property

    'Private Sub ControlInitialisation()
    '    tagControls = {txtTagName, numTagLocX, numTagLocY, numTagLayer, numTagScale, lstTags, btnTagAdd, btnTagEdit, btnTagRemove}

    '    ToggleTagControls(False)
    'End Sub

    Private Sub RefreshTagsList()
        If Not IsNothing(Tags) Then
            Dim items(UBound(Tags)) As String
            For index As Integer = 0 To UBound(items)
                items(index) = Tags(index).ToString
            Next
            RefreshList(LstActorTags, items)
        Else
            RefreshList(LstActorTags, Nothing)
        End If

        ShowActorTags(SelectedActor)
    End Sub

    Private Sub ToggleTagControls(enabled As Boolean)
        'enables or disables all controls for tags, depending on whether provided True or False

        Dim tagControls() As Control = {TxtActorName, NumActorLocX, NumActorLocY,
            NumActorLayer, NumActorScale, LstActorTags, BtnAddActorTag}

        For Each ctrl As Control In tagControls
            ctrl.Enabled = enabled
        Next
    End Sub

    Private Sub ShowActorTags(ByVal displayActor As Actor)
        'changes the values displayed in the controls for tags to show values of the current actor

        disableTagChangedEvent = True

        If IsNothing(displayActor) Then      'if no actor provided then uses an empty actor
            displayActor = New Actor       'this doesn't work as actors have some default properties
            'ToggleTagControls(False)
        End If

        TxtActorName.Text = RemoveQuotes(displayActor.Name)
        NumActorLocX.Value = displayActor.Location.X
        NumActorLocY.Value = displayActor.Location.Y
        NumActorLayer.Value = displayActor.Layer
        NumActorScale.Value = displayActor.Scale

        disableTagChangedEvent = False
    End Sub

    Private Sub KeyTagChanged(sender As Object, e As EventArgs) Handles NumActorLocX.ValueChanged, NumActorLocY.ValueChanged,
        NumActorLayer.ValueChanged, NumActorScale.ValueChanged, TxtActorName.TextChanged
        'updates the key tags (location, layer, scale) of selected actor using the key tags controls' values

        If Not disableTagChangedEvent Then
            disableTagChangedEvent = True
            SelectedActor.Name = TxtActorName.Text
            SelectedActor.Location = New PointF(NumActorLocX.Value, NumActorLocY.Value)
            SelectedActor.Layer = NumActorLayer.Value
            SelectedActor.Scale = NumActorScale.Value

            disableTagChangedEvent = False
            End If
        RefreshTagsList()
        RenderCurrentRoom()
    End Sub

    Private Sub BtnAddActorTag_Click(sender As Object, e As EventArgs) Handles BtnAddActorTag.Click
        Using tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()
            If tagMaker.userFinished Then
                Tags = InsertItem(Tags, tagMaker.CreatedTag)
                LstActorTags.SelectedIndex = UBound(Tags)
            End If
        End Using
    End Sub

    Private Sub ItmTagsDelete_Click(sender As Object, e As EventArgs) Handles ItmTagsDelete.Click
        If MsgBox("Are you sure you wish to delete tag " & SelectedTag.name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Tags = RemoveItem(Tags, LstActorTags.SelectedIndex)
        End If
    End Sub

    Private Sub ItmTagsEdit_Click(sender As Object, e As EventArgs) Handles ItmTagsEdit.Click
        Using tagMaker As New FrmTagMaker(SelectedTag)
            tagMaker.ShowDialog()
            If tagMaker.userFinished Then
                SelectedTag = tagMaker.CreatedTag
            End If
        End Using
    End Sub

    Private Sub ItmTagsDuplicate_Click(sender As Object, e As EventArgs) Handles ItmTagsDuplicate.Click
        SelectedActor.AddTag(SelectedTag, False)
        LstActorTags.SelectedIndex = UBound(Tags)
    End Sub


#Region "Mouse Location Control"

    Dim heldInstanceIndex As Integer = -1           'index of the actor being held by the user
    Dim relativeHoldLocation As PointF = Nothing    'used so that the mouse holds a specific location on the instance

    Private Sub PnlRenderMouseDown(sender As Object, e As MouseEventArgs) Handles PnlRender.MouseDown
        'mouse starts holding the instance underneath it

        'gets the relative mouse location in the game render
        Dim mouseLocationInRender As New PointF(e.X / renderEngine.RenderScale.Width, e.Y / renderEngine.RenderScale.Height)

        'finds which instances the mouse is over
        Dim possibleInstanceIndices() As Integer = Nothing
        If Not IsNothing(Actors) Then
            For index As Integer = 0 To UBound(Actors)
                Dim instanceArea As RectangleF = Actors(index).Hitbox()

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

            If Not IsNothing(possibleInstanceIndices) Then
                'finds which instance has the highest index
                Dim topMostInstanceIndex As Integer = possibleInstanceIndices(0)
                For index As Integer = 0 To UBound(possibleInstanceIndices)
                    If Actors(possibleInstanceIndices(index)).Layer > Actors(topMostInstanceIndex).Layer Then
                        topMostInstanceIndex = possibleInstanceIndices(index)
                    End If
                Next

                'starts holding the top most actor
                heldInstanceIndex = topMostInstanceIndex
                relativeHoldLocation = New PointF(Actors(heldInstanceIndex).Hitbox.Left - mouseLocationInRender.X,
                                                  Actors(heldInstanceIndex).Hitbox.Top - mouseLocationInRender.Y)

                'show the user which actor is selected by changing the selected actor in the actor list
                LstActors.SelectedIndex = heldInstanceIndex
            End If
        End If
    End Sub

    Private Sub PnlRenderMouseDrag(sender As Object, e As MouseEventArgs) Handles PnlRender.MouseMove
        'mouse moves the held instance

        If heldInstanceIndex >= 0 Then
            'calculates where the mouse has moved the held actor to
            Actors(heldInstanceIndex).Location = New PointF(e.X / renderEngine.RenderScale.Width + relativeHoldLocation.X,
                                                            e.Y / renderEngine.RenderScale.Height + relativeHoldLocation.Y)
            RenderCurrentRoom()
            ShowActorTags(Actors(heldInstanceIndex))
        End If
    End Sub

    Private Sub PnlRenderMouseUp(sender As Object, e As MouseEventArgs) Handles PnlRender.MouseUp
        'mouse lets go of the held instance

        heldInstanceIndex = -1
    End Sub

#End Region

#End Region

#Region "Parameters"

    Private Property Parameters As Tag()
        Get
            Return createdLevel.tags
        End Get
        Set(value As Tag())
            createdLevel.tags = value

            RefreshEverything()
        End Set
    End Property

    Private Property SelectedParameter As Tag
        Get
            If LstLevelParams.SelectedIndex > -1 Then
                Return createdLevel.tags(LstLevelParams.SelectedIndex)
            Else
                Return New Tag("UnselectedTag")
            End If
        End Get
        Set(value As Tag)
            If LstLevelParams.SelectedIndex > -1 Then
                createdLevel.tags(LstLevelParams.SelectedIndex) = value

                RefreshEverything()
            End If
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
                Parameters = InsertItem(Parameters, tagMaker.CreatedTag)
            End If
        End Using
    End Sub

    Private Sub ItmParameterDelete_Click(sender As Object, e As EventArgs) Handles ItmParameterDelete.Click
        If MsgBox("Are you sure you wish to delete parameter " & SelectedParameter.name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Parameters = RemoveItem(Parameters, LstLevelParams.SelectedIndex)
        End If
    End Sub

    Private Sub ItmParameterEdit_Click(sender As Object, e As EventArgs) Handles ItmParameterEdit.Click
        Using tagMaker As New FrmTagMaker(SelectedParameter)
            tagMaker.ShowDialog()
            If tagMaker.userFinished Then
                SelectedParameter = tagMaker.CreatedTag
            End If
        End Using
    End Sub

    Private Sub ItmParameterDuplicate_Click(sender As Object, e As EventArgs) Handles ItmParameterDuplicate.Click
        createdLevel.AddTag(SelectedParameter, False)
    End Sub

#End Region

#Region "Rooms"

    Private Property Rooms As Room()
        Get
            Return createdLevel.rooms
        End Get
        Set(value As Room())
            createdLevel.rooms = value

            RefreshEverything()
        End Set
    End Property

    Private Property SelectedRoom As Room
        Get
            If LstRooms.SelectedIndex > -1 And Not IsNothing(Rooms) AndAlso LstRooms.SelectedIndex <= UBound(Rooms) Then
                Return Rooms(LstRooms.SelectedIndex)
            Else
                Return New Room With {.name = "UnselectedRoom"}
            End If
        End Get
        Set(value As Room)
            If LstRooms.SelectedIndex > -1 And LstRooms.SelectedIndex <= UBound(Rooms) Then
                Rooms(LstRooms.SelectedIndex) = value
                RefreshEverything()
            End If
        End Set
    End Property

    Private Sub RefreshRoomsList()
        If Not IsNothing(Rooms) Then
            Dim items(UBound(Rooms)) As String
            For index As Integer = 0 To UBound(Rooms)
                items(index) = Rooms(index).name
            Next

            RefreshList(LstRooms, items)
        End If
    End Sub

    Private Sub LstRooms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstRooms.SelectedIndexChanged
        'currently selected room is changed

        LstActors.SelectedIndex = -1
        RefreshActorsList()
        RenderCurrentRoom()
    End Sub

    Private Sub BtnRoomAdd_Click(sender As Object, e As EventArgs) Handles BtnRoomAdd.Click
        'a new room is added to the level

        Dim roomName As String = InputBox("Please enter a name for the new room")

        If Not IsNothing(roomName) AndAlso roomName.Length > 0 Then
            Rooms = InsertItem(Rooms, New Room With {.name = roomName})
            LstRooms.SelectedIndex = UBound(Rooms)      'automatically selects the new room
        End If
    End Sub

    Private Sub ItmRoomDelete_Click(sender As Object, e As EventArgs) Handles ItmRoomDelete.Click
        If MsgBox("Are you sure you wish to delete room " & SelectedRoom.name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Rooms = RemoveItem(Rooms, LstRooms.SelectedIndex)
        End If
    End Sub

    Private Sub ItmRoomEdit_Click(sender As Object, e As EventArgs) Handles ItmRoomEdit.Click
        Dim newName As String = InputBox("Please enter the new name for " & SelectedRoom.name)

        If Not IsNothing(newName) AndAlso newName.Length > 0 Then
            SelectedRoom.name = newName
            RefreshEverything()
        End If
    End Sub

    Private Sub ItmRoomDuplicate_Click(sender As Object, e As EventArgs) Handles ItmRoomDuplicate.Click
        Rooms = InsertItem(Rooms, SelectedRoom)
        LstRooms.SelectedIndex = UBound(Rooms)  'automatically selects the new room
    End Sub

#End Region

#Region "General Procedures"

    Private Sub AnySelectionChanged(sender As Object, e As EventArgs) Handles _
        LstActors.SelectedIndexChanged, LstLevelParams.SelectedIndexChanged, LstLevelParams.SelectedIndexChanged,
        LstRooms.SelectedIndexChanged, LstActorTags.SelectedIndexChanged

        RefreshControlsEnabled()
    End Sub

    Private Sub RefreshControlsEnabled()
        'enables or disables controls based on which lists have a selected item

        Dim actorSelected As Boolean = LstActors.SelectedIndex > -1
        Dim roomSelected As Boolean = LstRooms.SelectedIndex > -1
        Dim actorTagSelected As Boolean = LstActorTags.SelectedIndex > -1
        Dim paramSelected As Boolean = LstLevelParams.SelectedIndex > -1

        'enabled if a room is selected
        BtnCreateActor.Enabled = roomSelected
        ItmRoomDelete.Enabled = roomSelected
        ItmRoomDuplicate.Enabled = roomSelected

        'enabled if an actor is selected
        ItmActorDelete.Enabled = actorSelected
        ItmActorDuplicate.Enabled = actorSelected
        ItmActorEdit.Enabled = actorSelected
        BtnAddActorTag.Enabled = actorSelected
        ToggleTagControls(actorSelected)

        'enabled if a tag is selected
        ItmTagsDelete.Enabled = actorTagSelected
        ItmTagsDuplicate.Enabled = actorTagSelected
        ItmTagsEdit.Enabled = actorTagSelected

        'enabled if a parameter is selected
        ItmParameterDelete.Enabled = paramSelected
        ItmParameterDuplicate.Enabled = paramSelected
        ItmParameterEdit.Enabled = paramSelected
    End Sub

    Private Sub RefreshEverything()
        'updates all the lists and renders the current room
        'used to show the current state of the level to the user

        RefreshParameterList()
        RefreshRoomsList()
        RefreshActorsList()
        RefreshTagsList()
        RenderCurrentRoom()
    End Sub




#End Region

#Region "Testing"

    Private Sub ToolBarTestStart_Click(sender As Object, e As EventArgs) Handles ToolBarTestStart.Click
        'opens game executor with the current level

        Using executor As New FrmGame(createdLevel.ToString)
            executor.ShowDialog()
        End Using
    End Sub

#End Region

End Class
