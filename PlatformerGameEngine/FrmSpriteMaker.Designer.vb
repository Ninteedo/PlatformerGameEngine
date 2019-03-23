<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSpriteMaker
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlDraw = New System.Windows.Forms.Panel()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnSaveAs = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.tblColourSelect = New System.Windows.Forms.TableLayoutPanel()
        Me.lstColours = New System.Windows.Forms.ListBox()
        Me.numColourR = New System.Windows.Forms.NumericUpDown()
        Me.lblRed = New System.Windows.Forms.Label()
        Me.lblGreen = New System.Windows.Forms.Label()
        Me.numColourG = New System.Windows.Forms.NumericUpDown()
        Me.lblBlue = New System.Windows.Forms.Label()
        Me.numColourB = New System.Windows.Forms.NumericUpDown()
        Me.pnlCustomColourDisplay = New System.Windows.Forms.Panel()
        Me.btnAddColour = New System.Windows.Forms.Button()
        Me.btnSwapColour = New System.Windows.Forms.Button()
        Me.lblResizeW = New System.Windows.Forms.Label()
        Me.numResizeW = New System.Windows.Forms.NumericUpDown()
        Me.numResizeH = New System.Windows.Forms.NumericUpDown()
        Me.lblResizeH = New System.Windows.Forms.Label()
        Me.numResizeS = New System.Windows.Forms.NumericUpDown()
        Me.lblResizeS = New System.Windows.Forms.Label()
        Me.btnResize = New System.Windows.Forms.Button()
        Me.btnRedraw = New System.Windows.Forms.Button()
        CType(Me.numColourR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numColourG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numColourB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numResizeW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numResizeH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numResizeS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlDraw
        '
        Me.pnlDraw.BackColor = System.Drawing.Color.Black
        Me.pnlDraw.Location = New System.Drawing.Point(13, 13)
        Me.pnlDraw.Name = "pnlDraw"
        Me.pnlDraw.Size = New System.Drawing.Size(400, 400)
        Me.pnlDraw.TabIndex = 0
        '
        'btnOpen
        '
        Me.btnOpen.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpen.ForeColor = System.Drawing.Color.Yellow
        Me.btnOpen.Location = New System.Drawing.Point(420, 13)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(93, 67)
        Me.btnOpen.TabIndex = 1
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = False
        '
        'btnSaveAs
        '
        Me.btnSaveAs.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveAs.ForeColor = System.Drawing.Color.Yellow
        Me.btnSaveAs.Location = New System.Drawing.Point(519, 13)
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(93, 67)
        Me.btnSaveAs.TabIndex = 2
        Me.btnSaveAs.Text = "Save As..."
        Me.btnSaveAs.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSave.Enabled = False
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.Yellow
        Me.btnSave.Location = New System.Drawing.Point(618, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(93, 67)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'tblColourSelect
        '
        Me.tblColourSelect.ColumnCount = 4
        Me.tblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblColourSelect.Location = New System.Drawing.Point(420, 86)
        Me.tblColourSelect.Name = "tblColourSelect"
        Me.tblColourSelect.RowCount = 5
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tblColourSelect.Size = New System.Drawing.Size(280, 350)
        Me.tblColourSelect.TabIndex = 4
        '
        'lstColours
        '
        Me.lstColours.BackColor = System.Drawing.Color.DarkGray
        Me.lstColours.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstColours.FormattingEnabled = True
        Me.lstColours.ItemHeight = 20
        Me.lstColours.Location = New System.Drawing.Point(637, 93)
        Me.lstColours.Name = "lstColours"
        Me.lstColours.Size = New System.Drawing.Size(120, 342)
        Me.lstColours.TabIndex = 5
        '
        'numColourR
        '
        Me.numColourR.Location = New System.Drawing.Point(794, 93)
        Me.numColourR.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numColourR.Name = "numColourR"
        Me.numColourR.Size = New System.Drawing.Size(67, 26)
        Me.numColourR.TabIndex = 4
        '
        'lblRed
        '
        Me.lblRed.AutoSize = True
        Me.lblRed.Location = New System.Drawing.Point(764, 93)
        Me.lblRed.Name = "lblRed"
        Me.lblRed.Size = New System.Drawing.Size(21, 20)
        Me.lblRed.TabIndex = 0
        Me.lblRed.Text = "R"
        '
        'lblGreen
        '
        Me.lblGreen.AutoSize = True
        Me.lblGreen.Location = New System.Drawing.Point(764, 125)
        Me.lblGreen.Name = "lblGreen"
        Me.lblGreen.Size = New System.Drawing.Size(22, 20)
        Me.lblGreen.TabIndex = 0
        Me.lblGreen.Text = "G"
        '
        'numColourG
        '
        Me.numColourG.Location = New System.Drawing.Point(794, 125)
        Me.numColourG.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numColourG.Name = "numColourG"
        Me.numColourG.Size = New System.Drawing.Size(67, 26)
        Me.numColourG.TabIndex = 5
        '
        'lblBlue
        '
        Me.lblBlue.AutoSize = True
        Me.lblBlue.Location = New System.Drawing.Point(764, 157)
        Me.lblBlue.Name = "lblBlue"
        Me.lblBlue.Size = New System.Drawing.Size(20, 20)
        Me.lblBlue.TabIndex = 0
        Me.lblBlue.Text = "B"
        '
        'numColourB
        '
        Me.numColourB.Location = New System.Drawing.Point(794, 157)
        Me.numColourB.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numColourB.Name = "numColourB"
        Me.numColourB.Size = New System.Drawing.Size(67, 26)
        Me.numColourB.TabIndex = 6
        '
        'pnlCustomColourDisplay
        '
        Me.pnlCustomColourDisplay.BackColor = System.Drawing.Color.Black
        Me.pnlCustomColourDisplay.Location = New System.Drawing.Point(867, 93)
        Me.pnlCustomColourDisplay.Name = "pnlCustomColourDisplay"
        Me.pnlCustomColourDisplay.Size = New System.Drawing.Size(32, 90)
        Me.pnlCustomColourDisplay.TabIndex = 12
        '
        'btnAddColour
        '
        Me.btnAddColour.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnAddColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddColour.ForeColor = System.Drawing.Color.Yellow
        Me.btnAddColour.Location = New System.Drawing.Point(768, 189)
        Me.btnAddColour.Name = "btnAddColour"
        Me.btnAddColour.Size = New System.Drawing.Size(93, 67)
        Me.btnAddColour.TabIndex = 7
        Me.btnAddColour.Text = "Add Colour"
        Me.btnAddColour.UseVisualStyleBackColor = False
        '
        'btnSwapColour
        '
        Me.btnSwapColour.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSwapColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSwapColour.ForeColor = System.Drawing.Color.Yellow
        Me.btnSwapColour.Location = New System.Drawing.Point(768, 262)
        Me.btnSwapColour.Name = "btnSwapColour"
        Me.btnSwapColour.Size = New System.Drawing.Size(93, 67)
        Me.btnSwapColour.TabIndex = 8
        Me.btnSwapColour.Text = "Swap Colour"
        Me.btnSwapColour.UseVisualStyleBackColor = False
        '
        'lblResizeW
        '
        Me.lblResizeW.AutoSize = True
        Me.lblResizeW.Location = New System.Drawing.Point(763, 332)
        Me.lblResizeW.Name = "lblResizeW"
        Me.lblResizeW.Size = New System.Drawing.Size(24, 20)
        Me.lblResizeW.TabIndex = 0
        Me.lblResizeW.Text = "W"
        '
        'numResizeW
        '
        Me.numResizeW.Location = New System.Drawing.Point(793, 330)
        Me.numResizeW.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.numResizeW.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numResizeW.Name = "numResizeW"
        Me.numResizeW.Size = New System.Drawing.Size(67, 26)
        Me.numResizeW.TabIndex = 9
        Me.numResizeW.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'numResizeH
        '
        Me.numResizeH.Location = New System.Drawing.Point(793, 362)
        Me.numResizeH.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.numResizeH.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numResizeH.Name = "numResizeH"
        Me.numResizeH.Size = New System.Drawing.Size(67, 26)
        Me.numResizeH.TabIndex = 10
        Me.numResizeH.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblResizeH
        '
        Me.lblResizeH.AutoSize = True
        Me.lblResizeH.Location = New System.Drawing.Point(763, 364)
        Me.lblResizeH.Name = "lblResizeH"
        Me.lblResizeH.Size = New System.Drawing.Size(21, 20)
        Me.lblResizeH.TabIndex = 0
        Me.lblResizeH.Text = "H"
        '
        'numResizeS
        '
        Me.numResizeS.Location = New System.Drawing.Point(793, 394)
        Me.numResizeS.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.numResizeS.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numResizeS.Name = "numResizeS"
        Me.numResizeS.Size = New System.Drawing.Size(67, 26)
        Me.numResizeS.TabIndex = 11
        Me.numResizeS.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblResizeS
        '
        Me.lblResizeS.AutoSize = True
        Me.lblResizeS.Location = New System.Drawing.Point(763, 396)
        Me.lblResizeS.Name = "lblResizeS"
        Me.lblResizeS.Size = New System.Drawing.Size(20, 20)
        Me.lblResizeS.TabIndex = 0
        Me.lblResizeS.Text = "S"
        '
        'btnResize
        '
        Me.btnResize.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnResize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResize.ForeColor = System.Drawing.Color.Yellow
        Me.btnResize.Location = New System.Drawing.Point(763, 426)
        Me.btnResize.Name = "btnResize"
        Me.btnResize.Size = New System.Drawing.Size(93, 67)
        Me.btnResize.TabIndex = 13
        Me.btnResize.Text = "Resize"
        Me.btnResize.UseVisualStyleBackColor = False
        '
        'btnRedraw
        '
        Me.btnRedraw.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnRedraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRedraw.ForeColor = System.Drawing.Color.Yellow
        Me.btnRedraw.Location = New System.Drawing.Point(763, 12)
        Me.btnRedraw.Name = "btnRedraw"
        Me.btnRedraw.Size = New System.Drawing.Size(93, 67)
        Me.btnRedraw.TabIndex = 0
        Me.btnRedraw.Text = "Redraw"
        Me.btnRedraw.UseVisualStyleBackColor = False
        '
        'FrmSpriteMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(911, 496)
        Me.Controls.Add(Me.btnRedraw)
        Me.Controls.Add(Me.btnResize)
        Me.Controls.Add(Me.numResizeS)
        Me.Controls.Add(Me.lblResizeS)
        Me.Controls.Add(Me.numResizeH)
        Me.Controls.Add(Me.lblResizeH)
        Me.Controls.Add(Me.numResizeW)
        Me.Controls.Add(Me.lblResizeW)
        Me.Controls.Add(Me.btnSwapColour)
        Me.Controls.Add(Me.btnAddColour)
        Me.Controls.Add(Me.pnlCustomColourDisplay)
        Me.Controls.Add(Me.lblBlue)
        Me.Controls.Add(Me.numColourB)
        Me.Controls.Add(Me.lblGreen)
        Me.Controls.Add(Me.numColourG)
        Me.Controls.Add(Me.lblRed)
        Me.Controls.Add(Me.numColourR)
        Me.Controls.Add(Me.lstColours)
        Me.Controls.Add(Me.tblColourSelect)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnSaveAs)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.pnlDraw)
        Me.ForeColor = System.Drawing.Color.Yellow
        Me.Name = "FrmSpriteMaker"
        Me.Text = "Sprite Maker"
        CType(Me.numColourR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numColourG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numColourB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numResizeW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numResizeH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numResizeS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlDraw As Panel
    Friend WithEvents btnOpen As Button
    Friend WithEvents btnSaveAs As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents tblColourSelect As TableLayoutPanel
    Friend WithEvents lstColours As ListBox
    Friend WithEvents numColourR As NumericUpDown
    Friend WithEvents lblRed As Label
    Friend WithEvents lblGreen As Label
    Friend WithEvents numColourG As NumericUpDown
    Friend WithEvents lblBlue As Label
    Friend WithEvents numColourB As NumericUpDown
    Friend WithEvents pnlCustomColourDisplay As Panel
    Friend WithEvents btnAddColour As Button
    Friend WithEvents btnSwapColour As Button
    Friend WithEvents lblResizeW As Label
    Friend WithEvents numResizeW As NumericUpDown
    Friend WithEvents numResizeH As NumericUpDown
    Friend WithEvents lblResizeH As Label
    Friend WithEvents numResizeS As NumericUpDown
    Friend WithEvents lblResizeS As Label
    Friend WithEvents btnResize As Button
    Friend WithEvents btnRedraw As Button
End Class
