'Richard Holmes
'22/03/2019
'Panel Render Engine v2

Imports SpriteHandler = PlatformerGameEngine.SpriteStringHandler

Public Class PanelRenderEngine2

    Public renderPanel As Panel
    Public loadedSprites() As Sprite

    Public renderScaleFactor As Single = 10                 'the overall custom render scaling of the game (might be unnecessary)
    Public renderResolution As Size = New Size(640, 480)      'the intended size for the game
    Public renderPixelPerfect As Boolean = False              'true: render window size is set to resolution, false: game is scaled to fit the render window

    Public spriteFolderLocation As String
    Public entityFolderLocation As String
    Public levelFolderLocation As String
    'Public roomFolderLocation As String

#Region "Rendering"

    Public Sub DoGameRender(ByVal entityList() As Entity)
        'renders a list of entities

        Using canvas As New PaintEventArgs(renderPanel.CreateGraphics, New Rectangle(New Point(0, 0), renderPanel.Size))
            'canvas.Graphics.Clear(Color.White)
            canvas.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor

            Const rotationEnabled As Boolean = False        'used for when rotation isn't working properly
            Const bitmapMode As Boolean = True
            Const altLayerMode As Boolean = False

            'render layers
            Dim context As BufferedGraphicsContext = BufferedGraphicsManager.Current
            context.MaximumBuffer = renderPanel.Size

            Dim renderLayers() As BufferedGraphics = Nothing      'each render layer
            Dim renderLayerNumbers() As Integer = Nothing         'the z coordinate of each render layer, parallel to above array

            Dim overallRender As BufferedGraphics = context.Allocate(canvas.Graphics, canvas.ClipRectangle)

            'entity rendering
            If IsNothing(entityList) = False Then
                For entityIndex As Integer = 0 To UBound(entityList)
                    Dim currentEntity As Entity = entityList(entityIndex)
                    If IsNothing(currentEntity.Frames) = False AndAlso currentEntity.CurrentFrame <= UBound(currentEntity.Frames) And currentEntity.CurrentFrame >= 0 Then
                        Dim renderFrame As Frame = currentEntity.Frames(currentEntity.CurrentFrame)
                        Dim renderPixels(,) As Color = renderFrame.ToColourArray

                        Dim renderLayer As BufferedGraphics

                        If altLayerMode Then
                            'selects the correct layer to draw to for this entity
                            If IsNothing(renderLayers) = False Then
                                If renderLayerNumbers.Contains(currentEntity.Layer) = True Then
                                    renderLayer = renderLayers(Array.IndexOf(renderLayerNumbers, currentEntity.Layer))
                                Else
                                    ReDim Preserve renderLayers(UBound(renderLayers) + 1)
                                    ReDim Preserve renderLayerNumbers(UBound(renderLayers))

                                    renderLayers(UBound(renderLayers)) = context.Allocate(canvas.Graphics, canvas.ClipRectangle)
                                    renderLayerNumbers(UBound(renderLayers)) = currentEntity.Layer
                                    renderLayer = renderLayers(UBound(renderLayers))
                                    'renderLayer.Graphics.Clear(Color.Transparent)
                                    renderLayer.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                                End If
                            Else
                                ReDim renderLayers(0)
                                ReDim renderLayerNumbers(0)

                                renderLayerNumbers(UBound(renderLayers)) = currentEntity.Layer
                                renderLayers(UBound(renderLayers)) = context.Allocate(canvas.Graphics, canvas.ClipRectangle)
                                renderLayer = renderLayers(UBound(renderLayers))
                                renderLayer.Graphics.Clear(Color.White)
                                renderLayer.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                            End If
                        Else
                            If IsNothing(renderLayers) Then
                                ReDim renderLayers(0)
                                renderLayers(0) = context.Allocate(canvas.Graphics, canvas.ClipRectangle)
                                renderLayer = renderLayers(0)
                                renderLayer.Graphics.Clear(Color.White)
                                renderLayer.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
                            Else
                                renderLayer = renderLayers(0)
                            End If
                        End If

                        Dim rotationAnchor As PointF = currentEntity.RotationAnchor       'the point where the entity is rotated from
                        Dim rotation As Single = If(rotationEnabled, currentEntity.Rotation, 0)

                        'draws the pixels of the entity to the correct layer
                        If bitmapMode Then
                            If IsNothing(renderFrame.bitmapVersion) Then
                                renderFrame.bitmapVersion = renderFrame.ToBitmap(currentEntity.Opacity)
                                entityList(entityIndex).Frames(currentEntity.CurrentFrame).bitmapVersion = renderFrame.bitmapVersion
                            End If

                            Dim renderSize As SizeF = New SizeF(
                                        currentEntity.Scale * RenderScale.Width * (renderFrame.Dimensions.Width + 0.5),
                                        currentEntity.Scale * RenderScale.Height * (renderFrame.Dimensions.Height + 0.5))
                            Dim renderArea As New RectangleF(New PointF((currentEntity.Location.X - currentEntity.RotationAnchor.X - 0.5) * RenderScale.Width,
                                                                        (currentEntity.Location.Y - currentEntity.RotationAnchor.Y - 0.5) * RenderScale.Height), renderSize)
                            Dim imageToRender As Image = renderFrame.bitmapVersion

                            renderLayer.Graphics.DrawImage(imageToRender, renderArea)
                        Else
                            For pixelY As Integer = 0 To renderFrame.Dimensions.Height - 1
                                For pixelX As Integer = 0 To renderFrame.Dimensions.Width - 1
                                    Dim angle As Single = Math.Atan((pixelY - rotationAnchor.Y) / (pixelX - rotationAnchor.X)) + rotation * Math.PI / 180
                                    Dim scale As SizeF = New SizeF(
                                currentEntity.Scale * RenderScale.Width,
                                currentEntity.Scale * RenderScale.Height)

                                    Dim pixelCentre As New PointF(
                                    (currentEntity.Location.X * RenderScale.Width) + ((pixelX - currentEntity.RotationAnchor.X) * scale.Width * 2 / 3),
                                    (currentEntity.Location.Y * RenderScale.Height) + ((pixelY - currentEntity.RotationAnchor.Y) * scale.Height * 2 / 3))

                                    DrawPixel(canvas.Graphics, pixelCentre, renderPixels(pixelX, pixelY), rotation, scale)
                                Next pixelX
                            Next pixelY
                        End If
                    End If
                Next entityIndex

                If altLayerMode AndAlso Not IsNothing(renderLayers) Then
                    'sorts the render layers from lowest to highest
                    Dim sortedRenderLayerNumbers() As Integer
                    sortedRenderLayerNumbers = renderLayerNumbers
                    'QuickSortRecursive(sortedRenderLayerNumbers, 0, UBound(sortedRenderLayerNumbers))

                    'renders each layer
                    For index As Integer = 0 To UBound(renderLayers)
                        renderLayers(Array.IndexOf(renderLayerNumbers, sortedRenderLayerNumbers(index))).Render()
                        'MsgBox("Artifical delay")
                    Next
                ElseIf Not IsNothing(renderLayers) AndAlso UBound(renderLayers) >= 0 Then
                    renderLayers(0).Render()
                End If
            End If
        End Using
    End Sub

    Private Sub DrawPixel(graphicsInstance As Graphics, center As PointF, colour As Color, rotation As Single, scale As SizeF)
        'draws a pixel

        Dim vertices(3) As PointF
        Dim brush As New SolidBrush(colour)

        'finds the vertices of the pixel
        For vIndex As Integer = 0 To UBound(vertices)
            vertices(vIndex) = VertexOfPolygon(center, vIndex, vertices.Length, rotation, scale)
        Next vIndex

        graphicsInstance.FillPolygon(brush, vertices)
    End Sub

    Private Function VertexOfPolygon(center As PointF, index As Integer, sides As Integer, rotation As Single, scale As SizeF) As PointF
        'returns the point of a vertex of a polygon with given attributes

        Dim angleDegrees As Single = (360 / sides) * (index + 0.5) + rotation
        Dim angleRads As Single = (Math.PI / 180) * angleDegrees

        Return New PointF(
            center.X + (scale.Width / 2 * Math.Cos(angleRads)),
            center.Y + (scale.Height / 2 * Math.Sin(angleRads)))
    End Function

    Public Sub ResizeRenderWindow()
        'resizes the render window in accordance with the parameters provided
        'TODO: this is probably unnecessary

        If renderPixelPerfect Then    'sets panel size to game resolution
            renderPanel.Size = renderResolution
        Else        'fills the window
            renderPanel.Dock = DockStyle.Fill
        End If
    End Sub

    Public ReadOnly Property RenderScale As SizeF   'the render scaling used by the renderer
        Get
            Const useScaleFactor As Boolean = False
            Return New SizeF((renderPanel.Size.Width / renderResolution.Width) * renderScaleFactor ^ If(useScaleFactor, 1, 0),
                             (renderPanel.Size.Height / renderResolution.Height) * renderScaleFactor ^ If(useScaleFactor, 1, 0))
        End Get
    End Property

