<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON08.aspx.cs" Inherits="VSS_CON08_Default" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=123,left=280,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <%--<script type="text/javascript">
        $(document).ready(function() {            
            $('#<%=Button3.ClientID%>').click();
        });        
    </script>--%>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品主配作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductDistribution %>"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table id="Tab1" >
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
                        <!--廠商代號-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--廠商名稱-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--門市編號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--門市名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--商品料號-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
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
                <asp:TemplateField HeaderText="<%$ Resources:WebResources, SupplierNo %>" HeaderStyle-Width="100px" HeaderStyle-Wrap="false" ItemStyle-Width="100px" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("廠商代號") %>' Width="50px"></asp:TextBox>
                        <asp:Button ID="Button10" runat="server" Text="選" OnClientClick="openwindow('../CONS/CON10_chooseSupplierNo.aspx',640,350);return false;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" HeaderStyle-Width="80px" HeaderStyle-Wrap="false" ItemStyle-Width="80px" ItemStyle-Wrap="false"></asp:BoundField>

                <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreNo %>" HeaderStyle-Width="100px" HeaderStyle-Wrap="false" ItemStyle-Width="100px" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("門市編號") %>' Width="50px"></asp:TextBox>
                        <asp:Button ID="Button9" runat="server" Text="選" OnClientClick="openwindow('../INV/INV18_3.aspx',640,350);return false;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" HeaderStyle-Width="80px" HeaderStyle-Wrap="false" ItemStyle-Width="80px" ItemStyle-Wrap="false"></asp:BoundField>

                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" HeaderStyle-Width="120px" HeaderStyle-Wrap="false" ItemStyle-Width="120px" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("商品料號") %>' Width="80px"></asp:TextBox>
                        <asp:Button ID="Button8" runat="server" Text="選" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,350);return false;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" HeaderStyle-Width="80px" HeaderStyle-Wrap="false" ItemStyle-Width="80px" ItemStyle-Wrap="false"></asp:BoundField>

                <asp:TemplateField HeaderText="<%$ Resources:WebResources, DistributionQuantity %>" HeaderStyle-Width="80px" HeaderStyle-Wrap="false" ItemStyle-Width="80px" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("實際訂購量") %>' Width="80px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="異常原因" HeaderStyle-Width="120px" HeaderStyle-Wrap="false" ItemStyle-Width="120px" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("異常原因") %>' ></asp:Label>
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
