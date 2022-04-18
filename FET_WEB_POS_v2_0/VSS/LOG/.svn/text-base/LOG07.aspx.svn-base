<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG07.aspx.cs"    Inherits="VSS_LOG_LOG07" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, PasswordModification %>" /></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div >
            <table >
                <tr class="titlef">
                    <td colspan="2">
                        <!--修改密碼-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, PasswordModification %>" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <!--請設定密碼-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ChangePasswordCaption %>" />
                        </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--舊密碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OldPassword %>" />：</td>
                    <td class="tdval">
                        <input id="Password3" type="password" runat="server" /></td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--新密碼-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, NewPassword %>" />：
                    </td>
                    <td class="tdval">
                        <input id="Password1" type="password" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--確認新密碼-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ConfirmNewPassword %>" />：
                    </td>
                    <td class="tdval">
                        <input id="Password2" type="password" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--新密碼確認不相符，請重新輸入-->
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password1"
                ControlToValidate="Password2" Display="Dynamic" ErrorMessage="<%$ Resources:WebResources, ExPasswordConfirmationDoesNotMatch %>"></asp:CompareValidator>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClick="btnCommit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClientClick="window.close();return false;" />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                <ContentTemplate>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
