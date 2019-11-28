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
        Me.CntxtTags = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ItmTagsDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItmTagsEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItmTagsDuplicate = New System.Windows.Forms.ToolStripMenuItem()
        Me.BtnAddActorTag = New System.Windows.Forms.Button()
        Me.TblControlsOverall = New System.Windows.Forms.TableLayoutPanel()
        Me.ToolBar = New System.Windows.Forms.ToolStrip()
        Me.ToolBarFile = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolBarFileOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolBarFileSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolBarFileSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.BtnAddLevelParam = New System.Windows.Forms.Button()
        Me.LblLevelParams = New System.Windows.Forms.Label()
        Me.LstLevelParams = New System.Windows.Forms.ListBox()
        Me.CntxtParameters = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ItmParameterDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItmParameterEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItmParameterDuplicate = New System.Windows.Forms.ToolStripMenuItem()
        Me.LblActorTags = New System.Windows.Forms.Label()
        Me.BtnRoomAdd = New System.Windows.Forms.Button()
        Me.LstRooms = New System.Windows.Forms.ListBox()
        Me.CntxtRooms = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ItmRoomDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItmRoomEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ItmRoomDuplicate = New System.Windows.Forms.ToolStripMenuItem()
        Me.CntxtActors.SuspendLayout()
        CType(Me.NumActorLocX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumActorLocY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumActorLayer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumActorScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TblActorProperties.SuspendLayout()
        Me.CntxtTags.SuspendLayout()
        Me.TblControlsOverall.SuspendLayout()
        Me.ToolBar.SuspendLayout()
        Me.CntxtParameters.SuspendLayout()
        Me.CntxtRooms.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlRender
        '
        Me.PnlRender.BackColor = System.Drawing.Color.Black
        Me.TblControlsOverall.SetColumnSpan(Me.PnlRender, 2)
        Me.PnlRender.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlRender.Location = New System.Drawing.Point(3, 41)
        Me.PnlRender.Name = "PnlRender"
        Me.TblControlsOverall.SetRowSpan(Me.PnlRender, 3)
        Me.PnlRender.Size = New System.Drawing.Size(640, 439)
        Me.PnlRender.TabIndex = 1
        '
        'LstActors
        '
        Me.LstActors.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstActors.ContextMenuStrip = Me.CntxtActors
        Me.LstActors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstActors.FormattingEnabled = True
        Me.LstActors.ItemHeight = 20
        Me.LstActors.Location = New System.Drawing.Point(3, 530)
        Me.LstActors.Name = "LstActors"
        Me.LstActors.Size = New System.Drawing.Size(209, 306)
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
        Me.LblActors.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LblActors.AutoSize = True
        Me.LblActors.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.LblActors.Location = New System.Drawing.Point(53, 483)
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
        Me.LblRooms.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LblRooms.AutoSize = True
        Me.LblRooms.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRooms.Location = New System.Drawing.Point(691, 38)
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
        Me.TblActorProperties.Location = New System.Drawing.Point(218, 486)
        Me.TblActorProperties.Name = "TblActorProperties"
        Me.TblActorProperties.RowCount = 5
        Me.TblControlsOverall.SetRowSpan(Me.TblActorProperties, 2)
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TblActorProperties.Size = New System.Drawing.Size(425, 172)
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
        Me.BtnCreateActor.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BtnCreateActor.Enabled = False
        Me.BtnCreateActor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCreateActor.Location = New System.Drawing.Point(13, 842)
        Me.BtnCreateActor.Name = "BtnCreateActor"
        Me.BtnCreateActor.Size = New System.Drawing.Size(189, 69)
        Me.BtnCreateActor.TabIndex = 20
        Me.BtnCreateActor.Text = "Create Actor"
        Me.BtnCreateActor.UseVisualStyleBackColor = True
        '
        'LstActorTags
        '
        Me.LstActorTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TblControlsOverall.SetColumnSpan(Me.LstActorTags, 2)
        Me.LstActorTags.ContextMenuStrip = Me.CntxtTags
        Me.LstActorTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstActorTags.FormattingEnabled = True
        Me.LstActorTags.ItemHeight = 20
        Me.LstActorTags.Location = New System.Drawing.Point(649, 530)
        Me.LstActorTags.Name = "LstActorTags"
        Me.LstActorTags.Size = New System.Drawing.Size(426, 306)
        Me.LstActorTags.TabIndex = 28
        '
        'CntxtTags
        '
        Me.CntxtTags.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.CntxtTags.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItmTagsDelete, Me.ItmTagsEdit, Me.ItmTagsDuplicate})
        Me.CntxtTags.Name = "CntxtActors"
        Me.CntxtTags.Size = New System.Drawing.Size(159, 100)
        '
        'ItmTagsDelete
        '
        Me.ItmTagsDelete.Name = "ItmTagsDelete"
        Me.ItmTagsDelete.Size = New System.Drawing.Size(158, 32)
        Me.ItmTagsDelete.Text = "Delete"
        '
        'ItmTagsEdit
        '
        Me.ItmTagsEdit.Name = "ItmTagsEdit"
        Me.ItmTagsEdit.Size = New System.Drawing.Size(158, 32)
        Me.ItmTagsEdit.Text = "Edit"
        '
        'ItmTagsDuplicate
        '
        Me.ItmTagsDuplicate.Name = "ItmTagsDuplicate"
        Me.ItmTagsDuplicate.Size = New System.Drawing.Size(158, 32)
        Me.ItmTagsDuplicate.Text = "Duplicate"
        '
        'BtnAddActorTag
        '
        Me.BtnAddActorTag.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TblControlsOverall.SetColumnSpan(Me.BtnAddActorTag, 2)
        Me.BtnAddActorTag.Enabled = False
        Me.BtnAddActorTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddActorTag.Location = New System.Drawing.Point(767, 842)
        Me.BtnAddActorTag.Name = "BtnAddActorTag"
        Me.BtnAddActorTag.Size = New System.Drawing.Size(189, 69)
        Me.BtnAddActorTag.TabIndex = 29
        Me.BtnAddActorTag.Text = "Add Tag"
        Me.BtnAddActorTag.UseVisualStyleBackColor = True
        '
        'TblControlsOverall
        '
        Me.TblControlsOverall.ColumnCount = 4
        Me.TblControlsOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TblControlsOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TblControlsOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TblControlsOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TblControlsOverall.Controls.Add(Me.ToolBar, 0, 0)
        Me.TblControlsOverall.Controls.Add(Me.BtnAddLevelParam, 3, 3)
        Me.TblControlsOverall.Controls.Add(Me.LblLevelParams, 3, 1)
        Me.TblControlsOverall.Controls.Add(Me.LstLevelParams, 3, 2)
        Me.TblControlsOverall.Controls.Add(Me.LblActorTags, 2, 4)
        Me.TblControlsOverall.Controls.Add(Me.PnlRender, 0, 1)
        Me.TblControlsOverall.Controls.Add(Me.LstActors, 0, 5)
        Me.TblControlsOverall.Controls.Add(Me.BtnRoomAdd, 2, 3)
        Me.TblControlsOverall.Controls.Add(Me.LblRooms, 2, 1)
        Me.TblControlsOverall.Controls.Add(Me.LstRooms, 2, 2)
        Me.TblControlsOverall.Controls.Add(Me.BtnCreateActor, 0, 6)
        Me.TblControlsOverall.Controls.Add(Me.LblActors, 0, 4)
        Me.TblControlsOverall.Controls.Add(Me.TblActorProperties, 1, 4)
        Me.TblControlsOverall.Controls.Add(Me.BtnAddActorTag, 2, 6)
        Me.TblControlsOverall.Controls.Add(Me.LstActorTags, 2, 5)
        Me.TblControlsOverall.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TblControlsOverall.Location = New System.Drawing.Point(0, 0)
        Me.TblControlsOverall.Name = "TblControlsOverall"
        Me.TblControlsOverall.RowCount = 7
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TblControlsOverall.Size = New System.Drawing.Size(1078, 931)
        Me.TblControlsOverall.TabIndex = 24
        '
        'ToolBar
        '
        Me.TblControlsOverall.SetColumnSpan(Me.ToolBar, 4)
        Me.ToolBar.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarFile})
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.ToolBar.Size = New System.Drawing.Size(1078, 38)
        Me.ToolBar.TabIndex = 0
        Me.ToolBar.Text = "ToolBar"
        '
        'ToolBarFile
        '
        Me.ToolBarFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolBarFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarFileOpen, Me.ToolBarFileSaveAs, Me.ToolBarFileSave})
        Me.ToolBarFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarFile.Name = "ToolBarFile"
        Me.ToolBarFile.Size = New System.Drawing.Size(56, 29)
        Me.ToolBarFile.Text = "File"
        '
        'ToolBarFileOpen
        '
        Me.ToolBarFileOpen.Name = "ToolBarFileOpen"
        Me.ToolBarFileOpen.Size = New System.Drawing.Size(188, 34)
        Me.ToolBarFileOpen.Text = "Open..."
        '
        'ToolBarFileSaveAs
        '
        Me.ToolBarFileSaveAs.Name = "ToolBarFileSaveAs"
        Me.ToolBarFileSaveAs.Size = New System.Drawing.Size(188, 34)
        Me.ToolBarFileSaveAs.Text = "Save As..."
        '
        'ToolBarFileSave
        '
        Me.ToolBarFileSave.Name = "ToolBarFileSave"
        Me.ToolBarFileSave.Size = New System.Drawing.Size(188, 34)
        Me.ToolBarFileSave.Text = "Save"
        '
        'BtnAddLevelParam
        '
        Me.BtnAddLevelParam.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BtnAddLevelParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddLevelParam.Location = New System.Drawing.Point(874, 397)
        Me.BtnAddLevelParam.Name = "BtnAddLevelParam"
        Me.BtnAddLevelParam.Size = New System.Drawing.Size(191, 69)
        Me.BtnAddLevelParam.TabIndex = 33
        Me.BtnAddLevelParam.Text = "Add Parameter"
        Me.BtnAddLevelParam.UseVisualStyleBackColor = True
        '
        'LblLevelParams
        '
        Me.LblLevelParams.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LblLevelParams.AutoSize = True
        Me.LblLevelParams.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLevelParams.Location = New System.Drawing.Point(875, 38)
        Me.LblLevelParams.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.LblLevelParams.Name = "LblLevelParams"
        Me.LblLevelParams.Size = New System.Drawing.Size(182, 37)
        Me.LblLevelParams.TabIndex = 34
        Me.LblLevelParams.Text = "Parameters"
        '
        'LstLevelParams
        '
        Me.LstLevelParams.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstLevelParams.ContextMenuStrip = Me.CntxtParameters
        Me.LstLevelParams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstLevelParams.FormattingEnabled = True
        Me.LstLevelParams.ItemHeight = 20
        Me.LstLevelParams.Location = New System.Drawing.Point(864, 85)
        Me.LstLevelParams.Name = "LstLevelParams"
        Me.LstLevelParams.Size = New System.Drawing.Size(211, 306)
        Me.LstLevelParams.TabIndex = 32
        '
        'CntxtParameters
        '
        Me.CntxtParameters.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.CntxtParameters.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItmParameterDelete, Me.ItmParameterEdit, Me.ItmParameterDuplicate})
        Me.CntxtParameters.Name = "CntxtActors"
        Me.CntxtParameters.Size = New System.Drawing.Size(159, 100)
        '
        'ItmParameterDelete
        '
        Me.ItmParameterDelete.Name = "ItmParameterDelete"
        Me.ItmParameterDelete.Size = New System.Drawing.Size(158, 32)
        Me.ItmParameterDelete.Text = "Delete"
        '
        'ItmParameterEdit
        '
        Me.ItmParameterEdit.Name = "ItmParameterEdit"
        Me.ItmParameterEdit.Size = New System.Drawing.Size(158, 32)
        Me.ItmParameterEdit.Text = "Edit"
        '
        'ItmParameterDuplicate
        '
        Me.ItmParameterDuplicate.Name = "ItmParameterDuplicate"
        Me.ItmParameterDuplicate.Size = New System.Drawing.Size(158, 32)
        Me.ItmParameterDuplicate.Text = "Duplicate"
        '
        'LblActorTags
        '
        Me.LblActorTags.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LblActorTags.AutoSize = True
        Me.TblControlsOverall.SetColumnSpan(Me.LblActorTags, 2)
        Me.LblActorTags.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.LblActorTags.Location = New System.Drawing.Point(775, 483)
        Me.LblActorTags.Name = "LblActorTags"
        Me.LblActorTags.Size = New System.Drawing.Size(174, 37)
        Me.LblActorTags.TabIndex = 28
        Me.LblActorTags.Text = "Actor Tags"
        '
        'BtnRoomAdd
        '
        Me.BtnRoomAdd.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BtnRoomAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRoomAdd.Location = New System.Drawing.Point(659, 397)
        Me.BtnRoomAdd.Name = "BtnRoomAdd"
        Me.BtnRoomAdd.Size = New System.Drawing.Size(189, 69)
        Me.BtnRoomAdd.TabIndex = 12
        Me.BtnRoomAdd.Text = "Add Room"
        Me.BtnRoomAdd.UseVisualStyleBackColor = True
        '
        'LstRooms
        '
        Me.LstRooms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstRooms.ContextMenuStrip = Me.CntxtRooms
        Me.LstRooms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstRooms.FormattingEnabled = True
        Me.LstRooms.ItemHeight = 20
        Me.LstRooms.Location = New System.Drawing.Point(649, 85)
        Me.LstRooms.Name = "LstRooms"
        Me.LstRooms.Size = New System.Drawing.Size(209, 306)
        Me.LstRooms.TabIndex = 11
        '
        'CntxtRooms
        '
        Me.CntxtRooms.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.CntxtRooms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItmRoomDelete, Me.ItmRoomEdit, Me.ItmRoomDuplicate})
        Me.CntxtRooms.Name = "CntxtActors"
        Me.CntxtRooms.Size = New System.Drawing.Size(159, 100)
        '
        'ItmRoomDelete
        '
        Me.ItmRoomDelete.Name = "ItmRoomDelete"
        Me.ItmRoomDelete.Size = New System.Drawing.Size(158, 32)
        Me.ItmRoomDelete.Text = "Delete"
        '
        'ItmRoomEdit
        '
        Me.ItmRoomEdit.Name = "ItmRoomEdit"
        Me.ItmRoomEdit.Size = New System.Drawing.Size(158, 32)
        Me.ItmRoomEdit.Text = "Edit"
        '
        'ItmRoomDuplicate
        '
        Me.ItmRoomDuplicate.Name = "ItmRoomDuplicate"
        Me.ItmRoomDuplicate.Size = New System.Drawing.Size(158, 32)
        Me.ItmRoomDuplicate.Text = "Duplicate"
        '
        'FrmLevelEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(1078, 931)
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
        Me.CntxtTags.ResumeLayout(False)
        Me.TblControlsOverall.ResumeLayout(False)
        Me.TblControlsOverall.PerformLayout()
        Me.ToolBar.ResumeLayout(False)
        Me.ToolBar.PerformLayout()
        Me.CntxtParameters.ResumeLayout(False)
        Me.CntxtRooms.ResumeLayout(False)
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
    Friend WithEvents ToolBar As ToolStrip
    Friend WithEvents ToolBarFile As ToolStripDropDownButton
    Friend WithEvents ToolBarFileOpen As ToolStripMenuItem
    Friend WithEvents ToolBarFileSaveAs As ToolStripMenuItem
    Friend WithEvents ToolBarFileSave As ToolStripMenuItem
    Friend WithEvents CntxtRooms As ContextMenuStrip
    Friend WithEvents ItmRoomDelete As ToolStripMenuItem
    Friend WithEvents ItmRoomDuplicate As ToolStripMenuItem
    Friend WithEvents CntxtParameters As ContextMenuStrip
    Friend WithEvents ItmParameterDelete As ToolStripMenuItem
    Friend WithEvents ItmParameterEdit As ToolStripMenuItem
    Friend WithEvents ItmParameterDuplicate As ToolStripMenuItem
    Friend WithEvents CntxtTags As ContextMenuStrip
    Friend WithEvents ItmTagsDelete As ToolStripMenuItem
    Friend WithEvents ItmTagsEdit As ToolStripMenuItem
    Friend WithEvents ItmTagsDuplicate As ToolStripMenuItem
    Friend WithEvents ItmRoomEdit As ToolStripMenuItem
End Class
