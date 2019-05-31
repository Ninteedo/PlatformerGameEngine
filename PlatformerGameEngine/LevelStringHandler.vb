'Richard Holmes
'24/04/2019
'Level String Handler for platformer game engine

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Class LevelStringHandler
	
	Public Function LoadEntities(levelString As String, entityFolder As String) As PRE2.Entity()
		Dim ents() As PRE2.Entity
		
		Dim lines() As String = levelString.Split(Environment.NewLine)
		
		For Each line In lines
            Dim splits() As String = line.Split("/")
            Dim ent As PRE2.Entity

            For index As Integer = 0 To UBound(splits)
                If LCase(splits(index).Split(":")(0)) = "file" Then
                    ent = PRE2.LoadEntity(entityFolder & splits(index).Split(":")(1))
                End If
            Next index

            Dim tags(UBound(splits)) As PRE2.Tag

            For index As Integer = 1 To UBound(splits)
                tags(index) = New PRE2.Tag(splits(index))
            Next index

            If IsNothing(ent) = False Then
                If IsNothing(ents) = True Then
                    ReDim ents(0)
                Else
                    ReDim Preserve ents(UBound(ents) + 1)
                End If

                ents(UBound(ents)) = ent
            End If
        Next line

        Return ents
	End Function
End Class