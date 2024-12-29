using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace YourNamespace
{
    public partial class AccountUsagePlan : Page
    {
        // Connection string to your database
        private string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Event handler for the search button click
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string mobileNo = MobileNoInput.Text.Trim();
            string startDate = StartDateInput.Text.Trim();

            // Validate the mobile number
            if (string.IsNullOrEmpty(mobileNo))
            {
                ErrorMessageLabel.Text = "Please provide a mobile number.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            if (!IsAllDigits(mobileNo))
            {
                ErrorMessageLabel.Text = "Mobile number must be an integer.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            if (mobileNo.Length != 11)
            {
                ErrorMessageLabel.Text = "Mobile number must be exactly 11 digits.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            // Validate the start date
            if (string.IsNullOrEmpty(startDate))
            {
                ErrorMessageLabel.Text = "Please provide a start date.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            // Validate if the mobile number exists in the customer_account table
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand checkMobileCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM dbo.customer_account WHERE mobileNo = @mobile_num", conn);

                    checkMobileCmd.Parameters.AddWithValue("@mobile_num", mobileNo);

                    int mobileExists = (int)checkMobileCmd.ExecuteScalar();

                    if (mobileExists == 0)
                    {
                        ErrorMessageLabel.Text = "Error: The mobile number does not exist.";
                        ErrorMessageLabel.Visible = true;
                        return;
                    }
                }

                // Proceed with querying the Account_Usage_Plan function
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(
                            "SELECT * FROM dbo.Account_Usage_Plan(@mobile_num, @start_date)", conn);

                        da.SelectCommand.Parameters.AddWithValue("@mobile_num", mobileNo);
                        da.SelectCommand.Parameters.AddWithValue("@start_date", DateTime.Parse(startDate));

                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        AccountUsageGridView.DataSource = dt;
                        AccountUsageGridView.DataBind();
                        ErrorMessageLabel.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessageLabel.Text = "Error: " + ex.Message;
                    ErrorMessageLabel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "Error: " + ex.Message;
                ErrorMessageLabel.Visible = true;
            }
        }

        // Helper method to check if a string contains only digits
        private bool IsAllDigits(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        // Optional: handle pagination if necessary
        protected void AccountUsageGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            AccountUsageGridView.PageIndex = e.NewPageIndex;
            SearchButton_Click(sender, e); // Re-run the search with pagination.
        }

        // Optional: Redirect functionality
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx"); // Adjust as needed
        }
    }
}
