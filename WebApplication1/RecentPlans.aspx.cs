using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class AllSubscribedPlans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the mobile number from the session
                string mobileNo = Session["UserMobileNo"] as string;

                // Check if mobile number exists
                if (string.IsNullOrEmpty(mobileNo))
                {
                    // If no mobile number is found, redirect to login page
                    Response.Write("<script>alert('No mobile number found. Please log in again.');</script>");
                    Response.Redirect("login_customer.aspx");
                }
                else
                {
                    // Display the subscribed plans for the logged-in user
                    DisplaySubscribedPlans(mobileNo);
                }
            }
        }

        private void DisplaySubscribedPlans(string mobileNo)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Query to get the subscribed plans using the database function
                    string query = "SELECT * FROM dbo.Subscribed_plans_5_Months(@mobile_num)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNo);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    PlansGridView.DataSource = dt;
                    PlansGridView.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions and show an error message
                    PlansGridView.DataSource = null;
                    PlansGridView.DataBind();
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }
            }
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            // Redirect to the choose_transaction page
            Response.Redirect("choose_transaction.aspx");
        }
    }
}
