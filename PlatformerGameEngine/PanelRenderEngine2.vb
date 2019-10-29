'Richard Holmes
'22/03/2019
'Panel Render Engine v2

'Imports SpriteHandler = PlatformerGameEngine.SpriteStringHandler

Public Class PanelRenderEngine2

    Public renderPanel As Panel
    Public loadedSprites() As Sprite

    'Public renderScaleFactor As Single = 10                 'the overall custom render scaling of the game (might be unnecessary)
    Public renderResolution As Size = New Size(640, 480)      'the intended size for the game
    'Public renderPixelPerfect As Boolean = False              'true: render window size is set to resolution, false: game is scaled to fit the render window

    Public spriteFolderLocation As String
    Public actorFolderLocation As String
    Public levelFolderLocation As String
    'Public roomFolderLocation As String

#Region "Rendering"

    Public Sub DoGameRender(ByRef actorList() As Actor)
        'renders a list of actors

        Using canvas As New PaintEventArgs(renderPanel.CreateGraphics, New Rectangle(New Point(0, 0), renderPanel.Size))
            canvas.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            Dim context As BufferedGraphicsContext = BufferedGraphicsManager.Current
            context.MaximumBuffer = renderPanel.Size
            Dim renderLayer As BufferedGraphics = context.Allocate(canvas.Graphics, canvas.ClipRectangle)

            If IsNothing(actorList) = False Then
                'sorts actorList by layer
                Dim swaps As Integer = 0
                Dim passes As Integer = 0
                Do While swaps > 0 Or passes = 0
                    swaps = 0
                    For index As Integer = 1 To UBound(actorList)
                        If actorList(index).Layer > actorList(index - 1).Layer Then
                            Dim temp As Actor = actorList(index)
                            actorList(index) = actorList(index - 1)
                            actorList(index - 1) = temp
                            swaps += 1
                        End If
                    Next
                    passes += 1
                Loop

                'renders each actor in sorted order
                For actorIndex As Integer = 0 To UBound(actorList)
                    Dim currentActor As Actor = actorList(actorIndex)
                    If IsNothing(currentActor.Sprites) = False AndAlso currentActor.CurrentFrame <= UBound(currentActor.Sprites) And currentActor.CurrentFrame >= 0 Then
                        Dim renderSprite As Sprite = currentActor.Sprites(currentActor.CurrentFrame)

                        Dim renderSize As SizeF = New SizeF(
                                        currentActor.Scale * RenderScale.Width * (renderSprite.Dimensions.Width + 0.5),
                                        currentActor.Scale * RenderScale.Height * (renderSprite.Dimensions.Height + 0.5))
                        'Dim renderArea As New RectangleF(New PointF((currentActor.Location.X - currentActor.RotationAnchor.X - 0.5) * RenderScale.Width,
                        '                                            (currentActor.Location.Y - currentActor.RotationAnchor.Y - 0.5) * RenderScale.Height), renderSize)
                        Dim renderArea As New RectangleF(New PointF(currentActor.Location.X * RenderScale.Width, currentActor.Location.Y * RenderScale.Height), renderSize)
                        renderLayer.Graphics.DrawImage(renderSprite.Bitmap, renderArea)
                    End If
                Next actorIndex
            End If

            renderLayer.Render()
        End Using
    End Sub

    Public ReadOnly Property RenderScale As SizeF   'the render scaling used by the renderer
        Get
            Return New SizeF(renderPanel.Size.Width / renderResolution.Width, renderPanel.Size.Height / renderResolution.Height)
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




'Public Structure Frame
'    'a frame of an actor is what is actually rendered, can be made up of multiple sprites with individual offsets

'    Dim sprites() As Sprite
'    Dim offsets() As Point

'    Dim bitmapVersion As Bitmap
'    Private spriteFolderLocation As String

'    Public Sub New(frameTag As Tag, spriteFolderLocation As String)
'        'creates a new frame from a frame tag

'        Dim spriteTagStrings() As Object = frameTag.InterpretArgument()
'        'ReDim sprites(UBound(spriteTagStrings))
'        'ReDim offsets(UBound(spriteTagStrings))
'        If Not IsNothing(spriteTagStrings) Then
'            For index As Integer = 0 To UBound(spriteTagStrings)
'                Dim spriteTag As New Tag(spriteTagStrings(index).ToString)
'                Dim offsetArg As Object = spriteTag.InterpretArgument
'                AddSprite(New Sprite(spriteFolderLocation & spriteTag.name), New Point(Val(offsetArg(0)), Val(offsetArg(1))))
'            Next
'        End If

'        Me.spriteFolderLocation = spriteFolderLocation
'    End Sub


'    Public Function ToColourArray() As Color(,)
'        Dim pixels(Dimensions.Width - 1, Dimensions.Height - 1) As Color

'        For x As Integer = 0 To pixels.GetUpperBound(0)
'            For y As Integer = 0 To pixels.GetUpperBound(1)
'                pixels(x, y) = Color.Transparent            'sets all the default colours to transparent
'            Next y
'        Next x

'        'this is the part which actually sets the frame's pixels
'        For index As Integer = 0 To UBound(sprites)
'            For x As Integer = offsets(index).X To offsets(index).X + sprites(index).pixels.GetUpperBound(0)
'                For y As Integer = offsets(index).Y To offsets(index).Y + sprites(index).pixels.GetUpperBound(1)
'                    If x >= 0 And y >= 0 Then       'checks that x and y are both positive
'                        Dim pixelColour As Color = sprites(index).pixels(x - offsets(index).X, y - offsets(index).Y)

