﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view_service_plans.aspx.cs" Inherits="YourNamespace.view_service_plans" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View All Service Plans</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            View All Service Plans
        </div>
        <div class="container">
            <asp:GridView ID="ServicePlansGridView" runat="server" CssClass="gridview" AutoGenerateColumns="True" 
                OnPageIndexChanging="ServicePlansGridView_PageIndexChanging" EmptyDataText="No service plans found.">
            </asp:GridView>
            
            <asp:Button ID="ProceedButton" runat="server" Text="Proceed" CssClass="btn-redirect" OnClick="ProceedButton_Click" />

            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
