<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCreditStage.aspx.cs"
    Inherits="VSS_CheckOut_CheckOutCreditStage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分期付款卡輸入</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="checkOutDiv">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="seperate">
                </div>
                <table>
                    <tr>
                        <td class="tdtxt">
                            信用卡金額：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
                <div class="btnPosition">
                    <table border="0" cellpadding="0" cellspacing="0" align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnCommit1" runat="server" Text="確定" OnClick="btnCommit1_Click">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCommit4" runat="server" Text="取消" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div class="seperate">
                </div>
                <table>
                    <tr>
                        <td style="width: 200px; height: 200px; background-color: Silver" align="center"
                            valign="middle">
                            請刷卡
                        </td>
                    </tr>
                </table>
                <div class="btnPosition">
                    <table border="0" cellpadding="0" cellspacing="0" align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnCommit2" runat="server" Text="確定" OnClick="btnCommit2_Click">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCommit5" runat="server" Text="取消" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <div class="seperate">
                </div>
                <table>
                    <tr>
                        <td style="width: 200px; height: 200px; background-color: Silver" align="center"
                            valign="middle">
                            授權失敗，請重新選擇付款方式
                        </td>
                    </tr>
                </table>
                <div class="btnPosition">
                    <table border="0" cellpadding="0" cellspacing="0" align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnCommit3" runat="server" Text="確定" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCommit6" runat="server" Text="取消" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
