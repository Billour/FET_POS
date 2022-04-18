<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainFrame.aspx.cs" Inherits="MainFrame" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>遠傳電信-WebPOS</title>
    <script type="text/javascript" src="ClientUtility/shortcut.js">
        
    </script>
    <script type="text/javascript">
        shortcut.add('Ctrl+Shift+X', function() {
            alert('F6');
        });
    </script>
</head>
<frameset rows="61,*" id="rowFrame" framespacing="0" border="0" frameborder="0">
	<frame name="Toolbar" scrolling="no" noresize src="./Top.aspx">
	<frameset cols="13%,87%" framespacing="3" border="1" id="colFrame">
		<frame name="menu" style="border-right: #99ccff 1px solid; border-top: #003366 1px solid"
			target="main" src="./TreeMenu.aspx" scrolling="auto">
		<frame name="Working" style="border-left: #99ccff 2px groove" 
		border="0" frameborder="0" src="">
	</frameset>		
</frameset>
<noframes>
    <body>「此網頁使用框架，但是您的瀏覽器並不支援」</body>
</noframes> 
</html>
