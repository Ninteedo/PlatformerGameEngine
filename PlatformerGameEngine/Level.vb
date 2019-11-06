Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class Level
    'class to store actor defaults and rooms

    Inherits TagContainer

    Public rooms() As Room                     'stores each room in a 1D array, indexed from the uppermost

    Public Sub New()
        tags = Nothing
        rooms = Nothing
    End Sub

    Public Sub New(levelString As String, renderEngine As PRE2)
        'templates = Nothing
        tags = Nothing
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
                        Case "tags"
                            Dim temp() As Object = thisTag.InterpretArgument
                            If Not IsNothing(temp) Then
                                ReDim Me.tags(UBound(temp))
                                For index As Integer = 0 To UBound(temp)
                                    Me.tags(index) = New Tag(temp(index).ToString)
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

        Dim parametersTag As New Tag("tags", ArrayToString(tags))
        'Dim templatesTag As New Tag("templates", ArrayToString(templates))
        Dim roomsTag As New Tag("rooms", ArrayToString(rooms))

        Return New Tag(Name, ArrayToString({parametersTag, roomsTag})).ToString
    End Function

    Public Property Name As String
        Get
            If HasTag("name") Then
                Return FindTag("name").InterpretArgument()
            Else
                Return "UnnamedLevel"
            End If
        End Get
        Set(value As String)
            AddTag(New Tag("name", value), True)
        End Set
    End Property
End Class