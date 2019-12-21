'Richard Holmes
'20/03/2019
'Sprite Maker

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
        createdSprite = New Sprite(New Size(16, 16))    'default size 16 x 16

        SetUpUsedColoursTable()
        ResetColourOptions()
    End Sub

    Private Sub SetUpUsedColoursTable()
        'fills TblColourSelect with buttons which will be used to allow the user to select previously used colours and swap pages
        
        'adds the buttons for the colours
        Dim btn As Button
        ReDim colourButtons(TblColourSelect.RowCount - 3, TblColourSelect.ColumnCount - 1)
        For row As Integer = 0 To TblColourSelect.RowCount - 3
            For col As Integer = 0 To TblColourSelect.ColumnCount - 1
                btn = New Button With {.Name = "BtnColourX" & row & "Y" & col,
                    .Dock = DockStyle.Fill,
                    .Enabled = False,
                    .Text = "#"}
                AddHandler btn.Click, AddressOf UserSelectColour
                colourButtons(row, col) = btn
                TblColourSelect.Controls.Add(btn, col, row + 1)
            Next
        Next

        'adds the buttons for the change page buttons
        ReDim pageSwapButtons(1)
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
            pageSwapButtons(index) = btn
        Next
    End Sub

#End Region

#Region "Save/Load"

    Dim saveLocation As String
    Dim createdSprite As Sprite

    Const SpriteFileFilter As String = "Sprite file (*.sprt)|*.sprt"

    Private Sub OpenFile(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarFileOpen.Click
        'asks the user to select a sprite file and reads it

        Using openDialog As New OpenFileDialog With {.Filter = SpriteFileFilter, .Multiselect = False}
            If openDialog.ShowDialog = DialogResult.OK Then
                saveLocation = openDialog.FileName
                createdSprite = New Sprite(saveLocation, spriteFolderLocation:="")
                DrawSavedPixels()
                DisplayColourOptions(0)
            End If
        End Using
    End Sub

    Private Sub SaveAs(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarFileSaveAs.Click
        'asks the user to select a save location, then saves the sprite there and enables the regular save button

        Using saveDialog As New SaveFileDialog With {.Filter = SpriteFileFilter}
            If saveDialog.ShowDialog = DialogResult.OK Then
                saveLocation = saveDialog.FileName
                WriteFile(saveLocation, createdSprite.ToString)
            End If
        End Using
    End Sub

    Private Sub SaveFile(sender As ToolStripMenuItem, e As EventArgs) Handles ToolBarFileSave.Click
        If IsNothing(saveLocation) Then
            'if save location not selected then asks user to save as
            SaveAs(Nothing, Nothing)
        Else
            'saves the file to the already selected location
            WriteFile(saveLocation, createdSprite.ToString)
        End If
    End Sub

    Private Sub UserCloseForm(sender As FrmSpriteMaker, e As FormClosingEventArgs) Handles Me.FormClosing
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
        If Not IsNothing(createdSprite) Then
            DrawSavedPixels()
        End If
    End Sub

    Private Sub ResizeSprite(sender As NumericUpDown, e As EventArgs) Handles NumResizeW.ValueChanged, NumResizeH.ValueChanged
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

    Private Sub ChangePixel(coords As Point)
        'changes the pixels at the given coordinates to match the selected colour index
        createdSprite.SetPixelColour(coords, CurrentColour)
        DrawSavedPixels(coords)
        DisplayColourOptions(colourPageNumber)
    End Sub

    Private Sub PnlDraw_MouseClick(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseClick
        'draws the user's selected colour at the clicked area

        Dim clickCoords As Point = CalculateClickCoords(e.Location)
        ChangePixel(clickCoords)
    End Sub

    Private Sub PnlDraw_MouseDown(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseDown
        'records that the user is dragging along the panel

        mouseDragging = True
        dragStartPoint = e.Location
    End Sub

    Private Sub PnlDraw_MouseMove(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseMove
        If mouseDragging Then
            dragEndPoint = e.Location
            ChangePixel(CalculateClickCoords(dragStartPoint))
            ChangePixel(CalculateClickCoords(dragEndPoint))
            dragStartPoint = e.Location
        End If
    End Sub

    Private Sub PnlDraw_MouseUp(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseUp
        'records that the user is no longer dragging along the panel

        If mouseDragging Then
            dragEndPoint = e.Location
            ChangePixel(CalculateClickCoords(dragEndPoint))
            mouseDragging = False
        End If
    End Sub

    Private Function CalculateClickCoords(coords As PointF) As Point
        'returns the x and y index of the pixel the provided click coords corresponds to
        Return New Point(Math.Floor(coords.X / GridScale.Width), Math.Floor(coords.Y / GridScale.Height))
    End Function

#End Region

#Region "Colours"

    Dim colourButtons(,) As Button
    Dim pageSwapButtons() As Button
    Dim colourPageNumber As Integer

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

                'doesn't change the RGB values, because then the colour would be set to black
                NumColourR.Value = value.R
                NumColourG.Value = value.G
                NumColourB.Value = value.B
            End If
        End Set
    End Property

    Private Sub ColourCreatorUpdate(sender As NumericUpDown, e As EventArgs) Handles NumColourR.ValueChanged, NumColourG.ValueChanged, NumColourB.ValueChanged, NumColourR.KeyPress, NumColourG.KeyPress, NumColourB.KeyPress
        'updates the colour being shown to the user in the custom colour preview when the RGB values are edited
        PnlCustomColourDisplay.BackColor = Color.FromArgb(NumColourR.Value, NumColourG.Value, NumColourB.Value)
    End Sub

    Private Sub ResetColourOptions()
        Colours = {Color.Transparent}
    End Sub

    Private Sub DisplayColourOptions(pageNumber As Integer)
        'updates TblColourSelect to show the colours on the provided page number

        Dim rows As Integer = TblColourSelect.RowCount - 2
        Dim cols As Integer = TblColourSelect.ColumnCount

        LblUsedColours.Text = "Used Colours Page " & pageNumber + 1
        colourPageNumber = pageNumber

        'shows the colours of the new page
        For row As Integer = 0 To rows - 1
            For col As Integer = 0 To cols - 1
                Dim colourIndex As Integer = (pageNumber * rows * cols) + row * cols + col
                Dim valid As Boolean = colourIndex >= 0 And colourIndex <= UBound(Colours)  'checks if the colour index is in bounds
                colourButtons(row, col).Enabled = valid
                colourButtons(row, col).Visible = valid

                If valid Then
                    colourButtons(row, col).BackColor = Colours(colourIndex)
                    colourButtons(row, col).Text = colourIndex
                End If
            Next
        Next

        'enables or disables swap page buttons as appropriate
        pageSwapButtons(0).Enabled = pageNumber > 0
        pageSwapButtons(1).Enabled = Math.Floor(UBound(Colours) / 16) > pageNumber
    End Sub

    Private Sub PreviousColourPage()
        DisplayColourOptions(colourPageNumber - 1)
    End Sub

    Private Sub NextColourPage()
        DisplayColourOptions(colourPageNumber + 1)
    End Sub

    Private Sub UserSelectColour(sender As Button, e As EventArgs)
        'updates the selected colour index with the colour which the user clicked on

        CurrentColour = sender.BackColor
    End Sub

#End Region

End Class