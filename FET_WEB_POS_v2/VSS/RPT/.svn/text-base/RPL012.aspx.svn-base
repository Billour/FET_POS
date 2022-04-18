<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="RPL012.aspx.cs" Inherits="VSS_RPT_RPL012" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--代收業務彙總表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL012 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--交易日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="交易日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--門市編號-->
                <td class="tdtxt">
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
            </tr>
            <tr>
                <!--代收服務類別：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CollectionClass %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="120px" SelectedIndex="0"
                        ValueType="System.String">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="" />
                            <dx:ListEditItem Text="遠傳帳單" Value="1" />
                            <dx:ListEditItem Text="和信帳單" Value="2" />
                            <dx:ListEditItem Text="Seednet帳單" Value="3" />
                            <dx:ListEditItem Text="遠通帳單(有單)" Value="4" />
                            <dx:ListEditItem Text="遠通帳單(無單)" Value="5" />
                            <dx:ListEditItem Text="速博帳單" Value="6" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <!--交易別：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TradeClass %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox4" runat="server" Width="120px" SelectedIndex="0"
                        ValueType="System.String">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="一般帳單" Value="一般帳單" />
                            <dx:ListEditItem Text="作廢帳單" Value="作廢帳單" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
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
        Width="98%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="門市編號" />
            <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataColumn FieldName="代收類別" Caption="<%$ Resources:WebResources, CollectionClass %>" />
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="<%$ Resources:WebResources, TransactionNo %>" />
            <dx:GridViewDataColumn FieldName="付款方式" Caption="<%$ Resources:WebResources, PaymentMethod %>" />
            <dx:GridViewDataColumn FieldName="交易別" Caption="<%$ Resources:WebResources, TradeClass%>" />
            <dx:GridViewDataTextColumn FieldName="客戶帳號" Caption="<%$ Resources:WebResources, CustomerID %>" />
            <dx:GridViewDataTextColumn FieldName="代收門號號碼" Caption="<%$ Resources:WebResources, CollectionMobNo %>" />
            <dx:GridViewDataColumn FieldName="代收金額" Caption="<%$ Resources:WebResources, CollectionAmount %>" />
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
