<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutHG3.aspx.cs" Inherits="VSS_CheckOut_CheckOutHG3" %>

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
            <table align="center">
                <tr>
                    <td>
                        ＨＧ卡號：
                    </td>
                    <td>
                        HG12345678
                    </td>
                </tr>
                <tr>
                    <td>
                        剩餘點數：
                    </td>
                    <td>
                        500
                    </td>
                </tr>
                <tr>
                    <td>
                        加回點數：
                    </td>
                    <td>
                        <font color="red">400元(1350點)</font>
                    </td>
                </tr>                                  
            </table>
        </div>
        <div id="div1" runat="server" align="center">
            <table>
                <tr>
                    <td style="width: 300px; height: 200px; background-color: Silver" align="center"
                        valign="middle">
                        請過卡，加回原交易點數
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <dx:ASPxButton ID="ASPxButton4" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
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
