﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCustomerPlans.aspx.cs" Inherits="YourNamespace.ViewCustomerPlans" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Accounts and Subscribed Service Plans</title>
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
            width: 90%;
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

        .pagination {
            display: flex;
            justify-content: flex-start; /* Align buttons to the left */
            margin-top: 20px;
        }

        .pagination a {
            color: #1e90ff;
            padding: 8px 16px;
            margin: 0 5px;
            text-decoration: none;
            border: 1px solid #1e90ff;
            border-radius: 5px;
        }

        .pagination a:hover {
            background-color: #1e90ff;
            color: white;
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
            Customer Accounts and Subscribed Service Plans
        </div>
        <div class="container">
            <asp:GridView ID="CustomerPlansGridView" runat="server" AutoGenerateColumns="False" CssClass="gridview" EmptyDataText="No data available." OnPageIndexChanging="CustomerPlansGridView_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="mobileNo" HeaderText="Mobile Number" SortExpression="mobileNo" />
                    <asp:BoundField DataField="pass" HeaderText="Password" SortExpression="pass" />
                    <asp:BoundField DataField="balance" HeaderText="Balance" SortExpression="balance" />
                    <asp:BoundField DataField="account_type" HeaderText="Account Type" SortExpression="account_type" />
                    <asp:BoundField DataField="start_date" HeaderText="Start Date" SortExpression="start_date" />
                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                    <asp:BoundField DataField="points" HeaderText="Points" SortExpression="points" />
                    <asp:BoundField DataField="nationalID" HeaderText="National ID" SortExpression="nationalID" />
                    <asp:BoundField DataField="planID" HeaderText="Plan ID" SortExpression="planID" />
                    <asp:BoundField DataField="name" HeaderText="Plan Name" SortExpression="name" />
                    <asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" />
                    <asp:BoundField DataField="SMS_offered" HeaderText="SMS Offered" SortExpression="SMS_offered" />
                    <asp:BoundField DataField="minutes_offered" HeaderText="Minutes Offered" SortExpression="minutes_offered" />
                    <asp:BoundField DataField="data_offered" HeaderText="Data Offered" SortExpression="data_offered" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                </Columns>
            </asp:GridView>

            <!-- Error Message Label -->
            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error" Visible="false"></asp:Label>

            <!-- Pagination - Align to the left -->
            <div class="pagination">
                &nbsp;
            </div>

            <!-- Redirect Button -->
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
