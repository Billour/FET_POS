<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT09.aspx.cs" Inherits="VSS_OPT_OPT09" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="titlef">
        員工基本資料查詢
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        員工編號：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        員工姓名：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        員工類別：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>員工類別1</asp:ListItem>
                            <asp:ListItem>員工類別2</asp:ListItem>
                            <asp:ListItem>員工類別3</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        門市編號：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        成本中心：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        狀態：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>過期</asp:ListItem>
                            <asp:ListItem>已停止</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="清除" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server" class="SubEditBlock" visible="false">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnNew" runat="server" Text="新增" Visible="false" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" Visible="false" />
                    </div>
                    <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        AllowPaging="True" OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                        OnRowUpdating="gvMaster_RowUpdating" PageSize="5" PagerStyle-HorizontalAlign="Right"
                        OnPageIndexChanging="GridView_PageIndexChanging">
                        <PagerStyle HorizontalAlign="Right" />
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                    狀態
                                </th>
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
                                <th scope="col">
                                    更新日期
                                </th>
                                <th scope="col">
                                    更新人員
                                </th>
                            </tr>
                            <tr>
                                <td colspan="7" class="tdEmptyData">
                                    choose add button
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
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="存檔" CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="取消" CommandName="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="編輯"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="狀態" HeaderText="狀態" />
                            <asp:BoundField DataField="門市編號" HeaderText="門市編號" />
                            <asp:BoundField DataField="門市名稱" HeaderText="門市名稱" />
                            <asp:BoundField DataField="員工編號" HeaderText="員工編號" />
                            <asp:BoundField DataField="員工姓名" HeaderText="員工姓名" />
                            <asp:BoundField DataField="更新日期" HeaderText="更新日期" ReadOnly="true" />
                            <asp:BoundField DataField="更新人員" HeaderText="更新人員" ReadOnly="true" />
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
                            <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                            頁
                            <asp:Button ID="btnGoToIndex" runat="server" Text="GO" OnClick="btnGoToIndex_Click" />
                        </PagerTemplate>
                    </asp:GridView>
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
