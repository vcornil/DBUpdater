Imports System.Reflection

Public Class AboutForm
    Private Sub AboutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Acknowledgment message for DbUp
        lblAcknowledgment.Text = "This application uses the DbUp library for database migrations." & vbCrLf &
                                 "DbUp is licensed under the MIT license. Click on the link below for more information:"

        ' Initialize DataGridView
        dgvScripts.ReadOnly = True ' Make the DataGridView read-only
        dgvScripts.ColumnCount = 1
        dgvScripts.Columns(0).Name = "Embedded SQL Scripts"

        ' Get the list of embedded resources
        Dim assembly As Assembly = assembly.GetExecutingAssembly()
        Dim resourceNames As String() = assembly.GetManifestResourceNames()

        ' Filter and list embedded SQL scripts
        dgvScripts.Rows.Clear()
        For Each resourceName As String In resourceNames
            If resourceName.StartsWith("DBUpdater.DBU_") Then ' Adjust this based on your namespace and script naming convention
                dgvScripts.Rows.Add(resourceName)
            End If
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close() ' Closes only the About form
    End Sub

    Private Sub LinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel.LinkClicked
        Process.Start("https://dbup.github.io/")
    End Sub
End Class