<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargeBalance.aspx.cs" Inherits="YourNamespace.RechargeBalance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recharge Balance</title>
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

        .result {
            font-size: 18px;
            font-weight: bold;
            margin-top: 20px;
            color: #1e90ff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Recharge Balance for Mobile Number
        </div>
        <div class="container">
            <asp:TextBox ID="AmountTextBox" runat="server" Placeholder="Enter Payment Amount" Width="50%"></asp:TextBox>
            <br /><br />

            <!-- Payment Method Dropdown -->
            <asp:DropDownList ID="PaymentMethodDropDown" runat="server" Width="50%">
                <asp:ListItem Text="Select Payment Method" Value="" />
                <asp:ListItem Text="Credit" Value="Credit" />
                <asp:ListItem Text="Cash" Value="Cash" />
            </asp:DropDownList>
            <br /><br />

            <asp:Button ID="RechargeBalanceButton" runat="server" Text="Recharge Balance" CssClass="btn-redirect" OnClick="RechargeBalanceButton_Click" />

            <div class="result">
                <asp:Label ID="ResultLabel" runat="server" Text=""></asp:Label>
            </div>

            <!-- Return Button -->
            <asp:Button ID="ReturnButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="ReturnButton_Click" />
        </div>
    </form>
</body>
</html>
