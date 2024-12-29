using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace YourNamespace
{
    public partial class WalletCashbackAmount : Page
    {
        protected void GetCashbackAmountButton_Click(object sender, EventArgs e)
        {
            // Clear previous messages
            ResultLabel.Visible = false;
            ErrorMessageLabel.Visible = false;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(WalletID.Text) || string.IsNullOrWhiteSpace(PlanID.Text))
            {
                ErrorMessageLabel.Text = "Please enter both Wallet ID and Plan ID.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            try
            {
                // Retrieve inputs
                int walletId = int.Parse(WalletID.Text.Trim());
                int planId = int.Parse(PlanID.Text.Trim());

                // Call the function to validate and get cashback amount
                string validationMessage = ValidateInputs(walletId, planId);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    ErrorMessageLabel.Text = validationMessage;
                    ErrorMessageLabel.Visible = true;
                }
                else
                {
                    // Check if the join condition exists
                    if (!CheckJoinCondition(walletId, planId))
                    {
                        ErrorMessageLabel.Text = "No matching cashback data found for the given Wallet ID and Plan ID.";
                        ErrorMessageLabel.Visible = true;
                    }
                    else
                    {
                        int cashbackAmount = GetCashbackAmount(walletId, planId);
                        ResultLabel.Text = $"Cashback Amount: {cashbackAmount}";
                        ResultLabel.Visible = true;
                    }
                }
            }
            catch (FormatException)
            {
                ErrorMessageLabel.Text = "Please enter valid numeric values for Wallet ID and Plan ID.";
                ErrorMessageLabel.Visible = true;
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
                ErrorMessageLabel.Visible = true;
            }
        }

        private string ValidateInputs(int walletId, int planId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Check if Wallet ID exists
                using (SqlCommand walletCheckCmd = new SqlCommand("SELECT COUNT(*) FROM Wallet WHERE walletID = @walletID", conn))
                {
                    walletCheckCmd.Parameters.AddWithValue("@walletID", walletId);
                    int walletCount = (int)walletCheckCmd.ExecuteScalar();

                    if (walletCount == 0)
                        return "The Wallet ID does not exist.";
                }

                // Check if Plan ID exists
                using (SqlCommand planCheckCmd = new SqlCommand("SELECT COUNT(*) FROM Service_Plan WHERE planID = @planID", conn))
                {
                    planCheckCmd.Parameters.AddWithValue("@planID", planId);
                    int planCount = (int)planCheckCmd.ExecuteScalar();

                    if (planCount == 0)
                        return "The Plan ID does not exist.";
                }
            }

            return null;
        }

        private bool CheckJoinCondition(int walletId, int planId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT COUNT(*)
                    FROM Cashback c
                    INNER JOIN Plan_Provides_Benefits pb ON c.benefitID = pb.benefitID
                    WHERE c.walletID = @walletID AND pb.planID = @planID", conn))
                {
                    cmd.Parameters.AddWithValue("@walletID", walletId);
                    cmd.Parameters.AddWithValue("@planID", planId);

                    conn.Open();
                    int matchCount = (int)cmd.ExecuteScalar();

                    return matchCount > 0; // Returns true if a match exists
                }
            }
        }

        private int GetCashbackAmount(int walletId, int planId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT dbo.Wallet_Cashback_Amount(@walletID, @planID)", conn))
                {
                    cmd.Parameters.AddWithValue("@walletID", walletId);
                    cmd.Parameters.AddWithValue("@planID", planId);

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
