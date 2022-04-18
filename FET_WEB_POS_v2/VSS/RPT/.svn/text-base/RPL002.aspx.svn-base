<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="RPL002.aspx.cs" Inherits="VSS_RPT_RPL002" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

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
        <!--門市對帳明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="門市對帳明細表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <!--門市編號-->
                    <asp:Literal ID="lblSTORE_NO" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblSTORE_NO_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_NO_S" runat="server" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="lblSTORE_NO_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_NO_E" runat="server" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
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
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" ClientInstanceName="txtOrdDateStart">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="txtOrdDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--付款方式-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                        <Items>                            
                            <dx:ListEditItem Text="現金" Value="1" />
                            <dx:ListEditItem Text="信用卡" Value="2" />
                            <dx:ListEditItem Text="離線信用卡" Value="3" />
                            <%--<dx:ListEditItem Text="分期付款" Value="4" />
                            <dx:ListEditItem Text="禮券" Value="5" />
                            <dx:ListEditItem Text="金融卡" Value="6" />
                            <dx:ListEditItem Text="Happy Go" Value="7" />--%>
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
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false" OnClick="btnReset_Click">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click">
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
            <dx:GridViewDataColumn FieldName="銷售" Caption="銷售" />
            <dx:GridViewDataColumn FieldName="代收" Caption="代收" />
            <dx:GridViewDataColumn FieldName="收款總計" Caption="收款總計" />
            <dx:GridViewDataColumn FieldName="POS加值金額" Caption="POS加值金額" />
            <dx:GridViewDataColumn FieldName="銀行應入帳金額" Caption="銀行應入帳金額" />
            <dx:GridViewDataColumn FieldName="卡機加值金額" Caption="卡機加值金額" />
            <dx:GridViewDataTextColumn FieldName="銀行存入日期" Caption="銀行存入日期" />
            <dx:GridViewDataColumn FieldName="銀行入帳金額" Caption="銀行入帳金額" />
            <dx:GridViewDataColumn FieldName="信用卡手續費" Caption="信用卡手續費" />
            <dx:GridViewDataColumn FieldName="當日差額" Caption="當日差額" />
            <dx:GridViewDataColumn FieldName="差異調整金額" Caption="差異調整金額" />
            <dx:GridViewDataColumn FieldName="累計差額" Caption="累計差額" />
            <dx:GridViewDataColumn FieldName="加值差額" Caption="加值差額" />
            <dx:GridViewDataColumn FieldName="加值差異調整" Caption="加值差異調整" />
            <dx:GridViewDataColumn FieldName="加值累計差額" Caption="加值累計差額" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
        </dx:ASPxGridViewExporter>
    </div>
</asp:Content>
