'Richard Holmes
'24/03/2019
'Tag editor for actor maker

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmTagMaker

    'TODO: clean this all up

    Public dataTypes() As String = {"number", "text", "tag"}
    Private tagInCreation As New Tag("NewTag")
    Public arguments As Object
    Public userFinished As Boolean = False

    Public ReadOnly Property CreatedTag As Tag
        Get
            If IsArray(arguments) Then
                tagInCreation.SetArgument(ArrayToString(arguments))
            ElseIf Not IsNothing(arguments) Then
                tagInCreation.SetArgument(arguments.ToString)
            End If
            Return tagInCreation
        End Get
    End Property


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(startTag As Tag)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        tagInCreation = startTag

        arguments = InterpretValue(tagInCreation.argument)

        'For argIndex As Integer = 0 To UBound(arguments)
        '    AddArgument(arguments(argIndex))
        'Next argIndex

        RefreshArgumentsList()
        txtName.Text = RemoveQuotes(tagInCreation.name)
    End Sub

    Private Sub FrmTagMaker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbDataType.Items.AddRange(dataTypes)

        'uses basic tag maker instead since this one is a mess
        Using basicTagMaker As New FrmTagMakerBasic With {.TagCreated = CreatedTag}
            basicTagMaker.ShowDialog()
            If basicTagMaker.userFinished Then
                arguments = basicTagMaker.TagCreated.argument
                tagInCreation = basicTagMaker.TagCreated
            End If
            userFinished = basicTagMaker.userFinished
            Me.Close()
        End Using
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
                    AddArgument(AddQuotes(userInput))
                End If
            Case 2      'tag
                Dim tagMaker As New FrmTagMaker

                tagMaker.ShowDialog()

                If tagMaker.userFinished = True Then
                    AddArgument(tagMaker.tagInCreation)
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

        tagInCreation.name = txtName.Text

        If Len(tagInCreation.name) > 0 Then            'not a valid tag if it doesn't have a name
            userFinished = True
        End If
        Me.Close()
    End Sub

    Private Sub RefreshArgumentsList()
        lstArguments.Items.Clear()

        If Not IsNothing(arguments) Then
            If IsArray(arguments) Then
                For Each arg As Object In arguments
                    'Dim argString As String

                    'If IsNumeric(arg) = True Then      'number
                    '    argString = "num:" & Trim(Val(arg))
                    'ElseIf Not IsNothing(New Tag(arg.ToString)) Then       'another tag
                    '    argString = arg.ToString
                    'Else                'plain string
                    '    argString = "text:" & arg
                    'End If
                    lstArguments.Items.Add(arg.ToString)
                Next arg
            Else
                lstArguments.Items.Add(arguments.ToString)
            End If
        End If
    End Sub

    Public Sub AddArgument(argument As Object)
        'adds the given argument to the user's created tag's arguments

        If IsNothing(tagInCreation.InterpretArgument()) = False Then
            ReDim Preserve arguments(UBound(arguments) + 1)
        Else
            ReDim arguments(0)
        End If

        tagInCreation.InterpretArgument()(UBound(arguments)) = argument
        RefreshArgumentsList()
    End Sub

    Private Sub RemoveArgument(argIndex As UInteger)
        'removes the argument at the given index from the created tag

        For index As Integer = argIndex To UBound(arguments) - 1
            arguments(index) = arguments(index + 1)
        Next index

        'used for reducing the length of the array of the arguments
        If UBound(arguments) > 0 Then
            ReDim Preserve arguments(UBound(arguments) - 1)
        Else        'if removing only argument, then array becomes nothing
            arguments = Nothing
        End If

        RefreshArgumentsList()
    End Sub

    Private Sub BtnBasicEditor_Click(sender As Object, e As EventArgs) Handles btnBasicEditor.Click
        'uses FrmTagMakerBasic

        Using basicTagMaker As New FrmTagMakerBasic With {.TagCreated = tagInCreation}
            basicTagMaker.ShowDialog()

            If basicTagMaker.userFinished Then
                tagInCreation = basicTagMaker.TagCreated
                RefreshArgumentsList()
                txtName.Text = tagInCreation.name
            End If
        End Using
    End Sub
End Class