<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON04.aspx.cs" Inherits="VSS_CONS_CON04" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        //上架日驗證
        function chkSupportStartDate(s, e) {
            e.isValid = true;
            var x = SupportDateRangeFrom.GetValue();
            var y = SupportDateRangeTo.GetValue();

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

                var Sx = SupportDateRangeFrom.GetValue();
                var Sy = SupportDateRangeTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue > Sx) || (Sy != "" && dvalue > Sy)) {
                    e.isValid = false;
                    alert("[開始日期]不允許大於[結束日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }

        }
        //下架日驗證
        function chkSupportExpiryDate(s, e) {

            e.isValid = true;
            var x = SupportDateRangeFrom.GetValue();
            var y = SupportDateRangeTo.GetValue();

            var dvalue = s.GetValue();

            if (x == null) { x = ""; }
            if (y == null) {
                y = "";
                txtCeaseDate.SetEnabled(false);
                txtCeaseDate.SetText(null);
                txtCeaseDate.SetValue(null);
            }
            else {
                txtCeaseDate.SetEnabled(true);
            }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[上下架日期起值]不允許大於[上下架日期訖值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {
                var Sx = SupportDateRangeFrom.GetValue();
                var Sy = SupportDateRangeTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue < Sx) || (Sy != "" && dvalue < Sy)) {
                    e.isValid = false;
                    alert("[結束日期]不允許小於[開始日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }
        function getSupplierId(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getSupplierId(_gvSender.GetText(), getSupplierId_OnOK);
        }
        function getSupplierId_OnOK(returnData) {
            if (returnData == '') {
                alert("廠商編號有誤!");
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
                _gvSender.SetText(null);

            }

            else {
                $('#txtSupplierId').val(returnData);
                PageMethods.getSupplierAccountCode(returnData, getSupplierAccountCode_OnOK);

            }


        }
        function getSupplierAccountCode_OnOK(returnData) {
            if (returnData == '') {
                //alert("查無會計科目!");
                _gvEventArgs.processOnServer = false;

            }
            else {
                var acctcount = returnData.toString();
                var acct1 = acctcount.substr(0, 2).toString();
                var acct2 = acctcount.substr(2, 2).toString();
                var acct3 = acctcount.substr(4, 6).toString();
                var acct4 = acctcount.substr(10, 6).toString();
                var acct5 = acctcount.substr(16, 4).toString();
                var acct6 = acctcount.substr(20, 4).toString();
                txtAcct1.SetValue(acct1);
                txtAcct2.SetValue(acct2);
                txtAcct3.SetValue(acct3);
                txtAcct4.SetValue(acct4);
                txtAcct5.SetValue(acct5);
                txtAcct6.SetValue(acct6);
                getCommissionData();
            }

        }

        function getCommissionData() {
            if ($('input[id*="txtSupplierNo"]').val() != "" && $('input[id*="txtProductCode"]').val() != "")
                ac1.PerformCallback();

        }
        function checkCommissionRepeat() {

//            //debugger;
//            if (typeof (gvMaster) != "undefined") {
//                //取edit row
//                for (var i = 0; i < gvMaster.cpallRowsCount + 1; i++) {
//                    var gvRowName = "ctl00_MainContentPlaceHolder_ac1_ASPxPageControl1_gvMaster_cell" + i;
//                    var commission = getClientInstance('TxtBox', gvRowName + "_3_txtCommissionRate");
//                    var s_date = getClientInstance('TxtBox', gvRowName + "_4_txtSDate");
//                    var e_date = getClientInstance('TxtBox', gvRowName + "_5_txtEDate");
//                    var rowCommission;
//                    rowCommission = commission.GetValue();
//                    var rS_date;
//                    rS_date = new Date(s_date.GetValue() + "/01");

//                    var rowS_date = new Date(rS_date.getFullYear() + "/" + (rS_date.getMonth() + 1) + "/01");
//                    var rE_date;

//                    if (e_date != null) {
//                        rE_date = new Date(e_date.GetValue() + "/28");
//                    }
//                    else {
//                        rE_date = new Date("9999/12/28");
//                    }
//                    var rowE_date = new Date(rE_date.getFullYear() + "/" + (rE_date.getMonth() + 1) + "/28");
//                    alert('rowCommission = ' + rowCommission + ' , rowS_date =' + rowS_date + ' , rowE_date =' + rowE_date);
//                }
//                //與畫面上的label做比對
//                    for (var j = 0; j < gvMaster.cpallRowsCount; j++) {
//                       
//                            var gvRowName2 = "ctl00_MainContentPlaceHolder_ac1_ASPxPageControl1_gvMaster_cell" + j;
//                            var txtCommission = getClientInstance('Label', gvRowName2 + "_3_lblCommissionRate");
//                            var txtSDate = getClientInstance('Label', gvRowName2 + "_4_lblSDate");
//                            var txtEDate = getClientInstance('Label', gvRowName2 + "_5_lblEDate");
//                            var sCommission = txtCommission.innerText;
//                            var sSDate = new Date(txtSDate.innerText + "/01");
//                            var sEDate;
//                            if (txtEDate.innerText != null) {
//                                sEDate = new Date(txtEDate.innerText + "/28");
//                            }
//                            else {
//                                sEDate = new Date("9999/12/31");
//                            }


//                            alert('sCommission = ' + sCommission + ' , sSDate =' + sSDate + ' , sEDate =' + sEDate );
//                            if ((rowS_date >= sSDate && rowS_date <= sEDate)
//                               || (rowE_date >= sSDate && rowE_date <= sEDate)
////                               || (sSDate >= rowS_date && sSDate <= rowE_date)
////                               || (sSDate >= rowS_date && sSDate <= rowE_date)
//                               ) {
//                               var rtn = confirm("你確定要儲存資料嗎?");
//                               return rtn;

//                            

//                        }
//                   
//                }
//            }
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <asp:Panel ID="PanelPage" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品維護作業(總部)-->
                        <asp:Literal ID="Literal110" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductMaintenanceHQ %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                            AutoPostBack="false" CausesValidation="false" ClientSideEvents-Click="function(){document.location='../CON03/CON03.aspx';return false;}" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td align="right">
                        <!--廠商編號-->
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <uc1:PopupControl ID="txtSupplierNo" runat="server" PopupControlName="ConsignmentVendorsPopup"
                            IsValidation="true" KeyFieldValue2="SUPP_NO" />
                        <input type="hidden" id="txtSupplierId" name="txtSupplierId" runat="server" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <!--狀態-->
                        <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Status %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblStatus" runat="server">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--商品編號-->
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtProductCode" runat="server" MaxLength="10" ClientSideEvents-TextChanged="getCommissionData">
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                <RegularExpression ValidationExpression="^[a-zA-Z]{2}\d{7}$" ErrorText="請輸入前二碼英文字後七碼為數字的字串" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <!--日期-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Date %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblDate" runat="server">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--商品名稱-->
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductName %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtProductName" runat="server" MaxLength="20">
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <!--人員-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Staff %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblUser" runat="server">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--商品類別-->
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ddlProductCategory" runat="server">
                            <ValidationSettings SetFocusOnError="true">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--上下架日期-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, SupportDateRange %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="SupportDateRangeFrom" runat="server" ClientInstanceName="SupportDateRangeFrom"
                                        EditFormatString="yyyy/MM/dd">
                                        <ValidationSettings SetFocusOnError="true">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                        <ClientSideEvents ValueChanged="function(s, e){ chkSupportStartDate(s, e); }" />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="SupportDateRangeTo" runat="server" ClientInstanceName="SupportDateRangeTo"
                                        EditFormatString="yyyy/MM/dd">
                                        <ValidationSettings SetFocusOnError="true">
                                        </ValidationSettings>
                                        <ClientSideEvents ValueChanged="function(s, e){ chkSupportExpiryDate(s, e); }" />
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--停止訂購日期-->
                        <dx:ASPxLabel ID="Literal9" runat="server" Text="<%$ Resources:WebResources, OrderEndDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="txtCeaseDate" runat="server" ClientInstanceName="txtCeaseDate"
                            EditFormatString="yyyy/MM/dd" ClientEnabled="false">
                            <ValidationSettings SetFocusOnError="true">
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--會計科目-->
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal25" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="Literal26" runat="server" Text="<%$ Resources:WebResources, Subject1 %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal27" runat="server" Text="<%$ Resources:WebResources, Subject2 %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Subject3 %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal29" runat="server" Text="<%$ Resources:WebResources, Subject4 %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal30" runat="server" Text="<%$ Resources:WebResources, Subject5 %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal31" runat="server" Text="<%$ Resources:WebResources, Subject6 %>">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtAcct1" runat="server" Width="40" MaxLength="2" ClientInstanceName="txtAcct1">
                                        <ValidationSettings SetFocusOnError="true">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            <RegularExpression ValidationExpression="^\d{2}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtAcct2" runat="server" Width="40" MaxLength="2" ClientInstanceName="txtAcct2">
                                        <ValidationSettings SetFocusOnError="true">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            <RegularExpression ValidationExpression="^\d{2}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtAcct3" runat="server" Width="50" MaxLength="6" ClientInstanceName="txtAcct3">
                                        <ValidationSettings SetFocusOnError="true">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            <RegularExpression ValidationExpression="^\d{6}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtAcct4" runat="server" Width="50" MaxLength="6" ClientInstanceName="txtAcct4">
                                        <ValidationSettings SetFocusOnError="true">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            <RegularExpression ValidationExpression="^\d{6}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtAcct5" runat="server" Width="40" MaxLength="4" ClientInstanceName="txtAcct5">
                                        <ValidationSettings SetFocusOnError="true">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            <RegularExpression ValidationExpression="^\d{4}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtAcct6" runat="server" Width="40" MaxLength="4" ClientInstanceName="txtAcct6">
                                        <ValidationSettings SetFocusOnError="true">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            <RegularExpression ValidationExpression="^\d{4}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--單位-->
                        <dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Unit %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtUnit" runat="server" Width="40" MaxLength="2">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <dx:ASPxCallbackPanel ID="ac1" runat="server" ClientInstanceName="ac1" OnCallback="ac1_Callback">
                    <PanelCollection>
                        <dx:PanelContent>
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged"
                                ActiveTabIndex="0" ClientInstanceName="ASPxPageControl1" EnableClientSideAPI="True"
                                AutoPostBack="True">
                                <TabPages>
                                    <dx:TabPage>
                                        <TabTemplate>
                                            <table width="70px">
                                                <tr>
                                                    <td>
                                                        <span style="color: Red">*</span>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, CommissionRate %>">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </TabTemplate>
                                        <ContentCollection>
                                            <dx:ContentControl runat="server">
                                                <div>
                                                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="CPC_ID"
                                                        Width="100%" AutoGenerateColumns="False" OnRowInserting="gvMaster_RowInserting"
                                                        OnRowUpdating="gvMaster_RowUpdating" OnRowValidating="gvMaster_RowValidating"
                                                        OnPageIndexChanged="gvMaster_PageIndexChanged" OnInitNewRow="gvMaster_InitNewRow"
                                                        OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                                                        OnCustomJSProperties="gvMaster_CustomJSProperties">
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                                <HeaderTemplate>
                                                                    <div style="text-align: center">
                                                                        <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                                                    </div>
                                                                </HeaderTemplate>
                                                            </dx:GridViewCommandColumn>
                                                           <%-- <dx:GridViewDataTextColumn Width="30px" VisibleIndex="1">
                                                                <DataItemTemplate>
                                                                    <input type="button" value="編輯" onclick="gvMaster.StartEditRow(<%# Container.VisibleIndex %>);" />
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    
                                                                    <%-- <dx:ASPxButton ID="btnSaveRow" runat="server" Text="儲存">
                                                                    <ClientSideEvents Click="function(s,e){checkCommissionRepeat(s,e);}" />
                                                                    </dx:ASPxButton>
                                                                    <input type="button" value="儲存" onclick=" if(checkCommissionRepeat())
                                                                    {gvMaster.CancelEdit();} " />
                                                                    <br />
                                                                    <input type="button" value="取消" onclick=" gvMaster.CancelEdit(); " />
                                                                    
                                                                </EditItemTemplate>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <EditCellStyle HorizontalAlign="Center">
                                                                </EditCellStyle>
                                                            </dx:GridViewDataTextColumn>--%>
                                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                                <EditButton Visible="True">
                                                                </EditButton>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="CPC_ID" Visible="false">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="COMMISSION" Caption="<%$ Resources:WebResources, CommissionRate %>"
                                                                VisibleIndex="2">
                                                                <PropertiesTextEdit DisplayFormatString="{0:N}%">
                                                                </PropertiesTextEdit>
                                                                <DataItemTemplate>
                                                                    
                                                                    <dx:ASPxLabel ID="lblCommissionRate" runat="server" Text='<%# Bind("COMMISSION") %>'>
                                                                    </dx:ASPxLabel>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxTextBox ID="txtCommissionRate" Width="100px" runat="server" Text='<%# Bind("COMMISSION") %>'
                                                                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                                                                    <ValidationSettings SetFocusOnError="true">
                                                                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                                                                        <RegularExpression ValidationExpression="^\d{0,2}$" ErrorText="格式錯誤" />
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td>
                                                                                %
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartMonthOfTheFirstDay %>"
                                                                VisibleIndex="3">
                                                                <PropertiesTextEdit DisplayFormatString="yyyy/MM">
                                                                </PropertiesTextEdit>
                                                                <DataItemTemplate>
                                                                    <dx:ASPxLabel ID="lblSDate" runat="server" Text='<%# Bind("S_DATE") %>'>
                                                                    </dx:ASPxLabel>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <dx:ASPxDateEdit ID="txtSDate" runat="server" EditFormatString="yyyy/MM" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                                        Text='<%# Bind("S_DATE") %>' MinDate='<%# DateTime.Today.AddDays(1) %>'>
                                                                        <ValidationSettings SetFocusOnError="True">
                                                                            <RequiredField IsRequired="True" ErrorText="必填欄位"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxDateEdit>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="結束月份" VisibleIndex="4">
                                                                <PropertiesTextEdit DisplayFormatString="yyyy/MM">
                                                                </PropertiesTextEdit>
                                                                <DataItemTemplate>
                                                                    <dx:ASPxLabel ID="lblEDate" runat="server" Text='<%# Bind("E_DATE") %>'>
                                                                    </dx:ASPxLabel>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <dx:ASPxDateEdit ID="txtEDate" runat="server" EditFormatString="yyyy/MM" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                                        Text='<%# Bind("E_DATE") %>' MinDate='<%# DateTime.Today.AddDays(1) %>'>
                                                                    </dx:ASPxDateEdit>
                                                                    <dx:ASPxTextBox ID="txtCpcId" Width="50px" runat="server" Text='<%# Bind("CPC_ID") %>'
                                                                        ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' ForeColor="White"
                                                                        ClientVisible="false">
                                                                    </dx:ASPxTextBox>
                                                                      
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <Templates>
                                                            <TitlePanel>
                                                                <table align="left">
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                                OnClick="btnAddRow_Click" CausesValidation="false" />
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxButton SkinID="DeleteBtn" ID="btnDeleteRow" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                                                OnClick="btnDeleteRow_Click" CausesValidation="false" ClientInstanceName="btnDeleteRow" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </TitlePanel>
                                                        </Templates>
                                                        <SettingsPager PageSize="5" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <Settings ShowTitlePanel="True" />
                                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                                                    </cc:ASPxGridView>
                                                    <dx:ASPxButton ID="btnSave1" ClientInstanceName="btnSave1" ClientVisible="false"
                                                        runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnSave1_Click">
                                                    </dx:ASPxButton>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="<%$ Resources:WebResources, ProductAmount %>">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server">
                                                <div>
                                                    <cc:ASPxGridView ID="GridView1" ClientInstanceName="GridView1" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" EnableRowsCache="False" IsClearStatus="True">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                                                VisibleIndex="0">
                                                                <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd">
                                                                </PropertiesTextEdit>
                                                                <DataItemTemplate>
                                                                    <dx:ASPxLabel ID="lblModiDtm" Width="100px" runat="server" Text='<%# Bind("MODI_DTM") %>'
                                                                        ClientInstanceName="lblModiDtm">
                                                                    </dx:ASPxLabel>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, EffectiveDate %>"
                                                                VisibleIndex="1">
                                                                <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd">
                                                                </PropertiesTextEdit>
                                                                <DataItemTemplate>
                                                                    <dx:ASPxLabel ID="lblSdate" Width="100px" runat="server" Text='<%# Bind("S_DATE") %>'
                                                                        ClientInstanceName="lblSdate">
                                                                    </dx:ASPxLabel>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, ExpiryDate %>"
                                                                VisibleIndex="2">
                                                                <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd">
                                                                </PropertiesTextEdit>
                                                                <DataItemTemplate>
                                                                    <dx:ASPxLabel ID="lblEdate" Width="100px" runat="server" Text='<%# Bind("E_DATE") %>'
                                                                        ClientInstanceName="lblEdate">
                                                                    </dx:ASPxLabel>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="PRICE" Caption="<%$ Resources:WebResources, ProductAmount %>"
                                                                VisibleIndex="3" />
                                                        </Columns>
                                                        <SettingsPager PageSize="5" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                    </cc:ASPxGridView>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                            OnClick="btnSave_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="importButton" runat="server" Text="<%$ Resources:WebResources, Import %>"
                            AutoPostBack="false" CausesValidation="false">
                        </dx:ASPxButton>
                        <cc:ASPxPopupControl ID="dataImportPopup" runat="server" AllowDragging="True" AllowResize="True"
                            CloseAction="CloseButton" ContentUrl="~/VSS/CONS/CON04/CON04_1.aspx" PopupHorizontalAlign="Center"
                            PopupVerticalAlign="WindowCenter" ShowFooter="false" Width="950px" Height="600px"
                            FooterText="Try to resize the control using the resize grip or the control's edges"
                            HeaderText="<%$ Resources:WebResources, DataImport %>" EnableHierarchyRecreation="True"
                            PopupElementID="importButton" LoadingPanelID="lp">
                            <ClientSideEvents CloseUp="function(s, e) {s.SetContentUrl(s.GetContentUrl()); }" />
                        </cc:ASPxPopupControl>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReset" SkinID="ResetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
        </dx:ASPxLoadingPanel>
    </asp:Panel>
</asp:Content>
