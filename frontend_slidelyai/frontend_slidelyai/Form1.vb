Public Class Form1
    Inherits Form

    Private btnViewSubmissions As Button
    Private btnCreateSubmission As Button

    Public Sub New()
        ' Initialize the form
        Me.Text = "Main Form"
        Me.Size = New Size(300, 200)

        ' Initialize buttons
        btnViewSubmissions = New Button()
        btnViewSubmissions.Text = "View Submissions"
        btnViewSubmissions.Location = New Point(50, 50)
        AddHandler btnViewSubmissions.Click, AddressOf btnViewSubmissions_Click

        btnCreateSubmission = New Button()
        btnCreateSubmission.Text = "Create Submission"
        btnCreateSubmission.Location = New Point(50, 100)
        AddHandler btnCreateSubmission.Click, AddressOf btnCreateSubmission_Click

        ' Add buttons to the form
        Me.Controls.Add(btnViewSubmissions)
        Me.Controls.Add(btnCreateSubmission)
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs)
        Dim viewSubmissionsForm As New ViewSubmissionsForm()
        viewSubmissionsForm.ShowDialog()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs)
        Dim createSubmissionForm As New CreateSubmissionForm()
        createSubmissionForm.ShowDialog()
    End Sub

    Public Shared Sub Main()
        Application.Run(New Form1())
    End Sub
End Class
