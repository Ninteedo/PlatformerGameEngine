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
        Me.btnSaveAs = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.lblTemplatesList = New System.Windows.Forms.Label()
        Me.lstTemplates = New System.Windows.Forms.ListBox()
        Me.btnLoadEntity = New System.Windows.Forms.Button()
        Me.lstInstances = New System.Windows.Forms.ListBox()
        Me.lblInstancesList = New System.Windows.Forms.Label()
        Me.lblTags = New System.Windows.Forms.Label()
        Me.lblTagLocation = New System.Windows.Forms.Label()
        Me.numTagLocX = New System.Windows.Forms.NumericUpDown()
        Me.numTagLocY = New System.Windows.Forms.NumericUpDown()
        Me.numTagLayer = New System.Windows.Forms.NumericUpDown()
        Me.lblTagLayer = New System.Windows.Forms.Label()
        Me.numTagScale = New System.Windows.Forms.NumericUpDown()
        Me.lblTagScale = New System.Windows.Forms.Label()
        Me.flwSaveLoad = New System.Windows.Forms.FlowLayoutPanel()
        Me.tblTags = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTagName = New System.Windows.Forms.Label()
        Me.txtTagName = New System.Windows.Forms.TextBox()
        Me.btnInstanceCreate = New System.Windows.Forms.Button()
        Me.tblEntities = New System.Windows.Forms.TableLayoutPanel()
        Me.btnInstanceDuplicate = New System.Windows.Forms.Button()
        Me.btnInstanceDelete = New System.Windows.Forms.Button()
        Me.lstTags = New System.Windows.Forms.ListBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTagAdd = New System.Windows.Forms.Button()
        Me.btnTagEdit = New System.Windows.Forms.Button()
        Me.btnTagRemove = New System.Windows.Forms.Button()
        CType(Me.numTagLocX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numTagLocY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numTagLayer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numTagScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flwSaveLoad.SuspendLayout()
        Me.tblTags.SuspendLayout()
        Me.tblEntities.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.btnSave.Location = New System.Drawing.Point(215, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 69)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnSaveAs
        '
        Me.btnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveAs.Location = New System.Drawing.Point(109, 3)
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(100, 69)
        Me.btnSaveAs.TabIndex = 2
        Me.btnSaveAs.Text = "Save As..."
        Me.btnSaveAs.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpen.Location = New System.Drawing.Point(3, 3)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(100, 69)
        Me.btnOpen.TabIndex = 1
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'lblTemplatesList
        '
        Me.lblTemplatesList.AutoSize = True
        Me.lblTemplatesList.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTemplatesList.Location = New System.Drawing.Point(3, 0)
        Me.lblTemplatesList.Name = "lblTemplatesList"
        Me.lblTemplatesList.Size = New System.Drawing.Size(104, 25)
        Me.lblTemplatesList.TabIndex = 0
        Me.lblTemplatesList.Text = "Templates"
        '
        'lstTemplates
        '
        Me.lstTemplates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstTemplates.FormattingEnabled = True
        Me.lstTemplates.ItemHeight = 20
        Me.lstTemplates.Location = New System.Drawing.Point(3, 28)
        Me.lstTemplates.Name = "lstTemplates"
        Me.lstTemplates.Size = New System.Drawing.Size(151, 480)
        Me.lstTemplates.TabIndex = 4
        '
        'btnLoadEntity
        '
        Me.btnLoadEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadEntity.Location = New System.Drawing.Point(3, 514)
        Me.btnLoadEntity.Name = "btnLoadEntity"
        Me.btnLoadEntity.Size = New System.Drawing.Size(151, 69)
        Me.btnLoadEntity.TabIndex = 5
        Me.btnLoadEntity.Text = "Load Entity..."
        Me.btnLoadEntity.UseVisualStyleBackColor = True
        '
        'lstInstances
        '
        Me.lstInstances.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstInstances.FormattingEnabled = True
        Me.lstInstances.ItemHeight = 20
        Me.lstInstances.Location = New System.Drawing.Point(172, 28)
        Me.lstInstances.Name = "lstInstances"
        Me.lstInstances.Size = New System.Drawing.Size(151, 480)
        Me.lstInstances.TabIndex = 7
        '
        'lblInstancesList
        '
        Me.lblInstancesList.AutoSize = True
        Me.lblInstancesList.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstancesList.Location = New System.Drawing.Point(172, 0)
        Me.lblInstancesList.Name = "lblInstancesList"
        Me.lblInstancesList.Size = New System.Drawing.Size(96, 25)
        Me.lblInstancesList.TabIndex = 6
        Me.lblInstancesList.Text = "Instances"
        '
        'lblTags
        '
        Me.lblTags.AutoSize = True
        Me.tblTags.SetColumnSpan(Me.lblTags, 2)
        Me.lblTags.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTags.Location = New System.Drawing.Point(3, 0)
        Me.lblTags.Name = "lblTags"
        Me.lblTags.Size = New System.Drawing.Size(57, 25)
        Me.lblTags.TabIndex = 8
        Me.lblTags.Text = "Tags"
        '
        'lblTagLocation
        '
        Me.lblTagLocation.AutoSize = True
        Me.lblTagLocation.Location = New System.Drawing.Point(3, 72)
        Me.lblTagLocation.Name = "lblTagLocation"
        Me.lblTagLocation.Size = New System.Drawing.Size(70, 20)
        Me.lblTagLocation.TabIndex = 9
        Me.lblTagLocation.Text = "Location"
        '
        'numTagLocX
        '
        Me.numTagLocX.Location = New System.Drawing.Point(79, 75)
        Me.numTagLocX.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.numTagLocX.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.numTagLocX.Name = "numTagLocX"
        Me.numTagLocX.Size = New System.Drawing.Size(82, 26)
        Me.numTagLocX.TabIndex = 10
        '
        'numTagLocY
        '
        Me.numTagLocY.Location = New System.Drawing.Point(167, 75)
        Me.numTagLocY.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.numTagLocY.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.numTagLocY.Name = "numTagLocY"
        Me.numTagLocY.Size = New System.Drawing.Size(82, 26)
        Me.numTagLocY.TabIndex = 11
        '
        'numTagLayer
        '
        Me.numTagLayer.Location = New System.Drawing.Point(79, 122)
        Me.numTagLayer.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.numTagLayer.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.numTagLayer.Name = "numTagLayer"
        Me.numTagLayer.Size = New System.Drawing.Size(82, 26)
        Me.numTagLayer.TabIndex = 13
        '
        'lblTagLayer
        '
        Me.lblTagLayer.AutoSize = True
        Me.lblTagLayer.Location = New System.Drawing.Point(3, 119)
        Me.lblTagLayer.Name = "lblTagLayer"
        Me.lblTagLayer.Size = New System.Drawing.Size(48, 20)
        Me.lblTagLayer.TabIndex = 12
        Me.lblTagLayer.Text = "Layer"
        '
        'numTagScale
        '
        Me.numTagScale.DecimalPlaces = 2
        Me.numTagScale.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.numTagScale.Location = New System.Drawing.Point(79, 169)
        Me.numTagScale.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numTagScale.Minimum = New Decimal(New Integer() {256, 0, 0, -2147483648})
        Me.numTagScale.Name = "numTagScale"
        Me.numTagScale.Size = New System.Drawing.Size(82, 26)
        Me.numTagScale.TabIndex = 15
        Me.numTagScale.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblTagScale
        '
        Me.lblTagScale.AutoSize = True
        Me.lblTagScale.Location = New System.Drawing.Point(3, 166)
        Me.lblTagScale.Name = "lblTagScale"
        Me.lblTagScale.Size = New System.Drawing.Size(49, 20)
        Me.lblTagScale.TabIndex = 14
        Me.lblTagScale.Text = "Scale"
        '
        'flwSaveLoad
        '
        Me.flwSaveLoad.Controls.Add(Me.btnOpen)
        Me.flwSaveLoad.Controls.Add(Me.btnSaveAs)
        Me.flwSaveLoad.Controls.Add(Me.btnSave)
        Me.flwSaveLoad.Location = New System.Drawing.Point(818, 12)
        Me.flwSaveLoad.Name = "flwSaveLoad"
        Me.flwSaveLoad.Size = New System.Drawing.Size(319, 76)
        Me.flwSaveLoad.TabIndex = 16
        '
        'tblTags
        '
        Me.tblTags.ColumnCount = 3
        Me.tblTags.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTags.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTags.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTags.Controls.Add(Me.lblTagLocation, 0, 2)
        Me.tblTags.Controls.Add(Me.lblTagLayer, 0, 3)
        Me.tblTags.Controls.Add(Me.numTagLocY, 2, 2)
        Me.tblTags.Controls.Add(Me.numTagLayer, 1, 3)
        Me.tblTags.Controls.Add(Me.lblTags, 0, 0)
        Me.tblTags.Controls.Add(Me.numTagLocX, 1, 2)
        Me.tblTags.Controls.Add(Me.numTagScale, 1, 4)
        Me.tblTags.Controls.Add(Me.lblTagScale, 0, 4)
        Me.tblTags.Controls.Add(Me.lblTagName, 0, 1)
        Me.tblTags.Controls.Add(Me.txtTagName, 1, 1)
        Me.tblTags.Location = New System.Drawing.Point(1168, 104)
        Me.tblTags.Name = "tblTags"
        Me.tblTags.RowCount = 5
        Me.tblTags.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTags.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblTags.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblTags.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblTags.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblTags.Size = New System.Drawing.Size(381, 215)
        Me.tblTags.TabIndex = 17
        '
        'lblTagName
        '
        Me.lblTagName.AutoSize = True
        Me.lblTagName.Location = New System.Drawing.Point(3, 25)
        Me.lblTagName.Name = "lblTagName"
        Me.lblTagName.Size = New System.Drawing.Size(51, 20)
        Me.lblTagName.TabIndex = 16
        Me.lblTagName.Text = "Name"
        '
        'txtTagName
        '
        Me.tblTags.SetColumnSpan(Me.txtTagName, 2)
        Me.txtTagName.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtTagName.Location = New System.Drawing.Point(79, 28)
        Me.txtTagName.MaxLength = 32
        Me.txtTagName.Name = "txtTagName"
        Me.txtTagName.Size = New System.Drawing.Size(299, 26)
        Me.txtTagName.TabIndex = 17
        '
        'btnInstanceCreate
        '
        Me.btnInstanceCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstanceCreate.Location = New System.Drawing.Point(172, 514)
        Me.btnInstanceCreate.Name = "btnInstanceCreate"
        Me.btnInstanceCreate.Size = New System.Drawing.Size(151, 69)
        Me.btnInstanceCreate.TabIndex = 18
        Me.btnInstanceCreate.Text = "Create Instance"
        Me.btnInstanceCreate.UseVisualStyleBackColor = True
        '
        'tblEntities
        '
        Me.tblEntities.ColumnCount = 2
        Me.tblEntities.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblEntities.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblEntities.Controls.Add(Me.btnLoadEntity, 0, 2)
        Me.tblEntities.Controls.Add(Me.btnInstanceCreate, 1, 2)
        Me.tblEntities.Controls.Add(Me.lstTemplates, 0, 1)
        Me.tblEntities.Controls.Add(Me.lstInstances, 1, 1)
        Me.tblEntities.Controls.Add(Me.lblInstancesList, 1, 0)
        Me.tblEntities.Controls.Add(Me.lblTemplatesList, 0, 0)
        Me.tblEntities.Controls.Add(Me.btnInstanceDuplicate, 1, 3)
        Me.tblEntities.Controls.Add(Me.btnInstanceDelete, 1, 4)
        Me.tblEntities.Location = New System.Drawing.Point(818, 104)
        Me.tblEntities.Name = "tblEntities"
        Me.tblEntities.RowCount = 5
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.Size = New System.Drawing.Size(339, 737)
        Me.tblEntities.TabIndex = 19
        '
        'btnInstanceDuplicate
        '
        Me.btnInstanceDuplicate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstanceDuplicate.Location = New System.Drawing.Point(172, 589)
        Me.btnInstanceDuplicate.Name = "btnInstanceDuplicate"
        Me.btnInstanceDuplicate.Size = New System.Drawing.Size(151, 69)
        Me.btnInstanceDuplicate.TabIndex = 19
        Me.btnInstanceDuplicate.Text = "Duplicate"
        Me.btnInstanceDuplicate.UseVisualStyleBackColor = True
        '
        'btnInstanceDelete
        '
        Me.btnInstanceDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstanceDelete.Location = New System.Drawing.Point(172, 664)
        Me.btnInstanceDelete.Name = "btnInstanceDelete"
        Me.btnInstanceDelete.Size = New System.Drawing.Size(151, 69)
        Me.btnInstanceDelete.TabIndex = 20
        Me.btnInstanceDelete.Text = "Delete Instance"
        Me.btnInstanceDelete.UseVisualStyleBackColor = True
        '
        'lstTags
        '
        Me.lstTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstTags.FormattingEnabled = True
        Me.lstTags.ItemHeight = 20
        Me.lstTags.Location = New System.Drawing.Point(3, 3)
        Me.lstTags.Name = "lstTags"
        Me.TableLayoutPanel1.SetRowSpan(Me.lstTags, 3)
        Me.lstTags.Size = New System.Drawing.Size(184, 500)
        Me.lstTags.TabIndex = 21
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnTagAdd, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lstTags, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnTagEdit, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnTagRemove, 1, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(1168, 326)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(381, 511)
        Me.TableLayoutPanel1.TabIndex = 22
        '
        'btnTagAdd
        '
        Me.btnTagAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagAdd.Location = New System.Drawing.Point(193, 3)
        Me.btnTagAdd.Name = "btnTagAdd"
        Me.btnTagAdd.Size = New System.Drawing.Size(151, 69)
        Me.btnTagAdd.TabIndex = 21
        Me.btnTagAdd.Text = "Add Tag"
        Me.btnTagAdd.UseVisualStyleBackColor = True
        '
        'btnTagEdit
        '
        Me.btnTagEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagEdit.Location = New System.Drawing.Point(193, 78)
        Me.btnTagEdit.Name = "btnTagEdit"
        Me.btnTagEdit.Size = New System.Drawing.Size(151, 69)
        Me.btnTagEdit.TabIndex = 22
        Me.btnTagEdit.Text = "Edit Tag"
        Me.btnTagEdit.UseVisualStyleBackColor = True
        '
        'btnTagRemove
        '
        Me.btnTagRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagRemove.Location = New System.Drawing.Point(193, 153)
        Me.btnTagRemove.Name = "btnTagRemove"
        Me.btnTagRemove.Size = New System.Drawing.Size(151, 69)
        Me.btnTagRemove.TabIndex = 23
        Me.btnTagRemove.Text = "Remove Tag"
        Me.btnTagRemove.UseVisualStyleBackColor = True
        '
        'FrmLevelEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(1647, 874)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.tblEntities)
        Me.Controls.Add(Me.tblTags)
        Me.Controls.Add(Me.flwSaveLoad)
        Me.Controls.Add(Me.pnlGame)
        Me.Name = "FrmLevelEditor"
        Me.Text = "Level Editor"
        CType(Me.numTagLocX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numTagLocY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numTagLayer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numTagScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flwSaveLoad.ResumeLayout(False)
        Me.tblTags.ResumeLayout(False)
        Me.tblTags.PerformLayout()
        Me.tblEntities.ResumeLayout(False)
        Me.tblEntities.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlGame As Panel
    Friend WithEvents btnSave As Button
    Friend WithEvents btnSaveAs As Button
    Friend WithEvents btnOpen As Button
    Friend WithEvents lblTemplatesList As Label
    Friend WithEvents lstTemplates As ListBox
    Friend WithEvents btnLoadEntity As Button
    Friend WithEvents lstInstances As ListBox
    Friend WithEvents lblInstancesList As Label
    Friend WithEvents lblTags As Label
    Friend WithEvents lblTagLocation As Label
    Friend WithEvents numTagLocX As NumericUpDown
    Friend WithEvents numTagLocY As NumericUpDown
    Friend WithEvents numTagLayer As NumericUpDown
    Friend WithEvents lblTagLayer As Label
    Friend WithEvents numTagScale As NumericUpDown
    Friend WithEvents lblTagScale As Label
    Friend WithEvents flwSaveLoad As FlowLayoutPanel
    Friend WithEvents tblTags As TableLayoutPanel
    Friend WithEvents lblTagName As Label
    Friend WithEvents txtTagName As TextBox
    Friend WithEvents btnInstanceCreate As Button
    Friend WithEvents tblEntities As TableLayoutPanel
    Friend WithEvents btnInstanceDuplicate As Button
    Friend WithEvents btnInstanceDelete As Button
    Friend WithEvents lstTags As ListBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents btnTagAdd As Button
    Friend WithEvents btnTagEdit As Button
    Friend WithEvents btnTagRemove As Button
End Class
