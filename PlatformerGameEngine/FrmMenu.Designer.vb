<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMenu
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
        Me.BtnMenu1 = New System.Windows.Forms.Button()
        Me.BtnMenu2 = New System.Windows.Forms.Button()
        Me.BtnMenu3 = New System.Windows.Forms.Button()
        Me.BtnMenuBack = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnMenu1
        '
        Me.BtnMenu1.BackColor = System.Drawing.Color.Silver
        Me.BtnMenu1.Location = New System.Drawing.Point(18, 108)
        Me.BtnMenu1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnMenu1.Name = "BtnMenu1"
        Me.BtnMenu1.Size = New System.Drawing.Size(146, 72)
        Me.BtnMenu1.TabIndex = 1
        Me.BtnMenu1.Text = "1"
        Me.BtnMenu1.UseVisualStyleBackColor = False
        '
        'BtnMenu2
        '
        Me.BtnMenu2.BackColor = System.Drawing.Color.Silver
        Me.BtnMenu2.Location = New System.Drawing.Point(18, 189)
        Me.BtnMenu2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnMenu2.Name = "BtnMenu2"
        Me.BtnMenu2.Size = New System.Drawing.Size(146, 72)
        Me.BtnMenu2.TabIndex = 2
        Me.BtnMenu2.Text = "2"
        Me.BtnMenu2.UseVisualStyleBackColor = False
        '
        'BtnMenu3
        '
        Me.BtnMenu3.BackColor = System.Drawing.Color.Silver
        Me.BtnMenu3.Location = New System.Drawing.Point(18, 271)
        Me.BtnMenu3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnMenu3.Name = "BtnMenu3"
        Me.BtnMenu3.Size = New System.Drawing.Size(146, 72)
        Me.BtnMenu3.TabIndex = 3
        Me.BtnMenu3.Text = "3"
        Me.BtnMenu3.UseVisualStyleBackColor = False
        '
        'BtnMenuBack
        '
        Me.BtnMenuBack.BackColor = System.Drawing.Color.Silver
        Me.BtnMenuBack.Location = New System.Drawing.Point(18, 352)
        Me.BtnMenuBack.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnMenuBack.Name = "BtnMenuBack"
        Me.BtnMenuBack.Size = New System.Drawing.Size(146, 72)
        Me.BtnMenuBack.TabIndex = 4
        Me.BtnMenuBack.Text = "Back"
        Me.BtnMenuBack.UseVisualStyleBackColor = False
        '
        'FrmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(264, 455)
        Me.Controls.Add(Me.BtnMenuBack)
        Me.Controls.Add(Me.BtnMenu3)
        Me.Controls.Add(Me.BtnMenu2)
        Me.Controls.Add(Me.BtnMenu1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "FrmMenu"
        Me.Text = "Platformer Engine"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnMenu1 As System.Windows.Forms.Button
    Friend WithEvents BtnMenu2 As System.Windows.Forms.Button
    Friend WithEvents BtnMenu3 As System.Windows.Forms.Button
    Friend WithEvents BtnMenuBack As System.Windows.Forms.Button

End Class
