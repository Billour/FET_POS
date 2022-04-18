<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL071.aspx.cs" Inherits="VSS_RPT_RPL071"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[日期區間起值]不允許大於[日期區間訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--折扣明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="折扣明細表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="日期區間"></asp:Literal>：
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
                <!--商品料號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal14" runat="server" Text="商品料號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width: 126px">
                        <uc1:PopupControl ID="popPLU" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                    </div>
                </td>

                <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="折扣原因"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width: 100px">
                        <%--<dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="90%">
                        </dx:ASPxTextBox>--%>
                        <dx:ASPxComboBox ID="ddlReason" runat="server" Width="120px" SelectedIndex="0" ValueType="System.String"></dx:ASPxComboBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="成本中心"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width: 126px">
                        <uc1:PopupControl ID="popCOSTCENTER" runat="server" IsValidation="false" PopupControlName="CostCenterPopup" />
                    </div>
                </td>

                <!--員工編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width: 126px">
                        <uc1:PopupControl ID="popEMPLOYEE" runat="server" IsValidation="false" PopupControlName="EmployeesPopup" />
                    </div>
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
            <dx:GridViewDataTextColumn FieldName="商品類別" Caption="商品類別" />
            <dx:GridViewDataTextColumn FieldName="折扣原因" Caption="折扣原因" />
            <dx:GridViewDataTextColumn FieldName="成本中心" Caption="成本中心" />
            <dx:GridViewDataTextColumn FieldName="折扣日期" Caption="折扣日期" />
            <dx:GridViewDataTextColumn FieldName="機台" Caption="機台" />
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="交易序號" />
            <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="發票號碼" />
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="商品料號" />
            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="商品名稱" />
            <dx:GridViewDataColumn FieldName="數量" Caption="數量" />
            <dx:GridViewDataColumn FieldName="售價" Caption="售價" />
            <dx:GridViewDataColumn FieldName="折扣金額" Caption="折扣金額" />
            <dx:GridViewDataColumn FieldName="折扣率" Caption="折扣率" />
            <dx:GridViewDataTextColumn FieldName="銷售人員" Caption="銷售人員" />
            <dx:GridViewDataTextColumn FieldName="員工類別" Caption="員工類別" />
            <dx:GridViewDataTextColumn FieldName="折扣說明" Caption="折扣說明" />
            
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
