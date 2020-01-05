'Richard Holmes
'23/03/2019
'Game loader and executor, uses RenderEngine

Imports PlatformerGameEngine.My.Resources

Public Class FrmGameExecutor

#Region "Disposing"

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        'used to prevent an error occuring when the user closes the form

        _frameTimer.Stop()
        MyBase.OnFormClosed(e)
    End Sub

#End Region

#Region "Constructors"

    Public Sub New(levelTagString As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _renderer = New RenderEngine With {.RenderPanel = pnlGame}
        _currentLevel = New Level(levelTagString)
        _frameTimer = New Timer() With {.Interval = 1000 / 60}   'interval is delay between ticks in ms

        AddHandler _frameTimer.Tick, AddressOf GameTick
        _frameTimer.Start()
    End Sub

#End Region

#Region "Render Control"

    ReadOnly _renderer As RenderEngine
    ReadOnly _currentLevel As Level

    ReadOnly _frameTimer As Timer

    Public ReadOnly Property CurrentRoom As Room
        Get
            If Not IsNothing(_currentLevel) AndAlso Not IsNothing(_currentLevel.rooms) AndAlso _currentLevel.RoomIndex >= 0 AndAlso _currentLevel.RoomIndex <= UBound(_currentLevel.rooms) Then
                Return _currentLevel.rooms(_currentLevel.RoomIndex)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private Sub GameTick()
        'advances the game state by 1 frame, with respect to keys held and actor tags
        'then renders the new game state to the player

        'broadcasts an event for the game tick
        BroadcastEvent(New Tag(EventTagName, New Tag(NameTagName, AddQuotes("tick")).ToString))

        'broadcasts the key held event for each key currently held
        If Not IsNothing(_keysHeld) Then
            Dim kc As New KeysConverter     'used to convert the held key to a string
            For Each keyHeld As Keys In _keysHeld
                If keyHeld <> Keys.None Then
                    BroadcastEvent(New Tag(EventTagName,
                        New Tag(NameTagName, AddQuotes("key" & kc.ConvertToString(keyHeld))).ToString))
                End If
            Next
        End If

        'processes each actor's tags
        If Not IsNothing(CurrentRoom.actors) Then
            For Each act As Actor In CurrentRoom.actors
                'processes the actor's actions for this tick
                Dim tagIndex As Integer = 0
                'TODO: how to deal with this list changing, tag IDs?
                Do
                    ProcessTag(act.tags(tagIndex), act, Me)
                    tagIndex += 1
                Loop Until tagIndex > UBound(act.tags)
            Next
        End If

        _renderer.DoGameRender(CurrentRoom.actors)
    End Sub

#End Region

#Region "References"

    'find what contains the reference (me, level, other actor)
    'for each other part
    '   

    Public Function FindReference(act As Actor, refString As String) As Object
        'finds what a reference is referring to
        'e.g. "ExampleActor.velocity[0]" may return 5
        'TODO: clean this up

        Dim parts() As String = JsonSplit(refString, 0, ".")    'reference delimited by "."
        Dim result As Object = Nothing

        Dim startArrayCharIndex As Integer = 0
        Dim endArrayCharIndex As Integer = 0

        'find object (actor or room) which the reference is coming from
        'Select Case LCase(parts(0))
        '    Case "me"
        '        result = act

        '        If UBound(parts) >= 1 Then
        '            If GetArrayCharIndices(parts(1), startArrayCharIndex, endArrayCharIndex) Then
        '                Dim temp As String = parts(1).Remove(startArrayCharIndex)
        '                result = act.FindTag(temp)
        '            Else
        '                result = act.FindTag(parts(1))
        '            End If
        '        End If
        '    Case "level"
        '        'result = currentRoom.FindTag(parts(1))
        '        result = _currentLevel
        '    Case Else
        '        For index As Integer = 0 To UBound(CurrentRoom.actors)
        '            If CurrentRoom.actors(index).Name = parts(0) Then
        '                result = CurrentRoom.actors(index)

        '                If UBound(parts) >= 1 Then
        '                    If GetArrayCharIndices(parts(1), startArrayCharIndex, endArrayCharIndex) Then
        '                        Dim temp As String = parts(1).Remove(startArrayCharIndex)
        '                        result = result.FindTag(temp)
        '                    Else
        '                        result = result.FindTag(parts(1))
        '                    End If
        '                End If
        '            End If
        '        Next
        'End Select

        ''finds what (in the object being referenced) is being referenced
        'If Not IsNothing(result) Then
        '    If UBound(parts) > 1 Then
        '        For index As Integer = 1 To UBound(parts)
        '            If GetArrayCharIndices(parts(index), startArrayCharIndex, endArrayCharIndex) Then
        '                Dim arrayIndex As Integer = ProcessCalculation(
        '                    Mid(parts(index), startArrayCharIndex + 2, endArrayCharIndex - startArrayCharIndex - 1), act,
        '                    Me)
        '                result = result.InterpretArgument(parts(index).Remove(startArrayCharIndex))
        '                result = result(arrayIndex)
        '            Else
        '                result = result.InterpretArgument(parts(index))
        '            End If
        '        Next

        '    ElseIf UBound(parts) = 1 Then
        '        'TODO: is this part needed?
        '        If GetArrayCharIndices(parts(1), startArrayCharIndex, endArrayCharIndex) Then
        '            Dim start As Integer = startArrayCharIndex + 2
        '            Dim length As Integer = endArrayCharIndex - startArrayCharIndex - 1
        '            Dim arrayIndex As Integer = Int(ProcessCalculation(Mid(parts(1), start, length), act, Me))
        '            result = result.InterpretArgument()(arrayIndex)
        '        Else
        '            result = result.InterpretArgument()
        '        End If
        '    End If
        'End If

        Select Case LCase(parts(0))
            Case "me"
                result = act
            Case "level"
                result = _currentLevel
            Case Else
                For Each otherAct As Actor In CurrentRoom.actors
                    If LCase(otherAct.Name) = LCase(parts(0)) Then
                        result = otherAct
                        Exit For
                    End If
                Next
        End Select

        If Not IsNothing(result) Then
            For index As Integer = 1 To UBound(parts)
                Dim partString = parts(index)

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
        'broadcasts a single event to all actors with a listener for the event
        'TODO: take arguments instead of eventTag?

        For index As Integer = 0 To UBound(CurrentRoom.actors)
            Dim act As Actor = CurrentRoom.actors(index)
            If Not IsNothing(act.tags) Then
                For tagIndex As Integer = 0 To UBound(act.tags)
                    If LCase(act.tags(tagIndex).name) = ListenerTagName AndAlso
                       act.tags(tagIndex).InterpretArgument(NameTagName) = eventTag.InterpretArgument(NameTagName) Then
                        ReceiveEvent(act, act.tags(tagIndex))
                    End If
                Next
            End If

            CurrentRoom.actors(index) = act
        Next
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

    Dim _keysHeld() As Keys  'a list of keys currently being held by the user

    Private Sub FrmGame_KeyDown(sender As FrmGameExecutor, e As KeyEventArgs) Handles MyBase.KeyDown
        'if the key is pressed down then it is added to the list of held keys

        'checks that key isn't already in keys held list, not sure how it could occur but I added this for safety
        Dim addedToList As Boolean = False
        If Not IsNothing(_keysHeld) Then
            For index As Integer = 0 To UBound(_keysHeld)
                If _keysHeld(index) = e.KeyCode Then
                    _keysHeld(index) = e.KeyCode
                    addedToList = True
                    Exit For
                End If
            Next
        End If

        If Not addedToList Then
            _keysHeld = InsertItem(_keysHeld, e.KeyCode)
        End If
    End Sub

    Private Sub FrmGame_KeyUp(sender As FrmGameExecutor, e As KeyEventArgs) Handles MyBase.KeyUp
        'when the key is no longer being pressed by the user it is removed from the list of held keys

        If Not IsNothing(_keysHeld) Then
            For index As Integer = 0 To UBound(_keysHeld)
                If _keysHeld(index) = e.KeyCode Then
                    _keysHeld = RemoveItem(_keysHeld, index)
                    Exit For
                End If
            Next
        End If
    End Sub

#End Region

End Class
