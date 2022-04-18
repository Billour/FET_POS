<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV05.aspx.cs" Inherits="VSS_INV_INV05" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--退倉設定作業-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingSettings %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='INV04.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="lbOrderNo" runat="server" Text="HR100914001">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開單日期-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReceiptDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <div style="width: 120px;">
                            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </div>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Label2" runat="server" Text="00 未存檔">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉開始日-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <div style="width: 120px;">
                            <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </div>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉結束日-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <div style="width: 120px;">
                            <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server">
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </div>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Label3" runat="server" Text="10/07/12 15:00">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉原因代號-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReason %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <div style="width: 120px;">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="100px">
                                <Items>
                                    <dx:ListEditItem Text="<%$ Resources:WebResources, DropDownListPrompt %>" Value="" />
                                    <dx:ListEditItem Text="退倉原因1" Value="退倉原因1" />
                                    <dx:ListEditItem Text="退倉原因2" Value="退倉原因2" />
                                </Items>
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </div>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--後續處理-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PostProcess %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <div style="width: 120px;">
                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="100px">
                                <Items>
                                    <dx:ListEditItem Text="<%$ Resources:WebResources, DropDownListPrompt %>" Value="" />
                                    <dx:ListEditItem Text="後續處理1" Value="後續處理1" />
                                    <dx:ListEditItem Text="後續處理2" Value="後續處理2" />
                                </Items>
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </div>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新人員-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Label4" runat="server" Text="64591 李家駿">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--備註-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
            <TabPages>
                <dx:TabPage Text="商品">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號"
                                    Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                                    OnPageIndexChanged="gvMaster_PageIndexChanged" AutoGenerateColumns="False" OnRowUpdating="gvMaster_RowUpdating"
                                    OnRowInserting="gvMaster_RowInserting" Settings-ShowTitlePanel="true">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                            <EditButton Visible="true" Text=""></EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"
                                            VisibleIndex="2">
                                            <EditItemTemplate>
                                                <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  Text='<%# BIND("[商品料號]") %>' />
                                                <%--<table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="ProductCOde" runat="server" Width="68px" Text='<%# BIND("[商品料號]") %>'>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="chooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                SkinID="PopupButton">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                                    PopupElementID="chooseButton1" TargetElementID="ProductCOde">
                                                </cc:ASPxPopupControl>--%>
                                            </EditItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductName %>"
                                            VisibleIndex="4">
                                            <PropertiesTextEdit>
                                                <ReadOnlyStyle>
                                                    <Border BorderStyle="None" />
                                                </ReadOnlyStyle>
                                            </PropertiesTextEdit>
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                            OnClick="btnAddNew_Click"  CausesValidation="false"/>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"  CausesValidation="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <SettingsEditing Mode="Inline" />
                                    <SettingsPager PageSize="5">
                                    </SettingsPager>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="門市">
                    <ContentCollection>
                        <dx:ContentControl>
                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td class="tdval">
                                                <table>
                                                    <tr>
                                                        <td class="tdcen">
                                                            <!--未選擇-->
                                                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Nonselect %>"></asp:Literal>
                                                        </td>
                                                        <td class="tdcen">
                                                        </td>
                                                        <td class="tdcen">
                                                            <!--已選擇-->
                                                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Selected %>"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcen">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlSubZone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubZone_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td class="tdcen">
                                                        </td>
                                                        <td class="tdcen">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdListBox" rowspan="5">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ListBox ID="ListBox1" runat="server" Height="327px" SelectionMode="Multiple"
                                                                        Width="259px"></asp:ListBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td class="tdBtn">
                                                        </td>
                                                        <td rowspan="5" class="tdListBox">
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ListBox ID="ListBox2" runat="server" Height="327px" SelectionMode="Multiple"
                                                                        Width="259px"></asp:ListBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/next.png" OnClick="btnAdd_Click" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/previous.png" OnClick="btnBack_Click" /></ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Cancel %>"  CausesValidation="false" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" 
                            AutoPostBack="false" CausesValidation="false">                            
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server"
            AllowDragging="True" AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/INV/INV05_Import.aspx"
            PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
            Width="400px" Height="350px"
            HeaderText="資料匯入" ClientInstanceName="dataImportPopup" PopupElementID="btnImport" LoadingPanelID="lp">            
        </cc:ASPxPopupControl>
        <dx:ASPxLoadingPanel ID="lp" runat="server"></dx:ASPxLoadingPanel>
</asp:Content>
