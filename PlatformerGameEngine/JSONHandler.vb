'Richard Holmes
'06/09/2019
'Procedures for converting between tags and JSON

Imports PRE2 = PlatformerGameEngine.PanelRenderEngine2

Public Module JSONHandler

    'uses standards shown at https://www.json.org/
    'may not be identical though

    Public Function TagToJSON(tag As PRE2.Tag) As String
        Dim result As String = ""
        If Not IsNothing(tag.name) Then
            result = "{" & tag.name & ":"   'initialiser of an object
            If Not IsNothing(tag.args) Then
                If UBound(tag.args) = 0 Then
                    'single value
                    result += tag.args(0).ToString
                Else
                    'array
                    result += "["
                    For argIndex As Integer = 0 To UBound(tag.args)     'adds each argument as a value
                        result += tag.args(argIndex).ToString
                        If argIndex < UBound(tag.args) Then
                            result += ","
                        End If
                    Next
                    result += "]"
                End If
            End If
            result += "}"
        End If
        Return result
    End Function

    Public Function JSONToTag(jsonString As String) As PRE2.Tag
        'converts a JSON string into a tag
        'TODO: input validation and error handling

        Dim resultName As String = ""
        Dim resultArgs() As Object = {}

        Dim cIndex As Integer = 0
        Dim inString As Boolean = False
        Dim currentString As String = ""

        Dim inValue As Boolean = False
        Dim currentValue As String = ""

        'Dim objectStartIndex As Integer = -1

        Do
            Dim c As String = jsonString(cIndex)

            If Not inValue Then
                'part for reading strings in the JSON correctly
                If Not inString Then
                    If c = """" Then
                        inString = True
                    End If
                Else
                    If c = """" And jsonString(cIndex - 1) <> "\" Then
                        inString = False
                    Else
                        currentString += c
                    End If
                End If

                If c = ":" Then     'marks end of name and beginning of value
                    resultName = InterpretString(currentString).Trim
                    currentString = ""

                    inValue = True
                End If
            Else
                If Not inString And c = "}" Then        'end of value
                    resultArgs = InterpretValue(currentValue)
                    Exit Do
                Else
                    currentValue += c
                End If
            End If


            cIndex += 1
        Loop Until cIndex >= Len(jsonString)

        Return New PRE2.Tag(resultName, resultArgs)
    End Function

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

    Public Function InterpretValue(valueString As String) As Object()
        valueString = valueString.Trim
        Select Case LCase(valueString)
            'specific values for boolean/nothing
            Case "true"
                Return {True}
            Case "false"
                Return {False}
            Case "null"
                Return {Nothing}
            Case "nill"
                Return {Nothing}
            Case Else
                Select Case valueString(0)
                    Case """"       'string
                        Return {InterpretString(valueString)}
                    Case "{"        'object (another tag)
                        Return {JSONToTag(valueString)}
                    Case "["        'array
                        Dim valueStrings() As String = {""""}
                        Dim values() As Object

                        Dim inString As Boolean = False
                        Dim cIndex As Integer = 0

                        'splits by "," but only when the comma isn't in a string
                        Do
                            Dim c As String = valueString(cIndex)
                            If Not inString Then
                                If c = """" Then
                                    inString = True
                                ElseIf c = "," Then
                                    ReDim Preserve valueStrings(UBound(valueStrings) + 1)
                                    valueStrings(UBound(valueStrings)) = """"
                                Else
                                    'valueStrings(UBound(valueStrings)) += c
                                End If
                            Else
                                If c = """" And valueString(cIndex - 1) <> "\" Then
                                    valueStrings(UBound(valueStrings)) += """"
                                    inString = False
                                Else
                                    valueStrings(UBound(valueStrings)) += c
                                End If
                            End If

                            cIndex += 1
                        Loop Until cIndex >= Len(valueString)

                        ReDim values(UBound(valueStrings))
                        For index As Integer = 0 To UBound(valueStrings)
                            values(index) = InterpretValue(valueStrings(index).Trim)
                        Next

                        Return values
                    Case Else
                        If Decimal.TryParse(valueString, Globalization.NumberStyles.Float) Then
                            Return {Decimal.Parse(valueString, Globalization.NumberStyles.Float)}
                        Else
                            PRE2.DisplayError("Couldn't evaluate: " & valueString)
                            Return {valueString}
                        End If
                End Select
        End Select
    End Function
End Module
