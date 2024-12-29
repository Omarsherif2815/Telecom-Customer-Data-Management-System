using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace YourNamespace
{
    public partial class VoucherHighestValue : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Try to retrieve the mobile number from the session
                string mobileNo = Session["UserMobileNo"] as string;

                // If no mobile number is found, redirect to the login page
                if (string.IsNullOrEmpty(mobileNo))
                {
                    Response.Write("<script>alert('No mobile number found. Please log in again.');</script>");
                    Response.Redirect("login_customer.aspx");
                }
                else
                {
                    // If the mobile number is found, proceed to fetch the highest voucher
                    ShowHighestVoucher(mobileNo);
                }
            }
        }

        private void ShowHighestVoucher(string mobileNo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Call the stored procedure to get the highest voucher for the provided mobile number
                    string procedureQuery = "EXEC Account_Highest_Voucher @MobileNo";
                    using (SqlCommand cmd = new SqlCommand(procedureQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@MobileNo", mobileNo);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Check if data exists
                        if (dt.Rows.Count > 0)
                        {
                            // Bind the result to the GridView
                            HighestVoucherGridView.DataSource = dt;
                            HighestVoucherGridView.DataBind();
                        }
                        else
                        {
                            // No data found
                            Response.Write("<script>alert('No voucher data found for the provided mobile number.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and show error messages
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            // Redirect to the desired page
            Response.Redirect("choose_transaction.aspx");
        }
    }
}
