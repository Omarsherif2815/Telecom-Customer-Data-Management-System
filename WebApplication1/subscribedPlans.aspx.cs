using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace YourNamespace
{
    public partial class ViewCustomerPlans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string connString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("Account_Plan", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    CustomerPlansGridView.DataSource = dt;
                    CustomerPlansGridView.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorMessageLabel.Visible = true;
                    ErrorMessageLabel.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void CustomerPlansGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            CustomerPlansGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx"); // Replace with your redirect page
        }
    }
}
