<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL065.aspx.cs" Inherits="VSS_RPT_RPL065"  MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtSDate.GetText() != '' && txtEDate.GetText() != '') {
                if (txtSDate.GetValue() > txtEDate.GetValue()) {
                    alert("[退換貨日期起值]不允許大於[退換貨日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--退換貨明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="退換貨明細表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="退換貨日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtSDate" ClientInstanceName="txtSDate" runat="server" EditFormatString="yyyy/MM/dd">
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtEDate" ClientInstanceName="txtEDate" runat="server" EditFormatString="yyyy/MM/dd">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>               
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="txtInvoiceNo" runat="server" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval" style="width:211px">
                    <uc1:PopupControl ID="pupProdNo" runat="server" PopupControlName="ProductsPopup"  />
                </td>
            </tr>      
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal4" runat="server" Text="交易序號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="txtSALE_NO" runat="server" Text=""></asp:TextBox>
                </td>
            </tr>          
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>"></asp:Literal>：
                </td>
                <td class="tdval" style="width:211px">
                    <uc1:PopupControl ID="pupEmployeeNo" runat="server" PopupControlName="EmployeesPopup2" />
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
                    OnClick="btnReset_Clicked">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                    onclick="btnExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate"></div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged" 
        ondetailrowexpandedchanged="gvMaster_DetailRowExpandedChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="SALE_STATUS" Caption="交易型態"></dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="TRADE_DATE" Caption="<%$ Resources:WebResources, TradeDate %>" />
            <dx:GridViewDataColumn FieldName="INVOICE_NO" Caption="<%$ Resources:WebResources, InvoiceNo %>" />
            <dx:GridViewDataTextColumn FieldName="MACHINE_ID" Caption="收銀機號" />
            <dx:GridViewDataColumn FieldName="INVOICE_DATE" Caption="<%$ Resources:WebResources, InvoiceDate %>" />
            <dx:GridViewDataTextColumn FieldName="SALE_NO" Caption="<%$ Resources:WebResources, TransactionNo %>" />
            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataColumn FieldName="BEFORE_TAX" Caption="<%$ Resources:WebResources, SalesAmount %>" />
            <dx:GridViewDataColumn FieldName="TAX" Caption="<%$ Resources:WebResources, Tax %>" Width="80" />
            <dx:GridViewDataColumn FieldName="INVOICE_PRICE" Caption="<%$ Resources:WebResources, InvoiceAmount %>" />
            <dx:GridViewDataColumn FieldName="PAID_MODE" Caption="<%$ Resources:WebResources, PaymentMethod %>" />
            <dx:GridViewDataColumn FieldName="SALE_PERSON" Caption="<%$ Resources:WebResources, EmployeeNo %>" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10" />
    </cc:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster" ></dx:ASPxGridViewExporter>
    <div class="seperate"></div>
</asp:Content>
