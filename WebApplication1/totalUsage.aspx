<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountUsagePlan.aspx.cs" Inherits="YourNamespace.AccountUsagePlan" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Total Usage by Plan</title>
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

    <!-- Include jQuery and jQuery UI Datepicker -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize datepicker for StartDateInput
            $('#<%= StartDateInput.ClientID %>').datepicker({
                dateFormat: 'yy-mm-dd',  // Set the format to 'yyyy-mm-dd'
                showAnim: 'fadeIn'  // Define an animation for showing the datepicker
            }).prop('readonly', true);  // Disable typing in the field

            // Function to display the calendar when focusing on the date input
            $('#<%= StartDateInput.ClientID %>').focus(function () {
                var inputField = $(this);
                inputField.datepicker('show'); // Show the datepicker
            });

            // Close the calendar if clicked outside
            $(document).click(function (e) {
                if (!$(e.target).closest('#<%= StartDateInput.ClientID %>').length) {
                    $('#<%= StartDateInput.ClientID %>').datepicker('hide'); // Hide the calendar when clicking outside
                }
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Total Usage of Subscribed Plans
        </div>
        <div class="container">
            <!-- Input fields for mobile number and start date -->
            <div>
                <label for="MobileNoInput">Mobile Number:</label>
                <asp:TextBox ID="MobileNoInput" runat="server" placeholder="Enter Mobile Number" CssClass="form-control" />

                <label for="StartDateInput">Start Date:</label>
                <asp:TextBox ID="StartDateInput" runat="server" placeholder="Enter Start Date" CssClass="form-control" />

                <asp:Button ID="SearchButton" runat="server" Text="Search" CssClass="btn-redirect" OnClick="SearchButton_Click" />
            </div>

            <!-- GridView to display data -->
            <asp:GridView ID="AccountUsageGridView" runat="server" AutoGenerateColumns="False" CssClass="gridview" EmptyDataText="No usage data found." OnPageIndexChanging="AccountUsageGridView_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="planID" HeaderText="Plan ID" SortExpression="planID" />
                    <asp:BoundField DataField="total data" HeaderText="Total Data Consumption (MB)" SortExpression="total_data" />
                    <asp:BoundField DataField="total mins" HeaderText="Total Minutes Used" SortExpression="total_mins" />
                    <asp:BoundField DataField="total SMS" HeaderText="Total SMS Sent" SortExpression="total_SMS" />
                </Columns>
            </asp:GridView>

            <!-- Error Message Label -->
            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error" Visible="false"></asp:Label>

            <!-- Pagination - Align to the left -->
            <div class="pagination">
                &nbsp;
            </div>

            <!-- Redirect Button -->
            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
