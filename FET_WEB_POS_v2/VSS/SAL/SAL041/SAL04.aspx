<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SAL04.aspx.cs" Inherits="VSS_SAL_SAL04" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../Style/FormBox.css" rel="stylesheet" type="text/css" />
    
    <link href="../../../Style/screen.css" rel="stylesheet" type="text/css" />
    
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>


<%-- jQuery box    --%>
    <script src="../../../ClientUtility/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/jquery.corner.js" type="text/javascript"></script>
    
    <script src="../../../ClientUtility/jquery-impromptu.3.1.min.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/FormButtonCommon.js" type="text/javascript"></script>
<%-- jQuery box  --%>

    <script type="text/javascript" language="javascript">   
        var ETCProdNo = "";
        var ETCProdNoExist = false;
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

            if (gvMaster != undefined) {
                for (var i = 0; i < gvMaster.pageRowCount; i++) {
                    var gvRowName = "ctl00_MainContentPlaceHolder_gvMaster_cell" + i;
                    var divIMEI = window.document.all[gvRowName + "_10_divIMEI"]; 
                    var itemStatus = getClientInstance('TxtBox', gvRowName + "_1_txtITEM_STATUS");
                    
                    if (itemStatus.GetText() != "作廢" && divIMEI.style.visibility != "hidden") {
                        var lblIMEI_QTY = getClientInstance('TxtBox', gvRowName + "_10_lbIMEI_QTY");
                        var QUANTITY = getClientInstance('TxtBox', gvRowName + "_6_txtQUANTITY");
                        if (lblIMEI_QTY.IsVisible()) {
                            if (lblIMEI_QTY.GetText() != QUANTITY.GetText()) {   //檢查商品數量及IMEI數量一不一致
                                alert("請確認IMEI數量!!");
                                e.processOnServer = false;
                                return false;
                            }
                        }
                    }

                    var unitPrice = getClientInstance('TxtBox', gvRowName + "_7_txtUNIT_PRICE");
                    if (itemStatus.GetText() != "作廢" && isNaN(unitPrice.GetText())) {
                        alert("無單價無法結帳!!");
                        e.processOnServer = false;
                        return false;
                    }
                }
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
                    var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutHG2.aspx?date=' + Date()
                        + '&CREDIT_CARD_NO=' + values[2]
                        + '&PAID_AMOUNT=' + values[4]
                         , 500, 500);
                    if (r == '' || r == undefined) {
                        gvCheckOut.SelectRowsByKey(values[0], false);
                    }
                    else {
                        if (r == "fail") {
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

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<OBJECT ID="detect"
CLASSID="CLSID:7BACE1A5-F435-45DB-BFB3-23B5AE9069F1"
CODEBASE="detctPrinter.CAB#version=1,0,0,1">
</OBJECT>
    <div id="tooltip"></div>
   
    <div class="func1">
        <div class="titlef">
            <!--銷售作廢作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, SalesCancel %>" />
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--作廢序號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CancelNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblCancelNo" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--作廢日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, CancelDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblCancelDate" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                </tr>
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
                        <dx:ASPxLabel ID="lblVOUCHER_TYPE_VALUE" runat="server" Text="">
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
                        <asp:Literal ID="lblINVOICE_NO" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lbINVOICE_NO" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--統一編號-->
                        <asp:Literal ID="lblUNI_NO" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblUNI_NO_VALUE" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="lblMODI_USER" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lbMODI_USER" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--發票抬頭-->
                        <asp:Literal ID="lblUNI_TITLE" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblUNI_TITLE_VALUE" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--備註-->
                        <asp:Literal ID="lblREMARK" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <dx:ASPxLabel ID="lblREMARK_VALUE" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        HG卡號：
                    </td>
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
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
            <asp:HiddenField ID="PrintName" runat="server" />
            <asp:HiddenField ID="hidETCProdNo" runat="server" />
            <asp:HiddenField ID="hidAddProdList" runat="server" />
            <dx:ASPxTextBox ID="hdPaidInfo" ClientInstanceName="hdPaidInfo" runat="server" ClientVisible="false"></dx:ASPxTextBox>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="ID"
                        AutoGenerateColumns="False" Width="100%" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                        OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="ITEM_TYPE_NAME" Caption="<%$ Resources:WebResources, Category %>" VisibleIndex="0"/>
                            <dx:GridViewDataColumn FieldName="PROMO_NAME" Caption="<%$ Resources:WebResources, PromotionName %>" VisibleIndex="1"/>
                            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="2">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="hdOldPRODNO" runat="server" Text='<%# Bind("[PRODNO]") %>' ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hdIMEIFlag" runat="server" Text='' ClientVisible="false"></dx:ASPxTextBox>
                                    <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup" KeyFieldValue1="salehouse"
                                        IsValidation="true" Text='<%# Bind("[PRODNO]") %>' OnClientTextChanged="function(s, e) { getPRODINFO(s,e); }" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="3">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtPRODNAME" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[PRODNAME]") %>'
                                        runat="server">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="QUANTITY" Caption="<%$ Resources:WebResources, Quantity %>" VisibleIndex="4">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="hdOldQUANTITY" runat="server" ClientVisible="false" Text='<%# Bind("[QUANTITY]") %>'>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtQUANTITY" AutoPostBack="false" ClientSideEvents-TextChanged='function(s,e){ checkSaleQty(s,e);getPROD_IMEI_COUNT(s,e,"6_txtQUANTITY");}'
                                        Text='<%# Bind("[QUANTITY]") %>' runat="server" Width="50px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UNIT_PRICE" Caption="<%$ Resources:WebResources, UnitPrice %>" VisibleIndex="5">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtUNIT_PRICE" Text='<%# Bind("[UNIT_PRICE]") %>' ReadOnly="true"
                                        Border-BorderStyle="None" runat="server" Width="80px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="TOTAL_AMOUNT" Caption="<%$ Resources:WebResources, TotalPrice %>" VisibleIndex="6">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtTOTAL_AMOUNT" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[TOTAL_AMOUNT]") %>'
                                        runat="server" Width="40px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI_QTY" Caption="<%$ Resources:WebResources, Imei %>" VisibleIndex="7">
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
                                                    <dx:ASPxTextBox ID="txtIMEI" runat="server" ReadOnly="true" Border-BorderStyle="None"
                                                            Width="150px" DisabledStyle-Font-Underline="true">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                        <Settings ShowTitlePanel="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                    </cc:ASPxGridView>
                    <div class="criteria" id="divpay" runat="server">
                        <table>
                            <tr>
                                <td class="tdtxt">
                                    <!--已收總金額-->
                                    <asp:Literal ID="Literal22" runat="server" Text="已收總金額"></asp:Literal>：
                                </td>
                                <td class="tdval">
                                    <dx:ASPxLabel ID="lbTOTAL_AMOUNT" ClientInstanceName="lbTOTAL_AMOUNT" runat="server"
                                        Text="">
                                    </dx:ASPxLabel>
                                    <!--HappyGo折抵資訊 -->
                                    <!--兌換點數 -->
                                    <dx:ASPxTextBox ID="hdHG_REDEEM_POINT" ClientInstanceName="HG_REDEEM_POINT" runat="server"
                                        ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <!--HappyGo卡號 -->
                                    <dx:ASPxTextBox ID="hdHG_CARD_NO" ClientInstanceName="HG_CARD_NO" runat="server"
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
                                </td>
                                <td class="tdtxt">
                                    &nbsp;
                                </td>
                                <td class="tdval">
                                    &nbsp;
                                </td>
                                <td class="tdtxt">
                                    &nbsp;
                                </td>
                                <td class="tdval">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="seperate">
                    </div>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvDetail" runat="server" EnableCallBacks="true" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="ID" Visible="false" onpageindexchanged="gvDetail_PageIndexChanged">
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
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                        </cc:ASPxGridView>
                    </div>
                </div>
                <div class="seperate">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <cc:ASPxGridView ID="gvCheckOut" ClientInstanceName="gvCheckOut" runat="server" EnableCallBacks="true"
                        AutoGenerateColumns="false" Width="100%" KeyFieldName="ID" OnHtmlRowPrepared="gvCheckOut_HtmlRowPrepared">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="PAID_MODE_NAME" Caption="<%$ Resources:WebResources, PaymentMethod %>">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PAID_AMOUNT" Caption="<%$ Resources:WebResources, Amount %>">
                                <DataItemTemplate>
                                    <dx:ASPxLabel ID="lbPAID_AMOUNT" runat="server" Text='<%# Bind("[PAID_AMOUNT]") %>'>
                                    </dx:ASPxLabel>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <%-- 付款明細--%>
                            <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="<%$ Resources:WebResources, PaymentStatement %>">
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
                        <Settings ShowTitlePanel="True" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    </cc:ASPxGridView>
                    <div style="text-align: left">
                        <table cellpadding="0" width="100%">
                            <tr>
                                <td style="width: 70%">
                                    <!--應退總金額-->
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, TotalRefundDue%>"></asp:Literal>：
                                    <dx:ASPxLabel ID="lbPayAmount" ClientInstanceName="lbPayAmount" runat="server">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;
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
                                <dx:ASPxButton ID="btnConfirmCancel" ClientInstanceName ="btnConfirmCancel1" runat="server" Text="<%$ Resources:WebResources, ConfirmCancel %>"
                                    OnClick="btnConfirmCancel_Click" ClientSideEvents-Click="function(s,e){                                              
                                                 if (!confirm('確定作廢?'))
                                                    e.processOnServer=false;
                                                 else {
                                                    s.SendPostBack('Click');
                                                    s.SetEnabled(false);
                                                 }
                                             }" UseSubmitBehavior="false">
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="Invalid" ClientInstanceName ="Invalid" ClientVisible ="false"  runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                    OnClick="Save1">
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="Discount" ClientInstanceName ="Discount" ClientVisible ="false"  runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                    OnClick="Save1">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>   
    <iframe id="fDownload" style="display:none" src="" runat="server"></iframe>
</asp:Content>
