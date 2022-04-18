<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG04.aspx.cs" Inherits="VSS_LOG_LOG04" %>
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
                        <!--系統參數設定-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SystemParametersSetting %>" />
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--參數代碼-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ParameterCode %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--參數名稱-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ParameterName %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <%--<td class="tdtxt">
                        <!--參數狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ParameterStatus %>" />：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>已失效</asp:ListItem>
                        </asp:DropDownList>
                    </td>--%>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--參數分類-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ParameterCategory %>" />：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>參數一</asp:ListItem>
                            <asp:ListItem>參數二</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
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
                <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="true" PageSize="10" PagerStyle-HorizontalAlign="Right" AutoGenerateColumns="False" CssClass="mGrid"
                    OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing" OnPageIndexChanging="GridView_PageIndexChanging"
                    OnRowUpdating="gvMaster_RowUpdating">
                    <EmptyDataTemplate>
                        <tr>    
                            <th scope="col"></th>                       
                            <th scope="col">
                                <!--參數分類-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ParameterCategory %>" />
                            </th>
                            <th scope="col">
                                <!--參數代碼-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ParameterCode %>" />
                            </th>
                            <th scope="col">
                                <!--參數名稱-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ParameterName %>" />
                            </th>
                            <th scope="col">
                                <!--值-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ParameterValue %>" />
                            </th>
                            <th scope="col">
                                <!--備註說明-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, RemarkAndDescription %>" />
                            </th>
                            <th scope="col">
                                <!--更新日期-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>" />
                            </th>
                            <th scope="col">
                                <!--更新人員-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>" />
                            </th>
                        </tr>
                        <tr id="trEmptyData" runat="server">
                            <td colspan="8" class="tdEmptyData">
                                <!--此無明細資料-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>" />
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
                                                
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ParameterCategory %>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtParameterCategory" runat="server" Text='<%# Bind("參數分類") %>' Width="80"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblParameterCategory" runat="server" Text='<%# Eval("參數分類") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtParameterCategory" runat="server" Width="80"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ParameterCode %>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtParameterCode" runat="server" Text='<%# Bind("參數代碼") %>' Width="80"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblParameterCode" runat="server" Text='<%# Eval("參數代碼") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtParameterCode" runat="server" Width="80"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ParameterName %>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtParameterName" runat="server" Text='<%# Bind("參數名稱") %>' Width="80"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblParameterName" runat="server" Text='<%# Eval("參數名稱") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtParameterName" runat="server" Width="80"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>                                                                                              
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ParameterValue %>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtParameterValue" runat="server" Text='<%# Bind("值") %>' Width="40"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblParameterValue" runat="server" Text='<%# Eval("值") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtParameterValue" runat="server" Width="40"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, RemarkAndDescription %>">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarkAndDescription" runat="server" Text='<%# Bind("備註說明") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRemarkAndDescription" runat="server" Text='<%# Eval("備註說明") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtRemarkAndDescription" runat="server"></asp:TextBox>
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
        </div>
    </div>
    </form>
</body>
</html>
