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

    Public Sub New(Optional startSize As Size = Nothing)
        coloursUsed = {Color.Transparent}
        colourIndices = Nothing
        fileName = Nothing
        bitmapVersion = Nothing
        Dimensions = startSize
    End Sub

    Public Sub New(colourIndices(,) As Integer, colours() As Color, Optional fileName As String = Nothing)
        'uses colours and colour indices to make a sprite

        Me.colourIndices = colourIndices
        Me.coloursUsed = colours
        Me.fileName = fileName
    End Sub

    Public Sub New(ByVal fileLocation As String, ByVal spriteFolderLocation As String)
        Dim fileText As String = ReadFile(fileLocation)
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
        Dim fileTag As Tag = spriteTag.InterpretArgument(fileTagName)
        Dim coloursTag As Tag = spriteTag.InterpretArgument(coloursTagName)
        Dim pixelsTag As Tag = spriteTag.InterpretArgument(pixelsTagName)

        'gets the file name
        If Not IsNothing(fileTag) Then
            fileName = fileTag.InterpretArgument
        End If

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
                    ReDim coloursUsed(UBound(colourNames))
                    For colourIndex As Integer = 0 To UBound(colourNames)
                        coloursUsed(colourIndex) = ColorTranslator.FromHtml(colourNames(colourIndex))
                    Next

                    'gets pixels
                    If Not IsNothing(pixelsTag) Then
                        Dim colourIndicesTemp As Object = pixelsTag.InterpretArgument       'returns as a jagged array of integer
                        If Not IsNothing(colourIndicesTemp) Then
                            ReDim colourIndices(UBound(colourIndicesTemp), UBound(colourIndicesTemp(0)))
                            For index2 As Integer = 0 To colourIndices.GetUpperBound(1)
                                For index1 As Integer = 0 To colourIndices.GetUpperBound(0)
                                    SetPixelColour(New Point(index1, index2), coloursUsed(colourIndicesTemp(index1)(index2)))
                                Next
                            Next
                        Else
                            DisplayError("Unable to read pixels in sprite: " & fileName)
                        End If
                    Else
                        DisplayError("Unable to find tag for pixels in sprite: " & fileName)
                    End If
                Else
                    DisplayError("Unable to find colours for sprite: " & fileName)
                End If
            Else
                DisplayError("Invalid colour argument: " & coloursTag.argument)
            End If
        Else
            DisplayError("Unable to find tag for colours in sprite: " & fileName)
        End If
    End Sub

#End Region

#Region "Bitmap"

    Public ReadOnly Property Bitmap As Image
        Get
            If IsNothing(bitmapVersion) Then
                bitmapVersion = ToBitmap()
            End If
            Return bitmapVersion
        End Get
    End Property

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

    Private Sub BitmapModified()
        'sets the bitmap to nothing if the colours or colourIndices are changed so the bitmap isnt outdated
        bitmapVersion = Nothing
    End Sub

#End Region

#Region "Pixels"

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

        Return Not IsNothing(colourIndices) AndAlso (coords.X >= 0 And coords.X < Dimensions.Width And coords.Y >= 0 And coords.Y < Dimensions.Height)
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

#End Region

#Region "Other"

    Public Overrides Function ToString() As String
        'converts this sprite to a tag with subtags {file name, colours, colour indices}

        'converts all the colours to names
        Dim colourNames(UBound(Colours)) As String
        For index As Integer = 0 To UBound(Colours)
            colourNames(index) = AddQuotes(ColorTranslator.ToHtml(Colours(index)))
        Next

        Return New Tag(spriteTagName, ArrayToString({
                New Tag(fileTagName, AddQuotes(fileName)),
                New Tag(coloursTagName, ArrayToString(colourNames)),
                New Tag(pixelsTagName, ArrayToString(Indices))
                                            })).ToString
    End Function

    Public Property Dimensions As Size
        Get
            'returns the max X and Y of the frame
            If Not IsNothing(colourIndices) Then
                Return New Size(colourIndices.GetLength(0), colourIndices.GetLength(1))
            Else
                Return New Size(0, 0)
            End If
        End Get
        Set(value As Size)
            'resizes the sprite, preserving pixels within the old size of the sprite
            Dim oldIndices(,) As Integer = colourIndices
            ReDim Indices(value.Width - 1, value.Height - 1)

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

    Public ReadOnly Property Centre As PointF
        'returns the location of the centre of the frame

        Get
            Return New PointF(Dimensions.Width / 2, Dimensions.Height / 2)
        End Get
    End Property

#End Region

#Region "Operators"

    Public Shared Operator =(sprite1 As Sprite, sprite2 As Sprite)
        Return AreSpritesEqual(sprite1, sprite2)
    End Operator

    Public Shared Operator <>(sprite1 As Sprite, sprite2 As Sprite)
        Return Not AreSpritesEqual(sprite1, sprite2)
    End Operator

    Public Shared Function AreSpritesEqual(sprite1 As Sprite, sprite2 As Sprite) As Boolean
        'returns whether 2 provided frames are identical

        If IsNothing(sprite1) Or IsNothing(sprite2) Then
            Return IsNothing(sprite2) = IsNothing(sprite2)
        Else
            Return Not (sprite1.fileName <> sprite2.fileName OrElse sprite1.coloursUsed IsNot sprite2.coloursUsed OrElse sprite1.colourIndices IsNot sprite2.colourIndices)
        End If
    End Function

#End Region

End Class
