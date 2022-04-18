<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL046.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL046" %>

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
        <asp:Literal ID="Literal1" runat="server" Text="門市銷售日報表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--查詢條件-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal6" runat="server" Text="查詢條件"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" RepeatDirection="Horizontal"
                        Width="143px">
                        <Items>
                            <dx:ListEditItem Text="總表" Value="0" />
                            <dx:ListEditItem Text="明細表" Value="1" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
                <!--門市編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_S" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_E" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
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
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTranDateEnd" runat="server" ClientInstanceName="txtTranDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--機台編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal11" runat="server" Text="機台編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbCashNo" runat="server" Width="123px">
                    </dx:ASPxComboBox>
                </td>
                <!--門號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="門號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="123px">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <!--商品類別-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox1" runat="server" IsValidation="false" PopupControlName="ProductType" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox2" runat="server" IsValidation="false" PopupControlName="ProductType" />
                            </td>
                        </tr>
                    </table>
                </td>
                <!--商品料號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox3" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox4" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--發票號碼-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal10" runat="server" Text="發票號碼"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="103px" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="103px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <!--交易金額-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal16" runat="server" Text="交易金額"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="103px" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="103px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--員工編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal19" runat="server" Text="員工編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox5" runat="server" IsValidation="false" PopupControlName="EmployeesPopup2" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox6" runat="server" IsValidation="false" PopupControlName="EmployeesPopup2" />
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
            <dx:GridViewDataTextColumn FieldName="交易日期" Caption="交易日期" />
            <dx:GridViewDataTextColumn FieldName="交易類別" Caption="交易類別" />
            <dx:GridViewDataTextColumn FieldName="機台編號" Caption="機台編號" />
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="交易序號" />
            <dx:GridViewDataTextColumn>
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                        <tr>
                            <td colspan="2" align="center" style="height:20px;border-bottom: 1px #8E8E8E  solid">
                                發票號碼
                            </td>
                        </tr>
                        <tr>
                            <td width="50%" style="height:20px;">
                                帳單號碼/條碼一
                            </td>
                            <td width="50%">
                                門號/條碼二
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <DataItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="right" width="100%" style="height: 30px">
                        <tr>
                            <td colspan="2" align="center" style="border-bottom: 1px #BEBEBE  solid; height: 50%;">
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#BIND("發票號碼")  %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" id="Barcode1" runat="server" width="50%">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("帳單號碼_條碼一")  %>' />
                            </td>
                            <td align="center" width="50%">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%#BIND("門號_條碼二")  %>' />
                            </td>
                        </tr>
                    </table>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="商品料號" />
            <dx:GridViewDataTextColumn>
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                        <tr>
                            <td align="center" style="height:20px; border-bottom: 1px #8E8E8E  solid">
                                商品名稱
                            </td>
                        </tr>
                        <tr>
                            <td style="height:20px;">
                                條碼三
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <DataItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="right" width="100%" style="height: 30px">
                        <tr>
                            <td align="center" style="border-bottom: 1px #BEBEBE  solid; height: 50%;">
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#BIND("商品名稱")  %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" id="Barcode1" runat="server" width="50%" style="height: 50%">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("條碼三")  %>' />
                            </td>
                        </tr>
                    </table>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="數量" Caption="數量" />
            <dx:GridViewDataColumn FieldName="未稅金額" Caption="未稅金額" />
            <dx:GridViewDataColumn FieldName="稅額" Caption="稅額" />
            <dx:GridViewDataColumn FieldName="銷售金額" Caption="銷售金額" />
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="促銷代碼" />
            <dx:GridViewDataTextColumn FieldName="備註" Caption="備註" />
        </Columns>
    </cc:ASPxGridView>
    <div id="DivSUM" runat="server">
        <cc:ASPxGridView ID="gvSUM" ClientInstanceName="gvSUM" runat="server" Width="100%"
            KeyFieldName="MACHINE_ID" Visible="false" OnPageIndexChanged="gvSUM_PageIndexChanged">
            <Styles>
                <Header Paddings-PaddingBottom="0"></Header>
                <cell Paddings-PaddingTop="0" Paddings-PaddingBottom="0"></cell>
            </Styles>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="23">
            </SettingsPager>
            <Columns>
                <dx:GridViewDataTextColumn>
                    <HeaderTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                            <tr>
                                <td colspan ="8" align="left" style="border-bottom: 1px #8E8E8E  solid; font-size:medium; height:30px;">
                                    <asp:Label ID="txtSUM_TITLE" runat="server" Text="門市總計" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="border-bottom: 1px #8E8E8E  solid; border-right: 1px #8E8E8E  solid; height:20px; vertical-align:middle;">
                                    項目
                                </td>
                                <td colspan="7" align="center" style="border-bottom: 1px #8E8E8E  solid">
                                    支付方式
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 12.5%; border-right: 1px #8E8E8E  solid; height:20px;">
                                    開立發票
                                </td>
                                <td align="center" style="width: 12.5%; border-right: 1px #8E8E8E  solid">
                                    現金
                                </td>
                                <td align="center" style="width: 12.5%; border-right: 1px #8E8E8E  solid">
                                    信用卡
                                </td>
                                <td align="center" style="width: 12.5%; border-right: 1px #8E8E8E  solid">
                                    分期付款
                                </td>
                                <td align="center" style="width: 12.5%; border-right: 1px #8E8E8E  solid">
                                    金融卡
                                </td>
                                <td align="center" style="width: 12.5%; border-right: 1px #8E8E8E  solid">
                                    禮券
                                </td>
                                <td align="center" style="width: 12.5%; border-right: 1px #8E8E8E  solid">
                                    HG
                                </td>
                                <td align="center" style="width: 12.5%;">
                                    加總
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <DataItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" align="right" width="100%">
                            <tr>
                                <td align="center" style="width: 12.5%; border-right: 1px #BEBEBE  solid; height:20px;">
                                    <dx:ASPxLabel ID="COL01" runat="server" Text='<%#BIND("TITLE")  %>' />
                                </td>
                                <td align="right" style="width: 12.5%; border-right: 1px #BEBEBE  solid; padding-right:5px;">
                                    <asp:HiddenField ID="MACHINE_ID" runat="server" Value='<%#BIND("MACHINE_ID")  %>' />
                                    <dx:ASPxLabel ID="COL02" runat="server" Text='<%#BIND("現金")  %>' />
                                </td>
                                <td align="right" style="width: 12.5%; border-right: 1px #BEBEBE  solid; padding-right:5px;">
                                    <dx:ASPxLabel ID="COL03" runat="server" Text='<%#BIND("信用卡")  %>' />
                                </td>
                                <td align="right" style="width: 12.5%; border-right: 1px #BEBEBE  solid; padding-right:5px;">
                                    <dx:ASPxLabel ID="COL04" runat="server" Text='<%#BIND("分期付款")  %>' />
                                </td>
                                <td align="right" style="width: 12.5%; border-right: 1px #BEBEBE  solid; padding-right:5px;">
                                    <dx:ASPxLabel ID="COL05" runat="server" Text='<%#BIND("金融卡")  %>' />
                                </td>
                                <td align="right" style="width: 12.5%; border-right: 1px #BEBEBE  solid; padding-right:5px;">
                                    <dx:ASPxLabel ID="COL06" runat="server" Text='<%#BIND("禮券")  %>' />
                                </td>
                                <td align="right" style="width: 12.5%; border-right: 1px #BEBEBE  solid; padding-right:5px;">
                                    <dx:ASPxLabel ID="COL07" runat="server" Text='<%#BIND("HG")  %>' />
                                </td>
                                <td align="right" style="width: 12.5%;">
                                    <dx:ASPxLabel ID="COL08" runat="server" Text='<%#BIND("加總")  %>' />
                                </td>
                            </tr>
                        </table>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
