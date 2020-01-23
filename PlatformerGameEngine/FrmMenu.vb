'Richard Holmes
'22/03/2019
'Menu for platformer engine

Imports PlatformerGameEngine.My.Resources

Public Class FrmMenu

    Dim _menuButtons() As Button
    Dim _menuLayouts() As MenuOptions
    Dim _currentMenuIndex As Integer = 0

    Private Class MenuOptions
        'this is used to store the layout of each menu, so the same buttons can be reused

        Public ReadOnly MenuItems() As MenuItem
        Public ReadOnly PreviousMenuIndex As Integer        'stores the index of the menu to go to when the back button is pressed

        Public Sub New(menuItems As MenuItem(), previousMenuIndex As Integer)
            Me.MenuItems = menuItems
            Me.PreviousMenuIndex = previousMenuIndex
        End Sub
    End Class

    Private Class MenuItem
        Public ReadOnly ButtonText As String     'the text that is displayed on the button
        Public ReadOnly Behaviour As MenuLink    'what happens when the user clicks on the button

        Public Sub New(buttonText As String, behaviour As MenuLink)
            Me.ButtonText = buttonText
            Me.Behaviour = behaviour
        End Sub
    End Class

    Private Enum MenuLink As Integer
        'the behaviours available for when a button is pressed

        LoadGame
        OptionsMenu
        ToolsMenu
        SpriteMaker
        LevelEditor
        Scoreboard
    End Enum

    Private Sub FrmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initialisation()
    End Sub

    Private Sub Initialisation()
        _menuButtons = {btnMenu1, btnMenu2, btnMenu3}

        'assigns the click event of all the menu buttons to the MenuButtonClicked procedure
        For index As Integer = 0 To UBound(_menuButtons)
            AddHandler _menuButtons(index).Click, AddressOf MenuButtonClicked
        Next index

        'sets up the menu layouts
        Dim mainMenu As New MenuOptions(
                    {New MenuItem("Load Game", MenuLink.LoadGame),
                     New MenuItem("Options", MenuLink.OptionsMenu),
                     New MenuItem("Tools", MenuLink.ToolsMenu)}, -1)
        Dim toolsMenu As New MenuOptions(
                    {New MenuItem("Sprite Maker", MenuLink.SpriteMaker),
                     New MenuItem("Level Editor", MenuLink.LevelEditor),
                     New MenuItem("Scoreboard", MenuLink.Scoreboard)}, 0)

        _menuLayouts = {mainMenu, toolsMenu}

        'shows the main menu
        SetMenu(0)
    End Sub

    Private Sub MenuButtonClicked(sender As Object, e As EventArgs)
        'handles the click event for menu buttons 1-3

        Dim buttonIndex As Integer = -1
        Dim layout As MenuOptions = _menuLayouts(_currentMenuIndex)

        'finds the index of the button pressed
        For index As Integer = 0 To UBound(_menuButtons)
            If _menuButtons(index).Name = sender.Name Then
                buttonIndex = index
            End If
        Next index

        If buttonIndex > -1 AndAlso buttonIndex <= UBound(layout.MenuItems) AndAlso
            Not IsNothing(layout.MenuItems(buttonIndex)) Then   'checks index is valid
            Select Case layout.MenuItems(buttonIndex).Behaviour      'does whatever the behaviour linked to the button is
                Case MenuLink.LoadGame
                    'loads the game selected by the user

                    Using openDialog As New OpenFileDialog _
                                With {.Filter = LevelFileFilter, .Title = "Select Level"}
                        'MsgBox("Please select the level file")
                        If openDialog.ShowDialog() = DialogResult.OK Then
                            Using game As New FrmGameExecutor(ReadFile(openDialog.FileName))
                                game.ShowDialog()
                            End Using
                        End If
                    End Using
                Case MenuLink.OptionsMenu

                Case MenuLink.ToolsMenu
                    'changes the menu layout to one relevant to the tools menu

                    SetMenu(1)
                Case MenuLink.SpriteMaker
                    'opens the sprite maker tool

                    Using spriteMaker As New FrmSpriteMaker
                        spriteMaker.ShowDialog()
                    End Using
                Case MenuLink.LevelEditor
                    'opens the level editor tool

                    Using levelEditor As New FrmLevelEditor
                        levelEditor.ShowDialog()
                    End Using
                Case MenuLink.Scoreboard
                    'opens the scoreboard viewer

                    Using scoreboard As New FrmScoreboard
                        If Not scoreboard.IsDisposed Then
                            scoreboard.ShowDialog()
                        End If
                    End Using
                Case Else
                    DisplayError("Unknown Menu Behaviour")
            End Select
        End If
    End Sub

    Private Sub SetMenu(menuIndex As Integer)
        'changes the text on the menu buttons and the behaviours they are linked to

        Dim layout As MenuOptions = _menuLayouts(menuIndex)

        'shows button text or hides button if there is no menu item
        For index As Integer = 0 To UBound(_menuButtons)
            Dim itemPresent As Boolean = index > -1 AndAlso index <= UBound(layout.MenuItems) AndAlso
                                         Not IsNothing(layout.MenuItems(index))
            If itemPresent Then
                _menuButtons(index).Text = layout.MenuItems(index).ButtonText
            End If
            _menuButtons(index).Enabled = itemPresent
            _menuButtons(index).Visible = itemPresent
        Next index

        If layout.PreviousMenuIndex = -1 Then   'if on the top level menu then back is replaced by quit
            btnMenuBack.Text = "Quit"
        Else
            btnMenuBack.Text = "Back"
        End If

        _currentMenuIndex = menuIndex
    End Sub

    Private Sub BtnMenuBack_Click(sender As Object, e As EventArgs) Handles btnMenuBack.Click
        'code for the user pressing the back button

        _currentMenuIndex = _menuLayouts(_currentMenuIndex).PreviousMenuIndex

        If _currentMenuIndex >= 0 Then       'on the top level menu back is replaced by quit
            SetMenu(_currentMenuIndex)
        Else
            End         'closes application
        End If
    End Sub

End Class