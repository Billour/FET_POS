<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_checkIDNumber.aspx.cs"
    Inherits="VSS_SAL_SAL01_checkIDNumber" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, VerifyUnifiedBusinessNo %>" /></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div class="criteria">
            <table>
                <tr>
                    <td colspan="2">
                        <!--統一編號檢查碼錯誤-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ExInvalidUnifiedBusinessNo %>" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="color: Red">
                        <!--請確認無誤後，重新輸入。-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ExPleaseCheckThatYouEntered %>" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--統一編號-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>" />：
                    </td>
                    <td class="tdval">
                        <input id="Password1" type="password" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--確認統一編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ConfirmUnifiedBusinessNo %>" />：
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
