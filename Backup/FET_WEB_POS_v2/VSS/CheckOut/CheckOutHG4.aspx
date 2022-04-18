<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutHG4.aspx.cs" Inherits="VSS_CheckOut_CheckOutHG4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HappyGo刷卡兌點</title>
    
    <script type="text/javascript" language="javascript">
        //刷卡
        function Call_HappyGo_Card_Sale() {
            var oECR = new ActiveXObject("ProjECR.ECRAPI");
            var storeNo = hidStoreNo.GetText();
            var HGCardNo = lblFinHG_CAR_NO.GetText();
            var HGRedeemPoint = hidRedeemPoint.GetText();
            var retStr = oECR.Full_Redeem(storeNo,HGCardNo,HGRedeemPoint);
            var retArr = retStr.split(',');
            if (retArr[0] == "0000") 
                return true;
            else 
                return false;
            //return '0000,1111-2222-3333-4444,7000';
        }
        
        function ReturnValue(s, e) {
            if (Call_HappyGo_Card_Sale()) {
                returnValue = 'OK';
            } else {
                alert('HG 扣點失敗!');
                returnValue = '';
            }
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table>
                <tr>
                    <td class="tdtxt">
                        HG卡號：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblFinHG_CAR_NO" ClientInstanceName="lblFinHG_CAR_NO" runat="server"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        剩餘點數：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lblFinHG_LEFT_POINT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        欲兌金額/點數：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="hdFinSumPoint" ClientInstanceName="hdFinSumPoint" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="hdFinSumAmount" ClientInstanceName="hdFinSumAmount" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="hdFinSID" ClientInstanceName="hdFinSID" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxLabel ID="lblFinHG_REDEEM_POINT" ClientInstanceName="lblFinHG_REDEEM_POINT" runat="server"></dx:ASPxLabel>
                        <dx:ASPxLabel ID="hidStoreNo" ClientInstanceName="hidStoreNo" runat="server" ClientVisible="false"></dx:ASPxLabel>
                        <dx:ASPxLabel ID="hidRedeemPoint" ClientInstanceName="hidRedeemPoint" runat="server" ClientVisible="false"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 160px; background-color: Silver" align="center" valign="middle">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="請刷卡進行點數扣抵" Font-Size="Larger"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, OK %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){ ReturnValue(s, e); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
