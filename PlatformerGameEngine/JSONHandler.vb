'Richard Holmes
'06/09/2019
'Procedures for converting between tags and JSON

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module JSONHandler

    'uses standards shown at https://www.json.org/
    'may not be identical though

#Region "JSON-Tag Conversions"

    Public Function TagToJSON(tag As Tag) As String
        Dim result As String = ""
        If Not IsNothing(tag.name) Then
            result = "{" & AddQuotes(tag.name) &
                If(Not IsNothing(tag.argument) AndAlso Len(tag.argument) > 0, ":" & tag.argument & "}", "}")
        End If
        Return result
    End Function

    'Public Function JSONToTag(jsonString As String) As Tag
    '    'converts a JSON string into a tag
    '    'TODO: input validation and error handling

    '    'Dim splits() As String = JSONSplit(jsonString, 0)
    '    'Dim resultName As String = RemoveQuotes(Mid(splits(0), 1, splits(0).IndexOf(":")))
    '    'Dim currentValue As String = Mid(splits(0), splits(0).IndexOf(":"))

    '    Dim cIndex As Integer = 0
    '    Dim inString As Boolean = False
    '    Dim currentString As String = ""

    '    Dim resultName As String = ""

    '    Dim inValue As Boolean = False
    '    Dim currentValue As String = ""

    '    Dim subStructureLevel As Integer = 0    'tracks how many tags have been opened

    '    If Not IsNothing(jsonString) Then
    '        Do
    '            Dim c As String = jsonString(cIndex)

    '            If Not inValue Then
    '                If Not inString AndAlso c = ":" Or c = "}" Then 'marks end of name and beginning of value
    '                    'resultName = InterpretString(currentString).Trim
    '                    resultName = currentString.Trim
    '                    currentString = ""

    '                    inValue = True
    '                End If
    '            Else
    '                currentValue += c
    '                If Not inString And c = "}" Then        'end of value
    '                    subStructureLevel -= 1

    '                    If subStructureLevel < 0 Then
    '                        currentValue = currentValue.Remove(Len(currentValue) - 1)
    '                        Exit Do
    '                    End If
    '                ElseIf Not inString And c = "{" Then     'sub tag opened
    '                    subStructureLevel += 1
    '                End If
    '            End If

    '            If Not inString And c = """" Then
    '                inString = True
    '            ElseIf inString AndAlso c = """" AndAlso jsonString(cIndex - 1) <> "\" Then
    '                inString = False
    '            ElseIf inString Then
    '                currentString += c
    '            End If

    '            cIndex += 1
    '        Loop Until cIndex >= Len(jsonString)

    '        If Len(currentValue) = 0 Then
    '            currentValue = Nothing
    '        End If
    '    End If

    '    Return New Tag(resultName, currentValue)
    'End Function

    Public Function JsonToTag(json As String) As Tag
        'converts a JSON string into a tag

        Dim cIndex As Integer           'current character index of json string
        Dim name As String = ""
        Dim argument As String = ""
        Dim inString As Boolean         'is the current character in a string (includes quotation marks)
        Dim stringEscaped As Boolean    'has the string been 'escaped' (escaped by \, _
        '                               used for special cases such as \n for new line or \" for a quotation mark)
        Dim inArgument As Boolean       'false: currently in name, true: currently in argument

        json = Trim(json)       'removes any leading or trailing whitespace
        json = Mid(json, 2, Len(json) - 2)      'removes beginning and ending braces

        For cIndex = 0 To Len(json) - 1
            Dim c As String = Mid(json, cIndex + 1, 1)      'current character
            Dim addChar As Boolean = True       'if true then c is added to name/argument

            If inString Then
                If stringEscaped Then
                    stringEscaped = False
                Else
                    If c = "\" Then
                        stringEscaped = True
                    Else
                        If c = """" Then        '4 quotes represents a quotation mark
                            inString = False
                        End If
                    End If
                End If
            Else
                If c = ":" Then
                    If Not inArgument Then
                        inArgument = True
                        addChar = False
                    End If
                Else
                    If c = """" Then
                        inString = True
                    ElseIf Not inArgument Then
                        addChar = False
                    End If
                End If
            End If

            If addChar Then
                If inArgument Then
                    argument += c
                Else
                    name += c
                End If
            End If
        Next cIndex

        Return New Tag(RemoveQuotes(name), RemoveQuotes(argument))
    End Function

