'Richard Holmes
'24/03/2019
'Tag editor for entity maker

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmTagMaker

    Public dataTypes() As String = {"number", "text", "tag"}
    Public createdTag As New PRE2.Tag
    Public userFinished As Boolean = False


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(startTag As PRE2.Tag)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        createdTag = startTag

        'For argIndex As Integer = 0 To UBound(createdTag.args)
        '    AddArgument(createdTag.args(argIndex))
        'Next argIndex

        RefreshArgumentsList()
        txtName.Text = createdTag.name
    End Sub

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
                        AddArgument(Val(userInput))
                    End If
                End If
            Case 1      'string
                Dim userInput As String = InputBox("Please enter text", "Enter text")

                If userInput.Length > 0 Then
                    AddArgument(userInput)
                End If
            Case 2      'tag
                Dim tagMaker As New FrmTagMaker

                tagMaker.ShowDialog()

                If tagMaker.userFinished = True Then
                    AddArgument(tagMaker.createdTag)
                End If
        End Select
    End Sub

    Private Sub btnArgRemove_Click(sender As Object, e As EventArgs) Handles btnArgRemove.Click
        If lstArguments.SelectedIndex > -1 Then
            Dim removeIndex As Integer = lstArguments.SelectedIndex

            RemoveArgument(removeIndex)
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

    Private Sub RefreshArgumentsList()
        lstArguments.Items.Clear()

        If IsNothing(createdTag.args) = False Then
            For Each arg As Object In createdTag.args
                Dim argString As String

                If IsNumeric(arg) = True Then      'number
                    argString = "num:" & Trim(Val(arg))
                ElseIf Not IsNothing(New PRE2.Tag(arg.ToString)) Then       'another tag
                    argString = arg.ToString
                Else                'plain string
                    argString = "text:" & arg
                End If
                lstArguments.Items.Add(argString)
            Next arg
        End If
    End Sub

    Public Sub AddArgument(argument As Object)
        'adds the given argument to the user's created tag's arguments

        If IsNothing(createdTag.args) = False Then
            ReDim Preserve createdTag.args(UBound(createdTag.args) + 1)
        Else
            ReDim createdTag.args(0)
        End If

        createdTag.args(UBound(createdTag.args)) = argument
        RefreshArgumentsList()
    End Sub

    Private Sub RemoveArgument(argIndex As UInteger)
        'removes the argument at the given index from the created tag

        For index As Integer = argIndex To UBound(createdTag.args) - 1
            createdTag.args(index) = createdTag.args(index + 1)
        Next index

        'used for reducing the length of the array of the arguments
        If UBound(createdTag.args) > 0 Then
            ReDim Preserve createdTag.args(UBound(createdTag.args) - 1)
        Else        'if removing only argument, then array becomes nothing
            createdTag.args = Nothing
        End If

        RefreshArgumentsList()
    End Sub


End Class