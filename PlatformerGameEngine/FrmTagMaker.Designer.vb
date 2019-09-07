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
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lstArguments = New System.Windows.Forms.ListBox()
        Me.lblNameTitle = New System.Windows.Forms.Label()
        Me.lblArgumentsTitle = New System.Windows.Forms.Label()
        Me.lblDataTypeTitle = New System.Windows.Forms.Label()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.btnArgAdd = New System.Windows.Forms.Button()
        Me.btnArgRemove = New System.Windows.Forms.Button()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnBasicEditor = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.Location = New System.Drawing.Point(13, 32)
        Me.txtName.MaxLength = 32
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(280, 26)
        Me.txtName.TabIndex = 1
        '
        'lstArguments
        '
        Me.lstArguments.FormattingEnabled = True
        Me.lstArguments.ItemHeight = 20
        Me.lstArguments.Location = New System.Drawing.Point(12, 84)
        Me.lstArguments.Name = "lstArguments"
        Me.lstArguments.Size = New System.Drawing.Size(281, 184)
        Me.lstArguments.TabIndex = 2
        '
        'lblNameTitle
        '
        Me.lblNameTitle.AutoSize = True
        Me.lblNameTitle.Location = New System.Drawing.Point(13, 6)
        Me.lblNameTitle.Name = "lblNameTitle"
        Me.lblNameTitle.Size = New System.Drawing.Size(51, 20)
        Me.lblNameTitle.TabIndex = 0
        Me.lblNameTitle.Text = "Name"
        '
        'lblArgumentsTitle
        '
        Me.lblArgumentsTitle.AutoSize = True
        Me.lblArgumentsTitle.Location = New System.Drawing.Point(13, 61)
        Me.lblArgumentsTitle.Name = "lblArgumentsTitle"
        Me.lblArgumentsTitle.Size = New System.Drawing.Size(87, 20)
        Me.lblArgumentsTitle.TabIndex = 0
        Me.lblArgumentsTitle.Text = "Arguments"
        '
        'lblDataTypeTitle
        '
        Me.lblDataTypeTitle.AutoSize = True
        Me.lblDataTypeTitle.Location = New System.Drawing.Point(295, 61)
        Me.lblDataTypeTitle.Name = "lblDataTypeTitle"
        Me.lblDataTypeTitle.Size = New System.Drawing.Size(82, 20)
        Me.lblDataTypeTitle.TabIndex = 0
        Me.lblDataTypeTitle.Text = "Data Type"
        '
        'cmbDataType
        '
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(299, 84)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(121, 28)
        Me.cmbDataType.TabIndex = 3
        '
        'btnArgAdd
        '
        Me.btnArgAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnArgAdd.Location = New System.Drawing.Point(299, 118)
        Me.btnArgAdd.Name = "btnArgAdd"
        Me.btnArgAdd.Size = New System.Drawing.Size(100, 70)
        Me.btnArgAdd.TabIndex = 4
        Me.btnArgAdd.Text = "Add Argument"
        Me.btnArgAdd.UseVisualStyleBackColor = True
        '
        'btnArgRemove
        '
        Me.btnArgRemove.Enabled = False
        Me.btnArgRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnArgRemove.Location = New System.Drawing.Point(299, 194)
        Me.btnArgRemove.Name = "btnArgRemove"
        Me.btnArgRemove.Size = New System.Drawing.Size(100, 70)
        Me.btnArgRemove.TabIndex = 5
        Me.btnArgRemove.Text = "Remove Argument"
        Me.btnArgRemove.UseVisualStyleBackColor = True
        '
        'btnFinish
        '
        Me.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFinish.Location = New System.Drawing.Point(427, 194)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(100, 70)
        Me.btnFinish.TabIndex = 7
        Me.btnFinish.Text = "Done"
        Me.btnFinish.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(427, 118)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 70)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnBasicEditor
        '
        Me.btnBasicEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBasicEditor.Location = New System.Drawing.Point(423, 6)
        Me.btnBasicEditor.Name = "btnBasicEditor"
        Me.btnBasicEditor.Size = New System.Drawing.Size(100, 70)
        Me.btnBasicEditor.TabIndex = 8
        Me.btnBasicEditor.Text = "Basic Editor"
        Me.btnBasicEditor.UseVisualStyleBackColor = True
        '
        'FrmTagMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(535, 281)
        Me.Controls.Add(Me.btnBasicEditor)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnFinish)
        Me.Controls.Add(Me.btnArgRemove)
        Me.Controls.Add(Me.btnArgAdd)
        Me.Controls.Add(Me.cmbDataType)
        Me.Controls.Add(Me.lblDataTypeTitle)
        Me.Controls.Add(Me.lblArgumentsTitle)
        Me.Controls.Add(Me.lblNameTitle)
        Me.Controls.Add(Me.lstArguments)
        Me.Controls.Add(Me.txtName)
        Me.Name = "FrmTagMaker"
        Me.Text = "Tag Maker"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtName As TextBox
    Friend WithEvents lstArguments As ListBox
    Friend WithEvents lblNameTitle As Label
    Friend WithEvents lblArgumentsTitle As Label
    Friend WithEvents lblDataTypeTitle As Label
    Friend WithEvents cmbDataType As ComboBox
    Friend WithEvents btnArgAdd As Button
    Friend WithEvents btnArgRemove As Button
    Friend WithEvents btnFinish As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnBasicEditor As Button
End Class
