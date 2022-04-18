<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG05.aspx.cs" Inherits="VSS_LOG_LOG05" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                        <!--功能清單設定-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, FunctionList %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--系統別-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, System %>" />：
                    </td>
                    <td class="tdval">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:DropDownList ID="DropDownList2" runat="server">
                                    <asp:ListItem>-請選擇-</asp:ListItem>
                                    <asp:ListItem>Online</asp:ListItem>
                                    <asp:ListItem>Offline</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdtxt">
                        <!--功能代碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, FunctionCode %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--功能狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, FunctionStatus %>" />：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>已失效</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--模組名稱-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModuleName %>" />：
                    </td>
                    <td class="tdval">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:DropDownList ID="DropDownList4" runat="server">
                                    <asp:ListItem>-請選擇-</asp:ListItem>
                                    <asp:ListItem>模組一</asp:ListItem>
                                    <asp:ListItem>模組二</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdtxt">
                        <!--功能名稱-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, FunctionName %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                </tr>
                </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock" id="div1" runat="server">
            <div class="GridScrollBar" style="height: auto">
                <div class="SubEditCommand">
                    <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click" />
                </div>
                <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="true" PageSize="10" PagerStyle-HorizontalAlign="Right"  AutoGenerateColumns="False" CssClass="mGrid"
                    OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing" OnPageIndexChanging="GridView_PageIndexChanging"
                    OnRowUpdating="gvMaster_RowUpdating">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col"></th>
                            <th scope="col">
                                <!--狀態-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />
                            </th>
                            <th scope="col">
                                <!--系統別-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, System %>" />
                            </th>
                            <th scope="col">
                                <!--模組名稱-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModuleName %>" />                               
                            </th>
                            <th scope="col">
                                <!--功能代碼-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, FunctionCode %>" />
                            </th>
                            <th scope="col">
                                <!--功能名稱-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, FunctionName %>" />
                            </th>
                            <th scope="col">
                                <!--url-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Url %>" />                                
                            </th>
                            <th scope="col">
                                <!--更新日期-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>" />
                            </th>
                            <th scope="col">
                                <!--更新人員-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>" />
                            </th>
                        </tr>
                        <tr id="trEmptyData" runat="server">
                            <td colspan="9" class="tdEmptyData">
                                <!--此無明細資料-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                  <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>         
                        
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, Status %>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server" SelectedValue='<%# Bind("狀態") %>'>  
                                    <asp:ListItem>-請選擇-</asp:ListItem>                                  
                                    <asp:ListItem Value="有效">有效</asp:ListItem>
                                    <asp:ListItem Value="已失效">已失效</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("狀態") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlStatus" runat="server">  
                                    <asp:ListItem>-請選擇-</asp:ListItem>                                  
                                    <asp:ListItem Value="有效">有效</asp:ListItem>
                                    <asp:ListItem Value="已失效">已失效</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, System %>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlSystem" runat="server" SelectedValue='<%# Bind("系統別") %>'>
                                    <asp:ListItem>-請選擇-</asp:ListItem>                                  
                                    <asp:ListItem Value="Online">Online</asp:ListItem>
                                    <asp:ListItem Value="Offline">Offline</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSystem" runat="server" Text='<%# Bind("系統別") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlSystem" runat="server"> 
                                    <asp:ListItem>-請選擇-</asp:ListItem>                                   
                                    <asp:ListItem Value="Online">Online</asp:ListItem>
                                    <asp:ListItem Value="Offline">Offline</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>                                               
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ModuleName %>">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlModuleName" runat="server" SelectedValue='<%# Bind("模組名稱") %>'> 
                                    <asp:ListItem>-請選擇-</asp:ListItem>                                     
                                    <asp:ListItem Value="模組1">模組1</asp:ListItem>
                                    <asp:ListItem Value="模組2">模組2</asp:ListItem>
                                    <asp:ListItem Value="模組3">模組3</asp:ListItem>
                                    <asp:ListItem Value="模組4">模組4</asp:ListItem>
                                    <asp:ListItem Value="模組5">模組5</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("模組名稱") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlModuleName" runat="server"> 
                                    <asp:ListItem>-請選擇-</asp:ListItem>                                     
                                    <asp:ListItem Value="模組1">模組1</asp:ListItem>
                                    <asp:ListItem Value="模組2">模組2</asp:ListItem>
                                    <asp:ListItem Value="模組3">模組3</asp:ListItem>
                                    <asp:ListItem Value="模組4">模組4</asp:ListItem>
                                    <asp:ListItem Value="模組5">模組5</asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>  
                        
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, FunctionCode %>">
                            <EditItemTemplate>
                               <asp:TextBox ID="txtFunctionCode" runat="server" Width="80" Text='<%# Bind("功能代碼") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFunctionCode" runat="server" Text='<%# Bind("功能代碼") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFunctionCode" runat="server" Width="80"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, FunctionName %>">
                            <EditItemTemplate>
                               <asp:TextBox ID="txtFunctionName" runat="server" Width="80" Text='<%# Bind("功能名稱") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFunctionName" runat="server" Text='<%# Bind("功能名稱") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFunctionName" runat="server" Width="80"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, Url %>">
                            <EditItemTemplate>
                               <asp:TextBox ID="txtUrl" runat="server" Width="80" Text='<%# Bind("url") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblUrl" runat="server" Text='<%# Bind("url") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtUrl" runat="server" Width="80"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                                                                                                                    
                        <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true" />
                        <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" />
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
                </cc1:ExGridView>
            </div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" Visible="false" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" Visible="false" />
        </div>
    </div>
    </form>
</body>
</html>
