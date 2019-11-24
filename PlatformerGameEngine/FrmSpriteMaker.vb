'Richard Holmes
'20/03/2019
'Sprite Maker

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmSpriteMaker

#Region "Initialisation"

    Dim delayTimer As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        delayTimer.Stop()

        createdSprite = New Sprite
        GridSize = New Size(16, 16)
        RedoLayout()
        'ReDim createdSprite.ColourIndices(gridSize.Width - 1, gridSize.Height - 1)
        'AddColourOption(Color.Transparent, True)
        ResetColourOptions()

        'DrawGridOutline()
        DrawSavedPixels()
    End Sub

    Private Sub RedoLayout()
        'moves around and resizes any controls that need it

        PnlDraw.Size = New Size(gridSize.Width * gridScale, gridSize.Height * gridScale)
        'TblControls.Location = New Point(PnlDraw.Right + 10, PnlDraw.Top)

        NumResizeW.Value = gridSize.Width
        NumResizeH.Value = gridSize.Height
        NumResizeS.Value = gridScale

        'Me.Size = New Size(TblControls.Right + 20, PnlDraw.Bottom + 45)
        'If TblControls.Bottom > PnlDraw.Bottom Then
        '    Me.Height = BtnResize.Bottom + 45
        'End If

        Refresh()
    End Sub

#End Region

#Region "Disposing"

    Protected Overrides Sub OnFormClosed(ByVal e As FormClosedEventArgs)
        delayTimer.Dispose()
        MyBase.OnFormClosed(e)
    End Sub

#End Region

