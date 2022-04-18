<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="CON05.aspx.cs" Inherits="VSS_CONS_CON05" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品訂單查詢-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentOrderSearch %>"></asp:Literal>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--訂單日期-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxDateEdit ID="txtORDDATE" runat="server">
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--訂單編號-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtORDNO" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--訂單狀態-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="ddlSTATUS" runat="server">
                            <Items>
                                <dx:ListEditItem Value="0" Text="已存檔" Selected="true" />
                                <dx:ListEditItem Value="1" Text="轉單中" />
                                <dx:ListEditItem Value="2" Text="已成單" />
                                <dx:ListEditItem Value="3" Text="待進貨" />
                                <dx:ListEditItem Value="4" Text="已驗收" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品料號-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup3"
                            KeyFieldValue1="consignmentsale" KeyFieldValue2="PRODNO" />                    
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <dx:ASPxLabel ID="Literal23" runat="server" Text="<%$ Resources:WebResources, StoreNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" colspan="2" nowrap="nowrap">
                        <dx:ASPxLabel ID="lblSTORE" runat="server">
                        </dx:ASPxLabel>
                        <uc1:PopupControl ID="pcSTORENO" runat="server" ClientInstanceName="pcSTORENO" PopupControlName="StoresPopup" />
                    </td>
                </tr>
            </table>
        </div>

        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            SkinID="ResetButton" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="CSM_ORDERM_ID"
                    Width="100%" AutoGenerateColumns="False" EnableRowsCache="False" OnPageIndexChanged="gvMaster_PageIndexChanged"
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                    <Columns>
                        <dx:GridViewDataColumn Visible="false" FieldName="CSM_ORDERM_ID" />
                        <dx:GridViewDataTextColumn FieldName="ORDDATE" Caption="<%$ Resources:WebResources, OrderDate %>" />
                        <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/CONS/CON06/CON06.aspx?dno={0}"
                            FieldName="ORDNO" Caption="<%$ Resources:WebResources, OrderNo %>" PropertiesHyperLinkEdit-Style-Font-Underline="true">
                        </dx:GridViewDataHyperLinkColumn>
                        <dx:GridViewDataTextColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, Status %>" />
                        <dx:GridViewDataTextColumn FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                                KeyFieldName="CSM_ORDERD_ID" Width="100%" EnableRowsCache="true" OnPageIndexChanged="gvDetail_PageIndexChanged">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="SEQNO" Caption="<%$ Resources:WebResources, Items %>" />
                                    <dx:GridViewDataTextColumn FieldName="SUPP_NAME" Caption="<%$ Resources:WebResources, SupplierName %>" />
                                    <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                    <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" />
                                    <dx:GridViewDataTextColumn FieldName="PRODTYPENAME" Caption="<%$ Resources:WebResources, ProductCategory %>" />
                                    <dx:GridViewDataTextColumn FieldName="ADVISEQTY" Caption="<%$ Resources:WebResources, RecommendedOrderQuantity %>" />
                                    <dx:GridViewDataTextColumn FieldName="ORDQTY" Caption="<%$ Resources:WebResources, ActualOrderQuantity %>" />
                                    <dx:GridViewDataTextColumn FieldName="APPROVEQTY" Caption="<%$ Resources:WebResources, ApprovedQuantity %>" />
                                    <dx:GridViewDataTextColumn FieldName="IN_QTY" Caption="<%$ Resources:WebResources, InspectionQuantity %>" />
                                </Columns>
                                <Settings ShowFooter="false" />
                                <SettingsDetail IsDetailGrid="true" />
                                <SettingsPager PageSize="5">
                                </SettingsPager>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                          
                                <Styles>
                                    <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                            </cc:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsPager PageSize="10" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                </cc:ASPxGridView>
                <div class="seperate">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
