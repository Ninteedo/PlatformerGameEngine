Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class Sprite
    'sprites loaded from .sprt files and used to create frames

    'Private pixelColours(,) As Color
    Private coloursUsed() As Color
    Private colourIndices(,) As Integer
    Public fileName As String

    Private bitmapVersion As Image

    Private Const spriteTagName As String = "sprite"
    Private Const fileTagName As String = "fileName"
    Private Const coloursTagName As String = "colours"
    Private Const pixelsTagName As String = "pixels"

#Region "Contructors"

    Public Sub New()
        coloursUsed = {Color.Transparent}
        colourIndices = Nothing
        fileName = Nothing
        bitmapVersion = Nothing
    End Sub

    Public Sub New(colourIndices(,) As Integer, colours() As Color, Optional fileName As String = Nothing)
        'uses colours and colour indices to make a sprite

        If colours(0) <> Color.Transparent Then
            PRE2.DisplayError("Colour index 0 for new sprite is not transparent")
        End If

        Me.colourIndices = colourIndices
        Me.coloursUsed = colours
        Me.fileName = fileName
    End Sub

    Public Overrides Function ToString() As String
        'converts this sprite to a tag with subtags {file name, colours, colour indices}

        'converts all the colours to names
        Dim colourNames(UBound(Colours)) As String
        For index As Integer = 0 To UBound(Colours)
            Dim colourHex As String = "#"
            Dim hexSystem() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"}
            'converts the ARGB values of the colour to hex
            For Each channel As Integer In {Colours(index).A, Colours(index).R, Colours(index).G, Colours(index).B}
                'hex conversion
                Dim hexVersion As String = ""
                Dim num As Integer = channel
                Dim remainder As Integer = 0

                Do Until remainder = 0
                    Dim original As Integer = num
                    num = Int(num / 16)
                    remainder = (original / 16 - num) * 16

                    hexVersion += hexSystem(remainder)
                Loop

                colourHex += hexVersion
            Next
            colourNames(index) = colourHex
        Next

        Return New Tag(spriteTagName, ArrayToString({
                New Tag(fileTagName, fileName),
                New Tag(coloursTagName, ArrayToString(colourNames)),
                New Tag(pixelsTagName, ArrayToString(Indices))
                                            })).ToString
    End Function

    Public Sub New(ByVal fileLocation As String, ByVal spriteFolderLocation As String)
        Dim fileText As String = PRE2.ReadFile(fileLocation)
        If Not IsNothing(fileText) Then
            fileName = fileLocation.Remove(0, Len(spriteFolderLocation))
            Dim spriteTag As New Tag(fileText)

            Dim newSprite As New Sprite(spriteTag.ToString)
            Me.Colours = newSprite.Colours
            Me.Indices = newSprite.Indices
        End If
    End Sub

    Public Sub New(ByVal spriteString As String)
        Dim spriteTag As New Tag(spriteString)
        Dim coloursTag As Tag = spriteTag.InterpretArgument(coloursTagName)
        Dim pixelsTag As Tag = spriteTag.InterpretArgument(pixelsTagName)

        If Not IsNothing(coloursTag) Then
            Dim colourNamesTemp() As Object = coloursTag.InterpretArgument
            If Not IsNothing(colourNamesTemp) Then
                Dim colourNames(UBound(colourNamesTemp)) As String
                For index As Integer = 0 To UBound(colourNamesTemp)
                    colourNames(index) = colourNamesTemp(index)
                Next
                If Not IsNothing(colourNames) Then
                    'get colours
                    Dim coloursUsed(UBound(colourNames)) As Color
                    For colourIndex As Integer = 0 To UBound(colourNames)
                        coloursUsed(colourIndex) = ColorTranslator.FromHtml(colourNames(colourIndex))
                    Next

                    'gets pixels
                    If Not IsNothing(pixelsTag) Then
                        Dim colourIndicesTemp As Object = pixelsTag.InterpretArgument
                        If Not IsNothing(colourIndicesTemp) AndAlso colourIndicesTemp.Rank = 2 Then
                            Dim colourIndices(colourIndicesTemp.GetUpperBound(0), colourIndicesTemp.GetUpperBound(1)) As Integer
                            For index2 As Integer = 0 To colourIndices.GetUpperBound(1)
                                For index1 As Integer = 0 To colourIndices.GetUpperBound(0)
                                    SetPixelColour(New Point(index1, index2), coloursUsed(colourIndicesTemp(index1, index2)))
                                Next
                            Next
                        End If
                    Else
                        PRE2.DisplayError("Unable to find tag for pixels in sprite: " & fileName)
                    End If
                Else
                    PRE2.DisplayError("Unable to find colours for sprite: " & fileName)
                End If
            Else
                PRE2.DisplayError("Invalid colour argument: " & coloursTag.argument)
            End If
        Else
            PRE2.DisplayError("Unable to find tag for colours in sprite: " & fileName)
        End If
    End Sub

