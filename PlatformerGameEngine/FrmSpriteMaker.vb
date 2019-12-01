'Richard Holmes
'20/03/2019
'Sprite Maker

Public Class FrmSpriteMaker

#Region "Initialisation"

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initialisation()
    End Sub

    Private Sub FrmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'this is here because drawing only works when the form is open, not when loading

        'SetUpUsedColoursTable()
        DrawSavedPixels()
    End Sub

    Private Sub Initialisation()
        createdSprite = New Sprite
        GridSize = New Size(16, 16)
        RedoLayout()

        SetUpUsedColoursTable()
        ResetColourOptions()
    End Sub

    Private Sub RedoLayout()
        'moves around and resizes any controls that need it

        'PnlDraw.Size = New Size(gridSize.Width * gridScale, gridSize.Height * gridScale)
        'TblControls.Location = New Point(PnlDraw.Right + 10, PnlDraw.Top)

        'NumResizeW.Value = gridSize.Width
        'NumResizeH.Value = gridSize.Height
        'NumResizeS.Value = gridScale

        'Me.Size = New Size(TblControls.Right + 20, PnlDraw.Bottom + 45)
        'If TblControls.Bottom > PnlDraw.Bottom Then
        '    Me.Height = BtnResize.Bottom + 45
        'End If

        Refresh()
    End Sub

    Private Sub SetUpUsedColoursTable()
        'fills TblColourSelect with buttons which will be used to allow the user to select previously used colours and swap pages

        'adds the buttons for the colours
        ReDim colourButtons(TblColourSelect.RowCount - 3, TblColourSelect.ColumnCount - 1)
        For row As Integer = 0 To TblColourSelect.RowCount - 3
            For col As Integer = 0 To TblColourSelect.ColumnCount - 1
                Dim btn As New Button With {.Name = $"BtnColourX{row}Y{col}",
                    .Dock = DockStyle.Fill,
                    .Enabled = False,
                    .Text = "#"}
                AddHandler btn.Click, AddressOf UserSelectColour
                colourButtons(row, col) = btn
                'Controls.Add(btn)
                TblColourSelect.Controls.Add(btn, col, row + 1)
            Next
        Next

        'adds the buttons for the change page buttons
        ReDim pageSwapButtons(1)
        For index As Integer = 0 To 1
            Dim btn As Button
            'Controls.Add(btn)
            If index = 0 Then
                btn = New Button With {.Name = "BtnColourPrevPage",
            .Enabled = False, .Dock = DockStyle.Fill, .Text = "<"}
                AddHandler btn.Click, AddressOf PreviousColourPage
                TblColourSelect.Controls.Add(btn, 0, TblColourSelect.RowCount - 1)
            Else
                btn = New Button With {.Name = "BtnColourNextPage",
            .Enabled = False, .Dock = DockStyle.Fill, .Text = ">"}
                AddHandler btn.Click, AddressOf NextColourPage

                TblColourSelect.Controls.Add(btn, TblColourSelect.ColumnCount - 1, TblColourSelect.RowCount - 1)
            End If
            pageSwapButtons(index) = btn
        Next
    End Sub

#End Region

#Region "Disposing"

    'Protected Overrides Sub OnFormClosed(ByVal e As FormClosedEventArgs)
    '    MyBase.OnFormClosed(e)
    'End Sub

#End Region

