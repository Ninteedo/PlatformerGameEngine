Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class Sprite
    'sprites loaded from .sprt files and used to create frames

    Private pixelColours(,) As Color
    Public fileName As String

    Private bitmapVersion As Image

    Private Const spriteTagName As String = "sprite"
    Private Const fileTagName As String = "fileName"
    Private Const coloursTagName As String = "colours"
    Private Const pixelsTagName As String = "pixels"

    Public Sub New()
        pixelColours = Nothing
        fileName = Nothing
        bitmapVersion = Nothing
    End Sub

    Public Sub New(colourIndices(,) As Integer, colours() As Color)
        'uses colours and colour indices to make a sprite

        If colours(0) <> Color.Transparent Then
            PanelRenderEngine2.DisplayError("Colour index 0 for new sprite is not transparent")
        End If

        ReDim Pixels(colourIndices.GetUpperBound(0), colourIndices.GetUpperBound(1))
        For x As Integer = 0 To colourIndices.GetUpperBound(0)
            For y As Integer = 0 To colourIndices.GetUpperBound(1)
                Pixels(x, y) = colours(colourIndices(x, y))
            Next y
        Next x
    End Sub

    Public Overrides Function ToString() As String
        'converts this sprite to a tag with subtags {file name, colours, colour indices}
        Dim colourIndices()() As Integer = Nothing
        Dim colourNames() As String = Nothing
        If Not IsNothing(Pixels) Then
            'finds all colours used
            Dim coloursUsed() As Color = {Color.Transparent}
            colourNames = {ColorTranslator.ToHtml(coloursUsed(0))}
            For index2 As Integer = 0 To Pixels.GetUpperBound(1)
                For index1 As Integer = 0 To Pixels.GetUpperBound(0)
                    'checks if colour has already been added
                    Dim duplicate As Boolean = False
                    For colourIndex As Integer = 0 To UBound(coloursUsed)
                        If Pixels(index1, index2) = coloursUsed(colourIndex) Then
                            duplicate = True
                            Exit For
                        End If
                    Next

                    'only adds colour if it isn't a duplicate
                    If Not duplicate Then
                        coloursUsed = InsertItem(coloursUsed, Pixels(index1, index2))
                        colourNames = InsertItem(colourNames, ColorTranslator.ToHtml(coloursUsed(UBound(coloursUsed))))
                    End If
                Next
            Next

            'finds the colour index of each pixel
            ReDim colourIndices(Pixels.GetUpperBound(0))
            For index1 As Integer = 0 To Pixels.GetUpperBound(0)
                ReDim colourIndices(index1)(Pixels.GetUpperBound(1))
                For index2 As Integer = 0 To Pixels.GetUpperBound(1)
                    For colourIndex As Integer = 0 To UBound(coloursUsed)
                        If coloursUsed(colourIndex) = Pixels(index1, index2) Then
                            colourIndices(index1)(index2) = colourIndex
                        End If
                    Next
                Next
            Next
        End If

        Return New Tag(spriteTagName, ArrayToString({
                    New Tag(fileTagName, fileName),
                    New Tag(coloursTagName, ArrayToString(colourNames)),
                    New Tag(pixelsTagName, ArrayToString(colourIndices))
                                               })).ToString
    End Function

    Public Sub New(ByVal fileLocation As String, ByVal spriteFolderLocation As String)
        Dim fileText As String = PanelRenderEngine2.ReadFile(fileLocation)
        If Not IsNothing(fileText) Then
            fileName = fileLocation.Remove(0, Len(spriteFolderLocation))
            Dim spriteTag As New Tag(fileText)

            Dim newSprite As New Sprite(spriteTag.ToString)
            Pixels = newSprite.Pixels
        End If
    End Sub

    Public Sub New(ByVal spriteString As String)
        Dim spriteTag As New Tag(spriteString)
        Dim coloursTag As Tag = spriteTag.FindSubTag(coloursTagName)
        Dim pixelsTag As Tag = spriteTag.FindSubTag(pixelsTagName)

        If Not IsNothing(coloursTag) Then
            Dim colourNames() As String = coloursTag.InterpretArgument(Of String())
            Dim coloursUsed() As Color
            If Not IsNothing(colourNames) Then
                'get colours
                ReDim coloursUsed(UBound(colourNames))
                For colourIndex As Integer = 0 To UBound(colourNames)
                    coloursUsed(colourIndex) = ColorTranslator.FromHtml(colourNames(colourIndex))
                Next

                'gets pixels
                If Not IsNothing(pixelsTag) Then
                    Dim colourIndices()() As Integer = pixelsTag.InterpretArgument(Of Integer()())

                    For index2 As Integer = 0 To colourIndices.GetUpperBound(1)
                        For index1 As Integer = 0 To colourIndices.GetUpperBound(0)
                            Pixels(index1, index2) = coloursUsed(colourIndices(index1)(index2))
                        Next
                    Next
                Else
                    PRE2.DisplayError("Unable to find tag for pixels in sprite " & fileName)
                End If
            Else
                PRE2.DisplayError("Unable to find colours for sprite " & fileName)
            End If
        Else
            PRE2.DisplayError("Unable to find tag for colours in sprite " & fileName)
        End If
    End Sub

    Public ReadOnly Property Bitmap As Image
        Get
            If IsNothing(bitmapVersion) Then
                bitmapVersion = ToBitmap()
            End If
            Return bitmapVersion
        End Get
    End Property

    Public Property Pixels As Color(,)
        Get
            Return pixelColours
        End Get
        Set(value As Color(,))
            pixelColours = value
            bitmapVersion = Nothing
        End Set
    End Property

    Public Property Colours As Color()
        'the colours present in the sprite
        '0 is always transparent
        'the rest are ordered by first appearance
        Get
            Dim coloursUsed() As Color = {Color.Transparent}
            If Not IsNothing(Pixels) Then
                For index2 As Integer = 0 To Pixels.GetUpperBound(1)
                    For index1 As Integer = 0 To Pixels.GetUpperBound(0)
                        'checks if colour has already been added
                        Dim duplicate As Boolean = False
                        For colourIndex As Integer = 0 To UBound(coloursUsed)
                            If Pixels(index1, index2) = coloursUsed(colourIndex) Then
                                duplicate = True
                                Exit For
                            End If
                        Next

                        'only adds colour if it isn't a duplicate
                        If Not duplicate Then
                            coloursUsed = InsertItem(coloursUsed, Pixels(index1, index2))
                        End If
                    Next
                Next
            End If

            Return coloursUsed
        End Get
        Set(value As Color())
            Dim startColourIndices(,) As Integer = ColourIndices

            If Not IsNothing(Pixels) Then
                For index2 As Integer = 0 To Pixels.GetUpperBound(1)
                    For index1 As Integer = 0 To Pixels.GetUpperBound(0)
                        If startColourIndices(index1, index2) <= UBound(value) Then
                            Pixels(index1, index2) = value(startColourIndices(index1, index2))
                        Else        'if colour index is out of range then sets pixel to transparent
                            Pixels(index1, index2) = Color.Transparent
                        End If
                    Next
                Next
            End If
        End Set
    End Property

    Public Property ColourIndices As Integer(,)
        'the colour index is where index of where the colour of the pixel appears in the colours array

        Get
            If Not IsNothing(Pixels) Then
                Dim result(Pixels.GetUpperBound(0), Pixels.GetUpperBound(1)) As Integer

                For index2 As Integer = 0 To Pixels.GetUpperBound(1)
                    For index1 As Integer = 0 To Pixels.GetUpperBound(0)
                        For colourIndex As Integer = 0 To UBound(Colours)       'finds the corresponding colour index
                            If Colours(colourIndex) = Pixels(index1, index2) Then
                                result(index1, index2) = colourIndex
                                Exit For
                            End If
                        Next
                    Next
                Next

                Return result
            Else
                Return Nothing
            End If
        End Get
        Set(value As Integer(,))
            Dim coloursUsed As Color() = Colours
            ReDim Pixels(value.GetUpperBound(0), value.GetUpperBound(1))

            For index2 As Integer = 0 To Pixels.GetUpperBound(1)
                For index1 As Integer = 0 To Pixels.GetUpperBound(0)
                    'If value(index1, index2) <= UBound(coloursUsed) Then
                    Pixels(index1, index2) = coloursUsed(value(index1, index2))
                    'Else        'if colour index is out of range then sets pixel to transparent
                    '    Pixels(index1, index2) = Color.Transparent
                    'End If
                Next
            Next
        End Set
    End Property

    Private Function ToBitmap(Optional opacity As Single = 1) As Bitmap
        'returns a bitmap version of this frame

        Dim result As New Bitmap(Pixels.GetUpperBound(0) + 1, Pixels.GetUpperBound(1) + 1, Imaging.PixelFormat.Format32bppArgb)
        result.MakeTransparent()

        For pixelY As Integer = 0 To Pixels.GetUpperBound(1)
            For pixelX As Integer = 0 To Pixels.GetUpperBound(0)
                'updates the alpha channel of the colour
                Dim pixelColour As Color = Pixels(pixelX, pixelY)
                If pixelColour.A > 0 Then       'doesn't change pixels that are already transparent
                    pixelColour = Color.FromArgb(opacity * 255, pixelColour)
                End If

                result.SetPixel(pixelX, pixelY, pixelColour)
            Next pixelX
        Next pixelY

        Return result
    End Function

    Public ReadOnly Property Dimensions As Size
        'returns the max X and Y of the frame

        Get
            If Not IsNothing(Pixels) Then
                Return New Size(Pixels.GetLength(0), Pixels.GetLength(1))
            Else
                Return New Size(0, 0)
            End If
        End Get
    End Property

    Public ReadOnly Property Centre As PointF
        'returns the location of the centre of the frame

        Get
            Return New PointF(Dimensions.Width / 2, Dimensions.Height / 2)
        End Get
    End Property
End Class
