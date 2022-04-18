<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL034.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL034" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--門市銷售量分析表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL034 %>"></asp:Literal>
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
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>                        
                            <td>
                                <uc1:PopupControl ID="pupPRODTYPENO_S" runat="server" PopupControlName="ProductType" Width="150px" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="pupPRODTYPENO_E" runat="server" PopupControlName="ProductType" Width="150px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品料號-->
                    <asp:Literal ID="lblPRODNO" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>                        
                            <td>
                                <uc1:PopupControl ID="txtPRODNO_S" runat="server" PopupControlName="ProductsPopup" />
                            </td>
                             <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPRODNO_E" runat="server" PopupControlName="ProductsPopup" />
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
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" OnClick="btnReset_Click"
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
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
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
        OnPageIndexChanged="gvMaster_PageIndexChanged" AutoGenerateColumns="false" >
        <Columns>
            <dx:GridViewDataColumn FieldName="ITEMCODE" Caption="ITEMCODE" />
            <dx:GridViewDataColumn FieldName="品名" Caption="<%$ Resources:WebResources, ProductName %>" />
            <dx:GridViewDataColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName %>" />
            <dx:GridViewDataColumn FieldName="全區" Caption="<%$ Resources:WebResources, Allarea %>" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
    </dx:ASPxGridViewExporter>
</asp:Content>
