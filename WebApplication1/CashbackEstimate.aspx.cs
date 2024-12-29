using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class DisplayCashback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mobileNo = Session["UserMobileNo"] as string; // Assume the mobile number is stored in the session
                if (string.IsNullOrEmpty(mobileNo))
                {
                    Response.Redirect("login_customer.aspx");
                }

                LoadPaymentDropdown(mobileNo);
                LoadBenefitDropdown(mobileNo);
                DisplayCurrentBalance(mobileNo);
            }
        }

        private void LoadPaymentDropdown(string mobileNo)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT paymentID 
                        FROM Payment 
                        WHERE mobileNo = @mobileNo AND status = 'Successful'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mobileNo", mobileNo);

                    SqlDataReader reader = cmd.ExecuteReader();
                    PaymentIDDropDown.Items.Clear();
                    PaymentIDDropDown.Items.Add(new System.Web.UI.WebControls.ListItem("Select Payment ID", ""));

                    while (reader.Read())
                    {
                        PaymentIDDropDown.Items.Add(new System.Web.UI.WebControls.ListItem(reader["paymentID"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    CashbackLabel.Text = $"Error loading payments: {ex.Message}";
                    CashbackLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void LoadBenefitDropdown(string mobileNo)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT DISTINCT benefitID 
                        FROM Benefits 
                        WHERE mobileNo = @mobileNo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mobileNo", mobileNo);

                    SqlDataReader reader = cmd.ExecuteReader();
                    BenefitIDDropDown.Items.Clear();
                    BenefitIDDropDown.Items.Add(new System.Web.UI.WebControls.ListItem("Select Benefit ID", ""));

                    while (reader.Read())
                    {
                        BenefitIDDropDown.Items.Add(new System.Web.UI.WebControls.ListItem(reader["benefitID"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    CashbackLabel.Text = $"Error loading benefits: {ex.Message}";
                    CashbackLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void DisplayCurrentBalance(string mobileNo)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT Wallet.current_balance 
                        FROM Customer_Account 
                        INNER JOIN Wallet ON Customer_Account.nationalID = Wallet.nationalID 
                        WHERE Customer_Account.mobileNo = @mobileNo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mobileNo", mobileNo);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        BalanceLabel.Text = $"The current balance is: {result} units";
                        BalanceLabel.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        BalanceLabel.Text = "Balance information is unavailable.";
                        BalanceLabel.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    BalanceLabel.Text = $"Error retrieving balance: {ex.Message}";
                    BalanceLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void ShowCashbackButton_Click(object sender, EventArgs e)
        {
            if (PaymentIDDropDown.SelectedValue != "" && BenefitIDDropDown.SelectedValue != "")
            {
                string mobileNo = Session["UserMobileNo"] as string;
                int paymentId = Convert.ToInt32(PaymentIDDropDown.SelectedValue);
                int benefitId = Convert.ToInt32(BenefitIDDropDown.SelectedValue);

                GetCashbackAmount(mobileNo, paymentId, benefitId);
            }
            else
            {
                CashbackLabel.Text = "Please select valid Payment ID and Benefit ID.";
                CashbackLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void GetCashbackAmount(string mobileNo, int paymentId, int benefitId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    // Execute the stored procedure Payment_wallet_cashback
                    SqlCommand cmd = new SqlCommand("Payment_wallet_cashback", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                    cmd.Parameters.AddWithValue("@payment_id", paymentId);
                    cmd.Parameters.AddWithValue("@benefit_id", benefitId);

                    // Execute the stored procedure
                    cmd.ExecuteNonQuery();  // No need to capture the result here

                    // Now, query the Cashback table to get the cashback amount
                    string query = @"
                        SELECT TOP 1 amount 
                        FROM Cashback 
                        WHERE benefitID = @benefit_id AND walletID = 
                            (SELECT w.walletID 
                             FROM Wallet w 
                             INNER JOIN Customer_Account a 
                             ON w.nationalID = a.nationalID 
                             WHERE a.mobileNo = @mobile_no) 
                        ORDER BY cashbackID DESC";  // Get the most recent cashback entry

                    SqlCommand getCashbackCmd = new SqlCommand(query, conn);
                    getCashbackCmd.Parameters.AddWithValue("@benefit_id", benefitId);
                    getCashbackCmd.Parameters.AddWithValue("@mobile_no", mobileNo);

                    object result = getCashbackCmd.ExecuteScalar();

                    // Display the cashback amount
                    if (result != null)
                    {
                        CashbackLabel.Text = $"Cashback Amount: {result} units";
                        CashbackLabel.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        CashbackLabel.Text = "No cashback found for the given details.";
                        CashbackLabel.ForeColor = System.Drawing.Color.Red;
                    }

                    // Retrieve and display the updated wallet balance after cashback is applied
                    DisplayCurrentBalance(mobileNo);

                }
                catch (Exception ex)
                {
                    CashbackLabel.Text = $"Error: {ex.Message}";
                    CashbackLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choose_transaction.aspx");
        }
    }
}
