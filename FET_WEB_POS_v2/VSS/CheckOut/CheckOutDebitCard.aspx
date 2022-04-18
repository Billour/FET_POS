<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutDebitCard.aspx.cs"
	Inherits="VSS_CheckOut_CheckOutDebitCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>金融卡金額輸入</title>
</head>
<body>
	<form id="form1" runat="server">
	<div class="checkOutDiv">
		<div id="Page1">
			<table>
				<tr>
					<td class="tdtxt">
						金融卡金額：
					</td>
					<td class="tdval">
						<dx:ASPxTextBox ID="txtAMOUNT" ClientInstanceName="txtAMOUNT" UseSubmitBehavior="false"
							runat="server" AutoPostBack="false" MaxLength="8">
							<ClientSideEvents KeyDown="function(s, e)
                        {
                          if(event.keyCode == 13)
                            VisaDebit();
                         }" />
						</dx:ASPxTextBox>
					</td>
				</tr>
			</table>
			<div class="btnPosition">
				<table border="0" cellpadding="0" cellspacing="0" align="center">
					<tr>
						<td>
							<dx:ASPxButton ID="btnCommit1" runat="server" Text="<%$ Resources:WebResources, Ok %>"
								AutoPostBack="false">
								<ClientSideEvents Click="function(s,e){ VisaDebit(); }" />
							</dx:ASPxButton>

							<script type="text/javascript">
								function $get(n) {
									return window.document.getElementById(n);
								}
								function VisaDebit() {
									var r = Number(txtAMOUNT.GetText());

									if (txtAMOUNT.GetText() == "") {
										alert('請輸入金額!');
									}
									else if (isNaN(r)) {
										txtAMOUNT.SetText(null);
										alert('輸入字串不為數字格式，請重新輸入!');
									}
									else {
										$get('Page1').style["visibility"] = "hidden";
										$get('Page1').style["position"] = "absolute";
										$get('Page2').style["visibility"] = "";
										$get('Page2').style["position"] = "";
										$get('Page3').style["visibility"] = "hidden";
										$get('Page3').style["position"] = "absolute";
										var r = ''; //呼叫刷卡程式 Call_Credit_Card_Sale();
										//狀態,金融卡號,授權碼
										r = '00,1111***444433,99000701052'; //刷卡成功 TEST
										//r = '01,ERROR';  //刷卡失敗 TEST
										var ra = r.split(',');
										if (ra[0] == '00') //刷卡成功
										{
											ra[0] = 'VisaDebit,' + txtAMOUNT.GetText();
											//付款別,金額,金融卡號,授權碼
											returnValue = ra.toString();
											window.close();
										} else //失敗
										{
											lbMsg.SetText("授權失敗，請重新選擇付款方式");
											$get('Page1').style["visibility"] = "hidden";
											$get('Page1').style["position"] = "absolute";
											$get('Page2').style["visibility"] = "hidden";
											$get('Page2').style["position"] = "absolute";
											$get('Page3').style["visibility"] = "";
											$get('Page3').style["position"] = "";
										}
									}
								}                                            
							</script>

						</td>
						<td>
							&nbsp;
						</td>
						<td>
							<dx:ASPxButton ID="btnCommit4" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
								AutoPostBack="false">
								<ClientSideEvents Click="function(s,e){window.close();return false;}" />
							</dx:ASPxButton>
						</td>
					</tr>
				</table>
			</div>
		</div>
		<div id="Page2" style="position: absolute; visibility: hidden;">
			<table align="center">
				<tr>
					<td colspan="2" style="width: 200px; height: 200px; background-color: Silver" align="center"
						valign="middle">
						請刷卡
					</td>
				</tr>
			</table>
		</div>
		<div id="Page3" style="position: absolute; visibility: hidden;">
			<table align="center">
				<tr>
					<td colspan="2" style="width: 200px; height: 200px; background-color: Silver" valign="middle">
						<%-- 扣款失敗，請重新選擇付款方式--%>
						<dx:ASPxLabel ID="lbMsg" ClientInstanceName="lbMsg" runat="server">
						</dx:ASPxLabel>
					</td>
				</tr>
			</table>
			<div class="btnPosition">
				<table border="0" cellpadding="0" cellspacing="0" align="center">
					<tr>
						<td>
							<dx:ASPxButton ID="btnCommit3" runat="server" Text="<%$ Resources:WebResources, Ok %>"
								AutoPostBack="false">
								<ClientSideEvents Click="function(s,e){window.close();return false;}" />
							</dx:ASPxButton>
						</td>
					</tr>
				</table>
			</div>
		</div>
	</div>
	</form>
</body>
</html>
