<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV03.aspx.cs" Inherits="VSS_INV03_INV03" %>

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
            <!--庫存查詢作業-->
             <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StockSearch %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table>
             <tr>                    
                    <td class="tdtxt">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                        <asp:ListItem>ALL</asp:ListItem>
                        <asp:ListItem>北一區</asp:ListItem>
                        <asp:ListItem>北二區</asp:ListItem>
                        <asp:ListItem>中二區</asp:ListItem>
                        <asp:ListItem>南一區</asp:ListItem>                        
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox4" runat="server" Text="2101"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server" Text="遠企"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                   
                    <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Warehouse %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem>ALL</asp:ListItem>
                        <asp:ListItem Selected="True">銷售倉</asp:ListItem>
                        <asp:ListItem>展示倉</asp:ListItem>
                        <asp:ListItem>維修倉</asp:ListItem>
                        <asp:ListItem>租賃倉</asp:ListItem>                        
                        </asp:DropDownList>
                    </td>
                     <td class="tdtxt">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                        
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>               
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                    AllowPaging="True" PageSize="12" PagerStyle-HorizontalAlign="Right" 
                    OnPageIndexChanging="GridView_PageIndexChanging">
                     <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--區域別-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市編號-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市名稱-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品編號-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                        
                                    </th>
                                    <th scope="col">
                                        <!--出貨倉別-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Warehouse %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--數量-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Quantity %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                     <td colspan="9" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                                </tr>
                            </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="區域別" HeaderText="<%$ Resources:WebResources, ByDistrict %>" />
                        <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                        <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />
                        <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                        <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />                        
                        <asp:BoundField DataField="出貨倉別" HeaderText="<%$ Resources:WebResources, Warehouse %>" />
                        <asp:BoundField DataField="數量" HeaderText="<%$ Resources:WebResources, Quantity %>" />
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
                    <PagerStyle HorizontalAlign="Right" />
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
