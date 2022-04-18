<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV10.aspx.cs" Inherits="VSS_INV10_INV10" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                        <!--盤點查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventorySearch %>"></asp:Literal>                        
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, StockTaking %>" OnClientClick="document.location='INV11.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--盤點單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            &nbsp;
                        </td>
                        <td class="tdval">
                            &nbsp;
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--盤點日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                            &nbsp;<asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate"></div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <div class="GridScrollBar" style="height: auto">
                            <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" 
                                OnPageIndexChanging="GridView_PageIndexChanging" 
                                onrowdatabound="gvMaster_RowDataBound">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            盤點單號
                                        </th>
                                        <th scope="col">
                                            盤點日期
                                        </th>
                                        <th scope="col">
                                            盤點類型
                                        </th>
                                        <th scope="col">
                                            盤點狀態
                                        </th>
                                        <th scope="col">
                                            盤點人員
                                        </th>
                                        <th scope="col">
                                            更新人員
                                        </th>
                                        <th scope="col">
                                            更新日期
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="8" class="tdEmptyData">
                                            <!--查無資料，請修改條件，重新查詢-->
                                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>                                    
                                    <asp:HyperLinkField DataTextField="盤點單號" HeaderText="盤點單號" DataNavigateUrlFields="盤點單號"  DataNavigateUrlFormatString="~/VSS/INV/INV11.aspx?InventoryNo={0}"  />
                                    <asp:BoundField DataField="盤點日期" HeaderText="盤點日期" />
                                    <asp:BoundField DataField="盤點類型" HeaderText="盤點類型" />
                                    <asp:BoundField DataField="盤點狀態" HeaderText="盤點狀態" />
                                    <asp:BoundField DataField="盤點人員" HeaderText="盤點人員" />
                                    <asp:BoundField DataField="更新人員" HeaderText="更新人員" />
                                    <asp:BoundField DataField="更新日期" HeaderText="更新日期" />
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
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="seperate"></div>
    </div>
    </form>
</body>
</html>
