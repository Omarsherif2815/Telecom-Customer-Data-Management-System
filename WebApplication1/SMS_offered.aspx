<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account_SMS_Offers.aspx.cs" Inherits="YourNamespace.Account_SMS_Offers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer SMS Offers</title>
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

        .form-control {
            padding: 10px;
            margin-top: 10px;
            margin-bottom: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .btn-submit {
            background-color: #1e90ff;
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-submit:hover {
            background-color: #555;
        }

        .error {
            color: #e74c3c;
            font-size: 16px;
            margin-top: 10px;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .gridview th, .gridview td {
            padding: 12px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Customer SMS Offers
        </div>
        <div class="container">
            <label for="MobileNoInput">Mobile Number:</label>
            <asp:TextBox ID="MobileNoInput" runat="server" CssClass="form-control" placeholder="Enter Mobile Number" Width="424px"></asp:TextBox>

            <asp:Button ID="GetSMSOffersButton" runat="server" Text="Get SMS Offers" CssClass="btn-submit" OnClick="GetSMSOffersButton_Click" />

            <br />

            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error" Visible="false"></asp:Label>

            <asp:GridView ID="SMSOffersGridView" runat="server" CssClass="gridview" AutoGenerateColumns="True" OnPageIndexChanging="SMSOffersGridView_PageIndexChanging"></asp:GridView>

            <br />

            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-submit" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
