<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DemoTemplate.aspx.cs" Inherits="VSS_ACL_DemoTemplate"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>
            角色維護作業DemoTemplate</h3>
        <table width="100%">
            <tr>
                <td style="width: 15%">
                    角色代碼
                </td>
                <td style="width: 18%">
                    <asp:TextBox ID="tbRoleCode" runat="server"></asp:TextBox>
                </td>
                <td style="width: 15%">
                    角色名稱
                </td>
                <td style="width: 18%">
                    <asp:TextBox ID="tbRoleName" runat="server"></asp:TextBox>
                </td>
                <td style="width: 15%">
                    &nbsp;
                </td>
                <td style="width: 19%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" />
        <asp:Button ID="btnNew" runat="server" Text="新增" OnClick="btnNew_Click" />
        <asp:Button ID="btnClear" runat="server" Text="清除" OnClick="btnClear_Click" />
        <hr>
        <br />
        <div id="divNewBlock" runat="server" style="display: none">
            <table>
                <tr>
                    <td>
                        角色代碼
                    </td>
                    <td>
                        <asp:TextBox ID="tbNewRoleCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        角色名稱
                    </td>
                    <td>
                        <asp:TextBox ID="tbNewRoleName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        說明
                    </td>
                    <td>
                        <asp:TextBox ID="tbNewRoleMemo" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnNewOK" runat="server" Text="確定" OnClick="btnNewOK_Click" />
                        <asp:Button ID="btnNewCancel" runat="server" Text="取消" OnClick="btnNewCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divDataBlock" runat="server">
        <div id ="dataScoll" style ="overflow:auto; height:200px; width:400px ">
            <asp:GridView ID="grdRoleData" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grdRoleData_RowCancelingEdit"
                OnRowDeleting="grdRoleData_RowDeleting" OnRowEditing="grdRoleData_RowEditing"
                OnRowUpdating="grdRoleData_RowUpdating">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="ROLENO" HeaderText="NO" ReadOnly="true" />
                    <asp:BoundField DataField="ROLECODE" HeaderText="角色代碼" ReadOnly="true" />
                    <asp:TemplateField HeaderText="角色名稱">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbRoleName" runat="server" Text='<%# Bind("ROLENAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ROLENAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="說明">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbRoleMemo" runat="server" Text='<%# Bind("ROLEMEMO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ROLEMEMO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
            <br />
            <br />
            <br />
            <asp:GridView ID="grdDemo1" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                PageSize="5" OnPageIndexChanging="GridView_PageIndexChanging"
                 OnRowDataBound="grdDemo_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ROLENO" HeaderText="NO" ReadOnly="true" />
                    <asp:BoundField DataField="ROLECODE" HeaderText="角色代碼" ReadOnly="true" />
                    <asp:TemplateField HeaderText="角色名稱">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ROLENAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="說明">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ROLEMEMO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                    <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Page" CommandArgument="First"
                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/first.png" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/previous.png" /></asp:LinkButton>
                    第
                    <asp:Label ID="lblCurrPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                    <asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                    <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                    <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                    到第
                    <asp:TextBox ID="tbGoToIndex" runat="server" Width="20" AutoCompleteType="None"></asp:TextBox>
                    頁
                    <asp:Button ID="btnGoToIndex" runat="server" Text="GO" OnClick="btnGoToIndex_Click" />
                </PagerTemplate>
            </asp:GridView>
        </div>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
