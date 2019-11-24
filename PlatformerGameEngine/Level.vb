Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class Level
    'class to store actor defaults and rooms

    Inherits TagContainer

    Public rooms() As Room                     'stores each room in a 1D array, indexed from the uppermost

    Private Const roomsTagName As String = "rooms"
    Private Const tagsTagName As String = "tags"

#Region "Constructors"

    Public Sub New()
        tags = Nothing
        rooms = Nothing
    End Sub

    Public Sub New(levelString As String, renderEngine As PRE2)
        'templates = Nothing
        tags = Nothing
        rooms = Nothing

        Dim levelTag As Tag = New Tag(levelString)
        If Not IsNothing(levelTag) Then
            'loads each room
            Dim roomsTag As Tag = levelTag.FindSubTag(roomsTagName)
            If Not IsNothing(roomsTag) Then
                Dim temp As Object = roomsTag.InterpretArgument
                If Not IsNothing(temp) Then
                    ReDim rooms(UBound(temp))
                    For index As Integer = 0 To UBound(temp)
                        rooms(index) = New Room(temp(index).ToString, renderEngine)
                    Next
                End If
            End If

            'loads each tag
            Dim tagsTag As Tag = levelTag.FindSubTag(tagsTagName)
            If Not IsNothing(tagsTag) Then
                Dim temp As Object = tagsTag.InterpretArgument
                If IsArray(temp) Then
                    For index As Integer = 0 To UBound(temp)
                        AddTag(temp(index))
                    Next
                End If
            End If
        End If
    End Sub

#End Region

#Region "Key Properties"

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

#End Region

#Region "Other"

    Public Overrides Function ToString() As String
        'updated version of level tostring which uses tags better

        Dim parametersTag As New Tag(tagsTagName, ArrayToString(tags))
        Dim roomsTag As New Tag(roomsTagName, ArrayToString(rooms))

        Return New Tag(Name, ArrayToString({parametersTag, roomsTag})).ToString
    End Function

#End Region

End Class