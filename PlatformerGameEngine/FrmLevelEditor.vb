'Richard Holmes
'29/03/2019
'Level editor for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

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

        renderer = New PRE2 With {.renderPanel = PnlRender}
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

    Private Sub LoadLevel(fileLocation As String)
        'loads a level and sets the interface up

        If IO.File.Exists(fileLocation) Then
            levelSaveLocation = fileLocation
            Dim levelString As String = PRE2.ReadFile(levelSaveLocation)
            createdLevel = New Level(levelString, renderer)

            RefreshRoomsList()
            RefreshActorsList()
            RefreshParameterList()

            RefreshControlsEnabled()
        Else
            PRE2.DisplayError("No file found at " & fileLocation)
        End If
    End Sub

    Private Sub SaveLevel(levelToSave As Level, saveLocation As String)
        'saves a level to a file

        PRE2.WriteFile(saveLocation, levelToSave.ToString)
    End Sub

    Private Sub SaveAsPrompt()
        'asks the user for a name to save the level

        Dim fileName As String = InputBox("Enter file name for level")

        If fileName.Length >= 1 Then        'checks that the user actually entered something
            levelSaveLocation = renderer.levelFolderLocation & fileName & ".lvl"
            SaveLevel(createdLevel, levelSaveLocation)
        End If
    End Sub

    Private Sub ToolBarFileOpen_Click(sender As Object, e As EventArgs) Handles ToolBarFileOpen.Click
        'opens a level file selected by the user

        Using openDialog As New OpenFileDialog With {.Filter = "Level file (*.lvl)|*.lvl", .InitialDirectory = renderer.levelFolderLocation}
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
            Dim savedLevelString As String = PRE2.ReadFile(levelSaveLocation)

            If savedLevelString <> createdLevel.ToString Then
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

    Dim renderer As PRE2
	
    Private Sub RenderCurrentRoom()
        'renders the current room
        If Not IsNothing(renderer) Then
            renderer.renderPanel = PnlRender
            renderer.DoGameRender(SelectedRoom.actors)
        End If
    End Sub

#End Region

