'Richard Holmes
'22/03/2019
'Menu for platformer engine

Public Class FrmMenu

    Public menuButtons() As Button
    Dim menuLayouts() As MenuOptions
    Dim currentMenuIndex As Integer = 0
    Dim delayTimer As New Timer With {.Interval = 1, .Enabled = False}

    Private Structure MenuOptions
        'this is used to store the layout of each menu, so the same buttons can be reused

        Dim buttonText() As String      'the text that is displayed on each button
        Dim behaviours() As String      'the actual behaviour performed when the user clicks the button
        Dim previousMenuIndex As Integer        'stores the layout index of the menu to go to when the back button is pressed
    End Structure

    Private Sub FrmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'starts the delay timer for the proper initialisation

        AddHandler delayTimer.Tick, AddressOf Initialisation
        delayTimer.Start()
    End Sub

    Private Sub Initialisation()
        'proper of initialisation of the form

        delayTimer.Stop()

        menuButtons = {btnMenu1, btnMenu2, btnMenu3}
        menuLayouts = {New MenuOptions With {
            .buttonText = {"Load Game", "Options", "Tools"},
            .behaviours = {"LoadGame", "OpenOptionsMenu", "OpenToolsMenu"},
            .previousMenuIndex = -1
        }}
        SetMenu(0)
        For index As Integer = 0 To UBound(menuButtons)
            AddHandler menuButtons(index).Click, AddressOf MenuButtonClicked
        Next index
    End Sub

    Private Sub MenuButtonClicked(sender As Button, e As EventArgs)
        'handles the click event for menu buttons 1-3

        Dim buttonIndex As Integer = -1
        Dim layout As MenuOptions = menuLayouts(currentMenuIndex)

        For index As Integer = 0 To UBound(menuButtons)
            If menuButtons(index).Name = sender.Name Then
                buttonIndex = index
            End If
        Next index

        If buttonIndex > -1 AndAlso buttonIndex <= UBound(layout.behaviours) AndAlso IsNothing(layout.behaviours(buttonIndex)) = False Then
            Select Case layout.behaviours(buttonIndex)      'does whatever the behaviour linked to the button is
                Case "LoadGame"
                    LoadGame()
                Case "OpenOptionsMenu"
                    OpenOptionsMenu()
                Case "OpenToolsMenu"
                    OpenToolsMenu()
                Case "OpenSpriteMaker"
                    OpenSpriteMaker()
                Case "OpenLevelEditor"
                    OpenLevelEditor()
                Case "OpenActorMaker"
                    OpenActorMaker()
            End Select
        Else
            MsgBox("This button doesn't do anything")
        End If
    End Sub

    Private Sub LoadGame()
        'loads the game selected by the user

        Using openDialog As New OpenFileDialog With {.Filter = "Loader File (*.ldr)|*.ldr", .Multiselect = False}
            MsgBox("Please select the game loader file")
            If openDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Using game As New FrmGame With {.loaderFileLocation = openDialog.FileName}
                    game.ShowDialog()
                End Using
            End If
        End Using
    End Sub

    Private Sub OpenOptionsMenu()
        'changes the menu layout to one relevant to the options menu

        'Dim optionsMenuLayout As New MenuOptions With {
        '    .buttonText = {},
        '    .behaviours = {},
        '    .previousMenuIndex = currentMenuIndex
        '    }
        'ReDim Preserve menuLayouts(UBound(menuLayouts) + 1)
        'menuLayouts(UBound(menuLayouts)) = optionsMenuLayout
        'currentMenuIndex = UBound(menuLayouts)
        'SetMenu(currentMenuIndex)

        'open FrmOptions

        'Dim test As New PanelRenderEngine2.Tag(InputBox("Input Tag"))
        'MsgBox(test.ToString)
        'Dim argument As Object = InterpretValue(test.argument)
    End Sub

    Private Sub OpenToolsMenu()
        'changes the menu layout to one relevant to the tools menu

        Dim toolsMenuLayout As New MenuOptions With {
            .buttonText = {"Sprite Maker", "Actor Maker", "Level Editor"},
            .behaviours = {"OpenSpriteMaker", "OpenActorMaker", "OpenLevelEditor"},
            .previousMenuIndex = currentMenuIndex
        }
        ReDim Preserve menuLayouts(UBound(menuLayouts) + 1)
        menuLayouts(UBound(menuLayouts)) = toolsMenuLayout
        currentMenuIndex = UBound(menuLayouts)
        SetMenu(currentMenuIndex)
    End Sub

    Private Sub SetMenu(menuIndex As Integer)
        'changes the text on the menu buttons and the behaviours they are linked to

        Dim layout As MenuOptions = menuLayouts(menuIndex)

        For index As Integer = 0 To UBound(menuButtons)
            If index <= UBound(layout.buttonText) AndAlso IsNothing(layout.buttonText(index)) = False Then
                menuButtons(index).Text = layout.buttonText(index)
                menuButtons(index).Visible = True
            Else
                menuButtons(index).Text = Trim(Str(index + 1))
                menuButtons(index).Visible = False
            End If
        Next index

        If layout.previousMenuIndex = -1 Then       'if on the top level menu then back is replaced by quit
            btnMenuBack.Text = "Quit"
        Else
            btnMenuBack.Text = "Back"
        End If
    End Sub

    Private Sub btnMenuBack_Click(sender As Object, e As EventArgs) Handles btnMenuBack.Click
        'code for the user pressing the back button

        currentMenuIndex = menuLayouts(currentMenuIndex).previousMenuIndex

        If currentMenuIndex >= 0 Then       'on the top level menu back is replaced by quit
            SetMenu(currentMenuIndex)
        Else
            End         'closes application
        End If
    End Sub

    Private Sub OpenSpriteMaker()
        'opens the sprite maker tool

        Using spriteMaker As New FrmSpriteMaker
            spriteMaker.ShowDialog()
        End Using
    End Sub

    Private Sub OpenLevelEditor()
        'opens the level editor tool

        Using levelEditor As New FrmLevelEditor
            levelEditor.ShowDialog()
        End Using
    End Sub

    Private Sub OpenActorMaker()
        'opens the actor maker tool

        Using actorMaker As New FrmActorMaker
            actorMaker.ShowDialog()
        End Using
    End Sub
End Class