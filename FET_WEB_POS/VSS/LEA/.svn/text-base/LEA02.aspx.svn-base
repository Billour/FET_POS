<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEA02.aspx.cs" Inherits="VSS_LEA02_LEA02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div class="titlef">
            <!--可租賃設備查詢-->            
             <asp:Literal ID="Literal10" runat="server" Text="可租賃設備查詢"></asp:Literal>
            </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--區域-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, District %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>北區</asp:ListItem>
                            <asp:ListItem>中區</asp:ListItem>
                            <asp:ListItem>南區</asp:ListItem>
                            <asp:ListItem>東區</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--庫存地點-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem Text="地點1"></asp:ListItem>
                            <asp:ListItem Text="地點2"></asp:ListItem>
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
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <div class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            Visible="true" OnRowCommand="gvMaster_RowCommand" 
                            PageSize="5">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--庫存地點-->
                                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--手機類型-->
                                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, MobileType %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--庫存量-->
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="3" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelect" runat="server" Text="<%$ Resources:WebResources, Select %>" CommandName="select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="庫存地點" HeaderText="<%$ Resources:WebResources, StorageLocation %>" />
                                <asp:BoundField DataField="手機類型" HeaderText="<%$ Resources:WebResources, MobileType %>" />
                                <asp:BoundField DataField="庫存量" HeaderText="<%$ Resources:WebResources, StockQuantity %>" />
                            </Columns>
                               
                        </asp:GridView>
                    </div>
                    <div class="seperate">
                    </div>
                    <div class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gv2" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowDataBound="gv2_RowDataBound" AllowPaging="True" 
                            onselectedindexchanged="gv2_SelectedIndexChanged" PageSize="7">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--庫存地點-->
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--手機類型-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, MobileType %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--手機序號-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, MobileIdentityNumber %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--狀態-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                     <asp:Button ID="btnReserve" runat="server" Text="<%$ Resources:WebResources, Reserve %>" ></asp:Button>
                                       <%-- <asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ Resources:WebResources, Reserve %>"></asp:HyperLink>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="庫存地點" HeaderText="<%$ Resources:WebResources, StorageLocation %>" />
                                <asp:BoundField DataField="手機類型" HeaderText="<%$ Resources:WebResources, MobileType %>" />
                                <asp:BoundField DataField="手機序號" HeaderText="<%$ Resources:WebResources, MobileIdentityNumber %>" />
                                <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
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
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="seperate">
    </div>
    </form>
</body>
</html>
