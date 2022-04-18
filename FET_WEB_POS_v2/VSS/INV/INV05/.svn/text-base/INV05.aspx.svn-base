<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV05.aspx.cs" Inherits="VSS_INV_INV05" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function onOK() {
            __doPostBack('<%= lblError.UniqueID %>', 'AAA');
        }

        //檢查商品料號是否存在
        _gvSender = null;
        _gvEventArgs = null;
        function getPRODNAME(s, e) {
            this.s = s;
            this.EventArgs = e;
            this.Sender = s;
            if (s.GetText() != '')
               PageMethods.getPRODNAME(Sender.GetText() , getPRODNAME_OnOK);
        }
        function getPRODNAME_OnOK(returnData) {
            if (returnData == '') {
                alert("商品料號不存在!");
                EventArgs.processOnServer = false;
                Sender.Focus();
                PRODNAME.SetValue(null);
            }
            else {
                PRODNAME.SetValue(returnData);
            }
        }
    </script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   <dx:ASPxTextBox ID="lblError" runat="server" ClientVisible="false"></dx:ASPxTextBox>
   
    <div class="titlef">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--退倉設定作業-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingSettings %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='INV04.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>   
    
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <asp:HiddenField ID="CreateUser" runat="server" />
        <asp:HiddenField ID="CreateDTM" runat="server" />
        
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="lbRTNNo" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開單日期-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReceiptDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <div style="width: 120px;">
                            <dx:ASPxDateEdit ID="ReceiptDate" runat="server" ClientEnabled="false">
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </div>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Status1" runat="server" Text="未存檔">
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
                            <dx:ASPxDateEdit ID="RTNM_BDate" runat="server" MinDate='<%# DateTime.Today %>'>
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
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
                            <dx:ASPxDateEdit ID="RTNM_EDate" runat="server" MinDate='<%# DateTime.Today %>'>
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </div>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="ModifiedDate" runat="server"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉原因代號-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReason %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <div style="width: 200px;">
                            <dx:ASPxComboBox ID="RETURN_REASON_CODE" runat="server" Width="200px">
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
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
                        <div style="width: 200px;">
                            <dx:ASPxComboBox ID="AFTER_PROCESS_CODE" runat="server" Width="200px">
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </div>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新人員-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="ModifiedBy" runat="server"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--備註-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="Remark1" runat="server" MaxLength="100" Width="200px">
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
                        <dx:ASPxTextBox ID="txtuuid" runat="server" Width="150" ClientVisible="false" ></dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" 
            ActiveTabIndex="0">
            <TabPages>
                <dx:TabPage Text="商品">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PRODNO"
                                    Width="100%" EnableCallBacks="false" AutoGenerateColumns="False" 
                                    OnPageIndexChanged="gvMaster_PageIndexChanged"
                                    OnRowUpdating="gvMaster_RowUpdating"
                                    OnRowInserting="gvMaster_RowInserting"
                                    OnCancelRowEditing="gvMaster_CancelRowEditing" 
                                    OnInitNewRow="gvMaster_InitNewRow"
                                    OnRowValidating="gvMaster_RowValidating" 
                                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize">
                                    <Columns> 
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center" Caption=" " ButtonType="Button">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <UpdateButton  Visible="true" Text="確認"></UpdateButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button" Caption=" ">
                                            <EditButton Visible="true"></EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>" >
                                            <EditItemTemplate>
                                                <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup" Text='<%#BIND("[PRODNO]") %>' 
                                                     IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                                     OnClientTextChanged="function(s,e){ getPRODNAME(s,e);}" />
                                            </EditItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>" >
                                            <EditItemTemplate> 
                                                <dx:ASPxTextBox ID="txtProdName" Width="500" runat="server" Text='<%# Bind("PRODNAME") %>' ClientInstanceName="PRODNAME" Border-BorderStyle="None" ReadOnly="true"></dx:ASPxTextBox>
                                            </EditItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                            OnClick="btnAddNew_Click" Visible="true" ClientInstanceName="btnNew" CausesValidation="false">
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                                                            OnClick="btnDelete_Click" ClientInstanceName="btnDelete" CausesValidation="false" SkinID="DeleteBtn" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <SettingsEditing Mode="Inline" />
                                    <SettingsPager PageSize="5" />
                                    <Settings ShowTitlePanel="True"></Settings>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="門市">
                    <ContentCollection>
                        <dx:ContentControl>
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
                                                    <dx:ASPxComboBox ID="ddlZone" runat="server" Width="80px" OnSelectedIndexChanged="ddlSubZone_SelectedIndexChanged" AutoPostBack="true"></dx:ASPxComboBox>
                                                </td>
                                                <td class="tdcen">
                                                </td>
                                                <td class="tdcen">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  rowspan="5" style="z-index:1;">
                                                    <asp:ListBox ID="ZoneTypeList" runat="server" Height="327px" SelectionMode="Multiple" Width="259px"></asp:ListBox>
                                                </td>
                                                <td class="tdBtn"></td>
                                                <td rowspan="5"  style="z-index:1;">
                                                    <asp:ListBox ID="StoreList" runat="server" Height="327px" SelectionMode="Multiple" Width="259px"></asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdBtn">
                                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/next.png" OnClick="btnAdd_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdBtn"></td>
                                            </tr>
                                            <tr>
                                                <td class="tdBtn">
                                                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/previous.png" OnClick="btnBack_Click" /> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdBtn"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
        
        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" SkinID="ResetButton" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" 
                            CausesValidation="false">
                        </dx:ASPxButton>
                        <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server"
                            AllowDragging="True" AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/INV/INV05/INV05_Import.aspx"
                            PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
                            Width="500px" Height="500px" TargetElementID="lblError" onOKScript="onOK"
                            HeaderText="資料匯入" ClientInstanceName="dataImportPopup" PopupElementID="btnImport" LoadingPanelID="lp">  
                            <ClientSideEvents CloseUp="function(s, e) {s.SetContentUrl(s.GetContentUrl()); }"  />          
                        </cc:ASPxPopupControl>
                        <dx:ASPxLoadingPanel ID="lp" runat="server"></dx:ASPxLoadingPanel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        
        </ContentTemplate>    
    </asp:UpdatePanel>
        
</asp:Content>
