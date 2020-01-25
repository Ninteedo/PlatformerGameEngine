'Richard Holmes
'20/03/2019
'Sprite Maker

Imports PlatformerGameEngine.My.Resources

Public Class FrmSpriteMaker

#Region "Initialisation"

    Private Sub FrmMain_Load(sender As FrmSpriteMaker, e As EventArgs) Handles MyBase.Load
        Initialization()
    End Sub

    Private Sub FrmMain_Shown(sender As FrmSpriteMaker, e As EventArgs) Handles MyBase.Shown
        'this is here because drawing only works when the form is open, not when loading

        DrawSavedPixels()
    End Sub

    Private Sub Initialization()
        _createdSprite = New Sprite(New Size(16, 16))    'default size 16 x 16

        SetUpUsedColoursTable()
        ResetColourOptions()
    End Sub

    Private Sub SetUpUsedColoursTable()
        'fills TblColourSelect with buttons which will be used to allow the user to select previously used colours and swap pages

        'adds the buttons for the colours
        Dim btn As Button
        ReDim _colourButtons(TblColourSelect.RowCount - 3, TblColourSelect.ColumnCount - 1)
        For row As Integer = 0 To TblColourSelect.RowCount - 3
            For col As Integer = 0 To TblColourSelect.ColumnCount - 1
                btn = New Button With {.Name = "BtnColourX" & row & "Y" & col,
                    .Dock = DockStyle.Fill,
                    .Enabled = False,
                    .Text = "#"}
                AddHandler btn.Click, AddressOf UserSelectColour
                _colourButtons(row, col) = btn
                TblColourSelect.Controls.Add(btn, col, row + 1)
            Next
        Next

        'adds the buttons for the change page buttons
        ReDim _pageSwapButtons(1)
        For index As Integer = 0 To 1
            btn = New Button With {.Enabled = False, .Dock = DockStyle.Fill}
            If index = 0 Then
                btn = New Button With {.Name = "BtnColourPrevPage", .Text = "<"}
                AddHandler btn.Click, AddressOf PreviousColourPage
                TblColourSelect.Controls.Add(btn, 0, TblColourSelect.RowCount - 1)
            Else
                btn = New Button With {.Name = "BtnColourNextPage", .Text = ">"}
                AddHandler btn.Click, AddressOf NextColourPage

                TblColourSelect.Controls.Add(btn, TblColourSelect.ColumnCount - 1, TblColourSelect.RowCount - 1)
            End If
            _pageSwapButtons(index) = btn
        Next
    End Sub

#End Region

