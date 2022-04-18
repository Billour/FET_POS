<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON09.aspx.cs" Inherits="VSS_CON09_CON09" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品退倉查詢(總部)-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSearch %>"></asp:Literal>
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
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        <asp:Button ID="Button3" runat="server" Text="選" OnClientClick="openwindow('../INV/INV18_3.aspx',640,300);return false;" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem>已存檔</asp:ListItem>
                            <asp:ListItem>轉單中</asp:ListItem>
                            <asp:ListItem>已成單</asp:ListItem>
                            <asp:ListItem>己驗退</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--廠商編號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem>AC1</asp:ListItem>
                            <asp:ListItem>AC2</asp:ListItem>
                            <asp:ListItem>AP1</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品編號-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        <asp:Button ID="Button4" runat="server" Text="選" OnClientClick="openwindow('../SAL/SAL01_searchProductNo.aspx',640,300);return false;" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        起<cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;訖<cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        起<cc1:postbackDate_TextBox ID="postbackDate_TextBox3" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;訖<cc1:postbackDate_TextBox ID="postbackDate_TextBox4" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Export %>" />
        </div>
        <div class="seperate">
        </div>
    </div>

        <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
            OnRowCommand="gvMaster_RowCommand" onrowdatabound="gvMaster_RowDataBound">
            <PagerStyle HorizontalAlign="Right" />
            <EmptyDataTemplate>
                <tr>
                    <th scope="col" nowrap="nowrap">
                        &nbsp;
                    </th>
                    <th scope="col" nowrap="nowrap">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>                                
                    </th>
                    <th scope="col" nowrap="nowrap">
                        <!--退倉起日-->
                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></asp:Literal>
                    </th>
                    <th scope="col" nowrap="nowrap">
                        <!--退倉訖日-->
                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></asp:Literal>
                    </th>                            
                    <th scope="col" nowrap="nowrap">
                        <!--狀態-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                    </th>
                    <th scope="col" nowrap="nowrap">
                        <!--更新人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                    </th>
                    <th scope="col" nowrap="nowrap">
                        <!--更新日期-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                    </th>
                </tr>
                <tr id="trEmptyData" runat="server">
                    <td colspan="7" class="tdEmptyData">
                        <!--查無資料，請修改條件，重新查詢-->
                        <asp:Literal ID="Literal12" runat="server" 
                            Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                    </td>
                </tr>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" CausesValidation="False" CommandName="select"
                            Text="<%$ Resources:WebResources, Select %>" />
                        <%--<asp:Button ID="btnfix" runat="server" CausesValidation="False" CommandName="fix"
                            Text="修改" Visible="false"/>--%>
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="退倉單號">
                    <EditItemTemplate>
                        <asp:TextBox ID="Label211" runat="server" Text='<%# Bind("退倉單號") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("退倉單號") %>' 
                            CommandName="View"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="退倉起日" HeaderText="退倉起日" />
                <asp:BoundField DataField="退倉訖日" HeaderText="退倉訖日" />
                <asp:BoundField DataField="狀態" HeaderText="狀態" />
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
                <asp:Label ID="lblCurrPage" runat="server" 
                    Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                <asp:Label ID="lblPageCount" runat="server" 
                    Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                    
                    
                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                    
                    
                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                到第
                <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                頁
                <asp:Button ID="btnGoToIndex" runat="server" Text="GO" 
                    OnClick="btnGoToIndex_Click" />
            </PagerTemplate>
        </asp:GridView> 

        <div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false">

            <div class="SubEditCommand">
                <asp:Label ID="Label5" runat="server" Text="退倉單號:COR2010073001" ForeColor="White"></asp:Label>
            </div>
            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                Visible="False" OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                OnRowUpdating="gvDetail_RowUpdating" AllowPaging="True" PageSize="2" PagerStyle-HorizontalAlign="Right" 
                OnPageIndexChanging="GridView_PageIndexChanging">
                <EmptyDataTemplate>
                    <tr>
                        <td colspan="8" class="tdEmptyData">
                            <!--此無明細資料-->
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField Visible="False">
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckItem" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" Visible="False">
                        <EditItemTemplate>
                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:BoundField DataField="廠商編號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" ReadOnly="true" />
                    <asp:BoundField DataField="廠商名稱" HeaderText="廠商名稱" />
                    <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                    <asp:BoundField DataField="門市名稱" HeaderText="門市名稱" />
                    <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                    <asp:BoundField DataField="商品名稱" HeaderText="商品名稱" />
                    <asp:BoundField DataField="庫存數量" HeaderText="庫存數量" />
                    <asp:BoundField DataField="實際退倉數量" HeaderText="實際退倉數量" />
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
    </form>
</body>
</html>
