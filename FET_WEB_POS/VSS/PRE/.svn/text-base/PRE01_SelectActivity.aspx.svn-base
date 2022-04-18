<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PRE01_SelectActivity.aspx.cs" Inherits="VSS_PRE_PRE01_SelectActivity" %>

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
        <td align="right" style="width:30%">
        <!--預購活動-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderActivity %>"></asp:Literal>：
       </td>
       <td><asp:DropDownList ID="ddlActivity" runat="server" 
               onselectedindexchanged="ddlActivity_SelectedIndexChanged" 
               AutoPostBack="True" Width="100%" /> </td>
       </tr>
       
       <tr><td align="right"><!--最低預付訂金-->
       <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, MinimumPrepaidDeposit %>"></asp:Literal>：</td>
       <td><asp:Label runat="server" ID="lbPrepaid" /></td></tr>
      
       </table>
        <div class="btnPosition">
          
            <asp:Button ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClientClick="window.close();return false;" />
            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClientClick="window.close();return false;" />
               
        </div>
    </div>
    </form>
</body>
</html>
