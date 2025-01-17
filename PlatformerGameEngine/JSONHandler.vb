﻿Public Module JsonHandler

    'uses standards shown at https://www.json.org/
    'may not be identical though

    'these methods were largely designed with a slightly older version of tags in mind
    'the strings in these older tags could use escape characters to display things like quotes
    'this was removed from the design but much of the code is still here...
    'because I didn't have long enough to re-implement this section
    'lots of the code here was for handling escaped quote marks but are in practice irrelevant in this version

#Region "JSON-Tag Conversions"

    Public Function TagToJson(inputTag As Tag) As String
        Dim result As String = ""
        If Not IsNothing(inputTag.Name) Then
            result = "{" & AddQuotes(inputTag.Name) &
                If(Not IsNothing(inputTag.Argument) AndAlso Len(inputTag.Argument) > 0, ":" & inputTag.Argument & "}", "}")
        End If
        Return result
    End Function

    Public Function JsonToTag(json As String) As Tag
        'converts a JSON string into a tag

        If Not IsNothing(json) AndAlso Len(json) > 0 Then
            Dim cIndex As Integer           'current character index of json string
            Dim name As String = ""
            Dim argument As String = ""
            Dim inString As Boolean         'is the current character in a string (includes quotation marks)
            Dim stringEscaped As Boolean _
            'has the string been 'escaped' (escaped by \, used for special cases such as \n for new line or \" for a quotation mark)
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
        Else
            Return Nothing
        End If
    End Function

#End Region

#Region "Value/String Interpreting"

    Public Function InterpretValue(valueString As String, Optional fullInterpret As Boolean = False, Optional act As Actor = Nothing, Optional ByRef game As FrmGameExecutor = Nothing) As Object
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
                Case Else
                    Dim firstChar As String = Left(valueString, 1)
                    Dim lastChar As String = Right(valueString, 1)

                    If firstChar = """" And lastChar = """" Then    'string
                        result = RemoveQuotes(valueString)
                    ElseIf firstChar = "{" And lastChar = "}" Then  'tag
                        Dim resultTag As Tag = New Tag(valueString)

                        If fullInterpret Then
                            resultTag.SetArgument(InterpretValue(resultTag.Argument, fullInterpret, act, game))
                        End If

                        result = resultTag
                    ElseIf firstChar = "[" And lastChar = "]" Then  'array
                        Dim valueSplits As String() = JsonSplit(valueString)
                        ReDim result(UBound(valueSplits))
                        For index As Integer = 0 To UBound(valueSplits)
                            result(index) = InterpretValue(valueSplits(index).Trim, fullInterpret, act, game)
                        Next
                    ElseIf IsNumeric(valueString) Then
                        result = Val(valueString)
                    ElseIf fullInterpret Then
                        result = ProcessCalculation(valueString, act, game)
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

    Public Function ArrayToString(input As Object) As String
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

    Private Function HasQuotes(input As String) As Boolean
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

    Public Function JsonSplit(input As String, Optional subStructureLevelRequired As Integer = 0,
                              Optional delimiter As String = ",") As String()
        'splits a JSON string into its tags

        If Len(input) > 1 Then
            Dim result() As String = {}
            If input(0) = "[" And input(Len(input) - 1) = "]" Or input(0) = "{" And input(Len(input) - 1) = "}" Then
                input = Mid(input, 2, Len(input) - 2)   'removes outermost {} or []
            End If
            Dim inString As Boolean = False
            Dim subStructureLevel As Integer = 0
            Dim delimiterProgress As Integer = 0        'used for tracking multi character delimiters

            For cIndex As Integer = 0 To Len(input) - 1
                Dim c As Char = input(cIndex)

                If UBound(result) < 0 Then  'only triggers on first character
                    result = {""}
                End If

                result(UBound(result)) += c

                If inString And c = """" AndAlso input(cIndex - 1) <> "\" Then
                    inString = False
                ElseIf Not inString And c = """" Then
                    inString = True
                ElseIf Not inString And (c = "{" Or c = "[") Then
                    subStructureLevel += 1
                ElseIf Not inString And (c = "}" Or c = "]") Then
                    subStructureLevel -= 1
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
                Else
                    result += c
                End If
            Else
                If Not escaped Then
                    If c = "\" Then
                        escaped = True
                    ElseIf c = """" Then
                        inString = False
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
