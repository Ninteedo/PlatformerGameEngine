'Richard Holmes
'23/03/2019
'Game loader and executor, uses PanelRenderEngine2

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmGame

#Region "Initialisation"

    Dim delayTimer As New Timer With {.Interval = 1, .Enabled = False}

    Private Sub FrmGame_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'used because issues can arise if using the form load event for lots of code

        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
        AddHandler frameTimer.Tick, AddressOf GameTick
    End Sub

    Private Sub Initialisation()
        'executed once the form loads

        delayTimer.Stop()

        'renderer = New PRE2 With {.panelCanvasGameArea = New PaintEventArgs(pnlGame.CreateGraphics, New Rectangle(New Point(0, 0), pnlGame.Size))}
        renderer = New PRE2 With {.renderPanel = pnlGame}
        LoadGame()

        AddHandler frameTimer.Tick, AddressOf GameTick
    End Sub

#End Region

#Region "Disposing"

    Protected Overrides Sub OnFormClosed(ByVal e As FormClosedEventArgs)
        frameTimer.Stop()
        frameTimer.Dispose()
        renderer = Nothing
        MyBase.OnFormClosed(e)
    End Sub

#End Region

#Region "Loading"

    Public loaderFileLocation As String
    Public levelFiles(0) As String

    Private Sub LoadGame()      'this loads the game
        'TODO: redo this entirely
        If IO.File.Exists(loaderFileLocation) = True Then
            Dim loaderFileText As String = PRE2.ReadFile(loaderFileLocation)

            'loads locations of each folder
            Dim topLevelFolder As String = loaderFileLocation.Remove(loaderFileLocation.LastIndexOf("\") + 1)
            renderer.levelFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "levelFolder")
            renderer.actorFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "actorFolder")
            renderer.spriteFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "spriteFolder")

            'loads the file names of each level, keeps going until a level isn't provided
            Dim index As Integer = 0
            Dim finished As Boolean = False
            Do
                Dim currentFile As String = renderer.FindProperty(loaderFileText, "level" & Trim(Str(index + 1)))
                If IsNothing(currentFile) = False Then
                    ReDim Preserve levelFiles(index)
                    levelFiles(index) = currentFile

                    index += 1
                Else
                    finished = True
                End If
            Loop Until finished = True

            'loads level 1
            PlayLevel(1)
        Else
            PRE2.DisplayError("Could not find file: " & loaderFileLocation)
        End If
    End Sub

    Public Sub PlayLevel(levelNumber As UInteger)
        'loads the level into memory and starts playing it

        If IsNothing(levelFiles(levelNumber - 1)) = True Then
            PRE2.DisplayError("No known level number " & levelNumber)
        Else
            'currentLevel = LoadLevelFile(renderer.levelFolderLocation & levelFiles(levelNumber - 1), renderer, levelDelimiters, roomDelimiters)
            currentLevel = LoadLevelFile(renderer.levelFolderLocation & levelFiles(levelNumber - 1), renderer)
            currentRoom = currentLevel.rooms(0) 'currentLevel.RoomWithCoords(New Point(0, 0))      'sets the starting room to the one with coords 0,0
            Const frameRate As Single = 60

            frameTimer.Interval = 1000 / frameRate
            frameTimer.Start()
        End If
    End Sub

    Public Shared Function LoadLevelFile(fileLocation As String, renderEngine As PRE2) As Level
        'loads a level from a given file location
        If IO.File.Exists(fileLocation) Then
            Dim levelString As String = PRE2.ReadFile(fileLocation)
            If Not IsNothing(levelString) Then
                Return New Level(levelString, renderEngine)
            End If
        Else
            PRE2.DisplayError("Could not find level file at " & fileLocation)
        End If

        Return Nothing
    End Function

#End Region

#Region "Render Control"

    Dim renderer As PRE2            'panel render engine 2

    Public currentLevel As Level
    Public currentRoom As Room      'should probably make this a property
    'Public playerActor As Actor
    Dim frameTimer As New Timer

    Private Sub GameTick()
        'broadcasts the key held event for each key currently held
        BroadcastEvent(New Tag("event", New Tag("name", AddQuotes("tick")).ToString), currentRoom, renderer)
        For keyIndex As Integer = 0 To UBound(keysHeld)
            If keysHeld(keyIndex) <> Keys.None Then
                BroadcastEvent(New Tag("event", New Tag("name", AddQuotes("key" & ChrW(keysHeld(keyIndex)))).ToString), currentRoom, renderer)
            End If
        Next keyIndex

        For actorIndex As Integer = 0 To UBound(currentRoom.actors)
            ActorTick(currentRoom.actors(actorIndex))
        Next

        renderer.DoGameRender(currentRoom.actors)
    End Sub

    Private Sub ActorTick(ByRef ent As Actor)
        'processes the actor's actions for this tick
        Dim tagIndex As Integer = 0
        Do
            TagBehaviours.ProcessTag(ent.tags(tagIndex), ent, currentRoom, renderer)

            tagIndex += 1
        Loop Until tagIndex > UBound(ent.tags)
    End Sub

