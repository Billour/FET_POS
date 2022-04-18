<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL069.aspx.cs" Inherits="VSS_RPT_RPL069"
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
        <!--門市促銷分析表-->
        <asp:Literal ID="Literal1" runat="server" Text="門市促銷分析表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--促銷代碼-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="促銷代碼"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 332px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPromotionCode1" runat="server" IsValidation="false" PopupControlName="PromotionsPopupOnly" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPromotionCode2" runat="server" IsValidation="false" PopupControlName="PromotionsPopupOnly" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--交易日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 332px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" ClientInstanceName="txtOrdDateStart"
                                    Width="100%">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="txtOrdDateEnd"
                                    Width="100%">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
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
                    <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
        OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="促銷代碼" />
            <dx:GridViewDataTextColumn FieldName="促銷名稱" Caption="促銷名稱" />
            <dx:GridViewDataTextColumn FieldName="商品編號" Caption="商品編號" />
            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="商品名稱" />
            <dx:GridViewDataColumn FieldName="數量" Caption="數量" />
            <dx:GridViewDataColumn FieldName="銷售金額" Caption="銷售金額" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <cc:ASPxGridView ID="gvExpoter" ClientInstanceName="gvMaster" runat="server" Width="100%"
        Visible="false">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="促銷代碼" />
            <dx:GridViewDataTextColumn FieldName="促銷名稱" Caption="促銷名稱" />
            <dx:GridViewDataTextColumn FieldName="商品編號" Caption="商品編號" />
            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="商品名稱" />
            <dx:GridViewDataColumn FieldName="數量" Caption="數量" />
            <dx:GridViewDataColumn FieldName="銷售金額" Caption="銷售金額" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvExpoter">
    </dx:ASPxGridViewExporter>
    <div class="seperate">
    </div>
</asp:Content>
