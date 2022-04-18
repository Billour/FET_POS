<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON18.aspx.cs" Inherits="VSS_CONS_CON18" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        <!--總部寄銷商品移撥查詢作業-->             
        <%= string.Format("{0}{1}", GetGlobalResourceObject("WebResources", "Headquarters"),                                
                                GetGlobalResourceObject("WebResources", "ConsignmentStockTransferSearch"))%>
   </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                     <td class="tdtxt">
                        <!--移撥單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--撥出門市-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>：
                    </td>
                    <td class="tdval" >
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><asp:Button ID="Button1"
                            runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx','600','400');return false;"/>
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><asp:Button ID="Button2"
                            runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx','600','400');return false;"/>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移出日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                       <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt">
                        <!--移入日期-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Productcode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                     <td class="tdtxt">
                        <!--移撥狀態-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem>在途中</asp:ListItem>
                            <asp:ListItem>巳撥入</asp:ListItem>
                            <asp:ListItem>結案</asp:ListItem>                            
                        </asp:DropDownList>
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
                <div id="Div1" runat="server" class="SubEditBlock" visible="false">
                    <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                        <EmptyDataTemplate>
                            <tr>                            
                                <th scope="col">
                                    <!--移撥單號-->
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--撥出門市-->
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--撥入門市-->
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品編號-->
                                     <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品名稱-->
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--數量-->
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Quantity %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--移撥狀態-->
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--更新人員-->
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--更新日期-->
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="9" class="tdEmptyData">
                                    <!--查無資料，請修改條件，重新查詢-->
                                     <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>               
                            <asp:BoundField DataField="移撥單號" HeaderText="<%$ Resources:WebResources, TransferSlipNo %>" />
                            <asp:BoundField DataField="移出門市" HeaderText="<%$ Resources:WebResources, TransferFrom %>" />
                            <asp:BoundField DataField="移出日期" HeaderText="<%$ Resources:WebResources, TransferOutDate %>" />
                            <asp:BoundField DataField="撥入門市" HeaderText="<%$ Resources:WebResources, TransferTo %>" />
                            <asp:BoundField DataField="撥入日期" HeaderText="<%$ Resources:WebResources, TransferInDate %>" />
                            <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                            <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                            <asp:BoundField DataField="數量" HeaderText="<%$ Resources:WebResources, Quantity %>" />
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
