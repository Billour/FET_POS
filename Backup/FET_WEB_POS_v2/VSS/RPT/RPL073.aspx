<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL073.aspx.cs" Inherits="VSS_RPT_RPL073"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (ASPxDateEditS.GetText() != '' && ASPxDateEditE.GetText() != '') {
                if (ASPxDateEditS.GetValue() > ASPxDateEditE.GetValue()) {
                    alert("[交易日期起值]不允許大於[交易日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
            if (ASPxComboBox1.GetText() == '') {
                alert("[折扣類別]不允許為空，請重新輸入!");
                _gvEventArgs.processOnServer = false;
                return;
            }
            if (ASPxComboBox2.GetText() == '') {
                alert("[服務類型]不允許為空，請重新輸入!");
                _gvEventArgs.processOnServer = false;
                return;
            }
            if (ASPxComboBox3.GetText() == '') {
                alert("[兌點類別]不允許為空，請重新輸入!");
                _gvEventArgs.processOnServer = false;
                return;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--集團卡明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL073 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <center>
            <table width="70%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdtxt">
                        <!--門市-->
                        <asp:Literal ID="Literal12" runat="server" Text="門市編號"></asp:Literal>：
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
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="ASPxTextBox4" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table style="width: 200px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtTranDateStart" runat="server" ClientInstanceName="ASPxDateEditS">
                                        <%--<ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>--%>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtTranDateEnd" runat="server" ClientInstanceName="ASPxDateEditE">
                                        <%--<ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>--%>
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, DiscountClass %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="cbDiscountClass" runat="server" Width="120px" ClientInstanceName="ASPxComboBox1">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                <dx:ListEditItem Text="銷售兌點" Value="銷售兌點" />
                                <dx:ListEditItem Text="代收兌點" Value="代收兌點" />
                                <dx:ListEditItem Text="一般累點" Value="一般累點" />
                                <dx:ListEditItem Text="來店禮累點" Value="來店禮累點" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ServiceType %>"></asp:Literal>：
                    </td>
                    <td align="left">
                        <dx:ASPxComboBox ID="cbServiceType" runat="server" Width="120px" ClientInstanceName="ASPxComboBox2">
                            <Items>
                                <%--<dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                <dx:ListEditItem Text="銷售交易" Value="銷售交易" />
                                <dx:ListEditItem Text="帳單代收" Value="帳單代收" />--%>
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
                    <td class="tdtxt">
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ChangeClass %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="cbChangeClass" runat="server" Width="120px" OnSelectedIndexChanged="on_SelectedChanged"
                            AutoPostBack="true" ClientInstanceName="ASPxComboBox3">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                <dx:ListEditItem Text="兌點" Value="兌點" />
                                <dx:ListEditItem Text="累點" Value="累點" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </center>
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
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
        OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataTextColumn FieldName="日期" Caption="<%$ Resources:WebResources, Date %>" />
            <dx:GridViewDataTextColumn FieldName="機台" Caption="<%$ Resources:WebResources, CashRegister %>" />
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="<%$ Resources:WebResources, TransactionNo %>" />
            <dx:GridViewDataTextColumn FieldName="折扣類別" Caption="<%$ Resources:WebResources, DiscountClass %>" />
            <dx:GridViewDataTextColumn FieldName="兌點類別" Caption="<%$ Resources:WebResources, ChangeClass %>" />
            <dx:GridViewDataTextColumn FieldName="集團卡卡號" Caption="<%$ Resources:WebResources, HG_CARD_NO %>" />

            <dx:GridViewDataColumn FieldName="交易類型" Caption="<%$ Resources:WebResources, SaleType %>" />
            <dx:GridViewDataColumn FieldName="交易類別" Caption="<%$ Resources:WebResources, TradeType %>" />
            <dx:GridViewDataColumn FieldName="服務類型" Caption="<%$ Resources:WebResources, ServiceType %>" />
            <dx:GridViewDataColumn FieldName="資料來源" Caption="<%$ Resources:WebResources, Source_Type %>" />

            <%--<dx:GridViewDataTextColumn FieldName="服務類型" Caption="<%$ Resources:WebResources, ServiceType %>" />
            <dx:GridViewDataTextColumn FieldName="交易類別" Caption="<%$ Resources:WebResources, TradeType %>" />--%>
            <dx:GridViewDataTextColumn FieldName="折扣代號" Caption="<%$ Resources:WebResources, DiscountNo %>" />
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="<%$ Resources:WebResources, PromotionCode %>" />
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />
            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
            <dx:GridViewDataTextColumn FieldName="累兌點說明" Caption="<%$ Resources:WebResources, CONVERT_NAME %>" />
            <dx:GridViewDataTextColumn FieldName="兌換點數" Caption="兌換點數" />
            <dx:GridViewDataTextColumn FieldName="兌換金額" Caption="兌換金額" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
    </dx:ASPxGridViewExporter>
    <div class="seperate">
    </div>
</asp:Content>
