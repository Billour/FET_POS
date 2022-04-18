<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON19.aspx.cs" Inherits="VSS_CON19_CON19" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品移出查詢作業-->
                    <asp:Literal ID="Literal01" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferOutSearch %>"></asp:Literal>
                </td>
               
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
               
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Productcode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--移撥狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem>在途中</asp:ListItem>
                            <asp:ListItem>已撥入</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移出日期-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        起<cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />&nbsp;
                        訖<cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                </tr>
                
            </table>
        </div>
       
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate"></div>
                <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging" OnRowCommand="gvMaster_RowCommand" >
                    <PagerStyle HorizontalAlign="Right" />
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--移出門市-->
                                <asp:Literal ID="Literal7" runat="server" 
                                    Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>                                
                            </th>
                            <th scope="col">
                                <!--移出日期-->
                                <asp:Literal ID="Literal6" runat="server" 
                                    Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--撥入門市-->
                                <asp:Literal ID="Literal5" runat="server" 
                                    Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--撥入日期-->
                                <asp:Literal ID="Literal8" runat="server" 
                                    Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--移撥狀態-->
                                <asp:Literal ID="Literal17" runat="server" 
                                    Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--更新人員-->
                                <asp:Literal ID="Literal10" runat="server" 
                                    Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--更新日期-->
                                <asp:Literal ID="Literal11" runat="server" 
                                    Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="8" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal12" runat="server" 
                                    Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, TransferSlipNo %>">
                            <EditItemTemplate>
                                <asp:TextBox ID="Label211" runat="server" Text='<%# Bind("移撥單號") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("移撥單號") %>' 
                                    CommandName="select"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="移出門市" HeaderText="<%$ Resources:WebResources, TransferFrom %>" />
                        <asp:BoundField DataField="移出日期" 
                             HeaderText="<%$ Resources:WebResources, TransferOutDate %>" />
                         <asp:BoundField DataField="撥入門市" HeaderText="<%$ Resources:WebResources, TransferTo %>" />
                        <asp:BoundField DataField="撥入日期" 
                             HeaderText="<%$ Resources:WebResources, TransferInDate %>" />
                         <asp:BoundField DataField="移撥狀態" HeaderText="<%$ Resources:WebResources, TransferStatus %>" />
                        <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" />
                            <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
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
                        <asp:Label ID="lblCurrPage" runat="server" 
                            Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                        <asp:Label ID="lblPageCount" runat="server" 
                            Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                        <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                            
                            
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                            
                            
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                        到第
                        <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                        頁
                        <asp:Button ID="btnGoToIndex" runat="server" Text="GO" 
                            OnClick="btnGoToIndex_Click" />
                    </PagerTemplate>
                </asp:GridView>
        
        
                <div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false">
                    <div class="SubEditCommand">
                        <asp:Label ID="Label5" runat="server" Text="移撥單號:CST2010070101" ForeColor="White"></asp:Label>
                    </div>
                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        Visible="False" OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                        OnRowUpdating="gvDetail_RowUpdating" AllowPaging="True" PageSize="2">
                        <PagerStyle HorizontalAlign="Right" />
                        <EmptyDataTemplate>
                            
                            <tr>
                                <td colspan="9" class="tdEmptyData">
                                    <!--此無明細資料-->
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckAll" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckItem" runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" Visible="False">
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="商品類別" HeaderText="<%$ Resources:WebResources, ProductCategory %>" ReadOnly="true" />
                            <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductName %>">
                                <EditItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="撥出數量" HeaderText="<%$ Resources:WebResources,TransferredOutQuantity %>" />
                            
                        </Columns>
                        <FooterStyle HorizontalAlign="Right" />
                        <PagerTemplate>
                        <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Page" CommandArgument="First"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/first.png" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/previous.png" /></asp:LinkButton>
                        第
                        <asp:Label ID="lblCurrPage" runat="server" 
                            Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                        <asp:Label ID="lblPageCount" runat="server" 
                            Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                        <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                            
                            
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                            
                            
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                        到第
                        <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                        頁
                        <asp:Button ID="btnGoToIndex" runat="server" Text="GO" 
                            OnClick="btnGoToIndex_Click" />
                    </PagerTemplate>
                    </asp:GridView>
                </div>
        
    </div>
    </form>
</body>
</html>
