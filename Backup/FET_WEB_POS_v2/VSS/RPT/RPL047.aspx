<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL047.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL047" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtCancelDateStart.GetText() != '' && txtCancelDateEnd.GetText() != '') {
                if (txtCancelDateStart.GetValue() > txtCancelDateEnd.GetValue()) {
                    alert("[作廢日期起值]不允許大於[作廢日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--發票作廢明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL047 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="查詢條件"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="choice" Text="全部" />
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="choice" Text="當月" />
                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName="choice" Text="跨月" />
                </td>
                <td class="tdtxt">
                    <!--門市編號-->
                    <asp:Literal ID="lblSTORE_NO" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblSTORE_NO_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_NO_S" runat="server" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="lblSTORE_NO_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_NO_E" runat="server" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--作廢日期-->
                    <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, CancelDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="CancelDate_S" runat="server" ClientInstanceName="txtCancelDateStart">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="CancelDate_E" runat="server" ClientInstanceName="txtCancelDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--發票號碼-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="InvoiceNo_S" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="InvoiceNo_E" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--發票金額-->
                    <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, InvoiceAmount %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="InvoiceAmount_S" runat="server" Width="100px">
                                    <ValidationSettings CausesValidation="false">
                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式，請重新輸入" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="InvoiceAmount_E" runat="server" Width="100px">
                                    <ValidationSettings CausesValidation="false">
                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式，請重新輸入" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--員工編號-->
                    <asp:Literal ID="Literal11" runat="server" Text=" <%$ Resources:WebResources, EmployeeNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="EmployeeNo_S" runat="server" IsValidation="false" PopupControlName="EmployeesPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="EmployeeNo_E" runat="server" IsValidation="false" PopupControlName="EmployeesPopup" />
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
            <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="門市編號">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataTextColumn FieldName="作廢日期" runat="server" Caption="<%$ Resources:WebResources, CancelDate %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="作廢發票號碼" Caption="<%$ Resources:WebResources, CancelInvoiceNo %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="折讓單號" Caption="折讓單號" />
            <dx:GridViewDataColumn FieldName="機台編號" Caption="機台編號" />
            <dx:GridViewDataColumn FieldName="發票日期" Caption="<%$ Resources:WebResources, InvoiceDate %>" />
            <dx:GridViewDataTextColumn FieldName="銷售金額" runat="server" Caption="銷售金額">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="稅額" Caption="<%$ Resources:WebResources, Tax%>" />
            <dx:GridViewDataTextColumn FieldName="發票金額" runat="server" Caption="<%$ Resources:WebResources, InvoiceAmount %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="付款方式" Caption="付款方式">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="銷售人員" Caption="<%$ Resources:WebResources, SalesClerk %>" />
            <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>" />
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
