using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class RedeemVoucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Removed logic for displaying mobile number
                string mobileNo = Session["UserMobileNo"] as string; // Get mobile number from session
                if (string.IsNullOrEmpty(mobileNo))
                {
                    Response.Redirect("login_customer.aspx"); // Redirect if no mobile number is found
                }

                // Load Voucher dropdown
                LoadVoucherDropdown();
            }
        }

        private void LoadVoucherDropdown()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT voucherID FROM Voucher WHERE expiry_date > CURRENT_TIMESTAMP";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    VoucherIDDropDown.Items.Clear();
                    VoucherIDDropDown.Items.Add(new System.Web.UI.WebControls.ListItem("Select Voucher", ""));

                    while (reader.Read())
                    {
                        VoucherIDDropDown.Items.Add(new System.Web.UI.WebControls.ListItem(reader["voucherID"].ToString(), reader["voucherID"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    ResultLabel.Text = $"Error loading vouchers: {ex.Message}";
                    ResultLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void RedeemVoucherButton_Click(object sender, EventArgs e)
        {
            int voucherId;

            // Ensure that the selected value is a valid voucherID
            if (int.TryParse(VoucherIDDropDown.SelectedValue, out voucherId) && voucherId > 0)
            {
                // Call the method to redeem the voucher points
                string mobileNo = Session["UserMobileNo"] as string;
                RedeemVoucherPoints(mobileNo, voucherId);
            }
            else
            {
                ResultLabel.Text = "Please select a valid Voucher ID.";
                ResultLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void RedeemVoucherPoints(string mobileNo, int voucherId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Attach the InfoMessage event handler to capture SQL messages
                conn.InfoMessage += new SqlInfoMessageEventHandler(OnSqlInfoMessage);

                try
                {
                    conn.Open();

                    // Call the stored procedure to redeem voucher points
                    string query = "EXEC Redeem_voucher_points @mobile_num, @voucher_id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                    cmd.Parameters.AddWithValue("@voucher_id", voucherId);

                    // Execute the stored procedure
                    cmd.ExecuteNonQuery();

                    // If we reach here, it means the process was successful
                    // We do not display a success message here as we rely on the InfoMessage event
                    if (string.IsNullOrEmpty(ResultLabel.Text))
                    {
                        ResultLabel.Text = "Voucher redeemed successfully!";
                        ResultLabel.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception ex)
                {
                    // If there was an error, display the error message
                    ResultLabel.Text = $"Error: {ex.Message}";
                    ResultLabel.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    conn.InfoMessage -= new SqlInfoMessageEventHandler(OnSqlInfoMessage);
                }
            }
        }

        // Event handler for capturing SQL messages (e.g., 'no enough points to redeem voucher')
        private void OnSqlInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            // If there are any messages from SQL Server, display them
            ResultLabel.Text = e.Message;
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }

        // Redirect to the homepage
        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choose_transaction.aspx"); // Replace with your actual homepage URL
        }
    }
}
