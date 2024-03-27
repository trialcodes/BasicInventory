Public Class Outgoing

    Private Sub Outgoing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WindowState = FormWindowState.Maximized
        Lv1_outgoing()
        Opencon()
        Reload_outgoingorders()
    End Sub

    Sub Lv1_outgoing()
        With ListView1
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .HeaderStyle = ColumnHeaderStyle.Nonclickable
            .BorderStyle = BorderStyle.Fixed3D
            .Columns.Clear()
            .Columns.Add("#", 0)
            .Columns.Add("Supplier", 150)
            .Columns.Add("Contact No.", 100)
            .Columns.Add("Pullout", 100)
            .Columns.Add("Brand/Description", 500)
            .Columns.Add("Quantity", 100)
            ' .Columns.Add("Weight(kg)", 0)
            .Columns.Add("Seller Price", 150)
            .Columns.Add("Total SP", 100)
            .Columns.Add("SRP", 100)
            .Columns.Add("Total SRP", 150)
            .Columns.Add("NET INCOME", 150)
            .Columns.Add("Date Purchased", 150)
        End With
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        For Each subf As Form In Form1.MdiChildren
            subf.Close()
        Next
        Form1.GroupBox1.Visible = True
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text = "" Then
            Reload_outgoingorders()
        Else
            Search_ougoingorders_bysupplier()
        End If
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged
        Search_ougoingorders_bydate()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Filter_income()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        If DateTimePicker2.Value < DateTimePicker1.Value Then
            DateTimePicker2.Value = DateTimePicker1.Value
        Else
        End If
        Filter_income()
    End Sub
End Class