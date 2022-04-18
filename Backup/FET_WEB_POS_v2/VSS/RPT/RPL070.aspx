<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL070.aspx.cs" Inherits="VSS_RPT_RPL070"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtTradeDateStart.GetText() != '' && txtTradeDateEnd.GetText() != '') {
                if (txtTradeDateStart.GetValue() > txtTradeDateEnd.GetValue()) {
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
        <!--促銷/商品分析表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL070 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--交易日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="TradeDate_S" runat="server" ClientInstanceName="txtTradeDateStart">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="TradeDate_E" runat="server" ClientInstanceName="txtTradeDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--促銷代號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal9" runat="server" Text="促銷代號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 120px">
                        <tr>
                            <td>
                                <uc1:PopupControl ID="Promotions_No" runat="server" IsValidation="false" PopupControlName="PromotionsPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--商品類別-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal6" runat="server" Text="商品類別"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 120px">
                        <tr>
                            <td>
                                <%--<uc1:PopupControl ID="Product_Type" runat="server" IsValidation="false" PopupControlName="ProductCategory" />--%>
                                <dx:ASPxComboBox ID="cboProductCategory" runat="server">
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--商品料號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 120px">
                        <tr>
                            <td>
                                <uc1:PopupControl ID="Product_Code" runat="server" IsValidation="false" Width="120px"
                                    PopupControlName="ProductsPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--交易金額-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal26" runat="server" Text="交易金額"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="TradeMo_S" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="TradeMo_E" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--銷售人員-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 120px">
                        <tr>
                            <td>
                                <dx:ASPxComboBox ID="cbSALE_PERSON" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                                    SelectedIndex="0" Width="120px">
                                </dx:ASPxComboBox>
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
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="促銷代碼" />
            <dx:GridViewDataColumn FieldName="促銷名稱" Caption="促銷名稱" />
            <dx:GridViewDataTextColumn FieldName="商品類別" Caption="商品類別" />
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="商品料號" />
            <dx:GridViewDataColumn FieldName="商品名稱" Caption="商品名稱" />
            <dx:GridViewDataColumn FieldName="交易日期" Caption="交易日期" />
            <dx:GridViewDataColumn FieldName="交易金額" Caption="交易金額" />
            <dx:GridViewDataTextColumn FieldName="銷售人員" Caption="銷售人員">
            </dx:GridViewDataTextColumn>
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
