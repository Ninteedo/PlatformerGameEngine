'Richard Holmes
'29/08/2019
'Some predefined events for tags

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module TagEvents

    Public Sub BroadcastEvent(eventTag As PRE2.Tag, ByRef entityList() As PRE2.Entity)
        'broadcasts a single event to all entities with a listener for the event

        For Each ent As PRE2.Entity In entityList			
			For tagIndex As Integer = 0 To UBound(ent.tags)
				If LCase(ent.tags(tagIndex).name) = "listener" AndAlso ent.tags(tagIndex).args(0) = eventTag.args(0) Then
                    ReceiveEvent(ent, eventTag)
                End If
			Next
        Next
    End Sub

    Public Sub ReceiveEvent(ent As PRE2.Entity, eventTag As PRE2.Tag)
        'processes a received event


    End Sub


End Module
