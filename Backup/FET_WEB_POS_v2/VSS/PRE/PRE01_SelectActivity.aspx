<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PRE01_SelectActivity.aspx.cs"
    Inherits="VSS_PRE_PRE01_SelectActivity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>選擇預購商品</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" width="95%">
            <tr>
                <td align="right" style="width: 30%">
                    <!--預購活動-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderActivity %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxComboBox ID="ddlActivity" runat="server" Width="100" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged">
                        <Items>
                            <dx:ListEditItem Value="請選擇" Text="請選擇" Selected="true" />
                            <dx:ListEditItem Value="PR-010203 iPhone4G預購" Text="PR-010203 iPhone4G預購" />
                            <dx:ListEditItem Value="PR-020103 HTC Desire HD 預購" Text="PR-020103 HTC Desire HD 預購" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <!--最低預付訂金-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, MinimumPrepaidDeposit %>"></asp:Literal>：
                </td>
                <td>
                    <asp:Label runat="server" ID="lbPrepaid" />
                </td>
            </tr>
        </table>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td align="right">
                        <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" >
                            <ClientSideEvents Click="function(s, e) {
	                            window.close();return false;
                            }" />
                        </dx:ASPxButton>
                    </td>
                    <td align="left">
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                            <ClientSideEvents Click="function(s, e) {
	window.close();return false;
}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
