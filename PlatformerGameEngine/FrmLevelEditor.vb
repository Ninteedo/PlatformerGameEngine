Imports PlatformerGameEngine.My.Resources

Public Class FrmLevelEditor

#Region "Constructors"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _createdLevel = New Level({})
        _renderEngine = New RenderEngine(PnlRender)

        RefreshControlsEnabled()
    End Sub

#End Region

#Region "Save/Load"

    Dim _createdLevel As Level
    Dim _levelSaveLocation As String = Nothing

    Private Sub LoadLevel(fileLocation As String)
        'loads a level and sets the interface up

        _levelSaveLocation = fileLocation
        Dim levelString As String = ReadFile(_levelSaveLocation)
        _createdLevel = New Level(levelString)

        RefreshEverything()
        RefreshControlsEnabled()
    End Sub

    Private Sub SaveLevel()
        'saves created level to file at level save location

        WriteFile(_levelSaveLocation, _createdLevel.ToString)
    End Sub

    Private Sub SaveAsPrompt()
        'asks the user for a Name to save the level

        Using saveDialog As New SaveFileDialog With {.Filter = LevelFileFilter}
            If saveDialog.ShowDialog() = DialogResult.OK Then        'checks that the user actually entered something
                _levelSaveLocation = saveDialog.FileName
                SaveLevel()
            End If
        End Using
    End Sub

    Private Sub ToolBarFileOpen_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarFileOpen.Click
        'opens a level file selected by the user

        If UnsavedChanges AndAlso MsgBox("Opening a new level will lead to loss of unsaved work. Continue?", MsgBoxStyle.OkCancel) <> DialogResult.OK Then
            Exit Sub    'doesn't continue onto open dialog
        End If

        Using openDialog As New OpenFileDialog With {.Filter = LevelFileFilter, .Title = "Open Level"}
            If openDialog.ShowDialog() = DialogResult.OK Then
                LoadLevel(openDialog.FileName)
            End If
        End Using
    End Sub

    Private Sub ToolBarFileSaveAs_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarFileSaveAs.Click
        SaveAsPrompt()
    End Sub

    Private Sub ToolBarFileSave_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarFileSave.Click
        'saves the level if there is a valid save location already or asks the user to save as

        If Not IsNothing(_levelSaveLocation) Then
            SaveLevel()
        Else
            SaveAsPrompt()
        End If
    End Sub

    Private Sub UserCloseForm(sender As FrmLevelEditor, e As FormClosingEventArgs) Handles Me.FormClosing
        'displays a warning to the user if they have unsaved work when they close the form

        If UnsavedChanges Then
            If MsgBox("There are unsaved changes, do wish to close anyway?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private ReadOnly Property UnsavedChanges As Boolean
        Get
            'checks if the saved level matches the level currently in the level editor
            Dim result As Boolean = False

            If IO.File.Exists(_levelSaveLocation) Then
                Dim savedLevelString As String = ReadFile(_levelSaveLocation)

                If savedLevelString <> _createdLevel.ToString Then
                    result = True
                End If
            ElseIf _createdLevel <> New Level({}) Then
                'if changes have been made but no saves made then there are unsaved changes
                result = True
            End If

            Return result
        End Get
    End Property

#End Region

#Region "Render"

    Dim _renderEngine As RenderEngine

    Private Sub RenderCurrentRoom()
        'renders the current room
        If Not IsNothing(_renderEngine) Then
            _renderEngine.DoGameRenderNoSort(SelectedRoom.Actors, _createdLevel.Scroll)
        End If
    End Sub

    Private Sub FrmLevelEditor_SizeChanged(sender As FrmLevelEditor, e As EventArgs) Handles MyBase.SizeChanged
        RenderCurrentRoom()
    End Sub

#End Region

#Region "Actors"

    ReadOnly _unselectedActor As New Actor With {.Name = "UnselectedActor"}

    Private Property Actors As Actor()
        Get
            Return SelectedRoom.Actors
        End Get
        Set
            SelectedRoom.Actors = Value

            RefreshActorsList()
            RefreshTagsList()
            RenderCurrentRoom()
        End Set
    End Property

    Private Property SelectedActor As Actor
        Get
            If LstActors.SelectedIndex > -1 Then
                Return SelectedRoom.Actors(LstActors.SelectedIndex)
            Else
                Return _unselectedActor
            End If
        End Get
        Set
            If LstActors.SelectedIndex > -1 Then
                SelectedRoom.Actors(LstActors.SelectedIndex) = Value
            Else
                DisplayError("Tried to modify an actor but none were selected")
            End If
        End Set
    End Property

    Private Sub BtnCreateActor_Click(sender As Button, e As EventArgs) Handles BtnCreateActor.Click
        'opens Actor Maker for user and adds created actor to room

        Using actorMaker As New FrmActorMaker(Nothing)
            actorMaker.ShowDialog()

            If actorMaker.Finished Then
                AddActor(actorMaker.CreatedActor)
            End If
        End Using
    End Sub

    Private Sub ItmActorDelete_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmActorDelete.Click
        Actors = RemoveItem(Actors, LstActors.SelectedIndex)
        RefreshActorsList()
    End Sub

    Private Sub ItmActorEdit_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmActorEdit.Click
        Using actorMaker As New FrmActorMaker(SelectedActor)
            actorMaker.ShowDialog()

            If actorMaker.Finished Then
                SelectedActor = actorMaker.CreatedActor
            End If
        End Using
    End Sub

    Private Sub ItmActorDuplicate_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmActorDuplicate.Click
        AddActor(_createdLevel.Rooms(LstRooms.SelectedIndex).Actors(LstActors.SelectedIndex))
    End Sub

    Private Sub AddActor(template As Actor)
        'creates a new instance from the given actor

        Dim newActor As Actor = template.Clone()

        If Not IsNothing(Actors) Then
            newActor.Name = MakeActorNameUnique(newActor.Name)
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

    ReadOnly _unselectedTag As New Tag("UnselectedTag", Nothing)
    Dim _disableTagChangedEvent As Boolean = False  'used so that an infinite loop of KeyTagChanged is prevented

    Private Property Tags As Tag()
        Get
            Return SelectedActor.Tags
        End Get
        Set
            SelectedActor.Tags = Value
            RefreshTagsList()
        End Set
    End Property

    Private Property SelectedTag As Tag
        Get
            If LstActorTags.SelectedIndex > -1 And LstActorTags.SelectedIndex <= UBound(SelectedActor.Tags) Then
                Return SelectedActor.Tags(LstActorTags.SelectedIndex)
            Else
                Return _unselectedTag
            End If
        End Get
        Set
            If LstActorTags.SelectedIndex > -1 Then
                Tags(LstActorTags.SelectedIndex) = Value
            End If
        End Set
    End Property

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

    Private Sub ToggleTagControls(enableControls As Boolean)
        'enables or disables all controls for tags, depending on whether provided True or False

        Dim tagControls() As Control = {TxtActorName, NumActorLocX, NumActorLocY,
            NumActorLayer, NumActorScale, LstActorTags, BtnAddActorTag}

        For Each ctrl As Control In tagControls
            ctrl.Enabled = enableControls
        Next
    End Sub

    Private Sub ShowActorTags(ByVal displayActor As Actor)
        'changes the values displayed in the controls for tags to show values of the current actor

        _disableTagChangedEvent = True

        If IsNothing(displayActor) Then      'if no actor provided then uses an empty actor
            displayActor = New Actor       'this doesn't work as Actors have some default properties
            'ToggleTagControls(False)
        End If

        TxtActorName.Text = RemoveQuotes(displayActor.Name)
        NumActorLocX.Value = displayActor.Location.X
        NumActorLocY.Value = displayActor.Location.Y
        NumActorLayer.Value = displayActor.Layer
        NumActorScale.Value = displayActor.Scale

        _disableTagChangedEvent = False
    End Sub

    Private Sub KeyTagChanged(sender As Control, e As EventArgs) Handles NumActorLocX.ValueChanged, NumActorLocY.ValueChanged,
        NumActorLayer.ValueChanged, NumActorScale.ValueChanged, TxtActorName.TextChanged
        'updates the key Tags (location, layer, scale) of selected actor using the key Tags controls' values

        If Not _disableTagChangedEvent Then
            _disableTagChangedEvent = True

            SelectedActor.Name = MakeActorNameUnique(TxtActorName.Text)
            SelectedActor.Location = New PointF(NumActorLocX.Value, NumActorLocY.Value)
            SelectedActor.Layer = NumActorLayer.Value
            SelectedActor.Scale = NumActorScale.Value

            _disableTagChangedEvent = False
        End If
        RefreshTagsList()
        RenderCurrentRoom()
    End Sub

    Private Sub BtnAddActorTag_Click(sender As Button, e As EventArgs) Handles BtnAddActorTag.Click
        Using tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()
            If tagMaker.UserFinished Then
                Tags = InsertItem(Tags, tagMaker.CreatedTag)
                LstActorTags.SelectedIndex = UBound(Tags)
            End If
        End Using
    End Sub

    Private Sub ItmTagsDelete_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmTagsDelete.Click
        If MsgBox("Are you sure you wish to delete tag " & SelectedTag.Name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Tags = RemoveItem(Tags, LstActorTags.SelectedIndex)
        End If
    End Sub

    Private Sub ItmTagsEdit_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmTagsEdit.Click
        Using tagMaker As New FrmTagMaker(SelectedTag)
            tagMaker.ShowDialog()
            If tagMaker.UserFinished Then
                SelectedTag = tagMaker.CreatedTag
                RefreshTagsList()
            End If
        End Using
    End Sub

    Private Sub ItmTagsDuplicate_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmTagsDuplicate.Click
        SelectedActor.AddTag(SelectedTag)
        RefreshTagsList()
        LstActorTags.SelectedIndex = UBound(Tags)
    End Sub

    Private Function MakeActorNameUnique(actorName As String) As String
        If Not IsNothing(Actors) Then
            Dim otherNames(UBound(Actors)) As String

            For index As Integer = 0 To UBound(otherNames)
                otherNames(index) = Actors(index).Name
            Next

            Return MakeNameUnique(actorName, otherNames, True)
        Else
            Return actorName
        End If
    End Function

#Region "Mouse Location Control"

    Dim _heldInstanceIndex As Integer = -1           'index of the actor being held by the user
    Dim _relativeHoldLocation As PointF = Nothing    'used so that the mouse holds a specific location on the instance

    Private Sub PnlRenderMouseDown(sender As Panel, e As MouseEventArgs) Handles PnlRender.MouseDown
        'mouse starts holding the instance underneath it

        'gets the relative mouse location in the game render
        Dim mouseLocationInRender As New PointF(e.X / _renderEngine.RenderScale.Width, e.Y / _renderEngine.RenderScale.Height)


        If Not IsNothing(Actors) Then
            'finds which instances the mouse is over
            Dim possibleInstanceIndices() As Integer = {}
            For index As Integer = 0 To UBound(Actors)
                Dim instanceArea As RectangleF = Actors(index).Hitbox()

                If mouseLocationInRender.X <= instanceArea.Right And mouseLocationInRender.X >= instanceArea.Left _
                    And mouseLocationInRender.Y >= instanceArea.Top And mouseLocationInRender.Y <= instanceArea.Bottom Then
                    possibleInstanceIndices = InsertItem(possibleInstanceIndices, index)
                End If
            Next

            'finds which of the instances the mouse is over is on the highest layer
            If UBound(possibleInstanceIndices) >= 0 Then
                'finds which instance has the highest index
                Dim topMostInstanceIndex As Integer = possibleInstanceIndices(0)
                For index As Integer = 0 To UBound(possibleInstanceIndices)
                    If Actors(possibleInstanceIndices(index)).Layer > Actors(topMostInstanceIndex).Layer Then
                        topMostInstanceIndex = possibleInstanceIndices(index)
                    End If
                Next

                'starts holding the top most actor
                _heldInstanceIndex = topMostInstanceIndex
                _relativeHoldLocation = New PointF(Actors(_heldInstanceIndex).Hitbox.Left - mouseLocationInRender.X,
                                                  Actors(_heldInstanceIndex).Hitbox.Top - mouseLocationInRender.Y)

                'show the user which actor is selected by changing the selected actor in the actor list
                LstActors.SelectedIndex = _heldInstanceIndex
            End If
        End If
    End Sub

    Private Sub PnlRenderMouseDrag(sender As Panel, e As MouseEventArgs) Handles PnlRender.MouseMove
        'mouse moves the held instance

        If _heldInstanceIndex >= 0 Then
            'calculates where the mouse has moved the held actor to
            Actors(_heldInstanceIndex).Location = New PointF(e.X / _renderEngine.RenderScale.Width + _relativeHoldLocation.X,
                                                            e.Y / _renderEngine.RenderScale.Height + _relativeHoldLocation.Y)
            RenderCurrentRoom()
            ShowActorTags(Actors(_heldInstanceIndex))
        End If
    End Sub

    Private Sub PnlRenderMouseUp(sender As Panel, e As MouseEventArgs) Handles PnlRender.MouseUp
        'mouse lets go of the held instance

        _heldInstanceIndex = -1
    End Sub

#End Region

#End Region

#Region "Parameters"

    Private Property Parameters As Tag()
        Get
            Return _createdLevel.Tags
        End Get
        Set
            _createdLevel.Tags = Value

            RefreshEverything()
        End Set
    End Property

    Private Property SelectedParameter As Tag
        Get
            If LstLevelParams.SelectedIndex > -1 Then
                Return _createdLevel.Tags(LstLevelParams.SelectedIndex)
            Else
                Return _unselectedTag
            End If
        End Get
        Set
            If LstLevelParams.SelectedIndex > -1 And LstLevelParams.SelectedIndex <= UBound(Parameters) Then
                _createdLevel.Tags(LstLevelParams.SelectedIndex) = Value

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

    Private Sub LstLevelParams_SelectedIndexChanged(sender As ListBox, e As EventArgs) Handles LstLevelParams.SelectedIndexChanged
        'a different parameter is selected for editing/removing

        If LstLevelParams.SelectedIndex > -1 Then

        End If
    End Sub

    Private Sub BtnAddLevelParam_Click(sender As Button, e As EventArgs) Handles BtnAddLevelParam.Click
        'user creates a new parameter using FrmTagMaker

        'gets the user to create a parameter (same as a tag)
        Using tagMaker As New FrmTagMaker
            tagMaker.ShowDialog()
            If tagMaker.UserFinished Then
                Parameters = InsertItem(Parameters, tagMaker.CreatedTag)
            End If
        End Using
    End Sub

    Private Sub ItmParameterDelete_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmParameterDelete.Click
        If MsgBox("Are you sure you wish to delete parameter " & SelectedParameter.Name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Parameters = RemoveItem(Parameters, LstLevelParams.SelectedIndex)
        End If
    End Sub

    Private Sub ItmParameterEdit_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmParameterEdit.Click
        Using tagMaker As New FrmTagMaker(SelectedParameter)
            tagMaker.ShowDialog()
            If tagMaker.UserFinished Then
                SelectedParameter = tagMaker.CreatedTag
            End If
        End Using
    End Sub

    Private Sub ItmParameterDuplicate_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmParameterDuplicate.Click
        _createdLevel.AddTag(SelectedParameter)
    End Sub

#End Region

#Region "Rooms"

    ReadOnly _unselectedRoom As New Room({})

    Private Property Rooms As Room()
        Get
            If Not IsNothing(_createdLevel) Then
                Return _createdLevel.Rooms
            Else
                Return Nothing
            End If

        End Get
        Set
            _createdLevel.Rooms = Value

            RefreshEverything()
        End Set
    End Property

    Private Property SelectedRoom As Room
        Get
            If LstRooms.SelectedIndex > -1 And Not IsNothing(Rooms) AndAlso LstRooms.SelectedIndex <= UBound(Rooms) Then
                Return Rooms(LstRooms.SelectedIndex)
            Else
                Return _unselectedRoom
            End If
        End Get
        Set
            If LstRooms.SelectedIndex > -1 And LstRooms.SelectedIndex <= UBound(Rooms) Then
                Rooms(LstRooms.SelectedIndex) = Value
                RefreshEverything()
            End If
        End Set
    End Property

    Private Sub RefreshRoomsList()
        If Not IsNothing(Rooms) Then
            Dim items(UBound(Rooms)) As String
            For index As Integer = 0 To UBound(Rooms)
                items(index) = Rooms(index).Name
            Next

            RefreshList(LstRooms, items)
        End If
    End Sub

    Private Sub LstRooms_SelectedIndexChanged(sender As ListBox, e As EventArgs) Handles LstRooms.SelectedIndexChanged
        'currently selected room is changed

        LstActors.SelectedIndex = -1
        RefreshActorsList()
        RenderCurrentRoom()
    End Sub

    Private Sub BtnRoomAdd_Click(sender As Button, e As EventArgs) Handles BtnRoomAdd.Click
        'a new room is added to the level

        Dim roomName As String = InputBox("Please enter a Name for the new room")

        If Not IsNothing(roomName) AndAlso roomName.Length > 0 Then
            Rooms = InsertItem(Rooms, New Room({}, MakeRoomNameUnique(roomName)))
            LstRooms.SelectedIndex = UBound(Rooms)      'automatically selects the new room
        End If
    End Sub

    Private Sub ItmRoomDelete_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmRoomDelete.Click
        If MsgBox("Are you sure you wish to delete room " & SelectedRoom.Name & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Rooms = RemoveItem(Rooms, LstRooms.SelectedIndex)
        End If
    End Sub

    Private Sub ItmRoomEdit_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmRoomEdit.Click
        Dim newName As String = InputBox("Please enter the new Name for " & SelectedRoom.Name)

        If Not IsNothing(newName) AndAlso newName.Length > 0 Then
            SelectedRoom.Name = MakeRoomNameUnique(newName)
            RefreshEverything()
        End If
    End Sub

    Private Sub ItmRoomDuplicate_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ItmRoomDuplicate.Click
        Rooms = InsertItem(Rooms, SelectedRoom)
        LstRooms.SelectedIndex = UBound(Rooms)  'automatically selects the new room
    End Sub

    Private Function MakeRoomNameUnique(roomName As String) As String
        If Not IsNothing(Rooms) Then
            Dim otherNames(UBound(Rooms)) As String

            For index As Integer = 0 To UBound(otherNames)
                otherNames(index) = Rooms(index).Name
            Next

            Return MakeNameUnique(roomName, otherNames, True)
        Else
            Return roomName
        End If
    End Function

#End Region

#Region "General Procedures"

    Private Sub AnySelectionChanged(sender As ListBox, e As EventArgs) _
        Handles LstActors.SelectedIndexChanged, LstLevelParams.SelectedIndexChanged, LstLevelParams.SelectedIndexChanged,
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

    Private Sub ToolBarTestStart_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarTestStart.Click
        'opens game executor with the current level

        Using executor As New FrmGameExecutor(_createdLevel.ToString)
            executor.ShowDialog()
        End Using
    End Sub

#End Region

End Class
