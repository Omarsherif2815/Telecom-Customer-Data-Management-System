<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountPlanDate.aspx.cs" Inherits="YourNamespace.AccountPlanDate" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Accounts by Plan and Date</title>
    <style>
        /* Your existing styles */
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
            justify-content: flex-start;
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

        /* Style the calendar pop-up */
        #SubDateCalendar {
            display: none;
            position: absolute;
            z-index: 1000;
        }
    </style>

    <!-- Include jQuery and jQuery UI Datepicker -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize datepicker for SubDateInput
            $('#<%= SubDateInput.ClientID %>').datepicker({
                dateFormat: 'yy-mm-dd',  // Set the format to 'yyyy-mm-dd'
                showAnim: 'fadeIn'  // Define an animation for showing the datepicker
            }).prop('readonly', true);  // Disable typing in the field

            // Function to display the calendar when focusing on the date input
            $('#<%= SubDateInput.ClientID %>').focus(function () {
                var calendar = $('#<%= SubDateCalendar.ClientID %>');
                var inputField = $(this);
                var offset = inputField.offset();
                var top = offset.top + inputField.outerHeight();
                var left = offset.left;

                calendar.css({
                    top: top,
                    left: left,
                    display: 'block' // Show the calendar
                });
            });

            // Close the calendar if clicked outside
            $(document).click(function (e) {
                if (!$(e.target).closest('#<%= SubDateInput.ClientID %>, #<%= SubDateCalendar.ClientID %>').length) {
                    $('#<%= SubDateCalendar.ClientID %>').hide(); // Hide the calendar when clicking outside
                }
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Customer Accounts by Plan and Date
        </div>
        <div class="container">
            <!-- Input fields for PlanID and Subscription Date -->
            <div>
                <label for="planID">PlanID:</label>
                <asp:TextBox ID="planIDInput" runat="server" placeholder="Enter Plan ID" CssClass="form-control" />

                <label for="SubDateInput">Subscription Date:</label>
                <asp:TextBox ID="SubDateInput" runat="server" placeholder="yyyy-mm-dd" CssClass="form-control" 
                    OnFocus="showCalendar();" />  <!-- Disabled typing -->

                <!-- Calendar control -->
                <asp:Calendar ID="SubDateCalendar" runat="server" Visible="false" OnSelectionChanged="SubDateCalendar_SelectionChanged" />

                <asp:Button ID="SearchButton" runat="server" Text="Search" CssClass="btn-redirect" OnClick="SearchButton_Click" />
            </div>

            <!-- GridView to display data -->
            <asp:GridView ID="CustomerPlansGridView" runat="server" AutoGenerateColumns="False" CssClass="gridview" EmptyDataText="No data found." OnPageIndexChanging="CustomerPlansGridView_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="mobileNo" HeaderText="Mobile Number" SortExpression="mobileNo" />
                    <asp:BoundField DataField="planID" HeaderText="Plan ID" SortExpression="planID" />
                    <asp:BoundField DataField="name" HeaderText="Plan Name" SortExpression="name" />
                </Columns>
            </asp:GridView>

            <!-- Error Message Label -->
            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error" Visible="false"></asp:Label>

            <!-- Pagination -->
            <div class="pagination">
                &nbsp;
            </div>

            <!-- Redirect Button -->
            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
