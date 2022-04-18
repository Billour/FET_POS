<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD07.aspx.cs" Inherits="VSS_ORD07_ORD07" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "商品料號查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
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
                        <!--Non-DropShipment主配查詢作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="Non-DropShipment主配查詢作業"></asp:Literal>
                    </td>
                    <%--<td align="right">
                        <asp:Button ID="Button3" runat="server" Text="Non-DropShipment主配作業" OnClientClick="document.location='ORD08.aspx';return false;" />
                    </td>--%>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--主配單號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DistributionNo %>"></asp:Literal>:
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--出貨倉別-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ShipmentWarehouse %>"></asp:Literal>:
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>請選擇</asp:ListItem>
                                <asp:ListItem>Retail-北</asp:ListItem>
                                <asp:ListItem>Retail-中</asp:ListItem>
                                <asp:ListItem>Retail-南</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            <!--狀 態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>請選擇</asp:ListItem>
                                <asp:ListItem>已存檔</asp:ListItem>
                                <asp:ListItem>巳刪除</asp:ListItem>
                                <asp:ListItem>已上傳</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--主配日期-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, DistributionDate %>"></asp:Literal>:
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox
                                ID="PostbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                            &nbsp;<asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            <cc1:postbackDate_TextBox ID="PostbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        </td>
                        <td class="tdtxt">
                            商品料號：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat" Width="110"></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" Text="選" OnClientClick="openwindow('ORD01_searchProductNO.aspx',450,350);return false;" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                    OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" />
                            <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <uc1:popupwindow id="PopupWindow1" runat="server" name="Import" popupbuttonid="btnImport"
                                targetcontrolid="HiddenField1" width="500" height="500" navigateurl="~/VSS/ORD/ORD10_Import.aspx" />
                        </div>
                        <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                            OnRowDataBound="gvMaster_RowDataBound">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--主配單號-->
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, DistributionNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--出貨倉別-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ShipmentWarehouse %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--狀態-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新人員-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="5" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, DistributionNo %>">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlkdno" runat="server" Text='<%# Bind("主配單號") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="主配日期" HeaderText="主配日期" />
                                <asp:BoundField DataField="出貨倉別" HeaderText="<%$ Resources:WebResources, ShipmentWarehouse %>" />
                                <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
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
