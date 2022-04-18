<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEA07.aspx.cs" Inherits="VSS_LEA07_LEA07" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--設備租賃設定查詢作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="設備租賃設定查詢作業"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--類別-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">漫遊租賃</asp:ListItem>
                                <asp:ListItem Value="2">維修租賃</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            <!--產品類別-->
                            <asp:Literal ID="Literal3" runat="server" Text="產品類別"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">產品類別1</asp:ListItem>
                                <asp:ListItem Value="2">產品類別2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            <!--產品名稱-->
                            <asp:Literal ID="Literal5" runat="server" Text="產品名稱"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList4" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">產品名稱1</asp:ListItem>
                                <asp:ListItem Value="2">產品名稱2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--外部廠商代碼-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, OutsideFirmNo %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--外部廠商名稱-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, OutsideFirmName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
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
                    <div class="SubEditBlock">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                            AllowPaging="True">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--類別-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--產品類別-->
                                        <asp:Literal ID="Literal10" runat="server" Text="產品類別"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--產品名稱-->
                                        <asp:Literal ID="Literal1" runat="server" Text="產品名稱"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--外部廠商代碼-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, OutsideFirmNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--外部廠商名稱-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, OutsideFirmName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--開始日期-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--結束日期-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新人員-->
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="10" class="tdEmptyData">
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input id="Radio" type="radio" name="SameRadio"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="類別" HeaderText="<%$ Resources:WebResources, Category %>" />
                                <asp:BoundField DataField="產品類別" HeaderText="產品類別" />
                                <asp:BoundField DataField="產品名稱" HeaderText="產品名稱" />
                                <asp:BoundField DataField="外部廠商代碼" HeaderText="<%$ Resources:WebResources, OutsideFirmNo %>" />
                                <asp:BoundField DataField="外部廠商名稱" HeaderText="<%$ Resources:WebResources, OutsideFirmName %>" />
                                <asp:BoundField DataField="開始日期" HeaderText="<%$ Resources:WebResources, StartDate %>" />
                                <asp:BoundField DataField="結束日期" HeaderText="<%$ Resources:WebResources, EndDate %>" />
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
                        <div class="seperate">
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSure" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                    OnClick="btnSure_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
