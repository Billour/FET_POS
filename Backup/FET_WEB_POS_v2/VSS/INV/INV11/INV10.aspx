<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV10.aspx.cs" Inherits="VSS_INV_INV10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--盤點查詢作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventorySearch %>"></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--盤點單號-->
                    <asp:Literal ID="lblStkchkNo" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtStkchkNo" runat="server" MaxLength="20" Width="280px">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--盤點日期-->
                    <span style="color: Red">*</span>
                    <asp:Literal ID="lblStkchkDate" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtStkchkDateS" runat="server" ClientInstanceName="txtSDate">
                                        <ValidationSettings>
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
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtStkchkDateE" runat="server" ClientInstanceName="txtEDate">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
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
                        SkinID="ResetButton">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="SubEditBlock">
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="STKCHK_M_ID"
            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnPageIndexChanged="gvMaster_PageIndexChanged">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="STKCHKNO" Caption="<%$ Resources:WebResources, InventoryNo %>">
                    <DataItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("STKCHKNO") %>'></asp:HyperLink>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="STKCHKDATE" Caption="<%$ Resources:WebResources, InventoryDate %>" />
                <dx:GridViewDataColumn FieldName="STKCHK_TYPE_NAME" Caption="<%$ Resources:WebResources, InventoryType %>" />
                <dx:GridViewDataColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, InventoryStatus %>" />
                <dx:GridViewDataColumn FieldName="STKCHK_USERNAME" Caption="<%$ Resources:WebResources, CountedBy %>" />
                <dx:GridViewDataColumn FieldName="MODI_USERNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="20">
            </SettingsPager>
        </cc:ASPxGridView>
    </div>
</asp:Content>
