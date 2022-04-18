<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutDebitCard.aspx.cs" Inherits="VSS_CheckOut_CheckOutDebitCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>金融卡金額輸入</title>
</head>
<body>
   
    <form id="form1" runat="server">
    <div class="checkOutDiv">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
            <div class="seperate"></div>
                <table>
                    <tr>
                        <td class="tdtxt">
                           金融卡金額：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="btnPosition">
                    <asp:Button ID="btnCommit1" runat="server" Text="確定" OnClick="btnCommit1_Click" />
                    <asp:Button ID="btnCommit4" runat="server" 
                        OnClientClick="window.close();return false;" Text="取消" Visible="false"/>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
               <div class="seperate"></div>
                <table>
                    <tr>
                        <td colspan="2" style="width: 200px; height: 200px; background-color: Silver" align="center"
                            valign="middle">
                            請刷卡
                        </td>
                    </tr>
                </table>
                <div class="btnPosition">
                    <asp:Button ID="btnCommit2" runat="server" Text="確定" OnClick="btnCommit2_Click" />
                    <asp:Button ID="btnCommit5" runat="server" 
                        OnClientClick="window.close();return false;" Text="取消" Visible="false"/>
                </div>
            </asp:View>
            <asp:View ID="View3" runat="server">
             <div class="seperate"></div>
                <table>
                    <tr>
                        <td colspan="2" style="width: 200px; height: 200px; background-color: Silver" align="center"
                            valign="middle">
                            扣款失敗，請重新選擇付款方式
                        </td>
                    </tr>
                </table>
                <div class="btnPosition">
                    <asp:Button ID="btnCommit3" runat="server" Text="確定" OnClientClick="window.close();return false;" />
                    <asp:Button ID="btnCommit6" runat="server" 
                        OnClientClick="window.close();return false;" Text="取消" Visible="false"/>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
