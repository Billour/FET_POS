<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT10.aspx.cs" Inherits="VSS_OPT_OPT10" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
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
        <!--商品主檔設定-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductDataManagement %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--商品類別-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>3G Handset</asp:ListItem>
                            <asp:ListItem>SIM Card</asp:ListItem>
                            <asp:ListItem>3G Accessory</asp:ListItem>
                            <asp:ListItem>On Line Recharge</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--商品狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>已過期</asp:ListItem>
                            <asp:ListItem>未生效</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--檢核IMEI-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, VerifyImei %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>不控管</asp:ListItem>
                            <asp:ListItem>銷售時記錄</asp:ListItem>
                            <asp:ListItem>銷售時確認</asp:ListItem>
                            <asp:ListItem>庫存異動控管</asp:ListItem>
                        </asp:DropDownList>
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

                <div id="Div1" runat="server" class="SubEditBlock">
                
                    <div class="SubEditCommand">
                        <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click"/>
                        <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Import %>" />
                    </div>
                    <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="true" PageSize="5" PagerStyle-HorizontalAlign="Right" AutoGenerateColumns="False" CssClass="mGrid"
                        OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing" OnPageIndexChanging="GridView_PageIndexChanging"
                        OnRowUpdating="gvMaster_RowUpdating">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col" style="width:40px" nowrap="nowrap">
                                        <!--狀態-->
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:80px" nowrap="nowrap">
                                        <!--商品編號-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:80px" nowrap="nowrap">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:80px" nowrap="nowrap">
                                        <!--商品類別-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:40px" nowrap="nowrap">
                                        <!--單位-->
                                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Unit %>"></asp:Literal>
                                    </th>                                    
                                    <th scope="col" style="width:80px" nowrap="nowrap">
                                        <!--單機價格-->
                                        <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, StandAlonePrice %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:100px" nowrap="nowrap">
                                        <!--有效日期(起)-->
                                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, ValidStartDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:100px" nowrap="nowrap">
                                        <!--有效日期(訖)-->
                                        <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, ValidEndDate %>"></asp:Literal>
                                    </th>                                                                                                            
                                    <th scope="col" style="width:80px" nowrap="nowrap">
                                        <!--檢核IMEI-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, VerifyImei %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:80px" nowrap="nowrap">
                                        <!--扣庫存-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ReduceInventory %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:80px" nowrap="nowrap">
                                        <!--自訂價格-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CustomPrice %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:50px" nowrap="nowrap">
                                        <!--科目1-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Subject1 %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:50px" nowrap="nowrap">
                                        <!--科目2-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Subject2 %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:50px" nowrap="nowrap">
                                        <!--科目3-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Subject3 %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:50px" nowrap="nowrap">
                                        <!--科目4-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Subject4 %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:50px" nowrap="nowrap">
                                        <!--科目5-->
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Subject5 %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:50px" nowrap="nowrap">
                                        <!--科目6-->
                                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Subject6 %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:80px" nowrap="nowrap">
                                        <!--更新人員-->
                                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                    </th>
                                    <th scope="col" style="width:80px" nowrap="nowrap">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                </tr>                               
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="21" class="tdEmptyData">
                                        <asp:Literal ID="Literal36" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
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
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, Status %>" >
                                    <EditItemTemplate>
                                        <asp:Label ID="Label231" runat="server" Text='<%# Bind("狀態") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("狀態") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label311" runat="server" Text='<%# Bind("商品編號") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label31" runat="server" Text='<%# Bind("商品編號") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="80px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField ControlStyle-Width="80px" HeaderText="<%$ Resources:WebResources, ProductName %>" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtProduceName" runat="server" Text='<%# Bind("商品名稱") %>' Width="80px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduceName" runat="server" Text='<%# Eval("商品名稱") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtProduceName" runat="server" Width="80px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCategory %>" ControlStyle-Width="140px">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                            <asp:ListItem>-請選擇-</asp:ListItem>
                                            <asp:ListItem>3G Handset</asp:ListItem>
                                            <asp:ListItem>SIM Card</asp:ListItem>
                                            <asp:ListItem>3G Accessory</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("商品類別") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                            <asp:ListItem>-請選擇-</asp:ListItem>
                                            <asp:ListItem>3G Handset</asp:ListItem>
                                            <asp:ListItem>SIM Card</asp:ListItem>
                                            <asp:ListItem>3G Accessory</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                

                                <asp:TemplateField ControlStyle-Width="30px" HeaderText="<%$ Resources:WebResources, Unit %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("單位") %>' Width="30px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("單位") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtUnit" runat="server" Width="30px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ControlStyle-Width="80px" HeaderText="<%$ Resources:WebResources, StandAlonePrice %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStandAlonePrice" runat="server" Text='<%# Bind("單機價格") %>' Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStandAlonePrice" runat="server" Text='<%# Eval("單機價格") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtStandAlonePrice" runat="server" Width="50px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField ControlStyle-Width="100px" HeaderText="<%$ Resources:WebResources, ValidStartDate %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtValidStartDate" runat="server" Text='<%# Bind("有效日期1") %>' Width="80px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValidStartDate" runat="server" Text='<%# Eval("有效日期1") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtValidStartDate" runat="server" Width="80px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="100px" HeaderText="<%$ Resources:WebResources, ValidEndDate %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtValidEndDate" runat="server" Text='<%# Bind("有效日期2") %>' Width="80px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValidEndDate" runat="server" Text='<%# Eval("有效日期2") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtValidEndDate" runat="server" Width="80px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="扣庫存">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox102" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox101" runat="server" Enabled="false" Checked="true" />
                                    </ItemTemplate>
                                    <ControlStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, VerifyImei %>" ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownList111" runat="server">
                                            <asp:ListItem>-請選擇-</asp:ListItem>
                                            <asp:ListItem>不控管</asp:ListItem>
                                            <asp:ListItem>銷售時記錄</asp:ListItem>
                                            <asp:ListItem>銷售時確認</asp:ListItem>
                                            <asp:ListItem>庫存異動控管</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label91" runat="server" Text='<%# Bind("檢核IMEI") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="DropDownList111" runat="server">
                                            <asp:ListItem>-請選擇-</asp:ListItem>
                                            <asp:ListItem>不控管</asp:ListItem>
                                            <asp:ListItem>銷售時記錄</asp:ListItem>
                                            <asp:ListItem>銷售時確認</asp:ListItem>
                                            <asp:ListItem>庫存異動控管</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, CustomPrice %>">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" />
                                    </ItemTemplate>
                                    <ControlStyle Width="80px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField ControlStyle-Width="50px" HeaderText="<%$ Resources:WebResources, Subject1 %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSubject1" runat="server" Text='<%# Bind("科目1") %>' Width="35px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubject1" runat="server" Text='<%# Eval("科目1") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtSubject1" runat="server" Width="35px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50px" HeaderText="<%$ Resources:WebResources, Subject2 %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSubject2" runat="server" Text='<%# Bind("科目2") %>' Width="35px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubject2" runat="server" Text='<%# Eval("科目2") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtSubject2" runat="server" Width="35px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50px" HeaderText="<%$ Resources:WebResources, Subject3 %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSubject3" runat="server" Text='<%# Bind("科目3") %>' Width="35px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubject3" runat="server" Text='<%# Eval("科目3") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtSubject3" runat="server" Width="35px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50px" HeaderText="<%$ Resources:WebResources, Subject4 %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSubject4" runat="server" Text='<%# Bind("科目4") %>' Width="35px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubject4" runat="server" Text='<%# Eval("科目4") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtSubject4" runat="server" Width="35px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50px" HeaderText="<%$ Resources:WebResources, Subject5 %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSubject5" runat="server" Text='<%# Bind("科目5") %>' Width="35px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubject5" runat="server" Text='<%# Eval("科目5") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtSubject5" runat="server" Width="35px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50px" HeaderText="<%$ Resources:WebResources, Subject6 %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSubject6" runat="server" Text='<%# Bind("科目6") %>' Width="35px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubject6" runat="server" Text='<%# Eval("科目6") %>'></asp:Label>
                                    </ItemTemplate>                        
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtSubject6" runat="server" Width="35px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                </asp:BoundField>
                                <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" >
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

    </div>
    </form>
</body>
</html>