#Region "Actors"

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

        Using actorMaker As New FrmActorMaker(Nothing, renderer.spriteFolderLocation)
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
        Using actorMaker As New FrmActorMaker(SelectedActor, renderer.spriteFolderLocation)
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
        If Not IsNothing(SelectedRoom.actors) Then
            Dim names(UBound(SelectedRoom.actors)) As String

            For index As Integer = 0 To UBound(SelectedRoom.actors)
                names(index) = SelectedRoom.actors(index).Name
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
            If LstActorTags.SelectedIndex > -1 And LstActorTags.SelectedIndex <= UBound(Rooms) Then
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

        disableTagChangedEvent = False
    End Sub

    Private Sub KeyTagChanged(sender As Object, e As EventArgs) Handles NumActorLocX.ValueChanged, NumActorLocY.ValueChanged,
        NumActorLayer.ValueChanged, NumActorScale.ValueChanged, TxtActorName.TextChanged
        'updates the key tags (location, layer, scale) of selected actor using the key tags controls' values

        If Not disableTagChangedEvent Then
            SelectedActor.Name = TxtActorName.Text
            SelectedActor.Location = New PointF(NumActorLocX.Value, NumActorLocY.Value)
            SelectedActor.Layer = NumActorLayer.Value
            SelectedActor.Scale = NumActorScale.Value
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
        Dim mouseLocationInRender As New PointF(e.X / renderer.RenderScale.Width, e.Y / renderer.RenderScale.Height)

        'finds which instances the mouse is over
        Dim possibleInstanceIndices() As Integer = Nothing
        For index As Integer = 0 To UBound(SelectedRoom.actors)
            Dim instanceArea As RectangleF = SelectedRoom.actors(index).Hitbox()

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
            relativeHoldLocation = New PointF(SelectedRoom.actors(heldInstanceIndex).Hitbox.Left - mouseLocationInRender.X,
                                              SelectedRoom.actors(heldInstanceIndex).Hitbox.Top - mouseLocationInRender.Y)
            LstActors.SelectedIndex = heldInstanceIndex
        End If
    End Sub

    Private Sub PnlRenderMouseDrag(sender As Object, e As MouseEventArgs) Handles PnlRender.MouseMove
        'mouse moves the held instance

        If heldInstanceIndex >= 0 Then
            SelectedRoom.actors(heldInstanceIndex).Location = New PointF(e.X / renderer.RenderScale.Width + relativeHoldLocation.X,
                                                                            e.Y / renderer.RenderScale.Height + relativeHoldLocation.Y)
            RenderCurrentRoom()
            ShowActorTags(SelectedRoom.actors(heldInstanceIndex))
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

            RefreshParameterList()

            RenderCurrentRoom()
        End Set
    End Property

    Private Property SelectedParameter As Tag
        Get
            If LstLevelParams.SelectedIndex > -1 Then
                Return SelectedActor.tags(LstLevelParams.SelectedIndex)
            Else
                Return New Tag("UnselectedTag")
            End If
        End Get
        Set(value As Tag)
            If LstActors.SelectedIndex > -1 Then
                SelectedActor.tags(LstLevelParams.SelectedIndex) = value
                'Else
                '    PRE2.DisplayError("Tried to modify an tag but none were selected")
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

            RefreshRoomsList()
            RefreshActorsList()

            RenderCurrentRoom()
        End Set
    End Property

    Private Property SelectedRoom As Room
        Get
            If LstRooms.SelectedIndex > -1 And Not IsNothing(Rooms) AndAlso LstRooms.SelectedIndex <= UBound(Rooms) Then
                Return Rooms(LstRooms.SelectedIndex)
            Else
                Return New Room With {.Name = "UnselectedRoom"}
            End If
        End Get
        Set(value As Room)
            If LstRooms.SelectedIndex > -1 And LstRooms.SelectedIndex <= UBound(Rooms) Then
                Rooms(LstRooms.SelectedIndex) = value
            End If
        End Set
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
            LstRooms.SelectedIndex = UBound(Rooms)
        End If
    End Sub

    Private Sub ItmRoomDelete_Click(sender As Object, e As EventArgs) Handles ItmRoomDelete.Click
        If MsgBox("Are you sure you wish to delete room " & SelectedRoom.Name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Rooms = RemoveItem(Rooms, LstRooms.SelectedIndex)
        End If
    End Sub

    Private Sub ItmRoomDuplicate_Click(sender As Object, e As EventArgs) Handles ItmRoomDuplicate.Click
        Rooms = InsertItem(Rooms, SelectedRoom)
        LstRooms.SelectedIndex = UBound(Rooms)
    End Sub

#End Region

#Region "General Procedures"

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
        LstRooms.SelectedIndexChanged, LstActorTags.SelectedIndexChanged

        RefreshControlsEnabled()
    End Sub

    Private Sub RefreshControlsEnabled()
        'enables or disables controls based on current condition

        'Dim templateSelected As Boolean = lstTemplates.SelectedIndex > -1
        Dim actorSelected As Boolean = LstActors.SelectedIndex > -1
        Dim roomSelected As Boolean = LstRooms.SelectedIndex > -1
        Dim actorTagSelected As Boolean = LstActorTags.SelectedIndex > -1
        Dim paramSelected As Boolean = LstLevelParams.SelectedIndex > -1
        'Dim levelParamSelected As Boolean = LstLevelParams.SelectedIndex > -1
        'Dim levelSaveLocationSelected As Boolean = Not IsNothing(levelSaveLocation) AndAlso IO.File.Exists(levelSaveLocation)

        Dim controlsDefaultDisabled() As Object = {
            BtnCreateActor, ItmActorDuplicate, ItmActorDelete, ItmActorDuplicate, BtnAddActorTag,
            ItmTagsEdit, ItmTagsDelete, ItmTagsDuplicate, BtnAddLevelParam, ItmParameterDelete,
            ItmParameterDuplicate, ItmParameterEdit
        }
        Dim controlsDefaultEnabled() As Object = {
            BtnRoomAdd, BtnAddLevelParam
        }
        For Each ctrl As Object In controlsDefaultDisabled
            ctrl.Enabled = False
        Next ctrl
        For Each ctrl As Object In controlsDefaultEnabled
            ctrl.Enabled = True
        Next
        ToggleTagControls(False)

        If roomSelected Then
            BtnCreateActor.Enabled = True
            ItmRoomDelete.Enabled = True
            ItmRoomDuplicate.Enabled = True

            If actorSelected Then
                ToggleTagControls(True)

                ItmActorDelete.Enabled = True
                ItmActorDuplicate.Enabled = True
                ItmActorEdit.Enabled = True
                BtnAddActorTag.Enabled = True

                BtnAddActorTag.Enabled = True
                If actorTagSelected Then
                    ItmTagsDelete.Enabled = True
                    ItmTagsDuplicate.Enabled = True
                    ItmTagsEdit.Enabled = True
                End If
            End If
        End If

        If paramSelected Then
            ItmParameterDelete.Enabled = True
            ItmParameterDuplicate.Enabled = True
            ItmParameterEdit.Enabled = True
        End If
    End Sub




#End Region

End Class
