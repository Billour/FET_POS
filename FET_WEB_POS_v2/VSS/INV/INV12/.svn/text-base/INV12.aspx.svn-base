<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV12.aspx.cs" Inherits="VSS_INV_INV12_INV12" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        _s = null;
        _e = null;
        function getProductInfo(s, e) {
            _s = s;
            _e = e;
            
            if (s.GetText() != '')
                PageMethods.getProductInfo(s.GetText(), getProductInfo_OnOK);
        }
        
        function getProductInfo_OnOK(returnData) {
            if (returnData != '') {
                var info = returnData.toString().split(';');
                ProductName.SetText(info[0]);
                ProductUnit.SetText(info[1]);
            }
            else {
                ProductName.SetText(null);
                ProductUnit.SetText(null);
                _e.isValid = false;
                _e.errorText = '商品料號不存在，請重新輸入';
                _e.processOnServer = false;
            }
        }

        //檢查門市
        function CheckStoreNO(s, e) {
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
                btnSave.SetEnabled(true);
                btnAddNew.SetEnabled(true);
                btnDelete.SetEnabled(true);
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
                btnSave.SetEnabled(true);
                btnAddNew.SetEnabled(true);
                btnDelete.SetEnabled(true);
                return true;
            }
        }

        function CheckReceivedDate(s, e) {
            var Rec_Date = s.GetText();
            if (Rec_Date != '') {
                if (!IsDate(Rec_Date)) {
                    e.isValid = false;
                    e.errorText = '輸入字串非日期格式，請重新輸入';
                }
                else {
                    var today = new Date();
                    var SDate = new Date(Rec_Date);
                    e.isValid = (SDate <= today);

                    if (!e.isValid) {
                        e.errorText = '進貨日期不允許大於系統日期，請重新輸入';
                    }
                }
            }
            else {
                e.isValid = false;
                e.errorText = '進貨日期不允許空值，請重新輸入';
            }

            if (!e.isValid) {
                btnSave.SetEnabled(false);
                btnAddNew.SetEnabled(false);
                btnDelete.SetEnabled(false);
                e.processOnServer = false;
            }
            else {
                btnSave.SetEnabled(true);
                btnAddNew.SetEnabled(true);
                btnDelete.SetEnabled(true);
            }
        }

        function CheckNumber(s, e) {
            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty != '') {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                if (s.name.indexOf("txt1") > 0 && iQty <= 0) {
                    e.isValid = false;
                    e.errorText = '數量不允許小於等於0，請重新輸入';
                    return false;
                }
                else if (iQty < 0) {
                    e.isValid = false;
                    e.errorText = '總金額不允許小於0，請重新輸入';
                    return false;
                }
            }
        }
            
            
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<asp:HiddenField ID="tempcheck" runat="server" />
    <div>
        <div class="titlef">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <!--無訂單進貨資料輸入-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, NoPurchaseOrderDataEntry %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                            AutoPostBack="false" CausesValidation="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ document.location='../INV13/INV13.aspx'; }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>        
            
                <div>
                    <table style="width: 100%">
                        <tr>
                            <td class="tdtxt">
                                <!--進貨單號-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReceivingNoteNumber %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:Literal ID="NPOrderNO" runat="server"></asp:Literal>
                            </td>
                            <td class="tdtxt">
                                <!--門市編號-->
                                <span style="color: Red">*</span>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <uc1:PopupControl ID="txtStoreNO" runat="server" PopupControlName="StoresPopup" IsValidation="true" SetClientValidationEvent="CheckStoreNO" />
                                <dx:ASPxTextBox ID="lblStoreName" runat="server" ClientInstanceName="lblStoreName" ClientVisible="false"></dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                                <!--更新日期-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:Literal ID="ModifiedDate" runat="server" Text="">
                                </asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--進貨日期-->
                                <span style="color: Red">*</span>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReceivedDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxDateEdit ID="ReceivedDate" runat="server" ClientInstanceName="ReceivedDate" ClientEnabled="false">
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s,e) { CheckReceivedDate(s,e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                            <td class="tdtxt">
                                <!--備註-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="txtRemark" runat="server" Width="200px" />
                            </td>
                            <td class="tdtxt">
                                <!--更新人員-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:Literal ID="ModifiedBy" runat="server" Text=""></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div class="seperate"></div>
        
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="NP_ORDER_D_ID"
                    Width="100%" EnableCallBacks="False"
                    OnRowInserting="gvMaster_RowInserting"
                    OnRowValidating="gvMaster_RowValidating"
                    OnInitNewRow="gvMaster_InitNewRow"
                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" 
                    OnPageIndexChanged="gvMaster_PageIndexChanged" 
                    OnStartRowEditing="gvMaster_StartRowEditing" 
                    oncancelrowediting="gvMaster_CancelRowEditing" 
                    onprerender="gvMaster_PreRender">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" ButtonType="Button" VisibleIndex="0">
                            <HeaderTemplate>
                                <input type="checkbox" onclick="if (typeof(gvMaster) != 'undefined') {gvMaster.SelectAllRowsOnPage(this.checked);}" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn VisibleIndex="1" Caption=" ">
                            <EditItemTemplate>
                                 <input type="button" value="儲存" onclick="gvMaster.UpdateEdit(); btnSave.SetEnabled(true); btnDrop.SetEnabled(true);"/>
                                 <input type="button" value="取消" onclick="gvMaster.CancelEdit(); btnSave.SetEnabled(true); btnDrop.SetEnabled(true);"/>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn ReadOnly="true" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="2">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <EditItemTemplate>
                                &nbsp;
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="PRODNO" VisibleIndex="3">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            
                            <EditItemTemplate>
                                <uc1:PopupControl ID="txtProductCode" runat="server" PopupControlName="ProductsPopup"
                                    Text='<%# Bind("PRODNO") %>' IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                    SetClientValidationEvent="getProductInfo" />
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="ProductName" VisibleIndex="4" Caption="<%$ Resources:WebResources, ProductName %>">
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="lblProductName" runat="server" Text='<%# Bind("ProductName") %>' ClientInstanceName="ProductName"
                                  ReadOnly="true" Border-BorderStyle="None">
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="ProductUnit" Caption="<%$Resources:WebResources, Unit %>"
                            VisibleIndex="5">
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="lblProductUnit" runat="server" Text='<%# Bind("ProductUnit") %>'
                                    ClientInstanceName="ProductUnit" ReadOnly="true" Border-BorderStyle="None">
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ORDER_QTY" VisibleIndex="6">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Quantity %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txt1" runat="server" Width="100px" Text='<%#BIND("ORDER_QTY")%>' HorizontalAlign="Right"
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="9">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式/數量不允許小於等於0，請重新輸入。"/>
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s,e){ CheckNumber(s, e); }" />
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ORDER_AMT" VisibleIndex="7">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TotalAmount %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txt2" runat="server" Width="100px" Text='<%#BIND("ORDER_AMT")  %>'
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' HorizontalAlign="Right" MaxLength="9">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式/總金額不允許小於0，請重新輸入。" />
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s,e){ CheckNumber(s, e); }" />
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                         <dx:ASPxButton ID="btnAddNew" ClientInstanceName="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" 
                                            AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false" OnClick="btnAddNewM_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" ClientInstanceName="btnDelete" AutoPostBack="false" runat="server" CausesValidation="false"
                                            Text="<%$ Resources:WebResources, Delete %>" OnClick="btnDelete_Click" UseSubmitBehavior="false">
                                            <ClientSideEvents Click = "function(s,e){if (!confirm('系統將刪除勾選之資料，確認刪除？')){e.processOnServer=false;}}"/>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <Settings ShowTitlePanel="true"></Settings>
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                </cc:ASPxGridView>
        
                <div class="seperate"></div>
                
                <div class="btnPosition">
                    <table align="center" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnSave" ClientInstanceName="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                     UseSubmitBehavior="false" OnClick="btnSave_Click" CausesValidation="false" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCancel" ClientInstanceName="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                     UseSubmitBehavior="false" CausesValidation="false" onclick="btnCancel_Click">
                                     <ClientSideEvents Click="function(s,e){if (!confirm('你確定要取消嗎？')){e.processOnServer=false;}}" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnPrint" ClientInstanceName="btnPrint" runat="server" Text="<%$ Resources:WebResources, PrintBarCode %>"
                                     UseSubmitBehavior="false" CausesValidation="false" AutoPostBack="false" OnClick="btnPrint_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
        
            </ContentTemplate>
        </asp:UpdatePanel>
       
       <iframe id="fDownload" style="display:none" src="" runat="server"></iframe>
    </div>
</asp:Content>
