<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutHG3.aspx.cs" Inherits="VSS_CheckOut_CheckOutHG3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HappyGo刷退作業</title>

    <script type="text/javascript" language="javascript">
        //HappyGo卡刷退
        function Call_HG_Card_Refund(s, e) {
            var oECR = new ActiveXObject("ProjECR.ECRAPI");
            var STORENO = $get("hiddenSTORENO").value;
            return oECR.Redeem_Refund(STORENO, lblHG_CAR_NO.GetText(), lblHG_REDEEM_POINT.GetText());
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="divStep0" runat="server" class="checkOutDiv">
            <table align="center">
                <tr>
                    <td>
                        ＨＧ卡號：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblHG_CAR_NO" runat="server"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        剩餘點數：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblHG_LEFT_POINT" runat="server"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        加回點數：
                    </td>
                    <td>
                       <dx:ASPxLabel ID="lblHG_REDEEM_POINT" runat="server" ForeColor="Red"></dx:ASPxLabel>
                    </td>
                </tr>                                  
            </table>
        </div>
        <div id="div1" runat="server" align="center">
            <table>
                <tr>
                    <td style="width: 300px; height: 200px; background-color: Silver" align="center" valign="middle">
                        請過卡，加回原交易點數
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){ Call_HG_Card_Refund(s,e); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

    </div>
    </form>
</body>
</html>
