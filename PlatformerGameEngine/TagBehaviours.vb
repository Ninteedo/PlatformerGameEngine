'Richard Holmes
'28/08/2019
'Some predefined behaviours for specific tags

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module TagBehaviours

    Dim errorMessageArgumentInvalid As String = " has an invalid argument"
    Const collisionTagName As String = "collision"
    Const collisionTypeTagName As String = "collisionType"
    Const vulnerableTagName As String = "vulnerable"
    Const effectTagName As String = "effect"
    Const solidTagName As String = "solid"

    Public Sub ProcessTag(tag As Tag, ByRef ent As Entity, ByRef room As FrmGame.Room, renderEngine As PRE2)
        'processes a single tag and modifies the entity accordingly

        'If Not IsNothing(ent) AndAlso Not IsNothing(ent.tags) AndAlso tagIndex >= 0 AndAlso tagIndex <= UBound(ent.tags) Then
        'Dim tag As Tag = ent.tags(tagIndex)

        Select Case LCase(tag.name)
            'basic movement
            Case "velocity"     '[xChange,yChange]
                Dim velocity1Temp As Object = tag.GetArgument
                Dim velocity1 As New Vector(velocity1Temp(0), velocity1Temp(1))

                VelocityHandling(ent, velocity1, room)


                'Case "move"     '[xChange,yChange]
                '    Dim moveTemp As Object = tag.GetArgument()
                '    Dim frictionTag As Tag = ent.FindTag("friction")
                '    Dim newCoords As New PointF(ent.location.X + moveTemp(0), ent.location.Y + moveTemp(1))
                '    ent.AddTag(New Tag("lastMove", ArrayToString({moveTemp(0), moveTemp(1)})), True)       'used to reverse movement if necessary

                '    If ent.HasTag("hitbox") Then
                '        CheckForOverlaps(ent, room, renderEngine)
                '    End If

                '    ent.location = newCoords
                'Case LCase("xVel")
                '    'TagXVel(ent, tagIndex)
                '    ent.location = New PointF(ent.location.X + FrmGame.GetEntityArgument(tag, ent, room, 0), ent.location.Y)
                'Case LCase("yVel")
                '    'TagYVel(ent, tagIndex)
                '    ent.location = New PointF(ent.location.X, ent.location.Y + FrmGame.GetEntityArgument(tag, ent, room, 0))
                'Case "xacc"


                'Case LCase("gravity")
                '    'TagGravity(ent, tagIndex)
                '    ent.AddTag(New Tag("yVel", FrmGame.GetEntityArgument(tag, ent, room, 0) * -1 +
                '                            FrmGame.GetEntityArgument(ent.FindTag("yVel"), ent, room, 0)))


                'meta
            Case LCase("addTag")
                Dim newTag As New Tag(FrmGame.GetArgument(tag, ent, room))
                ent.AddTag(newTag, True)
            Case LCase("removeTag")
                ent.RemoveTag(tag.GetArgument())
            Case "execute"
                ProcessTag(New Tag(tag.GetArgument.ToString), ent, room, renderEngine)
        End Select
        'End If
    End Sub

