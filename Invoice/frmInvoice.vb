Imports System.Data.SqlClient

Public Class frmInvoice
    Dim conn As New SqlClient.SqlConnection

    Dim strServer As String
    Dim strDatabase As String

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim strSQL As String = "INSERT INTO Invoice (InvoiceNo, Date) VALUES('" & Me.txtInvoiceNo.Text & "','" & Me.dtpDate.Value & "') SELECT SCOPE_IDENTITY();"

            Dim cmd As SqlCommand = New SqlCommand(strSQL, conn)
            Dim identity As Integer

            identity = Integer.Parse(cmd.ExecuteScalar().ToString()) 'Get the @Identity Column

            For Each row As DataGridViewRow In Me.InvoiceDetailsDataGridView.Rows
                If row.Cells(Me.Qty.Index).Value = "" Then Exit For

                strSQL = "INSERT INTO InvoiceDetails (InvoiceID, Qty, ProductName, Price) VALUES(" & identity & ", " & row.Cells(Me.Qty.Index).Value & ",'" & row.Cells(Me.ProductName.Index).Value & "'," & row.Cells(Me.Price.Index).Value & ")"

                cmd = New SqlCommand(strSQL, conn)
                cmd.ExecuteNonQuery()
            Next row

            MessageBox.Show("Record saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strServer = "localhost"
        strDatabase = "Invoice"

        Dim cnString As String = "Data Source=" & strServer & ";Initial Catalog=" & strDatabase & ";Integrated Security=True;MultipleActiveResultSets=True"

        conn.ConnectionString = cnString
        conn.Open()

        Me.dtpDate.Value = Now
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("http://www.sourcecodester.com")
    End Sub
End Class
