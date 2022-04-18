<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL031.aspx.cs" Inherits="VSS_SAL031_SAL031" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

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
                        換貨作業
                    </td>
                    <td align="right">
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
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--狀態-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--交易日期-->
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--交易序號-->
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--機台-->
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CashRegister %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--客戶門號-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--金額-->
                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Amount %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--現金-->
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Cash %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--信用卡-->
                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, CreditCard %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--禮券-->
                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Coupon %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--金融卡-->
                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, BankCard %>"></asp:Literal>
                        </th>
                        <th scope="col">                            
                            <asp:Literal ID="Literal19" runat="server" Text="HG"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--更新人員-->
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--更新日期-->
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td colspan="14" class="tdEmptyData">
                            <!--查無資料，請修改條件，重新查詢-->
                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
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
                    <asp:BoundField DataField="金額" HeaderText="<%$ Resources:WebResources, Amount %>" />
                    <asp:BoundField DataField="現金" HeaderText="<%$ Resources:WebResources, Cash %>" />
                    <asp:BoundField DataField="信用卡" HeaderText="<%$ Resources:WebResources, CreditCard %>" />
                    <asp:BoundField DataField="禮券" HeaderText="<%$ Resources:WebResources, Coupon %>" />
                    <asp:BoundField DataField="金融卡" HeaderText="<%$ Resources:WebResources, BankCard %>" />
                    <asp:BoundField DataField="HG" HeaderText="HG" />
                    <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" />
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
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="檢視" Visible="false" />
            <asp:Button ID="Button21" runat="server" Text="換貨" OnClientClick="document.location='SAL03.aspx';return false;" />
        </div>
    </div>
    </div>
    </form>
</body>
</html>
