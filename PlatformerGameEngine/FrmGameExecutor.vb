Imports PlatformerGameEngine.My.Resources

Public Class FrmGameExecutor

#Region "Disposing"

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        _endGameLoop = True
        MyBase.OnFormClosing(e)
    End Sub

#End Region

#Region "Constructors"

    Public Sub New(levelTagString As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _renderer = New RenderEngine(pnlGame)
        CurrentLevel = New Level(levelTagString)
    End Sub

    Private Sub FormShown(sender As FrmGameExecutor, e As EventArgs) Handles MyBase.Shown
        'separate from constructor because the game loop shouldn't begin until the form is actually shown to the player

        GameLoop()
    End Sub

#End Region

#Region "Render Control"

    ReadOnly _renderer As RenderEngine
    Public ReadOnly CurrentLevel As Level

    Public ReadOnly Property CurrentRoom As Room
        Get
            If Not IsNothing(CurrentLevel) AndAlso Not IsNothing(CurrentLevel.Rooms) AndAlso CurrentLevel.RoomIndex >= 0 AndAlso CurrentLevel.RoomIndex <= UBound(CurrentLevel.Rooms) Then
                Return CurrentLevel.Rooms(CurrentLevel.RoomIndex)
            Else
                Return Nothing
            End If
        End Get
    End Property

#Region "Timer Control"

    Dim _frameStopwatch As Stopwatch
    Dim _endGameLoop As Boolean = False     'true when the user presses close

    Private Sub GameLoop()
        'adapted from https://www.dreamincode.net/forums/topic/140697-creating-games-with-vbnet/
        'continuously loops, delaying each loop for approx 16ms

        _frameStopwatch = New Stopwatch
        Const interval As Integer = 16 'ms
        Dim startTick As Integer

        _frameStopwatch.Start()
        Do While Not _endGameLoop
            startTick = _frameStopwatch.ElapsedMilliseconds

            If Not _paused Then
                GameTick()
            End If

            Application.DoEvents()

            'delay
            Do While _frameStopwatch.ElapsedMilliseconds - startTick < interval
                Threading.Thread.Sleep(0)   'reduces CPU usage
            Loop
        Loop
        _frameStopwatch.Stop()
    End Sub

    Private Sub GameTick()
        'advances the game state by 1 frame, with respect to keys held and actor Tags
        'then renders the new game state to the player

        'broadcasts an event for the game tick
        BroadcastEvent(New Tag(EventTagName, New Tag(NameTagName, AddQuotes("tick")).ToString))

        'broadcasts the key held event for each key currently held
        Dim kc As New KeysConverter     'used to convert the held key to a string
        For Each keyHeld As Keys In _keysHeld
            If keyHeld <> Keys.None Then
                BroadcastEvent(New Tag(EventTagName,
                    New Tag(NameTagName, AddQuotes("key" & kc.ConvertToString(keyHeld))).ToString))
            End If
        Next
        'processes each actor's tags
        If Not IsNothing(CurrentRoom) Then
            If Not IsNothing(CurrentRoom.Actors) Then
                For Each act As Actor In CurrentRoom.Actors
                    'processes the actor's actions for this tick
                    Dim tagIndex As Integer = 0
                    Do
                        If Not ProcessTag(act.Tags(tagIndex), act, Me) Then
                            'error occured
                            Me.Close()
                            Exit For
                        End If
                        tagIndex += 1
                    Loop Until tagIndex > UBound(act.Tags)
                Next
            End If

            _renderer.DoGameRender(CurrentRoom.Actors, CurrentLevel.Scroll)
        End If
    End Sub

#Region "Pausing"

    Dim _paused As Boolean = False

    Public Sub Pause()
        'pauses the game

        _paused = True
        LblPause.Visible = True
        ResetKeysHeld()
    End Sub

    Public Sub Unpause()
        'unpauses the game

        _paused = False
        LblPause.Visible = False
    End Sub

    Public Sub TogglePause()
        If _paused Then
            Unpause()
        Else
            Pause()
        End If
    End Sub

#End Region

#End Region

#End Region

#Region "References"

    Public Function FindReference(act As Actor, refString As String) As Object
        'finds what a reference is referring to
        'e.g. "ExampleActor.velocity[0]" may return 5

        Dim parts() As String = JsonSplit(refString, 0, ".")    'reference delimited by "."
        Dim result As Object = Nothing

        Select Case LCase(parts(0))
            Case "me"
                result = act
            Case "level"
                result = CurrentLevel
            Case Else
                For Each otherAct As Actor In CurrentRoom.Actors
                    If LCase(otherAct.Name) = LCase(parts(0)) Then
                        result = otherAct
                        Exit For
                    End If
                Next
        End Select

        If Not IsNothing(result) Then
            For index As Integer = 1 To UBound(parts)
                Dim partString As String = parts(index)

                Dim startArrayCharIndex As Integer = 0
                Dim endArrayCharIndex As Integer = 0

                'gets array index
                Dim arrayIndex As Integer = -1
                If GetArrayCharIndices(partString, startArrayCharIndex, endArrayCharIndex) Then
                    arrayIndex = ProcessCalculation(
                        Mid(parts(index), startArrayCharIndex + 2, endArrayCharIndex - startArrayCharIndex - 1), act,
                        Me)
                    partString = partString.Remove(startArrayCharIndex)
                End If

                If result.GetType() = GetType(Tag) Then
                    result = result.InterpretArgument(partString)
                    If arrayIndex > -1 Then
                        result = result(arrayIndex)
                    End If
                ElseIf result.GetType() = GetType(Level) Or result.GetType() = GetType(Actor) Then
                    result = result.FindTag(partString).InterpretArgument()
                    If arrayIndex > -1 Then
                        result = result(arrayIndex)
                    End If
                End If
            Next
        End If

        Return result
    End Function

    Private Shared Function GetArrayCharIndices(partString As String, ByRef startArrayCharIndex As Integer,
                                           ByRef endArrayCharIndex As Integer) As Boolean
        'updates the indices of the characters that denote array bounds (square brackets)

        Const startArrayChar As String = "["
        Const endArrayChar As String = "]"

        startArrayCharIndex = partString.IndexOf(startArrayChar, StringComparison.Ordinal)
        endArrayCharIndex = partString.IndexOf(endArrayChar, StringComparison.Ordinal)

        Return partString.Contains(startArrayChar) And partString.Contains(endArrayChar)
    End Function


#End Region

#Region "Events"

    Public Sub BroadcastEvent(eventTag As Tag)
        'broadcasts a single event to all Actors with a listener for the event

        Dim eventName As String = eventTag.FindSubTag(NameTagName).Argument

        If Not IsNothing(CurrentRoom) AndAlso Not IsNothing(CurrentRoom.Actors) Then
            For Each act As Actor In CurrentRoom.Actors
                Dim listeners() As Tag = act.FindTags(ListenerTagName)
                For Each listener As Tag In listeners
                    Dim listenerName As String = listener.FindSubTag(NameTagName).Argument
                    If LCase(listenerName) = LCase(eventName) Then
                        ReceiveEvent(act, listener)
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub ReceiveEvent(ByRef act As Actor, listenerTag As Tag)
        'processes a received event

        Dim behaviourArgument As Object = listenerTag.InterpretArgument(BehaviourTagName)
        If IsArray(behaviourArgument) Then
            For index As Integer = 0 To UBound(behaviourArgument)
                ProcessSubTags(behaviourArgument(index), act, Me)
            Next
        ElseIf Not IsNothing(behaviourArgument) Then
            ProcessSubTags(behaviourArgument, act, Me)
        End If
    End Sub

#End Region

#Region "Player Input"

    Dim _keysHeld() As Keys = {}  'a list of keys currently being held by the user

    Private Sub FrmGame_KeyDown(sender As FrmGameExecutor, e As KeyEventArgs) Handles MyBase.KeyDown
        'if the key is pressed down then it is added to the list of held keys

        If e.KeyCode = Keys.Escape Then
            'toggles pause when escape is pressed
            TogglePause()
        Else
            'checks that key isn't already in keys held list
            Dim alreadyAdded As Boolean = False
            For Each k As Keys In _keysHeld
                If k = e.KeyCode Then
                    alreadyAdded = True
                    Exit For
                End If
            Next

            If Not alreadyAdded Then
                _keysHeld = InsertItem(_keysHeld, e.KeyCode)
            End If
        End If
    End Sub

    Private Sub FrmGame_KeyUp(sender As FrmGameExecutor, e As KeyEventArgs) Handles MyBase.KeyUp
        'when the key is no longer being pressed by the user it is removed from the list of held keys

        For index As Integer = 0 To UBound(_keysHeld)
            If _keysHeld(index) = e.KeyCode Then
                _keysHeld = RemoveItem(_keysHeld, index)
                Exit For
            End If
        Next
    End Sub

    Private Sub ResetKeysHeld()
        'removes all keys from the list of keys held

        _keysHeld = {}
    End Sub

#End Region

End Class