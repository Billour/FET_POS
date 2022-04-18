<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL048.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL048" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtTranDateStart.GetText() != '' && txtTranDateEnd.GetText() != '') {
                if (txtTranDateStart.GetValue() > txtTranDateEnd.GetValue()) {
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
        <!--HAPPY GO明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL048 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--類別-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbTYPE1" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                            <dx:ListEditItem Text="累點" Value="累點" />
                            <dx:ListEditItem Text="兌點" Value="兌點" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <!--分類-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, Type %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbTYPE2" runat="server" Width="120px">
                        <Items>
                        <%-- --%>
                            <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                            <dx:ListEditItem Text="銷售兌點" Value="銷售兌點" />
                            <dx:ListEditItem Text="代收兌點" Value="代收兌點" />
                            <dx:ListEditItem Text="一般累點" Value="一般累點" />
                            <dx:ListEditItem Text="來店禮累點" Value="來店禮累點" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <!--門市編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width: 126px">
                        <uc1:PopupControl ID="txtStoreNo" runat="server" IsValidation="false" PopupControlName="StoresPopup"
                            OnTextChanged="StoreNo_Changed" AutoPosBack="truet" />
                    </div>
                </td>
                <!--機台編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal11" runat="server" Text="機台編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbCashNo" runat="server" Width="120px">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <!--交易序號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtTransactionNo" runat="server" Width="120px">
                    </dx:ASPxTextBox>
                </td>
                <!--Happy Go卡號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, HappyGoCardNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtHGCardNo" runat="server" Width="120px">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <!--交易日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTranDateStart" runat="server" ClientInstanceName="txtTranDateStart">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTranDateEnd" runat="server" ClientInstanceName="txtTranDateEnd">
                                </dx:ASPxDateEdit>
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
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
        OnPageIndexChanged="gvMaster_PageIndexChanged">
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
        <Columns>
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="門市名稱" />
            <dx:GridViewDataTextColumn FieldName="交易日期" Caption="交易日期" />
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="交易序號" />
            <dx:GridViewDataTextColumn FieldName="機台編號" Caption="機台編號" />
            <dx:GridViewDataTextColumn FieldName="兌點/累點" Caption="兌點/累點" />
            <dx:GridViewDataTextColumn FieldName="分類" Caption="分類" />
            <dx:GridViewDataTextColumn FieldName="Happy Go卡號" Caption="Happy Go卡號" />
            <dx:GridViewDataTextColumn FieldName="項目名稱" Caption="項目名稱" />
            <dx:GridViewDataColumn FieldName="數量" Caption="數量" />
            <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="發票號碼" />
            <dx:GridViewDataTextColumn FieldName="累/兌點數" Caption="累/兌點數" />
            <dx:GridViewDataColumn FieldName="兌換金額" Caption="兌換金額" />
            <dx:GridViewDataTextColumn FieldName="銷售人員" Caption="銷售人員" />
        </Columns>
    </cc:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
    </dx:ASPxGridViewExporter>
</asp:Content>