#End Region

#Region "Sprite Loading"

    Public Sub LoadSprite(fileLocation As String)
        'loads a sprite from a given location

        If IO.File.Exists(fileLocation) Then
            Dim newSprite As New Sprite(fileLocation)
            'Dim fileName As String = newSprite.fileName

            If IsNothing(FindLoadedSprite(newSprite.fileName).fileName) Then      'checks that the same sprite isn't already loaded
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

        If IsNothing(loadedSprites) = False Then
            For index As Integer = 0 To UBound(loadedSprites)
                If loadedSprites(index).fileName = fileLocation Then
                    Return loadedSprites(index)
                End If
            Next index
        End If

        Return Nothing
    End Function

#End Region

#Region "Error Handling"

    Shared Sub DisplayError(message As String)
        'displays a given error message to the user

        MsgBox(message, MsgBoxStyle.Exclamation)
    End Sub

    Public Sub Log(message As String, warnLevel As Integer)
        'logs something noteworthy, eg an error or a warning or a debug
        'warn levels are 0:info, 1:warn, 2:error, 3:fatal
    End Sub

#End Region

#Region "Array Modding"

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

    Public Shared Function RemoveElementFromArray(oldArray() As Object, indexToRemove As Integer) As Object
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

#End Region

#Region "File Handling"

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

    'Public Shared Function FindFolderPath(path As String, folderName As String) As String
    '    'returns the location of the folder with the given name in the string

    '    Dim folders() As String = path.Split("\")
    '    Dim result As String = ""

    '    If folders.Contains(folderName) = True Then
    '        Dim foundFolder As Boolean = False

    '        For index As Integer = UBound(folders) To 0 Step -1
    '            If foundFolder = True Then
    '                result = result.Insert(1, folders(index) & "\")
    '            End If

    '            If folders(index) = folderName Then
    '                foundFolder = True
    '            End If
    '        Next index
    '    End If

    '    Return result
    'End Function

    Public Function FindProperty(fileText As String, propertyName As String) As String     'returns the property in a file with a given name, property: value
        Dim lines() As String = fileText.Split(Environment.NewLine)

        For Each line As String In lines
            Dim currentProperty As String = line.Split(":")(0).Replace(vbLf, "")

            If currentProperty = propertyName Then
                Return Trim(line.Split(":")(1))         'issue: cant have colons anywhere else in the line
            End If
        Next line

        'DisplayError("Couldn't find property " & propertyName)

        Return Nothing
    End Function
