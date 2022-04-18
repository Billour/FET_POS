<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailReport.aspx.cs" Inherits="DetailReport" Title="Detail Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Detail Report</h2>
        <a href="Default.aspx">首頁</a><br/>
        <asp:Button ID="uxExportGridViewToPdf" runat="server" Text="Export GridView to PDF" OnClick="uxExportGridViewToPdf_Click" />
        <asp:Button ID="uxExportDataTableToPdf" runat="server" Text="Export DataTable to PDF" OnClick="uxExportDataTableToPdf_Click" />       
        <asp:GridView ID="uxProducts" runat="server" DataSourceID="SalesDataSource" 
         Font-Size="Small" AutoGenerateColumns="false" AllowPaging="false" PageSize="20"
         OnRowDataBound="uxProducts_RowDataBound" ShowFooter="true">   
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="商品編號" />
                <asp:BoundField DataField="ProductNumber" HeaderText="商品條碼" />
                <asp:BoundField DataField="Description" HeaderText="商品名稱" ItemStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="價格" FooterStyle-Font-Bold="True" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# Eval("Price", "{0:N0}") %>
                    </ItemTemplate>
                    <FooterTemplate>0</FooterTemplate>
                </asp:TemplateField>                                                               
            </Columns>                
            <HeaderStyle BackColor="#cccccc" />            
        </asp:GridView>
    </div>
    <asp:AccessDataSource ID="SalesDataSource" runat="server"
        DataFile="~/App_Data/Sales.mdb"   
        SelectCommand="SELECT * FROM Product ORDER BY ProductID"></asp:AccessDataSource>
    </form>
</body>
</html>
