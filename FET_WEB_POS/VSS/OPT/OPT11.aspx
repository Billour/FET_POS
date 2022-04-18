<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT11.aspx.cs" Inherits="VSS_OPT11_OPT11" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

    <style type="text/css">
        .mGrid
        {
            margin-top: 0px;
        }
    </style>

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
                        <!--HappyGo點數兌換設定-->
                        <asp:Literal ID="Literal1" runat="server" Text="HappyGo點數兌換設定"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table width="100%">
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--類別-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem>ALL</asp:ListItem>
                                <asp:ListItem>銷售</asp:ListItem>
                                <asp:ListItem>代收</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--開始日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="開始日期"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3" nowrap="nowrap">
                            起<cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                            &nbsp;訖<cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--兌點名稱-->
                            <asp:Literal ID="Literal16" runat="server" Text="兌點名稱"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--兌點點數-->
                            <asp:Literal ID="Literal17" runat="server" Text="兌點點數"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3" nowrap="nowrap">
                            起<asp:TextBox ID="txtMemo1" runat="server" Width="40%"></asp:TextBox>
                            訖<asp:TextBox ID="txtMemo2" runat="server" Width="40%"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--兌換金額-->
                            <asp:Literal ID="Literal5" runat="server" Text="兌換金額"></asp:Literal>：
                        </td>
                        <td colspan="5" class="tdval" nowrap="nowrap">
                            起<asp:TextBox ID="txtMemo" runat="server" Width="30%"></asp:TextBox>
                            訖<asp:TextBox ID="txtMemo0" runat="server" Width="30%"></asp:TextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                        </td>
                        <td class="tdval" nowrap="nowrap"></td>
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
            
                    <div id="Div1" class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                OnClick="btnAddNew_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                        </div>
                        <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="true" PageSize="5" PagerStyle-HorizontalAlign="Right" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing" OnPageIndexChanging="GridView_PageIndexChanging"
                            OnRowUpdating="gvMaster_RowUpdating">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col" nowrap="nowrap">
                                            &nbsp;
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <asp:Literal ID="Literal18" runat="server" Text=""></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--項次-->
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--類別-->
                                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--兑點代號-->
                                            <asp:Literal ID="Literal10" runat="server" Text="兑點代號"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--兑點名稱-->
                                            <asp:Literal ID="Literal11" runat="server" Text="兑點名稱"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--開始日期-->
                                            <asp:Literal ID="Literal6" runat="server" Text="開始日期"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--結束日期-->
                                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--點數-->
                                            <asp:Literal ID="Literal9" runat="server" Text="點數"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--兑換金額-->
                                            <asp:Literal ID="Literal13" runat="server" Text="兑換金額"></asp:Literal>
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
                                        <td colspan="12" class="tdEmptyData">
                                            <!--請點選新增按鍵增加資料-->
                                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript:if(this.checked){$('#Div1').checkCheckboxes();}else{$('#Div1').unCheckCheckboxes();}" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckItem" runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center">
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

                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, Items %>" ControlStyle-Width="40px" >
                                                    <EditItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("項次") %>' ReadOnly="true"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, Category %>" ControlStyle-Width="60px" >
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server" Width="60px">
                                                <asp:ListItem>銷售</asp:ListItem>
                                                <asp:ListItem>代收</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("類別") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server" Width="60px">
                                                <asp:ListItem>銷售</asp:ListItem>
                                                <asp:ListItem>代收</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="兑點代號" ControlStyle-Width="80px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt1" runat="server" Text='<%# Bind("兑點代號") %>' Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl1" runat="server" Text='<%# Eval("兑點代號") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt1" runat="server" Width="75px"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="兑點名稱" ControlStyle-Width="80px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("兑點名稱") %>' Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl2" runat="server" Text='<%# Eval("兑點名稱") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt2" runat="server" Width="75px"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="開始日期" ControlStyle-Width="80px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt3" runat="server" Text='<%# Bind("開始日期") %>' Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl3" runat="server" Text='<%# Eval("開始日期") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt3" runat="server" Width="75px"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="結束日期" ControlStyle-Width="80px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt4" runat="server" Text='<%# Bind("結束日期") %>' Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl4" runat="server" Text='<%# Eval("結束日期") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt4" runat="server" Width="75px"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="點數" ControlStyle-Width="30px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt5" runat="server" Text='<%# Bind("點數") %>' Width="30px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl5" runat="server" Text='<%# Eval("點數") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt5" runat="server" Width="30px"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="兑換金額" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt6" runat="server" Text='<%# Bind("兑換金額") %>' Width="30px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl6" runat="server" Text='<%# Eval("兑換金額") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt6" runat="server" Width="50px"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %> "
                                        ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" 
                                        HeaderStyle-Wrap="false" >
                                    </asp:BoundField>
                                    <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>"
                                        ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" 
                                        HeaderStyle-Wrap="false" >
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
            
            <div class="seperate">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
