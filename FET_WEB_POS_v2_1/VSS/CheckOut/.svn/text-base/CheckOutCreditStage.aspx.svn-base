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
                <table>
                <tr>
                        <td class="tdtxt">
                            銀行別：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" SelectedIndex="0" Width="100px">
                                <Items>
                                    <dx:ListEditItem Text="--請選擇--" Value="--請選擇--" />
                                    <dx:ListEditItem Text="遠東商銀" Value="遠東商銀" />
                                    <dx:ListEditItem Text="中國信託" Value="中國信託" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            期數：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" SelectedIndex="0" Width="100px">
                                <Items>
                                    <dx:ListEditItem Text="--請選擇--" Value="--請選擇--" />
                                    <dx:ListEditItem Text="6" Value="6" />
                                    <dx:ListEditItem Text="12" Value="12" />
                                    <dx:ListEditItem Text="24" Value="24" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            信用卡金額：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2" align="center">
                            <table border="0" cellpadding="0" cellspacing="0" align="center">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnCommit1" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClick="btnCommit1_Click">
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
                        </td>
                     </tr>
                </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table>
                    <tr>
                        <td style="width: 200px; height: 200px; background-color: Silver" align="center" valign="middle">
                            請刷卡
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <table border="0" cellpadding="0" cellspacing="0" align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnCommit2" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClick="btnCommit2_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                                    </dx:ASPxButton>
                                </td>

                            </tr>
                        </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <table>
                    <tr>
                        <td style="width: 200px; height: 200px; background-color: Silver" align="center" valign="middle">
                            授權失敗，請重新選擇付款方式
                        </td>
                    </tr>
                     <tr>
                        <td align="center">
                            <dx:ASPxButton ID="btnCommit3" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                                <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
