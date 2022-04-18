<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL028.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL028" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;

            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[交易日期起值]不允許大於[交易日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }

            }

            if (ASPxTextBox1.GetText() != '' && ASPxTextBox2.GetText() != '') {
                if (parseInt(ASPxTextBox1.GetValue()) > parseInt(ASPxTextBox2.GetValue())) {
                    alert("[帳單金額起值]不允許大於[帳單金額訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }

            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--代收資費彙總表-->
        <asp:Literal ID="Literal1" runat="server" Text="代收資費彙總表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, CompyCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                            <dx:ListEditItem Text="FET" Value="1" />
                            <dx:ListEditItem Text="KGT" Value="2" />
                            <dx:ListEditItem Text="遠通" Value="4" />
                            <dx:ListEditItem Text="速博" Value="6" />
                            <dx:ListEditItem Text="Seednet" Value="3" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal11" runat="server" Text="交易型態"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                            <dx:ListEditItem Text="一般帳單" Value="NORMAL_BILL" />
                            <dx:ListEditItem Text="作廢帳單" Value="BAD_BILL" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="StoreNoPop_S" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="StoreNoPop_E" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
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
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, BillAmount %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px" ClientInstanceName="ASPxTextBox1">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100px" ClientInstanceName="ASPxTextBox2">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--帳單號碼-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, BillNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--門號-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, MobileNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="100px">
                                </dx:ASPxTextBox>
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
        OnPageIndexChanged="gvMaster_PageIndexChanged" Visible="false">
        <Columns> 
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption= "門市編號" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption= "門市名稱" />
            <dx:GridViewDataTextColumn FieldName="交易日期" Caption= "交易日期" />
            <dx:GridViewDataTextColumn FieldName="機台" Caption= "機台" />
            <dx:GridViewDataTextColumn FieldName="交易型態" Caption= "交易型態" />
            <dx:GridViewDataColumn FieldName="交易筆數" Caption= "交易筆數" />
            <dx:GridViewDataTextColumn FieldName="現金/信用卡" Caption= "付款方式" />
            <dx:GridViewDataColumn FieldName="信用卡筆數" Caption= "信用卡筆數" />
            <dx:GridViewDataColumn FieldName="遠傳現金" Caption= "遠傳現金" />
            <dx:GridViewDataColumn FieldName="和信現金" Caption= "和信現金" />
            <dx:GridViewDataColumn FieldName="速博現金" Caption= "速博現金" />
            <dx:GridViewDataColumn FieldName="遠通有單現金" Caption= "遠通有單現金" />
            <dx:GridViewDataColumn FieldName="遠通無單現金" Caption= "遠通無單現金" />
            <dx:GridViewDataColumn FieldName="SeedNet帳單現金" Caption= "SeedNet帳單現金" />
            <dx:GridViewDataColumn FieldName="遠傳帳單折抵" Caption= "遠傳帳單折抵" />
            <dx:GridViewDataColumn FieldName="和信帳單折抵" Caption= "和信帳單折抵" />
            <dx:GridViewDataColumn FieldName="快樂購總兌點數" Caption= "快樂購總兌點數" />
            <dx:GridViewDataColumn FieldName="遠傳快樂購兌點數" Caption= "遠傳快樂購兌點數" />
            <dx:GridViewDataColumn FieldName="和信快樂購兌點數" Caption= "和信快樂購兌點數" />
            <dx:GridViewDataColumn FieldName="遠傳帳單快樂購金" Caption= "遠傳帳單快樂購兌點金額" />
            <dx:GridViewDataColumn FieldName="和信帳單快樂購金" Caption= "和信帳單快樂購兌點金額" />
            <dx:GridViewDataTextColumn FieldName="信用卡別" Caption= "信用卡別" />
            <dx:GridViewDataColumn FieldName="遠傳信用卡" Caption= "遠傳信用卡" />
            <dx:GridViewDataColumn FieldName="和信信用卡" Caption= "和信信用卡" />
            <dx:GridViewDataColumn FieldName="速博信用卡" Caption= "速博信用卡" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <cc:ASPxGridView ID="gvExport" ClientInstanceName="gvExport" runat="server" Width="100%" OnPageIndexChanged="gvExport_PageIndexChanged" >
        <Columns>
             <dx:GridViewDataTextColumn FieldName="門市編號" Caption= "門市編號" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption= "門市名稱" />
            <dx:GridViewDataTextColumn FieldName="交易日期" Caption= "交易日期" />
            <dx:GridViewDataTextColumn FieldName="機台" Caption= "機台" />
            <dx:GridViewDataTextColumn FieldName="交易型態" Caption= "交易型態" />
            <dx:GridViewDataColumn FieldName="交易筆數" Caption= "交易筆數" />
            <dx:GridViewDataTextColumn FieldName="現金/信用卡" Caption= "現金/信用卡" />
            <dx:GridViewDataColumn FieldName="信用卡筆數" Caption= "信用卡筆數" />
            <dx:GridViewDataColumn FieldName="遠傳現金" Caption= "遠傳現金" />
            <dx:GridViewDataColumn FieldName="和信現金" Caption= "和信現金" />
            <dx:GridViewDataColumn FieldName="速博現金" Caption= "速博現金" />
            <dx:GridViewDataColumn FieldName="遠通有單現金" Caption= "遠通有單現金" />
            <dx:GridViewDataColumn FieldName="遠通無單現金" Caption= "遠通無單現金" />
            <dx:GridViewDataColumn FieldName="SeedNet帳單現金" Caption= "SeedNet帳單現金" />
            <dx:GridViewDataColumn FieldName="遠傳帳單折抵" Caption= "遠傳帳單折抵" />
            <dx:GridViewDataColumn FieldName="和信帳單折抵" Caption= "和信帳單折抵" />
            <dx:GridViewDataColumn FieldName="快樂購總兌點數" Caption= "快樂購總兌點數" />
            <dx:GridViewDataColumn FieldName="遠傳快樂購兌點數" Caption= "遠傳快樂購兌點數" />
            <dx:GridViewDataColumn FieldName="和信快樂購兌點數" Caption= "和信快樂購兌點數" />
            <dx:GridViewDataColumn FieldName="遠傳帳單快樂購金" Caption= "遠傳帳單快樂購兌點金額" />
            <dx:GridViewDataColumn FieldName="和信帳單快樂購金" Caption= "和信帳單快樂購兌點金額" />
            <dx:GridViewDataTextColumn FieldName="信用卡別" Caption= "信用卡別" />
            <dx:GridViewDataColumn FieldName="遠傳信用卡" Caption= "遠傳信用卡" />
            <dx:GridViewDataColumn FieldName="和信信用卡" Caption= "和信信用卡" />
            <dx:GridViewDataColumn FieldName="速博信用卡" Caption= "速博信用卡" />

        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvExport">
        </dx:ASPxGridViewExporter>
    </div>
</asp:Content>
