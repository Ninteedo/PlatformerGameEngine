Imports PlatformerGameEngine.My.Resources

Public Class Actor

    Inherits TagContainer

    Implements ICloneable

    Private _spritesList() As Sprite

#Region "Constructors"

    Public Sub New()
        _spritesList = Nothing
        Tags = Nothing
    End Sub

    Public Sub New(actorString As String)
        'creates a new actor from an actor string

        If Not IsNothing(actorString) Then
            'Try
            'loads the Tags
            Dim temp As Object = New Tag(actorString).InterpretArgument()
            For index As Integer = 0 To UBound(temp)
                AddTag(New Tag(temp(index).ToString))
            Next
            RefreshSpritesList()
            'Catch ex As Exception
            '    RenderEngine.DisplayError("An error occured whilst loading an actor" & vbCrLf & ex.ToString)
            'End Try
        End If
    End Sub

#End Region

#Region "Sprites"

    Public Property Sprites As Sprite()
        Get
            If IsNothing(_spritesList) Then
                RefreshSpritesList()
            End If
            Return _spritesList
        End Get
        Set
            _spritesList = Value
            RefreshSpritesTag()
        End Set
    End Property

    Private Sub RefreshSpritesList()
        'changes what is stored in spriteList() using the "sprites" tag

        If HasTag(SpritesTagName) Then
            Dim spritesArgument As Object = FindTag(SpritesTagName).InterpretArgument()

            If Not IsNothing(spritesArgument) Then
                Dim newSprites(UBound(spritesArgument)) As Sprite
                For spriteIndex As Integer = 0 To UBound(spritesArgument)
                    Dim spriteTag As New Tag(spritesArgument(spriteIndex).ToString)
                    newSprites(spriteIndex) = New Sprite(spriteTag.ToString)
                Next

                _spritesList = newSprites
            Else
                If Not IsNothing(Sprites) Then
                    _spritesList = Nothing
                End If
            End If
        Else
            _spritesList = Nothing
        End If
    End Sub

    Private Sub RefreshSpritesTag()
        'changes the "sprites" tag to match what is in framesList()

        AddTag(New Tag(SpritesTagName, ArrayToString(_spritesList)), True)
    End Sub

    Public Function GetCurrentSprite() As Sprite
        Return Sprites(SpriteIndex)
    End Function

#End Region

#Region "Key Properties"

    Property Name As String
        Get
            Return GetProperty(NameTagName, "UnnamedActor")
        End Get
        Set
            SetProperty(NameTagName, AddQuotes(Value))
        End Set
    End Property

    Property Location As PointF
        Get
            If HasTag(LocationTagName) Then
                Return New PointF(Val(FindTag(LocationTagName).InterpretArgument()(0)), Val(FindTag(LocationTagName).InterpretArgument()(1)))
            Else
                Return New PointF(0, 0)
            End If
        End Get
        Set
            SetProperty(LocationTagName, ArrayToString({Value.X, Value.Y}))
        End Set
    End Property

    Property Layer As Integer
        Get
            Return GetProperty(LayerTagName, 0)
        End Get
        Set(value As Integer)
            SetProperty(LayerTagName, value)
        End Set
    End Property

    Property Scale As Single
        Get
            Return GetProperty(ScaleTagName, 1.0)
        End Get
        Set
            SetProperty(ScaleTagName, Value)
        End Set
    End Property

    Property Opacity As Single
        Get
            Return GetProperty(OpacityTagName, 1.0)
        End Get
        Set
            SetProperty(OpacityTagName, Value)
        End Set
    End Property

    Property SpriteIndex As UInteger
        Get
            Return GetProperty(CurrentSpriteTagName, 0)
        End Get
        Set
            SetProperty(CurrentSpriteTagName, Value)
        End Set
    End Property

    Public ReadOnly Property Hitbox As RectangleF
        Get
            If Not IsNothing(Sprites) AndAlso Not IsNothing(Sprites(SpriteIndex)) Then
                Return New RectangleF(Location,
                ScaleSize(Sprites(SpriteIndex).Dimensions, Scale))
            Else
                Return New RectangleF(New PointF(0, 0), New SizeF(0, 0))
            End If
        End Get
    End Property

#End Region

#Region "Other"

    Public Overrides Function ToString() As String
        'adds each tag to the main tag
        Return New Tag(TagsTagName, ArrayToString(Tags)).ToString
    End Function

    Public Function Clone() As Object Implements ICloneable.Clone
        'returns a deep clone of this actor

        Dim newClone As Actor = Nothing

        If Not IsNothing(Me) Then
            Dim clonedTags() As Tag = Nothing

            'clones each tag
            If Not IsNothing(Tags) Then
                ReDim clonedTags(UBound(Tags))
                For index As Integer = 0 To UBound(Tags)
                    clonedTags(index) = Tags(index).Clone
                Next
            End If

            newClone = New Actor With {.Tags = clonedTags}
            newClone.RefreshSpritesList()
        End If

        Return newClone
    End Function

#End Region

#Region "Operators"

    Public Shared Operator =(ent1 As Actor, ent2 As Actor) As Boolean
        Return AreActorsEqual(ent1, ent2)
    End Operator

    Public Shared Operator <>(ent1 As Actor, ent2 As Actor) As Boolean
        Return Not AreActorsEqual(ent1, ent2)
    End Operator

    Private Shared Function AreActorsEqual(ent1 As Actor, ent2 As Actor) As Boolean
        'returns whether 2 provided frames are identical

        If IsNothing(ent1) Or IsNothing(ent2) Then
            Return IsNothing(ent1) = IsNothing(ent2)
        Else
            Return Not (ent1.Tags IsNot ent2.Tags OrElse ent1.Sprites IsNot ent2.Sprites)
        End If
    End Function

#End Region

End Class
