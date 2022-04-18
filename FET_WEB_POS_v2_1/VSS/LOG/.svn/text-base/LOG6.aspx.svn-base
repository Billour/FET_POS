<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG6.aspx.cs" Inherits="VSS_LOG_LOG6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        <!--店長折扣密碼設定-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DiscountPasswordManager %>"></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--有效日期-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ValidDate %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3" nowrap="nowrap">
                    <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                        <tr>
                            <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ></dx:ASPxDateEdit></td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ></dx:ASPxDateEdit></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--輸入密碼-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, InputPassword %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="pw" runat="server" TextMode="Password" />
                </td>
            </tr>
            <td class="tdtxt">
                <!--確認密碼-->
                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ConfirmPassword %>"></asp:Literal>：
            </td>
            <td class="tdval">
                <asp:TextBox ID="cpw" runat="server" TextMode="Password" />
            </td>
            <tr>
                <td class="tdtxt">
                    <!--更新日期-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval" visible="true">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                OnClick="btnCommit_Click" Style="height: 21px" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                OnClientClick="window.close();return false;" />
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" visible="true">
                      
                    </td>
                    <td class="tdval" visible="true">
                      <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    </td>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
