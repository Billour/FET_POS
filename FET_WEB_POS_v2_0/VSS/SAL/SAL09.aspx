<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL09.aspx.cs" Inherits="VSS_SAL_SAL09" %>

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
            <!--交易暫存清單-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TemporaryTransactionList %>"></asp:Literal>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--銷售日期-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SalesDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--服務屬性-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ServiceNature %>"></asp:Literal>：
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
                            <!--狀 態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
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
                            <!--銷售人員-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--服務類別-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ServiceClass %>"></asp:Literal>：
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
                    <tr>
                        <td class="tdtxt">
                            <!--機 台-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CashRegister %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox8" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
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
                <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="divContent" runat="server" visible="false" class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, ConsolidatedCheckout %>" />
                        </div>
                        <div class="GridScrollBar" style="height: auto">
                            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                Visible="false" OnSelectedIndexChanged="gvMaster_SelectedIndexChanged">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            <!--狀態-->
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--服務序號-->
                                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ServiceNo %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--銷售日期-->
                                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, SalesDate %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--服務屬性-->
                                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ServiceNature %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--服務類別-->
                                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ServiceClass %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--門號-->
                                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, MobileNumber %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--機台-->
                                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, CashRegister %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--銷售人員-->
                                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="8" class="tdEmptyData">
                                            <!--查無資料，請修改條件，重新查詢-->
                                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
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
                                    <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
                                    <asp:BoundField DataField="服務序號" HeaderText="<%$ Resources:WebResources, ServiceNo %>" />
                                    <asp:BoundField DataField="銷售日期" HeaderText="<%$ Resources:WebResources, SalesDate %>" />
                                    <asp:BoundField DataField="服務屬性" HeaderText="<%$ Resources:WebResources, ServiceNature %>" />
                                    <asp:BoundField DataField="服務類別" HeaderText="<%$ Resources:WebResources, ServiceClass %>" />
                                    <asp:BoundField DataField="門號" HeaderText="<%$ Resources:WebResources, MobileNumber %>" />
                                    <asp:BoundField DataField="機台" HeaderText="<%$ Resources:WebResources, CashRegister %>" />
                                    <asp:BoundField DataField="銷售人員" HeaderText="<%$ Resources:WebResources, SalesClerk %>" />
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
