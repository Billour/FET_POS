<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV04.aspx.cs" Inherits="VSS_INV04_INV04" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=250,left=380,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--退倉設定查詢作業-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingSearch %>"></asp:Literal>
                    </td>
                    <%--<td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingSettings %>" OnClientClick="document.location='INV05.aspx';return false;" />
                    </td>--%>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx',640,300);return false;" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,350);return false;" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                       <!--開單日-->
                       <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReceiptDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉狀態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>已存檔</asp:ListItem>
                            <asp:ListItem>已傳輸</asp:ListItem>
                        </asp:DropDownList>
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
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="divContent" runat="server" class="SubEditBlock">
                    <div class="GridScrollBar" style="height: auto">
                        <div class="SubEditCommand">
                            <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                        </div>
                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid" onrowdatabound="gvMaster_RowDataBound"
                            AllowPaging="True" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--退倉單號-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--退倉狀態-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStatus %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--退倉開始日-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--退倉結束日-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--退倉原因-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ReasonForWarehousing %>"></asp:Literal>
                                        
                                    </th>
                                    <th scope="col">
                                        <!--更新人員-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="7" class="tdEmptyData">
                                        <!--請點選新增按鍵增加資料-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("退倉單號") %>' ></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="退倉狀態" HeaderText="<%$ Resources:WebResources, ReturnWarehousingStatus %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="退倉開始日" HeaderText="<%$ Resources:WebResources, ReturnWarehousingStartDate %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="退倉結束日" HeaderText="<%$ Resources:WebResources, ReturnWarehousingEndDate %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="退倉原因" HeaderText="<%$ Resources:WebResources, ReasonForWarehousing %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
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
                        </cc1:ExGridView>
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
