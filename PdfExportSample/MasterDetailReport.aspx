<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterDetailReport.aspx.cs" Inherits="MasterDetailReport" Title="Master/Detail Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Master/Detail Report</h2>
        <a href="Default.aspx">首頁</a><br/>
        <asp:Button ID="uxExport" runat="server" Text="Export GridView to PDF" OnClick="uxExport_Click" />
        <asp:GridView ID="uxOrders" runat="server" DataSourceID="OrderHeaderDataSource" DataKeyNames="OrderID" 
            ShowHeader="false" AutoGenerateColumns="false" Font-Size="Small" BorderStyle="None" BorderWidth="0" CellPadding="0" CellSpacing="0">
            <Columns>                
                <asp:TemplateField>                           
                <ItemTemplate> 
                <asp:HiddenField ID="uxOrderID" runat="server" Value='<%# Eval("OrderID") %>' />               
                <table border="0" cellpadding="0" cellspacing="0" width="500px">
                    <thead>
                    <tr>
                        <td style="width:100px">訂單編號：</td><td  style="width:400px"><asp:Label ID="uxOrderNumber" runat="server" Text='<%# Eval("OrderNumber") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td>訂購人：</td><td><asp:Label ID="uxCustomerName" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td>訂購日期：</td><td><asp:Label ID="uxOrderDate" runat="server" Text='<%# Eval("OrderDate") %>'></asp:Label></td>
                    </tr>
                    </thead>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="uxOrderDetails" runat="server" DataSourceID="OrderDetailDataSource" 
                            AutoGenerateColumns="False" Font-Size="Small" BorderColor="#cccccc" CellPadding="0" CellSpacing="0" 
                            GridLines="Both" ShowFooter="true" 
                            OnRowDataBound="uxOrderDetails_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="ProductNumber" HeaderText="商品條碼" ItemStyle-Width="100" />
                                <asp:BoundField DataField="Description" HeaderText="商品名稱" ItemStyle-Width="250" />                                
                                <asp:BoundField DataField="Price" HeaderText="單價" DataFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50" />
                                <asp:BoundField DataField="Quantity" HeaderText="數量" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50" />
                                <asp:TemplateField HeaderText="金額" FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50">
                                    <ItemTemplate><%# Eval("Subtotal") %></ItemTemplate>
                                    <FooterTemplate>0</FooterTemplate>
                                </asp:TemplateField>                                                      
                            </Columns>
                            <EmptyDataTemplate>
                            <i>沒有資料可以顯示</i>
                            </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:AccessDataSource ID="OrderDetailDataSource" runat="server"
                            DataFile="~/App_Data/Sales.mdb"   
                            SelectCommand="SELECT * FROM vwOrderDetail WHERE OrderID=@OrderID">
                                <SelectParameters>
                                    <asp:ControlParameter Name="OrderID" ControlID="uxOrderID" PropertyName="Value" />
                                </SelectParameters>
                            </asp:AccessDataSource>
                        </td>
                    </tr>
                </table><br/>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>            
        </asp:GridView>
        <asp:AccessDataSource ID="OrderHeaderDataSource" runat="server"
        DataFile="~/App_Data/Sales.mdb"   
        SelectCommand="SELECT * FROM vwOrderHeader"></asp:AccessDataSource>
        
    </div>
    </form>
</body>
</html>
