Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class Room
    'a room is a collection of actors which are all rendered at once

    Public instances() As Actor    'the actors which are used in the game, modified copies of the defaults
    Public parameters() As Tag

    Public Sub New()
        instances = Nothing
        parameters = Nothing
    End Sub

    Public Sub New(roomTag As Tag, renderEngine As PRE2)
        Dim tagStrings() As Object = roomTag.InterpretArgument
        If Not IsNothing(tagStrings) Then
            Dim tags(UBound(tagStrings)) As Tag
            For tagIndex As Integer = 0 To UBound(tags)
                tags(tagIndex) = New Tag(tagStrings(tagIndex).ToString)
            Next

            For Each thisTag As Tag In tags
                If Not IsNothing(thisTag) Then
                    Select Case thisTag.name
                        Case "instances"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim instances(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    instances(index) = New Actor(temp(index).ToString, renderEngine)
                                Next
                            End If
                        Case "parameters"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim parameters(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    parameters(index) = temp(index)
                                Next
                            End If
                        Case Else
                            PRE2.DisplayError("Unknown tag in room tag: " & thisTag.name)
                    End Select
                End If
            Next
        End If
    End Sub

    Public Overloads Function ToString(levelOfRoom As Level, roomDelimiters() As String) As String
        'returns a string to save the given room

        Dim roomString As String = ""

        'adds an addParam line for each instance
        If Not IsNothing(parameters) Then
            For Each param As Tag In parameters
                'only adds parameter if the level doesn't have an identical global parameter
                'Dim levelHasParam As Boolean = False
                'For Each globalParam As Tag In levelOfRoom.globalParameters
                '    If param = globalParam Then
                '        levelHasParam = True
                '    End If
                'Next globalParam

                'If Not levelHasParam Then
                roomString += "addParam" & roomDelimiters(0) & param.ToString & roomDelimiters(2)
                'End If
            Next param
        End If

        'adds an addEnt line for each instance
        If Not IsNothing(instances) Then
            For Each instance As Actor In instances
                'finds the template of the instance
                Dim templateOfInstance As Actor = Nothing           'used so that tags can be compared and identical ones can be ignored
                Dim templateName As String = Nothing
                If instance.HasTag("templateName") Then
                    templateName = instance.FindTag("templateName").InterpretArgument()
                    For Each template As Actor In levelOfRoom.templates
                        If template.Name = templateName Then
                            templateOfInstance = template
                            Exit For
                        End If
                    Next template
                End If

                If IsNothing(templateOfInstance) Then
                    PRE2.DisplayError("Could not find a template called " & templateName & " for instance " & instance.Name)
                Else
                    'Dim line As String = "addEnt" & roomDelimiters(0) & templateName & roomDelimiters(1) & instance.name        'this looks wrong
                    Dim line As String = "addEnt" & roomDelimiters(0) & templateName

                    'adds each added tag to the line, which is not identical to one which the template has
                    For Each thisTag As Tag In instance.tags
                        If Not IsNothing(templateOfInstance.FindTag(thisTag.name)) AndAlso templateOfInstance.FindTag(thisTag.name) <> thisTag Then
                            line += roomDelimiters(1) & thisTag.ToString
                        End If
                    Next thisTag

                    roomString += line & roomDelimiters(2)
                End If
            Next instance
        End If

        roomString = roomString.TrimEnd     'removes any trailing whitespace

        Return roomString
    End Function

    Public Overrides Function ToString() As String
        'updated version of room tostring which makes better use of tags

        Dim parametersTag As New Tag("parameters", ArrayToString(parameters))
        Dim instancesTag As New Tag("instances", ArrayToString(instances))

        Return New Tag(Name, ArrayToString({parametersTag, instancesTag})).ToString
    End Function


    Public Function FindParam(paramName As String) As Tag
        'returns the first parameter this room has with the given name

        If IsNothing(parameters) = False Then
            For index As Integer = 0 To UBound(parameters)
                If LCase(parameters(index).name) = LCase(paramName) Then
                    Return parameters(index)
                End If
            Next index
        End If

        Return Nothing
    End Function

    Public Function HasParam(paramName As String) As Boolean
        'returns whether or not this room has a parameter with the given name

        If FindParam(paramName).name <> Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub AddParam(newParam As Tag, Optional removeDuplicates As Boolean = False)
        'adds the given parameter to this room's list of tags

        If removeDuplicates Then
            RemoveParam(newParam.name)
        End If

        If IsNothing(parameters) = True Then
            ReDim parameters(0)
        Else
            ReDim Preserve parameters(UBound(parameters) + 1)
        End If

        parameters(UBound(parameters)) = newParam
    End Sub

    Public Sub RemoveParam(paramName As String)
        'removes all parameters with the given name

        Dim paramIndex As Integer = 0

        If Not IsNothing(parameters) Then
            Do While paramIndex <= UBound(parameters)
                If parameters(paramIndex).name = paramName Then
                    For removeIndex As Integer = paramIndex To UBound(parameters) - 1
                        parameters(removeIndex) = parameters(removeIndex + 1)
                    Next removeIndex

                    ReDim Preserve parameters(UBound(parameters) - 1)
                Else
                    paramIndex += 1       'param index isn't incremented when a param with matching name is found so none are skipped
                End If
            Loop
        End If
    End Sub

    Public Property Name As String
        Get
            If HasParam("name") Then
                Return FindParam("name").InterpretArgument()
            Else
                Return "UnnamedRoom"
            End If
        End Get
        Set(value As String)
            AddParam(New Tag("name", AddQuotes(value)), True)
        End Set
    End Property

    Public Property Coords As Point     'need to implement a proper coords system
        Get
            If HasParam("coords") Then
                Return New Point(Val(FindParam("coords").InterpretArgument(0)), Val(FindParam("coords").InterpretArgument(1)))
            Else
                Return New Point(0, 0)
            End If
        End Get
        Set(value As Point)
            RemoveParam("coords")
            AddParam(New Tag("coords", "[" & value.X & "," & value.Y & "]"))
        End Set
    End Property
End Class
