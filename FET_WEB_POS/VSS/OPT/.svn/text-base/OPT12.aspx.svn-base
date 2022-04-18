<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT12.aspx.cs" Inherits="VSS_OPT12_OPT12" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
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
                        <!--HappyGo點數累點設定-->
                        <asp:Literal ID="Literal10" runat="server" Text="HappyGo點數累點設定"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--累點名稱-->
                        累點名稱：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開始日期-->
                        開始日期：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox3" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox4" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--累點點數-->
                        累點點數：
                    </td>
                    <td class="tdval" colspan="5" nowrap="nowrap">
                        起<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        訖<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--累點金額-->
                        累點金額：
                    </td>
                    <td class="tdval" colspan="5" nowrap="nowrap">
                        起<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        訖<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
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

        <div>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                Width="100%" CssClass="visoft__tab_xpie7">
                <asp:TabPanel ID="TabPanel3" runat="server">
                    <HeaderTemplate>
                        <span>
                            <!--累點設定-->
                            <asp:Literal ID="Literal3" runat="server" Text="累點設定"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdataPanel3" runat="server">
                            <ContentTemplate>
                                <div id="Div1" runat="server" class="SubEditBlock">
                                <div class="SubEditCommand">
                                    <asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                        OnClick="btnAddNew_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                </div>
                                <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                                    OnRowUpdating="gvMaster_RowUpdating" AllowPaging="true" PageSize="5" PagerStyle-HorizontalAlign="Right"
                                    OnPageIndexChanging="GridView_PageIndexChanging">
                                    <EmptyDataTemplate>
                                        <tr>
                                            <th scope="col">&nbsp;</th>
                                            <th scope="col" nowrap="nowrap">
                                                <asp:Literal ID="Literal18" runat="server" Text=""></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--項次-->
                                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--累點代號-->
                                                <asp:Literal ID="Literal12" runat="server" Text="累點代號"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--累點名稱-->
                                                <asp:Literal ID="Literal10" runat="server" Text="累點名稱"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--開始日期-->
                                                <asp:Literal ID="Literal6" runat="server" Text="開始日期"></asp:Literal>
                                            </th>
                                            <th scope="col nowrap="nowrap"
                                                <!--結束日期-->
                                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>
                                            </th>
                                            <th scope="col nowrap="nowrap"
                                                <!--累點金額-->
                                                <asp:Literal ID="Literal11" runat="server" Text="累點金額"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--累點點數-->
                                                <asp:Literal ID="Literal9" runat="server" Text="累點點數"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--更新人員-->
                                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--更新日期-->
                                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr id="trEmptyData" runat="server">
                                            <td colspan="11" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckAll" runat="server" CssClass="SHERRY" onclick="javascript:if(this.checked){$('.SHERRY').checkCheckboxes();}else{$('.SHERRY').unCheckCheckboxes();}" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckItem" runat="server" CssClass="SHERRY" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
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

                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, Items %>" ControlStyle-Width="40px">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label13" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("項次") %>' ReadOnly="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="累點代號" ControlStyle-Width="80px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt1" runat="server" Text='<%# Bind("累點代號") %>' Width="50px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("累點代號") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt1" runat="server" Width="50px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="累點名稱" ControlStyle-Width="80px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("累點名稱") %>' Width="100px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl2" runat="server" Text='<%# Eval("累點名稱") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt2" runat="server" Width="100px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="開始日期" ControlStyle-Width="80px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt3" runat="server" Text='<%# Bind("開始日期") %>' Width="80px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl3" runat="server" Text='<%# Eval("開始日期") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt3" runat="server" Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, EndDate %>" ControlStyle-Width="80px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt4" runat="server" Text='<%# Bind("結束日期") %>' Width="80px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl4" runat="server" Text='<%# Eval("結束日期") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt4" runat="server" Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="累點金額" ControlStyle-Width="80px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt5" runat="server" Text='<%# Bind("累點金額") %>' Width="50px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl5" runat="server" Text='<%# Eval("累點金額") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt5" runat="server" Width="50px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="累點點數" ControlStyle-Width="80px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt61" runat="server" Text='<%# Bind("累點點數") %>' Width="50px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl6" runat="server" Text='<%# Eval("累點點數") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt6" runat="server" Width="50px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>"
                                            ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>"
                                            ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false"  />
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
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                        <span>
                            <!--會員日期設定-->
                            <asp:Literal ID="Literal18" runat="server" Text="會員日期設定"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="Button1_Click" /><asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                    </div>
                                    <cc1:ExGridView ID="gvMember" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        OnRowCancelingEdit="gvMember_RowCancelingEdit" OnRowEditing="gvMember_RowEditing"
                                        OnRowUpdating="gvMember_RowUpdating">
                                        <EmptyDataTemplate>
                                            <tr>
                                                <th scope="col">&nbsp;</th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--項次-->
                                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                                </th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--會員起日-->
                                                    <asp:Literal ID="Literal18" runat="server" Text="會員起日"></asp:Literal>
                                                </th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--會員訖日-->
                                                    <asp:Literal ID="Literal19" runat="server" Text="會員訖日"></asp:Literal>
                                                </th>
                                            </tr>
                                            <tr id="trEmptyData" runat="server">
                                                <td colspan="4" class="tdEmptyData">
                                                    <!--請點選新增按鍵增加資料-->
                                                    <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                        <Columns>

                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                     <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                                      <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click1" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click1" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, Items %>">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("項次") %>' ReadOnly="true"> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="會員起日" ControlStyle-Width="80px">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt7" runat="server" Text='<%# Bind("會員起日") %>' Width="80px"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl7" runat="server" Text='<%# Eval("會員起日") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt7" runat="server" Width="80px"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="會員訖日" ControlStyle-Width="80px">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt8" runat="server" Text='<%# Bind("會員訖日") %>' Width="80px"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl8" runat="server" Text='<%# Eval("會員訖日") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt8" runat="server" Width="80px"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </cc1:ExGridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server">
                    <HeaderTemplate>
                        <span>
                            <!--排外條件-->
                            <asp:Literal ID="Literal21" runat="server" Text="排外條件"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="Button3_Click" /><asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                    </div>
                                    <cc1:ExGridView ID="gvCondition" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        OnRowCancelingEdit="gvCondition_RowCancelingEdit" OnRowEditing="gvCondition_RowEditing"
                                        OnRowUpdating="gvCondition_RowUpdating">
                                        <EmptyDataTemplate>
                                            <tr>
                                                <th scope="col">&nbsp;</th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--項次-->
                                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                                </th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--商品料號-->
                                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                                </th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--商品名稱-->
                                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                                </th>
                                            </tr>
                                            <tr id="trEmptyData" runat="server">
                                                <td colspan="4" class="tdEmptyData">
                                                    <!--請點選新增按鍵增加資料-->
                                                    <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                        <Columns>

                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                     <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                                      <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click2" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click2" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, Items %>">
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" >
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt9" runat="server" Text='<%# Bind("商品料號") %>' Width="80px"></asp:TextBox>
                                                    <asp:Button ID="ChooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>" Width="25px" />
                                                    <uc1:PopupWindow ID="PopupWindow3" runat="server"
                                                    Name="Choose" 
                                                    PopupButtonID="ChooseButton1" 
                                                    TargetControlID="txt9"
                                                    Width="550" Height="400"                       
                                                    NavigateUrl="~/VSS/SAL/SAL01_searchProductNo.aspx" />                                    
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl9" runat="server" Text='<%# Eval("商品料號") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt9" runat="server" Width="80px"></asp:TextBox>
                                                    <asp:Button ID="ChooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>" Width="25px" />
                                                        <uc1:PopupWindow ID="PopupWindow3" runat="server"
                                                        Name="Choose" 
                                                        PopupButtonID="ChooseButton1" 
                                                        TargetControlID="txt9"
                                                        Width="550" Height="400"                       
                                                        NavigateUrl="~/VSS/SAL/SAL01_searchProductNo.aspx" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %> "
                                                ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" 
                                                HeaderStyle-Wrap="false" >
                                            </asp:BoundField>
                                
                                        </Columns>
                                    </cc1:ExGridView> 
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
        <div class="seperate">
        </div>
    </form>
</body>
</html>
