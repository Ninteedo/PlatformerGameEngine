'Richard Holmes
'23/03/2019
'Game loader and executor, uses PanelRenderEngine2

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmGame

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


    Public Structure Room
        'a room is a collection of entities which are all rendered at once

        Dim instances() As PRE2.Entity    'the entities which are used in the game, modified copies of the defaults
        'Dim name As String

        Dim parameters() As PRE2.Tag

        Public Overloads Function ToString(levelOfRoom As Level, roomDelimiters() As String) As String
            'returns a string to save the given room

            Dim roomString As String = ""

            'adds an addParam line for each instance
            If Not IsNothing(parameters) Then
                For Each param As PRE2.Tag In parameters
                    'only adds parameter if the level doesn't have an identical global parameter
                    'Dim levelHasParam As Boolean = False
                    'For Each globalParam As PRE2.Tag In levelOfRoom.globalParameters
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
                For Each instance As PRE2.Entity In instances
                    'finds the template of the instance
                    Dim templateOfInstance As PRE2.Entity = Nothing           'used so that tags can be compared and identical ones can be ignored
                    Dim templateName As String = Nothing
                    If instance.HasTag("templateName") Then
                        templateName = instance.FindTag("templateName").args(0)
                        For Each template As PRE2.Entity In levelOfRoom.templates
                            If template.name = templateName Then
                                templateOfInstance = template
                                Exit For
                            End If
                        Next template
                    End If

                    If IsNothing(templateOfInstance) Then
                        PRE2.DisplayError("Could not find a template called " & templateName & " for instance " & instance.name)
                    Else
                        'Dim line As String = "addEnt" & roomDelimiters(0) & templateName & roomDelimiters(1) & instance.name        'this looks wrong
                        Dim line As String = "addEnt" & roomDelimiters(0) & templateName

                        'adds each added tag to the line, which is not identical to one which the template has
                        For Each thisTag As PRE2.Tag In instance.tags
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

        Public Function FindParam(paramName As String) As PRE2.Tag
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

        Public Sub AddParam(newParam As PRE2.Tag)
            'adds the given parameter to this room's list of tags

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

        Public Property name As String
            Get
                If HasParam("name") Then
                    Return FindParam("name").args(0)
                Else
                    Return "UnnamedRoom"
                End If
            End Get
            Set(value As String)
                RemoveParam("name")
                AddParam(New PRE2.Tag("name", {value}))
            End Set
        End Property

        Public Property coords As Point     'need to implement a proper coords system
            Get
                If HasParam("coords") Then
                    Return FindParam("coords").args(0)
                Else
                    Return New Point(0, 0)
                End If
            End Get
            Set(value As Point)
                RemoveParam("coords")
                AddParam(New PRE2.Tag("coords", {value}))
            End Set
        End Property
    End Structure

    Public Structure Level
        'class to store entity defaults and rooms

        Dim templates() As PRE2.Entity     'the formats for loaded entities, not actually displayed, used to create instances of entities
        Dim globalParameters() As PRE2.Tag            'essentially global variables for the level

        Dim rooms() As Room                     'stores each room in a 2D array, indexed from the uppermost
        Dim roomCoords() As Point               'stores the coordinates of each room, parallel to rooms array
        Dim currentRoomCoords As Point          'stores the coordinates of which room is being used currently

        Public Overloads Function ToString(levelDelimiters() As String, roomDelimiters() As String) As String
            'creates a string of a level so it can be saved and loaded

            Dim levelString As String = ""

            'adds an addParam line for each parameter
            If Not IsNothing(globalParameters) Then
                For Each param As PRE2.Tag In globalParameters
                    levelString += "addParam" & levelDelimiters(0) & param.ToString & levelDelimiters(2)
                Next param
            End If

            'adds a loadEnt line for each template
            If Not IsNothing(templates) Then
                For Each template As PRE2.Entity In templates
                    If template.HasTag("fileName") = True Then
                        Dim line As String = "loadEnt" & levelDelimiters(0) & template.FindTag("fileName").args(0) & levelDelimiters(1) & template.name

                        'adds each tag
                        For Each thisTag As PRE2.Tag In template.tags
                            line += levelDelimiters(1) & thisTag.ToString
                        Next

                        levelString += line & levelDelimiters(2)
                    Else
                        PRE2.DisplayError("Template " & template.name & " is missing tag 'fileName' so couldn't be saved")
                    End If
                Next
            End If

            'adds a loadRoom line for each room
            If Not IsNothing(rooms) Then
                For Each currentRoom As FrmGame.Room In rooms
                    levelString += "loadRoom" & levelDelimiters(0) & currentRoom.ToString(Me, roomDelimiters) & levelDelimiters(2)
                Next currentRoom
            End If
            'levelString = levelString.Remove(Len(levelString) - 2, 2)
            levelString = levelString.Trim

            Return levelString
        End Function


        Public Function GetRoomParameters(roomIndex As Integer) As PRE2.Tag()
            'returns the level's global parameters combined with the room's parameters

            Dim result() As PRE2.Tag = rooms(roomIndex).parameters

            For Each parameter As PRE2.Tag In globalParameters
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

            For index As Integer = 0 To UBound(roomCoords)
                If coords = roomCoords(index) Then
                    Return rooms(index)
                End If
            Next index

            PRE2.DisplayError("Couldn't find a room with coordinates " & Str(coords.X) & "," & Str(coords.Y))
            Return Nothing
        End Function

        Public Sub AddParam(newParam As PRE2.Tag)
            'adds the given parameter to this level's list of tags

            If IsNothing(globalParameters) = True Then
                ReDim globalParameters(0)
            Else
                ReDim Preserve globalParameters(UBound(globalParameters) + 1)
            End If

            globalParameters(UBound(globalParameters)) = newParam
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
    End Structure

    'save load

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
            currentLevel = LoadLevelFile(renderer.levelFolderLocation & levelFiles(levelNumber - 1), renderer, levelDelimiters, roomDelimiters)
            currentRoom = currentLevel.rooms(0) 'currentLevel.RoomWithCoords(New Point(0, 0))      'sets the starting room to the one with coords 0,0
            Const frameRate As Single = 60

            frameTimer.Interval = 1000 / frameRate
            frameTimer.Start()
        End If
    End Sub

    Public Shared Function LoadLevelFile(fileLocation As String, renderEngine As PRE2,
                                         levelDelimiters() As String, roomDelimiters() As String) As Level
        If IO.File.Exists(fileLocation) = True Then
            Dim levelString As String = PRE2.ReadFile(fileLocation)
            Dim thisLevel As Level = New Level
            Dim lines() As String = Strings.Split(levelString.Trim, levelDelimiters(2))

            For lineIndex As Integer = 0 To UBound(lines)
                ParseLevelLine(lines(lineIndex), thisLevel, renderEngine, levelDelimiters, roomDelimiters)
            Next lineIndex

            Return thisLevel
        Else
            PRE2.DisplayError("Could not find level file at " & fileLocation)

            Return Nothing
        End If
    End Function



    Public Shared Sub ParseLevelLine(line As String, ByRef thisLevel As Level, renderEngine As PRE2,
                                     levelDelimiters() As String, roomDelimiters() As String)
        'reads a line and loads/adds to/changes the level accordingly

        Try     'maybe clean this up
            Dim lineTypeTest As String = line.Split(levelDelimiters(0))(0)
            Dim attributesTest() As String = line.Split(levelDelimiters(0))(1).Split(levelDelimiters(1))
        Catch ex As IndexOutOfRangeException        'failsafe for bad lines
            PRE2.DisplayError("Could not parse line: " & line)
            Exit Sub
        End Try

        Dim lineType As String = line.Split(levelDelimiters(0))(0)          'line type decides what the current line does, eg loadEnt
        Dim attributes() As String = Strings.Split(Strings.Split(line, levelDelimiters(0))(1), levelDelimiters(1))   'the specifics of what the line does, the order matters

        'removes any whitespace in the attributes
        If Not IsNothing(attributes) Then
            For index As Integer = 0 To UBound(attributes)
                attributes(index) = attributes(index).Trim()
            Next index
        End If

        Select Case LCase(lineType.Trim)
            Case "roomfolder"   'sets the folder for this levels rooms (folder location (excluding everything up to and including the level folder))
                renderEngine.roomFolderLocation = renderEngine.levelFolderLocation & attributes(0)

            Case "loadent"      'loads an entity from a file (file location)
                Dim newEntity As PRE2.Entity

                newEntity = LoadEntity(renderEngine.entityFolderLocation & attributes(0), renderEngine)
                'newEntity.AddTag(New PRE2.Tag("name", {attributes(1)}))

                Dim templateNames() As String
                If Not IsNothing(thisLevel.templates) Then
                    ReDim templateNames(UBound(thisLevel.templates))
                    For index As Integer = 0 To UBound(thisLevel.templates)
                        templateNames(index) = thisLevel.templates(index).name
                    Next index
                End If
                newEntity.name = MakeNameUnique(newEntity.name, templateNames, True)

                If IsNothing(thisLevel.templates) = True Then
                    ReDim thisLevel.templates(0)
                Else
                    ReDim Preserve thisLevel.templates(UBound(thisLevel.templates) + 1)
                End If
                thisLevel.templates(UBound(thisLevel.templates)) = newEntity

            Case "loadroom"     'loads a room from a file (room name, room string) or (room string)?
                Dim newRoom As New Room
                newRoom = LoadRoom(attributes(0).Trim, thisLevel, roomDelimiters) 'LoadRoomFile(renderEngine.roomFolderLocation & attributes(0), thisLevel, renderEngine.roomFolderLocation)
                'Dim coords As Point
                'If UBound(attributes) >= 1 Then
                '    coords = New Point(Int(attributes(1).Split(levelDelimiters(2))(0)), Int(attributes(1).Split(levelDelimiters(2))(1)))
                'Else        'safeguard for if room has no coords
                '    coords = New Point
                'End If

                If IsNothing(thisLevel.rooms) = True Then
                    ReDim thisLevel.rooms(0)
                    ReDim thisLevel.roomCoords(0)
                Else
                    ReDim Preserve thisLevel.rooms(UBound(thisLevel.rooms) + 1)
                    ReDim Preserve thisLevel.roomCoords(UBound(thisLevel.rooms) + 1)
                End If

                thisLevel.rooms(UBound(thisLevel.rooms)) = newRoom
                'thisLevel.roomCoords(UBound(thisLevel.rooms)) = coords

            Case "addparam"     'adds a parameter and deletes any duplicates (tag)
                Dim newTag As New PRE2.Tag(attributes(0))

                thisLevel.RemoveParam(newTag.name)
                thisLevel.AddParam(newTag)

            Case Else           'unknown line type
                PRE2.DisplayError("Unknown line type: " & lineType)
        End Select
    End Sub

    'Public Shared Function LoadRoomFile(fileLocation As String, ByRef thisLevel As Level, roomFolderLocation As String) As Room
    '    If IO.File.Exists(fileLocation) = True Then
    '        Dim roomString As String = PRE2.ReadFile(fileLocation)
    '        Dim thisRoom As New Room With {
    '            .name = fileLocation.Remove(Len(fileLocation) - 5, 5).Remove(1, roomFolderLocation),
    '            .parameters = thisLevel.globalParameters
    '        }           'initial parameters are inherited from the level
    '        Dim lines() As String = roomString.Trim.Split(Environment.NewLine)

    '        For lineIndex As Integer = 0 To UBound(lines)
    '            ParseRoomLine(lines(lineIndex), thisRoom, thisLevel)
    '        Next lineIndex

    '        Return thisRoom
    '    Else
    '        PRE2.DisplayError("Couldn't find room file at " & fileLocation)
    '        Return Nothing
    '    End If
    'End Function

    Public Shared Function LoadRoom(roomString As String, ByRef thisLevel As Level, roomDelimiters() As String) As Room
        If Not IsNothing(thisLevel) Then
            Dim newRoom As New Room 'With {.parameters = thisLevel.globalParameters}
            Dim lines() As String = roomString.Trim.Split(roomDelimiters(2))

            For Each line As String In lines
                If Len(line) > 0 Then
                    ParseRoomLine(line, newRoom, thisLevel, roomDelimiters)
                End If
            Next line

            Return newRoom
        Else
            PRE2.DisplayError("Attempted to load a room but there was no level")
            Return Nothing
        End If
    End Function

    Public Shared Sub ParseRoomLine(line As String, ByRef thisRoom As Room, ByRef thisLevel As Level, roomDelimiters() As String)

        Dim lineType As String = line.Split(roomDelimiters(0))(0)          'line type decides what the current line does, eg loadEnt
        Dim attributes() As String = line.Split(roomDelimiters(0))(1).Split(roomDelimiters(1))     'the specifics of what the line does, the order matters

        'removes any whitespace from attributes
        If Not IsNothing(attributes) Then
            For index As Integer = 0 To UBound(attributes)
                attributes(index) = attributes(index).Trim
            Next index
        End If

        Select Case LCase(lineType.Trim)
            Case "addent"       'creates a new instance of a loaded entity (template name, instance name, tags...)
                Dim entityTemplate As New PRE2.Entity

                'modifies the name of the instance if necessary
                'checks that there isn't another instance with the same name as the new instance
                Dim instanceNames() As String
                If Not IsNothing(thisRoom.instances) Then
                    ReDim instanceNames(UBound(thisRoom.instances))
                    For index As Integer = 0 To UBound(thisRoom.instances)
                        instanceNames(index) = thisRoom.instances(index).name
                    Next index
                End If
                Dim newInstanceName As String = MakeNameUnique(entityTemplate.name, instanceNames, False)

                'finds the entity template with the name
                For index As Integer = 0 To UBound(thisLevel.templates)
                    If thisLevel.templates(index).FindTag("name").args(0) = attributes(0) Then
                        entityTemplate = thisLevel.templates(index)
                    End If
                Next index

                'checks if an entity was found with the name
                If IsNothing(entityTemplate) = True Then
                    PRE2.DisplayError("Could not find loaded entity with name " & attributes(0))
                Else
                    Dim newEnt As PRE2.Entity = entityTemplate

                    newEnt.RemoveTag("name")
                    newEnt.AddTag(New PRE2.Tag("name", {newInstanceName}))

                    'adds the rest of the tags to the entity
                    For index As Integer = 2 To UBound(attributes)
                        Dim currentTag As New PRE2.Tag(attributes(index))
                        newEnt.RemoveTag(currentTag.name)
                        newEnt.AddTag(currentTag)
                    Next index

                    If IsNothing(thisRoom.instances) = True Then
                        ReDim thisRoom.instances(0)
                    Else
                        ReDim Preserve thisRoom.instances(UBound(thisRoom.instances) + 1)
                    End If
                    thisRoom.instances(UBound(thisRoom.instances)) = newEnt
                End If

            Case "editent"      'modifies an instance of an entity (instance name, tags)
                Dim entityInstance As PRE2.Entity = Nothing

                'finds the entity instance with the name
                For index As Integer = 0 To UBound(thisRoom.instances)
                    If thisRoom.instances(index).FindTag("name").args(0) = attributes(0) Then
                        entityInstance = thisRoom.instances(index)
                    End If
                Next index

                'checks if an entity was found with the name
                If IsNothing(entityInstance) = True Then
                    PRE2.DisplayError("Couldn't find any instance of an entity called " & attributes(0))
                Else
                    'adds the tags to the entity
                    For index As Integer = 1 To UBound(attributes)
                        Dim currentTag As New PRE2.Tag(attributes(index))

                        'if the entity already has the tag then removes the previous copy of it
                        If entityInstance.HasTag(currentTag.name) Then
                            entityInstance.RemoveTag(currentTag.name)
                        End If

                        entityInstance.AddTag(currentTag)
                    Next index
                End If

            Case "addparam"     'adds a parameter to this level (parameter aka tag) and removes any duplicates
                Dim newTag As New PRE2.Tag(attributes(0))

                thisRoom.RemoveParam(newTag.name)
                thisRoom.AddParam(newTag)

            Case Else
                PRE2.DisplayError("Unknown line type: " & lineType)
        End Select

    End Sub

    'Private Sub LoadLevel(fileLocation As String)       'loads the level from the given file location

    'End Sub

    Public Shared Function LoadEntity(fileLocation As String, renderEngine As PRE2) As PRE2.Entity
        If IO.File.Exists(fileLocation) = True Then
            Dim fileText As String = PRE2.ReadFile(fileLocation)
            Return EntityStringHandler.ReadEntityString(fileText, renderEngine)
        Else
            PRE2.DisplayError("Couldn't find entity file " & fileLocation)
        End If

        Return Nothing
    End Function

    Public Shared Function MakeNameUnique(name As String, otherNames() As String, removeUnnecessary As Boolean) As String
        'returns a name with a number appended to it so the name is unique

        If Not IsNothing(otherNames) Then
            Dim copyNumber As Integer = 0           'used to find which number needs to added to the end of the instance name so there aren't any duplicate names
            Dim nameUnique As Boolean = False
            Dim generatedName As String = name

            Do
                copyNumber += 1
                If Not removeUnnecessary Or copyNumber > 1 Then
                    generatedName = name & "-" & Trim(Str(copyNumber))
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


    'render control

    Dim renderer As PRE2            'panel render engine 2

    Public currentLevel As Level
    Public currentRoom As Room
    'Public playerEntity As PRE2.Entity
    Dim frameTimer As New Timer

    Private Sub GameTick()

        'broadcasts the key held event for each key currently held
        For keyIndex As Integer = 0 To UBound(keysHeld)
            If Not IsNothing(keysHeld(keyIndex)) AndAlso keysHeld(keyIndex) > 0 Then
                TagEvents.BroadcastEvent(New PRE2.Tag("event", {"key" & ChrW(keysHeld(keyIndex))}), currentRoom.instances)
            End If
        Next keyIndex

        For entityIndex As Integer = 0 To UBound(currentRoom.instances)
            EntityTick(currentRoom.instances(entityIndex))
        Next

        renderer.DoGameRender(currentRoom.instances)
    End Sub

    Private Sub EntityTick(ByRef ent As PRE2.Entity)
        'processes the entity's actions for this tick
        Dim tagIndex As Integer = 0
        Do
            TagBehaviours.ProcessTag(ent, ent.tags(tagIndex), currentRoom)

            tagIndex += 1
        Loop Until tagIndex > UBound(ent.tags)
    End Sub

    Public Sub EntityEvent(ent As PRE2.Entity, behaviour As PRE2.Tag, Optional entTarget As PRE2.Entity = Nothing)

    End Sub

    Public Sub EntityBehaviour(ent As PRE2.Entity, behaviour As PRE2.Tag, Optional ent2 As PRE2.Entity = Nothing)
        'does the behaviour for the entity, based on the tag given

        Select Case behaviour.name
            Case "xVel"         'args(0):float
                Dim start As PointF = ent.location

                'ent.location.X += behaviour.args(0)
            Case "yVel"         'args(0):float
                'ent.location.Y += behaviour.args(0)
            Case "xAcc"         'args(0):float
                ent.FindTag("xVel").args(0) += behaviour.args(0)
            Case "yAcc"         'args(0):float
                ent.FindTag("yVel").args(0) += behaviour.args(0)
            Case "layer"

            Case "setBackgroundColour"

            Case "changeLevel"

            Case "gameOver"

            Case "damage"       'args(0):int
                If ent.FindTag("health").name <> Nothing Then
                    ent.FindTag("health").args(0) -= behaviour.args(0)
                ElseIf ent.FindTag("maxHealth").name <> Nothing Then
                    ent.AddTag(New PRE2.Tag("health", {ent.FindTag("maxHealth").args(0) - behaviour.args(0)}))
                End If

                ent.RemoveTag("damage")
        End Select
    End Sub

    Public Function EntityWithName(entityName As String, roomToCheck As Room) As PRE2.Entity
        Select Case entityName
            Case "other"

            Case Else
                For index As Integer = 0 To UBound(roomToCheck.instances)
                    If roomToCheck.instances(index).name = entityName Then
                        Return roomToCheck.instances(index)
                    End If
                Next index
        End Select
    End Function

    'higher level entity control

    Public Function GetEntityArgument(tag As PRE2.Tag, argIndex As Integer, Optional ent As PRE2.Entity = Nothing,
                                      Optional room As Room = Nothing, Optional defaultResult As Object = Nothing) As Object
        'returns a processed entity argument

        Dim result As Object = defaultResult

        If Not IsNothing(tag.args) AndAlso argIndex <= UBound(tag.args) Then
            Dim rawArg As Object = tag.args(argIndex)
            Dim argCalculated As String = TagBehaviours.ProcessCalculation(rawArg, ent, room)

            If IsNothing(rawArg) Then           'is not anything
                result = Nothing
            ElseIf rawArg(0) = """" Then        'is a string
                result = Mid(rawArg, 2, Len(rawArg) - 1)
            ElseIf IsNumeric(argCalculated) Then    'is a calculation
                result = argCalculated
            End If
        End If

        Return result
    End Function

    'player control

    Dim keysHeld(0) As Keys

    Private Sub FrmGame_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        'if the key is pressed down then it is added to the list of held keys

        If keysHeld.Contains(e.KeyCode) = False Then
            Dim addedToList As Boolean = False
            For index As Integer = 0 To UBound(keysHeld)
                If IsNothing(keysHeld(index)) = True Then
                    keysHeld(index) = e.KeyCode
                    addedToList = True
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
                    keysHeld(index) = Nothing
                End If
            Next index
        End If
    End Sub
End Class