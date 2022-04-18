<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL055.aspx.cs" Inherits="VSS_RPT_RPL055"
    MasterPageFile="~/MasterPage.master" %>

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
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--庫存日報表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL055 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <tr>
                    <!--門市編號-->
                    <td class="tdtxt">
                        <asp:Literal ID="Literal8" runat="server" Text="門市編號"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table style="width: 345px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="ASPxTextBox2" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                                </td>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="ASPxTextBox3" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
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
                    <!--商品類別-->
                    <td class="tdtxt">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table style="width: 345px">
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
                    
                    <!--單品金額-->
                    <td class="tdtxt">
                        <asp:Literal ID="Literal5" runat="server" Text="單品金額"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table style="width: 250px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>                    
                </tr>
                <tr>
                    <!--商品料號-->
                    <td class="tdtxt">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table style="width: 345px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="ASPxComboBox4" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                                </td>
                                <td>
                                    <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="ASPxComboBox5" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                    <!--銷售金額-->
                    <td class="tdtxt">
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SalesAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table style="width: 250px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
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
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click">
                    <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                    </dx:ASPxButton>
                </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false" OnClick="btnReset_Click" >
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click" >
                <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="98%" OnPageIndexChanged ="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, StoreName %>" runat="server"
                Caption="<%$ Resources:WebResources, StoreName %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="交易序號" Caption="<%$ Resources:WebResources, TransactionNo %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, TradeDate %>" Caption="<%$ Resources:WebResources, TradeDate %>" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, InvoiceNo %>" Caption="<%$ Resources:WebResources, InvoiceNo %>" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, UnifiedBusinessNo %>"
                Caption="<%$ Resources:WebResources, UnifiedBusinessNo %>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCategory %>"
                runat="server" Caption="<%$ Resources:WebResources, ProductCategory %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCode %>" Caption="商品料號" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductName %>" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataColumn FieldName="單品金額" Caption="單品金額" />
            <dx:GridViewDataColumn FieldName="銷售金額" Caption="<%$ Resources:WebResources, SalesAmount %>" />
            <dx:GridViewDataTextColumn FieldName="備註" runat="server" Caption="<%$ Resources:WebResources, Remark %>" />
            <dx:GridViewDataTextColumn FieldName="狀態" Caption="狀態" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProcessedBy %>"
                runat="server" Caption="<%$ Resources:WebResources, ProcessedBy %>" />
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
