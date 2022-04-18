<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD04.aspx.cs" Inherits="VSS_ORD04_ORD04" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        <!--調整訂單作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderAdjustment %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--區域別-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlArea" runat="server">
                            <asp:ListItem Value="0">全選</asp:ListItem>
                            <asp:ListItem Value="1">北一區</asp:ListItem>
                            <asp:ListItem Value="2">北二區</asp:ListItem>
                            <asp:ListItem Value="3">中一區</asp:ListItem>
                            <asp:ListItem Value="3">南一區</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--訂單狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="2">預訂</asp:ListItem>
                            <asp:ListItem Value="1">正式</asp:ListItem>
                            <asp:ListItem Value="3">已轉入</asp:ListItem>
                            <asp:ListItem Value="4">已成單</asp:ListItem>
                            <asp:ListItem Value="5">未驗收</asp:ListItem>
                            <asp:ListItem Value="5">部分驗收</asp:ListItem>
                            <asp:ListItem Value="5">已結案</asp:ListItem>
                            <asp:ListItem Value="6" Selected="True">已傳輸</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label6" runat="server" Text="10/07/12 15:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox
                            ID="TextBox3" runat="server"></asp:TextBox>
                        &nbsp;<asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox
                            ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label7" runat="server" Text="64591 李家駿"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox
                            ID="TextBox7" runat="server"></asp:TextBox>
                        &nbsp;<asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox
                            ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--訂單編號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox
                            ID="TextBox1" runat="server"></asp:TextBox>
                        &nbsp;<asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox
                            ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--訂單日期-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox
                            ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox
                            ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                OnClick="btnSearch_Click" />
            <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="divSummary" runat="server" style="display: none">
                    <table border="0">
                        <tr>
                            <td>
                                <!--商品編號-->
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:ListBox ID="ProductListBox" runat="server" DataTextField="商品名稱" DataValueField="商品編號"
                                    AutoPostBack="true" OnSelectedIndexChanged="ProductListBox_SelectedIndexChanged">
                                </asp:ListBox>
                            </td>
                            <td>
                                <table style="height: 65px; border-collapse: collapse" border="1" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>
                                                <!--可分配量-->
                                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, AllocatableQuantity %>"></asp:Literal>
                                            </th>
                                            <th>
                                                <!--門市調整量-->
                                                <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, StoreAdjustment %>"></asp:Literal>
                                            </th>
                                            <th>
                                                <!--總調整量-->
                                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, TotalAdjustments %>"></asp:Literal>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="10"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAdjustmentQty" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalAdjustmentQty" runat="server" Text="0"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="SubEditBlock">
                    <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                        OnRowUpdating="gvMaster_RowUpdating" OnRowDataBound="gvMaster_RowDataBound"
                        AllowPaging="True" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                        <PagerStyle HorizontalAlign="Right" />
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                    <!--訂單日期-->
                                    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--訂單編號-->
                                    <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--訂單狀態-->
                                    <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--門市編號-->
                                    <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--門市名稱-->
                                    <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--訂購量-->
                                    <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, OrderQuantity %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--庫存量-->
                                    <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--業助調整數量-->
                                    <asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, AssistantAdjustment %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--備註-->
                                    <asp:Literal ID="Literal30" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="9" class="tdEmptyData">
                                    <!--查無資料，請修改條件，重新查詢-->
                                    <asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <HeaderStyle Width="60px" Wrap="true" />
                                <ItemStyle Width="60px" Wrap="true" />
                                <EditItemTemplate>
                                    <asp:Button ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                        Text="<%$ Resources:WebResources, Save %>"></asp:Button>
                                    <%--&nbsp;--%><asp:Button ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                        Text="<%$ Resources:WebResources, Cancel %>"></asp:Button>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Edit"
                                        Text="<%$ Resources:WebResources, Edit %>" Width="40px"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="訂單日期" HeaderText="<%$ Resources:WebResources, OrderDate %>"
                                ReadOnly="true" HtmlEncode="false">
                                <HeaderStyle Width="120px" Wrap="true" />
                                <ItemStyle Width="120px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="訂單編號" HeaderText="<%$ Resources:WebResources, OrderNo %>"
                                ReadOnly="true">
                                <HeaderStyle Width="120px" Wrap="true" />
                                <ItemStyle Width="120px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="訂單狀態" HeaderText="<%$ Resources:WebResources, OrderStatus %>"
                                ReadOnly="true">
                                <HeaderStyle Width="100px" Wrap="true" />
                                <ItemStyle Width="100px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>"
                                ReadOnly="true">
                                <HeaderStyle Width="100px" Wrap="true" />
                                <ItemStyle Width="100px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>"
                                ReadOnly="true">
                                <HeaderStyle Width="100px" Wrap="true" />
                                <ItemStyle Width="100px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="訂購量" HeaderText="<%$ Resources:WebResources, OrderQuantity %>"
                                ReadOnly="true">
                                <HeaderStyle Width="80px" Wrap="true" />
                                <ItemStyle Width="80px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="庫存量" HeaderText="<%$ Resources:WebResources, StockQuantity %>"
                                ReadOnly="true">
                                <HeaderStyle Width="80px" Wrap="true" />
                                <ItemStyle Width="80px" Wrap="true" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, AssistantAdjustment %>">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("業助調整數量") %>' Width="60px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("業助調整數量") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="140px" Wrap="True" />
                                <ItemStyle Width="60px" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, Remark %>">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("備註") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("備註") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="120px" Wrap="True" />
                                <ItemStyle Width="120px" Wrap="True" />
                            </asp:TemplateField>
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
