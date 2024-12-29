<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountPaymentPoints.aspx.cs" Inherits="YourNamespace.AccountPaymentPoints" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Points</title>
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

        .form-group {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
        }

        .form-group label {
            margin-right: 10px;
            font-size: 16px;
        }

        .form-group input {
            padding: 6px;
            font-size: 14px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        .btn-payment {
            background-color: #1e90ff;
            color: white;
            padding: 6px 12px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            margin-left: 10px;
        }

        .btn-payment:hover {
            background-color: #555;
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

        .error {
            color: #e74c3c;
            font-size: 16px;
            margin-top: 15px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Payment Points
        </div>
        <div class="container">
            <div class="form-group">
                <label for="MobileNoInput">Mobile Number:</label>
                <asp:TextBox ID="MobileNoInput" runat="server" placeholder="Enter Mobile Number" CssClass="form-control" />
                <asp:Button ID="GetPaymentPointsButton" runat="server" Text="Get Payment Points" CssClass="btn-payment" OnClick="GetPaymentPointsButton_Click" />
            </div>

            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error" Visible="false"></asp:Label>

            <asp:GridView ID="PaymentPointsGridView" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                OnPageIndexChanging="PaymentPointsGridView_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="transactionCount" HeaderText="Number of Accepted Transactions" />
                    <asp:BoundField DataField="totalPoints" HeaderText="Total Earned Points" />
                </Columns>
            </asp:GridView>

            <br />

            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
