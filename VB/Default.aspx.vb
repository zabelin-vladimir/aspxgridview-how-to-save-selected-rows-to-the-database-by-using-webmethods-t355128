Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports System.Web.Services
Imports System.Web.UI

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private Shared connectionString As String = ConfigurationManager.ConnectionStrings("NorthwindConnectionString").ConnectionString.ToString()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        ASPxGridView1.DataBind()
        If Not IsPostBack Then
            SetSelectedItems()
        End If
    End Sub
    Private Sub SetSelectedItems()
        Dim dv As DataView = DirectCast(SqlDataSource1.Select(DataSourceSelectArguments.Empty), DataView)
        Dim dt As DataTable = dv.ToTable()
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim selected As Boolean = dt.Rows(i).Field(Of Boolean)("ProductSelection")
            If selected Then
                ASPxGridView1.Selection.SetSelection(i, selected)
            End If
        Next i
    End Sub
    <WebMethod> _
    Public Shared Sub UpdateDB(ByVal array() As Object)
        Dim count As Integer = array.Count()
        Dim sqlCommand As New StringBuilder()
        For i As Integer = 0 To count - 1
            Dim currentProduct = TryCast(array(i), Dictionary(Of String, Object))
            sqlCommand.AppendFormat("UPDATE Products SET ProductSelection='{0}' WHERE ProductID='{1}'; ",If(DirectCast(currentProduct("status"), Boolean), "1", "0"), DirectCast(currentProduct("id"), String))
        Next i
        Using con As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(sqlCommand.ToString(), con)
            cmd.Connection.Open()
            'cmd.ExecuteNonQuery(); uncomment to update DB
        End Using
    End Sub
End Class