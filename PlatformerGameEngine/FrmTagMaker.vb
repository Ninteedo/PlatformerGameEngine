Public Class FrmTagMaker

    Public UserFinished As Boolean = False

#Region "Constructors"

    Public Sub New(Optional startTag As Tag = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        If Not IsNothing(startTag) Then
            TxtJson.Text = startTag.ToString
        End If
    End Sub

#End Region

    Public ReadOnly Property CreatedTag As Tag
        Get
            Dim temp As Tag = JsonToTag(TxtJson.Text)
            Return temp
        End Get
    End Property

#Region "Controls"

    Private Sub BtnFinish_Click(sender As Button, e As EventArgs) Handles BtnFinish.Click
        If VerifyCreatedTag() Then
            UserFinished = True
            Close()
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Button, e As EventArgs) Handles BtnCancel.Click
        UserFinished = False
        Close()
    End Sub

#End Region

    Private Function VerifyCreatedTag() As Boolean
        'checks whether the created tag has correct basic syntax

        Dim valid As Boolean = True

        Dim openedBrackets() As String = {}    'stores an ordered list of currently opened brackets: { or [
        Dim tagString As String = RemoveSubStrings(TxtJson.Text)
        Dim errorIndex As Integer = -1

        For cIndex As Integer = 0 To Len(tagString) - 1
            Dim c As String = tagString(cIndex)

            If c = "[" Or c = "{" Then
                openedBrackets = InsertItem(openedBrackets, c)
            ElseIf c = "]" Or c = "}" Then
                Dim lastOpenedBracket As String = openedBrackets(UBound(openedBrackets))
                If IsNothing(openedBrackets) Then
                    'no opened brackets
                    errorIndex = cIndex
                    Exit For
                ElseIf (lastOpenedBracket = "{" And c <> "}") Or (lastOpenedBracket = "[" And c <> "]") Then
                    'checks that if the brackets match
                    errorIndex = cIndex
                    Exit For
                Else
                    openedBrackets = RemoveItem(openedBrackets, UBound(openedBrackets))
                End If
            End If
        Next

        If errorIndex = -1 And (Not IsNothing(openedBrackets) AndAlso UBound(openedBrackets) > -1) Then   'check for remaining opened brackets
            errorIndex = Len(TxtJson.Text) - 1
        End If

        If errorIndex <> -1 Then
            DisplayError("Tag is invalid, please see selection for what is wrong")
            valid = False
            TxtJson.Select(errorIndex, 1)
        End If

        Return valid
    End Function

End Class
