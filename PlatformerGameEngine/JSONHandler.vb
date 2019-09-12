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

        Dim resultName As String = ""

        Dim cIndex As Integer = 0
        Dim inString As Boolean = False
        Dim currentString As String = ""

        Dim inValue As Boolean = False
        Dim currentValue As String = ""

        Dim subStructureLevel As Integer = 0    'tracks how many tags have been opened

        Do
            Dim c As String = jsonString(cIndex)

            If Not inValue Then
                If Not inString AndAlso c = ":" Or c = "}" Then 'marks end of name and beginning of value
                    resultName = InterpretString(currentString).Trim
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

        Return result
    End Function

    Public Function InterpretValue(valueString As String) As Object
        valueString = valueString.Trim
        Select Case LCase(valueString)
            'specific values for boolean/nothing
            Case "true"
                Return True
            Case "false"
                Return False
            Case "null"
                Return Nothing
            Case "nill"
                Return Nothing
            Case Else
                Select Case valueString(0)
                    Case """"       'string
                        Return InterpretString(valueString)
                    Case "{"        'object (another tag)
                        Return JSONToTag(valueString)
                    Case "["        'array
                        Dim valueStrings() As String = {""}
                        Dim values() As Object

                        Dim inString As Boolean = False
                        Dim subStrucureLevel As Integer = 0        'eg string, array or tag

                        'splits by "," but only when the comma isn't in a string
                        For cIndex As Integer = 0 To Len(valueString) - 1
                            Dim c As String = valueString(cIndex)

                            If Not inString AndAlso c = """" Or c = "[" Or c = "{" Then
                                subStrucureLevel += 1
                                If c = """" Then
                                    inString = True
                                End If
                                If subStrucureLevel > 1 Then
                                    valueStrings(UBound(valueStrings)) += c
                                End If
                            ElseIf Not inString AndAlso c = "]" Or c = "}" Then
                                subStrucureLevel -= 1
                                If subStrucureLevel > 0 Then
                                    valueStrings(UBound(valueStrings)) += c
                                End If
                            ElseIf inString And c = """" And valueString(cIndex - 1) <> "\" Then
                                inString = False
                                subStrucureLevel -= 1
                                valueStrings(UBound(valueStrings)) += c
                            ElseIf subStrucureLevel = 1 And c = "," Then
                                ReDim Preserve valueStrings(UBound(valueStrings) + 1)
                                valueStrings(UBound(valueStrings)) = ""
                            Else
                                valueStrings(UBound(valueStrings)) += c
                            End If

                        Next

                        ReDim values(UBound(valueStrings))
                        For index As Integer = 0 To UBound(valueStrings)
                            values(index) = InterpretValue(valueStrings(index).Trim)
                        Next

                        Return values
                    Case Else
                        If IsNumeric(valueString) Then
                            Return Val(valueString)
                        End If
                End Select
        End Select

        Return valueString
    End Function
#End Region

#Region "Misc String Handling"

    Public Function ArrayToString(input As Object) As String
        'turns an array into a string

        Dim result As String
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
            result = input.ToString
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
#End Region
End Module
