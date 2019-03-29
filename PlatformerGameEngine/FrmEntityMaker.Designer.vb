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
        CType(Me.numFrameIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlFramePreview
        '
        Me.pnlFramePreview.BackColor = System.Drawing.Color.Black
        Me.pnlFramePreview.Location = New System.Drawing.Point(9, 8)
        Me.pnlFramePreview.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.pnlFramePreview.MaximumSize = New System.Drawing.Size(213, 208)
        Me.pnlFramePreview.MinimumSize = New System.Drawing.Size(213, 208)
        Me.pnlFramePreview.Name = "pnlFramePreview"
        Me.pnlFramePreview.Size = New System.Drawing.Size(213, 208)
        Me.pnlFramePreview.TabIndex = 0
        '
        'btnOpen
        '
        Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpen.Location = New System.Drawing.Point(227, 8)
        Me.btnOpen.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(67, 45)
        Me.btnOpen.TabIndex = 1
        Me.btnOpen.Text = "Open..."
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnSaveAs
        '
        Me.btnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveAs.Location = New System.Drawing.Point(297, 8)
        Me.btnSaveAs.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(67, 45)
        Me.btnSaveAs.TabIndex = 2
        Me.btnSaveAs.Text = "Save As..."
        Me.btnSaveAs.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(368, 8)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 45)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lstTags
        '
        Me.lstTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstTags.FormattingEnabled = True
        Me.lstTags.Location = New System.Drawing.Point(226, 73)
        Me.lstTags.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.lstTags.Name = "lstTags"
        Me.lstTags.Size = New System.Drawing.Size(80, 143)
        Me.lstTags.TabIndex = 4
        '
        'lblTagListTitle
        '
        Me.lblTagListTitle.AutoSize = True
        Me.lblTagListTitle.Location = New System.Drawing.Point(227, 58)
        Me.lblTagListTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTagListTitle.Name = "lblTagListTitle"
        Me.lblTagListTitle.Size = New System.Drawing.Size(31, 13)
        Me.lblTagListTitle.TabIndex = 0
        Me.lblTagListTitle.Text = "Tags"
        '
        'lblFrameListTitle
        '
        Me.lblFrameListTitle.AutoSize = True
        Me.lblFrameListTitle.Location = New System.Drawing.Point(311, 58)
        Me.lblFrameListTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFrameListTitle.Name = "lblFrameListTitle"
        Me.lblFrameListTitle.Size = New System.Drawing.Size(41, 13)
        Me.lblFrameListTitle.TabIndex = 0
        Me.lblFrameListTitle.Text = "Frames"
        '
        'lstFrames
        '
        Me.lstFrames.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstFrames.FormattingEnabled = True
        Me.lstFrames.Location = New System.Drawing.Point(310, 73)
        Me.lstFrames.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.lstFrames.Name = "lstFrames"
        Me.lstFrames.Size = New System.Drawing.Size(80, 143)
        Me.lstFrames.TabIndex = 6
        '
        'lblSpriteListTitle
        '
        Me.lblSpriteListTitle.AutoSize = True
        Me.lblSpriteListTitle.Location = New System.Drawing.Point(395, 58)
        Me.lblSpriteListTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSpriteListTitle.Name = "lblSpriteListTitle"
        Me.lblSpriteListTitle.Size = New System.Drawing.Size(39, 13)
        Me.lblSpriteListTitle.TabIndex = 8
        Me.lblSpriteListTitle.Text = "Sprites"
        '
        'lstSprites
        '
        Me.lstSprites.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstSprites.FormattingEnabled = True
        Me.lstSprites.Location = New System.Drawing.Point(394, 73)
        Me.lstSprites.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.lstSprites.Name = "lstSprites"
        Me.lstSprites.Size = New System.Drawing.Size(80, 143)
        Me.lstSprites.TabIndex = 7
        '
        'btnSpriteLoad
        '
        Me.btnSpriteLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSpriteLoad.Location = New System.Drawing.Point(394, 220)
        Me.btnSpriteLoad.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSpriteLoad.Name = "btnSpriteLoad"
        Me.btnSpriteLoad.Size = New System.Drawing.Size(67, 45)
        Me.btnSpriteLoad.TabIndex = 9
        Me.btnSpriteLoad.Text = "Load..."
        Me.btnSpriteLoad.UseVisualStyleBackColor = True
        '
        'btnFrameNew
        '
        Me.btnFrameNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrameNew.Location = New System.Drawing.Point(310, 220)
        Me.btnFrameNew.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnFrameNew.Name = "btnFrameNew"
        Me.btnFrameNew.Size = New System.Drawing.Size(67, 45)
        Me.btnFrameNew.TabIndex = 10
        Me.btnFrameNew.Text = "New Frame"
        Me.btnFrameNew.UseVisualStyleBackColor = True
        '
        'btnFrameRemove
        '
        Me.btnFrameRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrameRemove.Location = New System.Drawing.Point(310, 270)
        Me.btnFrameRemove.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnFrameRemove.Name = "btnFrameRemove"
        Me.btnFrameRemove.Size = New System.Drawing.Size(67, 45)
        Me.btnFrameRemove.TabIndex = 11
        Me.btnFrameRemove.Text = "Remove Frame"
        Me.btnFrameRemove.UseVisualStyleBackColor = True
        '
        'btnTagsEdit
        '
        Me.btnTagsEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagsEdit.Location = New System.Drawing.Point(226, 270)
        Me.btnTagsEdit.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnTagsEdit.Name = "btnTagsEdit"
        Me.btnTagsEdit.Size = New System.Drawing.Size(67, 45)
        Me.btnTagsEdit.TabIndex = 12
        Me.btnTagsEdit.Text = "Edit Tag"
        Me.btnTagsEdit.UseVisualStyleBackColor = True
        '
        'btnFrameAddSprite
        '
        Me.btnFrameAddSprite.Enabled = False
        Me.btnFrameAddSprite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrameAddSprite.Location = New System.Drawing.Point(310, 319)
        Me.btnFrameAddSprite.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnFrameAddSprite.Name = "btnFrameAddSprite"
        Me.btnFrameAddSprite.Size = New System.Drawing.Size(67, 45)
        Me.btnFrameAddSprite.TabIndex = 13
        Me.btnFrameAddSprite.Text = "Add Sprite"
        Me.btnFrameAddSprite.UseVisualStyleBackColor = True
        '
        'numFrameIndex
        '
        Me.numFrameIndex.Location = New System.Drawing.Point(327, 369)
        Me.numFrameIndex.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.numFrameIndex.Name = "numFrameIndex"
        Me.numFrameIndex.Size = New System.Drawing.Size(49, 20)
        Me.numFrameIndex.TabIndex = 14
        '
        'lblFrameIndex
        '
        Me.lblFrameIndex.AutoSize = True
        Me.lblFrameIndex.Location = New System.Drawing.Point(311, 370)
        Me.lblFrameIndex.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFrameIndex.Name = "lblFrameIndex"
        Me.lblFrameIndex.Size = New System.Drawing.Size(14, 13)
        Me.lblFrameIndex.TabIndex = 15
        Me.lblFrameIndex.Text = "#"
        '
        'btnTagsNew
        '
        Me.btnTagsNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagsNew.Location = New System.Drawing.Point(226, 220)
        Me.btnTagsNew.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnTagsNew.Name = "btnTagsNew"
        Me.btnTagsNew.Size = New System.Drawing.Size(67, 45)
        Me.btnTagsNew.TabIndex = 16
        Me.btnTagsNew.Text = "New Tag"
        Me.btnTagsNew.UseVisualStyleBackColor = True
        '
        'btnRedraw
        '
        Me.btnRedraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRedraw.Location = New System.Drawing.Point(479, 8)
        Me.btnRedraw.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnRedraw.Name = "btnRedraw"
        Me.btnRedraw.Size = New System.Drawing.Size(67, 45)
        Me.btnRedraw.TabIndex = 17
        Me.btnRedraw.Text = "Redraw"
        Me.btnRedraw.UseVisualStyleBackColor = True
        '
        'FrmEntityMaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(661, 467)
        Me.Controls.Add(Me.btnRedraw)
        Me.Controls.Add(Me.btnTagsNew)
        Me.Controls.Add(Me.lblFrameIndex)
        Me.Controls.Add(Me.numFrameIndex)
        Me.Controls.Add(Me.btnFrameAddSprite)
        Me.Controls.Add(Me.btnTagsEdit)
        Me.Controls.Add(Me.btnFrameRemove)
        Me.Controls.Add(Me.btnFrameNew)
        Me.Controls.Add(Me.btnSpriteLoad)
        Me.Controls.Add(Me.lblSpriteListTitle)
        Me.Controls.Add(Me.lstSprites)
        Me.Controls.Add(Me.lblFrameListTitle)
        Me.Controls.Add(Me.lstFrames)
        Me.Controls.Add(Me.lblTagListTitle)
        Me.Controls.Add(Me.lstTags)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnSaveAs)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.pnlFramePreview)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "FrmEntityMaker"
        Me.Text = "Entity Maker"
        CType(Me.numFrameIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
End Class
