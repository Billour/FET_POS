<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL011.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL011" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '')
            {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue())
                {
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
        <!--代收業務彙總表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL011 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--交易日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
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
                
                <!--代收類別：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CollectionClass %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="120px" SelectedIndex="0"
                        ValueType="System.String">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="遠傳帳單" Value="1" />
                            <dx:ListEditItem Text="和信帳單" Value="2" />
                            <dx:ListEditItem Text="Seednet帳單" Value="3" />
                            <dx:ListEditItem Text="遠通帳單(有單)" Value="4" />
                            <dx:ListEditItem Text="遠通帳單(無單)" Value="5" />
                            <dx:ListEditItem Text="速博帳單" Value="6" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
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
                                <uc1:popupcontrol id="ASPxTextBox1" runat="server" isvalidation="false" popupcontrolname="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:popupcontrol id="ASPxTextBox4" runat="server" isvalidation="false" popupcontrolname="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
                
                <!--付款方式-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddlPAID_MODE" runat="server" Width="120px" SelectedIndex="0"
                        ValueType="System.String">
                        <%--<Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="現金" Value="1" />
                            <dx:ListEditItem Text="信用卡" Value="2" />
                            <dx:ListEditItem Text="離線信用卡" Value="3" />
                            <dx:ListEditItem Text="分期付款" Value="4" />
                            <dx:ListEditItem Text="禮券" Value="5" />
                            <dx:ListEditItem Text="金融卡" Value="6" />
                            <dx:ListEditItem Text="Happy Go" Value="7" />
                        </Items>--%>
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
                    <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" OnPageIndexChanged="gvMaster_PageIndexChanged"
        Width="98%">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, TradeDate %>" Caption="<%$ Resources:WebResources, TradeDate %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
            <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataColumn FieldName="交易別" Caption="<%$ Resources:WebResources, TradeClass %>" />
            <dx:GridViewDataColumn FieldName="代收類別" Caption="<%$ Resources:WebResources, CollectionClass %>" />
            <dx:GridViewDataColumn FieldName="付款方式" Caption="<%$ Resources:WebResources, PaymentMethod %>" />
            <dx:GridViewDataColumn FieldName="信用卡卡別" Caption="<%$ Resources:WebResources, CreditCardType %>" />
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
