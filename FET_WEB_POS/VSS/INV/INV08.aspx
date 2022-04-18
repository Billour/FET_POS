<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV08.aspx.cs" Inherits="VSS_INV08_INV08" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                        進貨驗收作業
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            PO/OE_NO：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        
                       <td class="tdtxt">
                            供貨商：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem Value="">-請選擇-</asp:ListItem>
                                <asp:ListItem Value="供貨商1">供貨商1</asp:ListItem>
                                <asp:ListItem Value="供貨商2">供貨商2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            訂單狀態：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Value="">-請選擇-</asp:ListItem>
                                <asp:ListItem Value="未驗收">未驗收</asp:ListItem>
                                <asp:ListItem Value="部分驗收">部分驗收</asp:ListItem>
                                <asp:ListItem Value="已結案">已結案</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            訂單/主配編號：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            驗收日期：
                        </td>
                        <td class="tdval" colspan="3">
                            起<cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                                ImageUrl="~/Icon/calendar.jpg" />
                            &nbsp;訖<cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            商品編號：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </td>
                        
                       
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="清空" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <div class="GridScrollBar" style="height: auto">
                            <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" 
                                OnPageIndexChanging="GridView_PageIndexChanging" 
                                onrowdatabound="gvMaster_RowDataBound">
                                <PagerStyle HorizontalAlign="Right" />
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            PO/OE_NO
                                        </th>
                                        <th scope="col">
                                            訂單/主配編號
                                        </th>
                                        <th scope="col">
                                            驗收單號
                                        </th>
                                        <th scope="col">
                                            門市編號
                                        </th>
                                        <th scope="col">
                                            門市名稱
                                        </th>
                                        <th scope="col">
                                            驗收狀態
                                        </th>
                                        <th scope="col">
                                            驗收日期
                                        </th>
                                        <th scope="col">
                                            更新人員
                                        </th>
                                        <th scope="col">
                                            更新日期
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="9" class="tdEmptyData">
                                            查無資料，請修改條件，重新查詢
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                   <%-- <asp:TemplateField HeaderText="OE_NO">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("OE_NO") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("OE_NO") %>' NavigateUrl="~/VSS/INV/INV09.aspx"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                   
                                     <asp:TemplateField HeaderText="PO/OE_NO">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("OE_NO") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("OE_NO") %>' NavigateUrl="~/VSS/INV/INV09.aspx"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="訂單編號" HeaderText="訂單/主配編號" />
                                    
                                    <asp:TemplateField HeaderText="驗收單編號">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("驗收單編號") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("驗收單編號") %>' NavigateUrl="~/VSS/INV/INV09.aspx"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="門市編號" HeaderText="門市編號" />
                                    <asp:BoundField DataField="門市名稱" HeaderText="門市名稱" />
                                    <asp:BoundField DataField="驗收狀態" HeaderText="驗收狀態" />
                                    <asp:BoundField DataField="驗收日期" HeaderText="驗收日期" />
                                    <asp:BoundField DataField="人員" HeaderText="更新人員" />
                                    <asp:BoundField DataField="日期" HeaderText="更新日期" />
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
    </div>
    </form>
</body>
</html>
