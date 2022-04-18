<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON10.aspx.cs" Inherits="VSS_CON10_Default" %>

<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=250,left=330,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }       
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left" style="width: 99%">
                        <!--寄銷商品退倉設定作業(總部)-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSettings %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button6" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='CON09.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開單日期-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReceiptDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox4_CalendarExtender" runat="server" Format="yyyy/MM/dd"
                            TargetControlID="TextBox4">
                        </asp:CalendarExtender>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="Label1" runat="server" Text="未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉開始日-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉結束日-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox3" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="Label3" runat="server" Text="2010/07/01 22:00"></asp:Label>
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
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="seperate">
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
            Width="100%" CssClass="visoft__tab_xpie7">
            <asp:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>
                    <span><!--商品-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Product %>"></asp:Literal></span>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <div class="SubEditBlock">
                                <div class="SubEditCommand">
                                    <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnNew_Click" />
                                    <asp:Button ID="btnDel" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                </div>
                                <div class="GridScrollBar" style="height: auto">
                                    <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                        OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                                        OnRowUpdating="gvMaster_RowUpdating" AllowPaging="true" PageSize="5" PagerStyle-HorizontalAlign="Right"
                                        OnPageIndexChanging="GridView_PageIndexChanging">
                                        <EmptyDataTemplate>
                                            <tr>
                                                <th scope="col" nowrap="nowrap">
                                                    &nbsp;
                                                </th>
                                                <th scope="col" nowrap="nowrap">
                                                    &nbsp;
                                                </th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--廠商代號-->
                                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                                </th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--廠商名稱-->
                                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                                                </th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--商品料號-->
                                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                                </th>
                                                <th scope="col" nowrap="nowrap">
                                                    <!--商品名稱-->
                                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                                </th>
                                            </tr>
                                            <tr id="trEmptyData" runat="server">
                                                <td colspan="6" class="tdEmptyData">
                                                    <!--請點選新增按鍵增加資料-->
                                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="SHERRY" onclick="javascript:if(this.checked){$('.SHERRY').checkCheckboxes();}else{$('.SHERRY').unCheckCheckboxes();}"/>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" CssClass="SHERRY"/>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField  FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click" />
                                                </FooterTemplate> 
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, SupplierNo %>">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("廠商代號") %>'></asp:TextBox>
                                                    <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../CONS/CON10_chooseSupplierNo.aspx',500,400);return false;" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label33" runat="server" Text='<%# Bind("廠商代號") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("廠商代號") %>'></asp:TextBox>
                                                    <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../CONS/CON10_chooseSupplierNo.aspx',500,400);return false;" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" ReadOnly="true"/>
                                            
                                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("商品料號") %>'></asp:TextBox>
                                                    <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox7" runat="server" Text='<%# Bind("商品料號") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("商品料號") %>'></asp:TextBox>
                                                    <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true"/>
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
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>
                    <span><!--門市-->
                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Store %>"></asp:Literal>
                    </span>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td class="tdval">
                                        <table>
                                            <tr>
                                                <td class="tdcen">
                                                    <!--未選擇-->                                                   
                                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Nonselect %>"></asp:Literal>
                                                </td>
                                                <td class="tdcen">
                                                </td>
                                                <td class="tdcen">
                                                    <!--已選擇-->
                                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Selected %>"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdcen">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlSubZone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubZone_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="tdcen">
                                                </td>
                                                <td class="tdcen">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdListBox" rowspan="5">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="ListBox1" runat="server" Height="327px" SelectionMode="Multiple"
                                                                Width="259px"></asp:ListBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="tdBtn">
                                                </td>
                                                <td rowspan="5" class="tdListBox">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListBox ID="ListBox2" runat="server" Height="327px" SelectionMode="Multiple"
                                                                Width="259px"></asp:ListBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdBtn">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/next.png" OnClick="btnAdd_Click" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdBtn">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdBtn">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/previous.png" OnClick="btnBack_Click" /></ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdBtn">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    <br />
    <br />
    <div class="btnPosition">
        <asp:Button ID="Button10" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
        <asp:Button ID="Button11" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
        <asp:Button ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" />
        <asp:HiddenField ID="HiddenField1" runat="server"  OnValueChanged="HiddenField1_ValueChanged" />
        <uc1:PopupWindow ID="PopupWindow1" runat="server"
                        Name="Import" 
                        PopupButtonID="btnImport" 
                        TargetControlID="HiddenField1"                                    
                        Width="400" Height="400"  
                        OnOkScript="onOk"                     
                        NavigateUrl="~/VSS/INV/INV05_Import.aspx" />
        <script type="text/javascript">
            function onOk() {
                __doPostBack('<%= HiddenField1.UniqueID %>', '');
            }
        </script>

    </div>
    
    
    
    </form>
</body>
</html>