#End Region
End Class

Public Structure Tag
    Dim name As String
    Dim argument As String

    Public Sub New(tagName As String, Optional argument As String = Nothing)
        name = tagName
        Me.argument = argument
    End Sub

    Public Sub New(ByVal tagString As String)
        'creates a tag from a string

        Dim newTag As Tag = JSONToTag(tagString)
        name = newTag.name
        argument = newTag.argument
    End Sub

    Public Overrides Function ToString() As String
        'turns this tag into a string

        Return TagToJSON(Me)
    End Function

    Public Function InterpretArgument(Optional subTagName As String = Nothing) As Object
        Dim temp As Object = InterpretValue(argument)

        'if subTagName is provided then searches the argument for a tag with the same name
        If Not IsNothing(subTagName) Then
            If IsArray(temp) Then
                For index As Integer = 0 To UBound(temp)
                    If Not IsNothing(temp(index)) AndAlso temp(index).name = subTagName Then
                        Return temp(index)
                    End If
                Next
            ElseIf Not IsNothing(temp) AndAlso temp.name = subTagName Then
                Return temp
            End If

            'couldn't find a matching sub tag
            Return Nothing
        End If

        Return temp
    End Function

    Public Sub SetArgument(newValue As Object)
        If IsArray(newValue) Then
            argument = ArrayToString(newValue)
        Else
            argument = newValue
        End If
    End Sub

    Public Shared Function AreTagsIdentical(tag1 As Tag, tag2 As Tag) As Boolean
        'used for = and <> operators

        If LCase(tag1.name) = LCase(tag2.name) AndAlso tag1.argument = tag2.argument Then
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

    Private framesList() As Frame
    Dim tags() As Tag

    Dim spriteFolderLocation As String

    Public Sub New(startFrames() As Frame, startTags() As Tag, spriteFolder As String, startLocation As PointF, Optional startRotation As Single = 0.0,
                       Optional startScale As Single = 1.0)
        spriteFolderLocation = spriteFolder
        Frames = startFrames
        tags = startTags

        CurrentFrame = 0
        Location = startLocation
        Rotation = startRotation
        Scale = startScale
    End Sub

    Public Sub New(entityString As String, renderEngine As PanelRenderEngine2)
        'creates a new entity from an entity string

        spriteFolderLocation = renderEngine.spriteFolderLocation
        If Not IsNothing(entityString) Then
            Dim newEnt As Entity = EntityStringHandler.ReadEntityString(entityString, renderEngine)
            Frames = newEnt.Frames
            tags = newEnt.tags
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return CreateEntityString(Me, spriteFolderLocation)
    End Function

    Public Overloads Function ToString(spriteFolderLocation As String) As String
        Return EntityStringHandler.CreateEntityString(Me, spriteFolderLocation)
    End Function

    Public Function Clone() As Entity
        'returns a clone of this entity

        'newClone.Frames = Frames
        Dim newClone As New Entity With {
            .spriteFolderLocation = spriteFolderLocation.Clone,
            .tags = tags.Clone
        }
        RefreshFramesList()

        Return newClone
    End Function


    Public Sub RefreshFramesList()
        'changes what is stored in framesList() using the "frames" tag

        Dim framesArgument() As Object = FindTag("frames").InterpretArgument()

        If Not IsNothing(framesArgument) Then
            Dim newFrames(UBound(framesArgument)) As Frame
            For frameIndex As Integer = 0 To UBound(framesArgument)
                Dim frameTag As New Tag(framesArgument(frameIndex).ToString)
                newFrames(frameIndex) = New Frame(frameTag, spriteFolderLocation)
            Next

            If newFrames IsNot Frames Then
                framesList = newFrames
            End If
        Else
            If Not IsNothing(Frames) Then
                framesList = Nothing
            End If
        End If
    End Sub

    Public Sub RefreshFramesTag()
        'changes the "frames" tag to match what is in framesList()

        AddTag(New Tag("frames", ArrayToString(framesList)), True)
    End Sub

    'Public Function GetFrames() As Frame()
    '    Return frames
    'End Function

    'Public Sub SetFrames(newFrames As Frame())
    '    frames = 
    'End Sub

    Public Property Frames As Frame()
        Get
            Return framesList
        End Get
        Set(value As Frame())
            framesList = value
            RefreshFramesTag()
        End Set
    End Property


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

    Public Sub AddTag(newTag As Tag, Optional removeDuplicates As Boolean = False)
        'adds the given tag to this entity's list of tags

        If removeDuplicates Then
            RemoveTag(newTag.name)
        End If

        If IsNothing(tags) Then
            ReDim tags(0)
        Else
            ReDim Preserve tags(UBound(tags) + 1)
        End If
        tags(UBound(tags)) = newTag

        CheckSpecialTagModified(newTag)
    End Sub

    Public Sub RemoveTag(tagName As String)
        'removes all tags with the given name

        Dim tagIndex As Integer = 0

        If Not IsNothing(tags) Then
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
        End If

        CheckSpecialTagModified(New Tag(tagName, Nothing))
    End Sub

    Public Sub SetTag(tagIndex As Integer, newTag As Tag)
        'changes the tag at the given index to the new tag

        If Not IsNothing(tags) And tagIndex >= 0 AndAlso tagIndex <= UBound(tags) Then
            tags(tagIndex) = newTag

            CheckSpecialTagModified(newTag)
        Else
            PanelRenderEngine2.DisplayError("Tried to change tag for entity " & Name & " but index (" & tagIndex & ") was out of bounds")
        End If
    End Sub

    Private Sub CheckSpecialTagModified(modifiedTag As Tag)
        'used for if something special needs to be done when a specific tag is changed

        Select Case modifiedTag.name
            Case "frames"
                RefreshFramesList()
        End Select
    End Sub


    Property Name As String
        Get
            If HasTag("name") Then
                Return FindTag("name").InterpretArgument()
            Else
                Return "unnamed"
            End If
        End Get
        Set(value As String)
            AddTag(New Tag("name", AddQuotes(value)), True)
        End Set
    End Property

    Property Location As PointF
        Get
            If HasTag("location") Then
                'Dim textForm As String = FindTag("location").InterpretArgument(0).ToString.Replace("{", "").Replace("}", "").Replace("{", "")
                'Return New PointF(Val(textForm.Split(",")(0).Trim.Replace("X=", "")),
                '                        Val(textForm.Split(",")(1).Trim.Replace("Y=", "")))
                Return New Point(Val(FindTag("location").InterpretArgument()(0)), Val(FindTag("location").InterpretArgument()(1)))
            Else
                Return New PointF(0, 0)
            End If
        End Get
        Set(value As PointF)
            AddTag(New Tag("location", "[" & value.X & "," & value.Y & "]"), True)
        End Set
    End Property

    Property Layer As Integer
        Get
            If HasTag("layer") Then
                Return FindTag("layer").InterpretArgument()
            Else
                Return 0
            End If
        End Get
        Set(value As Integer)
            AddTag(New Tag("layer", value), True)
        End Set
    End Property

    Property Scale As Single
        Get
            If HasTag("scale") Then
                Return FindTag("scale").InterpretArgument()
            Else
                Return 1
            End If
        End Get
        Set(value As Single)
            AddTag(New Tag("scale", value), True)
        End Set
    End Property

    Property Rotation As Single
        Get
            If HasTag("rotation") Then
                Return FindTag("rotation").InterpretArgument()
            Else
                Return 0
            End If
        End Get
        Set(value As Single)
            AddTag(New Tag("rotation", value), True)
        End Set
    End Property

    Property RotationAnchor As PointF
        Get
            If HasTag("rotationAnchor") Then
                Dim argStrings() As String = {FindTag("rotationAnchor").InterpretArgument()(0), FindTag("rotationAnchor").InterpretArgument()(1)}

                If Not IsNothing(argStrings(0)) AndAlso IsNumeric(argStrings(0)) AndAlso
                    Not IsNothing(argStrings(1)) AndAlso IsNumeric(argStrings(1)) Then
                    Return New PointF(Val(argStrings(0)), Val(argStrings(1)))
                End If
            End If

            Return New PointF(Frames(CurrentFrame).Dimensions.Width / 2, Frames(CurrentFrame).Dimensions.Height / 2)
        End Get
        Set(value As PointF)
            AddTag(New Tag("rotationAnchor", "[" & value.X & "," & value.Y & "]"), True)
        End Set
    End Property

    Property Opacity As Single
        Get
            If HasTag("opacity") Then
                Return FindTag("opacity").InterpretArgument()
            Else
                Return 1.0
            End If
        End Get
        Set(value As Single)
            AddTag(New Tag("opacity", value), True)

            'resets all the bitmaps because they are wrong now
            For Each currentFrame As Frame In Frames
                currentFrame.bitmapVersion = Nothing
            Next
        End Set
    End Property

    Property CurrentFrame As UInteger
        Get
            If HasTag("currentFrame") Then
                Return FindTag("currentFrame").InterpretArgument()
            Else
                Return 0
            End If
        End Get
        Set(value As UInteger)
            AddTag(New Tag("currentFrame", value), True)
        End Set
    End Property


    Public Shared Operator =(ent1 As Entity, ent2 As Entity)
        Return AreEntitiesEqual(ent1, ent2)
    End Operator

    Public Shared Operator <>(ent1 As Entity, ent2 As Entity)
        Return Not AreEntitiesEqual(ent1, ent2)
    End Operator

    Public Shared Function AreEntitiesEqual(ent1 As Entity, ent2 As Entity) As Boolean
        'returns whether 2 provided frames are identical

        If ent1.tags IsNot ent2.tags OrElse ent1.Frames IsNot ent2.Frames Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function GetEntityHitbox() As RectangleF
        Return New RectangleF(New PointF((Location.X - RotationAnchor.X),
                                            (Location.Y - RotationAnchor.Y)),
                                    New SizeF(Scale * (Frames(CurrentFrame).Dimensions.Width - 0),
                                            Scale * (Frames(CurrentFrame).Dimensions.Height - 0)))
    End Function
