<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT02.aspx.cs" Inherits="VSS_OPT_OPT02" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        信用卡手續費設定作業
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        信用卡別：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlCardType" runat="server" />
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        狀態：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>尚未生效</asp:ListItem>
                            <asp:ListItem>已過期</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="清空" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server" class="SubEditBlock">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnAdd" runat="server" Text="新增" Visible="true"  OnClick="btnAdd_Click"/>
                        <%--<asp:Button ID="btnDelete" runat="server" Text="刪除" Visible="true" />--%>
                    </div>
                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating" AllowPaging="true" PageSize="5" PagerStyle-HorizontalAlign="Right"
                            OnPageIndexChanging="GridView_PageIndexChanging" >
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col" nowrap="nowrap">
                                        項次
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        狀態
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        信用卡別
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        手續費
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        開始日期
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        結束日期
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        更新日期
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                       更新人員
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                <td colspan="10" class="tdEmptyData">
                                    <!--查無資料，請修改條件，重新查詢-->
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                </td>
                            </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField  Visible="false">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript:if(this.checked){$('#Div1').checkCheckboxes();}else{$('#Div1').unCheckCheckboxes();}" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckItem" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
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
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="True"  FooterStyle-Wrap ="true" ControlStyle-Width="40px"/>
                                <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" ReadOnly="True" FooterStyle-Wrap ="true" ControlStyle-Width="40px" />
                                <asp:TemplateField HeaderText="信用卡別" FooterStyle-Wrap ="true" ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label_02" runat="server" Text='<%# Bind("信用卡別") %>' Width="80px"></asp:Label>
                                        <%--<asp:DropDownList ID="ddlCardType" runat="server">
                                            <asp:ListItem Text="VISA" Value="VISA"></asp:ListItem>
                                            <asp:ListItem Text="Master" Value="Master"></asp:ListItem>
                                            <asp:ListItem Text="AE" Value="AE"></asp:ListItem>
                                            <asp:ListItem Text="JCB" Value="AE"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("信用卡別") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                    <%--<asp:TextBox ID="txtCrdert" runat="server" Width="80"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlCardType" runat="server">
                                            <asp:ListItem Text="VISA" Value="VISA"></asp:ListItem>
                                            <asp:ListItem Text="Master" Value="Master"></asp:ListItem>
                                            <asp:ListItem Text="AE" Value="AE"></asp:ListItem>
                                            <asp:ListItem Text="JCB" Value="AE"></asp:ListItem>
                                        </asp:DropDownList>
                                </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="手續費" FooterStyle-Wrap ="true" ControlStyle-Width="40px" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <EditItemTemplate>
                                        <%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("手續費") %>' Width="40px" ></asp:TextBox>%--%>
                                        <asp:Label ID="Label_01" runat="server" Text='<%# Bind("手續費") %>' Width="40px"></asp:Label>%
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("手續費") %>' Width="40px"></asp:Label>%
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:TextBox ID="txtMoney" runat="server" Width="60"></asp:TextBox>%
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="開始日期">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("開始日期") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("開始日期") %>'></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:TextBox ID="txtStartDate" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="結束日期">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEndDate" runat="server" Width="80"></asp:TextBox>
                                        <%--<asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("結束日期") %>'></asp:Label>--%>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("結束日期") %>'></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:TextBox ID="txtEndDate" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>                                                                                                
                                <asp:BoundField DataField="更新日期" HeaderText="更新日期" ReadOnly="true" FooterStyle-Wrap ="true" ControlStyle-Width="40px"/>
                                <asp:BoundField DataField="更新人員" HeaderText="更新人員" ReadOnly="true" FooterStyle-Wrap ="true" ControlStyle-Width="40px" />
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
