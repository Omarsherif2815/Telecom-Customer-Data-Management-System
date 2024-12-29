<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WalletAverageTransaction.aspx.cs" Inherits="YourNamespace.WalletAverageTransaction" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Average Transaction Amount</title>
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

        .result {
            margin-top: 20px;
            font-size: 18px;
            font-weight: bold;
            color: #1e90ff;
        }

        .error {
            color: #e74c3c;
            font-size: 16px;
            margin-top: 15px;
        }
    </style>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#StartDate').datepicker({
                dateFormat: 'yy-mm-dd',
                showAnim: 'fadeIn'
            }).prop('readonly', true);

            $('#EndDate').datepicker({
                dateFormat: 'yy-mm-dd',
                showAnim: 'fadeIn'
            }).prop('readonly', true);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Average Transaction Amount
        </div>
        <div class="container">
            <div class="form-group">
                <label for="WalletID">Wallet ID:</label>
                <asp:TextBox ID="WalletID" runat="server" placeholder="Enter Wallet ID" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="StartDate">Start Date:</label>
                <asp:TextBox ID="StartDate" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="EndDate">End Date:</label>
                <asp:TextBox ID="EndDate" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Button ID="GetAverageButton" runat="server" Text="Get Average" CssClass="btn-submit" OnClick="GetAverageButton_Click" />
                <asp:Button ID="ReturnButton" runat="server" Text="Return" CssClass="btn-return" OnClick="ReturnButton_Click" />
            </div>
            <asp:Label ID="ResultLabel" runat="server" CssClass="result" Visible="false"></asp:Label>
            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
