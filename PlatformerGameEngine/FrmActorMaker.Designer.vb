<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmActorMaker
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LblSpriteListTitle = New System.Windows.Forms.Label()
        Me.LstSprites = New System.Windows.Forms.ListBox()
        Me.CntxtLstSprites = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuLstSpriteDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.BtnSpriteLoad = New System.Windows.Forms.Button()
        Me.TblControlLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.FlwDoneCancel = New System.Windows.Forms.FlowLayoutPanel()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnDone = New System.Windows.Forms.Button()
        Me.FlwSpriteNumber = New System.Windows.Forms.FlowLayoutPanel()
        Me.LblSpriteNum = New System.Windows.Forms.Label()
        Me.NumSpriteIndex = New System.Windows.Forms.NumericUpDown()
        Me.PnlPreview = New System.Windows.Forms.Panel()
        Me.CntxtLstSprites.SuspendLayout()
        Me.TblControlLayout.SuspendLayout()
        Me.FlwDoneCancel.SuspendLayout()
        Me.FlwSpriteNumber.SuspendLayout()
        CType(Me.NumSpriteIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblSpriteListTitle
        '
        Me.LblSpriteListTitle.AutoSize = True
        Me.LblSpriteListTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSpriteListTitle.Location = New System.Drawing.Point(333, 0)
        Me.LblSpriteListTitle.Name = "LblSpriteListTitle"
        Me.LblSpriteListTitle.Size = New System.Drawing.Size(89, 29)
        Me.LblSpriteListTitle.TabIndex = 8
        Me.LblSpriteListTitle.Text = "Sprites"
        '
        'LstSprites
        '
        Me.LstSprites.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstSprites.ContextMenuStrip = Me.CntxtLstSprites
        Me.LstSprites.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstSprites.FormattingEnabled = True
        Me.LstSprites.HorizontalScrollbar = True
        Me.LstSprites.ItemHeight = 20
        Me.LstSprites.Location = New System.Drawing.Point(333, 32)
        Me.LstSprites.Name = "LstSprites"
        Me.LstSprites.Size = New System.Drawing.Size(243, 320)
        Me.LstSprites.TabIndex = 7
        '
        'CntxtLstSprites
        '
        Me.CntxtLstSprites.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.CntxtLstSprites.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuLstSpriteDelete})
        Me.CntxtLstSprites.Name = "CntxtLstSprites"
        Me.CntxtLstSprites.Size = New System.Drawing.Size(135, 36)
        '
        'MenuLstSpriteDelete
        '
        Me.MenuLstSpriteDelete.Name = "MenuLstSpriteDelete"
        Me.MenuLstSpriteDelete.Size = New System.Drawing.Size(134, 32)
        Me.MenuLstSpriteDelete.Text = "Delete"
        '
        'BtnSpriteLoad
        '
        Me.BtnSpriteLoad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnSpriteLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSpriteLoad.Location = New System.Drawing.Point(333, 358)
        Me.BtnSpriteLoad.Name = "BtnSpriteLoad"
        Me.BtnSpriteLoad.Size = New System.Drawing.Size(243, 60)
        Me.BtnSpriteLoad.TabIndex = 9
        Me.BtnSpriteLoad.Text = "Load..."
        Me.BtnSpriteLoad.UseVisualStyleBackColor = True
        '
        'TblControlLayout
        '
        Me.TblControlLayout.ColumnCount = 2
        Me.TblControlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblControlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblControlLayout.Controls.Add(Me.FlwDoneCancel, 1, 4)
        Me.TblControlLayout.Controls.Add(Me.FlwSpriteNumber, 1, 3)
        Me.TblControlLayout.Controls.Add(Me.LstSprites, 1, 1)
        Me.TblControlLayout.Controls.Add(Me.BtnSpriteLoad, 1, 2)
        Me.TblControlLayout.Controls.Add(Me.LblSpriteListTitle, 1, 0)
        Me.TblControlLayout.Controls.Add(Me.PnlPreview, 0, 0)
        Me.TblControlLayout.Location = New System.Drawing.Point(16, 17)
        Me.TblControlLayout.Name = "TblControlLayout"
        Me.TblControlLayout.RowCount = 5
        Me.TblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlLayout.Size = New System.Drawing.Size(579, 546)
        Me.TblControlLayout.TabIndex = 20
        '
        'FlwDoneCancel
        '
        Me.FlwDoneCancel.Controls.Add(Me.BtnCancel)
        Me.FlwDoneCancel.Controls.Add(Me.BtnDone)
        Me.FlwDoneCancel.Location = New System.Drawing.Point(333, 465)
        Me.FlwDoneCancel.Name = "FlwDoneCancel"
        Me.FlwDoneCancel.Size = New System.Drawing.Size(236, 70)
        Me.FlwDoneCancel.TabIndex = 21
        '
        'BtnCancel
        '
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Location = New System.Drawing.Point(3, 3)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(102, 61)
        Me.BtnCancel.TabIndex = 18
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnDone
        '
        Me.BtnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDone.Location = New System.Drawing.Point(111, 3)
        Me.BtnDone.Name = "BtnDone"
        Me.BtnDone.Size = New System.Drawing.Size(102, 61)
        Me.BtnDone.TabIndex = 19
        Me.BtnDone.Text = "Done"
        Me.BtnDone.UseVisualStyleBackColor = True
        '
        'FlwSpriteNumber
        '
        Me.FlwSpriteNumber.Controls.Add(Me.LblSpriteNum)
        Me.FlwSpriteNumber.Controls.Add(Me.NumSpriteIndex)
        Me.FlwSpriteNumber.Location = New System.Drawing.Point(333, 424)
        Me.FlwSpriteNumber.Name = "FlwSpriteNumber"
        Me.FlwSpriteNumber.Size = New System.Drawing.Size(200, 35)
        Me.FlwSpriteNumber.TabIndex = 22
        '
        'LblSpriteNum
        '
        Me.LblSpriteNum.AutoSize = True
        Me.LblSpriteNum.Location = New System.Drawing.Point(3, 0)
        Me.LblSpriteNum.Name = "LblSpriteNum"
        Me.LblSpriteNum.Size = New System.Drawing.Size(111, 20)
        Me.LblSpriteNum.TabIndex = 20
        Me.LblSpriteNum.Text = "Sprite Number"
        '
        'NumSpriteIndex
        '
        Me.NumSpriteIndex.Enabled = False
        Me.NumSpriteIndex.Location = New System.Drawing.Point(120, 3)
        Me.NumSpriteIndex.Name = "NumSpriteIndex"
        Me.NumSpriteIndex.Size = New System.Drawing.Size(39, 26)
        Me.NumSpriteIndex.TabIndex = 21
        '
        'PnlPreview
        '
        Me.PnlPreview.BackColor = System.Drawing.Color.Black
        Me.PnlPreview.Location = New System.Drawing.Point(3, 3)
        Me.PnlPreview.MinimumSize = New System.Drawing.Size(150, 154)
        Me.PnlPreview.Name = "PnlPreview"
        Me.TblControlLayout.SetRowSpan(Me.PnlPreview, 3)
        Me.PnlPreview.Size = New System.Drawing.Size(324, 317)
        Me.PnlPreview.TabIndex = 0
        '
        'FrmActorMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(748, 580)
        Me.Controls.Add(Me.TblControlLayout)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmActorMaker"
        Me.Text = "Actor Maker"
        Me.CntxtLstSprites.ResumeLayout(False)
        Me.TblControlLayout.ResumeLayout(False)
        Me.TblControlLayout.PerformLayout()
        Me.FlwDoneCancel.ResumeLayout(False)
        Me.FlwSpriteNumber.ResumeLayout(False)
        Me.FlwSpriteNumber.PerformLayout()
        CType(Me.NumSpriteIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblSpriteListTitle As Label
    Friend WithEvents LstSprites As ListBox
    Friend WithEvents BtnSpriteLoad As Button
    Friend WithEvents TblControlLayout As TableLayoutPanel
    Friend WithEvents CntxtLstSprites As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MenuLstSpriteDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnDone As System.Windows.Forms.Button
    Friend WithEvents FlwSpriteNumber As FlowLayoutPanel
    Friend WithEvents LblSpriteNum As Label
    Friend WithEvents NumSpriteIndex As NumericUpDown
    Friend WithEvents PnlPreview As Panel
    Friend WithEvents FlwDoneCancel As FlowLayoutPanel
End Class