#End Region

    Public ReadOnly Property Bitmap As Image
        Get
            If IsNothing(bitmapVersion) Then
                bitmapVersion = ToBitmap()
            End If
            Return bitmapVersion
        End Get
    End Property

    Private Sub BitmapModified()
        'sets the bitmap to nothing if the colours or colourIndices are changed so the bitmap isnt outdated
        bitmapVersion = Nothing
    End Sub

    'the colours of each individual pixel
    'Public ReadOnly Property Pixels As Color(,)
    '    Get
    '        If Not IsNothing(colourIndices) AndAlso Not IsNothing(coloursUsed) Then
    '            Dim result(colourIndices.GetUpperBound(0), colourIndices.GetUpperBound(1)) As Color
    '            For index2 As Integer = 0 To result.GetUpperBound(1)
    '                For index1 As Integer = 0 To result.GetUpperBound(0)
    '                    result(index1, index2) = coloursUsed(colourIndices(index1, index2))
    '                Next
    '            Next

    '            Return result
    '        Else
    '            Return Nothing
    '        End If
    '    End Get
    'End Property

    Public Function GetPixelColour(ByVal coords As Point) As Color
        'returns the colour of the pixel at the specified coordinates

        If ValidCoords(coords) Then
            Return coloursUsed(colourIndices(coords.X, coords.Y))
        End If
    End Function

    Public Function GetPixelColour(ByVal x As Integer, ByVal y As Integer) As Color
        Return GetPixelColour(New Point(x, y))
    End Function

    Public Sub SetPixelColour(ByVal coords As Point, ByVal newColour As Color)
        'sets the colour of a single pixel at the specified coordinates

        If ValidCoords(coords) Then
            'finds colour index
            Dim colourIndex As Integer = -1
            For index As Integer = 0 To UBound(coloursUsed)
                If newColour = coloursUsed(index) Then
                    colourIndex = index
                End If
            Next

            'colour not already present
            If colourIndex = -1 Then
                coloursUsed = InsertItem(coloursUsed, newColour)
                colourIndex = UBound(coloursUsed)
            End If

            colourIndices(coords.X, coords.Y) = colourIndex
        End If
    End Sub

    Public Sub SetPixelColour(ByVal x As Integer, ByVal y As Integer, ByVal newColour As Color)
        SetPixelColour(New Point(x, y), newColour)
    End Sub

    Public Function ValidCoords(ByVal coords As Point) As Boolean
        'returns whether provided coords are within range of the grid

        If Not IsNothing(colourIndices) Then
            Return coords.X >= 0 And coords.X <= colourIndices.GetUpperBound(0) And coords.Y >= 0 And coords.Y < colourIndices.GetUpperBound(1)
        Else
            Return False
        End If
    End Function

    Public Function ValidCoords(ByVal x As Integer, ByVal y As Integer) As Boolean
        Return ValidCoords(New Point(x, y))
    End Function

    Public Property Colours As Color()
        Get
            Return coloursUsed
        End Get
        Set(value As Color())
            coloursUsed = value
            BitmapModified()
        End Set
    End Property

    Public Property Indices As Integer(,)
        Get
            Return colourIndices
        End Get
        Set(value As Integer(,))
            colourIndices = value
            BitmapModified()
        End Set
    End Property

    'Public Property Colours As Color()
    '    'the colours present in the sprite
    '    '0 is always transparent
    '    'the rest are ordered by first appearance
    '    Get
    '        Dim coloursUsed() As Color = {Color.Transparent}
    '        If Not IsNothing(Pixels) Then
    '            For index2 As Integer = 0 To Pixels.GetUpperBound(1)
    '                For index1 As Integer = 0 To Pixels.GetUpperBound(0)
    '                    'checks if colour has already been added
    '                    Dim duplicate As Boolean = False
    '                    For colourIndex As Integer = 0 To UBound(coloursUsed)
    '                        If Pixels(index1, index2) = coloursUsed(colourIndex) Then
    '                            duplicate = True
    '                            Exit For
    '                        End If
    '                    Next

    '                    'only adds colour if it isn't a duplicate
    '                    If Not duplicate Then
    '                        coloursUsed = InsertItem(coloursUsed, Pixels(index1, index2))
    '                    End If
    '                Next
    '            Next
    '        End If

    '        Return coloursUsed
    '    End Get
    '    Set(value As Color())
    '        Dim startColourIndices(,) As Integer = ColourIndices

    '        If Not IsNothing(Pixels) Then
    '            For index2 As Integer = 0 To Pixels.GetUpperBound(1)
    '                For index1 As Integer = 0 To Pixels.GetUpperBound(0)
    '                    If startColourIndices(index1, index2) <= UBound(value) Then
    '                        Pixels(index1, index2) = value(startColourIndices(index1, index2))
    '                    Else        'if colour index is out of range then sets pixel to transparent
    '                        Pixels(index1, index2) = Color.Transparent
    '                    End If
    '                Next
    '            Next
    '        End If
    '    End Set
    'End Property

    'Public Property ColourIndices As Integer(,)
    '    'the colour index is where index of where the colour of the pixel appears in the colours array

    '    Get
    '        If Not IsNothing(Pixels) Then
    '            Dim result(Pixels.GetUpperBound(0), Pixels.GetUpperBound(1)) As Integer

    '            For index2 As Integer = 0 To Pixels.GetUpperBound(1)
    '                For index1 As Integer = 0 To Pixels.GetUpperBound(0)
    '                    For colourIndex As Integer = 0 To UBound(Colours)       'finds the corresponding colour index
    '                        If Colours(colourIndex) = Pixels(index1, index2) Then
    '                            result(index1, index2) = colourIndex
    '                            Exit For
    '                        End If
    '                    Next
    '                Next
    '            Next

    '            Return result
    '        Else
    '            Return Nothing
    '        End If
    '    End Get
    '    Set(value As Integer(,))
    '        Dim coloursUsed As Color() = Colours
    '        ReDim Pixels(value.GetUpperBound(0), value.GetUpperBound(1))

    '        For index2 As Integer = 0 To Pixels.GetUpperBound(1)
    '            For index1 As Integer = 0 To Pixels.GetUpperBound(0)
    '                'If value(index1, index2) <= UBound(coloursUsed) Then
    '                Pixels(index1, index2) = coloursUsed(value(index1, index2))
    '                'Else        'if colour index is out of range then sets pixel to transparent
    '                '    Pixels(index1, index2) = Color.Transparent
    '                'End If
    '            Next
    '        Next
    '    End Set
    'End Property

    Private Function ToBitmap(Optional opacity As Single = 1) As Bitmap
        'returns a bitmap version of this frame

        Dim result As New Bitmap(colourIndices.GetUpperBound(0) + 1, colourIndices.GetUpperBound(1) + 1, Imaging.PixelFormat.Format32bppArgb)
        result.MakeTransparent()        'makes the background of the bitmap transparent

        For pixelY As Integer = 0 To colourIndices.GetUpperBound(1)
            For pixelX As Integer = 0 To colourIndices.GetUpperBound(0)
                'updates the alpha channel of the colour
                Dim pixelColour As Color = GetPixelColour(pixelX, pixelY)
                If Not pixelColour.A = 0 Then       'doesn't change pixels that are already transparent
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
            If Not IsNothing(colourIndices) Then
                Return New Size(colourIndices.GetLength(0), colourIndices.GetLength(1))
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
