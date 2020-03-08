Imports PlatformerGameEngine.My
Imports MySql.Data.MySqlClient

Public Module TagBehaviours

    Public Function ProcessTag(inputTag As Tag, ByRef act As Actor, ByRef game As FrmGameExecutor) As Boolean
        'processes a single tag and modifies the actor accordingly
        'returns false if an error occured or true if no error occured

        Try
            Select Case LCase(inputTag.Name)
                Case "velocity"     '[xChange,yChange]
                    Dim velocityTemp As Object = InterpretValue(inputTag.Argument, True, act, game)
                    Dim velocity As New Vector(velocityTemp(0), velocityTemp(1))

                    VelocityHandling(act, velocity, game)

                Case "settag"
                    'all tags in the actor with a given tag name have the argument set to what is provided
                    Dim newTag As Tag = InterpretValue(inputTag.Argument, True, act, game)
                    act.SetTag(newTag)
                Case "addtag"
                    'adds a tag to the actor
                    Dim newTag As Tag = InterpretValue(inputTag.Argument, True, act, game)
                    act.AddTag(newTag)
                Case "removetag"
                    act.RemoveTag(inputTag.InterpretArgument())
                Case "addparam"
                    Dim newTag As Tag = InterpretValue(inputTag.Argument, True, act, game)
                    game.CurrentLevel.AddTag(newTag)
                Case "setparam"
                    Dim newTag As Tag = InterpretValue(inputTag.Argument, True, act, game)
                    game.CurrentLevel.SetTag(newTag)
                Case "removeparam"
                    game.CurrentLevel.RemoveTag(inputTag.InterpretArgument())

                Case "execute"
                    ProcessTag(New Tag(inputTag.InterpretArgument.ToString), act, game)

                Case "broadcast"
                    game.BroadcastEvent(New Tag(inputTag.InterpretArgument.ToString))

                Case "if"
                    IfTagHandling(inputTag, act, game)
                Case "for"
                    ForTagHandling(inputTag, act, game)

                Case "savescore"
                    Dim points As String = InterpretValue(inputTag.FindSubTag("points").Argument, True, act, game)
                    Dim gameName As String = inputTag.FindSubTag("gameName").InterpretArgument()

                    game.Pause()
                    SaveScore(points, gameName)
                    game.Unpause()
            End Select

            Return True
        Catch ex As Exception
            DisplayError("An error occured whilst trying to process the following tag: " & inputTag.ToString() &
                         vbCrLf & "Game will now close" &
                         vbCrLf & ex.ToString())
            Return False    'ends game
        End Try
    End Function

    Public Sub ProcessSubTags(inputTag As Tag, ByRef act As Actor, ByRef game As FrmGameExecutor)
        'calls ProcessTag on all the subtags in the input tag
        If Not IsNothing(inputTag) Then
            Dim temp As Object = inputTag.InterpretArgument()
            If IsArray(temp) Then
                For index As Integer = 0 To UBound(temp)
                    If temp(index).GetType() = GetType(Tag) Then   'checks that the temp is actually a tag before processing it
                        ProcessTag(temp(index), act, game)
                    End If
                Next
            ElseIf Not IsNothing(temp) Then
                If temp.GetType() = GetType(Tag) Then   'checks that the temp is actually a tag before processing it
                    ProcessTag(temp, act, game)
                End If
            End If
        End If
    End Sub

#Region "Code Structures"

#Region "If"

    Private Sub IfTagHandling(ifTag As Tag, ByRef act As Actor, ByRef game As FrmGameExecutor)
        Dim condition As String = ifTag.FindSubTag("con").Argument
        Dim executeTag As Tag

        'assesses condition then executes either "then" or "else" part of the if
        If AssessCondition(condition, act, game) Then
            executeTag = ifTag.FindSubTag("then")
        Else
            executeTag = ifTag.FindSubTag("else")
        End If
        ProcessSubTags(executeTag, act, game)
    End Sub

#End Region

