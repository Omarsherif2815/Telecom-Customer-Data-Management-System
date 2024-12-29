using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class ViewAccountPayments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure data is loaded on page load
            if (!IsPostBack)
            {
                LoadAccountPaymentsData();
            }
        }

        // Function to load data from the AccountPayments view
        private void LoadAccountPaymentsData()
        {
            try
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL query to retrieve data from the AccountPayments view
                    SqlCommand cmd = new SqlCommand("SELECT * FROM AccountPayments", conn);

                    // Create a DataTable to hold the result set
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // Bind the result set to the GridView
                    AccountPaymentsGridView.DataSource = dt;
                    AccountPaymentsGridView.DataBind();

                    if (dt.Rows.Count == 0)
                    {
                        // Display message if no payments found
                        AccountPaymentsGridView.Visible = false;
                    }
                    else
                    {
                        AccountPaymentsGridView.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors and display to the user if needed
                AccountPaymentsGridView.Visible = false;
                Response.Write("An error occurred: " + ex.Message);
            }
        }

        // Handle page index changing for pagination
        protected void AccountPaymentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AccountPaymentsGridView.PageIndex = e.NewPageIndex;
            LoadAccountPaymentsData();
        }

        // Redirect back to the previous page or homepage
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx");
        }
    }
}
