<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>遠傳電信Web POS 系統</title>
    	<script language="javascript">
 			function window_onload()
 			{ document.forms[0].btnSubmit.focus(); }							
		</script>
    
    <style type="text/css">
        .style1
        {
            width: 650px;
            height: 459px;
            background-image: url('Images/Login.jpg');
        }
        .style2
        {
            width: 73px;
        }
        .style3
        {
            width: 73px;
            height: 308px;
        }
        .style4
        {
            height: 308px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" 
                AllowResize="True" CloseAction="CloseButton" PopupHorizontalAlign="Center" 
                PopupHorizontalOffset="400" PopupVerticalAlign="Middle" 
                PopupVerticalOffset="50" Width="234px" Height="37px" HeaderText="登入狀態" ShowOnPageLoad="true"
                EnableHierarchyRecreation="True" Enabled="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">帳號密碼輸入錯誤，請重新輸入！</dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <asp:Login ID="Login1" runat="server">
            <LayoutTemplate>
                <table border="0" cellpadding="1" cellspacing="0" 
                    style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0">
                                <tr>
                                    <td align="center" colspan="2">登入</td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">使用者名稱:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                            ControlToValidate="UserName" ErrorMessage="必須提供使用者名稱。" ToolTip="必須提供使用者名稱。" 
                                            ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">密碼:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                            ControlToValidate="Password" ErrorMessage="必須提供密碼。" ToolTip="必須提供密碼。" 
                                            ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="RememberMe" runat="server" Text="記憶密碼供下次使用。" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color:Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="登入" 
                                            ValidationGroup="Login1" OnClick="Button1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login> 
               
    </div>

    </form>

</body>
</html>
