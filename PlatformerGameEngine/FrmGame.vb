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

    Public levelDelimiters() As String = {"||", "//", vbCrLf}
    Public roomDelimiters() As String = {"|", "/", ";"}

    Private Sub LoadGame()      'this loads the game
        If IO.File.Exists(loaderFileLocation) = True Then
            Dim loaderFileText As String = PRE2.ReadFile(loaderFileLocation)

            'loads locations of each folder
            Dim topLevelFolder As String = loaderFileLocation.Remove(loaderFileLocation.LastIndexOf("\") + 1)
            renderer.levelFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "levelFolder")
            renderer.entityFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "entityFolder")
            renderer.spriteFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "spriteFolder")
            'renderer.roomFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "roomFolder")

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

    'Public Shared Function LoadLevelFileOld(fileLocation As String, renderEngine As PRE2,
    '                                     levelDelimiters() As String, roomDelimiters() As String) As Level
    '    If IO.File.Exists(fileLocation) = True Then
    '        Dim levelString As String = PRE2.ReadFile(fileLocation)
    '        Dim thisLevel As Level = New Level
    '        Dim lines() As String = Strings.Split(levelString.Trim, levelDelimiters(2))

    '        For lineIndex As Integer = 0 To UBound(lines)
    '            ParseLevelLine(lines(lineIndex), thisLevel, renderEngine, levelDelimiters, roomDelimiters)
    '        Next lineIndex

    '        Return thisLevel
    '    Else
    '        PRE2.DisplayError("Could not find level file at " & fileLocation)

    '        Return Nothing
    '    End If
    'End Function

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

    'Public Shared Function LoadEntity(fileLocation As String, renderEngine As PRE2) As Entity
    '    If IO.File.Exists(fileLocation) = True Then
    '        Dim fileText As String = PRE2.ReadFile(fileLocation)
    '        Return EntityStringHandler.ReadEntityString(fileText, renderEngine)
    '    Else
    '        PRE2.DisplayError("Couldn't find entity file " & fileLocation)
    '    End If

    '    Return Nothing
    'End Function

#End Region

#Region "Render Control"

    Dim renderer As PRE2            'panel render engine 2

    Public currentLevel As Level
    Public currentRoom As Room		'should probably make this a property
    'Public playerEntity As Entity
    Dim frameTimer As New Timer

    Private Sub GameTick()
        'broadcasts the key held event for each key currently held
        BroadcastEvent(New Tag("event", New Tag("name", AddQuotes("tick")).ToString), currentRoom, renderer)
        For keyIndex As Integer = 0 To UBound(keysHeld)
            If keysHeld(keyIndex) <> Keys.None Then
                BroadcastEvent(New Tag("event", New Tag("name", AddQuotes("key" & ChrW(keysHeld(keyIndex)))).ToString), currentRoom, renderer)
            End If
        Next keyIndex

        For entityIndex As Integer = 0 To UBound(currentRoom.instances)
            EntityTick(currentRoom.instances(entityIndex))
        Next

        renderer.DoGameRender(currentRoom.instances)
    End Sub

    Private Sub EntityTick(ByRef ent As Entity)
        'processes the entity's actions for this tick
        Dim tagIndex As Integer = 0
        Do
            TagBehaviours.ProcessTag(ent.tags(tagIndex), ent, currentRoom, renderer)

            tagIndex += 1
        Loop Until tagIndex > UBound(ent.tags)
    End Sub

#End Region