#Region "For"

    Private Sub ForTagHandling(forTag As Tag, ByRef act As Actor, ByRef game As FrmGameExecutor)
        Dim repeats As UInteger = forTag.FindSubTag("repeats").InterpretArgument()
        Dim executeTag As Tag = forTag.FindSubTag("do").InterpretArgument()

        For index As Integer = 0 To repeats - 1
            ProcessSubTags(executeTag, act, game)
        Next
    End Sub

#End Region

#End Region


#Region "Collision Detection"

    Private Sub VelocityHandling(ByRef act As Actor, ByRef velocity As Vector, ByRef game As FrmGameExecutor)
        Const collisionTagName As String = "collision"
        Const collisionTypeTagName As String = "collisionType"
        Const vulnerableTagName As String = "vulnerable"
        Const effectTagName As String = "effect"
        Const solidTagName As String = "solid"

        If act.HasTag(collisionTagName) Then
            For Each otherAct As Actor In game.CurrentRoom.Actors
                If act.Name <> otherAct.Name Then   'prevents actor colliding with itself
                    If otherAct.HasTag(collisionTagName) Then
                        Dim collisionResult As PolygonCollisionResult = CheckPolygons(act, otherAct, velocity)

                        'checks that collision is occurring
                        If collisionResult.Intersecting Or collisionResult.WillIntersect Then
                            'gets this actor's collision types
                            Dim entCollisionTypesTemp As Object = act.FindTag(collisionTagName).InterpretArgument(collisionTypeTagName).InterpretArgument()
                            Dim entVulnerable As Boolean = False        'stores whether the actor is vulnerable
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

                            'gets other actor's collision types
                            Dim otherEntCollisionTypesTemp As Object = otherAct.FindTag(collisionTagName).InterpretArgument(collisionTypeTagName).InterpretArgument()
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

                            If otherEntSolid And collisionResult.WillIntersect Then
                                'adjusts velocity to prevent penetration
                                velocity += collisionResult.MinTranslationVect
                            End If

                            If entVulnerable And Not IsNothing(otherEntEffect) Then
                                'executes the effect of the other actor
                            End If
                        End If
                    End If

                End If
            Next
            act.SetTag(New Tag("velocity", ArrayToString({velocity.X, velocity.Y})))      'changes the actor's velocity
        End If

        act.Location = New PointF(act.Location.X + velocity.X, act.Location.Y + velocity.Y)
    End Sub

    Private Function RectanglesOverlapping(rect1 As RectangleF, rect2 As RectangleF) As Boolean
        Return Not (rect1.Left > rect2.Right Or rect1.Right < rect2.Left Or rect1.Top > rect2.Bottom Or rect1.Bottom < rect2.Top)
    End Function

    Private Function CheckPolygons(ent1 As Actor, ent2 As Actor, velocity As Vector) As PolygonCollisionResult
        Dim ent1Rect As RectangleF = ent1.Hitbox()
        Dim ent2Rect As RectangleF = ent2.Hitbox()
        Dim ent1RectMoved As New RectangleF(New PointF(ent1Rect.X + velocity.X, ent2Rect.Y + velocity.Y), ent1Rect.Size)
        Dim ent1Poly As New Polygon(ent1Rect)
        Dim ent2Poly As New Polygon(ent2Rect)

        Dim collisionResult As PolygonCollisionResult

        'Dim poly1Translation As New Vector
        If RectanglesOverlapping(ent1Rect, ent2Rect) OrElse RectanglesOverlapping(ent1RectMoved, ent2Rect) Then
            collisionResult = PolygonCollision(ent1Poly, ent2Poly, velocity)
        Else
            collisionResult = New PolygonCollisionResult With {.Intersecting = False, .WillIntersect = False, .MinTranslationVect = New Vector(0, 0)}
        End If
        Return collisionResult
    End Function

    Private Structure Vector
        Dim X As Single
        Dim Y As Single

        Public Sub New(x As Single, y As Single)
            Me.X = x
            Me.Y = y
        End Sub

        Public ReadOnly Property Magnitude As Single
            Get
                Return Math.Sqrt(X ^ 2 + Y ^ 2)
            End Get
        End Property

        Public Sub Normalize()
            'changes the magnitude of the vector to 1

            X /= Magnitude
            Y /= Magnitude
        End Sub

        Public Overrides Function ToString() As String
            Return "(" & X & "," & Y & ")"
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

    Private Structure Polygon
        ReadOnly Points() As Vector
        Dim Edges() As Vector

        Public Sub New(points() As Vector)
            Me.Points = points
            CalculateEdges()
        End Sub

        Public Sub New(rect As RectangleF)
            Points = {New Vector(rect.Right, rect.Top), New Vector(rect.Right, rect.Bottom), New Vector(rect.Left, rect.Bottom), New Vector(rect.Left, rect.Top)}
            CalculateEdges()
        End Sub

        Private Sub CalculateEdges()
            'creates the edges array using the points array

            Dim point1 As Vector
            Dim point2 As Vector

            If Not IsNothing(Points) Then
                ReDim Edges(UBound(Points))

                For index As Integer = 0 To UBound(Points)
                    point1 = Points(index)
                    point2 = If(index < UBound(Points), Points(index + 1), Points(0))      'loops back round to close polygon

                    Edges(index) = point2 - point1
                Next
            Else
                Edges = {}
            End If
        End Sub

        Public ReadOnly Property Centre As Vector
            Get
                Dim totalX As Single = 0
                Dim totalY As Single = 0

                For index As Integer = 0 To UBound(Points)
                    totalX += Points(index).X
                    totalY += Points(index).Y
                Next

                Return New Vector(totalX / Points.Length, totalY / Points.Length)
            End Get
        End Property

        Public Sub ChangeLocation(change As Vector)
            'changes the location of the polygon by the given vector

            For index As Integer = 0 To UBound(Points)
                Points(index) += change
            Next

            CalculateEdges()
        End Sub
    End Structure

    Private Structure PolygonCollisionResult
        Dim Intersecting As Boolean
        Dim WillIntersect As Boolean
        Dim MinTranslationVect As Vector
    End Structure

    Private Sub ProjectPolygon(axis As Vector, shape As Polygon, ByRef min As Single, ByRef max As Single)
        'adapted from https://www.metanetsoftware.com/technique/tutorialA.html#appendixA

        Dim dp As Single = axis.DotProduct(shape.Points(0))
        min = dp
        max = dp

        For index As Integer = 1 To UBound(shape.Points)
            dp = axis.DotProduct(shape.Points(index))

            If dp < min Then
                min = dp
            ElseIf dp > max Then
                max = dp
            End If
        Next
    End Sub

    Private Function IntervalDistance(min1 As Single, max1 As Single, min2 As Single, max2 As Single) As Single
        If min1 < min2 Then
            Return min2 - max1
        Else
            Return min1 - max2
        End If
    End Function

    Private Function PolygonCollision(poly1 As Polygon, poly2 As Polygon, velocity As Vector) As PolygonCollisionResult
        'adapted from https://www.codeproject.com/Articles/15573/2D-Polygon-Collision-Detection

        Dim result As New PolygonCollisionResult

        Dim minInterval As Single = Single.PositiveInfinity     'infinity so it will always be greater than the interval distance
        Dim translationAxis As Vector = Nothing

        'loops through each edge for poly1 and poly2
        For edgeIndex As Integer = 0 To UBound(poly1.Edges) + UBound(poly2.Edges)
            Dim edge As Vector

            If edgeIndex <= UBound(poly1.Edges) Then
                edge = poly1.Edges(edgeIndex)
            Else
                edge = poly2.Edges(edgeIndex - 4)
            End If

            'find if the polygons are intersecting

            'gets perpendicular axis of this edge
            Dim axis As Vector = New Vector(edge.Y * -1, edge.X)
            axis.Normalize()

            'get the projection of the polygon on the axis
            Dim min1, max1, min2, max2 As Single
            ProjectPolygon(axis, poly1, min1, max1)
            ProjectPolygon(axis, poly2, min2, max2)

            'check if the projections are intersecting
            Dim intervalDist As Single = IntervalDistance(min1, max1, min2, max2)
            result.Intersecting = intervalDist <= 0

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
            result.WillIntersect = Not intervalDist > 0

            'checks if any intersection is or will occur, and exits the for loop if it wont
            If Not result.Intersecting And Not result.WillIntersect Then
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
        If result.WillIntersect Then
            result.MinTranslationVect = translationAxis * minInterval
        End If

        Return result
    End Function

