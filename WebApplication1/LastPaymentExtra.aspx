﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayExtraAmount.aspx.cs" Inherits="YourNamespace.DisplayExtraAmount" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Extra Amount for Last Payment</title>
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

        .result {
            font-size: 18px;
            font-weight: bold;
            margin-top: 20px;
            color: #1e90ff;
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
            Display Extra Amount for Last Payment
        </div>
        <div class="container">
            <!-- Dropdown for selecting the plan -->
            <asp:DropDownList ID="PlanNameDropDown" runat="server" CssClass="dropdown">
                <asp:ListItem Text="Please select a plan" Value="" />
            </asp:DropDownList>

            <asp:Button ID="ShowExtraAmountButton" runat="server" Text="Show Extra Amount" CssClass="btn-redirect" OnClick="ShowExtraAmountButton_Click" />
            
            <div class="result">
                <asp:Label ID="ExtraAmountLabel" runat="server" Text=""></asp:Label>
            </div>

            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>