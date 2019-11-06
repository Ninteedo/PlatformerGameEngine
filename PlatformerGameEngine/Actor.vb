Public Class Actor
    'most things are actors, even if they don't move
    'each actor has at least 1 sprite and can have lots of tags

    Inherits TagContainer

    Private spritesList() As Sprite
    Public spriteFolderLocation As String

    Public Sub New()
        spritesList = Nothing
        tags = Nothing
        spriteFolderLocation = Nothing
    End Sub

    Public Sub New(startSprites() As Sprite, startTags() As Tag, spriteFolder As String, startLocation As PointF, Optional startScale As Single = 1.0)
        spriteFolderLocation = spriteFolder
        Sprites = startSprites
        tags = startTags

        CurrentFrame = 0
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


    Public Sub RefreshSpritesList()
        'changes what is stored in spriteList() using the "sprites" tag

        If HasTag("sprites") Then
            Dim spritesArgument() As Object = FindTag("sprites").InterpretArgument()

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

    Public Sub RefreshSpritesTag()
        'changes the "frames" tag to match what is in framesList()

        AddTag(New Tag("sprites", ArrayToString(spritesList)), True)
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


    'Public Function FindTag(tagName As String) As Tag
    '    'returns the first tag this actor has with the given name

    '    If IsNothing(tags) = False Then
    '        For index As Integer = 0 To UBound(tags)
    '            If LCase(tags(index).name) = LCase(tagName) Then
    '                Return tags(index)
    '            End If
    '        Next index
    '    End If

    '    Return Nothing
    'End Function

    'Public Function HasTag(tagName As String) As Boolean
    '    'returns whether or not this actor has a tag with the given name

    '    If FindTag(tagName).name <> Nothing Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    'Public Sub AddTag(newTag As Tag, Optional removeDuplicates As Boolean = False)
    '    'adds the given tag to this actor's list of tags

    '    If removeDuplicates Then
    '        RemoveTag(newTag.name)
    '    End If

    '    If IsNothing(tags) Then
    '        ReDim tags(0)
    '    Else
    '        ReDim Preserve tags(UBound(tags) + 1)
    '    End If
    '    tags(UBound(tags)) = newTag

    '    CheckSpecialTagModified(newTag)
    'End Sub

    'Public Sub RemoveTag(tagName As String)
    '    'removes all tags with the given name

    '    Dim tagIndex As Integer = 0

    '    If Not IsNothing(tags) Then
    '        Do While tagIndex <= UBound(tags)
    '            If tags(tagIndex).name = tagName Then
    '                For removeIndex As Integer = tagIndex To UBound(tags) - 1
    '                    tags(removeIndex) = tags(removeIndex + 1)
    '                Next removeIndex

    '                ReDim Preserve tags(UBound(tags) - 1)
    '            Else
    '                tagIndex += 1       'tag index isn't incremented when a tag with matching name is found so none are skipped
    '            End If
    '        Loop
    '    End If

    '    CheckSpecialTagModified(New Tag(tagName, Nothing))
    'End Sub

    'Public Sub SetTag(tagIndex As Integer, newTag As Tag)
    '    'changes the tag at the given index to the new tag

    '    If Not IsNothing(tags) And tagIndex >= 0 AndAlso tagIndex <= UBound(tags) Then
    '        tags(tagIndex) = newTag

    '        CheckSpecialTagModified(newTag)
    '    Else
    '        PanelRenderEngine2.DisplayError("Tried to change tag for actor " & Name & " but index (" & tagIndex & ") was out of bounds")
    '    End If
    'End Sub

    Private Sub CheckSpecialTagModified(modifiedTag As Tag)
        'used for if something special needs to be done when a specific tag is changed

        Select Case modifiedTag.name
            Case "frames"
                RefreshSpritesList()
        End Select
    End Sub


    Property Name As String
        Get
            If HasTag("name") Then
                Return FindTag("name").InterpretArgument()
            Else
                Return "unnamed"
            End If
        End Get
        Set(value As String)
            AddTag(New Tag("name", AddQuotes(value)), True)
        End Set
    End Property

    Property Location As PointF
        Get
            If HasTag("location") Then
                'Dim textForm As String = FindTag("location").InterpretArgument(0).ToString.Replace("{", "").Replace("}", "").Replace("{", "")
                'Return New PointF(Val(textForm.Split(",")(0).Trim.Replace("X=", "")),
                '                        Val(textForm.Split(",")(1).Trim.Replace("Y=", "")))
                Return New Point(Val(FindTag("location").InterpretArgument()(0)), Val(FindTag("location").InterpretArgument()(1)))
            Else
                Return New PointF(0, 0)
            End If
        End Get
        Set(value As PointF)
            AddTag(New Tag("location", "[" & value.X & "," & value.Y & "]"), True)
        End Set
    End Property

    Property Layer As Integer
        Get
            If HasTag("layer") Then
                Return FindTag("layer").InterpretArgument()
            Else
                Return 0
            End If
        End Get
        Set(value As Integer)
            AddTag(New Tag("layer", value), True)
        End Set
    End Property

    Property Scale As Single
        Get
            If HasTag("scale") Then
                Return FindTag("scale").InterpretArgument()
            Else
                Return 1
            End If
        End Get
        Set(value As Single)
            AddTag(New Tag("scale", value), True)
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

    '        Return New PointF(Sprites(CurrentFrame).Dimensions.Width / 2, Sprites(CurrentFrame).Dimensions.Height / 2)
    '    End Get
    '    Set(value As PointF)
    '        AddTag(New Tag("rotationAnchor", "[" & value.X & "," & value.Y & "]"), True)
    '    End Set
    'End Property

    Property Opacity As Single
        Get
            If HasTag("opacity") Then
                Return FindTag("opacity").InterpretArgument()
            Else
                Return 1.0
            End If
        End Get
        Set(value As Single)
            AddTag(New Tag("opacity", value), True)
        End Set
    End Property

    Property CurrentFrame As UInteger
        Get
            If HasTag("currentFrame") Then
                Return FindTag("currentFrame").InterpretArgument()
            Else
                Return 0
            End If
        End Get
        Set(value As UInteger)
            AddTag(New Tag("currentFrame", value), True)
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
                                    New SizeF(Scale * (Sprites(CurrentFrame).Dimensions.Width - 0),
                                            Scale * (Sprites(CurrentFrame).Dimensions.Height - 0)))
        End Get
    End Property
End Class
