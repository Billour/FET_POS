<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCredit.aspx.cs" Inherits="VSS_CheckOut_CheckOutCredit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信用卡金額輸入</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(function() {
            $("#btnStep2_OK").click(function() {
                window.close();
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="divStep0" runat="server" class="checkOutDiv">
            <table>
                <tr>
                    <td>
                        信用卡金額：
                    </td>
                    <td>
                        <input type="text" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <table border="0" cellpadding="0" cellspacing="0" align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnStep0_OK" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClick="btnStep0_OK_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnStep0_Cancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divStep1" runat="server" class="checkOutDiv">
            <table>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 200px; height: 200px; background-color: Silver" align="center"
                        valign="middle">
                        請刷卡
                    </td>
                </tr>
                <tr>
                    <td>
                        <table align="center">
                            <tr>
                                <td  align="center">
                                    <dx:ASPxButton ID="btnStep1_OK" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClick="btnStep1_OK_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td  align="center">
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){window.close();}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        
        <div id="divStep2" runat="server" style="display: none">
            <table>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        信用卡卡號：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label1" runat="server" Text="1111-2222-3333-4444"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        卡別：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label2" runat="server" Text="VISA"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        發卡銀行：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="中國信託"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        期限：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="10/2013"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <dx:ASPxButton ID="btnStep2_OK" runat="server" Text="<%$ Resources:WebResources, Ok %>">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divStep2_1" runat="server" align="center" style="display: none">
            <table>
                <tr>
                    <td style="width: 200px; height: 200px; background-color: Silver" align="center"
                        valign="middle">
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
        </div>
        <div id="divsal03_2" runat="server" align="center">
            <table>
                <tr>
                    <td style="width: 300px; height: 200px; background-color: Silver" align="center"
                        valign="middle">
                        請於刷卡機，刷退原交易信用卡金額
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <dx:ASPxButton ID="ASPxButton4" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
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
