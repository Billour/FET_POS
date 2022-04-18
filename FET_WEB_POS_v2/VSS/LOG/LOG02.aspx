<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG02.aspx.cs" Inherits="VSS_LOG_LOG02" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
                        <!--角色查詢-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RoleSearch %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Add %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--角色代碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, RoleID %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--角色名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, RoleName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--角色狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, RoleStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>已失效</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock" id="div1" runat="server" visible="false">
            <div class="GridScrollBar" style="height: auto">
                <asp:GridView ID="gvMaster" runat="server" Visible="false" AutoGenerateColumns="False"
                    CssClass="mGrid" OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                    OnRowUpdating="gvMaster_RowUpdating">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--狀態-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--角色代碼-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, RoleID %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--角色名稱-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, RoleName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--備註說明-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Description %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--更新日期-->                                
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--更新人員-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6" class="tdEmptyData">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckAll" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckItem" runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" ReadOnly="true" />
                        <asp:BoundField DataField="角色代碼" HeaderText="<%$ Resources:WebResources, RoleID %>" />
                        <asp:BoundField DataField="角色名稱" HeaderText="<%$ Resources:WebResources, RoleName %>" />
                        <asp:BoundField DataField="備註說明" HeaderText="<%$ Resources:WebResources, Description %>" />
                        <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
                        <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
        </div>
    </div>
    </form>
</body>
</html>
