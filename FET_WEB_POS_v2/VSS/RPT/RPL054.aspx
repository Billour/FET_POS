<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL054.aspx.cs" Inherits="VSS_RPT_RPL054"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--門市商品銷售統計表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL054 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CheckOption %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:RadioButtonList ID="rbCheck" runat="server" RepeatDirection="Horizontal" Width="250px">
                        <asp:ListItem Text="ALL" Value="ALL" />
                        <asp:ListItem Text="一般商品" Value="0" />
                        <asp:ListItem Text="寄銷商品" Value="1" />
                    </asp:RadioButtonList>
                </td>
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
                                <dx:ASPxDateEdit ID="txtTRADE_DATE_S" runat="server" ClientInstanceName="txtSDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="lblTRADE_DATE_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTRADE_DATE_E" runat="server" ClientInstanceName="txtEDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品類別-->
                    <asp:Literal ID="lblPRODTYPENO" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="pupTYPE_S" runat="server" IsValidation="false" PopupControlName="ProductType"
                                    Width="150px" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="pupTYPE_E" runat="server" IsValidation="false" PopupControlName="ProductType"
                                    Width="150px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--商品料號-->
                    <asp:Literal ID="lblPRODNO" runat="server" Text="商品料號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <uc1:PopupControl ID="txtPRODNO_S" runat="server" PopupControlName="ProductsPopup" />
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPRODNO_E" runat="server" PopupControlName="ProductsPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--銷售型態-->
                    <asp:Literal ID="lblSALE_STATUS" runat="server" Text="銷售型態"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddlSALE_STATUS" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                            <dx:ListEditItem Text="銷貨" Value="銷貨" />
                            <dx:ListEditItem Text="銷退" Value="銷退" />
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
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false" OnClick="btnReset_Click">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                    OnClick="btnExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <table width="98%">
        <tr>
            <td>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
                    OnPageIndexChanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="交易日期" Caption="交易日期" />
                        <dx:GridViewDataTextColumn FieldName="商品料號" Caption="商品料號" />
                        <dx:GridViewDataColumn FieldName="商品名稱" Caption="商品名稱" />
                        <dx:GridViewDataColumn FieldName="數量" Caption="數量" />
                        <dx:GridViewDataColumn FieldName="金額" Caption="金額" />
                        <dx:GridViewDataColumn FieldName="銷售型態" Caption="銷售型態" />
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                </cc:ASPxGridView>
            </td>
        </tr>
    </table>
    <cc:ASPxGridView ID="gvExporter" ClientInstanceName="gvMaster" runat="server" Width="100%"
        Visible="false" >
        <Columns>
            <dx:GridViewDataColumn FieldName="交易日期" Caption="交易日期" />
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="商品料號" />
            <dx:GridViewDataColumn FieldName="商品名稱" Caption="商品名稱" />
            <dx:GridViewDataColumn FieldName="數量" Caption="數量" />
            <dx:GridViewDataColumn FieldName="金額" Caption="金額" />
            <dx:GridViewDataColumn FieldName="銷售型態" Caption="銷售型態" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvExporter">
        </dx:ASPxGridViewExporter>
    </div>
</asp:Content>
