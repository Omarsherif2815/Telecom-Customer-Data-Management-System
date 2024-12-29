using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class ActiveBenefits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadActiveBenefits();
            }
        }

        private void LoadActiveBenefits()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT benefitID, description, validity_date, status, mobileNo FROM Benefits WHERE status = 'Active'";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ActiveBenefitsGridView.DataSource = dt;
                    ActiveBenefitsGridView.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }
            }
        }

        protected void ActiveBenefitsGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            ActiveBenefitsGridView.PageIndex = e.NewPageIndex;
            LoadActiveBenefits();
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            // Redirect to a specific page (adjust the URL as needed)
            Response.Redirect("choose_transaction.aspx");
        }
    }
}
