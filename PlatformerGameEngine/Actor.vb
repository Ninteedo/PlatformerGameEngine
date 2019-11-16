Public Class Actor
    'most things are actors, even if they don't move
    'each actor has at least 1 sprite and can have lots of tags

    Inherits TagContainer

    Private spritesList() As Sprite
    Public spriteFolderLocation As String

    Private Const spriteTagName As String = "sprites"
    Private Const nameTagName As String = "name"
    Private Const locationTagName As String = "location"
    Private Const layerTagName As String = "layer"
    Private Const scaleTagName As String = "scale"
    Private Const opacityTagName As String = "opacity"
    Private Const currentSpriteIndexTagName As string = "currentSpriteIndex"

    Public Sub New()
        spritesList = Nothing
        tags = Nothing
        spriteFolderLocation = Nothing
    End Sub

    Public Sub New(startSprites() As Sprite, startTags() As Tag, spriteFolder As String, startLocation As PointF, Optional startScale As Single = 1.0)
        spriteFolderLocation = spriteFolder
        Sprites = startSprites
        tags = startTags

        CurrentSprite = 0
        Location = startLocation
        'Rotation = startRotation
        Scale = startScale
    End Sub

    Public Sub New(actorString As String, renderEngine As PanelRenderEngine2)
        'creates a new actor from an actor string

        spriteFolderLocation = renderEngine.spriteFolderLocation
        If Not IsNothing(actorString) Then
            Dim newEnt As Actor = ActorStringHandler.ReadActorString(actorString, renderEngine)
            Sprites = newEnt.Sprites
            tags = newEnt.tags
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return CreateActorString(Me)
    End Function

    Public Function Clone() As Actor
        'returns a clone of this actor

        Dim newClone As Actor = Nothing

        If Not IsNothing(Me) Then
            newClone = New Actor With {
            .spriteFolderLocation = spriteFolderLocation,
            .tags = tags
                }
            RefreshSpritesList()
        End If

        Return newClone
    End Function


    Private Sub RefreshSpritesList()
        'changes what is stored in spriteList() using the sprites tag

        If HasTag(spriteTagName) Then
            Dim spritesArgument() As Tag = FindTag(spriteTagName).InterpretArgument(Of Tag())

            If Not IsNothing(spritesArgument) Then
                Dim newSprites(UBound(spritesArgument)) As Sprite
                For spriteIndex As Integer = 0 To UBound(spritesArgument)
                    Dim spriteTag As New Tag(spritesArgument(spriteIndex).ToString)
                    newSprites(spriteIndex) = New Sprite(spriteTag.ToString)
                Next

                If newSprites IsNot Sprites Then
                    spritesList = newSprites
                End If
            Else
                If Not IsNothing(Sprites) Then
                    spritesList = Nothing
                End If
            End If
        Else
            spritesList = Nothing
        End If
    End Sub

    Private Sub RefreshSpritesTag()
        'changes the "frames" tag to match what is in framesList()

        AddTag(New Tag(spriteTagName, ArrayToString(spritesList)), True)
    End Sub

    Public Property Sprites As Sprite()
        Get
            Return spritesList
        End Get
        Set(value As Sprite())
            spritesList = value
            RefreshSpritesTag()
        End Set
    End Property

    Public Property Name As String
        Get
            If HasTag(nameTagName) Then
                Return FindTag(nameTagName).InterpretArgument(Of String)
            Else
                Return "unnamed"
            End If
        End Get
        Set(value As String)
            AddTag(New Tag(nameTagName, AddQuotes(value)), True)
        End Set
    End Property

    Public Property Location As PointF
        Get
            If HasTag(locationTagName) Then
                'Dim textForm As String = FindTag("location").InterpretArgument(0).ToString.Replace("{", "").Replace("}", "").Replace("{", "")
                'Return New PointF(Val(textForm.Split(",")(0).Trim.Replace("X=", "")),
                '                        Val(textForm.Split(",")(1).Trim.Replace("Y=", "")))
                Return New Point(Val(FindTag(locationTagName).InterpretArgument(Of Single())(0)), Val(FindTag("location").InterpretArgument(Of Single())(1)))
            Else
                Return New PointF(0, 0)
            End If
        End Get
        Set(value As PointF)
            AddTag(New Tag(locationTagName, ArrayToString({value.X, value.Y})), True)
        End Set
    End Property

    Public Property Layer As Integer
        Get
            If HasTag(layerTagName) Then
                Return FindTag(layerTagName).InterpretArgument(Of Integer)
            Else
                Return 0
            End If
        End Get
        Set(value As Integer)
            AddTag(New Tag(layerTagName, value), True)
        End Set
    End Property

    Public Property Scale As Single
        Get
            If HasTag(scaleTagName) Then
                Return FindTag(scaleTagName).InterpretArgument(Of Single)
            Else
                Return 1
            End If
        End Get
        Set(value As Single)
            AddTag(New Tag(scaleTagName, value), True)
        End Set
    End Property

    'Property Rotation As Single
    '    Get
    '        If HasTag("rotation") Then
    '            Return FindTag("rotation").InterpretArgument()
    '        Else
    '            Return 0
    '        End If
    '    End Get
    '    Set(value As Single)
    '        AddTag(New Tag("rotation", value), True)
    '    End Set
    'End Property

    'Property RotationAnchor As PointF
    '    Get
    '        If HasTag("rotationAnchor") Then
    '            Dim argStrings() As String = {FindTag("rotationAnchor").InterpretArgument()(0), FindTag("rotationAnchor").InterpretArgument()(1)}

    '            If Not IsNothing(argStrings(0)) AndAlso IsNumeric(argStrings(0)) AndAlso
    '                Not IsNothing(argStrings(1)) AndAlso IsNumeric(argStrings(1)) Then
    '                Return New PointF(Val(argStrings(0)), Val(argStrings(1)))
    '            End If
    '        End If

    '        Return New PointF(Sprites(CurrentSprite).Dimensions.Width / 2, Sprites(CurrentSprite).Dimensions.Height / 2)
    '    End Get
    '    Set(value As PointF)
    '        AddTag(New Tag("rotationAnchor", "[" & value.X & "," & value.Y & "]"), True)
    '    End Set
    'End Property

    Public Property Opacity As Single
        Get
            If HasTag(opacityTagName) Then
                Return FindTag(opacityTagName).InterpretArgument(Of Single)
            Else
                Return 1.0
            End If
        End Get
        Set(value As Single)
            AddTag(New Tag(opacityTagName, value), True)
        End Set
    End Property

    Public Property CurrentSprite As UInteger
        Get
            If HasTag(currentSpriteIndexTagName) Then
                Return FindTag(currentSpriteIndexTagName).InterpretArgument(Of UInteger)
            Else
                Return 0
            End If
        End Get
        Set(value As UInteger)
            AddTag(New Tag(currentSpriteIndexTagName, value), True)
        End Set
    End Property


    Public Shared Operator =(ent1 As Actor, ent2 As Actor)
        Return AreActorsEqual(ent1, ent2)
    End Operator

    Public Shared Operator <>(ent1 As Actor, ent2 As Actor)
        Return Not AreActorsEqual(ent1, ent2)
    End Operator

    Public Shared Function AreActorsEqual(ent1 As Actor, ent2 As Actor) As Boolean
        'returns whether 2 provided frames are identical

        If IsNothing(ent1) Or IsNothing(ent2) Then
            Return IsNothing(ent1) = IsNothing(ent2)
        Else
            Return Not (ent1.tags IsNot ent2.tags OrElse ent1.Sprites IsNot ent2.Sprites)
        End If
    End Function

    Public ReadOnly Property Hitbox As RectangleF
        Get
            Return New RectangleF(New PointF(Location.X, Location.Y),
                                    New SizeF(Scale * Sprites(CurrentSprite).Dimensions.Width,
                                            Scale * Sprites(CurrentSprite).Dimensions.Height))
        End Get
    End Property
End Class
