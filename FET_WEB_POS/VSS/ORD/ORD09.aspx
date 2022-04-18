<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD09.aspx.cs" Inherits="VSS_ORD_ORD09" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>寄銷主配上傳</title>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="titlef">
            DropShipment主配上傳</div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdval">
                        <!--匯入檔案名稱-->
                        <asp:Literal ID="Literal2" runat="server" Text="匯入檔案路徑"></asp:Literal>：
                    </td>
                    <td colspan="5">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Import %>"
                OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
            OnRowDataBound="gvMaster_RowDataBound">
            <EmptyDataTemplate>
                <tr>
                    <th scope="col">
                        <!--門市代碼-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--門市名稱-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--商品料號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--主配量-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, DistributionQuantity %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--異常原因-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ErrorDescription %>"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td colspan="8" class="tdEmptyData">
                        目前無匯入資料
                    </td>
                </tr>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="門市代碼">
                    <HeaderStyle Width="100px" Wrap="true" />
                    <ItemStyle Width="100px" Wrap="true" />
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("門市代碼") %>' Width="80px"></asp:TextBox>
                        <asp:Button ID="Button5" runat="server" Text="選" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx',550,350);return false;" />
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreName %>">
                    <HeaderStyle Width="100px" Wrap="true" />
                    <ItemStyle Width="100px" Wrap="true" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("門市名稱") %>' Width="80px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>">
                    <HeaderStyle Width="100px" Wrap="true" />
                    <ItemStyle Width="100px" Wrap="true" />
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("商品料號") %>' Width="80px"></asp:TextBox>
                        <asp:Button ID="Button8" runat="server" Text="選" OnClientClick="openwindow('ORD01_searchProductNo.aspx',640,300);return false;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>">
                    <HeaderStyle Width="100px" Wrap="true" />
                    <ItemStyle Width="100px" Wrap="true" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="<%$ Resources:WebResources, DistributionQuantity %>">
                    <HeaderStyle Width="100px" Wrap="true" />
                    <ItemStyle Width="100px" Wrap="true" />
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("主配量") %>' Width="80px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="異常原因">
                    <HeaderStyle Width="100px" Wrap="true" />
                    <ItemStyle Width="100px" Wrap="true" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("異常原因") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button3" runat="server" Text="上傳確認" OnClientClick="window.close();return false;" />
            <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                OnClientClick="window.close();return false;" />
        </div>
    </div>
    </form>
</body>
</html>
