using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace YourNamespace
{
    public partial class view_cashback_transactions : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the mobile number from the session
                string mobileNumber = Session["UserMobileNo"] as string;

                if (string.IsNullOrEmpty(mobileNumber))
                {
                    // If no mobile number is stored, redirect to login or show an error
                    Response.Write("<div style='color:red;'>No mobile number found. Please log in again.</div>");
                    RedirectToLogin();
                }
                else
                {
                    // Automatically load the cashback transactions for the mobile number
                    LoadCashbackData(mobileNumber);
                }
            }
        }

        private void LoadCashbackData(string mobileNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Step 1: Retrieve the National ID using Mobile Number
                    string nationalIdQuery = "SELECT NationalID FROM Customer_Account WHERE mobileNo = @MobileNo";
                    string nationalID = null;

                    using (SqlCommand cmd = new SqlCommand(nationalIdQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@MobileNo", mobileNumber);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            nationalID = result.ToString();
                        }
                        else
                        {
                            Response.Write("<div style='color:red;'>No customer found for the provided mobile number. Redirecting to login.</div>");
                            RedirectToLogin();
                            return;
                        }
                    }

                    // Step 2: Fetch Cashback Transactions using the National ID
                    string cashbackQuery = "SELECT * FROM dbo.Cashback_Wallet_Customer(@NID)";
                    using (SqlCommand command = new SqlCommand(cashbackQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NID", int.Parse(nationalID));

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            CashbackGridView.DataSource = dataTable;
                            CashbackGridView.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<div style='color:red;'>Error: {ex.Message}</div>");
            }
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choose_transaction.aspx");
        }

        private void RedirectToLogin()
        {
            Response.Redirect("login_customer.aspx");
        }
    }
}
