<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCredit.aspx.cs" Inherits="VSS_CheckOut_CheckOutCredit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>信用卡金額輸入</title>
	<object id="oECR" classid="CLSID:9126B51B-F1B9-4AED-AA4C-131FC05482B3" codebase="ECRAPI.CAB#version=1,0,0,0">
	</object>

	<script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

	<script type="text/javascript" language="javascript">
		$(document).ready(function() {
			$("#btnStep2_OK").click(function() { window.close(); });
			txtAMOUNT.focus(function() { $(this).select(); })
				.keydown(function(e) { if (e.keyCode == 13) CreditCard(null, null); });
		});
		//刷卡
		function Call_Credit_Card_Sale() {
			var oECR = new ActiveXObject("ProjECR.ECRAPI");
			var STORENO = $get("hiddenSTORENO").value;
			//return oECR.Credit_Card_Sale(txtAMOUNT.GetText(), STORENO, "0", "");
			return oECR.Credit_Card_Sale(txtAMOUNT.val(), STORENO, "0", "");
		}
		//刷退
		function Call_Credit_Card_Refund() {
			var oECR = new ActiveXObject("ProjECR.ECRAPI");
			var STORENO = $("#hiddenSTORENO").value;
			var result = oECR.Credit_Card_Refund(txtAmount.value, txtAPPROVAL.value, txtStoreId.value, "0");
		}

	</script>

</head>
<body>
	<form id="form1" runat="server">
	<div>
		<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				<div id="divStep0" runat="server">
					<table align="center" style="vertical-align: middle">
						<tr>
							<td>
								信用卡金額：
							</td>
							<td>
								<asp:TextBox ID="txtAMOUNT" runat="server" Width="170px" MaxLength="8" BorderStyle="Solid"
									BorderWidth="1"></asp:TextBox>
								<%-- 
                                <dx:ASPxTextBox ID="txtAMOUNT" ClientInstanceName="txtAMOUNT" runat="server" Width="170px" MaxLength="8" ClientSideEvents-KeyDown="function(e) { alert(e.keyCode); if (e.keyCode == 13) CreditCard(null,e); else { event.returnValue = false; return false; } }">
                                </dx:ASPxTextBox>
                                --%>
							</td>
						</tr>
						<tr>
							<td colspan="2" align="center">
								<table border="0" cellpadding="0" cellspacing="0" align="center">
									<tr>
										<td>
											<dx:ASPxButton ID="btnStep0_OK" runat="server" AutoPostBack="false" Text="<%$ Resources:WebResources, Ok %>">
												<ClientSideEvents Click="function(s,e){CreditCard(s,e);}" />
											</dx:ASPxButton>

											<script type="text/javascript">
												function CreditCard(s, e) {
													$get('divStep0').style["display"] = "none";
													$get('divStep1').style["display"] = "";
													var r = Call_Credit_Card_Sale();
													//TEST等刷信用卡程式用好來就可以WORK了
													//r = '0000,3678***444433,99000701052,00004';
													var ra = r.split(',');
													//4 0000 ,CARD_NO,REF_NO,RECEIPT_NO
													if (ra[0] == '0000') //刷卡成功
													{  //金額,信用卡號,授權碼
														//returnValue = 'CreditCard,' + txtAMOUNT.GetText() + ',' + ra[1] + ',' + ra[2] + ',' + ra[3];
														returnValue = 'CreditCard,' + txtAMOUNT.val() + ',' + ra[1] + ',' + ra[2] + ',' + ra[3]+ ',' + ra[4]+ ',' + ra[5]+ ',' + ra[6]+ ',' + ra[7];
														window.close();
													} else //失敗
													{
														lbMsg.SetText("授權失敗，請重新選擇付款方式");
														$get('divStep1').style["display"] = "none";
														$get('divStep2_1').style["display"] = "";
													}
												}
                                            
											</script>

										</td>
										<td>
											&nbsp;
										</td>
										<td>
											<dx:ASPxButton ID="btnStep0_Cancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
												AutoPostBack="false">
												<ClientSideEvents Click="function(s,e){window.close();return false;}" />
											</dx:ASPxButton>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</div>
				<div id="divStep1" runat="server" class="checkOutDiv">
					<table align="center">
						<tr>
							<td>
								&nbsp;
							</td>
						</tr>
						<tr>
							<td style="width: 200px; height: 200px; background-color: Silver" align="center"
								valign="middle">
								請刷卡
							</td>
						</tr>
						<tr>
							<td>
								<table align="center">
									<tr>
										<td align="center">
											<dx:ASPxButton ID="btnStep1_OK" runat="server" Visible="false" Text="<%$ Resources:WebResources, Ok %>"
												OnClick="btnStep1_OK_Click">
											</dx:ASPxButton>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</div>
				<div id="divStep2" runat="server" style="display: none">
					<table align="center">
						<tr>
							<td>
								&nbsp;
							</td>
						</tr>
						<tr>
							<td class="tdtxt">
								信用卡卡號：
							</td>
							<td class="tdval">
								<asp:Label ID="Label1" runat="server" Text="1111-2222-3333-4444"></asp:Label>
							</td>
						</tr>
						<tr>
							<td class="tdtxt">
								卡別：
							</td>
							<td class="tdval">
								<asp:Label ID="Label2" runat="server" Text="VISA"></asp:Label>
							</td>
						</tr>
						<tr>
							<td class="tdtxt">
								發卡銀行：
							</td>
							<td class="tdval">
								<asp:Label ID="Label3" runat="server" Text="中國信託"></asp:Label>
							</td>
						</tr>
						<tr>
							<td class="tdtxt">
								期限：
							</td>
							<td class="tdval">
								<asp:Label ID="Label4" runat="server" Text="10/2013"></asp:Label>
							</td>
						</tr>
						<tr>
							<td colspan="2" align="center">
								<dx:ASPxButton ID="btnStep2_OK" runat="server" Text="<%$ Resources:WebResources, Ok %>">
								</dx:ASPxButton>
							</td>
						</tr>
					</table>
				</div>
				<div id="divStep2_1" runat="server" align="center" style="display: none">
					<table align="center">
						<tr>
							<td style="width: 200px; height: 200px; background-color: Silver" align="center"
								valign="middle">
								<dx:ASPxLabel ID="lbMsg" ClientInstanceName="lbMsg" runat="server">
								</dx:ASPxLabel>
							</td>
						</tr>
						<tr>
							<td align="center">
								<dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Ok %>"
									AutoPostBack="false">
									<ClientSideEvents Click="function(s,e){returnValue = '';window.close();}" />
								</dx:ASPxButton>
							</td>
						</tr>
					</table>
				</div>
				<div id="divsal03_2" runat="server" align="center">
					<table>
						<tr>
							<td style="width: 300px; height: 200px; background-color: Silver" align="center"
								valign="middle">
								請於刷卡機，刷退原交易信用卡金額
							</td>
						</tr>
						<tr>
							<td align="center">
								<dx:ASPxButton ID="ASPxButton4" runat="server" Text="<%$ Resources:WebResources, Ok %>"
									AutoPostBack="false">
									<ClientSideEvents Click="function(s,e){window.close();return false;}" />
								</dx:ASPxButton>
							</td>
						</tr>
					</table>
				</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</form>
</body>
</html>
