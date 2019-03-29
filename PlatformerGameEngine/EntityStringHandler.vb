'Richard Holmes
'24/03/2019
'Entity file handler

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Module EntityStringHandler

    Public Function CreateEntityString(ent As PRE2.Entity) As String
        'creates a string which can be saved to a file

        Dim result As String = ""

        'adds a list of frames and which sprites make them up
        For frameIndex As Integer = 0 To UBound(ent.frames)
            Dim currentFrame As PRE2.Frame = ent.frames(frameIndex)

            For spriteIndex As Integer = 0 To UBound(currentFrame.sprites)
                Dim currentSprite As PRE2.Sprite = currentFrame.sprites(spriteIndex)

                If currentSprite.fileName <> Nothing Then
                    'eg Sprite1\25,0/
                    result += currentSprite.fileName & ":" & Trim(Str(currentFrame.offsets(spriteIndex).X)) & "," & Trim(Str(currentFrame.offsets(spriteIndex).Y)) & "/"
                Else
                    PRE2.DisplayError("Unknown file name for sprite #" & Trim(Str(spriteIndex + 1)) & " of frame #" & Trim(Str(frameIndex + 1)) & " for entity " & ent.name)
                End If
            Next spriteIndex

            result = result.Remove(Len(result) - 1, 1) & ";"        'removes the last / and adds ;
        Next frameIndex

        result = result.Remove(Len(result) - 1, 1)      'removes the last ;

        'adds a line for tags
        For tagIndex As Integer = 0 To UBound(ent.tags)
            Dim currentTag As PRE2.Tag = ent.tags(tagIndex)

            result += currentTag.name

            For argIndex As Integer = 0 To UBound(currentTag.args)
                result += "\" & currentTag.args(argIndex).ToString
            Next argIndex

            result += "/"
        Next tagIndex

        result = result.Remove(Len(result) - 1, 1)      'removes last /

        Return result
    End Function

    Public Function ReadEntityString(entityString As String, ByRef renderEngine As PRE2, folderLocation As String) As PRE2.Entity
        'reads a string, as created in CreateEntityString, returning an entity

        Dim result As New PRE2.Entity
        Dim lines() As String = entityString.Split(Environment.NewLine)     'there should be 2 lines
        Dim currentLine As String

        'loads the frames of the entity
        currentLine = lines(0)
        Dim framesValues() As String = currentLine.Split(";")
        ReDim result.frames(UBound(framesValues))
        For frameIndex As Integer = 0 To UBound(framesValues)
            'loads each individual frame
            Dim newFrame As New PRE2.Frame

            'part for each individual sprite
            Dim spritesInFrame() As String = framesValues(frameIndex).Split("/")
            ReDim newFrame.sprites(UBound(spritesInFrame))
            ReDim newFrame.offsets(UBound(spritesInFrame))
            For spriteIndex As Integer = 0 To UBound(spritesInFrame)
                Dim values() As String = spritesInFrame(spriteIndex).Split(":")
                Dim spriteFile As String = values(0)
                Dim offset As New Point(Val(values(1).Split(",")(0)), Val(values(1).Split(",")(1)))

                renderEngine.LoadSprite(folderLocation & spriteFile)
                newFrame.sprites(spriteIndex) = renderEngine.FindLoadedSprite(spriteFile)
                newFrame.offsets(spriteIndex) = offset
            Next spriteIndex

            result.frames(frameIndex) = newFrame
        Next frameIndex

        'loads the tags
        currentLine = lines(1)
        Dim tagStrings() As String = currentLine.Split("/")
        ReDim result.tags(UBound(tagStrings))
        For tagIndex As Integer = 0 To UBound(tagStrings)
            Dim values() As String = tagStrings(tagIndex).Split("\")
            result.tags(tagIndex) = New PRE2.Tag(values(tagIndex))
        Next tagIndex

        Return result
    End Function

End Module