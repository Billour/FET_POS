<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CHK02.aspx.cs" Inherits="VSS_CHK02_CHK02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="titlef">
            <!--機台讀帳作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReadingCheckOperation %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table>
                <tr>
                     <td class="tdtxt">
                        <!--日結日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, DailyClosingDate %>"></asp:Literal>：</td>
                    <td class="tdval" colspan="5">
                        
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                        <asp:Button ID="Button1" runat="server" 
                            Text="<%$ Resources:WebResources, Confirm %>" onclick="Button1_Click" />
                     </td>
                    
                </tr>
                <tr>
                     <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：</td>
                    <td class="tdval">
                         
                        <asp:Label ID="Label2" runat="server" Text="A0001"></asp:Label>
                         
                    </td>
                    <td class="tdtxt">
                        <!--機台號碼-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CashRegisterNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="G0001"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                         <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--營業日期-->
                         <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, OperationDate %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:Label ID="Label7" runat="server" Text="2010/07/14"></asp:Label>
                    </td>
                    
                    <td class="tdtxt">
                        <!--營業總額-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TotalTurnover %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label5" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label6" runat="server" Text="2010/07/01 22:00 "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                    <td class="tdtxt">
                        <!--來客數-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ConsumersShoppingFrequency %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label8" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label9" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="criteria">
            <table>
                <tr style="background-color:#780C0C; color:White; text-align:Left; font-size:12pt">
                    <td colspan="2">                    
                        <!--銷售統計-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SalesStatistics %>"></asp:Literal>
                    </td>
                    <td colspan="2">
                        <!--代收統計-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CollectionStatistics %>"></asp:Literal>
                    </td>
                    <td colspan="2">
                        HappyGo                        
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--銷售總額-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, TotalSales %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label11" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--代收總額-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, TotalCollections %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label12" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--快樂購點數-->
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, HappyGoPoints %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:Label ID="Label25" runat="server" Text="1000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--銷退總額-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, TotalSalesReturn %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label13" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--代收作廢總額-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, TotalVoidCollections %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label14" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--快樂購金額-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, HappyGoAmount %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:Label ID="Label24" runat="server" Text="1000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--現金總額-->
                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, TotalCash %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label15" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--代收現金總額-->
                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, TotalCollectionCash %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label23" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--信用卡額-->
                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, CollectionCreditCardAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label16" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--代收信用卡額-->
                        <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, CollectionCreditCardAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label22" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt" 
                        style="background-color:#780C0C; color:White; text-align:Left; font-size:12pt" 
                        colspan="2">
                        <!--其他-->
                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, Other %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--分期付款-->
                        <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, InstalmentAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label17" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                    <td class="tdtxt">
                        <!--刷卡張數-->
                        <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, NumberOfCards %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:Label ID="Label26" runat="server" Text="1000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--禮券總額-->
                        <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, TotalCouponAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label18" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                    <td class="tdtxt">
                        <!--發票作廢張數-->
                        <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, VoidInvoices %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:Label ID="Label27" runat="server" Text="1000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--金融卡額-->
                        <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, BankCardAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label19" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--帳單作廢張數-->
                        <asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, VoidBills %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:Label ID="Label28" runat="server" Text="1000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--繳大鈔-->
                        <asp:Literal ID="Literal30" runat="server" Text="<%$ Resources:WebResources, SaveMoney %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label20" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--找零金-->
                        <asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:WebResources, PettyCash %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:Label ID="Label21" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>              
            </table>
            <!--讀帳確認-->
            <div class="btnPosition" ><asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, ConfirmingReadingCheck %>" /></div>
        </div>
    </form>
</body>
</html>
