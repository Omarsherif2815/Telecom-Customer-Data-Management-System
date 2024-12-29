<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenewSubscription.aspx.cs" Inherits="YourNamespace.RenewSubscription" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Renew Subscription</title>
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

        .dropdown {
            margin: 10px 0;
            width: 50%;
            padding: 10px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 5px;
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

        .success {
            color: green;
            font-weight: bold;
        }

        .error {
            color: red;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Renew Subscription for a Plan
        </div>
        <div class="container">
            <!-- Dropdown for Payment Method -->
            <asp:DropDownList ID="PaymentMethodDropDown" runat="server" CssClass="dropdown">
                <asp:ListItem Text="Select Payment Method" Value="" />
                <asp:ListItem Text="Cash" Value="Cash" />
                <asp:ListItem Text="Credit" Value="Credit" />
            </asp:DropDownList>
            <br />

            <!-- Dropdown for Plan ID -->
            <asp:DropDownList ID="PlanIDDropDown" runat="server" CssClass="dropdown">
                <asp:ListItem Text="Select a Plan" Value="" />
            </asp:DropDownList>
            <br />

            <!-- Textbox for Amount -->
            <asp:TextBox ID="AmountTextBox" runat="server" Placeholder="Enter Payment Amount" Width="50%"></asp:TextBox>
            &nbsp;
            <asp:Button ID="RenewSubscriptionButton" runat="server" Text="Renew Subscription" CssClass="btn-redirect" OnClick="RenewSubscriptionButton_Click" />
            <br /><br />

            <!-- Result Label -->
            <asp:Label ID="ResultLabel" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
            <br /><br />

            <!-- Buttons -->
            <br />
            <asp:Button ID="ReturnButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="ReturnButton_Click" />
        </div>
    </form>
</body>
</html>
