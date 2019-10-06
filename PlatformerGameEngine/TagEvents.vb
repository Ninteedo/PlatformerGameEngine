'Richard Holmes
'29/08/2019
'Some predefined events for tags

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module TagEvents

    Public Sub BroadcastEvent(eventTag As Tag, ByRef thisRoom As Room, renderEngine As PRE2)
        'broadcasts a single event to all entities with a listener for the event

        For index As Integer = 0 To UBound(thisRoom.instances)
            Dim ent As Entity = thisRoom.instances(index)
            If Not IsNothing(ent.tags) Then
                For tagIndex As Integer = 0 To UBound(ent.tags)
                    If LCase(ent.tags(tagIndex).name) = "listener" AndAlso ent.tags(tagIndex).InterpretArgument("name") = eventTag.InterpretArgument("name") Then   'TODO: fix this condition
                        ReceiveEvent(ent, ent.tags(tagIndex), renderEngine, thisRoom)
                    End If
                Next
            End If

            thisRoom.instances(index) = ent
        Next
    End Sub

    Public Sub ReceiveEvent(ByRef ent As Entity, listenerTag As Tag, renderEngine As PRE2, Optional room As Room = Nothing)
        'processes a received event

        Dim temp As Object = listenerTag.InterpretArgument("behaviour")
        If IsArray(temp) Then
            For index As Integer = 0 To UBound(temp)
                TagBehaviours.ProcessTag(temp(index).GetArgument(), ent, room, renderEngine)
            Next
        ElseIf Not IsNothing(temp) Then
            TagBehaviours.ProcessTag(temp.GetArgument(), ent, room, renderEngine)
        End If
    End Sub


End Module