End Structure

Public Structure Sprite
    'sprites loaded from .sprt files and used to create frames

    Dim pixels(,) As Color
    Dim fileName As String

    Public Sub New(fileLocation As String)
        'creates a new sprite by reading it from a file

        If IO.File.Exists(fileLocation) Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd

            reader.Close()

            If SpriteHandler.ValidSpriteText(fileText, fileLocation) Then
                pixels = SpriteHandler.ReadPixelColours(fileText)
            End If

            'FindSpriteFileName(fileLocation)
            fileName = fileLocation
        Else
            PanelRenderEngine2.DisplayError("Couldn't find file " & fileLocation)
        End If
    End Sub

    'Public Sub New(spriteString As String, fileLocation As String)
    '    'creates a new sprite from a string

    '    If SpriteHandler.ValidSpriteText(spriteString, fileLocation) Then
    '        pixels = SpriteHandler.ReadPixelColours(spriteString)
    '    End If

    '    fileName = fileLocation
    'End Sub

    Public Sub New(colourIndices(,) As Integer, colours() As Color)
        'uses colours and colour indices to make a sprite

        If colours(0) <> Color.Transparent Then
            PanelRenderEngine2.DisplayError("Colour index 0 for new sprite is not transparent")
        End If

        ReDim pixels(colourIndices.GetUpperBound(0), colourIndices.GetUpperBound(1))
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
            PanelRenderEngine2.DisplayError("Sprite at " & fileLocation & " is not in folder " & spriteFolderName)
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return SpriteStringHandler.CreateString(Me)
    End Function
