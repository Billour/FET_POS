<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutGift.aspx.cs" Inherits="VSS_CheckOut_CheckOutGift" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>禮券金額輸入</title> 
</head>
<body> 
    <form id="form1" runat="server">
   <div>
    <div class="seperate"></div>
        <table>
            <tr>
                <td class="tdtxt">
                   禮券名稱：
                </td>
                <td class="tdval">
                <dx:ASPxComboBox ID="cb1" runat="server">
                
                </dx:ASPxComboBox>
                </td>
            </tr>        
            <tr>
                <td class="tdtxt">
                   禮券金額：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                   禮券號碼：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server" MaxLength="8"></dx:ASPxTextBox>
                </td>
            </tr> 
        </table>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table border="0" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCommit4" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false">
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
