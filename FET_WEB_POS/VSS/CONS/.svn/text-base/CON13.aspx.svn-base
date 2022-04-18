<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON13.aspx.cs" Inherits="VSS_CON13_CON13" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
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
                    <!--寄銷商品進貨驗收查詢作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventoryExaminationSearch %>"></asp:Literal>
                </td>
                <%--<td align="right">
                    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClientClick="document.location='CON14.aspx';return false;" />
                </td>--%>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" colspan="1">
                        <!--訂單/主配編號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--出貨編號-->
                        <asp:Literal ID="Literal23" runat="server" Text="出貨編號"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server" Width="110"></asp:TextBox>
                        <%--<uc1:PopupWindow ID="PopupWindow2" runat="server"
                            Name="DeliveryOrderNoSearch" 
                            PopupButtonID="ChooseButton1" 
                            TargetControlID="TextBox1"
                            Width="300" Height="300"                       
                            NavigateUrl="~/VSS/CONS/SelectDeliveryOrderNo.aspx" />--%>
                        </td>                    
                    <td class="tdtxt">
                        <!--驗收狀態-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReceiveStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">       
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem Value="">-請選擇-</asp:ListItem>
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem>已存檔</asp:ListItem>
                            <asp:ListItem>轉單中</asp:ListItem>
                            <asp:ListItem>已成單</asp:ListItem>
                            <asp:ListItem>待進貨</asp:ListItem>
                            <asp:ListItem>已驗收</asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                </tr>
                <tr>
                    <td class="tdtxt"> 
                        <!--商品編號-->
                        <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server" Width="110"></asp:TextBox>
                        <%--<asp:Button ID="ChooseButton2" runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                        <uc1:PopupWindow ID="PopupWindow1" runat="server"
                        Name="ProductSearch" 
                        PopupButtonID="ChooseButton2" 
                        TargetControlID="TextBox2"
                        Width="500" Height="500"                       
                        NavigateUrl="~/VSS/ORD/ORD01_searchProductNo.aspx" />--%>
                    </td>
                
                    <td class="tdtxt">
                        <!--驗收日期-->
                         <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ReceivedDate %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                              <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />                      
                    </td>                    
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
    
                        <asp:DropDownList ID="AutoCompleteDropDownList1" runat="server">
                            <asp:ListItem Value="">-請選擇-</asp:ListItem>
                            <asp:ListItem>AC1</asp:ListItem>
                            <asp:ListItem>AC2</asp:ListItem>
                            <asp:ListItem>AC3</asp:ListItem>
                            <asp:ListItem>AC4</asp:ListItem>
                            <asp:ListItem>AC5</asp:ListItem>                          
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox4" runat="server" Width="110"></asp:TextBox>
                        <%--<asp:AutoCompleteDropDownList ID="AutoCompleteDropDownList2" runat="server">
                            <asp:ListItem Value="">-請選擇-</asp:ListItem>
                            <asp:ListItem>全虹</asp:ListItem>
                            <asp:ListItem>蘋果</asp:ListItem>
                            <asp:ListItem>橘子工坊</asp:ListItem>                      
                        </asp:AutoCompleteDropDownList>--%>
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
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Examine %>" Visible="false" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="mGrid" 
            DataKeyNames="訂單編號"
            PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"                    
            OnRowDataBound="gvMaster_OnRowDataBound" OnRowEditing="gvMaster_RowEditing" 
            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowCommand="gvMaster_RowCommand">
            <EmptyDataTemplate>
                <tr>                    
                    <th scope="col">
                        <!--訂單編號-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--出貨編號-->
                        <asp:Literal ID="Literal11" runat="server" Text="出貨編號"></asp:Literal>
                    </th>                                      
                    <th scope="col">
                        <!--廠商名稱-->
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--驗收狀態-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ReceiveStatus %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--進貨日期-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ReceivedDate %>"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--人員-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, ReceivedBy %>"></asp:Literal>
                    </th>                   
                </tr>
                <tr>
                    <td colspan="6" class="tdEmptyData">
                        <!--查無資料，請修改條件，重新查詢-->
                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                    </td>
                </tr>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CommandName="GoToEdit" Text="<%$ Resources:WebResources, View %>" CommandArgument='<%# Bind("訂單編號") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="Button2" runat="server" CommandName="Update" Text="<%$ Resources:WebResources, Save %>" />
                        <asp:Button ID="Button3" runat="server" CommandName="Cancel" Text="<%$ Resources:WebResources, Cancel %>" />
                    </EditItemTemplate>
                </asp:TemplateField> 
                <asp:BoundField DataField="訂單編號" HeaderText="訂單編號" />               
                <%--<asp:TemplateField HeaderText="訂單編號">                    
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("訂單編號") %>' CommandName="Select" CommandArgument='<%# Bind("訂單編號") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="出貨編號">                    
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("出貨編號") %>' CommandName="Select" CommandArgument='<%# Bind("出貨編號") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="出貨編號" HeaderText="出貨編號" />--%>
                <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" Visible="false" />
                <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" Visible="false" />                
                <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" />
                <asp:BoundField DataField="驗收狀態" HeaderText="<%$ Resources:WebResources, ReceiveStatus %>" />
                <asp:BoundField DataField="進貨日期" HeaderText="<%$ Resources:WebResources, ReceivedDate %>" />
                <asp:BoundField DataField="人員" HeaderText="<%$ Resources:WebResources, ReceivedBy %>" />      
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
        <div class="seperate"></div>
            				
        <div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false">
            <div class="SubEditCommand">
                <asp:Label ID="lblCaption" runat="server" Text="訂單編號:101900074" ForeColor="White"></asp:Label>
            </div>
            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                Visible="False" AllowPaging="True" PageSize="2" PagerStyle-HorizontalAlign="Right">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col">
                            <!--出貨編號-->
                             <asp:Literal ID="Literal12" runat="server" Text="出貨編號"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--商品編號-->
                             <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--商品名稱-->
                             <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                        </th>   
                        <th scope="col">
                            <!--驗收量-->
                             <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, InspectionQuantity %>"></asp:Literal>
                        </th>                                            
                    </tr>
                    <tr>
                        <td colspan="4" class="tdEmptyData">
                            <!--此無明細資料-->
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>                    
                    <asp:BoundField DataField="出貨編號" HeaderText="出貨編號" ReadOnly="true" />
                    <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                    <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />                    
                     <asp:BoundField DataField="驗收量" HeaderText="<%$ Resources:WebResources, InspectionQuantity %>" ReadOnly="true" />                    
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
    </form>
</body>
</html>
