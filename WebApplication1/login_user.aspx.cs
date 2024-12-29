using System;
using System.Web.UI;

namespace YourNamespace
{
    public partial class login : Page
    {
        // Hardcoded admin credentials
        private const int AdminID = 12345; // Example hardcoded admin ID
        private const string AdminPassword = "admin123"; // Example hardcoded admin password

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Hide the error message initially
                ErrorMessageLabel.Visible = false;
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate Admin ID input (ensure it's a number)
                if (!int.TryParse(AdminIDTextBox.Text, out int inputAdminID))
                {
                    // Show error if Admin ID is not numeric
                    ErrorMessageLabel.Text = "Admin ID must be a numeric value.";
                    ErrorMessageLabel.Visible = true;
                    ClearInputFields();
                    return;
                }

                // Get input password
                string inputPassword = PasswordTextBox.Text;

                // Check credentials
                if (inputAdminID == AdminID && inputPassword == AdminPassword)
                {
                    // Login successful - Redirect to the customer profile page
                    Response.Redirect("choosing_view.aspx");
                }
                else
                {
                    // Login failed - Show error message and clear input fields
                    ErrorMessageLabel.Text = "Incorrect Admin ID or Password. Please try again.";
                    ErrorMessageLabel.Visible = true;
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and show the error
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
                ErrorMessageLabel.Visible = true;
                ClearInputFields();
            }
        }

        /// <summary>
        /// Clears the AdminID and Password textboxes.
        /// </summary>
        private void ClearInputFields()
        {
            AdminIDTextBox.Text = string.Empty;
            PasswordTextBox.Text = string.Empty;
        }
    }
}
