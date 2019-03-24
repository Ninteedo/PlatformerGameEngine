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
        'reads the entity stored in a given file

        If IO.File.Exists(fileLocation) = True Then
            Dim reader As New IO.StreamReader(fileLocation)
            Dim fileText As String = reader.ReadToEnd

            reader.Close()

            ent = EntityStringHandler.ReadEntityString(fileText)
        Else
            PRE2.DisplayError("Couldn't find file " & fileLocation)
        End If
    End Sub


    Dim ent As PRE2.Entity          'the user's created entity

    'sprites

    Dim sprites() As PRE2.Sprite



    'frames

    Dim frames() As PRE2.Frame

    Private Sub DrawFramePreview(frameToDraw As PRE2.Frame)
        'draws the given frame in the preview box

        Dim panelCanvas As New PaintEventArgs(pnlFramePreview.CreateGraphics, New Rectangle(New Point(0, 0), pnlFramePreview.Size))
        Dim previewEntity As New PRE2.Entity({frameToDraw}, {}, New PointF(panelCanvas.ClipRectangle.Width / 2, panelCanvas.ClipRectangle.Height / 2))
        Dim rendererEngine As New PRE2 With {.entities = {previewEntity}, .panelCanvasGameArea = panelCanvas}

    End Sub

    'tags

    Dim tags() As PRE2.Tag

    Private Sub btnTagsNew_Click(sender As Object, e As EventArgs) Handles btnTagsNew.Click
        'opens FrmTagMaker to allow the user to create tags

        Dim tagMaker As New FrmTagMaker
        tagMaker.ShowDialog()

        If tagMaker.userFinished = True Then
            Try
                ReDim Preserve tags(UBound(tags) + 1)
            Catch ex As Exception
                ReDim tags(0)
            End Try

            tags(UBound(tags)) = tagMaker.createdTag

            lstTags.Items.Add(tagMaker.createdTag.name)
        End If
    End Sub

    Private Sub btnTagsEdit_Click(sender As Object, e As EventArgs) Handles btnTagsEdit.Click
        'opens FrmTagMaker with the selected tag already loaded

        Dim tagIndex As Integer = lstTags.SelectedIndex

        If tagIndex > -1 Then
            Dim tagMaker As New FrmTagMaker With {
                .createdTag = tags(tagIndex)
            }

            For argIndex As Integer = 0 To UBound(tagMaker.createdTag.args)
                Dim currentArg As Object = tagMaker.createdTag.args(argIndex)
                Dim dataTypeIndex As Integer

                Try
                    Dim test As PRE2.Tag = currentArg
                    dataTypeIndex = 2   'tag
                Catch ex As Exception
                    If IsNumeric(currentArg) = True Then
                        dataTypeIndex = 0   'number
                    Else
                        dataTypeIndex = 1   'text
                    End If
                End Try

                tagMaker.AddArgument(currentArg, dataTypeIndex)
            Next argIndex

            tagMaker.txtName.Text = tagMaker.createdTag.name
            tagMaker.ShowDialog()

            If tagMaker.userFinished = True Then
                tags(tagIndex) = tagMaker.createdTag
                lstTags.Items(tagIndex) = tagMaker.createdTag.name
            End If
        End If
    End Sub
End Class