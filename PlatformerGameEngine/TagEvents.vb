'Richard Holmes
'29/08/2019
'Some predefined events for tags

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module TagEvents

    Public Sub BroadcastEvent(eventTag As PRE2.Tag, ByRef entityList() As PRE2.Entity)
        'broadcasts a single event to all entities with a listener for the event

        For Each ent As PRE2.Entity In entityList
            If Not IsNothing(ent.tags) Then
                For tagIndex As Integer = 0 To UBound(ent.tags)
                    If LCase(ent.tags(tagIndex).name) = "listener" AndAlso ent.tags(tagIndex).args(0) = eventTag.args(0) Then
                        ReceiveEvent(ent, ent.tags(tagIndex))
                    End If
                Next
            End If
        Next
    End Sub

    Public Sub ReceiveEvent(ent As PRE2.Entity, listenerTag As PRE2.Tag, Optional room As FrmGame.Room = Nothing)
        'processes a received event

        For Each tag As PRE2.Tag In ent.tags
            'If tag.name = listenerTag.args(0) Then
            For index As Integer = 1 To UBound(listenerTag.args)
                TagBehaviours.ProcessTag(ent, listenerTag.args(index), room)
            Next
            'End If
        Next
    End Sub


End Module
