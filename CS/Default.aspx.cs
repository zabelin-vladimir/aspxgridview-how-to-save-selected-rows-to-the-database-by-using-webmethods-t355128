using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page {
	static string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString.ToString();

	protected void Page_Load(object sender, EventArgs e) {
		ASPxGridView1.DataBind();
		if(!IsPostBack) {
			SetSelectedItems();
		}
	}
	void SetSelectedItems() {
		DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
		DataTable dt = dv.ToTable();
		for(int i = 0; i < dt.Rows.Count; i++) {
			bool selected = dt.Rows[i].Field<bool>("ProductSelection");
			if(selected)
				ASPxGridView1.Selection.SetSelection(i, selected);
		}
	}
	[WebMethod]
	public static void UpdateDB(object[] array) {
		int count = array.Count();
		StringBuilder sqlCommand = new StringBuilder();
		for(int i = 0; i < count; i++) {
			var currentProduct = array[i] as Dictionary<string, object>;
			sqlCommand.AppendFormat("UPDATE Products SET ProductSelection='{0}' WHERE ProductID='{1}'; ", ((bool)currentProduct["status"]) ? "1" : "0", (string)currentProduct["id"]);
		}
		using(SqlConnection con = new SqlConnection(connectionString)) {
			SqlCommand cmd = new SqlCommand(sqlCommand.ToString(), con);
			cmd.Connection.Open();
			//cmd.ExecuteNonQuery(); uncomment to update DB
		}
	}
}