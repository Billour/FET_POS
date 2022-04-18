<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PRE01.aspx.cs" Inherits="VSS_PRE_PRE01" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        //檢查商品料號是否存在
        function getProductInfo(s, e) {

            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '') {
                PageMethods.getProductInfo(s.GetText(), txtSTORE_NO.GetText(), getProductInfo_OnOK);
            }
        }

        function getProductInfo_OnOK(returnData) {
            var fName = "1_txtPRODNO_txtControl";

            //變更商品
            var txtPRODNAME = getClientInstance('TxtBox', _gvSender.name.replace(fName, "2_txtPRODNAME"));

            var txtUNIT_PRICE = getClientInstance('TxtBox', _gvSender.name.replace(fName, "4_txtUNIT_PRICE"));
            var txtQUANTITY = getClientInstance('TxtBox', _gvSender.name.replace(fName, "5_txtQUANTITY"));
            var txtAMOUNT = getClientInstance('TxtBox', _gvSender.name.replace(fName, "6_txtAMOUNT"));
            var OAMT = Number(txtAMOUNT.GetValue());    //舊值
            var NAMT = 0;                               //新值
            if (returnData == '') {
                alert("商品料號不存在!");
                _gvSender.SetValue(null);
                txtPRODNAME.SetText(null);
                txtUNIT_PRICE.SetText(0);
                txtAMOUNT.SetText(0);
            }
            else {
                var values = returnData.split(';');
                txtPRODNAME.SetText(values[1]);        //商品名稱
                txtUNIT_PRICE.SetValue(values[2]);  //單價
                if (txtQUANTITY.GetValue() != "" && txtQUANTITY.GetValue() != null) {
                    NAMT = Number(txtUNIT_PRICE.GetText()) * Number(txtQUANTITY.GetText());
                    txtAMOUNT.SetText(NAMT);
                } else {
                    txtAMOUNT.SetText("0");
                }
            }
            getAR_AMOUNT(NAMT, OAMT);
        }

        function getAR_AMOUNT(NAMT, OAMT) {
            var AMT = 0;
            AMT = Number(txtAR_AMOUNT.GetText().replace("應收總金額：", ""));
            AMT = AMT + NAMT - OAMT
            txtAR_AMOUNT.SetText("應收總金額：" + AMT);
        }


        function checkSaleQty(s, e) {
            _gvSender = s;

            var fName = "5_txtQUANTITY";
            var txtUNIT_PRICE = getClientInstance('TxtBox', s.name.replace(fName, "4_txtUNIT_PRICE"));
            var txtAMOUNT = getClientInstance('TxtBox', s.name.replace(fName, "6_txtAMOUNT"));
            var OAMT = Number(txtAMOUNT.GetValue());    //舊值
            var NAMT = 0;                               //新值
            if (isNaN(s.GetText())) {
                //s.SetText("1");
                //                alert('請輸入數字!');
                //                e.processOnServer = false;
                //                s.Focus();
            } else if (s.GetText() == '' || Number(s.GetText()) <= 0 || (!isInteger(s.GetText()))) {
                //                s.SetText("1");
                //                alert('數量只能為正整數!');
                //                e.processOnServer = false;
                //                s.Focus();
            } else if (txtUNIT_PRICE.GetValue() != "" && txtUNIT_PRICE.GetValue() != null) {
                NAMT = Number(txtUNIT_PRICE.GetText()) * Number(s.GetText());
                txtAMOUNT.SetText(NAMT);
            } else {
                txtAMOUNT.SetText("0");
            }
            getAR_AMOUNT(NAMT, OAMT);
        }
       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <asp:HiddenField ID="hidForcedInput" runat="server" Value="N" />
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--預購作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderOperation1 %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) { document.location='../PRE02/PRE02.aspx';return false; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--預購單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PreOrderSheetNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="txtPREPAY_NO" runat="server" />
                        </td>
                        <td class="tdtxt">
                            <!--客戶身份證號-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CustomerIdentifyNumber %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtID_NO" runat="server">
                                <ValidationSettings CausesValidation="false" ErrorText="">
                                    <RegularExpression ValidationExpression="^[A-Z0-9]{10}" ErrorText="請輸入身分證號數字十位" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--狀態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="txtPREPAY_STATUS" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--發票號碼-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="txtINVOICE_NO" runat="server" />
                        </td>
                        <td class="tdtxt">
                            <span style="color: Red">*</span><!--客戶姓名-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtCUST_NAME" runat="server" MaxLength="10">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="客戶姓名不允許空值，請重新輸入" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--更新日期-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblMODTM" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <span style="color: Red">*</span><!--發票抬頭-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtUNI_TITLE" runat="server">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="發票抬頭不允許空值，請重新輸入" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--客戶門號-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtMSISDN" runat="server">
                                <ValidationSettings>
                                    <RegularExpression ErrorText="客戶門號輸入錯誤" ValidationExpression="^[0-9]{0,10}" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--維護人員-->
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblMOUSER" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--統一編號-->
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtUNI_NO" runat="server">
                                <ValidationSettings CausesValidation="false" ErrorText="">
                                    <RegularExpression ValidationExpression="^[0-9]{8}" ErrorText="請輸入統一編號數字八位" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <span style="color: Red">*</span><!--聯絡電話-->
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtCONTACT_PHONE" runat="server">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="聯絡電話不允許空值，請重新輸入" />
                                    <RegularExpression ErrorText="聯絡電話輸入錯誤" ValidationExpression="^[0-9]{1,10}" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="hdPaidInfo" ClientInstanceName="hdPaidInfo" runat="server" ClientVisible="false" />
                            <dx:ASPxTextBox ID="txtSTORE_NO" ClientInstanceName="txtSTORE_NO" runat="server"
                                ClientVisible="false" />
                            <dx:ASPxTextBox ID="txtCOUNT" ClientInstanceName="txtCOUNT" runat="server" ClientVisible="false" />
                            <dx:ASPxTextBox ID="txtUUID" ClientInstanceName="txtUUID" runat="server" ClientVisible="false" />
                        </td>
                        <td class="tdval">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--啟用類型-->
                            <span style="color: Red">*</span><asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ActivationType %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="txtSTART_TYPE" runat="server" Width="100">
                                <Items>
                                    <dx:ListEditItem Value="1" Text="新啟用" Selected="true" />
                                    <dx:ListEditItem Value="2" Text="續約" />
                                    <dx:ListEditItem Value="3" Text="MNP" />
                                    <dx:ListEditItem Value="4" Text="其他" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            <!--e-Mail-->
                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Email %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <dx:ASPxTextBox ID="txtEMAIL" runat="server" Width="98%">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="SubEditBlock">
                <div class="GridScrollBar" style="height: auto">
                    <cc:ASPxGridView ClientInstanceName="gvMaster" ID="gvMaster" runat="server" Width="100%"
                        KeyFieldName="ID" OnHtmlFooterCellPrepared="gvMaster_HtmlFooterCellPrepared"
                        OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                        OnPageIndexChanged="gvMaster_PageIndexChanged">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                    </div>
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"
                                VisibleIndex="1">
                                <DataItemTemplate>
                                    <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup"
                                        KeyFieldValue1="salehouse" IsValidation="true" Text='<%# Bind("PRODNO") %>' OnClientTextChanged="getProductInfo" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="2">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtPRODNAME" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("PRODNAME") %>'
                                        runat="server">
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtID" ReadOnly="true" ClientVisible="false" Text='<%# Bind("ID") %>'
                                        runat="server">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="預收款說明" VisibleIndex="3">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtDESCRIPTION" AutoPostBack="false" Text='<%# Bind("DESCRIPTION") %>'
                                        MaxLength="50" runat="server" Width="100px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UNIT_PRICE" Caption="<%$ Resources:WebResources, PreOrderAmount %>"
                                VisibleIndex="4">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtUNIT_PRICE" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("UNIT_PRICE") %>'
                                        runat="server">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="QUANTITY" Caption="<%$ Resources:WebResources, Quantity %>"
                                VisibleIndex="5">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtQUANTITY" AutoPostBack="false" Text='<%# Bind("QUANTITY") %>'
                                        ClientSideEvents-TextChanged='function(s,e){ checkSaleQty(s,e);}' runat="server"
                                        Width="50px">
                                        <ValidationSettings CausesValidation="false" ErrorText="">
                                            <RegularExpression ValidationExpression="^\d*" ErrorText="請輸入正整數" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="AMOUNT" Caption="<%$ Resources:WebResources, TotalPrice %>"
                                VisibleIndex="6">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtAMOUNT" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("AMOUNT") %>'
                                        runat="server">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="REMARK" Caption="<%$ Resources:WebResources, RemarksLimitedTo50Chars %>">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtREMARK" AutoPostBack="false" Text='<%# Bind("REMARK") %>'
                                        MaxLength="50" runat="server" Width="100px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table cellpadding="0" cellspacing="0" border="0" align="left">
                                    <tr>
                                        <td align="right">
                                            <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                OnClick="btnNew_Click" />
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                ClientSideEvents-Click="function(s,e){                                              
                                                 if (!confirm('您確定要刪除[商品料號]資料嗎？')) {
                                                    e.processOnServer=false;
                                                 } 
                                             }" OnClick="btnDelete_Click" ClientInstanceName="btnDelete" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                            <FooterCell>
                                <dx:ASPxLabel ID="txtAR_AMOUNT" runat="server" />
                            </FooterCell>
                            <EmptyDataRow>
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                            </EmptyDataRow>
                        </Templates>
                        <Settings ShowTitlePanel="True" ShowFooter="True" />
                        <SettingsPager PageSize="2" />
                    </cc:ASPxGridView>
                </div>
            </div>
            <div class="seperate">
            </div>
            <div>
                <div class="SubEditBlock">
                    <div class="GridScrollBar" style="height: auto">
                        <cc:ASPxGridView ID="gvPAID" ClientInstanceName="gvPAID" runat="server" Width="100%"
                            OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" KeyFieldName="ID"
                            OnHtmlFooterCellPrepared="gvPAID_HtmlFooterCellPrepared" OnHtmlRowPrepared="gvPAID_HtmlRowPrepared"
                            OnPageIndexChanged="gvPAID_PageIndexChanged">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                        </div>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="PAID_MODE_NAME" Caption="<%$ Resources:WebResources, PaymentMethod %>"
                                    VisibleIndex="1">
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="txtPAID_MODE_NAME" ReadOnly="true" Border-BorderStyle="None"
                                            Text='<%# Bind("PAID_MODE_NAME") %>' runat="server">
                                        </dx:ASPxTextBox>
                                        <dx:ASPxTextBox ID="txtID1" ReadOnly="true" ClientVisible="false" Text='<%# Bind("ID") %>'
                                            runat="server">
                                        </dx:ASPxTextBox>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="PAID_AMOUNT" Caption="<%$ Resources:WebResources, Amount %>"
                                    VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="<%$ Resources:WebResources, PaymentStatement %>"
                                    VisibleIndex="3" />
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <table cellpadding="0" cellspacing="0" border="0" align="left">
                                        <tr>
                                            <td align="right">
                                                <dx:ASPxButton ID="btnCash" runat="server" Text="<%$ Resources:WebResources, Cash %>"
                                                    UseSubmitBehavior="false" OnClick="btnPay_Click">
                                                    <ClientSideEvents Click="function(s,e){gvPAID.SelectAllRowsOnPage(false);OpenWindow(s,e,'CheckOutCash',400,200);}" />
                                                </dx:ASPxButton>

                                                <script type="text/javascript">
                                                    function OpenWindow(s, e, p, l, w) {
                                                        hdPaidInfo.SetText(null);
                                                        var r = '';
                                                        r = openDialogWindowByEncrypt('../../CheckOut/' + p + '.aspx?date=' + Date(), l, w);
                                                        if (r == '' || r == undefined) {
                                                            e.processOnServer = false;
                                                        } else {
                                                            hdPaidInfo.SetText(r);
                                                            e.processOnServer = true;
                                                        }
                                                    }
                                                </script>

                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <dx:ASPxButton ID="btnCredit" runat="server" Text="<%$ Resources:WebResources, CreditCard %>"
                                                    UseSubmitBehavior="false" OnClick="btnPay_Click">
                                                    <ClientSideEvents Click="function(s, e){ gvPAID.SelectAllRowsOnPage(false);OpenWindow(s,e,'CheckBackCredit',400,300);}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <dx:ASPxButton ID="btnVisaDebit" runat="server" Text="<%$ Resources:WebResources, BankCard %>"
                                                    UseSubmitBehavior="false" AutoPostBack="false" OnClick="btnPay_Click">
                                                    <ClientSideEvents Click="function(s, e){gvPAID.SelectAllRowsOnPage(false);OpenWindow(s,e,'CheckOutDebitCard',400,300);}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <dx:ASPxButton ID="btnOffLineCredit" runat="server" Text="<%$ Resources:WebResources, OffLineCreditCard %>"
                                                    UseSubmitBehavior="false" OnClick="btnPay_Click">
                                                    <ClientSideEvents Click="function(s, e){gvPAID.SelectAllRowsOnPage(false); OpenWindow(s,e,'CheckOutCreditUnline',400,200);}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <dx:ASPxButton ID="btnPayDel" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                    ClientSideEvents-Click="function(s,e){                                              
                                                 if (!confirm('您確定是要刪除[付款方式]資料嗎？')) {
                                                    e.processOnServer=false;}}" OnClick="btnPayDel_Click" ClientInstanceName="btnPayDel"
                                                    CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                                <FooterCell>
                                    <dx:ASPxLabel ID="txtPA_AMOUNT" runat="server" />
                                </FooterCell>
                                <EmptyDataRow>
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal></EmptyDataRow>
                            </Templates>
                            <Settings ShowTitlePanel="True" />
                        </cc:ASPxGridView>
                    </div>
                </div>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td align="right">
                            <dx:ASPxButton ID="BtnCheckOut" runat="server" Text="<%$ Resources:WebResources, CheckOut %>"
                                OnClick="BtnCheckOut_Click">
                                <ClientSideEvents Click="function(s,e)
                                    {
                                        if (!confirm('您確定要進行結帳嗎？')) 
                                        {
                                            e.processOnServer=false;
                                        } 
                        }" />
                            </dx:ASPxButton>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="BtnCel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                CausesValidation="false" ClientSideEvents-Click="function(s,e){                                              
                                                 if (!confirm('您確定要取消[預收單號]交易資料嗎？')) {
                                                    e.processOnServer=false;
                                                 } 
                                             }" OnClick="BtnCel_Click" />
                            <%--刪除當筆HEAD的CACHE資料--%>
                            <dx:ASPxButton ID="BtnCelALL" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                ClientInstanceName="BtnCelALL" ClientVisible="false" CausesValidation="false"
                                OnClick="BtnCelALL_Click" />
                            <%--刪除當日的CACHE資料--%>
                            <dx:ASPxButton ID="btnPayDelALL" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                ClientVisible="false" OnClick="btnPayDelALL_Click" ClientInstanceName="btnPayDelALL"
                                CausesValidation="false" />
                            <%--刪除當筆的CACHE資料--%>
                            <dx:ASPxButton ID="btnPayDel1" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                ClientVisible="false" OnClick="btnPayDel1_Click" ClientInstanceName="btnPayDel1"
                                CausesValidation="false" />
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="BtnCelOrd" runat="server" Text="<%$ Resources:WebResources, CancelPreOrder %>"
                                CausesValidation="false" ClientSideEvents-Click="function(s,e){                                              
                                                 if (!confirm('您確定要作廢[預收單號]交易資料嗎？')) {
                                                    e.processOnServer=false;
                                                 } 
                                             }" OnClick="BtnCelOrd_Click" />
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="BtnPri" runat="server" Text="<%$ Resources:WebResources, PrintPreOrderVoucher %>"
                                OnClick="BtnPri_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <iframe id="fDownload" style="display: none" src="" runat="server" width="100%" height="100%">
    </iframe>

    <script type="text/javascript">
        function confirmPaidnput(k) {
            if (confirm(k)) {
                $("#hidForcedInput").value = "Y";
                btnPayDel1.SendPostBack('Click');
            }
        }

        function confirmPaidBtnCel(k) {
            if (confirm(k)) {
                $("#hidForcedInput").value = "Y";
                BtnCelALL.SendPostBack('Click');
            }
        }

        //筆數大於零代表有未結資料
        if (Number(txtCOUNT.GetText()) > 0) {
            txtCOUNT.SetText("");
            if (confirm('有未結信用卡資料請先刷退?')) {
                $("#hidForcedInput").value = "Y";
                btnPayDelALL.SendPostBack('Click');
            }
        }
    </script>

</asp:Content>