#End Region

#Region "Calculation"

    Private Function ProcessCalcBrackets(calc As String, Optional ByRef act As Actor = Nothing,
                                         Optional ByRef game As FrmGameExecutor = Nothing) As String
        'processes each of the brackets in the calculation
        'TODO: nested brackets

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
                        Dim bracketedCalc As String = Mid(calc, largestBracketOpeningIndex + 1,
                                                          cIndex - largestBracketOpeningIndex + 1)
                        'recursion
                        calc = calc.Replace(bracketedCalc,
                                            ProcessCalculation(Mid(bracketedCalc, 2, Len(bracketedCalc) - 2), act, game))

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

        Return calc
    End Function

    Public Function ProcessCalculation(calc As String, Optional ByRef act As Actor = Nothing,
                                       Optional ByRef game As FrmGameExecutor = Nothing) As String
        'takes in a calculation as a string and returns the result

        If Not IsNumeric(calc) And Not IsNothing(calc) AndAlso Len(calc) > 0 Then
            Dim operatorSymbols() As String = {"^", "/", "*", "+", "-"}     'ordered by BODMAS
            Dim parts() As String = {""}
            Dim operatorsUsed() As String = {}

            'recursively processes the brackets first
            calc = ProcessCalcBrackets(calc, act, game)

            'creates an array of parts split by the operators listed above
            'also creates an ordered list of operators used to split
            For Each c As String In calc
                If Array.IndexOf(operatorSymbols, c) = -1 Then
                    parts(UBound(parts)) += c
                Else
                    'new part
                    operatorsUsed = InsertItem(operatorsUsed, c)
                    parts = InsertItem(parts, "")
                End If
            Next

            'checks for any negative signs
            Dim partIndex As Integer = 0
            Do While partIndex < UBound(parts)
                If Len(parts(partIndex)) = 0 And operatorsUsed(partIndex) = "-" Then
                    If IsNumeric(parts(partIndex + 1)) Then
                        parts(partIndex + 1) = parts(partIndex + 1) * -1
                    ElseIf partIndex + 2 <= UBound(parts) AndAlso IsNumeric(parts(partIndex + 1) & parts(partIndex + 2)) Then
                        'this is used to prevent errors from small scientific notation numbers like 4.2E-06 

                        parts(partIndex + 1) = parts(partIndex + 1) & operatorsUsed(partIndex + 1) & parts(partIndex + 2)
                        parts = RemoveItem(parts, partIndex + 2)
                        operatorsUsed = RemoveItem(operatorsUsed, partIndex + 1)
                    End If

                    parts = RemoveItem(parts, partIndex)
                    operatorsUsed = RemoveItem(operatorsUsed, partIndex)
                Else
                    partIndex += 1
                End If
            Loop

            'goes in order using BODMAS of each operator finding each calculation it is used in
            If UBound(parts) > 0 Then
                For Each operatorSymbol As String In operatorSymbols
                    Dim operatorIndex As Integer = 0
                    Do
                        If Not IsNothing(operatorsUsed) AndAlso operatorIndex <= UBound(operatorsUsed) AndAlso
                            operatorSymbol = operatorsUsed(operatorIndex) Then
                            Dim leftPart As String = parts(operatorIndex).Trim
                            Dim rightPart As String = parts(operatorIndex + 1).Trim
                            Dim newPart As String

                            Dim reference As Object = game.FindReference(act, leftPart)
                            If Not IsNothing(reference) Then
                                leftPart = reference
                            End If
                            reference = game.FindReference(act, rightPart)
                            If Not IsNothing(reference) Then
                                rightPart = reference
                            End If

                            If IsNumeric(leftPart) AndAlso IsNumeric(rightPart) Then
                                Dim leftVal As Single = Val(leftPart)
                                Dim rightVal As Single = Val(rightPart)

                                Select Case operatorSymbol      'TODO: replace references with strings
                                    Case "^"
                                        newPart = leftVal ^ rightVal
                                    Case "/"
                                        newPart = leftVal / rightVal
                                    Case "*"
                                        newPart = leftVal * rightVal
                                    Case "+"
                                        newPart = leftVal + rightVal
                                    Case "-"
                                        newPart = leftVal - rightVal
                                    Case Else
                                        newPart = leftVal & operatorSymbol & rightVal
                                End Select

                                parts(operatorIndex) = newPart
                                parts = RemoveItem(parts, operatorIndex + 1)
                                operatorsUsed = RemoveItem(operatorsUsed, operatorIndex)
                            Else
                                operatorIndex += 1
                            End If
                        Else
                            operatorIndex += 1
                        End If
                    Loop Until IsNothing(operatorsUsed) OrElse operatorIndex >= UBound(parts)
                Next
            Else
                Dim reference As Object = game.FindReference(act, parts(0))
                If Not IsNothing(reference) Then
                    parts(0) = ArrayToString(reference)
                End If
            End If

            Return parts(0)
        Else
            Return calc
        End If
    End Function

