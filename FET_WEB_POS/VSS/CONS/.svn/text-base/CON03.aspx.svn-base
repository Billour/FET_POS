<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON03.aspx.cs" Inherits="VSS_CON03_CON03" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
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
        <!--寄銷商品查詢作業(總部)-->
        <asp:Literal ID="Literal1" runat="server" Text="寄銷商品查詢作業(總部)"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem Selected="True">ALL</asp:ListItem>
                            <asp:ListItem>AC1</asp:ListItem>
                            <asp:ListItem>AC2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox4" runat="server" Width="100"></asp:TextBox>
                        <asp:Button ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                       <!--上架日期-->
                       <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupportStartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--下架日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SupportExpiryDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox3" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox4" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--商品類別-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlProductCategory" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>3G Handset</asp:ListItem>
                            <asp:ListItem>SIM Card</asp:ListItem>
                            <asp:ListItem>3G Accessory</asp:ListItem>
                            <asp:ListItem>On Line Recharge</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--停止訂購日-->
                        <asp:Literal ID="Literal12" runat="server" Text="停止訂購日"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox5" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox6" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
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
                <div id="Div1" runat="server" class="SubEditBlock">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                    </div>
                    <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="mGrid" PageSize="10" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                    <!--廠商編號-->
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品編號-->
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品名稱-->
                                    <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品類別-->
                                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--上架日期-->
                                    <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, SupportStartDate %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--下架日期-->
                                    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, SupportExpiryDate %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--停止訂購日-->
                                    <asp:Literal ID="Literal20" runat="server" Text="停止訂購日"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--更新人員-->
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                   <!--更新日期-->
                                   <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="9" class="tdEmptyData">
                                    <!--查無資料，請修改條件，重新查詢-->
                                    <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="廠商編號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                            
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>">
                                <EditItemTemplate>
                                    <asp:TextBox ID="Label299" runat="server" Text='<%# Bind("商品編號") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("商品編號") %>' NavigateUrl='<%# Eval("商品編號", "~/VSS/CONS/CON04.aspx?No={0}") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />--%>
                            <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                            <asp:BoundField DataField="商品類別" HeaderText="<%$ Resources:WebResources, ProductCategory %>" />
                            <asp:BoundField DataField="上架日期" HeaderText="<%$ Resources:WebResources, SupportStartDate %>" />
                            <asp:BoundField DataField="下架日期" HeaderText="<%$ Resources:WebResources, SupportExpiryDate %>" />
                            <asp:BoundField DataField="停止訂購日" HeaderText="停止訂購日" />
                            <asp:BoundField DataField="人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" />
                            <asp:BoundField DataField="日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
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
