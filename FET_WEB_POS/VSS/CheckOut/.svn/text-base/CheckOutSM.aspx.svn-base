<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutSM.aspx.cs" Inherits="VSS_CheckOut_CheckOutSM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>店長折扣</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="seperate">
        </div>
        <table>
            <tr>
                <td class="tdtxt">
                    密碼輸入：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="輸入" onclick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    剩餘可折抵金額：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label1" runat="server" Text="99,999"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    折抵金額：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox2" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <font color="red">折抵比率：</font>
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox3" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <asp:UpdatePanel ID="upddl" runat="server">
                <ContentTemplate>
                    <tr>
                        <td class="tdtxt">
                            折抵原因：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Enabled="false">
                                <asp:ListItem>原因1</asp:ListItem>
                                <asp:ListItem>原因2</asp:ListItem>
                                <asp:ListItem>原因3</asp:ListItem>
                                <asp:ListItem>其它</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:TextBox ID="TextBox4" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </ContentTemplate>
            </asp:UpdatePanel>
        </table>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnOK" runat="server" Text="確定" OnClientClick="window.close();return false;" />
            <asp:Button ID="btnCommit4" runat="server" OnClientClick="window.close();return false;"
                Text="取消" />
        </div>
    </div>
    </form>
</body>
</html>