#Region "Save/Load"

    Dim saveLocation As String
    Dim createdSprite As Sprite

    Private Sub BtnOpen_Click(sender As Object, e As EventArgs) Handles BtnOpen.Click
        'asks the user to select a .sprt file and reads it

        'cantDraw = True
        Using openDialog As New OpenFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt", .Multiselect = False}
            If openDialog.ShowDialog = DialogResult.OK Then
                saveLocation = openDialog.FileName
                createdSprite = New Sprite(saveLocation, spriteFolderLocation:="")

                BtnSave.Enabled = True
            End If
            'cantDraw = False
        End Using
    End Sub

    Private Sub BtnSaveAs_Click(sender As Object, e As EventArgs) Handles BtnSaveAs.Click
        'asks the user to select a save location, then saves the sprite there and enables the regular save button

        'cantDraw = True
        Using saveDialog As New SaveFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt"}
            If saveDialog.ShowDialog = DialogResult.OK Then
                saveLocation = saveDialog.FileName
                PRE2.WriteFile(saveLocation, createdSprite.ToString)
                BtnSave.Enabled = True
            End If
            'cantDraw = False
        End Using
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        'saves the file to the already selected location

        PRE2.WriteFile(saveLocation, createdSprite.ToString)
    End Sub

    Private Sub UserCloseForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'displays a warning to the user if they have unsaved work when they close the form

        Dim unsavedChanges As Boolean = False

        If Not IsNothing(saveLocation) AndAlso IO.File.Exists(saveLocation) Then
            Dim savedSpriteString As String = PRE2.ReadFile(saveLocation)

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

    Dim gridScale As Integer = 24
    'Dim gridSize As New Size(16, 16)

    Private Property GridSize As Size
        Get
            Return createdSprite.Dimensions
        End Get
        Set(value As Size)
            createdSprite.Dimensions = value
        End Set
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

        If ValidCoords(coords) Then
            If pixelColour <> Color.Transparent Then    'not transparent
                Dim brush As New SolidBrush(pixelColour)
                Dim rect As New Rectangle(New Point(coords.X * gridScale, coords.Y * gridScale), New Size(gridScale, gridScale))

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
                                                New RectangleF(New PointF((coords.X + col / 2) * gridScale, (coords.Y + row / 2) * gridScale),
                                                New SizeF(gridScale / 2, gridScale / 2)))
                Next

                brush1.Dispose()
                brush2.Dispose()

                'panelDrawCanvas.Graphics.FillRectangles(brush1, rects1)
                'panelDrawCanvas.Graphics.FillRectangles(brush2, rects2)
            End If
        End If
    End Sub

    Private Sub BtnRedraw_Click(sender As Object, e As EventArgs) Handles BtnRedraw.Click
        'incase the sprite gets cleared from the grid for some reason

        DrawSavedPixels()
    End Sub


    Public Function ValidCoords(coords As Point) As Boolean
        'returns whether provided coords are within range of the grid

        Return createdSprite.ValidCoords(coords)
    End Function

    Private Sub BtnResize_Click(sender As Object, e As EventArgs) Handles BtnResize.Click
        'resizes the draw panel, this will reset the drawn sprite so a warning is given

        'cantDraw = True

        If MsgBox("Are you sure you want to resize, this will lead to the loss of unsaved work", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then       'displays a warning to the user
            GridSize = New Size(NumResizeW.Value, NumResizeH.Value)
            gridScale = NumResizeS.Value
            'ReDim createdSprite.ColourIndices(gridSize.Width - 1, gridSize.Height - 1)

            ResetColourOptions()
            RedoLayout()
            DrawSavedPixels()
        End If

        'cantDraw = False
    End Sub

#End Region

#Region "User Drawing"

    Dim mouseDragging As Boolean = False
    Dim dragStartPoint As Point
    Dim dragEndPoint As Point
    'Dim cantDraw As Boolean = False

    Private Sub ChangePixel(ByVal coords As Point)
        'changes the pixels at the given coordinates to match the selected colour index
        createdSprite.SetPixelColour(coords, Colours(selectedColourIndex))
        DrawSavedPixels(coords)
    End Sub

    Private Sub PnlDraw_MouseClick(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseClick
        'draws the user's selected colour at the clicked area

        'If cantDraw = False Then
        Dim clickCoords As New Point(Math.Floor(e.X / gridScale), Math.Floor(e.Y / gridScale))

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
            ChangePixel(New Point(Math.Floor(dragStartPoint.X / gridScale), Math.Floor(dragStartPoint.Y / gridScale)))
            ChangePixel(New Point(Math.Floor(dragEndPoint.X / gridScale), Math.Floor(dragEndPoint.Y / gridScale)))
            dragStartPoint = e.Location
        End If
    End Sub

    Private Sub PnlDraw_MouseUp(sender As Panel, e As MouseEventArgs) Handles PnlDraw.MouseUp
        'records that the user is no longer dragging along the panel
        If mouseDragging Then
            dragEndPoint = e.Location
            ChangePixel(New Point(Math.Floor(dragEndPoint.X / gridScale), Math.Floor(dragEndPoint.Y / gridScale)))

            mouseDragging = False
        End If
    End Sub

#End Region

#Region "Colours"

    Dim selectedColourIndex As Integer = 0
    Dim colourButtons() As Button
    'Dim coloursUsed() As Color
    Const maxColours As Integer = 20
    'Dim colourIndices(,) As Integer

    Private Property Colours As Color()
        Get
            Return createdSprite.Colours
        End Get
        Set(value As Color())
            createdSprite.Colours = value
            DisplayColourOptions()
        End Set
    End Property

    Private Sub ColourCreaterUpdate(sender As NumericUpDown, e As EventArgs) Handles NumColourR.ValueChanged, NumColourG.ValueChanged, NumColourB.ValueChanged, NumColourR.KeyPress, NumColourG.KeyPress, NumColourB.KeyPress
        'updates the colour being shown to the user in the custom colour preview when the RGB values are editted

        PnlCustomColourDisplay.BackColor = Color.FromArgb(NumColourR.Value, NumColourG.Value, NumColourB.Value)
    End Sub

    Private Sub ResetColourOptions()
        Colours = {Color.Transparent}
    End Sub

    Private Sub DisplayColourOptions()
        'clears tblColourSelect and adds all of the buttons back

        TblColourSelect.Controls.Clear()
        ReDim colourButtons(UBound(Colours))

        For index As Integer = 0 To UBound(Colours)
            Dim textColour As Color = Color.Black       'default text colour is black

            'text is made white if the back colour is dark enough
            Const swapTextColourThreshold As Integer = 100
            Dim temp As Integer = Colours(index).R
            temp += Colours(index).G
            temp += Colours(index).B
            If temp < (3 * swapTextColourThreshold) Then
                textColour = Color.White
            End If

            'adds a new button corresponding to the colour
            Dim newBtn As New Button With {.Text = Trim(Str(index)), .BackColor = Colours(index), .ForeColor = textColour, .Dock = DockStyle.Fill, .FlatStyle = FlatStyle.Flat}
            colourButtons(index) = newBtn
            AddHandler newBtn.Click, AddressOf UserSelectColour
            TblColourSelect.Controls.Add(newBtn)
        Next
    End Sub

    Private Sub AddColourOption(newColour As Color)
        If Colours.Length < maxColours Then     'cant add any more colours if limit reached
            'only adds colour if it hasn't already been added
            For index As Integer = 0 To UBound(Colours)
                If Colours(index) = newColour Then
                    PRE2.DisplayError("This colour is already in the colour palette")
                    Exit For
                End If
            Next index

            Colours = InsertItem(Colours, newColour)
        Else
            PRE2.DisplayError("Cannot add any more colours as limit has been reached")
        End If
    End Sub

    Private Sub SwapColourOption(newColour As Color, colourIndex As Integer)
        'swaps the current custom colour with the currently selected colour

        If colourIndex > 0 Then
            For index As Integer = 0 To UBound(colourButtons)
                If Colours(index) = newColour Then
                    PRE2.DisplayError("You have already added that colour to the palette")
                    Exit Sub
                End If
            Next index

            Colours(colourIndex) = newColour
            DrawSavedPixels()
        Else
            PRE2.DisplayError("Please select a custom colour to swap out for, index " & selectedColourIndex & " is not viable")
        End If
    End Sub

    Private Sub RemoveColourOption(removeIndex As Integer)
        Colours = RemoveItem(createdSprite.Colours, removeIndex)
    End Sub

    Private Sub UserSelectColour(sender As Button, e As EventArgs)
        'updates the selected colour index with the colour which the user clicked on

        selectedColourIndex = Int(sender.Text)
    End Sub

    Private Sub BtnAddColour_Click(sender As Object, e As EventArgs) Handles BtnAddColour.Click
        AddColourOption(Color.FromArgb(NumColourR.Value, NumColourG.Value, NumColourB.Value))
    End Sub

    Private Sub BtnSwapColour_Click(sender As Object, e As EventArgs) Handles BtnSwapColour.Click
        'swaps the current custom colour with the currently selected colour

        SwapColourOption(Color.FromArgb(NumColourR.Value, NumColourG.Value, NumColourB.Value), selectedColourIndex)
    End Sub

    Private Function AreColoursEqual(colour1 As Color, colour2 As Color) As Boolean
        'returns whether the RGB values of 2 colours are equal

        Return colour1.A = colour2.A And colour1.R = colour2.R And colour1.G = colour2.G And colour1.B = colour2.B
    End Function

#End Region

End Class