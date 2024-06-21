Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Inherits Form

    Private lblName As Label
    Private lblEmail As Label
    Private lblPhone As Label
    Private lblGithub As Label
    Private lblStopwatchTime As Label
    Private btnPrevious As Button
    Private btnNext As Button

    Private submissions As List(Of Submission)
    Private currentIndex As Integer

    Public Sub New()
        ' Initialize the form
        Me.Text = "View Submissions"
        Me.Size = New Size(400, 300)

        ' Initialize labels
        lblName = New Label() With {.Location = New Point(20, 20), .Width = 300}
        lblEmail = New Label() With {.Location = New Point(20, 60), .Width = 300}
        lblPhone = New Label() With {.Location = New Point(20, 100), .Width = 300}
        lblGithub = New Label() With {.Location = New Point(20, 140), .Width = 300}
        lblStopwatchTime = New Label() With {.Location = New Point(20, 180), .Width = 300}

        ' Initialize buttons
        btnPrevious = New Button() With {.Text = "Previous", .Location = New Point(20, 220)}
        AddHandler btnPrevious.Click, AddressOf btnPrevious_Click

        btnNext = New Button() With {.Text = "Next", .Location = New Point(100, 220)}
        AddHandler btnNext.Click, AddressOf btnNext_Click

        ' Add controls to the form
        Me.Controls.Add(lblName)
        Me.Controls.Add(lblEmail)
        Me.Controls.Add(lblPhone)
        Me.Controls.Add(lblGithub)
        Me.Controls.Add(lblStopwatchTime)
        Me.Controls.Add(btnPrevious)
        Me.Controls.Add(btnNext)

        ' Load submissions
        LoadSubmissions()
    End Sub

    Private Async Sub LoadSubmissions()
        Try
            submissions = Await FetchSubmissions()
            If submissions.Count > 0 Then
                currentIndex = 0
                DisplaySubmission(currentIndex)
            Else
                MessageBox.Show("No submissions found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error fetching submissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Function FetchSubmissions() As Task(Of List(Of Submission))
        Dim client As New HttpClient()
        Dim submissions As New List(Of Submission)
        Dim index As Integer = 0
        Dim moreData As Boolean = True

        While moreData
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync($"http://localhost:3000/read?index={index}")
                If response.IsSuccessStatusCode Then
                    Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()
                    Dim submission As Submission = JsonConvert.DeserializeObject(Of Submission)(jsonResponse)
                    submissions.Add(submission)
                    index += 1
                Else
                    moreData = False
                End If
            Catch ex As HttpRequestException
                moreData = False
            End Try
        End While

        Return submissions
    End Function

    Private Sub DisplaySubmission(index As Integer)
        If index >= 0 AndAlso index < submissions.Count Then
            Dim currentSubmission As Submission = submissions(index)
            lblName.Text = "Name: " & currentSubmission.Name
            lblEmail.Text = "Email: " & currentSubmission.Email
            lblPhone.Text = "Phone: " & currentSubmission.Phone
            lblGithub.Text = "GitHub Link: " & currentSubmission.GithubLink
            lblStopwatchTime.Text = "Time: " & currentSubmission.StopwatchTime
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(currentIndex)
        End If
    End Sub
End Class
