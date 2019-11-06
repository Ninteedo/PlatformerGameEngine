Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Module ArrayModifying
    Public Function RemoveItem(Of t)(ByVal array As t(), ByVal removeIndex As Integer) As t()
        'removes the item at the given index from a given array of a given type

        If IsNothing(array) Then
            PRE2.DisplayError("Attempted to remove something from an empty array")
        ElseIf removeIndex < 0 Or removeIndex > UBound(array) Then
            PRE2.DisplayError("Attempted to remove something from an array at an invalid index")
        Else
            If UBound(array) > 0 Then
                For index As Integer = removeIndex To UBound(array) - 1
                    array(index) = array(index + 1)
                Next
                ReDim Preserve array(UBound(array) - 1)
            Else        'if array length is 0 then removing the last item will remove last element of array
                array = Nothing
            End If
        End If

        Return array
    End Function

    Public Function InsertItem(Of t)(ByVal array As t(), ByVal newItem As t, Optional insertIndex As Integer = -1) As t()
        'inserts the given item to the end of the given array of a given type

        If Not IsNothing(array) Then
            ReDim Preserve array(UBound(array) + 1)

            If insertIndex >= 0 Then        'inserts into middle of array
                For index As Integer = insertIndex To UBound(array) - 1
                    array(index + 1) = array(index)
                Next
                array(insertIndex) = newItem
            Else    'if no value (default -1) given then adds item to end of array
                array(UBound(array)) = newItem
            End If
        Else        'previously empty array
            array = {newItem}
        End If

        Return array
    End Function

End Module
