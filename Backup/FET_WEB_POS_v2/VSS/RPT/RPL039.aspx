<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL039.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL039" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[生效日期起值]不允許大於[生效日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--促代新增商品檢核表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL039 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, EffectiveDate %>"></asp:Literal>：
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
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        OnPageIndexChanged="gvMaster_PageIndexChanged" Width="100%">
        <Columns>
            <dx:GridViewDataColumn FieldName="促銷代碼" Caption="<%$ Resources:WebResources, PromotionCode %>" />
            <dx:GridViewDataTextColumn FieldName="促銷名稱" runat="server" Caption="<%$ Resources:WebResources, PromotionName %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="商品料號">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataColumn FieldName="組合促銷價格" Caption="<%$ Resources:WebResources, COMProPrice %>" />
            <dx:GridViewDataTextColumn FieldName="單機價" runat="server" Caption="<%$ Resources:WebResources, StandAlonePrice %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="組合促銷轉換值" Caption="組合促銷轉換值" />
            <dx:GridViewDataColumn FieldName="該促銷的補貼金額" Caption="該促銷的補貼金額" />
            <dx:GridViewDataColumn FieldName="基準補貼值" Caption="<%$ Resources:WebResources, StandAttPrice %>" />
            <dx:GridViewDataColumn FieldName="補貼差異值" Caption="<%$ Resources:WebResources, AttDiffPrice %>" />
            <dx:GridViewDataColumn FieldName="價格變動" Caption="<%$ Resources:WebResources, PriceChange %>" />
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
