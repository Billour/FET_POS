<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL07.aspx.cs" Inherits="VSS_SAL07_SAL07" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="func">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--促銷商品價格查詢-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionPriceEnquiry %>" />
                    </td>
                    <td align="right">
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--專案類型-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProjectType %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:DropDownList ID="ddlProjectType" runat="server">
                            <asp:ListItem Text="ALL" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="現行"></asp:ListItem>
                            <asp:ListItem Text="過期專案"></asp:ListItem>
                        </asp:DropDownList>                        
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品廠牌-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductBrand %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="tbSpanWidth"></asp:TextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap"></td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="含過期料號" />
                    </td>
                   
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--促銷類型-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, PromotionType %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:DropDownList ID="AutoCompleteDropDownList1" runat="server">
                            <asp:ListItem Text="ALL" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="啟用"></asp:ListItem>
                            <asp:ListItem Text="續約"></asp:ListItem>
                        </asp:DropDownList>                                                                  
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品料號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="tbSpanWidth"></asp:TextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap"></td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="只查詢單品(不含促銷)" />
                    </td>
                   
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--促銷代號-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap"> 
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="tbSpanWidth"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" TargetControlID="TextBox2">
                        </asp:CalendarExtender>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="tbSpanWidth"></asp:TextBox>
                    </td><td class="tdtxt"></td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="庫存量>0" />
                    </td>
                   
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--促銷名稱-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="tbSpanWidth"></asp:TextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--促銷價-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PromotionPrice %>" />：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>" /><asp:TextBox ID="TextBox8" runat="server" CssClass="tbSpanWidth"></asp:TextBox>
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>" /><asp:TextBox ID="TextBox9" runat="server" CssClass="tbSpanWidth"></asp:TextBox>
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
        <div class="SubEditBlock">
            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                AllowPaging="True" OnRowDataBound="gvMaster_RowDataBound" PageSize="5" PagerStyle-HorizontalAlign="Right"
                OnPageIndexChanging="GridView_PageIndexChanging">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col" nowrap="nowrap">
                            <!--促銷代號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--促銷名稱-->
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--開始日期-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, StartDate %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--結束日期-->
                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, EndDate %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--促銷類別-->
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, PromotionType %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--商品料號-->
                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--商品名稱-->
                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--商品群-->
                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, ProductGroup %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--促銷價-->
                            <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, PromotionPrice %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--庫存量-->
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="10" class="tdEmptyData">
                            <!--查無資料，請修改條件，重新查詢-->
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="促銷代號" HeaderText="<%$ Resources:WebResources, PromotionCode %>" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="促銷名稱" HeaderText="<%$ Resources:WebResources, PromotionName %>" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="開始日期" HeaderText="<%$ Resources:WebResources, StartDate %>" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="結束日期" HeaderText="<%$ Resources:WebResources, EndDate %>" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="促銷類別" HeaderText="<%$ Resources:WebResources, ProductCategory %>" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="商品群" HeaderText="<%$ Resources:WebResources, ProductGroup %>" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="促銷價" HeaderText="<%$ Resources:WebResources, PromotionPrice %>" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="庫存量" HeaderText="<%$ Resources:WebResources, StockQuantity %>" HeaderStyle-Wrap="false"/>
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
        <div class="seperate">
        </div>
         
    </div>
    
    </form>
</body>
</html>
