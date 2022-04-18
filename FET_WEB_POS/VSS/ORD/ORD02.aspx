<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD02.aspx.cs" Inherits="VSS_ORD02_ORD02" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
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
                        <!--訂單查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderSearch %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, OrderPlacement %>" OnClientClick="document.location='ORD01.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <!--tr>
                        <td class="tdtxt">                            
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="ddlArea" runat="server">
                               <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">北一區</asp:ListItem>
                                <asp:ListItem Value="2">北二區</asp:ListItem>
                                <asp:ListItem Value="3">中一區</asp:ListItem>
                                <asp:ListItem Value="3">南一區</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">                            
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="ddlOrdStatus" runat="server">
                               <asp:ListItem Value="0">ALL</asp:ListItem>
                               <asp:ListItem Value="2">預訂</asp:ListItem>
                               <asp:ListItem Value="1">正式</asp:ListItem>
                               <asp:ListItem Value="3">已轉入</asp:ListItem>
                               <asp:ListItem Value="4">已成單</asp:ListItem>
                               <asp:ListItem Value="5">未驗收</asp:ListItem>
                               <asp:ListItem Value="5">部分驗收</asp:ListItem>
                               <asp:ListItem Value="5">已結案</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">                           
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="txtOrdNoStart" runat="server"></asp:TextBox>
                            &nbsp;<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="txtOrdNoEnd" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">                            
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="txtStoreNoStart" runat="server"></asp:TextBox>&nbsp;
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="txtStoreNoEnd" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr-->
                    <tr>
                        <td class="tdtxt">
                            <!--訂單日期-->
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                                ImageUrl="~/Icon/calendar.jpg" />
                            &nbsp;<asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                            </td>
                        <!--td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td-->
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--商品編號-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:TextBox ID="txtParNoStart" runat="server"></asp:TextBox><asp:Button ID="ChooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                            <uc1:PopupWindow ID="PopupWindow1" runat="server"
                        Name="ProductSearch" 
                        PopupButtonID="ChooseButton1" 
                        TargetControlID="txtParNoStart"
                        Width="500" Height="500"                       
                        NavigateUrl="~/VSS/ORD/ORD01_searchProductNo.aspx" />
                        </td>
                        <!--td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td-->
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditCommand" runat="server" visible="false" id="div1">
                        <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                    </div>
                    <div class="SubEditBlock">
                        <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--訂單日期-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--區域-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, District %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市編號-->
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市名稱-->
                                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--訂單編號-->
                                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>
                                    </th>
                                     <th scope="col">
                                        <!--預訂單號-->
                                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PreOrderSheetNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--狀態-->
                                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
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
                                    <td colspan="8" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="訂單日期" HeaderText="<%$ Resources:WebResources, OrderDate %>" />
                                <asp:BoundField DataField="區域" HeaderText="<%$ Resources:WebResources, District %>" Visible="false" />
                                <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" Visible="false" />
                                <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" Visible="false" />
                                
                                <asp:HyperLinkField DataNavigateUrlFields="訂單編號" DataTextField="訂單編號" HeaderText="<%$ Resources:WebResources, OrderNo %>" DataNavigateUrlFormatString="~/VSS/ORD/ORD01.aspx?OrderNo={0}" />
                                <asp:HyperLinkField DataNavigateUrlFields="預訂單號" DataTextField="預訂單號" HeaderText="<%$ Resources:WebResources, PreOrderSheetNo %>" DataNavigateUrlFormatString="~/VSS/ORD/ORD12.aspx?OrderNo={0}" />
                                
                                <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
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
            <div class="seperate">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
