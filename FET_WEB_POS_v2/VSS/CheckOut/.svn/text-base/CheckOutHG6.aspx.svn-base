<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutHG6.aspx.cs" Inherits="VSS_CheckOut_CheckOutHG6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<base target="_self"></base>
	<title>HappyGo欲兌換來店禮</title>
	<script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
	<script type="text/javascript" language="javascript">
	
	    //刷卡
	    function Call_HappyGo_Card_Sale() {
	        var oECR = new ActiveXObject("ProjECR.ECRAPI");
	        var retStr = oECR.Inquiry_Loyalty_Point("");
	        return retStr;
	        //return '0000,1111-2222-3333-4444,7000';
	    }
		//HappyGo卡查詢點數
	    function HGCard() {
	        //debugger;
			// 查詢點數與卡號
		    //以下為刷卡程式，commit必須使用以下程式                          
		    var CardNo;
		    var CardLeftPoint;
		    var blCheck = false;
		    var r = Call_HappyGo_Card_Sale();
		    //TEST等刷卡程式用好來就可以WORK了
		    var ra = r.split(',');
		    if (ra[0] == '0000') //刷卡成功
		    {
		        CardNo = ra[1];//HG卡號
		        CardLeftPoint = ra[2]; //剩餘點數
		        blCheck = true;
		        
		    }
		    else //失敗
		    {
		        blCheck = false;
		        alert("授權失敗!!");		       
		    }
		   // blCheck = true;
		   // CardNo = "test-1234567890";
		   // CardLeftPoint = "9999";
		    if (blCheck) {

		        var url2 = "../SAL/SAL15/SAL15_2.aspx?HGCardNo=" + CardNo + "&HGCardCount=" + CardLeftPoint + '&date=' + Date();
		        
		        $('#hidUrl').val(url2);
		        $('form').submit();
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
					<td colspan="2" align="center" style="height:150px; background-color: Silver;" jqKey="MSG">請刷卡</td>
				</tr>
				<tr>
					<td colspan="2" align="center">
         
                    <input id="btnStart" type="button" onclick="HGCard()" value="<%$ Resources:WebResources, Ok %>" runat="server"/>					
                    <input id="hidUrl" type="hidden" runat="server" />
                 </td>
				</tr>
			</table>
		</div>
	</center>
	</form>
</body>
</html>
