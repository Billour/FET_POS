<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCredit.aspx.cs" Inherits="VSS_CheckOut_CheckOutCredit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信用卡金額輸入</title>
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
    <div>
    
        <div id="divStep0" runat="server" class="checkOutDiv">
            <table >
                <tr><td>信用卡金額：</td>
                   <td>
                        <input type="text" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnStep0_OK" runat="server" Text="確定" 
                            onclick="btnStep0_OK_Click"  />
                        <asp:Button id="btnStep0_Cancel" runat="server" Text="取消" OnClientClick="javascript:window.close()" />
                    </td>
                </tr>
            </table>
        </div>
        
        <div id="divStep1" runat="server" class="checkOutDiv">
            <table >
                <tr><td>&nbsp;</td></tr>
                <tr>
                    <td colspan="2" style="width: 200px; height: 200px; background-color: Silver" align="center"
                        valign="middle">
                        請刷卡
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnStep1_OK" runat="server" Text="確定" OnClick="btnStep1_OK_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divStep2" runat="server" style="display: none">
            <table>
                <tr><td>&nbsp;</td></tr>
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
                        <asp:Button ID="btnStep2_OK" runat="server" Text="確定" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
