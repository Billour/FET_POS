<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SAL01.aspx.cs" Inherits="VSS_SAL_SA01" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/jquery.checkboxes.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">       
        var ETCProdNo = "";
        var ETCProdNoExist = "";
        var etcAmt = 0;
        
        function Call_DetectPrinterName() {
            //ctl00$MainContentPlaceHolder$PrintName

            var PrinterNAME = window.document.getElementById("ctl00_MainContentPlaceHolder_PrintName").value;

            var oBarcodePrint = new ActiveXObject("detctPrinter.detect");
            var result = oBarcodePrint.detectP(PrinterNAME);
            if (result == "") {

                alert("印表機名稱有誤!請重新確認");
                // e.processOnServer = false;
            }
            else {
                window.document.getElementById("ctl00_MainContentPlaceHolder_PrintName").value = result;
                //this.PrintName.SetText = result;

            }
            //    alert(window.document.getElementById("ctl00_MainContentPlaceHolder_PrintName").value);
        }
        
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
            if (PAID_MODE == "2" || PAID_MODE == "7") {
                //進行刷退
                if (PAID_MODE == "2") {  //信用卡
                    var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutCredit2.aspx?date=' + Date()
                        + '&CREDIT_CARD_NO=' + values[2]
                        + '&PAID_AMOUNT=' + values[4]
                         , 500, 500);
                    if (r == '' || r == undefined) {
                        gvCheckOut.SelectRowsByKey(values[0], false);
                    }
                    else {
                        if (r != "0000") {
                            gvCheckOut.SelectRowsByKey(values[0], false);
                            alert("信用卡刷退失敗!");
                        }
                        else {
                            strIDs += "'" + values[0] + "',";
                        }
                    }
                }
                else {  //HappyGo卡
                    var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutHG3.aspx?date=' + Date()
                        + '&HG_CARD_NO=' + values[3]
                        + '&TOTAL_AMOUNT=' + values[4]
                        + '&HG_REDEEM_POINT=' + values[5]
                        + '&HG_LEFT_POINT=' + values[6]
                        , 500, 500);
                    if (r == '' || r == undefined) {
                        gvCheckOut.SelectRowsByKey(values[0], false);
                    }
                    else {
                        if (r == "fail") {
                            gvCheckOut.SelectRowsByKey(values[0], false);
                            alert("HappyGo刷退失敗!");
                        }
                        else {
                            strIDs += "'" + values[0] + "',";
                        }
                    }
                } //end if PAID_MODE
            }
            else {
                strIDs += "'" + values[0] + "',";
            }
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<OBJECT ID="detect"
CLASSID="CLSID:7BACE1A5-F435-45DB-BFB3-23B5AE9069F1"
CODEBASE="detctPrinter.CAB#version=1,0,0,1">
</OBJECT>
    <div id="tooltip">
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                        <tr>
                            <td align="left">
                                <!--銷售作業-->
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Selling %>"></asp:Literal>
                            </td>
                            <td align="right">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                                UseSubmitBehavior="false" OnClick="ASPxButton1_Click" CausesValidation="false">
                                                <%-- <ClientSideEvents Click="function(s, e){document.location='../SAL02/SAL02.aspx';}" />--%>
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, UnclearedTradeListing %>"
                                                UseSubmitBehavior="false" OnClick="ASPxButton2_Click" CausesValidation="false">
                                                <%--    <ClientSideEvents Click="function(s, e){ document.location='../SAL05/SAL05.aspx';}" />--%>
                                            </dx:ASPxButton>
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
                                <asp:Literal ID="lbVOUCHER_TYPE" runat="server" Text="<%$ Resources:WebResources, InvoiceOrReceipt %>"></asp:Literal>
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="">
                                </dx:ASPxLabel>
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
                                <dx:ASPxLabel ID="lbTran_Date" ClientInstanceName="TRAN_DATE" runat="server" Text="">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td class="tdtxt">
                                <!--更新日期-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxLabel ID="lbMODI_DTM" runat="server" Text="">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--發票號碼-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxLabel ID="lbINVOICE_NO" runat="server" Text="">
                                </dx:ASPxLabel>
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
                                                var r = openDialogWindowByEncrypt("SAL01_checkIDNumber.aspx?date=" + Date() + "&INVOICE_NO=" + txtUNI_NO.GetText(), 400, 200);
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
                                <!--備註-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                            </td>
                            <td class="tdval" colspan="3">
                                <dx:ASPxTextBox ID="txtREMARK" runat="server" ClientInstanceName="txtREMARK" CssClass="tbWidthFormat">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                HG卡號：
                            </td>
                            <td>
                                <dx:ASPxLabel ID="lbHG_CARD_NO" runat="server" Text="">
                                </dx:ASPxLabel>
                            </td>
                            <td class="tdtxt">
                                HG剩餘點數：
                            </td>
                            <td class="tdval">
                                <dx:ASPxLabel ID="lbHG_REMAIN_POINT" runat="server" Text="">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="seperate">
                    <asp:HiddenField ID="PrintName" runat="server" />
                    <asp:HiddenField ID="hidETCProdNo" runat="server" />
                    <asp:HiddenField ID="hidETCCardProdNo" runat="server" />
                    <asp:HiddenField ID="hidETCCardPromiseProdNo" runat="server" />
                    <asp:HiddenField ID="hidETCProdNoExist" runat="server" />
                    <asp:HiddenField ID="hidAddProdList" runat="server" />
                    <asp:HiddenField ID="hidGetProdInfoComplete" runat="server" />
                    <asp:HiddenField ID="hidStore_No" runat="server" />
                    <asp:HiddenField ID="hidETCAMT" runat="server" />
                    <asp:HiddenField ID="hidETCLoadInfo" runat="server" />
                    <dx:ASPxTextBox ID="hdPaidInfo" ClientInstanceName="hdPaidInfo" runat="server" ClientVisible="false">
                    </dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidPromotion_Code" ClientInstanceName="hidPromotion_Code" runat="server" ClientVisible="false">
                    </dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidPromotion_Name" ClientInstanceName="hidPromotion_Name" runat="server" ClientVisible="false">
                    </dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidPosuuid_Detail" ClientInstanceName="hidPosuuid_Detail" runat="server" ClientVisible="false">
                    </dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidSOURCE_TYPE" ClientInstanceName="hidSOURCE_TYPE" runat="server" ClientVisible="false">
                    </dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hidPromotionProdList" ClientInstanceName="hidPromotionProdList" runat="server" ClientVisible="false">
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
                        function setPopupContentUrl_SAL01(imeiPop, UUID, PRODNO, QTY, IMEI_FLAG) {
                            
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
                            this.Sender = s;
                            window.document.getElementById("ctl00_MainContentPlaceHolder_hidGetProdInfoComplete").value = "false";
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
                            var txtQUANTITY = getClientInstance('TxtBox', Sender.name.replace(fName, "5_txtQUANTITY"));   ////單價
                            var hidISSTOCK = getClientInstance('TxtBox', Sender.name.replace(fName, "5_hidISSTOCK"));
                            var hidON_HAND_QTY = getClientInstance('TxtBox', Sender.name.replace(fName, "5_hidON_HAND_QTY"));
                            var txtOpenPrice = getClientInstance('TxtBox', Sender.name.replace(fName, "6_txtOpenPrice"));
                            var txtUNIT_PRICE = getClientInstance('TxtBox', Sender.name.replace(fName, "6_txtUNIT_PRICE"));
                            var txtTOTAL_AMOUNT = getClientInstance('TxtBox', Sender.name.replace(fName, "7_txtTOTAL_AMOUNT"));
                            var imgIMEI = getClientInstance('Image', Sender.name.replace(fName, "8_imgIMEI"));
                            var txtInputIMEIData = getClientInstance('TxtBox', Sender.name.replace(fName, "9_InputIMEIData_txtControl"));
                            var btnInputIMEIData = getClientInstance('Button', Sender.name.replace(fName, "9_InputIMEIData_btnControl"));
                            var imeiPop = getClientInstance('Popup', Sender.name.replace(fName, "9_InputIMEIData_ASPxPopupControl1"));
                            var lblIMEI_QTY = getClientInstance('TxtBox', Sender.name.replace(fName, "9_lbIMEI_QTY"));
                            var UUID = getClientInstance('TxtBox', Sender.name.replace(fName, "9_hd_ID"));

                            ETCProdNo = window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCProdNo").value;    
                            ETCProdNoExist = window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCProdNoExist").value;                      
                            
                            if (returnData == '') {
                                if (hdOldPRODNO.GetText() != "") {
                                    txtPRODNO.SetText(hdOldPRODNO.GetText());
                                }
                                alert("無效的商品料號或商品料號不存在!");
                                Sender.Focus();
                            }
                            else {
                                if (txtPRODNO.GetText() == ETCProdNo) {
                                    //商品為ETC Card 加值料號
                                    if (ETCProdNoExist == "true") {
                                        alert("ETC加值料號已經存在，不允許重複新增ETC加值料號商品!");
                                        Sender.Focus();
                                        window.document.getElementById("ctl00_MainContentPlaceHolder_hidGetProdInfoComplete").value = "true";
                                        return false;
                                    } else {
                                        window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCProdNoExist").value = "true";
                                    }
                                }
                                
                                var addProdList = window.document.getElementById("ctl00_MainContentPlaceHolder_hidAddProdList").value;
                                if (addProdList == "") 
                                    addProdList = txtPRODNO.GetText();
                                else 
                                    addProdList += "^" + txtPRODNO.GetText();
                                window.document.getElementById("ctl00_MainContentPlaceHolder_hidAddProdList").value = addProdList;
                               
                                var p = returnData.split(';');
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
                                    window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCAMT").value = etcAmt;
                                }
                                // gvMaster.PerformCallback();
                                hidISSTOCK.SetText(p[3]);
                                hidON_HAND_QTY.SetText(p[4]);
                                var onHandQty = 0;
                                if (p[4] != null && p[4] != "" && !isNaN(p[4]))
                                    onHandQty = parseInt(Number(p[4]));
                                if (p[3] == "1" && onHandQty < 0) {
                                    alert('商品庫存量不足!');
                                }

                                ASPxCallback1.SendCallback(Sender.name + ';' +
                                                           Sender.GetText() + ';' +
                                                           txtQUANTITY.GetText() + ';' +
                                                           txtUNIT_PRICE.GetText() + ';' +
                                                           txtTOTAL_AMOUNT.GetText()
                                                          );
                                                          
                                EnableIMEI(fName, Sender.name);
                                //imeiPop.SetContentUrl(setContentUrl_IMEI(imeiPop.GetContentUrl(), Sender.GetText(), txtQUANTITY.GetText(), IMET_FLAG));

                                //**2011/04/27 Tina：將URL傳遞的參數加密。
                                setPopupContentUrl_SAL01(imeiPop, UUID.GetText(), Sender.GetText(), txtQUANTITY.GetText(), IMET_FLAG);

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
                            var imgIMEI = getClientInstance('Image', ControlName.replace(fName, "8_imgIMEI"));
                            var imeiPop = getClientInstance('Popup', ControlName.replace(fName, "9_InputIMEIData_ASPxPopupControl1"));
                            var txtInputIMEIData = getClientInstance('TxtBox', ControlName.replace(fName, "9_InputIMEIData_txtControl"));
                            var btnInputIMEIData = getClientInstance('Button', ControlName.replace(fName, "9_InputIMEIData_btnControl"));
                            var lblIMEI_QTY = getClientInstance('TxtBox', ControlName.replace(fName, "9_lbIMEI_QTY"));
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
                            var txtUNIT_PRICE = getClientInstance('TxtBox', s.name.replace(fName, "6_txtUNIT_PRICE"));
                            var txtTOTAL_AMOUNT = getClientInstance('TxtBox', s.name.replace(fName, "7_txtTOTAL_AMOUNT"));
                            var txtInputIMEIData = getClientInstance('TxtBox', s.name.replace(fName, "9_InputIMEIData_txtControl"));
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
                                    window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCAMT").value = etcAmt;
                                }
                                
                                ASPxCallback1.SendCallback(s.name + ';' +
                                                           PRODNO.GetText() + ';' +
                                                           s.GetText() + ';' +
                                                           txtUNIT_PRICE.GetText() + ';' +
                                                           txtTOTAL_AMOUNT.GetText()
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
                           // var pValues = getIMEI(s, e);
                            if (s.GetText() != '') {
                                //if (pValues != null) {
                                    //PageMethods.getPROD_IMEI_COUNT(pValues[0], pValues[1], pValues[2], getPROD_IMEI_COUNT_OK);
                                    PageMethods.getPROD_IMEI_COUNT("SALE_IMEI_LOG", UUID.GetText(), PRODNO.GetText(), getPROD_IMEI_COUNT_OK);
                               // }
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
//                                txtQUANTITY = getClientInstance('TxtBox', s.name.replace(objName, "5_txtQUANTITY"));
//                                txtUNIT_PRICE = getClientInstance('TxtBox', s.name.replace(objName, "6_txtUNIT_PRICE"));
//                                txtTOTAL_AMOUNT = getClientInstance('TxtBox', s.name.replace(objName, "txtTOTAL_AMOUNT"));
//                                var qty = parseInt(txtQUANTITY.GetText());
//                                var Price = parseInt(p[2]);
//                                txtTOTAL_AMOUNT.SetText(qty * Price);
//                                txtUNIT_PRICE.SetText(Price);
                                //lbTOTAL_AMOUNT.innerHTML=(qty * Price).toString();
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
                                        window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCProdNoExist").value = "false";
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
                                
                                txtUnit_Price.SetText(s.GetText());
                                txtTOTAL_AMOUNT.SetText((qty * Price).toString());
                                
                                ASPxCallback1.SendCallback(s.name + ';' +
                                                           PRODNO.GetText() + ';' +
                                                           QUANTITY.GetText() + ';' +
                                                           s.GetText() + ';' +
                                                           txtTOTAL_AMOUNT.GetText()
                                                          );
                            }
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

                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="ID"
                        AutoGenerateColumns="False" Width="100%" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                        OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                        OnStartRowEditing="gvMaster_StartRowEditing">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <input type="checkbox" id="checkbox1" onclick="if (typeof(gvMaster) != 'undefined') {CheckAll_onclick();}"
                                        title="Select/Unselect all rows on the page" />
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
                                    <dx:ASPxTextBox ID="hdOldPRODNO" runat="server" Text='<%# Bind("[PRODNO]") %>' ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hdIMEIFlag" runat="server" Text='' ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup"
                                        KeyFieldValue1="salehouse" IsValidation="true" Text='<%# Bind("[PRODNO]") %>'
                                        OnClientTextChanged="function(s, e) { getPRODINFO(s,e); }" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="4">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtPRODNAME" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[PRODNAME]") %>'
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
                                    <dx:ASPxTextBox ID="hdOldQUANTITY" runat="server" ClientVisible="false" Text='<%# Bind("[QUANTITY]") %>'>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtQUANTITY" AutoPostBack="false" ClientSideEvents-TextChanged='function(s,e){ checkSaleQty(s,e);getPROD_IMEI_COUNT(s,e,"5_txtQUANTITY");}'
                                        Text='<%# Bind("[QUANTITY]") %>' runat="server" Width="50px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UNIT_PRICE" Caption="<%$ Resources:WebResources, UnitPrice %>"
                                VisibleIndex="6">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtUNIT_PRICE" Text='<%# Bind("[UNIT_PRICE]") %>' 
                                        Border-BorderStyle="None" runat="server" Width="80px"   
                                        ClientVisible="true" ReadOnly="true" >
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
                                    <dx:ASPxTextBox ID="txtTOTAL_AMOUNT" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[TOTAL_AMOUNT]") %>'
                                        runat="server" Width="40px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="imgIMEI" Caption=" " VisibleIndex="8">
                                <DataItemTemplate>
                                    <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                    </dx:ASPxImage>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI_QTY" Caption="<%$ Resources:WebResources, Imei %>"
                                VisibleIndex="9">
                                <DataItemTemplate>
                                    <div id="divIMEI" runat="server">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <div id="divIMEI_QTY" runat="server">
                                                        <dx:ASPxTextBox ID="lbIMEI_QTY" runat="server" ReadOnly="true" Border-BorderStyle="None"
                                                            Width="20px" DisabledStyle-Font-Underline="true">
                                                            <ClientSideEvents TextChanged="function(s, e) { ChangeImageIMEI(s, e); }" />
                                                        </dx:ASPxTextBox>
                                                    </div>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox ID="hd_ID" runat="server" Text='<%# Bind("ID") %>' ClientVisible="false"></dx:ASPxTextBox>
                                                    <uc1:PopupControl ID="InputIMEIData" runat="Server" PopupControlName="InputIMEIData"
                                                        Text='<%# Bind("[IMEI]") %>' AssignToControlId="lbIMEI_QTY" KeyFieldValue1="" />
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
                                            <dx:ASPxButton ID="btnItemDel" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                CausesValidation="false" UseSubmitBehavior="false" ClientSideEvents-Click="function(s,e){                                              
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
                                            <dx:ASPxButton ID="btnOrdToSale" runat="server" Text="預收轉銷售" AutoPostBack="false"
                                                UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s, e){ gvMaster.SelectAllRowsOnPage(false);OrdToSaleWindow(s,e);}" />
                                            </dx:ASPxButton>

                                            <script type="text/javascript">
                                                 function OrdToSaleWindow(s, e) //預收轉銷售
                                                 {
                                                     hdPaidInfo.SetText(null);
                                                     if (!chkGetInfoComplete()) {
                                                        e.processOnServer=false;
                                                        return false;
                                                     }

                                                     var r = openDialogWindowByEncrypt('SAL01_inputPreOrderNumber.aspx?date=' + Date(), 520, 380);
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
                                                    if (hidPromotion_Code.GetText() == null || hidPromotion_Code.GetText() == "" || 
                                                        hidPosuuid_Detail.GetText() == null || hidPosuuid_Detail.GetText() == "") {
                                                        alert('請先點選促銷組合商品!!');
                                                        e.processOnServer=false;
                                                        return false;
                                                    }
                                                    var strWin = 'SAL01_choosePromotions.aspx?Promotion_Code=' + hidPromotion_Code.GetText() + "&Posuuid_Detail=" 
                                                                    + hidPosuuid_Detail.GetText() + "&PromotionProdList=" + hidPromotionProdList.GetText();
                                                    var r = openDialogWindowByEncrypt(strWin, 700, 420);
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
                                            <dx:ASPxButton ID="btnETCAdd" runat="server" Text="<%$ Resources:WebResources, ETCADD %>"
                                                UseSubmitBehavior="false" AutoPostBack="false" OnClick="btnETCAdd_Click">
                                                <ClientSideEvents Click="function(s, e){ETCAddWindow(s,e);}" />
                                            </dx:ASPxButton>

                                            <script type="text/javascript">
                                                function ETCAddWindow(s, e) //
                                                {
                                                    hdPaidInfo.SetText(null);
                                                    var r = openDialogWindowByEncrypt('../../CheckOut/ETCInput.aspx', 400, 200);
                                                    if (r == '' || r == 'undefined') {
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
                                            <dx:ASPxButton ID="btnHappyGoNet" runat="server" Text="HappyGo折抵" AutoPostBack="false"
                                                UseSubmitBehavior="false" OnClick="btnHappyGoNet_Click">
                                                <ClientSideEvents Click="function(s, e){gvMaster.SelectAllRowsOnPage(false); HappyGoNetWindow(s,e);}" />
                                            </dx:ASPxButton>

                                            <script type="text/javascript">
                                                function HappyGoNetWindow(s, e) //HappyGo折抵
                                                {
                                                    hdPaidInfo.SetText(null);
                                                    if (!chkGetInfoComplete()) {
                                                        e.processOnServer=false;
                                                        return false;
                                                    }
                                                    if (lbTOTAL_AMOUNT.GetText() == "" || isNaN(lbTOTAL_AMOUNT.GetText()) || lbTOTAL_AMOUNT.GetText() == "0") {
                                                        alert('無應收總金額，請先填入商品料號');
                                                        e.processOnServer = false;
                                                        return;
                                                    }
                                                    
                                                    var addProdList = window.document.getElementById("ctl00_MainContentPlaceHolder_hidAddProdList").value;
                                                     
                                                    //改成無法調整大小的對話視窗
                                                    var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutHG.aspx?date=' + Date() + '&TOTAL_AMOUNT=' + lbTOTAL_AMOUNT.GetText() + '&TRAN_DATE=' + TRAN_DATE.GetText() + '&addProdList=' + addProdList, 500, 550);
                                                    //if (r == '' || r == undefined) {
                                                    if (r == undefined) {
                                                        e.processOnServer = false;
                                                    } else {
                                                        if (r.length <= 0) {
                                                            e.processOnServer = false;
                                                        }
                                                        else {
                                                            var content = "";
                                                            for (i = 0; i < r.length; i = i + 1) {
                                                                content += r[i] + "|";  
                                                            }
                                                            //window.document.getElementById('__EVENTARGUMENT').value = content;
                                                            hdPaidInfo.SetText(content);
                                                             e.processOnServer = true;
                                                        }
                                                    }
                                                }
                                            </script>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnStoreDiscount" runat="server" Text="特殊抱怨折扣" AutoPostBack="false"
                                                UseSubmitBehavior="false" OnClick="btnStoreDiscount_Click">
                                                <ClientSideEvents Click="function(s, e){gvMaster.SelectAllRowsOnPage(false); StoreDiscountWindow(s,e);}" />
                                            </dx:ASPxButton>

                                            <script type="text/javascript">
                                                function StoreDiscountWindow(s, e) //特殊抱怨折扣
                                                {
                                                    hdPaidInfo.SetText(null);
                                                    if (!chkGetInfoComplete()) {
                                                        e.processOnServer=false;
                                                        return false;
                                                    }
                                                    if (lbTOTAL_AMOUNT.GetText() == "" || isNaN(lbTOTAL_AMOUNT.GetText()) || lbTOTAL_AMOUNT.GetText() == "0") {
                                                        alert('無應收總金額，請先填入商品料號');
                                                        e.processOnServer = false;
                                                        return;
                                                    }
                                                    var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutSM.aspx?date=' + Date() + '&TOTAL_AMOUNT=' + lbTOTAL_AMOUNT.GetText() + '&TRAN_DATE=' + TRAN_DATE.GetText(), 500, 250);
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
                                            <dx:ASPxButton ID="btnAddProd" runat="server" Text="加價購" 
                                                UseSubmitBehavior="false" AutoPostBack="false" OnClick="btnAddProd_Click">
                                                <ClientSideEvents Click="function(s, e){ gvMaster.SelectAllRowsOnPage(false);AddProdWindow(s,e);}" />
                                            </dx:ASPxButton>

                                            <script type="text/javascript">
                                                 function AddProdWindow(s, e) //加價購
                                                 {
                                                     hdPaidInfo.SetText(null);
                                                     if (!chkGetInfoComplete()) {
                                                        e.processOnServer=false;
                                                        return false;
                                                     }
                                                     
                                                     var addProdList = window.document.getElementById("ctl00_MainContentPlaceHolder_hidAddProdList").value;
                                                     
                                                     //改成無法調整大小的對話視窗
                                                     //var r = window.showModalDialog('../../CheckOut/CheckOutAddProd.aspx?addProdList=' + addProdList, self, 'dialogWidth:500px;dialogHeight:550px;status:no;resizable:no;scroll:yes');
                                                     var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutAddProd.aspx?addProdList=' + addProdList, 500, 550);
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
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnETCCard" runat="server" Text="<%$ Resources:WebResources, ETCCard %>"
                                                UseSubmitBehavior="false" AutoPostBack="false" OnClick="btnETCCard_Click">
                                                <ClientSideEvents Click="function(s, e){ETCCardWindow(s,e);}" />
                                            </dx:ASPxButton>

                                            <script type="text/javascript">
                                                function ETCCardWindow(s, e) //
                                                {
                                                    hdPaidInfo.SetText(null);
                                                    var r = openDialogWindowByEncrypt('../../CheckOut/ETCCarNumber.aspx', 400, 200);
                                                    if (r == '' || r == 'undefined') {
                                                        e.processOnServer = false;
                                                    } else {
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
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                        <Settings ShowTitlePanel="True" />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
                    </cc:ASPxGridView>
                    <div class="txt" style="text-align: left">
                        <!--應收總金額-->
                        <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>"></asp:Literal>：
                        <dx:ASPxLabel ID="lbTOTAL_AMOUNT" ClientInstanceName="lbTOTAL_AMOUNT" runat="server"
                            Text="">
                        </dx:ASPxLabel>
                        <!--HappyGo折抵資訊 -->
                        <!--兌換點數 -->
                        <dx:ASPxTextBox ID="hdHG_REDEEM_POINT" ClientInstanceName="HG_REDEEM_POINT" runat="server"
                            ClientVisible="false">
                        </dx:ASPxTextBox>
                        <!--HappyGo卡號 -->
                        <dx:ASPxTextBox ID="hdHG_CARD_NO" ClientInstanceName="hdHG_CARD_NO" runat="server"
                            ClientVisible="false">
                        </dx:ASPxTextBox>
                        <!--兌點金額 -->
                        <dx:ASPxTextBox ID="hdTOTAL_AMOUNT" ClientInstanceName="TOTAL_AMOUNT" runat="server"
                            ClientVisible="false">
                        </dx:ASPxTextBox>
                        <!--剩餘點數 -->
                        <dx:ASPxTextBox ID="hdHG_LEFT_POINT" ClientInstanceName="HG_LEFT_POINT" runat="server"
                            ClientVisible="false">
                        </dx:ASPxTextBox>
                    </div>
                    <div class="seperate">
                    </div>
                    <div style="text-align: left">
                        <asp:Label ID="Label8" Text="<%$ Resources:WebResources, DiscountDetail %>" runat="server" />
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
                    <div class="seperate">      
                    </div>
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
                                          if (!chkGetInfoComplete()) {
                                            e.processOnServer=false;
                                            return false;
                                          }
                                          CheckgvMaster(s, e);
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
                            OnStartRowEditing="gvCheckOut_StartRowEditing">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="if (typeof(gvCheckOut) != 'undefined') {gvCheckOut.SelectAllRowsOnPage(this.checked);}"
                                            title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="PAID_MODE_NAME" Caption="<%$ Resources:WebResources, PaymentMethod %>"
                                    VisibleIndex="1">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PAID_AMOUNT" Caption="<%$ Resources:WebResources, Amount %>"
                                    VisibleIndex="2">
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lbPAID_AMOUNT" runat="server" Text='<%# Bind("[PAID_AMOUNT]") %>'>
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <%-- 付款明細--%>
                                <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="<%$ Resources:WebResources, PaymentStatement %>"
                                    VisibleIndex="3">
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lbDESCRIPTION" runat="server" Text='<%# Bind("[DESCRIPTION]") %>'>
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PAID_MODE" Visible="false">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="CREDIT_CARD_NO" Visible="false">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PAID_AMOUNT" Visible="false">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="HG_CARD_NO" Visible="false">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="HG_REDEEM_POINT" Visible="false">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="HG_LEFT_POINT" Visible="false">
                                </dx:GridViewDataColumn>
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
                                                <dx:ASPxButton ID="btnCredit" runat="server" Text="<%$ Resources:WebResources, CreditCard %>"
                                                    UseSubmitBehavior="false" OnClick="btnCredit_Click">
                                                    <ClientSideEvents Click="function(s, e){ gvCheckOut.SelectAllRowsOnPage(false);CreditCardWindow(s,e);}" />
                                                </dx:ASPxButton>

                                                <script type="text/javascript">

                                                    function CreditCardWindow(s, e) //信用卡
                                                    {
                                                        hdPaidInfo.SetText(null);
                                                        var r = '';
                                                        if (lbPayAmount.GetText() == "")
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutCredit.aspx?date=' + Date() + "&ShouldPayAmt=" + lbTOTAL_AMOUNT.GetText(), 400, 300);
                                                        else
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutCredit.aspx?date=' + Date() + "&ShouldPayAmt=" + lbPayAmount.GetText(), 400, 300);
                                                        
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
                                                <dx:ASPxButton ID="btnDivCredit" runat="server" Text="<%$ Resources:WebResources, Instalment %>"
                                                    UseSubmitBehavior="false" AutoPostBack="false" OnClick="btnDivCredit_Click" Style="height: 21px">
                                                    <ClientSideEvents Click="function(s, e){ gvCheckOut.SelectAllRowsOnPage(false);DivCreditWindow(s,e);}" />
                                                </dx:ASPxButton>

                                                <script type="text/javascript">
                                                    function DivCreditWindow(s, e) //信用卡分期
                                                    {
                                                        hdPaidInfo.SetText(null);
                                                        var r = '';
                                                        if (lbPayAmount.GetText() == "")
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutCreditStage.aspx?date=' + Date() + "&ShouldPayAmt=" + lbTOTAL_AMOUNT.GetText(), 400, 300);
                                                        else
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutCreditStage.aspx?date=' + Date() + "&ShouldPayAmt=" + lbPayAmount.GetText(), 400, 300);
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
                                                <dx:ASPxButton ID="btnHappyGo" runat="server" Text="<%$ Resources:WebResources, HappyGo %>"
                                                    UseSubmitBehavior="false" OnClick="btnHappyGo_Click">
                                                    <ClientSideEvents Click="function(s, e){gvCheckOut.SelectAllRowsOnPage(false); HappyGoWindow(s,e); }" />
                                                </dx:ASPxButton>

                                                <script type="text/javascript">
                                                     function HappyGoWindow(s, e) //HappyGo支付
                                                     {
                                                        hdPaidInfo.SetText(null);
                                                        var r = '';
                                                        if (lbPayAmount.GetText() == "")
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutHG2.aspx?date=' + Date() + '&TOTAL_AMOUNT=' + lbTOTAL_AMOUNT.GetText() + '&TRAN_DATE=' + TRAN_DATE.GetText(), 500, 500);
                                                        else
                                                            r = openDialogWindowByEncrypt('../../CheckOut/CheckOutHG2.aspx?date=' + Date() + '&TOTAL_AMOUNT=' + lbPayAmount.GetText() + '&TRAN_DATE=' + TRAN_DATE.GetText(), 500, 500);
                                                        if (r == '' || r == undefined) {
                                                            e.processOnServer = false;
                                                        } else {
                                                            // window.document.getElementById('__EVENTARGUMENT').value = r;
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
                                                <dx:ASPxTextBox ID="hdDeleteIDs" ClientInstanceName="hdDeleteIDs" runat="server"
                                                    ClientVisible="false">
                                                </dx:ASPxTextBox>
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
                                        <dx:ASPxLabel ID="lbPayAmount" ClientInstanceName="lbPayAmount" runat="server">
                                        </dx:ASPxLabel>
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
                    <div class="seperate">
                    </div>
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
                                                
                                                if (!confirm('您確定要進行結帳嗎？')) {
                                                    e.processOnServer = false;
                                                }
                                                else {
                                                    ETCProdNoExist = window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCProdNoExist").value;
                                                    etcAmt = window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCAMT").value;
                                                    if (ETCProdNoExist == "true") {
                                                        var r = openDialogWindowByEncrypt('../../CheckOut/ETCCardLoading.aspx?date=' + Date() + "&PaidAmt=" + etcAmt, 500, 500);
                                                         if (r == '' || r == undefined) {
                                                             e.processOnServer = false;
                                                             return false;
                                                         } else {
                                                             var resp = r.split(',');
                                                             
                                                             if (resp[0] != '0000') {
                                                                alert('ETC 加值失敗，終止結帳動作！');
                                                                e.processOnServer = false;
                                                                return false;
                                                             } else {
                                                                window.document.getElementById("ctl00_MainContentPlaceHolder_hidETCLoadInfo").value = r;
                                                             }
                                                         }
                                                    }
                                                
                                                    //若有HappyGo折抵，則需進行刷卡兌點
                                                    if (hdHG_CARD_NO.GetText() != '' && hdHG_CARD_NO.GetText() != '0') {
                                                        var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutHG4.aspx?date=' + Date()
                                                            + '&HG_CARD_NO=' + hdHG_CARD_NO.GetText()
                                                            + '&HG_REDEEM_POINT=' + HG_REDEEM_POINT.GetText()
                                                            + '&TOTAL_AMOUNT=' + TOTAL_AMOUNT.GetText()
                                                            + '&HG_LEFT_POINT=' + HG_LEFT_POINT.GetText()
                                                            , 500, 500);
                                                        if (r == '' || r == undefined) {
                                                            e.processOnServer = false;
                                                        } else {
                                                            e.processOnServer = true;
                                                        }
                                                    }
                                                    else {
                                                        e.processOnServer = true;
                                                    }
                                                    
                                                    if (confirm('是否有Happy Go卡累積點數？')) {
                                                        //進行刷卡累點
                                                        if (lbTOTAL_AMOUNT.GetText() != '' && lbTOTAL_AMOUNT.GetText() != '0') {
                                                            var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutHG5.aspx?date=' + Date()
                                                                + '&HG_CARD_NO=' 
                                                                + '&TOTAL_AMOUNT=' + lbTOTAL_AMOUNT.GetText() 
                                                                , 500, 500);
                                                            if (r == '' || r == undefined) {
                                                                e.processOnServer = false;
                                                            } else {
                                                                e.processOnServer = true;
                                                            }
                                                        }
                                                        else if (lbTOTAL_AMOUNT.GetText() == '0') {
                                                            e.processOnServer = true;
                                                        } else {
                                                            alert('應收金額為空白，不可結帳!');
                                                            e.processOnServer = false;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    </script>

                                </td>
                                <td>
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

                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnReprint" runat="server" Enabled="false" Text="<%$ Resources:WebResources, ReprintSalesSlip %>"
                                        OnClick="btnReprint_Click" UseSubmitBehavior="false">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <iframe id="fDownload" style="display: none" src="" runat="server"></iframe>
</asp:Content>
