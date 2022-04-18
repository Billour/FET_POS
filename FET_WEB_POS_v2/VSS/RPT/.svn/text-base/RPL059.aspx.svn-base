<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL059.aspx.cs" Inherits="VSS_RPT_RPL059"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtTranDateStart.GetText() != '' && txtTranDateEnd.GetText() != '') {
                if (txtTranDateStart.GetValue() > txtTranDateEnd.GetValue()) {
                    alert("[交易日期起值]不允許大於[交易日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }

            if (txtTranAmount1.GetText() != '' && txtTranAmount2.GetText() != '') {
                if (txtTranAmount1.GetValue() > txtTranAmount2.GetValue()) {
                    alert("[交易金額起值]不允許大於[交易金額訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }            
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--信用卡明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL059 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
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
                                <dx:ASPxDateEdit ID="txtTranDateStart" runat="server" ClientInstanceName="txtTranDateStart">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTranDateEnd" runat="server" ClientInstanceName="txtTranDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--交易類型-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal24" runat="server" Text="交易類型"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbTranType" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                            <dx:ListEditItem Text="繳款" Value="繳款" />
                            <dx:ListEditItem Text="銷售" Value="銷售" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <!--交易金額-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="交易金額"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtTranAmount1" runat="server" Width="100px" ClientInstanceName="txtTranAmount1">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtTranAmount2" runat="server" Width="100px" ClientInstanceName="txtTranAmount2">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--卡別-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal25" runat="server" Text="卡別"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbCardType" runat="server" Width="120px">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
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
    <table width="98%">
        <tr>
            <td>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
                    Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="交易日期" Caption="交易日期" />
                        <dx:GridViewDataColumn FieldName="機台編號" Caption="機台編號" />
                        <dx:GridViewDataTextColumn FieldName="交易序號" Caption="交易序號" />
                        <dx:GridViewDataTextColumn FieldName="銀行授權號碼" Caption="銀行授權號碼" />
                        <dx:GridViewDataTextColumn FieldName="交易類型" Caption="交易類型" />
                        <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="帳單號/發票號碼" />
                        <dx:GridViewDataTextColumn FieldName="用戶門號" Caption="用戶門號" />
                        <dx:GridViewDataTextColumn FieldName="信用卡別" Caption="信用卡別" />
                        <dx:GridViewDataTextColumn FieldName="信用卡號" Caption="信用卡號" />
                        <dx:GridViewDataColumn FieldName="信用卡金額" Caption="信用卡金額" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataTextColumn FieldName="備註" Caption="備註" />
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                </cc:ASPxGridView>
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td align="right">
                <cc:ASPxGridView ID="gvSum" ClientInstanceName="gvSum" runat="server">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="卡別" Caption="" />
                        <dx:GridViewDataColumn FieldName="金額" Caption="金額" />
                        <dx:GridViewDataColumn FieldName="數量" Caption="數量" />
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                </cc:ASPxGridView>
            </td>
        </tr>
    </table>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvExport">
        </dx:ASPxGridViewExporter>
        <cc:ASPxGridView ID="gvExport" ClientInstanceName="gvExport" runat="server" Visible="false">
            <Columns>
                <dx:GridViewDataColumn FieldName="交易日期" Caption="交易日期" />
                <dx:GridViewDataColumn FieldName="機台編號" Caption="機台編號" />
                <dx:GridViewDataTextColumn FieldName="交易序號" Caption="交易序號" />
                <dx:GridViewDataTextColumn FieldName="銀行授權號碼" Caption="銀行授權號碼" />
                <dx:GridViewDataTextColumn FieldName="交易類型" Caption="交易類型" />
                <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="帳單號/發票號碼" />
                <dx:GridViewDataTextColumn FieldName="用戶門號" Caption="用戶門號" />
                <dx:GridViewDataTextColumn FieldName="信用卡別" Caption="信用卡別" />
                <dx:GridViewDataTextColumn FieldName="信用卡號" Caption="信用卡號" />
                <dx:GridViewDataColumn FieldName="信用卡金額" Caption="信用卡金額" CellStyle-HorizontalAlign="Right" />
                <dx:GridViewDataTextColumn FieldName="備註" Caption="備註" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>
    </div>
</asp:Content>
