Imports PlatformerGameEngine.My.Resources

Public Class Sprite

    Dim _coloursUsed() As Color
    Dim _colourIndices(,) As Integer
    Dim _bitmapVersion As Bitmap


#Region "Contructors"

    Public Sub New(Optional startSize As Size = Nothing)
        _coloursUsed = {Color.Transparent}
        _colourIndices = Nothing
        _bitmapVersion = Nothing
        Dimensions = startSize
    End Sub

    Public Sub New(spriteString As String)
        'checks if the input is actually a file location
        If IO.File.Exists(spriteString) Then
            spriteString = ReadFile(spriteString)
        End If

        Dim spriteTag As New Tag(spriteString)
        Dim coloursTag As Tag = spriteTag.InterpretArgument(ColoursTagName)
        Dim pixelsTag As Tag = spriteTag.InterpretArgument(PixelsTagName)

        'gets the colour and pixels
        If Not IsNothing(coloursTag) Then
            Dim colourNamesTemp() As Object = coloursTag.InterpretArgument
            If Not IsNothing(colourNamesTemp) Then
                'gets colour names
                Dim colourNames(UBound(colourNamesTemp)) As String
                For index As Integer = 0 To UBound(colourNamesTemp)
                    colourNames(index) = colourNamesTemp(index)
                Next
                If Not IsNothing(colourNames) Then
                    'converts colour names to colours
                    ReDim _coloursUsed(UBound(colourNames))
                    For colourIndex As Integer = 0 To UBound(colourNames)
                        _coloursUsed(colourIndex) = ColorTranslator.FromHtml(colourNames(colourIndex))
                    Next

                    'gets pixels
                    If Not IsNothing(pixelsTag) Then
                        Dim colourIndicesTemp As Object = pixelsTag.InterpretArgument       'returns as a jagged array of integer
                        If Not IsNothing(colourIndicesTemp) Then
                            ReDim _colourIndices(UBound(colourIndicesTemp), UBound(colourIndicesTemp(0)))
                            For index2 As Integer = 0 To _colourIndices.GetUpperBound(1)
                                For index1 As Integer = 0 To _colourIndices.GetUpperBound(0)
                                    SetPixelColour(New Point(index1, index2), _coloursUsed(colourIndicesTemp(index1)(index2)))
                                Next
                            Next
                        Else
                            DisplayError("Unable to read pixels in a sprite")
                        End If
                    Else
                        DisplayError("Unable to find tag for pixels in a sprite")
                    End If
                Else
                    DisplayError("Unable to find colours in a sprite")
                End If
            Else
                DisplayError("Invalid colour argument: " & coloursTag.Argument)
            End If
        Else
            DisplayError("Unable to find tag for colours in a sprite")
        End If
    End Sub

#End Region

#Region "Bitmap"

    Public ReadOnly Property Bitmap As Bitmap
        Get
            If IsNothing(_bitmapVersion) Then
                _bitmapVersion = ToBitmap()
            End If
            Return _bitmapVersion
        End Get
    End Property

    Private Function ToBitmap(Optional opacity As Single = 1) As Bitmap
        'returns a bitmap version of this frame

        'returns a blank bitmap if no colour or indices
        If IsNothing(Colours) Or IsNothing(Indices) Then
            Return New Bitmap(1, 1)
        End If

        Dim result As New Bitmap(_colourIndices.GetUpperBound(0) + 1, _colourIndices.GetUpperBound(1) + 1, Imaging.PixelFormat.Format32bppArgb)
        result.MakeTransparent()        'makes the background of the bitmap transparent
        For pixelY As Integer = 0 To _colourIndices.GetUpperBound(1)
            For pixelX As Integer = 0 To _colourIndices.GetUpperBound(0)
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

    Private Sub ResetBitmap()
        'sets the bitmap to nothing if the colours or indices are changed so the bitmap isn't outdated
        _bitmapVersion = Nothing
    End Sub

#End Region

