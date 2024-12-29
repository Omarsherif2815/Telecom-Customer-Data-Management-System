using System;
using System.Web.UI;

namespace YourNamespace
{
    public partial class ChooseLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            // Redirect to the Admin login page
            Response.Redirect("login_user.aspx");
        }

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
            // Redirect to the Customer login page
            Response.Redirect("ViewPlans.aspx");
        }
    }
}