#Region "Collision Detection"

    Private Sub VelocityHandling(ByRef ent As Entity, ByRef velocity As Vector, ByRef room As FrmGame.Room)
        If ent.HasTag(collisionTagName) Then
            For Each otherEnt As Entity In room.instances
                If ent.name <> otherEnt.name Then
                    'Dim velocity2Temp As Object = otherEnt.FindTag("velocity").GetArgument
                    'Dim velocity2 As New Vector(0, 0)
                    'If Not IsNothing(velocity2Temp) Then
                    '    velocity2 = New Vector(velocity2Temp(0), velocity2Temp(1))
                    'End If

                    If otherEnt.HasTag(collisionTagName) Then
                        Dim collisionResult As PolygonCollisionResult = CheckPolygons(ent, otherEnt, velocity) ' + velocity2)

                        Dim entCollisionTypesTemp As Object = ent.FindTag(collisionTagName).GetArgument(collisionTypeTagName).GetArgument()
                        Dim entVulnerable As Boolean = False        'stores whether the entity is vulnerable
                        If Not IsNothing(entCollisionTypesTemp) Then
                            If IsArray(entCollisionTypesTemp) Then
                                For index As Integer = 0 To UBound(entCollisionTypesTemp)
                                    If entCollisionTypesTemp(index).name = vulnerableTagName Then
                                        entVulnerable = True
                                        Exit For
                                    End If
                                Next
                            Else
                                entVulnerable = entCollisionTypesTemp.name = vulnerableTagName   'compares the collision type to the stored vulnerable name
                            End If
                        End If

                        Dim otherEntCollisionTypesTemp As Object = otherEnt.FindTag(collisionTagName).GetArgument(collisionTypeTagName).GetArgument()
                        Dim otherEntEffect As Tag = Nothing
                        Dim otherEntSolid As Boolean = False
                        If Not IsNothing(otherEntCollisionTypesTemp) Then
                            If IsArray(otherEntCollisionTypesTemp) Then
                                For index As Integer = 0 To UBound(otherEntCollisionTypesTemp)
                                    If otherEntCollisionTypesTemp(index).name = effectTagName Then
                                        otherEntEffect = otherEntCollisionTypesTemp(index)
                                    ElseIf otherEntCollisionTypesTemp(index).name = solidTagName Then
                                        otherEntSolid = True
                                    End If
                                Next
                            Else
                                If otherEntCollisionTypesTemp.name = effectTagName Then
                                    otherEntEffect = otherEntCollisionTypesTemp
                                ElseIf otherEntCollisionTypesTemp.name = solidTagName Then
                                    otherEntSolid = True
                                End If
                            End If
                        End If

                        If otherEntSolid And collisionResult.willIntersect Then
                            'adjusts velocity to prevent penetration
                            velocity += collisionResult.minTranslationVect
                        End If

                        If entVulnerable And Not IsNothing(otherEntEffect) Then
                            'executes the effect of the other entity
                        End If
                    End If

                End If
            Next
        End If

        ent.location = New PointF(ent.location.X + velocity.X, ent.location.Y + velocity.Y)
    End Sub


    Public Function CheckPolygons(ent1 As Entity, ent2 As Entity, velocity As Vector) As PolygonCollisionResult
        Dim ent1Poly As New Polygon(ent1.GetEntityHitbox())
        Dim ent2Poly As New Polygon(ent2.GetEntityHitbox())

        Dim poly1Translation As New Vector
        Dim collisionResult As PolygonCollisionResult = PolygonCollision(ent1Poly, ent2Poly, velocity)



        'poly1.ChangeLocation(poly1Translation)
        Return collisionResult
    End Function



    Public Structure Vector
        Dim X As Single
        Dim Y As Single

        Public Sub New(x As Single, y As Single)
            Me.X = x
            Me.Y = y
        End Sub

        Public Sub New(point As PointF)
            Me.X = point.X
            Me.Y = point.Y
        End Sub

        Public ReadOnly Property Magnitude
            Get
                Return Math.Sqrt(X ^ 2 + Y ^ 2)
            End Get
        End Property

        Public Sub Normalize()
            'changes the magnitude of the vector to 1

            X /= Magnitude
            Y /= Magnitude
        End Sub

        Public Function GetNormalized()
            'returns a copy of this vector with magnitude set to 1

            Return New Vector(X / Magnitude, Y / Magnitude)
        End Function

        Public Function DotProduct(otherVect As Vector) As Single
            'returns the dot product of this vector and another given vector

            Return Me.X * otherVect.X + Me.Y * otherVect.Y
        End Function

        Public Shared Operator +(vect1 As Vector, vect2 As Vector) As Vector
            Return New Vector(vect1.X + vect2.X, vect1.Y + vect2.Y)
        End Operator

        Public Shared Operator -(vect1 As Vector, vect2 As Vector) As Vector
            Return New Vector(vect1.X - vect2.X, vect1.Y - vect2.Y)
        End Operator

        Public Shared Operator *(vect1 As Vector, factor As Single) As Vector
            Return New Vector(vect1.X * factor, vect1.Y * factor)
        End Operator
    End Structure

    Public Structure Polygon
        Dim points() As Vector
        Dim edges() As Vector

        Public Sub New(points() As Vector)
            Me.points = points
            CalculateEdges()
        End Sub

        Public Sub New(rect As RectangleF)
            points = {New Vector(rect.Right, rect.Top), New Vector(rect.Right, rect.Bottom), New Vector(rect.Left, rect.Bottom), New Vector(rect.Left, rect.Top)}
            CalculateEdges()
        End Sub

        Public Sub CalculateEdges()
            'creates the edges array using the points array

            Dim point1 As Vector = Nothing
            Dim point2 As Vector = Nothing

            If Not IsNothing(points) Then
                ReDim edges(UBound(points))

                For index As Integer = 0 To UBound(points)
                    point1 = points(index)
                    point2 = If(index < UBound(points), points(index + 1), points(0))      'loops back round to close polygon

                    edges(index) = point2 - point1
                Next
            Else
                edges = Nothing
            End If
        End Sub

        Public Function Centre() As Vector
            Dim totalX As Single = 0
            Dim totalY As Single = 0

            For index As Integer = 0 To UBound(points)
                totalX += points(index).X
                totalY += points(index).Y
            Next

            Return New Vector(totalX / points.Length, totalY / points.Length)
        End Function

        Public Sub ChangeLocation(change As Vector)
            'changes the location of the polygon by the given vector

            For index As Integer = 0 To UBound(points)
                points(index) += change
            Next

            CalculateEdges()
        End Sub

        'Public Function ToRectangle() As RectangleF
        '    'wont work with irregular quadrahedrons

        '    If points.Length = 4 Then
        '        Dim topLeft As Vector = points(0)
        '        Dim bottomRight As Vector = points(0)

        '        For pointIndex As Integer = 0 To UBound(points)
        '            If points(pointIndex).X < topLeft.X And points(pointIndex).Y < topLeft.Y Then
        '                topLeft = points(pointIndex)
        '            ElseIf points(pointIndex).X > bottomRight.X And points(pointIndex).Y > bottomRight.Y Then
        '                bottomRight = points(pointIndex)
        '            End If
        '        Next

        '        Return New RectangleF(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y)
        '    Else
        '        PRE2.DisplayError("Tried to convert a polygon with " & points.Length & " sides to a polygon")
        '        Return Nothing
        '    End If
        'End Function
    End Structure

    Public Structure PolygonCollisionResult
        Dim intersecting As Boolean
        Dim willIntersect As Boolean
        Dim minTranslationVect As Vector
    End Structure

    Public Sub ProjectPolygon(axis As Vector, shape As Polygon, ByRef min As Single, ByRef max As Single)
        'https://www.metanetsoftware.com/technique/tutorialA.html#appendixA

        Dim dp As Single = axis.DotProduct(shape.points(0))
        min = dp
        max = dp

        For index As Integer = 1 To UBound(shape.points)
            dp = axis.DotProduct(shape.points(index))

            If dp < min Then
                min = dp
            ElseIf dp > max Then
                max = dp
            End If
        Next
    End Sub

    Public Function IntervalDistance(min1 As Single, max1 As Single, min2 As Single, max2 As Single) As Single
        If min1 < min2 Then
            Return min2 - max1
        Else
            Return min1 - max2
        End If
    End Function

    Public Function PolygonCollision(poly1 As Polygon, poly2 As Polygon, velocity As Vector) As PolygonCollisionResult
        Dim result As New PolygonCollisionResult

        Dim minInterval As Single = Single.PositiveInfinity     'infinity so it will always be greater than the interval distance
        Dim translationAxis As Vector = Nothing

        'loops through each edge for poly1 and poly2
        For edgeIndex As Integer = 0 To UBound(poly1.edges) + UBound(poly2.edges)
            Dim edge As Vector = Nothing

            If edgeIndex <= UBound(poly1.edges) Then
                edge = poly1.edges(edgeIndex)
            Else
                edge = poly2.edges(edgeIndex - 4)
            End If

            'find if the polygons are intersecting
            Dim axis As Vector = New Vector(edge.Y * -1, edge.X)       'perpendicular to edge
            axis.Normalize()

            'get the projection of the polygon on the axis
            Dim min1 As Single
            Dim max1 As Single
            Dim min2 As Single
            Dim max2 As Single
            ProjectPolygon(axis, poly1, min1, max1)
            ProjectPolygon(axis, poly2, min2, max2)

            'check if the projections are intersecting
            Dim intervalDist As Single = IntervalDistance(min1, max1, min2, max2)
            result.intersecting = Not intervalDist > 0

            'part for finding if the shapes will intersect after moving

            'projects the velocity onto the axis
            Dim velocityProjection As Single = axis.DotProduct(velocity)

            'gets the projection of poly1 after the movement
            If velocityProjection < 0 Then
                min1 += velocityProjection
            Else
                max1 += velocityProjection
            End If

            intervalDist = IntervalDistance(min1, max1, min2, max2)
            result.willIntersect = Not intervalDist > 0

            'checks if any intersection is or will occur, and exits the for loop if it wont
            If Not result.intersecting And Not result.willIntersect Then
                Exit For
            End If

            'finds the magnitude of the interval distance
            intervalDist = Math.Abs(intervalDist)
            'compares the interval to the min interval
            If intervalDist < minInterval Then
                minInterval = intervalDist
                translationAxis = axis

                'makes sure that translation axis is positive
                Dim difference As Vector = poly1.Centre - poly2.Centre
                If difference.DotProduct(translationAxis) < 0 Then
                    translationAxis *= -1
                End If
            End If
        Next

        'finds the vector to prevent intersection
        If result.willIntersect Then
            result.minTranslationVect = translationAxis * minInterval
        End If

        Return result
    End Function

