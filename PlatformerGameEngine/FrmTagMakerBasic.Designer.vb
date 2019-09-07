<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTagMakerBasic
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
        Me.txtJSON = New System.Windows.Forms.TextBox()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtJSON
        '
        Me.txtJSON.AcceptsReturn = True
        Me.txtJSON.AcceptsTab = True
        Me.txtJSON.Location = New System.Drawing.Point(13, 13)
        Me.txtJSON.Multiline = True
        Me.txtJSON.Name = "txtJSON"
        Me.txtJSON.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtJSON.Size = New System.Drawing.Size(775, 344)
        Me.txtJSON.TabIndex = 1
        '
        'btnFinish
        '
        Me.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFinish.Location = New System.Drawing.Point(688, 363)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(100, 70)
        Me.btnFinish.TabIndex = 3
        Me.btnFinish.Text = "Done"
        Me.btnFinish.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(582, 363)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 70)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'FrmTagMakerBasic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnFinish)
        Me.Controls.Add(Me.txtJSON)
        Me.Name = "FrmTagMakerBasic"
        Me.Text = "FrmTagMakerBasic"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtJSON As TextBox
    Friend WithEvents btnFinish As Button
    Friend WithEvents btnCancel As Button
End Class
