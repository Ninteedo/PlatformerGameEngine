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
    End Sub


    Public Structure Room
        'a room is a collection of entities which are all used at once

        Dim instances() As PRE2.Entity    'the entities which are used in the game, modified copies of the defaults
        Dim name As String
		Dim fileLocation As String
    End Structure

    Public Structure Level
        'class to store entity defaults and rooms

        Dim templates() As PRE2.Entity     'the formats for loaded entities, not actually displayed, used to create instances of entities
        Dim parameters() As PRE2.Tag            'essentially global variables for the level
        Dim rooms() As Room                     'stores each room in a 2D array, indexed from the uppermost
        Dim roomCoords() As Point               'stores the coordinates of each room, parallel to rooms array
        Dim currentRoomCoords As Point          'stores the coordinates of which room is being used currently

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
    End Structure

    'save load

    Public loaderFileLocation As String
    Public levelFiles(0) As String

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

        If IsNothing(levelFiles(levelNumber)) = True Then
            PRE2.DisplayError("No known level number " & levelNumber)
        Else
            currentLevel = LoadLevelFile(renderer.levelFolderLocation & levelFiles(levelNumber - 1), renderer)
            currentRoom = currentLevel.RoomWithCoords(New Point(0, 0))      'sets the starting room to the one with coords 0,0
            Const frameRate As Single = 60

            frameTimer.Interval = 1000 / frameRate
            frameTimer.Start()
        End If
    End Sub

    Public Shared Function LoadLevelFile(fileLocation As String, renderEngine As PRE2) As Level
        If IO.File.Exists(fileLocation) = True Then
            Dim levelString As String = PRE2.ReadFile(fileLocation)

            Dim thisLevel As Level = New Level

            Dim lines() As String = levelString.Split(Environment.NewLine)

            For lineIndex As Integer = 0 To UBound(lines)
                ParseLevelLine(lines(lineIndex), thisLevel, renderEngine)
            Next lineIndex

            Return thisLevel
        Else
            PRE2.DisplayError("Could not find level file at " & fileLocation)

            Return Nothing
        End If
    End Function



    Public Shared Sub ParseLevelLine(line As String, ByRef thisLevel As Level, renderEngine As PRE2)
        'reads a line and loads/adds to/changes the level accordingly

        Try     'maybe clean this up
            Dim lineTypeTest As String = line.Split(":")(0)
            Dim attributesTest() As String = line.Split(":")(1).Split("/")
        Catch ex As IndexOutOfRangeException        'failsafe for bad lines
            PRE2.DisplayError("Could not parse line: " & line)
            Exit Sub
        End Try

        Dim lineType As String = line.Split(":")(0)          'line type decides what the current line does, eg loadEnt
        Dim attributes() As String = line.Split(":")(1).Split("/")     'the specifics of what the line does, the order matters, delimited by /

        Select Case lineType
            Case "roomFolder"   'sets the folder for this levels rooms
                renderEngine.roomFolderLocation = renderEngine.levelFolderLocation & attributes(0)
            Case "loadEnt"      'loads an entity from a file (file location, name)
                Dim newEntity As PRE2.Entity

                newEntity = LoadEntity(attributes(0), renderEngine)
                newEntity.AddTag(New PRE2.Tag("name", {attributes(1)}))

                If IsNothing(thisLevel.templates) = True Then
                    ReDim thisLevel.templates(0)
                Else
                    ReDim Preserve thisLevel.templates(UBound(thisLevel.templates) + 1)
                End If
                thisLevel.templates(UBound(thisLevel.templates)) = newEntity
            Case "loadRoom"     'loads a room from a file (file location, room coords (x,y))
                Dim newRoom As Room = LoadRoomFile(renderEngine.roomFolderLocation & attributes(0), thisLevel)
                Dim coords As New Point(Int(attributes(1).Split(",")(0)), Int(attributes(1).Split(",")(1)))

                If IsNothing(thisLevel.rooms) = True Then
                    ReDim thisLevel.rooms(0)
                    ReDim thisLevel.roomCoords(0)
                Else
                    ReDim Preserve thisLevel.rooms(UBound(thisLevel.rooms) + 1)
                    ReDim Preserve thisLevel.roomCoords(UBound(thisLevel.rooms) + 1)
                End If

                thisLevel.rooms(UBound(thisLevel.rooms)) = newRoom
                thisLevel.roomCoords(UBound(thisLevel.rooms)) = coords
            Case "addParam"     'adds a parameter but wont delete any duplicates (tag)
                Dim newParam As PRE2.Tag = New PRE2.Tag(attributes(0))
                ReDim Preserve thisLevel.parameters(UBound(thisLevel.parameters) + 1)
                thisLevel.parameters(UBound(thisLevel.parameters)) = newParam
            Case "editParam"    'changes a parameter (tag)
                Dim newParam As PRE2.Tag = New PRE2.Tag(attributes(0))

                'removes any other parameters with the same name
                For paramIndex As Integer = 0 To UBound(thisLevel.parameters)
                    If thisLevel.parameters(paramIndex).name = newParam.name Then
                        For index As Integer = paramIndex To UBound(thisLevel.parameters) - 1
                            thisLevel.parameters(index) = thisLevel.parameters(index + 1)
                        Next index

                        ReDim Preserve thisLevel.parameters(UBound(thisLevel.parameters) - 1)
                    End If
                Next paramIndex

                ReDim Preserve thisLevel.parameters(UBound(thisLevel.parameters) + 1)
                thisLevel.parameters(UBound(thisLevel.parameters)) = newParam

            Case Else           'unknown line type
                PRE2.DisplayError("Unknown line type: " & lineType)
        End Select
    End Sub

    Public Shared Function LoadRoomFile(fileLocation As String, ByRef thisLevel As Level) As Room
        If IO.File.Exists(fileLocation) = True Then
            Dim roomString As String = PRE2.ReadFile(fileLocation)

            Dim thisRoom As New Room With {.fileLocation = fileLocation}
            Dim lines() As String = roomString.Split(Environment.NewLine)

            For lineIndex As Integer = 0 To UBound(lines)
                ParseRoomLine(lines(lineIndex), thisRoom, thisLevel)
            Next lineIndex

            Return thisRoom
        Else
            PRE2.DisplayError("Couldn't find room file at " & fileLocation)

            Return Nothing
        End If
    End Function

    Public Shared Sub ParseRoomLine(line As String, ByRef thisRoom As Room, ByRef thisLevel As Level)

        Dim lineType As String = line.Split(":")(0)          'line type decides what the current line does, eg loadEnt
        Dim attributes() As String = line.Split(":")(1).Split("/")     'the specifics of what the line does, the order matters, delimited by /

        Select Case lineType
            Case "addEnt"       'creates a new instance of a loaded entity (template name, instance name, tags...)
                Dim entityTemplate As New PRE2.Entity

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
                    newEnt.AddTag(New PRE2.Tag("name", {attributes(1)}))

                    'adds the rest of the tags to the entity
                    For index As Integer = 2 To UBound(attributes)
                        Dim currentTag As New PRE2.Tag(attributes(index))

                        'if the entity already has the tag then removes the previous copy of it
                        If newEnt.HasTag(currentTag.name) Then
                            newEnt.RemoveTag(currentTag.name)
                        End If

                        newEnt.AddTag(currentTag)
                    Next index

                    If IsNothing(thisRoom.instances) = True Then
                        ReDim thisRoom.instances(0)
                    Else
                        ReDim Preserve thisRoom.instances(UBound(thisRoom.instances) + 1)
                    End If
                    thisRoom.instances(UBound(thisRoom.instances)) = newEnt
                End If

            Case "editEnt"      'modifies an instance of an entity (instance name, tags)
                Dim entityInstance As PRE2.Entity

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
        End Select

    End Sub

    'Private Sub LoadLevel(fileLocation As String)       'loads the level from the given file location

    'End Sub

    Public Shared Function LoadEntity(fileLocation As String, renderEngine As PRE2) As PRE2.Entity
        If IO.File.Exists(fileLocation) = True Then
            Dim fileText As String = PRE2.ReadFile(fileLocation)

            Dim result As PRE2.Entity = EntityStringHandler.ReadEntityString(fileText, renderEngine)

            Return result
        Else
            PRE2.DisplayError("Couldn't find entity file " & fileLocation)
        End If

        Return Nothing
    End Function

    Private Sub LoadSprite(fileLocation As String)
        If IO.File.Exists(fileLocation) = True Then
            Dim fileText As String = PRE2.ReadFile(fileLocation)

            'this is not finished, might be replaced by PRE2
        End If
    End Sub

    Dim renderer As PRE2            'panel render engine 2
    Public currentLevel As Level
    Public currentRoom As Room
    Public playerEntity As PRE2.Entity
    Dim frameTimer As New Timer

    Private Sub GameTick()
        Dim entitiesToRender() As PRE2.Entity
        entitiesToRender = currentRoom.instances
        ReDim Preserve entitiesToRender(UBound(entitiesToRender) + 1)
        entitiesToRender(UBound(entitiesToRender)) = playerEntity

        renderer.DoGameRender(entitiesToRender)
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

    Public Function EntityWithName(entityName As String) As PRE2.Entity
        Select Case entityName
            Case "other"

            Case Else
                For index As Integer = 0 To UBound(renderer.entities)
                    If renderer.entities(index).name = entityName Then
                        Return renderer.entities(index)
                    End If
                Next index
        End Select
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