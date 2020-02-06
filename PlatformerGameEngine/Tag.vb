Public Class Tag

    Implements ICloneable

    Public ReadOnly Name As String
    Public Argument As String

#Region "Constructors"

    Public Sub New(Optional ByVal name As String = Nothing, Optional ByVal argument As String = Nothing)
        Me.Name = name
        Me.Argument = argument
    End Sub

    Public Sub New(tagString As String)
        'creates a tag from a string

        Dim newTag As Tag = JsonToTag(tagString)
        Name = newTag.Name
        Argument = newTag.Argument
    End Sub

#End Region

#Region "Arguments"

    Public Function InterpretArgument(Optional subTagName As String = Nothing) As Object
        If IsNothing(subTagName) Then
            Return InterpretValue(Argument)
        Else
            Return FindSubTag(subTagName)
        End If
    End Function

    Public Function FindSubTag(subTagName As String) As Tag
        'returns a subtag in this tag's argument which has a name which matches the provided subtag name

        Dim subTagsTemp As Object = InterpretArgument()

        If IsArray(subTagsTemp) Then
            For index As Integer = 0 To UBound(subTagsTemp)
                Dim subTag As Tag = TryCast(subTagsTemp(index), Tag)
                If Not IsNothing(subTag) AndAlso LCase(subTag.Name) = LCase(subTagName) Then
                    Return subTag
                End If
            Next
        ElseIf Not IsNothing(subTagsTemp) Then
            'checks if a single tag was given instead of an array, and returns it if the name matches
            If subTagsTemp.GetType() = GetType(Tag) Then
                If CType(subTagsTemp, Tag).Name = subTagName Then
                    Return CType(subTagsTemp, Tag)
                End If
            End If
        End If

        'couldn't find a matching sub tag
        Return Nothing
    End Function

    Public Sub SetArgument(newValue As Object)
        Argument = ArrayToString(newValue)
    End Sub

#End Region

#Region "Operators"

    Private Shared Function AreTagsIdentical(tag1 As Tag, tag2 As Tag) As Boolean
        'used for = and <> operators

        If IsNothing(tag1) Or IsNothing(tag2) Then
            Return IsNothing(tag1) = IsNothing(tag2)
        Else
            Return LCase(tag1.Name) = LCase(tag2.Name) AndAlso tag1.Argument = tag2.Argument
        End If
    End Function

    Public Shared Operator =(tag1 As Tag, tag2 As Tag) As Boolean
        Return AreTagsIdentical(tag1, tag2)
    End Operator

    Public Shared Operator <>(tag1 As Tag, tag2 As Tag) As Boolean
        Return Not AreTagsIdentical(tag1, tag2)
    End Operator

#End Region

#Region "Other"

    Public Overrides Function ToString() As String
        Return TagToJson(Me)
    End Function

    Public Function Clone() As Object Implements ICloneable.Clone
        'returns a deep clone of this tag

        Return New Tag(Name.Clone, Argument.Clone)
    End Function

#End Region

End Class
