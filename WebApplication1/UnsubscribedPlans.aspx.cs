using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class unsubscribed_plans : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the mobile number is stored in the session
                string mobileNumber = Session["UserMobileNo"] as string;

                if (string.IsNullOrEmpty(mobileNumber))
                {
                    // If no mobile number is stored, redirect to login or show an error
                    Response.Write("<div style='color:red;'>No mobile number found. Please log in again.</div>");
                    RedirectToLogin();
                }
                else
                {
                    // Automatically populate the grid with unsubscribed plans for the mobile number
                    LoadUnsubscribedPlans(mobileNumber);
                }
            }
        }

        private void LoadUnsubscribedPlans(string mobileNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("Unsubscribed_Plans", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@mobile_num", mobileNumber);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            UnsubscribedPlansGridView.DataSource = dataTable;
                            UnsubscribedPlansGridView.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<div style='color:red;'>Error: {ex.Message}</div>");
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            // Reuse the stored mobile number from the session
            string mobileNumber = Session["UserMobileNo"] as string;

            if (string.IsNullOrEmpty(mobileNumber))
            {
                Response.Write("<div style='color:red;'>No mobile number found. Please log in again.</div>");
                RedirectToLogin();
                return;
            }

            LoadUnsubscribedPlans(mobileNumber);
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
