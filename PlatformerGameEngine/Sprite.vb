Public Class Sprite
    'sprites loaded from .sprt files and used to create frames

    Private pixelColours(,) As Color
    Public fileName As String

    Private bitmapVersion As Image

    Public Sub New(fileLocation As String)
        'creates a new sprite by reading it from a file

        If IO.File.Exists(fileLocation) Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd

            reader.Close()

            If SpriteStringHandler.ValidSpriteText(fileText, fileLocation) Then
                Pixels = SpriteStringHandler.ReadPixelColours(fileText)
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

        ReDim Pixels(colourIndices.GetUpperBound(0), colourIndices.GetUpperBound(1))
        For x As Integer = 0 To colourIndices.GetUpperBound(0)
            For y As Integer = 0 To colourIndices.GetUpperBound(1)
                Pixels(x, y) = colours(colourIndices(x, y))
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

    Public ReadOnly Property Bitmap As Image
        Get
            Return bitmapVersion
        End Get
    End Property

    Public Property Pixels As Color(,)
        Get
            Return pixelColours
        End Get
        Set(value As Color(,))
            pixelColours = value
            bitmapVersion = ToBitmap()
        End Set
    End Property

    Public Function ToBitmap(Optional opacity As Single = 1) As Bitmap
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
