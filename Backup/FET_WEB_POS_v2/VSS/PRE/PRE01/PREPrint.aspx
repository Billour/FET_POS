<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PREPrint.aspx.cs" Inherits="VSS_PRE_PREPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>發票/收據/折讓單列印</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <dx:ASPxButton ID="Invoice" runat="server" Text="列印發票" OnClick="BtnPri_Click" CausesValidation="false" />
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="Receipt" runat="server" Text="列印收據" OnClick="BtnPri_Click" CausesValidation="false" />
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                         <td align="left">
                            <dx:ASPxButton ID="Discount" runat="server" Text="列印折讓單" OnClick="BtnPri_Click" CausesValidation="false" />
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        

                        <td align="left">
                            <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){window.close();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <iframe id="fDownload" style="display: none" src="" runat="server" width="100%" height="100%">
        </iframe>
    </div>
    </form>
</body>
</html>
