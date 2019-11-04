<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLevelEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PnlRender = New System.Windows.Forms.Panel()
        Me.LstActors = New System.Windows.Forms.ListBox()
        Me.CntxtActors = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ItmActorDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItmActorEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItmActorDuplicate = New System.Windows.Forms.ToolStripMenuItem()
        Me.LblActors = New System.Windows.Forms.Label()
        Me.LblActorProperties = New System.Windows.Forms.Label()
        Me.LblActorLocation = New System.Windows.Forms.Label()
        Me.NumActorLocX = New System.Windows.Forms.NumericUpDown()
        Me.NumActorLocY = New System.Windows.Forms.NumericUpDown()
        Me.NumActorLayer = New System.Windows.Forms.NumericUpDown()
        Me.LblActorLayer = New System.Windows.Forms.Label()
        Me.NumActorScale = New System.Windows.Forms.NumericUpDown()
        Me.LblActorScale = New System.Windows.Forms.Label()
        Me.LblRooms = New System.Windows.Forms.Label()
        Me.TblActorProperties = New System.Windows.Forms.TableLayoutPanel()
        Me.LblActorName = New System.Windows.Forms.Label()
        Me.TxtActorName = New System.Windows.Forms.TextBox()
        Me.BtnCreateActor = New System.Windows.Forms.Button()
        Me.LstActorTags = New System.Windows.Forms.ListBox()
        Me.BtnAddActorTag = New System.Windows.Forms.Button()
        Me.TblControlsOverall = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnAddLevelParam = New System.Windows.Forms.Button()
        Me.LblLevelParams = New System.Windows.Forms.Label()
        Me.LstLevelParams = New System.Windows.Forms.ListBox()
        Me.LblActorTags = New System.Windows.Forms.Label()
        Me.BtnRoomAdd = New System.Windows.Forms.Button()
        Me.LstRooms = New System.Windows.Forms.ListBox()
        Me.CntxtActors.SuspendLayout()
        CType(Me.NumActorLocX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumActorLocY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumActorLayer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumActorScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TblActorProperties.SuspendLayout()
        Me.TblControlsOverall.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlRender
        '
        Me.PnlRender.BackColor = System.Drawing.Color.Black
        Me.TblControlsOverall.SetColumnSpan(Me.PnlRender, 2)
        Me.PnlRender.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlRender.Location = New System.Drawing.Point(3, 3)
        Me.PnlRender.Name = "PnlRender"
        Me.TblControlsOverall.SetRowSpan(Me.PnlRender, 3)
        Me.PnlRender.Size = New System.Drawing.Size(600, 449)
        Me.PnlRender.TabIndex = 1
        '
        'LstActors
        '
        Me.LstActors.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstActors.ContextMenuStrip = Me.CntxtActors
        Me.LstActors.FormattingEnabled = True
        Me.LstActors.ItemHeight = 20
        Me.LstActors.Location = New System.Drawing.Point(3, 495)
        Me.LstActors.Name = "LstActors"
        Me.LstActors.Size = New System.Drawing.Size(200, 300)
        Me.LstActors.TabIndex = 19
        '
        'CntxtActors
        '
        Me.CntxtActors.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.CntxtActors.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItmActorDelete, Me.ItmActorEdit, Me.ItmActorDuplicate})
        Me.CntxtActors.Name = "CntxtActors"
        Me.CntxtActors.Size = New System.Drawing.Size(159, 100)
        '
        'ItmActorDelete
        '
        Me.ItmActorDelete.Name = "ItmActorDelete"
        Me.ItmActorDelete.Size = New System.Drawing.Size(158, 32)
        Me.ItmActorDelete.Text = "Delete"
        '
        'ItmActorEdit
        '
        Me.ItmActorEdit.Name = "ItmActorEdit"
        Me.ItmActorEdit.Size = New System.Drawing.Size(158, 32)
        Me.ItmActorEdit.Text = "Edit"
        '
        'ItmActorDuplicate
        '
        Me.ItmActorDuplicate.Name = "ItmActorDuplicate"
        Me.ItmActorDuplicate.Size = New System.Drawing.Size(158, 32)
        Me.ItmActorDuplicate.Text = "Duplicate"
        '
        'LblActors
        '
        Me.LblActors.AutoSize = True
        Me.LblActors.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.LblActors.Location = New System.Drawing.Point(3, 455)
        Me.LblActors.Name = "LblActors"
        Me.LblActors.Size = New System.Drawing.Size(109, 37)
        Me.LblActors.TabIndex = 6
        Me.LblActors.Text = "Actors"
        '
        'LblActorProperties
        '
        Me.LblActorProperties.AutoSize = True
        Me.TblActorProperties.SetColumnSpan(Me.LblActorProperties, 3)
        Me.LblActorProperties.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.LblActorProperties.Location = New System.Drawing.Point(3, 0)
        Me.LblActorProperties.Name = "LblActorProperties"
        Me.LblActorProperties.Size = New System.Drawing.Size(247, 37)
        Me.LblActorProperties.TabIndex = 8
        Me.LblActorProperties.Text = "Actor Properties"
        '
        'LblActorLocation
        '
        Me.LblActorLocation.AutoSize = True
        Me.LblActorLocation.Location = New System.Drawing.Point(3, 70)
        Me.LblActorLocation.Name = "LblActorLocation"
        Me.LblActorLocation.Size = New System.Drawing.Size(70, 20)
        Me.LblActorLocation.TabIndex = 9
        Me.LblActorLocation.Text = "Location"
        '
        'NumActorLocX
        '
        Me.NumActorLocX.DecimalPlaces = 1
        Me.NumActorLocX.Location = New System.Drawing.Point(79, 73)
        Me.NumActorLocX.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.NumActorLocX.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.NumActorLocX.Name = "NumActorLocX"
        Me.NumActorLocX.Size = New System.Drawing.Size(84, 26)
        Me.NumActorLocX.TabIndex = 24
        '
        'NumActorLocY
        '
        Me.NumActorLocY.DecimalPlaces = 1
        Me.NumActorLocY.Location = New System.Drawing.Point(169, 73)
        Me.NumActorLocY.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.NumActorLocY.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.NumActorLocY.Name = "NumActorLocY"
        Me.NumActorLocY.Size = New System.Drawing.Size(84, 26)
        Me.NumActorLocY.TabIndex = 25
        '
        'NumActorLayer
        '
        Me.NumActorLayer.Location = New System.Drawing.Point(79, 106)
        Me.NumActorLayer.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.NumActorLayer.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.NumActorLayer.Name = "NumActorLayer"
        Me.NumActorLayer.Size = New System.Drawing.Size(84, 26)
        Me.NumActorLayer.TabIndex = 26
        '
        'LblActorLayer
        '
        Me.LblActorLayer.AutoSize = True
        Me.LblActorLayer.Location = New System.Drawing.Point(3, 103)
        Me.LblActorLayer.Name = "LblActorLayer"
        Me.LblActorLayer.Size = New System.Drawing.Size(48, 20)
        Me.LblActorLayer.TabIndex = 12
        Me.LblActorLayer.Text = "Layer"
        '
        'NumActorScale
        '
        Me.NumActorScale.DecimalPlaces = 2
        Me.NumActorScale.Increment = New Decimal(New Integer() {10, 0, 0, 131072})
        Me.NumActorScale.Location = New System.Drawing.Point(79, 139)
        Me.NumActorScale.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumActorScale.Name = "NumActorScale"
        Me.NumActorScale.Size = New System.Drawing.Size(84, 26)
        Me.NumActorScale.TabIndex = 27
        Me.NumActorScale.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'LblActorScale
        '
        Me.LblActorScale.AutoSize = True
        Me.LblActorScale.Location = New System.Drawing.Point(3, 136)
        Me.LblActorScale.Name = "LblActorScale"
        Me.LblActorScale.Size = New System.Drawing.Size(49, 20)
        Me.LblActorScale.TabIndex = 14
        Me.LblActorScale.Text = "Scale"
        '
        'LblRooms
        '
        Me.LblRooms.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblRooms.AutoSize = True
        Me.LblRooms.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRooms.Location = New System.Drawing.Point(609, 0)
        Me.LblRooms.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LblRooms.Name = "LblRooms"
        Me.LblRooms.Size = New System.Drawing.Size(118, 37)
        Me.LblRooms.TabIndex = 4
        Me.LblRooms.Text = "Rooms"
        '
        'TblActorProperties
        '
        Me.TblActorProperties.ColumnCount = 3
        Me.TblActorProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblActorProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblActorProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblActorProperties.Controls.Add(Me.LblActorLocation, 0, 2)
        Me.TblActorProperties.Controls.Add(Me.LblActorLayer, 0, 3)
        Me.TblActorProperties.Controls.Add(Me.NumActorLocY, 2, 2)
        Me.TblActorProperties.Controls.Add(Me.NumActorLayer, 1, 3)
        Me.TblActorProperties.Controls.Add(Me.LblActorProperties, 0, 0)
        Me.TblActorProperties.Controls.Add(Me.NumActorLocX, 1, 2)
        Me.TblActorProperties.Controls.Add(Me.NumActorScale, 1, 4)
        Me.TblActorProperties.Controls.Add(Me.LblActorScale, 0, 4)
        Me.TblActorProperties.Controls.Add(Me.LblActorName, 0, 1)
        Me.TblActorProperties.Controls.Add(Me.TxtActorName, 1, 1)
        Me.TblActorProperties.Dock = System.Windows.Forms.DockStyle.Top
        Me.TblActorProperties.Location = New System.Drawing.Point(209, 458)
        Me.TblActorProperties.Name = "TblActorProperties"
        Me.TblActorProperties.RowCount = 5
        Me.TblControlsOverall.SetRowSpan(Me.TblActorProperties, 2)
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TblActorProperties.Size = New System.Drawing.Size(394, 172)
        Me.TblActorProperties.TabIndex = 17
        '
        'LblActorName
        '
        Me.LblActorName.AutoSize = True
        Me.LblActorName.Location = New System.Drawing.Point(3, 37)
        Me.LblActorName.Name = "LblActorName"
        Me.LblActorName.Size = New System.Drawing.Size(51, 20)
        Me.LblActorName.TabIndex = 16
        Me.LblActorName.Text = "Name"
        '
        'TxtActorName
        '
        Me.TblActorProperties.SetColumnSpan(Me.TxtActorName, 2)
        Me.TxtActorName.Location = New System.Drawing.Point(79, 40)
        Me.TxtActorName.MaxLength = 32
        Me.TxtActorName.Name = "TxtActorName"
        Me.TxtActorName.Size = New System.Drawing.Size(174, 26)
        Me.TxtActorName.TabIndex = 23
        '
        'BtnCreateActor
        '
        Me.BtnCreateActor.Enabled = False
        Me.BtnCreateActor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCreateActor.Location = New System.Drawing.Point(3, 801)
        Me.BtnCreateActor.Name = "BtnCreateActor"
        Me.BtnCreateActor.Size = New System.Drawing.Size(200, 69)
        Me.BtnCreateActor.TabIndex = 20
        Me.BtnCreateActor.Text = "Create Actor"
        Me.BtnCreateActor.UseVisualStyleBackColor = True
        '
        'LstActorTags
        '
        Me.LstActorTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstActorTags.FormattingEnabled = True
        Me.LstActorTags.ItemHeight = 20
        Me.LstActorTags.Location = New System.Drawing.Point(609, 495)
        Me.LstActorTags.Name = "LstActorTags"
        Me.LstActorTags.Size = New System.Drawing.Size(200, 300)
        Me.LstActorTags.TabIndex = 28
        '
        'BtnAddActorTag
        '
        Me.BtnAddActorTag.Enabled = False
        Me.BtnAddActorTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddActorTag.Location = New System.Drawing.Point(609, 801)
        Me.BtnAddActorTag.Name = "BtnAddActorTag"
        Me.BtnAddActorTag.Size = New System.Drawing.Size(200, 69)
        Me.BtnAddActorTag.TabIndex = 29
        Me.BtnAddActorTag.Text = "Add Tag"
        Me.BtnAddActorTag.UseVisualStyleBackColor = True
        '
        'TblControlsOverall
        '
        Me.TblControlsOverall.ColumnCount = 4
        Me.TblControlsOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblControlsOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblControlsOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblControlsOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 502.0!))
        Me.TblControlsOverall.Controls.Add(Me.BtnAddLevelParam, 3, 2)
        Me.TblControlsOverall.Controls.Add(Me.LblLevelParams, 3, 0)
        Me.TblControlsOverall.Controls.Add(Me.LstLevelParams, 3, 1)
        Me.TblControlsOverall.Controls.Add(Me.LblActorTags, 2, 3)
        Me.TblControlsOverall.Controls.Add(Me.PnlRender, 0, 0)
        Me.TblControlsOverall.Controls.Add(Me.LstActors, 0, 4)
        Me.TblControlsOverall.Controls.Add(Me.BtnRoomAdd, 2, 2)
        Me.TblControlsOverall.Controls.Add(Me.LblRooms, 2, 0)
        Me.TblControlsOverall.Controls.Add(Me.LstRooms, 2, 1)
        Me.TblControlsOverall.Controls.Add(Me.BtnCreateActor, 0, 5)
        Me.TblControlsOverall.Controls.Add(Me.LblActors, 0, 3)
        Me.TblControlsOverall.Controls.Add(Me.TblActorProperties, 1, 3)
        Me.TblControlsOverall.Controls.Add(Me.BtnAddActorTag, 2, 5)
        Me.TblControlsOverall.Controls.Add(Me.LstActorTags, 2, 4)
        Me.TblControlsOverall.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TblControlsOverall.Location = New System.Drawing.Point(0, 0)
        Me.TblControlsOverall.Name = "TblControlsOverall"
        Me.TblControlsOverall.RowCount = 6
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.Size = New System.Drawing.Size(1028, 874)
        Me.TblControlsOverall.TabIndex = 24
        '
        'BtnAddLevelParam
        '
        Me.BtnAddLevelParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddLevelParam.Location = New System.Drawing.Point(815, 346)
        Me.BtnAddLevelParam.Name = "BtnAddLevelParam"
        Me.BtnAddLevelParam.Size = New System.Drawing.Size(200, 69)
        Me.BtnAddLevelParam.TabIndex = 33
        Me.BtnAddLevelParam.Text = "Add Parameter"
        Me.BtnAddLevelParam.UseVisualStyleBackColor = True
        '
        'LblLevelParams
        '
        Me.LblLevelParams.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblLevelParams.AutoSize = True
        Me.LblLevelParams.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLevelParams.Location = New System.Drawing.Point(815, 0)
        Me.LblLevelParams.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LblLevelParams.Name = "LblLevelParams"
        Me.LblLevelParams.Size = New System.Drawing.Size(182, 37)
        Me.LblLevelParams.TabIndex = 34
        Me.LblLevelParams.Text = "Parameters"
        '
        'LstLevelParams
        '
        Me.LstLevelParams.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstLevelParams.FormattingEnabled = True
        Me.LstLevelParams.ItemHeight = 20
        Me.LstLevelParams.Location = New System.Drawing.Point(815, 40)
        Me.LstLevelParams.Name = "LstLevelParams"
        Me.LstLevelParams.Size = New System.Drawing.Size(200, 300)
        Me.LstLevelParams.TabIndex = 32
        '
        'LblActorTags
        '
        Me.LblActorTags.AutoSize = True
        Me.LblActorTags.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.LblActorTags.Location = New System.Drawing.Point(609, 455)
        Me.LblActorTags.Name = "LblActorTags"
        Me.LblActorTags.Size = New System.Drawing.Size(174, 37)
        Me.LblActorTags.TabIndex = 28
        Me.LblActorTags.Text = "Actor Tags"
        '
        'BtnRoomAdd
        '
        Me.BtnRoomAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRoomAdd.Location = New System.Drawing.Point(609, 346)
        Me.BtnRoomAdd.Name = "BtnRoomAdd"
        Me.BtnRoomAdd.Size = New System.Drawing.Size(200, 69)
        Me.BtnRoomAdd.TabIndex = 12
        Me.BtnRoomAdd.Text = "Add Room"
        Me.BtnRoomAdd.UseVisualStyleBackColor = True
        '
        'LstRooms
        '
        Me.LstRooms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstRooms.FormattingEnabled = True
        Me.LstRooms.ItemHeight = 20
        Me.LstRooms.Location = New System.Drawing.Point(609, 40)
        Me.LstRooms.Name = "LstRooms"
        Me.LstRooms.Size = New System.Drawing.Size(200, 300)
        Me.LstRooms.TabIndex = 11
        '
        'FrmLevelEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(1028, 874)
        Me.Controls.Add(Me.TblControlsOverall)
        Me.DoubleBuffered = True
        Me.Name = "FrmLevelEditor"
        Me.Text = "Level Editor"
        Me.CntxtActors.ResumeLayout(False)
        CType(Me.NumActorLocX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumActorLocY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumActorLayer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumActorScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TblActorProperties.ResumeLayout(False)
        Me.TblActorProperties.PerformLayout()
        Me.TblControlsOverall.ResumeLayout(False)
        Me.TblControlsOverall.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PnlRender As Panel
    Friend WithEvents LstActors As ListBox
    Friend WithEvents LblActors As Label
    Friend WithEvents LblActorProperties As Label
    Friend WithEvents LblActorLocation As Label
    Friend WithEvents NumActorLocX As NumericUpDown
    Friend WithEvents NumActorLocY As NumericUpDown
    Friend WithEvents NumActorLayer As NumericUpDown
    Friend WithEvents LblActorLayer As Label
    Friend WithEvents NumActorScale As NumericUpDown
    Friend WithEvents LblActorScale As Label
    Friend WithEvents TblActorProperties As TableLayoutPanel
    Friend WithEvents LblActorName As Label
    Friend WithEvents TxtActorName As TextBox
    Friend WithEvents BtnCreateActor As Button
    Friend WithEvents LstActorTags As ListBox
    Friend WithEvents BtnAddActorTag As Button
    Friend WithEvents LblRooms As Label
    Friend WithEvents TblControlsOverall As TableLayoutPanel
    Friend WithEvents LstLevelParams As ListBox
    Friend WithEvents BtnAddLevelParam As Button
    Friend WithEvents BtnRoomAdd As Button
    Friend WithEvents LstRooms As ListBox
    Friend WithEvents LblActorTags As Label
    Friend WithEvents LblLevelParams As Label
    Friend WithEvents CntxtActors As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ItmActorDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItmActorEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItmActorDuplicate As System.Windows.Forms.ToolStripMenuItem
End Class