End Structure

Public Structure Frame
    'a frame of an entity is what is actually rendered, can be made up of multiple sprites with individual offsets

    Dim sprites() As Sprite
    Dim offsets() As Point

    Dim bitmapVersion As Bitmap
    Private spriteFolderLocation As String

    Public Sub New(frameTag As Tag, spriteFolderLocation As String)
        'creates a new frame from a frame tag

        Dim spriteTagStrings() As Object = frameTag.InterpretArgument()
        'ReDim sprites(UBound(spriteTagStrings))
        'ReDim offsets(UBound(spriteTagStrings))
        If Not IsNothing(spriteTagStrings) Then
            For index As Integer = 0 To UBound(spriteTagStrings)
                Dim spriteTag As New Tag(spriteTagStrings(index).ToString)
                Dim offsetArg As Object = spriteTag.InterpretArgument
                AddSprite(New Sprite(spriteFolderLocation & spriteTag.name), New Point(Val(offsetArg(0)), Val(offsetArg(1))))
            Next
        End If

        Me.spriteFolderLocation = spriteFolderLocation
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

    Public Function ToBitmap(opacity As Single) As Bitmap
        'returns a bitmap version of this frame

        Dim pixels(,) As Color = ToColourArray()
        Dim result As New Bitmap(pixels.GetUpperBound(0) + 1, pixels.GetUpperBound(1) + 1, Imaging.PixelFormat.Format32bppArgb)
        result.MakeTransparent()

        For pixelY As Integer = 0 To pixels.GetUpperBound(1)
            For pixelX As Integer = 0 To pixels.GetUpperBound(0)
                'updates the alpha channel of the colour
                Dim pixelColour As Color = pixels(pixelX, pixelY)
                If pixelColour.A > 0 Then       'doesn't change pixels that are already transparent
                    pixelColour = Color.FromArgb(opacity * 255, pixelColour)
                End If

                result.SetPixel(pixelX, pixelY, pixelColour)
            Next pixelX
        Next pixelY

        Return result
    End Function

    Public Function Dimensions() As Size
        'returns the max X and Y of the frame

        Dim result As New Size(0, 0)
        For index As Integer = 0 To UBound(sprites)
            If Not IsNothing(sprites(index).pixels) Then
                Dim maxSize As New Size(sprites(index).pixels.GetLength(0) + offsets(index).X, sprites(index).pixels.GetLength(1) + offsets(index).Y)

                If maxSize.Width > result.Width Then
                    result.Width = maxSize.Width
                End If
                If maxSize.Height > result.Height Then
                    result.Height = maxSize.Height
                End If
            End If
        Next index

        Return result
    End Function

    Public ReadOnly Property Centre As PointF
        'returns the location of the centre of the frame

        Get
            Return New PointF(Dimensions.Width / 2, Dimensions.Height / 2)
        End Get
    End Property

    'Public Sub Trim()
    '    'removes any outermost rows or columns which only have transparent pixels in them
    '    'unfinished

    '    MsgBox("Trimming for frames isn't ready yet")
    '    Dim trimmedSides(3) As Boolean

    '    For side As Integer = 1 To trimmedSides.Length
    '        Do Until trimmedSides(side) = True
    '            Dim x As Integer = 0
    '            Dim y As Integer = 0

    '            If side = 1 Or side = 2 Then      'for 1 and 2: top and bottom
    '                Dim rowEmpty As Boolean = True
    '            ElseIf side = 3 Or side = 4 Then  'for 3 and 4: left and right
    '                Dim colEmpty As Boolean = True
    '            End If
    '        Loop
    '    Next
    'End Sub

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

    Public Overrides Function ToString() As String
        If Not IsNothing(sprites) Then
            Dim spriteTags(UBound(sprites)) As Tag
            For index As Integer = 0 To UBound(sprites)
                spriteTags(index) = New Tag(sprites(index).fileName.Remove(0, Len(spriteFolderLocation)),
                                                ArrayToString({offsets(index).X, offsets(index).Y}))
            Next

            Return New Tag("frame", ArrayToString(spriteTags)).ToString
        Else
            Return New Tag("frame").ToString
        End If


        'Dim result As String = ""

        'For index As Integer = 0 To UBound(sprites)
        '    If IsNothing(sprites(index)) = False And Not IsNothing(offsets(index)) Then
        '        'this is wrong
        '        result += sprites(index).fileName.Remove(0, Len(spriteFolderLocation)) & "/" & offsets(index).ToString & ";"
        '    End If
        'Next

        'result = result.Remove(Len(result) - 1)

        'Return result
    End Function
End Structure