#End Region

#Region "Value/String Interpreting"
    Public Function InterpretString(rawString As String) As String
        'handles breaked characters such as \n

        Dim result As String = ""

        If Not IsNothing(rawString) AndAlso Len(rawString) > 0 Then
            Dim cIndex As Integer = 0

            Do
                Dim c As String = rawString(cIndex)

                If c = "\" Then     'used for special things such as \n for new line
                    cIndex += 1
                    c = rawString(cIndex)

                    Select Case c
                        Case "\"
                            result += "\"
                        Case """"
                            result += """"
                        Case "n"
                            result += vbCrLf
                        Case "t"
                            result += vbTab
                    End Select
                    'ElseIf c = """" Then
                    '    Return result
                ElseIf c <> """" Then   'ignores quotes, may cause issues
                    result += c
                End If

                cIndex += 1
            Loop Until cIndex > Len(rawString) - 1
        End If

        Return result
    End Function

    Public Function InterpretValue(valueString As String, Optional fullInterpret As Boolean = False, Optional act As Actor = Nothing, Optional room As Room = Nothing) As Object
        'interprets a value from JSON
        'full interpret means that for tags and arrays the arguments are also interpreted

        Dim result As Object = Nothing

        If Not IsNothing(valueString) AndAlso Len(valueString.Trim) > 0 Then
            valueString = valueString.Trim
            Select Case LCase(valueString)
                'specific values for boolean/nothing
                Case "true"
                    result = True
                Case "false"
                    result = False
                Case "null"
                    result = Nothing
                Case "nill"
                    result = Nothing
                Case Else
                    Dim firstChar As String = Left(valueString, 1)
                    Dim lastChar As String = Right(valueString, 1)

                    If firstChar = """" And lastChar = """" Then    'string
                        result = RemoveQuotes(valueString)
                    ElseIf firstChar = "{" And lastChar = "}" Then  'tag
                        Dim resultTag = New Tag(valueString)

                        If fullInterpret Then
                            resultTag.SetArgument(InterpretValue(resultTag.argument, fullInterpret, act, room))
                        End If

                        result = resultTag
                    ElseIf firstChar = "[" And lastChar = "]" Then  'array
                        Dim valueSplits As String() = JSONSplit(valueString)
                        ReDim result(UBound(valueSplits))
                        For index As Integer = 0 To UBound(valueSplits)
                            result(index) = InterpretValue(valueSplits(index).Trim, fullInterpret, act, room)
                        Next
                    ElseIf IsNumeric(valueString) Then
                        result = Val(valueString)
                    ElseIf fullInterpret Then
                        result = ProcessCalculation(valueString, act, room)
                    End If
            End Select
        End If

        If Not IsNothing(result) Then
            Return result
        Else
            Return valueString
        End If
    End Function
#End Region

#Region "Misc String Handling"

    Public Function ArrayToString(ByVal input As Object) As String
        'turns an array into a string e.g. "[63,-34,87]"

        Dim result As String = Nothing

        If IsNothing(input) Then
            Return result
        End If

        If Not IsArray(input) Then
            If Not IsNothing(input) Then
                result = input.ToString
            End If
        Else
            'support for multidimensional arrays, only up to 2D
            Select Case input.Rank
                Case 1
                    result = "["
                    For index As Integer = 0 To UBound(input)
                        result += input(index).ToString

                        If index < UBound(input) Then
                            result += ","
                        End If
                    Next
                    result += "]"
                Case 2
                    result += "["
                    For index1 As Integer = 0 To input.GetUpperBound(0)
                        result += "["
                        For index2 As Integer = 0 To input.GetUpperBound(1)
                            result += input(index1, index2).ToString

                            If index2 < input.GetUpperBound(1) Then
                                result += ","
                            End If
                        Next
                        result += "]"

                        If index1 < input.GetUpperBound(0) Then
                            result += ","
                        End If
                    Next
                    result += "]"
                Case Else
                    DisplayError("Cannot turn a multidimensional array with more than 2 dimensions into a string")
            End Select
        End If

        Return result
    End Function

    Public Function HasQuotes(ByVal input As String) As Boolean
        Return Not IsNothing(input) AndAlso Len(input) > 1 AndAlso (Mid(input, 1, 1) = """" And Mid(input, Len(input), 1) = """")
    End Function

    Public Function AddQuotes(ByVal initial As String, Optional ByVal ignoreAlreadyQuoted As Boolean = False) As String
        If ignoreAlreadyQuoted OrElse Not HasQuotes(initial) Then
            Return """" & initial & """"
        Else
            Return initial
        End If
    End Function

    Public Function RemoveQuotes(ByVal initial As String) As String
        If HasQuotes(initial) Then
            Return Mid(initial, 2, Len(initial) - 2)
        Else
            Return initial
        End If
    End Function

    Public Function JSONSplit(ByVal input As String, Optional ByVal subStructureLevelRequired As Integer = 0, Optional ByVal delimiter As String = ",") As String()
        'splits a JSON string into its tags

        If Len(input) > 1 Then
            Dim result() As String = {""}
            If input(0) = "[" And input(Len(input) - 1) = "]" Or input(0) = "{" And input(Len(input) - 1) = "}" Then
                input = Mid(input, 2, Len(input) - 2)   'removes outermost {} or []
            End If
            Dim inString As Boolean = False
            Dim subStructureLevel As Integer = 0
            Dim delimiterProgress As Integer = 0        'used for tracking multicharacter delimiters

            For cIndex As Integer = 0 To Len(input) - 1
                Dim c As String = input(cIndex)

                result(UBound(result)) += c

                If inString And c = """" AndAlso input(cIndex - 1) <> "\" Then
                    inString = False
                ElseIf Not inString And c = """" Then
                    inString = True
                ElseIf Not inString And {"{", "["}.Contains(c) Then
                    subStructureLevel += 1
                ElseIf Not inString And {"}", "]"}.Contains(c) Then
                    subStructureLevel -= 1
                    'ElseIf subStructureLevel = subStructureLevelRequired And c = "[" Or c = "]" Then
                    '    result(UBound(result)) = result(UBound(result)).Remove(Len(result(UBound(result))) - 1, 1)
                End If

                'only splits when not in a string and at the required substructure level
                If Not inString And subStructureLevel = subStructureLevelRequired Then
                    'checks if current char makes any delimiter progress
                    If c = delimiter(delimiterProgress) Then
                        delimiterProgress += 1

                        'checks if delimiter has been found
                        If delimiterProgress = Len(delimiter) Then
                            'removes the delimiter from the result
                            Dim lastElement As String = result(UBound(result))
                            result(UBound(result)) = Left(lastElement, Len(lastElement) - Len(delimiter))

                            result = InsertItem(result, "")
                            delimiterProgress = 0
                        End If
                    Else
                        'resets delimiter progress as does not match delimiter
                        delimiterProgress = 0
                    End If
                End If
            Next

            Return result
        Else
            Return {input}
        End If
    End Function

    Public Function RemoveSubStrings(jsonInput As String) As String
        'returns the input with anything surrounding by quotation marks removed
        'eg {"exampleTag":[{"exampleSubTagA":1}]} to {:[{:1}]}

        Dim result As String = ""
        Dim escaped As Boolean = False      'escaped when a \ is used
        Dim inString As Boolean = False

        For Each c As String In jsonInput
            If Not inString Then
                If c = """" Then
                    inString = True
                End If
            Else
                If Not escaped Then
                    If c = "\" Then
                        escaped = True
                    ElseIf c = """" Then
                        inString = False
                    Else
                        result += c
                    End If
                Else
                    escaped = False
                End If
            End If
        Next

        Return result
    End Function

#End Region

End Module
