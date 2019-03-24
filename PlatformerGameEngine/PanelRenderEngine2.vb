'Richard Holmes
'22/03/2019
'Panel Render Engine v2

Imports SpriteHandler = PlatformerGameEngine.SpriteStringHandler

Public Class PanelRenderEngine2

    Public panelCanvasGameArea As PaintEventArgs
    Public entities() As Entity
    Public sprites() As Sprite
    'Dim gameResolution As Size = panelCanvasGameArea.ClipRectangle.Size

    Public Structure Tag
        Dim name As String
        Dim args() As Object        'does this need to be object?

        Public Sub New(tagName As String, Optional arguments() As Object = Nothing)
            name = tagName
            args = arguments
        End Sub
    End Structure

    Public Structure Entity
        'most things are entities, even if they don't move
        'each entity has at least 1 frame and can have lots of tags

        Dim frames() As Frame
        Dim tags() As Tag
        Dim name As String

        Dim currentFrame As UInteger
        Dim location As PointF          'location of the entity (either top left or center, not sure yet)
        Dim rotation As Single          'rotation clockwise in degrees
        Dim scale As Single         'how much each pixel is scaled up by


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

            For index As Integer = 0 To UBound(tags)
                If tags(index).name = tagName Then
                    Return tags(index)
                End If
            Next index

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

            ReDim Preserve tags(UBound(tags) + 1)

            tags(UBound(tags)) = newTag
        End Sub

        Public Sub RemoveTag(tagName As String)
            'removes a tag with given name

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
    End Structure

    Public Structure Frame
        'a frame is what is actually rendered, can be made up of multiple sprites

        'Dim pixels(,) As Color
        'Dim dimensions As Size
        Dim sprites() As Sprite
        Dim offsets() As Point

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
                For x As Integer = offsets(index).X To sprites(index).pixels.GetUpperBound(0)
                    For y As Integer = offsets(index).Y To sprites(index).pixels.GetUpperBound(1)
                        If x >= 0 And y >= 0 Then       'checks that x and y are both positive
                            Dim pixelColour As Color = sprites(index).pixels(x, y)

                            If pixelColour <> Color.Transparent Then
                                pixels(x, y) = pixelColour
                            End If
                        End If
                    Next y
                Next x
            Next index
        End Function

        Public Function Dimensions() As Size
            'returns the max X and Y of the frame

            Dim result As New Size
            For index As Integer = 0 To UBound(sprites)
                Dim maxSize As New Size(sprites(index).pixels.GetUpperBound(0) + offsets(index).X, sprites(index).pixels.GetUpperBound(1) + offsets(index).Y)

                If maxSize.Width > Dimensions.Width Then
                    result.Width = maxSize.Width
                End If
                If maxSize.Height > Dimensions.Height Then
                    result.Height = maxSize.Height
                End If
            Next index
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

            If frame1.dimensions <> frame2.dimensions Then      'checks that they are the same size
                result = False
            Else
                'checks that none of the pixels are different
                Dim pixels1(,) As Color = frame1.ToColourArray
                Dim pixels2(,) As Color = frame2.ToColourArray

                For x As Integer = 0 To frame1.dimensions.Width - 1
                    For y As Integer = 0 To frame1.dimensions.Width - 1
                        If pixels1(x, y) <> pixels2(x, y) Then
                            result = False
                        End If
                    Next y
                Next x
            End If

            Return result
        End Function
    End Structure

    Public Structure Sprite
        'sprites loaded from .sprt files and used to create frames

        Dim pixels(,) As Color

        Public Sub New(ByRef fileLocation As String)
            'creates a new sprite by reading it from a file

            If IO.File.Exists(fileLocation) = True Then
                Dim reader As New IO.StreamReader(fileLocation)
                Dim fileText As String = reader.ReadToEnd

                reader.Close()

                If SpriteHandler.ValidSpriteText(fileText, fileLocation) Then
                    pixels = SpriteHandler.ReadPixelColours(fileText)
                End If
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
    End Structure

    Shared Sub DisplayError(message As String)
        'displays a given error message to the user

        MsgBox(message, MsgBoxStyle.Exclamation)
    End Sub


    Private Sub Initialisation()
        EntityInitialisation()
    End Sub

    Private Sub EntityInitialisation()
        ReDim entities(0)
        entities(0) = New Entity
        entities(0).AddTag(New Tag("unusuable"))

    End Sub

    'actual rendering part of the program

    Public Sub DoGameRender()
        'renders everything

        'entity rendering
        For entityIndex As Integer = 0 To UBound(entities)
            Dim currentEntity As Entity = entities(entityIndex)
            Dim renderFrame As Frame = currentEntity.frames(currentEntity.currentFrame)
            Dim renderPixels(,) As Color = renderFrame.ToColourArray

            For x As Integer = 0 To renderFrame.Dimensions.Width - 1
                For y As Integer = 0 To renderFrame.Dimensions.Height - 1
                    Dim rotation As Single = currentEntity.rotation
                    Dim scale As Single = currentEntity.scale
                    Dim pixelCentre As New PointF(currentEntity.location.X + x * scale, currentEntity.location.Y + y * scale)

                    DrawPixel(pixelCentre, renderPixels(x, y), rotation, scale)
                Next y
            Next x
        Next entityIndex
    End Sub

    Private Sub DrawPixel(center As PointF, colour As Color, Optional rotation As Single = 0.0, Optional scale As Single = 1.0)
        'draws a pixel

        Dim vertices(3) As PointF
        Dim brush As New SolidBrush(colour)

        'finds the vertices of the pixel
        For vIndex As Integer = 0 To UBound(vertices)
            vertices(vIndex) = VertexOfPolygon(center, vIndex, vertices.Length, rotation, scale)
        Next vIndex

        panelCanvasGameArea.Graphics.FillPolygon(brush, vertices)
    End Sub

    Private Function VertexOfPolygon(center As PointF, index As Integer, sides As Integer, rotation As Single, scale As Single) As PointF
        'returns the point of a vertex of a polygon with given attributes

        Dim angleDegrees As Single = 360 / sides + rotation
        Dim angleRads As Single = (Math.PI / 180) * angleDegrees

        Return New PointF(center.X + (scale * Math.Cos(angleRads)), center.Y + (scale * Math.Sin(angleRads)))
    End Function

End Class