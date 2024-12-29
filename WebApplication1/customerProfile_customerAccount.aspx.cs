using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class customerProfile_customerAccount : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
            string query = "SELECT * FROM allCustomerAccounts"; // Your view in the database

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                // Bind the DataTable to the GridView
                CustomerProfilesGridView.DataSource = dt;
                CustomerProfilesGridView.DataBind();
            }
        }

        // Handle paging if needed
        protected void CustomerProfilesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CustomerProfilesGridView.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        // Handle Next button click (if you want to implement custom paging logic)
        protected void NextButton_Click(object sender, EventArgs e)
        {
            if (CustomerProfilesGridView.PageIndex < CustomerProfilesGridView.PageCount - 1)
            {
                CustomerProfilesGridView.PageIndex++;
                BindGridView();
            }
        }

        // Handle Previous button click (if you want to implement custom paging logic)
        protected void PrevButton_Click(object sender, EventArgs e)
        {
            if (CustomerProfilesGridView.PageIndex > 0)
            {
                CustomerProfilesGridView.PageIndex--;
                BindGridView();
            }
        }

        // Redirect to physicalStores.aspx when the button is clicked
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx");
        }
    }
}
