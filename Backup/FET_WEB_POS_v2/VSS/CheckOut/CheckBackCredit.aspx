<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckBackCredit.aspx.cs" Inherits="VSS_CheckOut_CheckBackCredit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信用卡刷退</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="width: 300px; height: 200px; background-color: Silver" align="center"
                            valign="middle">
                            請於刷卡機，刷退原交易信用卡金額
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="0" align="center">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>">
                                            <ClientSideEvents Click="function(s,e){returnValue = 'OK';window.close();}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                                            AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){returnValue = 'NOK';window.close();}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
