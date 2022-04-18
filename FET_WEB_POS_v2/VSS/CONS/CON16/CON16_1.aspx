<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON16_1.aspx.cs" Inherits="VSS_CONS_CON16_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    
        <div class="titlef">
            <!--寄銷商品盤點查詢作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductInventorySearch %>"></asp:Literal>
        </div>
        
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt"></td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--盤點單號-->
                        <asp:Literal ID="lblStkchkNo" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtStkchkNo" runat="server" MaxLength="20" Width="280px"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--盤點狀態-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, InventoryStatus %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="dropStyle" runat="server" Width="100px">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                <dx:ListEditItem Text="盤點中" Value="0"  />
                                <dx:ListEditItem Text="已盤點" Value="1" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt"></td>
                    <td class="tdtxt"></td>
                </tr>
                <tr>
                    <td class="tdtxt"></td>
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
                                    <div style="width:120px;">
                                        <dx:ASPxDateEdit ID="txtStkchkDateS" runat="server" ClientInstanceName="txtSDate">
                                            <ValidationSettings>                                                
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />                                                  
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
                                        <dx:ASPxDateEdit ID="txtStkchkDateE" runat="server" ClientInstanceName="txtEDate">
                                            <ValidationSettings>                                                
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>      
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />                                                     
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                    <td class="tdtxt"></td>                    
                    <td class="tdtxt"></td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>
        
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
                        <dx:ASPxButton ID="btnClear" runat="server" 
                            Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton" >
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="SubEditBlock">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
            <ContentTemplate>
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="STKCHKNO"
                Width="100%" Settings-ShowTitlePanel="true" EnableCallBacks="false"
                OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" 
                OnPageIndexChanged="gvMaster_PageIndexChanged" 
                    onfocusedrowchanged="gvMaster_FocusedRowChanged">
                <Columns>
                    <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/CONS/CON16/CON16.aspx?InventoryNo={0}"
                        FieldName="STKCHKNO" Caption="<%$ Resources:WebResources, InventoryNo %>" PropertiesHyperLinkEdit-Style-Font-Underline="true">
                    </dx:GridViewDataHyperLinkColumn>
                    <dx:GridViewDataColumn FieldName="STKCHKDATE" Caption="<%$ Resources:WebResources, InventoryDate %>" />
                    <dx:GridViewDataColumn FieldName="STKCHK_TYPE_NAME" Caption="<%$ Resources:WebResources, InventoryType %>" />
                    <dx:GridViewDataColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, InventoryStatus %>" />
                    <dx:GridViewDataColumn FieldName="STKCHK_USERNAME" Caption="<%$ Resources:WebResources, CountedBy %>" />
                    <dx:GridViewDataColumn FieldName="MODI_USERNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                    <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsPager PageSize="20"></SettingsPager>
                <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="True" />
            </cc:ASPxGridView>
            <br />
            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail"  Visible="false"
                    KeyFieldName="PROD_NO"   Width="100%" 
                    onpageindexchanged="gvDetail_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>            
                        <dx:GridViewDataTextColumn FieldName="SUPP_NO" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SUPP_NAME" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STKQTY" runat="server" Caption="<%$ Resources:WebResources, BookInventory %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STKCHKQTY" runat="server" Caption="<%$ Resources:WebResources, PhysicalInventory %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="DIFF_STKQTY" runat="server" Caption="<%$ Resources:WebResources, DifferenceQuantity %>"></dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
            <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
</asp:Content>
