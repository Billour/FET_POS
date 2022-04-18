<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG03a.aspx.cs" Inherits="VSS_LOG_LOG03a" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, UserRoleMapping %>"></asp:Literal>
    </title>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--角色使用者對應作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, UserRoleMapping %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSearch" runat="server" Text="查詢" />
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--角色代碼-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, RoleID %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--角色名稱-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, RoleName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--角色狀態-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, RoleStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdtxt">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>已失效</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--備註說明-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, RemarkAndDescription %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:TextBox ID="TextBox1" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        &nbsp;<asp:Label ID="Label7" runat="server" Text="2010/08/07"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" >
                        
                        
                    </td>
                    <td >
                       <asp:CheckBox ID="CheckBox1" runat="server" Text="從SSO同步過來的角色" />
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label8" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
        </div>
        <div class="seperate">
        </div>
       <div class="SubEditBlock">
            <div class="SubEditCommand">
                <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Add %>" /><asp:Button
                    ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            </div>
            <div class="GridScrollBar" style="height: auto">
                <asp:GridView ID="gvMobileStock" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                門市編號
                            </th>
                            <th scope="col">
                                門市名稱
                            </th>
                            <th scope="col">
                                員工編號
                            </th>
                            <th scope="col">
                                員工姓名
                            </th>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdEmptyData">
                                請點選新增按鍵增加資料
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox2" runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                        <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />
                        <asp:BoundField DataField="員工編號" HeaderText="<%$ Resources:WebResources, EmployeeNo %>" />
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, EmployeeNo %>" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="label556" runat="server" Text='<%# Bind("員工編號") %>'></asp:Label>
                                <asp:Button ID="Button94" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('SearchEmpNum.aspx');return false;" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="員工姓名" HeaderText="<%$ Resources:WebResources, EmployeeName %>" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>                            
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="儲存" />
            <asp:Button ID="btnClear" runat="server" Text="取消" />
        </div>
        <div class="btnPosition">
        </div>
    </div>
    </form>
</body>
</html>
