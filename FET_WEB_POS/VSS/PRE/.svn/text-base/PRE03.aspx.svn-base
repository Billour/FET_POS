<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PRE03.aspx.cs" Inherits="VSS_PRE03_PRE03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div class="titlef">
            <!--預購活動設定作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderActivitySetting %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--活動代號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ActivityNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--活動名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ActivityName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label1" runat="server" Text="00-未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--有效期間-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, EffectiveDuration %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="2010/07/01 22:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">                        
                        <!--訂金-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Deposit %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>不需要</asp:ListItem>
                            <asp:ListItem>需要</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="tdtxt">
                        <!--最低預購訂金-->
                         <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, MinimumPreOrderDeposit %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--維護人員-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
                </table>
        </div>
        <div class="seperate">
        </div>
        <div>
            <div class="SubEditBlock">
                <div class="GridScrollBar" style="height: auto">
                    <div class="SubEditCommand">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="Button1_Click" />                     
                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                    </div>
                    <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                    <!--商品料號-->
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品名稱-->
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>                                    
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2" class="tdEmptyData">
                                    <!--choose add button-->
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal> 
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                            <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" />
            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
        </div>
    </div>
    </form>
</body>
</html>
