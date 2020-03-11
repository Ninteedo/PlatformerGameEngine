Imports PlatformerGameEngine.My.Resources

Public Class Actor

    Inherits TagContainer

    Implements ICloneable

    Private _spriteList() As Sprite

#Region "Constructors"

    Public Sub New()
        _spriteList = {}
    End Sub

    Public Sub New(actorString As String)
        'creates a new actor from an actor string

        If Not IsNothing(actorString) Then
            'loads all tags
            Dim temp As Object = New Tag(actorString).InterpretArgument()
            For index As Integer = 0 To UBound(temp)
                AddTag(New Tag(temp(index).ToString))
            Next
            RefreshSpritesList()
        End If
    End Sub

#End Region

#Region "Sprites"

    Public Property Sprites As Sprite()
        Get
            If IsNothing(_spriteList) Then
                RefreshSpritesList()
            End If
            Return _spriteList
        End Get
        Set
            _spriteList = Value
            RefreshSpritesTag()
        End Set
    End Property

    Private Sub RefreshSpritesList()
        'changes what is stored in _spriteList using the "sprites" tag

        If HasTag(SpritesTagName) Then
            Dim spritesArgument As Object = FindTag(SpritesTagName).InterpretArgument()

            If Not IsNothing(spritesArgument) Then
                Dim newSprites(UBound(spritesArgument)) As Sprite
                For index As Integer = 0 To UBound(spritesArgument)
                    Dim spriteTag As New Tag(spritesArgument(index).ToString)
                    newSprites(index) = New Sprite(spriteTag.ToString)
                Next

                _spriteList = newSprites
            Else
                If Not IsNothing(Sprites) Then
                    _spriteList = {}
                End If
            End If
        Else
            _spriteList = {}
        End If
    End Sub

    Private Sub RefreshSpritesTag()
        'changes the "sprites" tag to match what is in _spriteList()

        SetTag(New Tag(SpritesTagName, ArrayToString(_spriteList)))
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
            Dim def As Object = {0.0, 0.0}
            Dim temp As Object = GetProperty("location", def)
            If IsArray(temp) AndAlso UBound(temp) = 1 Then
                Return New PointF(temp(0), temp(1))
            Else
                Return New PointF
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
        Set
            SetProperty(LayerTagName, Value)
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
            If UBound(Sprites) >= 0 Then
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
            Dim clonedTags() As Tag = {}

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

End Class
