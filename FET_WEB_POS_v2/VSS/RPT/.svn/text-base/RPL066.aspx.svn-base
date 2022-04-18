<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL066.aspx.cs" Inherits="VSS_RPT_RPL066"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[維修日期起值]不允許大於[維修日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
            if (ASPxDateEdit1.GetText() != '' && ASPxDateEdit2.GetText() != '') {
                if (ASPxDateEdit1.GetValue() > ASPxDateEdit2.GetValue()) {
                    alert("[發票日期起值]不允許大於[發票日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--維修費明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="維修費明細表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Lit3" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <dx:ASPxTextBox ID="Labelx" runat="server" Text="" Width="100px" />
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="維修單號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtHRS_NO" runat="server" Text="" Width="100px" />
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="維修日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" ClientInstanceName="txtOrdDateStart" EditFormatString="yyyy/MM/dd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="txtOrdDateEnd" EditFormatString="yyyy/MM/dd" >
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
                    <asp:Literal ID="Literal8" runat="server" Text="IMEI"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="Textbox3" runat="server" Text="" Width="100px" />
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal10" runat="server" Text="發票日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ClientInstanceName="ASPxDateEdit1" EditFormatString="yyyy/MM/dd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ClientInstanceName="ASPxDateEdit2" EditFormatString="yyyy/MM/dd">
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
                    <!--維護廠商-->
                    <asp:Literal ID="Literal9" runat="server" Text="維護廠商"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbMaintenance" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--處理人員-->
                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProcessedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbSALE_PERSON" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                        SelectedIndex="0" Width="120px">
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
        Width="98%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="門市編號">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, StoreName %>" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataColumn FieldName="維修單號" Caption="維修單號" />
            <dx:GridViewDataColumn FieldName="維修日期" Caption="維修日期" />
            <dx:GridViewDataColumn FieldName="維修廠商" Caption="維修廠商" />
            <dx:GridViewDataTextColumn FieldName="商品類別" Caption="<%$ Resources:WebResources, ProductCategory %>" />
            <dx:GridViewDataTextColumn FieldName="廠牌" Caption="廠牌" />
            <dx:GridViewDataTextColumn FieldName="型號" Caption="型號" />
            <dx:GridViewDataTextColumn FieldName="IMEI" Caption="IMEI" />
            <dx:GridViewDataColumn FieldName="維修費用" Caption="維修費用" />
            <dx:GridViewDataColumn FieldName="發票日期" Caption="發票日期" />
            <dx:GridViewDataColumn FieldName="發票號碼" Caption="<%$ Resources:WebResources, InvoiceNo %>" />
            <dx:GridViewDataColumn FieldName="處理人員" Caption="<%$ Resources:WebResources, ProcessedBy %>" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster" />
    </div>
</asp:Content>
