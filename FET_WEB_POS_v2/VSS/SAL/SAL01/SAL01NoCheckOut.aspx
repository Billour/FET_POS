<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01NoCheckOut.aspx.cs" Inherits="VSS_SAL_SA01NoCheckOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../../ClientUtility/jquery.js" type="text/javascript"></script> 
   
</head>
<body>
    <form id="form1" runat="server">
     <script type ="text/javascript" language ="javascript" >
         $(function() {
             $("#btnOK").click(function() {
                 window.close();
             });
         });
    </script>
    <div>
        <table>
            <tr>
                <td class="tdtxt">
                    <!--門號-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, MobileNumber %>" />：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--交易日期-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TradeDate %>" />：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--處理人員-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProcessedBy %>" />：
                </td>
                <td class="tdval"> 
                    <asp:Label ID="Label1" runat="server" Text="王大寶"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" onclick="btnSearch_Click" />
        
        
        <div id ="divContent" runat ="server"  style ="display :none">
         <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="門號" HeaderText="<%$ Resources:WebResources, MobileNumber %>" />
                <asp:BoundField DataField="交易日期" HeaderText="<%$ Resources:WebResources, TradeDate %>" />
                <asp:BoundField DataField="服務類別" HeaderText="<%$ Resources:WebResources, ServiceClass %>" />
            </Columns>
        </asp:GridView>
        <br />
            <asp:Button ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>" />
        </div>
    </div>
    </form>
</body>
</html>
