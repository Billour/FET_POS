<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV25.aspx.cs" Inherits="VSS_INV_INV25" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">

        function getProductInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getProductInfo(s.GetText(), getProductInfo_OnOK);
        }

        function getProductInfo_OnOK(returnData) {

            if (returnData != '') {
                var values = returnData.split(';');

                var OldProdName = ProductName.GetText();
                if (OldProdName != '' && OldProdName != values[1]) {
                    //變更商品，就把原本選取的IMEI個數清空
                    lblIMEI_QTY.SetText(null);
                    txtIMEI.SetText(null);
                }
                ProductName.SetText(values[1]);
                txtIMEIFlag.SetText(values[2]);

                EnableIMEI();
                //imeiPop.SetContentUrl(setContentUrl_IMEI_PRODNO(imeiPop.GetContentUrl(), values[0]));
                var url = setContentUrl_IMEI_PRODNO(imeiPop.GetContentUrl());
                //**2011/04/25 Tina：將URL傳遞的參數加密。
                ModifyPopupURLByEncrypt(url, imeiPop);
            }
            else {
                ProductName.SetText(null);
                txtIMEIFlag.SetText(null);
                lblIMEI_QTY.SetText(null);
                txtIMEI.SetText(null);
            }
        }

        //設定IMEI的商品料號
        function setContentUrl_IMEI_PRODNO(url) {
//            var s = url.split('SysDate=');
//            if (s.length > 1) {
//                var ordSysDate = s[1].split('&')[0];
//                var newSysDate = Date();
//                url = url.replace(ordSysDate, newSysDate);
//            }
//            
//            var u = url.split('KeyFieldValue1=');
//            if (u.length > 1) {
//                var oldKeyFieldValue1 = u[1].split('&')[0];
//                pValues = oldKeyFieldValue1.split(';');
//                if (pValues.length >= 4) {
//                    pValues[2] = prodno;
//                    var newKeyFieldValue1 = pValues[0] + ';' + pValues[1] + ';' + pValues[2] + ';' + pValues[3];
//                    url = url.replace(oldKeyFieldValue1, newKeyFieldValue1);
//                }
//            }
            var s = url.split('?');
             if (s.length > 1) {
                 var t = new Date().getTime();
                 var D_UUID = STORETRANSFER_D_ID.GetText();
                 url = s[0] + "?SysDate=" + t + "&KeyFieldValue1=STORETRANSFER_IMEI;" + D_UUID + ";" + txtProductCode.GetText() + ";" + txtTranOutQty.GetText();
             }
            return url;
        }

        //是否可編輯IMEI
        function EnableIMEI() {

            var strProductName = ProductName.GetText();
            var strTranOutQty = txtTranOutQty.GetText();
            var iQty = Number(strTranOutQty);

            //商品料號 && 移出數量 皆有值，才可編輯IMEI (IMEI_FLAG = 3, 4 要設定IMEI)
            if ((txtIMEIFlag.GetText() == "3" || txtIMEIFlag.GetText() == "4") && strProductName != '' && !isNaN(iQty) && strTranOutQty != '') {
                txtIMEI.SetVisible(true);
                btnIMEI.SetVisible(true);
                txtIMEI.SetEnabled(true);
                btnIMEI.SetEnabled(true);

                imeiPop.SetPopupElementID(btnIMEI.name);
                imgIMEI.SetVisible(true);
                if (strTranOutQty == lblIMEI_QTY.GetText()) {
                    imgIMEI.SetImageUrl("../../../Icon/check.png");
                    imgIMEI.SetSize(16, 16);
                }
                else {
                    imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                    imgIMEI.SetSize(27, 16);
                }
            }
            else {
                if (txtIMEI.GetVisible()) txtIMEI.SetVisible(false);
                if (btnIMEI.GetVisible()) btnIMEI.SetVisible(false);
                imeiPop.SetPopupElementID(null);
                imgIMEI.SetVisible(false); 
            }

        }

        //變更IMEI圖示
        function ChangeImageIMEI(s, e) {

            var IMEI_Qty = s;

            if (txtTranOutQty.GetText() == IMEI_Qty.GetText()) {
                imgIMEI.SetImageUrl("../../../Icon/check.png");
                imgIMEI.SetSize(16, 16);
            }
            else {
                imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                imgIMEI.SetSize(27, 16);
            }

            var pValues = getIMEI(s, e);
            //0:TableName 1:OE_NO 2:PRODNO,4 IMEI_QTY
            //PageMethods.IMEIContent(pValues[0], pValues[1], pValues[2], IMEIContent);
            PageMethods.IMEIContent("STORETRANSFER_IMEI", STORETRANSFER_D_ID.GetText(), txtProductCode.GetText(), IMEIContent);
        }

        function IMEIContent(values) {
            var lblIMEI = window.document.all[lblIMEI_QTY.name];
            lblIMEI.attributes["onmouseover"].value = "show('" + values + "');";
            lblIMEI.attributes["onmouseout"].value = "hide();";
        }
                        
        //取得IMEI上傳參數;
        function getIMEI(s, e) {
            var pValues = null;
            var u = imeiPop.GetContentUrl().split('KeyFieldValue1=');
            if (u.length > 1) {
                var oldKeyFieldValue1 = u[1].split('&')[0];
                pValues = oldKeyFieldValue1.split(';');
            }
            return pValues;
        }

        //檢查移出數量
        function CheckTranOutQty(s, e) {

            if (lblError.GetValue != "") {
                lblError.SetValue(null);
            }

            EnableIMEI();

            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty == null || Qty == "") {
                e.isValid = false;
                e.errorText = '移出數量不允許空白，請重新輸入';
                return false;
            }
            else {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串不為數字格式，請重新輸入';
                    return false;
                }
                else if (iQty <= 0) {
                    e.isValid = false;
                    e.errorText = '移出數量需不允許小於等於0，請重新輸入';
                    return false;
                }
                else {
                    //imeiPop.SetContentUrl(setContentUrl_IMEI_QTY(imeiPop.GetContentUrl(), s.GetText()));
                    var url = setContentUrl_IMEI_PRODNO(imeiPop.GetContentUrl());
                    //**2011/04/25 Tina：將URL傳遞的參數加密。
                    ModifyPopupURLByEncrypt(url, imeiPop);
                }
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
                btnAddNew.SetEnabled(false);
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
                btnAddNew.SetEnabled(false);
                btnDelete.SetEnabled(false);
            }
            else {
                lblStoreName.SetText(returnData);
                if (!btnPrint.GetEnabled()) {
                    btnAddNew.SetEnabled(true);
                    btnDelete.SetEnabled(true);
                }
            }
        }
        
        function CheckStoreNoIsEmpty() {
            if (lblStoreName.GetText() == "") {
                if (gvMaster.IsEditing()) gvMaster.CancelEdit();
                btnSave.SetEnabled(false);
                btnAddNew.SetEnabled(false);
                btnDelete.SetEnabled(false);
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
                btnAddNew.SetEnabled(true);
                btnDelete.SetEnabled(true);
            }
        }
        
      
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip"></div>
    
    <div class="titlef">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--移出作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockTransferOut %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="LinkButton1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        PostBackUrl="INV24.aspx" CausesValidation="false" UseSubmitBehavior="false">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="criteria">
                <table>
                    <tr>
                        <td align="right" class="tdtxt">
                            <!--移撥單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                        </td>
                        <td align="left" class="tdval">
                            <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                        </td>
                        <td align="right" class="tdtxt">
                            <!--撥入門市-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="lblToStoreNO" runat="server" Text="撥入門市編號"></asp:Literal>：
                        </td>
                        <td align="left" class="tdval">
                            <uc1:PopupControl ID="txtToStoreNO" runat="server" PopupControlName="StoresPopup"
                                IsValidation="true" SetClientValidationEvent="CheckStoreNO" />
                            <dx:ASPxTextBox ID="lblStoreName" runat="server" ClientInstanceName="lblStoreName" ClientVisible="false"></dx:ASPxTextBox>
                        </td>
                        <td align="right" class="tdtxt">
                            <!--狀態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td align="left" class="tdval">
                            <dx:ASPxLabel ID="lblStatus" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td align="right">
                            <!--更新日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources,ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td align="left">
                            <dx:ASPxLabel ID="lblDateTime" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3">
                        </td>
                        <td align="right">
                            <!--更新人員-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td align="left">
                            <dx:ASPxLabel ID="lblUser" runat="server"></dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <div>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
                    KeyFieldName="STORETRANSFER_D_ID" EnableCallBacks="false"
                    OnRowInserting="gvMaster_RowInserting"
                    OnRowUpdating="gvMaster_RowUpdating" 
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                    OnInitNewRow="gvMaster_InitNewRow"
                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" 
                    OnRowValidating="gvMaster_RowValidating"
                    OnPageIndexChanged="gvMaster_PageIndexChanged" 
                    OnStartRowEditing="gvMaster_StartRowEditing" 
                    oncancelrowediting="gvMaster_CancelRowEditing">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                            <HeaderTemplate>
                                <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                            <EditButton Visible="True"></EditButton>
                            <%--<EditItemTemplate>
                                 <input type="button" value="儲存" onclick="gvMaster.UpdateEdit(); btnSave.SetEnabled(true); btnDrop.SetEnabled(true);"/>
                                 <input type="button" value="取消" onclick="gvMaster.CancelEdit(); EnabledBtnSave(); btnDrop.SetEnabled(true);"/>
                            </EditItemTemplate>--%>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
                            <EditItemTemplate>
                                <uc1:PopupControl ID="txtProductCode" runat="server" PopupControlName="ProductsPopup" TextBoxClientInstanceName="txtProductCode"
                                    Text='<%# Bind("PRODNO") %>' IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                    SetClientValidationEvent="getProductInfo" />
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="ProductName" Caption="<%$ Resources:WebResources, ProductName %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="ProductName" runat="server" Text='<% #Bind("PRODNAME") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="lblProductName" runat="server" ClientInstanceName="ProductName"
                                    Text='<% #Bind("PRODNAME") %>' ReadOnly="true" Border-BorderStyle="None">
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="TRANOUTQTY" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>">
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtTranOutQty" runat="server" HorizontalAlign="Right" Text='<%# Bind("TRANOUTQTY") %>' ClientInstanceName="txtTranOutQty"
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' Width="100px" MaxLength="9">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串不為數字格式，請重新輸入" />
                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                    <ClientSideEvents TextChanged="function(s,e){ CheckTranOutQty(s, e); }" />
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="imgIMEI" Caption=" " Width="20px">
                            <DataItemTemplate>
                                <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                </dx:ASPxImage>
                            </DataItemTemplate>
                            <EditItemTemplate>
                                <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="" ClientInstanceName="imgIMEI">
                                </dx:ASPxImage>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>">
                            <DataItemTemplate>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td align="right">
                                            <div id="divIMEI_QTY" runat="server" >
                                            <dx:ASPxLabel ID="lblIMEI" runat="server" Text='<% #Bind("IMEI_QTY") %>'>
                                            </dx:ASPxLabel>
                                            </div>
                                        </td>
                                        <div id="divIMEI" runat="server">
                                            <td>
                                                <dx:ASPxTextBox ID="lblIMEIFlag" runat="server" ReadOnly="true" Text='<% #Bind("IMEI_FLAG") %>'
                                                    Border-BorderStyle="None" ClientVisible="false">
                                                </dx:ASPxTextBox>
                                                <uc1:PopupControl ID="lblShowIMEI" runat="server" PopupControlName="InputIMEIData" Text='<%# Bind("IMEI") %>'
                                                    Enabled="false" />
                                            </td>
                                        </div>
                                    </tr>
                                </table>&nbsp;
                            </DataItemTemplate>
                            <EditItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="lblIMEI_QTY" runat="server" ReadOnly="true" Text='<% #Bind("IMEI_QTY") %>' ClientInstanceName="lblIMEI_QTY"
                                                Border-BorderStyle="None" Width="20px" DisabledStyle-Font-Underline="true">
                                                <ClientSideEvents TextChanged="function(s, e) { ChangeImageIMEI(s, e); }" />
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="hdSTORETRANSFER_D_ID" runat="server" Text='<%# Bind("STORETRANSFER_D_ID") %>' ClientInstanceName="STORETRANSFER_D_ID" ClientVisible="false"></dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="txtIMEIFlag" runat="server" ReadOnly="true" Text='<% #Bind("IMEI_FLAG") %>'
                                                Border-BorderStyle="None" ClientInstanceName="txtIMEIFlag" ClientVisible="false">
                                            </dx:ASPxTextBox>
                                            <uc1:PopupControl ID="txtIMEI" runat="server" PopupControlName="InputIMEIData" AssignToControlId="lblIMEI_QTY" Text='<%# Bind("IMEI") %>' 
                                                TextBoxClientInstanceName="txtIMEI" ButtonClientInstanceName="btnIMEI" PopupControlClientInstanceName="imeiPop"/>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table cellpadding="0" cellspacing="0" border="0" align="left">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddNew" ClientInstanceName="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" 
                                            OnClick="btnAddNewM_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td> 
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton SkinID="DeleteBtn" ClientInstanceName="btnDelete" ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                            CausesValidation="false" OnClick="btnDelete_Click"  UseSubmitBehavior="false"/>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <SettingsEditing Mode="Inline" />
                    <Settings ShowTitlePanel="true"/>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                    <%--<ClientSideEvents RowDblClick="function(s, e) { if (!gvMaster.IsEditing()) {gvMaster.SelectAllRowsOnPage(false); gvMaster.StartEditRow(e.visibleIndex); btnSave.SetEnabled(false); btnDrop.SetEnabled(false); } }" />--%>
                </cc:ASPxGridView>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="lblError" runat="server" Text="" ForeColor="Red" ClientInstanceName="lblError">
                            </dx:ASPxLabel>
                        </td>
                        <td align="right">
                            <dx:ASPxButton ID="btnSave" ClientInstanceName="btnSave" runat="server" Text="<%$ Resources:WebResources, ConfirmTransferOut %>"
                                OnClick="btnSave_Click" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function (s, e) {e.processOnServer = confirm('移出商品確認移出後，不可再修改，是否確定要移出？');}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                                <dx:ASPxButton ID="btnDrop"  ClientInstanceName="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                   UseSubmitBehavior="false" CausesValidation="false" OnClick="btnCancel_Click" >
                                </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnPrint" ClientInstanceName="btnPrint" runat="server" Text="<%$ Resources:WebResources, PrintTransferOutSlip %>"
                                UseSubmitBehavior="false" CausesValidation="false" OnClick="btnPrint_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <iframe id="fDownload" style="display:none" src="" runat="server"></iframe>
    
</asp:Content>
