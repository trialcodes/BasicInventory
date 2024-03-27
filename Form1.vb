Public Class Form1

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button6.Enabled = False
        IsMdiContainer = True
        Timer1.Start()
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
        Analysis1()
    End Sub

    Sub Analysis1()
        With Chart1
            .Series.Clear()
            .Series.Add("Incoming")
            .Series("Incoming").Points.Clear()
            .Series("Incoming").Points.AddXY("Stocks", Label6.Text)
            .Series("Incoming").ChartType = DataVisualization.Charting.SeriesChartType.Bar
            .Series.Add("Outgoing")
            .Series("Outgoing").Points.Clear()
            .Series("Outgoing").Points.AddXY("Stocks", Label5.Text)
            .Series("Outgoing").ChartType = DataVisualization.Charting.SeriesChartType.Bar
        End With

        With Chart2
            .Series.Clear()
            .Series.Add("Available")
            .Series("Available").Points.Clear()
            .Series("Available").Points.AddXY("Stocks", Label9.Text)
            .Series("Available").ChartType = DataVisualization.Charting.SeriesChartType.Bar
            .Series.Add("Out Of Stocks")
            .Series("Out Of Stocks").Points.Clear()
            .Series("Out Of Stocks").Points.AddXY("Stocks", Label7.Text)
            .Series("Out Of Stocks").ChartType = DataVisualization.Charting.SeriesChartType.Bar
        End With
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        Login.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For Each subf As Form In Me.MdiChildren
            subf.Close()
        Next
        GroupBox1.Visible = True
        Opencon()
        Createdatabase()
        Createtable_incoming()
        Createtable_outgoing()
        Reload_incomingorders()
        Counts_outofstocks()
        Counts_notoutofstocks()
        Reload_outgoingorders()
        Analysis1()
    End Sub

    Private Sub btnincoming_Click(sender As Object, e As EventArgs) Handles btnincoming.Click
        GroupBox1.Visible = False
        For Each subf As Form In Me.MdiChildren
            subf.Close()
        Next
        Incoming.MdiParent = Me
        Incoming.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ToolStripStatusLabel3.Text = Today
        ToolStripStatusLabel4.Text = TimeOfDay
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        GroupBox1.Visible = False
        For Each subf As Form In Me.MdiChildren
            subf.Close()
        Next
        Outgoing.MdiParent = Me
        Outgoing.Show()
    End Sub
End Class
