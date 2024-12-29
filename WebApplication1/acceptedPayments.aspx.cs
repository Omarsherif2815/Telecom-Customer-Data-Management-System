using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class AccountPaymentPoints : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // This function is triggered when the "Get Payment Points" button is clicked
        protected void GetPaymentPointsButton_Click(object sender, EventArgs e)
        {
            string mobileNo = MobileNoInput.Text.Trim();

            if (string.IsNullOrEmpty(mobileNo))
            {
                // Show error message if mobile number is empty
                ShowErrorMessage("Please enter a valid mobile number.");
                return;
            }

            // Validate mobile number format
            if (!IsAllDigits(mobileNo))
            {
                ShowErrorMessage("Mobile number must be an integer.");
                return;
            }

            if (mobileNo.Length != 11)
            {
                ShowErrorMessage("Mobile number must be exactly 11 digits.");
                return;
            }

            // Validate if the mobile number exists in the database
            if (!IsMobileNumberValid(mobileNo))
            {
                // Show error if mobile number does not exist in the database
                ShowErrorMessage("The mobile number does not exist in the system.");
                return;
            }

            try
            {
                // Define the connection string to your database
                string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Define the command to call the stored procedure to retrieve payment points information
                    SqlCommand cmd = new SqlCommand("Account_Payment_Points", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNo);

                    // Create a DataTable to hold the result set
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // Check if the result contains rows
                    if (dt.Rows.Count == 0)
                    {
                        // Display message if no data is found
                        ShowErrorMessage("No payment data found for this mobile number.");
                    }
                    else
                    {
                        // Bind the result set to the GridView
                        PaymentPointsGridView.DataSource = dt;
                        PaymentPointsGridView.DataBind();
                        PaymentPointsGridView.Visible = true;
                        ErrorMessageLabel.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Show error if there is any issue with database operation
                ShowErrorMessage("An error occurred: " + ex.Message);
            }
        }

        // Check if the mobile number exists in the customer_account table
        private bool IsMobileNumberValid(string mobileNo)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
            string query = "SELECT COUNT(1) FROM customer_account WHERE mobileNo = @mobileNo";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@mobileNo", mobileNo);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
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

        // Show an error message and hide the GridView
        private void ShowErrorMessage(string message)
        {
            ErrorMessageLabel.Visible = true;
            ErrorMessageLabel.Text = message;
            PaymentPointsGridView.Visible = false;
        }

        // Handle page index changing for the GridView (for pagination)
        protected void PaymentPointsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PaymentPointsGridView.PageIndex = e.NewPageIndex;
            GetPaymentPointsButton_Click(sender, e);
        }

        // Redirect back to the previous page or homepage
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx");
        }
    }
}
