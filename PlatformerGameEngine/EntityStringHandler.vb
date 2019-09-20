'Richard Holmes
'24/03/2019
'Entity file handler

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Module EntityStringHandler

    Public Function CreateEntityString(ent As PRE2.Entity, spriteFolderLocation As String) As String
        'creates a string which can be saved to a file
        'TODO: creating an entity string from a previously loaded entity is wrong

        Dim result As String = "" '= If(Len(ent.name) > 0, ent.name, "unnamed") & Environment.NewLine

        'adds a tag for frames

        If False Then
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
        End If

        'adds each tag to the main tag
        Dim mainTag As New PRE2.Tag("tags", ArrayToString(ent.tags))
        result += mainTag.ToString

        Return result
    End Function

    Public Function ReadEntityString(entityString As String, ByRef renderEngine As PRE2, Optional ByRef successfulLoad As Boolean = False) As PRE2.Entity
        'reads a string, as created in CreateEntityString, returning an entity

        Try
            Dim result As New PRE2.Entity With {.spriteFolderLocation = renderEngine.spriteFolderLocation}

            'loads the tags
            'Dim tagStrings() As String = JSONSplit(entityString, 0)
            'Dim temp As Object = JSONToTag(tagStrings(0)).GetArgument()
            Dim temp As Object = New PRE2.Tag(entityString).GetArgument()
            For index As Integer = 0 To UBound(temp)
                result.AddTag(New PRE2.Tag(temp(index).ToString))
            Next

            successfulLoad = True       'so that what calls this function knows the load was successful
            Return result
        Catch ex As Exception
            PRE2.DisplayError("An error occured whilst loading an entity" & vbCrLf & ex.ToString)
            Return Nothing
        End Try
    End Function

End Module