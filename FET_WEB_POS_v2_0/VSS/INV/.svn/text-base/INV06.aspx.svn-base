<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV06.aspx.cs" Inherits="VSS_INV06_INV06" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title><</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="func">
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                    <tr>
                        <td align="left">
                            <!--退倉作業-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousing %>"></asp:Literal>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉單號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox1" runat="server" Width="80px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉日-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, WarehousedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--商品編號-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox4" runat="server" Width="80px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉狀態-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStatus %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                                <Items>
                                    <dx:ListEditItem Text="ALL" Selected="true" />
                                    <dx:ListEditItem Text="未完成" Value="00" />
                                    <dx:ListEditItem Text="已完成" Value="01" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="退倉單號"
            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
            OnPageIndexChanged="gvMaster_PageIndexChanged">
            <Columns>
                <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/INV/INV07.aspx"
                    FieldName="退倉單號" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"
                    PropertiesHyperLinkEdit-Style-Font-Underline="true">
                </dx:GridViewDataHyperLinkColumn>
                <dx:GridViewDataColumn FieldName="退倉開始日" Caption="<%$ Resources:WebResources, ReturnWarehousingStartDate %>" />
                <dx:GridViewDataColumn FieldName="退倉結束日" Caption="<%$ Resources:WebResources, ReturnWarehousingEndDate %>" />
                <dx:GridViewDataColumn FieldName="退倉日" Caption="<%$ Resources:WebResources, WarehousedDate %>" />
                <dx:GridViewDataColumn FieldName="退倉狀態" Caption="<%$ Resources:WebResources, ReturnWarehousingStatus %>" />
                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="5">
            </SettingsPager>
        </cc:ASPxGridView>
    </div>
</asp:Content>
