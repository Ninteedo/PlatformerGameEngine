'Richard Holmes
'20/30/2019
'Sprite Maker

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmSpriteMaker

    Dim gridScale As Integer = 20
    Dim gridSize As Size = New Size(20, 20)

    Dim delayTimer As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        delayTimer.Stop()

        RedoLayout()
        panelDrawCanvas = New PaintEventArgs(pnlDraw.CreateGraphics, New Rectangle(New Point(0, 0), pnlDraw.Size))
        ReDim colourIndices(gridSize.Width - 1, gridSize.Height - 1)
        'AddColourOption(Color.Transparent, True)
        ResetColourOptions()

        'DrawGridOutline()
        DrawSavedColours()
    End Sub

    Private Sub RedoLayout()
        'moves around and resizes any controls that need it

        pnlDraw.Size = New Size(gridSize.Width * gridScale, gridSize.Height * gridScale)
        tblControls.Location = New Point(pnlDraw.Right + 10, pnlDraw.Top)

        numResizeW.Value = gridSize.Width
        numResizeH.Value = gridSize.Height
        numResizeS.Value = gridScale

        panelDrawCanvas = New PaintEventArgs(pnlDraw.CreateGraphics, New Rectangle(New Point(0, 0), pnlDraw.Size))

        Me.Size = New Size(tblControls.Right + 20, pnlDraw.Bottom + 45)
        If tblControls.Bottom > pnlDraw.Bottom Then
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
			PRE2.WriteFile(saveLocation, SpriteString)

            btnSave.Enabled = True
        End If
        cantDraw = False
    End Sub

    Private Sub btnSave_Click(sender As Button, e As EventArgs) Handles btnSave.Click
        'saves the file to the already selected location

        If IO.File.Exists(saveLocation) Then
			PRE2.WriteFile(saveLocation, SpriteString)
        Else
            PRE2.DisplayError("Couldn't find file at " & saveLocation)
        End If
    End Sub

    Private Sub ReadSpriteFromFile(fileLocation As String)
        If IO.File.Exists(fileLocation) = True Then
            Dim fileText As String = PRE2.ReadFile(fileLocation)

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
				PRE2.DisplayError("Provided file was not a valid sprite file")
            End If
        Else
            PRE2.DisplayError("Couldn't find file " & fileLocation)
        End If
    End Sub

    Private Sub btnRedraw_Click(sender As Object, e As EventArgs) Handles btnRedraw.Click
        'incase the sprite gets cleared from the grid for some reason

        DrawSavedColours()
    End Sub


    Dim selectedColourIndex As Integer = 1
    Dim colourButtons() As Button
    Dim colours() As Color = {Color.Transparent}
    Dim maxColours As Integer = 20
	Dim colourIndices(,) As Integer
	
    Dim panelDrawCanvas As PaintEventArgs 

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
                DrawSquare(New Point(x, y), colourIndices(x, y))
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
            Dim clickCoords As New Point(Math.Floor(e.X / gridScale), Math.Floor(e.Y / gridScale))

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
            DrawSquare(New Point(Math.Floor(dragStartPoint.X / gridScale), Math.Floor(dragStartPoint.Y / gridScale)), selectedColourIndex)
            DrawSquare(New Point(Math.Floor(dragEndPoint.X / gridScale), Math.Floor(dragEndPoint.Y / gridScale)), selectedColourIndex)
            dragStartPoint = e.Location
        End If
    End Sub

    Private Sub pnlDraw_MouseUp(sender As Panel, e As MouseEventArgs) Handles pnlDraw.MouseUp
        'records that the user is no longer dragging along the panel
        dragEndPoint = e.Location
        DrawSquare(New Point(Math.Floor(dragEndPoint.X / gridScale), Math.Floor(dragEndPoint.Y / gridScale)), selectedColourIndex)

        mouseDragging = False
    End Sub

    Private Sub DrawSquare(coords As Point, colourIndex As Integer)
        'draws a square on the grid, colour depends on selected colour index

        If ValidCoords(coords, gridSize) Then
            colourIndices(coords.x, coords.y) = colourIndex

            If colourIndex > 0 Then
                Dim brush As New SolidBrush(colourButtons(colourIndex).BackColor)
                Dim rect As New Rectangle(New Point(coords.x * gridScale, coords.y * gridScale), New Size(gridScale, gridScale))

                panelDrawCanvas.Graphics.FillRectangle(brush, rect)
                colourIndices(coords.x, coords.y) = colourIndex
            ElseIf colourIndex = 0 Then         'colour index 0 is transparent
                DrawTransparentSquare(coords)
            Else
                PRE2.DisplayError("Selected colour index " & Trim(Str(colourIndex)) & " is invalid")
            End If
        End If
    End Sub

    Private Sub DrawTransparentSquare(coords As Point)
        'draws a 2x2 grid of light and dark grey squares to show that the current square is transparent

        Dim brush1 As New SolidBrush(Color.Gray)
        Dim brush2 As New SolidBrush(Color.DimGray)
        Dim rects1() As RectangleF = {New RectangleF(New PointF(coords.X * gridScale, coords.Y * gridScale), New SizeF(gridScale / 2, gridScale / 2)), New RectangleF(New PointF(coords.X * gridScale + gridScale / 2, coords.Y * gridScale + gridScale / 2), New SizeF(gridScale / 2, gridScale / 2))}
        Dim rects2() As RectangleF = {New RectangleF(New PointF(coords.X * gridScale + gridScale / 2, coords.Y * gridScale), New SizeF(gridScale / 2, gridScale / 2)), New RectangleF(New PointF(coords.X * gridScale, coords.Y * gridScale + gridScale / 2), New SizeF(gridScale / 2, gridScale / 2))}

        panelDrawCanvas.Graphics.FillRectangles(brush1, rects1)
        panelDrawCanvas.Graphics.FillRectangles(brush2, rects2)
    End Sub

    Private Sub ColourCreaterUpdate(sender As NumericUpDown, e As EventArgs) Handles numColourR.ValueChanged, numColourG.ValueChanged, numColourB.ValueChanged
        'updates the colour being shown to the user in the custom colour preview when the RGB values are editted

        pnlCustomColourDisplay.BackColor = Color.FromArgb(numColourR.Value, numColourG.Value, numColourB.Value)
    End Sub

    Private Sub ResetColourOptions()
        colours = {Color.Transparent}
        DisplayColourOptions()
    End Sub

    Private Sub DisplayColourOptions()
        'clears tblColourSelect and adds all of the buttons back

        tblColourSelect.Controls.Clear()
        ReDim colourButtons(UBound(colours))

        For index As Integer = 0 To UBound(colours)
            Dim textColour As Color = Color.Black       'default text colour is black
            Const swapTextColourThreshold As Integer = 100
            'text is white if the back colour is dark enough
            If colours(index).R < swapTextColourThreshold And colours(index).G < swapTextColourThreshold And colours(index).B < swapTextColourThreshold Then
                textColour = Color.White
            End If

            'adds a new button corresponding to the colour
            Dim newBtn As New Button With {.Text = Trim(Str(index)), .BackColor = colours(index), .ForeColor = textColour, .Dock = DockStyle.Fill, .FlatStyle = FlatStyle.Flat}
            colourButtons(index) = newBtn
            AddHandler newBtn.Click, AddressOf UserSelectColour
            tblColourSelect.Controls.Add(newBtn)
        Next
    End Sub
	
	Private Sub AddColourOption(newColour As Color)
		If colours.Length < maxColours Then
			Dim colourUnique As Boolean = True

			For index As Integer = 0 To UBound(colourButtons)
				If AreColoursEqual(colourButtons(index).BackColor, newColour) Then
					colourUnique = False
					Exit For
				End If
			Next index
			
			If colourUnique Then
                If IsNothing(colours) Then
                    ReDim colours(0)
                Else
                    ReDim Preserve colours(UBound(colours) + 1)
                End If
				colours(UBound(colours)) = newColour
				
                DisplayColourOptions()
			Else
				PRE2.DisplayError("This colour is already in the colour palette")
			End If
		Else
            PRE2.DisplayError("Cannot add any more colours as limit has been reached")
		End If
	End Sub

    Private Sub SwapColourOption(newColour As Color, colourIndex As Integer)
        'swaps the current custom colour with the currently selected colour

        If colourIndex > 0 Then
            Dim colourNotPreviouslyAdded As Boolean = True
            For index As Integer = 0 To UBound(colourButtons)
                If AreColoursEqual(colourButtons(index).BackColor, newColour) Then
                    colourNotPreviouslyAdded = False
                    PRE2.DisplayError("You have already added that colour to the palette")
                    Exit Sub
                End If
            Next index

            If colourNotPreviouslyAdded Then
                colours(colourIndex) = newColour

                DisplayColourOptions()
                DrawSavedColours()
            End If
        Else
            PRE2.DisplayError("Please select a custom colour to swap out for, index " & selectedColourIndex & " is not viable")
        End If
    End Sub
	
	Private Sub RemoveColourOption(removeIndex As Integer)
		If IsNothing(colours) = False AndAlso removeIndex >= 0 And removeIndex <= UBound(colours) Then
            If UBound(colours) - 1 < 0 Then
                colours = Nothing
            Else
                For index As Integer = removeIndex To UBound(colours)
                    colours(index) = colours(index - 1)
                Next

                ReDim Preserve colours(UBound(colours) - 1)
            End If
		Else
            PRE2.DisplayError("Attempted to remove a colour option at invalid index " & removeIndex)
		End If
	End Sub

    Private Sub UserSelectColour(sender As Button, e As EventArgs)
        'updates the selected colour index with the colour which the user clicked on

        selectedColourIndex = Int(sender.Text)
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
	
	Private Function AreColoursEqual(colour1 As Color, colour2 As Color) As Boolean
		'returns whether the RGB values of 2 colours are equal
		
        If colour1.A = colour2.A And colour1.R = colour2.R And colour1.G = colour2.G And colour1.B = colour2.B Then
            Return True
        Else
            Return False
        End If
	End Function

	Public Function ValidCoords(coords As Point, gridSize As Size) As Boolean
		'returns whether provided coords are within range of the grid

		If coords.x < 0 Or coords.x >= gridSize.Width Or coords.y < 0 Or coords.y >= gridSize.Height Then
			Return False
		Else
			Return True
		End If
	End Function



    Private ReadOnly Property SpriteString
        Get
            'returns the string version of the user's sprite

            Dim createdSprite As PRE2.Sprite = New PRE2.Sprite(colourIndices, colours)
			Return createdSprite.ToString()
        End Get
    End Property
End Class