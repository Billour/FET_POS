<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutUnionCard.aspx.cs"
    Inherits="VSS_CheckOut_CheckOutUnionCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="titlef">
                    請輸入聯名卡資料</div>
                <table>
                    <tr>
                        <td colspan="2" style="width: 200px; height: 200px; background-color: Silver" align="center"
                            valign="middle">
                            請插入聯名卡
                        </td>
                    </tr>
                </table>
                <div class="btnPosition">
                    <asp:Button ID="btnCommit1" runat="server" Text="確定" OnClick="btnCommit1_Click" />
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div class="titlef">
                    請輸入加值資料</div>
                <table>
                    <tr>
                        <td class="tdtxt">
                            聯名卡：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label4" runat="server">(刷卡機帶入,不可改)</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            卡片餘額：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label1" runat="server">(刷卡機帶入,不可改)</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            金額：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="8"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="btnPosition">
                    <asp:Button ID="btnCommit2" runat="server" Text="確定" OnClick="btnCommit2_Click" />
                </div>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="titlef">
                    訊息視窗</div>
                <table>
                    <tr>
                        <td colspan="2" style="width: 200px; height: 200px; background-color: Silver" align="center"
                            valign="middle">
                            刷卡失敗，請重新刷卡
                        </td>
                    </tr>
                </table>
                <div class="btnPosition">
                    <asp:Button ID="btnCommit3" runat="server" Text="確定" OnClientClick="window.close();return false;" />
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
