<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="INV04.aspx.cs" Inherits="VSS_INV_INV04" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Src="../../../Controls/ExportExcelData.ascx" TagName="ExportExcelData"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--退倉設定查詢作業-->
            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingSearch %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="RTN_NO" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td nowrap="nowrap">
                        <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td nowrap="nowrap">
                        <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開單日-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReceiptDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 240px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width: 120px;">
                                        <dx:ASPxDateEdit ID="B_DATE" runat="server" ClientInstanceName="txtSDate">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width: 120px;">
                                        <dx:ASPxDateEdit ID="E_DATE" runat="server" ClientInstanceName="txtEDate">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉狀態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="RTN_STATUS" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="" Selected="true" />
                                <dx:ListEditItem Text="已存檔" Value="10" />
                                <dx:ListEditItem Text="已傳輸" Value="50" />
                                <dx:ListEditItem Text="已完成" Value="60" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PRODNO"
            Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="RTNNO" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>">
                    <DataItemTemplate>
                        <asp:HyperLink ID="hlkdno1" runat="server" Text='<%# Bind("RTNNO") %>'></asp:HyperLink>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, ReturnWarehousingStatus %>" />
                <dx:GridViewDataColumn FieldName="B_DATE" Caption="<%$ Resources:WebResources, ReturnWarehousingStartDate %>" />
                <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, ReturnWarehousingEndDate %>" />
                <dx:GridViewDataColumn FieldName="RETURN_DESCRIPTION" Caption="<%$ Resources:WebResources, ReasonForWarehousing %>" />
                <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                <dx:GridViewDataColumn FieldName="MODI_USER_NAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
            </Columns>
            <Templates>
                <TitlePanel>
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                    AutoPostBack="false" CausesValidation="false" OnClick="btnExport_Click">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="10">
            </SettingsPager>
            <Settings ShowTitlePanel="true" />
        </cc:ASPxGridView>
    </div>
</asp:Content>
