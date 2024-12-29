using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace YourNamespace
{
    public partial class RemoveBenefits : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Handle Remove Benefits button click
        protected void RemoveBenefitsButton_Click(object sender, EventArgs e)
        {
            string mobileNo = MobileNoInput.Text.Trim();
            string planID = PlanIDInput.Text.Trim();

            // Clear previous messages
            MessageLabel.Visible = false;
            ErrorMessageLabel.Visible = false;

            // Validate Mobile Number
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

            // Validate Mobile Number in Database
            if (!IsMobileNumberValid(mobileNo))
            {
                ErrorMessageLabel.Text = "The entered mobile number does not exist in the system.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            // Validate Plan ID
            if (string.IsNullOrEmpty(planID))
            {
                ErrorMessageLabel.Text = "Please provide a Plan ID.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            if (!IsPlanIDValid(planID))
            {
                ErrorMessageLabel.Text = "The entered Plan ID does not exist in the system.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            // Validate if the combination of Mobile Number and Plan ID exists in the plan_provides_benefits table
            if (!IsPlanBenefitExists(mobileNo, planID))
            {
                ErrorMessageLabel.Text = "Cannot remove benefit. Please provide a correct Plan ID for the mobile number.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            // Call the stored procedure to remove the benefits
            if (RemoveBenefitsFromAccount(mobileNo, planID))
            {
                MessageLabel.Text = "Benefits removed successfully!";
                MessageLabel.Visible = true;
            }
            else
            {
                ErrorMessageLabel.Text = "An error occurred while removing benefits. Please try again.";
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

        // Check if the mobile number exists in the customer_account table
        private bool IsMobileNumberValid(string mobileNo)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
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

        // Check if the Plan ID exists in the service_plan table
        private bool IsPlanIDValid(string planID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            string query = "SELECT COUNT(1) FROM service_plan WHERE planID = @planID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@planID", planID);
                conn.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        // Check if the combination of Mobile Number and Plan ID exists in the benefits table
        private bool IsPlanBenefitExists(string mobileNo, string planID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            string query = "SELECT COUNT(1) FROM Benefits B " +
                           "INNER JOIN plan_provides_benefits pb ON B.benefitID = pb.benefitid " +
                           "WHERE B.mobileNo = @mobile_num AND pb.planID = @plan_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                cmd.Parameters.AddWithValue("@plan_id", planID);
                conn.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        // Call the stored procedure to remove benefits
        private bool RemoveBenefitsFromAccount(string mobileNo, string planID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Benefits_Account", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                cmd.Parameters.AddWithValue("@plan_id", planID);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        // Redirect button click handler
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx"); // Adjust the redirection as needed
        }
    }
}
