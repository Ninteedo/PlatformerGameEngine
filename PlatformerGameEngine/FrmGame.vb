'Richard Holmes
'23/03/2019
'Game loader and executor, uses PanelRenderEngine2

Public Class FrmGame

#Region "Disposing"

    Protected Overrides Sub OnFormClosed(ByVal e As FormClosedEventArgs)
        frameTimer.Stop()
        frameTimer.Dispose()
        renderEngine = Nothing
        MyBase.OnFormClosed(e)
    End Sub

#End Region

#Region "Constructors"

    Public Sub New(ByVal levelTagString As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        renderEngine = New PanelRenderEngine2 With {.renderPanel = pnlGame}
        currentLevel = New Level(levelTagString, renderEngine)

        AddHandler frameTimer.Tick, AddressOf GameTick
        frameTimer.Start()
    End Sub

#End Region

#Region "Render Control"

    Dim renderEngine As PanelRenderEngine2            'panel render engine 2
    Dim currentLevel As Level

    Private ReadOnly Property CurrentRoom As Room
        Get
            If Not IsNothing(currentLevel) AndAlso Not IsNothing(currentLevel.rooms) AndAlso currentLevel.RoomIndex >= 0 AndAlso currentLevel.RoomIndex <= UBound(currentLevel.rooms) Then
                Return currentLevel.rooms(currentLevel.RoomIndex)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Dim frameTimer As New Timer With {.Interval = 1000 / 60}

    Private Sub GameTick()
        'advances the game state by 1 frame, with respect to keys held and actor tags
        'then renders the new game state to the player

        'broadcasts an event for the game tick
        BroadcastEvent(New Tag("event", New Tag("name", AddQuotes("tick")).ToString), CurrentRoom, renderEngine)

        'broadcasts the key held event for each key currently held
        If Not IsNothing(keysHeld) Then
            Dim kc As New KeysConverter     'used to convert the held key to a string
            For Each keyHeld As Keys In keysHeld
                If keyHeld <> Keys.None Then
                    BroadcastEvent(New Tag("event", New Tag("name", AddQuotes("key" & kc.ConvertToString(keyHeld))).ToString), CurrentRoom, renderEngine)
                End If
            Next
        End If

        'processes each actor's tags
        If Not IsNothing(CurrentRoom.actors) Then
            For Each act As Actor In CurrentRoom.actors
                'processes the actor's actions for this tick
                Dim tagIndex As Integer = 0
                Do
                    ProcessTag(act.tags(tagIndex), act, CurrentRoom, renderEngine)
                    tagIndex += 1
                Loop Until tagIndex > UBound(act.tags)
            Next
        End If

        renderEngine.DoGameRender(CurrentRoom.actors)
    End Sub

#End Region

#Region "Higher Level Actor Control"

    Public Shared Function GetArgument(tag As Tag, Optional ent As Actor = Nothing,
                                      Optional room As Room = Nothing, Optional defaultResult As Object = Nothing) As Object
        'returns a processed actor argument
        'TODO: this doesnt work with arrays

        '{"velocity":[velocity(0)+1,velocity(1)]}

        'Dim result As Object = defaultResult

        'If Not IsNothing(tag.argument) Then ' AndAlso argIndex <= UBound(tag.args) Then
        '    Dim rawArg As Object = InterpretValue(tag.argument, fullInterpret:=True, act:=act, room:=room).ToString
        '    'Dim argCalculated As String = TagBehaviours.ProcessCalculation(rawArg, act, room)

        '    If IsNothing(rawArg) Then           'is not anything
        '        result = Nothing
        '        'ElseIf HasQuotes(rawArg) Then        'is a string
        '        '    result = RemoveQuotes(rawArg)
        '        'ElseIf IsNumeric(argCalculated) Then    'is a calculation
        '        '    result = argCalculated
        '    Else
        '        result = rawArg
        '    End If
        'End If

        Return InterpretValue(tag.argument, fullInterpret:=True, act:=ent, room:=room)
    End Function

    'Public Shared Function FindInstanceByName(name As String, room As Room, Optional thisActor As Actor = Nothing) As Actor
    '    'returns an actor in a room when referred to by name

    '    Select Case LCase(name)
    '        Case "me"
    '            Return thisActor
    '        Case Else
    '            For index As Integer = 0 To UBound(room.instances)
    '                If room.instances(index).name = name Then
    '                    Return room.instances(index)
    '                End If
    '            Next
    '    End Select

    '    Return Nothing
    'End Function

    Public Shared Function FindReference(act As Actor, refString As String, currentRoom As Room) As Object
        'finds what a reference is referring to
        'ExampleActor.velocity[0]
        'TODO: clean this up

        Dim parts() As String = JSONSplit(refString, 0, ".")
        Dim result As Object = Nothing
        Dim arrayBoundsCharacters() As String = {"[", "]"}

        'find object (actor or room) which the reference is coming from
        Select Case LCase(parts(0))
            Case "me"
                result = act

                If UBound(parts) >= 1 Then
                    If parts(1).Contains(arrayBoundsCharacters(0)) Then
                        Dim temp As String = parts(1).Remove(parts(1).IndexOf(arrayBoundsCharacters(0)))
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
                    End If
                Next
        End Select

        If Not IsNothing(result) Then
            If UBound(parts) > 1 Then

                For index As Integer = 1 To UBound(parts)
                    If parts(index).Contains(arrayBoundsCharacters(0)) Then
                        Dim arrayIndex As Integer = ProcessCalculation(Mid(parts(index), parts(index).IndexOf(arrayBoundsCharacters(0)) + 1, parts(index).IndexOf(arrayBoundsCharacters(1)) - parts(index).IndexOf(arrayBoundsCharacters(0))), act, currentRoom)
                        result = result.InterpretArgument(parts(index).Remove(parts(index).IndexOf(arrayBoundsCharacters(0))))(arrayIndex)
                    Else
                        result = result.InterpretArgument(parts(index))
                    End If
                Next
            ElseIf UBound(parts) = 1 Then

                If parts(1).Contains(arrayBoundsCharacters(0)) Then
                    Dim start As Integer = parts(1).IndexOf(arrayBoundsCharacters(0)) + 2
                    Dim length As Integer = parts(1).IndexOf(arrayBoundsCharacters(1)) - parts(1).IndexOf(arrayBoundsCharacters(0)) - 1
                    Dim arrayIndex As Integer = Int(ProcessCalculation(Mid(parts(1), start, length), act, currentRoom))
                    'result = result.InterpretArgument(parts(1).Remove(parts(1).IndexOf(arrayBoundsCharacters(0))))(arrayIndex)
                    result = result.InterpretArgument()(arrayIndex)
                Else
                    result = result.InterpretArgument()
                End If
            End If
        End If

        Return result
    End Function

#End Region

#Region "Player Input"

    Dim keysHeld() As Keys

    Private Sub FrmGame_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        'if the key is pressed down then it is added to the list of held keys

        'checks that key isn't already in keys held list
        Dim addedToList As Boolean = False
        If Not IsNothing(keysHeld) Then
            For index As Integer = 0 To UBound(keysHeld)
                If keysHeld(index) = e.KeyCode Then
                    keysHeld(index) = e.KeyCode
                    addedToList = True
                    Exit For
                End If
            Next
        End If

        If Not addedToList Then
            keysHeld = InsertItem(keysHeld, e.KeyCode)
        End If
    End Sub

    Private Sub FrmGame_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        'when the key is no longer being pressed by the user it is removed from the list of held keys

        If Not IsNothing(keysHeld) Then
            For index As Integer = 0 To UBound(keysHeld)
                If keysHeld(index) = e.KeyCode Then
                    keysHeld = RemoveItem(keysHeld, index)
                    Exit For
                End If
            Next
        End If
    End Sub

#End Region

#Region "General Procedures"

    Public Shared Function MakeNameUnique(ByVal name As String, otherNames() As String, removeUnnecessary As Boolean) As String
        'returns a name with a number appended to it so the name is unique

        name = RemoveQuotes(name)

        If Not IsNothing(otherNames) Then
            Dim copyNumber As Integer = 0           'used to find which number needs to added to the end of the instance name so there aren't any duplicate names
            Dim generatedName As String = name
            Dim nameUnique As Boolean

            Do
                copyNumber += 1
                If Not removeUnnecessary Or copyNumber > 1 Then
                    generatedName = AddQuotes(name & "-" & Trim(Str(copyNumber)))
                End If
                nameUnique = True

                'checks if name is unique
                For Each otherName As String In otherNames
                    If otherName = generatedName Then
                        nameUnique = False
                        Exit For
                    End If
                Next
            Loop Until nameUnique = True

            Return generatedName
        Else
            Return name & If(Not removeUnnecessary, "-1", "")
        End If
    End Function

#End Region

End Class
