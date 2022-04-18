<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL057.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL057" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[日期起值]不允許大於[日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--商品迴轉率表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL057 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <!--商品類別-->
                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width: 140px">
                       
                        <dx:ASPxComboBox ID="cboProductCategory" runat="server">
                        </dx:ASPxComboBox>
                    </div>
                </td>
                <td class="tdtxt">
                    <!--商品料號-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox2" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品名稱-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="日期"></asp:Literal>：
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
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="txtOrdDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
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
        Width="100%"  OnPageIndexChanged ="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductCategory %>"
                Caption="<%$ Resources:WebResources, ProductCategory %>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCode %>" runat="server"
                Caption="<%$ Resources:WebResources, ProductCode %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductName %>"
                Caption="<%$ Resources:WebResources, ProductName%>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="銷貨" Caption="銷貨" />
            <dx:GridViewDataColumn FieldName="期初庫存" Caption="<%$ Resources:WebResources, BeginningStocks %>" />
            <dx:GridViewDataColumn FieldName="期末庫存" Caption="<%$ Resources:WebResources, EndingStocks %>" />
            <dx:GridViewDataTextColumn FieldName="週轉率" Caption="週轉率">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="月週轉天數" Caption="月週轉天數" />
            <dx:GridViewDataColumn FieldName="庫存天數" Caption="庫存天數" />
            <dx:GridViewDataColumn FieldName="暢滯銷" Caption="暢滯銷" />
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
