<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON12.aspx.cs" Inherits="VSS_CON12_CON12" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品退倉設定作業(門市)-->                       
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousing %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='CON11.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                           <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>COR2010072101</asp:ListItem>
                            <asp:ListItem>COR2010072102</asp:ListItem>
                            <asp:ListItem>COR2010072103</asp:ListItem>
                           </asp:DropDownList>
                           
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉日期-->
                             <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, WarehousedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="lblReturnDate" runat="server"> </asp:Label>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--狀態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                             <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Label ID="Label2" runat="server" Text="00 未存檔"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉起日-->
                             <asp:Literal ID="Literal5" runat="server" Text="退倉起日"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="Label6" runat="server">2010/07/01</asp:Label>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉訖日-->
                             <asp:Literal ID="Literal6" runat="server" Text="退倉訖日"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="Label8" runat="server">2010/07/01</asp:Label>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="Label3" runat="server" Text="10/08/31 15:00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating" AllowPaging="True" 
                            PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col" nowrap="nowrap">
                                        <!--項次-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--商品料號-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--廠商代號-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--廠商名稱-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--庫存數量-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--實際退倉數量-->
                                         <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ActualRetunedQuantity %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="7" class="tdEmptyData">
                                        <!--此無明細資料-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true" />
                                <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />
                                <asp:BoundField DataField="廠商編號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" ReadOnly="true" />
                                <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" ReadOnly="true" />
                                <asp:BoundField DataField="庫存數量" HeaderText="<%$ Resources:WebResources, StockQuantity %>" ReadOnly="true" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ActualRetunedQuantity %>" ControlStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("實際退倉數量") %>'></asp:TextBox>
                                    </ItemTemplate>           
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
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
                <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                <asp:Button ID="Button4" runat="server" Text="列印簽收單" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
