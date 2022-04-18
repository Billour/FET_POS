<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutHG5.aspx.cs" Inherits="VSS_CheckOut_CheckOutHG5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>HappyGo累點作業</title>
	<script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
	<script type="text/javascript" language="javascript">
		//HappyGo卡累點
		function Call_Inquiry_Loyalty_Point() {
			// 查詢點數與卡號
			$(':button[jqKey="btnLoadCard"]').hide();
			var oECR = new ActiveXObject("ProjECR.ECRAPI");
			var result = oECR.Inquiry_Loyalty_Point('');
			var arr = result.split(',');
			if (arr[0] == "0000") {
				CARD_NO = arr[1];
				$('span[jqKey="labCardNo"]').text(CARD_NO);
				LEFT_POINT = arr[2];
				$('span[jqKey="labLeftPoint"]').text(LEFT_POINT);
				Call_FEG_REWARD();
			} else {
				$(':button[jqKey="btnLoadCard"]').show();
                alert('無法查詢卡片點數 !');
                returnValue = '';
                window.close();
			}
		}

		function Call_FEG_REWARD() {
			// 累積點數
			$('td[jqKey="MSG"]').text('累積點數中, 請稍候...'); 
			var oECR = new ActiveXObject("ProjECR.ECRAPI");
			var result = oECR.Reward_Transaction(AMOUNT, STORE_NO, '2', CARD_NO);
			var arr = result.split(',');
			if (arr[0] == "0000") {
				returnValue = "OK";
			} else {
				alert('無法累積點數 !');
				returnValue = '';
			}
			window.close();
		}
	</script>

</head>
<body>
	<form id="form1" runat="server">
	<center>
		<div id="divStep0" style="width: 300px;">
			<table align="center" width="100%">
				<tr>
					<td style="white-space:nowrap; width:30%; text-align: right;">
						ＨＧ卡號：
					</td>
					<td align="left">
						<span id="labCardNo" runat="server" jqKey="labCardNo"></span>
					</td>
				</tr>
				<tr>
					<td>
						剩餘點數：
					</td>
					<td align="left">
						<span id="labLeftPoint" runat="server" jqKey="labLeftPoint"></span>
					</td>
				</tr>
				<tr>
					<td colspan="2" align="center" style="height:150px; background-color: Silver;" jqKey="MSG"></td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<input type="button" id="btnLoadCard" value="確定" jqKey="btnLoadCard" style="display:none; border-width:1px;" />
					</td>
				</tr>
			</table>
		</div>
	</center>
	</form>
	<script type="text/javascript">
		$(document).ready(function() {
			if (CARD_NO.length == 0) {
				$('td[jqKey="MSG"]').text('請先過卡');
				$(':button[jqKey="btnLoadCard"]')
					.css({ width: '80px', height: '24px' })
					.show()
					.click(function() { Call_Inquiry_Loyalty_Point(); })
					.focus();
			} else {
				Call_FEG_REWARD();
			}
		});
	</script>
</body>
</html>
