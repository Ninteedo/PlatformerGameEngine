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
        'FrmLevelEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(1647, 874)
        Me.Controls.Add(Me.pnlGame)
        Me.Name = "FrmLevelEditor"
        Me.Text = "Level Editor"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlGame As Panel
End Class
