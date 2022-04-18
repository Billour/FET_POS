<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Style/main_style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="ClientUtility/shortcut.js"></script>
    <script type="text/javascript">
        shortcut.add('Ctrl+Shift+X', function() {
            alert('Hi there!');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
        <img src="images/logo.gif" alt="遠傳電信LOGO" width="165" height="45" id="fet_logo" /><img
            src="images/hd_middle.jpg" width="340" height="45" alt="Slogan" />
        <!-- end .header -->
    </div>
    <div class="login">
        <span style="float: left"><%=System.DateTime.Now.ToString("yyyy/MM/dd HH:mm") %></span> <span style="float: right">遠企門市 銷售專員:
            64591 李家駿 | 登出 </span>
        <!-- end .login -->
    </div>
    </form>
</body>
</html>
