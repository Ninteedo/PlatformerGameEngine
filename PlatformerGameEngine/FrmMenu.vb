'Richard Holmes
'22/03/2019
'Menu for platformer engine

Public Class FrmMenu

    Dim menuButtons() As Button
    Dim menuLayouts() As MenuOptions
    Dim currentMenuIndex As Integer = 0

    Private Class MenuOptions
        'this is used to store the layout of each menu, so the same buttons can be reused

        Public buttonText() As String      'the text that is displayed on each button
        Public behaviours() As MenuLink      'the behaviour performed when the user clicks the button
        Public previousMenuIndex As Integer        'stores the index of the menu to go to when the back button is pressed

        Public Sub New(buttonText() As String, behaviours() As MenuLink, previousMenuIndex As Integer)
            Me.buttonText = buttonText
            Me.behaviours = behaviours
            Me.previousMenuIndex = previousMenuIndex
        End Sub
    End Class

    Private Enum MenuLink As Integer
        'the behaviours available for when a button is pressed

        loadGame
        optionsMenu
        toolsMenu
        spriteMaker
        levelEditor
    End Enum

    Private Sub FrmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initialisation()
    End Sub

    Private Sub Initialisation()
        menuButtons = {btnMenu1, btnMenu2, btnMenu3}

        'sets up the menu layouts
        Dim mainMenu As New MenuOptions({"Load Game", "Options", "Tools"}, {MenuLink.loadGame, MenuLink.optionsMenu, MenuLink.toolsMenu}, -1)
        Dim toolsMenu As New MenuOptions({"Sprite Maker", "Level Editor"}, {MenuLink.spriteMaker, MenuLink.levelEditor}, 0)
        menuLayouts = {mainMenu, toolsMenu}
        SetMenu(0)

        'assigns the click event of all the menu buttons to the MenuButtonClicked procedure
        For index As Integer = 0 To UBound(menuButtons)
            AddHandler menuButtons(index).Click, AddressOf MenuButtonClicked
        Next index
    End Sub

    Private Sub MenuButtonClicked(sender As Object, e As EventArgs)
        'handles the click event for menu buttons 1-3

        Dim buttonIndex As Integer = -1
        Dim layout As MenuOptions = menuLayouts(currentMenuIndex)

        'finds the index of the button pressed
        For index As Integer = 0 To UBound(menuButtons)
            If menuButtons(index).Name = sender.Name Then
                buttonIndex = index
            End If
        Next index

        If buttonIndex > -1 AndAlso buttonIndex <= UBound(layout.behaviours) AndAlso Not IsNothing(layout.behaviours(buttonIndex)) Then
            Select Case layout.behaviours(buttonIndex)      'does whatever the behaviour linked to the button is
                Case MenuLink.loadGame
                    'loads the game selected by the user

                    Using openDialog As New OpenFileDialog With {.Filter = "Level File (*.lvl)|*.lvl", .Title = "Select Level", .Multiselect = False}
                        'MsgBox("Please select the level file")
                        If openDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                            Using game As New FrmGame(ReadFile(openDialog.FileName))
                                game.ShowDialog()
                            End Using
                        End If
                    End Using
                Case MenuLink.optionsMenu
                    'open FrmOptions

                    Dim example As String = "0 < 1 AND 1 < 2 OR 2 < 3 AND 3 < 4 OR 4 < 5 OR 5 < 6"
                    Dim result As Boolean = AssessCondition(example)
                Case MenuLink.toolsMenu
                    'changes the menu layout to one relevant to the tools menu

                    SetMenu(1)
                Case MenuLink.spriteMaker
                    'opens the sprite maker tool

                    Using spriteMaker As New FrmSpriteMaker
                        spriteMaker.ShowDialog()
                    End Using
                Case MenuLink.levelEditor
                    'opens the level editor tool

                    Using levelEditor As New FrmLevelEditor
                        levelEditor.ShowDialog()
                    End Using
                Case Else
                    DisplayError("Unknown Menu Behaviour")
            End Select
        End If
    End Sub

    Private Sub SetMenu(menuIndex As Integer)
        'changes the text on the menu buttons and the behaviours they are linked to

        Dim layout As MenuOptions = menuLayouts(menuIndex)

        For index As Integer = 0 To UBound(menuButtons)
            If index <= UBound(layout.buttonText) AndAlso IsNothing(layout.buttonText(index)) = False Then
                menuButtons(index).Text = layout.buttonText(index)
                menuButtons(index).Enabled = True
            Else
                menuButtons(index).Text = Trim(Str(index + 1))
                menuButtons(index).Enabled = False
            End If
        Next index

        If layout.previousMenuIndex = -1 Then       'if on the top level menu then back is replaced by quit
            btnMenuBack.Text = "Quit"
        Else
            btnMenuBack.Text = "Back"
        End If

        currentMenuIndex = menuIndex
    End Sub

    Private Sub BtnMenuBack_Click(sender As Object, e As EventArgs) Handles btnMenuBack.Click
        'code for the user pressing the back button

        currentMenuIndex = menuLayouts(currentMenuIndex).previousMenuIndex

        If currentMenuIndex >= 0 Then       'on the top level menu back is replaced by quit
            SetMenu(currentMenuIndex)
        Else
            End         'closes application
        End If
    End Sub

End Class