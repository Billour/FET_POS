<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01AddItem.aspx.cs" Inherits="VSS_SA01_SA01AddItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script> 
    <script type ="text/javascript" language ="javascript" >
        $(function() {
            $("#btnOK").click(function() {
                window.close();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table>
            <tr>    
                <td  class="tdtxt">
                   <!--促銷代碼-->
                   <asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td class="tdtxt">
                    <!--料號-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, MaterialNo %>"></asp:Literal>：
                </td>
                <td class="tdval">  
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            </table> 
        <asp:Button ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>" />     
    </div>
    </form>
</body>
</html>