#Region "Pixels"

    Public Function GetPixelColour(coords As Point) As Color
        'returns the colour of the pixel at the specified coordinates

        If ValidCoords(coords) Then
            Return _coloursUsed(_colourIndices(coords.X, coords.Y))
        End If
    End Function

    Public Function GetPixelColour(x As Integer, y As Integer) As Color
        Return GetPixelColour(New Point(x, y))
    End Function

    Public Sub SetPixelColour(coords As Point, newColour As Color)
        'sets the colour of a single pixel at the specified coordinates

        If ValidCoords(coords) Then
            'finds colour index
            Dim colourIndex As Integer = -1
            For index As Integer = 0 To UBound(_coloursUsed)
                If newColour = _coloursUsed(index) Then
                    colourIndex = index
                End If
            Next

            'colour not already present
            If colourIndex = -1 Then
                _coloursUsed = InsertItem(_coloursUsed, newColour)
                colourIndex = UBound(_coloursUsed)
            End If

            _colourIndices(coords.X, coords.Y) = colourIndex
        End If
    End Sub

    Public Function ValidCoords(coords As Point) As Boolean
        'returns whether provided coords are within range of the grid

        Return Not IsNothing(_colourIndices) AndAlso (coords.X >= 0 And coords.X < Dimensions.Width And coords.Y >= 0 And coords.Y < Dimensions.Height)
    End Function

    Public Property Colours As Color()
        Get
            Return _coloursUsed
        End Get
        Set
            _coloursUsed = Value
            ResetBitmap()
        End Set
    End Property

    Public Property Indices As Integer(,)
        Get
            Return _colourIndices
        End Get
        Set
            _colourIndices = Value
            ResetBitmap()
        End Set
    End Property

#End Region

#Region "Other"

    Public Overrides Function ToString() As String
        'converts this sprite to a tag

        'converts all the colours to names
        Dim colourNames() As String = Nothing
        If Not IsNothing(Colours) Then
            ReDim colourNames(UBound(Colours))
            For index As Integer = 0 To UBound(Colours)
                colourNames(index) = AddQuotes(ColorTranslator.ToHtml(Colours(index)))
            Next
        End If

        Return New Tag(SpriteTagName,
                ArrayToString(
                        {New Tag(ColoursTagName, ArrayToString(colourNames)),
                         New Tag(PixelsTagName, ArrayToString(Indices))})).ToString
    End Function

    Public Property Dimensions As Size
        Get
            'returns the max X and Y of the frame
            If Not IsNothing(_colourIndices) Then
                Return New Size(_colourIndices.GetLength(0), _colourIndices.GetLength(1))
            Else
                Return New Size(0, 0)
            End If
        End Get
        Set
            'resizes the sprite, preserving pixels within the old size of the sprite
            Dim oldIndices(,) As Integer = _colourIndices
            ReDim Indices(Value.Width - 1, Value.Height - 1)

            For index1 As Integer = 0 To Indices.GetUpperBound(0)
                For index2 As Integer = 0 To Indices.GetUpperBound(1)
                    If Not IsNothing(oldIndices) AndAlso index1 <= oldIndices.GetUpperBound(0) AndAlso index2 <= oldIndices.GetUpperBound(1) Then
                        Indices(index1, index2) = oldIndices(index1, index2)
                    Else
                        'if the current pixel is outside the old sprite then fills in with transparent
                        Indices(index1, index2) = 0
                    End If
                Next
            Next
        End Set
    End Property

#End Region

#Region "Operators"

    Public Shared Operator =(sprite1 As Sprite, sprite2 As Sprite) As Boolean
        Return AreSpritesEqual(sprite1, sprite2)
    End Operator

    Public Shared Operator <>(sprite1 As Sprite, sprite2 As Sprite) As Boolean
        Return Not AreSpritesEqual(sprite1, sprite2)
    End Operator

    Private Shared Function AreSpritesEqual(sprite1 As Sprite, sprite2 As Sprite) As Boolean
        'returns whether 2 provided frames are identical

        If IsNothing(sprite1) Or IsNothing(sprite2) Then
            Return IsNothing(sprite2) = IsNothing(sprite2)  'XNOR
        Else
            Return Not (sprite1._coloursUsed IsNot sprite2._coloursUsed OrElse sprite1._colourIndices IsNot sprite2._colourIndices)
        End If
    End Function

#End Region

End Class
