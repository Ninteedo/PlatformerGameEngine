Imports MySql.Data.MySqlClient
Imports PlatformerGameEngine.My

Public Class FrmScoreboard

#Region "Constructors"

    Public Sub New(gameName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _gameName = gameName
        GetScores()
    End Sub

#End Region

#Region "Query"

    ReadOnly _gameName As String

    Private Sub GetScores()
        'displays either top 10 scores or 10 scores around the search initials to the user

        Try
            Dim conn As New MySqlConnection(Settings.ProjectScoresConnectionString)
            conn.Open()

            Dim query As String =
                    "SELECT initials AS 'Player', MAX(points) AS 'Score'
                    FROM Score
                    WHERE gameName = @gameName
                    GROUP BY initials                    
                    ORDER BY MAX(points) DESC;"

            'using preparing to prevent SQL injection attacks (injection possible for game name and possibly initials)
            Using cmd As New MySqlCommand(query, conn)
                'prepares query
                cmd.Parameters.Add("@gameName", MySqlDbType.String, 64)
                cmd.Prepare()

                'binds values to parameters
                cmd.Parameters(0).Value = _gameName
                cmd.ExecuteNonQuery()

                'executes and stores result in reader
                Using reader As MySqlDataReader = cmd.ExecuteReader()

                    If reader.HasRows Then
                        DisplayScores(reader)
                    Else
                        DisplayError("This game has no scores associated with it")
                    End If
                End Using
            End Using

            conn.Close()
        Catch ex As Exception
            DisplayError("An Error has occured so score couldn't be displayed" & vbCrLf & ex.ToString())
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
            'newItem.SubItems.Add(place)
            'newItem.SubItems.Add(scores("Player"))
            'newItem.SubItems.Add(scores("Score"))

            LstScoreboard.Items.Add(newItem)
        Loop

    End Sub

#End Region

#Region "Controls"

    Private Sub BtnClose_Click(sender As Button, e As EventArgs) Handles BtnClose.Click
        Close()
    End Sub

#End Region

End Class