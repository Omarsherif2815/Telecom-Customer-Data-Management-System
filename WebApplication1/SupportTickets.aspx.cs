using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class UnresolvedTickets : System.Web.UI.Page
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
                    // Automatically load unresolved tickets for the mobile number
                    LoadUnresolvedTickets(mobileNumber);
                }
            }
        }

        private void LoadUnresolvedTickets(string mobileNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Step 1: Retrieve the National ID using Mobile Number
                    string nationalIdQuery = "SELECT nationalID FROM customer_account WHERE mobileNo = @MobileNo";
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

                    // Step 2: Execute the stored procedure to get the count of unresolved tickets
                    using (SqlCommand cmd = new SqlCommand("Ticket_Account_Customer", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NID", nationalID);

                        // Execute the procedure and capture the scalar result
                        object unresolvedCount = cmd.ExecuteScalar();

                        // Convert the result to a DataTable to bind it to the GridView
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("Unresolved_Tickets_Count", typeof(int));
                        DataRow row = dataTable.NewRow();
                        row["Unresolved_Tickets_Count"] = unresolvedCount != DBNull.Value ? unresolvedCount : 0;
                        dataTable.Rows.Add(row);

                        // Set the DataSource for the GridView
                        UnresolvedTicketsGridView.DataSource = dataTable;
                        UnresolvedTicketsGridView.DataBind();
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

        protected void UnresolvedTicketsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index for the GridView
            UnresolvedTicketsGridView.PageIndex = e.NewPageIndex;

            // Reload unresolved tickets
            string mobileNumber = Session["UserMobileNo"] as string;
            if (!string.IsNullOrEmpty(mobileNumber))
            {
                LoadUnresolvedTickets(mobileNumber); // Reload the tickets
            }
            else
            {
                RedirectToLogin();
            }
        }
    }
}
