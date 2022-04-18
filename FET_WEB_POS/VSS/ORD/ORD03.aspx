<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD03.aspx.cs" Inherits="VSS_ORD03_ORD03" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        <!--訂單報表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderReport %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--區域別-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlArea" runat="server">
                            <asp:ListItem Value="0">全選</asp:ListItem>
                            <asp:ListItem Value="1">北一區</asp:ListItem>
                            <asp:ListItem Value="2">北二區</asp:ListItem>
                            <asp:ListItem Value="3">中一區</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--訂單狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlOrdStatus" runat="server">
                            <asp:ListItem Value="0">全選</asp:ListItem>
                            <asp:ListItem Value="1">完成</asp:ListItem>
                            <asp:ListItem Value="1">作廢</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--訂單編號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="txtOrdNoStart" runat="server"></asp:TextBox>&nbsp;<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="txtOrdNoEnd" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--店組代碼-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreCategoryCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="txtStoreNoStart" runat="server"></asp:TextBox>&nbsp;<asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="txtStoreNoEnd" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--訂單日期-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                         <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="txtOrdDateStart" runat="server" Width="130"></asp:TextBox><img id="img1" src="~/Icon/calendar.jpg"  runat="server" /> 
                         <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="txtOrdDateEnd" runat="server" Width="130"></asp:TextBox><img id="img2" src="~/Icon/calendar.jpg"  runat="server" />
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy/MM/dd"
                                TargetControlID="txtOrdDateEnd" PopupButtonID="img2">
                            </asp:CalendarExtender>                                                                
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--料號-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, MaterialNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="txtParNoStart" runat="server"></asp:TextBox>&nbsp;<asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="txtParNoEnd" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
