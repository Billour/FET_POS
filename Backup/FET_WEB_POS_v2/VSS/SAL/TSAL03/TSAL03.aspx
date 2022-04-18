<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="TSAL03.aspx.cs" Inherits="VSS_SAL_TSAL03_TSAL03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

	<script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

	<script src="../../../ClientUtility/jquery.checkboxes.js" type="text/javascript"></script>

	<script src="../../../ClientUtility/jquery-ui-custom-min.js" type="text/javascript"></script>

	<script src="../../../ClientUtility/jquery-ui-numeric-min.js" type="text/javascript"></script>

	<script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

	<script src="../TSAL01/Scripts/TSAL01_Common.js" type="text/javascript"></script>

	<script src="../TSAL01/Scripts/TSAL01_HappyGo.js" type="text/javascript"></script>

	<script src="scripts/TSAL03_AJAX.js" type="text/javascript"></script>

	<script src="scripts/TSAL03_Checkout.js" type="text/javascript"></script>

	<script src="scripts/TSAL03_Master.js" type="text/javascript"></script>

	<script src="scripts/TSAL03_Discount.js" type="text/javascript"></script>

	<style>
		.grid_header
		{
			background-color: #CCCCCC;
			color: Black;
			text-align: center;
		}
		.grid_row_0
		{
			background-color: #CCCCCC;
			color: Black;
		}
		.grid_row_1
		{
			background-color: #EEEEEE;
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
	<asp:HiddenField ID="hfPOSUUID_MASTER" runat="server" />
	<asp:HiddenField ID="hfOriginal_MASTER" runat="server" />
	<asp:HiddenField ID="hfPOSUUID_DETAIL" runat="server" />
	<asp:HiddenField ID="hfGetOrigItems" runat="server" Value="0" />
	<div>
		<table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
			<tr>
				<td align="left">
					<!--換貨作業-->
					<asp:Label ID="labTitle" runat="server" Text="換貨作業(測)"></asp:Label>
				</td>
				<td align="right">
					<table cellpadding="0" cellspacing="0" border="0">
						<tr>
							<td>
								<asp:Button runat="server" ID="btnSearch" Text="<%$ Resources:WebResources, Search %>"
									OnClick="btnSearch_Click" jqKey="btnSearch" />
							</td>
							<td>
								&nbsp;
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
					<span id="lbSALE_NO" runat="server" jqkey="lbSALE_NO" />
				</td>
				<td class="tdtxt">
					<!--單據類別-->
					<asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SalesSlipType %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<!--發票/收據-->
					<span id="labVOUCHER_TYPE" runat="server" jqkey="labVOUCHER_TYPE">連線電子計算機發票</span>
				</td>
				<td class="tdtxt">
					<!--狀態-->
					<asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<span id="labSale_Status" runat="server" jqkey="labSale_Status">換貨未結帳</span>
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
				<td>
				</td>
				<td>
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
					<span id="labInvoice_No"></span>
				</td>
				<td class="tdtxt">
					<!--統一編號-->
					<asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<asp:TextBox ID="txtUNI_NO" runat="server" CssClass="tbWidthFormat" MaxLength="8"
						Columns="10" jqKey="txtUNI_NO"></asp:TextBox>
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
					<!--備註-->
					<asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
				</td>
				<td class="tdval" colspan="3">
					<asp:TextBox ID="txtREMARK" runat="server" CssClass="tbWidthFormat" Columns="60"
						jqKey="txtREMARK"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					HG卡號：
				</td>
				<td>
					<span id="labHG_CARD_NO"></span>
				</td>
				<td class="tdtxt">
					HG剩餘點數：
				</td>
				<td class="tdval">
					<span id="labHG_REMAIN_POINT"></span>
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
		</table>
	</div>
	<div style="border: 1px solid #808080; min-height: 100px;">
		<div style="background-color: #AAAAAA;">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="3" border="0" align="left">
							<tr>
								<td>
									<input type="button" id="btnAddItem" name="btnAddItem" runat="server" value="<%$ Resources:WebResources, Add %>"
										onclick="createItemRow();" disabled="disabled" jqkey="btnAddItem" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<input type="button" id="btnDeleteItems" name="btnDeleteItems" runat="server" value="<%$ Resources:WebResources, Delete %>"
										onclick="deleteItemRow();" disabled="disabled" jqkey="btnDeleteItems" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<input type="button" id="btnMixPromotion" name="btnMixPromotion" runat="server" value="<%$ Resources:WebResources, MixPromotion %>"
										onclick="mixPromotion();" disabled="disabled" jqkey="btnMixPromotion" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<input type="button" id="btnHappyGoNet" name="btnHappyGoNet" runat="server" value="HappyGo折抵"
										onclick="happyGoDiscount();" disabled="disabled" jqkey="btnHappyGoNet" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<input type="button" id="btnStoreDiscount" name="btnStoreDiscount" value="特殊抱怨折扣"
										onclick="storeDiscount();" disabled="disabled" jqkey="btnStoreDiscount" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<div id="divMasterMsg" style="color: Red;">
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="gvMaster" name="gvMaster" cellpadding="2" cellspacing="1" border="0" width="100%">
							<tr style="height: 24px;">
								<td class="grid_header" style="width: 20px;">
									<input type="checkbox" id="chkAllRow" onclick="checkAllCheckboxs();" />
								</td>
								<td class="grid_header" style="width: 30px;">
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
		<table width="100%" border="0">
			<tr>
				<td class="tdtxt">
					<!--應收總金額-->
					<asp:Literal ID="Literal30" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<span id="labTotalAmount">0</span>
				</td>
				<td class="tdtxt">
					原交易金額 ：
				</td>
				<td class="tdval">
					<span id="labOrigTotal">0</span>
				</td>
				<td class="tdtxt">
					<span id="labDiffText">應補金額</span>：
				</td>
				<td class="tdval">
					<span id="labDiffValue">0</span>
				</td>
			</tr>
		</table>
	</div>
	<p />
	<div id="divDiscount" style="display: none;">
		<asp:Label ID="Label8" Text="<%$ Resources:WebResources, DiscountDetail %>" runat="server" />
		<div style="background-color: #AAAAAA;">
			<table id="gvDiscount" name="gvDiscount" cellpadding="2" cellspacing="1" border="0"
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
			value="<%$ Resources:WebResources, ExchangeGoodsConfirm %>" onclick="confirmItems();"
			jqkey="btnConfirm" />
		<input type="button" id="btnCancel" name="btnCancel" disabled="disabled" runat="server"
			value="<%$ Resources:WebResources, Cancel %>" onclick="cancelItems();" jqkey="btnCancel" />
	</div>
	<p />
	<div style="border: 1px solid #808080; min-height: 100px;">
		<div style="background-color: #AAAAAA;">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="3" border="0" align="left">
							<tr>
								<td>
									<!-- 現金 -->
									<input type="button" id="btnCash" name="btmCash" disabled="disabled" runat="server"
										value="<%$ Resources:WebResources, Cash %>" onclick="paidCash();" jqkey="btnCash" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<!-- 信用卡 -->
									<input type="button" id="btnCreditCard" name="btnCreditCard" disabled="disabled"
										runat="server" value="<%$ Resources:WebResources, CreditCard %>" onclick="creditCard();"
										jqkey="btnCreditCard" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<!-- 信用卡分期 -->
									<input type="button" id="btnInstalment" name="btnInstalment" disabled="disabled"
										runat="server" value="<%$ Resources:WebResources, Instalment %>" onclick="instalment();"
										jqkey="btnInstalment" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<!-- 金融卡 -->
									<input type="button" id="btnBankCard" name="btnBankCard" disabled="disabled" runat="server"
										value="<%$ Resources:WebResources, BankCard %>" onclick="bankCard();" jqkey="btnBankCard" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<!-- HappyGo -->
									<input type="button" id="btnHappyGo" name="btnHappyGo" disabled="disabled" runat="server"
										value="<%$ Resources:WebResources, HappyGo %>" onclick="happyGo();" jqkey="btnHappyGo" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<!-- 禮卷 -->
									<input type="button" id="btnCoupon" name="btnCoupon" disabled="disabled" runat="server"
										value="<%$ Resources:WebResources, Coupon %>" onclick="coupon();" jqkey="btnCoupon" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<!-- 離線信用卡 -->
									<input type="button" id="btnOffLineCreditCard" name="btnOffLineCreditCard" disabled="disabled"
										runat="server" value="<%$ Resources:WebResources, OffLineCreditCard %>" onclick="offLineCreditCard();"
										jqkey="btnOffLineCreditCard" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<!-- 刪除 -->
									<input type="button" id="btnDeleteCheckout" name="btnDeleteCheckout" disabled="disabled"
										runat="server" value="<%$ Resources:WebResources, Delete %>" onclick="deleteCheckoutRow();"
										jqkey="btnDeleteCheckout" />
								</td>
								<td>
									&nbsp;
								</td>
								<td>
									<div id="divCheckoutMsg" style="color: Red;">
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="gvCheckout" name="gvCheckout" cellpadding="2" cellspacing="1" border="0"
							width="100%">
							<tr style="height: 24px;">
								<td class="grid_header" style="width: 20px;">
									<input type="checkbox" id="chkAllCheckout" onclick="checkAllCheckoutItems();" />
								</td>
								<td class="grid_header" style="width: 30px;">
									&nbsp;
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>" />
								</td>
								<td class="grid_header">
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
							value="<%$ Resources:WebResources, ExchangeGoodsCheckOut %>" jqkey="btnCheckout" />
						<input type="button" id="btnCancelTransaction" name="btnCancelTransaction" disabled="disabled"
							runat="server" value="<%$ Resources:WebResources, ExchangeGoodsCancel %>" jqkey="btnCancelTransaction" />
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div style="width: 0px; height: 0px;">
		<iframe id="pdfFrame" height="0px" width="0px"></iframe>
	</div>
	<!--<textarea id="txtJsonReturn" cols="80" rows="5" visible="false"></textarea>-->
</asp:Content>
