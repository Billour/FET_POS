<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG03.aspx.cs" Inherits="VSS_LOG_LOG03" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, RolePermissionMapping %>"></asp:Literal>
    </title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>     
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
                        <!--角色功能對應作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RolePermissionMapping %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" Visible="false" />
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--角色代碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, RoleID %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>AC</asp:ListItem>
                            <asp:ListItem>CB</asp:ListItem>
                            <asp:ListItem>AC</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--角色名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, RoleName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>店員</asp:ListItem>
                            <asp:ListItem>店長</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--角色狀態-->                        
                    </td>
                    <td class="tdval">                       
                    </td>
                </tr>                
            </table>
        </div>
        <div class="seperate">
        </div>
         <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <script type="text/javascript">
            function onOk() {
                __doPostBack('<%= PermissionsHiddenField.UniqueID %>', '');
            }
        </script>
        <div class="SubEditBlock">
            <div class="SubEditCommand">
                <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>" /><asp:Button
                    ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                <asp:HiddenField ID="PermissionsHiddenField" runat="server" OnValueChanged="PermissionsHiddenField_ValueChanged" />
                <uc1:PopupWindow ID="PopupWindow1" runat="server"
                            Name="GrantingPermission" 
                            PopupButtonID="btnAdd" 
                            TargetControlID="PermissionsHiddenField"
                            Width="300" Height="320"  
                            OnOkScript="onOk"                     
                            NavigateUrl="~/VSS/LOG/GrantPermissions.aspx" />    
                
            </div>
            <div class="GridScrollBar" style="height:auto">
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" PagerStyle-HorizontalAlign="Right" CssClass="mGrid"
                    OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing" OnPageIndexChanging="GridView_PageIndexChanging"
                    OnRowUpdating="gvMaster_RowUpdating">
                    <EmptyDataTemplate>
                        <tr>
                        <th></th>
                            <th scope="col">
                                <!--順序-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, SortNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--系統別-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, System %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--角色代碼-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, RoleID %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--角色名稱-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, RoleName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--模組名稱-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ModuleName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--功能代碼-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, FunctionCode %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--功能名稱-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, FunctionName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--URL-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Url %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="9" class="tdEmptyData">
                                <!--請點選新增按鍵增加資料-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:if(this.checked){$('.GridScrollBar').checkCheckboxes();}else{$('.GridScrollBar').unCheckCheckboxes();}" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox2" runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="順序" HeaderText="<%$ Resources:WebResources, SortNo %>" />
                        <asp:BoundField DataField="角色代碼" HeaderText="<%$ Resources:WebResources, RoleID%>" />
                        <asp:BoundField DataField="角色名稱" HeaderText="<%$ Resources:WebResources, RoleName %>" />
                        <asp:BoundField DataField="系統別" HeaderText="<%$ Resources:WebResources, System %>" />
                        <asp:BoundField DataField="模組名稱" HeaderText="<%$ Resources:WebResources, ModuleName %>" />
                        <asp:TemplateField HeaderText="功能代碼" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFunCode" runat="server" Text='<%# Bind("功能代碼") %>'></asp:Label>
                                <asp:Button ID="btnSearchFunCode" runat="server" Text="<%$ Resources:WebResources, Choose %>" Visible="false" />
                                <uc1:PopupWindow ID="PopupWindow2" runat="server"
                                Name="SearchFunCode" 
                                PopupButtonID="btnSearchFunCode" 
                                TargetControlID="lblFunCode"
                                Width="450" Height="400"  
                                OnOkScript="onOk"                     
                                NavigateUrl="~/VSS/LOG/SearchFunCode.aspx" Visible="false" /> 
                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="功能名稱" HeaderText="<%$ Resources:WebResources, FunctionName %>" />
                        <asp:BoundField DataField="URL" HeaderText="<%$ Resources:WebResources, Url %>" />
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
                        
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
        </div>        
    </div>
    </form>
</body>
</html>