#End Region

#Region "Conditions"

    Private Enum LogicOp As Integer
        'logical operators

        AndOp
        OrOp
    End Enum

    Private Enum CompareOp As Integer
        'comparison operators

        EqualGreaterThan
        EqualLessThan
        Equal
        NotEqual
        GreaterThan
        LessThan
    End Enum


    Private Function AssessCondition(condition As String, Optional ByRef act As Actor = Nothing,
                                        Optional ByRef game As FrmGameExecutor = Nothing) As Boolean
        'takes a condition in the form of a string and returns true or false

        If Not IsNothing(condition) AndAlso Len(condition) > 0 Then
            condition = LCase(condition)    'conditions are not case sensitive

            Dim comparisonOperators() As String = {">=", "<=", "=", "<>", ">", "<"} _
            'ordered so there are no conflicts (eg >= goes before > and =)
            Dim logicOperators() As String = {" and ", " or "}
            Const notOperator As String = "not "

            Dim comparisons() As String = {condition}      'list of individual comparisons split by logic operators
            Dim comparisonTypes() As LogicOp = {}      'list of the order of comparisons used

            'finds individual comparisons
            For opIndex As Integer = 0 To UBound(logicOperators)
                Dim comIndex As Integer = 0
                Do While comIndex <= UBound(comparisons)
                    Dim splits() As String = JsonSplit(comparisons(comIndex), 0, logicOperators(opIndex))

                    'checks if there was any split made
                    If UBound(splits) > 0 Then
                        'removes the overall comparison, then inserts the split version
                        comparisons = RemoveItem(comparisons, comIndex)
                        For splitIndex As Integer = 0 To UBound(splits)
                            'adds the comparison type used if it is in the middle of 2 splits
                            If splitIndex < UBound(splits) Then
                                comparisonTypes = InsertItem(comparisonTypes, opIndex, comIndex)
                            End If

                            comparisons = InsertItem(comparisons, splits(splitIndex), comIndex)
                            comIndex += 1
                        Next
                    Else
                        'if not split made then moves onto the next comparison
                        comIndex += 1
                    End If
                Loop
            Next

            'evaluates each comparison to a boolean
            For comIndex As Integer = 0 To UBound(comparisons)
                Dim leftPart As String = Nothing
                Dim rightPart As String = Nothing

                'finds which (if any) comparison operator is used
                Dim opIndex As Integer = 0
                Do While opIndex <= UBound(comparisonOperators)
                    Dim splits() As String = JsonSplit(comparisons(comIndex), 0, comparisonOperators(opIndex))

                    'checks if there are multiple parts when operator is used to split
                    If UBound(splits) = 1 Then
                        leftPart = Trim(splits(0))
                        rightPart = Trim(splits(1))

                        Exit Do
                    ElseIf UBound(splits) > 1 Then
                        'multiple comparisons used without a logic operators
                        DisplayError("A condition used multiple comparisons without a logic operator" & vbCrLf & condition)
                    Else
                        opIndex += 1
                    End If
                Loop

                'opIndex is set to -1 if no operator found
                If opIndex > UBound(comparisonOperators) Then
                    opIndex = -1
                    leftPart = comparisons(comIndex)
                Else
                    'processes the left and right parts
                    leftPart = InterpretValue(leftPart, True, act, game)
                    rightPart = InterpretValue(rightPart, True, act, game)
                End If

                Dim comResult As Boolean = False    'stores the result of this comparison
                'compares left and right part using the chosen operator
                If IsNumeric(leftPart) And IsNumeric(rightPart) Then
                    'numeric comparisons
                    Dim leftVal As Single = Val(leftPart)
                    Dim rightVal As Single = Val(rightPart)

                    Select Case opIndex
                        Case CompareOp.EqualGreaterThan
                            comResult = leftVal >= rightVal
                        Case CompareOp.EqualLessThan
                            comResult = leftVal <= rightVal
                        Case CompareOp.Equal
                            comResult = leftVal = rightVal
                        Case CompareOp.NotEqual
                            comResult = leftVal <> rightVal
                        Case CompareOp.GreaterThan
                            comResult = leftVal > rightVal
                        Case CompareOp.LessThan
                            comResult = leftVal < rightVal
                    End Select
                Else
                    'non numeric comparisons (eg strings)
                    'only equal or not equal operators available
                    Select Case opIndex
                        Case CompareOp.Equal
                            comResult = leftPart = rightPart
                        Case CompareOp.NotEqual
                            comResult = leftPart <> rightPart
                        Case Else
                            DisplayError("Tried to compare non numeric values" & vbCrLf & "Condition: " & comparisons(comIndex))
                    End Select
                End If

                'checks for a not operator
                If Left(leftPart, Len(notOperator)) = notOperator Then
                    comResult = Not comResult
                End If

                comparisons(comIndex) = comResult.ToString
            Next

            'combines the comparisons into a single boolean using logic operators
            Dim overall As Boolean = comparisons(0)
            If Not IsNothing(comparisonTypes) Then
                For index As Integer = 0 To UBound(comparisonTypes)
                    Select Case comparisonTypes(index)
                        Case LogicOp.AndOp
                            overall = overall And comparisons(index + 1)
                        Case LogicOp.OrOp
                            overall = overall Or comparisons(index + 1)
                    End Select
                Next
            End If

            Return overall
        End If

        'defaults to false
        Return False
    End Function

