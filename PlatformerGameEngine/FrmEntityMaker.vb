'Richard Holmes
'24/03/2019
'Entity creator for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmEntityMaker

    Dim delayTimer As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub FrmEntityMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
    End Sub

    Private Sub Initialisation()

    End Sub


    'save load

    Dim saveLocation As String

    Private ReadOnly Property EntityString
        Get
            'Dim frames() As PRE2.Frame
            'Dim tags() As PRE2.Tag

            Dim ent As New PRE2.Entity With {
                .frames = frames,
                .tags = tags
            }

            Return EntityStringHandler.CreateEntityString(ent)
        End Get
    End Property

    Private Sub btnOpen_Click(sender As Button, e As EventArgs) Handles btnOpen.Click
        'asks the user to select a .sprt file and reads it

        Dim openDialog As New OpenFileDialog With {.Filter = "Entity file (*.ent)|*.ent", .Multiselect = False, .CheckFileExists = True}

        If openDialog.ShowDialog = DialogResult.OK Then
            saveLocation = openDialog.FileName
            ReadEntityFromFile(saveLocation)

            btnSave.Enabled = True
        End If
    End Sub

    Private Sub btnSaveAs_Click(sender As Button, e As EventArgs) Handles btnSaveAs.Click
        'asks the user to select a save location, then saves the sprite there and enables the regular save button

        Dim saveDialog As New SaveFileDialog With {.Filter = "Sprite file (*.sprt)|*.sprt"}

        If saveDialog.ShowDialog = DialogResult.OK Then
            saveLocation = saveDialog.FileName
            SaveTextToFile(EntityString, saveLocation)

            btnSave.Enabled = True
        End If
    End Sub

    Private Sub btnSave_Click(sender As Button, e As EventArgs) Handles btnSave.Click
        'saves the file to the already selected location

        If IO.File.Exists(saveLocation) Then
            SaveTextToFile(EntityString, saveLocation)
        Else
            PRE2.DisplayError("Couldn't find file at " & saveLocation)
        End If
    End Sub

    Private Sub SaveTextToFile(text As String, fileLocation As String)
        Dim writer As New IO.StreamWriter(fileLocation)
        Dim toWrite As String = text

        For Each c As Char In toWrite
            writer.Write(c)
        Next c

        writer.Close()
    End Sub

    Private Sub ReadEntityFromFile(fileLocation As String)

    End Sub

    'sprites

    Dim sprites() As PRE2.Sprite



    'frames

    Dim frames() As PRE2.Frame


    'tags

    Dim tags() As PRE2.Tag


End Class