Imports PlatformerGameEngine.My.Resources

Public Class Level
    'class to store actor defaults and rooms

    Inherits TagContainer

    Public rooms() As Room                     'stores each room in a 1D array, indexed from the uppermost

#Region "Constructors"

    Public Sub New(Optional rooms() As Room = Nothing)
        Me.rooms = rooms
    End Sub

    Public Sub New(levelString As String)
        'templates = Nothing
        Tags = Nothing
        rooms = Nothing

        Dim levelTag As New Tag(levelString)
        If Not IsNothing(levelTag) Then
            'loads each room
            Dim roomsTag As Tag = levelTag.FindSubTag(RoomsTagName)
            If Not IsNothing(roomsTag) Then
                Dim temp As Object = roomsTag.InterpretArgument
                If IsArray(temp) Then
                    ReDim rooms(UBound(temp))
                    For index As Integer = 0 To UBound(temp)
                        rooms(index) = New Room(temp(index).ToString)
                    Next
                End If
            End If

            'loads each tag
            Dim tagsTag As Tag = levelTag.FindSubTag(TagsTagName)
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
            Return GetProperty(NameTagName, "UnnamedLevel")
        End Get
        Set
            SetProperty(NameTagName, Value)
        End Set
    End Property

    Public Property RoomIndex As Integer
        'the index of the current room in the rooms list
        Get
            Return GetProperty(RoomIndexTagName, 0)
        End Get
        Set
            SetProperty(RoomIndexTagName, Value)
        End Set
    End Property

#End Region

#Region "Other"

    Public Overrides Function ToString() As String
        Dim parametersTag As New Tag(tagsTagName, ArrayToString(Tags))
        Dim roomsTag As New Tag(roomsTagName, ArrayToString(rooms))

        Return New Tag(Name, ArrayToString({parametersTag, roomsTag})).ToString
    End Function

#End Region

End Class