'                        If pixelColour.A <> 0 Then
'                            pixels(x, y) = pixelColour
'                        End If
'                    End If
'                Next y
'            Next x
'        Next index

'        Return pixels
'    End Function

'    Public Function ToBitmap(opacity As Single) As Bitmap
'        'returns a bitmap version of this frame

'        Dim pixels(,) As Color = ToColourArray()
'        Dim result As New Bitmap(pixels.GetUpperBound(0) + 1, pixels.GetUpperBound(1) + 1, Imaging.PixelFormat.Format32bppArgb)
'        result.MakeTransparent()

'        For pixelY As Integer = 0 To pixels.GetUpperBound(1)
'            For pixelX As Integer = 0 To pixels.GetUpperBound(0)
'                'updates the alpha channel of the colour
'                Dim pixelColour As Color = pixels(pixelX, pixelY)
'                If pixelColour.A > 0 Then       'doesn't change pixels that are already transparent
'                    pixelColour = Color.FromArgb(opacity * 255, pixelColour)
'                End If

'                result.SetPixel(pixelX, pixelY, pixelColour)
'            Next pixelX
'        Next pixelY

'        Return result
'    End Function

'    Public Function Dimensions() As Size
'        'returns the max X and Y of the frame

'        Dim result As New Size(0, 0)
'        For index As Integer = 0 To UBound(sprites)
'            If Not IsNothing(sprites(index).pixels) Then
'                Dim maxSize As New Size(sprites(index).pixels.GetLength(0) + offsets(index).X, sprites(index).pixels.GetLength(1) + offsets(index).Y)

'                If maxSize.Width > result.Width Then
'                    result.Width = maxSize.Width
'                End If
'                If maxSize.Height > result.Height Then
'                    result.Height = maxSize.Height
'                End If
'            End If
'        Next index

'        Return result
'    End Function

'    Public ReadOnly Property Centre As PointF
'        'returns the location of the centre of the frame

'        Get
'            Return New PointF(Dimensions.Width / 2, Dimensions.Height / 2)
'        End Get
'    End Property

'    'Public Sub Trim()
'    '    'removes any outermost rows or columns which only have transparent pixels in them
'    '    'unfinished

'    '    MsgBox("Trimming for frames isn't ready yet")
'    '    Dim trimmedSides(3) As Boolean

'    '    For side As Integer = 1 To trimmedSides.Length
'    '        Do Until trimmedSides(side) = True
'    '            Dim x As Integer = 0
'    '            Dim y As Integer = 0

'    '            If side = 1 Or side = 2 Then      'for 1 and 2: top and bottom
'    '                Dim rowEmpty As Boolean = True
'    '            ElseIf side = 3 Or side = 4 Then  'for 3 and 4: left and right
'    '                Dim colEmpty As Boolean = True
'    '            End If
'    '        Loop
'    '    Next
'    'End Sub

'    Public Shared Operator =(frame1 As Frame, frame2 As Frame)
'        Return AreFramesEqual(frame1, frame2)
'    End Operator

'    Public Shared Operator <>(frame1 As Frame, frame2 As Frame)
'        Return Not AreFramesEqual(frame1, frame2)
'    End Operator

'    Public Shared Function AreFramesEqual(frame1 As Frame, frame2 As Frame) As Boolean
'        'returns whether 2 provided frames are identical

'        Dim result As Boolean = True

'        If frame1.Dimensions <> frame2.Dimensions Then      'checks that they are the same size
'            result = False
'        Else
'            'checks that none of the pixels are different
'            Dim pixels1(,) As Color = frame1.ToColourArray
'            Dim pixels2(,) As Color = frame2.ToColourArray

'            For x As Integer = 0 To frame1.Dimensions.Width - 1
'                For y As Integer = 0 To frame1.Dimensions.Width - 1
'                    If pixels1(x, y) <> pixels2(x, y) Then
'                        result = False
'                    End If
'                Next y
'            Next x
'        End If

'        Return result
'    End Function

'    Public Sub AddSprite(newSprite As Sprite, offset As Point)
'        'adds a sprite to the frame

'        If IsNothing(sprites) = True Then
'            ReDim sprites(0)
'        Else
'            ReDim Preserve sprites(UBound(sprites) + 1)
'        End If
'        sprites(UBound(sprites)) = newSprite

'        If IsNothing(offsets) = True Then
'            ReDim offsets(0)
'        Else
'            ReDim Preserve offsets(UBound(offsets) + 1)
'        End If
'        offsets(UBound(offsets)) = offset
'    End Sub

'    Public Overrides Function ToString() As String
'        If Not IsNothing(sprites) Then
'            Dim spriteTags(UBound(sprites)) As Tag
'            For index As Integer = 0 To UBound(sprites)
'                spriteTags(index) = New Tag(sprites(index).fileName.Remove(0, Len(spriteFolderLocation)),
'                                                ArrayToString({offsets(index).X, offsets(index).Y}))
'            Next

'            Return New Tag("frame", ArrayToString(spriteTags)).ToString
'        Else
'            Return New Tag("frame").ToString
'        End If


'        'Dim result As String = ""

'        'For index As Integer = 0 To UBound(sprites)
'        '    If IsNothing(sprites(index)) = False And Not IsNothing(offsets(index)) Then
'        '        'this is wrong
'        '        result += sprites(index).fileName.Remove(0, Len(spriteFolderLocation)) & "/" & offsets(index).ToString & ";"
'        '    End If
'        'Next

'        'result = result.Remove(Len(result) - 1)

'        'Return result
'    End Function
'End Structure