#Region "Save/Load"

    Dim saveLocation As String
    Dim createdSprite As Sprite

    Private Sub OpenFile(sender As Object, e As EventArgs) Handles ToolBarFileOpen.Click
        'asks the user to select a .sprt file and reads it

        Using openDialog As New OpenFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt", .Multiselect = False}
            If openDialog.ShowDialog = DialogResult.OK Then
                saveLocation = openDialog.FileName
                createdSprite = New Sprite(saveLocation, spriteFolderLocation:="")
                DrawSavedPixels()
                DisplayColourOptions(0)
            End If
        End Using
    End Sub

    Private Sub SaveAs(sender As Object, e As EventArgs) Handles ToolBarFileSaveAs.Click
        'asks the user to select a save location, then saves the sprite there and enables the regular save button

        'cantDraw = True
        Using saveDialog As New SaveFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt"}
            If saveDialog.ShowDialog = DialogResult.OK Then
                saveLocation = saveDialog.FileName
                WriteFile(saveLocation, createdSprite.ToString)
                'BtnSave.Enabled = True
            End If
            'cantDraw = False
        End Using
    End Sub

    Private Sub SaveFile(sender As Object, e As EventArgs) Handles ToolBarFileSave.Click
        If IsNothing(saveLocation) Then
            'if save location not selected then asks user to save as
            SaveAs(Nothing, Nothing)
        Else
            'saves the file to the already selected location
            WriteFile(saveLocation, createdSprite.ToString)
        End If
    End Sub

    Private Sub UserCloseForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'displays a warning to the user if they have unsaved work when they close the form

        Dim unsavedChanges As Boolean = False

        If Not IsNothing(saveLocation) AndAlso IO.File.Exists(saveLocation) Then
            Dim savedSpriteString As String = ReadFile(saveLocation)

            If savedSpriteString <> createdSprite.ToString Then
                unsavedChanges = True
            End If
        End If

        'if there are unsaved changes then warns the user
        If unsavedChanges Then
            If MsgBox("There are unsaved changes, do wish to close anyway?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Render"

    'Dim gridScale As Integer = 24
    'Dim gridSize As New Size(16, 16)

    Private Property GridSize As Size
        Get
            Return createdSprite.Dimensions
        End Get
        Set(value As Size)
            createdSprite.Dimensions = value
            DrawSavedPixels()
        End Set
    End Property

    Private ReadOnly Property GridScale As SizeF
        'the scaling between the sprite resolution and the panel size
        Get
            Return New SizeF(PnlDraw.Width / GridSize.Width, PnlDraw.Height / GridSize.Height)
        End Get
    End Property

    Private Sub DrawSavedPixels(Optional singleRedrawCoords As Point = Nothing)
        'draws all the colours saved in the sprite
        Using panelDrawCanvas As New PaintEventArgs(PnlDraw.CreateGraphics, New Rectangle(New Point(0, 0), PnlDraw.Size))
            If singleRedrawCoords.IsEmpty Then       'draws everything
                For x As Integer = 0 To GridSize.Width - 1
                    For y As Integer = 0 To GridSize.Height - 1
                        DrawSquare(New Point(x, y), createdSprite.GetPixelColour(x, y), panelDrawCanvas)
                    Next
                Next
            Else            'only draws the pixel at the given coordinates
                DrawSquare(singleRedrawCoords, createdSprite.GetPixelColour(singleRedrawCoords.X, singleRedrawCoords.Y), panelDrawCanvas)
            End If
        End Using
    End Sub

    Private Sub DrawSquare(coords As Point, pixelColour As Color, ByRef panelDrawCanvas As PaintEventArgs)
        'draws a square on the grid, colour depends on selected colour index

        If createdSprite.ValidCoords(coords) Then
            If pixelColour <> Color.Transparent Then    'not transparent
                Dim brush As New SolidBrush(pixelColour)
                Dim rect As New RectangleF(New PointF(coords.X * GridScale.Width, coords.Y * GridScale.Height), GridScale)

                panelDrawCanvas.Graphics.FillRectangle(brush, rect)
            Else
                'draws a 2x2 grid of light and dark grey squares to show that the current square is transparent
                Dim brush1 As New SolidBrush(Color.Gray)
                Dim brush2 As New SolidBrush(Color.DimGray)
                'Dim rects1() As RectangleF = {New RectangleF(New PointF(coords.X * gridScale, coords.Y * gridScale), New SizeF(gridScale / 2, gridScale / 2)),
                '    New RectangleF(New PointF(coords.X * gridScale + gridScale / 2, coords.Y * gridScale + gridScale / 2), New SizeF(gridScale / 2, gridScale / 2))}
                'Dim rects2() As RectangleF = {New RectangleF(New PointF(coords.X * gridScale + gridScale / 2, coords.Y * gridScale), New SizeF(gridScale / 2, gridScale / 2)),
                '    New RectangleF(New PointF(coords.X * gridScale, coords.Y * gridScale + gridScale / 2), New SizeF(gridScale / 2, gridScale / 2))}

                For index As Integer = 0 To 3
                    Dim row As Integer = Math.Floor(index / 2)  'row, either 0 or 1
                    Dim col As Integer = index Mod 2        'column, either 0 or 1
                    Dim thisBrush As SolidBrush = If(row = col, brush1, brush2)     'selects the brush to use for this square
                    panelDrawCanvas.Graphics.FillRectangle(thisBrush,
                        New RectangleF(New PointF((coords.X + col / 2) * GridScale.Width, (coords.Y + row / 2) * GridScale.Height),
                        New SizeF(GridScale.Width / 2, GridScale.Height / 2)))
                Next

                brush1.Dispose()
                brush2.Dispose()

                'panelDrawCanvas.Graphics.FillRectangles(brush1, rects1)
                'panelDrawCanvas.Graphics.FillRectangles(brush2, rects2)
            End If
        End If
    End Sub

    Private Sub BtnRedraw_Click(sender As Object, e As EventArgs) Handles BtnRedraw.Click
        'incase the sprite gets cleared from the grid for any reason, eg dragging window offscreen
        DrawSavedPixels()
    End Sub

    Private Sub PnlDraw_SizeChanged() Handles PnlDraw.SizeChanged
        'resizing the window does not resize the render on its own
        If Not IsNothing(createdSprite) Then
            DrawSavedPixels()
        End If
    End Sub

    Private Sub ResizeSprite(sender As Object, e As EventArgs) Handles NumResizeW.ValueChanged, NumResizeH.ValueChanged
        'resizes the sprite according to the new width and height inputted by the user

        If Not IsNothing(createdSprite) Then
            GridSize = New Size(NumResizeW.Value, NumResizeH.Value)
        End If
    End Sub

#End Region

#Region "User Drawing"

    Dim mouseDragging As Boolean = False
    Dim dragStartPoint As Point
    Dim dragEndPoint As Point
    'Dim cantDraw As Boolean = False

    Private Sub ChangePixel(ByVal coords As Point)
        'changes the pixels at the given coordinates to match the selected colour index
        createdSprite.SetPixelColour(coords, CurrentColour)
        DrawSavedPixels(coords)
        DisplayColourOptions(colourPageNumber)
    End Sub

    Private Sub PnlDraw_MouseClick(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseClick
        'draws the user's selected colour at the clicked area

        'If cantDraw = False Then
        Dim clickCoords As New Point(Math.Floor(e.X / GridScale.Width), Math.Floor(e.Y / GridScale.Height))

        ChangePixel(clickCoords)
        'End If
    End Sub

    Private Sub PnlDraw_MouseDown(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseDown
        'records that the user is dragging along the panel

        mouseDragging = True
        dragStartPoint = e.Location
    End Sub

    Private Sub PnlDraw_MouseMove(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseMove
        If mouseDragging = True Then
            dragEndPoint = e.Location
            ChangePixel(New Point(Math.Floor(dragStartPoint.X / GridScale.Width), Math.Floor(dragStartPoint.Y / GridScale.Height)))
            ChangePixel(New Point(Math.Floor(dragEndPoint.X / GridScale.Width), Math.Floor(dragEndPoint.Y / GridScale.Height)))
            dragStartPoint = e.Location
        End If
    End Sub

    Private Sub PnlDraw_MouseUp(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseUp
        'records that the user is no longer dragging along the panel

        If mouseDragging Then
            dragEndPoint = e.Location
            ChangePixel(New Point(Math.Floor(dragEndPoint.X / GridScale.Width), Math.Floor(dragEndPoint.Y / GridScale.Height)))

            mouseDragging = False
        End If
    End Sub

#End Region

#Region "Colours"

    'Dim selectedColourIndex As Integer = 0
    Dim colourButtons(,) As Button
    Dim pageSwapButtons() As Button
    Dim colourPageNumber As Integer
    'Dim colourTransparent As Boolean
    'Dim coloursUsed() As Color
    'Const maxColours As Integer = 20
    'Dim colourIndices(,) As Integer

    Private Property Colours As Color()
        Get
            Return createdSprite.Colours
        End Get
        Set(value As Color())
            createdSprite.Colours = value
            DisplayColourOptions(colourPageNumber)
        End Set
    End Property

    Private Property CurrentColour As Color
        Get
            If ChkEraser.Checked Then
                Return Color.Transparent
            Else
                Return Color.FromArgb(NumColourR.Value, NumColourG.Value, NumColourB.Value)
            End If
        End Get
        Set(value As Color)
            If value = Color.Transparent Then
                ChkEraser.Checked = True
            Else
                ChkEraser.Checked = False
                NumColourR.Value = value.R
                NumColourG.Value = value.G
                NumColourB.Value = value.B
            End If
        End Set
    End Property



    Private Sub ColourCreaterUpdate(sender As NumericUpDown, e As EventArgs) Handles NumColourR.ValueChanged, NumColourG.ValueChanged, NumColourB.ValueChanged, NumColourR.KeyPress, NumColourG.KeyPress, NumColourB.KeyPress
        'updates the colour being shown to the user in the custom colour preview when the RGB values are editted

        PnlCustomColourDisplay.BackColor = Color.FromArgb(NumColourR.Value, NumColourG.Value, NumColourB.Value)
    End Sub

    Private Sub ResetColourOptions()
        Colours = {Color.Transparent}
    End Sub

    'Private Sub DisplayColourOptions()
    '    'clears TblColourSelect and adds all of the buttons back

    '    TblColourSelect.Controls.Clear()
    '    ReDim colourButtons(UBound(Colours))

    '    For index As Integer = 0 To UBound(Colours)
    '        Dim textColour As Color = Color.Black       'default text colour is black

    '        'text is made white if the back colour is dark enough
    '        If Colours(index).R + Colours(index).G + Colours(index).B < 300 Then
    '            textColour = Color.White
    '        End If

    '        'adds a new button corresponding to the colour
    '        Dim newBtn As New Button With {.Text = Trim(Str(index)), .BackColor = Colours(index), .ForeColor = textColour, .Dock = DockStyle.Fill, .FlatStyle = FlatStyle.Flat}
    '        colourButtons(index) = newBtn
    '        AddHandler newBtn.Click, AddressOf UserSelectColour
    '        TblColourSelect.Controls.Add(newBtn)
    '    Next
    'End Sub

    Private Sub DisplayColourOptions(pageNumber As Integer)
        'updates TblColourSelect to show the colours on the provided page number

        Dim rows As Integer = TblColourSelect.RowCount - 2
        Dim cols As Integer = TblColourSelect.ColumnCount

        LblUsedColours.Text = $"Used Colours Page {pageNumber + 1}"
        colourPageNumber = pageNumber

        'shows the colours of the new page
        For row As Integer = 0 To rows - 1
            For col As Integer = 0 To cols - 1
                Dim colourIndex As Integer = (pageNumber * rows * cols) + row * cols + col
                If colourIndex <= UBound(Colours) Then
                    colourButtons(row, col).BackColor = Colours(colourIndex)
                    colourButtons(row, col).Enabled = True
                    colourButtons(row, col).Visible = True
                    colourButtons(row, col).Text = colourIndex
                Else
                    'no colour to display for this button
                    colourButtons(row, col).Enabled = False
                    colourButtons(row, col).Visible = False
                End If

            Next
        Next

        'enables or disables swap page buttons as appropriate
        If pageNumber > 1 Then
            pageSwapButtons(0).Enabled = True
        End If
        If Math.Ceiling(UBound(Colours) / 16) > pageNumber Then
            pageSwapButtons(1).Enabled = True
        End If
    End Sub

    Private Sub PreviousColourPage()
        DisplayColourOptions(colourPageNumber - 1)
    End Sub

    Private Sub NextColourPage()
        DisplayColourOptions(colourPageNumber + 1)
    End Sub

    'Private Sub AddColourOption(newColour As Color)

    '    'only adds colour if it hasn't already been added
    '    For index As Integer = 0 To UBound(Colours)
    '        If Colours(index) = newColour Then
    '            DisplayError("This colour is already in the colour palette")
    '            Exit Sub
    '        End If
    '    Next index

    '    Colours = InsertItem(Colours, newColour)
    'End Sub

    'Private Sub SwapColourOption(newColour As Color, colourIndex As Integer)
    '    'swaps the current custom colour with the currently selected colour

    '    If colourIndex > 0 Then
    '        For index As Integer = 0 To UBound(colourButtons)
    '            If Colours(index) = newColour Then
    '                DisplayError("You have already added that colour to the palette")
    '                Exit Sub
    '            End If
    '        Next index

    '        Colours(colourIndex) = newColour
    '        DrawSavedPixels()
    '    Else
    '        'DisplayError("Please select a custom colour to swap out for, index " & selectedColourIndex & " is not viable")
    '    End If
    'End Sub

    'Private Sub RemoveColourOption(removeIndex As Integer)
    '    Colours = RemoveItem(createdSprite.Colours, removeIndex)
    'End Sub

    Private Sub UserSelectColour(sender As Button, e As EventArgs)
        'updates the selected colour index with the colour which the user clicked on

        CurrentColour = sender.BackColor
    End Sub

    'Private Sub BtnAddColour_Click(sender As Object, e As EventArgs)
    '    AddColourOption(Color.FromArgb(NumColourR.Value, NumColourG.Value, NumColourB.Value))
    'End Sub

    'Private Sub BtnSwapColour_Click(sender As Object, e As EventArgs)
    '    'swaps the current custom colour with the currently selected colour

    '    'SwapColourOption(Color.FromArgb(NumColourR.Value, NumColourG.Value, NumColourB.Value), selectedColourIndex)
    'End Sub

    Private Function AreColoursEqual(colour1 As Color, colour2 As Color) As Boolean
        'returns whether the RGB values of 2 colours are equal

        Return colour1.A = colour2.A And colour1.R = colour2.R And colour1.G = colour2.G And colour1.B = colour2.B
    End Function

    Private Sub ColourCreaterUpdate(sender As Object, e As EventArgs) Handles NumColourR.ValueChanged, NumColourR.KeyPress, NumColourG.ValueChanged, NumColourG.KeyPress, NumColourB.ValueChanged, NumColourB.KeyPress

    End Sub

    Private Sub ColourCreaterUpdate(sender As Object, e As KeyPressEventArgs)

    End Sub

#End Region

End Class