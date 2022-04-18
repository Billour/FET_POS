<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL10.aspx.cs" Inherits="VSS_SAL_SAL10" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div class="titlef">
            交易未結清單
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            銷售日期：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            服務屬性：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>-請選擇-</asp:ListItem>
                                <asp:ListItem>IA</asp:ListItem>
                                <asp:ListItem>Loyalty</asp:ListItem>
                                <asp:ListItem>SSI</asp:ListItem>
                                <asp:ListItem>HRS</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            狀 態：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>-請選擇-</asp:ListItem>
                                <asp:ListItem>待結帳</asp:ListItem>
                                <asp:ListItem>已審核</asp:ListItem>
                                <asp:ListItem>拒絕</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            銷售人員：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            服務類別：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>-請選擇-</asp:ListItem>
                                <asp:ListItem>全球卡</asp:ListItem>
                                <asp:ListItem>換補卡</asp:ListItem>
                                <asp:ListItem>2轉3</asp:ListItem>
                                <asp:ListItem>新啟用</asp:ListItem>
                                <asp:ListItem>續約</asp:ListItem>
                                <asp:ListItem>代收</asp:ListItem>
                                <asp:ListItem>維修</asp:ListItem>
                                <asp:ListItem>網購</asp:ListItem>
                                <asp:ListItem>預購</asp:ListItem>
                            </asp:DropDownList>
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
                <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="清除" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="divContent" runat="server" visible="false" class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="Button1" runat="server" Text="匯出" />
                            <asp:Button ID="Button2" runat="server" Text="合併結帳" />
                        </div>
                        <div class="GridScrollBar" style="height: auto">
                            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                Visible="false" OnSelectedIndexChanged="gvMaster_SelectedIndexChanged">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            狀態
                                        </th>
                                        <th scope="col">
                                            服務序號
                                        </th>
                                        <th scope="col">
                                            銷售日期
                                        </th>
                                        <th scope="col">
                                            服務屬性
                                        </th>
                                        <th scope="col">
                                            服務類別
                                        </th>
                                        <th scope="col">
                                            門號
                                        </th>
                                        <th scope="col">
                                            機台
                                        </th>
                                        <th scope="col">
                                            銷售人員
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="8" class="tdEmptyData">
                                            查無資料，請修改條件，重新查詢
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
                                    <asp:CommandField ShowDeleteButton="false" ShowEditButton="false" ShowSelectButton="true"
                                        ButtonType="Button" />
                                    <asp:BoundField DataField="狀態" HeaderText="狀態" />
                                    <asp:BoundField DataField="服務序號" HeaderText="服務序號" />
                                    <asp:BoundField DataField="銷售日期" HeaderText="銷售日期" />
                                    <asp:BoundField DataField="服務屬性" HeaderText="服務屬性" />
                                    <asp:BoundField DataField="服務類別" HeaderText="服務類別" />
                                    <asp:BoundField DataField="門號" HeaderText="門號" />
                                    <asp:BoundField DataField="機台" HeaderText="機台" />
                                    <asp:BoundField DataField="銷售人員" HeaderText="銷售人員" />
                                </Columns>
                            </asp:GridView>
                            <div class="seperate">
                            </div>
                            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                Visible="false">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            促銷編號
                                        </th>
                                        <th scope="col">
                                            促銷名稱
                                        </th>
                                        <th scope="col">
                                            商品編號
                                        </th>
                                        <th scope="col">
                                            商品名稱
                                        </th>
                                        <th scope="col">
                                            卡片序號(SIM)
                                        </th>
                                        <th scope="col">
                                            金額
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="tdEmptyData">
                                            此無明細資料
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="促銷編號" HeaderText="促銷編號" />
                                    <asp:BoundField DataField="促銷名稱" HeaderText="促銷名稱" />
                                    <asp:BoundField DataField="商品編號" HeaderText="商品編號" />
                                    <asp:BoundField DataField="商品名稱" HeaderText="商品名稱" />
                                    <asp:BoundField DataField="卡片序號(SIM)" HeaderText="卡片序號(SIM)" />
                                    <asp:BoundField DataField="金額" HeaderText="金額" />
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
    </div>
    </form>
</body>
</html>
