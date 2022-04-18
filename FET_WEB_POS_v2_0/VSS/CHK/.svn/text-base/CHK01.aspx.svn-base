<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CHK01.aspx.cs" Inherits="VSS_CHK01_CHK01" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    <div>

        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--門市日結作業-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, DayEndProcess %>"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DailyClosingDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" >
                        <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td align="right"><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ></dx:ASPxDateEdit></td>
                                <td align="left"><dx:ASPxButton ID="btnConfirm" runat="server" Text="<%$ Resources:WebResources, Confirm %>" OnClick="btnConfirm_Click" Width="50" HorizontalAlign="Left"></dx:ASPxButton></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="A0001"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--營業總額-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TotalTurnover %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label5" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--營業日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OperationDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--來客數-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ConsumersShoppingFrequency %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label8" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Date %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--人員-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Staff %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label9" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr style="background-color: #780C0C; color: White; text-align: Left; font-size: 12pt">
                    <td colspan="2">
                        <!--交易統計-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TradeStatistics %>"></asp:Literal>
                    </td>
                    <td colspan="2">
                        <!--HappyGo統計-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, HappyGoStatistics %>"></asp:Literal>
                    </td>
                    <td colspan="2">
                        <!--讀帳確認-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ConfirmingReadingCheck %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--銷售總額-->
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, TotalSales %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label11" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--快樂購點數-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, HappyGoPoints %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label12" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--機台1-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, CashRegister1 %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox13" runat="server" BackColor="Red" Enabled="false"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--銷退總額-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, TotalSalesReturn %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label13" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--快樂購金額-->
                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, HappyGoAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label14" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--機台2-->
                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, CashRegister2 %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox14" runat="server" BackColor="Green" Enabled="false"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--代收總額-->
                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, TotalCollections %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label32" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        <!--機台3-->
                        <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, CashRegister3 %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox15" runat="server" BackColor="Green" Enabled="false"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--代收作廢總額-->
                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, TotalVoidCollections %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label33" runat="server" Text="1000"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
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
                        <!--現金總額-->
                        <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, TotalCash %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox4" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
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
                        <!--信用卡額-->
                        <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, CreditCardAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--分期付款-->
                        <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, InstalmentAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox6" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--禮券總額-->
                        <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, TotalCouponAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox7" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
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
                        <!--金融卡額-->
                        <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, BankCardAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox8" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
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
                        <!--日結時間-->
                        <asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, DailyClosingTime %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label31" runat="server" Text="2010/07/14"></asp:Label>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div class="btnPosition">
                <!--結算確認-->
                <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, ConfirmSettlement %>" Width="80" HorizontalAlign="Center" />
            </div>
        </div>
    </div>
</asp:Content>

