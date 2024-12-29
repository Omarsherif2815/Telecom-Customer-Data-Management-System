<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="physicalStoreVouchers.aspx.cs" Inherits="YourNamespace.physicalStoreVouchers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Physical Stores and Redeemed Vouchers</title>
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            Physical Stores and Redeemed Vouchers
        </div>
        <div class="container">
            <!-- GridView to display data -->


            <!-- Error Message Label -->
            <asp:GridView ID="PhysicalStoreVouchersGridView" runat="server" AutoGenerateColumns="False" CssClass="gridview" EmptyDataText="No physical store vouchers found." OnPageIndexChanging="PhysicalStoreVouchersGridView_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="shopID" HeaderText="Shop ID" SortExpression="shopID" />
                    <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />
                    <asp:BoundField DataField="working_hours" HeaderText="Working Hours" SortExpression="working_hours" />
                    <asp:BoundField DataField="voucherID" HeaderText="Voucher ID" SortExpression="voucherID" />
                    <asp:BoundField DataField="value" HeaderText="Voucher Value" SortExpression="value" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error" Visible="false"></asp:Label>

            <!-- Pagination - Align to the left -->
            <div class="pagination">
                &nbsp;
            </div>

            <!-- Redirect Button -->
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