#End Region

#Region "Scoreboard"

    Private Sub SaveScore(points As Integer, gameName As String)
        'asks the user for initials and saves their score to the Score table
        'uses a MySQL server running from XAMPP

        'gets initials from the user
        Dim initials As String = ""
        Do Until Len(initials) = 3
            initials = InputBox("Enter initials for " & gameName & " scoreboard" & vbCrLf _
            & "Score: " & points & vbCrLf _
            & "Press cancel if you don't want your score saved",
            "Save Score")

            If Len(initials) = 0 Then
                'doesn't insert score if user presses cancel
                Exit Sub
            ElseIf Len(initials) <> 3 Then
                DisplayError("Initials must be 3 characters long")
            End If
        Loop

        initials = UCase(initials)

        Try
            'creates and opens connection to database
            Using conn As New MySqlConnection(Settings.ProjectScoresConnectionString)
                conn.Open()

                'sql query
                Const query As String = "INSERT INTO Score (initials, points, gameName) VALUES (@initials, @points, @gameName);"

                'uses preparing to prevent SQL injection attacks (injection possible for game name and possibly initials)
                Using cmd As New MySqlCommand(query, conn)
                    'prepares query
                    cmd.Parameters.Add("@initials", MySqlDbType.String, 3)
                    cmd.Parameters.Add("@points", MySqlDbType.Int16)
                    cmd.Parameters.Add("@gameName", MySqlDbType.String, 64)
                    cmd.Prepare()

                    'binds values to parameters and executes with values
                    cmd.Parameters(0).Value = initials
                    cmd.Parameters(1).Value = points
                    cmd.Parameters(2).Value = gameName
                    cmd.ExecuteNonQuery()
                End Using

                'closes connection
                conn.Close()

                MsgBox("Successfully added your score to " & gameName & "'s scoreboard")
            End Using
        Catch ex As Exception
            DisplayError("An error has occured so your score couldn't be saved" & vbCrLf & ex.ToString())
        End Try
    End Sub

#End Region

End Module