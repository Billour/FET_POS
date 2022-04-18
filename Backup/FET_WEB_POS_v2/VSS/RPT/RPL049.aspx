<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL049.aspx.cs" Inherits="VSS_RPT_RPL049"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[交易日期起值]不允許大於[交易日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--信用卡交易明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL049 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal8" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox2" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox3" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
                <!--刷卡類型-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, UseCardType %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:RadioButtonList runat="server" ID="rbPAID_MODE" RepeatDirection="Horizontal"
                        Width="200px">
                        <asp:ListItem Text="ALL" />
                        <asp:ListItem Text="一次付清" />
                        <asp:ListItem Text="<%$ Resources:WebResources, Instalment %>" />
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <!--交易日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" ClientInstanceName="txtOrdDateStart">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="txtOrdDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--信用卡卡號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, CreditCardNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width: 200px">
                        <dx:ASPxTextBox ID="txtCARDNO" runat="server">
                        </dx:ASPxTextBox>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <table align="center">
        <tr>
            <td>
                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                    OnClick="btnSearch_Click">
                    <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    OnClick="btnReset_Click" AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click">
                    <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="門市名稱" />
            <dx:GridViewDataTextColumn FieldName="交易日期" Caption="交易日期" />
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="交易序號" />
            <dx:GridViewDataTextColumn FieldName="機台編號" Caption="機台編號" />
            <dx:GridViewDataTextColumn FieldName="刷卡類型" Caption="刷卡類型" />
            <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="發票號碼" Name ="發票號碼" />
            <dx:GridViewDataColumn FieldName="發票金額" Caption="發票金額" Name="發票金額" />
            <dx:GridViewDataTextColumn FieldName="分期付款" Caption="分期付款" Name="分期付款" />
            <dx:GridViewDataTextColumn FieldName="分期期數" Caption="分期期數" />
            <dx:GridViewDataTextColumn FieldName="分期銀行名稱" Caption="分期銀行名稱" />
            <dx:GridViewDataTextColumn FieldName="信用卡號" Caption="信用卡號" />
            <dx:GridViewDataTextColumn FieldName="處理人員" Caption="處理人員" />
        </Columns>
        <Settings ShowFooter="True" />
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="發票號碼" SummaryType="Count" DisplayFormat="總計：" />
            <dx:ASPxSummaryItem FieldName="發票金額" SummaryType="Sum" DisplayFormat="0" />
            <dx:ASPxSummaryItem FieldName="分期付款" SummaryType="Sum" DisplayFormat="0" />
        </TotalSummary>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <cc:ASPxGridView ID="gvExpoter" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%" Visible="false">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="門市名稱" />
            <dx:GridViewDataTextColumn FieldName="交易日期" Caption="交易日期" />
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="交易序號" />
            <dx:GridViewDataTextColumn FieldName="機台編號" Caption="機台編號" />
            <dx:GridViewDataTextColumn FieldName="刷卡類型" Caption="刷卡類型" />
            <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="發票號碼" />
            <dx:GridViewDataColumn FieldName="發票金額" Caption="發票金額" />
            <dx:GridViewDataTextColumn FieldName="分期付款" Caption="分期付款" />
            <dx:GridViewDataTextColumn FieldName="分期期數" Caption="分期期數" />
            <dx:GridViewDataTextColumn FieldName="分期銀行名稱" Caption="分期銀行名稱" />
            <dx:GridViewDataTextColumn FieldName="信用卡號" Caption="信用卡號" />
            <dx:GridViewDataTextColumn FieldName="處理人員" Caption="處理人員" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvExpoter">
        </dx:ASPxGridViewExporter>
    </div>
</asp:Content>
