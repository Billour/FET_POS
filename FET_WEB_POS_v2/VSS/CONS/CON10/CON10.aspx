<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON10.aspx.cs" Inherits="VSS_CONS_CON10" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function onOK()
        {
            __doPostBack('<%= lblError.UniqueID %>', 'AAA');
        }

        //�ˬd�ӫ~�Ƹ��O�_�s�b
        _gvSender = null;
        _gvEventArgs = null;
        function getPRODNAME(s, e) {
            _gvEventArgs = e;
            _gvSender = s;

            if (s.GetText() != '')
            {
                var fName = "2_txtProdNo_txtControl";
                SuppId = getClientInstance('TxtBox', _gvSender.name.replace(fName, "4_txtSuppId"));
//                if (SuppId.GetText() != '')
//                {
                    PageMethods.getPRODNAME(_gvSender.GetText() + "��" + SuppId.GetText(), getPRODNAME_OnOK);
//                } else
//                {
//                    alert("�Х���J�t�ӽs��!");
//                    _gvEventArgs.processOnServer = false;
//                    _gvSender.Focus();
//                    _gvSender.SetValue(null);
//                }

            }
        }
        function getPRODNAME_OnOK(returnData)
        {
            var fName = "2_txtProdNo_txtControl";
            var vProdName = getClientInstance('TxtBox', _gvSender.name.replace(fName, "3_txtProdName"));
            var vSuppId = getClientInstance('TxtBox', _gvSender.name.replace(fName, "4_txtSuppId"));
            var vSuppNo = getClientInstance('TxtBox', _gvSender.name.replace(fName, "4_txtSuppNo_txtControl"));
            var vSuppName = getClientInstance('TxtBox', _gvSender.name.replace(fName, "5_txtSuppName"));
            if (returnData == '')
            {
                alert("�ӫ~�Ƹ����s�b!");
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
                _gvSender.SetValue(null);
                ProdName.SetValue(null);
            }
            else
            {
                var ProdName = returnData[0];
                var SuppId = returnData[1];
                var SuppNo = returnData[2];
                var SuppName = returnData[3];
                vProdName.SetValue(ProdName);
                vSuppId.SetValue(SuppId);
                vSuppNo.SetValue(SuppNo);
                vSuppName.SetValue(SuppName);
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();

            }
        }
        function getSUPPINFO(s, e)
        {
            _gvEventArgs = e;
            _gvSender = s;

            if (s.GetText() != '')
                PageMethods.getSuppInfo(_gvSender.GetText(), getSUPPINFO_OnOK);
        }

        function getSUPPINFO_OnOK(returnData) {

            var fName = "4_txtSuppNo_txtControl";
            SuppId = getClientInstance('TxtBox', _gvSender.name.replace(fName, "4_txtSuppId"));
            SuppName = getClientInstance('TxtBox', _gvSender.name.replace(fName, "5_txtSuppName"));
            ProdfNo = _gvSender.name.replace(fName, "2_txtProdNo_txtControl_I");
            ProdName = getClientInstance('TxtBox', _gvSender.name.replace(fName, "3_txtProdName"));
            if (returnData == '') {
                alert("�t�ӽs�����s�b!");
                _gvSender.Focus();
                _gvSender.SetValue(null);
                SuppId.SetText(null);
                SuppName.SetText(null);

                $('#' + ProdfNo + '').val('');
                ProdName.SetText(null);
            }
            else {
                //�t�ӦW�١��`���B����
                var DataArray = returnData.split("��");
                SuppId.SetText(DataArray[0]);
                SuppName.SetText(DataArray[1]);


                $('#' + ProdfNo + '').val('');
                ProdName.SetText(null);
                //

                //var url = "~/VSS/Common/ProductsPopup3.aspx?SysDate=Date()&KeyFieldValue1=consignmentsale_suppid&KeyFieldValue2=" + DataArray[0];
                //prodnoPop = getClientInstance('Popup', _gvSender.name.replace(fName, "2_txtProdNo_ASPxPopupControl1"));
                //prodnoPop.SetContentUrl(url);

                var url = ASPxPopupControl_ProdNo.GetContentUrl();

                var s = url.split('SysDate=');
                if (s.length > 1) {
                    var ordSysDate = s[1].split('&')[0];
                    var newSysDate = Date();
                    url = url.replace(ordSysDate, newSysDate);
                }
                var u = url.split('KeyFieldValue1=');
                if (u.length > 1) {
                    var oldKeyFieldValue1 = u[1].split('&')[0];
                    var newKeyFieldValue1 = "consignmentsale_suppid";
                    url = url.replace(oldKeyFieldValue1, newKeyFieldValue1);
                }
                u = url.split('KeyFieldValue2=');
                if (u.length > 1) {
                    var oldKeyFieldValue2 = u[1].split('&')[0];
                    var newKeyFieldValue2 = DataArray[0];
                    url = url.replace(oldKeyFieldValue2, newKeyFieldValue2);
                }
                
                ASPxPopupControl_ProdNo.SetContentUrl(url);
            }

        }
        
        function CheckAll_clear() {
            for (var i = 0; i < gvMaster.pageRowCount; i++) {
                gvMaster.SelectRowOnPage(i + gvMaster.visibleStartIndex, false);
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
                    <!--�H�P�h�ܳ]�w�@�~-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSettings %>"></asp:Literal>
                    <dx:ASPxLabel ID="lbStatus"  runat="server" ClientVisible="false" />
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='CON09.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--�H�P�h�ܳ渹-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>�G
                </td>
                <td class="tdval" nowrap="nowrap">
                    <dx:ASPxLabel ID="lbRTNNo" runat="server" Text="">
                    </dx:ASPxLabel>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--�}����-->
                    <span style="color: Red">*</span>
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReceiptDate %>"></asp:Literal>�G
                </td>
                <td class="tdval" nowrap="nowrap">
                    <div style="width: 120px;">
                        <dx:ASPxDateEdit ID="ReceiptDate" runat="server" ClientEnabled="false">
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" ErrorText="�������" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </div>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--���A-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>�G
                </td>
                <td class="tdval" nowrap="nowrap">
                    <dx:ASPxLabel ID="Status1" runat="server" Text="���s��">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--�H�P�h�ܶ}�l��-->
                    <span style="color: Red">*</span>
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></asp:Literal>�G
                </td>
                <td class="tdval" nowrap="nowrap">
                    <div style="width: 120px;">
                        <dx:ASPxDateEdit ID="RTNM_BDate" runat="server" MinDate='<%# DateTime.Today %>'>
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" ErrorText="�������" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </div>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--�H�P�h�ܵ�����-->
                    <span style="color: Red">*</span>
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></asp:Literal>�G
                </td>
                <td class="tdval" nowrap="nowrap">
                    <div style="width: 120px;">
                        <dx:ASPxDateEdit ID="RTNM_EDate" runat="server" MinDate='<%# DateTime.Today %>'>
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" ErrorText="�������" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </div>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--��s���-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>�G
                </td>
                <td class="tdval" nowrap="nowrap">
                    <dx:ASPxLabel ID="ModifiedDate" runat="server">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                </td>
                <td class="tdval" nowrap="nowrap">
                </td>
                <td class="tdtxt" nowrap="nowrap">
                </td>
                <td class="tdval" nowrap="nowrap">
                    <dx:ASPxTextBox ID="txtuuid" runat="server" Width="150" ClientVisible="false">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--��s�H��-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>�G
                </td>
                <td class="tdval" nowrap="nowrap">
                    <dx:ASPxLabel ID="ModifiedBy" runat="server">
                    </dx:ASPxLabel>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div> 
       
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="CreateUser" runat="server" />
            <asp:HiddenField ID="CreateDTM" runat="server" />
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0">
                <TabPages>
                    <dx:TabPage Text="�ӫ~">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PRODNO;SUPPNO" Width="100%" 
                                        OnPageIndexChanged="gvMaster_PageIndexChanged"
                                        OnRowUpdating="gvMaster_RowUpdating" 
                                        OnRowInserting="gvMaster_RowInserting" 
                                        OnCancelRowEditing="gvMaster_CancelRowEditing"
                                        OnInitNewRow="gvMaster_InitNewRow" 
                                        OnRowValidating="gvMaster_RowValidating" 
                                        OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                                        OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                                        OnStartRowEditing="gvMaster_StartRowEditing"
                                        OnHtmlRowCreated ="gvMaster_HtmlRowCreated">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center"
                                                Caption=" " ButtonType="Button">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                     <HeaderTemplate>
                                                    <div style="text-align: center">
                                                        <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                                    </div>
                                                </HeaderTemplate>
                                                <UpdateButton Visible="false" Text="�T�{"> 
                                                </UpdateButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button" Caption=" " VisibleIndex="1">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"
                                                VisibleIndex="2">
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup3"
                                                        Text='<%#BIND("PRODNO") %>' Width="150" SetClientValidationEvent="getPRODNAME"
                                                        IsValidation="true" KeyFieldValue1="consignmentsale"  KeyFieldValue2="PRODNO"
                                                        ValidationGroup='<%# Container.ValidationGroup %>' />
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"
                                                VisibleIndex="3">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtProdName" Width="300" runat="server" Text='<%# Bind("PRODNAME") %>'
                                                        Border-BorderStyle="None" ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="SUPPNO" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>"
                                                VisibleIndex="4">
                                                <DataItemTemplate>
                                                    <dx:ASPxTextBox ID="txtSuppId" runat="server" Text='<%# Bind("SUPPID") %>' ClientVisible="false"
                                                        Border-BorderStyle="None" ReadOnly="true" >
                                                    </dx:ASPxTextBox>                                                  
                                                    <dx:ASPxLabel ID="txtSuppNo" runat="server" Text='<%# Bind("SUPPNO") %>'></dx:ASPxLabel>
                                                </DataItemTemplate>
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtSuppId" runat="server" Text='<%# Bind("SUPPID") %>' ClientVisible="false"
                                                        Border-BorderStyle="None" ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                    <uc1:PopupControl ID="txtSuppNo" runat="server" PopupControlName="ConsignmentVendorsPopup"
                                                        Text='<%#BIND("SUPPNO") %>' Width="150" SetClientValidationEvent="getSUPPINFO"
                                                        IsValidation="true" KeyFieldValue2="SUPP_NO" ValidationGroup='<%# Container.ValidationGroup %>' />
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="SUPPNAME" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>"
                                                VisibleIndex="5">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtSuppName" Width="300" runat="server" Text='<%# Bind("SUPPNAME") %>'
                                                         Border-BorderStyle="None" ReadOnly="true">
                                                    </dx:ASPxTextBox>
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
                                                                OnClick="btnDelete_Click" ClientInstanceName="btnDelete" CausesValidation="false" >
                                                                  <ClientSideEvents Click="function(s,e){if(!confirm('�O�_�T�{�R��'))e.processOnServer = false; return e.processOnServer;} " />
                                                            </dx:ASPxButton>
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
                    <dx:TabPage Text="����">
                        <ContentCollection>
                            <dx:ContentControl>
                                <table>
                                    <tr>
                                        <td class="tdval">
                                            <table>
                                                <tr>
                                                    <td class="tdcen">
                                                        <!--�����-->
                                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Nonselect %>"></asp:Literal>
                                                    </td>
                                                    <td class="tdcen">
                                                    </td>
                                                    <td class="tdcen">
                                                        <!--�w���-->
                                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Selected %>"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdcen">
                                                        <dx:ASPxComboBox ID="ddlZone" runat="server" Width="80px" OnSelectedIndexChanged="ddlSubZone_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td class="tdcen">
                                                    </td>
                                                    <td class="tdcen">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdListBox" rowspan="5">
                                                        <asp:ListBox ID="ZoneTypeList" runat="server" Height="327px" SelectionMode="Multiple"
                                                            Width="259px"></asp:ListBox>
                                                    </td>
                                                    <td class="tdBtn">
                                                    </td>
                                                    <td rowspan="5" class="tdListBox">
                                                        <asp:ListBox ID="StoreList" runat="server" Height="327px" SelectionMode="Multiple"
                                                            Width="259px"></asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdBtn">
                                                        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/next.png" OnClick="btnAdd_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdBtn">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdBtn">
                                                        <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/previous.png" OnClick="btnBack_Click" />
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
                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                OnClick="btnSave_Click" >
                                   <clientsideevents click="function (s, e) {e.processOnServer = confirm('�h�ܳ]�w�x�s�ᤣ�i�A�ק�A�нT�{');}" />
                             </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                SkinID="ResetButton" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>"
                                CausesValidation="false">
                            </dx:ASPxButton>
                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                CloseAction="CloseButton" ContentUrl="~/VSS/CONS/CON10/CON10_Import.aspx" PopupHorizontalAlign="Center"
                                PopupVerticalAlign="WindowCenter" ShowFooter="false" Width="500px" Height="500px"
                                TargetElementID="lblError" onOKScript="onOK" HeaderText="��ƶפJ" ClientInstanceName="dataImportPopup"
                                PopupElementID="btnImport" LoadingPanelID="lp">
                                <ClientSideEvents CloseUp="function(s, e) {s.SetContentUrl(s.GetContentUrl()); }" />
                            </cc:ASPxPopupControl>
                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                            </dx:ASPxLoadingPanel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
