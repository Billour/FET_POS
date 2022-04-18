<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL031.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL031" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtTRADE_DATE_S.GetText() != '' && txtTRADE_DATE_E.GetText() != '') {
                if (txtTRADE_DATE_S.GetValue() > txtTRADE_DATE_E.GetValue()) {
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
        <!--各項促銷銷售分析收入表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL031 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <!--交易日期-->
                    <asp:Literal ID="lblTRADE_DATE" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblTRADE_DATE_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTRADE_DATE_S" runat="server" ClientInstanceName="txtTRADE_DATE_S">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="lblTRADE_DATE_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTRADE_DATE_E" runat="server" ClientInstanceName="txtTRADE_DATE_E">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--促銷代碼-->
                    <asp:Literal ID="lblPROMO_NO" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width: 200px">
                        <uc1:PopupControl ID="popPROMOTIONSPOPUPONLY" runat="server" IsValidation="false"
                            PopupControlName="PromotionsPopupOnly" />
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
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
        OnPageIndexChanged="gvMaster_PageIndexChanged">
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
        <Columns>
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="促銷代碼" />
            <dx:GridViewDataTextColumn FieldName="促銷名稱" Caption="促銷名稱" />
            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="商品名稱" />
            <dx:GridViewDataColumn FieldName="全區" Caption="全區" />
        </Columns>
    </cc:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
    </dx:ASPxGridViewExporter>
</asp:Content>
