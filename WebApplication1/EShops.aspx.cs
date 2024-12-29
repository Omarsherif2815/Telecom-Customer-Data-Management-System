using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class ViewEShopVouchers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure data is loaded on page load
            if (!IsPostBack)
            {
                LoadEShopVouchersData();
            }
        }

        // Function to load data from the E_shopVouchers view
        private void LoadEShopVouchersData()
        {
            try
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query to retrieve data from the E_shopVouchers view
                    SqlCommand cmd = new SqlCommand("SELECT * FROM E_shopVouchers", conn);

                    // Create a DataTable to hold the result set
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // Check if there are any rows in the DataTable
                    if (dt.Rows.Count == 0)
                    {
                        // If no data, show the "No vouchers are available" message and hide the GridView
                        NoVouchersLabel.Visible = true;
                        EShopVouchersGridView.Visible = false;
                    }
                    else
                    {
                        // If data exists, bind the result set to the GridView and hide the "No vouchers" message
                        NoVouchersLabel.Visible = false;
                        EShopVouchersGridView.Visible = true;
                        EShopVouchersGridView.DataSource = dt;
                        EShopVouchersGridView.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors and display to the user if needed
                NoVouchersLabel.Visible = true;
                NoVouchersLabel.Text = "An error occurred: " + ex.Message;
                EShopVouchersGridView.Visible = false;
            }
        }

        // Handle page index changing for pagination
        protected void EShopVouchersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EShopVouchersGridView.PageIndex = e.NewPageIndex;
            LoadEShopVouchersData();
        }

        // Redirect back to the previous page or homepage
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx");
        }
    }
}
