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
        Me.PnlRender.Location = New System.Drawing.Point(2, 27)
        Me.PnlRender.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PnlRender.Name = "PnlRender"
        Me.TblControlsOverall.SetRowSpan(Me.PnlRender, 3)
        Me.PnlRender.Size = New System.Drawing.Size(400, 292)
        Me.PnlRender.TabIndex = 1
        '
        'LstActors
        '
        Me.LstActors.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstActors.ContextMenuStrip = Me.CntxtActors
        Me.LstActors.FormattingEnabled = True
        Me.LstActors.Location = New System.Drawing.Point(2, 349)
        Me.LstActors.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LstActors.Name = "LstActors"
        Me.LstActors.Size = New System.Drawing.Size(133, 195)
        Me.LstActors.TabIndex = 19
        '
        'CntxtActors
        '
        Me.CntxtActors.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.CntxtActors.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItmActorDelete, Me.ItmActorEdit, Me.ItmActorDuplicate})
        Me.CntxtActors.Name = "CntxtActors"
        Me.CntxtActors.Size = New System.Drawing.Size(125, 70)
        '
        'ItmActorDelete
        '
        Me.ItmActorDelete.Name = "ItmActorDelete"
        Me.ItmActorDelete.Size = New System.Drawing.Size(124, 22)
        Me.ItmActorDelete.Text = "Delete"
        '
        'ItmActorEdit
        '
        Me.ItmActorEdit.Name = "ItmActorEdit"
        Me.ItmActorEdit.Size = New System.Drawing.Size(124, 22)
        Me.ItmActorEdit.Text = "Edit"
        '
        'ItmActorDuplicate
        '
        Me.ItmActorDuplicate.Name = "ItmActorDuplicate"
        Me.ItmActorDuplicate.Size = New System.Drawing.Size(124, 22)
        Me.ItmActorDuplicate.Text = "Duplicate"
        '
        'LblActors
        '
        Me.LblActors.AutoSize = True
        Me.LblActors.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.LblActors.Location = New System.Drawing.Point(2, 321)
        Me.LblActors.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblActors.Name = "LblActors"
        Me.LblActors.Size = New System.Drawing.Size(74, 26)
        Me.LblActors.TabIndex = 6
        Me.LblActors.Text = "Actors"
        '
        'LblActorProperties
        '
        Me.LblActorProperties.AutoSize = True
        Me.TblActorProperties.SetColumnSpan(Me.LblActorProperties, 3)
        Me.LblActorProperties.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.LblActorProperties.Location = New System.Drawing.Point(2, 0)
        Me.LblActorProperties.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblActorProperties.Name = "LblActorProperties"
        Me.LblActorProperties.Size = New System.Drawing.Size(168, 26)
        Me.LblActorProperties.TabIndex = 8
        Me.LblActorProperties.Text = "Actor Properties"
        '
        'LblActorLocation
        '
        Me.LblActorLocation.AutoSize = True
        Me.LblActorLocation.Location = New System.Drawing.Point(2, 47)
        Me.LblActorLocation.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblActorLocation.Name = "LblActorLocation"
        Me.LblActorLocation.Size = New System.Drawing.Size(48, 13)
        Me.LblActorLocation.TabIndex = 9
        Me.LblActorLocation.Text = "Location"
        '
        'NumActorLocX
        '
        Me.NumActorLocX.DecimalPlaces = 1
        Me.NumActorLocX.Location = New System.Drawing.Point(54, 49)
        Me.NumActorLocX.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.NumActorLocX.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.NumActorLocX.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.NumActorLocX.Name = "NumActorLocX"
        Me.NumActorLocX.Size = New System.Drawing.Size(56, 20)
        Me.NumActorLocX.TabIndex = 24
        '
        'NumActorLocY
        '
        Me.NumActorLocY.DecimalPlaces = 1
        Me.NumActorLocY.Location = New System.Drawing.Point(114, 49)
        Me.NumActorLocY.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.NumActorLocY.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.NumActorLocY.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.NumActorLocY.Name = "NumActorLocY"
        Me.NumActorLocY.Size = New System.Drawing.Size(56, 20)
        Me.NumActorLocY.TabIndex = 25
        '
        'NumActorLayer
        '
        Me.NumActorLayer.Location = New System.Drawing.Point(54, 70)
        Me.NumActorLayer.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.NumActorLayer.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.NumActorLayer.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.NumActorLayer.Name = "NumActorLayer"
        Me.NumActorLayer.Size = New System.Drawing.Size(56, 20)
        Me.NumActorLayer.TabIndex = 26
        '
        'LblActorLayer
        '
        Me.LblActorLayer.AutoSize = True
        Me.LblActorLayer.Location = New System.Drawing.Point(2, 68)
        Me.LblActorLayer.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblActorLayer.Name = "LblActorLayer"
        Me.LblActorLayer.Size = New System.Drawing.Size(33, 13)
        Me.LblActorLayer.TabIndex = 12
        Me.LblActorLayer.Text = "Layer"
        '
        'NumActorScale
        '
        Me.NumActorScale.DecimalPlaces = 2
        Me.NumActorScale.Increment = New Decimal(New Integer() {10, 0, 0, 131072})
        Me.NumActorScale.Location = New System.Drawing.Point(54, 91)
        Me.NumActorScale.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.NumActorScale.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumActorScale.Name = "NumActorScale"
        Me.NumActorScale.Size = New System.Drawing.Size(56, 20)
        Me.NumActorScale.TabIndex = 27
        Me.NumActorScale.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'LblActorScale
        '
        Me.LblActorScale.AutoSize = True
        Me.LblActorScale.Location = New System.Drawing.Point(2, 89)
        Me.LblActorScale.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblActorScale.Name = "LblActorScale"
        Me.LblActorScale.Size = New System.Drawing.Size(34, 13)
        Me.LblActorScale.TabIndex = 14
        Me.LblActorScale.Text = "Scale"
        '
        'LblRooms
        '
        Me.LblRooms.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblRooms.AutoSize = True
        Me.LblRooms.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRooms.Location = New System.Drawing.Point(406, 25)
        Me.LblRooms.Margin = New System.Windows.Forms.Padding(2, 0, 7, 0)
        Me.LblRooms.Name = "LblRooms"
        Me.LblRooms.Size = New System.Drawing.Size(82, 26)
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
        Me.TblActorProperties.Location = New System.Drawing.Point(139, 323)
        Me.TblActorProperties.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TblActorProperties.Name = "TblActorProperties"
        Me.TblActorProperties.RowCount = 5
        Me.TblControlsOverall.SetRowSpan(Me.TblActorProperties, 2)
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TblActorProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13.0!))
        Me.TblActorProperties.Size = New System.Drawing.Size(263, 112)
        Me.TblActorProperties.TabIndex = 17
        '
        'LblActorName
        '
        Me.LblActorName.AutoSize = True
        Me.LblActorName.Location = New System.Drawing.Point(2, 26)
        Me.LblActorName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblActorName.Name = "LblActorName"
        Me.LblActorName.Size = New System.Drawing.Size(35, 13)
        Me.LblActorName.TabIndex = 16
        Me.LblActorName.Text = "Name"
        '
        'TxtActorName
        '
        Me.TblActorProperties.SetColumnSpan(Me.TxtActorName, 2)
        Me.TxtActorName.Location = New System.Drawing.Point(54, 28)
        Me.TxtActorName.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TxtActorName.MaxLength = 32
        Me.TxtActorName.Name = "TxtActorName"
        Me.TxtActorName.Size = New System.Drawing.Size(117, 20)
        Me.TxtActorName.TabIndex = 23
        '
        'BtnCreateActor
        '
        Me.BtnCreateActor.Enabled = False
        Me.BtnCreateActor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCreateActor.Location = New System.Drawing.Point(2, 548)
        Me.BtnCreateActor.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.BtnCreateActor.Name = "BtnCreateActor"
        Me.BtnCreateActor.Size = New System.Drawing.Size(133, 45)
        Me.BtnCreateActor.TabIndex = 20
        Me.BtnCreateActor.Text = "Create Actor"
        Me.BtnCreateActor.UseVisualStyleBackColor = True
        '
        'LstActorTags
        '
        Me.LstActorTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstActorTags.ContextMenuStrip = Me.CntxtTags
        Me.LstActorTags.FormattingEnabled = True
        Me.LstActorTags.Location = New System.Drawing.Point(406, 349)
        Me.LstActorTags.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LstActorTags.Name = "LstActorTags"
        Me.LstActorTags.Size = New System.Drawing.Size(133, 195)
        Me.LstActorTags.TabIndex = 28
        '
        'CntxtTags
        '
        Me.CntxtTags.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.CntxtTags.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItmTagsDelete, Me.ItmTagsEdit, Me.ItmTagsDuplicate})
        Me.CntxtTags.Name = "CntxtActors"
        Me.CntxtTags.Size = New System.Drawing.Size(125, 70)
        '
        'ItmTagsDelete
        '
        Me.ItmTagsDelete.Name = "ItmTagsDelete"
        Me.ItmTagsDelete.Size = New System.Drawing.Size(124, 22)
        Me.ItmTagsDelete.Text = "Delete"
        '
        'ItmTagsEdit
        '
        Me.ItmTagsEdit.Name = "ItmTagsEdit"
        Me.ItmTagsEdit.Size = New System.Drawing.Size(124, 22)
        Me.ItmTagsEdit.Text = "Edit"
        '
        'ItmTagsDuplicate
        '
        Me.ItmTagsDuplicate.Name = "ItmTagsDuplicate"
        Me.ItmTagsDuplicate.Size = New System.Drawing.Size(124, 22)
        Me.ItmTagsDuplicate.Text = "Duplicate"
        '
        'BtnAddActorTag
        '
        Me.BtnAddActorTag.Enabled = False
        Me.BtnAddActorTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddActorTag.Location = New System.Drawing.Point(406, 548)
        Me.BtnAddActorTag.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.BtnAddActorTag.Name = "BtnAddActorTag"
        Me.BtnAddActorTag.Size = New System.Drawing.Size(133, 45)
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
        Me.TblControlsOverall.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
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
        Me.TblControlsOverall.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TblControlsOverall.Name = "TblControlsOverall"
        Me.TblControlsOverall.RowCount = 7
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TblControlsOverall.Size = New System.Drawing.Size(685, 573)
        Me.TblControlsOverall.TabIndex = 24
        '
        'ToolBar
        '
        Me.TblControlsOverall.SetColumnSpan(Me.ToolBar, 4)
        Me.ToolBar.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarFile})
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolBar.Size = New System.Drawing.Size(685, 25)
        Me.ToolBar.TabIndex = 0
        Me.ToolBar.Text = "ToolBar"
        '
        'ToolBarFile
        '
        Me.ToolBarFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolBarFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolBarFileOpen, Me.ToolBarFileSaveAs, Me.ToolBarFileSave})
        Me.ToolBarFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarFile.Name = "ToolBarFile"
        Me.ToolBarFile.Size = New System.Drawing.Size(38, 22)
        Me.ToolBarFile.Text = "File"
        '
        'ToolBarFileOpen
        '
        Me.ToolBarFileOpen.Name = "ToolBarFileOpen"
        Me.ToolBarFileOpen.Size = New System.Drawing.Size(123, 22)
        Me.ToolBarFileOpen.Text = "Open..."
        '
        'ToolBarFileSaveAs
        '
        Me.ToolBarFileSaveAs.Name = "ToolBarFileSaveAs"
        Me.ToolBarFileSaveAs.Size = New System.Drawing.Size(123, 22)
        Me.ToolBarFileSaveAs.Text = "Save As..."
        '
        'ToolBarFileSave
        '
        Me.ToolBarFileSave.Name = "ToolBarFileSave"
        Me.ToolBarFileSave.Size = New System.Drawing.Size(123, 22)
        Me.ToolBarFileSave.Text = "Save"
        '
        'BtnAddLevelParam
        '
        Me.BtnAddLevelParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddLevelParam.Location = New System.Drawing.Point(543, 252)
        Me.BtnAddLevelParam.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.BtnAddLevelParam.Name = "BtnAddLevelParam"
        Me.BtnAddLevelParam.Size = New System.Drawing.Size(133, 45)
        Me.BtnAddLevelParam.TabIndex = 33
        Me.BtnAddLevelParam.Text = "Add Parameter"
        Me.BtnAddLevelParam.UseVisualStyleBackColor = True
        '
        'LblLevelParams
        '
        Me.LblLevelParams.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblLevelParams.AutoSize = True
        Me.LblLevelParams.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLevelParams.Location = New System.Drawing.Point(543, 25)
        Me.LblLevelParams.Margin = New System.Windows.Forms.Padding(2, 0, 7, 0)
        Me.LblLevelParams.Name = "LblLevelParams"
        Me.LblLevelParams.Size = New System.Drawing.Size(125, 26)
        Me.LblLevelParams.TabIndex = 34
        Me.LblLevelParams.Text = "Parameters"
        '
        'LstLevelParams
        '
        Me.LstLevelParams.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstLevelParams.ContextMenuStrip = Me.CntxtParameters
        Me.LstLevelParams.FormattingEnabled = True
        Me.LstLevelParams.Location = New System.Drawing.Point(543, 53)
        Me.LstLevelParams.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LstLevelParams.Name = "LstLevelParams"
        Me.LstLevelParams.Size = New System.Drawing.Size(133, 195)
        Me.LstLevelParams.TabIndex = 32
        '
        'CntxtParameters
        '
        Me.CntxtParameters.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.CntxtParameters.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItmParameterDelete, Me.ItmParameterEdit, Me.ItmParameterDuplicate})
        Me.CntxtParameters.Name = "CntxtActors"
        Me.CntxtParameters.Size = New System.Drawing.Size(125, 70)
        '
        'ItmParameterDelete
        '
        Me.ItmParameterDelete.Name = "ItmParameterDelete"
        Me.ItmParameterDelete.Size = New System.Drawing.Size(124, 22)
        Me.ItmParameterDelete.Text = "Delete"
        '
        'ItmParameterEdit
        '
        Me.ItmParameterEdit.Name = "ItmParameterEdit"
        Me.ItmParameterEdit.Size = New System.Drawing.Size(124, 22)
        Me.ItmParameterEdit.Text = "Edit"
        '
        'ItmParameterDuplicate
        '
        Me.ItmParameterDuplicate.Name = "ItmParameterDuplicate"
        Me.ItmParameterDuplicate.Size = New System.Drawing.Size(124, 22)
        Me.ItmParameterDuplicate.Text = "Duplicate"
        '
        'LblActorTags
        '
        Me.LblActorTags.AutoSize = True
        Me.LblActorTags.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!)
        Me.LblActorTags.Location = New System.Drawing.Point(406, 321)
        Me.LblActorTags.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LblActorTags.Name = "LblActorTags"
        Me.LblActorTags.Size = New System.Drawing.Size(116, 26)
        Me.LblActorTags.TabIndex = 28
        Me.LblActorTags.Text = "Actor Tags"
        '
        'BtnRoomAdd
        '
        Me.BtnRoomAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRoomAdd.Location = New System.Drawing.Point(406, 252)
        Me.BtnRoomAdd.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.BtnRoomAdd.Name = "BtnRoomAdd"
        Me.BtnRoomAdd.Size = New System.Drawing.Size(133, 45)
        Me.BtnRoomAdd.TabIndex = 12
        Me.BtnRoomAdd.Text = "Add Room"
        Me.BtnRoomAdd.UseVisualStyleBackColor = True
        '
        'LstRooms
        '
        Me.LstRooms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LstRooms.ContextMenuStrip = Me.CntxtRooms
        Me.LstRooms.FormattingEnabled = True
        Me.LstRooms.Location = New System.Drawing.Point(406, 53)
        Me.LstRooms.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LstRooms.Name = "LstRooms"
        Me.LstRooms.Size = New System.Drawing.Size(133, 195)
        Me.LstRooms.TabIndex = 11
        '
        'CntxtRooms
        '
        Me.CntxtRooms.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.CntxtRooms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItmRoomDelete, Me.ItmRoomDuplicate})
        Me.CntxtRooms.Name = "CntxtActors"
        Me.CntxtRooms.Size = New System.Drawing.Size(125, 48)
        '
        'ItmRoomDelete
        '
        Me.ItmRoomDelete.Name = "ItmRoomDelete"
        Me.ItmRoomDelete.Size = New System.Drawing.Size(124, 22)
        Me.ItmRoomDelete.Text = "Delete"
        '
        'ItmRoomDuplicate
        '
        Me.ItmRoomDuplicate.Name = "ItmRoomDuplicate"
        Me.ItmRoomDuplicate.Size = New System.Drawing.Size(124, 22)
        Me.ItmRoomDuplicate.Text = "Duplicate"
        '
        'FrmLevelEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(685, 573)
        Me.Controls.Add(Me.TblControlsOverall)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
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
End Class
