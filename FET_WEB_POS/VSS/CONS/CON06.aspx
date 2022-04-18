<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON06.aspx.cs" Inherits="VSS_CON06_CON06" %>
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
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=230,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品訂貨作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentOrderPlacement %>"></asp:Literal>
                    </td>
                    <td align="right">&nbsp;</td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--訂單編號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="lblOrderNo" runat="server" Text="101900073"></asp:Label>
                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--訂單日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="lblOrderDate" runat="server">2010/07/01 22:00</asp:Label>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--狀態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="Label2" runat="server" Text="00 未存檔"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--廠商編號-->
                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources,SupplierNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                    AppendDataBoundItems="True">
                                <asp:ListItem>-請選擇-</asp:ListItem>
                                <asp:ListItem>AC1</asp:ListItem>
                                <asp:ListItem>AC2</asp:ListItem>
                                <asp:ListItem>AP1</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--廠商名稱-->
                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources,SupplierName %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:TextBox ID="TextBox1" runat="server" Width="98%"></asp:TextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources,ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--備註-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources,Remark %>"></asp:Literal>：
                        </td>
                        <td colspan="3" class="tdval" nowrap="nowrap">
                            <asp:TextBox ID="txtMemo" runat="server" Width="99%"></asp:TextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="Div1" class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="btnSaleToOrder" runat="server" Text="<%$ Resources:WebResources, SalesToOrder %>" />
                            <asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddNew_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                        </div>
                            <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                                OnRowUpdating="gvMaster_RowUpdating" AllowPaging="True"
                                 PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col" nowrap="nowrap">
                                            &nbsp;
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            &nbsp;
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--項次-->
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--商品編號-->
                                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--商品名稱-->
                                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--商品類別-->
                                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--建議訂購量-->
                                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, RecommendedOrderQuantity %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--實際訂購量-->
                                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ActualOrderQuantity %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--單價-->
                                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnitPrice %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--總價-->
                                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, TotalPrice %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr id="trEmptyData" runat="server">
                                        <td colspan="10" class="tdEmptyData">
                                            <!--請點選新增按鍵增加資料-->
                                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript:if(this.checked){$('#Div1').checkCheckboxes();}else{$('#Div1').unCheckCheckboxes();}"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckItem" runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
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

                                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" FooterStyle-Wrap="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("商品料號") %>' Width="70"></asp:TextBox>
                                            <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("商品料號") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("商品料號") %>' Width="70"></asp:TextBox>
                                            <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="商品類別" HeaderText="<%$ Resources:WebResources, ProductCategory %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="建議訂購量" HeaderText="<%$ Resources:WebResources, RecommendedOrderQuantity %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, ActualOrderQuantity %>" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" FooterStyle-Wrap="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("實際訂購量") %>' Width="50"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("實際訂購量") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("實際訂購量") %>' Width="50"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="單價" HeaderText="<%$ Resources:WebResources, UnitPrice %>" ReadOnly="true" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" FooterStyle-Wrap="false"/>
                                    <asp:BoundField DataField="總價" HeaderText="<%$ Resources:WebResources, TotalPrice %>" ReadOnly="true" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" FooterStyle-Wrap="false"/>
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

                <div align="right">
                    <asp:Label ID="Label13" runat="server" ></asp:Label>
                
                    <!--最低訂單金額-->
                    <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, MinimumOrderAmount %>"></asp:Literal>
                    <asp:Label ID="Label5" runat="server" Text="：500元"></asp:Label>
                    <asp:Label ID="Label7" runat="server" Text="　　　"></asp:Label>
       
                    <!--訂單總價-->
                    <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, OrderAmount %>"></asp:Literal>
                    <asp:Label ID="Label6" runat="server" Text="："></asp:Label>
                    <asp:Label ID="Order_TotalPrice" runat="server" Text="0" ></asp:Label>
                    <asp:Label ID="Label8" runat="server" Text="元"></asp:Label>
                </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAddNew" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>



            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
                <asp:Button ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Discard %>" />
                <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
