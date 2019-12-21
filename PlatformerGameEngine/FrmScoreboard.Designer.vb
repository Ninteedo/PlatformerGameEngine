<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmScoreboard
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
        Me.LstScoreboard = New System.Windows.Forms.ListView()
        Me.ClmPlace = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ClmPlayer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ClmScore = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.LblScores = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblFind = New System.Windows.Forms.Label()
        Me.TxtFind = New System.Windows.Forms.TextBox()
        Me.LblGameName = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LstScoreboard
        '
        Me.LstScoreboard.AutoArrange = False
        Me.LstScoreboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LstScoreboard.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ClmPlace, Me.ClmPlayer, Me.ClmScore})
        Me.TableLayoutPanel1.SetColumnSpan(Me.LstScoreboard, 3)
        Me.LstScoreboard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstScoreboard.FullRowSelect = True
        Me.LstScoreboard.GridLines = True
        Me.LstScoreboard.HideSelection = False
        Me.LstScoreboard.Location = New System.Drawing.Point(3, 69)
        Me.LstScoreboard.MultiSelect = False
        Me.LstScoreboard.Name = "LstScoreboard"
        Me.LstScoreboard.Size = New System.Drawing.Size(268, 460)
        Me.LstScoreboard.TabIndex = 0
        Me.LstScoreboard.UseCompatibleStateImageBehavior = False
        Me.LstScoreboard.View = System.Windows.Forms.View.Details
        '
        'ClmPlace
        '
        Me.ClmPlace.Text = "Place"
        '
        'ClmPlayer
        '
        Me.ClmPlayer.Text = "Player"
        '
        'ClmScore
        '
        Me.ClmScore.Text = "Score"
        '
        'BtnClose
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.BtnClose, 3)
        Me.BtnClose.Location = New System.Drawing.Point(3, 535)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(266, 63)
        Me.BtnClose.TabIndex = 1
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'LblScores
        '
        Me.LblScores.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LblScores.AutoSize = True
        Me.LblScores.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblScores.Location = New System.Drawing.Point(3, 0)
        Me.LblScores.Name = "LblScores"
        Me.LblScores.Size = New System.Drawing.Size(146, 46)
        Me.LblScores.TabIndex = 2
        Me.LblScores.Text = "Scores"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.LstScoreboard, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnClose, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LblScores, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblFind, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtFind, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblGameName, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(274, 601)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'LblFind
        '
        Me.LblFind.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblFind.AutoSize = True
        Me.LblFind.Location = New System.Drawing.Point(155, 13)
        Me.LblFind.Name = "LblFind"
        Me.LblFind.Size = New System.Drawing.Size(44, 20)
        Me.LblFind.TabIndex = 3
        Me.LblFind.Text = "Find:"
        '
        'TxtFind
        '
        Me.TxtFind.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtFind.Location = New System.Drawing.Point(205, 10)
        Me.TxtFind.MaxLength = 3
        Me.TxtFind.Name = "TxtFind"
        Me.TxtFind.Size = New System.Drawing.Size(64, 26)
        Me.TxtFind.TabIndex = 4
        '
        'LblGameName
        '
        Me.LblGameName.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.LblGameName, 3)
        Me.LblGameName.Location = New System.Drawing.Point(3, 46)
        Me.LblGameName.Name = "LblGameName"
        Me.LblGameName.Size = New System.Drawing.Size(99, 20)
        Me.LblGameName.TabIndex = 5
        Me.LblGameName.Text = "Game Name"
        '
        'FrmScoreboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.ClientSize = New System.Drawing.Size(274, 601)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmScoreboard"
        Me.Text = "Scoreboard"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LstScoreboard As ListView
    Friend WithEvents ClmPlace As ColumnHeader
    Friend WithEvents ClmPlayer As ColumnHeader
    Friend WithEvents ClmScore As ColumnHeader
    Friend WithEvents BtnClose As Button
    Friend WithEvents LblScores As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LblFind As Label
    Friend WithEvents TxtFind As TextBox
    Friend WithEvents LblGameName As Label
End Class
