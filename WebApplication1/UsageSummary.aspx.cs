using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class view_plan_consumption : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the DropDownList with plan names from the Service_plan table
                PopulatePlanNames();
            }
        }

        private void PopulatePlanNames()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT name FROM Service_plan"; // Adjust to your actual column name
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            PlanNameInput.Items.Clear();
                            PlanNameInput.Items.Add(new ListItem("Select a Plan", ""));  // Default option

                            while (reader.Read())
                            {
                                PlanNameInput.Items.Add(new ListItem(reader["name"].ToString(), reader["name"].ToString()));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<div style='color:red;'>Error: {ex.Message}</div>");
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string planName = PlanNameInput.SelectedValue.Trim();

            // Check if the user has selected a plan other than the default
            if (string.IsNullOrEmpty(planName))
            {
                Response.Write("<div style='color:red;'>Please select a plan.</div>");
                return;
            }

            // Get the selected dates from TextBox controls (StartDateInput and EndDateInput)
            DateTime startDate, endDate;

            // Validate and parse Start Date
            if (!DateTime.TryParse(StartDateInput.Text.Trim(), out startDate))
            {
                Response.Write("<div style='color:red;'>Please enter a valid Start Date.</div>");
                return;
            }

            // Validate and parse End Date
            if (!DateTime.TryParse(EndDateInput.Text.Trim(), out endDate))
            {
                Response.Write("<div style='color:red;'>Please enter a valid End Date.</div>");
                return;
            }

            // Validate that both dates are not empty
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                Response.Write("<div style='color:red;'>Please fill in all fields and select valid dates.</div>");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM dbo.Consumption(@PlanName, @StartDate, @EndDate)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters for the SQL query
                        command.Parameters.AddWithValue("@PlanName", planName);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        // Execute the query and fill the results into the GridView
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            ConsumptionGridView.DataSource = dataTable;
                            ConsumptionGridView.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display error message
                Response.Write($"<div style='color:red;'>Error: {ex.Message}</div>");
            }
        }

        protected void RedirectButton_Click(object sender, EventArgs e)
        {
            // Redirect to another page
            Response.Redirect("choose_transaction.aspx");
        }
    }
}
