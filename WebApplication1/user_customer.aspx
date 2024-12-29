<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_customer.aspx.cs" Inherits="YourNamespace.ChooseLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Selection</title>
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
            background: #f0f8ff;
            color: #333;
            height: 100vh; /* Full viewport height */
            display: flex;
            flex-direction: column;
        }

        .header {
            background-color: #1e90ff;
            color: white;
            padding: 20px;
            text-align: center;
            font-size: 24px;
            font-weight: bold;
        }

        .main-container {
            flex: 1; /* Occupies the space below the header */
            display: flex;
            justify-content: center; /* Centers horizontally */
            align-items: center; /* Centers vertically */
        }

        .container {
            text-align: center;
            background: white;
            padding: 40px;
            border-radius: 8px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
        }

        .login-text {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 30px;
        }

        .btn-login {
            background-color: #1e90ff;
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 18px;
            margin: 10px;
            width: 150px;
        }

        .btn-login:hover {
            background-color: #555;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Choose Login Type
        </div>
        <div class="main-container">
            <div class="container">
                <br />
                <div class="login-text">
                    Login as:
                </div>
                <asp:Button ID="btnAdmin" runat="server" Text="Admin" CssClass="btn-login" OnClick="btnAdmin_Click" />
                <asp:Button ID="btnCustomer" runat="server" Text="Customer" CssClass="btn-login" OnClick="btnCustomer_Click" />
            </div>
        </div>
    </form>
</body>
</html>
