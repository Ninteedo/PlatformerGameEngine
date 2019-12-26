Imports MySql.Data.MySqlClient
Imports PlatformerGameEngine.My

Public Class FrmScoreboard

#Region "Constructors"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'gets the game name from the user

        Dim input As String = InputBox("Enter the name of the game for which the scoreboard will be shown", "Enter Game Name", "Game Name")

        'if cancel pressed then close the form
        If IsNothing(input) Then
            Close()
        Else
            GetScores(input)
        End If
    End Sub

    Public Sub New(gameName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        LblGameName.Text = gameName
        GetScores(gameName)
    End Sub

#End Region

#Region "Query"

    Private Sub GetScores(gameName As String)
        'displays either top 10 scores or 10 scores around the search initials to the user

        Try
            Using conn As New MySqlConnection(Settings.ProjectScoresConnectionString)
                conn.Open()

                Dim query As String =
                    "SELECT initials AS 'Player', MAX(points) AS 'Score'
                    FROM Score
                    WHERE gameName = @gameName
                    GROUP BY initials                    
                    ORDER BY MAX(points) DESC;"

                'uses preparing to prevent SQL injection attacks (injection possible for game name and possibly initials)
                Using cmd As New MySqlCommand(query, conn)
                    'prepares query
                    cmd.Parameters.Add("@gameName", MySqlDbType.String, 64)
                    cmd.Prepare()

                    'binds values to parameters
                    cmd.Parameters(0).Value = gameName
                    cmd.ExecuteNonQuery()

                    'executes and stores result in reader
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            DisplayScores(reader)
                        Else
                            DisplayError("This game has no scores associated with it")
                            Close()
                        End If
                    End Using
                End Using

                conn.Close()
            End Using
        Catch ex As Exception
            DisplayError("An Error has occured so score couldn't be displayed" & vbCrLf & ex.ToString())
            Close()
        End Try
    End Sub

#End Region

#Region "Scoreboard"

    Private Sub DisplayScores(scores As MySqlDataReader)
        'displays each score from the query result

        LstScoreboard.Items.Clear()

        Dim place As Integer = 0    'the index of where the score appears
        Do While scores.Read()
            place += 1
            Dim newItem As New ListViewItem({place, scores("Player"), scores("Score")})

            LstScoreboard.Items.Add(newItem)
        Loop

    End Sub

#End Region

#Region "Controls"

    Private Sub BtnClose_Click(sender As Button, e As EventArgs) Handles BtnClose.Click
        Close()
    End Sub

    Private Sub TxtFind_TextChanged(sender As TextBox, e As EventArgs) Handles TxtFind.TextChanged
        'finds the score with matching initials if 3 characters are entered

        Dim searchTerm As String = TxtFind.Text
        If Len(searchTerm) = 3 Then
            For index As Integer = 0 To LstScoreboard.Items.Count - 1
                'selects the item if the name matches
                Dim match As Boolean = LstScoreboard.Items(index).SubItems(1).Text = searchTerm
                LstScoreboard.Items(index).Selected = match

                'scrolls the list to the matching item
                If match Then
                    LstScoreboard.EnsureVisible(index)
                End If
            Next
        End If
    End Sub

#End Region

End Class