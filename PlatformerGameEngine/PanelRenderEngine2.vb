'Richard Holmes
'22/03/2019
'Panel Render Engine v2

Imports SpriteHandler = PlatformerGameEngine.SpriteStringHandler

Public Class PanelRenderEngine2

    'Public panelCanvasGameArea As PaintEventArgs
    Public renderPanel As Panel
    Public entities() As Entity
    Public loadedSprites() As Sprite
    Public renderScale As Single = 20
    'Dim gameResolution As Size = panelCanvasGameArea.ClipRectangle.Size

    Public spriteFolderLocation As String
    Public entityFolderLocation As String
    Public levelFolderLocation As String
    Public roomFolderLocation As String

    Public Structure Tag
        Dim name As String
        Dim args() As Object        'does this need to be object?

        Public Sub New(tagName As String, Optional arguments() As Object = Nothing)
            name = tagName
            args = arguments
        End Sub

        Public Sub New(ByVal tagString As String)
            'creates a tag from a string

            'checks that string is actually a tag
            If Len(tagString) > 4 Then
                If Mid(tagString, 1, 4) = "tag(" And Mid(tagString, Len(tagString), 1) = ")" Then

                    tagString = tagString.Remove(0, 4)      'removes "tag("
                    tagString = tagString.Remove(Len(tagString) - 1, 1)   'removes ")"

                    Dim values() As String = tagString.Split("\")

                    name = values(0)
                    ReDim args(UBound(values) - 1)
                    For argIndex As Integer = 1 To UBound(values)
                        If IsNumeric(values(argIndex)) = True Then      'number
                            args(argIndex - 1) = Val(values(argIndex))
                        ElseIf New Tag(values(argIndex)).name <> Nothing Then       'another tag
                            args(argIndex - 1) = New Tag(values(argIndex))
                        Else                'plain string
                            args(argIndex - 1) = values(argIndex)
                        End If
                    Next argIndex
                End If
            End If
        End Sub

        Public Overrides Function ToString() As String
            'turns this tag into a string

            Dim result As String = "tag(" & name

            If IsNothing(args) = False Then
                For Each argument As Object In args
                    result += "\" & argument.ToString
                Next argument
            End If

            result += ")"

            Return result
        End Function

        Public Shared Function AreTagsIdentical(tag1 As Tag, tag2 As Tag) As Boolean
            'used for = and <> operators

            If LCase(tag1.name) = LCase(tag2.name) AndAlso tag1.args Is tag2.args Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Operator =(tag1 As Tag, tag2 As Tag) As Boolean
            Return AreTagsIdentical(tag1, tag2)
        End Operator

        Public Shared Operator <>(tag1 As Tag, tag2 As Tag) As Boolean
            Return Not AreTagsIdentical(tag1, tag2)
        End Operator
    End Structure

    Public Structure Entity
        'most things are entities, even if they don't move
        'each entity has at least 1 frame and can have lots of tags

        Dim frames() As Frame
        Dim tags() As Tag
        'Dim name As String

        Dim currentFrame As UInteger
        'Dim location As PointF          'location of the entity (either top left or center, not sure yet)
        'Dim rotation As Single          'rotation clockwise in degrees
        'Dim scale As Single         'how much each pixel is scaled up by
        'Dim layer As Integer        'z index of entity, higher is further forward


        Public Sub New(startFrames() As Frame, startTags() As Tag, startLocation As PointF, Optional startRotation As Single = 0.0, Optional startScale As Single = 1.0)
            frames = startFrames
            tags = startTags

            currentFrame = 0
            location = startLocation
            rotation = startRotation
            scale = startScale
        End Sub



        Public Function FindTag(tagName As String) As Tag
            'returns the first tag this entity has with the given name

            If IsNothing(tags) = False Then
                For index As Integer = 0 To UBound(tags)
                    If LCase(tags(index).name) = LCase(tagName) Then
                        Return tags(index)
                    End If
                Next index
            End If

            Return Nothing
        End Function

        Public Function HasTag(tagName As String) As Boolean
            'returns whether or not this entity has a tag with the given name

            If FindTag(tagName).name <> Nothing Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub AddTag(newTag As Tag)
            'adds the given tag to this entities list of tags

            If IsNothing(tags) = True Then
                ReDim tags(0)
            Else
                ReDim Preserve tags(UBound(tags) + 1)
            End If

            tags(UBound(tags)) = newTag
        End Sub

        Public Sub RemoveTag(tagName As String)
            'removes a tag with given name, removes all tags with the name

            Dim tagIndex As Integer = 0

            Do While tagIndex <= UBound(tags)
                If tags(tagIndex).name = tagName Then
                    For removeIndex As Integer = tagIndex To UBound(tags) - 1
                        tags(removeIndex) = tags(removeIndex + 1)
                    Next removeIndex

                    ReDim Preserve tags(UBound(tags) - 1)
                Else
                    tagIndex += 1       'tag index isn't incremented when a tag with matching name is found so none are skipped
                End If
            Loop
        End Sub



        Property name As String
            Get
                If HasTag("name") = True Then
                    Return FindTag("name").args(0)
                Else
                    Return "unnamed"
                End If
            End Get
            Set(value As String)
                If HasTag("name") = True Then
                    FindTag("name").args(0) = value
                Else
                    AddTag(New Tag("name", {value}))
                End If
            End Set
        End Property

        Property location As PointF
            Get
                If HasTag("location") = True Then
                    Return FindTag("location").args(0)
                Else
                    Return New PointF(0, 0)
                End If
            End Get
            Set(value As PointF)
                If HasTag("location") = True Then
                    FindTag("location").args(0) = value
                Else
                    AddTag(New Tag("location", {value}))
                End If
            End Set
        End Property

        Property layer As Integer
            Get
                If HasTag("layer") = True Then
                    Return FindTag("layer").args(0)
                Else
                    Return 0
                End If
            End Get
            Set(value As Integer)
                If HasTag("layer") = True Then
                    FindTag("layer").args(0) = value
                Else
                    AddTag(New Tag("layer", {value}))
                End If
            End Set
        End Property

        Property scale As Single
            Get
                If HasTag("scale") = True Then
                    Return FindTag("scale").args(0)
                Else
                    Return 1
                End If
            End Get
            Set(value As Single)
                If HasTag("scale") = True Then
                    FindTag("scale").args(0) = value
                Else
                    AddTag(New Tag("scale", {value}))
                End If
            End Set
        End Property

        Property rotation As Single
            Get
                If HasTag("rotation") = True Then
                    Return FindTag("rotation").args(0)
                Else
                    Return 0
                End If
            End Get
            Set(value As Single)
                If HasTag("rotation") = True Then
                    FindTag("rotation").args(0) = value
                Else
                    AddTag(New Tag("rotation", {value}))
                End If
            End Set
        End Property

        Property rotationAnchor As PointF
            Get
                If HasTag("rotationAnchor") = True Then
                    Return New PointF(FindTag("rotationAnchor").args(0), FindTag("rotationAnchor").args(1))
                Else
                    Return New PointF(frames(currentFrame).Dimensions.Width / 2, frames(currentFrame).Dimensions.Height / 2)
                End If
            End Get
            Set(value As PointF)
                If HasTag("rotationAnchor") = True Then
                    RemoveTag("rotationAnchor")
                End If
                AddTag(New Tag("rotationAnchor", {value.X, value.Y}))
            End Set
        End Property
    End Structure

    Public Structure Frame
        'a frame is what is actually rendered, can be made up of multiple sprites

        'Dim pixels(,) As Color
        'Dim dimensions As Size
        Dim sprites() As Sprite
        Dim offsets() As Point
        'Dim fileLocation As String


        Public Sub New(compositeSprites() As Sprite, offsets() As Point)
            'creates a new frame by combining sprites

            If compositeSprites.Length <> offsets.Length Then
                DisplayError(compositeSprites.Length & " sprites provided for new frame, but " & offsets.Length & " offsets provided for them")
            End If


        End Sub

        Public Function ToColourArray() As Color(,)
            Dim pixels(Dimensions.Width - 1, Dimensions.Height - 1) As Color

            For x As Integer = 0 To pixels.GetUpperBound(0)
                For y As Integer = 0 To pixels.GetUpperBound(1)
                    pixels(x, y) = Color.Transparent            'sets all the default colours to transparent
                Next y
            Next x

            'this is the part which actually sets the frame's pixels
            For index As Integer = 0 To UBound(sprites)
                For x As Integer = offsets(index).X To offsets(index).X + sprites(index).pixels.GetUpperBound(0)
                    For y As Integer = offsets(index).Y To offsets(index).Y + sprites(index).pixels.GetUpperBound(1)
                        If x >= 0 And y >= 0 Then       'checks that x and y are both positive
                            Dim pixelColour As Color = sprites(index).pixels(x - offsets(index).X, y - offsets(index).Y)

                            If pixelColour.A <> 0 Then
                                pixels(x, y) = pixelColour
                            End If
                        End If
                    Next y
                Next x
            Next index

            Return pixels
        End Function

        Public Function Dimensions() As Size
            'returns the max X and Y of the frame

            Dim result As New Size(0, 0)
            For index As Integer = 0 To UBound(sprites)
                Dim maxSize As New Size(sprites(index).pixels.GetLength(0) + offsets(index).X, sprites(index).pixels.GetLength(1) + offsets(index).Y)

                If maxSize.Width > result.Width Then
                    result.Width = maxSize.Width
                End If
                If maxSize.Height > result.Height Then
                    result.Height = maxSize.Height
                End If
            Next index

            Return result
        End Function

        Public Sub Trim()
            'removes any outermost rows or columns which only have transparent pixels in them
            'unfinished

            MsgBox("Trimming for frames isn't ready yet")
            Dim trimmedSides(3) As Boolean

            For side As Integer = 1 To trimmedSides.Length
                Do Until trimmedSides(side) = True
                    Dim x As Integer = 0
                    Dim y As Integer = 0

                    If side = 1 Or side = 2 Then      'for 1 and 2: top and bottom
                        Dim rowEmpty As Boolean = True
                    ElseIf side = 3 Or side = 4 Then  'for 3 and 4: left and right
                        Dim colEmpty As Boolean = True
                    End If
                Loop
            Next
        End Sub

        Public Shared Operator =(frame1 As Frame, frame2 As Frame)
            Return AreFramesEqual(frame1, frame2)
        End Operator

        Public Shared Operator <>(frame1 As Frame, frame2 As Frame)
            Return Not AreFramesEqual(frame1, frame2)
        End Operator

        Public Shared Function AreFramesEqual(frame1 As Frame, frame2 As Frame) As Boolean
            'returns whether 2 provided frames are identical

            Dim result As Boolean = True

            If frame1.Dimensions <> frame2.Dimensions Then      'checks that they are the same size
                result = False
            Else
                'checks that none of the pixels are different
                Dim pixels1(,) As Color = frame1.ToColourArray
                Dim pixels2(,) As Color = frame2.ToColourArray

                For x As Integer = 0 To frame1.Dimensions.Width - 1
                    For y As Integer = 0 To frame1.Dimensions.Width - 1
                        If pixels1(x, y) <> pixels2(x, y) Then
                            result = False
                        End If
                    Next y
                Next x
            End If

            Return result
        End Function

        Public Sub AddSprite(newSprite As Sprite, offset As Point)
            'adds a sprite to the frame

            If IsNothing(sprites) = True Then
                ReDim sprites(0)
            Else
                ReDim Preserve sprites(UBound(sprites) + 1)
            End If
            sprites(UBound(sprites)) = newSprite

            If IsNothing(offsets) = True Then
                ReDim offsets(0)
            Else
                ReDim Preserve offsets(UBound(offsets) + 1)
            End If
            offsets(UBound(offsets)) = offset
        End Sub
    End Structure

    Public Structure Sprite
        'sprites loaded from .sprt files and used to create frames

        Dim pixels(,) As Color
        Dim fileName As String

        Public Sub New(fileLocation As String)
            'creates a new sprite by reading it from a file

            If IO.File.Exists(fileLocation) = True Then
                Dim reader As New IO.StreamReader(fileLocation)
                Dim fileText As String = reader.ReadToEnd

                reader.Close()

                If SpriteHandler.ValidSpriteText(fileText, fileLocation) Then
                    pixels = SpriteHandler.ReadPixelColours(fileText)
                End If

                'FindSpriteFileName(fileLocation)
                fileName = fileLocation
            Else
                DisplayError("Couldn't find file " & fileLocation)
            End If
        End Sub

        Public Sub New(colourIndices(,) As UInteger, colours() As Color)
            'uses colours and colour indices to make a sprite

            If colours(0) <> Color.Transparent Then
                DisplayError("Colour index 0 for new sprite is not transparent")
            End If

            For x As Integer = 0 To colourIndices.GetUpperBound(0)
                For y As Integer = 0 To colourIndices.GetUpperBound(1)
                    pixels(x, y) = colours(colourIndices(x, y))
                Next y
            Next x
        End Sub

        Public Sub FindSpriteFileName(fileLocation As String)
            'finds the extra part of the file location, relative to the sprites folder
            'eg D:\Users\Example\sprites\mario\MarioIdle.sprt -> \mario\marioIdle.sprt

            Dim spriteFolderName As String = "sprites"
            Dim folders() As String = fileLocation.Split("\")
            fileName = ""

            If folders.Contains(spriteFolderName) = True Then
                Dim foundFolder As Boolean = False

                For index As Integer = 0 To UBound(folders)
                    If foundFolder = True Then
                        fileName += "\" & folders(index)
                    End If

                    If folders(index) = spriteFolderName Then
                        foundFolder = True
                    End If
                Next index
            Else
                DisplayError("Sprite at " & fileLocation & " is not in folder " & spriteFolderName)
            End If
        End Sub
    End Structure

    Public Sub LoadSprite(fileLocation As String)
        'loads a sprite from a given location

        If IO.File.Exists(fileLocation) = True Then
            Dim newSprite As New Sprite(fileLocation)
            'Dim fileName As String = newSprite.fileName

            If IsNothing(FindLoadedSprite(newSprite.fileName).fileName) = True Then      'checks that the same sprite isn't already loaded
                If IsNothing(loadedSprites) = True Then
                    ReDim loadedSprites(0)
                Else
                    ReDim Preserve loadedSprites(UBound(loadedSprites) + 1)
                End If
                loadedSprites(UBound(loadedSprites)) = newSprite
            End If
        End If
    End Sub

    Public Function FindLoadedSprite(fileLocation As String) As Sprite
        'returns a loaded sprite with the given file name if it is already loaded

        Try
            For index As Integer = 0 To UBound(loadedSprites)
                If loadedSprites(index).fileName = fileLocation Then
                    Return loadedSprites(index)
                End If
            Next index
        Catch ex As Exception
            Return Nothing
        End Try

        Return Nothing
    End Function

    Shared Sub DisplayError(message As String)
        'displays a given error message to the user

        MsgBox(message, MsgBoxStyle.Exclamation)
    End Sub

    Public Sub Log(message As String, warnLevel As Integer)
        'logs something noteworthy, eg an error or a warning or a debug
        'warn levels are 0:info, 1:warn, 2:error, 3:fatal
    End Sub

    Public Shared Function FindFolderPath(path As String, folderName As String) As String
        'returns the location of the folder with the given name in the string

        Dim folders() As String = path.Split("\")
        Dim result As String = ""

        If folders.Contains(folderName) = True Then
            Dim foundFolder As Boolean = False

            For index As Integer = UBound(folders) To 0 Step -1
                If foundFolder = True Then
                    result = result.Insert(1, folders(index) & "\")
                End If

                If folders(index) = folderName Then
                    foundFolder = True
                End If
            Next index
        End If

        Return result
    End Function

    Public Function FindProperty(fileText As String, propertyName As String) As String     'returns the property in a file with a given name, property: value
        Dim lines() As String = fileText.Split(Environment.NewLine)
        Dim result As String = ""

        For Each line As String In lines
            Dim currentProperty As String = line.Split(":")(0).Replace(vbLf, "")

            If currentProperty = propertyName Then
                Return Trim(line.Split(":")(1))         'issue: cant have colons anywhere else in the line
            End If
        Next line

        Log("Couldn't find property " & propertyName, 1)

        Return Nothing
    End Function

    Private Sub Initialisation()
        EntityInitialisation()
    End Sub

    Private Sub EntityInitialisation()
        ReDim entities(0)
        entities(0) = New Entity
        entities(0).AddTag(New Tag("unusuable"))

    End Sub

    'actual rendering part of the program

    Public Sub DoGameRender(entityList() As Entity)
        'renders everything

        Dim canvas As New PaintEventArgs(renderPanel.CreateGraphics, New Rectangle(New Point(0, 0), renderPanel.Size))

        canvas.Graphics.Clear(Color.White)

        'find used layer numbers
        'Dim layers(0) As Integer
        'For Each entity In entityList
        '    Dim layer As Integer = If(IsNumeric(entity.FindTag("layer")), Int(entity.FindTag("layer")), 0)      'default is 0

        '    If layers.Contains(layer) = False Then
        '        ReDim Preserve layers(UBound(layers) + 1)
        '        layers(UBound(layers)) = layer
        '    End If
        'Next

        'render layers
        Dim context As BufferedGraphicsContext = BufferedGraphicsManager.Current
        context.MaximumBuffer = renderPanel.Size

        Dim renderLayers() As BufferedGraphics      'each render layer
        Dim renderLayerNumbers() As Integer         'the z coordinate of each render layer, parallel to above array

        Dim overallRender As BufferedGraphics = context.Allocate(canvas.Graphics, canvas.ClipRectangle)

        'entity rendering
        If IsNothing(entityList) = False Then
            For entityIndex As Integer = 0 To UBound(entityList)
                Dim currentEntity As Entity = entityList(entityIndex)
                If IsNothing(currentEntity.frames) = False AndAlso currentEntity.currentFrame <= UBound(currentEntity.frames) And currentEntity.currentFrame >= 0 Then
                    Dim renderFrame As Frame = currentEntity.frames(currentEntity.currentFrame)
                    Dim renderPixels(,) As Color = renderFrame.ToColourArray

                    Dim renderLayer As BufferedGraphics

                    'selects the correct layer to draw to for this entity
                    If IsNothing(renderLayers) = False Then
                        If renderLayerNumbers.Contains(currentEntity.layer) = True Then
                            renderLayer = renderLayers(Array.IndexOf(renderLayerNumbers, currentEntity.layer))
                        Else
                            ReDim Preserve renderLayers(UBound(renderLayers) + 1)
                            ReDim Preserve renderLayerNumbers(UBound(renderLayers))

                            renderLayers(UBound(renderLayers)) = context.Allocate(canvas.Graphics, canvas.ClipRectangle)
                            renderLayerNumbers(UBound(renderLayers)) = currentEntity.layer
                            renderLayer = renderLayers(UBound(renderLayers))
                        End If
                    Else
                        ReDim renderLayerNumbers(0)
                        ReDim renderLayers(0)

                        renderLayers(UBound(renderLayers)) = context.Allocate(canvas.Graphics, canvas.ClipRectangle)
                        renderLayerNumbers(UBound(renderLayers)) = currentEntity.layer
                        renderLayer = renderLayers(UBound(renderLayers))
                        renderLayer.Graphics.Clear(Color.Transparent)
                    End If

                    Dim rotationAnchor As PointF = currentEntity.rotationAnchor       'the point where the entity is rotated from
                    Dim rotation As Single = currentEntity.rotation

                    'draws the pixels of the entity to the correct layer
                    For x As Integer = 0 To renderFrame.Dimensions.Width - 1
                        For y As Integer = 0 To renderFrame.Dimensions.Height - 1
                            Dim angle As Single = Math.Atan((y - rotationAnchor.Y) / (x - rotationAnchor.X)) + rotation * Math.PI / 180
                            Dim scale As Single = currentEntity.scale * renderScale / 2
                            Dim pixelCentre As New PointF(currentEntity.location.X + x * Math.Sin(angle) * scale, currentEntity.location.Y + y * Math.Cos(angle) * scale)
                            'Dim pixelCentre As New PointF(currentEntity.location.X + ((x - rotationAnchor.X) * Math.Sin((90 - rotation) * Math.PI / 180)), currentEntity.location.Y + ((y - rotationAnchor.Y) * Math.Cos(rotation * Math.PI / 180) * scale * 2))
                            'Dim pixelCentre As New PointF(currentEntity.location.X + x * (scale * 3 / 2), currentEntity.location.Y + y * (scale * 3 / 2))

                            DrawPixel(canvas.Graphics, pixelCentre, renderPixels(x, y), rotation, scale)
                        Next y
                    Next x
                End If
            Next entityIndex

            If True = False AndAlso IsNothing(renderLayers) = False Then
                'sorts the render layers from lowest to highest
                Dim sortedRenderLayerNumbers() As Integer
                sortedRenderLayerNumbers = renderLayerNumbers
                QuickSortRecursive(sortedRenderLayerNumbers, 0, UBound(sortedRenderLayerNumbers))

                'renders each layer
                For index As Integer = 0 To UBound(renderLayers)
                    renderLayers(Array.IndexOf(renderLayerNumbers, sortedRenderLayerNumbers(index))).Render()
                    MsgBox("Artifical delay")
                Next
            End If
        End If
    End Sub

    Private Sub DrawPixel(graphicsInstance As Graphics, center As PointF, colour As Color, Optional rotation As Single = 0.0, Optional scale As Single = 1.0)
        'draws a pixel

        Dim vertices(3) As PointF
        Dim brush As New SolidBrush(colour)

        'finds the vertices of the pixel
        For vIndex As Integer = 0 To UBound(vertices)
            vertices(vIndex) = VertexOfPolygon(center, vIndex, vertices.Length, rotation, scale)
        Next vIndex

        graphicsInstance.FillPolygon(brush, vertices)
    End Sub

    Private Function VertexOfPolygon(center As PointF, index As Integer, sides As Integer, rotation As Single, scale As Single) As PointF
        'returns the point of a vertex of a polygon with given attributes

        Dim angleDegrees As Single = (360 / sides) * (index + 0.5) + rotation
        Dim angleRads As Single = (Math.PI / 180) * angleDegrees

        Return New PointF(center.X + (scale * Math.Cos(angleRads)), center.Y + (scale * Math.Sin(angleRads)))
    End Function

    Public Shared Function ModArrayLength(arrayToMod() As Object, lengthChange As Integer) As Object()
        'changes the length of the given array by the given amount

        Dim result() As Object = arrayToMod

        If IsNothing(arrayToMod) = False Then
            If UBound(result) + lengthChange >= 0 Then
                ReDim Preserve result(UBound(result) + lengthChange)
            Else
                result = Nothing
            End If
        Else
            If lengthChange >= 0 Then
                ReDim result(lengthChange)
            End If
        End If

        Return result
    End Function

    Public Shared Function RemoveElementFromArray(oldArray(), indexToRemove) As Object
        'returns the given array but with an element removed

        If indexToRemove >= 0 And indexToRemove <= UBound(oldArray) Then
            Dim result() As Object = oldArray

            For index As Integer = indexToRemove To UBound(result) - 1
                result(index) = result(index + 1)
            Next index

            ReDim Preserve result(UBound(result) - 1)

            Return result
        Else
            DisplayError("Tried to remove an element at index " & indexToRemove & " in an array with a max index of " & UBound(oldArray))

            Return oldArray
        End If
    End Function

    Public Shared Function ReadFile(fileLocation As String) As String
        'returns the contents of a text file at a given location

        If IO.File.Exists(fileLocation) = True Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd()
            reader.Close()

            Return fileText
        Else
            DisplayError("Couldn't find file " & fileLocation)
            Return Nothing
        End If
    End Function

    Public Shared Sub WriteFile(fileLocation As String, message As String)
        Dim writer As New IO.StreamWriter(fileLocation)

        For Each c As Char In message
            writer.Write(c)
        Next c

        writer.Close()
    End Sub





    'this is not my own code, I got it from https://www.programmingalgorithms.com/algorithm/quick-sort-recursive?lang=VB.Net
    Public Shared Sub QuickSortRecursive(ByRef data As Integer(), left As Integer, right As Integer)
        If left < right Then
            Dim q As Integer = Partition(data, left, right)
            QuickSortRecursive(data, left, q - 1)
            QuickSortRecursive(data, q + 1, right)
        End If
    End Sub

    Private Shared Function Partition(ByRef data As Integer(), left As Integer, right As Integer) As Integer
        Dim pivot As Integer = data(right)
        Dim temp As Integer
        Dim i As Integer = left

        For j As Integer = left To right - 1
            If data(j) <= pivot Then
                temp = data(j)
                data(j) = data(i)
                data(i) = temp
                i += 1
            End If
        Next

        data(right) = data(i)
        data(i) = pivot

        Return i
    End Function
End Class