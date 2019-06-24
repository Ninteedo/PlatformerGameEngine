'Richard Holmes
'20/30/2019
'Sprite Maker

Public Class FrmSpriteMaker

    Dim gridScale As Integer = 20
    Dim gridSize As Size = New Size(20, 20)

    Dim delayTimer As New Timer With {.Enabled = False, .Interval = 1}

    Private Structure Coordinates
        Dim x As Integer
        Dim y As Integer

        Public Sub New(xCoord As Integer, yCoord As Integer)
            x = xCoord
            y = yCoord
        End Sub

        Public Function Valid(gridSize As Size) As Boolean
            'returns whether these coordinates are within range of the grid

            Dim result As Boolean = True

            If x < 0 Or x >= gridSize.Width Or y < 0 Or y >= gridSize.Height Then
                result = False
            End If

            Return result
        End Function
    End Structure

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        delayTimer.Stop()

        RedoLayout()
        panelDrawCanvas = New PaintEventArgs(pnlDraw.CreateGraphics, New Rectangle(New Point(0, 0), pnlDraw.Size))
        ReDim colourIndices(gridSize.Width - 1, gridSize.Height - 1)
        AddColourOption(Color.Transparent, True)

        'DrawGridOutline()
        DrawSavedColours()
    End Sub

    Private Sub RedoLayout()
        'moves around and resizes any controls that need it

        pnlDraw.Size = New Size(gridSize.Width * gridScale, gridSize.Height * gridScale)
        btnOpen.Location = New Point(pnlDraw.Left + pnlDraw.Width + 5, btnOpen.Top)
        btnSaveAs.Location = New Point(btnOpen.Left + btnOpen.Width + 5, btnSaveAs.Top)
        btnSave.Location = New Point(btnSaveAs.Left + btnSaveAs.Width + 5, btnSave.Top)
        tblColourSelect.Location = New Point(pnlDraw.Left + pnlDraw.Width + 5, btnOpen.Top + btnOpen.Height + 5)
        lstColours.Location = New Point(tblColourSelect.Left + tblColourSelect.Width + 5, btnOpen.Top + btnOpen.Height + 5)

        lblRed.Location = New Point(lstColours.Right + 5, lstColours.Top)
        numColourR.Location = New Point(lblRed.Right + 5, lblRed.Top)
        lblGreen.Location = New Point(lstColours.Right + 5, numColourR.Bottom + 5)
        numColourG.Location = New Point(numColourR.Left, lblGreen.Top)
        lblBlue.Location = New Point(lstColours.Right + 5, numColourG.Bottom + 5)
        numColourB.Location = New Point(numColourG.Left, lblBlue.Top)
        pnlCustomColourDisplay.Location = New Point(numColourR.Right + 5, numColourR.Top)
        btnAddColour.Location = New Point(lblBlue.Left, numColourB.Bottom + 5)
        btnSwapColour.Location = New Point(btnAddColour.Left, btnAddColour.Bottom + 5)

        lblResizeW.Location = New Point(lblRed.Left, btnSwapColour.Bottom + 5)
        numResizeW.Location = New Point(lblResizeW.Right + 5, lblResizeW.Top)
        lblResizeH.Location = New Point(lblResizeW.Left, numResizeW.Bottom + 5)
        numResizeH.Location = New Point(numResizeW.Left, lblResizeH.Top)
        lblResizeS.Location = New Point(lblResizeH.Left, numResizeH.Bottom + 5)
        numResizeS.Location = New Point(numResizeH.Left, lblResizeS.Top)
        btnResize.Location = New Point(lblResizeS.Left, numResizeS.Bottom + 5)
        btnRedraw.Location = New Point(btnResize.Left, btnSave.Top)

        numResizeW.Value = gridSize.Width
        numResizeH.Value = gridSize.Height
        numResizeS.Value = gridScale

        panelDrawCanvas = New PaintEventArgs(pnlDraw.CreateGraphics, New Rectangle(New Point(0, 0), pnlDraw.Size))

        Me.Size = New Size(pnlCustomColourDisplay.Right + 20, pnlDraw.Bottom + 45)
        If btnResize.Bottom > pnlDraw.Bottom Then
            Me.Height = btnResize.Bottom + 45
        End If

        Me.Refresh()
    End Sub


    Dim saveLocation As String

    Private Sub btnOpen_Click(sender As Button, e As EventArgs) Handles btnOpen.Click
        'asks the user to select a .sprt file and reads it

        cantDraw = True
        Dim openDialog As New OpenFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt", .Multiselect = False, .CheckFileExists = True}

        If openDialog.ShowDialog = DialogResult.OK Then
            saveLocation = openDialog.FileName
            ReadSpriteFromFile(saveLocation)

            btnSave.Enabled = True
        End If
        cantDraw = False
    End Sub

    Private Sub btnSaveAs_Click(sender As Button, e As EventArgs) Handles btnSaveAs.Click
        'asks the user to select a save location, then saves the sprite there and enables the regular save button

        cantDraw = True
        Dim saveDialog As New SaveFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt"}

        If saveDialog.ShowDialog = DialogResult.OK Then
            saveLocation = saveDialog.FileName
            SaveTextToFile(SpriteString, saveLocation)

            btnSave.Enabled = True
        End If
        cantDraw = False
    End Sub

    Private Sub btnSave_Click(sender As Button, e As EventArgs) Handles btnSave.Click
        'saves the file to the already selected location

        If IO.File.Exists(saveLocation) Then
            SaveTextToFile(SpriteString, saveLocation)
        Else
            DisplayError("Couldn't find file at " & saveLocation)
        End If
    End Sub

    Private Sub SaveTextToFile(text As String, fileLocation As String)
        Dim writer As New IO.StreamWriter(fileLocation)
        Dim toWrite As String = text

        For Each c As Char In toWrite
            writer.Write(c)
        Next c

        writer.Close()
    End Sub

    Private Sub ReadSpriteFromFile(fileLocation As String)
        If IO.File.Exists(fileLocation) = True Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd

            reader.Close()

            ResetColourOptions()

            Dim colours(0) As Color
            If SpriteStringHandler.ValidSpriteText(fileText, fileLocation) Then
                SpriteStringHandler.ReadIndices(fileText, colourIndices, gridSize, colours)


                For index As Integer = 1 To UBound(colours)
                    If index <= UBound(colourButtons) Then
                        SwapColourOption(colours(index), index)
                    Else
                        AddColourOption(colours(index))
                    End If
                Next index

                RedoLayout()
                Me.Refresh()
                DrawSavedColours()
            Else        'invalid file

            End If
        Else
            DisplayError("Couldn't find file " & fileLocation)
        End If
    End Sub

    Private Sub btnRedraw_Click(sender As Object, e As EventArgs) Handles btnRedraw.Click
        'incase the sprite gets cleared from the grid for some reason

        DrawSavedColours()
    End Sub


    Dim selectedColourIndex As Integer = 1
    Dim colourButtons(0) As Button
    Dim maxColours As Integer = 20
    Dim panelDrawCanvas As PaintEventArgs
    Dim colourIndices(,) As Integer

    Private Sub DrawGridOutline()
        'draws a white outline of where each square can go 
        'unused

        For x As Integer = 0 To gridSize.Width - 1
            For y As Integer = 0 To gridSize.Height - 1
                panelDrawCanvas.Graphics.DrawRectangle(New Pen(Color.White, 1), New Rectangle(New Point(x * gridScale, y * gridScale), New Size(gridScale, gridScale)))
            Next y
        Next x
    End Sub

    Private Sub DrawSavedColours()
        'draws all the colours saved in the colourIndices array

        For x As Integer = 0 To gridSize.Width - 1
            For y As Integer = 0 To gridSize.Height - 1
                DrawSquare(New Coordinates(x, y), colourIndices(x, y))
            Next y
        Next x
    End Sub

    Dim mouseDragging As Boolean = False
    Dim dragStartPoint As Point
    Dim dragEndPoint As Point
    Dim cantDraw As Boolean = False

    Private Sub pnlDraw_MouseClick(sender As Panel, e As MouseEventArgs) Handles pnlDraw.MouseClick
        'draws the user's selected colour at the clicked area

        If cantDraw = False Then
            Dim clickCoords As New Coordinates(Math.Floor(e.X / gridScale), Math.Floor(e.Y / gridScale))

            DrawSquare(clickCoords, selectedColourIndex)
        End If
    End Sub

    Private Sub pnlDraw_MouseDown(sender As Panel, e As MouseEventArgs) Handles pnlDraw.MouseDown
        'records that the user is dragging along the panel

        mouseDragging = True
        dragStartPoint = e.Location
    End Sub

    Private Sub pnlDraw_MouseMove(sender As Panel, e As MouseEventArgs) Handles pnlDraw.MouseMove
        If mouseDragging = True And cantDraw = False Then
            dragEndPoint = e.Location
            DrawSquare(New Coordinates(Math.Floor(dragStartPoint.X / gridScale), Math.Floor(dragStartPoint.Y / gridScale)), selectedColourIndex)
            DrawSquare(New Coordinates(Math.Floor(dragEndPoint.X / gridScale), Math.Floor(dragEndPoint.Y / gridScale)), selectedColourIndex)
            dragStartPoint = e.Location
        End If
    End Sub

    Private Sub pnlDraw_MouseUp(sender As Panel, e As MouseEventArgs) Handles pnlDraw.MouseUp
        'records that the user is no longer dragging along the panel
        dragEndPoint = e.Location
        DrawSquare(New Coordinates(Math.Floor(dragEndPoint.X / gridScale), Math.Floor(dragEndPoint.Y / gridScale)), selectedColourIndex)

        mouseDragging = False
    End Sub

    'Private Sub ProcessDrawDrag()
    '    'finds the unique squares between the start and end point

    '    DrawSquare(New Coordinates(Math.Floor(dragStartPoint.X / gridScale), Math.Floor(dragStartPoint.Y / gridScale)))
    '    DrawSquare(New Coordinates(Math.Floor(dragEndPoint.X / gridScale), Math.Floor(dragEndPoint.Y / gridScale)))
    'End Sub

    Private Sub DrawSquare(coords As Coordinates, colourIndex As Integer)
        'draws a square on the grid, colour depends on selected colour index

        If coords.Valid(gridSize) = True Then
            colourIndices(coords.x, coords.y) = colourIndex

            If colourIndex > 0 Then
                Dim brush As New SolidBrush(colourButtons(colourIndex).BackColor)
                Dim rect As New Rectangle(New Point(coords.x * gridScale, coords.y * gridScale), New Size(gridScale, gridScale))

                panelDrawCanvas.Graphics.FillRectangle(brush, rect)
                colourIndices(coords.x, coords.y) = colourIndex
            ElseIf colourIndex = 0 Then         'colour index 0 is transparent
                DrawTransparentSquare(coords)
            Else
                DisplayError("Selected colour index " & Trim(Str(colourIndex)) & " is invalid")
            End If
        End If
    End Sub

    Private Sub DrawTransparentSquare(coords As Coordinates)
        'draws a 2x2 grid of light and dark grey squares to show that the current square is transparent

        Dim brush1 As New SolidBrush(Color.Gray)
        Dim brush2 As New SolidBrush(Color.DimGray)
        Dim rects1() As Rectangle = {New Rectangle(New Point(coords.x * gridScale, coords.y * gridScale), New Size(gridScale / 2, gridScale / 2)), New Rectangle(New Point(coords.x * gridScale + gridScale / 2, coords.y * gridScale + gridScale / 2), New Size(gridScale / 2, gridScale / 2))}
        Dim rects2() As Rectangle = {New Rectangle(New Point(coords.x * gridScale + gridScale / 2, coords.y * gridScale), New Size(gridScale / 2, gridScale / 2)), New Rectangle(New Point(coords.x * gridScale, coords.y * gridScale + gridScale / 2), New Size(gridScale / 2, gridScale / 2))}

        panelDrawCanvas.Graphics.FillRectangles(brush1, rects1)
        panelDrawCanvas.Graphics.FillRectangles(brush2, rects2)
    End Sub

    Private Sub ColourCreaterUpdate(sender As NumericUpDown, e As EventArgs) Handles numColourR.ValueChanged, numColourG.ValueChanged, numColourB.ValueChanged
        'updates the colour being shown to the user in the custom colour preview when the RGB values are editted

        pnlCustomColourDisplay.BackColor = Color.FromArgb(numColourR.Value, numColourG.Value, numColourB.Value)
    End Sub

    Private Sub ResetColourOptions()
        Dim transparentButton As Button = colourButtons(0)

        tblColourSelect.Controls.Clear()
        ReDim colourButtons(0)
        colourButtons(0) = transparentButton
        tblColourSelect.Controls.Add(colourButtons(0))
    End Sub

    Private Sub AddColourOption(newColour As Color, Optional firstOne As Boolean = False)
        'adds the custom RGB colour created by the user to the palette

        If colourButtons.Length < maxColours Then
            If firstOne = False Then            'wont add an extra array element if this is the first colour being added (transparent)
                Dim colourNotPreviouslyAdded As Boolean = True

                For index As Integer = 0 To UBound(colourButtons)
                    If AreColoursEqual(colourButtons(index).BackColor, newColour) Then
                        colourNotPreviouslyAdded = False
                        Exit For
                    End If
                Next index

                If colourNotPreviouslyAdded = True Then     'checks that the colour isn't already in the palette
                    ReDim Preserve colourButtons(UBound(colourButtons) + 1)
                Else
                    DisplayError("You have already added that colour to the palette")
                    Exit Sub
                End If
            End If

            Dim btn As New Button With {.Text = Trim(Str(UBound(colourButtons))), .BackColor = newColour, .ForeColor = newColour, .Dock = DockStyle.Fill, .FlatStyle = FlatStyle.Flat}
            colourButtons(UBound(colourButtons)) = btn

            AddHandler btn.Click, AddressOf UserSelectColour
            tblColourSelect.Controls.Add(btn)
        Else
            DisplayError("Can't add any more colours")
        End If
    End Sub

    Private Sub SwapColourOption(newColour As Color, colourIndex As Integer)
        'swaps the current custom colour with the currently selected colour

        If colourIndex > 0 Then
            Dim colourNotPreviouslyAdded As Boolean = True
            For index As Integer = 0 To UBound(colourButtons)
                If AreColoursEqual(colourButtons(index).BackColor, newColour) Then
                    colourNotPreviouslyAdded = False
                    DisplayError("You have already added that colour to the palette")
                    Exit Sub
                End If
            Next index

            tblColourSelect.Controls(colourIndex).BackColor = newColour
            tblColourSelect.Controls(colourIndex).ForeColor = newColour
            colourButtons(colourIndex) = tblColourSelect.Controls(colourIndex)
            DrawSavedColours()
        Else
            DisplayError("Please select a custom colour to swap out for, index " & selectedColourIndex & " is not viable")
        End If
    End Sub

    Private Sub UserSelectColour(sender As Button, e As EventArgs)
        'updates the selected colour index with the colour which the user clicked on

        selectedColourIndex = Array.IndexOf(colourButtons, sender)
    End Sub

    Private Sub btnAddColour_Click(sender As Object, e As EventArgs) Handles btnAddColour.Click
        AddColourOption(Color.FromArgb(numColourR.Value, numColourG.Value, numColourB.Value))
    End Sub

    Private Sub btnSwapColour_Click(sender As Object, e As EventArgs) Handles btnSwapColour.Click
        'swaps the current custom colour with the currently selected colour

        SwapColourOption(Color.FromArgb(numColourR.Value, numColourG.Value, numColourB.Value), selectedColourIndex)
    End Sub

    Private Sub btnResize_Click(sender As Object, e As EventArgs) Handles btnResize.Click
        'resizes the draw panel, this will reset the drawn sprite so a warning is given

        cantDraw = True

        If MsgBox("Are you sure you want to resize, this will lead to the loss of unsaved work", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then       'displays a warning to the user
            gridSize = New Size(numResizeW.Value, numResizeH.Value)
            gridScale = numResizeS.Value
            ReDim colourIndices(gridSize.Width - 1, gridSize.Height - 1)

            ResetColourOptions()
            RedoLayout()
            DrawSavedColours()
        End If

        cantDraw = False
    End Sub

    Private Sub DisplayError(message As String)
        'shows an error to the user

        cantDraw = True
        MsgBox(message, MsgBoxStyle.Exclamation)
        cantDraw = False
    End Sub
	
	Private Function AreColoursEqual(colour1 As Color, colour2 As Color) As Boolean
		'returns whether the RGB values of 2 colours are equal
		
        If colour1.A = colour2.A And colour1.R = colour2.R And colour1.G = colour2.G And colour1.B = colour2.B Then
            Return True
        Else
            Return False
        End If
	End Function





    Private ReadOnly Property SpriteString
        Get
            'returns the string version of the user's sprite

            Dim usedColours(UBound(colourButtons)) As Color
            For index As Integer = 0 To UBound(colourButtons)
                usedColours(index) = colourButtons(index).BackColor
            Next index

            Return SpriteStringHandler.Create(colourIndices, gridSize, usedColours)
        End Get
    End Property
End Class