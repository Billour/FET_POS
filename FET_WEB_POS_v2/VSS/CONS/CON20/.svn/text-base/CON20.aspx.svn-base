<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON20.aspx.cs" Inherits="VSS_CONS_CON20" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function getProductInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getProductInfo(s.GetText(), getProductInfo_OnOK);
        }

        function getProductInfo_OnOK(returnData) {

            if (returnData != '') {
                var values = returnData.split(';');
                ProductName.SetText(values[1]);
                ProductTypeName.SetText(values[2]);
                ProductTypeNo.SetText(values[3]);

            }
            else {
                ProductName.SetText(null);
                ProductTypeName.SetText(null);
                ProductTypeNo.SetText(null);
            }
        }
        //檢查門市
        function CheckStoreNO(s, e) {
            if (lblError.GetValue != "") {
                lblError.SetValue(null);
            }

            var StoreNO = s.GetValue();
            if (StoreNO == null || StoreNO == "") {
                lblStoreName.SetText(null);
                btnAddNewRow.SetEnabled(false);
                e.isValid = false;
                e.errorText = '撥入門市編號不允許空白，請重新輸入';
                e.processOnServer = false;
                CheckStoreNoIsEmpty();
            }
            else {
                PageMethods.getStoreInfo(StoreNO, getStoreInfo_OnOK);

            }
        }

        function getStoreInfo_OnOK(returnData) {
            if (returnData == '') {
                lblStoreName.SetText(null);
                alert('門市編號不存在，請重新輸入');
                btnSave.SetEnabled(false);
                btnAddNewRow.SetEnabled(false);
                btnDeleteRow.SetEnabled(false);
            }
            else {
                lblStoreName.SetText(returnData);
                if (!btnPrint.GetEnabled()) {
                    btnAddNewRow.SetEnabled(true);
                    btnDeleteRow.SetEnabled(true);
                }
            }
        }

        function CheckStoreNoIsEmpty() {
            if (lblStoreName.GetText() == "") {
                if (gvMaster.IsEditing()) gvMaster.CancelEdit();
                btnSave.SetEnabled(false);
                btnAddNewRow.SetEnabled(false);
                btnDeleteRow.SetEnabled(false);
                return false;
            }
            else {
                EnabledBtnSave();
                return true;
            }
        }


        function EnabledBtnSave() {
            if (gvMaster.pageRowCount > 0) {
                btnSave.SetEnabled(true);
                btnAddNewRow.SetEnabled(true);
                btnDeleteRow.SetEnabled(true);
            }
        }

        function CheckConfirm(s, e) {
        var rtn= confirm('移出商品確認移出後，不可再修改，是否確定要移出？');
        e.processOnServer = rtn;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品移出作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferOut %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        ClientSideEvents-Click="function(){document.location='CON19.aspx'}" AutoPostBack="False"
                        CausesValidation="false" />
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="seperate">
            </div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--移撥單號-->
                            <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>">
                            </dx:ASPxLabel>
                            ：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--撥入門市-->
                            <dx:ASPxLabel ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TransferTo %>">
                            </dx:ASPxLabel>
                            ：
                        </td>
                        <td class="tdval">
                            <uc1:PopupControl ID="txtToStoreNO" runat="server" PopupControlName="StoresPopup"
                                IsValidation="true" SetClientValidationEvent="CheckStoreNO" />
                            <dx:ASPxTextBox ID="lblStoreName" runat="server" ClientInstanceName="lblStoreName"
                                ClientVisible="false">
                            </dx:ASPxTextBox>
                            <%-- <table cellpadding="0" cellspacing="0" border="0" align="center" style="width: 120px">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="TextBox2" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="Button3" runat="server" SkinID="PopupButton" AutoPostBack="false" />
                            </td>
                        </tr>
                    </table>
                    <cc:ASPxPopupControl ID="StoresPopup" SkinID="StoresPopup" runat="server" EnableViewState="False"
                        PopupElementID="Button3" TargetElementID="TextBox2" LoadingPanelID="lp1">
                    </cc:ASPxPopupControl>
                    <dx:ASPxLoadingPanel ID="lp1" runat="server">
                    </dx:ASPxLoadingPanel>--%>
                        </td>
                        <td class="tdtxt">
                            <!--狀態-->
                            <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, status %>">
                            </dx:ASPxLabel>
                            ：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblStatus" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                            <!--更新日期-->
                            <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>">
                            </dx:ASPxLabel>
                            ：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblDateTime" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                            <!--更新人員-->
                            <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>">
                            </dx:ASPxLabel>
                            ：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblUser" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div id="Div1" runat="server" class="SubEditBlock" visible="true">
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PRODNO"
                    Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" OnPageIndexChanged="gvMaster_PageIndexChanged"
                    OnRowInserting="gvMaster_RowInserting" OnRowUpdating="gvMaster_RowUpdating" OnCancelRowEditing="gvMaster_CancelRowEditing"
                    OnRowValidating="gvMaster_RowValidating" OnStartRowEditing="gvMaster_StartRowEditing"
                    OnInitNewRow="gvMaster_InitNewRow" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center"
                            Caption=" " ButtonType="Button">
                            <HeaderTemplate>
                                <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                            
                           <UpdateButton Visible="false" Text="確認"></UpdateButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption=" ">
                           
                            <EditItemTemplate>
                                <input type="button" value="儲存" onclick="gvMaster.UpdateEdit(); btnSave.SetEnabled(true); btnDrop.SetEnabled(true);" />
                                <input type="button" value="取消" onclick="gvMaster.CancelEdit(); EnabledBtnSave(); btnDrop.SetEnabled(true);" />
                            </EditItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <EditCellStyle HorizontalAlign="Center">
                            </EditCellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="PRODTYPENAME" runat="server"
                            Caption="<%$ Resources:WebResources, ProductCategory %>">
                            <EditItemTemplate>
                                <table border="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="lblProductTypeName" runat="server" ClientInstanceName="ProductTypeName"
                                                Text='<%# Bind("PRODTYPENAME") %>' ReadOnly="true" Border-BorderStyle="None">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="lblProductTypeNo" runat="server" Text='<%# Bind("PRODTYPENO") %>'
                                                ClientInstanceName="ProductTypeNo" ClientVisible="false">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="PRODNO" runat="server">
                            <HeaderCaptionTemplate>
                                <span style="color: red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <uc1:PopupControl ID="txtProductCode" runat="server" PopupControlName="ProductsPopup"
                                    KeyFieldValue1="consignmentsale" Text='<%# Bind("PRODNO") %>' IsValidation="true"
                                    ValidationGroup='<%# Container.ValidationGroup %>' OnClientTextChanged="function(s, e) { getProductInfo(s, e); }" />
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="lblProductName" runat="server" ClientInstanceName="ProductName"
                                    Text='<% #Bind("PRODNAME") %>' ReadOnly="true" Border-BorderStyle="None">
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="TRANOUTQTY" runat="server">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TransferredOutQuantity %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtTranOutQty" runat="server" HorizontalAlign="Right" Text='<%# Bind("TRANOUTQTY") %>'
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' Width="100px"
                                    MaxLength="9">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="[1-9]{1,5}\d{0,}" ErrorText="輸入數字格式有誤，請重新輸入" />
                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table cellpadding="0" cellspacing="0" border="0" align="left">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddNewRow" ClientInstanceName="btnAddNewRow" runat="server"
                                            Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddRow_Click" AutoPostBack="false"
                                            UseSubmitBehavior="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton SkinID="DeleteBtn" ID="btnDeleteRow" ClientInstanceName="btnDeleteRow"
                                            runat="server" Text="<%$ Resources:WebResources, Delete %>" AutoPostBack="false"
                                            UseSubmitBehavior="false" CausesValidation="false" OnClick="btnDeleteRow_Click" />
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <SettingsPager PageSize="5" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <ClientSideEvents RowDblClick="function(s, e) { if (!gvMaster.IsEditing()) {gvMaster.SelectAllRowsOnPage(false); gvMaster.StartEditRow(e.visibleIndex); btnSave.SetEnabled(false); btnDrop.SetEnabled(false); } }" />
                </cc:ASPxGridView>
            </div>
            <div class="seperate">
            </div>
              <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxLabel ID="lblError" runat="server" Text="" ForeColor="Red" ClientInstanceName="lblError">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, ConfrimTransferOut %>"
                        OnClick="btnSave_Click" ClientInstanceName="btnSave" UseSubmitBehavior="false">
                        <ClientSideEvents  Click="function (s, e) {CheckConfirm(s, e);}" />
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnDrop" ClientInstanceName="btnDrop" runat="server" UseSubmitBehavior="false"
                        CausesValidation="false" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnPrint" ClientInstanceName="btnPrint" OnClick="btnPrint_Click"
                        UseSubmitBehavior="false" CausesValidation="false" runat="server" Text="<%$ Resources:WebResources, PrintTransferOutSlip %>" />
                </td>
            </tr>
        </table>
        </div>
        </ContentTemplate>
       
    </asp:UpdatePanel>
   
    <iframe id="fDownload" style="display:none"  runat="server"></iframe>
</asp:Content>
