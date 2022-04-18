<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCreditUnline.aspx.cs" Inherits="VSS_CheckOut_CheckOutCreditUnline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>離線信用卡輸入</title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        <table>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td class="tdtxt">
                   信用卡金額：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                   卡號：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                   授權碼：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnOK" runat="server" Text="確定" OnClientClick="window.close();return false;"/>
            <asp:Button ID="btnOK0" runat="server" Text="取消" 
                OnClientClick="window.close();return false;"/>
        </div>
    </div>
    </form>
</body>
</html>
