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
        Me.TblColourSelect = New System.Windows.Forms.TableLayoutPanel()
        Me.LblUsedColours = New System.Windows.Forms.Label()
        Me.TblControls = New System.Windows.Forms.TableLayoutPanel()
        Me.ToolBar = New System.Windows.Forms.ToolStrip()
        Me.ToolBarFile = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolBarFileOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolBarFileSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolBarFileSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.PnlDraw = New System.Windows.Forms.Panel()
        Me.ChkEraser = New System.Windows.Forms.CheckBox()
        Me.LblGreen = New System.Windows.Forms.Label()
        Me.NumColourR = New System.Windows.Forms.NumericUpDown()
        Me.PnlCustomColourDisplay = New System.Windows.Forms.Panel()
        Me.NumColourB = New System.Windows.Forms.NumericUpDown()
        Me.NumColourG = New System.Windows.Forms.NumericUpDown()
        Me.LblBlue = New System.Windows.Forms.Label()
        Me.LblRed = New System.Windows.Forms.Label()
        Me.TblColourCreater = New System.Windows.Forms.TableLayoutPanel()
        Me.NumResizeH = New System.Windows.Forms.NumericUpDown()
        Me.NumResizeW = New System.Windows.Forms.NumericUpDown()
        Me.LblResizeH = New System.Windows.Forms.Label()
        Me.LblResizeW = New System.Windows.Forms.Label()
        Me.BtnRedraw = New System.Windows.Forms.Button()
        Me.TblResizing = New System.Windows.Forms.TableLayoutPanel()
        Me.TblColourSelect.SuspendLayout()
        Me.TblControls.SuspendLayout()
        Me.ToolBar.SuspendLayout()
        CType(Me.NumColourR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumColourB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumColourG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TblColourCreater.SuspendLayout()
        CType(Me.NumResizeH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumResizeW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TblResizing.SuspendLayout()
        Me.SuspendLayout()
        '
        'TblColourSelect
        '
        Me.TblColourSelect.ColumnCount = 4
        Me.TblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblColourSelect.Controls.Add(Me.LblUsedColours, 0, 0)
        Me.TblColourSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TblColourSelect.Location = New System.Drawing.Point(553, 37)
        Me.TblColourSelect.Name = "TblColourSelect"
        Me.TblColourSelect.RowCount = 6
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TblColourSelect.Size = New System.Drawing.Size(361, 522)
        Me.TblColourSelect.TabIndex = 4
        '
        'LblUsedColours
        '
        Me.LblUsedColours.AutoSize = True
        Me.TblColourSelect.SetColumnSpan(Me.LblUsedColours, 4)
        Me.LblUsedColours.Location = New System.Drawing.Point(3, 0)
        Me.LblUsedColours.Name = "LblUsedColours"
        Me.LblUsedColours.Size = New System.Drawing.Size(159, 20)
        Me.LblUsedColours.TabIndex = 0
        Me.LblUsedColours.Text = "Used Colours Page 1"
        '
        'TblControls
        '
        Me.TblControls.AutoSize = True
        Me.TblControls.ColumnCount = 2
        Me.TblControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TblControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TblControls.Controls.Add(Me.ToolBar, 0, 0)
        Me.TblControls.Controls.Add(Me.TblResizing, 0, 2)
        Me.TblControls.Controls.Add(Me.TblColourSelect, 1, 1)
        Me.TblControls.Controls.Add(Me.PnlDraw, 0, 1)
        Me.TblControls.Controls.Add(Me.TblColourCreater, 1, 2)
        Me.TblControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TblControls.Location = New System.Drawing.Point(0, 0)
        Me.TblControls.Name = "TblControls"
        Me.TblControls.RowCount = 3
        Me.TblControls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControls.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TblControls.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TblControls.Size = New System.Drawing.Size(917, 695)
        Me.TblControls.TabIndex = 18
        '
        'ToolBar
        '
        Me.TblControls.SetColumnSpan(Me.ToolBar, 2)
        Me.ToolBar.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarFile})
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.ToolBar.Size = New System.Drawing.Size(917, 34)
        Me.ToolBar.TabIndex = 18
        Me.ToolBar.Text = "ToolBar"
        '
        'ToolBarFile
        '
        Me.ToolBarFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolBarFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarFileOpen, Me.ToolBarFileSaveAs, Me.ToolBarFileSave})
        Me.ToolBarFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarFile.Name = "ToolBarFile"
        Me.ToolBarFile.Size = New System.Drawing.Size(56, 29)
        Me.ToolBarFile.Text = "File"
        '
        'ToolBarFileOpen
        '
        Me.ToolBarFileOpen.Name = "ToolBarFileOpen"
        Me.ToolBarFileOpen.Size = New System.Drawing.Size(188, 34)
        Me.ToolBarFileOpen.Text = "Open..."
        '
        'ToolBarFileSaveAs
        '
        Me.ToolBarFileSaveAs.Name = "ToolBarFileSaveAs"
        Me.ToolBarFileSaveAs.Size = New System.Drawing.Size(188, 34)
        Me.ToolBarFileSaveAs.Text = "Save As..."
        '
        'ToolBarFileSave
        '
        Me.ToolBarFileSave.Name = "ToolBarFileSave"
        Me.ToolBarFileSave.Size = New System.Drawing.Size(188, 34)
        Me.ToolBarFileSave.Text = "Save"
        '
        'PnlDraw
        '
        Me.PnlDraw.BackColor = System.Drawing.Color.Black
        Me.PnlDraw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlDraw.Location = New System.Drawing.Point(3, 37)
        Me.PnlDraw.Name = "PnlDraw"
        Me.PnlDraw.Size = New System.Drawing.Size(544, 522)
        Me.PnlDraw.TabIndex = 0
        '
        'ChkEraser
        '
        Me.ChkEraser.AutoSize = True
        Me.TblColourCreater.SetColumnSpan(Me.ChkEraser, 3)
        Me.ChkEraser.Location = New System.Drawing.Point(3, 99)
        Me.ChkEraser.Name = "ChkEraser"
        Me.ChkEraser.Size = New System.Drawing.Size(82, 24)
        Me.ChkEraser.TabIndex = 14
        Me.ChkEraser.Text = "Eraser"
        Me.ChkEraser.UseVisualStyleBackColor = True
        '
        'LblGreen
        '
        Me.LblGreen.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblGreen.AutoSize = True
        Me.LblGreen.Location = New System.Drawing.Point(39, 38)
        Me.LblGreen.Name = "LblGreen"
        Me.LblGreen.Size = New System.Drawing.Size(22, 20)
        Me.LblGreen.TabIndex = 0
        Me.LblGreen.Text = "G"
        '
        'NumColourR
        '
        Me.NumColourR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumColourR.Location = New System.Drawing.Point(67, 3)
        Me.NumColourR.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumColourR.Name = "NumColourR"
        Me.NumColourR.Size = New System.Drawing.Size(106, 26)
        Me.NumColourR.TabIndex = 4
        '
        'PnlCustomColourDisplay
        '
        Me.PnlCustomColourDisplay.BackColor = System.Drawing.Color.Black
        Me.PnlCustomColourDisplay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlCustomColourDisplay.Location = New System.Drawing.Point(3, 3)
        Me.PnlCustomColourDisplay.Name = "PnlCustomColourDisplay"
        Me.TblColourCreater.SetRowSpan(Me.PnlCustomColourDisplay, 3)
        Me.PnlCustomColourDisplay.Size = New System.Drawing.Size(30, 90)
        Me.PnlCustomColourDisplay.TabIndex = 12
        '
        'NumColourB
        '
        Me.NumColourB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumColourB.Location = New System.Drawing.Point(67, 67)
        Me.NumColourB.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumColourB.Name = "NumColourB"
        Me.NumColourB.Size = New System.Drawing.Size(106, 26)
        Me.NumColourB.TabIndex = 6
        '
        'NumColourG
        '
        Me.NumColourG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumColourG.Location = New System.Drawing.Point(67, 35)
        Me.NumColourG.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumColourG.Name = "NumColourG"
        Me.NumColourG.Size = New System.Drawing.Size(106, 26)
        Me.NumColourG.TabIndex = 5
        '
        'LblBlue
        '
        Me.LblBlue.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblBlue.AutoSize = True
        Me.LblBlue.Location = New System.Drawing.Point(41, 70)
        Me.LblBlue.Name = "LblBlue"
        Me.LblBlue.Size = New System.Drawing.Size(20, 20)
        Me.LblBlue.TabIndex = 0
        Me.LblBlue.Text = "B"
        '
        'LblRed
        '
        Me.LblRed.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblRed.AutoSize = True
        Me.LblRed.Location = New System.Drawing.Point(40, 6)
        Me.LblRed.Name = "LblRed"
        Me.LblRed.Size = New System.Drawing.Size(21, 20)
        Me.LblRed.TabIndex = 0
        Me.LblRed.Text = "R"
        '
        'TblColourCreater
        '
        Me.TblColourCreater.ColumnCount = 3
        Me.TblColourCreater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblColourCreater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblColourCreater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblColourCreater.Controls.Add(Me.LblRed, 1, 0)
        Me.TblColourCreater.Controls.Add(Me.LblBlue, 1, 2)
        Me.TblColourCreater.Controls.Add(Me.NumColourG, 2, 1)
        Me.TblColourCreater.Controls.Add(Me.NumColourB, 2, 2)
        Me.TblColourCreater.Controls.Add(Me.PnlCustomColourDisplay, 0, 0)
        Me.TblColourCreater.Controls.Add(Me.NumColourR, 2, 0)
        Me.TblColourCreater.Controls.Add(Me.LblGreen, 1, 1)
        Me.TblColourCreater.Controls.Add(Me.ChkEraser, 0, 3)
        Me.TblColourCreater.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TblColourCreater.Location = New System.Drawing.Point(553, 565)
        Me.TblColourCreater.Name = "TblColourCreater"
        Me.TblColourCreater.RowCount = 4
        Me.TblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblColourCreater.Size = New System.Drawing.Size(176, 127)
        Me.TblColourCreater.TabIndex = 16
        '
        'NumResizeH
        '
        Me.NumResizeH.Location = New System.Drawing.Point(33, 37)
        Me.NumResizeH.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.NumResizeH.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumResizeH.Name = "NumResizeH"
        Me.NumResizeH.Size = New System.Drawing.Size(67, 26)
        Me.NumResizeH.TabIndex = 10
        Me.NumResizeH.Value = New Decimal(New Integer() {16, 0, 0, 0})
        '
        'NumResizeW
        '
        Me.NumResizeW.Location = New System.Drawing.Point(33, 3)
        Me.NumResizeW.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.NumResizeW.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumResizeW.Name = "NumResizeW"
        Me.NumResizeW.Size = New System.Drawing.Size(67, 26)
        Me.NumResizeW.TabIndex = 9
        Me.NumResizeW.Value = New Decimal(New Integer() {16, 0, 0, 0})
        '
        'LblResizeH
        '
        Me.LblResizeH.AutoSize = True
        Me.LblResizeH.Location = New System.Drawing.Point(3, 34)
        Me.LblResizeH.Name = "LblResizeH"
        Me.LblResizeH.Size = New System.Drawing.Size(21, 20)
        Me.LblResizeH.TabIndex = 0
        Me.LblResizeH.Text = "H"
        '
        'LblResizeW
        '
        Me.LblResizeW.AutoSize = True
        Me.LblResizeW.Location = New System.Drawing.Point(3, 0)
        Me.LblResizeW.Name = "LblResizeW"
        Me.LblResizeW.Size = New System.Drawing.Size(24, 20)
        Me.LblResizeW.TabIndex = 0
        Me.LblResizeW.Text = "W"
        '
        'BtnRedraw
        '
        Me.BtnRedraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRedraw.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnRedraw.Location = New System.Drawing.Point(106, 3)
        Me.BtnRedraw.Name = "BtnRedraw"
        Me.TblResizing.SetRowSpan(Me.BtnRedraw, 2)
        Me.BtnRedraw.Size = New System.Drawing.Size(111, 60)
        Me.BtnRedraw.TabIndex = 0
        Me.BtnRedraw.Text = "Redraw"
        Me.BtnRedraw.UseVisualStyleBackColor = False
        '
        'TblResizing
        '
        Me.TblResizing.ColumnCount = 3
        Me.TblResizing.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblResizing.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblResizing.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblResizing.Controls.Add(Me.BtnRedraw, 2, 0)
        Me.TblResizing.Controls.Add(Me.LblResizeW, 0, 0)
        Me.TblResizing.Controls.Add(Me.LblResizeH, 0, 1)
        Me.TblResizing.Controls.Add(Me.NumResizeW, 1, 0)
        Me.TblResizing.Controls.Add(Me.NumResizeH, 1, 1)
        Me.TblResizing.Location = New System.Drawing.Point(3, 565)
        Me.TblResizing.Name = "TblResizing"
        Me.TblResizing.RowCount = 2
        Me.TblResizing.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TblResizing.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TblResizing.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TblResizing.Size = New System.Drawing.Size(217, 69)
        Me.TblResizing.TabIndex = 15
        '
        'FrmSpriteMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(917, 695)
        Me.Controls.Add(Me.TblControls)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Name = "FrmSpriteMaker"
        Me.Text = "Sprite Maker"
        Me.TblColourSelect.ResumeLayout(False)
        Me.TblColourSelect.PerformLayout()
        Me.TblControls.ResumeLayout(False)
        Me.TblControls.PerformLayout()
        Me.ToolBar.ResumeLayout(False)
        Me.ToolBar.PerformLayout()
        CType(Me.NumColourR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumColourB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumColourG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TblColourCreater.ResumeLayout(False)
        Me.TblColourCreater.PerformLayout()
        CType(Me.NumResizeH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumResizeW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TblResizing.ResumeLayout(False)
        Me.TblResizing.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TblColourSelect As TableLayoutPanel
    Friend WithEvents BtnResize As Button
    Friend WithEvents TblControls As TableLayoutPanel
    Friend WithEvents ToolBar As ToolStrip
    Friend WithEvents ToolBarFile As ToolStripDropDownButton
    Friend WithEvents ToolBarFileOpen As ToolStripMenuItem
    Friend WithEvents ToolBarFileSaveAs As ToolStripMenuItem
    Friend WithEvents ToolBarFileSave As ToolStripMenuItem
    Friend WithEvents PnlDraw As Panel
    Friend WithEvents LblUsedColours As Label
    Friend WithEvents TblResizing As TableLayoutPanel
    Friend WithEvents BtnRedraw As Button
    Friend WithEvents LblResizeW As Label
    Friend WithEvents LblResizeH As Label
    Friend WithEvents NumResizeW As NumericUpDown
    Friend WithEvents NumResizeH As NumericUpDown
    Friend WithEvents TblColourCreater As TableLayoutPanel
    Friend WithEvents LblRed As Label
    Friend WithEvents LblBlue As Label
    Friend WithEvents NumColourG As NumericUpDown
    Friend WithEvents NumColourB As NumericUpDown
    Friend WithEvents PnlCustomColourDisplay As Panel
    Friend WithEvents NumColourR As NumericUpDown
    Friend WithEvents LblGreen As Label
    Friend WithEvents ChkEraser As CheckBox
End Class
