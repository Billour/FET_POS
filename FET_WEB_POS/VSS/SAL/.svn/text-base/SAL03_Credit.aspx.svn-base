<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL03_Credit.aspx.cs" Inherits="VSS_SAL_SAL03_Credit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>信用卡支付</title>
    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function() {
            $("#btnStep2_OK").click(function() {
                window.close();
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
            <table >
                <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                <tr>
                    <td colspan="2" style="width: 220px; height: 100px; background-color: Silver" align="center"
                        valign="middle">
                       <!--請於刷卡機，刷退原交易信用卡金額-->
                       <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RefundCreditCardPayments %>" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnStep1_OK" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClientClick="window.close();" />
                    </td>
                </tr>
            </table>
        
        
    </div>
    </form>
</body>
</html>
