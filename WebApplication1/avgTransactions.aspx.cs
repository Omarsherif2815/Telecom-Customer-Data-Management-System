using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class WalletAverageTransaction : System.Web.UI.Page
    {
        protected void GetAverageButton_Click(object sender, EventArgs e)
        {
            // Clear previous messages
            ResultLabel.Visible = false;
            ErrorMessageLabel.Visible = false;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(WalletID.Text) || string.IsNullOrWhiteSpace(StartDate.Text) || string.IsNullOrWhiteSpace(EndDate.Text))
            {
                ErrorMessageLabel.Text = "Please enter Wallet ID, Start Date, and End Date.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            int walletId;
            if (!int.TryParse(WalletID.Text, out walletId))
            {
                // Show error if Wallet ID is not an integer
                ErrorMessageLabel.Text = "Wallet ID must be an integer.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            try
            {
                // Parse date inputs
                DateTime startDate = DateTime.Parse(StartDate.Text);
                DateTime endDate = DateTime.Parse(EndDate.Text);

                // Check if the Wallet ID exists in the database
                if (!WalletExists(walletId))
                {
                    ErrorMessageLabel.Text = "The provided Wallet ID does not exist.";
                    ErrorMessageLabel.Visible = true;
                    return;
                }

                // Retrieve the average transaction amount
                int avgAmount = GetAverageTransaction(walletId, startDate, endDate);

                // Display the result
                ResultLabel.Text = $"Average Transaction Amount: {avgAmount}";
                ResultLabel.Visible = true;
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
                ErrorMessageLabel.Visible = true;
            }
        }

        private bool WalletExists(int walletId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Wallet WHERE WalletID = @walletID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@walletID", walletId);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        private int GetAverageTransaction(int walletId, DateTime startDate, DateTime endDate)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.Wallet_Transfer_Amount(@walletID, @start_date, @end_date)", conn))
                {
                    cmd.Parameters.AddWithValue("@walletID", walletId);
                    cmd.Parameters.AddWithValue("@start_date", startDate);
                    cmd.Parameters.AddWithValue("@end_date", endDate);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx");
        }
    }
}
