<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL08.aspx.cs" Inherits="VSS_SAL_SAL08" %>

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
        <!--促銷商品價格查詢-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionPriceEnquiry %>" />
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--促銷代碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="只查有效料號" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--促銷名稱-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品型號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductType %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--促銷類型-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, PromotionType %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="只查詢單品(不含促銷)" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品金額-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductAmount %>" />：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="tbWidthFormat"></asp:TextBox>~
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="只查詢單品(不含促銷)" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--專案類型-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ProjectType %>" />：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddlWidthFormat">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>現行專案</asp:ListItem>
                            <asp:ListItem>過期專案</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--商品廠牌-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductBrand %>" />：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddlWidthFormat">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>商品廠牌1</asp:ListItem>
                            <asp:ListItem>商品廠牌2</asp:ListItem>
                            <asp:ListItem>商品廠牌3</asp:ListItem>
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
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Export %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--促銷代碼-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />
                            </th>
                            <th scope="col">
                                <!--促銷名稱-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />
                            </th>
                            <th scope="col">
                                <!--起迄時間-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Duration %>" />
                            </th>
                            <th scope="col">
                                <!--促銷類別-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, PromotionType %>" />
                            </th>
                            <th scope="col">
                                <!--商品料號-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />
                            </th>
                            <th scope="col">
                                <!--商品名稱-->
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />
                            </th>
                            <th scope="col">
                                <!--商品群-->
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductGroup %>" />
                            </th>
                            <th scope="col">
                                <!--金額-->
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Amount %>" />
                            </th>
                            <th scope="col">
                                <!--庫存量-->
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="9" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>" />
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="促銷代碼" HeaderText="<%$ Resources:WebResources, PromotionCode %>" />
                        <asp:BoundField DataField="促銷名稱" HeaderText="<%$ Resources:WebResources, PromotionName %>" />
                        <asp:BoundField DataField="起迄時間" HeaderText="<%$ Resources:WebResources, Duration %>" />
                        <asp:BoundField DataField="促銷類別" HeaderText="<%$ Resources:WebResources, PromotionType %>" />
                        <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                        <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                        <asp:BoundField DataField="商品群" HeaderText="<%$ Resources:WebResources, ProductGroup %>" />
                        <asp:BoundField DataField="金額" HeaderText="<%$ Resources:WebResources, Amount %>" />
                        <asp:BoundField DataField="庫存量" HeaderText="<%$ Resources:WebResources, StockQuantity %>" />
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
