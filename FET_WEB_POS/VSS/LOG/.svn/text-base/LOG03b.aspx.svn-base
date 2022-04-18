<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG03b.aspx.cs" Inherits="VSS_LOG_LOG03b" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>    
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
                        <!--使用者功能對應作業-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, UserPermissionMapping %>" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, UserPermissionMapping %>" Visible="false" />
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                    <!--區域別-->
                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>" />：</td>
                    <td class="tdval" colspan="5">
                    <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem>北一區</asp:ListItem>
                            <asp:ListItem>中一區</asp:ListItem>
                            <asp:ListItem>南一區</asp:ListItem>
                        </asp:DropDownList>                    
                    </td>
                </tr>
                <tr>
                   <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StoreNo %>" />：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList3" runat="server">
                        <asp:ListItem>-請選擇-</asp:ListItem>
                        <asp:ListItem>2101</asp:ListItem>
                        <asp:ListItem>2102</asp:ListItem>
                        <asp:ListItem>2103</asp:ListItem>
                    </asp:DropDownList>
                    </td>     
                    <td class="tdtxt">
                        <!--使用者代碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, UserID %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--員工編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>" />：
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
                        <!--使用者名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, UserName %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                   </td>
                   <td class="tdtxt"></td>
                   <td class="tdval"></td>
                </tr>                
            </table>
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
                <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>" />
                <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                <asp:HiddenField ID="PermissionsHiddenField" runat="server" OnValueChanged="PermissionsHiddenField_ValueChanged" />
                <uc1:PopupWindow ID="PopupWindow1" runat="server"
                            Name="GrantingPermission" 
                            PopupButtonID="btnAdd" 
                            TargetControlID="PermissionsHiddenField"
                            Width="350" Height="320"  
                            OnOkScript="onOk"                     
                            NavigateUrl="~/VSS/LOG/GrantEmployeePermissions.aspx" />    
            </div>
            <div class="GridScrollBar" style="height: auto">
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" PagerStyle-HorizontalAlign="Right" CssClass="mGrid"
                    OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing" OnPageIndexChanging="GridView_PageIndexChanging"
                    OnRowUpdating="gvMaster_RowUpdating">
                    <EmptyDataTemplate>
                        <tr>                            
                            <th scope="col">
                                <!--項次-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Items %>" />
                            </th>
                            <th scope="col">
                                <!--員工編號-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>" />
                            </th>
                            <th scope="col">
                                <!--員工姓名-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, EmployeeName %>" />
                            </th>
                            <th scope="col">
                                <!--門市編號-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, StoreNo %>" />
                            </th>
                            <th scope="col">
                                <!--門市名稱-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StoreName %>" />
                            </th>
                            <th scope="col">
                                <!--區域別-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>" />
                            </th>
                            <th scope="col">
                                <!--模組名稱-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Modulename %>" />
                            </th>
                            <th scope="col">
                                <!--功能名稱-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, FunctionName %>" />
                            </th>
                            <th scope="col">
                                <!--更新人員-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>" />
                            </th>
                            <th scope="col">
                                <!--更新日期-->
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="10" class="tdEmptyData">
                                <!--請點選新增按鍵增加資料-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
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
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" />

                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, EmployeeNo %>">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEmployeeNo" runat="server" Text='<%# Bind("員工編號") %>' Width="80"></asp:TextBox>
                                    <asp:Button ID="btnChooseEmployee" runat="server" Text="<%$  Resources:WebResources, Choose %>" />
                                    <uc1:PopupWindow ID="PopupWindow1" runat="server"
                                        Name="ChooseEmployee" 
                                        PopupButtonID="btnChooseEmployee" 
                                        TargetControlID="txtEmployeeNo"
                                        Width="300" Height="320"                                                
                                        NavigateUrl="~/VSS/LOG/SearchEmpNum.aspx" />                                        
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeNo" runat="server" Text='<%# Eval("員工編號") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEmployeeNo" runat="server" Width="80"></asp:TextBox>
                                </FooterTemplate>
                         </asp:TemplateField>                                                     
                        <asp:BoundField DataField="員工姓名" HeaderText="<%$ Resources:WebResources, EmployeeName %>" ReadOnly="true" />
                        
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreNo %>">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStoreNo" runat="server" Text='<%# Bind("門市編號") %>' Width="80"></asp:TextBox>
                                    <asp:Button ID="btnChooseStore" runat="server" Text="<%$  Resources:WebResources, Choose %>" />
                                    <uc1:PopupWindow ID="PopupWindow1" runat="server"
                                        Name="ChooseEmployee" 
                                        PopupButtonID="btnChooseStore" 
                                        TargetControlID="txtStoreNo"
                                        Width="300" Height="320"                                                
                                        NavigateUrl="~/VSS/SAL/SAL01_chooseStore.aspx" />                                        
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStoreNo" runat="server" Text='<%# Eval("門市編號") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:TextBox ID="txtStoreNo" runat="server" Width="80"></asp:TextBox>
                                </FooterTemplate>
                         </asp:TemplateField>                                                                                                                          
                        <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" ReadOnly="true" />                        
                        <asp:BoundField DataField="區域別" HeaderText="<%$ Resources:WebResources, ByDistrict %>" ReadOnly="true" />                        
                         <asp:TemplateField HeaderText="<%$ Resources:WebResources, ModuleName %>">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlModuleName" runat="server" SelectedValue='<%# Bind("模組名稱") %>'>
                                        <asp:ListItem>-請選擇-</asp:ListItem>
                                        <asp:ListItem Value="模組A">模組A</asp:ListItem>
                                        <asp:ListItem Value="模組B">模組B</asp:ListItem>
                                        <asp:ListItem Value="模組C">模組C</asp:ListItem>
                                        <asp:ListItem Value="模組D">模組D</asp:ListItem>
                                        <asp:ListItem Value="模組E">模組E</asp:ListItem>
                                        <asp:ListItem Value="模組F">模組F</asp:ListItem>
                                    </asp:DropDownList>                                                                                                                                         
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("模組名稱") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlModuleName" runat="server">
                                        <asp:ListItem>-請選擇-</asp:ListItem>
                                        <asp:ListItem Value="模組A">模組A</asp:ListItem>
                                        <asp:ListItem Value="模組B">模組B</asp:ListItem>
                                        <asp:ListItem Value="模組C">模組C</asp:ListItem>
                                        <asp:ListItem Value="模組D">模組D</asp:ListItem>
                                        <asp:ListItem Value="模組E">模組E</asp:ListItem>
                                        <asp:ListItem Value="模組F">模組F</asp:ListItem>
                                    </asp:DropDownList> 
                                </FooterTemplate>
                         </asp:TemplateField>
                         
                         <asp:TemplateField HeaderText="<%$ Resources:WebResources, FunctionName %>">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlFuncName" runat="server" SelectedValue='<%# Bind("功能名稱") %>'>
                                        <asp:ListItem>-請選擇-</asp:ListItem>
                                        <asp:ListItem Value="功能A">功能A</asp:ListItem>
                                        <asp:ListItem Value="功能B">功能B</asp:ListItem>
                                        <asp:ListItem Value="功能C">功能C</asp:ListItem>
                                        <asp:ListItem Value="功能D">功能D</asp:ListItem>
                                        <asp:ListItem Value="功能E">功能E</asp:ListItem>
                                        <asp:ListItem Value="功能F">功能F</asp:ListItem>
                                    </asp:DropDownList>                                     
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFuncName" runat="server" Text='<%# Eval("功能名稱") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlFuncName" runat="server">
                                        <asp:ListItem>-請選擇-</asp:ListItem>
                                        <asp:ListItem Value="功能A">功能A</asp:ListItem>
                                        <asp:ListItem Value="功能B">功能B</asp:ListItem>
                                        <asp:ListItem Value="功能C">功能C</asp:ListItem>
                                        <asp:ListItem Value="功能D">功能D</asp:ListItem>
                                        <asp:ListItem Value="功能E">功能E</asp:ListItem>
                                        <asp:ListItem Value="功能F">功能F</asp:ListItem>
                                    </asp:DropDownList> 
                                </FooterTemplate>
                         </asp:TemplateField>                                                
                        <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" />
                        <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true" />                                                                                               
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
        <div class="btnPosition">
        </div>
    </div>
    </form>
</body>
</html>
