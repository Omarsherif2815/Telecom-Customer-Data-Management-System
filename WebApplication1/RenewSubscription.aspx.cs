using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class RenewSubscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the mobile number from the session
                string mobileNo = Session["UserMobileNo"] as string;

                if (string.IsNullOrEmpty(mobileNo))
                {
                    // Redirect to login if mobile number is not available
                    Response.Write("<script>alert('No mobile number found. Please log in again.');</script>");
                    Response.Redirect("login_customer.aspx");
                }
                else
                {
                    PopulatePlanDropdown(mobileNo);
                }
            }
        }

        private void PopulatePlanDropdown(string mobileNo)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Query to get active plans for the user
                    string query = @"
                        SELECT DISTINCT s.planID 
                        FROM Subscription s 
                        INNER JOIN Customer_Account c ON s.mobileNo = c.mobileNo 
                        WHERE c.mobileNo = @mobileNo";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mobileNo", mobileNo);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    PlanIDDropDown.DataSource = dt;
                    PlanIDDropDown.DataTextField = "planID";
                    PlanIDDropDown.DataValueField = "planID";
                    PlanIDDropDown.DataBind();

                    // Add a default "Select Plan" option
                    PlanIDDropDown.Items.Insert(0, new ListItem("Select a Plan", ""));
                }
                catch (Exception ex)
                {
                    ResultLabel.Text = $"Error loading plans: {ex.Message}";
                    ResultLabel.CssClass = "error";
                }
            }
        }

        protected void RenewSubscriptionButton_Click(object sender, EventArgs e)
        {
            string mobileNo = Session["UserMobileNo"] as string;
            string amountText = AmountTextBox.Text.Trim();
            string paymentMethod = PaymentMethodDropDown.SelectedValue;
            string planIdText = PlanIDDropDown.SelectedValue;

            if (string.IsNullOrEmpty(amountText) || string.IsNullOrEmpty(paymentMethod) || string.IsNullOrEmpty(planIdText))
            {
                ResultLabel.Text = "Error: All fields are required. Please provide valid input.";
                ResultLabel.CssClass = "error";
                return;
            }

            if (!int.TryParse(amountText, out int amount) || amount <= 0)
            {
                // Show error if amount is not a valid integer
                ResultLabel.Text = "Payment amount must be an integer value.";
                ResultLabel.CssClass = "error";
                return;
            }

            if (!int.TryParse(planIdText, out int planId))
            {
                ResultLabel.Text = "Error: Invalid plan ID. Please provide a correct value.";
                ResultLabel.CssClass = "error";
                return;
            }

            RenewSubscriptionForPlan(mobileNo, amount, paymentMethod, planId);
        }

        private void RenewSubscriptionForPlan(string mobileNo, decimal amount, string paymentMethod, int planId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "EXEC Initiate_plan_payment @mobile_num, @amount, @payment_method, @plan_id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mobile_num", mobileNo);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@payment_method", paymentMethod);
                    cmd.Parameters.AddWithValue("@plan_id", planId);

                    cmd.ExecuteNonQuery();

                    ResultLabel.Text = "Subscription renewed successfully!";
                    ResultLabel.CssClass = "success";
                }
                catch (Exception ex)
                {
                    ResultLabel.Text = $"Error: {ex.Message}";
                    ResultLabel.CssClass = "error";
                }
            }
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("choose_transaction.aspx");
        }
    }
}
