<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD06.aspx.cs" Inherits="VSS_ORD06_ORD06" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>    
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--一搭一設定作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TwoForOneOfferSetting %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--主商品編號-->
                            <asp:Literal ID="Literal1" runat="server" Text="主商品編號"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            <asp:TextBox ID="TextBox3" runat="server" Text="10004" Width="60px"></asp:TextBox>
                            <asp:Button ID="Button8" runat="server" Text="選" OnClientClick="openwindow('ORD01_searchProductNo.aspx',640,300);return false;" />
                            &nbsp;<asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            <asp:TextBox ID="TextBox1" runat="server" Text="10004" Width="60px"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" Text="選" OnClientClick="openwindow('ORD01_searchProductNo.aspx',640,300);return false;" />
                        </td>
                        <td class="tdtxt">
                            <!--主商品名稱-->
                            <asp:Literal ID="Literal3" runat="server" Text="主商品名稱"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox4" runat="server" Text="主商品名稱1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--搭配商品編號-->
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, CollocationProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            <asp:TextBox ID="TextBox2" runat="server" Text="10004" Width="60px"></asp:TextBox>
                            <asp:Button ID="Button5" runat="server" Text="選" OnClientClick="openwindow('ORD01_searchProductNo.aspx',640,300);return false;" />
                            &nbsp;<asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            <asp:TextBox ID="TextBox5" runat="server" Text="10004" Width="60px"></asp:TextBox>
                            <asp:Button ID="Button6" runat="server" Text="選" OnClientClick="openwindow('ORD01_searchProductNo.aspx',640,300);return false;" />
                        </td>
                        <td class="tdtxt">
                            <!--搭配商品名稱-->
                            <asp:Literal ID="Literal23" runat="server" Text="搭配商品名稱"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox6" runat="server" Text="搭配商品名稱1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--搭配日期-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CollocationDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox
                                ID="postbackDate_TextBox5" runat="server" ImageUrl="~/Icon/calendar.jpg" Width="110px" />
                            &nbsp;<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox
                                ID="postbackDate_TextBox6" runat="server" ImageUrl="~/Icon/calendar.jpg" Width="110px" />
                        </td>
                        <td class="tdtxt">
                            <!--狀 態-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>生效</asp:ListItem>
                                <asp:ListItem>過期</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                    OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <div class="SubEditCommand">
                <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                    OnClick="btnNew_Click" />
                <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating" 
                            OnSelectedIndexChanged="gvMaster_SelectedIndexChanged" AllowPaging="True" 
                            PageSize="5">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
                                <th></th>
                                <th></th>
                                    <th scope="col">
                                        <!--項次-->
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--狀態-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--主商品編號-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PrimaryProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--開始日期-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--結束日期-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新人員-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="10" class="tdEmptyData">
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                     <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckALL" runat="server" CssClass="CompenItems" onclick="javascript:if(this.checked){$('.CompenItems').checkCheckboxes();}else{$('.CompenItems').unCheckCheckboxes();}" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" CssClass="CompenItems" />
                                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <FooterTemplate>                                    
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                               <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ControlStyle-Width="40px"
                                    FooterStyle-Width="40px" FooterStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>"
                                            CommandName="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                            CommandName="Update" />
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                            CommandName="Cancel" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                            CommandName="Save" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                            OnClick="btnCancel_Click" />
                                    </FooterTemplate>
                                    <ControlStyle Width="40px" />
                                    <FooterStyle Width="40px" Wrap="True" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                
                                
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>"
                                    ReadOnly="true" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, PrimaryProductCode %>">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnProductNo" CommandName="Select" runat="server" Text='<%# Bind("主商品編號") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    <asp:LinkButton ID="lbtnProductNo" CommandName="Select" runat="server" Text='<%# Bind("主商品編號") %>' />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>"
                                    ReadOnly="true" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, StartDate %>">
                                    <EditItemTemplate>
                                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox3" runat="server" Text='<%# Bind("開始日期") %>'
                                            ImageUrl="~/Icon/calendar.jpg" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("開始日期") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    <cc1:postbackDate_TextBox ID="PostbackDate_TextBox3" runat="server" Text='<%# Bind("開始日期") %>'
                                            ImageUrl="~/Icon/calendar.jpg" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, EndDate %>">
                                    <EditItemTemplate>
                                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox4" runat="server" ImageUrl="~/Icon/calendar.jpg"
                                            Text='<%# Bind("結束日期") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("結束日期") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                     <cc1:postbackDate_TextBox ID="PostbackDate_TextBox4" runat="server" ImageUrl="~/Icon/calendar.jpg"
                                            Text='<%# Bind("結束日期") %>' />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>"
                                    ReadOnly="true" />
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
                        <div class="seperate">
                        </div>
                        <div class="SubEditCommand" id="dviShow" runat="server" visible="false">
                            <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                OnClick="btnNew3_Click" />
                        </div>
                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            Visible="false" OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                            OnRowUpdating="gvDetail_RowUpdating">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--搭配商品編號-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, CollocationProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="2" class="tdEmptyData">
                                        <!--此無明細資料-->
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:CommandField ShowEditButton="True" ButtonType="Button" UpdateText="<%$ Resources:WebResources, Save %>" />
                                <asp:BoundField DataField="搭配商品編號" HeaderText="<%$ Resources:WebResources, CollocationProductCode %>" />
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>"
                                    ReadOnly="true" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                    OnClick="btnSave_Click" />
                <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
