'Richard Holmes
'29/03/2019
'Level editor for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class FrmLevelEditor

	'initialisation

    Dim delayTimer As New Timer With {.Interval = 1, .Enabled = False}

    Private Sub FrmLevelEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler delayTimer.Tick, AddressOf Initialisation
    End Sub

    Private Sub Initialisation()
        LayoutInitialisation()
    End Sub

    Private Sub LayoutInitialisation()

    End Sub

    'save load

    Dim saveLocation As String = ""

    Private Function LoadLevel(fileLocation As String) As FrmGame.Level

    End Function

    Private Sub SaveLevel(levelToSave As FrmGame.Level, saveLocation As String)
        'saves a level (not the rooms) to a file

        Dim levelString As String = ""

        'adds a editParam line for each parameter




        'adds a loadEnt line for each template
        For Each template As PRE2.Entity In levelToSave.templates
            If template.HasTag("fileName") = True Then
                Dim line As String = "loadEnt: " & template.FindTag("fileName").args(0) & "/" & template.name

                'adds each tag
                For Each thisTag As PRE2.Tag In template.tags
                    line += "/" & thisTag.ToString
                Next

                levelString += line & Environment.NewLine
            Else
                PRE2.DisplayError("Template " & template.name & " is missing tag 'fileName' so couldn't be saved")
            End If
        Next

        'adds a loadRoom line for each room
        For Each currentRoom As FrmGame.Room In levelToSave.rooms
            'adds an addEnt line for each instance
            For Each instance As PRE2.Entity In currentRoom.instances
                Dim templateOfInstance As PRE2.Entity           'used so that tags can be compared and identical ones can be ignored
                For Each template As PRE2.Entity In levelToSave.templates
                    If template.name = instance.FindTag("templateName").args(0) Then
                        templateOfInstance = template
                        Exit For
                    End If
                Next template

                If IsNothing(templateOfInstance) = True Then
                    PRE2.DisplayError("Could not find a template called " & instance.FindTag("templateName").args(0) & " for instance " & instance.name)
                Else
                    Dim line As String = "addEnt: " & instance.FindTag("templateName").args(0) & "/" & instance.name

                    'adds each added tag to the line, which is not identical to one which the template has
                    For Each thisTag As PRE2.Tag In instance.tags
                        If IsNothing(templateOfInstance.FindTag(thisTag.name)) = False AndAlso templateOfInstance.FindTag(thisTag.name) <> thisTag Then
                            line += "/" & thisTag.ToString
                        End If
                    Next thisTag

                    levelString += line & Environment.NewLine
                End If
            Next instance
        Next currentRoom

        'saves level string to file
        PRE2.WriteFile(saveLocation, levelString)
    End Sub

    Private Function LoadRoom(fileLocation As String) As FrmGame.Room
        'loads a room from a file (is this necessary?)

        Return FrmGame.LoadRoomFile(fileLocation, thisLevel)
    End Function

    Private Sub SaveRoom(roomToSave As FrmGame.Room, saveLocation As String)
        'saves a single room to a file, can only be loaded when the level is loaded
    End Sub

    Private Function LoadEntityTemplate(fileLocation As String) As PRE2.Entity
        'returns an entity loaded from a file for a template
    End Function

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click

    End Sub

    Private Sub btnSaveAs_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub


    Private Function CreateRoomString(thisRoom As FrmGame.Room) As String
        'returns a string to save the given room
    End Function


    'render

    Dim renderer As New PRE2
    Dim thisLevel As FrmGame.Level
    Dim thisRoom As FrmGame.Room

    Private Sub RenderLevel()

    End Sub


    'control

    Dim tagControls() As Object = {txtTagName, numTagLocX, numTagLocY, numTagLayer, numTagScale, lstTags, btnTagAdd, btnTagEdit, btnTagRemove}

    Private Sub ControlInitialisation()
        ToggleTagControls(False)
    End Sub




    Private Sub AddEntityInstance(ByVal template As PRE2.Entity)
        'creates a new instance from the given entity

        Dim newInstance As PRE2.Entity = template
        newInstance.AddTag(New PRE2.Tag("templateName", {template.name}))       'instance stores the name of its template so the instance can be created from the correct template when loading

        'checks that there are any instances yet
        If IsNothing(thisRoom.instances) = True Then
            newInstance.name = template.name & "-1"

            ReDim thisRoom.instances(0)
            thisRoom.instances(0) = newInstance
        Else
            Dim copyNumber As Integer = 0           'used to find which number needs to added to the end of the instance name so there aren't any duplicate names
            Dim nameUnique As Boolean = False
            Dim generatedName As String = ""

            Do
                copyNumber += 1
                generatedName = template.name & "-" & Trim(Str(copyNumber))
                nameUnique = True

                'checks if name is unique
                For Each instance As PRE2.Entity In thisRoom.instances
                    If instance.name = generatedName Then
                        nameUnique = False
                        Exit For
                    End If
                Next
            Loop Until nameUnique = True

            newInstance.name = generatedName

            ReDim Preserve thisRoom.instances(UBound(thisRoom.instances) + 1)
            thisRoom.instances(UBound(thisRoom.instances)) = newInstance
        End If

        RefreshInstancesArray()
    End Sub

    Private Sub RemoveEntityInstance(instanceIndex As Integer)
        'deletes the instance with the given index

        'removes the instance from the room
        If instanceIndex >= 0 And instanceIndex <= UBound(thisRoom.instances) Then
            For index As Integer = instanceIndex To UBound(thisRoom.instances) - 1
                thisRoom.instances(index) = thisRoom.instances(index + 1)
            Next index

            ReDim Preserve thisRoom.instances(UBound(thisRoom.instances) - 1)

        Else
            PRE2.DisplayError("Tried to remove an instance at index " & instanceIndex & " in an array with a max index of " & UBound(thisRoom.instances))
        End If

        RefreshInstancesArray()
    End Sub




    Private Sub btnInstanceCreate_Click(sender As Object, e As EventArgs) Handles btnInstanceCreate.Click
        'creates a new instance of the currently selected template

        'checks that a template is selected
        If lstTemplates.SelectedIndex > -1 Then
            AddEntityInstance(thisLevel.templates(lstTemplates.SelectedIndex))
        Else
            PRE2.DisplayError("No selected template to create an instance from")
        End If
    End Sub

    Private Sub btnInstanceDuplicate_Click(sender As Object, e As EventArgs) Handles btnInstanceDuplicate.Click
        'creates a new instance as a copy of the currently selected instance

        'checks that an instance is selected
        If lstInstances.SelectedIndex > -1 Then
            AddEntityInstance(thisRoom.instances(lstInstances.SelectedIndex))
        Else
            PRE2.DisplayError("No selected instance to duplicate")
        End If
    End Sub

    Private Sub btnInstanceDelete_Click(sender As Object, e As EventArgs) Handles btnInstanceDelete.Click
        'deletes the currently selected instance

        'checks that an instance is selected
        If lstInstances.SelectedIndex > -1 Then
            'asks the user to confirm deleting the instance
            If MsgBox("Are you sure you wish to delete instance " & thisRoom.instances(lstInstances.SelectedIndex).name, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                RemoveEntityInstance(lstInstances.SelectedIndex)
            End If
        End If
    End Sub


    Private Sub RefreshTemplatesArray()
        'clears lstTemplates then adds all templates names back again

        lstTemplates.Items.Clear()

        For Each template As PRE2.Entity In thisLevel.templates
            lstTemplates.Items.Add(template.name)
        Next
    End Sub

    Private Sub RefreshInstancesArray()
        'clears lstInstances then adds all instances names back again

        lstInstances.Items.Clear()

        For Each instance As PRE2.Entity In thisRoom.instances
            lstInstances.Items.Add(instance.name)
        Next
    End Sub

    Private Sub lstTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTemplates.SelectedIndexChanged
        'deselects whatever is selected in lstInstances

        If lstTemplates.SelectedIndex > -1 Then     'checks that this isn't being unselected
            lstInstances.SelectedIndex = -1
        End If
    End Sub

    Private Sub lstInstances_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstInstances.SelectedIndexChanged
        'deselects whatever is selected in lstTemplates

        If lstInstances.SelectedIndex > -1 Then     'checks that this isn't being unselected
            lstTemplates.SelectedIndex = -1

            ToggleTagControls(True)     'enables tag controls as an instance has been selected
        Else
            ToggleTagControls(False)    'disables tag controls as there is no selected instance
        End If
    End Sub

    Private Sub ToggleTagControls(enable As Boolean)
        'enables or disables all controls for tags, depending on whether provided True or False

        For Each ctrl As Object In tagControls
            ctrl.Enabled = enable
        Next
    End Sub

End Class