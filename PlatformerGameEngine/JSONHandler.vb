'Richard Holmes
'06/09/2019
'Procedures for converting between tags and JSON

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module JSONHandler

    'uses standards shown at https://www.json.org/
    'may not be identical though

#Region "JSON-Tag Conversions"

    Public Function TagToJSON(tag As PRE2.Tag) As String
        Dim result As String = ""
        If Not IsNothing(tag.name) Then
            result = "{" & AddQuotes(tag.name) &
                If(Not IsNothing(tag.argument) AndAlso Len(tag.argument) > 0, ":" & tag.argument & "}", "}")
        End If
        Return result
    End Function

    Public Function JSONToTag(jsonString As String) As PRE2.Tag
        'converts a JSON string into a tag
        'TODO: input validation and error handling

        'Dim splits() As String = JSONSplit(jsonString, 0)
        'Dim resultName As String = RemoveQuotes(Mid(splits(0), 1, splits(0).IndexOf(":")))
        'Dim currentValue As String = Mid(splits(0), splits(0).IndexOf(":"))

        Dim cIndex As Integer = 0
        Dim inString As Boolean = False
        Dim currentString As String = ""

        Dim resultName As String = ""

        Dim inValue As Boolean = False
        Dim currentValue As String = ""

        Dim subStructureLevel As Integer = 0    'tracks how many tags have been opened

        Do
            Dim c As String = jsonString(cIndex)

            If Not inValue Then
                If Not inString AndAlso c = ":" Or c = "}" Then 'marks end of name and beginning of value
                    'resultName = InterpretString(currentString).Trim
                    resultName = currentString.Trim
                    currentString = ""

                    inValue = True
                End If
            Else
                currentValue += c
                If Not inString And c = "}" Then        'end of value
                    subStructureLevel -= 1

                    If subStructureLevel < 0 Then
                        currentValue = currentValue.Remove(Len(currentValue) - 1)
                        Exit Do
                    End If
                ElseIf Not inString And c = "{" Then     'sub tag opened
                    subStructureLevel += 1
                End If
            End If

            If Not inString And c = """" Then
                inString = True
            ElseIf inString AndAlso c = """" AndAlso jsonString(cIndex - 1) <> "\" Then
                inString = False
            ElseIf inString Then
                currentString += c
            End If

            cIndex += 1
        Loop Until cIndex >= Len(jsonString)

        If Len(currentValue) = 0 Then
            currentValue = Nothing
        End If

        Return New PRE2.Tag(resultName, currentValue)
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

    Public Function InterpretValue(valueString As String) As Object
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
                            result = JSONToTag(valueString)
                        Case "["        'array
                            Dim valueStrings() As String = JSONSplit(valueString, 0)   '{""}
                            Dim values() As Object
                            ReDim values(UBound(valueStrings))
                            For index As Integer = 0 To UBound(valueStrings)
                                values(index) = InterpretValue(valueStrings(index).Trim)  'Mid(valueStrings(index), 2, Len(valueStrings(index)) - 2).Trim)
                            Next

                            result = values
                        Case Else
                            If IsNumeric(valueString) Then
                                result = Val(valueString)
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
        'turns an array into a string

        Dim result As String = Nothing

        If IsArray(input) Then
            result = "["

            If Not IsNothing(input) Then
                For index As Integer = 0 To UBound(input)
                    If IsArray(input(index)) Then
                        result += ArrayToString(input(index))        'recursive
                    Else
                        result += input(index).ToString
                    End If

                    If index < UBound(input) Then
                        result += ","
                    End If
                Next
            End If
            result += "]"
        Else
            If Not IsNothing(input) Then
                result = input.ToString
            End If
        End If

        Return result
    End Function

    Public Function HasQuotes(input As String) As Boolean
        If Not IsNothing(input) AndAlso Len(input) > 1 Then
            Return Mid(input, 1, 1) = """" And Mid(input, Len(input) - 1, 1) = """"
        Else
            Return False
        End If
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

    Public Function JSONSplit(input As String, subStructureLevelRequired As Integer) As String()
        'splits a JSON string into its tags

        Dim result() As String = {""}
        input = Mid(input, 2, Len(input) - 2)   'removes outermost {} or []
        Dim inString As Boolean = False
        Dim subStructureLevel As Integer = 0

        For cIndex As Integer = 0 To Len(input) - 1
            Dim c As String = input(cIndex)

            If Not inString And subStructureLevel = subStructureLevelRequired And c = "," Then  'only splits when it is at the required sub-structure level
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
    End Function
#End Region
End Module
