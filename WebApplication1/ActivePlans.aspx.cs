using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace YourNamespace
{
    public partial class view_plan_usage : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the mobile number from the session
                string mobileNumber = Session["UserMobileNo"] as string;

                if (string.IsNullOrEmpty(mobileNumber))
                {
                    // If no mobile number is stored, redirect to login or show an error
                    Response.Write("<div style='color:red;'>No mobile number found. Please log in again.</div>");
                    RedirectToLogin();
                }
                else
                {
                    // Automatically load the usage data for the mobile number
                    LoadUsageData(mobileNumber);
                }
            }
        }

        private void LoadUsageData(string mobileNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM dbo.Usage_Plan_CurrentMonth(@MobileNo)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MobileNo", mobileNumber);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            UsageGridView.DataSource = dataTable;
                            UsageGridView.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<div style='color:red;'>Error: {ex.Message}</div>");
            }
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choose_transaction.aspx");
        }

        private void RedirectToLogin()
        {
            Response.Redirect("login_customer.aspx");
        }
    }
}
