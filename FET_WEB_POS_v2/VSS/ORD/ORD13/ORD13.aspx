<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ORD13.aspx.cs" Inherits="VSS_ORD_ORD13_ORD13" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        /// <summary>
        /// ASPxGridView CheckBox全選；若該Row不可勾選(canSelect = false)，則選取全選時會忽略此列，否則選取此列。
        /// </summary>
        /// <param name="checktext">輸入字串</param>
        /// <returns>是否</returns>
        function CheckAllDetail_onclick() {

            for (var i = 0; i < gvDetail.pageRowCount; i++) {
                if (gvDetail.GetRow(i + gvDetail.visibleStartIndex) != null &&
            gvDetail.GetRow(i + gvDetail.visibleStartIndex).attributes["canSelect"].value == "true") {
                    var chk = document.getElementById("checkbox2");
                    if (chk.checked) {
                        gvDetail.SelectRowOnPage(i + gvDetail.visibleStartIndex, true);

                    } else {

                        gvDetail.SelectRowOnPage(i + gvDetail.visibleStartIndex, false);
                    }
                }
            }
        }
        
        function getStoreInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() == '') {
                e.errorText = '不允許空值，請重新輸入';
                return false;
            }
            if (s.GetText() != '')
                PageMethods.getStoreInfo(_gvSender.GetText(), getStoreInfo_OnOK);
        }
        
        function getStoreInfo_OnOK(returnData) {
            if (returnData != '') {
                ProductName.SetValue(returnData);
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
            else {
                ProductName.SetValue(null);
            }
        }

        function chkPoint(s, e) {
            //debugger;
            var Qty = s.GetValue();
            var iQty = 0;
            if (s.GetText() == '') {
                e.errorText = '安全庫存量不允許空值，請重新輸入';
                return false;
            }
            if (Qty != null) {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                else if (iQty <= 0) {
                    e.errorText = '安全庫存量不允許小於0，請重新輸入';
                    return false;
                }
                else if (Qty.indexOf(".") > 0) {
                    e.errorText = '安全庫存量不允許輸入小數點，請重新輸入';
                    return false;
                }
            }

        }

        function chkCurrency(s, e) {
            var Qty = s.GetValue();
            var iQty = 0;
            if (s.GetText() == '') {
                e.errorText = '最低庫存量不允許空值，請重新輸入';
                return false;
            }
            if (Qty != null) {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                else if (iQty <= 0) {
                    e.errorText = '最低庫存量不允許小於0，請重新輸入';
                    return false;
                }
                else if (Qty.indexOf(".") > 0) {
                    e.errorText = '最低庫存量不允許輸入小數點，請重新輸入';
                    return false;
                }
            }

        }

        function CheckRequiredField(s, e) {
            var Value = s.GetValue();
            if (Value == null || Value == "") {
                e.isValid = false;
                e.errorText = '【結束日期】不允許空白，請重新輸入';
                return false;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">     
        <!--卡片安全庫存量暨最低庫存量設定-->
        <asp:Literal ID="Literal1" runat="server" Text="卡片安全庫存量暨最低庫存量設定"></asp:Literal>
    </div>
    <div class="seperate"></div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--門市編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSStoreNO" runat="server" PopupControlName="StoresPopup"  />
                                
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtEStoreNO" runat="server" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--門市名稱-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtStoreName" runat="server" Width="120" MaxLength="12"></dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate"></div>
    <div class="btnPosition">
        <table align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click">
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate"></div>
    <cc:ASPxGridView ID="gvMasterDV" ClientInstanceName="gvMasterDV" runat="server" KeyFieldName="MASTER_ID"
        EnableCallBacks="False" Width="100%"
        OnPageIndexChanged="gvMasterDV_PageIndexChanged" 
        OnRowUpdating="gvMasterDV_RowUpdating"
        OnRowInserting="gvMasterDV_RowInserting"
        OnFocusedRowChanged="gvMasterDV_FocusedRowChanged"
        OnHtmlRowPrepared="gvMasterDV_HtmlRowPrepared" 
        OnStartRowEditing="gvMasterDV_StartRowEditing" 
        onrowvalidating="gvMasterDV_RowValidating" 
        oninitnewrow="gvMasterDV_InitNewRow">
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                <HeaderTemplate>
                    <input type="checkbox" onclick="gvMasterDV.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn ButtonType="Button">
                <HeaderCaptionTemplate>
                </HeaderCaptionTemplate>
                <EditButton Visible="true">
                </EditButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn runat="server" Caption="<%$ Resources:WebResources, Items %>">
                <DataItemTemplate>
                    <%#Container.ItemIndex + 1%>
                </DataItemTemplate>
                <EditItemTemplate>
                    &nbsp;</EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                <EditItemTemplate>
                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup"
                        Text='<%#Bind("STORE_NO") %>' IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                        SetClientValidationEvent="getStoreInfo" />
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn UnboundType="String" FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>"
                ReadOnly="true">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                <EditItemTemplate>
                    <dx:ASPxLabel ID="lblProductName" runat="server" Text='<%# Bind("STORENAME") %>'
                        ClientInstanceName="ProductName">
                    </dx:ASPxLabel>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                ReadOnly="true">
                <EditItemTemplate>
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#Bind("MODI_USER") %>'>
                    </dx:ASPxLabel>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                ReadOnly="true">
                <EditItemTemplate>
                    <dx:ASPxLabel ID="ASPxLabel51" runat="server" Text='<%#Bind("MODI_DTM") %>'>
                    </dx:ASPxLabel>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <TitlePanel>
                <table align="left">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { gvMasterDV.AddNewRow(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton SkinID="DeleteBtn" ID="btnDelete" AutoPostBack="false" runat="server" CausesValidation="false"
                                Text="<%$ Resources:WebResources, Delete %>" OnClick="btnDelete_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                            </dx:ASPxButton>
                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                CloseAction="CloseButton" PopupElementID="btnImport" HeaderText="卡片安全庫存量暨最低庫存量匯入" Modal="true"
                                ContentUrl="~/VSS/ORD/ORD13/ORD13_Import.aspx" Width="700" Height="500" LoadingPanelID="lp">
                                <ContentStyle>
                                    <Paddings Padding="4px"></Paddings>
                                </ContentStyle>
                            </cc:ASPxPopupControl>
                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                            </dx:ASPxLoadingPanel>
                        </td>
                        <td>
                            <dx:ASPxButton SkinID="btnExport" ID="btnExport" AutoPostBack="false" runat="server" CausesValidation="false"
                                Text="<%$ Resources:WebResources, Export %>" OnClick="btnExport_Click">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </TitlePanel>
        </Templates>
        <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
        <SettingsPager PageSize="5"></SettingsPager>
        <Settings ShowTitlePanel="True" />
        <SettingsEditing Mode="Inline" />        
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>
    <div class="seperate"></div>
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%" Visible="false">
        <TabPages>
            <dx:TabPage Text="卡片群組設定">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <div>
                            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail"
                                Width="100%" AutoGenerateColumns="False" KeyFieldName="DETAIL_ID"
                                OnRowInserting="gvDetail_RowInserting" 
                                OnRowUpdating="gvDetail_RowUpdating"
                                OnRowValidating="gvDetail_RowValidating"
                                 OnCellEditorInitialize="gvDetail_CellEditorInitialize"
                                OnCommandButtonInitialize="gvDetail_CommandButtonInitialize" 
                                OnHtmlRowPrepared="gvDetail_HtmlRowPrepared"
                                OnStartRowEditing="gvDetail_StartRowEditing" 
                                OnInitNewRow="gvDetail_InitNewRow"
                                OnPageIndexChanged="gvDetail_PageIndexChanged">
                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <HeaderTemplate>
                                            <input type="checkbox" id="checkbox2" onclick="CheckAllDetail_onclick();" title="Select/Unselect all rows on the page" />
                                        </HeaderTemplate>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                        <EditButton Visible="true">
                                        </EditButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn runat="server" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="2">
                                        <DataItemTemplate>
                                            <%#Container.ItemIndex + 1%>
                                        </DataItemTemplate>
                                        <EditItemTemplate>
                                            &nbsp;</EditItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataComboBoxColumn FieldName="SIM_GROUP_ID" runat="server" Caption="卡片群組名稱" VisibleIndex="3">
                                        <PropertiesComboBox ValueType="System.String" TextField="SIM_GROUP_NAME" ValueField="SIM_GROUP_ID">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                        </PropertiesComboBox>
                                        <DataItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("SIM_GROUP_NAME") %>'></asp:Label>
                                        </DataItemTemplate>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataDateColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartDate %>" VisibleIndex="4">
                                        <PropertiesDateEdit>
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>   
                                        </PropertiesDateEdit>
                                         <%--<EditItemTemplate>
                                            <dx:ASPxDateEdit ID="txtS_DATE" runat="server" Text='<%# Eval("[S_DATE]") %>'
                                                ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MinDate='<%# DateTime.Today.AddDays(1) %>'
                                                EditFormatString="yyyy/MM/dd" EditFormat="Custom">
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </EditItemTemplate>--%>
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>" VisibleIndex="5">
                                        <%--<EditItemTemplate>
                                            <dx:ASPxDateEdit ID="txtE_DATE" runat="server" Text='<%# Eval("[S_DATE]") %>'
                                                MinDate='<%# DateTime.Today.AddDays(1) %>'
                                                DisplayFormatString="yyyy/MM/dd" EditFormatString="yyyy/MM/dd" EditFormat="Custom">
                                            </dx:ASPxDateEdit>
                                        </EditItemTemplate> --%>
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataTextColumn FieldName="SAFE_QTY" Caption="安全庫存量" VisibleIndex="6">
                                        <PropertiesTextEdit MaxLength="9"  Style-HorizontalAlign="Right">
                                            <ValidationSettings>
                                                <RegularExpression ValidationExpression="\d*" />
                                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents  Validation="function(s, e){ chkPoint(s, e); }" />
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="L_BOUND" Caption="最低庫存量" VisibleIndex="7">                                            
                                        <PropertiesTextEdit MaxLength="9" Style-HorizontalAlign="Right">
                                            <ValidationSettings>
                                                <RegularExpression ValidationExpression="\d*"  />                                        
                                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents  Validation="function(s, e){ chkCurrency(s, e); }" />
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="ORDER_QTY" Caption="應補貨量" ReadOnly="true" VisibleIndex="8">
                                        <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None"></Border>
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="IN_QTY" Caption="己補貨量" ReadOnly="true" VisibleIndex="9">
                                        <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None"></Border>
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                        AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s, e) { gvDetail.AddNewRow(); }" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnDelete" SkinID="DeleteBtn" AutoPostBack="false" runat="server" CausesValidation="false"
                                                        Text="<%$ Resources:WebResources, Delete %>" OnClick="btnDelete2_Click">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                </Templates>
                                <SettingsPager PageSize="5"></SettingsPager>
                                <SettingsEditing Mode="Inline" />
                                <Settings ShowTitlePanel="True"></Settings>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
