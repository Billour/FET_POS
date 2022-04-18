<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV17_1.aspx.cs" Inherits="VSS_INV_INV17_1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div> <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
        <div runat="server" id="div2">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--關帳日設定-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ClosingDateSettings %>"></asp:Literal>
                    </td>
                   
                    <td align="right">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Search %>" />
                    </td>
                </tr>
            </table>
            <div style="text-align:center">
                <table>
                    <tr>
                        <td >
                            <!--關帳年月-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ClosingYearMonth %>"></asp:Literal>：
                        </td>
                        <td >
                            <asp:TextBox ID="TextBox512" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="TextBox512_CalendarExtender" runat="server"  Format="yyyy/MM"
                                TargetControlID="TextBox512" DefaultView="Months" 
                                TodaysDateFormat="yyyy/MM">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        
                        <td >
                            <!--關帳日-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ClosingDate %>"></asp:Literal>：
                        </td>
                        <td >
                            <asp:TextBox ID="TextBox513" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd"
                                TargetControlID="TextBox513" DefaultView="Days" 
                                TodaysDateFormat="yyyy/MM">
                            </asp:CalendarExtender>
                        </td>
                       
                    </tr>
                    <tr>
                    
                        <td >
                            <!--更新人員-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td >
                            <asp:Label ID="Label5" runat="server" Text="12345 王大寶"></asp:Label>
                        </td>
                       
                      
                    </tr>
                    <tr>
                     
                        <td >
                            <!--更新日期-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="2010/07/01 22:00"></asp:Label>
                        </td>
                       
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClientClick="document.location='INV17.aspx';return false;" />
                <asp:Button ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Reset %>" OnClientClick="window.close();return false;" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
