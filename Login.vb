Public Class Login

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opencon()
        Createdatabase()
        Createtable_incoming()
        Createtable_outgoing()
        Reload_incomingorders()
        Counts_outofstocks()
        Counts_notoutofstocks()
        Reload_outgoingorders()
        Try
            Altertables()
        Catch ex As Exception
        End Try
        Counts_notoutofstocks()
        Counts_outofstocks()
        Timer1.Start()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MessageBox.Show("Please input username or password", "Missing username or password", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox2.Clear()
        ElseIf Not TextBox1.Text = TextBox2.Text Then
            MessageBox.Show("Please input valid username or password", "Incorrect username or password", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox2.Clear()
        Else
            MessageBox.Show("You are now logged on. Click OK to get it.", "Logged On", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Form1.ToolStripStatusLabel2.Text = TextBox1.Text
            TextBox1.Clear()
            TextBox2.Clear()
            Me.Hide()
            Form1.Show()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        TextBox2.PasswordChar = "$"
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        End
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label5.Text = Today & "  |  " & TimeOfDay
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox2.PasswordChar = "$"
    End Sub
End Class