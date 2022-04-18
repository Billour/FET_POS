<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCreditUnline.aspx.cs"
	Inherits="VSS_CheckOut_CheckOutCreditUnline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>離線信用卡輸入</title>
	<script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
	<script src="../../ClientUtility/Common.js" type="text/javascript"></script>
	<script type="text/javascript" language="javascript">
		$(function() {
			$("#btnOK").click(function() {
				var amount = parseInt(Number(txtCreditAmount.GetText()));
				var cardNo = txtCardNo.GetText();
				var licenseCode = txtLicenseCode.GetText();
				if (amount.toString() == 'NaN')
					alert('請輸入金額 !');
				else if (cardNo == '')
					alert('請輸入卡號 !');
				else if (licenseCode == '')
					alert('請輸入授權碼 !');
				else if (!checkCreditCardNo(cardNo) || checkCardType(cardNo) == '') {
					alert('信用卡號輸入不正確，請重新輸入 !');
				} else {
					returnValue = 'OFF_LINE_CREDIT,' + amount + ',' + cardNo + ',' + licenseCode;
					window.close();
				}
			});
		});
	</script>

</head>
<body>
	<form id="form1" runat="server">
	<div>
		<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				<div class="criteria">
					<table>
						<tr>
							<td>
								&nbsp;
							</td>
						</tr>
						<tr>
							<td class="tdtxt">
								信用卡金額：
							</td>
							<td class="tdval">
								<dx:ASPxTextBox ID="txtCreditAmount" ClientInstanceName="txtCreditAmount" runat="server"
									MaxLength="8">
								</dx:ASPxTextBox>
							</td>
						</tr>
						<tr>
							<td class="tdtxt">
								卡號：
							</td>
							<td class="tdval">
								<dx:ASPxTextBox ID="txtCardNo" ClientInstanceName="txtCardNo" runat="server" MaxLength="16">
								</dx:ASPxTextBox>
							</td>
						</tr>
						<tr>
							<td class="tdtxt">
								授權碼：
							</td>
							<td class="tdval">
								<dx:ASPxTextBox ID="txtLicenseCode" ClientInstanceName="txtLicenseCode" runat="server">
								</dx:ASPxTextBox>
							</td>
						</tr>
					</table>
				</div>
				<div class="seperate">
				</div>
				<div class="btnPosition">
					<table border="0" cellpadding="0" cellspacing="0" align="center">
						<tr>
							<td>
								<dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>"
									AutoPostBack="false">
								</dx:ASPxButton>
							</td>
							<td>
								&nbsp;
							</td>
							<td>
								<dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
									AutoPostBack="false">
									<ClientSideEvents Click="function(s,e){window.close();return false;}" />
								</dx:ASPxButton>
							</td>
						</tr>
					</table>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
	</form>
</body>
</html>
