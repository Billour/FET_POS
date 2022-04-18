<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT07.aspx.cs" Inherits="VSS_OPT_OPT07" %>

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
        HappyGo兌點維護作業
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        兌點代碼：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        兌點名稱：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        狀態：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
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
                    <div class="GridScrollBar"  style="height :216px">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        狀態
                                    </th>
                                    <th scope="col">
                                        兌點代號
                                    </th>
                                    <th scope="col">
                                        兌點名稱
                                    </th>
                                    <th scope="col">
                                        有效期間(起)
                                    </th>
                                    <th scope="col">
                                        有效期間(迄)
                                    </th>
                                    <th scope="col">
                                        編號(迄)
                                    </th>
                                    <th scope="col">
                                        點數
                                    </th>
                                    <th scope="col">
                                        兌換金額
                                    </th>
                                    <th scope="col">
                                        更新日期
                                    </th>
                                    <th scope="col">
                                        更新人員
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="9" class="tdEmptyData">
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
                                 <asp:CommandField ShowDeleteButton="false" ShowEditButton="True" ButtonType="Button"
                                        UpdateText="存檔" />
                                <asp:BoundField DataField="狀態" HeaderText="狀態" />
                                <asp:BoundField DataField="兌點代號" HeaderText="兌點代號" />
                                <asp:BoundField DataField="兌點名稱" HeaderText="兌點名稱" />
                                <asp:BoundField DataField="有效期間(起)" HeaderText="有效期間(起)" />
                                <asp:BoundField DataField="有效期間(迄)" HeaderText="有效期間(迄)" />
                                <asp:BoundField DataField="點數" HeaderText="點數" />
                                <asp:BoundField DataField="兌換金額" HeaderText="兌換金額" />
                                <asp:BoundField DataField="更新日期" HeaderText="更新日期" ReadOnly="true" />
                                <asp:BoundField DataField="更新人員" HeaderText="更新人員" ReadOnly="true" />
                            </Columns>
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
