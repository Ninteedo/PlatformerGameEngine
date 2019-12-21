Public Class Actor

    Inherits TagContainer

    Implements ICloneable

    Private spritesList() As Sprite

    Private Const tagsTagName As String = "tags"
    Private Const spritesTagName As String = "sprites"
    Private Const nameTagName As String = "name"
    Private Const locationTagName As String = "location"
    Private Const layerTagName As String = "layer"
    Private Const scaleTagName As String = "scale"
    Private Const opacityTagName As String = "opacity"
    Private Const currentSpriteTagName As String = "currentSprite"


#Region "Constructors"

    Public Sub New()
        spritesList = Nothing
        tags = Nothing
    End Sub

    Public Sub New(actorString As String, ByRef renderEngine As PanelRenderEngine2)
        'creates a new actor from an actor string

        If Not IsNothing(actorString) Then
            'Try
            'loads the tags
            Dim temp As Object = New Tag(actorString).InterpretArgument()
            For index As Integer = 0 To UBound(temp)
                AddTag(New Tag(temp(index).ToString))
            Next
            RefreshSpritesList()
            'Catch ex As Exception
            '    PanelRenderEngine2.DisplayError("An error occured whilst loading an actor" & vbCrLf & ex.ToString)
            'End Try
        End If
    End Sub

#End Region

#Region "Sprites"

    Public Property Sprites As Sprite()
        Get
            If IsNothing(spritesList) Then
                RefreshSpritesList()
            End If
            Return spritesList
        End Get
        Set(value As Sprite())
            spritesList = value
            RefreshSpritesTag()
        End Set
    End Property

    Public Sub RefreshSpritesList()
        'changes what is stored in spriteList() using the "sprites" tag

        If HasTag(spritesTagName) Then
            Dim spritesTag As Tag = FindTag(spritesTagName)
            Dim spritesArgument() As Object = FindTag(spritesTagName).InterpretArgument()

            If Not IsNothing(spritesArgument) Then
                Dim newSprites(UBound(spritesArgument)) As Sprite
                For spriteIndex As Integer = 0 To UBound(spritesArgument)
                    Dim spriteTag As New Tag(spritesArgument(spriteIndex).ToString)
                    newSprites(spriteIndex) = New Sprite(spriteTag.ToString)
                Next

                spritesList = newSprites
            Else
                If Not IsNothing(Sprites) Then
                    spritesList = Nothing
                End If
            End If
        Else
            spritesList = Nothing
        End If
    End Sub

    Public Sub RefreshSpritesTag()
        'changes the "sprites" tag to match what is in framesList()

        AddTag(New Tag(spritesTagName, ArrayToString(spritesList)), True)
    End Sub

    Public Function GetCurrentSprite() As Sprite
        Return Sprites(CurrentSprite)
    End Function

#End Region

#Region "Key Properties"

    Property Name As String
        Get
            If HasTag(nameTagName) Then
                Return FindTag(nameTagName).InterpretArgument()
            Else
                Return "unnamed"
            End If
        End Get
        Set(value As String)
            AddTag(New Tag(nameTagName, AddQuotes(value)), True)
        End Set
    End Property

    Property Location As PointF
        Get
            If HasTag(locationTagName) Then
                'Dim textForm As String = FindTag("location").InterpretArgument(0).ToString.Replace("{", "").Replace("}", "").Replace("{", "")
                'Return New PointF(Val(textForm.Split(",")(0).Trim.Replace("X=", "")),
                '                        Val(textForm.Split(",")(1).Trim.Replace("Y=", "")))
                Return New Point(Val(FindTag(locationTagName).InterpretArgument()(0)), Val(FindTag(locationTagName).InterpretArgument()(1)))
            Else
                Return New PointF(0, 0)
            End If
        End Get
        Set(value As PointF)
            AddTag(New Tag(locationTagName, ArrayToString({value.X, value.Y})), True)
        End Set
    End Property

    Property Layer As Integer
        Get
            If HasTag(layerTagName) Then
                Return FindTag(layerTagName).InterpretArgument()
            Else
                Return 0
            End If
        End Get
        Set(value As Integer)
            AddTag(New Tag(layerTagName, value), True)
        End Set
    End Property

    Property Scale As Single
        Get
            If HasTag(scaleTagName) Then
                Return FindTag(scaleTagName).InterpretArgument()
            Else
                Return 1
            End If
        End Get
        Set(value As Single)
            AddTag(New Tag(scaleTagName, value), True)
        End Set
    End Property

    Property Opacity As Single
        Get
            If HasTag(opacityTagName) Then
                Return FindTag(opacityTagName).InterpretArgument()
            Else
                Return 1.0
            End If
        End Get
        Set(value As Single)
            AddTag(New Tag(opacityTagName, value), True)
        End Set
    End Property

    Property CurrentSprite As UInteger
        Get
            If HasTag(currentSpriteTagName) Then
                Return FindTag(currentSpriteTagName).InterpretArgument()
            Else
                Return 0
            End If
        End Get
        Set(value As UInteger)
            AddTag(New Tag(currentSpriteTagName, value), True)
        End Set
    End Property

    Public ReadOnly Property Hitbox As RectangleF
        Get
            If Not IsNothing(Sprites) AndAlso Not IsNothing(Sprites(CurrentSprite)) Then
                Return New RectangleF(New PointF(Location.X, Location.Y),
                New SizeF(Scale * Sprites(CurrentSprite).Dimensions.Width,
                Scale * Sprites(CurrentSprite).Dimensions.Height))
            Else
                Return New RectangleF(New PointF(0, 0), New SizeF(0, 0))
            End If
        End Get
    End Property

#End Region

#Region "Other"

    Public Overrides Function ToString() As String
        'adds each tag to the main tag
        Return New Tag(tagsTagName, ArrayToString(tags)).ToString
    End Function

    Public Function Clone() As Object Implements ICloneable.Clone
        'returns a deep clone of this actor

        Dim newClone As Actor = Nothing

        If Not IsNothing(Me) Then
            Dim clonedTags() As Tag = Nothing

            'clones each tag
            If Not IsNothing(Me.tags) Then
                ReDim clonedTags(UBound(Me.tags))
                For index As Integer = 0 To UBound(Me.tags)
                    clonedTags(index) = Me.tags(index).Clone
                Next
            End If

            newClone = New Actor With {.tags = clonedTags}
            newClone.RefreshSpritesList()
        End If

        Return newClone
    End Function

#End Region

#Region "Operators"

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

#End Region

End Class
