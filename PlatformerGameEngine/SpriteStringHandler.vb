'Richard Holmes
'22/03/2019
'Creates and reads sprite files

Public Module SpriteStringHandler
    'this is used to create and read sprite strings

    Public Function Create(ByVal colourIndices(,) As Integer, ByVal gridSize As Size, ByVal colours() As Color) As String
        'returns the string version of the user's sprite

        Dim result As String = ""

        result += gridSize.Width & "x" & gridSize.Height & vbNewLine        'dimensions

        For colourIndex As Integer = 1 To UBound(colours)
            result += ColorTranslator.ToHtml(colours(colourIndex)) & ","        'list of colours in hex form
        Next colourIndex
        result = result.Remove(Len(result) - 1) & vbNewLine     'removes the last comma

        Dim repeatedColourStreak As Integer = 0
        Dim repeatedColourIndex As Integer = colourIndices(0, 0)
        For y As Integer = 0 To gridSize.Height - 1                 'colour indices of every pixel
            For x As Integer = 0 To gridSize.Width - 1
                If x < gridSize.Width And y < gridSize.Height Then       'runs one more time so if last pixel is repeated it will still be added
                    ProcessColourIndex(colourIndices(x, y), repeatedColourStreak, repeatedColourIndex, result, x = gridSize.Width - 1 And y = gridSize.Height - 1)
                End If
            Next x
        Next y

        result = result.Remove(Len(result) - 1)     'removes the last comma

        Return result
    End Function

    Public Function CreateString(sprite As Sprite) As String
        'returns a string created from a given sprite

        'adds line for size
        Dim sizeLine As String = Trim(Str(sprite.pixels.GetLength(0))) & "x" & Trim(Str(sprite.pixels.GetLength(1)))

        'gets colours
        Dim colours() As Color = {Color.Transparent}

        For Each pixelColour As Color In sprite.pixels
            Dim duplicate As Boolean = False

            For Each colour As Color In colours
                If pixelColour = colour Then
                    duplicate = True
                    Exit For
                End If
            Next colour

            If duplicate = False Then
                ReDim Preserve colours(UBound(colours) + 1)
                colours(UBound(colours)) = pixelColour
            End If
        Next pixelColour


        'adds line for colours
        Dim colourLine As String = ""
        For Each colour As Color In colours
            If colour <> Color.Transparent Then
                colourLine += ColorTranslator.ToHtml(colour) & ","
            End If
        Next
        colourLine = colourLine.Remove(Len(colourLine) - 1)


        'creates two lists, once of colour indices and the other of how many times in a row that index is repeated
        Dim indices() As Integer = Nothing
        Dim repeats() As Integer = Nothing

        For y As Integer = 0 To sprite.pixels.GetUpperBound(1)
            For x As Integer = 0 To sprite.pixels.GetUpperBound(0)
                Dim thisIndex As Integer = Array.IndexOf(colours, sprite.pixels(x, y))

                If IsNothing(indices) = False Then
                    If thisIndex = indices(UBound(indices)) Then
                        repeats(UBound(indices)) += 1
                    Else
                        ReDim Preserve indices(UBound(indices) + 1)
                        ReDim Preserve repeats(UBound(repeats) + 1)
                        indices(UBound(indices)) = thisIndex
                        repeats(UBound(indices)) = 1
                    End If
                Else
                    ReDim indices(0)
                    ReDim repeats(0)
                    indices(0) = thisIndex
                    repeats(0) = 1
                End If
            Next x
        Next y


        'adds a line for colour indices
        Dim colourIndicesLine As String = ""

        For colourIndexIndex As Integer = 0 To UBound(indices)
            If repeats(colourIndexIndex) > 1 Then       'adds the number of repeats to the end of the index if there is repeats
                colourIndicesLine += Trim(Str(indices(colourIndexIndex))) & "*" & Trim(Str(repeats(colourIndexIndex))) & ","
            Else
                colourIndicesLine += Trim(Str(indices(colourIndexIndex))) & ","
            End If
        Next

        colourIndicesLine = colourIndicesLine.Remove(Len(colourIndicesLine) - 1)

        Return sizeLine & Environment.NewLine & colourLine & Environment.NewLine & colourIndicesLine
    End Function

    Public Sub ReadIndices(ByVal spriteString As String, ByRef colourIndices(,) As Integer, ByRef gridSize As Size, ByRef colours() As Color)
        'changes the values of colourIndices, gridSize and colours, by reading the spriteString

        Dim lines() As String = spriteString.Split(Environment.NewLine)
        Dim currentLineValues() As String

        currentLineValues = lines(0).Split("x")     'first line is [Width]x[Height]
        gridSize = New Size(Int(currentLineValues(0)), Int(currentLineValues(1)))

        currentLineValues = lines(1).Split(",")     'second line is colours in hex form
        ReDim Preserve colours(0)           'removes every previous colour except the first (transparent)
        For colourIndex As Integer = 0 To UBound(currentLineValues)
            ReDim Preserve colours(UBound(colours) + 1)    'adds an extra empty element
            colours(UBound(colours)) = ColorTranslator.FromHtml(currentLineValues(colourIndex))
        Next colourIndex

        currentLineValues = lines(2).Split(",")     'third line is the actual colour indices
        ReDim colourIndices(gridSize.Width - 1, gridSize.Height - 1)
        Dim x As Integer = 0
        Dim y As Integer = 0
        For index As Integer = 0 To UBound(currentLineValues)
            Dim temp() As String = currentLineValues(index).Split("*")
            Dim repeats As Integer = 1

            If UBound(temp) >= 1 Then
                repeats = Int(temp(1))
            End If

            For repeat As Integer = 1 To repeats
                colourIndices(x, y) = Int(temp(0))

                x += 1
                If x = gridSize.Width Then      'reached end of the row
                    x = 0
                    y += 1
                End If
            Next repeat
        Next index
    End Sub

    Public Function ReadPixelColours(spriteString As String) As Color(,)
        'returns a 2D array of colours for pixels

        Dim colourIndices(,) As Integer = {}
        Dim gridSize As Size
        Dim colours() As Color = Nothing


        ReadIndices(spriteString, colourIndices, gridSize, colours)

        Dim pixelColours(gridSize.Width - 1, gridSize.Height - 1) As Color

        For x As Integer = 0 To pixelColours.GetUpperBound(0)
            For y As Integer = 0 To pixelColours.GetUpperBound(1)
                pixelColours(x, y) = colours(colourIndices(x, y))
            Next y
        Next x

        Return pixelColours
    End Function

    Private Sub ProcessColourIndex(ByVal colourIndex As Integer, ByRef repeatedColourStreak As Integer, ByRef repeatedColourIndex As Integer, ByRef result As String, ByVal last As Boolean)
        'used to find repeats of the same colour

        If last = True Then
            ProcessColourIndex(colourIndex, repeatedColourStreak, repeatedColourIndex, result, False)       'if this wasn't here then last pixel wouldn't get checked
        End If

        If repeatedColourIndex <> colourIndex Or last = True Then     'adds string for streak ending
            If repeatedColourStreak > 1 Then
                result += repeatedColourIndex & "*" & repeatedColourStreak & ","
            Else            'doesn't bother saying streak if streak is 1
                result += repeatedColourIndex & ","
            End If

            repeatedColourStreak = 1
            repeatedColourIndex = colourIndex
        ElseIf repeatedColourIndex = colourIndex Then       'continue streak
            repeatedColourStreak += 1
        End If
    End Sub

    Public Function ValidSpriteText(spriteString As String, fileLocation As String) As Boolean
        'returns whether the text given is valid for a sprite

        Try
            Dim colourIndices(,) As Integer = {}
            Dim gridSize As Size
            Dim colours() As Color = {}

            ReadIndices(spriteString, colourIndices, gridSize, colours)

            Return True
        Catch ex As Exception
            MsgBox(fileLocation & " is not a valid sprite file")

            Return False
        End Try
    End Function
End Module