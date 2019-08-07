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
        Me.pnlRender = New System.Windows.Forms.Panel()
        Me.btnRoomSave = New System.Windows.Forms.Button()
        Me.btnRoomSaveAs = New System.Windows.Forms.Button()
        Me.btnRoomOpen = New System.Windows.Forms.Button()
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
        Me.flwRoomSaveLoad = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblRoomTitle = New System.Windows.Forms.Label()
        Me.tblTagsSummary = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTagName = New System.Windows.Forms.Label()
        Me.txtTagName = New System.Windows.Forms.TextBox()
        Me.btnInstanceCreate = New System.Windows.Forms.Button()
        Me.tblEntities = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRemoveEntity = New System.Windows.Forms.Button()
        Me.btnInstanceDuplicate = New System.Windows.Forms.Button()
        Me.btnInstanceDelete = New System.Windows.Forms.Button()
        Me.btnCreateEntity = New System.Windows.Forms.Button()
        Me.lstTags = New System.Windows.Forms.ListBox()
        Me.tblTagsDetailed = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTagAdd = New System.Windows.Forms.Button()
        Me.btnTagEdit = New System.Windows.Forms.Button()
        Me.btnTagRemove = New System.Windows.Forms.Button()
        Me.tblRoom = New System.Windows.Forms.TableLayoutPanel()
        Me.tblLevel = New System.Windows.Forms.TableLayoutPanel()
        Me.flwLevelSaveLoad = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblLevelTitle = New System.Windows.Forms.Label()
        Me.btnLevelOpen = New System.Windows.Forms.Button()
        Me.btnLevelSaveAs = New System.Windows.Forms.Button()
        Me.btnLevelSave = New System.Windows.Forms.Button()
        Me.tblRoomList = New System.Windows.Forms.TableLayoutPanel()
        Me.lblRoomsList = New System.Windows.Forms.Label()
        Me.btnLevelRoomAdd = New System.Windows.Forms.Button()
        Me.lstRooms = New System.Windows.Forms.ListBox()
        Me.btnLevelRoomRemove = New System.Windows.Forms.Button()
        Me.btnRoomEditCoords = New System.Windows.Forms.Button()
        Me.tblLevelParam = New System.Windows.Forms.TableLayoutPanel()
        Me.lstLevelParams = New System.Windows.Forms.ListBox()
        Me.btnLevelParamAdd = New System.Windows.Forms.Button()
        Me.btnLevelParamRemove = New System.Windows.Forms.Button()
        Me.btnLevelParamEdit = New System.Windows.Forms.Button()
        Me.lblLevelParamsList = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblRoomParamsList = New System.Windows.Forms.Label()
        Me.lstRoomParams = New System.Windows.Forms.ListBox()
        Me.btnAddRoomParam = New System.Windows.Forms.Button()
        Me.btnRemoveRoomParam = New System.Windows.Forms.Button()
        Me.btnEditRoomParam = New System.Windows.Forms.Button()
        CType(Me.numTagLocX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numTagLocY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numTagLayer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numTagScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flwRoomSaveLoad.SuspendLayout()
        Me.tblTagsSummary.SuspendLayout()
        Me.tblEntities.SuspendLayout()
        Me.tblTagsDetailed.SuspendLayout()
        Me.tblRoom.SuspendLayout()
        Me.tblLevel.SuspendLayout()
        Me.flwLevelSaveLoad.SuspendLayout()
        Me.tblRoomList.SuspendLayout()
        Me.tblLevelParam.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlRender
        '
        Me.pnlRender.BackColor = System.Drawing.Color.Black
        Me.pnlRender.Location = New System.Drawing.Point(12, 12)
        Me.pnlRender.Name = "pnlRender"
        Me.pnlRender.Size = New System.Drawing.Size(600, 450)
        Me.pnlRender.TabIndex = 1
        '
        'btnRoomSave
        '
        Me.btnRoomSave.Enabled = False
        Me.btnRoomSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRoomSave.Location = New System.Drawing.Point(330, 3)
        Me.btnRoomSave.Name = "btnRoomSave"
        Me.btnRoomSave.Size = New System.Drawing.Size(100, 53)
        Me.btnRoomSave.TabIndex = 3
        Me.btnRoomSave.Text = "Save"
        Me.btnRoomSave.UseVisualStyleBackColor = True
        '
        'btnRoomSaveAs
        '
        Me.btnRoomSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRoomSaveAs.Location = New System.Drawing.Point(224, 3)
        Me.btnRoomSaveAs.Name = "btnRoomSaveAs"
        Me.btnRoomSaveAs.Size = New System.Drawing.Size(100, 53)
        Me.btnRoomSaveAs.TabIndex = 2
        Me.btnRoomSaveAs.Text = "Save As..."
        Me.btnRoomSaveAs.UseVisualStyleBackColor = True
        '
        'btnRoomOpen
        '
        Me.btnRoomOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRoomOpen.Location = New System.Drawing.Point(118, 3)
        Me.btnRoomOpen.Name = "btnRoomOpen"
        Me.btnRoomOpen.Size = New System.Drawing.Size(100, 53)
        Me.btnRoomOpen.TabIndex = 1
        Me.btnRoomOpen.Text = "Open"
        Me.btnRoomOpen.UseVisualStyleBackColor = True
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
        Me.lstTemplates.Size = New System.Drawing.Size(140, 300)
        Me.lstTemplates.TabIndex = 4
        '
        'btnLoadEntity
        '
        Me.btnLoadEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadEntity.Location = New System.Drawing.Point(3, 334)
        Me.btnLoadEntity.Name = "btnLoadEntity"
        Me.btnLoadEntity.Size = New System.Drawing.Size(140, 69)
        Me.btnLoadEntity.TabIndex = 5
        Me.btnLoadEntity.Text = "Load Entity..."
        Me.btnLoadEntity.UseVisualStyleBackColor = True
        '
        'lstInstances
        '
        Me.lstInstances.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstInstances.FormattingEnabled = True
        Me.lstInstances.ItemHeight = 20
        Me.lstInstances.Location = New System.Drawing.Point(149, 28)
        Me.lstInstances.Name = "lstInstances"
        Me.lstInstances.Size = New System.Drawing.Size(141, 300)
        Me.lstInstances.TabIndex = 7
        '
        'lblInstancesList
        '
        Me.lblInstancesList.AutoSize = True
        Me.lblInstancesList.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstancesList.Location = New System.Drawing.Point(149, 0)
        Me.lblInstancesList.Name = "lblInstancesList"
        Me.lblInstancesList.Size = New System.Drawing.Size(96, 25)
        Me.lblInstancesList.TabIndex = 6
        Me.lblInstancesList.Text = "Instances"
        '
        'lblTags
        '
        Me.lblTags.AutoSize = True
        Me.tblTagsSummary.SetColumnSpan(Me.lblTags, 2)
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
        Me.lblTagLocation.Location = New System.Drawing.Point(3, 61)
        Me.lblTagLocation.Name = "lblTagLocation"
        Me.lblTagLocation.Size = New System.Drawing.Size(70, 20)
        Me.lblTagLocation.TabIndex = 9
        Me.lblTagLocation.Text = "Location"
        '
        'numTagLocX
        '
        Me.numTagLocX.Location = New System.Drawing.Point(79, 64)
        Me.numTagLocX.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.numTagLocX.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.numTagLocX.Name = "numTagLocX"
        Me.numTagLocX.Size = New System.Drawing.Size(84, 26)
        Me.numTagLocX.TabIndex = 10
        '
        'numTagLocY
        '
        Me.numTagLocY.Location = New System.Drawing.Point(169, 64)
        Me.numTagLocY.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.numTagLocY.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.numTagLocY.Name = "numTagLocY"
        Me.numTagLocY.Size = New System.Drawing.Size(84, 26)
        Me.numTagLocY.TabIndex = 11
        '
        'numTagLayer
        '
        Me.numTagLayer.Location = New System.Drawing.Point(79, 100)
        Me.numTagLayer.Maximum = New Decimal(New Integer() {32767, 0, 0, 0})
        Me.numTagLayer.Minimum = New Decimal(New Integer() {32768, 0, 0, -2147483648})
        Me.numTagLayer.Name = "numTagLayer"
        Me.numTagLayer.Size = New System.Drawing.Size(84, 26)
        Me.numTagLayer.TabIndex = 13
        '
        'lblTagLayer
        '
        Me.lblTagLayer.AutoSize = True
        Me.lblTagLayer.Location = New System.Drawing.Point(3, 97)
        Me.lblTagLayer.Name = "lblTagLayer"
        Me.lblTagLayer.Size = New System.Drawing.Size(48, 20)
        Me.lblTagLayer.TabIndex = 12
        Me.lblTagLayer.Text = "Layer"
        '
        'numTagScale
        '
        Me.numTagScale.DecimalPlaces = 2
        Me.numTagScale.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.numTagScale.Location = New System.Drawing.Point(79, 136)
        Me.numTagScale.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.numTagScale.Minimum = New Decimal(New Integer() {256, 0, 0, -2147483648})
        Me.numTagScale.Name = "numTagScale"
        Me.numTagScale.Size = New System.Drawing.Size(84, 26)
        Me.numTagScale.TabIndex = 15
        Me.numTagScale.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblTagScale
        '
        Me.lblTagScale.AutoSize = True
        Me.lblTagScale.Location = New System.Drawing.Point(3, 133)
        Me.lblTagScale.Name = "lblTagScale"
        Me.lblTagScale.Size = New System.Drawing.Size(49, 20)
        Me.lblTagScale.TabIndex = 14
        Me.lblTagScale.Text = "Scale"
        '
        'flwRoomSaveLoad
        '
        Me.flwRoomSaveLoad.Controls.Add(Me.lblRoomTitle)
        Me.flwRoomSaveLoad.Controls.Add(Me.btnRoomOpen)
        Me.flwRoomSaveLoad.Controls.Add(Me.btnRoomSaveAs)
        Me.flwRoomSaveLoad.Controls.Add(Me.btnRoomSave)
        Me.flwRoomSaveLoad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flwRoomSaveLoad.Location = New System.Drawing.Point(463, 3)
        Me.flwRoomSaveLoad.Name = "flwRoomSaveLoad"
        Me.flwRoomSaveLoad.Size = New System.Drawing.Size(455, 63)
        Me.flwRoomSaveLoad.TabIndex = 16
        '
        'lblRoomTitle
        '
        Me.lblRoomTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRoomTitle.AutoSize = True
        Me.lblRoomTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoomTitle.Location = New System.Drawing.Point(3, 11)
        Me.lblRoomTitle.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.lblRoomTitle.Name = "lblRoomTitle"
        Me.lblRoomTitle.Size = New System.Drawing.Size(102, 37)
        Me.lblRoomTitle.TabIndex = 4
        Me.lblRoomTitle.Text = "Room"
        '
        'tblTagsSummary
        '
        Me.tblTagsSummary.ColumnCount = 3
        Me.tblTagsSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTagsSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTagsSummary.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTagsSummary.Controls.Add(Me.lblTagLocation, 0, 2)
        Me.tblTagsSummary.Controls.Add(Me.lblTagLayer, 0, 3)
        Me.tblTagsSummary.Controls.Add(Me.numTagLocY, 2, 2)
        Me.tblTagsSummary.Controls.Add(Me.numTagLayer, 1, 3)
        Me.tblTagsSummary.Controls.Add(Me.lblTags, 0, 0)
        Me.tblTagsSummary.Controls.Add(Me.numTagLocX, 1, 2)
        Me.tblTagsSummary.Controls.Add(Me.numTagScale, 1, 4)
        Me.tblTagsSummary.Controls.Add(Me.lblTagScale, 0, 4)
        Me.tblTagsSummary.Controls.Add(Me.lblTagName, 0, 1)
        Me.tblTagsSummary.Controls.Add(Me.txtTagName, 1, 1)
        Me.tblTagsSummary.Location = New System.Drawing.Point(302, 3)
        Me.tblTagsSummary.Name = "tblTagsSummary"
        Me.tblTagsSummary.RowCount = 5
        Me.tblTagsSummary.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTagsSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblTagsSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblTagsSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblTagsSummary.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblTagsSummary.Size = New System.Drawing.Size(320, 172)
        Me.tblTagsSummary.TabIndex = 17
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
        Me.tblTagsSummary.SetColumnSpan(Me.txtTagName, 2)
        Me.txtTagName.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtTagName.Location = New System.Drawing.Point(79, 28)
        Me.txtTagName.MaxLength = 32
        Me.txtTagName.Name = "txtTagName"
        Me.txtTagName.Size = New System.Drawing.Size(320, 26)
        Me.txtTagName.TabIndex = 17
        '
        'btnInstanceCreate
        '
        Me.btnInstanceCreate.Enabled = False
        Me.btnInstanceCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstanceCreate.Location = New System.Drawing.Point(149, 334)
        Me.btnInstanceCreate.Name = "btnInstanceCreate"
        Me.btnInstanceCreate.Size = New System.Drawing.Size(141, 69)
        Me.btnInstanceCreate.TabIndex = 18
        Me.btnInstanceCreate.Text = "Create Instance"
        Me.btnInstanceCreate.UseVisualStyleBackColor = True
        '
        'tblEntities
        '
        Me.tblEntities.ColumnCount = 2
        Me.tblEntities.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblEntities.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblEntities.Controls.Add(Me.btnRemoveEntity, 0, 4)
        Me.tblEntities.Controls.Add(Me.btnLoadEntity, 0, 2)
        Me.tblEntities.Controls.Add(Me.btnInstanceCreate, 1, 2)
        Me.tblEntities.Controls.Add(Me.lstTemplates, 0, 1)
        Me.tblEntities.Controls.Add(Me.lstInstances, 1, 1)
        Me.tblEntities.Controls.Add(Me.lblInstancesList, 1, 0)
        Me.tblEntities.Controls.Add(Me.lblTemplatesList, 0, 0)
        Me.tblEntities.Controls.Add(Me.btnInstanceDuplicate, 1, 3)
        Me.tblEntities.Controls.Add(Me.btnInstanceDelete, 1, 4)
        Me.tblEntities.Controls.Add(Me.btnCreateEntity, 0, 3)
        Me.tblEntities.Location = New System.Drawing.Point(3, 3)
        Me.tblEntities.Name = "tblEntities"
        Me.tblEntities.RowCount = 5
        Me.tblRoom.SetRowSpan(Me.tblEntities, 2)
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEntities.Size = New System.Drawing.Size(293, 555)
        Me.tblEntities.TabIndex = 19
        '
        'btnRemoveEntity
        '
        Me.btnRemoveEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveEntity.Location = New System.Drawing.Point(3, 484)
        Me.btnRemoveEntity.Name = "btnRemoveEntity"
        Me.btnRemoveEntity.Size = New System.Drawing.Size(140, 69)
        Me.btnRemoveEntity.TabIndex = 22
        Me.btnRemoveEntity.Text = "Remove Template"
        Me.btnRemoveEntity.UseVisualStyleBackColor = True
        '
        'btnInstanceDuplicate
        '
        Me.btnInstanceDuplicate.Enabled = False
        Me.btnInstanceDuplicate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstanceDuplicate.Location = New System.Drawing.Point(149, 409)
        Me.btnInstanceDuplicate.Name = "btnInstanceDuplicate"
        Me.btnInstanceDuplicate.Size = New System.Drawing.Size(141, 69)
        Me.btnInstanceDuplicate.TabIndex = 19
        Me.btnInstanceDuplicate.Text = "Duplicate"
        Me.btnInstanceDuplicate.UseVisualStyleBackColor = True
        '
        'btnInstanceDelete
        '
        Me.btnInstanceDelete.Enabled = False
        Me.btnInstanceDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstanceDelete.Location = New System.Drawing.Point(149, 484)
        Me.btnInstanceDelete.Name = "btnInstanceDelete"
        Me.btnInstanceDelete.Size = New System.Drawing.Size(141, 69)
        Me.btnInstanceDelete.TabIndex = 20
        Me.btnInstanceDelete.Text = "Delete Instance"
        Me.btnInstanceDelete.UseVisualStyleBackColor = True
        '
        'btnCreateEntity
        '
        Me.btnCreateEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateEntity.Location = New System.Drawing.Point(3, 409)
        Me.btnCreateEntity.Name = "btnCreateEntity"
        Me.btnCreateEntity.Size = New System.Drawing.Size(140, 69)
        Me.btnCreateEntity.TabIndex = 21
        Me.btnCreateEntity.Text = "Create Entity"
        Me.btnCreateEntity.UseVisualStyleBackColor = True
        '
        'lstTags
        '
        Me.lstTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTags.FormattingEnabled = True
        Me.lstTags.ItemHeight = 20
        Me.lstTags.Location = New System.Drawing.Point(3, 3)
        Me.lstTags.Name = "lstTags"
        Me.tblTagsDetailed.SetRowSpan(Me.lstTags, 3)
        Me.lstTags.Size = New System.Drawing.Size(186, 371)
        Me.lstTags.TabIndex = 21
        '
        'tblTagsDetailed
        '
        Me.tblTagsDetailed.ColumnCount = 2
        Me.tblTagsDetailed.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.tblTagsDetailed.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.tblTagsDetailed.Controls.Add(Me.btnTagAdd, 1, 0)
        Me.tblTagsDetailed.Controls.Add(Me.lstTags, 0, 0)
        Me.tblTagsDetailed.Controls.Add(Me.btnTagEdit, 1, 1)
        Me.tblTagsDetailed.Controls.Add(Me.btnTagRemove, 1, 2)
        Me.tblTagsDetailed.Location = New System.Drawing.Point(302, 181)
        Me.tblTagsDetailed.Name = "tblTagsDetailed"
        Me.tblTagsDetailed.RowCount = 3
        Me.tblTagsDetailed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTagsDetailed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTagsDetailed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTagsDetailed.Size = New System.Drawing.Size(320, 377)
        Me.tblTagsDetailed.TabIndex = 22
        '
        'btnTagAdd
        '
        Me.btnTagAdd.Enabled = False
        Me.btnTagAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagAdd.Location = New System.Drawing.Point(195, 3)
        Me.btnTagAdd.Name = "btnTagAdd"
        Me.btnTagAdd.Size = New System.Drawing.Size(122, 69)
        Me.btnTagAdd.TabIndex = 21
        Me.btnTagAdd.Text = "Add Tag"
        Me.btnTagAdd.UseVisualStyleBackColor = True
        '
        'btnTagEdit
        '
        Me.btnTagEdit.Enabled = False
        Me.btnTagEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagEdit.Location = New System.Drawing.Point(195, 78)
        Me.btnTagEdit.Name = "btnTagEdit"
        Me.btnTagEdit.Size = New System.Drawing.Size(122, 69)
        Me.btnTagEdit.TabIndex = 22
        Me.btnTagEdit.Text = "Edit Tag"
        Me.btnTagEdit.UseVisualStyleBackColor = True
        '
        'btnTagRemove
        '
        Me.btnTagRemove.Enabled = False
        Me.btnTagRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTagRemove.Location = New System.Drawing.Point(195, 153)
        Me.btnTagRemove.Name = "btnTagRemove"
        Me.btnTagRemove.Size = New System.Drawing.Size(122, 69)
        Me.btnTagRemove.TabIndex = 23
        Me.btnTagRemove.Text = "Remove Tag"
        Me.btnTagRemove.UseVisualStyleBackColor = True
        '
        'tblRoom
        '
        Me.tblRoom.ColumnCount = 3
        Me.tblRoom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRoom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRoom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRoom.Controls.Add(Me.tblLevel, 0, 3)
        Me.tblRoom.Controls.Add(Me.TableLayoutPanel1, 2, 1)
        Me.tblRoom.Controls.Add(Me.tblTagsDetailed, 1, 2)
        Me.tblRoom.Controls.Add(Me.tblTagsSummary, 1, 1)
        Me.tblRoom.Controls.Add(Me.tblEntities, 0, 1)
        Me.tblRoom.Location = New System.Drawing.Point(618, 12)
        Me.tblRoom.Name = "tblRoom"
        Me.tblRoom.RowCount = 1
        Me.tblRoom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRoom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRoom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRoom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRoom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRoom.Size = New System.Drawing.Size(930, 974)
        Me.tblRoom.TabIndex = 24
        '
        'tblLevel
        '
        Me.tblLevel.ColumnCount = 2
        Me.tblRoom.SetColumnSpan(Me.tblLevel, 3)
        Me.tblLevel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLevel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLevel.Controls.Add(Me.flwLevelSaveLoad, 0, 0)
        Me.tblLevel.Controls.Add(Me.tblRoomList, 1, 1)
        Me.tblLevel.Controls.Add(Me.flwRoomSaveLoad, 1, 0)
        Me.tblLevel.Controls.Add(Me.tblLevelParam, 0, 1)
        Me.tblLevel.Location = New System.Drawing.Point(3, 564)
        Me.tblLevel.Name = "tblLevel"
        Me.tblLevel.RowCount = 2
        Me.tblLevel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLevel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLevel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblLevel.Size = New System.Drawing.Size(921, 341)
        Me.tblLevel.TabIndex = 26
        '
        'flwLevelSaveLoad
        '
        Me.flwLevelSaveLoad.Controls.Add(Me.lblLevelTitle)
        Me.flwLevelSaveLoad.Controls.Add(Me.btnLevelOpen)
        Me.flwLevelSaveLoad.Controls.Add(Me.btnLevelSaveAs)
        Me.flwLevelSaveLoad.Controls.Add(Me.btnLevelSave)
        Me.flwLevelSaveLoad.Location = New System.Drawing.Point(3, 3)
        Me.flwLevelSaveLoad.Name = "flwLevelSaveLoad"
        Me.flwLevelSaveLoad.Size = New System.Drawing.Size(454, 63)
        Me.flwLevelSaveLoad.TabIndex = 17
        '
        'lblLevelTitle
        '
        Me.lblLevelTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLevelTitle.AutoSize = True
        Me.lblLevelTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevelTitle.Location = New System.Drawing.Point(3, 11)
        Me.lblLevelTitle.Margin = New System.Windows.Forms.Padding(3, 0, 10, 0)
        Me.lblLevelTitle.Name = "lblLevelTitle"
        Me.lblLevelTitle.Size = New System.Drawing.Size(91, 37)
        Me.lblLevelTitle.TabIndex = 4
        Me.lblLevelTitle.Text = "Level"
        '
        'btnLevelOpen
        '
        Me.btnLevelOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLevelOpen.Location = New System.Drawing.Point(107, 3)
        Me.btnLevelOpen.Name = "btnLevelOpen"
        Me.btnLevelOpen.Size = New System.Drawing.Size(100, 53)
        Me.btnLevelOpen.TabIndex = 1
        Me.btnLevelOpen.Text = "Open"
        Me.btnLevelOpen.UseVisualStyleBackColor = True
        '
        'btnLevelSaveAs
        '
        Me.btnLevelSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLevelSaveAs.Location = New System.Drawing.Point(213, 3)
        Me.btnLevelSaveAs.Name = "btnLevelSaveAs"
        Me.btnLevelSaveAs.Size = New System.Drawing.Size(100, 53)
        Me.btnLevelSaveAs.TabIndex = 2
        Me.btnLevelSaveAs.Text = "Save As..."
        Me.btnLevelSaveAs.UseVisualStyleBackColor = True
        '
        'btnLevelSave
        '
        Me.btnLevelSave.Enabled = False
        Me.btnLevelSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLevelSave.Location = New System.Drawing.Point(319, 3)
        Me.btnLevelSave.Name = "btnLevelSave"
        Me.btnLevelSave.Size = New System.Drawing.Size(100, 53)
        Me.btnLevelSave.TabIndex = 3
        Me.btnLevelSave.Text = "Save"
        Me.btnLevelSave.UseVisualStyleBackColor = True
        '
        'tblRoomList
        '
        Me.tblRoomList.ColumnCount = 2
        Me.tblRoomList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.tblRoomList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.tblRoomList.Controls.Add(Me.lblRoomsList, 0, 0)
        Me.tblRoomList.Controls.Add(Me.btnLevelRoomAdd, 1, 1)
        Me.tblRoomList.Controls.Add(Me.lstRooms, 0, 1)
        Me.tblRoomList.Controls.Add(Me.btnLevelRoomRemove, 1, 2)
        Me.tblRoomList.Controls.Add(Me.btnRoomEditCoords, 1, 3)
        Me.tblRoomList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblRoomList.Location = New System.Drawing.Point(463, 72)
        Me.tblRoomList.Name = "tblRoomList"
        Me.tblRoomList.RowCount = 4
        Me.tblRoomList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRoomList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRoomList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRoomList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRoomList.Size = New System.Drawing.Size(455, 266)
        Me.tblRoomList.TabIndex = 25
        '
        'lblRoomsList
        '
        Me.lblRoomsList.AutoSize = True
        Me.lblRoomsList.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoomsList.Location = New System.Drawing.Point(3, 0)
        Me.lblRoomsList.Name = "lblRoomsList"
        Me.lblRoomsList.Size = New System.Drawing.Size(145, 25)
        Me.lblRoomsList.TabIndex = 0
        Me.lblRoomsList.Text = "Rooms in Level"
        '
        'btnLevelRoomAdd
        '
        Me.btnLevelRoomAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLevelRoomAdd.Location = New System.Drawing.Point(276, 28)
        Me.btnLevelRoomAdd.Name = "btnLevelRoomAdd"
        Me.btnLevelRoomAdd.Size = New System.Drawing.Size(133, 69)
        Me.btnLevelRoomAdd.TabIndex = 21
        Me.btnLevelRoomAdd.Text = "Add Room"
        Me.btnLevelRoomAdd.UseVisualStyleBackColor = True
        '
        'lstRooms
        '
        Me.lstRooms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstRooms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRooms.FormattingEnabled = True
        Me.lstRooms.ItemHeight = 20
        Me.lstRooms.Location = New System.Drawing.Point(3, 28)
        Me.lstRooms.Name = "lstRooms"
        Me.tblRoomList.SetRowSpan(Me.lstRooms, 3)
        Me.lstRooms.Size = New System.Drawing.Size(267, 235)
        Me.lstRooms.TabIndex = 21
        '
        'btnLevelRoomRemove
        '
        Me.btnLevelRoomRemove.Enabled = False
        Me.btnLevelRoomRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLevelRoomRemove.Location = New System.Drawing.Point(276, 103)
        Me.btnLevelRoomRemove.Name = "btnLevelRoomRemove"
        Me.btnLevelRoomRemove.Size = New System.Drawing.Size(133, 69)
        Me.btnLevelRoomRemove.TabIndex = 23
        Me.btnLevelRoomRemove.Text = "Remove Room"
        Me.btnLevelRoomRemove.UseVisualStyleBackColor = True
        '
        'btnRoomEditCoords
        '
        Me.btnRoomEditCoords.Enabled = False
        Me.btnRoomEditCoords.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRoomEditCoords.Location = New System.Drawing.Point(276, 178)
        Me.btnRoomEditCoords.Name = "btnRoomEditCoords"
        Me.btnRoomEditCoords.Size = New System.Drawing.Size(133, 69)
        Me.btnRoomEditCoords.TabIndex = 22
        Me.btnRoomEditCoords.Text = "Edit Coords"
        Me.btnRoomEditCoords.UseVisualStyleBackColor = True
        '
        'tblLevelParam
        '
        Me.tblLevelParam.ColumnCount = 2
        Me.tblLevelParam.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.tblLevelParam.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.tblLevelParam.Controls.Add(Me.lstLevelParams, 0, 1)
        Me.tblLevelParam.Controls.Add(Me.btnLevelParamAdd, 1, 1)
        Me.tblLevelParam.Controls.Add(Me.btnLevelParamRemove, 1, 2)
        Me.tblLevelParam.Controls.Add(Me.btnLevelParamEdit, 1, 3)
        Me.tblLevelParam.Controls.Add(Me.lblLevelParamsList, 0, 0)
        Me.tblLevelParam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblLevelParam.Location = New System.Drawing.Point(3, 72)
        Me.tblLevelParam.Name = "tblLevelParam"
        Me.tblLevelParam.RowCount = 4
        Me.tblLevelParam.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLevelParam.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLevelParam.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLevelParam.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLevelParam.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblLevelParam.Size = New System.Drawing.Size(454, 266)
        Me.tblLevelParam.TabIndex = 24
        '
        'lstLevelParams
        '
        Me.lstLevelParams.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstLevelParams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstLevelParams.FormattingEnabled = True
        Me.lstLevelParams.ItemHeight = 20
        Me.lstLevelParams.Location = New System.Drawing.Point(3, 28)
        Me.lstLevelParams.Name = "lstLevelParams"
        Me.tblLevelParam.SetRowSpan(Me.lstLevelParams, 3)
        Me.lstLevelParams.Size = New System.Drawing.Size(266, 235)
        Me.lstLevelParams.TabIndex = 21
        '
        'btnLevelParamAdd
        '
        Me.btnLevelParamAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLevelParamAdd.Location = New System.Drawing.Point(275, 28)
        Me.btnLevelParamAdd.Name = "btnLevelParamAdd"
        Me.btnLevelParamAdd.Size = New System.Drawing.Size(133, 69)
        Me.btnLevelParamAdd.TabIndex = 21
        Me.btnLevelParamAdd.Text = "Add Param"
        Me.btnLevelParamAdd.UseVisualStyleBackColor = True
        '
        'btnLevelParamRemove
        '
        Me.btnLevelParamRemove.Enabled = False
        Me.btnLevelParamRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLevelParamRemove.Location = New System.Drawing.Point(275, 103)
        Me.btnLevelParamRemove.Name = "btnLevelParamRemove"
        Me.btnLevelParamRemove.Size = New System.Drawing.Size(133, 69)
        Me.btnLevelParamRemove.TabIndex = 23
        Me.btnLevelParamRemove.Text = "Remove Param"
        Me.btnLevelParamRemove.UseVisualStyleBackColor = True
        '
        'btnLevelParamEdit
        '
        Me.btnLevelParamEdit.Enabled = False
        Me.btnLevelParamEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLevelParamEdit.Location = New System.Drawing.Point(275, 178)
        Me.btnLevelParamEdit.Name = "btnLevelParamEdit"
        Me.btnLevelParamEdit.Size = New System.Drawing.Size(133, 69)
        Me.btnLevelParamEdit.TabIndex = 22
        Me.btnLevelParamEdit.Text = "Edit Param"
        Me.btnLevelParamEdit.UseVisualStyleBackColor = True
        '
        'lblLevelParamsList
        '
        Me.lblLevelParamsList.AutoSize = True
        Me.lblLevelParamsList.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLevelParamsList.Location = New System.Drawing.Point(3, 0)
        Me.lblLevelParamsList.Name = "lblLevelParamsList"
        Me.lblLevelParamsList.Size = New System.Drawing.Size(164, 25)
        Me.lblLevelParamsList.TabIndex = 0
        Me.lblLevelParamsList.Text = "Level Parameters"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblRoomParamsList, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lstRoomParams, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnAddRoomParam, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnRemoveRoomParam, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.btnEditRoomParam, 1, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(628, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.tblRoom.SetRowSpan(Me.TableLayoutPanel1, 3)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(299, 553)
        Me.TableLayoutPanel1.TabIndex = 25
        '
        'lblRoomParamsList
        '
        Me.lblRoomParamsList.AutoSize = True
        Me.lblRoomParamsList.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoomParamsList.Location = New System.Drawing.Point(3, 0)
        Me.lblRoomParamsList.Name = "lblRoomParamsList"
        Me.lblRoomParamsList.Size = New System.Drawing.Size(168, 25)
        Me.lblRoomParamsList.TabIndex = 0
        Me.lblRoomParamsList.Text = "Room Parameters"
        '
        'lstRoomParams
        '
        Me.lstRoomParams.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstRoomParams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRoomParams.FormattingEnabled = True
        Me.lstRoomParams.ItemHeight = 20
        Me.lstRoomParams.Location = New System.Drawing.Point(3, 28)
        Me.lstRoomParams.Name = "lstRoomParams"
        Me.TableLayoutPanel1.SetRowSpan(Me.lstRoomParams, 3)
        Me.lstRoomParams.Size = New System.Drawing.Size(173, 522)
        Me.lstRoomParams.TabIndex = 21
        '
        'btnAddRoomParam
        '
        Me.btnAddRoomParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddRoomParam.Location = New System.Drawing.Point(182, 28)
        Me.btnAddRoomParam.Name = "btnAddRoomParam"
        Me.btnAddRoomParam.Size = New System.Drawing.Size(114, 69)
        Me.btnAddRoomParam.TabIndex = 21
        Me.btnAddRoomParam.Text = "Add Param"
        Me.btnAddRoomParam.UseVisualStyleBackColor = True
        '
        'btnRemoveRoomParam
        '
        Me.btnRemoveRoomParam.Enabled = False
        Me.btnRemoveRoomParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveRoomParam.Location = New System.Drawing.Point(182, 103)
        Me.btnRemoveRoomParam.Name = "btnRemoveRoomParam"
        Me.btnRemoveRoomParam.Size = New System.Drawing.Size(114, 69)
        Me.btnRemoveRoomParam.TabIndex = 23
        Me.btnRemoveRoomParam.Text = "Remove Param"
        Me.btnRemoveRoomParam.UseVisualStyleBackColor = True
        '
        'btnEditRoomParam
        '
        Me.btnEditRoomParam.Enabled = False
        Me.btnEditRoomParam.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditRoomParam.Location = New System.Drawing.Point(182, 178)
        Me.btnEditRoomParam.Name = "btnEditRoomParam"
        Me.btnEditRoomParam.Size = New System.Drawing.Size(114, 69)
        Me.btnEditRoomParam.TabIndex = 22
        Me.btnEditRoomParam.Text = "Edit Param"
        Me.btnEditRoomParam.UseVisualStyleBackColor = True
        '
        'FrmLevelEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(1731, 987)
        Me.Controls.Add(Me.tblRoom)
        Me.Controls.Add(Me.pnlRender)
        Me.DoubleBuffered = True
        Me.Name = "FrmLevelEditor"
        Me.Text = "Level Editor"
        CType(Me.numTagLocX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numTagLocY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numTagLayer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numTagScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flwRoomSaveLoad.ResumeLayout(False)
        Me.flwRoomSaveLoad.PerformLayout()
        Me.tblTagsSummary.ResumeLayout(False)
        Me.tblTagsSummary.PerformLayout()
        Me.tblEntities.ResumeLayout(False)
        Me.tblEntities.PerformLayout()
        Me.tblTagsDetailed.ResumeLayout(False)
        Me.tblRoom.ResumeLayout(False)
        Me.tblLevel.ResumeLayout(False)
        Me.flwLevelSaveLoad.ResumeLayout(False)
        Me.flwLevelSaveLoad.PerformLayout()
        Me.tblRoomList.ResumeLayout(False)
        Me.tblRoomList.PerformLayout()
        Me.tblLevelParam.ResumeLayout(False)
        Me.tblLevelParam.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlRender As Panel
    Friend WithEvents btnRoomSave As Button
    Friend WithEvents btnRoomSaveAs As Button
    Friend WithEvents btnRoomOpen As Button
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
    Friend WithEvents flwRoomSaveLoad As FlowLayoutPanel
    Friend WithEvents tblTagsSummary As TableLayoutPanel
    Friend WithEvents lblTagName As Label
    Friend WithEvents txtTagName As TextBox
    Friend WithEvents btnInstanceCreate As Button
    Friend WithEvents tblEntities As TableLayoutPanel
    Friend WithEvents btnInstanceDuplicate As Button
    Friend WithEvents btnInstanceDelete As Button
    Friend WithEvents lstTags As ListBox
    Friend WithEvents tblTagsDetailed As TableLayoutPanel
    Friend WithEvents btnTagAdd As Button
    Friend WithEvents btnTagEdit As Button
    Friend WithEvents btnTagRemove As Button
    Friend WithEvents lblRoomTitle As Label
    Friend WithEvents tblRoom As TableLayoutPanel
    Friend WithEvents btnCreateEntity As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblRoomParamsList As Label
    Friend WithEvents lstRoomParams As ListBox
    Friend WithEvents btnAddRoomParam As Button
    Friend WithEvents btnRemoveRoomParam As Button
    Friend WithEvents btnEditRoomParam As Button
    Friend WithEvents btnRemoveEntity As Button
    Friend WithEvents tblLevel As TableLayoutPanel
    Friend WithEvents flwLevelSaveLoad As FlowLayoutPanel
    Friend WithEvents lblLevelTitle As Label
    Friend WithEvents btnLevelOpen As Button
    Friend WithEvents btnLevelSaveAs As Button
    Friend WithEvents btnLevelSave As Button
    Friend WithEvents tblRoomList As TableLayoutPanel
    Friend WithEvents lblRoomsList As Label
    Friend WithEvents btnLevelRoomAdd As Button
    Friend WithEvents lstRooms As ListBox
    Friend WithEvents btnLevelRoomRemove As Button
    Friend WithEvents btnRoomEditCoords As Button
    Friend WithEvents tblLevelParam As TableLayoutPanel
    Friend WithEvents lblLevelParamsList As Label
    Friend WithEvents lstLevelParams As ListBox
    Friend WithEvents btnLevelParamAdd As Button
    Friend WithEvents btnLevelParamRemove As Button
    Friend WithEvents btnLevelParamEdit As Button
End Class
