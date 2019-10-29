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
        Me.pnlFramePreview = New System.Windows.Forms.Panel()
        Me.lblSpriteListTitle = New System.Windows.Forms.Label()
        Me.lstSprites = New System.Windows.Forms.ListBox()
        Me.CntxtLstSprites = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuLstSpriteDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSpriteLoad = New System.Windows.Forms.Button()
        Me.BtnRedraw = New System.Windows.Forms.Button()
        Me.tblControlLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnDone = New System.Windows.Forms.Button()
        Me.CntxtLstSprites.SuspendLayout()
        Me.tblControlLayout.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlFramePreview
        '
        Me.pnlFramePreview.BackColor = System.Drawing.Color.Black
        Me.pnlFramePreview.Location = New System.Drawing.Point(3, 3)
        Me.pnlFramePreview.MinimumSize = New System.Drawing.Size(150, 154)
        Me.pnlFramePreview.Name = "pnlFramePreview"
        Me.tblControlLayout.SetRowSpan(Me.pnlFramePreview, 3)
        Me.pnlFramePreview.Size = New System.Drawing.Size(324, 320)
        Me.pnlFramePreview.TabIndex = 0
        '
        'lblSpriteListTitle
        '
        Me.lblSpriteListTitle.AutoSize = True
        Me.lblSpriteListTitle.Location = New System.Drawing.Point(333, 75)
        Me.lblSpriteListTitle.Name = "lblSpriteListTitle"
        Me.lblSpriteListTitle.Size = New System.Drawing.Size(59, 20)
        Me.lblSpriteListTitle.TabIndex = 8
        Me.lblSpriteListTitle.Text = "Sprites"
        '
        'lstSprites
        '
        Me.lstSprites.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstSprites.ContextMenuStrip = Me.CntxtLstSprites
        Me.lstSprites.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstSprites.FormattingEnabled = True
        Me.lstSprites.HorizontalScrollbar = True
        Me.lstSprites.ItemHeight = 20
        Me.lstSprites.Location = New System.Drawing.Point(333, 98)
        Me.lstSprites.Name = "lstSprites"
        Me.lstSprites.Size = New System.Drawing.Size(243, 320)
        Me.lstSprites.TabIndex = 7
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
        'btnSpriteLoad
        '
        Me.btnSpriteLoad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSpriteLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSpriteLoad.Location = New System.Drawing.Point(333, 424)
        Me.btnSpriteLoad.Name = "btnSpriteLoad"
        Me.btnSpriteLoad.Size = New System.Drawing.Size(243, 69)
        Me.btnSpriteLoad.TabIndex = 9
        Me.btnSpriteLoad.Text = "Load..."
        Me.btnSpriteLoad.UseVisualStyleBackColor = True
        '
        'BtnRedraw
        '
        Me.BtnRedraw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnRedraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRedraw.Location = New System.Drawing.Point(333, 3)
        Me.BtnRedraw.Name = "BtnRedraw"
        Me.BtnRedraw.Size = New System.Drawing.Size(243, 69)
        Me.BtnRedraw.TabIndex = 17
        Me.BtnRedraw.Text = "Redraw"
        Me.BtnRedraw.UseVisualStyleBackColor = True
        '
        'tblControlLayout
        '
        Me.tblControlLayout.ColumnCount = 2
        Me.tblControlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblControlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblControlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tblControlLayout.Controls.Add(Me.lstSprites, 1, 2)
        Me.tblControlLayout.Controls.Add(Me.btnSpriteLoad, 1, 3)
        Me.tblControlLayout.Controls.Add(Me.pnlFramePreview, 0, 0)
        Me.tblControlLayout.Controls.Add(Me.lblSpriteListTitle, 1, 1)
        Me.tblControlLayout.Controls.Add(Me.BtnRedraw, 1, 0)
        Me.tblControlLayout.Controls.Add(Me.BtnCancel, 0, 6)
        Me.tblControlLayout.Controls.Add(Me.BtnDone, 1, 6)
        Me.tblControlLayout.Location = New System.Drawing.Point(16, 17)
        Me.tblControlLayout.Name = "tblControlLayout"
        Me.tblControlLayout.RowCount = 7
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.Size = New System.Drawing.Size(579, 577)
        Me.tblControlLayout.TabIndex = 20
        '
        'BtnCancel
        '
        Me.BtnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Location = New System.Drawing.Point(3, 499)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(324, 75)
        Me.BtnCancel.TabIndex = 18
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnDone
        '
        Me.BtnDone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDone.Location = New System.Drawing.Point(333, 499)
        Me.BtnDone.Name = "BtnDone"
        Me.BtnDone.Size = New System.Drawing.Size(243, 75)
        Me.BtnDone.TabIndex = 19
        Me.BtnDone.Text = "Done"
        Me.BtnDone.UseVisualStyleBackColor = True
        '
        'FrmActorMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(748, 614)
        Me.Controls.Add(Me.tblControlLayout)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmActorMaker"
        Me.Text = "Actor Maker"
        Me.CntxtLstSprites.ResumeLayout(False)
        Me.tblControlLayout.ResumeLayout(False)
        Me.tblControlLayout.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlFramePreview As Panel
    Friend WithEvents lblSpriteListTitle As Label
    Friend WithEvents lstSprites As ListBox
    Friend WithEvents btnSpriteLoad As Button
    Friend WithEvents BtnRedraw As System.Windows.Forms.Button
    Friend WithEvents tblControlLayout As TableLayoutPanel
    Friend WithEvents CntxtLstSprites As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MenuLstSpriteDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnDone As System.Windows.Forms.Button
End Class
