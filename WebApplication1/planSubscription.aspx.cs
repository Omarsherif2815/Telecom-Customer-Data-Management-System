using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace YourNamespace
{
    public partial class AccountPlanDate : Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Programmatically add the OnFocus attribute to the text box for SubDateInput
            if (!IsPostBack)
            {
                SubDateInput.Attributes.Add("onfocus", "showCalendar();");
            }
        }

        // Event handler for the search button click
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string planID = planIDInput.Text.Trim();
            string subDate = SubDateInput.Text.Trim();

            if (string.IsNullOrEmpty(planID) || string.IsNullOrEmpty(subDate))
            {
                ErrorMessageLabel.Text = "Please provide both Plan ID and Subscription Date.";
                ErrorMessageLabel.Visible = true;
                return;
            }

            // Check if the Plan ID exists in the service_plan table
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand checkPlanCmd = new SqlCommand("SELECT COUNT(*) FROM service_plan WHERE planID = @planID", conn);
                    checkPlanCmd.Parameters.AddWithValue("@planID", int.Parse(planID));

                    int planExists = (int)checkPlanCmd.ExecuteScalar();

                    if (planExists == 0)
                    {
                        ErrorMessageLabel.Text = "The Plan ID does not exist.";
                        ErrorMessageLabel.Visible = true;
                        return;
                    }
                }

                // Calling the stored procedure with valid data
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.Account_Plan_date(@sub_date, @plan_id)", conn);

                    da.SelectCommand.Parameters.AddWithValue("@sub_date", DateTime.Parse(subDate));
                    da.SelectCommand.Parameters.AddWithValue("@plan_id", int.Parse(planID));

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    CustomerPlansGridView.DataSource = dt;
                    CustomerPlansGridView.DataBind();
                    ErrorMessageLabel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "The plan id should be an integer " ;
                ErrorMessageLabel.Visible = true;
            }
        }

        // Event handler for when the calendar date is selected
        protected void SubDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            SubDateInput.Text = SubDateCalendar.SelectedDate.ToString("yyyy-MM-dd");
            SubDateCalendar.Visible = false;  // Hide calendar after date selection
        }

        // GridView pagination
        protected void CustomerPlansGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            CustomerPlansGridView.PageIndex = e.NewPageIndex;
            SearchButton_Click(sender, e); // Re-run the search with pagination
        }

        // Redirect functionality
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choosing_view.aspx");
        }
    }
}