#End Region

#Region "Tags to other Data Types"

    Public Function TagToRectangleF(rectangleTag As Tag, Optional relativeLocation As PointF = Nothing, Optional scale As Single = 1) As RectangleF
        'converts a rectangle tag into a rectangle

        Dim result As RectangleF = Nothing

        If Not IsNothing(rectangleTag) Then
            Dim originTag As Tag = rectangleTag.GetArgument("origin")
            Dim sizeTag As Tag = rectangleTag.GetArgument("size")

            result = New RectangleF(New PointF(originTag.GetArgument()(0) * scale + relativeLocation.X, originTag.GetArgument()(1) * scale + relativeLocation.Y),
                                    New SizeF(sizeTag.GetArgument()(0) * scale, sizeTag.GetArgument()(1) * scale))
        End If

        Return result
    End Function

#End Region

#Region "Calculation"

    Public Function ProcessCalculation(calc As String, Optional ent As Entity = Nothing, Optional room As FrmGame.Room = Nothing) As String
        'takes in a calculation as a string and returns the result

        Dim operatorSymbols() As String = {"^", "/", "*", "+", "-"}
        Dim parts(0) As String
        Dim operatorsUsed(0) As String

        If IsNothing(calc) OrElse IsNumeric(calc) Then
            Return calc
        End If

        'recursively processes the brackets first
        Dim cIndex As Integer = 0
        Dim currentBracketIndent As Integer = 0         'total opened brackets - total closed brackets
        Dim largestBracketOpeningIndex As Integer = -1  'index of opening of outermost bracket
        Dim inString As Boolean = False
        Do
            Dim c As String = calc(cIndex)
            If Not inString Then
                If c = """" Then
                    inString = True
                ElseIf c = "(" Then
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
            ElseIf c = """" AndAlso calc(cIndex - 1) = "\" Then
                inString = False
                cIndex += 1
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
                    Dim theseParts() As String = {leftPart, rightPart}

                    For index As Integer = 0 To UBound(theseParts)        'TODO: might need to expand on this part, eg referring to other instances
                        Dim reference As Object = FrmGame.FindReference(ent, theseParts(index), room)
                        If Not IsNothing(reference) Then
                            theseParts(index) = reference
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

#End Region

End Module