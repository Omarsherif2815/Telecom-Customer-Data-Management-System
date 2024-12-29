using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace YourNamespace
{
    public partial class ResolvedTickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM allResolvedTickets"; // View from your database
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                ResolvedTicketsGridView.DataSource = dt;
                ResolvedTicketsGridView.DataBind();
            }
        }

        protected void ResolvedTicketsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ResolvedTicketsGridView.PageIndex = e.NewPageIndex;
            BindGrid(); // Rebind the GridView with the updated page index
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx"); // Redirect to the page you want
        }
    }
}
