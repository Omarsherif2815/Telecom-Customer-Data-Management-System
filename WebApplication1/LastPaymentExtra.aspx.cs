using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class DisplayExtraAmount : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the mobile number from session
                string mobileNo = Session["UserMobileNo"] as string;

                // If no mobile number is found, redirect to login
                if (string.IsNullOrEmpty(mobileNo))
                {
                    Response.Write("<script>alert('No mobile number found. Please log in again.');</script>");
                    Response.Redirect("login_customer.aspx");
                }
                else
                {
                    // Populate the plan dropdown from the database
                    PopulatePlanDropdown();
                }
            }
        }

        private void PopulatePlanDropdown()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve the mobile number from session
                    string mobileNo = Session["UserMobileNo"] as string;

                    if (string.IsNullOrEmpty(mobileNo))
                    {
                        Response.Write("<script>alert('No mobile number found. Please log in again.');</script>");
                        Response.Redirect("login_customer.aspx");
                        return;
                    }

                    // SQL query to retrieve active plans linked to the specific mobile number
                    string query = @"
                                    SELECT DISTINCT p.name 
                                    FROM Service_plan p 
                                    INNER JOIN Subscription s ON p.planID = s.planID
                                    WHERE s.mobileNo = @MobileNo";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MobileNo", mobileNo);

                        SqlDataReader reader = cmd.ExecuteReader();
                        PlanNameDropDown.DataSource = reader;
                        PlanNameDropDown.DataTextField = "name";
                        PlanNameDropDown.DataValueField = "name";
                        PlanNameDropDown.DataBind();
                    }

                    // Add default "Please select a plan" option
                    PlanNameDropDown.Items.Insert(0, new ListItem("Please select a plan", ""));
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }


        protected void ShowExtraAmountButton_Click(object sender, EventArgs e)
        {
            // Retrieve the mobile number from session
            string mobileNo = Session["UserMobileNo"] as string;

            // If no mobile number is found, redirect to login
            if (string.IsNullOrEmpty(mobileNo))
            {
                Response.Write("<script>alert('No mobile number found. Please log in again.');</script>");
                Response.Redirect("login_customer.aspx");
            }
            else
            {
                // Get the selected plan name from the dropdown
                string selectedPlanName = PlanNameDropDown.SelectedValue;

                if (string.IsNullOrEmpty(selectedPlanName))
                {
                    ExtraAmountLabel.Text = "Please select a plan.";
                    return;
                }

                // Show the extra amount for the selected plan
                GetExtraAmountForLastPayment(mobileNo, selectedPlanName);
            }
        }

        private void GetExtraAmountForLastPayment(string mobileNo, string planName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Call the SQL function Extra_plan_amount to get the extra amount
                    string query = "SELECT dbo.Extra_plan_amount(@mobile_num, @plan_name) AS ExtraAmount";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                        cmd.Parameters.AddWithValue("@plan_name", planName);

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            ExtraAmountLabel.Text = $"Extra Amount: {result} units";
                        }
                        else
                        {
                            ExtraAmountLabel.Text = "No extra amount found for the specified input.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExtraAmountLabel.Text = $"Error: {ex.Message}";
            }
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choose_transaction.aspx");
        }
    }
}
