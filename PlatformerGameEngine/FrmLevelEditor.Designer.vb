<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLevelEditor
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
        Me.pnlGame = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.lblEntityListTitle = New System.Windows.Forms.Label()
        Me.lstEntity = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'pnlGame
        '
        Me.pnlGame.BackColor = System.Drawing.Color.Black
        Me.pnlGame.Location = New System.Drawing.Point(12, 12)
        Me.pnlGame.Name = "pnlGame"
        Me.pnlGame.Size = New System.Drawing.Size(800, 600)
        Me.pnlGame.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(1041, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 69)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(935, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 69)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Save As..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpen.Location = New System.Drawing.Point(829, 12)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(100, 69)
        Me.btnOpen.TabIndex = 1
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'lblEntityListTitle
        '
        Me.lblEntityListTitle.AutoSize = True
        Me.lblEntityListTitle.Location = New System.Drawing.Point(829, 99)
        Me.lblEntityListTitle.Name = "lblEntityListTitle"
        Me.lblEntityListTitle.Size = New System.Drawing.Size(62, 20)
        Me.lblEntityListTitle.TabIndex = 0
        Me.lblEntityListTitle.Text = "Entities"
        '
        'lstEntity
        '
        Me.lstEntity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstEntity.FormattingEnabled = True
        Me.lstEntity.ItemHeight = 20
        Me.lstEntity.Location = New System.Drawing.Point(829, 122)
        Me.lstEntity.Name = "lstEntity"
        Me.lstEntity.Size = New System.Drawing.Size(120, 220)
        Me.lstEntity.TabIndex = 4
        '
        'FrmLevelEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(1647, 874)
        Me.Controls.Add(Me.lstEntity)
        Me.Controls.Add(Me.lblEntityListTitle)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.pnlGame)
        Me.Name = "FrmLevelEditor"
        Me.Text = "Level Editor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlGame As Panel
    Friend WithEvents btnSave As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btnOpen As Button
    Friend WithEvents lblEntityListTitle As Label
    Friend WithEvents lstEntity As ListBox
End Class
