<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGameExecutor
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
        Me.PnlGame = New System.Windows.Forms.Panel()
        Me.LblPause = New System.Windows.Forms.Label()
        Me.PnlGame.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlGame
        '
        Me.PnlGame.BackColor = System.Drawing.Color.Black
        Me.PnlGame.Controls.Add(Me.LblPause)
        Me.PnlGame.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlGame.Location = New System.Drawing.Point(0, 0)
        Me.PnlGame.Name = "PnlGame"
        Me.PnlGame.Size = New System.Drawing.Size(962, 738)
        Me.PnlGame.TabIndex = 0
        '
        'LblPause
        '
        Me.LblPause.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblPause.AutoSize = True
        Me.LblPause.BackColor = System.Drawing.Color.White
        Me.LblPause.Enabled = False
        Me.LblPause.Font = New System.Drawing.Font("Playbill", 28.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPause.Location = New System.Drawing.Point(407, 258)
        Me.LblPause.Name = "LblPause"
        Me.LblPause.Size = New System.Drawing.Size(130, 56)
        Me.LblPause.TabIndex = 0
        Me.LblPause.Text = "PAUSED"
        Me.LblPause.Visible = False
        '
        'FrmGameExecutor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(962, 738)
        Me.Controls.Add(Me.PnlGame)
        Me.DoubleBuffered = True
        Me.Name = "FrmGameExecutor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game Executor"
        Me.PnlGame.ResumeLayout(False)
        Me.PnlGame.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PnlGame As Panel
    Friend WithEvents LblPause As Label
End Class
