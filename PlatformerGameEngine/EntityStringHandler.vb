'Richard Holmes
'24/03/2019
'Entity file handler

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Module EntityStringHandler

    Public Function CreateEntityString(ent As PRE2.Entity, spriteFolderLocation As String) As String
        'creates a string which can be saved to a file

        Dim result As String = "" '= If(Len(ent.name) > 0, ent.name, "unnamed") & Environment.NewLine

        'adds a tag for frames

        If Not IsNothing(ent.frames) Then
            Dim frameTags(UBound(ent.frames)) As PRE2.Tag

            For frameIndex As Integer = 0 To UBound(ent.frames)
                If Not IsNothing(ent.frames(frameIndex).sprites) Then
                    Dim frameTag As New PRE2.Tag("frame" & frameIndex, "")
                    Dim spriteTags(UBound(ent.frames(frameIndex).sprites)) As String

                    For spriteIndex As Integer = 0 To UBound(ent.frames(frameIndex).sprites)
                        Dim sprite As PRE2.Sprite = ent.frames(frameIndex).sprites(spriteIndex)
                        Dim offset As Point = ent.frames(frameIndex).offsets(spriteIndex)

                        '{"spriteLocation.sprt":[x,y]}
                        spriteTags(spriteIndex) = New PRE2.Tag(sprite.fileName.Remove(0, Len(spriteFolderLocation)),
                                                                         ArrayToString({offset.X, offset.Y})).ToString
                    Next

                    frameTag.argument = ArrayToString(spriteTags)
                    frameTags(frameIndex) = frameTag
                End If
            Next

            ent.RemoveTag("frames")
            ent.AddTag(New PRE2.Tag("frames", ArrayToString(frameTags)))
        End If

        'adds each tag to the main tag
        Dim mainTag As New PRE2.Tag("tags", ArrayToString(ent.tags))
        result += mainTag.ToString
        'If Not IsNothing(ent.tags) Then
        '    For tagIndex As Integer = 0 To UBound(ent.tags)
        '        result += ent.tags(tagIndex).ToString()
        '        result += vbCrLf
        '    Next tagIndex

        '    'result = result.Remove(Len(result) - 1, 1)      'removes last /
        'End If

        Return result
    End Function

    'Public Function ReadEntityStringOld(entityString As String, ByRef renderEngine As PRE2, Optional ByRef successfulLoad As Boolean = False) As PRE2.Entity
    '    'reads a string, as created in CreateEntityString, returning an entity

    '    Dim result As New PRE2.Entity
    '    Dim lines() As String = entityString.Split(Environment.NewLine)     'there should be 2 lines
    '    Dim currentLine As String

    '    Try
    '        'loads the frames of the entity
    '        If UBound(lines) >= 0 Then
    '            currentLine = lines(0).Replace(vbLf, "")
    '            Dim framesValues() As String = currentLine.Split(";")
    '            ReDim result.frames(UBound(framesValues))
    '            For frameIndex As Integer = 0 To UBound(framesValues)
    '                'loads each individual frame
    '                Dim newFrame As New PRE2.Frame

    '                'part for each individual sprite
    '                Dim spritesInFrame() As String = framesValues(frameIndex).Split("/")
    '                ReDim newFrame.sprites(UBound(spritesInFrame))
    '                ReDim newFrame.offsets(UBound(spritesInFrame))
    '                For spriteIndex As Integer = 0 To UBound(spritesInFrame)
    '                    Dim values() As String = spritesInFrame(spriteIndex).Split(":")
    '                    Dim spriteFile As String = values(0)
    '                    Dim offset As New Point(Val(values(1).Split(",")(0)), Val(values(1).Split(",")(1)))
    '                    Dim fileLocation As String = renderEngine.spriteFolderLocation & spritesInFrame(spriteIndex).Split(":")(0)
    '                    fileLocation = fileLocation.Replace(vbLf, "")
    '                    fileLocation = fileLocation.Replace("\\", "\")

    '                    renderEngine.LoadSprite(fileLocation)
    '                    newFrame.sprites(spriteIndex) = renderEngine.FindLoadedSprite(fileLocation)
    '                    newFrame.offsets(spriteIndex) = offset
    '                Next spriteIndex

    '                result.frames(frameIndex) = newFrame
    '            Next frameIndex
    '        Else
    '            PRE2.DisplayError("Error whilst loading entity " & result.name & ". No frames line found")
    '        End If

    '        'loads the tags
    '        If UBound(lines) >= 1 Then
    '            currentLine = lines(1).Replace(vbLf, "")
    '            Dim tagStrings() As String = currentLine.Split("/")
    '            ReDim result.tags(UBound(tagStrings))
    '            For tagIndex As Integer = 0 To UBound(tagStrings)
    '                result.tags(tagIndex) = New PRE2.Tag(tagStrings(tagIndex))
    '            Next tagIndex
    '        End If

    '        successfulLoad = True       'so that what calls this function knows the load was successful
    '        Return result
    '    Catch ex As Exception
    '        PRE2.DisplayError("An error occured whilst loading an entity" & vbCrLf & ex.ToString)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function ReadEntityString(entityString As String, ByRef renderEngine As PRE2, Optional ByRef successfulLoad As Boolean = False) As PRE2.Entity
        'reads a string, as created in CreateEntityString, returning an entity

        Dim result As New PRE2.Entity
        'Dim lines() As String = entityString.Split(Environment.NewLine)     'there should be 2 lines
        'Dim currentLine As String

        Try
            'loads the tags



            'Dim tagStrings() As String = {""}
            Dim mainTagString As String = ""
            Dim subStructureLevel As Integer = 0
            Dim inString As Boolean = False

            For cIndex As Integer = 0 To Len(entityString) - 1
                Dim c As String = entityString(cIndex)

                'If Not inString And subStructureLevel = 0 And c = "," Then
                '    ReDim Preserve tagStrings(UBound(tagStrings) + 1)
                '    tagStrings(UBound(tagStrings)) = ""
                'Else
                'tagStrings(UBound(tagStrings)) += c
                mainTagString += c

                If inString And c = """" AndAlso entityString(cIndex - 1) <> "\" Then
                        inString = False
                    ElseIf Not inString And c = """" Then
                        inString = True
                    ElseIf Not inString And c = "{" Then
                        subStructureLevel += 1
                    ElseIf Not inString And c = "}" Then
                        subStructureLevel -= 1
                    End If
                'End If
            Next

            'ReDim result.tags(UBound(tagStrings))
            'For index As Integer = 0 To UBound(tagStrings)
            '    result.tags(index) = New PRE2.Tag(tagStrings(index))
            'Next

            'loads the frames
            Dim framesArgument() As Object = result.FindTag("frames").GetArgument()
            ReDim result.frames(UBound(framesArgument))
            For frameIndex As Integer = 0 To UBound(framesArgument)
                result.frames(frameIndex) = New PRE2.Frame(framesArgument(frameIndex), renderEngine.spriteFolderLocation)
            Next

            successfulLoad = True       'so that what calls this function knows the load was successful
            Return result
        Catch ex As Exception
            PRE2.DisplayError("An error occured whilst loading an entity" & vbCrLf & ex.ToString)
            Return Nothing
        End Try
    End Function

End Module