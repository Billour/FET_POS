<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV10.aspx.cs" Inherits="VSS_INV_INV10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="func">
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                    <tr>
                        <td align="left">
                            <!--盤點查詢作業-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventorySearch %>"></asp:Literal>
                        </td>
                        
                    </tr>
                </table>
            </div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox3" runat="server">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點日期-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <div style="width:120px;">
                                            <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                                <ValidationSettings CausesValidation="false">                                                
                                                    <RequiredField IsRequired="true" />                                                       
                                                </ValidationSettings>                                                
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
                                        <div style="width:120px;">
                                            <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                                <ValidationSettings CausesValidation="false">                                                
                                                    <RequiredField IsRequired="true" />                                                       
                                                </ValidationSettings>                                                
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
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="盤點單號"
                Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                OnPageIndexChanged="gvMaster_PageIndexChanged">
                <Columns>
                    <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/INV/INV11.aspx?InventoryNo={0}"
                        FieldName="盤點單號" Caption="<%$ Resources:WebResources, InventoryNo %>" PropertiesHyperLinkEdit-Style-Font-Underline="true">
                    </dx:GridViewDataHyperLinkColumn>
                    <dx:GridViewDataColumn FieldName="盤點日期" Caption="<%$ Resources:WebResources, InventoryDate %>" />
                    <dx:GridViewDataColumn FieldName="盤點類型" Caption="<%$ Resources:WebResources, InventoryType %>" />
                    <dx:GridViewDataColumn FieldName="盤點狀態" Caption="<%$ Resources:WebResources, InventoryStatus %>" />
                    <dx:GridViewDataColumn FieldName="盤點人員" Caption="<%$ Resources:WebResources, CountedBy %>" />
                    <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                    <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsPager PageSize="25">
                </SettingsPager>
            </cc:ASPxGridView>
        </div>
    </div>
    </div>
    <div class="seperate">
    </div>
</asp:Content>
