<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigationMenu.aspx.cs" Inherits="YourNamespace.NavigationMenu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Navigation Menu</title>
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
            flex: 1; /* Occupies the space below the header */
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
            gap: 10px; /* Reduced gap between buttons */
            margin-top: 20px; /* Adjusted margin above the button grid */
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
            Navigation Menu
        </div>
        <div class="main-container">
            <div class="container">
                <h2>Select an Option to view:</h2>
                <div class="button-grid">
                    <asp:Button ID="btnCustomerProfile" runat="server" Text="Active Accounts" CssClass="btn-nav" OnClick="btnCustomerProfile_Click" />
                    <asp:Button ID="btnPhysicalStores" runat="server" Text="Physical Stores redeemed vouchers" CssClass="btn-nav" OnClick="btnPhysicalStores_Click" />
                    <asp:Button ID="btnResolvedTickets" runat="server" Text="Resolved Tickets" CssClass="btn-nav" OnClick="btnResolvedTickets_Click" />
                    <asp:Button ID="btnSubscribedPlans" runat="server" Text="Accounts' Subscribed Plans" CssClass="btn-nav" OnClick="btnSubscribedPlans_Click" />
                    <asp:Button ID="btnPlanSubscription" runat="server" Text="Plan subscribed Accounts" CssClass="btn-nav" OnClick="btnPlanSubscription_Click" />
                    <asp:Button ID="btnTotalUsage" runat="server" Text="Account Total Usage" CssClass="btn-nav" OnClick="btnTotalUsage_Click" />
                    <asp:Button ID="btnRemoveBenefits" runat="server" Text="Remove Benefits" CssClass="btn-nav" OnClick="btnRemoveBenefits_Click" />
                    <asp:Button ID="btnSMSOffered" runat="server" Text="SMS Offered" CssClass="btn-nav" OnClick="btnSMSOffered_Click" />
                    <asp:Button ID="btnCustomerWallet" runat="server" Text="Customer Wallet" CssClass="btn-nav" OnClick="btnCustomerWallet_Click" />
                    <asp:Button ID="btnEShops" runat="server" Text="E-Shops redeemed vouchers" CssClass="btn-nav" OnClick="btnEShops_Click" />
                    <asp:Button ID="btnPaymentTransactions" runat="server" Text="Payment Transactions details" CssClass="btn-nav" OnClick="btnPaymentTransactions_Click" />
                    <asp:Button ID="btnWalletCashback" runat="server" Text="Wallet Cashback" CssClass="btn-nav" OnClick="btnWalletCashback_Click" />
                    <asp:Button ID="btnAcceptedPayments" runat="server" Text="Accepted Payments" CssClass="btn-nav" OnClick="btnAcceptedPayments_Click" />
                    <asp:Button ID="btnCashbackReturned" runat="server" Text="Cashback Returned" CssClass="btn-nav" OnClick="btnCashbackReturned_Click" />
                    <asp:Button ID="btnAvgTransactions" runat="server" Text="Average Transactions" CssClass="btn-nav" OnClick="btnAvgTransactions_Click" />
                    <asp:Button ID="btnWalletLink" runat="server" Text="Wallet Link" CssClass="btn-nav" OnClick="btnWalletLink_Click" />
                    <asp:Button ID="btnUpdatePoints" runat="server" Text="Update Points" CssClass="btn-nav" OnClick="btnUpdatePoints_Click" />
                    <!-- Logout Button -->
                    <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn-nav" OnClick="btnLogout_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
