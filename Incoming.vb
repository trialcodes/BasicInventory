Public Class Incoming

    Private Sub Incoming_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opencon()
        WindowState = FormWindowState.Maximized
        Lv1_incoming()
        Reload_incomingorders()
        Counts_outofstocks()
        Counts_notoutofstocks()
        Sum_of_SP()
        Sum_of_SRP()
        Sum_of_Profit()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        For Each subf As Form In Form1.MdiChildren
            subf.Close()
        Next
        Form1.GroupBox1.Visible = True
    End Sub

    Sub Lv1_incoming()
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
            .Columns.Add("Pullout", 0)
            .Columns.Add("Brand/Description", 500)
            .Columns.Add("Quantity", 100)
            .Columns.Add("Weight(kg)", 0)
            .Columns.Add("Seller Price", 120)
            .Columns.Add("Total SP", 100)
            .Columns.Add("SRP", 100)
            .Columns.Add("Total SRP", 100)
            .Columns.Add("PROFIT", 100)
            .Columns.Add("Date Delived", 100)
        End With

        With ComboBox1
            .FlatStyle = FlatStyle.Flat
            .DropDownStyle = ComboBoxStyle.DropDownList
            .Items.Clear()
            .Items.Add("Available")
            .Items.Add("Out of Stock")
            .Text = "Available"
        End With

        txtweight.Enabled = False
        txtSPtotal.ReadOnly = True
        txtSRPtotal.ReadOnly = True
        txtprofit.ReadOnly = True
    End Sub

    Sub Txtclr1()
        txtparts.Clear()
        txtquantity.Clear()
        txtweight.Clear()
        txtsellerprice.Clear()
        txtSPtotal.Clear()
        txtsrp.Clear()
        txtSRPtotal.Clear()
        txtprofit.Clear()
        txtsearch.Clear()
        GroupBox5.Enabled = False
        Label23.Text = "0"
    End Sub

    Sub Txtclr2()
        txtparts.Clear()
        txtquantity.Clear()
        txtweight.Clear()
        txtsellerprice.Clear()
        txtSPtotal.Clear()
        txtsrp.Clear()
        txtSRPtotal.Clear()
        txtprofit.Clear()
        txtsearch.Clear()
        txtsupp.Clear()
        txtcontactno.Clear()
        txtsearch.Clear()
        GroupBox5.Enabled = True
        Label23.Text = "0"
    End Sub

    Private Sub txtweight_TextChanged(sender As Object, e As EventArgs) Handles txtweight.TextChanged
        If Not IsNumeric(txtweight.Text) Then
            txtweight.Clear()
        End If
    End Sub

    Sub SPTotal()
        Try
            Dim n1, n2, n3 As Double
            n1 = txtquantity.Text
            n2 = txtsellerprice.Text
            n3 = n1 * n2
            txtSPtotal.Text = n3
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtquantity_TextChanged(sender As Object, e As EventArgs) Handles txtquantity.TextChanged
        If Not IsNumeric(txtquantity.Text) Then
            txtquantity.Clear()
        ElseIf txtsellerprice.Text = "" Then
            txtSPtotal.Text = 0
            txtprofit.Text = 0
        ElseIf txtsellerprice.Text = "" Then
            txtSPtotal.Text = 0
            txtprofit.Text = 0
        Else
            SPTotal()
            SRPTotal()
        End If
    End Sub

    Private Sub txtsellerprice_TextChanged(sender As Object, e As EventArgs) Handles txtsellerprice.TextChanged
        If Not IsNumeric(txtsellerprice.Text) Then
            txtsellerprice.Clear()
        ElseIf txtsellerprice.Text = "" Then
            txtSPtotal.Text = 0
            txtprofit.Text = 0
        Else
            SPTotal()
        End If
    End Sub

    Sub SRPTotal()
        Try
            Dim n1, n2, n3 As Double
            n1 = txtquantity.Text
            n2 = txtsrp.Text
            n3 = n1 * n2
            txtSRPtotal.Text = n3
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtsrp_TextChanged(sender As Object, e As EventArgs) Handles txtsrp.TextChanged
        If Not IsNumeric(txtsrp.Text) Then
            txtsrp.Clear()
        ElseIf txtsrp.Text = "" Then
            txtSRPtotal.Text = 0
            txtprofit.Text = 0
        Else
            SRPTotal()
        End If
    End Sub


    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If txtsupp.Text = "" Or txtcontactno.Text = "" Or txtparts.Text = "" Or txtquantity.Text = "" Or txtsellerprice.Text = "" Or txtSPtotal.Text = "" Or txtsrp.Text = "" Then
            MessageBox.Show("All orders details required.", "Missing Details", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf Not txtcontactno.TextLength = 11 Then
            MessageBox.Show("Contact Number not 11 digits.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf Label23.Text > "0" Then
            MessageBox.Show("Invalid clicking save if you are updating or delete one order in the list. If you wanna add new order click Clr/Rfrsh button first. Thank you!", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Addnew_incomingorders()
            Reload_incomingorders()
            MessageBox.Show("A new order is now saved.", "Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Txtclr1()
            Counts_outofstocks()
            Counts_notoutofstocks()
            Sum_of_SP()
            Sum_of_SRP()
            Sum_of_Profit()
        End If
       
    End Sub

    Private Sub btnclrrfrsh_Click(sender As Object, e As EventArgs) Handles btnclrrfrsh.Click
        Txtclr2()
        Reload_incomingorders()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If txtquantity.Text = "0.00" Or txtquantity.Text = "0" Then
            MessageBox.Show("An order is OUT OF STOCK.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            SellOrder.ShowDialog()
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        GroupBox5.Enabled = True
        Select_incomingorders()
    End Sub

    Sub Profit()
        Try
            Dim n1, n2, n3 As Double
            n1 = txtSPtotal.Text
            n2 = txtSRPtotal.Text
            n3 = n2 - n1
            txtprofit.Text = n3
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSPtotal_TextChanged(sender As Object, e As EventArgs) Handles txtSPtotal.TextChanged
        Profit()
    End Sub

    Private Sub txtSRPtotal_TextChanged(sender As Object, e As EventArgs) Handles txtSRPtotal.TextChanged
        Profit()
    End Sub

    Private Sub txtcontactno_TextChanged(sender As Object, e As EventArgs) Handles txtcontactno.TextChanged
        txtcontactno.MaxLength = 11
        If Not IsNumeric(txtcontactno.Text) Then
            txtcontactno.Clear()
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        If Label23.Text = "0" Then
            MessageBox.Show("Please click one order in the list first.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim ask As MsgBoxResult = MessageBox.Show("Are you sure to delete that order?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = MsgBoxResult.Yes Then
                delete_incomingorder()
                Reload_incomingorders()
                MessageBox.Show("An order is now deleted.", "Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Counts_outofstocks()
                Counts_notoutofstocks()
                Sum_of_SP()
                Sum_of_SRP()
                Sum_of_Profit()
            Else

            End If
        End If
        Txtclr2()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "Available" Then
            Showall_available()
        ElseIf ComboBox1.Text = "Out of Stock" Then
            Showall_NOTavailable()
        End If
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        If ComboBox1.Text = "Available" Then
            Showall_available()
        ElseIf ComboBox1.Text = "Out of Stock" Then
            Showall_NOTavailable()
        End If
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        If Label23.Text = "0" Then
            MessageBox.Show("Please click one order in the list first.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim ask As MsgBoxResult = MessageBox.Show("Are you sure to update that order?", "Updating", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = MsgBoxResult.Yes Then
                Update_incomingorders()
                Reload_incomingorders()
                MessageBox.Show("An order is now updated.", "Updated Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Counts_outofstocks()
                Counts_notoutofstocks()
                Sum_of_SP()
                Sum_of_SRP()
                Sum_of_Profit()
            Else

            End If
        End If
        Txtclr2()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text = "" Then
            Reload_incomingorders()
        Else
            Search_incomingorders_bysupplier()
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        txtsearch.Clear()
        DateTimePicker3.Text = Date.Now.ToString
        Search_incomingorders()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged
        txtsearch.Clear()
        DateTimePicker2.Text = Date.Now.ToString
        Search_incomingorders()
    End Sub
End Class