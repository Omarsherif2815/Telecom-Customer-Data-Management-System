using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class DisplayRemainingAmount : System.Web.UI.Page
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

        // Populate the plan dropdown from the database
        // Populate the plan dropdown from the database
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

        // Show remaining amount when the button is clicked
        protected void ShowRemainingAmountButton_Click(object sender, EventArgs e)
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
                    Response.Write("<script>alert('Please select a plan.');</script>");
                    return;
                }

                // Show the remaining amount for the selected plan
                ShowRemainingAmount(mobileNo, selectedPlanName);
            }
        }

        // Function to get remaining amount based on plan name and mobile number
        private void ShowRemainingAmount(string mobileNo, string planName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Call the SQL function Remaining_plan_amount to get the remaining amount
                    string functionQuery = "SELECT dbo.Remaining_plan_amount(@MobileNo, @PlanName)";

                    using (SqlCommand cmd = new SqlCommand(functionQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                        cmd.Parameters.AddWithValue("@PlanName", planName);

                        // Execute the function and retrieve the result
                        var result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            int remainingAmount = Convert.ToInt32(result);

                            // Display the remaining amount in the label
                            lblRemainingAmount.Text = "Remaining Amount: " + remainingAmount.ToString();
                        }
                        else
                        {
                            lblRemainingAmount.Text = "No remaining amount found for this account or plan.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblRemainingAmount.Text = $"Error: {ex.Message}";
            }
        }

        // Redirect button click event
        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            // Redirect to the desired page
            Response.Redirect("choose_transaction.aspx");
        }
    }
}
