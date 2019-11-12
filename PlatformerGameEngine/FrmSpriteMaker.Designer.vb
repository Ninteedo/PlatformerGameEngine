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
        Me.PnlDraw = New System.Windows.Forms.Panel()
        Me.BtnOpen = New System.Windows.Forms.Button()
        Me.BtnSaveAs = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.TblColourSelect = New System.Windows.Forms.TableLayoutPanel()
        Me.NumColourR = New System.Windows.Forms.NumericUpDown()
        Me.LblRed = New System.Windows.Forms.Label()
        Me.LblGreen = New System.Windows.Forms.Label()
        Me.NumColourG = New System.Windows.Forms.NumericUpDown()
        Me.LblBlue = New System.Windows.Forms.Label()
        Me.NumColourB = New System.Windows.Forms.NumericUpDown()
        Me.PnlCustomColourDisplay = New System.Windows.Forms.Panel()
        Me.BtnAddColour = New System.Windows.Forms.Button()
        Me.BtnSwapColour = New System.Windows.Forms.Button()
        Me.LblResizeW = New System.Windows.Forms.Label()
        Me.NumResizeW = New System.Windows.Forms.NumericUpDown()
        Me.NumResizeH = New System.Windows.Forms.NumericUpDown()
        Me.LblResizeH = New System.Windows.Forms.Label()
        Me.NumResizeS = New System.Windows.Forms.NumericUpDown()
        Me.LblResizeS = New System.Windows.Forms.Label()
        Me.BtnResize = New System.Windows.Forms.Button()
        Me.BtnRedraw = New System.Windows.Forms.Button()
        Me.TblSaveLoad = New System.Windows.Forms.TableLayoutPanel()
        Me.TblResizing = New System.Windows.Forms.TableLayoutPanel()
        Me.TblColourCreater = New System.Windows.Forms.TableLayoutPanel()
        Me.TblColourAndResizing = New System.Windows.Forms.TableLayoutPanel()
        Me.TblControls = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.NumColourR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumColourG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumColourB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumResizeW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumResizeH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumResizeS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TblSaveLoad.SuspendLayout()
        Me.TblResizing.SuspendLayout()
        Me.TblColourCreater.SuspendLayout()
        Me.TblColourAndResizing.SuspendLayout()
        Me.TblControls.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlDraw
        '
        Me.PnlDraw.BackColor = System.Drawing.Color.Black
        Me.PnlDraw.Location = New System.Drawing.Point(3, 3)
        Me.PnlDraw.Name = "PnlDraw"
        Me.TblControls.SetRowSpan(Me.PnlDraw, 2)
        Me.PnlDraw.Size = New System.Drawing.Size(400, 400)
        Me.PnlDraw.TabIndex = 0
        '
        'BtnOpen
        '
        Me.BtnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOpen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnOpen.Location = New System.Drawing.Point(3, 3)
        Me.BtnOpen.Name = "BtnOpen"
        Me.BtnOpen.Size = New System.Drawing.Size(93, 67)
        Me.BtnOpen.TabIndex = 1
        Me.BtnOpen.Text = "Open"
        Me.BtnOpen.UseVisualStyleBackColor = False
        '
        'BtnSaveAs
        '
        Me.BtnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveAs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnSaveAs.Location = New System.Drawing.Point(103, 3)
        Me.BtnSaveAs.Name = "BtnSaveAs"
        Me.BtnSaveAs.Size = New System.Drawing.Size(93, 67)
        Me.BtnSaveAs.TabIndex = 2
        Me.BtnSaveAs.Text = "Save As..."
        Me.BtnSaveAs.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        Me.BtnSave.Enabled = False
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnSave.Location = New System.Drawing.Point(203, 3)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(93, 67)
        Me.BtnSave.TabIndex = 3
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'TblColourSelect
        '
        Me.TblColourSelect.ColumnCount = 4
        Me.TblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblColourSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblColourSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TblColourSelect.Location = New System.Drawing.Point(409, 85)
        Me.TblColourSelect.Name = "TblColourSelect"
        Me.TblColourSelect.RowCount = 6
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TblColourSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TblColourSelect.Size = New System.Drawing.Size(302, 528)
        Me.TblColourSelect.TabIndex = 4
        '
        'NumColourR
        '
        Me.NumColourR.Location = New System.Drawing.Point(31, 3)
        Me.NumColourR.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumColourR.Name = "NumColourR"
        Me.NumColourR.Size = New System.Drawing.Size(67, 26)
        Me.NumColourR.TabIndex = 4
        '
        'LblRed
        '
        Me.LblRed.AutoSize = True
        Me.LblRed.Location = New System.Drawing.Point(3, 0)
        Me.LblRed.Name = "LblRed"
        Me.LblRed.Size = New System.Drawing.Size(21, 20)
        Me.LblRed.TabIndex = 0
        Me.LblRed.Text = "R"
        '
        'LblGreen
        '
        Me.LblGreen.AutoSize = True
        Me.LblGreen.Location = New System.Drawing.Point(3, 33)
        Me.LblGreen.Name = "LblGreen"
        Me.LblGreen.Size = New System.Drawing.Size(22, 20)
        Me.LblGreen.TabIndex = 0
        Me.LblGreen.Text = "G"
        '
        'NumColourG
        '
        Me.NumColourG.Location = New System.Drawing.Point(31, 36)
        Me.NumColourG.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumColourG.Name = "NumColourG"
        Me.NumColourG.Size = New System.Drawing.Size(67, 26)
        Me.NumColourG.TabIndex = 5
        '
        'LblBlue
        '
        Me.LblBlue.AutoSize = True
        Me.LblBlue.Location = New System.Drawing.Point(3, 66)
        Me.LblBlue.Name = "LblBlue"
        Me.LblBlue.Size = New System.Drawing.Size(20, 20)
        Me.LblBlue.TabIndex = 0
        Me.LblBlue.Text = "B"
        '
        'NumColourB
        '
        Me.NumColourB.Location = New System.Drawing.Point(31, 69)
        Me.NumColourB.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumColourB.Name = "NumColourB"
        Me.NumColourB.Size = New System.Drawing.Size(67, 26)
        Me.NumColourB.TabIndex = 6
        '
        'PnlCustomColourDisplay
        '
        Me.PnlCustomColourDisplay.BackColor = System.Drawing.Color.Black
        Me.PnlCustomColourDisplay.Location = New System.Drawing.Point(104, 3)
        Me.PnlCustomColourDisplay.Name = "PnlCustomColourDisplay"
        Me.TblColourCreater.SetRowSpan(Me.PnlCustomColourDisplay, 3)
        Me.PnlCustomColourDisplay.Size = New System.Drawing.Size(30, 94)
        Me.PnlCustomColourDisplay.TabIndex = 12
        '
        'BtnAddColour
        '
        Me.BtnAddColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddColour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnAddColour.Location = New System.Drawing.Point(3, 182)
        Me.BtnAddColour.Name = "BtnAddColour"
        Me.BtnAddColour.Size = New System.Drawing.Size(93, 67)
        Me.BtnAddColour.TabIndex = 7
        Me.BtnAddColour.Text = "Add Colour"
        Me.BtnAddColour.UseVisualStyleBackColor = False
        '
        'BtnSwapColour
        '
        Me.BtnSwapColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSwapColour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnSwapColour.Location = New System.Drawing.Point(3, 255)
        Me.BtnSwapColour.Name = "BtnSwapColour"
        Me.BtnSwapColour.Size = New System.Drawing.Size(93, 67)
        Me.BtnSwapColour.TabIndex = 8
        Me.BtnSwapColour.Text = "Swap Colour"
        Me.BtnSwapColour.UseVisualStyleBackColor = False
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
        'NumResizeW
        '
        Me.NumResizeW.Location = New System.Drawing.Point(33, 3)
        Me.NumResizeW.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.NumResizeW.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumResizeW.Name = "NumResizeW"
        Me.NumResizeW.Size = New System.Drawing.Size(67, 26)
        Me.NumResizeW.TabIndex = 9
        Me.NumResizeW.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NumResizeH
        '
        Me.NumResizeH.Location = New System.Drawing.Point(33, 36)
        Me.NumResizeH.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.NumResizeH.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumResizeH.Name = "NumResizeH"
        Me.NumResizeH.Size = New System.Drawing.Size(67, 26)
        Me.NumResizeH.TabIndex = 10
        Me.NumResizeH.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'LblResizeH
        '
        Me.LblResizeH.AutoSize = True
        Me.LblResizeH.Location = New System.Drawing.Point(3, 33)
        Me.LblResizeH.Name = "LblResizeH"
        Me.LblResizeH.Size = New System.Drawing.Size(21, 20)
        Me.LblResizeH.TabIndex = 0
        Me.LblResizeH.Text = "H"
        '
        'NumResizeS
        '
        Me.NumResizeS.Location = New System.Drawing.Point(33, 69)
        Me.NumResizeS.Maximum = New Decimal(New Integer() {64, 0, 0, 0})
        Me.NumResizeS.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumResizeS.Name = "NumResizeS"
        Me.NumResizeS.Size = New System.Drawing.Size(67, 26)
        Me.NumResizeS.TabIndex = 11
        Me.NumResizeS.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'LblResizeS
        '
        Me.LblResizeS.AutoSize = True
        Me.LblResizeS.Location = New System.Drawing.Point(3, 66)
        Me.LblResizeS.Name = "LblResizeS"
        Me.LblResizeS.Size = New System.Drawing.Size(20, 20)
        Me.LblResizeS.TabIndex = 0
        Me.LblResizeS.Text = "S"
        '
        'BtnResize
        '
        Me.BtnResize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnResize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnResize.Location = New System.Drawing.Point(3, 434)
        Me.BtnResize.Name = "BtnResize"
        Me.BtnResize.Size = New System.Drawing.Size(93, 67)
        Me.BtnResize.TabIndex = 13
        Me.BtnResize.Text = "Resize"
        Me.BtnResize.UseVisualStyleBackColor = False
        '
        'BtnRedraw
        '
        Me.BtnRedraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRedraw.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnRedraw.Location = New System.Drawing.Point(3, 3)
        Me.BtnRedraw.Name = "BtnRedraw"
        Me.BtnRedraw.Size = New System.Drawing.Size(93, 67)
        Me.BtnRedraw.TabIndex = 0
        Me.BtnRedraw.Text = "Redraw"
        Me.BtnRedraw.UseVisualStyleBackColor = False
        '
        'TblSaveLoad
        '
        Me.TblSaveLoad.ColumnCount = 3
        Me.TblSaveLoad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TblSaveLoad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TblSaveLoad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TblSaveLoad.Controls.Add(Me.BtnOpen, 0, 0)
        Me.TblSaveLoad.Controls.Add(Me.BtnSaveAs, 1, 0)
        Me.TblSaveLoad.Controls.Add(Me.BtnSave, 2, 0)
        Me.TblSaveLoad.Location = New System.Drawing.Point(409, 3)
        Me.TblSaveLoad.Name = "TblSaveLoad"
        Me.TblSaveLoad.RowCount = 1
        Me.TblSaveLoad.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TblSaveLoad.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76.0!))
        Me.TblSaveLoad.Size = New System.Drawing.Size(302, 76)
        Me.TblSaveLoad.TabIndex = 14
        '
        'TblResizing
        '
        Me.TblResizing.ColumnCount = 2
        Me.TblResizing.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblResizing.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblResizing.Controls.Add(Me.LblResizeW, 0, 0)
        Me.TblResizing.Controls.Add(Me.LblResizeH, 0, 1)
        Me.TblResizing.Controls.Add(Me.LblResizeS, 0, 2)
        Me.TblResizing.Controls.Add(Me.NumResizeW, 1, 0)
        Me.TblResizing.Controls.Add(Me.NumResizeS, 1, 2)
        Me.TblResizing.Controls.Add(Me.NumResizeH, 1, 1)
        Me.TblResizing.Location = New System.Drawing.Point(3, 328)
        Me.TblResizing.Name = "TblResizing"
        Me.TblResizing.RowCount = 3
        Me.TblResizing.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TblResizing.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TblResizing.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TblResizing.Size = New System.Drawing.Size(111, 100)
        Me.TblResizing.TabIndex = 15
        '
        'TblColourCreater
        '
        Me.TblColourCreater.ColumnCount = 3
        Me.TblColourCreater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblColourCreater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblColourCreater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblColourCreater.Controls.Add(Me.LblRed, 0, 0)
        Me.TblColourCreater.Controls.Add(Me.LblGreen, 0, 1)
        Me.TblColourCreater.Controls.Add(Me.LblBlue, 0, 2)
        Me.TblColourCreater.Controls.Add(Me.NumColourR, 1, 0)
        Me.TblColourCreater.Controls.Add(Me.NumColourG, 1, 1)
        Me.TblColourCreater.Controls.Add(Me.NumColourB, 1, 2)
        Me.TblColourCreater.Controls.Add(Me.PnlCustomColourDisplay, 2, 0)
        Me.TblColourCreater.Location = New System.Drawing.Point(3, 76)
        Me.TblColourCreater.Name = "TblColourCreater"
        Me.TblColourCreater.RowCount = 3
        Me.TblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TblColourCreater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TblColourCreater.Size = New System.Drawing.Size(138, 100)
        Me.TblColourCreater.TabIndex = 16
        '
        'TblColourAndResizing
        '
        Me.TblColourAndResizing.ColumnCount = 1
        Me.TblColourAndResizing.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TblColourAndResizing.Controls.Add(Me.BtnRedraw, 0, 0)
        Me.TblColourAndResizing.Controls.Add(Me.TblResizing, 0, 4)
        Me.TblColourAndResizing.Controls.Add(Me.BtnResize, 0, 5)
        Me.TblColourAndResizing.Controls.Add(Me.TblColourCreater, 0, 1)
        Me.TblColourAndResizing.Controls.Add(Me.BtnAddColour, 0, 2)
        Me.TblColourAndResizing.Controls.Add(Me.BtnSwapColour, 0, 3)
        Me.TblColourAndResizing.Location = New System.Drawing.Point(717, 3)
        Me.TblColourAndResizing.Name = "TblColourAndResizing"
        Me.TblColourAndResizing.RowCount = 6
        Me.TblControls.SetRowSpan(Me.TblColourAndResizing, 2)
        Me.TblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblColourAndResizing.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblColourAndResizing.Size = New System.Drawing.Size(151, 517)
        Me.TblColourAndResizing.TabIndex = 17
        '
        'TblControls
        '
        Me.TblControls.ColumnCount = 3
        Me.TblControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblControls.Controls.Add(Me.TblColourSelect, 1, 1)
        Me.TblControls.Controls.Add(Me.PnlDraw, 0, 0)
        Me.TblControls.Controls.Add(Me.TblColourAndResizing, 2, 0)
        Me.TblControls.Controls.Add(Me.TblSaveLoad, 1, 0)
        Me.TblControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TblControls.Location = New System.Drawing.Point(0, 0)
        Me.TblControls.Name = "TblControls"
        Me.TblControls.RowCount = 2
        Me.TblControls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControls.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControls.Size = New System.Drawing.Size(901, 616)
        Me.TblControls.TabIndex = 18
        '
        'FrmSpriteMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(901, 616)
        Me.Controls.Add(Me.TblControls)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Name = "FrmSpriteMaker"
        Me.Text = "Sprite Maker"
        CType(Me.NumColourR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumColourG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumColourB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumResizeW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumResizeH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumResizeS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TblSaveLoad.ResumeLayout(False)
        Me.TblResizing.ResumeLayout(False)
        Me.TblResizing.PerformLayout()
        Me.TblColourCreater.ResumeLayout(False)
        Me.TblColourCreater.PerformLayout()
        Me.TblColourAndResizing.ResumeLayout(False)
        Me.TblControls.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PnlDraw As Panel
    Friend WithEvents BtnOpen As Button
    Friend WithEvents BtnSaveAs As Button
    Friend WithEvents BtnSave As Button
    Friend WithEvents TblColourSelect As TableLayoutPanel
    Friend WithEvents NumColourR As NumericUpDown
    Friend WithEvents LblRed As Label
    Friend WithEvents LblGreen As Label
    Friend WithEvents NumColourG As NumericUpDown
    Friend WithEvents LblBlue As Label
    Friend WithEvents NumColourB As NumericUpDown
    Friend WithEvents PnlCustomColourDisplay As Panel
    Friend WithEvents BtnAddColour As Button
    Friend WithEvents BtnSwapColour As Button
    Friend WithEvents LblResizeW As Label
    Friend WithEvents NumResizeW As NumericUpDown
    Friend WithEvents NumResizeH As NumericUpDown
    Friend WithEvents LblResizeH As Label
    Friend WithEvents NumResizeS As NumericUpDown
    Friend WithEvents LblResizeS As Label
    Friend WithEvents BtnResize As Button
    Friend WithEvents BtnRedraw As Button
    Friend WithEvents TblSaveLoad As TableLayoutPanel
    Friend WithEvents TblResizing As TableLayoutPanel
    Friend WithEvents TblColourCreater As TableLayoutPanel
    Friend WithEvents TblColourAndResizing As TableLayoutPanel
    Friend WithEvents TblControls As TableLayoutPanel
End Class
