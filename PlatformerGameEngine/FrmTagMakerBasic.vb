'Richard Holmes
'07/09/2019
'Tag maker for direct editing of the JSON string

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmTagMakerBasic

    Public userFinished As Boolean = False

    Public Property TagCreated As PRE2.Tag
        Get
            Return JSONToTag(txtJSON.Text)
        End Get
        Set(value As PRE2.Tag)
            txtJSON.Text = TagToJSON(value)
        End Set
    End Property

    Private Sub BtnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        userFinished = True
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        userFinished = False
        Me.Close()
    End Sub

    Private Sub VerifyCreatedTag()
        'TODO: show user when there are errors when creating a tag from the JSON
    End Sub
End Class