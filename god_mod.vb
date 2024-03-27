Imports MySql.Data.MySqlClient
Module god_mod
    Dim con As New MySqlConnection("server=localhost;username=root;database=mysql;")
    Dim com As New MySqlCommand
    Dim dr As MySqlDataReader

    Sub Opencon()
        con.Close()
        con.Open()
        com.Connection = con
    End Sub


    Sub Closereader()
        dr = com.ExecuteReader
        dr.Close()
    End Sub

    Sub Createdatabase()
        com.CommandText = "create database if not exists arminaporkmeatshop;"
        Closereader()
    End Sub


    Sub Altertables()
        com.CommandText = "alter table arminaporkmeatshop.incoming_orders modify column quantity decimal(6,2);"
        Closereader()
        com.CommandText = "alter table arminaporkmeatshop.outgoing_orders modify column weightperkg decimal(5,2);"
        Closereader()
    End Sub
    Sub Createtable_incoming()
        com.CommandText = "create table if not exists arminaporkmeatshop.incoming_orders(id int(100) auto_increment primary key, supplier text, contactno varchar(11), datepullout text, parts text, quantity decimal(6,2), weight text, sellerprice decimal(12,2), SPtotal decimal(12,2), SRP decimal(12,2), SRPtotal decimal(12,2), profit decimal(20,2), datedelivered text);"
        Closereader()
    End Sub

    Sub Addnew_orders_params()
        com.Parameters.AddWithValue("@supplier", Incoming.txtsupp.Text)
        com.Parameters.AddWithValue("@contactno", Incoming.txtcontactno.Text)
        com.Parameters.AddWithValue("@datepullout", Incoming.DateTimePicker1.Text)
        com.Parameters.AddWithValue("@parts", Incoming.txtparts.Text)
        com.Parameters.AddWithValue("@quantity", Incoming.txtquantity.Text)
        com.Parameters.AddWithValue("@weight", Incoming.txtweight.Text)
        com.Parameters.AddWithValue("@sellerprice", Incoming.txtsellerprice.Text)
        com.Parameters.AddWithValue("@SPTotal", Incoming.txtSPtotal.Text)
        com.Parameters.AddWithValue("@SRP", Incoming.txtsrp.Text)
        com.Parameters.AddWithValue("@SRPTotal", Incoming.txtSRPtotal.Text)
        com.Parameters.AddWithValue("@profit", Incoming.txtprofit.Text)
        com.Parameters.AddWithValue("@datedelivered", Form1.ToolStripStatusLabel3.Text)
        com.Parameters.AddWithValue("@id", Incoming.Label23.Text)
    End Sub

    Sub Addnew_incomingorders()
        com = New MySqlCommand("insert into arminaporkmeatshop.incoming_orders(supplier,contactno, datepullout, parts, quantity, weight, sellerprice, SPtotal, SRP, SRPtotal, profit, datedelivered)values(@supplier, @contactno, @datepullout, @parts, @quantity, @weight, @sellerprice, @SPtotal, @SRP, @SRPtotal, @profit, @datedelivered);", con)
        Addnew_orders_params()
        Closereader()
    End Sub

    Sub delete_incomingorder()
        com.CommandText = "delete from arminaporkmeatshop.incoming_orders where id='" & Incoming.ListView1.FocusedItem.Text & "';"
        Closereader()
    End Sub

    Sub Update_incomingorders()
        com = New MySqlCommand("update arminaporkmeatshop.incoming_orders set id=@id, supplier=@supplier, contactno=@contactno, datepullout=@datepullout, parts=@parts, quantity=@quantity, weight=@weight, sellerprice=@sellerprice, SPtotal=@SPtotal, SRP=@SRP, SRPtotal=@SRPtotal, profit=@profit where id=@id;", con)
        Addnew_orders_params()
        Closereader()
    End Sub

    Sub Reload_incomingorders()
        Incoming.ListView1.Items.Clear()
        com.CommandText = "select * from arminaporkmeatshop.incoming_orders order by id desc;"
        dr = com.ExecuteReader
        While dr.Read
            Dim orders As New ListViewItem(dr.Item(0).ToString)
            Dim orderc As Integer
            For orderc = 1 To 12 Step 1
                If dr.Item(5).ToString = "0.00" Then
                    orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.Firebrick
                Else
                    orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.DimGray
                End If
            Next
            orders.UseItemStyleForSubItems = False
            Incoming.ListView1.Items.AddRange(New ListViewItem() {orders})
        End While
        Incoming.Label15.Text = "No. of Orders: " & Incoming.ListView1.Items.Count
        Form1.Label6.Text = Incoming.ListView1.Items.Count
        dr.Close()
    End Sub

    Sub Select_incomingorders()
        com.CommandText = "select * from arminaporkmeatshop.incoming_orders where id='" & Incoming.ListView1.FocusedItem.Text & "';"
        dr = com.ExecuteReader
        While dr.Read
            Incoming.Label23.Text = dr.Item(0).ToString
            Incoming.txtsupp.Text = dr.Item(1).ToString
            Incoming.txtcontactno.Text = dr.Item(2).ToString
            Incoming.DateTimePicker1.Text = dr.Item(3).ToString
            Incoming.txtparts.Text = dr.Item(4).ToString
            Incoming.txtquantity.Text = dr.Item(5).ToString
            Incoming.txtsellerprice.Text = dr.Item(7).ToString
            Incoming.txtSPtotal.Text = dr.Item(8).ToString
            Incoming.txtsrp.Text = dr.Item(9).ToString
            Incoming.txtSRPtotal.Text = dr.Item(10).ToString
            Incoming.txtprofit.Text = dr.Item(11).ToString
            SellOrder.Label2.Text = dr.Item(0).ToString
            SellOrder.Label4.Text = dr.Item(7).ToString
            SellOrder.Label5.Text = dr.Item(9).ToString
        End While
        dr.Close()
    End Sub

    Sub Sum_of_SP()
        com.CommandText = "select sum(SPTotal) from arminaporkmeatshop.incoming_orders"
        dr = com.ExecuteReader
        While dr.Read
            Incoming.Label17.Text = dr.Item(0).ToString
        End While
        dr.Close()
    End Sub

    Sub Sum_of_SRP()
        com.CommandText = "select sum(SRPTotal) from arminaporkmeatshop.incoming_orders"
        dr = com.ExecuteReader
        While dr.Read
            Incoming.Label18.Text = dr.Item(0).ToString
        End While
        dr.Close()
    End Sub

    Sub Sum_of_Profit()
        com.CommandText = "select sum(profit) from arminaporkmeatshop.incoming_orders"
        dr = com.ExecuteReader
        While dr.Read
            Incoming.Label26.Text = dr.Item(0).ToString
        End While
        dr.Close()
    End Sub

    Sub Counts_outofstocks()
        com.CommandText = "select count(quantity) from arminaporkmeatshop.incoming_orders where quantity = ""0.00"";"
        dr = com.ExecuteReader
        While dr.Read
            Incoming.Label22.Text = dr.Item(0).ToString
            Form1.Label7.Text = dr.Item(0).ToString
            Login.Label7.Text = dr.Item(0).ToString
        End While
        dr.Close()
    End Sub

    Sub Counts_notoutofstocks()
        com.CommandText = "select count(quantity) from arminaporkmeatshop.incoming_orders where quantity > ""0.00"";"
        dr = com.ExecuteReader
        While dr.Read
            Form1.Label9.Text = dr.Item(0).ToString
            Login.Label6.Text = dr.Item(0).ToString
            Incoming.Label24.Text = dr.Item(0).ToString
        End While
        dr.Close()
    End Sub

    Sub Showall_available()
        Incoming.ListView1.Items.Clear()
        com.CommandText = "select * from arminaporkmeatshop.incoming_orders where quantity > ""0.00"";"
        dr = com.ExecuteReader
        While dr.Read
            Dim orders As New ListViewItem(dr.Item(0).ToString)
            Dim orderc As Integer
            For orderc = 1 To 12 Step 1
                orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.DimGray
            Next
            orders.UseItemStyleForSubItems = False
            Incoming.ListView1.Items.AddRange(New ListViewItem() {orders})
        End While
        dr.Close()
    End Sub

    Sub Showall_NOTavailable()
        Incoming.ListView1.Items.Clear()
        com.CommandText = "select * from arminaporkmeatshop.incoming_orders where quantity = ""0.00"";"
        dr = com.ExecuteReader
        While dr.Read
            Dim orders As New ListViewItem(dr.Item(0).ToString)
            Dim orderc As Integer
            For orderc = 1 To 12 Step 1
                orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.Firebrick
            Next
            orders.UseItemStyleForSubItems = False
            Incoming.ListView1.Items.AddRange(New ListViewItem() {orders})
        End While
        dr.Close()
    End Sub


    Sub Search_incomingorders()
        Incoming.ListView1.Items.Clear()
        com.CommandText = "select * from arminaporkmeatshop.incoming_orders where datepullout ='" & Incoming.DateTimePicker2.Text & "' or datedelivered ='" & Incoming.DateTimePicker3.Text & "' order by parts asc;"
        dr = com.ExecuteReader
        While dr.Read
            Dim orders As New ListViewItem(dr.Item(0).ToString)
            Dim orderc As Integer
            For orderc = 1 To 12 Step 1
                If dr.Item(5).ToString = "0.00" Then
                    orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.Firebrick
                Else
                    orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.DimGray
                End If
            Next
            orders.UseItemStyleForSubItems = False
            Incoming.ListView1.Items.AddRange(New ListViewItem() {orders})
        End While
        Incoming.Label15.Text = "No. of Orders: " & Incoming.ListView1.Items.Count
        dr.Close()
    End Sub

    Sub Search_incomingorders_bysupplier()
        Incoming.ListView1.Items.Clear()
        com.CommandText = "select * from arminaporkmeatshop.incoming_orders where supplier like '%" & Incoming.txtsearch.Text & "%' or parts like '%" & Incoming.txtsearch.Text & "%' order by parts asc;"
        dr = com.ExecuteReader
        While dr.Read
            Dim orders As New ListViewItem(dr.Item(0).ToString)
            Dim orderc As Integer
            For orderc = 1 To 12 Step 1
                If dr.Item(5).ToString = "0.00" Then
                    orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.Firebrick
                Else
                    orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.DimGray
                End If
            Next
            orders.UseItemStyleForSubItems = False
            Incoming.ListView1.Items.AddRange(New ListViewItem() {orders})
        End While
        Incoming.Label15.Text = "No. of Orders: " & Incoming.ListView1.Items.Count
        dr.Close()
    End Sub


    Sub Createtable_outgoing()
        com.CommandText = "create table if not exists arminaporkmeatshop.outgoing_orders(id int(100) auto_increment primary key, supplier text, contactno varchar(11), datepullout text, parts text, weightperkg decimal(6,2), sellerprice decimal(12,2), SPtotal decimal(12,2), SRP decimal(12,2), SRPtotal decimal(12,2), profit decimal(20,2), datedelivered text);"
        Closereader()
    End Sub

    Sub Addnew_outgoing_params()
        com.Parameters.AddWithValue("@supplier", Incoming.txtsupp.Text)
        com.Parameters.AddWithValue("@contactno", Incoming.txtcontactno.Text)
        com.Parameters.AddWithValue("@datepullout", Incoming.DateTimePicker1.Text)
        com.Parameters.AddWithValue("@parts", Incoming.txtparts.Text)
        com.Parameters.AddWithValue("@weightperkg", SellOrder.TextBox1.Text)
        com.Parameters.AddWithValue("@sellerprice", Incoming.txtsellerprice.Text)
        com.Parameters.AddWithValue("@SPTotal", Incoming.txtSPtotal.Text)
        com.Parameters.AddWithValue("@SPTotal2", SellOrder.Label6.Text)
        com.Parameters.AddWithValue("@SRP", Incoming.txtsrp.Text)
        com.Parameters.AddWithValue("@SRPTotal", Incoming.txtSRPtotal.Text)
        com.Parameters.AddWithValue("@SRPTotal2", SellOrder.Label7.Text)
        com.Parameters.AddWithValue("@profit", SellOrder.Label8.Text)
        com.Parameters.AddWithValue("@datedelivered", Form1.ToolStripStatusLabel3.Text)
        com.Parameters.AddWithValue("@id", Incoming.Label23.Text)
        com.Parameters.AddWithValue("@id2", Outgoing.Label23.Text)
    End Sub

    Sub Addnew_outgoingorders()
        com = New MySqlCommand("insert into arminaporkmeatshop.outgoing_orders(supplier,contactno, datepullout, parts, weightperkg, sellerprice, SPtotal, SRP, SRPtotal, profit, datedelivered)values(@supplier, @contactno, @datepullout, @parts, @weightperkg, @sellerprice, @SPtotal2, @SRP, @SRPtotal2, @profit, @datedelivered);", con)
        Addnew_outgoing_params()
        Closereader()
    End Sub

    Sub Delete_outgoingorders()
        com = New MySqlCommand("delete from arminaporkmeatshop.outgoing_orders where id=@id2;", con)
        Addnew_outgoing_params()
        Closereader()
    End Sub

    Sub Reload_outgoingorders()
        Outgoing.ListView1.Items.Clear()
        com.CommandText = "select * from arminaporkmeatshop.outgoing_orders order by id desc;"
        dr = com.ExecuteReader
        While dr.Read
            Dim orders As New ListViewItem(dr.Item(0).ToString)
            Dim orderc As Integer
            For orderc = 1 To 11 Step 1
                orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.DimGray
            Next
            orders.UseItemStyleForSubItems = False
            Outgoing.ListView1.Items.AddRange(New ListViewItem() {orders})
        End While
        Outgoing.Label15.Text = "No. of Purchased: " & Outgoing.ListView1.Items.Count
        Form1.Label5.Text = Outgoing.ListView1.Items.Count
        dr.Close()
    End Sub

    Sub Search_ougoingorders_bysupplier()
        Outgoing.ListView1.Items.Clear()
        com.CommandText = "select * from arminaporkmeatshop.outgoing_orders where supplier like '%" & Outgoing.txtsearch.Text & "%' or parts like '%" & Outgoing.txtsearch.Text & "%' order by parts asc;"
        dr = com.ExecuteReader
        While dr.Read
            Dim orders As New ListViewItem(dr.Item(0).ToString)
            Dim orderc As Integer
            For orderc = 1 To 11 Step 1
                orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.DimGray
            Next
            orders.UseItemStyleForSubItems = False
            Outgoing.ListView1.Items.AddRange(New ListViewItem() {orders})
        End While
        Outgoing.Label15.Text = "No. of Purchased: " & Outgoing.ListView1.Items.Count
        dr.Close()
    End Sub

    Sub Search_ougoingorders_bydate()
        Outgoing.ListView1.Items.Clear()
        com.CommandText = "select * from arminaporkmeatshop.outgoing_orders where datedelivered = '" & Outgoing.DateTimePicker3.Text & "' order by id asc;"
        dr = com.ExecuteReader
        While dr.Read
            Dim orders As New ListViewItem(dr.Item(0).ToString)
            Dim orderc As Integer
            For orderc = 1 To 11 Step 1
                orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.DimGray
            Next
            orders.UseItemStyleForSubItems = False
            Outgoing.ListView1.Items.AddRange(New ListViewItem() {orders})
        End While
        Outgoing.Label15.Text = "No. of Purchased: " & Outgoing.ListView1.Items.Count
        dr.Close()
    End Sub


    Sub Filter_income()
        Outgoing.ListView1.Items.Clear()
        com = New MySqlCommand("select * from  arminaporkmeatshop.outgoing_orders where datedelivered between '" & Outgoing.DateTimePicker1.Text & "' and '" & Outgoing.DateTimePicker2.Text & "' order by id asc;", con)
        dr = com.ExecuteReader
        While dr.Read
            Dim orders As New ListViewItem(dr.Item(0).ToString)
            Dim orderc As Integer
            For orderc = 1 To 11 Step 1
                orders.SubItems.Add(dr.Item(orderc).ToString).ForeColor = Color.DimGray
            Next
            orders.UseItemStyleForSubItems = False
            Outgoing.ListView1.Items.AddRange(New ListViewItem() {orders})
        End While
        dr.Close()

        com = New MySqlCommand("select sum(profit) from arminaporkmeatshop.outgoing_orders  where datedelivered between '" & Outgoing.DateTimePicker1.Text & "' and '" & Outgoing.DateTimePicker2.Text & "' order by id asc;", con)
        dr = com.ExecuteReader
        While dr.Read
            Outgoing.Label26.Text = dr.Item(0).ToString
        End While
        dr.Close()

    End Sub





End Module
