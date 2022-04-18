<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCash.aspx.cs" Inherits="VSS_CheckOut_CheckOutCash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>現金金額輸入</title>
    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function() {
            $("#btnOK").click(function() {
                window.close();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td class="tdtxt">
                    現金金額：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnOK" runat="server" Text="確定" />
                    <asp:Button ID="btnCancel" runat="server" Text="取消" OnClientClick="javascript:window.close();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
