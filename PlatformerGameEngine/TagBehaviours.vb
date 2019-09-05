'Richard Holmes
'28/08/2019
'Some predefined behaviours for specific tags

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module TagBehaviours

    Dim errorMessageArgumentInvalid As String = " has an invalid argument"

    Public Sub ProcessTag(ByRef ent As PRE2.Entity, tagIndex As Integer)
        'processes a single tag and modifies the entity accordingly

        If Not IsNothing(ent) AndAlso Not IsNothing(ent.tags) AndAlso tagIndex >= 0 AndAlso tagIndex <= UBound(ent.tags) Then
            Dim tag As PRE2.Tag = ent.tags(tagIndex)

            Select Case LCase(tag.name)
                'basic movement
                Case LCase("xVel")
                    TagXVel(ent, tagIndex)
                Case LCase("yVel")
                    TagYVel(ent, tagIndex)
                Case LCase("gravity")
                    TagGravity(ent, tagIndex)
					
				
				Case LCase("setTag")
				
            End Select
        End If
    End Sub

    Private Sub TagXVel(ByRef ent As PRE2.Entity, tagIndex As Integer)
        'changes the location of the entity by its x velocity

        ent.location = New PointF(ent.location.X + ent.tags(tagIndex).args(0), ent.location.Y)
    End Sub

    Private Sub TagYVel(ByRef ent As PRE2.Entity, tagIndex As Integer)
        'changes the location of the entity by its y velocity

        ent.location = New PointF(ent.location.X, ent.location.Y + ent.tags(tagIndex).args(0))
    End Sub

    Private Sub TagGravity(ByRef ent As PRE2.Entity, tagIndex As Integer)
        'changes the yVel by the value given

        Dim newTag As New PRE2.Tag("yVel", ent.tags(tagIndex).args(0) * -1)
        If ent.HasTag("yVel") Then
            newTag.args(0) += ent.FindTag("yVel").args(0)
            ent.RemoveTag("yVel")
        End If
        ent.AddTag(newTag)
    End Sub


	Private Sub TagSetTag(ByRef ent As PRE2.Entity, tagIndex As Integer)
		'sets the tag with the given name to the given value
		'0:new tag name, 1+:new tag arguments
		
		Dim newTagArgs() As Object
		Dim newTag As New PRE2.Tag(ent.tags(tagIndex).args(0))
	End Sub
	
    Private Function IsANumber(value As Object) As Boolean
        'used for validating tags

        If Not IsNothing(value) AndAlso IsNumeric(value) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ProcessCalculation(calc As String, Optional ent As PRE2.Entity = Nothing, Optional room As FrmGame.Room = Nothing) As String
        'takes in a calculation as a string and returns the result

        Dim operatorSymbols() As String = {"^", "/", "*", "+", "-"}
        Dim parts(0) As String
        Dim operatorsUsed(0) As String

        'recursively processes the brackets first
        Dim cIndex As Integer = 0
        Dim currentBracketIndent As Integer = 0         'total opened brackets - total closed brackets
        Dim largestBracketOpeningIndex As Integer = -1  'index of opening of outermost bracket
        Do
            Dim c As String = calc(cIndex)
            If c = "(" Then
                If largestBracketOpeningIndex = -1 Then
                    largestBracketOpeningIndex = cIndex
                End If
                largestBracketOpeningIndex += 1

                cIndex += 1
            ElseIf c = ")" Then
                largestBracketOpeningIndex -= 1
                If largestBracketOpeningIndex >= 0 And currentBracketIndent = 0 Then
                    Dim bracketedCalc As String = Mid(calc, largestBracketOpeningIndex + 1, cIndex - largestBracketOpeningIndex + 1)
                    calc = calc.Replace(bracketedCalc, ProcessCalculation(Mid(bracketedCalc, 2, Len(bracketedCalc) - 2)))

                    cIndex = 0
                    largestBracketOpeningIndex = -1
                End If
            Else
                cIndex += 1
            End If
        Loop Until cIndex >= Len(calc)

        'creates an array of parts split by the operators listed above
        'also creates an ordered list of operators used to split
        For Each c As String In calc
            If Array.IndexOf(operatorSymbols, c) = -1 Then
                parts(UBound(parts)) += c
            Else
                operatorsUsed(UBound(operatorsUsed)) = c
                ReDim Preserve operatorsUsed(UBound(operatorsUsed) + 1)

                ReDim Preserve parts(UBound(parts) + 1)
            End If
        Next

        ReDim Preserve operatorsUsed(UBound(operatorsUsed) - 1)

        'goes in order using BODMAS of each operator finding each calculation it is used in
        Dim currentPartIndex As Integer = 0
        For Each operatorSymbol As String In operatorSymbols
            Dim operatorIndex As Integer = 0
            Do
                If operatorIndex <= UBound(operatorsUsed) AndAlso operatorSymbol = operatorsUsed(operatorIndex) Then
                    Dim leftPart As String = parts(operatorIndex).Trim
                    Dim rightPart As String = parts(operatorIndex + 1).Trim
                    Dim newPart As String

                    For Each part As String In {leftPart, rightPart}        'TODO: might need to expand on this part, eg referring to other instances
                        If ent.HasTag(part) Then
                            part = ent.FindTag(part).args(0)
                        ElseIf room.HasParam(part) Then
                            part = room.FindParam(part).args(0)
                        End If
                    Next

                    If IsNumeric(leftPart) AndAlso IsNumeric(rightPart) Then
                        Select Case operatorSymbol
                            Case operatorSymbols(0) '^
                                newPart = Val(leftPart) ^ Val(rightPart)
                            Case operatorSymbols(1) '/
                                newPart = Val(leftPart) / Val(rightPart)
                            Case operatorSymbols(2) '*
                                newPart = Val(leftPart) * Val(rightPart)
                            Case operatorSymbols(3) '+
                                newPart = Val(leftPart) + Val(rightPart)
                            Case operatorSymbols(4) '-
                                newPart = Val(leftPart) - Val(rightPart)
                            Case Else
                                newPart = leftPart & rightPart
                        End Select

                        parts(operatorIndex) = newPart
                        For index As Integer = operatorIndex To UBound(operatorsUsed) - 1
                            operatorsUsed(index) = operatorsUsed(index + 1)
                        Next
                        ReDim Preserve operatorsUsed(UBound(operatorsUsed) - 1)
                        For index As Integer = operatorIndex + 1 To UBound(parts) - 1
                            parts(index) = parts(index + 1)
                        Next
                        ReDim Preserve parts(UBound(parts) - 1)
                    Else
                        operatorIndex += 1
                    End If
                Else
                    operatorIndex += 1
                End If
            Loop Until operatorIndex >= UBound(parts)
        Next

        Return parts(0)
    End Function
End Module