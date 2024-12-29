<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEShopVouchers.aspx.cs" Inherits="YourNamespace.ViewEShopVouchers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-shop Vouchers</title>
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

        .no-vouchers {
            color: #ff6347;  /* Red color */
            font-size: 18px;
            font-weight: bold;
            text-align: center;
            display: block;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            E-shop Vouchers Details
        </div>
        <div class="container">
            <!-- Label for No Vouchers Message -->
            <asp:Label ID="NoVouchersLabel" runat="server" CssClass="no-vouchers" Text="No vouchers are available." Visible="false" />

            <!-- GridView to display e-shops and their redeemed vouchers -->
            <asp:GridView ID="EShopVouchersGridView" runat="server" CssClass="gridview" AutoGenerateColumns="False" 
                OnPageIndexChanging="EShopVouchersGridView_PageIndexChanging" EmptyDataText="No e-shop vouchers found.">
                <Columns>
                    <asp:BoundField DataField="shopID" HeaderText="Shop ID" SortExpression="shopID" />
                    <asp:BoundField DataField="URL" HeaderText="Shop URL" SortExpression="URL" />
                    <asp:BoundField DataField="rating" HeaderText="Shop Rating" SortExpression="rating" />
                    <asp:BoundField DataField="voucherID" HeaderText="Voucher ID" SortExpression="voucherID" />
                    <asp:BoundField DataField="value" HeaderText="Voucher Value" SortExpression="value" />
                </Columns>
            </asp:GridView>

            <!-- Pagination controls -->
            <div class="pagination">
                <asp:Label ID="PaginationLabel" runat="server" CssClass="pagination"></asp:Label>
            </div>

            <!-- Redirect Button -->
            <asp:Button ID="RedirectButton" runat="server" Text="Return" CssClass="btn-redirect" OnClick="RedirectButton_Click" />
        </div>
    </form>
</body>
</html>
