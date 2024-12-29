using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace YourNamespace
{
    public partial class AllShops : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayAllShops();
            }
        }

        private void DisplayAllShops()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Query the view
                    string query = "SELECT * FROM allShops";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ShopsGridView.DataSource = dt;
                    ShopsGridView.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle errors gracefully
                    ShopsGridView.DataSource = null;
                    ShopsGridView.DataBind();
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }
            }
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choose_transaction.aspx");
        }
    }
}