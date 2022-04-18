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
    <div style="text-align: center">
        <div class="seperate">
        </div>
        <table>
            <tr>
                <td class="tdtxt">
                    密碼輸入：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="TextBox1" runat="server" TextMode="Password" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="Button1" runat="server" Text="輸入" OnClick="Button1_Click" Wrap="False">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
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
                    <dx:ASPxTextBox ID="TextBox2" runat="server" Enabled="false" Width="100px">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    折抵比率：
                </td>
                <td class="tdval">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="TextBox3" runat="server" Enabled="false" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                %
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <asp:UpdatePanel ID="upddl" runat="server">
                <ContentTemplate>
                    <tr>
                        <td class="tdtxt">
                            折抵原因：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                Enabled="false">
                                <asp:ListItem>原因1</asp:ListItem>
                                <asp:ListItem>原因2</asp:ListItem>
                                <asp:ListItem>原因3</asp:ListItem>
                                <asp:ListItem>其它</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <dx:ASPxTextBox ID="TextBox4" runat="server" Enabled="false" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </ContentTemplate>
            </asp:UpdatePanel>
        </table>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table border="0" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCommit4" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
