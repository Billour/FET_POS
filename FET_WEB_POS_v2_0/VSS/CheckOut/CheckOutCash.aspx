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
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    現金金額：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table cellpadding="0" cellspacing="0" border="0" align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, OK %>">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
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
    </form>
</body>
</html>
