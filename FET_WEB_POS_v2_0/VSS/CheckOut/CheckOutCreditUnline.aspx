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
                    <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                   卡號：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                   授權碼：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox>
                </td>
            </tr>
        </table>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table border="0" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnOK" runat="server" Text="確定" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnOK0" runat="server" Text="取消" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>        
        </div>
    </div>
    </form>
</body>
</html>
