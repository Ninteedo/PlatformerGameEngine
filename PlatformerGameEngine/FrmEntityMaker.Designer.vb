<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEntityMaker
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
        Me.pnlFramePreview = New System.Windows.Forms.Panel()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnSaveAs = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lstTags = New System.Windows.Forms.ListBox()
        Me.lblTagListTitle = New System.Windows.Forms.Label()
        Me.lblFrameListTitle = New System.Windows.Forms.Label()
        Me.lstFrames = New System.Windows.Forms.ListBox()
        Me.lblSpriteListTitle = New System.Windows.Forms.Label()
        Me.lstSprites = New System.Windows.Forms.ListBox()
        Me.btnSpriteLoad = New System.Windows.Forms.Button()
        Me.btnFrameNew = New System.Windows.Forms.Button()
        Me.btnFrameRemove = New System.Windows.Forms.Button()
        Me.btnTagsEdit = New System.Windows.Forms.Button()
        Me.btnFrameAddSprite = New System.Windows.Forms.Button()
        Me.numFrameIndex = New System.Windows.Forms.NumericUpDown()
        Me.lblFrameIndex = New System.Windows.Forms.Label()
        Me.btnTagsNew = New System.Windows.Forms.Button()
        Me.btnRedraw = New System.Windows.Forms.Button()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.flwSaveLoad = New System.Windows.Forms.FlowLayoutPanel()
        Me.tblControlLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.flwNameEdit = New System.Windows.Forms.FlowLayoutPanel()
        Me.flwFrameIndex = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnTagRemove = New System.Windows.Forms.Button()
        CType(Me.numFrameIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flwSaveLoad.SuspendLayout()
        Me.tblControlLayout.SuspendLayout()
        Me.flwNameEdit.SuspendLayout()
        Me.flwFrameIndex.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlFramePreview
        '
        Me.pnlFramePreview.BackColor = System.Drawing.Color.Black
        Me.pnlFramePreview.Location = New System.Drawing.Point(14, 12)
        Me.pnlFramePreview.MaximumSize = New System.Drawing.Size(320, 320)
        Me.pnlFramePreview.MinimumSize = New System.Drawing.Size(320, 320)
        Me.pnlFramePreview.Name = "pnlFramePreview"
        Me.pnlFramePreview.Size = New System.Drawing.Size(320, 320)
        Me.pnlFramePreview.TabIndex = 0
        '
        'btnOpen
        '
        Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpen.Location = New System.Drawing.Point(3, 3)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(140, 69)
        Me.btnOpen.TabIndex = 1
        Me.btnOpen.Text = "Open..."
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnSaveAs
        '
        Me.btnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveAs.Location = New System.Drawing.Point(149, 3)
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(140, 69)
        Me.btnSaveAs.TabIndex = 2
        Me.btnSaveAs.Text = "Save As..."
        Me.btnSaveAs.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(295, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(140, 69)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lstTags
        '
        Me.lstTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstTags.FormattingEnabled = True
        Me.lstTags.ItemHeight = 20
        Me.lstTags.Location = New System.Drawing.Point(3, 103)
        Me.lstTags.Name = "lstTags"
        Me.lstTags.Size = New System.Drawing.Size(150, 320)
        Me.lstTags.TabIndex = 4
        '
        'lblTagListTitle
        '
        Me.lblTagListTitle.AutoSize = True
        Me.lblTagListTitle.Location = New System.Drawing.Point(3, 80)
        Me.lblTagListTitle.Name = "lblTagListTitle"
        Me.lblTagListTitle.Size = New System.Drawing.Size(44, 20)
        Me.lblTagListTitle.TabIndex = 0
        Me.lblTagListTitle.Text = "Tags"
        '
        'lblFrameListTitle
        '
        Me.lblFrameListTitle.AutoSize = True
        Me.lblFrameListTitle.Location = New System.Drawing.Point(159, 80)
        Me.lblFrameListTitle.Name = "lblFrameListTitle"
        Me.lblFrameListTitle.Size = New System.Drawing.Size(63, 20)
        Me.lblFrameListTitle.TabIndex = 0
        Me.lblFrameListTitle.Text = "Frames"
        '
        'lstFrames
        '
        Me.lstFrames.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstFrames.FormattingEnabled = True
        Me.lstFrames.ItemHeight = 20
        Me.lstFrames.Location = New System.Drawing.Point(159, 103)
        Me.lstFrames.Name = "lstFrames"
        Me.lstFrames.Size = New System.Drawing.Size(150, 320)
        Me.lstFrames.TabIndex = 6
        '
        'lblSpriteListTitle
        '
        Me.lblSpriteListTitle.AutoSize = True
        Me.lblSpriteListTitle.Location = New System.Drawing.Point(315, 80)
        Me.lblSpriteListTitle.Name = "lblSpriteListTitle"
        Me.lblSpriteListTitle.Size = New System.Drawing.Size(59, 20)
        Me.lblSpriteListTitle.TabIndex = 8
        Me.lblSpriteListTitle.Text = "Sprites"
        '
        'lstSprites
        '
        Me.lstSprites.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstSprites.FormattingEnabled = True
        Me.lstSprites.ItemHeight = 20
        Me.lstSprites.Location = New System.Drawing.Point(315, 103)
        Me.lstSprites.Name = "lstSprites"
        Me.lstSprites.Size = New System.Drawing.Size(150, 320)
        Me.lstSprites.TabIndex = 7
        '
        'btnSpriteLoad
        '
        Me.btnSpriteLoad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSpriteLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSpriteLoad.Location = New System.Drawing.Point(315, 429)
        Me.btnSpriteLoad.Name = "btnSpriteLoad"
        Me.btnSpriteLoad.Size = New System.Drawing.Size(150, 69)
        Me.btnSpriteLoad.TabIndex = 9
        Me.btnSpriteLoad.Text = "Load..."
        Me.btnSpriteLoad.UseVisualStyleBackColor = True
        '
        'btnFrameNew
        '
        Me.btnFrameNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFrameNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrameNew.Location = New System.Drawing.Point(159, 429)
        Me.btnFrameNew.Name = "btnFrameNew"
        Me.btnFrameNew.Size = New System.Drawing.Size(150, 69)
        Me.btnFrameNew.TabIndex = 10
        Me.btnFrameNew.Text = "New Frame"
        Me.btnFrameNew.UseVisualStyleBackColor = True
        '
        'btnFrameRemove
        '
        Me.btnFrameRemove.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFrameRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrameRemove.Location = New System.Drawing.Point(159, 504)
        Me.btnFrameRemove.Name = "btnFrameRemove"
        Me.btnFrameRemove.Size = New System.Drawing.Size(150, 69)
        Me.btnFrameRemove.TabIndex = 11
        Me.btnFrameRemove.Text = "Remove Frame"
        Me.btnFrameRemove.UseVisualStyleBackColor = True
        '
        'btnTagsEdit
        '
        Me.btnTagsEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTagsEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagsEdit.Location = New System.Drawing.Point(3, 504)
        Me.btnTagsEdit.Name = "btnTagsEdit"
        Me.btnTagsEdit.Size = New System.Drawing.Size(150, 69)
        Me.btnTagsEdit.TabIndex = 12
        Me.btnTagsEdit.Text = "Edit Tag"
        Me.btnTagsEdit.UseVisualStyleBackColor = True
        '
        'btnFrameAddSprite
        '
        Me.btnFrameAddSprite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFrameAddSprite.Enabled = False
        Me.btnFrameAddSprite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrameAddSprite.Location = New System.Drawing.Point(159, 579)
        Me.btnFrameAddSprite.Name = "btnFrameAddSprite"
        Me.btnFrameAddSprite.Size = New System.Drawing.Size(150, 69)
        Me.btnFrameAddSprite.TabIndex = 13
        Me.btnFrameAddSprite.Text = "Add Sprite"
        Me.btnFrameAddSprite.UseVisualStyleBackColor = True
        '
        'numFrameIndex
        '
        Me.numFrameIndex.Location = New System.Drawing.Point(27, 3)
        Me.numFrameIndex.Name = "numFrameIndex"
        Me.numFrameIndex.Size = New System.Drawing.Size(74, 26)
        Me.numFrameIndex.TabIndex = 14
        '
        'lblFrameIndex
        '
        Me.lblFrameIndex.AutoSize = True
        Me.lblFrameIndex.Location = New System.Drawing.Point(3, 0)
        Me.lblFrameIndex.Name = "lblFrameIndex"
        Me.lblFrameIndex.Size = New System.Drawing.Size(18, 20)
        Me.lblFrameIndex.TabIndex = 15
        Me.lblFrameIndex.Text = "#"
        '
        'btnTagsNew
        '
        Me.btnTagsNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTagsNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagsNew.Location = New System.Drawing.Point(3, 429)
        Me.btnTagsNew.Name = "btnTagsNew"
        Me.btnTagsNew.Size = New System.Drawing.Size(150, 69)
        Me.btnTagsNew.TabIndex = 16
        Me.btnTagsNew.Text = "New Tag"
        Me.btnTagsNew.UseVisualStyleBackColor = True
        '
        'btnRedraw
        '
        Me.btnRedraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRedraw.Location = New System.Drawing.Point(471, 3)
        Me.btnRedraw.Name = "btnRedraw"
        Me.btnRedraw.Size = New System.Drawing.Size(140, 69)
        Me.btnRedraw.TabIndex = 17
        Me.btnRedraw.Text = "Redraw"
        Me.btnRedraw.UseVisualStyleBackColor = True
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(3, 0)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(51, 20)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Name"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(60, 3)
        Me.txtName.MaxLength = 32
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(162, 26)
        Me.txtName.TabIndex = 18
        '
        'flwSaveLoad
        '
        Me.tblControlLayout.SetColumnSpan(Me.flwSaveLoad, 3)
        Me.flwSaveLoad.Controls.Add(Me.btnOpen)
        Me.flwSaveLoad.Controls.Add(Me.btnSaveAs)
        Me.flwSaveLoad.Controls.Add(Me.btnSave)
        Me.flwSaveLoad.Location = New System.Drawing.Point(3, 3)
        Me.flwSaveLoad.Name = "flwSaveLoad"
        Me.flwSaveLoad.Size = New System.Drawing.Size(462, 74)
        Me.flwSaveLoad.TabIndex = 19
        '
        'tblControlLayout
        '
        Me.tblControlLayout.ColumnCount = 4
        Me.tblControlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblControlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblControlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblControlLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblControlLayout.Controls.Add(Me.flwNameEdit, 3, 2)
        Me.tblControlLayout.Controls.Add(Me.flwFrameIndex, 1, 6)
        Me.tblControlLayout.Controls.Add(Me.flwSaveLoad, 0, 0)
        Me.tblControlLayout.Controls.Add(Me.lblTagListTitle, 0, 1)
        Me.tblControlLayout.Controls.Add(Me.lstTags, 0, 2)
        Me.tblControlLayout.Controls.Add(Me.btnSpriteLoad, 2, 3)
        Me.tblControlLayout.Controls.Add(Me.btnTagsNew, 0, 3)
        Me.tblControlLayout.Controls.Add(Me.btnFrameAddSprite, 1, 5)
        Me.tblControlLayout.Controls.Add(Me.btnTagsEdit, 0, 4)
        Me.tblControlLayout.Controls.Add(Me.btnFrameRemove, 1, 4)
        Me.tblControlLayout.Controls.Add(Me.lblFrameListTitle, 1, 1)
        Me.tblControlLayout.Controls.Add(Me.btnFrameNew, 1, 3)
        Me.tblControlLayout.Controls.Add(Me.lblSpriteListTitle, 2, 1)
        Me.tblControlLayout.Controls.Add(Me.lstFrames, 1, 2)
        Me.tblControlLayout.Controls.Add(Me.lstSprites, 2, 2)
        Me.tblControlLayout.Controls.Add(Me.btnRedraw, 3, 0)
        Me.tblControlLayout.Controls.Add(Me.btnTagRemove, 0, 5)
        Me.tblControlLayout.Location = New System.Drawing.Point(340, 17)
        Me.tblControlLayout.Name = "tblControlLayout"
        Me.tblControlLayout.RowCount = 7
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblControlLayout.Size = New System.Drawing.Size(829, 741)
        Me.tblControlLayout.TabIndex = 20
        '
        'flwNameEdit
        '
        Me.flwNameEdit.Controls.Add(Me.lblName)
        Me.flwNameEdit.Controls.Add(Me.txtName)
        Me.flwNameEdit.Location = New System.Drawing.Point(471, 103)
        Me.flwNameEdit.Name = "flwNameEdit"
        Me.flwNameEdit.Size = New System.Drawing.Size(235, 90)
        Me.flwNameEdit.TabIndex = 21
        '
        'flwFrameIndex
        '
        Me.flwFrameIndex.Controls.Add(Me.lblFrameIndex)
        Me.flwFrameIndex.Controls.Add(Me.numFrameIndex)
        Me.flwFrameIndex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flwFrameIndex.Location = New System.Drawing.Point(159, 654)
        Me.flwFrameIndex.Name = "flwFrameIndex"
        Me.flwFrameIndex.Size = New System.Drawing.Size(150, 84)
        Me.flwFrameIndex.TabIndex = 21
        '
        'btnTagRemove
        '
        Me.btnTagRemove.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTagRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagRemove.Location = New System.Drawing.Point(3, 579)
        Me.btnTagRemove.Name = "btnTagRemove"
        Me.btnTagRemove.Size = New System.Drawing.Size(150, 69)
        Me.btnTagRemove.TabIndex = 22
        Me.btnTagRemove.Text = "Remove Tag"
        Me.btnTagRemove.UseVisualStyleBackColor = True
        '
        'FrmEntityMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(1181, 770)
        Me.Controls.Add(Me.tblControlLayout)
        Me.Controls.Add(Me.pnlFramePreview)
        Me.DoubleBuffered = True
        Me.Name = "FrmEntityMaker"
        Me.Text = "Entity Maker"
        CType(Me.numFrameIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flwSaveLoad.ResumeLayout(False)
        Me.tblControlLayout.ResumeLayout(False)
        Me.tblControlLayout.PerformLayout()
        Me.flwNameEdit.ResumeLayout(False)
        Me.flwNameEdit.PerformLayout()
        Me.flwFrameIndex.ResumeLayout(False)
        Me.flwFrameIndex.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlFramePreview As Panel
    Friend WithEvents btnOpen As Button
    Friend WithEvents btnSaveAs As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents lstTags As ListBox
    Friend WithEvents lblTagListTitle As Label
    Friend WithEvents lblFrameListTitle As Label
    Friend WithEvents lstFrames As ListBox
    Friend WithEvents lblSpriteListTitle As Label
    Friend WithEvents lstSprites As ListBox
    Friend WithEvents btnSpriteLoad As Button
    Friend WithEvents btnFrameNew As Button
    Friend WithEvents btnFrameRemove As Button
    Friend WithEvents btnTagsEdit As Button
    Friend WithEvents btnFrameAddSprite As Button
    Friend WithEvents numFrameIndex As NumericUpDown
    Friend WithEvents lblFrameIndex As Label
    Friend WithEvents btnTagsNew As Button
    Friend WithEvents btnRedraw As System.Windows.Forms.Button
    Friend WithEvents lblName As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents flwSaveLoad As FlowLayoutPanel
    Friend WithEvents tblControlLayout As TableLayoutPanel
    Friend WithEvents flwNameEdit As FlowLayoutPanel
    Friend WithEvents flwFrameIndex As FlowLayoutPanel
    Friend WithEvents btnTagRemove As Button
End Class
