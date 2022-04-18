<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV18.aspx.cs" Inherits="VSS_INV_INV18" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=200,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
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
                        <!--庫存調整查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="庫存調整查詢作業"></asp:Literal>
                    </td>
                    <%--<td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClientClick="document.location='INV18_1.aspx';return false;" />
                    </td>--%>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>                    
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>：
                        </td>
                        <td class="" nowrap="nowrap">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            <cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" Text="2010/07/01" />
                            &nbsp;
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            <cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" Text="2010/07/01" />
                        </td>                                           
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整門市-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td class="" colspan="3" nowrap="nowrap">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                            <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,300);return false;" />
                            &nbsp;
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                            <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,300);return false;" />
                        </td>                        
                        
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>                    
                    <div class="SubEditBlock">
                        <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False" onrowdatabound="gvMaster_RowDataBound"
                            CssClass="mGrid" PageSize="15" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--調整單號-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市編號-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市名稱-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--調整日期-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--狀態-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新人員-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="7" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, StockAdjustmentNoteNo %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("調整單號") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("調整單號") %>' ></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="調整日期" HeaderText="<%$ Resources:WebResources, AdjustmentDate %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false"/>
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
            <div class="seperate">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
