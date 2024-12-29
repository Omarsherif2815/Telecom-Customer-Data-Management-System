<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayCashback.aspx.cs" Inherits="YourNamespace.DisplayCashback" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Get Cashback Amount</title>
    <style>
        body {
            font-family: 'Poppins', sans-serif;
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
            Get Cashback Amount for Payment
        </div>
        <div class="container">
            <asp:DropDownList ID="PaymentIDDropDown" runat="server" CssClass="dropdown" Width="50%"></asp:DropDownList>
            <br /><br />
            <asp:DropDownList ID="BenefitIDDropDown" runat="server" CssClass="dropdown" Width="50%"></asp:DropDownList>
            &nbsp;<asp:Button ID="ShowCashbackButton" runat="server" Text="Get Cashback Amount" CssClass="btn-redirect" OnClick="ShowCashbackButton_Click" />
            <br />
            <br />
            <br />
                <asp:Label ID="CashbackLabel" runat="server" Text=""></asp:Label>
            <br />
            <div class="result">
                <asp:Label ID="BalanceLabel" runat="server" Text=""></asp:Label>
                <br />
                <br />
            </div>
            <asp:Button ID="ReturnButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="ReturnButton_Click" />
        </div>
    </form>
</body>
</html>
