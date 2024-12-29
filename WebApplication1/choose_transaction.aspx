<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="choose_transaction.aspx.cs" Inherits="WebApplication1.choose_transaction" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Choose Transaction</title>
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
            background: #f0f8ff;
            color: #333;
            height: 100vh;
            display: flex;
            flex-direction: column;
        }

        .header {
            background-color: #1e90ff;
            color: white;
            padding: 20px;
            text-align: center;
            font-size: 24px;
            font-weight: bold;
        }

        .main-container {
            flex: 1;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .container {
            width: 80%;
            background: white;
            padding: 40px;
            border-radius: 8px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        .button-grid {
            display: grid;
            grid-template-columns: 1fr 1fr; /* Two columns */
            gap: 10px;
            margin-top: 20px;
        }

        .btn-nav {
            background-color: #1e90ff;
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 18px;
        }

        .btn-nav:hover {
            background-color: #555;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Choose Transaction
        </div>
        <div class="main-container">
            <div class="container">
                <h2>Select an Option to view:</h2>
                <div class="button-grid">
                    <asp:Button ID="btnUsageSummary" runat="server" Text="View Usage Summary" CssClass="btn-nav" OnClick="btnUsageSummary_Click" />
                    <asp:Button ID="btnUnsubscribedPlans" runat="server" Text="Unsubscribed Plans" CssClass="btn-nav" OnClick="btnUnsubscribedPlans_Click" />
                    <asp:Button ID="btnActivePlans" runat="server" Text="Active Plan Usage" CssClass="btn-nav" OnClick="btnActivePlans_Click" />
                    <asp:Button ID="btnCashbackTransactions" runat="server" Text="Cashback Transactions" CssClass="btn-nav" OnClick="btnCashbackTransactions_Click" />
                    <asp:Button ID="btnActiveBenefits" runat="server" Text="Active Benefits" CssClass="btn-nav" OnClick="btnActiveBenefits_Click" />
                    <asp:Button ID="btnSupportTickets" runat="server" Text="Unresolved Tickets" CssClass="btn-nav" OnClick="btnSupportTickets_Click" />
                    <asp:Button ID="btnHighestVoucher" runat="server" Text="Highest Value Voucher" CssClass="btn-nav" OnClick="btnHighestVoucher_Click" />
                    <asp:Button ID="btnLastPaymentRemaining" runat="server" Text="Remaining Last Payment" CssClass="btn-nav" OnClick="btnLastPaymentRemaining_Click" />
                    <asp:Button ID="btnLastPaymentExtra" runat="server" Text="Extra Last Payment" CssClass="btn-nav" OnClick="btnLastPaymentExtra_Click" />
                    <asp:Button ID="btnTopPayments" runat="server" Text="Top 10 Payments" CssClass="btn-nav" OnClick="btnTopPayments_Click" />
                    <asp:Button ID="btnAllShops" runat="server" Text="All Shops" CssClass="btn-nav" OnClick="btnAllShops_Click" />
                    <asp:Button ID="btnRecentPlans" runat="server" Text="Recent Plans (5 Months)" CssClass="btn-nav" OnClick="btnRecentPlans_Click" />
                    <asp:Button ID="btnRenewSubscription" runat="server" Text="Renew Subscription" CssClass="btn-nav" OnClick="btnRenewSubscription_Click" />
                    <asp:Button ID="btnCashbackEstimate" runat="server" Text="Cashback Estimate" CssClass="btn-nav" OnClick="btnCashbackEstimate_Click" />
                    <asp:Button ID="btnRechargeBalance" runat="server" Text="Recharge Balance" CssClass="btn-nav" OnClick="btnRechargeBalance_Click" />
                    <asp:Button ID="btnRedeemVoucher" runat="server" Text="Redeem Voucher" CssClass="btn-nav" OnClick="btnRedeemVoucher_Click" />
                    <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="btn-nav" OnClick="btnLogout_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
