using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class UpdatePoints : System.Web.UI.Page
    {
        protected void UpdateButton_Click(object sender, EventArgs e)
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
                // Call the stored procedure to update points
                var result = UpdateTotalPoints(MobileNumber.Text);

                if (result == null)
                {
                    // Mobile number does not exist
                    ErrorMessageLabel.Text = "The mobile number does not exist in the system.";
                    ErrorMessageLabel.Visible = true;
                }
                else if (result.Value)
                {
                    // Update successful
                    ResultLabel.Text = "Update successful!";
                    ResultLabel.CssClass = "result";
                    ResultLabel.Visible = true;
                }
                else
                {
                    // Update unsuccessful
                    ResultLabel.Text = "Update unsuccessful.";
                    ResultLabel.CssClass = "error";
                    ResultLabel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
                ErrorMessageLabel.Visible = true;
            }
        }

        private bool? UpdateTotalPoints(string mobileNumber)
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

                // Call the stored procedure to update points
                using (SqlCommand cmd = new SqlCommand("Total_Points_Account", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNumber);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if rows were affected
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
