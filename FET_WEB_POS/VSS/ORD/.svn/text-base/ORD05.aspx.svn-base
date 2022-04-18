<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD05.aspx.cs" Inherits="VSS_ORD05_ORD05" %>

<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                        <!--一搭一查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TwoForOneOfferSearch %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClientClick="document.location='ORD06.aspx';return false;" />
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
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--主商品料號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PrimaryProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--搭配日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TieinSaleDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                                ImageUrl="~/Icon/calendar.jpg" />
                            &nbsp;<asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
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
                        <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                            <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            <!--商品編號-->
                                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--商品名稱-->
                                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--數量-->
                                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Quantity %>"></asp:Literal>
                                        </th>
                                           <th scope="col">
                                            <!--搭配日期-->
                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, TieinSaleDate %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--人員-->
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Staff %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--日期-->
                                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Date %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="tdEmptyData">
                                             <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                                <asp:BoundField DataField="數量" HeaderText="<%$ Resources:WebResources, Quantity %>" />
                                <asp:BoundField DataField="搭配日期" HeaderText="<%$ Resources:WebResources, TieinSaleDate %>" />
                                <asp:BoundField DataField="人員" HeaderText="<%$ Resources:WebResources, Staff %>" />
                                <asp:BoundField DataField="日期" HeaderText="<%$ Resources:WebResources, Date %>" />
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
    </div>
    </form>
</body>
</html>
