'Richard Holmes
'29/08/2019
'Some predefined events for tags

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module TagEvents

    Public Const eventTagName As String = "event"
    Public Const listenerTagName As String = "listener"
    Public Const behaviourTagName As String = "behaviour"
    Public Const identifierTagName As String = "name"

    Public Sub BroadcastEvent(eventTag As Tag, ByRef thisRoom As Room, renderEngine As PRE2)
        'broadcasts a single event to all actors with a listener for the event

        For index As Integer = 0 To UBound(thisRoom.actors)
            Dim act As Actor = thisRoom.actors(index)
            If Not IsNothing(act.tags) Then
                For tagIndex As Integer = 0 To UBound(act.tags)
                    If LCase(act.tags(tagIndex).name) = listenerTagName AndAlso act.tags(tagIndex).InterpretArgument(identifierTagName) = eventTag.InterpretArgument(identifierTagName) Then   'TODO: fix this condition
                        ReceiveEvent(act, act.tags(tagIndex), renderEngine, thisRoom)
                    End If
                Next
            End If

            thisRoom.actors(index) = act
        Next
    End Sub

    Public Sub ReceiveEvent(ByRef act As Actor, listenerTag As Tag, renderEngine As PRE2, thisRoom As Room)
        'processes a received event

        Dim temp As Object = listenerTag.InterpretArgument(behaviourTagName)
        If IsArray(temp) Then
            For index As Integer = 0 To UBound(temp)
                ProcessTag(temp(index).InterpretArgument(), act, thisRoom, renderEngine)
            Next
        ElseIf Not IsNothing(temp) Then
            ProcessTag(temp.InterpretArgument(), act, thisRoom, renderEngine)
        End If
    End Sub


End Module
