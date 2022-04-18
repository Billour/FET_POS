<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON19.aspx.cs" Inherits="VSS_CONS_CON19" %>

<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                    <tr>
                        <td align="left">
                            <!--寄銷商品移出查詢作業-->
                            <asp:Literal ID="Literal01" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferOutSearch %>">
                            </asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt">
                                <!--移撥單號-->
                                <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="txtStno" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                                <!--商品料號-->
                                <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Productcode %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                
                                <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup"
                                    KeyFieldValue1="consignmentsale" />
                            </td>
                            <td class="tdtxt">
                                <!--商品名稱-->
                                <asp:Literal ID="lblProductName" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="txtProductName" ClientInstanceName="txtProductName" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--移出日期-->
                                <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, start %>">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtSTDate_S" runat="server" ClientInstanceName="txtSDate">
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                                </ValidationSettings>
                                                <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, end %>">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtSTDate_E" runat="server" ClientInstanceName="txtEDate">
                                                <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdtxt">
                                <!--撥入門市-->
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TransferTo %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <uc1:PopupControl ID="txtToStoreName" KeyFieldValue1="name" KeyFieldValue2="STORENAME"
                                    runat="server" PopupControlName="StoresPopup" />
                            </td>
                            <td class="tdtxt">
                                <!--移撥狀態-->
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <dx:ASPxComboBox ID="ddlTStatus" runat="server">
                                    <Items>
                                        <dx:ListEditItem Value="" Text="ALL" Selected="true" />
                                        <dx:ListEditItem Value="20" Text="在途" />
                                        <dx:ListEditItem Value="30" Text="已撥入" />
                                    </Items>
                                </dx:ASPxComboBox>
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
                                <dx:ASPxButton ID="btnClear"  SkinID="ResetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="seperate">
                </div>
                <cc:ASPxGridView ID="gvMaster" 
                   runat="server" AutoGenerateColumns="False" Width="100%"
                    KeyFieldName="CSM_STORETRANSFERM_ID" 
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                    ClientInstanceName="gvMaster" 
                    OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" 
                    OnPageIndexChanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="STNO" Caption="<%$ Resources:WebResources, TransferSlipNo %>">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="FROM_STORE_NAME" Caption="<%$ Resources:WebResources, TransferFrom %>" />
                        <dx:GridViewDataColumn FieldName="STDATE" Caption="<%$ Resources:WebResources, TransferOutDate %>" />
                        <dx:GridViewDataColumn FieldName="TO_STORE_NAME" Caption="<%$ Resources:WebResources, TransferTo %>" />
                        <dx:GridViewDataColumn FieldName="TSTDATE" Caption="<%$ Resources:WebResources, TransferInDate %>" />
                        <dx:GridViewDataColumn FieldName="TSTATUS" Caption="<%$ Resources:WebResources, TransferStatus %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# Bind("TSTATUS") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                        <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Width="100%"
                                KeyFieldName="CSM_STORETRANSFER_D_ID" OnHtmlRowCreated="gvDetail_HtmlRowCreated"
                                OnPageIndexChanged="gvDetail_PageIndexChanged" Settings-ShowTitlePanel="true">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="PRODTYPENAME" Caption="<%$ Resources:WebResources, ProductCategory %>" />
                                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                    <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" />
                                    <dx:GridViewDataColumn FieldName="TRANOUTQTY" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>" />
                                    <dx:GridViewDataColumn FieldName="TRANINQTY" Caption="<%$ Resources:WebResources, TransferredInQuantity %>" />
                                </Columns>
                                <Settings ShowFooter="false" />
                                <SettingsDetail IsDetailGrid="true" />
                                <SettingsPager PageSize="5">
                                </SettingsPager>
                                
                                <Styles>
                                    <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsPager PageSize="5" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                </cc:ASPxGridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
