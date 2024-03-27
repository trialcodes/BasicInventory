Public Class SellOrder

    Private Sub SellOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opencon()
    End Sub

    Sub Calculate_weight_per_kg()
        Try
            Dim n1, n2, n3 As Decimal
            n1 = Incoming.txtquantity.Text
            n2 = TextBox1.Text
            n3 = n1 - n2
            Incoming.txtquantity.Text = n3
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter weight first.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Calculate_weight_per_kg()
                Addnew_outgoingorders()
                MessageBox.Show("An order purchased.", "Purchasing", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Update_incomingorders()
                Reload_incomingorders()
                Reload_outgoingorders()
                TextBox1.Clear()
                Incoming.Txtclr2()
                Me.Close()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.MaxLength = 5
        If Char.IsDigit(e.KeyChar) Or e.KeyChar = vbBack Or e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Sub Calc_in_grams()
        Try
            Dim n1, n2, n3 As Double
            n1 = TextBox1.Text
            n2 = Label4.Text
            n3 = n2 * n1
            Label6.Text = n3
        Catch ex As Exception
        End Try
        Try
            Dim n1, n2, n3 As Double
            n1 = TextBox1.Text
            n2 = Label5.Text
            n3 = n2 * n1
            Label7.Text = n3
        Catch ex As Exception
        End Try

        Try
            Dim n1, n2, n3 As Double
            n1 = Label6.Text
            n2 = Label7.Text
            n3 = n2 - n1
            Label8.Text = n3
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            Dim n1, n2 As Double
            n1 = TextBox1.Text
            n2 = Incoming.txtquantity.Text
            If n1 > n2 Then
                MessageBox.Show("Please enter enough stocks. Thank you!", "Not Enough stocks", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox1.Clear()
            Else
                Calc_in_grams()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class