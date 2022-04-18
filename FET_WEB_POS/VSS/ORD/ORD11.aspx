<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD11.aspx.cs" Inherits="VSS_ORD11_ORD11" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "_blank", 'width=' + width + ',height=' + height + ',top=200,left=280,resizable=no,scrollbars=no,location=no,toolbar=no,status=no');
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
                        <!--商品建議訂購量設定-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductSuggestOrderAmountSetting %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--商品編號-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:TextBox ID="txtParNoStart" runat="server" Width="120px"></asp:TextBox><asp:Button ID="ChooseButton1"
                                runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                            <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="ProductSearch" PopupButtonID="ChooseButton1"
                                TargetControlID="txtParNoStart" Width="500" Height="500" NavigateUrl="~/VSS/ORD/ORD01_searchProductNo.aspx" />
                        </td>
                        <td class="tdtxt">
                            <!--商品名稱-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox4" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
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
            <div class="SubEditCommand">
                <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                    OnClick="btnAdd_Click" />
                <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="Div1" runat="server" class="SubEditBlock">
                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating" PageSize="5" PagerStyle-HorizontalAlign="Right"
                            OnPageIndexChanging="GridView_PageIndexChanging" AllowPaging="True" OnRowDataBound="gvMaster_RowDataBound">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        <!--商品料號-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--銷售基準-->
                                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, SalesBase %>"></asp:Literal>
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
                                        <!--安全係數-->
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SafeOrderCoefficient %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="8" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="10px" ControlStyle-Width="10px" FooterStyle-Width="10px" HeaderStyle-Width="10px"
                                    FooterStyle-Wrap="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript:if(this.checked){$('#Div1').checkCheckboxes();}else{$('#Div1').unCheckCheckboxes();}"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckItem" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
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
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" ItemStyle-Width="120px" FooterStyle-Width="120px" FooterStyle-Wrap="false" >
                                 <EditItemTemplate>
                                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("商品料號") %>' Width="80px"></asp:TextBox>
                                     <asp:Button ID="Button1" Width="20px" runat="server" Text="選" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,400);return false;"  />
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("商品料號") %>' Width="80px"></asp:Label>
                                 </ItemTemplate>
                                 <FooterTemplate>
                                     <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("商品料號") %>' Width="80px"></asp:TextBox>
                                     <asp:Button ID="Button11" Width="20px" runat="server" Text="選" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,400);return false;"  />
                                 </FooterTemplate>
                                 </asp:TemplateField >
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>"
                                    ReadOnly="true" ItemStyle-Width="80px" FooterStyle-Wrap="true" ControlStyle-Width="80px" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, SalesBase %>" ItemStyle-Width="80px"
                                    ControlStyle-Width="80px" FooterStyle-Width="80px" FooterStyle-Wrap="true">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem Value="0">半個月</asp:ListItem>
                                            <asp:ListItem Value="1">一個月</asp:ListItem>
                                            <asp:ListItem Value="2">指定期間</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hf2" runat="server" Value='<%# Bind("銷售基準") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("銷售基準") %>'></asp:Label>
                                        <asp:HiddenField ID="hf1" runat="server" Value='<%# Bind("銷售基準") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                            <asp:ListItem Value="0">半個月</asp:ListItem>
                                            <asp:ListItem Value="1">一個月</asp:ListItem>
                                            <asp:ListItem Value="2">指定期間</asp:ListItem>                                        
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, StartDate %>" ItemStyle-Width="100px"
                                    ControlStyle-Width="100px" FooterStyle-Width="100px" FooterStyle-Wrap="true">
                                    <EditItemTemplate>
                                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox3" runat="server" Text='<%# Bind("開始日期") %>'
                                            ImageUrl="~/Icon/calendar.jpg" Width="100px"/>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("開始日期") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox33" runat="server" Text='<%# Bind("開始日期") %>'
                                            ImageUrl="~/Icon/calendar.jpg" Width="100px" Enabled="false"/>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, EndDate %>" ItemStyle-Width="100px"
                                    ControlStyle-Width="100px" FooterStyle-Width="100px" FooterStyle-Wrap="true">
                                    <EditItemTemplate>
                                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox4" runat="server" ImageUrl="~/Icon/calendar.jpg"
                                            Text='<%# Bind("結束日期") %>' Width="100px" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("結束日期") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox44" runat="server" ImageUrl="~/Icon/calendar.jpg"
                                            Text='<%# Bind("結束日期") %>' Width="100px" Enabled="false" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, SafeOrderCoefficient %>" ItemStyle-Width="80px"
                                    ControlStyle-Width="80px" FooterStyle-Width="80px" FooterStyle-Wrap="true">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("安全係數") %>' Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("安全係數") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("安全係數") %>' Width="40px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>                                
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
                    </div>
                    </div>
                </ContentTemplate>
                <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                </Triggers>--%>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
