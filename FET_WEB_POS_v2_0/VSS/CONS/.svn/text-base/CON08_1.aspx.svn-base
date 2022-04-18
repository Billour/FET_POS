<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON08_1.aspx.cs" Inherits="VSS_CON08_1" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>寄銷主配上傳</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdval">
                        <!--匯入檔案名稱-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ImportFileName %>"></asp:Literal>：
                    </td>
                    <td colspan="5">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Import %>"
                OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
            <EmptyDataTemplate>
                <tr>
                    <th scope="col">
                        <!--廠商編號-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--廠商名稱-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--門市編號-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--門市名稱-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--商品編號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--實際訂購量-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ActualOrderQuantity %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--異常原因-->
                        <asp:Literal ID="Literal5" runat="server" Text="異常原因"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td colspan="8" class="tdEmptyData">
                        目前無匯入資料
                    </td>
                </tr>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="廠商編號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" />
                <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />
                <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                <asp:BoundField DataField="實際訂購量" HeaderText="<%$ Resources:WebResources, ActualOrderQuantity %>" />
                <asp:BoundField DataField="異常原因" HeaderText="異常原因" />
            </Columns>
        </asp:GridView>
        <div class="seperate">
        </div>
         <div class="btnPosition">
            <asp:Button ID="Button3" runat="server" Text="上傳確認" OnClientClick="window.close();return false;"/>
            <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClientClick="window.close();return false;"/>
        </div>       
    </div>
    </form>
</body>
</html>