#Region "Higher Level Entity Control"

    Public Function GetArgument(tag As Tag, Optional ent As Entity = Nothing,
                                      Optional room As Room = Nothing, Optional defaultResult As Object = Nothing) As Object
        'returns a processed entity argument
        'TODO: this doesnt work with arrays

        '{"velocity":[velocity(0)+1,velocity(1)]}

        Dim result As Object = defaultResult

        If Not IsNothing(tag.argument) Then ' AndAlso argIndex <= UBound(tag.args) Then
            Dim rawArg As Object = InterpretValue(tag.argument, fullInterpret:=True, ent:=ent, room:=room).ToString
            'Dim argCalculated As String = TagBehaviours.ProcessCalculation(rawArg, ent, room)

            If IsNothing(rawArg) Then           'is not anything
                result = Nothing
                'ElseIf HasQuotes(rawArg) Then        'is a string
                '    result = RemoveQuotes(rawArg)
                'ElseIf IsNumeric(argCalculated) Then    'is a calculation
                '    result = argCalculated
            Else
                result = rawArg
            End If
        End If

        Return result
    End Function

    'Public Shared Function FindInstanceByName(name As String, room As Room, Optional thisEntity As Entity = Nothing) As Entity
    '    'returns an entity in a room when referred to by name

    '    Select Case LCase(name)
    '        Case "me"
    '            Return thisEntity
    '        Case Else
    '            For index As Integer = 0 To UBound(room.instances)
    '                If room.instances(index).name = name Then
    '                    Return room.instances(index)
    '                End If
    '            Next
    '    End Select

    '    Return Nothing
    'End Function

    Public Shared Function FindReference(ent As Entity, refString As String, currentRoom As Room)
        'finds what a reference is referring to
        'ExampleEntity.velocity[0]
        'TODO: clean this up

        Dim parts() As String = JSONSplit(refString, 0, ".")
        Dim result As Object = Nothing
        Dim arrayBoundsCharacters() As String = {"[", "]"}

        'find object (entity or room) which the reference is coming from
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
                result = currentRoom.FindParam(parts(1))
            Case Else
                For index As Integer = 0 To UBound(currentRoom.instances)
                    If currentRoom.instances(index).Name = parts(0) Then
                        result = currentRoom.instances(index)
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

            If addedToList = False Then
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

