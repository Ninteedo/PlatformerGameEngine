﻿'Richard Holmes
'23/03/2019
'Game loader and executor, uses RenderEngine

Public Class FrmGameExecutor

#Region "Disposing"

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        _frameTimer.Stop()
        _frameTimer.Dispose()
        '_renderer = Nothing
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

    Dim _renderer As RenderEngine
    ReadOnly _currentLevel As Level

    Private ReadOnly Property CurrentRoom As Room
        Get
            If Not IsNothing(_currentLevel) AndAlso Not IsNothing(_currentLevel.rooms) AndAlso _currentLevel.RoomIndex >= 0 AndAlso _currentLevel.RoomIndex <= UBound(_currentLevel.rooms) Then
                Return _currentLevel.rooms(_currentLevel.RoomIndex)
            Else
                Return Nothing
            End If
        End Get
    End Property

    ReadOnly _frameTimer As Timer

    Private Sub GameTick()
        'advances the game state by 1 frame, with respect to keys held and actor tags
        'then renders the new game state to the player

        'broadcasts an event for the game tick
        BroadcastEvent(New Tag(eventTagName, New Tag(identifierTagName, AddQuotes("tick")).ToString), CurrentRoom, _renderer)

        'broadcasts the key held event for each key currently held
        If Not IsNothing(_keysHeld) Then
            Dim kc As New KeysConverter     'used to convert the held key to a string
            For Each keyHeld As Keys In _keysHeld
                If keyHeld <> Keys.None Then
                    BroadcastEvent(New Tag(eventTagName, New Tag(identifierTagName, AddQuotes("key" & kc.ConvertToString(keyHeld))).ToString), CurrentRoom, _renderer)
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
                    ProcessTag(act.tags(tagIndex), act, CurrentRoom, _renderer)
                    tagIndex += 1
                Loop Until tagIndex > UBound(act.tags)
            Next
        End If

        _renderer.DoGameRender(CurrentRoom.actors)
    End Sub

#End Region

#Region "References"

    Public Shared Function FindReference(act As Actor, refString As String, currentRoom As Room) As Object
        'finds what a reference is referring to
        'e.g. "ExampleActor.velocity[0]" may return 5
        'TODO: clean this up

        Dim parts() As String = JsonSplit(refString, 0, ".")    'reference delimited by "."
        Dim result As Object = Nothing


        Dim startArrayCharIndex As Integer = 0
        Dim endArrayCharIndex As Integer = 0

        'find object (actor or room) which the reference is coming from
        Select Case LCase(parts(0))
            Case "me"
                result = act

                If UBound(parts) >= 1 Then
                    If GetArrayCharIndices(parts(1), startArrayCharIndex, endArrayCharIndex) Then
                        Dim temp As String = parts(1).Remove(startArrayCharIndex)
                        result = act.FindTag(temp)
                    Else
                        result = act.FindTag(parts(1))
                    End If
                End If
            Case "room"
                'result = currentRoom.FindTag(parts(1))
                result = currentRoom
            Case Else
                For index As Integer = 0 To UBound(currentRoom.actors)
                    If currentRoom.actors(index).Name = parts(0) Then
                        result = currentRoom.actors(index)

                        If UBound(parts) >= 1 Then
                            If GetArrayCharIndices(parts(1), startArrayCharIndex, endArrayCharIndex) Then
                                Dim temp As String = parts(1).Remove(startArrayCharIndex)
                                result = result.FindTag(temp)
                            Else
                                result = result.FindTag(parts(1))
                            End If
                        End If
                    End If
                Next
        End Select

        'finds what (in the object being referenced) is being referenced
        If Not IsNothing(result) Then
            If UBound(parts) > 1 Then
                For index As Integer = 1 To UBound(parts)
                    If GetArrayCharIndices(parts(index), startArrayCharIndex, endArrayCharIndex) Then
                        Dim arrayIndex As Integer = ProcessCalculation(
                            Mid(parts(index), startArrayCharIndex + 2, endArrayCharIndex - startArrayCharIndex - 1), act,
                            currentRoom)
                        result = result.InterpretArgument(parts(index).Remove(startArrayCharIndex))
                        result = result(arrayIndex)
                    Else
                        result = result.InterpretArgument(parts(index))
                    End If
                Next

            ElseIf UBound(parts) = 1 Then
                'TODO: is this part needed?
                If GetArrayCharIndices(parts(1), startArrayCharIndex, endArrayCharIndex) Then
                    Dim start As Integer = startArrayCharIndex + 2
                    Dim length As Integer = endArrayCharIndex - startArrayCharIndex - 1
                    Dim arrayIndex As Integer = Int(ProcessCalculation(Mid(parts(1), start, length), act, currentRoom))
                    result = result.InterpretArgument()(arrayIndex)
                Else
                    result = result.InterpretArgument()
                End If
            End If
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

#Region "Player Input"

    Dim _keysHeld() As Keys  'a list of keys currently being held by the user

    Private Sub FrmGame_KeyDown(sender As FrmGameExecutor, e As KeyEventArgs) Handles MyBase.KeyDown
        'if the key is pressed down then it is added to the list of held keys

        'checks that key isn't already in keys held list
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