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

        For cIndex = 0 To Len(json) - 1
            Dim c As String = json(cIndex)      'current character
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
                    inArgument = True
                    addChar = False
                Else
                    If c = """" Then
                        inString = True
                    Else
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

    Public Function InterpretValue(valueString As String, Optional fullInterpret As Boolean = False, Optional ent As Actor = Nothing, Optional room As Room = Nothing) As Object
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
                    Select Case valueString(0)
                        Case """"       'string
                            'result = InterpretString(valueString)
                            result = valueString
                        Case "{"        'object (another tag)
                            result = JsonToTag(valueString)
                            If fullInterpret Then
                                result.SetArgument(InterpretValue(result.argument, True, ent, room))
                            End If
                        Case "["        'array
                            Dim valueStrings() As String = JSONSplit(valueString, 0)   '{""}
                            Dim values() As Object
                            ReDim values(UBound(valueStrings))
                            For index As Integer = 0 To UBound(valueStrings)
                                values(index) = InterpretValue(valueStrings(index).Trim, fullInterpret, ent, room)  'Mid(valueStrings(index), 2, Len(valueStrings(index)) - 2).Trim)

                                'If fullInterpret Then
                                '    values(index) = InterpretValue()
                                'End If
                            Next

                            result = values
                        Case Else
                            If IsNumeric(valueString) Then
                                result = Val(valueString)
                            ElseIf fullInterpret Then
                                result = ProcessCalculation(valueString, ent, room)
                            End If
                    End Select
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

    Public Function ArrayToString(input As Object) As String
        'turns an array into a string, can take jagged arrays cant take multidimensional arrays

        Dim result As String = Nothing

        If Not IsArray(input) Then
            If Not IsNothing(input) Then
                result = input.ToString
            End If
        ElseIf input.Rank > 1 Then
            PRE2.DisplayError("Cannot turn a multidimensional array into a string")
        Else
            result = "["
            If UBound(input) = 0 Then
                If Not IsNothing(input(0)) Then
                    result = input(0).ToString
                End If
            Else
                For index As Integer = 0 To UBound(input)
                    result += ArrayToString(input(index))        'recursive

                    If index < UBound(input) Then
                        result += ","
                    End If
                Next
            End If
            result += "]"
        End If

        Return result
    End Function

    Public Function HasQuotes(input As String) As Boolean
        Return Not IsNothing(input) AndAlso Len(input) > 1 AndAlso (Mid(input, 1, 1) = """" And Mid(input, Len(input), 1) = """")
    End Function

    Public Function AddQuotes(initial As String, Optional ignoreAlreadyQuoted As Boolean = False) As String
        If ignoreAlreadyQuoted OrElse Not HasQuotes(initial) Then
            Return """" & initial & """"
        Else
            Return initial
        End If
    End Function

    Public Function RemoveQuotes(initial As String) As String
        If HasQuotes(initial) Then
            Return Mid(initial, 2, Len(initial) - 2)
        Else
            Return initial
        End If
    End Function

    Public Function JSONSplit(input As String, subStructureLevelRequired As Integer, Optional delimiter As Char = ",") As String()
        'splits a JSON string into its tags

        If Len(input) > 1 Then
            Dim result() As String = {""}
            If input(0) = "[" And input(Len(input) - 1) = "]" Or input(0) = "{" And input(Len(input) - 1) = "}" Then
                input = Mid(input, 2, Len(input) - 2)   'removes outermost {} or []
            End If
            Dim inString As Boolean = False
                Dim subStructureLevel As Integer = 0

                For cIndex As Integer = 0 To Len(input) - 1
                    Dim c As String = input(cIndex)

                    If Not inString And subStructureLevel = subStructureLevelRequired And c = delimiter Then  'only splits when it is at the required sub-structure level
                        ReDim Preserve result(UBound(result) + 1)
                        result(UBound(result)) = ""
                    Else
                        result(UBound(result)) += c

                        If inString And c = """" AndAlso input(cIndex - 1) <> "\" Then
                            inString = False
                        ElseIf Not inString And c = """" Then
                            inString = True
                        ElseIf Not inString And c = "{" Then
                            subStructureLevel += 1
                        ElseIf Not inString And c = "}" Then
                            subStructureLevel -= 1
                            'ElseIf subStructureLevel = subStructureLevelRequired And c = "[" Or c = "]" Then
                            '    result(UBound(result)) = result(UBound(result)).Remove(Len(result(UBound(result))) - 1, 1)
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