Public Structure Room
    'a room is a collection of entities which are all rendered at once

    Dim instances() As Entity    'the entities which are used in the game, modified copies of the defaults
    Dim parameters() As Tag

    Public Sub New(roomTag As Tag, renderEngine As PRE2)
        Dim tagStrings() As Object = roomTag.InterpretArgument
        If Not IsNothing(tagStrings) Then
            Dim tags(UBound(tagStrings)) As Tag
            For tagIndex As Integer = 0 To UBound(tags)
                tags(tagIndex) = New Tag(tagStrings(tagIndex).ToString)
            Next

            For Each thisTag As Tag In tags
                If Not IsNothing(thisTag) Then
                    Select Case thisTag.name
                        Case "instances"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim instances(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    instances(index) = New Entity(temp(index).ToString, renderEngine)
                                Next
                            End If
                        Case "parameters"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim parameters(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    parameters(index) = temp(index)
                                Next
                            End If
                        Case Else
                            PRE2.DisplayError("Unknown tag in room tag: " & thisTag.name)
                    End Select
                End If
            Next
        End If
    End Sub

    Public Overloads Function ToString(levelOfRoom As Level, roomDelimiters() As String) As String
        'returns a string to save the given room

        Dim roomString As String = ""

        'adds an addParam line for each instance
        If Not IsNothing(parameters) Then
            For Each param As Tag In parameters
                'only adds parameter if the level doesn't have an identical global parameter
                'Dim levelHasParam As Boolean = False
                'For Each globalParam As Tag In levelOfRoom.globalParameters
                '    If param = globalParam Then
                '        levelHasParam = True
                '    End If
                'Next globalParam

                'If Not levelHasParam Then
                roomString += "addParam" & roomDelimiters(0) & param.ToString & roomDelimiters(2)
                'End If
            Next param
        End If

        'adds an addEnt line for each instance
        If Not IsNothing(instances) Then
            For Each instance As Entity In instances
                'finds the template of the instance
                Dim templateOfInstance As Entity = Nothing           'used so that tags can be compared and identical ones can be ignored
                Dim templateName As String = Nothing
                If instance.HasTag("templateName") Then
                    templateName = instance.FindTag("templateName").InterpretArgument()
                    For Each template As Entity In levelOfRoom.templates
                        If template.Name = templateName Then
                            templateOfInstance = template
                            Exit For
                        End If
                    Next template
                End If

                If IsNothing(templateOfInstance) Then
                    PRE2.DisplayError("Could not find a template called " & templateName & " for instance " & instance.Name)
                Else
                    'Dim line As String = "addEnt" & roomDelimiters(0) & templateName & roomDelimiters(1) & instance.name        'this looks wrong
                    Dim line As String = "addEnt" & roomDelimiters(0) & templateName

                    'adds each added tag to the line, which is not identical to one which the template has
                    For Each thisTag As Tag In instance.tags
                        If Not IsNothing(templateOfInstance.FindTag(thisTag.name)) AndAlso templateOfInstance.FindTag(thisTag.name) <> thisTag Then
                            line += roomDelimiters(1) & thisTag.ToString
                        End If
                    Next thisTag

                    roomString += line & roomDelimiters(2)
                End If
            Next instance
        End If

        roomString = roomString.TrimEnd     'removes any trailing whitespace

        Return roomString
    End Function

    Public Overrides Function ToString() As String
        'updated version of room tostring which makes better use of tags

        Dim parametersTag As New Tag("parameters", ArrayToString(parameters))
        Dim instancesTag As New Tag("instances", ArrayToString(instances))

        Return New Tag(Name, ArrayToString({parametersTag, instancesTag})).ToString
    End Function


    Public Function FindParam(paramName As String) As Tag
        'returns the first parameter this room has with the given name

        If IsNothing(parameters) = False Then
            For index As Integer = 0 To UBound(parameters)
                If LCase(parameters(index).name) = LCase(paramName) Then
                    Return parameters(index)
                End If
            Next index
        End If

        Return Nothing
    End Function

    Public Function HasParam(paramName As String) As Boolean
        'returns whether or not this room has a parameter with the given name

        If FindParam(paramName).name <> Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub AddParam(newParam As Tag, Optional removeDuplicates As Boolean = False)
        'adds the given parameter to this room's list of tags

        If removeDuplicates Then
            RemoveParam(newParam.name)
        End If

        If IsNothing(parameters) = True Then
            ReDim parameters(0)
        Else
            ReDim Preserve parameters(UBound(parameters) + 1)
        End If

        parameters(UBound(parameters)) = newParam
    End Sub

    Public Sub RemoveParam(paramName As String)
        'removes all parameters with the given name

        Dim paramIndex As Integer = 0

        If Not IsNothing(parameters) Then
            Do While paramIndex <= UBound(parameters)
                If parameters(paramIndex).name = paramName Then
                    For removeIndex As Integer = paramIndex To UBound(parameters) - 1
                        parameters(removeIndex) = parameters(removeIndex + 1)
                    Next removeIndex

                    ReDim Preserve parameters(UBound(parameters) - 1)
                Else
                    paramIndex += 1       'param index isn't incremented when a param with matching name is found so none are skipped
                End If
            Loop
        End If
    End Sub

    Public Property Name As String
        Get
            If HasParam("name") Then
                Return FindParam("name").InterpretArgument()
            Else
                Return "UnnamedRoom"
            End If
        End Get
        Set(value As String)
            AddParam(New Tag("name", AddQuotes(value)), True)
        End Set
    End Property

    Public Property Coords As Point     'need to implement a proper coords system
        Get
            If HasParam("coords") Then
                Return New Point(Val(FindParam("coords").InterpretArgument(0)), Val(FindParam("coords").InterpretArgument(1)))
            Else
                Return New Point(0, 0)
            End If
        End Get
        Set(value As Point)
            RemoveParam("coords")
            AddParam(New Tag("coords", "[" & value.X & "," & value.Y & "]"))
        End Set
    End Property
End Structure

Public Structure Level
    'class to store entity defaults and rooms

    Dim templates() As Entity     'the formats for loaded entities, not actually displayed, used to create instances of entities
    Dim globalParameters() As Tag            'essentially global variables for the level

    Dim rooms() As Room                     'stores each room in a 1D array, indexed from the uppermost
    'Dim roomCoords() As Point               'stores the coordinates of each room, parallel to rooms array
    'Dim currentRoomCoords As Point          'stores the coordinates of which room is being used currently

    Public Sub New(levelString As String, renderEngine As PRE2)
        templates = Nothing
        globalParameters = Nothing
        rooms = Nothing

        Dim tagStrings() As Object = New Tag(levelString).InterpretArgument 'JSONSplit(levelString, 0)
        If Not IsNothing(tagStrings) Then
            Dim tags(UBound(tagStrings)) As Tag

            For tagIndex As Integer = 0 To UBound(tagStrings)
                tags(tagIndex) = New Tag(tagStrings(tagIndex).ToString)
            Next

            For Each thisTag As Tag In tags
                If Not IsNothing(thisTag) Then
                    Select Case thisTag.name
                        Case "parameters"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim globalParameters(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    globalParameters(index) = New Tag(temp(index).ToString)
                                Next
                            End If
                        Case "templates"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim templates(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    templates(index) = New Entity(temp(index).ToString, renderEngine)
                                Next
                            End If
                        Case "rooms"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim rooms(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    rooms(index) = New Room(temp(index), renderEngine)
                                Next
                            End If
                        Case Else
                            PRE2.DisplayError("Unknown tag in level file: " & thisTag.name)
                    End Select
                End If
            Next
        End If
    End Sub


    Public Overloads Function ToString(levelDelimiters() As String, roomDelimiters() As String) As String
        'creates a string of a level so it can be saved and loaded

        Dim levelString As String = ""

        'adds an addParam line for each parameter
        If Not IsNothing(globalParameters) Then
            For Each param As Tag In globalParameters
                levelString += "addParam" & levelDelimiters(0) & param.ToString & levelDelimiters(2)
            Next param
        End If

        'adds a loadEnt line for each template
        If Not IsNothing(templates) Then
            For Each template As Entity In templates
                If template.HasTag("fileName") = True Then
                    Dim line As String = "loadEnt" & levelDelimiters(0) & template.FindTag("fileName").InterpretArgument() & levelDelimiters(1) & template.Name

                    'adds each tag
                    For Each thisTag As Tag In template.tags
                        line += levelDelimiters(1) & thisTag.ToString
                    Next

                    levelString += line & levelDelimiters(2)
                Else
                    PRE2.DisplayError("Template " & template.Name & " is missing tag 'fileName' so couldn't be saved")
                End If
            Next
        End If

        'adds a loadRoom line for each room
        If Not IsNothing(rooms) Then
            For Each currentRoom As Room In rooms
                levelString += "loadRoom" & levelDelimiters(0) & currentRoom.ToString(Me, roomDelimiters) & levelDelimiters(2)
            Next currentRoom
        End If
        'levelString = levelString.Remove(Len(levelString) - 2, 2)
        levelString = levelString.Trim

        Return levelString
    End Function

    Public Overrides Function ToString() As String
        'updated version of level tostring which uses tags better

        Dim parametersTag As New Tag("parameters", ArrayToString(globalParameters))
        Dim templatesTag As New Tag("templates", ArrayToString(templates))
        Dim roomsTag As New Tag("rooms", ArrayToString(rooms))

        Return New Tag(Name, ArrayToString({parametersTag, templatesTag, roomsTag})).ToString
    End Function


    Public Function GetRoomParameters(roomIndex As Integer) As Tag()
        'returns the level's global parameters combined with the room's parameters

        Dim result() As Tag = rooms(roomIndex).parameters

        For Each parameter As Tag In globalParameters
            If Not rooms(roomIndex).HasParam(parameter.name) Then
                If IsNothing(result) Then
                    ReDim result(0)
                Else
                    ReDim result(UBound(result) + 1)
                End If
                result(UBound(result)) = parameter
            End If
        Next

        Return result
    End Function

    Public Function RoomWithCoords(coords As Point) As Room
        'returns the room with the coords provided

        For index As Integer = 0 To UBound(rooms)
            If coords = rooms(index).Coords Then
                Return rooms(index)
            End If
        Next index

        PRE2.DisplayError("Couldn't find a room with coordinates " & Str(coords.X) & "," & Str(coords.Y))
        Return Nothing
    End Function


    Public Function FindParam(paramName As String) As Tag
        'returns the first parameter this room has with the given name

        If IsNothing(globalParameters) = False Then
            For index As Integer = 0 To UBound(globalParameters)
                If LCase(globalParameters(index).name) = LCase(paramName) Then
                    Return globalParameters(index)
                End If
            Next index
        End If

        Return Nothing
    End Function

    Public Function HasParam(paramName As String) As Boolean
        'returns whether or not this room has a parameter with the given name

        If FindParam(paramName).name <> Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub AddParam(newParam As Tag, Optional removeDuplicates As Boolean = False)
        'adds the given parameter to this level's list of tags

        If IsNothing(globalParameters) = True Then
            ReDim globalParameters(0)
        Else
            ReDim Preserve globalParameters(UBound(globalParameters) + 1)
        End If

        globalParameters(UBound(globalParameters)) = newParam

        If removeDuplicates Then
            RemoveParam(newParam.name)
        End If
    End Sub

    Public Sub RemoveParam(paramName As String)
        'removes all parameters with the given name

        Dim paramIndex As Integer = 0

        If Not IsNothing(globalParameters) Then
            Do While paramIndex <= UBound(globalParameters)
                If globalParameters(paramIndex).name = paramName Then
                    For removeIndex As Integer = paramIndex To UBound(globalParameters) - 1
                        globalParameters(removeIndex) = globalParameters(removeIndex + 1)
                    Next removeIndex

                    ReDim Preserve globalParameters(UBound(globalParameters) - 1)
                Else
                    paramIndex += 1       'param index isn't incremented when a param with matching name is found so none are skipped
                End If
            Loop
        End If
    End Sub


    Public Property Name As String
        Get
            If HasParam("name") Then
                Return FindParam("name").InterpretArgument()
            Else
                Return "UnnamedLevel"
            End If
        End Get
        Set(value As String)
            RemoveParam("name")
            AddParam(New Tag("name", value))
        End Set
    End Property
End Structure