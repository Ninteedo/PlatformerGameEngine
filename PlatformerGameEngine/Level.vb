Imports PlatformerGameEngine.My.Resources

Public Class Level

    Inherits TagContainer

    Public Rooms() As Room


#Region "Constructors"

    Public Sub New(rooms() As Room)
        Me.Rooms = rooms
    End Sub

    Public Sub New(levelString As String)
        Rooms = {}

        Dim levelTag As New Tag(levelString)
        If Not IsNothing(levelTag) Then
            'loads each room
            Dim roomsTag As Tag = levelTag.FindSubTag(RoomsTagName)
            If Not IsNothing(roomsTag) Then
                Dim temp As Object = roomsTag.InterpretArgument
                If IsArray(temp) Then
                    ReDim Rooms(UBound(temp))
                    For index As Integer = 0 To UBound(temp)
                        Rooms(index) = New Room(temp(index).ToString)
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

    Public Property Scroll As PointF
        Get
            Dim def As Object = {0.0, 0.0}
            Dim temp As Object = GetProperty("scroll", def)
            If IsArray(temp) AndAlso UBound(temp) = 1 Then
                Return New SizeF(temp(0), temp(1))
            Else
                Return New SizeF(0, 0)
            End If
        End Get
        Set
            SetProperty("scroll", ArrayToString({Scroll.X, Scroll.Y}))
        End Set
    End Property

#End Region

#Region "Other"

    Public Overrides Function ToString() As String
        Dim parametersTag As New Tag(TagsTagName, ArrayToString(Tags))
        Dim roomsTag As New Tag(RoomsTagName, ArrayToString(Rooms))

        Return New Tag(Name, ArrayToString({parametersTag, roomsTag})).ToString
    End Function

    Private Shared Function AreLevelsIdentical(l1 As Level, l2 As Level) As Boolean
        If IsNothing(l1) Or IsNothing(l2) Then
            Return IsNothing(l1) = IsNothing(l2)
        Else
            Dim result As Boolean = l1.Name = l2.Name And l1.RoomIndex = l2.RoomIndex
            result = result And (IsNothing(l1.Rooms) = IsNothing(l2.Rooms))
            result = result And (IsNothing(l1.Tags) = IsNothing(l2.Tags))

            If result And Not IsNothing(l1.Rooms) And Not IsNothing(l1.Tags) Then
                result = l1.Rooms Is l2.Rooms And l1.Tags Is l2.Tags
            End If

            Return result
        End If
    End Function

    Public Shared Operator =(l1 As Level, l2 As Level) As Boolean
        Return AreLevelsIdentical(l1, l2)
    End Operator

    Public Shared Operator <>(l1 As Level, l2 As Level) As Boolean
        Return Not AreLevelsIdentical(l1, l2)
    End Operator
#End Region

End Class