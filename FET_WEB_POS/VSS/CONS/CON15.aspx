<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON15.aspx.cs" Inherits="VSS_CON15_CON15" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品盤點查詢作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductInventorySearch %>"></asp:Literal>
                </td>
                <td align="right">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--盤點單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                   <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" >
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--盤點狀態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, InventoryStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Value="">ALL</asp:ListItem>
                            <asp:ListItem Value="未盤點">未盤點</asp:ListItem>
                            <asp:ListItem Value="已盤點">已盤點</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--盤點日期-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                    </td>
                      <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
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
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>--%>
                <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" 
                    OnPageIndexChanging="GridView_PageIndexChanging" onrowdatabound="gvMaster_RowDataBound">

<PagerStyle HorizontalAlign="Right"></PagerStyle>
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--盤點單號-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--盤點日期-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>
                            </th>                           
                            <!--th scope="col">                               
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                            </th>
                            <th scope="col">                               
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                            </th-->
                            <th scope="col">
                                <!--盤點狀態-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, InventoryStatus %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--人員-->
                                <asp:Literal ID="Literal14" runat="server" Text="盤點人員"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--日期-->
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources,ModifiedDate %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="9" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                         <asp:TemplateField HeaderText="<%$ Resources:WebResources, InventoryNo %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("盤點單號") %>' CommandName="select"></asp:LinkButton>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("盤點單號") %>' 
                                        NavigateUrl='<%# Eval("盤點單號", "~/VSS/CONS/CON16.aspx?No={0}") %>'></asp:HyperLink>
                                
                            </ItemTemplate>
                            <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("盤點單號") %>' NavigateUrl='<%# Eval("盤點單號", "~/VSS/CONS/CON16.aspx?No={0}") %>'></asp:HyperLink>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="盤點日期" HeaderText="<%$ Resources:WebResources, InventoryDate %>" />
                        <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" Visible="false" />
                        <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" Visible="false" />
                        <asp:BoundField DataField="盤點狀態" HeaderText="<%$ Resources:WebResources, InventoryStatus %>" />
                        <asp:BoundField DataField="人員" HeaderText="盤點人員" />
                        <asp:BoundField DataField="日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <PagerTemplate>
                        <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Page" CommandArgument="First"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>"> <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/first.png" />
                        
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>"> <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/previous.png" /></asp:LinkButton>
                        第
                        <asp:Label ID="lblCurrPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                        <asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                        <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>"> <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>"> <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                        到第
                        <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                        頁
                        <asp:Button ID="btnGoToIndex" runat="server" Text="GO" OnClick="btnGoToIndex_Click" />
                    </PagerTemplate>
                </asp:GridView>
                <div class="seperate"></div>
               <%-- <div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false">
                    <div class="SubEditCommand">
                        <asp:Label ID="Label5" runat="server" Text="盤點單號:STC2010072801" ForeColor="White"></asp:Label>
                    </div>
                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        Visible="False">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                    <!--商品編號->
                                     <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品名稱-->
                                     <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                </th>
                               <th scope="col">
                                    <!--廠商編號-->
                                     <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--廠商名稱-->
                                     <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--庫存量-->
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--門市盤點量-->
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PhysicalInventory %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--盤差量-->
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, DifferenceQuantity %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="9" class="tdEmptyData">
                                    <!--此無明細資料-->
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                             <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources ,ProductName %>">
                                <EditItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:BoundField DataField="廠商編號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" ReadOnly="true" />
                            <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" ReadOnly="true" />
                            <asp:BoundField DataField="庫存量" HeaderText="<%$ Resources:WebResources, StockQuantity %>" />
                            <asp:BoundField DataField="門市盤點量" HeaderText="<%$ Resources:WebResources, PhysicalInventory %>" />
                            <asp:BoundField DataField="盤差量" HeaderText="<%$ Resources:WebResources, DifferenceQuantity %>" />
                        </Columns>
                    </asp:GridView>
                </div>--%>
           <%-- </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>
        
        
    </div>
    </form>
</body>
</html>
