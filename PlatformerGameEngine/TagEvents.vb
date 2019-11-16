'Richard Holmes
'29/08/2019
'Some predefined events for tags

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module TagEvents

    Public Sub BroadcastEvent(eventTag As Tag, ByRef thisRoom As Room, renderEngine As PRE2)
        'broadcasts a single event to all actors with a listener for the event

        For index As Integer = 0 To UBound(thisRoom.actors)
            Dim ent As Actor = thisRoom.actors(index)
            If Not IsNothing(ent.tags) Then
                For tagIndex As Integer = 0 To UBound(ent.tags)
                    If LCase(ent.tags(tagIndex).name) = "listener" AndAlso ent.tags(tagIndex).FindSubTag("name") = eventTag.FindSubTag("name") Then   'TODO: fix this condition
                        ReceiveEvent(ent, ent.tags(tagIndex), renderEngine, thisRoom)
                    End If
                Next
            End If

            thisRoom.actors(index) = ent
        Next
    End Sub

    Public Sub ReceiveEvent(ByRef ent As Actor, listenerTag As Tag, renderEngine As PRE2, Optional room As Room = Nothing)
        'processes a received event

        Dim temp As Tag() = listenerTag.FindSubTag("behaviour").InterpretArgument(Of Tag())
        For index As Integer = 0 To UBound(temp)
            TagBehaviours.ProcessTag(temp(index).InterpretArgument(Of Tag), ent, room, renderEngine)
        Next
    End Sub


End Module
