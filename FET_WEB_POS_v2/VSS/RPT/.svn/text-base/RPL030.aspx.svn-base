<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL030.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL030" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--商品銷售統計表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL030 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal6" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_NO_S" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_NO_E" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
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
                                <dx:ASPxDateEdit ID="txtTranDateStart" runat="server">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTranDateEnd" runat="server">
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
                    <!--商品類別-->
                    <asp:Literal ID="lblPRODTYPENO" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPRODTYPENO_S" runat="server" IsValidation="false" PopupControlName="ProductType" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPRODTYPENO_E" runat="server" IsValidation="false" PopupControlName="ProductType" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--商品料號-->
                    <asp:Literal ID="lblPRODNO" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPRODNO_S" runat="server" PopupControlName="ProductsPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPRODNO_E" runat="server" PopupControlName="ProductsPopup" />
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
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="98%"
        OnPageIndexChanged="gvMaster_PageIndexChanged" AutoGenerateColumns="false">
        <Columns>
            <dx:GridViewDataColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, StoreName %>" runat="server" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCategory %>" Caption="<%$ Resources:WebResources, ProductCategory %>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCode %>" Caption="<%$ Resources:WebResources, ProductCode %>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductName %>" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_1 %>" Caption="<%$ Resources:WebResources, Segnment_1%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_2 %>" Caption="<%$ Resources:WebResources, Segnment_2%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_3 %>" Caption="<%$ Resources:WebResources, Segnment_3%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_4 %>" Caption="<%$ Resources:WebResources, Segnment_4%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_5 %>" Caption="<%$ Resources:WebResources, Segnment_5%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_6 %>" Caption="<%$ Resources:WebResources, Segnment_6%>" />
            <dx:GridViewDataTextColumn FieldName="銷售型態" Caption="銷售型態" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, Quantity %>" Caption="<%$ Resources:WebResources, Quantity %>" />
            <dx:GridViewDataColumn FieldName="金額" Caption="金額" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvExport" />
    </div>
    <cc:ASPxGridView ID="gvExport" ClientInstanceName="gvExport" runat="server" Width="98%"
        OnPageIndexChanged="gvMaster_PageIndexChanged" AutoGenerateColumns="false" Visible="false">
        <Columns>
            <dx:GridViewDataColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, StoreName %>" runat="server" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCategory %>" Caption="<%$ Resources:WebResources, ProductCategory %>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCode %>" Caption="<%$ Resources:WebResources, ProductCode %>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductName %>" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_1 %>" Caption="<%$ Resources:WebResources, Segnment_1%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_2 %>" Caption="<%$ Resources:WebResources, Segnment_2%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_3 %>" Caption="<%$ Resources:WebResources, Segnment_3%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_4 %>" Caption="<%$ Resources:WebResources, Segnment_4%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_5 %>" Caption="<%$ Resources:WebResources, Segnment_5%>" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, Segnment_6 %>" Caption="<%$ Resources:WebResources, Segnment_6%>" />
            <dx:GridViewDataTextColumn FieldName="銷售型態" Caption="銷售型態" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, Quantity %>" Caption="<%$ Resources:WebResources, Quantity %>" />
            <dx:GridViewDataColumn FieldName="金額" Caption="金額" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>    
</asp:Content>
