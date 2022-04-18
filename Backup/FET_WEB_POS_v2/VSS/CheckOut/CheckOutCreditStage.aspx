<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCreditStage.aspx.cs"
    Inherits="VSS_CheckOut_CheckOutCreditStage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分期付款卡輸入</title>
    <object id="oECR" classid="CLSID:9126B51B-F1B9-4AED-AA4C-131FC05482B3" codebase="ECRAPI.CAB#version=1,0,0,0">
    </object>

    <script type="text/javascript" language="javascript">
        //刷卡
        function Call_Credit_Card_Sale() {
            var oECR = new ActiveXObject("ProjECR.ECRAPI");
            var STORENO = $get("hiddenSTORENO").value;
            //金額                門市代號 表示分期 ,期數
            return oECR.Credit_Card_Sale(txtAMOUNT.GetText(), STORENO, "I", cbIssue.GetValue());
            //return true;
        }
        //刷退
        function Call_Credit_Card_Refund() {
            var oECR = new ActiveXObject("ProjECR.ECRAPI");
            var STORENO = $("#hiddenSTORENO").value;
            var result = oECR.Credit_Card_Refund(txtAmount.value, txtAPPROVAL.value, txtStoreId.value, "0");
            alert(result);
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="checkOutDiv">
                <asp:Panel ID="step1" runat="server">
                    <table align="center">
                        <tr>
                            <td class="tdtxt">
                                銀行別：
                            </td>
                            <td class="tdval">
                                <dx:ASPxComboBox ID="cbBank" ClientInstanceName="cbBank" runat="server" SelectedIndex="0"
                                    Width="100px" onselectedindexchanged="cbBank_SelectedIndexChanged" AutoPostBack="true">
                                    <Items>
                                        <dx:ListEditItem Text="--請選擇--" Value="--請選擇--" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                期數：
                            </td>
                            <td class="tdval">
                                <dx:ASPxComboBox ID="cbIssue" ClientInstanceName="cbIssue" runat="server" SelectedIndex="0"
                                    Width="100px">
                                    <Items>
                                        <dx:ListEditItem Text="--請選擇--" Value="0" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                信用卡金額：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="txtAMOUNT" ClientInstanceName="txtAMOUNT" runat="server" CssClass="tbWidthFormat" Width="120" MaxLength="8">
                                </dx:ASPxTextBox>
                                <dx:ASPxTextBox ID="limitAmount" ClientInstanceName="limitAmount" runat="server" ClientVisible="false" >
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <table border="0" cellpadding="0" cellspacing="0" align="center">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnCommit1" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e){DivCreditCard(s,e);}" />
                                            </dx:ASPxButton>

                                            <script type="text/javascript">
                                                function DivCreditCard(s, e) {
                                                    if (cbBank.GetValue() == null || cbBank.GetValue() == "") {
                                                        alert('請選取銀行!');
                                                    } else if (cbIssue.GetValue() == "0") {
                                                        alert('請選期別!');
                                                    } else if (txtAMOUNT.GetText() == "") {
                                                        alert('請輸入金額!');
                                                    } else {
                                                        var amt = parseInt(Number(txtAMOUNT.GetText()));
                                                        var limitAmt = parseInt(Number(limitAmount.GetText()));
                                                        if (amt < limitAmt) {
                                                            alert('輸入金額不得小於' + limitAmt );
                                                            return;
                                                        }
                                                        $get('step1').style["visibility"] = "hidden";
                                                        $get('step1').style["position"] = "absolute";
                                                        $get('step2').style["visibility"] = "";
                                                        $get('step2').style["position"] = "";
                                                        $get('stepFinish').style["visibility"] = "hidden";
                                                        $get('stepFinish').style["position"] = "absolute";
                                                        var r = Call_Credit_Card_Sale();
                                                        //狀態,信用卡號,序號,閱讀編號,銀行編號,期數
                                                        //r = '0000,3678***444433,99000701052,00004,1234,004,06'; //刷卡成功 TEST
                                                        //r = '01,ERROR';  //刷卡失敗 TEST
                                                        var ra = r.split(',');
                                                        //4 0000 ,CARD_NO,REF_NO,RECEIPT_NO
                                                        if (ra[0] == '0000') //刷卡成功
                                                        {
                                                            ra[0] = 'CreditCard,' + txtAMOUNT.GetText();
                                                            //ra[5] = cbBank.GetValue(); //銀行
                                                            //ra[6] = cbIssue.GetValue(); //期數
                                                            if (ra[5] != cbBank.GetValue()) {
                                                                alert('回傳銀行代碼與選取的銀行代碼不同,停止信用卡分期支付!');
                                                                returnValue = ra.toString();
                                                            } else {
                                                                //付款別,金額,信用卡號,序號,閱讀編號,授權碼,銀行編號,期數,信用卡別名稱
                                                                returnValue = ra.toString();
                                                            }
                                                            window.close();
                                                        } 
                                                        else //失敗
                                                        {
                                                            lbMsg.SetText("授權失敗，請重新選擇付款方式");
                                                            $get('step1').style["visibility"] = "hidden";
                                                            $get('step1').style["position"] = "absolute";
                                                            $get('step2').style["visibility"] = "hidden";
                                                            $get('step2').style["position"] = "absolute";
                                                            $get('stepFinish').style["visibility"] = "";
                                                            $get('stepFinish').style["position"] = "";
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
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="step2" runat="server" Style="position: absolute; visibility: hidden;">
                    <table align="center">
                        <tr>
                            <td style="width: 200px; height: 200px; background-color: Silver" align="center"
                                valign="middle">
                                請刷卡
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table border="0" cellpadding="0" cellspacing="0" align="center">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnCommit2" runat="server" Text="<%$ Resources:WebResources, Ok %>">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="stepFinish" runat="server" Style="position: absolute; visibility: hidden;">
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
                                <dx:ASPxButton ID="btnCommit3" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
