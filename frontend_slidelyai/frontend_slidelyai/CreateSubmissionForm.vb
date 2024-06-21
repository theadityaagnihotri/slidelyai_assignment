Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Diagnostics

Public Class CreateSubmissionForm
    Inherits Form

    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhone As TextBox
    Private txtGithub As TextBox
    Private btnStartPause As Button
    Private btnSubmit As Button
    Private stopwatch As Stopwatch

    Public Sub New()
        ' Initialize the form
        Me.Text = "Create Submission"
        Me.Size = New Size(400, 300)

        ' Initialize controls
        txtName = New TextBox() With {.Location = New Point(20, 20), .Width = 200}
        AddPlaceholderText(txtName, "Name")

        txtEmail = New TextBox() With {.Location = New Point(20, 60), .Width = 200}
        AddPlaceholderText(txtEmail, "Email")

        txtPhone = New TextBox() With {.Location = New Point(20, 100), .Width = 200}
        AddPlaceholderText(txtPhone, "Phone")

        txtGithub = New TextBox() With {.Location = New Point(20, 140), .Width = 200}
        AddPlaceholderText(txtGithub, "GitHub Link")

        btnStartPause = New Button() With {.Text = "Start", .Location = New Point(20, 180)}
        AddHandler btnStartPause.Click, AddressOf btnStartPause_Click

        btnSubmit = New Button() With {.Text = "Submit", .Location = New Point(100, 180)}
        AddHandler btnSubmit.Click, AddressOf btnSubmit_Click

        ' Add controls to the form
        Me.Controls.Add(txtName)
        Me.Controls.Add(txtEmail)
        Me.Controls.Add(txtPhone)
        Me.Controls.Add(txtGithub)
        Me.Controls.Add(btnStartPause)
        Me.Controls.Add(btnSubmit)

        ' Initialize the stopwatch
        stopwatch = New Stopwatch()
    End Sub

    Private Sub AddPlaceholderText(textBox As TextBox, placeholder As String)
        textBox.Text = placeholder
        textBox.ForeColor = Color.Gray

        AddHandler textBox.GotFocus, Sub(sender As Object, e As EventArgs)
                                         If textBox.Text = placeholder Then
                                             textBox.Text = ""
                                             textBox.ForeColor = Color.Black
                                         End If
                                     End Sub

        AddHandler textBox.LostFocus, Sub(sender As Object, e As EventArgs)
                                          If String.IsNullOrWhiteSpace(textBox.Text) Then
                                              textBox.Text = placeholder
                                              textBox.ForeColor = Color.Gray
                                          End If
                                      End Sub
    End Sub

    Private Sub btnStartPause_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            btnStartPause.Text = "Resume"
        Else
            stopwatch.Start()
            btnStartPause.Text = "Pause"
        End If
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs)
        If ValidateInput() Then
            Dim submission As New Submission With {
                .Name = txtName.Text,
                .Email = txtEmail.Text,
                .Phone = txtPhone.Text,
                .GithubLink = txtGithub.Text,
                .StopwatchTime = stopwatch.Elapsed.ToString()
            }

            Try
                Await SubmitToBackend(submission)
                MessageBox.Show("Submission successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
            Catch ex As Exception
                MessageBox.Show($"Error submitting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function ValidateInput() As Boolean
        Return txtName.ForeColor <> Color.Gray AndAlso
         txtEmail.ForeColor <> Color.Gray AndAlso
         txtPhone.ForeColor <> Color.Gray AndAlso
         txtGithub.ForeColor <> Color.Gray AndAlso
         Not String.IsNullOrWhiteSpace(txtName.Text) AndAlso
         Not String.IsNullOrWhiteSpace(txtEmail.Text) AndAlso
         Not String.IsNullOrWhiteSpace(txtPhone.Text) AndAlso
         Not String.IsNullOrWhiteSpace(txtGithub.Text)
    End Function
    Private Async Function SubmitToBackend(submission As Submission) As Task
        Dim client As New HttpClient()
        Dim json As String = JsonConvert.SerializeObject(submission)
        Dim content As New StringContent(json, System.Text.Encoding.UTF8, "application/json")

        Try
            Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/submit", content)
            response.EnsureSuccessStatusCode()
        Catch ex As HttpRequestException
            Throw New Exception($"Error sending data to server: {ex.Message}")
        Catch ex As Exception
            Throw New Exception($"Error submitting data: {ex.InnerException?.Message }") ' Include original error message
        End Try
    End Function

    Private Sub ClearForm()
        txtName.Text = "Name"
        txtName.ForeColor = Color.Gray
        txtEmail.Text = "Email"
        txtEmail.ForeColor = Color.Gray
        txtPhone.Text = "Phone"
        txtPhone.ForeColor = Color.Gray
        txtGithub.Text = "GitHub Link"
        txtGithub.ForeColor = Color.Gray
        stopwatch.Reset()
        btnStartPause.Text = "Start"
    End Sub
End Class

