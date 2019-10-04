'Richard Holmes
'29/08/2019
'Some predefined events for tags

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module TagEvents

    Public Sub BroadcastEvent(eventTag As Tag, ByRef entityList() As Entity, renderEngine As PRE2)
        'broadcasts a single event to all entities with a listener for the event

        For index As Integer = 0 To UBound(entityList)
            Dim ent As Entity = entityList(index).Clone
            If Not IsNothing(ent.tags) Then
                For tagIndex As Integer = 0 To UBound(ent.tags)
                    If LCase(ent.tags(tagIndex).name) = "listener" AndAlso ent.tags(tagIndex).GetArgument("name") = eventTag.GetArgument("name") Then   'TODO: fix this condition
                        ReceiveEvent(ent, ent.tags(tagIndex), renderEngine)
                    End If
                Next
            End If

            entityList(index) = ent.Clone
        Next
    End Sub

    Public Sub ReceiveEvent(ByRef ent As Entity, listenerTag As Tag, renderEngine As PRE2, Optional room As Room = Nothing)
        'processes a received event

        For Each tag As Tag In ent.tags
            'If tag.name = listenerTag.args(0) Then

            Dim temp As Object = listenerTag.GetArgument("behaviour")
            If IsArray(temp) Then
                For index As Integer = 0 To UBound(temp)
                    TagBehaviours.ProcessTag(temp(index).GetArgument(), ent, room, renderEngine)
                Next
            ElseIf Not IsNothing(temp) Then
                TagBehaviours.ProcessTag(temp.GetArgument(), ent, room, renderEngine)
            End If
            'End If
        Next
    End Sub


End Module