#Region "Save/Load"

    Dim _saveLocation As String
    Dim _createdSprite As Sprite

    Private Sub Open()
        'asks the user for the sprite file location and loads it

        If UnsavedChanges AndAlso MsgBox("Opening a new sprite will lead to loss of unsaved work. Continue?", MsgBoxStyle.OkCancel) <> DialogResult.OK Then
            Exit Sub
        End If

        Using openDialog As New OpenFileDialog With {.Filter = SpriteFileFilter}
            If openDialog.ShowDialog = DialogResult.OK Then
                _saveLocation = openDialog.FileName
                _createdSprite = New Sprite(_saveLocation)
                DrawSavedPixels()
                DisplayColourOptions(0)
            End If
        End Using
    End Sub

    Private Sub SaveAs()
        'asks the user for save location and saves the file

        Using saveDialog As New SaveFileDialog With {.Filter = SpriteFileFilter}
            If saveDialog.ShowDialog = DialogResult.OK Then
                _saveLocation = saveDialog.FileName
                Save()
            End If
        End Using
    End Sub

    Private Sub Save()
        'saves the current sprite to the current save location
        'redirects to SaveAs if no save location currently

        If IsNothing(_saveLocation) Then
            SaveAs()
        Else
            WriteFile(_saveLocation, _createdSprite.ToString)
        End If
    End Sub

    Private Sub ToolBarFileOpen_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarFileOpen.Click
        Open()
    End Sub

    Private Sub ToolBarFileSaveAs_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarFileSaveAs.Click
        SaveAs()
    End Sub

    Private Sub ToolBarFileSave_Click(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarFileSave.Click
        Save()
    End Sub

    Private Sub UserCloseForm(sender As FrmSpriteMaker, e As FormClosingEventArgs) Handles Me.FormClosing
        'displays a warning to the user if they have unsaved work when they close the form

        'if there are unsaved changes then warns the user
        If UnsavedChanges AndAlso MsgBox("There are unsaved changes, do wish to close anyway?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
            e.Cancel = True
        End If
    End Sub

    Private ReadOnly Property UnsavedChanges As Boolean
        Get
            Return IO.File.Exists(_saveLocation) AndAlso ReadFile(_saveLocation) <> _createdSprite.ToString
        End Get
    End Property

#End Region

#Region "Render"

    Private Property GridSize As Size
        Get
            Return _createdSprite.Dimensions
        End Get
        Set
            _createdSprite.Dimensions = Value
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
                        DrawSquare(New Point(x, y), _createdSprite.GetPixelColour(x, y), panelDrawCanvas)
                    Next
                Next
            Else            'only draws the pixel at the given coordinates
                DrawSquare(singleRedrawCoords, _createdSprite.GetPixelColour(singleRedrawCoords.X, singleRedrawCoords.Y), panelDrawCanvas)
            End If
        End Using
    End Sub

    Private Sub DrawSquare(coords As Point, pixelColour As Color, ByRef panelDrawCanvas As PaintEventArgs)
        'draws a square on the grid, colour depends on selected colour index

        If _createdSprite.ValidCoords(coords) Then
            If pixelColour <> Color.Transparent Then    'not transparent
                Using brush As New SolidBrush(pixelColour)
                    Dim rect As New RectangleF(New PointF(coords.X * GridScale.Width, coords.Y * GridScale.Height), GridScale)
                    panelDrawCanvas.Graphics.FillRectangle(brush, rect)
                End Using
            Else
                'draws a 2x2 grid of light and dark grey squares to show that the current square is transparent
                Using brush1 As New SolidBrush(Color.Gray)
                    Using brush2 As New SolidBrush(Color.DimGray)
                        For index As Integer = 0 To 3
                            Dim row As Integer = Math.Floor(index / 2)  'row, either 0 or 1
                            Dim col As Integer = index Mod 2        'column, either 0 or 1
                            Dim thisBrush As SolidBrush = If(row = col, brush1, brush2)     'selects the brush to use for this square
                            panelDrawCanvas.Graphics.FillRectangle(thisBrush,
                                    New RectangleF(New PointF((coords.X + col / 2) * GridScale.Width, (coords.Y + row / 2) * GridScale.Height),
                                    New SizeF(GridScale.Width / 2, GridScale.Height / 2)))
                        Next
                    End Using
                End Using
            End If
        End If
    End Sub

    Private Sub BtnRedraw_Click(sender As Button, e As EventArgs) Handles BtnRedraw.Click
        'for if the sprite gets cleared from the grid for any reason, eg dragging window offscreen
        DrawSavedPixels()
    End Sub

    Private Sub PnlDraw_SizeChanged() Handles PnlDraw.SizeChanged
        'resizing the window does not resize the render on its own
        If Not IsNothing(_createdSprite) Then
            DrawSavedPixels()
        End If
    End Sub

    Private Sub ResizeSprite(sender As NumericUpDown, e As EventArgs) Handles NumResizeW.ValueChanged, NumResizeH.ValueChanged
        'resizes the sprite according to the new width and height inputted by the user

        If Not IsNothing(_createdSprite) Then
            GridSize = New Size(NumResizeW.Value, NumResizeH.Value)
        End If
    End Sub

#End Region

#Region "User Drawing"

    Dim _mouseDragging As Boolean
    Dim _dragStartPoint As Point
    Dim _dragEndPoint As Point

    Private Sub ChangePixel(coords As Point)
        'changes the pixels at the given coordinates to match the selected colour index
        _createdSprite.SetPixelColour(coords, CurrentColour)
        DrawSavedPixels(coords)
        DisplayColourOptions(_colourPageNumber)
    End Sub

    Private Sub PnlDraw_MouseClick(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseClick
        'draws the user's selected colour at the clicked area

        Dim clickCoords As Point = CalculateClickCoords(e.Location)
        ChangePixel(clickCoords)
    End Sub

    Private Sub PnlDraw_MouseDown(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseDown
        'records that the user is dragging along the panel

        _mouseDragging = True
        _dragStartPoint = e.Location
    End Sub

    Private Sub PnlDraw_MouseMove(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseMove
        If _mouseDragging Then
            _dragEndPoint = e.Location
            ChangePixel(CalculateClickCoords(_dragStartPoint))
            ChangePixel(CalculateClickCoords(_dragEndPoint))
            _dragStartPoint = e.Location
        End If
    End Sub

    Private Sub PnlDraw_MouseUp(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseUp
        'records that the user is no longer dragging along the panel

        If _mouseDragging Then
            _dragEndPoint = e.Location
            ChangePixel(CalculateClickCoords(_dragEndPoint))
            _mouseDragging = False
        End If
    End Sub

    Private Function CalculateClickCoords(coords As PointF) As Point
        'returns the x and y index of the pixel the provided click coords corresponds to
        Return New Point(Math.Floor(coords.X / GridScale.Width), Math.Floor(coords.Y / GridScale.Height))
    End Function

#End Region

#Region "Colours"

    Dim _colourButtons(,) As Button
    Dim _pageSwapButtons() As Button
    Dim _colourPageNumber As Integer

    Private Property Colours As Color()
        Get
            Return _createdSprite.Colours
        End Get
        Set
            _createdSprite.Colours = Value
            DisplayColourOptions(_colourPageNumber)
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
        Set
            If Value = Color.Transparent Then
                ChkEraser.Checked = True
            Else
                ChkEraser.Checked = False

                'doesn't change the RGB values, because then the colour would be set to black
                NumColourR.Value = Value.R
                NumColourG.Value = Value.G
                NumColourB.Value = Value.B
            End If
        End Set
    End Property

    Private Sub ColourCreatorUpdate(sender As NumericUpDown, e As EventArgs) Handles NumColourR.ValueChanged, NumColourG.ValueChanged, NumColourB.ValueChanged, NumColourR.KeyPress, NumColourG.KeyPress, NumColourB.KeyPress
        'updates the colour being shown to the user in the custom colour preview when the RGB values are edited
        PnlCustomColourDisplay.BackColor = Color.FromArgb(NumColourR.Value, NumColourG.Value, NumColourB.Value)
    End Sub

    Private Sub ResetColourOptions()
        Colours = {Color.Transparent}
        _colourPageNumber = 0
    End Sub

    Private Sub DisplayColourOptions(pageNumber As Integer)
        'updates TblColourSelect to show the colours on the provided page number

        Dim rows As Integer = TblColourSelect.RowCount - 2
        Dim cols As Integer = TblColourSelect.ColumnCount

        LblUsedColours.Text = "Used Colours Page #" & pageNumber + 1
        _colourPageNumber = pageNumber

        'shows the colours of the new page
        For row As Integer = 0 To rows - 1
            For col As Integer = 0 To cols - 1
                Dim colourIndex As Integer = (pageNumber * rows * cols) + row * cols + col
                Dim valid As Boolean = colourIndex >= 0 And colourIndex <= UBound(Colours)  'checks if the colour index is in bounds
                _colourButtons(row, col).Enabled = valid
                _colourButtons(row, col).Visible = valid

                If valid Then
                    _colourButtons(row, col).BackColor = Colours(colourIndex)
                    _colourButtons(row, col).Text = colourIndex
                End If
            Next
        Next

        'enables or disables swap page buttons as appropriate
        _pageSwapButtons(0).Enabled = pageNumber > 0
        _pageSwapButtons(1).Enabled = Math.Floor(UBound(Colours) / 16) > pageNumber
    End Sub

    Private Sub PreviousColourPage()
        DisplayColourOptions(_colourPageNumber - 1)
    End Sub

    Private Sub NextColourPage()
        DisplayColourOptions(_colourPageNumber + 1)
    End Sub

    Private Sub UserSelectColour(sender As Button, e As EventArgs)
        'updates the selected colour index with the colour which the user clicked on

        CurrentColour = sender.BackColor
    End Sub

#End Region

End Class