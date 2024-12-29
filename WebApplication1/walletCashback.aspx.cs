using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace YourNamespace
{
    public partial class ViewCashbackTransactions : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCashbackTransactions();
            }
        }

        private void LoadCashbackTransactions()
        {
            // Retrieve the connection string from web.config
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            string query = "SELECT walletID, [count of transactions] FROM Num_of_cashback";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open(); // Open the connection
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Bind the data to the GridView
                        CashbackTransactionsGridView.DataSource = dt;
                        CashbackTransactionsGridView.DataBind();
                    }
                    catch (Exception ex)
                    {
                        // Handle errors (Optional: Log them for debugging)
                        ErrorMessageLabel.Text = "Error loading data: " + ex.Message;
                        ErrorMessageLabel.Visible = true;
                    }
                }
            }
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            // Redirect to another page
            Response.Redirect("choosing_view.aspx");
        }
    }
}
