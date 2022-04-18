<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON01a.aspx.cs" Inherits="VSS_CON01a_CON01a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="titlef">
        <!--外部廠商查詢作業面(門市)-->
        <asp:Literal ID="Literal1" runat="server" Text="外部廠商查詢作業面(門市)"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                      <td class="tdtxt">
                        <!--廠商類別-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>寄售廠商</asp:ListItem>
                            <asp:ListItem>外部廠商</asp:ListItem>
                            <asp:ListItem>全部</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>AC1</asp:ListItem>
                            <asp:ListItem>AC2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--統一編號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                        <td class="tdtxt">
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CssClass="mGrid" PageSize="10" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--廠商編號-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--廠商名稱-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--廠商類別-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--統一編號-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--合作起訖日-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, CooperationDateRange %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--負責人-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Owner %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--電話號碼-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Telephone %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--人員-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Staff %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--日期-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Date %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="10" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="廠商編號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                        <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" />
                        <asp:BoundField DataField="廠商類別" HeaderText="<%$ Resources:WebResources, SupplierCategory %>" />
                        <asp:BoundField DataField="統一編號" HeaderText="<%$ Resources:WebResources, UnifiedBusinessNo %>" />
                        <asp:BoundField DataField="合作起日" HeaderText="<%$ Resources:WebResources, CooperationStartDate %>" />
                        <asp:BoundField DataField="合作訖日" HeaderText="<%$ Resources:WebResources, CooperationEndDate %>" />
                        <asp:BoundField DataField="負責人" HeaderText="<%$ Resources:WebResources, Owner %>" />
                        <asp:BoundField DataField="電話號碼" HeaderText="<%$ Resources:WebResources, Telephone %>" />
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
