using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class Account_SMS_Offers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Handle "Get SMS Offers" button click
        protected void GetSMSOffersButton_Click(object sender, EventArgs e)
        {
            string mobileNo = MobileNoInput.Text.Trim();

            if (string.IsNullOrEmpty(mobileNo))
            {
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
                ShowErrorMessage("The mobile number does not exist in the system.");
                return;
            }

            try
            {
                // Retrieve SMS offers for the given mobile number
                string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL command to call the table-valued function
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Account_SMS_Offers(@mobile_num)", conn);
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNo);

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // Bind results to GridView
                    SMSOffersGridView.DataSource = dt;
                    SMSOffersGridView.DataBind();

                    if (dt.Rows.Count == 0)
                    {
                        ShowErrorMessage("No SMS offers found for this mobile number.");
                    }
                    else
                    {
                        SMSOffersGridView.Visible = true;
                        ErrorMessageLabel.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle database errors
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
            SMSOffersGridView.Visible = false;
        }

        // Handle GridView pagination
        protected void SMSOffersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SMSOffersGridView.PageIndex = e.NewPageIndex;
            GetSMSOffersButton_Click(sender, e);
        }

        // Redirect button click handler
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx");
        }
    }
}
