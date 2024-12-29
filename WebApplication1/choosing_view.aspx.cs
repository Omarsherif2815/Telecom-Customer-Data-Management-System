using System;
using System.Web.UI;

namespace YourNamespace
{
    public partial class NavigationMenu : Page
    {
        protected void btnCustomerProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("customerProfile_customerAccount.aspx");
        }

        protected void btnPhysicalStores_Click(object sender, EventArgs e)
        {
            Response.Redirect("physicalStores.aspx");
        }

        protected void btnResolvedTickets_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResolvedTickets.aspx");
        }

        protected void btnSubscribedPlans_Click(object sender, EventArgs e)
        {
            Response.Redirect("subscribedPlans.aspx");
        }

        protected void btnPlanSubscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("planSubscription.aspx");
        }

        protected void btnTotalUsage_Click(object sender, EventArgs e)
        {
            Response.Redirect("totalUsage.aspx");
        }

        protected void btnRemoveBenefits_Click(object sender, EventArgs e)
        {
            Response.Redirect("removeBenefits.aspx");
        }

        protected void btnSMSOffered_Click(object sender, EventArgs e)
        {
            Response.Redirect("SMS_offered.aspx");
        }

        protected void btnCustomerWallet_Click(object sender, EventArgs e)
        {
            Response.Redirect("customer_wallet.aspx");
        }

        protected void btnEShops_Click(object sender, EventArgs e)
        {
            Response.Redirect("EShops.aspx");
        }

        protected void btnPaymentTransactions_Click(object sender, EventArgs e)
        {
            Response.Redirect("payment_transactions.aspx");
        }

        protected void btnWalletCashback_Click(object sender, EventArgs e)
        {
            Response.Redirect("walletCashback.aspx");
        }

        protected void btnAcceptedPayments_Click(object sender, EventArgs e)
        {
            Response.Redirect("acceptedPayments.aspx");
        }

        protected void btnCashbackReturned_Click(object sender, EventArgs e)
        {
            Response.Redirect("cashbackReturned.aspx");
        }

        protected void btnAvgTransactions_Click(object sender, EventArgs e)
        {
            Response.Redirect("avgTransactions.aspx");
        }

        protected void btnWalletLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("walletLink.aspx");
        }

        protected void btnUpdatePoints_Click(object sender, EventArgs e)
        {
            Response.Redirect("updatePoints.aspx");
        }

        // Logout Redirect
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("user_customer.aspx");
        }
    }
}
