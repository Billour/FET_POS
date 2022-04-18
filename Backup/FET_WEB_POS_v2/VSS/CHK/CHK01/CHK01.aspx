<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CHK01.aspx.cs" Inherits="VSS_CHK_CHK01"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--門市日結作業-->
            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, DayEndProcess %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DailyClosingDate1 %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td align="right">
                                    <dx:ASPxDateEdit ID="S_DATE" runat="server" >
                                    </dx:ASPxDateEdit>
                                </td>
                                <td align="left">
                                    <dx:ASPxButton ID="btnConfirm" runat="server" Text="<%$ Resources:WebResources, Confirm %>"
                                        OnClick="btnConfirm_Click" Width="50" HorizontalAlign="Left">
                                    </dx:ASPxButton>
                                </td>
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
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--營業總額-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TotalTurnover %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
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
                        <!--交易數-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ConsumerTransactionNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
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
                        <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
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
                        <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--快樂購點數-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, HappyGoPoints %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                    </td>
                 
                    <td class="tdval"  rowspan ="10" colspan ="2" nowrap=true valign="top">
                        <asp:Panel ID="Panel1" runat="server"  >
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--銷退總額-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, TotalSalesReturn %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--快樂購金額-->
                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, HappyGoAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label14" runat="server" Text=""></asp:Label>
                    </td>
                  
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--代收總額-->
                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, TotalCollections %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label32" runat="server" Text=""></asp:Label>
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
                        <!--代收作廢總額-->
                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, TotalVoidCollections %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
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
                        <dx:ASPxTextBox ID="txtCashTotal" runat="server">
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                <RegularExpression ValidationExpression="(-)?\d*" ErrorText="輸入字串非數字格式，請重新輸入" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                  
                      <dx:ASPxLabel ID="t1" runat="server"/>
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
                        <dx:ASPxTextBox ID="txtCreditCard" runat="server">
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                <RegularExpression ValidationExpression="(-)?\d*" ErrorText="輸入字串非數字格式，請重新輸入" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                     <dx:ASPxLabel ID="t2" runat="server"/>
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--分期付款-->
                        <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, InstalmentAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtInstallment" runat="server">
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                <RegularExpression ValidationExpression="(-)?\d*" ErrorText="輸入字串非數字格式，請重新輸入" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                     <dx:ASPxLabel ID="t3" runat="server"/>
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
                        <asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--舊POS銷售作廢總額-->
                        <asp:Literal ID="Literal6" runat="server" Text="舊POS銷售作廢總額"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lblSaleCancelTotalAmount" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                                <tr>
                    <td class="tdtxt">
                        <!--舊POS代收作廢總額-->
                        <asp:Literal ID="Literal7" runat="server" Text="舊POS代收作廢總額"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lblInsteadCancelTotalAmount" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <!--禮券總額及金融卡額隱藏-->
                <tr>
                    <td class="tdtxt">
                        <!--禮券總額-->
                        <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, TotalCouponAmount %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtGift" runat="server">
                            <ValidationSettings CausesValidation="false">
                                <RegularExpression ValidationExpression="(-)?\d*" ErrorText="輸入字串非數字格式，請重新輸入" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                     <dx:ASPxLabel ID="t4" runat="server"/>
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
             
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--金融卡額-->
                        <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, BankCardAmount %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtDebitCard" runat="server">
                            <ValidationSettings CausesValidation="false">
                                <RegularExpression ValidationExpression="(-)?\d*" ErrorText="輸入字串非數字格式，請重新輸入" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                     <dx:ASPxLabel ID="t5" runat="server"/>
                    </td>
                    <td class="tdval">
                    </td>
              
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <!--結算確認-->
            <dx:ASPxButton ID="btnExec" runat="server" Text="<%$ Resources:WebResources, ConfirmSettlement %>"
                Width="80" HorizontalAlign="Center" OnClick="btnExec_OnClick" />
            <asp:Label ID="lblDayCloseKey" runat="server" Text="" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
