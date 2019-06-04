'Richard Holmes
'24/03/2019
'Entity file handler

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Module EntityStringHandler

    Public Function CreateEntityString(ent As PRE2.Entity, spriteFolderLocation As String) As String
        'creates a string which can be saved to a file

        Dim result As String = If(Len(ent.name) > 0, ent.name, "unnamed") & Environment.NewLine

        'adds a list of frames and which sprites make them up
        If IsNothing(ent.frames) = False Then
            For frameIndex As Integer = 0 To UBound(ent.frames)
                Dim currentFrame As PRE2.Frame = ent.frames(frameIndex)

                For spriteIndex As Integer = 0 To UBound(currentFrame.sprites)
                    Dim currentSprite As PRE2.Sprite = currentFrame.sprites(spriteIndex)

                    If currentSprite.fileName <> Nothing Then
                        'eg Sprite1.sprt\25,0/
                        result += currentSprite.fileName.Remove(0, Len(spriteFolderLocation)) & ":" & Trim(Str(currentFrame.offsets(spriteIndex).X)) & "," & Trim(Str(currentFrame.offsets(spriteIndex).Y)) & "/"
                    Else
                        PRE2.DisplayError("Unknown file name for sprite #" & Trim(Str(spriteIndex + 1)) & " of frame #" & Trim(Str(frameIndex + 1)) & " for entity " & ent.name)
                    End If
                Next spriteIndex

                result = result.Remove(Len(result) - 1, 1) & ";"        'removes the last / and adds ;
            Next frameIndex
        End If

        result = result.Remove(Len(result) - 1, 1) & Environment.NewLine        'removes the last ; and adds a line break

        'adds a line for tags
        If IsNothing(ent.tags) = False Then
            For tagIndex As Integer = 0 To UBound(ent.tags)
                result += ent.tags(tagIndex).ToString()
                result += "/"
            Next tagIndex

            result = result.Remove(Len(result) - 1, 1)      'removes last /
        End If

        Return result
    End Function

    Public Function ReadEntityString(entityString As String, ByRef renderEngine As PRE2) As PRE2.Entity
        'reads a string, as created in CreateEntityString, returning an entity

        Dim result As New PRE2.Entity
        Dim lines() As String = entityString.Split(Environment.NewLine)     'there should be 2 lines
        Dim currentLine As String

        'loads the name of the entity
        currentLine = lines(0)
        result.name = currentLine

        'loads the frames of the entity
        If UBound(lines) >= 1 Then
            currentLine = lines(1).Replace(vbLf, "")
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
                    Dim fileLocation As String = renderEngine.spriteFolderLocation & spritesInFrame(spriteIndex).Split(":")(0)
                    fileLocation = fileLocation.Replace(vbLf, "")
                    fileLocation = fileLocation.Replace("\\", "\")

                    renderEngine.LoadSprite(fileLocation)
                    newFrame.sprites(spriteIndex) = renderEngine.FindLoadedSprite(fileLocation)
                    newFrame.offsets(spriteIndex) = offset
                Next spriteIndex

                result.frames(frameIndex) = newFrame
            Next frameIndex
        Else
            PRE2.DisplayError("Error whilst loading entity " & result.name & ". No line for frames found")
        End If

        'loads the tags
        If UBound(lines) >= 2 Then
            currentLine = lines(2).Replace(vbLf, "")
            Dim tagStrings() As String = currentLine.Split("/")
            ReDim result.tags(UBound(tagStrings))
            For tagIndex As Integer = 0 To UBound(tagStrings)
                result.tags(tagIndex) = New PRE2.Tag(tagStrings(tagIndex))
            Next tagIndex
        End If

        Return result
    End Function

End Module