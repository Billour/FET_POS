<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSAL01_AddProds.aspx.cs"
	Inherits="VSS_SAL_TSAL01_TSAL01_AddProds" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>加價購</title>

	<script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
	<script src="../../../ClientUtility/jquery-ui-custom-min.js" type="text/javascript"></script>
	<script src="../../../ClientUtility/jquery-ui-numeric-min.js" type="text/javascript"></script>
	<script src="scripts/TSAL01_Common.js" type="text/javascript"></script>
	<script src="scripts/TSAL01_GridRow.js" type="text/javascript"></script>
	<script src="scripts/TSAL01_AddProd.js" type="text/javascript"></script>
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
		.spanLabel
		{
			overflow: hidden;
			text-overflow: ellipsis;
			white-space: nowrap;
		}
		.buttonStyle
		{
			border-width: 1px;
			height: 24px;
		}
		.textBoxStyle
		{
			border: 1px solid #808080;
		}
	</style>
</head>
<body style="padding:5px;">
	<form id="form1" runat="server">
	<div style="text-align: center; width: 100%;">
		<table border="0" width="100%">
			<tr>
				<td class="tdtxt" style="width:15%;">
					<!--商品料號 -->
					<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
				</td>
				<td class="tdval" style="width:20%;">
					<span id="labProdNo" class="spanLabel" style="width: 80px;" />
				</td>
				<td class="tdtxt" style="width:15%;">
					<!--商品名稱-->
					<asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
				</td>
				<td class="tdval" style="width:50%;">
					<span id="labProdName" class="spanLabel" style="width: 200px; overflow: hidden;" />
				</td>
			</tr>
		</table>
		<br />
		<div style="border: 1px solid #808080; min-height: 100px; max-height:350px; overflow:auto;">
			<div style="background-color: #AAAAAA;">
				<table cellpadding="0" cellspacing="0" border="0" width="100%">
					<tr>
						<td>
							<table id="gvProds" name="gvProds" cellpadding="2" cellspacing="1" border="0" width="100%">
								<tr style="height: 24px;">
									<td class="grid_header" style="width: 60%;">
										商品
									</td>
									<td class="grid_header" style="width: 10%;">
										原價
									</td>
									<td class="grid_header" style="width: 10%;">
										加購價
									</td>
									<td class="grid_header" style="width: 10%;">
										數量
									</td>
									<td class="grid_header" style="width: 10%;">
										小計
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<div id="divLoadingMsg" style=" width:99.9%; height:70px; text-align:center;"></div>
		</div>
		<br />
		<input type="button" value="確定" jqKey="btnConfirm" />&nbsp;&nbsp;<input type="button"
			value="取消" jqKey="btnCancel" />
	</div>
	</form>
</body>
</html>
