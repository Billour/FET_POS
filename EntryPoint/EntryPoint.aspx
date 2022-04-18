<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntryPoint.aspx.cs" Inherits="EntryPoint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td>      
                <asp:Label ID="lbl01" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>     
                <asp:Label ID="lbl02" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl03" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td>      
                <asp:Label ID="lbl04" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td>   
                <asp:Label ID="lbl05" runat="server"></asp:Label> 
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl06" runat="server"></asp:Label>      
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gv01" runat="server">
                </asp:GridView>
               
            </td>
        </tr>
       
    </table>
    
    </div>
    </form>
</body>
</html>
