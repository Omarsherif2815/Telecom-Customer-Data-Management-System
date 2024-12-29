using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class RechargeBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get mobile number from session (after login)
            string mobileNo = Session["UserMobileNo"] as string;
            if (string.IsNullOrEmpty(mobileNo))
            {
                Response.Redirect("login_customer.aspx"); // Redirect if no mobile number is found
            }
        }

        protected void RechargeBalanceButton_Click(object sender, EventArgs e)
        {
            string mobileNo = Session["UserMobileNo"] as string; // Fetch mobile number from session
            string amountText = AmountTextBox.Text.Trim();
            string paymentMethod = PaymentMethodDropDown.SelectedValue; // Get selected payment method

            // Validate if the payment amount is numeric
            if (!decimal.TryParse(amountText, out decimal amount))
            {
                ResultLabel.Text = "Payment amount should be an integer value.";
                ResultLabel.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Ensure payment amount is greater than 0
            if (amount <= 0)
            {
                ResultLabel.Text = "Error: Payment amount must be a positive value.";
                ResultLabel.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Check if other required fields are valid
            if (!string.IsNullOrEmpty(mobileNo) && !string.IsNullOrEmpty(paymentMethod))
            {
                if (CheckIfMobileNumberExists(mobileNo))
                {
                    ProcessRechargeBalance(mobileNo, amount, paymentMethod);
                }
                else
                {
                    // Display error message if the mobile number doesn't exist
                    ResultLabel.Text = "Error: The mobile number does not exist in the customer account.";
                    ResultLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                ResultLabel.Text = "Please enter valid values for Amount and Payment Method.";
                ResultLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool CheckIfMobileNumberExists(string mobileNo)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM Customer_Account WHERE mobileNo = @mobileNo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mobileNo", mobileNo);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0; // Return true if mobile number exists
                }
                catch (Exception ex)
                {
                    ResultLabel.Text = $"Error: {ex.Message}";
                    ResultLabel.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
        }

        private void ProcessRechargeBalance(string mobileNo, decimal amount, string paymentMethod)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Call the stored procedure to initiate the balance payment
                    string query = "EXEC Initiate_balance_payment @mobile_num, @amount, @payment_method";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@payment_method", paymentMethod);

                    // Execute the stored procedure
                    cmd.ExecuteNonQuery();

                    // Show success message
                    ResultLabel.Text = "Balance recharged successfully!";
                    ResultLabel.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    // Show error message
                    ResultLabel.Text = $"Error: {ex.Message}";
                    ResultLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        // Redirect to the homepage
        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choose_transaction.aspx"); // Replace with your actual homepage URL
        }
    }
}
