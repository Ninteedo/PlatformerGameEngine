'Richard Holmes
'24/03/2019
'Tag editor for entity maker

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmTagMaker

    Public dataTypes() As String = {"number", "text", "tag"}
    Public createdTag As New PRE2.Tag
    Public userFinished As Boolean = False


    Private Sub FrmTagMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbDataType.Items.AddRange(dataTypes)
    End Sub

    Private Sub lstArguments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstArguments.SelectedIndexChanged
        'if the user hasn't selected an argument then remove argument button is disabled

        If lstArguments.SelectedIndex > -1 Then
            btnArgRemove.Enabled = True
        Else
            btnArgRemove.Enabled = False
        End If
    End Sub

    Private Sub btnArgAdd_Click(sender As Object, e As EventArgs) Handles btnArgAdd.Click

        Select Case cmbDataType.SelectedIndex
            Case 0      'number
                Dim userInput As String = InputBox("Please enter a numerical value", "Enter Number", "0.0")

                If userInput.Length > 0 Then
                    If IsNumeric(userInput) = False Then
                        PRE2.DisplayError(userInput & " is not a number")
                    Else
                        AddArgument(Val(userInput), cmbDataType.SelectedIndex)
                    End If
                End If
            Case 1      'string
                Dim userInput As String = InputBox("Please enter text", "Enter text")

                If userInput.Length > 0 Then
                    AddArgument(userInput, cmbDataType.SelectedIndex)
                End If
            Case 2      'tag
                Dim tagMaker As New FrmTagMaker

                tagMaker.ShowDialog()

                If tagMaker.userFinished = True Then
                    AddArgument(tagMaker.createdTag, cmbDataType.SelectedIndex)
                End If
        End Select
    End Sub

    Private Sub btnArgRemove_Click(sender As Object, e As EventArgs) Handles btnArgRemove.Click
        If lstArguments.SelectedIndex > -1 Then
            Dim removeIndex As Integer = lstArguments.SelectedIndex

            RemoveArgument(removeIndex)
            lstArguments.Items.RemoveAt(removeIndex)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'pressed by the user if they want to stop working on the tag and not use it

        Me.Close()
    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        'pressed by the user when they are done creating the tag

        createdTag.name = txtName.Text

        If Len(createdTag.name) > 0 Then            'not a valid tag if it doesn't have a name
            userFinished = True
        End If
        Me.Close()
    End Sub

    Public Sub AddArgument(argument As Object, dataTypeIndex As Integer)
        'adds the given argument to the user's created tag's arguments

        Select Case dataTypeIndex
            Case 0      'number
                If IsNumeric(argument) = False Then
                    PRE2.DisplayError(argument & " is not a number")
                    Exit Sub
                Else
                    lstArguments.Items.Add("num:" & Trim(Str(argument)))
                End If
            Case 1      'text
                lstArguments.Items.Add("text:" & argument)
            Case 2      'string
                lstArguments.Items.Add("tag:" & argument.name)
        End Select

        Try
            ReDim Preserve createdTag.args(UBound(createdTag.args) + 1)
        Catch ex As Exception
            ReDim createdTag.args(0)
        End Try

        createdTag.args(UBound(createdTag.args)) = argument
    End Sub

    Private Sub RemoveArgument(argIndex As UInteger)
        'removes the argument at the given index from the created tag

        For index As Integer = argIndex + 1 To UBound(createdTag.args)
            createdTag.args(index - 1) = createdTag.args(index)
        Next index

        'used for reducing the length of the array of the arguments
        If UBound(createdTag.args) > 0 Then
            ReDim Preserve createdTag.args(UBound(createdTag.args) - 1)
        Else        'if removing only argument, then array becomes nothing
            createdTag.args = Nothing
        End If
    End Sub


End Class