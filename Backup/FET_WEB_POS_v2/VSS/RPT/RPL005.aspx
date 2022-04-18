<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL005.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL005" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[訂單日期起值]不允許大於[訂單日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--信用卡付款入帳明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL05 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <!--門市編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox1" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox4" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--交易日期-->
                    <asp:Literal ID="Literal12" runat="server" Text="交易日期"></asp:Literal>：
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
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--交易類型：-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, SaleType %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                            <dx:ListEditItem Text="銷售交易" Value="1" />
                            <dx:ListEditItem Text="帳單代收" Value="2" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--訂單通路：-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderChain %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                            <dx:ListEditItem Text="門市" Value="StoreSale" />
                            <dx:ListEditItem Text="網路門市" Value="eSale" />
                        </Items>
                    </dx:ASPxComboBox>
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
            <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="發票號碼" />
            <dx:GridViewDataTextColumn FieldName="交易類型" Caption="交易類型" />
            <dx:GridViewDataTextColumn FieldName="交易類別" Caption="交易類別" />
            <dx:GridViewDataTextColumn FieldName="服務類型" Caption="服務類型" />
            <dx:GridViewDataTextColumn FieldName="資料來源" Caption="資料來源" />
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="促銷代碼" />
            <dx:GridViewDataColumn FieldName="發票金額" Caption="發票金額" />
            <dx:GridViewDataTextColumn FieldName="信用卡卡別" Caption="信用卡卡別" />
            <dx:GridViewDataColumn FieldName="刷卡金額" Caption="刷卡金額" />
            <dx:GridViewDataColumn FieldName="信用卡手續費" Caption="信用卡手續費" />
            <dx:GridViewDataTextColumn FieldName="訂單通路" Caption="訂單通路" />
            <dx:GridViewDataTextColumn FieldName="原交易序號" Caption="原交易序號" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
        </dx:ASPxGridViewExporter>
    </div>
</asp:Content>
