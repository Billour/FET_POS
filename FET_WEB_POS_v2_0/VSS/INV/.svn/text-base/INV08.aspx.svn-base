<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV08.aspx.cs" Inherits="VSS_INV08_INV08" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="func">
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                    <tr>
                        <td align="left">
                            <!--進貨驗收作業-->
                            <asp:Literal ID="Literal1" runat="server" Text="進貨驗收作業"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--PO/OE_NO-->
                                <asp:Literal ID="Literal2" runat="server" Text="PO/OE_NO"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="txt1" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--供貨商-->
                                <asp:Literal ID="Literal3" runat="server" Text="供應商"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                    <Items>
                                        <dx:ListEditItem Text="請選擇" Selected="true" />
                                        <dx:ListEditItem Text="供貨商1" />
                                        <dx:ListEditItem Text="供貨商2" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單狀態-->
                                <asp:Literal ID="Literal4" runat="server" Text="訂單狀態"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                    <Items>
                                        <dx:ListEditItem Text="請選擇" Selected="true" />
                                        <dx:ListEditItem Text="未驗收" />
                                        <dx:ListEditItem Text="部分驗收" />
                                        <dx:ListEditItem Text="已結案" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單/主配編號-->
                                <asp:Literal ID="Literal5" runat="server" Text=" 訂單/主配編號"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="TextBox6" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--驗收日期-->
                                <asp:Literal ID="Literal6" runat="server" Text=" 驗收日期"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
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
                                <asp:Literal ID="Literal9" runat="server" Text=" 商品編號"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="TextBox4" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </div>
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
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PO/OE_NO"
            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
            OnPageIndexChanged="gvMaster_PageIndexChanged">
            <Columns>
                <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/INV/INV09.aspx"
                    FieldName="PO/OE_NO" Caption="PO/OE_NO" PropertiesHyperLinkEdit-Style-Font-Underline="true">
                </dx:GridViewDataHyperLinkColumn>
                <dx:GridViewDataColumn FieldName="訂單編號" Caption="訂單/主配編號" />
                <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/INV/INV09.aspx"
                    FieldName="驗收單編號" Caption="驗收單編號" PropertiesHyperLinkEdit-Style-Font-Underline="true">
                </dx:GridViewDataHyperLinkColumn>
                <dx:GridViewDataColumn FieldName="門市編號" Caption="門市編號" />
                <dx:GridViewDataColumn FieldName="門市名稱" Caption="門市名稱" />
                <dx:GridViewDataColumn FieldName="訂單狀態" Caption="訂單狀態" />
                <dx:GridViewDataColumn FieldName="驗收日期" Caption="驗收日期" />
                <dx:GridViewDataColumn FieldName="人員" Caption="更新人員" />
                <dx:GridViewDataColumn FieldName="日期" Caption="更新日期" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="5">
            </SettingsPager>
        </cc:ASPxGridView>
    </div>
</asp:Content>
