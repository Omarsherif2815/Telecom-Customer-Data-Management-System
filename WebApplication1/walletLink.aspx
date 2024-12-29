<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckWalletMobile.aspx.cs" Inherits="YourNamespace.CheckWalletMobile" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check Wallet Link</title>
    <style>
        /* Reusing the styles from the provided design */
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

        .btn-submit, .btn-return {
            background-color: #1e90ff;
            color: white;
            padding: 6px 12px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            margin-left: 10px;
        }

        .btn-submit:hover, .btn-return:hover {
            background-color: #555;
        }

        /* Blue message (result) */
        .result {
            margin-top: 20px;
            font-size: 18px;
            font-weight: bold;
            color: #1e90ff; /* Blue color */
        }

        /* Red message (error) */
        .error {
            color: #e74c3c; /* Red color */
            font-size: 16px;
            margin-top: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Check Wallet Link
        </div>
        <div class="container">
            <!-- Mobile Number Input -->
            <div class="form-group">
                <label for="MobileNumber">Mobile Number:</label>
                <asp:TextBox ID="MobileNumber" runat="server" placeholder="Enter Mobile Number" CssClass="form-control" />
            </div>

            <!-- Submit and Return Buttons -->
            <div class="form-group">
                <asp:Button ID="CheckButton" runat="server" Text="Check Wallet" CssClass="btn-submit" OnClick="CheckButton_Click" />
                <asp:Button ID="ReturnButton" runat="server" Text="Return" CssClass="btn-return" OnClick="ReturnButton_Click" />
            </div>

            <!-- Result and Error Messages -->
            <asp:Label ID="ResultLabel" runat="server" CssClass="result" Visible="false"></asp:Label>
            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
