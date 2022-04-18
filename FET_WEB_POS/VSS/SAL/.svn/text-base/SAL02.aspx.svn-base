<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL02.aspx.cs" Inherits="VSS_SA02_SA02" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "商品編號查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');

        }
        function openwindow2(url, width, height) {
            window.open(url, "促銷代碼查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="func">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--銷售交易查詢-->
                         <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, SalesEnquiry %>" />
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Selling %>" OnClientClick="document.location='SAL01.aspx';return false;" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--交易日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TradeDate %>" />：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>" /><cc1:postbackDate_TextBox ID="PostbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>" /><cc1:postbackDate_TextBox ID="PostbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddlWidthFormat">
                            <asp:ListItem Text="ALL"></asp:ListItem>
                            <asp:ListItem Text="已結帳" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="已作廢"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--機台-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CashRegister %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" TargetControlID="TextBox2"
                            Enabled="false">
                        </asp:CalendarExtender>
                    </td>
                    <td class="tdtxt">
                        <!--客戶門號-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox8" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--交易序號-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--促銷代碼-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="tbWidthFormat" Width="110"></asp:TextBox>
                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow2('SAL02_searchDiscountNo.aspx',450,350);return false;" />
                    </td>
                    <td class="tdtxt">
                        <!--銷售人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>" />：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="ddlWidthFormat">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem Text="劉光俊"></asp:ListItem>
                            <asp:ListItem Text="林雅玲"></asp:ListItem>
                            <asp:ListItem Text="游惠貞"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--發票號碼-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="tbWidthFormat" Width="110"></asp:TextBox>
                        <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_searchProductNo.aspx');return false;" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td class="tdtxt">
                        <!--付款方式-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>" />：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlPaymentMethod" runat="server">
                            <asp:ListItem>現金</asp:ListItem>
                            <asp:ListItem>信用卡</asp:ListItem>
                            <asp:ListItem>禮券</asp:ListItem>
                            <asp:ListItem>金融卡</asp:ListItem>
                            <asp:ListItem>HappyGo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                AllowPaging="True" OnRowDataBound="gvMaster_RowDataBound" PageSize="5" PagerStyle-HorizontalAlign="Right"
                OnPageIndexChanging="GridView_PageIndexChanging">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col">
                            <!--項次-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Items %>" />
                        </th>
                        <th scope="col">
                            <!--狀態-->
                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Status %>" />
                        </th>
                        <th scope="col">
                            <!--交易日期-->
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, TradeDate %>" />
                        </th>
                        <th scope="col">
                            <!--交易序號-->
                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />
                        </th>
                        <th scope="col">
                            <!--機台-->
                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, CashRegister %>" />
                        </th>
                        <th scope="col">
                            <!--客戶門號-->
                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />
                        </th>
                        <th scope="col">
                            <!--發票號碼-->
                            <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>" />
                        </th>
                        <th scope="col">
                            <!--金額-->
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, Amount %>" />
                        </th>
                        <th scope="col">
                            <!--付款方式-->
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>" />
                        </th>                        
                        <th scope="col">
                            <!--銷售人員-->
                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>" />
                        </th>
                        <th scope="col">
                            <!--更新日期-->
                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="11" class="tdEmptyData">
                            <!--查無資料，請修改條件，重新查詢-->
                            <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>" />
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input id="Radio" type="radio" name="SameRadio" value='<%#Eval("項次") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" />
                    <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
                    <asp:BoundField DataField="交易日期" HeaderText="<%$ Resources:WebResources, TradeDate %>" />
                    <asp:BoundField DataField="交易序號" HeaderText="<%$ Resources:WebResources, TransactionNo %>" />
                    <asp:BoundField DataField="機台" HeaderText="<%$ Resources:WebResources, CashRegister %>" />
                    <asp:BoundField DataField="客戶門號" HeaderText="<%$ Resources:WebResources, CustomerMobileNumber %>" />
                    <asp:BoundField DataField="發票號碼" HeaderText="<%$ Resources:WebResources, InvoiceNo %>" />
                    <asp:BoundField DataField="金額" HeaderText="<%$ Resources:WebResources, Amount %>" />
                    <asp:BoundField DataField="付款方式" HeaderText="<%$ Resources:WebResources, PaymentMethod %>" />
                    <asp:BoundField DataField="銷售人員" HeaderText="<%$ Resources:WebResources, SalesClerk %>" />
                    <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
                </Columns>
                <PagerTemplate>
                    <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Page" CommandArgument="First"
                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/first.png" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/previous.png" /></asp:LinkButton>
                    第
                    <asp:Label ID="lblCurrPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                    <asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                    <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                    到第
                    <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                    頁
                    <asp:Button ID="btnGoToIndex" runat="server" Text="GO" OnClick="btnGoToIndex_Click" />
                </PagerTemplate>
            </asp:GridView>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition" id="showFooter" runat="server" visible="true">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, View %>" />
            <asp:Button ID="Button21" runat="server" Text="<%$ Resources:WebResources, ExchangeGoods %>" OnClientClick="document.location='SAL03.aspx';return false;" />
            <asp:Button ID="Button31" runat="server" Text="<%$ Resources:WebResources, Discard %>" OnClientClick="document.location='SAL04.aspx';return false;" />
        </div>
    </div>    
    </form>
</body>
</html>
