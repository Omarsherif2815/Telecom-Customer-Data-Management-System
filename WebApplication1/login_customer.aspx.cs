using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace YourNamespace
{
    public partial class login_customer : Page
    {
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string mobileNo = MobileNoTextBox.Text;
            string password = PasswordTextBox.Text;

            // Validate credentials using the SQL function AccountLoginValidation
            bool isValidLogin = ValidateLogin(mobileNo, password);

            if (isValidLogin)
            {
                // Store the mobile number in session for later use
                Session["UserMobileNo"] = mobileNo;

                // Redirect to the next page after successful login
                Response.Redirect("choose_transaction.aspx");
            }
            else
            {
                // Display error message if login is invalid
                ErrorMessageLabel.Text = "Invalid mobile number or password!";
                ErrorMessageLabel.Visible = true;
            }
        }

        private bool ValidateLogin(string mobileNo, string password)
        {
            bool result = false;

            string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString(); // Set your connection string

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT dbo.AccountLoginValidation(@mobileNo, @password)", conn))
                    {
                        cmd.Parameters.AddWithValue("@mobileNo", mobileNo);
                        cmd.Parameters.AddWithValue("@password", password);

                        result = (bool)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log or show message)
                    ErrorMessageLabel.Text = "Error occurred: " + ex.Message;
                    ErrorMessageLabel.Visible = true;
                }
            }

            return result;
        }
    }
}
