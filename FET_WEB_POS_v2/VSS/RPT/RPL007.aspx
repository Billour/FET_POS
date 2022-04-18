<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL007.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL007" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (ASPxDateEdit1.GetText() != '' && ASPxDateEdit2.GetText() != '') {
                if (ASPxDateEdit1.GetValue() > ASPxDateEdit2.GetValue()) {
                    alert("[交易日期起值]不允許大於[交易日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
//            if (ASPxComboBox3.GetText() == '') {
//                alert("[服務類型]不允許為空，請重新輸入!");
//                _gvEventArgs.processOnServer = false;
//                return;
//            }
//            if (ASPxComboBox4.GetText() == '') {
//                alert("[交易類型]不允許為空，請重新輸入!");
//                _gvEventArgs.processOnServer = false;
//                return;
//            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--門市交易明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL07 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--交易日期：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ClientInstanceName="ASPxDateEdit1">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ClientInstanceName="ASPxDateEdit2">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--交易類型：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SaleType %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbSaleType" runat="server" Width="120px" ClientInstanceName="ASPxComboBox4" OnSelectedIndexChanged="cbSaleType_OnChanged" AutoPostBack="true">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="銷售交易" Value="1" />
                            <dx:ListEditItem Text="帳單代收" Value="2" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <!--門市編號：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtStore_S" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtStore_E" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
                <!--交易類別：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TradeType %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbTradeType" runat="server" Width="120px" ClientInstanceName="ASPxComboBox4">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="銷售" Value="2" />
                            <dx:ListEditItem Text="退貨作廢" Value="3" />
                            <dx:ListEditItem Text="跨月退貨作廢" Value="4" />
                            <dx:ListEditItem Text="換貨作廢" Value="5" />
                            <dx:ListEditItem Text="跨月換貨作廢" Value="6" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <!--商品料號：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtProdNo_S" runat="server" PopupControlName="ProductsPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtProdNo_E" runat="server" PopupControlName="ProductsPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
                <!--服務類型：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ServiceType %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbServiceType" runat="server" Width="120px" ClientInstanceName="ASPxComboBox3" >
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="單品" Value="1" />
                            <dx:ListEditItem Text="組合促銷" Value="2" />
                            <dx:ListEditItem Text="預購轉銷售" Value="2" />
                            <dx:ListEditItem Text="Happy Go折抵" Value="3" />
                            <dx:ListEditItem Text="銷售折扣" Value="4" />
                            <dx:ListEditItem Text="門市特殊客訴折扣" Value="5" />
                            <dx:ListEditItem Text="加價購商品" Value="6" />
                            <dx:ListEditItem Text="HG來店禮" Value="7" />
                            <dx:ListEditItem Text="授信通聯" Value="8" />
                            <dx:ListEditItem Text="租賃" Value="9" />
                            <dx:ListEditItem Text="租賃折扣" Value="10" />
                            <dx:ListEditItem Text="舊機換新機折扣" Value="11" />
                            <dx:ListEditItem Text="贈品" Value="12" />
                            <dx:ListEditItem Text="Happy Go加價購" Value="13" />
                            <dx:ListEditItem Text="遠傳帳單" Value="A1" />
                            <dx:ListEditItem Text="和信帳單" Value="A2" />
                            <dx:ListEditItem Text="Seednet帳單" Value="A3" />
                            <dx:ListEditItem Text="遠通帳單(有單)" Value="A4" />
                            <dx:ListEditItem Text="遠通帳單(無單)" Value="A5" />
                            <dx:ListEditItem Text="速博帳單" Value="A6" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <!--促銷代碼：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtPromoNo" runat="server" Width="100px" ClientInstanceName="ASPxTextBox5">
                    </dx:ASPxTextBox>
                </td>
                <!--資料來源：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Source_Type %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbSource_Type" runat="server" Width="120px" ClientInstanceName="ASPxComboBox4">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="新啟用" Value="1" />
                            <dx:ListEditItem Text="續約" Value="2" />
                            <dx:ListEditItem Text="代收" Value="3" />
                            <dx:ListEditItem Text="變更促代" Value="4" />
                            <dx:ListEditItem Text="線上儲值" Value="5" />
                            <dx:ListEditItem Text="維修" Value="6" />
                            <dx:ListEditItem Text="網購" Value="10" />
                            <dx:ListEditItem Text="POS" Value="11" />
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
            <dx:GridViewDataTextColumn FieldName="門市區域" Caption="<%$ Resources:WebResources, StoreArea %>"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" runat="server"></dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataColumn FieldName="交易日期" Caption="<%$ Resources:WebResources, TradeDate %>" />
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="<%$ Resources:WebResources, TransactionNo %>" />
            <dx:GridViewDataTextColumn FieldName="發票憑證號碼" Caption="<%$ Resources:WebResources, InvoicedocNo %>" />
            <dx:GridViewDataTextColumn FieldName="機台" Caption="<%$ Resources:WebResources, CashRegister %>" />
            <dx:GridViewDataColumn FieldName="交易類型" Caption="<%$ Resources:WebResources, SaleType %>" />
            <dx:GridViewDataColumn FieldName="交易類別" Caption="<%$ Resources:WebResources, TradeType %>" />
            <dx:GridViewDataColumn FieldName="服務類型" Caption="<%$ Resources:WebResources, ServiceType %>" />
            <dx:GridViewDataColumn FieldName="資料來源" Caption="<%$ Resources:WebResources, Source_Type %>" />
            <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>" />
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />
            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
            <dx:GridViewDataTextColumn FieldName="促銷代號" Caption="<%$ Resources:WebResources, PromotionCode %>" />
            <dx:GridViewDataTextColumn FieldName="客戶帳號" Caption="<%$ Resources:WebResources, CustomerID %>" />
            <dx:GridViewDataTextColumn FieldName="啟用續約門號" Caption="<%$ Resources:WebResources, Enableaccount %>" />
            <dx:GridViewDataColumn FieldName="稅別" Caption="<%$ Resources:WebResources, TaxType %>" />
            <dx:GridViewDataColumn FieldName="未稅金額" Caption="<%$ Resources:WebResources, FreeTaxAmount %>" />
            <dx:GridViewDataColumn FieldName="稅額" Caption="<%$ Resources:WebResources, TaxAmount %>" />
            <dx:GridViewDataColumn FieldName="應收金額" Caption="<%$ Resources:WebResources, ReceiveAmount %>" />
            <dx:GridViewDataTextColumn FieldName="會計科目" Caption="<%$ Resources:WebResources, AccountingSubject %>" />
            <dx:GridViewDataTextColumn FieldName="原交易序號" Caption="原交易序號" />
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
