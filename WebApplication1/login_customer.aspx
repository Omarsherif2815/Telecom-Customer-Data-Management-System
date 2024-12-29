<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login_customer.aspx.cs" Inherits="YourNamespace.login_customer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Login</title>
    <style>
        /* Overall Page Style */
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background: linear-gradient(135deg, #1e90ff, #ff6b6b);
            color: #fff;
        }

        /* Login Box Style */
        .form-container {
            width: 100%;
            max-width: 400px;
            background: rgba(255, 255, 255, 0.15);
            backdrop-filter: blur(10px);
            border-radius: 15px;
            padding: 30px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
            text-align: center;
            color: #fff;
        }

        .form-container h2 {
            margin-bottom: 20px;
            font-size: 24px;
            font-weight: bold;
            color: #fff;
        }

        .form-container label {
            font-weight: 600;
            margin-top: 10px;
            display: block;
            text-align: left;
            color: #e9e9e9;
        }

        .form-container input {
            width: 100%;
            padding: 10px;
            margin: 10px 0;
            border: none;
            border-radius: 8px;
            font-size: 14px;
            box-sizing: border-box;
            color: #333;
        }

        .form-container input::placeholder {
            color: #aaa;
        }

        .form-container input:focus {
            outline: none;
            border: 2px solid #fff;
        }

        .form-container button {
            width: 100%;
            padding: 12px;
            margin-top: 20px;
            background: linear-gradient(90deg, #1e90ff, #ff6b6b);
            border: none;
            border-radius: 8px;
            color: #fff;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: 0.3s ease;
        }

        .form-container button:hover {
            background: linear-gradient(90deg, #ff6b6b, #1e90ff);
        }

        .error {
            color: #ffd700;
            margin-top: 15px;
            font-size: 14px;
            display: block;
            visibility: visible;
        }

        /* Responsive Design */
        @media (max-width: 480px) {
            .form-container {
                padding: 20px;
            }

            .form-container h2 {
                font-size: 20px;
            }
        }
    </style>
    <script>
        // Function to validate mobile number input (only numbers)
        function validateMobileNo() {
            var mobileNo = document.getElementById('<%= MobileNoTextBox.ClientID %>').value;
            var regex = /^[0-9]{11}$/; // Only 11 digits
            if (!regex.test(mobileNo)) {
                document.getElementById('<%= ErrorMessageLabel.ClientID %>').innerText = "Please enter a valid 11-digit mobile number.";
                document.getElementById('<%= ErrorMessageLabel.ClientID %>').style.visibility = 'visible';
                return false;
            }
            return true;
        }

        // Function to clear error message when mobile number is typed
        function clearErrorMessage() {
            document.getElementById('<%= ErrorMessageLabel.ClientID %>').style.visibility = 'hidden';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validateMobileNo()">
        <div class="form-container">
            <h2>Customer Login</h2>
            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error" Visible="false"></asp:Label>
            <div>
                <label for="MobileNoTextBox">Mobile Number:</label>
                <asp:TextBox ID="MobileNoTextBox" runat="server" placeholder="Enter Mobile Number" MaxLength="11" OnKeyUp="clearErrorMessage()" />
            </div>
            <div>
                <label for="PasswordTextBox">Password:</label>
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
