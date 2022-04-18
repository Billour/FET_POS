<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL006.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL006" %>

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
                alert("[分期期數]不允許為空，請重新輸入!");
                _gvEventArgs.processOnServer = false;
                return;
            }
            if (ASPxComboBox2.GetText() == '') {
                alert("[分期銀行]不允許為空，請重新輸入!");
                _gvEventArgs.processOnServer = false;
                return;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--信用卡分期付款明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL006 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <!--門市編號-->
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
                <td class="tdtxt">
                    <!--交易日期-->
                    <asp:Literal ID="Literal12" runat="server" Text="交易日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" ClientInstanceName="ASPxDateEditS">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="ASPxDateEditE">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--分期銀行：-->
                    <asp:Literal ID="Literal6" runat="server" Text="分期銀行"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="120px" 
                        ClientInstanceName="ASPxComboBox2" AutoPostBack="true" 
                        onselectedindexchanged="ASPxComboBox2_SelectedIndexChanged">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                            <dx:ListEditItem Text="中國信託商業銀行" Value="1" />
                            <dx:ListEditItem Text="花旗銀行" Value="2" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--分期期數：-->
                    <asp:Literal ID="Literal3" runat="server" Text="分期期數"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px" ClientInstanceName="ASPxComboBox1">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                            <dx:ListEditItem Text="3期" Value="3" />
                            <dx:ListEditItem Text="6期" Value="6" />
                            <dx:ListEditItem Text="12期" Value="12" />
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
        Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataTextColumn FieldName="交易日期" Caption="<%$ Resources:WebResources, TradeDate %>" />
            <dx:GridViewDataTextColumn FieldName="交易類型" Caption="<%$ Resources:WebResources, SaleType %>"/>
            <dx:GridViewDataTextColumn FieldName="交易類別" Caption="<%$ Resources:WebResources, TradeType %>"/>
            <dx:GridViewDataTextColumn FieldName="服務類型" Caption="<%$ Resources:WebResources, ServiceType %>"/>
            <dx:GridViewDataTextColumn FieldName="資料來源" Caption="<%$ Resources:WebResources, Source_Type %>"/>
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="<%$ Resources:WebResources, TransactionNo %>"/>
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="<%$ Resources:WebResources, PromotionCode %>"/>
            <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="<%$ Resources:WebResources, InvoiceNo %>"/>
            <dx:GridViewDataTextColumn FieldName="發票金額" Caption="<%$ Resources:WebResources, InvoiceAmount %>"/>
            <dx:GridViewDataTextColumn FieldName="分期代號" Caption="<%$ Resources:WebResources, InstalmentNo %>"/>
            <dx:GridViewDataTextColumn FieldName="分期銀行" Caption="<%$ Resources:WebResources, StagingBank %>"/>
            <dx:GridViewDataTextColumn FieldName="分期期數" Caption="<%$ Resources:WebResources, StagingCount %>"/>
            <dx:GridViewDataTextColumn FieldName="分期利率" Caption="<%$ Resources:WebResources, InstallmentRate %>"/>
            <dx:GridViewDataTextColumn FieldName="分期金額" Caption="分期金額"/>
            <dx:GridViewDataTextColumn FieldName="分期手續費" Caption="<%$ Resources:WebResources, InstallmentsFee %>"/>
            <dx:GridViewDataTextColumn FieldName="信用卡卡號" Caption="<%$ Resources:WebResources, CreditCardNo %>"/>
            <dx:GridViewDataTextColumn FieldName="門市分攤利率" Caption="<%$ Resources:WebResources, StoreRate %>"/>
            <dx:GridViewDataTextColumn FieldName="門市分攤手續費" Caption="門市分攤手續費"/>
            <dx:GridViewDataTextColumn FieldName="訂單通路" Caption="<%$ Resources:WebResources, OrderChain %>"/>
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