#End Region

#Region "Higher Level Actor Control"

    Public Function GetArgument(tag As Tag, Optional ent As Actor = Nothing,
                                      Optional room As Room = Nothing, Optional defaultResult As Object = Nothing) As Object
        'returns a processed actor argument
        'TODO: this doesnt work with arrays

        '{"velocity":[velocity(0)+1,velocity(1)]}

        'Dim result As Object = defaultResult

        'If Not IsNothing(tag.argument) Then ' AndAlso argIndex <= UBound(tag.args) Then
        '    Dim rawArg As Object = InterpretValue(tag.argument, fullInterpret:=True, ent:=ent, room:=room).ToString
        '    'Dim argCalculated As String = TagBehaviours.ProcessCalculation(rawArg, ent, room)

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

        Return InterpretValue(tag.argument, fullInterpret:=True, ent:=ent, room:=room).ToString
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

    Public Shared Function FindReference(ent As Actor, refString As String, currentRoom As Room)
        'finds what a reference is referring to
        'ExampleActor.velocity[0]
        'TODO: clean this up

        Dim parts() As String = JSONSplit(refString, 0, ".")
        Dim result As Object = Nothing
        Dim arrayBoundsCharacters() As String = {"[", "]"}

        'find object (actor or room) which the reference is coming from
        Select Case LCase(parts(0))
            Case "me"
                result = ent

                If UBound(parts) >= 1 Then
                    If parts(1).Contains(arrayBoundsCharacters(0)) Then
                        Dim temp As String = parts(1).Remove(parts(1).IndexOf(arrayBoundsCharacters(0)))
                        result = ent.FindTag(temp)
                    Else
                        result = ent.FindTag(parts(1))
                    End If
                End If
            Case "room"
                'result = currentRoom.FindTag(parts(1))
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
                        Dim arrayIndex As Integer = ProcessCalculation(Mid(parts(index), parts(index).IndexOf(arrayBoundsCharacters(0)) + 1, parts(index).IndexOf(arrayBoundsCharacters(1)) - parts(index).IndexOf(arrayBoundsCharacters(0))), ent, currentRoom)
                        result = result.InterpretArgument(parts(index).Remove(parts(index).IndexOf(arrayBoundsCharacters(0))))(arrayIndex)
                    Else
                        result = result.InterpretArgument(parts(index))
                    End If
                Next
            ElseIf UBound(parts) = 1 Then

                If parts(1).Contains(arrayBoundsCharacters(0)) Then
                    Dim start As Integer = parts(1).IndexOf(arrayBoundsCharacters(0)) + 2
                    Dim length As Integer = parts(1).IndexOf(arrayBoundsCharacters(1)) - parts(1).IndexOf(arrayBoundsCharacters(0)) - 1
                    Dim arrayIndex As Integer = Int(ProcessCalculation(Mid(parts(1), start, length), ent, currentRoom))
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

    Dim keysHeld(0) As Keys

    Private Sub FrmGame_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        'if the key is pressed down then it is added to the list of held keys

        If Not keysHeld.Contains(e.KeyCode) Then
            Dim addedToList As Boolean = False
            For index As Integer = 0 To UBound(keysHeld)
                If keysHeld(index) = Keys.None Then
                    keysHeld(index) = e.KeyCode
                    addedToList = True
                    Exit For
                End If
            Next index

            If Not addedToList Then
                ReDim Preserve keysHeld(UBound(keysHeld) + 1)
                keysHeld(UBound(keysHeld)) = e.KeyCode
            End If
        End If
    End Sub

    Private Sub FrmGame_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        'when the key is no longer being pressed by the user it is removed from the list of held keys

        If keysHeld.Contains(e.KeyCode) Then
            For index As Integer = 0 To UBound(keysHeld)
                If keysHeld(index) = e.KeyCode Then
                    keysHeld(index) = Keys.None
                End If
            Next index
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
