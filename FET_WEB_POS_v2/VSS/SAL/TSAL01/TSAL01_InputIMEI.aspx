<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSAL01_InputIMEI.aspx.cs"
	Inherits="VSS_SAL_TSAL01_TSAL01_InputIMEI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>IMEI 輸入</title>

	<script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

	<script src="scripts/TSAL01_Common.js" type="text/javascript"></script>
	<script src="scripts/TSAL01_GridRow.js" type="text/javascript"></script>
	<script src="scripts/TSAL01_InputIMEI.js" type="text/javascript"></script>

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
	<asp:HiddenField ID="hfRowID" runat="server" />
	<div>
		<table width="100%">
			<tr>
				<td class="tdtxt">
					<!--商品料號 -->
					<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
				</td>
				<td class="tdval">
					<span id="labProdNo" class="spanLabel" style="width:80px;" />
				</td>
				<td class="tdtxt">
					<!--商品名稱-->
					<asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<span id="labProdName" class="spanLabel" style="width:200px; overflow:hidden;" />
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					<!-- IMEI -->
					<asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Imei %>" />：
				</td>
				<td class="tdval" colspan="3">
					<table>
						<tr>
							<td>
								<input type="text" id="txtIMEI" size="35"  />
							</td>
							<td>
								<input type="button" id="btnInput" name="btnInput" runat="server" value="<%$ Resources:WebResources, Enter %>"
									onclick="checkIMEI();" jqkey="btnInput" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div style="border:1px solid #808080; min-height:100px;">
		<div style="background-color:#AAAAAA;">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="3" border="0" align="left">
							<tr>
								<td>
									<input type="button" id="btnDeleteItems" name="btnDeleteItems" runat="server" value="<%$ Resources:WebResources, Delete %>"
										onclick="deleteIMEIItemRow();" jqKey="btnDeleteItems" />
								</td>
								<td>
									<div id="divMasterMsg" style="color:Red;"></div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="gvIMEI" name="gvIMEI" cellpadding="2" cellspacing="1" border="0" width="100%">
							<tr style="height: 24px;">
								<td class="grid_header" style="width:20px;">
									<input type="checkbox" id="chkAllRow" onclick="checkAllCheckboxs();" />
								</td>
								<td class="grid_header">
									<asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Imei %>" />
								</td>
							</tr>
						</table>
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
					<input type="button" runat="server" id="btnOK" name="btnOK" value="<%$ Resources:WebResources, Ok %>" jqKey="btnOK" />
				</td>
				<td>
					&nbsp;
				</td>
				<td>
					<input type="button" runat="server" id="btnCancel" name="btnCancel" value="<%$ Resources:WebResources, Cancel %>" jqKey="btnCancel" />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
