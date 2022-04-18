<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSAL01_ETCInput.aspx.cs"
	Inherits="VSS_SAL_TSAL01_TSAL01_ETCInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<base target="_self" />
	<title>ETC加值</title>
	<script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
	<script src="../../../ClientUtility/jquery-ui-custom-min.js" type="text/javascript"></script>
	<script src="../../../ClientUtility/jquery-ui-numeric-min.js" type="text/javascript"></script>
	<script src="scripts/TSAL01_Common.js" type="text/javascript"></script>
	<script src="scripts/TSAL01_ETCInput.js" type="text/javascript"></script>
	<style>
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
<body>
	<form id="form1" runat="server">
	<asp:HiddenField ID="hfMinLimitAmount" runat="server" />
	<asp:HiddenField ID="hfETC_ProdNo" runat="server" />
	<div id="ReadETCCard" style="width: 100%;">
		<center>
		<table>
			<tr>
				<td style="background-color:Silver; width:200px; height:120px;" align="center" valign="middle">
					請準備 ETC 卡片準備加值
				</td>				
			</tr>
			<tr>
				<td><input type="button" id="btnReady" value="確定" /></td>
			</tr>
		</table>
		</center>
	</div>
	<div id="AmountLayer" style="text-align: center; display:none;">
		<table>
			<tr>
				<td class="tdtxt">
					卡號：
				</td>
				<td class="tdval" colspan="3">
					<span id="labETCCardNo" />
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					最低加值金額：
				</td>
				<td class="tdval">
					<span id="labMinLimitAmount" />
				</td>
				<td class="tdtxt">
					最大加值金額：
				</td>
				<td class="tdval">
					<span id="labMaxLimitAmount" />
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					加值金額：
				</td>
				<td class="tdval" align="left" colspan="3">
					<table cellpadding="0" cellspacing="0" border="0">
						<tr>
							<td><input type="text" id="txtAmount" maxlength="5" size="5" style="text-align: right;" /></td>
							<td>&nbsp;(每次加值以百元為單位)</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<div class="seperate">
		</div>
		<div class="btnPosition">
			<input type="button" id="btnCash" value="現金加值" jqkey="btnCash" />&nbsp;<input type="button"
				id="btnCreditCard" value="聯名卡加值" jqkey="btnCreditCard" />&nbsp;&nbsp;&nbsp;<input
					type="button" id="btnCancel" runat="server" value="<%$ Resources:WebResources, Cancel %>"
					jqkey="btnCancel" onclick="window.close();" />
		</div>
	</div>
	</form>
</body>
</html>
