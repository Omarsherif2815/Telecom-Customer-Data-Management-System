<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view_plan_consumption.aspx.cs" Inherits="YourNamespace.view_plan_consumption" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Plan Consumption</title>
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

        .form-group input, .form-group select {
            padding: 6px;
            font-size: 14px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        .btn-search {
            background-color: #1e90ff;
            color: white;
            padding: 6px 12px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            margin-left: 10px;
        }

        .btn-search:hover {
            background-color: #555;
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

    <!-- Include jQuery and jQuery UI Datepicker -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize datepicker for Start Date and End Date inputs
            $('#StartDateInput').datepicker({
                dateFormat: 'yy-mm-dd',  // Set the format to 'yyyy-mm-dd'
                showAnim: 'fadeIn'  // Define an animation for showing the datepicker
            }).prop('readonly', true);  // Disable typing in the field

            $('#EndDateInput').datepicker({
                dateFormat: 'yy-mm-dd',  // Set the format to 'yyyy-mm-dd'
                showAnim: 'fadeIn'  // Define an animation for showing the datepicker
            }).prop('readonly', true);  // Disable typing in the field
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Plan Consumption Details
        </div>
        <div class="container">
            <!-- Inputs for Plan Name and Date Range -->
            <div class="form-group">
                <label for="PlanNameInput">Plan Name:</label>
                <asp:DropDownList ID="PlanNameInput" runat="server" CssClass="form-control" Width="191px">
                    <asp:ListItem Text="Select Plan" Value="" Selected="True" />
                </asp:DropDownList>

                <label for="StartDateInput" style="margin-left: 20px;">Start Date:</label>
                <asp:TextBox ID="StartDateInput" runat="server" placeholder="yyyy-mm-dd" CssClass="form-control" Width="120px" />

                <label for="EndDateInput" style="margin-left: 20px;">End Date:</label>
                <asp:TextBox ID="EndDateInput" runat="server" placeholder="yyyy-mm-dd" CssClass="form-control" Width="120px" />

                <asp:Button ID="SearchButton" runat="server" Text="Search" CssClass="btn-search" OnClick="SearchButton_Click" />
            </div>

            <!-- GridView to display consumption -->
            <asp:GridView ID="ConsumptionGridView" runat="server" CssClass="gridview" AutoGenerateColumns="True" EmptyDataText="No data found.">
            </asp:GridView>

            <!-- Return Button -->
            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