<%--                <dx:GridViewDataTextColumn>
                    <HeaderTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <DataItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" align="right" width="100%">
                            <tr>
                            </tr>
                        </table>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>--%>
            </Columns>
        </cc:ASPxGridView>
    </div>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvSUM">
    </dx:ASPxGridViewExporter>
</asp:Content>
<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL046.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL046" %>

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
        <asp:Literal ID="Literal1" runat="server" Text="門市銷售日報表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--查詢條件-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal6" runat="server" Text="查詢條件"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" RepeatDirection="Horizontal"
                        Width="143px">
                        <Items>
                            <dx:ListEditItem Text="總表" Value="0" />
                            <dx:ListEditItem Text="明細表" Value="1" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
                <!--門市編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_S" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_E" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
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
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTranDateEnd" runat="server" ClientInstanceName="txtTranDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--機台編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal11" runat="server" Text="機台編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbCashNo" runat="server" Width="123px">
                    </dx:ASPxComboBox>
                </td>
                <!--門號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="門號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="123px">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <!--商品類別-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox1" runat="server" IsValidation="false" PopupControlName="ProductType" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox2" runat="server" IsValidation="false" PopupControlName="ProductType" />
                            </td>
                        </tr>
                    </table>
                </td>
                <!--商品料號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox3" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox4" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--發票號碼-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal10" runat="server" Text="發票號碼"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="103px" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="103px" />
                            </td>
                        </tr>
                    </table>
                </td>
                
                <!--交易金額-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal16" runat="server" Text="交易金額"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="103px" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="103px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--員工編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal19" runat="server" Text="員工編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox5" runat="server" IsValidation="false" PopupControlName="EmployeesPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox6" runat="server" IsValidation="false" PopupControlName="EmployeesPopup" />
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
            <dx:GridViewDataTextColumn FieldName="交易日期" Caption="交易日期" />
            <dx:GridViewDataTextColumn FieldName="交易類別" Caption="交易類別" />
            <dx:GridViewDataTextColumn FieldName="機台編號" Caption="機台編號" />
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="交易序號" />
            <dx:GridViewDataTextColumn>
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                        <tr>
                            <td colspan ="2" align="center" style="border-bottom: 1px #8E8E8E  solid">
                                發票號碼
                            </td>
                        </tr>
                        <tr>
                            <td width="50%">
                                帳單號碼/條碼一
                            </td>
                            <td width="50%">
                                門號/條碼二
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <DataItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="right" width="100%" style="height:30px">
                        <tr>
                            <td colspan ="2" align="center" style="border-bottom: 1px #8E8E8E  solid; height:50%;">
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#BIND("發票號碼")  %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" ID="Barcode1" runat="server" width="50%">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("帳單號碼_條碼一")  %>' />
                            </td>
                            <td align="center" width="50%">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%#BIND("門號_條碼二")  %>' />
                            </td>
                        </tr>
                    </table>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="商品料號" />
            <dx:GridViewDataTextColumn>
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                        <tr>
                            <td align="center" style="border-bottom: 1px #8E8E8E  solid">
                                商品名稱
                            </td>
                        </tr>
                        <tr>
                            <td>
                                條碼三
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <DataItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="right" width="100%" style="height:30px">
                        <tr>
                            <td align="center" style="border-bottom: 1px #8E8E8E  solid; height:50%;">
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#BIND("商品名稱")  %>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" ID="Barcode1" runat="server" width="50%" style="height:50%">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("條碼三")  %>' />
                            </td>
                        </tr>
                    </table>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="數量" Caption="數量" />
            <dx:GridViewDataColumn FieldName="未稅金額" Caption="未稅金額" />
            <dx:GridViewDataColumn FieldName="稅額" Caption="稅額" />
            <dx:GridViewDataColumn FieldName="銷售金額" Caption="銷售金額" />
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="促銷代碼" />
            <dx:GridViewDataTextColumn FieldName="備註" Caption="備註" />
        </Columns>
    </cc:ASPxGridView>
    
    <cc:ASPxGridView ID="gvSUM" ClientInstanceName="gvSUM" runat="server" Width="100%" Visible="false"
     OnPageIndexChanged="gvSUM_PageIndexChanged">
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="21">
        </SettingsPager>
        <Columns>
            <dx:GridViewDataTextColumn>
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                        <tr>
                            <td align="center" style="border-bottom: 1px #8E8E8E  solid">
                                項目
                            </td>
                        </tr>
                        <tr>
                            <td align="center">開立發票</td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <DataItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="right" width="100%">
                        <tr>
                            <td align="center">
                                <dx:ASPxLabel ID="COL01" runat="server" Text='<%#BIND("TITLE")  %>' />
                            </td>
                        </tr>
                    </table>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn>
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                        <tr>
                            <td colspan="7" align="center" style="border-bottom: 1px #8E8E8E  solid">
                                支付方式
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">現金</td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">信用卡</td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">分期付款</td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">金融卡</td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">禮券</td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">HG</td>
                            <td align="center" style="width:14.28%; ">加總</td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <DataItemTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="right" width="100%">
                        <tr>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">
                                <dx:ASPxLabel ID="COL02" runat="server" Text='<%#BIND("現金")  %>' />
                            </td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">
                                <dx:ASPxLabel ID="COL03" runat="server" Text='<%#BIND("信用卡")  %>' />
                            </td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">
                                <dx:ASPxLabel ID="COL04" runat="server" Text='<%#BIND("分期付款")  %>' />
                            </td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">
                                <dx:ASPxLabel ID="COL05" runat="server" Text='<%#BIND("金融卡")  %>' />
                            </td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">
                                <dx:ASPxLabel ID="COL06" runat="server" Text='<%#BIND("禮券")  %>' />
                            </td>
                            <td align="center" style="width:14.28%; border-right:1px #8E8E8E  solid">
                                <dx:ASPxLabel ID="COL07" runat="server" Text='<%#BIND("HG")  %>' />
                            </td>
                            <td align="center" style="width:14.28%;">
                                <dx:ASPxLabel ID="COL08" runat="server" Text='<%#BIND("加總")  %>' />
                            </td>
                        </tr>
                    </table>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
        </Columns>
    </cc:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvSUM">
    </dx:ASPxGridViewExporter>
</asp:Content>--%>
