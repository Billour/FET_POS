<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT01.aspx.cs" Inherits="VSS_OPT_OPT01" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
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
        <!--支付方式設定作業-->
        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, PaymentMethodSettings %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--支付方式-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PaymentMethod2 %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem>信用卡</asp:ListItem>
                            <asp:ListItem>信用卡分期</asp:ListItem>
                            <asp:ListItem>禮券</asp:ListItem>
                            <asp:ListItem>現金</asp:ListItem>
                            <asp:ListItem>金融卡</asp:ListItem>
                            <asp:ListItem>HappyGo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--會計科目-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
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
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server" class="SubEditBlock" visible="true">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                            Visible="true" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                            Visible="false" />
                    </div>
                    <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCancelingEdit="gvMaster_RowCancelingEdit"
                        OnRowEditing="gvMaster_RowEditing" OnRowUpdating="gvMaster_RowUpdating" 
                        Width="120%" CssClass="mGrid" ShowFooterWhenEmpty="False" 
                        ShowHeaderWhenEmpty="False" onrowdatabound="gvMaster_RowDataBound" PageSize="5"
                        PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                        <EmptyDataTemplate>
                            <tr>
                               <%-- <th scope="col">
                                    &nbsp;
                                </th>--%>
                                <th scope="col">
                                    &nbsp;
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--項次-->
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--狀態-->
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--支付方式-->
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, PaymentMethod2 %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Subject %>"></asp:Literal>1
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Subject %>"></asp:Literal>2
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Subject %>"></asp:Literal>3
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Subject %>"></asp:Literal>4
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Subject %>"></asp:Literal>5
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Subject %>"></asp:Literal>6
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--開始日期-->
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--結束日期-->
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--更新日期-->
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--更新人員-->
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr id="trEmptyData" runat="server">
                                <td colspan="15" class="tdEmptyData">
                                    <!--查無資料，請修改條件，重新查詢-->
                                    <asp:Literal ID="Literal99" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-Width="30px" 
                                FooterStyle-Width="30px" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CommandName="Edit" 
                                        Text="<%$ Resources:WebResources, Edit %>" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" CommandName="Update" 
                                        Text="<%$ Resources:WebResources, Save %>" />
                                    <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" 
                                        Text="<%$ Resources:WebResources, Cancel %>" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnSave" runat="server" CommandName="Save" 
                                        OnClick="btnCancel_Click" Text="<%$ Resources:WebResources, Save %>" />
                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" 
                                        Text="<%$ Resources:WebResources, Cancel %>" />
                                </FooterTemplate>
                                <FooterStyle Width="30px" />
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-Width="30px" 
                                FooterStyle-Width="30px" HeaderText="<%$ Resources:WebResources, Items %>">
                                <EditItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("項次") %>' Width="40px"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("項次") %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Width="30px" />
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, Status %>" 
                                HeaderStyle-Width="60px" ItemStyle-Width="60px" FooterStyle-Width="60px">
                                <EditItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("狀態") %>' Width="60px"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("狀態") %>' Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Width="60px" />
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, PaymentMethod2 %>" HeaderStyle-Width="60px" ItemStyle-Width="60px" FooterStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:Label ID="lb1" runat="server" Text='<%# Bind("支付方式") %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lb11" runat="server" Text='<%# Bind("支付方式") %>' Width="40px"></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlPayType11" runat="server">
                                        <asp:ListItem>信用卡</asp:ListItem>
                                        <asp:ListItem>信用卡分期</asp:ListItem>
                                        <asp:ListItem>禮券</asp:ListItem>
                                        <asp:ListItem>現金</asp:ListItem>
                                        <asp:ListItem>金融卡</asp:ListItem>
                                        <asp:ListItem>HappyGo</asp:ListItem>
                                    </asp:DropDownList>                                
                                    </FooterTemplate>
                                <FooterStyle Width="40px" />
                                <HeaderStyle Width="40px" />
                                <FooterStyle Width="60px" />
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="科目1" HeaderStyle-Width="40px" ItemStyle-Width="40px" FooterStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:Label ID="lb2" runat="server" Text='<%# Bind("科目1") %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lb211" runat="server" Text='<%# Bind("科目1") %>' Width="40px"></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtb2" runat="server" Width="40px"></asp:TextBox>
                                </FooterTemplate>
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="科目2" HeaderStyle-Width="40px" ItemStyle-Width="40px" FooterStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:Label ID="lb3" runat="server" Text='<%# Bind("科目2") %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lb311" runat="server" Text='<%# Bind("科目2") %>' Width="40px"></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtb3" runat="server" Width="40px"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Width="40px" />
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="科目3" HeaderStyle-Width="40px" ItemStyle-Width="40px" FooterStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:Label ID="lb4" runat="server" Text='<%# Bind("科目3") %>' Width="60px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lb411" runat="server" Text='<%# Bind("科目3") %>' Width="60px"></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtb4" runat="server" Width="60px"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Width="40px" />
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="科目4" HeaderStyle-Width="40px" 
                                ItemStyle-Width="60px" FooterStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:Label ID="lb5" runat="server" Text='<%# Bind("科目4") %>' Width="60px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lb511" runat="server" Text='<%# Bind("科目4") %>' Width="60px"></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPaymen" runat="server" Width="60px"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Width="60px" />
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="科目5" HeaderStyle-Width="40px" ItemStyle-Width="40px" FooterStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:Label ID="lb6" runat="server" Text='<%# Bind("科目5") %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lb611" runat="server" Text='<%# Bind("科目5") %>' Width="40px"></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtb6" runat="server" Width="40px"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Width="40px" />
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="科目6" HeaderStyle-Width="40px" 
                                ItemStyle-Width="40px" FooterStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:Label ID="lb7" runat="server" Text='<%# Bind("科目6") %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lb711" runat="server" Text='<%# Bind("科目6") %>' Width="40px"></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtb7" runat="server" Width="40px"></asp:TextBox>
                                </FooterTemplate>
                                <FooterStyle Width="40px" />
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="開始日期" HeaderText="<%$ Resources:WebResources, StartDate %>"
                                ReadOnly="true" HeaderStyle-Width="40px" ItemStyle-Width="40px" 
                                FooterStyle-Width="40px" >
                                <FooterStyle Width="40px" />
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="結束日期" 
                                HeaderText="<%$ Resources:WebResources, EndDate %>" HeaderStyle-Width="40px" 
                                ItemStyle-Width="40px" ControlStyle-Width="80px" FooterStyle-Width="40px">
                                <ControlStyle Width="80px" />
                                <FooterStyle Width="40px" />
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>"
                                ReadOnly="true" HeaderStyle-Width="40px" ItemStyle-Width="40px" 
                                FooterStyle-Width="40px">
                                <FooterStyle Width="40px" />
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>"
                                ReadOnly="true" HeaderStyle-Width="80px" ItemStyle-Width="80px" 
                                FooterStyle-Width="80px">
                                <FooterStyle Width="80px" />
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
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
