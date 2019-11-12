'used to standardize what happens when the user right clicks on a list

Public Class ListManipulationController
    Private listControl As ListBox
    Private rightClickMenu As ContextMenuStrip
    Private toolStripOptions() As ToolStripMenuItem
    Private name As String

    Public Sub New(ByRef listControl As ListBox, ByRef form As Form, ByVal actions() As Tag, ByVal name As String)
        Me.listControl = listControl
        Me.name = name

        Dim menuItems(UBound(actions)) As ToolStripMenuItem
        For index As Integer = 0 To UBound(actions)
            menuItems(index) = New ToolStripMenuItem(actions(index).InterpretArgument("displayText").ToString)
        Next
    End Sub

    Public Sub ItemClicked()

    End Sub
End Class
