<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON04.aspx.cs" Inherits="VSS_CON04_CON04" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 
    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window", "width:500px;height:450px,resizable=1,scrollbars=1'");
        }            
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
   <div class="func">
    <div>
        <table width="100%" class="titlef">
            <tr>
                <td align="left" style="width: 79%">
                    <!--寄銷商品維護作業(總部)-->
                    <asp:Literal ID="Literal20" runat="server" Text="寄銷商品維護作業(總部)"></asp:Literal>
                </td>
                <td align="right">
                    <asp:Button ID="Button6" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='CON03.aspx';return false;" />
                </td>
            </tr>
        </table>
    </div>
   
        <div class="criteria">
            <table>                
                <tr>
                    <td align="right">
                        <!--廠商編號-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSupplierNo" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>AC1</asp:ListItem>
                            <asp:ListItem>AC2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">
                        <!--狀態-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Text="00 未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--商品編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductCode" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">
                        <!--日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Date %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">
                        <!--人員-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Staff %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--商品類別-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductCategory" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>3G Handset</asp:ListItem>
                            <asp:ListItem>SIM Card</asp:ListItem>
                            <asp:ListItem>3G Accessory</asp:ListItem>
                            <asp:ListItem>On Line Recharge</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <!--上下架日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, SupportDateRange %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox
                            ID="PostbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox
                            ID="PostbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <!--停止訂購日期-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, OrderEndDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox3" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right">
                        <!--會計科目-->
                        <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>"></asp:Literal>：
                    </td>
                    <td colspan="3">                                                
                        <table>
                            <tr>
                                <td><asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, Subject1 %>"></asp:Literal></td>
                                <td><asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, Subject2 %>"></asp:Literal></td>
                                <td><asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Subject3 %>"></asp:Literal></td>
                                <td><asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, Subject4 %>"></asp:Literal></td>
                                <td><asp:Literal ID="Literal30" runat="server" Text="<%$ Resources:WebResources, Subject5 %>"></asp:Literal></td>
                                <td><asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:WebResources, Subject6 %>"></asp:Literal></td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="txtAcct1" runat="server" Width="40"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtAcct2" runat="server" Width="40"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtAcct3" runat="server" Width="50"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtAcct4" runat="server" Width="50"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtAcct5" runat="server" Width="40"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtAcct6" runat="server" Width="40"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right">
                        <!--單位-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Unit %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtUnit" runat="server" Width="40"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
            Width="100%" CssClass="visoft__tab_xpie7">
            <asp:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>
                    <span>
                        <!--佣金比率-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CommissionRate %>"></asp:Literal></span>
                </HeaderTemplate>
                <ContentTemplate>
                        <div class="SubEditCommand">
                            <asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click" />
                            <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                        </div>
                        <div class="GridScrollBar" style="height:auto">
                            <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                                OnRowUpdating="gvMaster_RowUpdating" OnRowDataBound="gvMaster_RowDataBound">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">&nbsp;</th>
                                        <th scope="col">&nbsp;</th>
                                        <th scope="col">
                                            <!--佣金比率-->
                                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CommissionRate %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--起始月份-->
                                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StartMonth %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--結束月份-->
                                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, EndMonth %>"></asp:Literal>
                                        </th>
                                        <tr id="trEmptyData" runat="server">
                                            <td colspan="5" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="CheckAll" runat="server" CssClass="chk" onclick="javascript:if(this.checked){$('.chk').checkCheckboxes();}else{$('.chk').unCheckCheckboxes();}" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckItem" runat="server" CssClass="chk" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                                                Text="<%$ Resources:WebResources, Edit %>" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button ID="Button11" runat="server" CausesValidation="True" CommandName="Update"
                                                Text="<%$ Resources:WebResources, Save %>" />
                                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="<%$ Resources:WebResources, Cancel %>" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                                                       
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, CommissionRate %>" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <EditItemTemplate>                                            
                                            <asp:TextBox ID="txtCommissionRate" runat="server" Text='<%# Bind("佣金比率") %>'></asp:TextBox>%
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommissionRate" runat="server" Text='<%# Bind("佣金比率","{0:N}%") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCommissionRate" runat="server"></asp:TextBox>%
                                        </FooterTemplate>
                                    </asp:TemplateField>            
                                    
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, StartMonth %>">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbStartDate" runat="server" Text='<%# Bind("起始月份") %>' Visible="false"></asp:Label>
                                            <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("起始月份") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("起始月份") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="tbStartDate" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, EndMonth %>">
                                        <EditItemTemplate>                                            
                                            <asp:TextBox ID="txtEndMonth" runat="server" Text='<%# Bind("結束月份") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndMonth" runat="server" Text='<%# Bind("結束月份") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtEndMonth" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>                                                                        
                                </Columns>
                            </cc1:ExGridView>
                        </div>
             
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>
                    <span>
                        <!--商品金額-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductAmount %>"></asp:Literal>
                    </span>
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="GridScrollBar" style="height: auto">
                        <cc1:ExGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--生效日期-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, EffectiveDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--失效日期-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ExpiryDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品金額-->
                                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, ProductAmount %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tdEmptyData">
                                        <!--此無明細資料-->
                                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
                                <asp:BoundField DataField="生效日期" HeaderText="<%$ Resources:WebResources, EffectiveDate %>" />
                                <asp:BoundField DataField="失效日期" HeaderText="<%$ Resources:WebResources, ExpiryDate %>" />
                                <asp:BoundField DataField="商品金額" HeaderText="<%$ Resources:WebResources, ProductAmount %>" />
                            </Columns>
                        </cc1:ExGridView>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Import %>"
                        OnClientClick="openwindow('con04_1.aspx');return false;" />
            <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
    
    </div>
    </form>
</body>
</html>
