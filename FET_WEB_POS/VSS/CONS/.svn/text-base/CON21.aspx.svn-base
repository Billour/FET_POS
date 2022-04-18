<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON21.aspx.cs" Inherits="VSS_CON21_CON21" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    <!--寄銷商品撥入作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferIn %>"></asp:Literal>
                </td>
               
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="1">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                       <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <td class="tdtxt">
                        <asp:Literal ID="Literal18" runat="server" 
                            Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>：</td>
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>ALL</asp:ListItem>       
                            <asp:ListItem>在途中</asp:ListItem>
                            <asp:ListItem>已撥入</asp:ListItem>                            
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">                        
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" Text="2010/09/01" />
                        &nbsp;<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox7" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" Text="2010/09/01" />
                    </td>
                  <td class="tdtxt">
                        <asp:Literal ID="Literal19" runat="server" 
                            Text="<%$ Resources:WebResources, Transferfrom %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                       
                        &nbsp;<asp:Literal ID="Literal17" runat="server" 
                            Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>：</td>
                   <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox8" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" Text="2010/09/01" />
                        &nbsp;<asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox9" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" Text="2010/09/01" />
                    </td>
                     <td class="tdtxt"><asp:Literal ID="Literal3" runat="server" 
                            Text="<%$ Resources:WebResources, Transferto %>"></asp:Literal>：</td>
                            <td class="tdval">2101</td>   
                </tr>
              
            </table>
        </div>
       
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate"></div>
                <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CssClass="mGrid" PageSize="8" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                    OnRowCommand="gvMaster_RowCommand" onrowdatabound="gvMaster_RowDataBound">
                    <PagerStyle HorizontalAlign="Right" />
                   
                    <Columns>
                         <asp:TemplateField HeaderText="<%$ Resources:WebResources, TransferSlipNo %>">
                           <ItemTemplate>
                                <asp:LinkButton ID="Label2" runat="server" CommandName="select" 
                                    Text='<%# Bind("移撥單號") %>'></asp:LinkButton>
                            </ItemTemplate>
                             <EditItemTemplate>
                                 <asp:TextBox ID="Label211" runat="server" Text='<%# Bind("移撥單號") %>'></asp:TextBox>
                             </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="移出門市" 
                             HeaderText="<%$ Resources:WebResources,TransferFrom%>" />
                        <asp:BoundField DataField="移出時間" 
                             HeaderText="移出時間" />
                        <asp:BoundField DataField="撥入門市" 
                             HeaderText="<%$ Resources:WebResources,TransferTo%>" />
                         <asp:BoundField DataField="撥入時間" HeaderText="撥入時間" />
                        <asp:BoundField DataField="移撥狀態" 
                             HeaderText="<%$ Resources:WebResources,TransferStatus%>" />
                        <asp:BoundField DataField="更新人員" 
                             HeaderText="<%$ Resources:WebResources,ModifiedBy%>" />
                        <asp:BoundField DataField="更新日期" 
                             HeaderText="<%$ Resources:WebResources,ModifiedDate%>" />
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
                        <asp:GridView ID="gvDetail" runat="server" 
                        OnRowCreated="gvDetail_RowCreated" AutoGenerateColumns="False"
                            OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                            CssClass="mGrid" OnRowUpdating="gvDetail_RowUpdating" 
                        AllowPaging="True" PageSize="2">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>    
                                    <th scope="col">
                                        <!--商品料號-->
                                        <asp:Literal ID="Literal7" runat="server" 
                                            Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal8" runat="server" 
                                            Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品類別-->
                                        <asp:Literal ID="Literal9" runat="server" 
                                            Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>
                                    </th>
                                </tr>                               
                                
                            </EmptyDataTemplate>
                            <Columns> 
                                <asp:BoundField DataField="商品類別" 
                                    HeaderText="<%$ Resources:WebResources, ProductCategory %>" ReadOnly="True" />
                                <asp:BoundField DataField="商品料號" 
                                    HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="True" />
                                <asp:BoundField DataField="商品名稱" 
                                    HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="True" />
                                <asp:BoundField DataField="撥出數量" 
                                    HeaderText="<%$ Resources:WebResources, TransferredOutQuantity %>" ReadOnly="True" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, TransferredInQuantity %>" HeaderStyle-Width="60px" ItemStyle-Width="60px" FooterStyle-Width="60px">      
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTransferredInQuantity" runat="server" Text='' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                            </asp:TemplateField>
                           <%--     <asp:BoundField DataField="撥入數量" 
                                    HeaderText="<%$ Resources:WebResources, TransferredInQuantity %>" />--%>
                              
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
                    <div class="seperate">
                        
                    </div>
                    <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Save %>" />
            <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, ConfrimTransferIn %>" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
        </div>
                </div>
        
    </div>
    </form>
</body>
</html>
