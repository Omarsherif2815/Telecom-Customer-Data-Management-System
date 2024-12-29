using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class ViewCustomerWallet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure data is loaded on page load
            if (!IsPostBack)
            {
                LoadWalletsData();
            }
        }

        // Function to load data from the CustomerWallet view
        private void LoadWalletsData()
        {
            try
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query to retrieve data from the CustomerWallet view
                    SqlCommand cmd = new SqlCommand("SELECT * FROM CustomerWallet", conn);

                    // Create a DataTable to hold the result set
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // Bind the result set to the GridView
                    WalletsGridView.DataSource = dt;
                    WalletsGridView.DataBind();

                    if (dt.Rows.Count == 0)
                    {
                        // Display message if no wallets found
                        WalletsGridView.Visible = false;
                    }
                    else
                    {
                        WalletsGridView.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors and display to the user if needed
                WalletsGridView.Visible = false;
                Response.Write("An error occurred: " + ex.Message);
            }
        }

        // Handle page index changing for pagination
        protected void WalletsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            WalletsGridView.PageIndex = e.NewPageIndex;
            LoadWalletsData();
        }

        // Redirect back to the previous page or homepage
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx");
        }
    }
}
