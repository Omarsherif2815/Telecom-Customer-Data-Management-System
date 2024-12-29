using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class physicalStoreVouchers : Page
    {
        // Connection string to your database
        private string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        // Method to bind data to GridView
        private void BindGridView()
        {
            try
            {
                // SQL query to fetch data from PhysicalStoreVouchers view
                string query = "SELECT * FROM PhysicalStoreVouchers";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            // Bind the data to the GridView
                            PhysicalStoreVouchersGridView.DataSource = dt;
                            PhysicalStoreVouchersGridView.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display error message if something goes wrong
                ErrorMessageLabel.Text = "Error: " + ex.Message;
                ErrorMessageLabel.Visible = true;
            }
        }

        // Event handler for page index changing (pagination)
        protected void PhysicalStoreVouchersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                // Set the new page index
                PhysicalStoreVouchersGridView.PageIndex = e.NewPageIndex;

                // Re-bind the GridView with updated page index
                BindGridView();
            }
            catch (Exception ex)
            {
                // Display error message if something goes wrong
                ErrorMessageLabel.Text = "Error: " + ex.Message;
                ErrorMessageLabel.Visible = true;
            }
        }

        // Event handler for redirect button click
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Redirect to the resolved tickets page (adjust URL as needed)
                Response.Redirect("choosing_view.aspx");
            }
            catch (Exception ex)
            {
                // Display error message if something goes wrong
                ErrorMessageLabel.Text = "Error: " + ex.Message;
                ErrorMessageLabel.Visible = true;
            }
        }
    }
}
