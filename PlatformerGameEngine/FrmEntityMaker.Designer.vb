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
        CType(Me.numFrameIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlFramePreview
        '
        Me.pnlFramePreview.BackColor = System.Drawing.Color.Black
        Me.pnlFramePreview.Location = New System.Drawing.Point(13, 13)
        Me.pnlFramePreview.Name = "pnlFramePreview"
        Me.pnlFramePreview.Size = New System.Drawing.Size(320, 320)
        Me.pnlFramePreview.TabIndex = 0
        '
        'btnOpen
        '
        Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpen.Location = New System.Drawing.Point(340, 13)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(100, 70)
        Me.btnOpen.TabIndex = 1
        Me.btnOpen.Text = "Open..."
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnSaveAs
        '
        Me.btnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveAs.Location = New System.Drawing.Point(446, 13)
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(100, 70)
        Me.btnSaveAs.TabIndex = 2
        Me.btnSaveAs.Text = "Save As..."
        Me.btnSaveAs.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(552, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 70)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lstTags
        '
        Me.lstTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstTags.FormattingEnabled = True
        Me.lstTags.ItemHeight = 20
        Me.lstTags.Location = New System.Drawing.Point(339, 113)
        Me.lstTags.Name = "lstTags"
        Me.lstTags.Size = New System.Drawing.Size(120, 220)
        Me.lstTags.TabIndex = 4
        '
        'lblTagListTitle
        '
        Me.lblTagListTitle.AutoSize = True
        Me.lblTagListTitle.Location = New System.Drawing.Point(340, 90)
        Me.lblTagListTitle.Name = "lblTagListTitle"
        Me.lblTagListTitle.Size = New System.Drawing.Size(44, 20)
        Me.lblTagListTitle.TabIndex = 0
        Me.lblTagListTitle.Text = "Tags"
        '
        'lblFrameListTitle
        '
        Me.lblFrameListTitle.AutoSize = True
        Me.lblFrameListTitle.Location = New System.Drawing.Point(466, 90)
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
        Me.lstFrames.Location = New System.Drawing.Point(465, 113)
        Me.lstFrames.Name = "lstFrames"
        Me.lstFrames.Size = New System.Drawing.Size(120, 220)
        Me.lstFrames.TabIndex = 6
        '
        'lblSpriteListTitle
        '
        Me.lblSpriteListTitle.AutoSize = True
        Me.lblSpriteListTitle.Location = New System.Drawing.Point(592, 90)
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
        Me.lstSprites.Location = New System.Drawing.Point(591, 113)
        Me.lstSprites.Name = "lstSprites"
        Me.lstSprites.Size = New System.Drawing.Size(120, 220)
        Me.lstSprites.TabIndex = 7
        '
        'btnSpriteLoad
        '
        Me.btnSpriteLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSpriteLoad.Location = New System.Drawing.Point(591, 339)
        Me.btnSpriteLoad.Name = "btnSpriteLoad"
        Me.btnSpriteLoad.Size = New System.Drawing.Size(100, 70)
        Me.btnSpriteLoad.TabIndex = 9
        Me.btnSpriteLoad.Text = "Load..."
        Me.btnSpriteLoad.UseVisualStyleBackColor = True
        '
        'btnFrameNew
        '
        Me.btnFrameNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrameNew.Location = New System.Drawing.Point(465, 415)
        Me.btnFrameNew.Name = "btnFrameNew"
        Me.btnFrameNew.Size = New System.Drawing.Size(100, 70)
        Me.btnFrameNew.TabIndex = 10
        Me.btnFrameNew.Text = "New Frame"
        Me.btnFrameNew.UseVisualStyleBackColor = True
        '
        'btnFrameRemove
        '
        Me.btnFrameRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrameRemove.Location = New System.Drawing.Point(465, 491)
        Me.btnFrameRemove.Name = "btnFrameRemove"
        Me.btnFrameRemove.Size = New System.Drawing.Size(100, 70)
        Me.btnFrameRemove.TabIndex = 11
        Me.btnFrameRemove.Text = "Remove Frame"
        Me.btnFrameRemove.UseVisualStyleBackColor = True
        '
        'btnTagsEdit
        '
        Me.btnTagsEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagsEdit.Location = New System.Drawing.Point(339, 415)
        Me.btnTagsEdit.Name = "btnTagsEdit"
        Me.btnTagsEdit.Size = New System.Drawing.Size(100, 70)
        Me.btnTagsEdit.TabIndex = 12
        Me.btnTagsEdit.Text = "Edit Tag"
        Me.btnTagsEdit.UseVisualStyleBackColor = True
        '
        'btnFrameAddSprite
        '
        Me.btnFrameAddSprite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrameAddSprite.Location = New System.Drawing.Point(465, 339)
        Me.btnFrameAddSprite.Name = "btnFrameAddSprite"
        Me.btnFrameAddSprite.Size = New System.Drawing.Size(100, 70)
        Me.btnFrameAddSprite.TabIndex = 13
        Me.btnFrameAddSprite.Text = "Add Sprite"
        Me.btnFrameAddSprite.UseVisualStyleBackColor = True
        '
        'numFrameIndex
        '
        Me.numFrameIndex.Location = New System.Drawing.Point(490, 567)
        Me.numFrameIndex.Name = "numFrameIndex"
        Me.numFrameIndex.Size = New System.Drawing.Size(70, 26)
        Me.numFrameIndex.TabIndex = 14
        '
        'lblFrameIndex
        '
        Me.lblFrameIndex.AutoSize = True
        Me.lblFrameIndex.Location = New System.Drawing.Point(466, 569)
        Me.lblFrameIndex.Name = "lblFrameIndex"
        Me.lblFrameIndex.Size = New System.Drawing.Size(18, 20)
        Me.lblFrameIndex.TabIndex = 15
        Me.lblFrameIndex.Text = "#"
        '
        'btnTagsNew
        '
        Me.btnTagsNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagsNew.Location = New System.Drawing.Point(339, 339)
        Me.btnTagsNew.Name = "btnTagsNew"
        Me.btnTagsNew.Size = New System.Drawing.Size(100, 70)
        Me.btnTagsNew.TabIndex = 16
        Me.btnTagsNew.Text = "New Tag"
        Me.btnTagsNew.UseVisualStyleBackColor = True
        '
        'FrmEntityMaker
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.DimGray
        Me.ClientSize = New System.Drawing.Size(992, 718)
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
End Class
