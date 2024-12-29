using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class view_service_plans : System.Web.UI.Page
    {
        // Replace with your database connection string
        string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();  // Set your connection string

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadServicePlans();
            }
        }

        private void LoadServicePlans()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM allServicePlans";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            ServicePlansGridView.DataSource = dataTable;
                            ServicePlansGridView.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log error or display a friendly message)
                Response.Write($"<div style='color: red;'>Error: {ex.Message}</div>");
            }
        }

        protected void ServicePlansGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ServicePlansGridView.PageIndex = e.NewPageIndex;
            LoadServicePlans();
        }

        // Redirect to the choose_transaction page when "Proceed" button is clicked
        protected void ProceedButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("login_customer.aspx"); // Replace with your actual transaction page URL
        }

        // Redirect to the user_customer page when "Return" button is clicked
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("user_customer.aspx");
        }
    }
}
