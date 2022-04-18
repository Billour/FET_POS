<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LEA01.aspx.cs" Inherits="VSS_LEA_LEA01" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">
       
        function chName(s, e) {
            var Qty = s.GetValue();
            var iQty = 0;
//            if (s.GetText() == '') {
//                e.errorText = '金額不允許空值，請重新輸入';
//                return false;
//            }
            if (Qty != null) {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                else if (iQty <= 0) {
                e.errorText = '不允許小於0，請重新輸入';
                    return false;
                }
                else if (Qty.indexOf(".") > 0) {
                e.errorText = '不允許輸入小數點，請重新輸入';
                    return false;
                }
            }
        }


        function chkNameFunction(s, e ,message) {
            var Qty = s.GetValue();
            var iQty = 0;
            
            if (Qty != null) {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.errorText = message;
                    return false;
                }
                else if (iQty <= 0) {
                    e.errorText = message;
                    return false;
                }
                else if (Qty.indexOf(".") > 0) {
                    e.errorText = message;
                    return false;
                }
            }
            
            return true;
        }

        
        function chkReturnMoney(s, e) {
            return chkNameFunction(s, e, '【賠償金料號欄位有誤】');
        }
        
        function chkMoney(s, e) {
            return chkNameFunction(s, e, '【保證金欄位有誤】');
        }
        
        function chkProdRent(s, e) {
            return chkNameFunction(s, e, '【租金料號欄位有誤】');
        }
        
        function chkDayRent(s, e) {
            return chkNameFunction(s, e, '【日租金額欄位有誤】');
        }

        function getSupplierId(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getSupplierId(_gvSender.GetText(), getSupplierId_OnOK);
        }
        
        function getSupplierId_OnOK(returnData) {
            //debugger;
            if (returnData == '') {
                alert("廠商編號有誤!");
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
                _gvSender.SetText(null);

            }
            else {
                $('#labSupplierName').val(returnData);
            }
        }

        function chkSupportStartDate(s, e) {
            e.isValid = true;
            var x = postbackDate_Start.GetValue();
            var y = postbackDate_End.GetValue();
            var dvalue = s.GetValue();
            if (x == null) { x = ""; }
            if (y == null) { y = ""; }
            if (x != "" && y != "") {
                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[開始起日訖值]不允許小於[開始起日起值]，請重新輸入!");
                    s.SetValue(null);
                }
            }
            if (e.isValid && dvalue != "") {
                var Sx = postbackDate_Start.GetValue();
                var Sy = postbackDate_End.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue > Sx) || (Sy != "" && dvalue > Sy)) {
                    e.isValid = false;
                    alert("[開始日期]不允許大於[結束日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }

        function chkSupportExpiryDate(s, e) {
            e.isValid = true;
            var x = postbackDate_Start.GetValue();
            var y = postbackDate_End.GetValue();
            var dvalue = s.GetValue();
            if (x == null) { x = ""; }
            if (y == null) { y = ""; }
            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[有效期間起值]不允許大於[結束訖日訖值]，請重新輸入!");
                    s.SetValue(null);
                }
            }
            if (e.isValid && dvalue != "") {
                var Sx = postbackDate_Start.GetValue();
                var Sy = postbackDate_End.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue < Sx) || (Sy != "" && dvalue < Sy)) {
                    e.isValid = false;
                    alert("[結束日期]不允許小於[開始日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }

        //檢查商品料號是否存在
        _gvSender = null;
        _gvEventArgs = null;
        
        function getPRODINFO1(s, e) {
            //debugger;
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '') {
                PageMethods.getPRODINFO(_gvSender.GetText(), getPRODINFO_OnOK1);
                PageMethods.getAccountCode(_gvSender.GetText(), getAccountCode_OnOK);
            }
        }

        function getPRODINFO_OnOK1(returnData) {
            var fName = "popPRODNO";
            if (returnData == '') {
                _gvEventArgs.processOnServer = false;
                alert("折扣料號不存在!");
                _gvSender.Focus();
            }
            else {
                //0 品名 1 I MEM_FLAGE
                if (_gvSender.GetText() == '')
                    txtActivityName.SetText('');
                else
                    txtActivityName.SetText(returnData);

            }
        }

        function getAccountCode_OnOK(returnData) {
            if (returnData == '') {
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
                txtACCOUNT_CODE.SetValue(null);
            }
            else {
                if (returnData == "fail") {
                    //alert("會計科目不允許設定!");
                    _gvSender.Focus();
                    _gvSender.SetText("");
                }
                else {
                    txtACCOUNT_CODE.SetValue(returnData);
                    _gvEventArgs.processOnServer = false;
                    _gvSender.Focus();
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
       
            <div class="titlef">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <!--設備租賃設定-->
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, EquipmentLeasing %>"></asp:Literal>
                        </td>
                        <td align="right">
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>">
                                <ClientSideEvents Click="function(s, e) { document.location='LEA07.aspx'; }" />
                            </dx:ASPxButton>
                                
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--類別-->
                            <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <dx:ASPxRadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" Border-BorderStyle="None">
                                <Items>
                                    <dx:ListEditItem Value="1" Text="漫遊租賃" Selected="true" />
                                    <dx:ListEditItem Value="2" Text="維修租賃" />
                                </Items>
                            </dx:ASPxRadioButtonList>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--商品類別-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="DropProduct" runat="server" >
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            <!--產品名稱-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <uc1:PopupControl ID="ProductsPopup2" KeyFieldValue1="consignmentsale" KeyFieldValue2="PRODNAME" runat="server" PopupControlName="ProductsPopup3"  />
                        </td>
                        <td class="tdtxt">
                            <!--狀態-->
                            <asp:Literal ID="Literal3" runat="server" Text="設定狀態"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="labState" runat="server"  Text="未存檔" ></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--外部廠商代碼-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, OutsideFirmNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <uc1:PopupControl ID="txtSupplierNo" runat="server" PopupControlName="ConsignmentVendorsPopup" 
                                IsValidation="true" KeyFieldValue2="SUPP_NO" Width="280px"  SetClientValidationEvent="getSupplierId"  />
                        </td>
                        <td class="tdtxt">
                            <!--外部廠商名稱-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OutsideFirmName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="labSupplierName"  ClientInstanceName="labSupplierName" runat="server" ></dx:ASPxLabel>
                        </td>
                        <td class="tdtxt">
                            <!--更新日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblModiDTM" ClientInstanceName="lblModiDTM" runat="server" ></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--租金料號-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PartNumberOfRent %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtRentNO" ClientInstanceName="txtRentNO" MaxLength="20" Width="250px" runat="server">
                                <ClientSideEvents  Validation="function(s, e){  chkProdRent(s, e); }" />
                                <ValidationSettings>
                                                    <RegularExpression ValidationExpression="\d*"  ErrorText="【租金料號欄位有誤】" />
                                                    <RequiredField IsRequired="true" ErrorText="必填欄位" /> 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--日租金額-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, DailyRent %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtDailyRent" runat="server" MaxLength="9" Text="0" ClientInstanceName="txtDailyRent" >
                                <ClientSideEvents  Validation="function(s, e){ chkDayRent(s, e);  }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="【日租金額欄位有誤】" />
                                                        <RequiredField IsRequired="true" ErrorText="必填欄位" /> 
                                    </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--維護人員-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblModiUser" runat="server" ></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--保證金料號-->
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, PartNumberOfRentDeposit %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtRentDepositNO" ClientInstanceName="txtRentDepositNO"  Width="250px"  MaxLength="20" runat="server">
                            <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                                        <RequiredField IsRequired="true" ErrorText="必填欄位" /> 
                                    </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--保證金-->
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, RentDeposit %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtRentDepositNumber" runat="server" MaxLength="8" ClientInstanceName="txtRentDepositNumber" >
                                    <ClientSideEvents  Validation="function(s, e){ chkMoney(s, e); }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="【保證金欄位有誤】" />
                                    </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--賠償金料號-->
                            <asp:Literal ID="Literal13" runat="server"  Text="<%$ Resources:WebResources, PartNumberOfCompensation %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtCompensationNO" ClientInstanceName="txtCompensationNO" Width="250px"  MaxLength="20" runat="server">
                                <ClientSideEvents  Validation="function(s, e){ chkReturnMoney(s, e); }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="【賠償金料號欄位有誤】" />
                                    </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--有效期間-->
                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, EffectiveDuration %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="1">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <dx:ASPxDateEdit ID="postbackDate_Start" 
                                            ClientInstanceName="postbackDate_Start" runat="server">
                                            <ValidationSettings SetFocusOnError="true">
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ chkSupportStartDate(s, e); }" />
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <dx:ASPxDateEdit ID="postbackDate_End" ClientInstanceName="postbackDate_End" runat="server">
                                            <ValidationSettings SetFocusOnError="true">
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ chkSupportExpiryDate(s, e); }" />
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--備註-->
                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <dx:ASPxMemo ID="memo" runat="server" Width="98%">
                            </dx:ASPxMemo>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="seperate"></div>
            
            <div>
                <dx:ASPxPageControl ID="TabContainer1"  runat="server"  OnActiveTabChanged="TabContainer1_ActiveTabChanged" AutoPostBack="true"
                     ActiveTabIndex="0" 
                     Width="100%">
                    <TabPages>
                        <dx:TabPage Text="<%$ Resources:WebResources, CompensationItems %>">
                            <ContentCollection>
                                <dx:ContentControl ID="TabPanel1" runat="server">
                                    <div id="Div1" runat="server" class="SubEditBlock">
                                        <div class="GridScrollBar" style="height: auto">
                                            <cc:ASPxGridView Width="100%" ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
                                                AutoGenerateColumns="False" KeyFieldName="RENT_INDEMNIFY_ITEMS"
                                                OnRowInserting="gvMaster_RowInserting"
                                                OnRowUpdating="gvMaster_RowUpdating" 
                                                OnCancelRowEditing="gvMaster_CancelRowEditing" 
                                                OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" 
                                                OnInitNewRow="gvMaster_InitNewRow" 
                                                OnPageIndexChanged="gvMaster_PageIndexChanged" 
                                                OnRowValidating="gvMaster_RowValidating" >
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <HeaderTemplate>
                                                            <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="全選" />
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                                        <EditButton Visible="True">
                                                        </EditButton>
                                                        <HeaderTemplate>
                                                            &nbsp;
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="IND_ITEM_NAME" Caption="<%$ Resources:WebResources, CompensationItems %>"
                                                        VisibleIndex="2">
                                                        <EditItemTemplate>
                                                            <dx:ASPxTextBox ID="txtIndItemName" MaxLength="49" runat="server" Text='<%# Bind("IND_ITEM_NAME") %>' ClientInstanceName="txtIndItemName" Width="150px"></dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="IND_UNIT_PRICE" Caption="<%$ Resources:WebResources, Amount %>"
                                                        VisibleIndex="3">
                                                        <EditItemTemplate>
                                                            <dx:ASPxTextBox ID="txtIndUnitPrice" MaxLength="5" runat="server" Text='<%# Bind("IND_UNIT_PRICE") %>' ClientInstanceName="txtIndUnitPrice" Width="150px">
                                                                <ValidationSettings SetFocusOnError="true">
                                                                    <RequiredField IsRequired="true" ErrorText="【輸入金額】" />
                                                                    <RegularExpression ValidationExpression="\d*"  ErrorText="【輸入金額】" />
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                   <dx:GridViewDataTextColumn FieldName="RENT_INDEMNIFY_ITEMS"  runat="server" Visible="false">
                                                        <%--<DataItemTemplate>
                                                        <dx:ASPxLabel ID="lblIndeItem" runat="server"  Text='<%# Bind("RENT_INDEMNIFY_ITEMS") %>' Border-BorderStyle="None" ></dx:ASPxLabel>
                                                        </DataItemTemplate>--%>
                                                        <EditItemTemplate>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Templates>
                                                    <TitlePanel>
                                                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                                                            <tr>
                                                                <td align="right">
                                                                    <dx:ASPxButton ID="btngvMasterAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                         OnClick="btngvMasterAdd_Click" >
                                                                        <%--<ClientSideEvents Click="function(s,e) { gvMaster.AddNewRow(); }" />--%>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <dx:ASPxButton ID="btngvMasterDelete" OnClick="btngvMasterDelete_Click" runat="server" Text="<%$ Resources:WebResources, Delete %>" >
                                                                        <ClientSideEvents Click= "function (s, e) {e.processOnServer = confirm('您確定是要刪除[賠償項目]資料嗎？');}" />
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </TitlePanel>
                                                </Templates>
                                                <Settings ShowTitlePanel="true" />
                                                <SettingsEditing Mode="Inline" />
                                                <SettingsPager PageSize="10" />
                                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                                            </cc:ASPxGridView>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, DiscountItems %>">
                            <ContentCollection>
                                <dx:ContentControl ID="TabPanel2" runat="server">
                                    <div class="SubEditBlock">
                                        <div class="GridScrollBar" style="height: auto">
                                            <cc:ASPxGridView ID="gvDiscountItem" ClientInstanceName="gvDiscountItem" 
                                                runat="server" Width="100%"
                                                AutoGenerateColumns="False" KeyFieldName="RENT_DISCOUNT_ID" AccessibilityCompliant="True" 
                                                OnRowInserting="gvDiscountItem_RowInserting" 
                                                OnRowUpdating="gvDiscountItem_RowUpdating" 
                                                OnCancelRowEditing="gvDiscountItem_CancelRowEditing" 
                                                OnPageIndexChanged="gvDiscountItem_PageIndexChanged" 
                                                OnCommandButtonInitialize="gvDiscountItem_CommandButtonInitialize" 
                                                OnRowValidating="gvDiscountItem_RowValidating" >
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <HeaderTemplate>
                                                            <input type="checkbox" onclick="gvDiscountItem.SelectAllRowsOnPage(this.checked);"
                                                                title="全選" />
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                                        <EditButton Visible="True">
                                                        </EditButton>
                                                        <HeaderTemplate>
                                                            &nbsp;
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                                                        VisibleIndex="2">
                                                        <EditItemTemplate>
                                                                <uc1:PopupControl ID="popPRODNO" runat="server" PopupControlName="LeasePopup" Text='<%#BIND("[PRODNO]") %>' 
                                                                                        AutoPostBack="false" SetClientValidationEvent="getPRODINFO1(s,e);" />
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, DiscountName %>"
                                                        VisibleIndex="3">
                                                        <EditItemTemplate>
                                                                <dx:ASPxTextBox ID="txtActivityName" ClientInstanceName="txtActivityName"  Width="80px" Text='<%# Bind("PRODNAME") %>'  runat="server">
                                                                </dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="DISCOUNT_AMT" Caption="<%$ Resources:WebResources, DiscountAmount %>"
                                                        VisibleIndex="4">
                                                        <EditItemTemplate>
                                                                <dx:ASPxTextBox ID="txtDISCOUNT_AMT" MaxLength="9" runat="server" Text='<%# Bind("DISCOUNT_AMT") %>' ClientInstanceName="txtDISCOUNT_AMT" Width="60px">
                                                                    <ValidationSettings SetFocusOnError="true">
                                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="【輸入折扣金額】" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="DISCOUNT_RATE" Caption="<%$ Resources:WebResources, DiscountRate %>"
                                                        VisibleIndex="5">
                                                        <EditItemTemplate>
                                                                 <dx:ASPxTextBox ID="txtDISCOUNT_RATE" MaxLength="3" runat="server" Text='<%# Bind("DISCOUNT_RATE") %>' ClientInstanceName="txtDISCOUNT_RATE" Width="40px">
                                                                    <ValidationSettings SetFocusOnError="true">
                                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="【輸入折扣比率】" />
                                                                    </ValidationSettings>
                                                                 </dx:ASPxTextBox>%
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="COST_CENTER_NO" Caption="<%$ Resources:WebResources, CostCenter %>"
                                                        VisibleIndex="6">
                                                        <EditItemTemplate>
                                                                        <uc1:PopupControl ID="popCOST_CENTER_NO" runat="server" PopupControlName="CostCenterPopup"  Text='<%#BIND("[COST_CENTER_NO]") %>' 
                                                                                        AutoPostBack="false" />
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="ACCOUNT_CODE" Caption="<%$ Resources:WebResources, AccountingSubject %>"
                                                        VisibleIndex="7">
                                                        <EditItemTemplate>
                                                                        <dx:ASPxTextBox ID="txtACCOUNT_CODE" MaxLength="30" runat="server" Text='<%# Bind("ACCOUNT_CODE") %>' ClientInstanceName="txtACCOUNT_CODE" Width="80px">
                                                                            <ValidationSettings SetFocusOnError="true">
                                                                                <RequiredField IsRequired="true" ErrorText="【會計科目】" />
                                                                                <RegularExpression ValidationExpression="\d*"  ErrorText="【會計科目】" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataTextColumn FieldName="RENT_DISCOUNT_ID" Visible="false"   runat="server" >
                                                        <EditItemTemplate>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Templates>
                                                    <TitlePanel>
                                                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                                                            <tr>
                                                                <td align="right">
                                                                    <dx:ASPxButton ID="btnAdd2" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                        AutoPostBack="false"  OnClick="btngvDiscountItemAdd_Click">
                                                                        <%--<ClientSideEvents Click="function(s, e) { gvDiscountItem.AddNewRow(); }" />--%>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <dx:ASPxButton ID="btnDiscounDelete" OnClick="btnDiscounDelete_Click" runat="server" Text="<%$ Resources:WebResources, Delete %>" >
                                                                        <ClientSideEvents Click= "function (s, e) {e.processOnServer = confirm('您確定是要刪除[折扣項目]資料嗎？');}" />
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </TitlePanel>
                                                    <EmptyDataRow>
                                                        <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                                                    </EmptyDataRow>
                                                </Templates>
                                                <SettingsEditing Mode="Inline" />
                                                <Settings ShowTitlePanel="true" />
                                            </cc:ASPxGridView>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, LeaseStock %>">
                            <ContentCollection>
                                <dx:ContentControl ID="TabPanel3" runat="server">
                                    <div class="SubEditBlock">
                                        <div class="GridScrollBar" style="height: auto">
                                            <cc:ASPxGridView ID="gvMobileStock" runat="server" ClientInstanceName="gvMobileStock" Width="100%"
                                                 AutoGenerateColumns="False" EnableCallBacks="False" AccessibilityCompliant="True"  KeyFieldName ="SERIAL_NO" 
                                                 OnPageIndexChanged="gvMobileStock_PageIndexChanged">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" VisibleIndex="0" />
                                                    <dx:GridViewDataColumn FieldName="STORE_NAME" Caption="<%$ Resources:WebResources, StoreName %>" VisibleIndex="1" />
                                                    <dx:GridViewDataColumn FieldName="SERIAL_NO" Caption="<%$ Resources:WebResources, MobileIdentityNumber %>" VisibleIndex="2" />
                                                </Columns>
                                                <Templates>
                                                    <EmptyDataRow>
                                                        <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                                                    </EmptyDataRow>
                                                </Templates>
                                                <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
                                                <SettingsPager PageSize="10"></SettingsPager>
                                                <Settings ShowTitlePanel="True" />
                                                <SettingsEditing EditFormColumnCount="10" Mode="EditFormAndDisplayRow" />
                                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                            </cc:ASPxGridView>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </div>
            
            <div class="seperate"></div>
                        
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td align="right">
                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, SaveData %>" OnClick="btnSave_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnCancel"  SkinID="ResetButton"  runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnDelete" Enabled ="false" SkinID="DeleteBtn"  runat="server" 
                                Text="<%$ Resources:WebResources, Delete %>" onclick="btnDelete_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" CausesValidation="false" />
                            <cc:ASPxPopupControl ID="dataImportPopup" runat="server" AllowDragging="True" AllowResize="True"
                                CloseAction="CloseButton" ContentUrl="~/VSS/LEA/LEA01/LEA01_Import.aspx" PopupHorizontalAlign="Center"
                                PopupVerticalAlign="WindowCenter" ShowFooter="false" Width="950px" Height="600px"
                                FooterText="Try to resize the control using the resize grip or the control's edges"
                                HeaderText="<%$ Resources:WebResources, DataImport %>" EnableHierarchyRecreation="True"
                                PopupElementID="btnImport" LoadingPanelID="lp">
                                <ClientSideEvents CloseUp="function(s, e) {s.SetContentUrl(s.GetContentUrl()); }" />
                        </cc:ASPxPopupControl>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="GridScrollBar" style="height: auto">
                <asp:Panel ID="Panel2" runat="server" Visible="false">
                            <table class="mGrid" width="60%">
                                <tr>
                                    <td align="center">
                                        <!--修改記錄-->
                                        <asp:Literal ID="Literal40" runat="server" Text="修改記錄"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal33" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <!--工號-->
                                        <asp:Literal ID="Literal30" runat="server" Text="工號"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <!--姓名-->
                                        <asp:Literal ID="Literal31" runat="server" Text="姓名"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <!--說明-->
                                        <asp:Literal ID="Literal34" runat="server" Text="說明"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <dx:ASPxLabel ID="libTime" runat="server" Text="2010/09/30">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="center">
                                        <dx:ASPxLabel ID="libEmpNO" runat="server" Text="60736">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="center">
                                        <dx:ASPxLabel ID="libEmpName" runat="server" Text="王小明">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="center">
                                        <dx:ASPxLabel ID="libMemo" runat="server" Text="修改完成">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                </asp:Panel>
            </div>
            <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
            </dx:ASPxLoadingPanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
