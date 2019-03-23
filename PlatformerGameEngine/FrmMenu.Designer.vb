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
        Me.btnMenu1 = New System.Windows.Forms.Button()
        Me.btnMenu2 = New System.Windows.Forms.Button()
        Me.btnMenu3 = New System.Windows.Forms.Button()
        Me.btnMenuBack = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnMenu1
        '
        Me.btnMenu1.BackColor = System.Drawing.Color.Silver
        Me.btnMenu1.Location = New System.Drawing.Point(18, 108)
        Me.btnMenu1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnMenu1.Name = "btnMenu1"
        Me.btnMenu1.Size = New System.Drawing.Size(146, 72)
        Me.btnMenu1.TabIndex = 1
        Me.btnMenu1.Text = "1"
        Me.btnMenu1.UseVisualStyleBackColor = False
        '
        'btnMenu2
        '
        Me.btnMenu2.BackColor = System.Drawing.Color.Silver
        Me.btnMenu2.Location = New System.Drawing.Point(18, 189)
        Me.btnMenu2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnMenu2.Name = "btnMenu2"
        Me.btnMenu2.Size = New System.Drawing.Size(146, 72)
        Me.btnMenu2.TabIndex = 2
        Me.btnMenu2.Text = "2"
        Me.btnMenu2.UseVisualStyleBackColor = False
        '
        'btnMenu3
        '
        Me.btnMenu3.BackColor = System.Drawing.Color.Silver
        Me.btnMenu3.Location = New System.Drawing.Point(18, 271)
        Me.btnMenu3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnMenu3.Name = "btnMenu3"
        Me.btnMenu3.Size = New System.Drawing.Size(146, 72)
        Me.btnMenu3.TabIndex = 3
        Me.btnMenu3.Text = "3"
        Me.btnMenu3.UseVisualStyleBackColor = False
        '
        'btnMenuBack
        '
        Me.btnMenuBack.BackColor = System.Drawing.Color.Silver
        Me.btnMenuBack.Location = New System.Drawing.Point(18, 352)
        Me.btnMenuBack.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnMenuBack.Name = "btnMenuBack"
        Me.btnMenuBack.Size = New System.Drawing.Size(146, 72)
        Me.btnMenuBack.TabIndex = 4
        Me.btnMenuBack.Text = "Back"
        Me.btnMenuBack.UseVisualStyleBackColor = False
        '
        'FrmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(264, 455)
        Me.Controls.Add(Me.btnMenuBack)
        Me.Controls.Add(Me.btnMenu3)
        Me.Controls.Add(Me.btnMenu2)
        Me.Controls.Add(Me.btnMenu1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "FrmMenu"
        Me.Text = "Platformer Engine"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnMenu1 As System.Windows.Forms.Button
    Friend WithEvents btnMenu2 As System.Windows.Forms.Button
    Friend WithEvents btnMenu3 As System.Windows.Forms.Button
    Friend WithEvents btnMenuBack As System.Windows.Forms.Button

End Class
