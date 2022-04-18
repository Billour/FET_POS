<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SAL11.aspx.cs" Inherits="VSS_SAL_SA11" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/jquery.checkboxes.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">       
        var etcAmt = 0;

        function CheckgvMaster(s, e) {         
            if (typeof(gvMaster) != "undefined") {
                for (var i = 0; i < gvMaster.pageRowCount; i++) {
                    var gvRowName = "ctl00_MainContentPlaceHolder_gvMaster_cell" + i;
                    var divIMEI = window.document.all[gvRowName + "_9_divIMEI"]; 
                    
                    if (divIMEI.style.visibility != "hidden") {
                        var lblIMEI_QTY = getClientInstance('TxtBox', gvRowName + "_9_lbIMEI_QTY");
                        var QUANTITY = getClientInstance('TxtBox', gvRowName + "_5_txtQUANTITY");
                        if (lblIMEI_QTY.IsVisible()) {
                            if (lblIMEI_QTY.GetText() != QUANTITY.GetText()) {   //檢查商品數量及IMEI數量一不一致
                                alert("請確認IMEI數量!!");
                                e.processOnServer = false;
                                return false;
                            }
                        }
                    }
                    
                    var txtPRODNO = getClientInstance('TxtBox', gvRowName + "_3_txtPRODNO_txtControl");
                    if (txtPRODNO.GetText() == "SIM") {
                        alert("請選取實際的SIM卡商品料號!!");
                        e.processOnServer = false;
                        return false;
                    }
                    
                    var prodName = getClientInstance('TxtBox', gvRowName + "_4_txtPRODNAME");
                    if (prodName.GetText() == "") {
                        alert("無商品名稱無法結帳!!");
                        e.processOnServer = false;
                        return false;
                    }

                    var unitPrice = getClientInstance('TxtBox', gvRowName + "_6_txtUNIT_PRICE");
                    if (unitPrice.GetText() == "" || isNaN(unitPrice.GetText())) {
                        alert("無單價無法結帳!!");
                        e.processOnServer = false;
                        return false;
                    }
                }
            }
            var txtUNI_TITLE = window.document.getElementById("ctl00_MainContentPlaceHolder_txtUNI_TITLE_I").value;
            var txtUNI_NO = window.document.getElementById("ctl00_MainContentPlaceHolder_txtUNI_NO_I").value;
            if (txtUNI_TITLE != "" && txtUNI_NO == "") {
                alert("統一編號不可為空值，請重新輸入。");
                e.processOnServer = false;
                return false;
            }
            var vINVType = window.document.getElementById("ctl00_MainContentPlaceHolder_rbINVOICE_TYPE_VI").value;
            if (vINVType == 1 && txtUNI_NO == "") {
                alert("發票格式為三聯式時，統一編號不可為空值，請重新輸入。");
                e.processOnServer = false;
                return false;
            }
            
            if (!checkInv()) {
                e.processOnServer = false;
                return false;
            }
            return true;
        }

        SelectedCount = 0;
        PageSelectCount = 0;
        strIDs = "";
        function DeletePaidItem(s, e) {
            hdDeleteIDs.SetText(null);
            this.s = s;
            this.e = e;
            if (!confirm('您確定要刪除所勾選的交易資料嗎？')) {
                e.processOnServer = false;
            }
            else {
                e.processOnServer = false;  //在下列call OnGetRowValues中執行刪除支付項目
                SelectedCount = 0;
                strIDs = "";
                
                for (var i = 0; i < gvCheckOut.pageRowCount; i++) {
                    var checked = gvCheckOut.IsRowSelectedOnPage(i);
                    if (checked) {

                        PageSelectCount++;
                        gvCheckOut.GetRowValues(i, 'ID;PAID_MODE;CREDIT_CARD_NO;HG_CARD_NO;PAID_AMOUNT;HG_REDEEM_POINT;HG_LEFT_POINT', OnGetRowValues);
                    }
                }
                
            }
        }

        //若選取刪除項目中，付費模式(PAID_MODE)含有 信用卡支付(2) 或 HappyGo支付(7)，則需先進行刷退的動作。
        function OnGetRowValues(values) {
            //[0] = ID
            //[1] = PAID_MODE       支付方式
            //[2] = CREDIT_CARD_NO  信用卡卡號
            //[3] = HG_CARD_NO      HG卡號
            //[4] = PAID_AMOUNT     支付金額 / 兌點金額
            //[5] = HG_REDEEM_POINT 兌換點數
            //[6] = HG_LEFT_POINT   剩餘點數

            var PAID_MODE = values[1];
            SelectedCount += 1;
           
            strIDs += "'" + values[0] + "',";
            DeleteItem();
        }
        
        function DeleteItem() {

            if (SelectedCount == PageSelectCount) {
                if (strIDs.length > 0) {
                    strIDs = strIDs.substring(0, strIDs.length - 1);
                    PageSelectCount = 0;
                }

                hdDeleteIDs.SetText(strIDs);
                s.SendPostBack('Click'); //開始刪除支付項目

            }
                     
        }

        function chkGetInfoComplete() {
            if (window.document.getElementById("ctl00_MainContentPlaceHolder_hidGetProdInfoComplete").value == "false") {
                alert('尚未取得商品完整資訊，請稍候!');
                return false;
            } else 
                return true;
        }
        
        function checkInv() {
            var vINVNO = window.document.getElementById("ctl00_MainContentPlaceHolder_lbINVOICE_NO_I").value;
            var vINVType = window.document.getElementById("ctl00_MainContentPlaceHolder_rbINVOICE_TYPE_VI").value;
            
            if (vINVType == 0 || vINVType == 1) {
                if (vINVNO == "") {
                    alert('發票格式為二聯式或三聯式時，發票號碼不可為空白');
                    return false;
                } else {
                    return true;
                }
            } else {
                return true;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip"></div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                        <tr>
                            <td align="left">
                                <!--銷售補登作業-->
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ManualSelling %>"></asp:Literal>
                            </td>
                            <td align="right">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, UnclearedTradeListing %>"
                                                UseSubmitBehavior="false" AutoPostBack="false" OnClick="ASPxButton2_Click">
                                               <ClientSideEvents Click="function(s, e){chkInvNo(s,e);}" />
                                            </dx:ASPxButton>
                                            <script type="text/javascript">
                                                function chkInvNo(s, e) //
                                                {
                                                    if (!checkInv()) {
                                                        e.processOnServer=false;
                                                        return false;
                                                    }
                                                    e.processOnServer = true;
                                                }
                                            </script>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt">
                                <!--交易序號-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:Label ID="lbSALE_NO" runat="server" />
                            </td>
                            <td class="tdtxt">
                                <!--單據類別-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SalesSlipType %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <!--發票/收據-->
                                <dx:ASPxTextBox ID="txtVOUCHER_TYPE" runat="server" Text="" ClientInstanceName="txtVOUCHER_TYPE" 
                                    Border-BorderStyle="None" ReadOnly="true" Width="120">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                                <!--狀態-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxLabel ID="lbSALE_STATUS" runat="server" Text="">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--交易日期-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxLabel ID="lbTran_Date" ClientInstanceName="TRAN_DATE" runat="server" Text=""></dx:ASPxLabel>
                            </td>
                            <td>
                                <!--發票日期-->
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, InvoiceDate %>"></asp:Literal>：
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtInv_Date" ClientInstanceName="txtInv_Date" runat="server">
                                    <ValidationSettings CausesValidation="false" ErrorText="請輸入日期">
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td class="tdtxt">
                                <!--更新日期-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxLabel ID="lbMODI_DTM" runat="server" Text=""></dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--發票號碼-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="lbINVOICE_NO" ClientInstanceName="lbINVOICE_NO" runat="server" Width="120" MaxLength="10" CssClass="tbWidthFormat">
                                    <ClientSideEvents TextChanged="function(s,e){checkINV_NO(s,e.htmlEvent);}" KeyPress="function(s,e){checkINV_NO(s, e.htmlEvent);}" />
                                    <ValidationSettings CausesValidation="false" ErrorText="">
                                        <RegularExpression ValidationExpression="^[A-Z]{2}\d{8}" ErrorText="發票格式錯誤" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <script type="text/javascript">
                                    function checkINV_NO(s, e) {
                                        if (e == null || e.keyCode == 13) {
                                            if (!checkInv())
                                                s.Focus();
                                        }
                                    }
                                </script>
                            </td>
                            </td>
                            <td class="tdtxt">
                                <!--發票格式-->
                                <span style="color: Red">*</span>
                                <asp:Literal ID="Literal33" runat="server" Text="發票格式"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxRadioButtonList ID="rbINVOICE_TYPE" runat="server" SelectedIndex="0"
                                    RepeatDirection="Horizontal" Border-BorderStyle="None" Width="200px">
                                    <Items>
                                        <dx:ListEditItem Text="二聯式" Value="03" />
                                        <dx:ListEditItem Text="三聯式" Value="04" />
                                        <dx:ListEditItem Text="無" Value="0" />
                                    </Items>
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" ErrorText=" " />
                                    </ValidationSettings>
                                    <ClientSideEvents ValueChanged="function(s,e){chgINVType(s,e);}" />
                                </dx:ASPxRadioButtonList>
                                <script type="text/javascript">
                                    function chgINVType(s, e) {
                                        lbINVOICE_NO.SetEnabled(true);
                                        txtUNI_TITLE.SetEnabled(true);
                                        txtUNI_NO.SetEnabled(true);
                                        txtInv_Date.SetEnabled(true);
                                        if (s.GetValue() == "03") 
                                            txtVOUCHER_TYPE.SetText("二聯式發票");
                                        else if (s.GetValue() == "04") 
                                            txtVOUCHER_TYPE.SetText("三聯式發票");
                                        else {
                                            txtVOUCHER_TYPE.SetText("收據");
                                            lbINVOICE_NO.SetEnabled(false);
                                            txtUNI_TITLE.SetEnabled(false);
                                            txtUNI_NO.SetEnabled(false);
                                            txtInv_Date.SetEnabled(false);
                                        }
                                        checkInv();
                                    }
                                </script>
                            </td>
                            <td class="tdtxt">
                                <!--更新人員-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxLabel ID="lbMODI_USER" runat="server" Text="">
                                </dx:ASPxLabel>
                                <%--
                        <asp:Label ID="Label6" runat="server" Text="64591 李家駿"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--發票抬頭-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="txtUNI_TITLE" ClientInstanceName="txtUNI_TITLE" runat="server" CssClass="tbWidthFormat">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                                <!--統一編號-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <script type="text/javascript">
                                    function checkUNI_NO(s, e) {
                                        if (e == null || e.keyCode == 13) {
                                            var vID = s.GetText();

                                            if (!vID.Uni_NoCheck()) {
                                                var r = openDialogWindowByEncrypt("../SAL01/SAL01_checkIDNumber.aspx?date=" + Date() + "&INVOICE_NO=" + txtUNI_NO.GetText(), 400, 200);
                                                if (r == null || r == 'undefined') {
                                                    txtUNI_NO.SetText(null);
                                                    txtUNI_NO.Focus();
                                                }
                                                else {
                                                    rt = r.split(";");
                                                    txtUNI_NO.SetText(rt[0]);
                                                    if (rt[1] != null) txtREMARK.SetText(rt[1]);
                                                }
                                            }
                                        }
                                    }
                                </script>

                                <dx:ASPxTextBox ID="txtUNI_NO" runat="server" CssClass="tbWidthFormat" ClientInstanceName="txtUNI_NO"
                                    AutoPostBack="false">
                                    <ClientSideEvents TextChanged="function(s,e){checkUNI_NO(s,e.htmlEvent);}" KeyPress="function(s,e){checkUNI_NO(s, e.htmlEvent);}" />
                                    <ValidationSettings CausesValidation="false" ErrorText="">
                                        <RegularExpression ValidationExpression="^\d*" ErrorText="請輸入數字" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                                &nbsp;
                            </td>
                            <td class="tdval">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                
                            </td>
                            <td class="tdval">
                                
                            </td>
                            <td class="tdtxt">
                                <!--備註-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                            </td>
                            <td class="tdval" colspan="3">
                                <dx:ASPxTextBox ID="txtREMARK" runat="server" ClientInstanceName="txtREMARK" CssClass="tbWidthFormat">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="seperate">
                    <asp:HiddenField ID="hidETCProdNo" runat="server" />
                    <asp:HiddenField ID="hidAddProdList" runat="server" />
                    <asp:HiddenField ID="hidGetProdInfoComplete" runat="server" />
                    <dx:ASPxTextBox ID="hdPaidInfo" ClientInstanceName="hdPaidInfo" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidPromotion_Code" ClientInstanceName="hidPromotion_Code" runat="server" ClientVisible="false">
                    </dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidPromotion_Name" ClientInstanceName="hidPromotion_Name" runat="server" ClientVisible="false">
                    </dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidPosuuid_Detail" ClientInstanceName="hidPosuuid_Detail" runat="server" ClientVisible="false">
                    </dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidSOURCE_TYPE" ClientInstanceName="hidSOURCE_TYPE" runat="server" ClientVisible="false">
                    </dx:ASPxTextBox>
                </div>
                <div class="SubEditBlock">

                    <script type="text/javascript">
                        /// <summary>
                        /// 變更原本IMEI URL路徑中的商品料號,數量及IMEI_FLAG
                        /// **2011/04/27 Tina：將URL傳遞的參數加密。
                        /// </summary>
                        /// <param name="imeiPop">IMEI的Popup Control</param>
                        /// <param name="UUID">此筆資料的UUID</param>
                        /// <param name="PRODNO">商品料號</param>
                        /// <param name="QTY">數量</param>
                        /// <param name="IMEI_FLAG">IMEI Flag</param>
                        /// <returns>新的URL路徑</returns>
                        function setPopupContentUrl_SAL11(imeiPop, UUID, PRODNO, QTY, IMEI_FLAG) {
                            
                            var url = imeiPop.GetContentUrl();
                            var sValue = url.split('?');
                            if (sValue.length > 1) {
                                var t = new Date().getTime();
                                url = sValue[0] + "?SysDate=" + t + "&KeyFieldValue1=SALE_IMEI_LOG;" + UUID + ";" + PRODNO + ";" + QTY + ";" + IMEI_FLAG;
                            }

                            ModifyPopupURLByEncrypt(url, imeiPop);
                        }
                    </script>
                    
                    <script type="text/javascript">
                    
                        IMET_FLAG ="1";
                        
                        function getPRODINFO(s, e) {
                            window.document.getElementById("ctl00_MainContentPlaceHolder_hidGetProdInfoComplete").value = "false";
                            this.Sender = s;
                            var store_no = window.document.getElementById("ctl00_MainContentPlaceHolder_hidStore_No").value;
                            if (s.GetText() != '')
                                PageMethods.getPRODINFO(Sender.GetText(), store_no, getPRODINFO_OnOK);
                        }

                        function getPRODINFO_OnOK(returnData) {

                            var fName = "3_txtPRODNO_txtControl";
                            var txtPRODNO = getClientInstance('TxtBox', Sender.name);
                            var hdOldPRODNO = getClientInstance('TxtBox', Sender.name.replace(fName, "3_hdOldPRODNO"));
                            var hdIMEIFlag = getClientInstance('TxtBox', Sender.name.replace(fName, "3_hdIMEIFlag"));
                            var txtPRODNAME = getClientInstance('TxtBox', Sender.name.replace(fName, "4_txtPRODNAME"));
                            var txtQUANTITY = getClientInstance('TxtBox', Sender.name.replace(fName, "5_txtQUANTITY"));   //單價
                            var hidISSTOCK = getClientInstance('TxtBox', Sender.name.replace(fName, "5_hidISSTOCK"));
                            var hidON_HAND_QTY = getClientInstance('TxtBox', Sender.name.replace(fName, "5_hidON_HAND_QTY"));
                            var hidIS_OPEN_PRICE = getClientInstance('TxtBox', Sender.name.replace(fName, "6_hidIS_OPEN_PRICE"));
                            var txtUNIT_PRICE = getClientInstance('TxtBox', Sender.name.replace(fName, "6_txtUNIT_PRICE"));
                            var txtTOTAL_AMOUNT = getClientInstance('TxtBox', Sender.name.replace(fName, "7_txtTOTAL_AMOUNT"));
                            var imgIMEI = getClientInstance('Image', Sender.name.replace(fName, "8_imgIMEI"));
                            var txtInputIMEIData = getClientInstance('TxtBox', Sender.name.replace(fName, "9_InputIMEIData_txtControl"));
                            var btnInputIMEIData = getClientInstance('Button', Sender.name.replace(fName, "9_InputIMEIData_btnControl"));
                            var imeiPop = getClientInstance('Popup', Sender.name.replace(fName, "9_InputIMEIData_ASPxPopupControl1"));
                            var lblIMEI_QTY = getClientInstance('TxtBox', Sender.name.replace(fName, "9_lbIMEI_QTY"));
                            var UUID = getClientInstance('TxtBox', Sender.name.replace(fName, "9_hd_ID"));

                            ETCProdNo = window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCProdNo").value;                          
                            
                            if (returnData == '') {
                                txtPRODNO.SetText("");
                                txtPRODNAME.SetText("");
                                alert("無效的商品料號或商品料號不存在!");
                                Sender.Focus();
                            }
                            else {
                                if (txtPRODNO.GetText() == ETCProdNo) {
                                    //商品為ETC Card 加值料號
                                    if (ETCProdNoExist == true) {
                                        alert("ETC加值料號已經存在，不允許重複新增ETC加值料號商品!");
                                        Sender.Focus();
                                        return false;
                                    } else {
                                        ETCProdNoExist = true;
                                    }
                                }
                                
                                var addProdList = window.document.getElementById("ctl00_MainContentPlaceHolder_hidAddProdList").value;
                                if (addProdList == "") 
                                    addProdList = txtPRODNO.GetText();
                                else 
                                    addProdList += "^" + txtPRODNO.GetText();
                                window.document.getElementById("ctl00_MainContentPlaceHolder_hidAddProdList").value = addProdList;
                                
                                p = returnData.split(';');
                                txtPRODNAME.SetText(p[0]);
                                IMET_FLAG = p[1];
                                //IMET_FLAG 為 "1" 時, 不需要IMEI, 所以要隱藏
                                if (txtPRODNO.GetText() != hdOldPRODNO.GetText() && IMET_FLAG != "1") {
                                    lblIMEI_QTY.SetText("0");
                                    txtInputIMEIData.SetText(null);
                                }
                                hdOldPRODNO.SetText(txtPRODNO.GetText());
                                hdIMEIFlag.SetText(IMET_FLAG);

                                var qty = parseInt(Number(txtQUANTITY.GetText()));
                                var Price = parseInt(Number(p[2]));
                                txtUNIT_PRICE.SetText(Price);
                                txtTOTAL_AMOUNT.SetText((qty * Price).toString());
                                
                                if (txtPRODNO.GetText() == ETCProdNo) {
                                    etcAmt = qty * Price;
                                }

                                hidISSTOCK.SetText(p[3]);
                                hidON_HAND_QTY.SetText(p[4]);
                                var onHandQty = 0;
                                if (p[4] != null && p[4] != "" && !isNaN(p[4]))
                                    onHandQty = parseInt(Number(p[4]));
                                if (p[3] == "1" && onHandQty <= 0) {
                                    txtPRODNO.SetText("");
                                    txtPRODNAME.SetText("");
                                    txtQUANTITY.SetText("");
                                    txtUNIT_PRICE.SetText("");
                                    txtTOTAL_AMOUNT.SetText("");
                                    alert('商品庫存量不足!');
                                }
                                hidIS_OPEN_PRICE.SetText(p[5]);

                                ASPxCallback1.SendCallback(Sender.name + ';' +
                                                           Sender.GetText() + ';' +
                                                           txtQUANTITY.GetText() + ';' +
                                                           txtUNIT_PRICE.GetText() + ';' +
                                                           txtTOTAL_AMOUNT.GetText() + ';' + 
                                                           hidIS_OPEN_PRICE.GetText()
                                                          );

                                EnableIMEI(fName, Sender.name);
                                //imeiPop.SetContentUrl(setContentUrl_IMEI(imeiPop.GetContentUrl(), Sender.GetText(), txtQUANTITY.GetText(), IMET_FLAG));

                                //**2011/04/27 Tina：將URL傳遞的參數加密。
                                setPopupContentUrl_SAL11(imeiPop, UUID.GetText(), Sender.GetText(), txtQUANTITY.GetText(), IMET_FLAG);

                                var txtOpenPrice = getClientInstance('TxtBox', Sender.name.replace(fName, "6_txtOpenPrice"));
                                if (p[5] == "Y") {  //商品料號為POS自訂價格商品
                                    txtUNIT_PRICE.SetVisible(false);
                                    txtOpenPrice.SetText(Price);
                                    txtOpenPrice.SetVisible(true);
                                } else {
                                    txtUNIT_PRICE.SetVisible(true);
                                    txtOpenPrice.SetVisible(false);
                                }
                            }
                            window.document.getElementById("ctl00_MainContentPlaceHolder_hidGetProdInfoComplete").value = "true";
                        }

                        //是否可編輯IMEI
                        function EnableIMEI(object, ControlName) {

                            var fName = object;
                            var strProductName = getClientInstance('TxtBox', Sender.name.replace(fName, "4_txtPRODNAME"));
                            var QUANTITY = getClientInstance('TxtBox', ControlName.replace(fName, "5_txtQUANTITY"));
                            var imeiPop = getClientInstance('Popup', ControlName.replace(fName, "9_InputIMEIData_ASPxPopupControl1"));
                            var txtInputIMEIData = getClientInstance('TxtBox', ControlName.replace(fName, "9_InputIMEIData_txtControl"));
                            var btnInputIMEIData = getClientInstance('Button', ControlName.replace(fName, "9_InputIMEIData_btnControl"));
                            var lblIMEI_QTY = getClientInstance('TxtBox', ControlName.replace(fName, "9_lbIMEI_QTY"));
                            var imgIMEI = getClientInstance('Image', ControlName.replace(fName, "8_imgIMEI"));
                            var divIMEI = window.document.all[ControlName.replace(fName, "9_divIMEI")];
                            
                            var iQty = Number(QUANTITY.GetText());
                            
                            //商品料號 && 數量 皆有值，才可編輯IMEI (IMET_FLAG = 1 也不設IMEI)
                            if (IMET_FLAG != "1" && strProductName.GetText() != '' && !isNaN(iQty) && QUANTITY.GetText() != '') {
                                divIMEI.style.visibility = "visible";
                                txtInputIMEIData.SetVisible(true);
                                btnInputIMEIData.SetVisible(true);
                                lblIMEI_QTY.SetVisible(true); 
                                txtInputIMEIData.SetEnabled(true);
                                btnInputIMEIData.SetEnabled(true);
                                imeiPop.SetPopupElementID(btnInputIMEIData.name);
                                imgIMEI.SetVisible(true);
                                if (QUANTITY.GetText() == lblIMEI_QTY.GetText()) {
                                    imgIMEI.SetImageUrl("../../../Icon/check.png");
                                    imgIMEI.SetSize(16, 16);
                                }
                                else {
                                    imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                                    imgIMEI.SetSize(27, 16);
                                }
                            }
                            else {
                                if (IMET_FLAG == "1") {
                                    
                                    divIMEI.style.visibility = "hidden";
                                    txtInputIMEIData.SetVisible(false);
                                    btnInputIMEIData.SetVisible(false);
                                    lblIMEI_QTY.SetVisible(false); 
                                }
                                else {
                                    divIMEI.style.visibility = "visible";
                                    txtInputIMEIData.SetEnabled(false);
                                    btnInputIMEIData.SetEnabled(false);
                                }
                                imeiPop.SetPopupElementID(null);
                                imgIMEI.SetVisible(false);
                            }

                        }

                        function checkSaleQty(s, e) {
                            var fName = "5_txtQUANTITY";
                            var PRODNO = getClientInstance('TxtBox', s.name.replace(fName, "3_txtPRODNO_txtControl"));
                            var hdIMEIFlag = getClientInstance('TxtBox', s.name.replace(fName, "3_hdIMEIFlag"));
                            var hidISSTOCK = getClientInstance('TxtBox', s.name.replace(fName, "5_hidISSTOCK"));
                            var hidON_HAND_QTY = getClientInstance('TxtBox', s.name.replace(fName, "5_hidON_HAND_QTY"));
                            var hidIS_OPEN_PRICE = getClientInstance('TxtBox', s.name.replace(fName, "6_hidIS_OPEN_PRICE"));
                            var txtUNIT_PRICE = getClientInstance('TxtBox', s.name.replace(fName, "6_txtUNIT_PRICE"));
                            var txtTOTAL_AMOUNT = getClientInstance('TxtBox', s.name.replace(fName, "7_txtTOTAL_AMOUNT"));
                            var imeiPop = getClientInstance('Popup', s.name.replace(fName, "9_InputIMEIData_ASPxPopupControl1"));
                            var UUID = getClientInstance('TxtBox', s.name.replace(fName, "9_hd_ID"));
                            
                            if (isNaN(s.GetText())) {
                                s.SetText("1");
                                alert('請輸入數字!');
                                e.processOnServer = false;
                                s.Focus();
                            } else if (s.GetText() == '' || Number(s.GetText()) <= 0 || (!isInteger(s.GetText()))) {
                                s.SetText("1");
                                alert('數量只能為正整數!');
                                e.processOnServer = false;
                                s.Focus();
                            } else if (hidISSTOCK.GetText() == '1' && (!isNaN(hidON_HAND_QTY.GetText())) && 
                                        Number(s.GetText()) > Number(hidON_HAND_QTY.GetText())) {
                                s.SetText("1");
                                alert('庫存量不足!');
                                e.processOnServer = false;
                                s.Focus();
                            } else {
                                EnableIMEI(fName, s.name);
                                
                                var qty = parseInt(Number(s.GetText()));
                                var Price = parseInt(Number(txtUNIT_PRICE.GetText()));
                                txtTOTAL_AMOUNT.SetText((qty * Price).toString());                              
                                
                                if (PRODNO.GetText() == ETCProdNo) {
                                    etcAmt = qty * Price;
                                }
                                
                                ASPxCallback1.SendCallback(s.name + ';' +
                                                           PRODNO.GetText() + ';' +
                                                           s.GetText() + ';' +
                                                           txtUNIT_PRICE.GetText() + ';' +
                                                           txtTOTAL_AMOUNT.GetText() + ';' + 
                                                           hidIS_OPEN_PRICE.GetText()
                                                          );

                                //imeiPop.SetContentUrl(setContentUrl_IMEI(imeiPop.GetContentUrl(), PRODNO, s.GetText(), hdIMEIFlag.GetText()));

                                //**2011/04/27 Tina：將URL傳遞的參數加密。
                                setPopupContentUrl_SAL01(imeiPop, UUID.GetText(), PRODNO.GetText(), s.GetText(), hdIMEIFlag.GetText());

                            }
                        }

                        //變更IMEI圖示
                        function ChangeImageIMEI(s, e) {
                            var fName = "9_lbIMEI_QTY";
                            var IMEI_Qty = s;
                            var PRODNO = getClientInstance('TxtBox', s.name.replace(fName, "3_txtPRODNO_txtControl"));
                            var QUANTITY = getClientInstance('TxtBox', s.name.replace(fName, "5_txtQUANTITY"));
                            var imgIMEI = getClientInstance('Image', s.name.replace(fName, "8_imgIMEI"));
                            var lblIMEI_QTY = getClientInstance('TxtBox', s.name.replace(fName, "9_lbIMEI_QTY"));
                            var UUID = getClientInstance('TxtBox', s.name.replace(fName, "9_hd_ID"));

                            if (QUANTITY.GetText() == lblIMEI_QTY.GetText()) {
                                imgIMEI.SetImageUrl("../../../Icon/check.png");
                                imgIMEI.SetSize(16, 16);
                            }
                            else {
                                imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                                imgIMEI.SetSize(27, 16);
                            }

                            this.objName = fName;
                            this.s = s;
                            //var pValues = getIMEI(s, e);
                            //0:TableName 1:OE_NO 2:PRODNO
                            //PageMethods.IMEIContent(pValues[0], pValues[1], pValues[2], IMEIContent);
                            PageMethods.IMEIContent("SALE_IMEI_LOG", UUID.GetText(), PRODNO.GetText(), IMEIContent);

                        }

                        function IMEIContent(values) {
                            var divIMEI_QTY = window.document.all[s.name.replace(objName, "9_divIMEI_QTY")];
                            divIMEI_QTY.attributes["onmouseover"].value = "show('" + values + "');";
                            divIMEI_QTY.attributes["onmouseout"].value = "hide();";
                        }
                        
                        //取得IMEI上傳參數;
                        function getIMEI(s, e) {
                            var fName = this.objName;
                            var pValues = null;
                            imeiPop = getClientInstance('TxtBox', s.name.replace(fName, "9_InputIMEIData_ASPxPopupControl1"));
                            var u = imeiPop.GetContentUrl().split('KeyFieldValue1=');
                            if (u.length > 1) {
                                var oldKeyFieldValue1 = u[1].split('&')[0];
                                pValues = oldKeyFieldValue1.split(';');
                            }
                            return pValues;
                        }

                        //銷售數量不能小於IMEI數量
                        function getPROD_IMEI_COUNT(s, e, objName) {
                            this.s = s;
                            this.objName = objName;
                            var PRODNO = getClientInstance('TxtBox', s.name.replace(objName, "3_txtPRODNO_txtControl"));
                            var UUID = getClientInstance('TxtBox', s.name.replace(objName, "9_hd_ID"));
                            
                            //0:TableName 1:OE_NO 2:PRODNO
                            //var pValues = getIMEI(s, e);
                            if (s.GetText() != '') {
                                //if (pValues != null) {
                                    //PageMethods.getPROD_IMEI_COUNT(pValues[0], pValues[1], pValues[2], getPROD_IMEI_COUNT_OK);
                                    PageMethods.getPROD_IMEI_COUNT("SALE_IMEI_LOG", UUID.GetText(), PRODNO.GetText(), getPROD_IMEI_COUNT_OK);
                                //}
                            }
                        }

                        function getPROD_IMEI_COUNT_OK(returnValue) {
                            var QUANTITY = getClientInstance('TxtBox', s.name);
                            var hdOldQUANTITY = getClientInstance('TxtBox', s.name.replace(objName, "5_hdOldQUANTITY"));

                            if (QUANTITY.GetText() < returnValue) {
                                QUANTITY.SetText(hdOldQUANTITY.GetText());
                                alert('銷售數量不能小於IMEI數量');
                                s.Focus();
                            } else {
                                hdOldQUANTITY.SetText(QUANTITY.GetText());
                            }
                        }
                        
                        function chkIfETCProdNoDelete(s, e) {
                            ETCProdNo = window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCProdNo").value;
                            
                            if (typeof (gvMaster) != "undefined") {
                                for (var i = 0; i < gvMaster.pageRowCount; i++) {
                                    var gvRowName = "ctl00_MainContentPlaceHolder_gvMaster_cell" + i;
                                    var txtPRODNO = getClientInstance('TxtBox', gvRowName + "_3_txtPRODNO_txtControl");
                                    var txtUNIT_PRICE = getClientInstance('TxtBox', gvRowName + "_6_txtUNIT_PRICE");
    
                                    if (txtPRODNO.GetText() == ETCProdNo && txtUNIT_PRICE.GetText() != "0") {
                                        //商品為ETC Card 加值料號
                                        ETCProdNoExist = false;
                                    }
                                }
                             }
                        }
                        
                        function OnGridFocusedRowChanged() {
                            if (typeof (gvMaster) != "undefined" && gvMaster.GetFocusedRowIndex() != -1) {
                                var gvRowName = "ctl00_MainContentPlaceHolder_gvMaster_cell" + gvMaster.GetFocusedRowIndex();
                                var txtPROMPTION_CODE = getClientInstance('TxtBox', gvRowName + "_2_txtPROMPTION_CODE");
                                var txtPROMO_NAME = getClientInstance('TxtBox', gvRowName + "_2_txtPROMO_NAME");
                                var txtPOSUUID_DETAIL = getClientInstance('TxtBox', gvRowName + "_2_txtPOSUUID_DETAIL");
                                var txtSOURCE_TYPE = getClientInstance('TxtBox', gvRowName + "_2_txtSOURCE_TYPE");
                             
                                hidPromotion_Code.SetText(null);
                                hidPromotion_Name.SetText(null);
                                hidPosuuid_Detail.SetText(null);
                                hidSOURCE_TYPE.SetText(null);
                                if (txtPROMPTION_CODE.GetText() != "") 
                                    hidPromotion_Code.SetText(txtPROMPTION_CODE.GetText());
                                if (txtPROMO_NAME.GetText() != "") 
                                    hidPromotion_Name.SetText(txtPROMO_NAME.GetText());
                                if (txtPOSUUID_DETAIL.GetText() != "") 
                                    hidPosuuid_Detail.SetText(txtPOSUUID_DETAIL.GetText());
                                if (txtSOURCE_TYPE.GetText() != "") 
                                    hidSOURCE_TYPE.SetText(txtSOURCE_TYPE.GetText());
                            }
                        }
                        
                        function changePrice(s, e) {
                            if (isNaN(s.GetText())) {
                                s.SetText("1");
                                alert('請輸入數字!');
                                e.processOnServer = false;
                                s.Focus();
                            } else if (s.GetText() == '' || Number(s.GetText()) <= 0 || (!isInteger(s.GetText()))) {
                                s.SetText("1");
                                alert('單價只能為正整數!');
                                e.processOnServer = false;
                                s.Focus();
                            } else if (Number(s.GetText()) > 99999999) {
                                s.SetText("1");
                                alert('單價不可上億!');
                                e.processOnServer = false;
                                s.Focus();
                            } else {
                                var fName = "6_txtOpenPrice";
                                var txtUnit_Price = getClientInstance('TxtBox', s.name.replace(fName, "6_txtUNIT_PRICE"));
                                var QUANTITY = getClientInstance('TxtBox', s.name.replace(fName, "5_txtQUANTITY"));
                                var txtTOTAL_AMOUNT = getClientInstance('TxtBox', s.name.replace(fName, "7_txtTOTAL_AMOUNT"));
                                var PRODNO = getClientInstance('TxtBox', s.name.replace(fName, "3_txtPRODNO_txtControl"));
                                var qty = parseInt(Number(QUANTITY.GetText()));
                                var Price = parseInt(Number(s.GetText()));
                                var hidIS_OPEN_PRICE = getClientInstance('TxtBox', s.name.replace(fName, "6_hidIS_OPEN_PRICE"));
                                
                                txtUnit_Price.SetText(Number(s.GetText()));
                                txtTOTAL_AMOUNT.SetText((qty * Price).toString());
                                
                                ASPxCallback1.SendCallback(s.name + ';' +
                                                           PRODNO.GetText() + ';' +
                                                           QUANTITY.GetText() + ';' +
                                                           s.GetText() + ';' +
                                                           txtTOTAL_AMOUNT.GetText() + ';' + 
                                                           hidIS_OPEN_PRICE.GetText()
                                                          );
                            }
                        }
                        
                        function changeUnitPrice(s, e) {
                            window.document.getElementById("ctl00_MainContentPlaceHolder_hidGetProdInfoComplete").value = "false";
                            if (isNaN(s.GetText())) {
                                s.SetText("1");
                                alert('請輸入數字!');
                                e.processOnServer = false;
                                s.Focus();
                            } else if (s.GetText() == '' || Number(s.GetText()) <= 0 || (!isInteger(s.GetText()))) {
                                s.SetText("1");
                                alert('單價只能為正整數!');
                                e.processOnServer = false;
                                s.Focus();
                            } else if (Number(s.GetText()) > 99999999) {
                                s.SetText("1");
                                alert('單價不可上億!');
                                e.processOnServer = false;
                                s.Focus();
                            } else {
                                var fName = "6_txtUNIT_PRICE";
                                var QUANTITY = getClientInstance('TxtBox', s.name.replace(fName, "5_txtQUANTITY"));
                                var txtTOTAL_AMOUNT = getClientInstance('TxtBox', s.name.replace(fName, "7_txtTOTAL_AMOUNT"));
                                var PRODNO = getClientInstance('TxtBox', s.name.replace(fName, "3_txtPRODNO_txtControl"));
                                var qty = parseInt(Number(QUANTITY.GetText()));
                                var Price = parseInt(Number(s.GetText()));
                                var hidIS_OPEN_PRICE = getClientInstance('TxtBox', s.name.replace(fName, "6_hidIS_OPEN_PRICE"));
                                
                                txtTOTAL_AMOUNT.SetText((qty * Price).toString());
                                
                                ASPxCallback1.SendCallback(s.name + ';' +
                                                           PRODNO.GetText() + ';' +
                                                           QUANTITY.GetText() + ';' +
                                                           s.GetText() + ';' +
                                                           txtTOTAL_AMOUNT.GetText() + ';' + 
                                                           hidIS_OPEN_PRICE.GetText()
                                                          );
                            }
                            window.document.getElementById("ctl00_MainContentPlaceHolder_hidGetProdInfoComplete").value = "true";
                        }
                    </script>

                    <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="ASPxCallback1"
                        OnCallback="ASPxCallback1_Callback">
                        <ClientSideEvents CallbackComplete="function(s,e){AMOUNT_CallbackComplete(s,e);}" />
                    </dx:ASPxCallback>

                    <script type="text/javascript">
                        function AMOUNT_CallbackComplete(s, e) {
                            var f = 'ctl00_MainContentPlaceHolder_';
                            var Amount = e.result.split(';');
                            $get(f + 'lbTOTAL_AMOUNT').innerHTML = Amount[0];
                            $get(f + 'lbPayAmount').innerHTML = Amount[1];
                            $get(f + 'lbChange').innerHTML = Amount[2];
                        }
                    </script>

                    <cc:ASPxGridView ID="gvMaster" runat="server"  ClientInstanceName="gvMaster"
                        KeyFieldName="ID" AutoGenerateColumns="False" Width="100%" 
                        OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                        OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" 
                        OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"  
                        onstartrowediting="gvMaster_StartRowEditing">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <input type="checkbox" id="checkbox1" onclick="if (typeof(gvMaster) != 'undefined') {CheckAll_onclick();}" title="Select/Unselect all rows on the page" />
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="ITEM_TYPE_NAME" Caption="<%$ Resources:WebResources, Category %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="PROMO_NAME" Caption="<%$ Resources:WebResources, PromotionName %>"
                                VisibleIndex="2">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtPROMPTION_CODE" runat="server" Text='<%# Bind("[PROMOTION_CODE]") %>' ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtPOSUUID_DETAIL" runat="server" Text='<%# Bind("[POSUUID_DETAIL]") %>' ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtSOURCE_TYPE" runat="server" Text='<%# Bind("[SOURCE_TYPE]") %>' ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtPROMO_NAME" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[PROMO_NAME]") %>'
                                        runat="server">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"
                                VisibleIndex="3">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="hdOldPRODNO" runat="server" Text='<%# Bind("[PRODNO]") %>' ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hdIMEIFlag" runat="server" Text='' ClientVisible="false"></dx:ASPxTextBox>
                                    <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup" KeyFieldValue1="salehouse"
                                        IsValidation="true" Text='<%# Bind("[PRODNO]") %>' OnClientTextChanged="function(s, e) { getPRODINFO(s,e); }" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="4">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtPRODNAME" ReadOnly="true" Border-BorderStyle="None"  Text='<%# Bind("[PRODNAME]") %>'
                                        runat="server">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="QUANTITY" Caption="<%$ Resources:WebResources, Quantity %>"
                                VisibleIndex="5">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="hidON_HAND_QTY" runat="server" ClientVisible="false" Text='<%# Bind("[ON_HAND_QTY]") %>'>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hidISSTOCK" runat="server" ClientVisible="false" Text='0'>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hdOldQUANTITY" runat="server" ClientVisible="false" Text='<%# Bind("[QUANTITY]") %>'></dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtQUANTITY" AutoPostBack="false" ClientSideEvents-TextChanged='function(s,e){ checkSaleQty(s,e);getPROD_IMEI_COUNT(s,e,"5_txtQUANTITY");}'
                                        Text='<%# Bind("[QUANTITY]") %>' runat="server" Width="50px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UNIT_PRICE" Caption="<%$ Resources:WebResources, UnitPrice %>"
                                VisibleIndex="6">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="hidIS_OPEN_PRICE" runat="server" ClientVisible="false" Text='<%# Bind("[IS_OPEN_PRICE]") %>'>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtUNIT_PRICE" Text='<%# Bind("[UNIT_PRICE]") %>' 
                                        Border-BorderStyle="None" runat="server" Width="80px" ClientVisible="true" 
                                        ClientSideEvents-TextChanged="function(s,e){ changeUnitPrice(s,e);}">
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtOpenPrice" Text='<%# Bind("[UNIT_PRICE]") %>' 
                                        runat="server" Width="80px" ClientVisible="false"   
                                        ClientSideEvents-TextChanged="function(s,e){ changePrice(s,e);}" >
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="TOTAL_AMOUNT" Caption="<%$ Resources:WebResources, TotalPrice %>"
                                VisibleIndex="7">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtTOTAL_AMOUNT" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[TOTAL_AMOUNT]") %>' runat="server" Width="40px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="imgIMEI" Caption=" " VisibleIndex="8">
                                <DataItemTemplate>
                                    <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl=""></dx:ASPxImage>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI_QTY" Caption="<%$ Resources:WebResources, Imei %>"
                                VisibleIndex="9">
                                <DataItemTemplate>
                                    <div id="divIMEI" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>                                               
                                                <div id="divIMEI_QTY" runat="server" >
                                                    <dx:ASPxTextBox ID="lbIMEI_QTY" runat="server" ReadOnly="true" Border-BorderStyle="None" Width="20px" DisabledStyle-Font-Underline="true">
                                                        <ClientSideEvents TextChanged="function(s, e) { ChangeImageIMEI(s, e); }" />
                                                    </dx:ASPxTextBox>
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td> 
                                                 <dx:ASPxTextBox ID="hd_ID" runat="server" Text='<%# Bind("ID") %>' ClientVisible="false"></dx:ASPxTextBox>                                           
                                                 <uc1:PopupControl ID="InputIMEIData" runat="Server" PopupControlName="InputIMEIData" Text='<%# Bind("[IMEI]") %>' AssignToControlId="lbIMEI_QTY" KeyFieldValue1="" />
                                           </td>
                                        </tr>
                                    </table>
                                    </div>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table cellpadding="0" cellspacing="0" border="0" align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnItemAdd" AutoPostBack="false" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                UseSubmitBehavior="false" OnClick="btnItemAdd_Click">
                                                <ClientSideEvents Click="function(s, e){ gvMaster.SelectAllRowsOnPage(false);ItemAddChk(s, e);}" />
                                            </dx:ASPxButton>
                                            <script type="text/javascript">
                                                 function ItemAddChk(s, e) //是否可新增項目
                                                 {
                                                     if (!chkGetInfoComplete())
                                                        e.processOnServer=false;
                                                 }
                                            </script>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnItemDel" runat="server" Text="<%$ Resources:WebResources, Delete %>" CausesValidation="false"
                                                UseSubmitBehavior="false" ClientSideEvents-Click="function(s,e){                                              
                                                 var selCnt = gvMaster.GetSelectedRowCount();
                                                 if (selCnt == 0) {
                                                     alert('請選取要刪除的資料!');
                                                     e.processOnServer = false;
                                                     return false;
                                                 }
                                                 if (!chkGetInfoComplete()) {
                                                     e.processOnServer = false;
                                                     return false;
                                                 }
                                                 if (!confirm('您確定要刪除所勾選的交易資料嗎？')) {
                                                    e.processOnServer=false;
                                                 } else {
                                                    chkIfETCProdNoDelete(s,e);
                                                 }
                                             }" OnClick="btnItemDel_Click">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnMixPromotion" runat="server" Text="<%$ Resources:WebResources, MixPromotion %>"
                                                UseSubmitBehavior="false" AutoPostBack="false" OnClick="btnMixPromotion_Click">
                                                <ClientSideEvents Click="function(s, e){gvMaster.SelectAllRowsOnPage(false); MixPromotionWindow(s,e);}" />
                                            </dx:ASPxButton>
                                            <script type="text/javascript">
                                                function MixPromotionWindow(s, e) //
                                                {
                                                    hdPaidInfo.SetText(null);
                                                    if (!chkGetInfoComplete()) {
                                                        e.processOnServer=false;
                                                        return false;
                                                    }
                                                    if (!checkInv()) {
                                                        e.processOnServer=false;
                                                        return false;
                                                    }
                                                    var strWin = '../SAL01/SAL01_choosePromotions.aspx?Promotion_Code=' + hidPromotion_Code.GetText() + "&Posuuid_Detail=" 
                                                                    + hidPosuuid_Detail.GetText();
                                                    var r = openDialogWindowByEncrypt(strWin, 700, 600);
                                                    if (r == '' || r == undefined) {
                                                        e.processOnServer = false;
                                                    } else {
                                                        hdPaidInfo.SetText(r);
                                                        e.processOnServer = true;
                                                    }
                                                }
                                            </script>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnCreditCommunication" runat="server" Text="<%$ Resources:WebResources, CreditCommunication %>" AutoPostBack="false"
                                                UseSubmitBehavior="false" OnClick="btnCreditCommunication_Click">
                                                <ClientSideEvents Click="function(s, e){gvMaster.SelectAllRowsOnPage(false); CreditCommunWindow(s,e);}" />
                                            </dx:ASPxButton>

                                            <script type="text/javascript">
                                                function CreditCommunWindow(s, e) //授信通聯
                                                {
                                                    hdPaidInfo.SetText(null);
                                                    if (!chkGetInfoComplete()) {
                                                        e.processOnServer=false;
                                                        return false;
                                                    }
                                                    var r = openDialogWindowByEncrypt('../../CheckOut/CreditCommunications.aspx', 500, 500);
                                                    if (r == '' || r == undefined) {
                                                        e.processOnServer = false;
                                                    } else {
                                                        //window.document.getElementById('__EVENTARGUMENT').value = r;
                                                        hdPaidInfo.SetText(r);
                                                        e.processOnServer = true;

                                                    }
                                                }                                               
                                            </script>

                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                        <Settings ShowTitlePanel="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                    </cc:ASPxGridView>

                    <div class="txt" style="text-align: left">
                        <!--應收總金額-->
                        <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>"></asp:Literal>：
                        <dx:ASPxLabel ID="lbTOTAL_AMOUNT" ClientInstanceName="lbTOTAL_AMOUNT" runat="server" Text=""></dx:ASPxLabel>
                    </div>
                    <div class="seperate"></div>
                    <div style="text-align: left">
                        <asp:Label ID="Label8" Text="<%$ Resources:WebResources, DiscountDetail %>" runat="server" />
                        <asp:HiddenField ID="hidStore_No" runat="server" />
                    </div>
                    
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvDetail" runat="server" EnableCallBacks="true" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="ID" Visible="false">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SEQNO" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                                    VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, DiscountName %>"
                                    VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="QUANTITY" Caption="<%$ Resources:WebResources, Quantity %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="UNIT_PRICE" Caption="<%$ Resources:WebResources, UnitPrice %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="TOTAL_AMOUNT" Caption="<%$ Resources:WebResources, TotalPrice %>"
                                    VisibleIndex="6" />
                            </Columns>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                        </cc:ASPxGridView>
                    </div>
                    <div class="seperate"></div>
                    <div class="btnPosition">
                        <table align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnConfirm" runat="server" Text="<%$ Resources:WebResources, Confirm %>"
                                        UseSubmitBehavior="false" OnClick="btnConfirm_Click">
                                        <ClientSideEvents Click="function(s,e){ConfirmTran(s,e);}" />
                                    </dx:ASPxButton>

                                    <script type="text/javascript">

                                        function ConfirmTran(s, e) { //交易確認
                                          alert('開始交易補登確認作業檢查，請稍待!');
                                          if (!chkGetInfoComplete()) {
                                            e.processOnServer=false;
                                            return false;
                                          }
                                          if (CheckgvMaster(s, e)) {
                                            alert('開始交易補登確認作業，請稍待!');
                                            if (s.GetEnabled()) {
                                                s.SendPostBack('Click');
                                                s.SetEnabled(false);
                                            }
                                          }
                                        }
                                    </script>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                        UseSubmitBehavior="false" OnClick="btnCancel_Click">
                                        <ClientSideEvents Click="function(s,e){BackTran(s,e);}" />
                                    </dx:ASPxButton>

                                    <script type="text/javascript">

                                        function BackTran(s, e) { //取消
                                          if (!confirm('確定取消否？')) {
                                            e.processOnServer=false;
                                            return false;
                                          } else {
                                            e.processOnServer=true;
                                          }
                                        }
                                    </script>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvCheckOut" ClientInstanceName="gvCheckOut" runat="server" EnableCallBacks="true" 
                            AutoGenerateColumns="false" Width="100%" KeyFieldName="ID" 
                            onstartrowediting="gvCheckOut_StartRowEditing">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="if (typeof(gvCheckOut) != 'undefined') {gvCheckOut.SelectAllRowsOnPage(this.checked);}" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="PAID_MODE_NAME" Caption="<%$ Resources:WebResources, PaymentMethod %>" VisibleIndex="1"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PAID_AMOUNT" Caption="<%$ Resources:WebResources, Amount %>" VisibleIndex="2">
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lbPAID_AMOUNT" runat="server" Text='<%# Bind("[PAID_AMOUNT]") %>'>
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <%-- 付款明細--%>
                                <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="<%$ Resources:WebResources, PaymentStatement %>" VisibleIndex="3">
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lbDESCRIPTION" runat="server" Text='<%# Bind("[DESCRIPTION]") %>'>
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PAID_MODE" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="CREDIT_CARD_NO" Visible="false"></dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PAID_AMOUNT" Visible="false"></dx:GridViewDataColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <table cellpadding="0" cellspacing="0" border="0" align="left">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnCash" runat="server" Text="<%$ Resources:WebResources, Cash %>"
                                                    UseSubmitBehavior="false" OnClick="btnCash_Click">
                                                    <ClientSideEvents Click="function(s,e){gvCheckOut.SelectAllRowsOnPage(false);CashWindow(s,e);}" />
                                                </dx:ASPxButton>

                                                <script type="text/javascript">

                                                    function CashWindow(s, e) { //付現
                                                        hdPaidInfo.SetText(null);
                                                        var r = '';
                                                        if (lbPayAmount.GetText() == "")
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutCash.aspx?date=' + Date() + "&ShouldPayAmt=" + lbTOTAL_AMOUNT.GetText(), 400, 200);
                                                        else
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutCash.aspx?date=' + Date() + "&ShouldPayAmt=" + lbPayAmount.GetText(), 400, 200);
                                                        if (r == '' || r == undefined) {
                                                            e.processOnServer = false;
                                                        } else {
                                                            //$("#__EVENTARGUMENT").value = r;
                                                            //window.document.getElementById('__EVENTARGUMENT').value = r;
                                                            hdPaidInfo.SetText(r);
                                                            e.processOnServer = true;
                                                        }
                                                    }
                                                </script>

                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnOffLineCredit" runat="server" Text="<%$ Resources:WebResources, OffLineCreditCard %>"
                                                    UseSubmitBehavior="false" OnClick="btnOffLineCredit_Click">
                                                    <ClientSideEvents Click="function(s, e){gvCheckOut.SelectAllRowsOnPage(false); OffLineCreditWindow(s,e);}" />
                                                </dx:ASPxButton>

                                                <script type="text/javascript">
                                                    function OffLineCreditWindow(s, e)  //離線信用卡
                                                    {
                                                        hdPaidInfo.SetText(null);
                                                        var r = '';
                                                        if (lbPayAmount.GetText() == "")
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutCreditUnline.aspx?date=' + Date() + "&ShouldPayAmt=" + lbTOTAL_AMOUNT.GetText(), 400, 200);
                                                        else
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutCreditUnline.aspx?date=' + Date() + "&ShouldPayAmt=" + lbPayAmount.GetText(), 400, 200);
                                                        if (r == '' || r == undefined) {
                                                            e.processOnServer = false;
                                                        } else {
                                                            //window.document.getElementById('__EVENTARGUMENT').value = r;
                                                            hdPaidInfo.SetText(r);
                                                            e.processOnServer = true;
                                                        }
                                                    }
                                                </script>

                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="hdDeleteIDs" ClientInstanceName="hdDeleteIDs" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                                                <dx:ASPxButton ID="btnPayDEL" ClientInstanceName="btnPayDEL" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                                                    UseSubmitBehavior="false" AutoPostBack="false" OnClick="btnPayDEL_Click">
                                                   <ClientSideEvents Click="function(s,e) { DeletePaidItem(s, e); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                            </Templates>
                            <Settings ShowTitlePanel="True" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                        </cc:ASPxGridView>
                        <div style="text-align: left">
                            <table cellpadding="0" width="100%">
                                <tr>
                                    <td style="width: 70%">
                                        <!--應付總金額-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, TotalAmountDue%>"></asp:Literal>：
                                        <dx:ASPxLabel ID="lbPayAmount" ClientInstanceName="lbPayAmount" runat="server" ></dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <!--找零-->
                                        <asp:Literal ID="Literal4" runat="server" Text="找零"></asp:Literal>：
                                        <dx:ASPxLabel ID="lbChange" ClientInstanceName="lbChange" runat="server" Text="0">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="seperate"></div>
                    <div class="btnPosition">
                        <table cellpadding="0" cellspacing="0" border="0" align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnOrderCheckOut" Enabled="false" runat="server" Text="<%$ Resources:WebResources, CheckOut %>"
                                        UseSubmitBehavior="false" OnClick="btnOrderCheckOut_Click">
                                        <ClientSideEvents Click="function(s,e)
                                                        {             
                                                           OrderCheckOut(s,e);
                                                        }" />
                                    </dx:ASPxButton>

                                    <script type="text/javascript">
                                        function OrderCheckOut(s, e) {
                                            var rec = parseInt(lbChange.GetText());
                                            if (rec < 0) {
                                                e.processOnServer = false;
                                                alert('支付不合理，請重新輸入');
                                            }
                                            else {
                                                if (rec > 0) {
                                                    alert('須找零:' + rec + '元');
                                                }
                                                if (!checkInv()) {
                                                    alert('請先輸入發票號碼');
                                                    e.processOnServer = false;
                                                } else {
                                                    if (!confirm('確定結帳否？')) {
                                                        e.processOnServer = false;
                                                    }
                                                    else {
                                                        e.processOnServer = true;
                                                    }
                                                }
                                            }
                                        }
                                    </script>

                                </td>
                                <%--<td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnOrderCancel" OnClick="btnOrderCancel_Click" runat="server"
                                        UseSubmitBehavior="false" Text="<%$ Resources:WebResources, CancelTransaction %>">
                                        <ClientSideEvents Click="function(s,e)
                                                        {             
                                                           OrderCancel(s,e);
                                                        }" />
                                    </dx:ASPxButton>
                                    <script type="text/javascript">
                                        function OrderCancel(s, e) {
                                            if (!confirm('確定取消交易否？')) {
                                                e.processOnServer = false;
                                            } else {
                                                e.processOnServer = true;
                                            }
                                        }
                                     </script>
                                </td> --%>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
