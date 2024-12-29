using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class CheckWalletMobile : System.Web.UI.Page
    {
        protected void CheckButton_Click(object sender, EventArgs e)
        {
            // Clear previous messages
            ResultLabel.Visible = false;
            ErrorMessageLabel.Visible = false;

            // Validate input
            if (string.IsNullOrWhiteSpace(MobileNumber.Text) || MobileNumber.Text.Length != 11)
            {
                ErrorMessageLabel.Text = "Please enter a valid 11-digit mobile number.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            try
            {
                // Check if the mobile number exists and retrieve the wallet link status
                var result = GetMobileNumberStatus(MobileNumber.Text);

                if (result == null)
                {
                    // Mobile number does not exist in the database
                    ErrorMessageLabel.Text = "The mobile number does not exist in the system.";
                    ErrorMessageLabel.Visible = true;
                }
                else if (result.Value)
                {
                    // Mobile number is linked to a wallet
                    ResultLabel.Text = "The mobile number is linked to a wallet.";
                    ResultLabel.CssClass = "result"; // Blue message
                    ResultLabel.Visible = true;
                }
                else
                {
                    // Mobile number is not linked to a wallet
                    ResultLabel.Text = "The mobile number is not linked to a wallet.";
                    ResultLabel.CssClass = "error"; // Red message
                    ResultLabel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
                ErrorMessageLabel.Visible = true;
            }
        }

        private bool? GetMobileNumberStatus(string mobileNumber)
        {
            // Retrieve connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if the mobile number exists in the Customer_Account table
                using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Customer_Account WHERE mobileNo = @mobile_num", conn))
                {
                    checkCmd.Parameters.AddWithValue("@mobile_num", mobileNumber);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        // Mobile number does not exist
                        return null;
                    }
                }

                // Check if the mobile number is linked to a wallet
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.Wallet_MobileNo(@mobile_num)", conn))
                {
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);

                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value && Convert.ToBoolean(result);
                }
            }
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            // Redirect to choosing_view page
            Response.Redirect("choosing_view.aspx");
        }
    }
}
