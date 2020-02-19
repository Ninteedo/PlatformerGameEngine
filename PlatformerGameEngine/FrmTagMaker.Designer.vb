<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTagMaker
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
        Me.BtnFinish = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.TxtJson = New System.Windows.Forms.TextBox()
        Me.TblOverall = New System.Windows.Forms.TableLayoutPanel()
        Me.FlwCancelDone = New System.Windows.Forms.FlowLayoutPanel()
        Me.TblOverall.SuspendLayout()
        Me.FlwCancelDone.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnFinish
        '
        Me.BtnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFinish.Location = New System.Drawing.Point(109, 3)
        Me.BtnFinish.Name = "BtnFinish"
        Me.BtnFinish.Size = New System.Drawing.Size(100, 70)
        Me.BtnFinish.TabIndex = 7
        Me.BtnFinish.Text = "Done"
        Me.BtnFinish.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Location = New System.Drawing.Point(3, 3)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(100, 70)
        Me.BtnCancel.TabIndex = 6
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'TxtJson
        '
        Me.TxtJson.AcceptsReturn = True
        Me.TxtJson.AcceptsTab = True
        Me.TxtJson.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtJson.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJson.Location = New System.Drawing.Point(3, 3)
        Me.TxtJson.Multiline = True
        Me.TxtJson.Name = "TxtJson"
        Me.TxtJson.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtJson.Size = New System.Drawing.Size(924, 438)
        Me.TxtJson.TabIndex = 8
        '
        'TblOverall
        '
        Me.TblOverall.ColumnCount = 1
        Me.TblOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TblOverall.Controls.Add(Me.FlwCancelDone, 0, 1)
        Me.TblOverall.Controls.Add(Me.TxtJson, 0, 0)
        Me.TblOverall.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TblOverall.Location = New System.Drawing.Point(0, 0)
        Me.TblOverall.Name = "TblOverall"
        Me.TblOverall.RowCount = 2
        Me.TblOverall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TblOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblOverall.Size = New System.Drawing.Size(930, 528)
        Me.TblOverall.TabIndex = 9
        '
        'FlwCancelDone
        '
        Me.FlwCancelDone.Controls.Add(Me.BtnCancel)
        Me.FlwCancelDone.Controls.Add(Me.BtnFinish)
        Me.FlwCancelDone.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlwCancelDone.Location = New System.Drawing.Point(708, 447)
        Me.FlwCancelDone.Name = "FlwCancelDone"
        Me.FlwCancelDone.Size = New System.Drawing.Size(219, 78)
        Me.FlwCancelDone.TabIndex = 10
        '
        'FrmTagMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(930, 528)
        Me.Controls.Add(Me.TblOverall)
        Me.Name = "FrmTagMaker"
        Me.Text = "Tag Maker"
        Me.TblOverall.ResumeLayout(False)
        Me.TblOverall.PerformLayout()
        Me.FlwCancelDone.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnFinish As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents TxtJson As TextBox
    Friend WithEvents TblOverall As TableLayoutPanel
    Friend WithEvents FlwCancelDone As FlowLayoutPanel
End Class
