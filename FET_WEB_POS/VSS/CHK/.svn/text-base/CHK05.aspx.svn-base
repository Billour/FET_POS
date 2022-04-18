<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CHK05.aspx.cs" Inherits="VSS_CHK05_CHK05" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
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
    <div class="titlef">
        <!--繳大鈔-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DollarBills %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--交易日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" Width="120" />
                    </td>
                    <td class="tdtxt">
                        <!--機台編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="機台編號"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem Text="01" Value="01"></asp:ListItem>
                            <asp:ListItem Text="02" Value="02"></asp:ListItem>
                            <asp:ListItem Text="03" Value="03"></asp:ListItem>
                            <asp:ListItem Text="04" Value="04"></asp:ListItem>
                            <asp:ListItem Text="05" Value="05"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>--%>
                <div id="Div1" runat="server" class="SubEditBlock">
                    <div class="SubEditCommand">
                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click" />
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                    </div>
                    <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                        OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                        OnRowUpdating="gvMaster_RowUpdating">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">&nbsp;</th>
                                <th scope="col">&nbsp;</th>
                                <th scope="col">
                                    <!--交易日期-->
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--機台編號-->
                                    <asp:Literal ID="Literal6" runat="server" Text="機台編號"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--批次-->
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, BatchNo %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--繳大鈔-->
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, DollarBills %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--更新人員-->
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--更新日期-->
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
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
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
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
                            <asp:BoundField DataField="交易日期" HeaderText="<%$ Resources:WebResources, TradeDate %>"
                                ReadOnly="true" />
                            <asp:TemplateField HeaderText="機台編號">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlCashRegister" runat="server" SelectedValue='<%# Bind("機台編號") %>'>
                                        <asp:ListItem>-請選擇-</asp:ListItem>
                                        <asp:ListItem Value="01">01</asp:ListItem>
                                        <asp:ListItem Value="02">02</asp:ListItem>
                                        <asp:ListItem Value="03">03</asp:ListItem>
                                        <asp:ListItem Value="04">04</asp:ListItem>
                                        <asp:ListItem Value="05">05</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCashRegister" runat="server" Text='<%# Eval("機台編號") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlCashRegister" runat="server">
                                        <asp:ListItem>-請選擇-</asp:ListItem>
                                        <asp:ListItem Value="01">01</asp:ListItem>
                                        <asp:ListItem Value="02">02</asp:ListItem>
                                        <asp:ListItem Value="03">03</asp:ListItem>
                                        <asp:ListItem Value="04">04</asp:ListItem>
                                        <asp:ListItem Value="05">05</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                           </asp:TemplateField>     
                                                        
                            <asp:BoundField DataField="批次" HeaderText="<%$ Resources:WebResources, BatchNo %>" ReadOnly="true" />
                                                        
                            <asp:TemplateField  HeaderText="<%$ Resources:WebResources, DollarBills %>">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("繳大鈔") %>' Width="80"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("繳大鈔") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" Width="80"></asp:TextBox>
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
                    </cc1:ExGridView>
                </div>
            <%--</ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>
    </div>
    </form>
</body>
</html>
