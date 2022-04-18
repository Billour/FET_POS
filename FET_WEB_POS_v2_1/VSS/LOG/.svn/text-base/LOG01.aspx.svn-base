<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG01.aspx.cs"    Inherits="VSS_LOG_LOG01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

 

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>           
        <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, UserLogin %>"></asp:Literal>
    </title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
    
        <div class="criteria">
            <table>
                <tr class="titlef">
                    <td colspan="2" >
                        <!--使用者登入-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, UserLogin %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"  >
                        <!--請輸入帳號及密碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, UserLoginCaption %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--店點-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, PointOfSale %>"></asp:Literal>：
                     </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--帳號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Account %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--密碼-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Password %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <input id="Password2" type="password" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            </asp:UpdatePanel>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>"  />
            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClientClick="window.close();return false;" />            
        </div>
    </div>
    </form>
</body>
</html>
