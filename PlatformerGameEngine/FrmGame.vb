'Richard Holmes
'23/03/2019
'Platformer engine, actual game executor

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmGame

    Dim delayTimer As New Timer With {.Interval = 1, .Enabled = False}

    Private Sub FrmGame_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'used because issues can arise if using the form load event for lots of code

        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        'executed once the form loads

        delayTimer.Stop()

        renderer = New PRE2 With {.panelCanvasGameArea = New PaintEventArgs(pnlGame.CreateGraphics, New Rectangle(New Point(0, 0), pnlGame.Size))}
    End Sub


    Public Class Room
        Public entityInstances() As PRE2.Entity    'the entities which are used in the game, modified copies of the defaults
    End Class

    Public Class Level
        Public entityDefaults() As PRE2.Entity     'the formats for loaded entities, not actually displayed, used to create instances of entities
        Public parameters() As PRE2.Tag            'essentially global variables for the level
        Public rooms() As Room                     'stores each room in a 2D array, indexed from the uppermost
        Public roomCoords() As Point               'stores the coordinates of each room, parallel to rooms array

    End Class

    'save load

    Dim gameFolder As String = ""
    Public loaderFileLocation As String

    Private Sub LoadGame()      'this loads the game
        If IO.File.Exists(loaderFileLocation) = True Then
            Dim reader As New IO.StreamReader(loaderFileLocation)
            Dim loaderFileText As String = reader.ReadToEnd
            reader.Close()

            'loads locations of each folder
            Dim topLevelFolder As String = loaderFileLocation.Remove(loaderFileLocation.LastIndexOf("\") + 1)
            renderer.levelFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "levelFolder")
            renderer.entityFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "entityFolder")
            renderer.spriteFolderLocation = topLevelFolder & renderer.FindProperty(loaderFileText, "spriteFolder")


        Else
            PRE2.DisplayError("Could not find file: " & loaderFileLocation)
        End If
    End Sub

    Private Function LoadLevelFile(fileLocation As String) As Level
        If IO.File.Exists(fileLocation) = True Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim levelString As String = reader.ReadToEnd
            reader.Close()

            Dim thisLevel As New Level

            Dim lines() As String = levelString.Split(Environment.NewLine)

            For lineIndex As Integer = 0 To UBound(lines)
                ParseLevelLine(lines(lineIndex), thisLevel)
            Next lineIndex

            Return thisLevel
        Else
            PRE2.DisplayError("Could not find level file at " & fileLocation)

            Return Nothing
        End If
    End Function



    Private Sub ParseLevelLine(line As String, ByRef thisLevel As Level)
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
            Case "loadEnt"      'loads an entity from a file (file location, name)
                Dim newEntity As PRE2.Entity

                newEntity = LoadEntity(attributes(0))
                newEntity.AddTag(New PRE2.Tag("name", {attributes(1)}))

                If IsNothing(thisLevel.entityDefaults) = True Then
                    ReDim thisLevel.entityDefaults(0)
                Else
                    ReDim Preserve thisLevel.entityDefaults(UBound(thisLevel.entityDefaults) + 1)
                End If
                thisLevel.entityDefaults(UBound(thisLevel.entityDefaults)) = newEntity
            Case "loadRoom"     'loads a room from a file (file location, room coords)
                Dim newRoom As Room = LoadRoomFile(attributes(0), thisLevel)
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

            Case "editParam"    'changes a parameter (tag)

            Case Else           'unknown line type
                PRE2.DisplayError("Unknown line type: " & lineType)
        End Select
    End Sub

    Private Function LoadRoomFile(fileLocation As String, ByRef thisLevel As Level) As Room
        If IO.File.Exists(fileLocation) = True Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim roomString As String = reader.ReadToEnd()
            reader.Close()

            Dim thisRoom As New Room
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

    Private Sub ParseRoomLine(line As String, ByRef thisRoom As Room, ByRef thisLevel As Level)

        Dim lineType As String = line.Split(":")(0)          'line type decides what the current line does, eg loadEnt
        Dim attributes() As String = line.Split(":")(1).Split("/")     'the specifics of what the line does, the order matters, delimited by /

        Select Case lineType
            Case "addEnt"       'creates a new instance of a loaded entity (template name, instance name, tags...)
                Dim entityTemplate As New PRE2.Entity

                'finds the entity template with the name
                For index As Integer = 0 To UBound(thisLevel.entityDefaults)
                    If thisLevel.entityDefaults(index).FindTag("name").args(0) = attributes(0) Then
                        entityTemplate = thisLevel.entityDefaults(index)
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

                    If IsNothing(thisRoom.entityInstances) = True Then
                        ReDim thisRoom.entityInstances(0)
                    Else
                        ReDim Preserve thisRoom.entityInstances(UBound(thisRoom.entityInstances) + 1)
                    End If
                    thisRoom.entityInstances(UBound(thisRoom.entityInstances)) = newEnt
                End If

            Case "editEnt"      'modifies an instance of an entity (instance name, tags)
                Dim entityInstance As PRE2.Entity

                'finds the entity instance with the name
                For index As Integer = 0 To UBound(thisRoom.entityInstances)
                    If thisRoom.entityInstances(index).FindTag("name").args(0) = attributes(0) Then
                        entityInstance = thisRoom.entityInstances(index)
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

    Private Function LoadEntity(fileLocation As String) As PRE2.Entity
        If IO.File.Exists(fileLocation) = True Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd

            reader.Close()

            Dim result As PRE2.Entity = EntityStringHandler.ReadEntityString(fileText, renderer)

            Return result
        Else
            PRE2.DisplayError("Couldn't find entity file " & fileLocation)
        End If

        Return Nothing
    End Function

    Private Sub LoadSprite(fileLocation As String)
        If IO.File.Exists(fileLocation) = True Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd

            reader.Close()
        End If
    End Sub

    Dim renderer As PRE2            'panel render engine 2





    Public Sub EntityEvent(ent As PRE2.Entity, behaviour As PRE2.Tag, Optional entTarget As PRE2.Entity = Nothing)

    End Sub

    Public Sub EntityBehaviour(ent As PRE2.Entity, behaviour As PRE2.Tag, Optional ent2 As PRE2.Entity = Nothing)
        'does the behaviour for the entity, based on the tag given

        Select Case behaviour.name
            Case "xVel"         'args(0):float
                Dim start As PointF = ent.location

                ent.location.X += behaviour.args(0)
            Case "yVel"         'args(0):float
                ent.location.Y += behaviour.args(0)
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