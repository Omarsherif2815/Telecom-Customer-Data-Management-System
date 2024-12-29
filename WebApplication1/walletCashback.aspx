<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCashbackTransactions.aspx.cs" Inherits="YourNamespace.ViewCashbackTransactions" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cashback Transactions</title>
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
            background: #f0f8ff;
            color: #333;
        }

        .header {
            background-color: #1e90ff;
            color: white;
            padding: 20px;
            text-align: center;
            font-size: 24px;
            font-weight: bold;
        }

        .container {
            width: 80%;
            margin: 30px auto;
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .gridview th, .gridview td {
            padding: 12px;
            text-align: left;
            border: 1px solid #ddd;
        }

        .gridview th {
            background-color: #1e90ff;
            color: white;
        }

        .gridview tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .gridview tr:hover {
            background-color: #e0f7fa;
        }

        .btn-redirect {
            background-color: #1e90ff;
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            margin-top: 20px;
        }

        .btn-redirect:hover {
            background-color: #555;
        }

        .error-label {
            color: #e74c3c;
            font-size: 14px;
            margin-top: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Cashback Transactions per Wallet
        </div>
        <div class="container">
            <!-- GridView for displaying transactions -->
            <asp:GridView ID="CashbackTransactionsGridView" runat="server" CssClass="gridview" AutoGenerateColumns="False" 
            EmptyDataText="No cashback transactions found." Visible="true">
            <Columns>
            <asp:BoundField DataField="walletID" HeaderText="Wallet ID" />
            <asp:BoundField DataField="count of transactions" HeaderText="Number of Transactions" />
            </Columns>
            </asp:GridView>

            <!-- Error Message Label -->
            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error-label" Visible="false"></asp:Label>

            <!-- Redirect Button -->
            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
