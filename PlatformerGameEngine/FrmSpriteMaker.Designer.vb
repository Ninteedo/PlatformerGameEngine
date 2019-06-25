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
        Me.tblSaveLoad = New System.Windows.Forms.TableLayoutPanel()
        Me.tblResizing = New System.Windows.Forms.TableLayoutPanel()
        Me.tblColourCreater = New System.Windows.Forms.TableLayoutPanel()
        Me.tblColourAndResizing = New System.Windows.Forms.TableLayoutPanel()
        Me.tblControls = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.numColourR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numColourG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numColourB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numResizeW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numResizeH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numResizeS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblSaveLoad.SuspendLayout()
        Me.tblResizing.SuspendLayout()
        Me.tblColourCreater.SuspendLayout()
        Me.tblColourAndResizing.SuspendLayout()
        Me.tblControls.SuspendLayout()
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
        Me.btnOpen.Location = New System.Drawing.Point(3, 3)
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
        Me.btnSaveAs.Location = New System.Drawing.Point(103, 3)
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
        Me.btnSave.Location = New System.Drawing.Point(203, 3)
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
        Me.tblColourSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblColourSelect.Location = New System.Drawing.Point(3, 85)
        Me.tblColourSelect.Name = "tblColourSelect"
        Me.tblColourSelect.RowCount = 6
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tblColourSelect.Size = New System.Drawing.Size(302, 481)
        Me.tblColourSelect.TabIndex = 4
        '
        'numColourR
        '
        Me.numColourR.Location = New System.Drawing.Point(31, 3)
        Me.numColourR.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numColourR.Name = "numColourR"
        Me.numColourR.Size = New System.Drawing.Size(67, 26)
        Me.numColourR.TabIndex = 4
        '
        'lblRed
        '
        Me.lblRed.AutoSize = True
        Me.lblRed.Location = New System.Drawing.Point(3, 0)
        Me.lblRed.Name = "lblRed"
        Me.lblRed.Size = New System.Drawing.Size(21, 20)
        Me.lblRed.TabIndex = 0
        Me.lblRed.Text = "R"
        '
        'lblGreen
        '
        Me.lblGreen.AutoSize = True
        Me.lblGreen.Location = New System.Drawing.Point(3, 33)
        Me.lblGreen.Name = "lblGreen"
        Me.lblGreen.Size = New System.Drawing.Size(22, 20)
        Me.lblGreen.TabIndex = 0
        Me.lblGreen.Text = "G"
        '
        'numColourG
        '
        Me.numColourG.Location = New System.Drawing.Point(31, 36)
        Me.numColourG.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numColourG.Name = "numColourG"
        Me.numColourG.Size = New System.Drawing.Size(67, 26)
        Me.numColourG.TabIndex = 5
        '
        'lblBlue
        '
        Me.lblBlue.AutoSize = True
        Me.lblBlue.Location = New System.Drawing.Point(3, 66)
        Me.lblBlue.Name = "lblBlue"
        Me.lblBlue.Size = New System.Drawing.Size(20, 20)
        Me.lblBlue.TabIndex = 0
        Me.lblBlue.Text = "B"
        '
        'numColourB
        '
        Me.numColourB.Location = New System.Drawing.Point(31, 69)
        Me.numColourB.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numColourB.Name = "numColourB"
        Me.numColourB.Size = New System.Drawing.Size(67, 26)
        Me.numColourB.TabIndex = 6
        '
        'pnlCustomColourDisplay
        '
        Me.pnlCustomColourDisplay.BackColor = System.Drawing.Color.Black
        Me.pnlCustomColourDisplay.Location = New System.Drawing.Point(104, 3)
        Me.pnlCustomColourDisplay.Name = "pnlCustomColourDisplay"
        Me.tblColourCreater.SetRowSpan(Me.pnlCustomColourDisplay, 3)
        Me.pnlCustomColourDisplay.Size = New System.Drawing.Size(30, 94)
        Me.pnlCustomColourDisplay.TabIndex = 12
        '
        'btnAddColour
        '
        Me.btnAddColour.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnAddColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddColour.ForeColor = System.Drawing.Color.Yellow
        Me.btnAddColour.Location = New System.Drawing.Point(3, 182)
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
        Me.btnSwapColour.Location = New System.Drawing.Point(3, 255)
        Me.btnSwapColour.Name = "btnSwapColour"
        Me.btnSwapColour.Size = New System.Drawing.Size(93, 67)
        Me.btnSwapColour.TabIndex = 8
        Me.btnSwapColour.Text = "Swap Colour"
        Me.btnSwapColour.UseVisualStyleBackColor = False
        '
        'lblResizeW
        '
        Me.lblResizeW.AutoSize = True
        Me.lblResizeW.Location = New System.Drawing.Point(3, 0)
        Me.lblResizeW.Name = "lblResizeW"
        Me.lblResizeW.Size = New System.Drawing.Size(24, 20)
        Me.lblResizeW.TabIndex = 0
        Me.lblResizeW.Text = "W"
        '
        'numResizeW
        '
        Me.numResizeW.Location = New System.Drawing.Point(33, 3)
        Me.numResizeW.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.numResizeW.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numResizeW.Name = "numResizeW"
        Me.numResizeW.Size = New System.Drawing.Size(67, 26)
        Me.numResizeW.TabIndex = 9
        Me.numResizeW.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'numResizeH
        '
        Me.numResizeH.Location = New System.Drawing.Point(33, 36)
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
        Me.lblResizeH.Location = New System.Drawing.Point(3, 33)
        Me.lblResizeH.Name = "lblResizeH"
        Me.lblResizeH.Size = New System.Drawing.Size(21, 20)
        Me.lblResizeH.TabIndex = 0
        Me.lblResizeH.Text = "H"
        '
        'numResizeS
        '
        Me.numResizeS.Location = New System.Drawing.Point(33, 69)
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
        Me.lblResizeS.Location = New System.Drawing.Point(3, 66)
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
        Me.btnResize.Location = New System.Drawing.Point(3, 434)
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
        Me.btnRedraw.Location = New System.Drawing.Point(3, 3)
        Me.btnRedraw.Name = "btnRedraw"
        Me.btnRedraw.Size = New System.Drawing.Size(93, 67)
        Me.btnRedraw.TabIndex = 0
        Me.btnRedraw.Text = "Redraw"
        Me.btnRedraw.UseVisualStyleBackColor = False
        '
        'tblSaveLoad
        '
        Me.tblSaveLoad.ColumnCount = 3
        Me.tblSaveLoad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tblSaveLoad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.tblSaveLoad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.tblSaveLoad.Controls.Add(Me.btnOpen, 0, 0)
        Me.tblSaveLoad.Controls.Add(Me.btnSaveAs, 1, 0)
        Me.tblSaveLoad.Controls.Add(Me.btnSave, 2, 0)
        Me.tblSaveLoad.Location = New System.Drawing.Point(3, 3)
        Me.tblSaveLoad.Name = "tblSaveLoad"
        Me.tblSaveLoad.RowCount = 1
        Me.tblSaveLoad.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSaveLoad.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSaveLoad.Size = New System.Drawing.Size(302, 76)
        Me.tblSaveLoad.TabIndex = 14
        '
        'tblResizing
        '
        Me.tblResizing.ColumnCount = 2
        Me.tblResizing.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblResizing.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblResizing.Controls.Add(Me.lblResizeW, 0, 0)
        Me.tblResizing.Controls.Add(Me.lblResizeH, 0, 1)
        Me.tblResizing.Controls.Add(Me.lblResizeS, 0, 2)
        Me.tblResizing.Controls.Add(Me.numResizeW, 1, 0)
        Me.tblResizing.Controls.Add(Me.numResizeS, 1, 2)
        Me.tblResizing.Controls.Add(Me.numResizeH, 1, 1)
        Me.tblResizing.Location = New System.Drawing.Point(3, 328)
        Me.tblResizing.Name = "tblResizing"
        Me.tblResizing.RowCount = 3
        Me.tblResizing.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tblResizing.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tblResizing.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tblResizing.Size = New System.Drawing.Size(111, 100)
        Me.tblResizing.TabIndex = 15
        '
        'tblColourCreater
        '
        Me.tblColourCreater.ColumnCount = 3
        Me.tblColourCreater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblColourCreater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblColourCreater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblColourCreater.Controls.Add(Me.lblRed, 0, 0)
        Me.tblColourCreater.Controls.Add(Me.lblGreen, 0, 1)
        Me.tblColourCreater.Controls.Add(Me.lblBlue, 0, 2)
        Me.tblColourCreater.Controls.Add(Me.numColourR, 1, 0)
        Me.tblColourCreater.Controls.Add(Me.numColourG, 1, 1)
        Me.tblColourCreater.Controls.Add(Me.numColourB, 1, 2)
        Me.tblColourCreater.Controls.Add(Me.pnlCustomColourDisplay, 2, 0)
        Me.tblColourCreater.Location = New System.Drawing.Point(3, 76)
        Me.tblColourCreater.Name = "tblColourCreater"
        Me.tblColourCreater.RowCount = 3
        Me.tblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tblColourCreater.Size = New System.Drawing.Size(138, 100)
        Me.tblColourCreater.TabIndex = 16
        '
        'tblColourAndResizing
        '
        Me.tblColourAndResizing.ColumnCount = 1
        Me.tblColourAndResizing.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblColourAndResizing.Controls.Add(Me.btnRedraw, 0, 0)
        Me.tblColourAndResizing.Controls.Add(Me.tblResizing, 0, 4)
        Me.tblColourAndResizing.Controls.Add(Me.btnResize, 0, 5)
        Me.tblColourAndResizing.Controls.Add(Me.tblColourCreater, 0, 1)
        Me.tblColourAndResizing.Controls.Add(Me.btnAddColour, 0, 2)
        Me.tblColourAndResizing.Controls.Add(Me.btnSwapColour, 0, 3)
        Me.tblColourAndResizing.Location = New System.Drawing.Point(311, 3)
        Me.tblColourAndResizing.Name = "tblColourAndResizing"
        Me.tblColourAndResizing.RowCount = 6
        Me.tblControls.SetRowSpan(Me.tblColourAndResizing, 2)
        Me.tblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblColourAndResizing.Size = New System.Drawing.Size(151, 517)
        Me.tblColourAndResizing.TabIndex = 17
        '
        'tblControls
        '
        Me.tblControls.ColumnCount = 2
        Me.tblControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblControls.Controls.Add(Me.tblColourSelect, 0, 1)
        Me.tblControls.Controls.Add(Me.tblColourAndResizing, 1, 0)
        Me.tblControls.Controls.Add(Me.tblSaveLoad, 0, 0)
        Me.tblControls.Location = New System.Drawing.Point(419, 13)
        Me.tblControls.Name = "tblControls"
        Me.tblControls.RowCount = 2
        Me.tblControls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControls.Size = New System.Drawing.Size(475, 569)
        Me.tblControls.TabIndex = 18
        '
        'FrmSpriteMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1095, 640)
        Me.Controls.Add(Me.tblControls)
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
        Me.tblSaveLoad.ResumeLayout(False)
        Me.tblResizing.ResumeLayout(False)
        Me.tblResizing.PerformLayout()
        Me.tblColourCreater.ResumeLayout(False)
        Me.tblColourCreater.PerformLayout()
        Me.tblColourAndResizing.ResumeLayout(False)
        Me.tblControls.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlDraw As Panel
    Friend WithEvents btnOpen As Button
    Friend WithEvents btnSaveAs As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents tblColourSelect As TableLayoutPanel
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
    Friend WithEvents tblSaveLoad As TableLayoutPanel
    Friend WithEvents tblResizing As TableLayoutPanel
    Friend WithEvents tblColourCreater As TableLayoutPanel
    Friend WithEvents tblColourAndResizing As TableLayoutPanel
    Friend WithEvents tblControls As TableLayoutPanel
End Class
