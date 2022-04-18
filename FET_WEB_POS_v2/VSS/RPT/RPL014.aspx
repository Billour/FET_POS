<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL014.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL014" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
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
        <!--統一發票明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL014 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtStoreNo_S" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtStoreNo_E" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>            
                <td class="tdtxt">
                    <!--訂單日期-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, InvoiceDate %>"></asp:Literal>：
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
                <!--發票號碼-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                            <td><dx:ASPxTextBox ID="INVOICE_S" runat="server" Width="100px"></dx:ASPxTextBox></td>
                            <td><asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                            <td><dx:ASPxTextBox ID="INVOICE_E" runat="server" Width="100px"></dx:ASPxTextBox></td>
                        </tr>
                    </table>
                </td>            
                <td class="tdtxt">
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, InvoiceAmount %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td><asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                            <td><dx:ASPxTextBox ID="AMOUNT_S" runat="server" Width="100px"></dx:ASPxTextBox></td>
                            <td><asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                            <td><dx:ASPxTextBox ID="AMOUNT_E" runat="server" Width="100px"></dx:ASPxTextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td><asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                            <td><uc1:PopupControl ID="popEMPLOYEE_S" runat="server" IsValidation="false" PopupControlName="EmployeesPopup" /></td>
                            <td><asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                            <td><uc1:PopupControl ID="popEMPLOYEE_E" runat="server" IsValidation="false" PopupControlName="EmployeesPopup" /></td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--交易類型：-->
                    <asp:Literal ID="Literal7" runat="server" Text="課稅別"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="0" />
                            <dx:ListEditItem Text="應稅" Value="應稅" />
                            <dx:ListEditItem Text="零稅" Value="零稅" />
                            <dx:ListEditItem Text="免稅" Value="免稅" />
                            <dx:ListEditItem Text="作廢" Value="作廢" />
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
    </div><!--GridViewDataColumn-->
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" runat="server" Caption="門市名稱" />
            <dx:GridViewDataTextColumn FieldName="發票日期" runat="server" Caption="發票日期" />
            <dx:GridViewDataTextColumn FieldName="發票號碼" runat="server" Caption="發票號碼" />
            <dx:GridViewDataTextColumn FieldName="格式" runat="server" Caption="格式" />
            <dx:GridViewDataTextColumn FieldName="機台" runat="server" Caption="機台" />
            <dx:GridViewDataTextColumn FieldName="課稅別" runat="server" Caption="課稅別" />
            <dx:GridViewDataTextColumn FieldName="客戶統編" runat="server" Caption="客戶統編" />
            <dx:GridViewDataColumn FieldName="銷售額" runat="server" Caption="銷售額" />
            <dx:GridViewDataColumn FieldName="稅額" runat="server" Caption="稅額" />
            <dx:GridViewDataColumn FieldName="發票總金額" runat="server" Caption="發票總金額" />
            <dx:GridViewDataColumn FieldName="員工編號" runat="server" Caption="員工編號" />
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
