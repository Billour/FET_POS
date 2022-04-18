<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON02.aspx.cs" Inherits="VSS_CONS_CON02" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function chName(s, e) {
            //debugger;
            var Qty = s.GetValue();
            var iQty = 0;
            if (s.GetText() == '') {
                e.errorText = '不允許空值，請重新輸入';
                return false;
            }
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
        //日驗證
        function chkSupportStartDate(s, e) {
            e.isValid = true;
            var x = CooperationDateRangeFrom.GetValue();
            var y = CooperationDateRangeTo.GetValue();

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

                var Sx = CooperationDateRangeFrom.GetValue();
                var Sy = CooperationDateRangeTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue > Sx) || (Sy != "" && dvalue > Sy)) {
                    e.isValid = false;
                    alert("[開始日期]不允許大於[結束日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }

        }
        //日驗證
        function chkSupportExpiryDate(s, e) {
            e.isValid = true;
            var x = CooperationDateRangeFrom.GetValue();
            var y = CooperationDateRangeTo.GetValue();

            var dvalue = s.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[結束訖日起值]不允許大於[結束訖日訖值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {
                var Sx = CooperationDateRangeFrom.GetValue();
                var Sy = CooperationDateRangeTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue < Sx) || (Sy != "" && dvalue < Sy)) {
                    e.isValid = false;
                    alert("[結束日期]不允許小於[開始日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }
        
     function checkINVcode(s, e) {
         _gvEventArgs = e;
         _gvSender = s;
         if (s.GetText() != '')
             PageMethods.checkINVcode(_gvSender.GetText(), wmCheck_OnOK);
     }

     function wmCheck_OnOK(returnData) {
         if (returnData == '') {
             _gvEventArgs.processOnServer = false;
             //_gvSender.Focus();
         }
         else if (returnData == 'notInteger') {
         _gvSender.Focus();
          alert("統一編號請輸入數字，請重新輸入!");
         }
         else {
             alert("統一編號請輸入8碼，請重新輸入");
         }
     }
     //檢查商品料號是否存在
     _gvSender = null;
     _gvEventArgs = null;
     function getPRODNAME(s, e) {
         _gvEventArgs = e;
         _gvSender = s;
         if (s.GetText() != '') {
             PageMethods.getPRODNAME(_gvSender.GetText(), getPRODNAME_OnOK);
             PRODNAME.SetValue(null);
             PageMethods.getAccountCode(_gvSender.GetText(), getAccountCode_OnOK);
         }
     }
     function getPRODNAME_OnOK(returnData) {
         if (returnData == '') {
             alert("商品料號不存在!");
             _gvEventArgs.processOnServer = false;
             _gvSender.Focus();
             PRODNAME.SetValue(null);
         }
         else {
             if (returnData == "fail") {
                 alert("商品料號不允許設定!");
                 _gvSender.Focus();
                 _gvSender.SetText("");
             }
             else {
                 PRODNAME.SetValue(returnData);
                 _gvEventArgs.processOnServer = false;
                 _gvSender.Focus();
             }
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

     var SuppNoError = "";
     
     function checkSuppNOcode(s, e) {
         _gvEventArgs = e;
         _gvSender = s;
         if (s.GetText() != '') {
             SuppNoError = "";
             //debugger;
             PageMethods.checkSuppNO(_gvSender.GetText(), checkSuppNO_OnOK);
             
             if (SuppNoError != "")
                 e.errorText = SuppNoError;
         }
     }

     function checkSuppNO_OnOK(returnData) {
         if (returnData == '') {
             _gvEventArgs.processOnServer = false;
             //_gvSender.Focus();
         }
         else {
             _gvSender.Focus();
             //alert("廠商代碼不可重覆，請重新輸入");
             //_gvEventArgs.errorText = "廠商代碼不可重覆，請重新輸入";
             _gvSender.SetText("");
         }
     }

     function Import(s, e) {
         var rtn = confirm('請確認是否要刪除?');
         if (!rtn) {
             e.processOnServer = false;
         }
     }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
  <asp:Panel ID="PnlCont" runat="server"> 
    <div>
        <table width="100%" class="titlef">
            <tr>
                <td align="left" style="width: 79%">
                    <!--外部廠商維護作業(總部)-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, SupplierInformationMaintenanceHQ %>"></asp:Literal>
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxButton ID="importButton" runat="server" Text="<%$ Resources:WebResources, Import %>" CausesValidation="false">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(){document.location='CON01.aspx';return false;}">
                                    </ClientSideEvents>
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <cc:ASPxPopupControl ID="dataImportPopup" runat="server" AllowDragging="True"
         AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/CONS/CON02/CON02_Import.aspx"    
         PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
         Width="950px" Height="600px" 
         HeaderText="<%$ Resources:WebResources, DataImport %>"
         PopupElementID="importButton" LoadingPanelID="lp">    
         <ClientSideEvents CloseUp="function(s, e) {s.SetContentUrl(s.GetContentUrl()); }"  />  
     </cc:ASPxPopupControl>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <asp:HiddenField ID="CreateUser" runat="server" />
        <asp:HiddenField ID="CreateDTM" runat="server" />
    
    <div class="criteria">
        <table border="0" id="tabCont" runat="server">
            <tr>
                <td style="width:13%" align="right">
                    <!--廠商類別-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>">
                    </dx:ASPxLabel>：
                </td>
                <td><div style="width:160px;">                    
                    <dx:ASPxComboBox ID="vendorTypeComboBox" runat="server" OnSelectedIndexChanged="vendorTypeComboBox_SelectedIndexChanged"
                        AutoPostBack="true">
                        <Items>
                            <dx:ListEditItem Text="<%$ Resources:WebResources, DropDownListPrompt %>" Value="" />
                            <dx:ListEditItem Text="寄銷廠商" Value="1" />
                            <dx:ListEditItem Text="外部廠商" Value="2" />                            
                        </Items>
                        <ValidationSettings CausesValidation="false">                                                
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />                                                       
                        </ValidationSettings>                                                
                    </dx:ASPxComboBox>   
                    </div>                                     
                </td>
                <td align="right" style="width:12%">
                    <!--遠傳聯絡窗口-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, FETContact %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                               <uc1:PopupControl ID="popEmployees" runat="server" PopupControlName="EmployeesDepartmentPopup"  />
                            </td>                            
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Department %>"></asp:Literal>：                                
                            </td>
                            <td align="right">
                                <dx:ASPxLabel ID="lblDepartment" runat="server" Width="40px"></dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width:10%">
                    <!--狀態-->
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, Status %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxLabel ID="lblStatus" runat="server" Text="未存檔"></dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <!--廠商編號-->
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxLabel ID="lblSupplierNo" runat="server">
                    </dx:ASPxLabel>
                </td>
                <td align="right">
                    <!--廠商代碼-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:WebResources, SupplierCode %>">
                    </dx:ASPxLabel>：
                </td>
                <td align="left">
                    <div style="width:110px;">
                    <dx:ASPxTextBox ID="txtSupplierCode" MaxLength="2" runat="server" >
                        <ClientSideEvents  Validation="function(s, e){ checkSuppNOcode(s, e); }" />
                        <ValidationSettings CausesValidation="false">                                                
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />                                                       
                        </ValidationSettings>       
                    </dx:ASPxTextBox>
                    </div>
                </td>
                <td>
                    <!--更新日期-->
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxLabel ID="txtUpdateTime" runat="server" Text="2010/07/01 22:00">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <!--廠商名稱-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="<%$ Resources:WebResources, SupplierName %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="3">
                     <div style="width:180px">
                    <dx:ASPxTextBox ID="txtSupplierName" MaxLength="100" Width="180px" runat="server" >
                        <ValidationSettings CausesValidation="false">                                                
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />                                                       
                        </ValidationSettings>       
                    </dx:ASPxTextBox>
                     </div>              
                </td>
                <td>
                    <!--維護人員-->
                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxLabel ID="txtUpdater" runat="server" Text="12345 王大寶"></dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <!--公司地址-->
                    <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="<%$ Resources:WebResources, CompanyAddress %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="3">
                    <dx:ASPxTextBox ID="txtAddress" MaxLength="200" runat="server"></dx:ASPxTextBox>
                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td align="right">
                    <!--聯絡人-->
                    <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="<%$ Resources:WebResources, Contact  %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <div style="width:180px"><dx:ASPxTextBox ID="txtContact" MaxLength="20"  Width="180px"  runat="server">
                    </dx:ASPxTextBox></div>
                </td>
                <td align="right">
                    <!--聯絡電話-->
                    <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="<%$ Resources:WebResources, ContactTelephone  %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtPhone" MaxLength="15" runat="server"></dx:ASPxTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <!--合作起訖日-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, CooperationDateRange %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="2">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="CooperationDateRangeFrom"
                                     ClientInstanceName = "CooperationDateRangeFrom" runat="server">
                                    <ValidationSettings CausesValidation="false">                                                
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />                                                       
                                    </ValidationSettings> 
                                    <ClientSideEvents ValueChanged="function(s, e){ chkSupportStartDate(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="CooperationDateRangeTo"   ClientInstanceName = "CooperationDateRangeTo" runat="server">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkSupportExpiryDate(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    <!--合約號碼-->
                    <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ContractNo %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <div style="width:180px"><dx:ASPxTextBox ID="txtContractNo" MaxLength="20" Width="180px"  runat="server">
                    </dx:ASPxTextBox></div>
                </td>
                <td align="right">
                    <!--結算日-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal15" runat="server" Text="<%$ Resources:WebResources, SettlementDate %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxRadioButtonList ID="cutoffDateRadioButtonList" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="cutoffDateRadioButtonList_SelectedIndexChanged" AutoPostBack="true">
                                    <Items>
                                        <dx:ListEditItem Text="<%$ Resources:WebResources, EndOfDay %>" Value="<%$ Resources:WebResources, EndOfDay %>" />
                                        <dx:ListEditItem Text="" Value="Day" />
                                    </Items>
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxRadioButtonList>
                            </td>
                            <td >
                                <table><tr>
                                <td>
                                <dx:ASPxTextBox ID="cutoffDayTextBox" Width="100px" MaxLength="2" runat="server" Enabled="false">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                </td>
                                <td>  日 </td>
                                </tr></table>                                
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    <!--統一編號-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal17" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>">
                    </dx:ASPxLabel>：
                </td>
                <td nowrap="nowrap">
                    <div style="width:180px"><dx:ASPxTextBox ID="txtUnifiedBusinessNo"  Width="180px"  MaxLength="8" runat="server" >
                        <ClientSideEvents  Validation="function(s, e){checkINVcode(s, e); }" />
                        <ValidationSettings CausesValidation="false">                                                
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />                                                       
                        </ValidationSettings> 
                    </dx:ASPxTextBox></div>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    <!--負責人-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Owner %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <div style="width:180px"><dx:ASPxTextBox ID="txtOwner" MaxLength="20"  Width="180px"  runat="server">
                        <ValidationSettings CausesValidation="false">                                                
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />                                                       
                        </ValidationSettings>
                    </dx:ASPxTextBox></div>
                </td>
                <td align="right">
                    <!--電話號碼-->
                    <dx:ASPxLabel ID="Literal19" runat="server" Text="<%$ Resources:WebResources, Telephone %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtOwnerPhone" MaxLength="15" runat="server"></dx:ASPxTextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    <!--傳真-->
                    <dx:ASPxLabel ID="Literal20" runat="server" Text="<%$ Resources:WebResources, Fax %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <div style="width:180px"><dx:ASPxTextBox ID="txtFax" MaxLength="15"  Width="180px"  runat="server"></dx:ASPxTextBox>
                    </div>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    <!--電子信箱-->
                    <dx:ASPxLabel ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Email %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="3">
                    <div style="width:180px"><dx:ASPxTextBox ID="txtEmail" runat="server" MaxLength="50" Width="100%" 
                        Height="17px"></dx:ASPxTextBox></div>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6"></td>
            </tr>
            <tr>
                <td align="right">
                    <!--最低訂單金額-->
                    <dx:ASPxLabel ID="Literal23" runat="server" Text="<%$ Resources:WebResources, MinimumOrderAmount %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <div style="width:180px">
                    <dx:ASPxTextBox ID="txtMinAmt" MaxLength="8"  Width="180px"  runat="server">
                        <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                        <ValidationSettings>
                                            <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                        </ValidationSettings>
                    </dx:ASPxTextBox></div>
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6"></td>
            </tr>
            <tr>
                <td align="right">
                    <!--會計科目-->
                    <dx:ASPxLabel ID="Literal25" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>">
                    </dx:ASPxLabel>：
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
                                <dx:ASPxTextBox ID="txtAcct1" MaxLength="2" runat="server" Width="40">
                                    <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct2" MaxLength="2" runat="server" Width="40">
                                    <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct3" MaxLength="6" runat="server" Width="50">
                                    <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct4" MaxLength="6" runat="server" Width="50">
                                    <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct5" MaxLength="4" runat="server" Width="40">
                                    <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct6" MaxLength="4" runat="server" Width="40">
                                    <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    <!--備註-->
                    <dx:ASPxLabel ID="Literal32" runat="server" Text="<%$ Resources:WebResources, Remark %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="3" nowrap="nowrap">
                    <dx:ASPxTextBox ID="txtMemo" MaxLength="100" runat="server" TextMode="MultiLine">
                    </dx:ASPxTextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div>
        <div>
            <dx:ASPxPageControl ID="TabContainer1" runat="server" Width="100%" 
                ActiveTabIndex="0">
                <TabPages>
                    <dx:TabPage Name="TabPanel1" Text='<%$ Resources:WebResources, CommissionRateSetting %>'>
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="CSC_ID"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" 
                                        OnRowInserting="gvMaster_RowInserting" 
                                        OnRowUpdating="gvMaster_RowUpdating" 
                                        OnCancelRowEditing="gvMaster_CancelRowEditing" 
                                        OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" 
                                        OnInitNewRow="gvMaster_InitNewRow" 
                                        OnPageIndexChanged="gvMaster_PageIndexChanged" 
                                        OnRowValidating="gvMaster_RowValidating">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="CSC_ID" Visible="false" runat="server" >
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="COMMISSIONRATE" PropertiesTextEdit-DisplayFormatString="{0:N}%"
                                                Caption="<%$ Resources:WebResources, CommissionRate %>" VisibleIndex="2">
                                                <PropertiesTextEdit DisplayFormatString="{0:N}%"></PropertiesTextEdit>
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtCommissionRate" runat="server" Text='<%# Bind("COMMISSIONRATE") %>'  MaxLength="8" ClientInstanceName="txtCommissionRate" Width="80px">
                                                        <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                                        <ValidationSettings>
                                                                            <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>%
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn FieldName="S_DATE" Caption="起始月份(當月的第一天)" VisibleIndex="3">
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="gvMasterS_Date" MinDate='<%# DateTime.Today %>'  runat="server" Text='<%# Bind("S_DATE") %>' ClientInstanceName="gvMasterS_Date" EditFormatString="yyyy/MM" >
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="結束月份(當月的最後一天)" VisibleIndex="4">
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="gvMasterE_Date" runat="server"  MinDate='<%# DateTime.Today %>' Text='<%# Bind("E_DATE") %>' ClientInstanceName="gvMasterE_Date" EditFormatString="yyyy/MM" >
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btngvMasterAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btngvMasterAdd_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btngvMasterDelete" OnClick="btngvMasterDelete_Click" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
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
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel2" Text='<%$ Resources:WebResources, CooperationStoreSettings %>'>
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <table>
                                        <tr>
                                            <td class="tdcen">
                                                <!--未選擇-->
                                                <asp:Literal ID="Literal37" runat="server" Text="<%$ Resources:WebResources, Nonselect %>"></asp:Literal>
                                            </td>
                                            <td class="tdcen">
                                            </td>
                                            <td class="tdcen">
                                                <!--已選擇-->
                                                <asp:Literal ID="Literal38" runat="server" Text="<%$ Resources:WebResources, Selected %>"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdcen">
                                                <dx:ASPxComboBox ID="ddlSubZone" runat="server" Width="80px"
                                                OnSelectedIndexChanged="ddlSubZone_SelectedIndexChanged" AutoPostBack="true">
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
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
            <dx:ASPxPageControl ID="TabContainer2" runat="server" Width="100%" 
                ActiveTabIndex="0">
                <TabPages>
                    <dx:TabPage Name="TabPanel3" Text="<%$ Resources:WebResources, Prorate %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="gvCommission" ClientInstanceName="gvCommission" 
                                        runat="server" KeyFieldName="CSC_ID"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" 
                                        OnRowInserting="gvCommission_RowInserting" 
                                        OnRowUpdating="gvCommission_RowUpdating" AccessibilityCompliant="True" 
                                        OnCancelRowEditing="gvCommission_CancelRowEditing" 
                                        OnPageIndexChanged="gvCommission_PageIndexChanged" 
                                        OnCommandButtonInitialize="gvCommission_CommandButtonInitialize" 
                                        OnInitNewRow="gvCommission_InitNewRow" 
                                        OnRowValidating="gvCommission_RowValidating">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvCommission.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="CSC_ID" Visible="false" runat="server" >
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="COMMISSIONRATE" runat="server" Caption="<%$ Resources:WebResources, CommissionRate %>"
                                                PropertiesTextEdit-DisplayFormatString="{0:N}%">
                                                <PropertiesTextEdit DisplayFormatString="{0:N}%"></PropertiesTextEdit>
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtCommissionRate" runat="server" Text='<%# Bind("COMMISSIONRATE") %>' MaxLength="8"  ClientInstanceName="txtCommissionRate" Width="80px">
                                                        <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                                        <ValidationSettings>
                                                                            <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>%
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn FieldName="S_DATE" runat="server" Caption="<%$ Resources:WebResources, StartMonth %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="gvCommissionS_Date" runat="server"  MinDate='<%# DateTime.Today %>' Text='<%# Bind("S_DATE") %>' ClientInstanceName="gvCommissionS_Date" EditFormatString="yyyy/MM" >
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="E_DATE" runat="server" Caption="<%$ Resources:WebResources, EndMonth %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="gvCommissionE_Date" runat="server"  MinDate='<%# DateTime.Today %>' Text='<%# Bind("E_DATE") %>' ClientInstanceName="gvCommissionE_Date" EditFormatString="yyyy/MM" >
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnGvCommissionAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnGvCommissionAdd_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnGvCommissionDelete" OnClick="btnGvCommissionDelete_Click" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
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
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel4" Text="<%$ Resources:WebResources, Bracket %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="gvAmtLevel" ClientInstanceName="gvAmtLevel" runat="server" KeyFieldName="CSAL_ID"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" 
                                        OnRowInserting="gvAmtLevel_RowInserting" 
                                        OnRowUpdating="gvAmtLevel_RowUpdating" AccessibilityCompliant="True" 
                                        OnPageIndexChanged="gvAmtLevel_PageIndexChanged" 
                                        OnRowValidating="gvAmtLevel_RowValidating" 
                                        OnCommandButtonInitialize="gvAmtLevel_CommandButtonInitialize">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvAmtLevel.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="SEQNO" runat="server"  Caption="<%$ Resources:WebResources, BracketItems %>">
                                                <DataItemTemplate>
                                                    <%#Container.ItemIndex + 1%>
                                                </DataItemTemplate>
                                                <EditItemTemplate>
                                                    &nbsp;</EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="S_AMT" runat="server" Caption="<%$ Resources:WebResources, BracketStart %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtSAMT" runat="server" Text='<%# Bind("S_AMT") %>'  MaxLength="8" ClientInstanceName="txtSAMT" Width="80px">
                                                        <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                                        <ValidationSettings>
                                                                            <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="E_AMT" runat="server" Caption="<%$ Resources:WebResources, BracketEnd %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtEAMT" runat="server" Text='<%# Bind("E_AMT") %>' MaxLength="8" ClientInstanceName="txtEAMT" Width="80px">
                                                        <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                                        <ValidationSettings>
                                                                            <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="COMMISION_RATE" runat="server" Caption="<%$ Resources:WebResources, CommissionRate %>"
                                                PropertiesTextEdit-DisplayFormatString="{0:N}%">
                                                <PropertiesTextEdit DisplayFormatString="{0:N}%"></PropertiesTextEdit>
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtCommissionRate" runat="server" Text='<%# Bind("COMMISION_RATE") %>'  MaxLength="8"  ClientInstanceName="txtCommissionRate" Width="80px">
                                                        <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                                        <ValidationSettings>
                                                                            <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>%
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn FieldName="S_DATE" runat="server" Caption="<%$ Resources:WebResources, StartDate %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="gvAmtLevelS_Date" runat="server"  MinDate='<%# DateTime.Today %>' Text='<%# Bind("S_DATE") %>' ClientInstanceName="gvAmtLevelS_Date" >
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="E_DATE" runat="server" Caption="<%$ Resources:WebResources, EndDate %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="gvAmtLevelE_Date" runat="server"  MinDate='<%# DateTime.Today %>' Text='<%# Bind("E_DATE") %>' ClientInstanceName="gvAmtLevelE_Date" >
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn FieldName="CSAL_ID"  Visible="false" runat="server" >
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="gvAmtAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="gvAmtAdd_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="gvAmtDelete" OnClick="gvAmtDelete_Click" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
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
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel5" Text="<%$ Resources:WebResources, ProductCodeAssignment %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="gvProduct" ClientInstanceName="gvProduct" runat="server" KeyFieldName="PRODNO"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" 
                                        OnRowInserting="gvProduct_RowInserting" 
                                        OnRowUpdating="gvProduct_RowUpdating" 
                                        OnPageIndexChanged="gvProduct_PageIndexChanged" 
                                        OnRowValidating="gvProduct_RowValidating">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvProduct.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                                                 <EditItemTemplate>
                                                    <uc1:PopupControl ID="popPRODNO" runat="server" PopupControlName="ProductsPopup"  Text='<%#BIND("[PRODNO]") %>' 
                                                     SetClientValidationEvent="getPRODNAME"  KeyFieldValue1="consignmentsale"/>
                                                 </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                                                 <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtPRODNAME" Width="200" runat="server" Text='<%# Bind("PRODNAME") %>' ClientInstanceName="PRODNAME" Border-BorderStyle="None" ReadOnly="true"></dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ACCOUNT_CODE" runat="server" Caption="<%$ Resources:WebResources, AccountingSubject %>">
                                                 <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtACCOUNT_CODE" runat="server" Text='<%# Bind("ACCOUNT_CODE") %>' ClientInstanceName="txtACCOUNT_CODE" Width="200px"></dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn FieldName="S_YYMM" runat="server" Caption="<%$ Resources:WebResources, StartMonth %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="gvProductS_YYMM"  MinDate='<%# DateTime.Today %>' runat="server" Text='<%# Bind("S_YYMM") %>' ClientInstanceName="gvProductS_YYMM" EditFormatString="yyyy/MM" >
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="E_YYMM" runat="server" Caption="<%$ Resources:WebResources, EndMonth %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="gvProductE_YYMM" runat="server"  MinDate='<%# DateTime.Today %>' Text='<%# Bind("E_YYMM") %>' ClientInstanceName="gvProductE_YYMM" EditFormatString="yyyy/MM" >
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnGvProductAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnGvProductAdd_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnGvProductDelete" OnClick="btnGvProductDelete_Click" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
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
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel6" Text="<%$ Resources:WebResources, CooperationStoreSettings %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <table>
                                        <tr>
                                            <td class="tdval">
                                                <table>
                                                    <tr>
                                                        <td class="tdcen">
                                                            <!--未選擇-->
                                                            <asp:Literal ID="Literal49" runat="server" Text="<%$ Resources:WebResources, Nonselect %>"></asp:Literal>
                                                        </td>
                                                        <td class="tdcen">
                                                        </td>
                                                        <td class="tdcen">
                                                            <!--已選擇-->
                                                            <asp:Literal ID="Literal50" runat="server" Text="<%$ Resources:WebResources, Selected %>"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcen">
                                                            <dx:ASPxComboBox ID="ddlSubZnLis" runat="server" AutoPostBack="True" 
                                                                ValueType="System.String" Width="80px" 
                                                                OnSelectedIndexChanged="ddlSubZnLis_SelectedIndexChanged">
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td class="tdcen">
                                                        </td>
                                                        <td class="tdcen">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdListBox" rowspan="5">
                                                            <%--<asp:ListBox ID="ZTypeLis" runat="server" Height="327px" SelectionMode="Multiple"
                                                                Width="259px"></asp:ListBox>--%>
                                                                <asp:ListBox runat="server" SelectionMode="Multiple" Height="327px" 
                                                                Width="259px" ID="ZTypeLis"></asp:ListBox>
                                                        </td>
                                                        <td class="tdBtn">
                                                        </td>
                                                        <td rowspan="5" class="tdListBox">
                                                            <%--<asp:ListBox ID="SList" runat="server" Height="327px" SelectionMode="Multiple"
                                                                Width="259px"></asp:ListBox>--%>
                                                            <asp:ListBox runat="server" SelectionMode="Multiple" Height="327px" 
                                                                Width="259px" ID="SList"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/next.png" OnClick="btnAdd_Click2" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                            <asp:ImageButton ID="ImagebtngvMasterDelete" runat="server" ImageUrl="~/Images/previous.png"
                                                                OnClick="btnBack_Click2" />
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
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel7" Text="<%$ Resources:WebResources, CreditCardFees %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="gvCard" ClientInstanceName="gvCard" runat="server" KeyFieldName="ITEMS"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" 
                                        OnRowInserting="gvCard_RowInserting" 
                                        OnRowUpdating="gvCard_RowUpdating" AccessibilityCompliant="True" 
                                        OnRowValidating="gvCard_RowValidating">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvCard.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ITEMS" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                                                VisibleIndex="2">
                                                <DataItemTemplate>
                                                    <%#Container.ItemIndex + 1%>
                                                </DataItemTemplate>
                                                <PropertiesTextEdit>
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None" />
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <CellStyle HorizontalAlign="Left">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="TYPE" runat="server" Caption="<%$ Resources:WebResources, TypeOfCreditCard %>">
                                                <PropertiesComboBox><Items>
                                                    <dx:ListEditItem Text="<%$ Resources:WebResources, DropDownListPrompt %>" Value="<%$ Resources:WebResources, DropDownListPrompt %>" />
                                                    <dx:ListEditItem Text="VISA" Value="VISA" /><dx:ListEditItem Text="MASTER" Value="MASTER" />
                                                    <dx:ListEditItem Text="AE" Value="AE" /><dx:ListEditItem Text="JCB" Value="JCB" /></Items></PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="RATE" runat="server" Caption="<%$ Resources:WebResources, ServiceCharges %>"
                                                PropertiesTextEdit-DisplayFormatString="{0:N}%">
                                                <PropertiesTextEdit DisplayFormatString="{0:N}%"></PropertiesTextEdit>
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="lblFee" runat="server" Text='<%# Bind("RATE") %>' MaxLength="8"  ClientInstanceName="lblFee" Width="80px">
                                                        <ClientSideEvents  Validation="function(s, e){ chName(s, e); }" />
                                                        <ValidationSettings>
                                                                            <RegularExpression ValidationExpression="\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>%
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="gvCardAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="gvCardAdd_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="gvCardDelete" OnClick="gvCardDelete_Click" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
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
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </div>
    </div>
    <div class="seperate"></div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSave" Enabled="false" runat="server" Text="<%$ Resources:WebResources, Save %>" 
                        OnClick="btnSave_Click" EnableClientSideAPI="false" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnDelete" runat="server" Enabled="false" 
                        Text="<%$ Resources:WebResources, Delete %>" onclick="btnDelete_Click" >
                        <ClientSideEvents Click="function(s, e) {
                                            Import(s, e);
                                        }" />
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server"  SkinID="ResetButton" 
                        Text="<%$ Resources:WebResources, Reset %>" onclick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>
<%--    <cc:ASPxPopupControl ID="employeesPopup" SkinID="EmployeesPopup" runat="server" PopupElementID="btnChooseEmp"
        TargetElementID="txtFETOwner" LoadingPanelID="lp">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>--%>
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
    
            </ContentTemplate>    
    </asp:UpdatePanel>
 </asp:Panel>
</asp:Content>
