Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class Level
    'class to store actor defaults and rooms

    'Public templates() As Actor     'the formats for loaded actors, not actually displayed, used to create instances of actors
    Public parameters() As Tag            'essentially global variables for the level

    Public rooms() As Room                     'stores each room in a 1D array, indexed from the uppermost
    'Dim roomCoords() As Point               'stores the coordinates of each room, parallel to rooms array
    'Dim currentRoomCoords As Point          'stores the coordinates of which room is being used currently

    Public Sub New()
        'templates = Nothing
        parameters = Nothing
        rooms = Nothing
    End Sub

    Public Sub New(levelString As String, renderEngine As PRE2)
        'templates = Nothing
        parameters = Nothing
        rooms = Nothing

        Dim tagStrings() As Object = New Tag(levelString).InterpretArgument 'JSONSplit(levelString, 0)
        If Not IsNothing(tagStrings) Then
            Dim tags(UBound(tagStrings)) As Tag

            For tagIndex As Integer = 0 To UBound(tagStrings)
                tags(tagIndex) = New Tag(tagStrings(tagIndex).ToString)
            Next

            For Each thisTag As Tag In tags
                If Not IsNothing(thisTag) Then
                    Select Case thisTag.name
                        Case "parameters"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim parameters(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    parameters(index) = New Tag(temp(index).ToString)
                                Next
                            End If
                        'Case "templates"
                        '    Dim temp() As Object = thisTag.InterpretArgument
                        '    If Not IsNothing(temp) Then
                        '        ReDim templates(UBound(temp))
                        '        For index As Integer = 0 To UBound(temp)
                        '            templates(index) = New Actor(temp(index).ToString, renderEngine)
                        '        Next
                        '    End If
                        Case "rooms"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim rooms(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    rooms(index) = New Room(temp(index), renderEngine)
                                Next
                            End If
                        Case Else
                            PRE2.DisplayError("Unknown tag in level file: " & thisTag.name)
                    End Select
                End If
            Next
        End If
    End Sub

    Public Overrides Function ToString() As String
        'updated version of level tostring which uses tags better

        Dim parametersTag As New Tag("parameters", ArrayToString(parameters))
        'Dim templatesTag As New Tag("templates", ArrayToString(templates))
        Dim roomsTag As New Tag("rooms", ArrayToString(rooms))

        Return New Tag(Name, ArrayToString({parametersTag, roomsTag})).ToString
    End Function


    'Public Function GetRoomParameters(roomIndex As Integer) As Tag()
    '    'returns the level's global parameters combined with the room's parameters

    '    Dim result() As Tag = rooms(roomIndex).parameters

    '    For Each parameter As Tag In parameters
    '        If Not rooms(roomIndex).HasParam(parameter.name) Then
    '            If IsNothing(result) Then
    '                ReDim result(0)
    '            Else
    '                ReDim result(UBound(result) + 1)
    '            End If
    '            result(UBound(result)) = parameter
    '        End If
    '    Next

    '    Return result
    'End Function

    'Public Function RoomWithCoords(coords As Point) As Room
    '    'returns the room with the coords provided

    '    For index As Integer = 0 To UBound(rooms)
    '        If coords = rooms(index).Coords Then
    '            Return rooms(index)
    '        End If
    '    Next index

    '    PRE2.DisplayError("Couldn't find a room with coordinates " & Str(coords.X) & "," & Str(coords.Y))
    '    Return Nothing
    'End Function


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
        'adds the given parameter to this level's list of tags

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
                Return "UnnamedLevel"
            End If
        End Get
        Set(value As String)
            AddParam(New Tag("name", value), True)
        End Set
    End Property
End Class