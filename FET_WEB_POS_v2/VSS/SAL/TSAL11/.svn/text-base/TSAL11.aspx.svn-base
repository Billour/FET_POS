<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TSAL11.aspx.cs" Inherits="VSS_SAL_TSAL11_TSAL11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
	<script src="../../../ClientUtility/jquery.checkboxes.js" type="text/javascript"></script>
	<script src="../../../ClientUtility/jquery-ui-custom-min.js" type="text/javascript"></script>
	<script src="../../../ClientUtility/jquery-ui-numeric-min.js" type="text/javascript"></script>
	<script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
	<script src="../TSAL01/scripts/TSAL01_Common.js" type="text/javascript"></script>
	<script src="scripts/TSAL11_AJAX.js" type="text/javascript"></script>
	<script src="scripts/TSAL11_SaleHead.js" type="text/javascript"></script>
    <script src="scripts/TSAL11_Master.js" type="text/javascript"></script>
	<script src="../TSAL01/scripts/TSAL01_GridRow.js" type="text/javascript"></script>
	<script src="scripts/TSAL11_Checkout.js" type="text/javascript"></script>
	<script src="../TSAL01/scripts/TSAL01_Discount.js" type="text/javascript"></script>
	<script src="../TSAL01/scripts/TSAL01_HappyGo.js" type="text/javascript"></script>
    
    <style>
		.grid_header
		{
			background-color: #FC9;
			color: Black;
			text-align: center;
		}
		.grid_row_0
		{
			background-color: #fffbdd;
			color: Black;
		}
		.grid_row_1
		{
			background-color: #fffbdd;
			color: Black;
		}
		.gridTextBox
		{
			border: 1px solid #808080;
		}
		.gridLabelCell
		{
			width: 200px;
			overflow: hidden;
			text-overflow: ellipsis;
			white-space: nowrap;
		}
		.gridNumberCell
		{
			width: 60px;
			white-space: nowrap;
			text-align: right;
		}
		.buttonStyle
		{
			border-width: 1px;
			height: 24px;
		}
		.chooseButton
		{
			border: 1px solid #404040;
			height: 20px;
		}
		.textBoxStyle
		{
			border: 1px solid #808080;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <asp:HiddenField ID="hfPOSUUID_MASTER" runat="server" />
	<asp:HiddenField ID="hfOriginal_MASTER" runat="server" />
	<asp:HiddenField ID="hfPOSUUID_DETAIL" runat="server" />
	<asp:HiddenField ID="hfActionType" runat="server" Value="0" />
	<asp:HiddenField ID="hfSALE_TYPE" runat="server" Value="1" />
	<div>
		<table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
			<tr>
				<td align="left">
					<!--交易補登-->
					<asp:Label ID="labTitle" runat="server" Text="交易補登(測)"></asp:Label>
				</td>
				<td align="right">
					<table cellpadding="0" cellspacing="0" border="0">
						<tr>
							<td>
								<asp:Button runat="server" ID="btnUnClose" Text="<%$ Resources:WebResources, UnclearedTradeListing %>"
									OnClick="btnUnClose_Click" jqKey="btnUnClose" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div class="criteria">
		<table id="tabHeader" jqkey="tabHeader">
			<tr>
				<td class="tdtxt">
					<!--交易序號-->
					<asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />：
				</td>
				<td class="tdval">
					<span id="labSALE_NO" runat="server" jqkey="labSALE_NO" />
				</td>
				<td class="tdtxt">
					<!--單據類別-->
					<asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SalesSlipType %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<!--發票/收據-->
					<span id="labVOUCHER_TYPE" runat="server" jqkey="labVOUCHER_TYPE">二聯式發票</span>
				</td>
				<td class="tdtxt">
					<!--狀態-->
					<asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<span id="labSale_Status" runat="server" jqkey="labSale_Status">交易補登未結帳</span>
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					<!--交易日期-->
					<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<span id="labTRADE_DATE" runat="server" jqkey="labTRADE_DATE" />
				</td>
                <td class="tdtxt">
                    <!--發票日期-->
                    <asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:WebResources, InvoiceDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxDateEdit ID="txtInv_Date" runat="server" jqkey="txtInv_Date">
                    </dx:ASPxDateEdit>
                </td>
				<td class="tdtxt">
					<!--更新日期-->
					<asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<asp:Label ID="labMODI_DTM" runat="server" Text="" jqKey="labMODI_DTM"></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					<!--發票號碼-->
					<asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<asp:TextBox ID="txtInvoice_No" runat="server" CssClass="tbWidthFormat" MaxLength="10"
						Columns="10" jqKey="txtInvoice_No"></asp:TextBox>
				</td>
				<td class="tdtxt">
					<!--發票格式-->
					<asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, InvoiceFormat %>"></asp:Literal>：
				</td>
				<td class="tdval">
				    <input type="radio" runat="server" name="INVOICE_TYPE" id="rbINVOICE_TYPE2" jqKey="rbINVOICE_TYPE2" checked onclick="checkrbINVOICE_TYPE()" />二聯式
				    <input type="radio" runat="server" name="INVOICE_TYPE" id="rbINVOICE_TYPE3" jqKey="rbINVOICE_TYPE3" onclick="checkrbINVOICE_TYPE()" />三聯式
				    <input type="radio" runat="server" name="INVOICE_TYPE" id="rbINVOICE_TYPEno" jqKey="rbINVOICE_TYPEno" onclick="checkrbINVOICE_TYPE()" />無
				</td>
				<td class="tdtxt">
					<!--更新人員-->
					<asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<asp:Label ID="labMODI_USER" runat="server" Text="" jqKey="labMODI_USER">
					</asp:Label>
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					<!--發票抬頭-->
					<asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<asp:TextBox ID="txtUNI_TITLE" runat="server" CssClass="tbWidthFormat" Columns="25"
						jqKey="txtUNI_TITLE"></asp:TextBox>
				</td>
				<td class="tdtxt">
					<!--統一編號-->
					<asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<asp:TextBox ID="txtUNI_NO" runat="server" CssClass="tbWidthFormat" MaxLength="8"
						Columns="10" jqKey="txtUNI_NO"></asp:TextBox>
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
				</td>
				<td>
				</td>
				<td class="tdtxt">
					<!--備註-->
					<asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
				</td>
				<td class="tdval" colspan="3">
					<asp:TextBox ID="txtREMARK" runat="server" CssClass="tbWidthFormat" Columns="60"
						jqKey="txtREMARK"></asp:TextBox>
				</td>
				<td colspan="2" id="divMasterMsg" style="color: Red; white-space:nowrap;" align="right"></td>
			</tr>
		</table>
	</div>
	<div style="border: 1px solid #FC9; min-height: 100px;">
		<div style="background-color: #FC9;">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="3" border="0" align="left">
							<tr>
								<td>
									<input type="button" id="btnAddItem" name="btnAddItem" runat="server" value="<%$ Resources:WebResources, Add %>"
										onclick="createItemRow();" disabled="disabled" jqkey="btnAddItem" style="margin-right: 5px;" />
								</td>
								<td>
									<input type="button" id="btnDeleteItems" name="btnDeleteItems" runat="server" value="<%$ Resources:WebResources, Delete %>"
										onclick="deleteItemRow();" disabled="disabled" jqkey="btnDeleteItems" style="margin-right: 5px;" />
								</td>
								<!--
								<td>
									<input type="button" id="btnAddProd" name="btnAddProd" runat="server" value="加價購"
										onclick="addProd();" disabled="disabled" jqkey="btnAddProd" style="margin-right: 5px;" />
								</td>
								<td>
									<input type="button" id="btnOrdToSale" name="btnOrdToSale" runat="server" value="預收轉銷售"
										onclick="orderToSale();" disabled="disabled" jqkey="btnOrdToSale" style="margin-right: 5px;" />
								</td>
								-->
								<td>
									<input type="button" id="btnMixPromotion" name="btnMixPromotion" runat="server" value="<%$ Resources:WebResources, MixPromotion %>"
										onclick="mixPromotion();" disabled="disabled" jqkey="btnMixPromotion" style="margin-right: 5px;" />
								</td>
								<!--
								<td>
									<input type="button" id="btnAddETC" name="btnAddETC" runat="server" value="<%$ Resources:WebResources, ETCADD %>"
										onclick="addETCItem();" disabled="disabled" jqkey="btnAddETC" style="margin-right: 5px;" />
								</td>
								<td>
									<input type="button" id="btnETCCard" name="btnETCCard" value="ETC加值卡"
										onclick="addETCCard();" disabled="disabled" jqkey="btnETCCard" style="margin-right: 5px;" />
								</td>
								<td>
									<input type="button" id="btnHappyGoNet" name="btnHappyGoNet" runat="server" value="HappyGo折抵"
										onclick="happyGoDiscount();" disabled="disabled" jqkey="btnHappyGoNet" style="margin-right: 5px;" />
								</td>
								<td>
									<input type="button" id="btnStoreDiscount" name="btnStoreDiscount" value="特殊抱怨折扣"
										onclick="storeDiscount();" disabled="disabled" jqkey="btnStoreDiscount" style="margin-right: 5px;" />
								</td>
								-->
								<td>
									<input type="button" id="btnCommunications" name="btnCommunications" value="授信通聯"
										onclick="communications();" disabled="disabled" jqkey="btnCommunications" style="margin-right: 5px;" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="gvMaster" name="gvMaster" cellpadding="2" cellspacing="2" border="0" width="100%">
							<tr style="height: 24px;">
								<td class="grid_header" style="width: 20px;">
									<input type="checkbox" id="chkAllRow" onclick="checkAllCheckboxs();" />
								</td>
								<td class="grid_header" style="width: 30px; display:none;" jqKey="ChangeProdCell">
									&nbsp;
								</td>
								<td class="grid_header" style="width: 30px;">
									<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Category %>" />
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Quantity %>" />
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, UnitPrice %>" />
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal17" runat="server" Text="小計" />
								</td>
								<td class="grid_header">
									&nbsp;
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Imei %>" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div class="txt" style="text-align: left">
		<!--應收總金額-->
		<table width="100%" border="0">
			<tr>
				<td class="tdtxt" style="width: 33.3%; text-align: left;">
					<!--應收總金額-->
					<asp:Literal ID="Literal30" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>"></asp:Literal>：<span
						id="labTotalAmount">0</span>
				</td>
				<td class="tdtxt" jqkey="ChangeProdCell" style="display: none;width: 33.3%; text-align: left;">
					原交易金額 ：<span id="labOrigTotal">0</span>
				</td>
				<td class="tdtxt" jqkey="ChangeProdCell" style="display: none;width: 33.3%; text-align: left;">
					<span id="labDiffText">應補金額</span>：<span id="labDiffValue">0</span>
				</td>
			</tr>
		</table>
	</div>
	<p />
	<div id="divDiscount" style="display: none;">
		<asp:Label ID="Label8" Text="<%$ Resources:WebResources, DiscountDetail %>" runat="server" />
		<div style="background-color: #FC9;">
			<table id="gvDiscount" name="gvDiscount" cellpadding="2" cellspacing="2" border="0"
				width="100%">
				<tr style="height: 24px;">
					<td class="grid_header" style="width: 30px;">
						<asp:Literal ID="Literal33" runat="server" Text="<%$ Resources:WebResources, Items %>" />
					</td>
					<td class="grid_header">
						<asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>" />
					</td>
					<td class="grid_header">
						<asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, DiscountName %>" />
					</td>
					<td class="grid_header">
						<asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, Quantity %>" />
					</td>
					<td class="grid_header">
						<asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, UnitPrice %>" />
					</td>
					<td class="grid_header">
						<asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, TotalPrice %>" />
					</td>
				</tr>
			</table>
		</div>
		<p />
	</div>
	<div class="btnPosition">
		<input type="button" id="btnConfirm" name="btnConfirm" disabled="disabled" runat="server"
			value="<%$ Resources:WebResources, Confirm %>" onclick="confirmItems();" jqkey="btnConfirm" />
		<input type="button" id="btnCancel" name="btnCancel" disabled="disabled" runat="server"
			value="<%$ Resources:WebResources, Cancel %>" onclick="cancelItems();" jqkey="btnCancel" />
	</div>
	<p />
	<div style="border: 1px solid #FC9; min-height: 100px;">
		<div style="background-color: #FC9;">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="3" border="0" align="left">
							<tr>
								<td>
									<!-- 現金 -->
									<input type="button" id="btnCash" name="btmCash" disabled="disabled" runat="server"
										value="<%$ Resources:WebResources, Cash %>" onclick="paidCash(this);" jqkey="btnCash"
										style="margin-right: 5px;" jqMode="1" />
								</td>
								<!--
								<td>
									//信用卡
									<input type="button" id="btnCreditCard" name="btnCreditCard" disabled="disabled"
										runat="server" value="<%$ Resources:WebResources, CreditCard %>" onclick="creditCard(this);"
										jqkey="btnCreditCard" style="margin-right: 5px;" jqMode="2" />
								</td>
								<td>
									//信用卡分期
									<input type="button" id="btnInstalment" name="btnInstalment" disabled="disabled"
										runat="server" value="<%$ Resources:WebResources, Instalment %>" onclick="instalment(this);"
										jqkey="btnInstalment" style="margin-right: 5px;" jqMode="4" />
								</td>
								<td style="display: none;">
									//金融卡
									<input type="button" id="btnBankCard" name="btnBankCard" disabled="disabled" runat="server"
										value="<%$ Resources:WebResources, BankCard %>" onclick="bankCard(this);" jqkey="btnBankCard"
										style="margin-right: 5px;" jqMode="6" />
								</td>
								<td>
									//HappyGo
									<input type="button" id="btnHappyGo" name="btnHappyGo" disabled="disabled" runat="server"
										value="<%$ Resources:WebResources, HappyGo %>" onclick="happyGo(this);" jqkey="btnHappyGo"
										style="margin-right: 5px;" jqMode="7" />
								</td>
								<td style="display: none;">
									//禮卷
									<input type="button" id="btnCoupon" name="btnCoupon" disabled="disabled" runat="server"
										value="<%$ Resources:WebResources, Coupon %>" onclick="coupon(this);" jqkey="btnCoupon"
										style="margin-right: 5px;" jqMode="5" />
								</td>
								-->
								<td>
									<!-- 離線信用卡 -->
									<input type="button" id="btnOffLineCreditCard" name="btnOffLineCreditCard" disabled="disabled"
										runat="server" value="<%$ Resources:WebResources, OffLineCreditCard %>" onclick="offLineCreditCard(this);"
										jqkey="btnOffLineCreditCard" style="margin-right: 5px;" jqMode="3" />
								</td>
								<td>
									<!-- 刪除 -->
									<input type="button" id="btnDeleteCheckout" name="btnDeleteCheckout" disabled="disabled"
										runat="server" value="<%$ Resources:WebResources, Delete %>" onclick="deleteCheckoutRow();"
										jqkey="btnDeleteCheckout" style="margin-right: 5px;" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="gvCheckout" name="gvCheckout" cellpadding="2" cellspacing="2" border="0"
							width="100%">
							<tr style="height: 24px;">
								<td class="grid_header" style="width: 20px;">
									<input type="checkbox" id="chkAllCheckout" onclick="checkAllCheckoutItems();" />
								</td>
								<td class="grid_header" style="width: 30px; display:none;" jqKey="ChangeProdCell">
									&nbsp;
								</td>
								<td class="grid_header" style="width:10%;">
									<asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>" />
								</td>
								<td class="grid_header" style="width:10%;">
									<asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, Amount %>" />
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal20" runat="server" Text="付款明細" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div>
		<table cellpadding="0" cellspacing="0" border="0" width="100%">
			<tr>
				<td style="width: 60%">
					<!--應付總金額-->
					<asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, TotalAmountDue %>"></asp:Literal>：
					<span id="labTotalPayable">0</span>
				</td>
				<td style="width: 40%">
					<!--找零-->
					<asp:Literal ID="Literal24" runat="server" Text="找零"></asp:Literal>： <span id="labChange">
						0</span>
				</td>
			</tr>
			<tr>
				<td align="center" colspan="2">
					<div class="btnPosition">
						<input type="button" id="btnCheckout" name="btnCheckout" disabled="disabled" runat="server"
							value="<%$ Resources:WebResources, CheckOut %>" jqkey="btnCheckout" />
						<input type="button" id="btnCancelTransaction" name="btnCancelTransaction" runat="server" visible = "false"
							value="<%$ Resources:WebResources, CancelTransaction %>" jqkey="btnCancelTransaction" />
							<input type="button" id="btnReprint" name="btnReprint" runat="server" visible = "false"
							value="<%$ Resources:WebResources, ReprintSalesSlip %>" jqkey="btnReprint" />
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div style="width: 0px; height: 0px;">
		<iframe id="pdfFrame" height="0px" width="0px" visible="false"></iframe>
	</div>
</asp:Content>

