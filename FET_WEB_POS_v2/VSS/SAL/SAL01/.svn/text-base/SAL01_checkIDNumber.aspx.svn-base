<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_checkIDNumber.aspx.cs"
    Inherits="VSS_SAL_SAL01_checkIDNumber" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, VerifyUnifiedBusinessNo %>" />
    </title>
    
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/jquery.checkboxes.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
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
                        <dx:ASPxTextBox ID="txtUNI_NO1" ClientInstanceName="txtUNI_NO1" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--確認統一編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ConfirmUnifiedBusinessNo %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtUNI_NO2" ClientInstanceName="txtUNI_NO2" runat="server">
                        </dx:ASPxTextBox>
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
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" align="center">
                        <tr>
                            <td>

                                <script type="text/javascript">
                                    function commitCheck(s, e) {
                                        if (txtUNI_NO1.GetText() != txtUNI_NO2.GetText())
                                            alert('統一編號輸入不相符，請重新輸入!');
                                        else {                                        
                                            var r = txtUNI_NO1.GetText()+";";
                                            if (!txtUNI_NO1.GetText().Uni_NoCheck())
                                                r += "【統一編號】為強制輸入";
                                            window.returnValue = r;
                                            window.close();
                                        }
                                    }
                                </script>

                                <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){commitCheck(s,e);}" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e){ window.close();return false;}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
