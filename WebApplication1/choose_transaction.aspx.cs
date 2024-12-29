using System;
using System.Web.UI;

namespace WebApplication1
{
    public partial class choose_transaction : Page
    {

        protected void btnUsageSummary_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsageSummary.aspx");
        }

        protected void btnUnsubscribedPlans_Click(object sender, EventArgs e)
        {
            Response.Redirect("UnsubscribedPlans.aspx");
        }

        protected void btnActivePlans_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActivePlans.aspx");
        }

        protected void btnCashbackTransactions_Click(object sender, EventArgs e)
        {
            Response.Redirect("CashbackTransactions.aspx");
        }

        protected void btnActiveBenefits_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActiveBenefits.aspx");
        }

        protected void btnSupportTickets_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupportTickets.aspx");
        }

        protected void btnHighestVoucher_Click(object sender, EventArgs e)
        {
            Response.Redirect("HighestVoucher.aspx");
        }

        protected void btnLastPaymentRemaining_Click(object sender, EventArgs e)
        {
            Response.Redirect("LastPaymentRemaining.aspx");
        }

        protected void btnLastPaymentExtra_Click(object sender, EventArgs e)
        {
            Response.Redirect("LastPaymentExtra.aspx");
        }

        protected void btnTopPayments_Click(object sender, EventArgs e)
        {
            Response.Redirect("TopPayments.aspx");
        }

        protected void btnAllShops_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllShops.aspx");
        }

        protected void btnRecentPlans_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecentPlans.aspx");
        }

        protected void btnRenewSubscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("RenewSubscription.aspx");
        }

        protected void btnCashbackEstimate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CashbackEstimate.aspx");
        }

        protected void btnRechargeBalance_Click(object sender, EventArgs e)
        {
            Response.Redirect("RechargeBalance.aspx");
        }

        protected void btnRedeemVoucher_Click(object sender, EventArgs e)
        {
            Response.Redirect("RedeemVoucher.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("user_customer.aspx");
        }
    }
}
