<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON02.aspx.cs" Inherits="VSS_CON02_Default" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 
    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window","width:500px;height:450px");
        }            
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server"></asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" class="titlef">
                <tr>
                    <td align="left" style="width: 79%">
                        <!--外部廠商維護作業(總部)-->
                        <asp:Literal ID="Literal1" runat="server" Text="外部廠商維護作業(總部)"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button11" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClientClick="openwindow('con01_1.aspx');return false;" />
                        <asp:Button ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='CON01.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            
            <table>
                <tr>
                    <td>
                        <!--廠商類別-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>"></asp:Literal>：
                    </td>
                    <td>                                                                             
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                            AppendDataBoundItems="True">
                            <asp:ListItem Text="<%$ Resources:WebResources, DropDownListPrompt %>"></asp:ListItem>
                            <asp:ListItem>寄售廠商</asp:ListItem>                                    
                            <asp:ListItem>外部廠商</asp:ListItem>                                    
                        </asp:DropDownList>       
                    </td>           
                    <td align="right">
                        <!--負責人-->
                        <asp:Literal ID="Literal34" runat="server" Text="<%$ Resources:WebResources, FETOwner %>" ></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtFETOwner" runat="server" Width="80" OnTextChanged="txtFETOwner_TextChanged"></asp:TextBox><asp:Button ID="btnChooseEmp" runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                        部門：<asp:Label ID="lblDepartment" runat="server"></asp:Label> 
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <uc1:PopupWindow ID="PopupWindow1" runat="server"
                                        Name="Employee" 
                                        PopupButtonID="btnChooseEmp" 
                                        TargetControlID="txtFETOwner"   
                                        OnOkScript="onOk"                                 
                                        Width="500" Height="450"                       
                                        NavigateUrl="~/VSS/LOG/SearchEmpNum.aspx" />      
                        <script type="text/javascript">
                            function onOk() {
                                __doPostBack('<%= txtFETOwner.UniqueID %>', '');
                            }
                        </script>                 
                    </td>                    
                    <td>
                        <!--狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Text="00-未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!--廠商編號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lblSupplierNo" runat="server"></asp:Label>
                    </td> 
                    <td align="right">  
                        <!--廠商代碼-->
                        <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, SupplierCode %>"></asp:Literal>：                     
                    </td>
                    <td>
                        <asp:TextBox ID="txtSupplierCode" runat="server" Width="40"></asp:TextBox>                        
                    </td> 
                    <td>
                        <!--更新日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="2010/07/01 22:00"></asp:Label>
                    </td>                                       
                </tr>
                <tr>
                    <td>
                        <!--廠商名稱-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtSupplierName" runat="server" Width="98%"></asp:TextBox>
                    </td>
                     
                    <td>
                        <!--維護人員-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!--公司地址-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, CompanyAddress %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtAddress" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td>
                        <!--聯絡人-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Contact %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtContact" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        <!--聯絡電話-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <!--合作起訖日-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, CooperationDateRange %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                         <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" Width="100" />
                            &nbsp;<asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" Width="100" />                            
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <!--合約號碼-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ContractNo %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtContractNo" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        <!--結算日-->
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, SettlementDate %>"></asp:Literal>：
                    </td>
                    <td>
                          <asp:RadioButton ID="RadioButton1" runat="server" GroupName="DATE" 
                                Text="月底日" AutoPostBack="True" 
                                oncheckedchanged="RadioButton1_CheckedChanged" />
                        
                          <asp:RadioButton ID="RadioButton2" runat="server" GroupName="DATE" 
                                AutoPostBack="True" 
                                oncheckedchanged="RadioButton1_CheckedChanged" />
                           <asp:TextBox ID="TextBox3" runat="server" Width="50px" Enabled="false" ></asp:TextBox>日   
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>                  
                    <td>
                        <!--統一編號-->
                         <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtUnifiedBusinessNo" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <!--負責人-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Owner %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtOwner" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        <!--電話號碼-->
                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, Telephone %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtOwnerPhone" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <!--傳真-->
                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, Fax %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <!--電子信箱-->
                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Email %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtEmail" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="6"></td>                    
                </tr>
                <tr>
                    <td>
                        <!--總金額底限-->
                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, MinimumTotalAmount %>"></asp:Literal>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtMinAmt" runat="server"></asp:TextBox>
                    </td>
                    <td><asp:CheckBox ID="cbMin" runat="server" Text="使用" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6"></td>                    
                </tr>
                <tr>
                    <td>
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
                    <td>
                        <!--備註-->
                        <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="TextBox19" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                Width="100%" Visible="False" CssClass="visoft__tab_xpie7">
                <asp:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                        <span><!--佣金比率設定-->
                        <asp:Literal ID="Literal33" runat="server" Text="<%$ Resources:WebResources, CommissionRateSetting %>"></asp:Literal>                                
                        </span>
                    </HeaderTemplate>
                    <ContentTemplate>                           
                        <div class="SubEditBlock">
                            <div class="SubEditCommand">
                                <asp:Button ID="btnAdd1" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd1_Click" /><asp:Button
                                    ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                            </div>
                            <div class="GridScrollBar">
                                <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                                    OnRowUpdating="gvMaster_RowUpdating">
                                    <EmptyDataTemplate>
                                        <tr>
                                            <th scope="col">&nbsp;</th>
                                            <th scope="col">&nbsp;</th>
                                            <th scope="col">
                                                <!--佣金比率-->
                                                <asp:Literal ID="Literal33" runat="server" 
                                                    Text="<%$ Resources:WebResources, CommissionRate %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--起始月份-->
                                                <asp:Literal ID="Literal34" runat="server" 
                                                    Text="<%$ Resources:WebResources, StartMonth %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--結束月份-->
                                                <asp:Literal ID="Literal35" runat="server" 
                                                    Text="<%$ Resources:WebResources, EndMonth %>"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr id="trEmptyData" runat="server">
                                            <td class="tdEmptyData" colspan="5">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal52" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal> 
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckAll" runat="server" CssClass="chk1" onclick="javascript:if(this.checked){$('.chk1').checkCheckboxes();}else{$('.chk1').unCheckCheckboxes();}" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckItem" runat="server" CssClass="chk1" />
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                                    CommandName="Edit" Text="<%$ Resources:WebResources, Edit %>" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                                                    CommandName="Update" Text="<%$ Resources:WebResources, Save %>" />
                                                &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                                    CommandName="Cancel" Text="<%$ Resources:WebResources, Cancel %>" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel1_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel1_Click" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, CommissionRate %>" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCommissionRate" runat="server" Text='<%# Bind("佣金比率") %>' Width="80px"></asp:TextBox>%
                                            </EditItemTemplate>
                                            <ItemTemplate >
                                                <asp:Label ID="lblCommissionRate" runat="server" Text='<%# Bind("佣金比率", "{0:N}%") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCommissionRate" runat="server" Width="80"></asp:TextBox>%
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="<%$ Resources:WebResources, StartMonth %>" ItemStyle-Wrap="false">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtStartMonth" runat="server" Text='<%# Bind("起始月份") %>' Width="80px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate >
                                                <asp:Label ID="lblStartMonth" runat="server" Text='<%# Bind("起始月份") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtStartMonth" runat="server" Width="80"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                                                                
                                         <asp:TemplateField HeaderText="<%$ Resources:WebResources, EndMonth %>" ItemStyle-Wrap="false">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEndMonth" runat="server" Text='<%# Bind("結束月份") %>' Width="80px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate >
                                                <asp:Label ID="lblEndMonth" runat="server" Text='<%# Bind("結束月份") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtEndMonth" runat="server" Width="80"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>                                                                                                                        
                                    </Columns>
                                </cc1:ExGridView>
                            </div>
                        </div>
                            
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server">
                    <HeaderTemplate>
                        <span><!--合作店組設定-->
                        <asp:Literal ID="Literal36" runat="server" Text="<%$ Resources:WebResources, CooperationStoreSettings %>"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td class="tdval">
                                    <table>
                                        <tr>
                                            <td class="tdcen">
                                                <!--未選擇-->
                                                <asp:Literal ID="Literal37" runat="server" Text="<%$ Resources:WebResources, Nonselect %>"></asp:Literal>
                                            </td>
                                            <td class="tdcen">
                                            </td>
                                            <td class="tdcen">
                                                <!--已選擇-->
                                                <asp:Literal ID="Literal38" runat="server" Text="<%$ Resources:WebResources, Selected %>"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdcen">                                                    
                                                <asp:DropDownList ID="ddlSubZone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubZone_SelectedIndexChanged">
                                                    <asp:ListItem Text="<%$ Resources:WebResources, DropDownListPrompt %>"></asp:ListItem>
                                                    <asp:ListItem Value="北">北一區</asp:ListItem>
                                                    <asp:ListItem Value="中">中一區</asp:ListItem>
                                                    <asp:ListItem Value="南">南一區</asp:ListItem>
                                                </asp:DropDownList>                                                      
                                            </td>
                                            <td class="tdcen">
                                            </td>
                                            <td class="tdcen">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdListBox" rowspan="5">                                                   
                                                <asp:ListBox ID="ListBox1" runat="server" Height="327px" SelectionMode="Multiple"
                                                    Width="259px"></asp:ListBox>                                                        
                                            </td>
                                            <td class="tdBtn">
                                            </td>
                                            <td rowspan="5" class="tdListBox">                                             
                                                <asp:ListBox ID="ListBox2" runat="server" Height="327px" SelectionMode="Multiple"
                                                    Width="259px"></asp:ListBox>                                                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdBtn">                                           
                                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/next.png" OnClick="btnAdd_Click" />                                                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdBtn">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdBtn">                                                
                                                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/previous.png" OnClick="btnBack_Click" />          
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
                </asp:TabPanel>
            </asp:TabContainer>
                                
            <asp:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" Style="text-align: left"
                Width="100%" Visible="False" CssClass="visoft__tab_xpie7">
                <asp:TabPanel ID="TabPanel3" runat="server">
                    <HeaderTemplate>
                        <span><!--總額抽成-->
                         <asp:Literal ID="Literal39" runat="server" Text="<%$ Resources:WebResources, Prorate %>"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>
            
                                <div class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd2_Click" /><asp:Button
                                            ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                    </div>
                                    <div class="GridScrollBar">
                                        <cc1:ExGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                            OnRowUpdating="GridView1_RowUpdating">
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <th scope="col">&nbsp;</th>
                                                    <th scope="col">&nbsp;</th>
                                                    <th scope="col">
                                                        <!--佣金比率-->
                                                        <asp:Literal ID="Literal39" runat="server" Text="<%$ Resources:WebResources, CommissionRate %>"></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--起始日期-->
                                                        <asp:Literal ID="Literal40" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--結束日期-->
                                                        <asp:Literal ID="Literal41" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>
                                                    </th>
                                                </tr>
                                                <tr id="trEmptyData" runat="server">
                                                    <td colspan="5" class="tdEmptyData">
                                                        <!--請點選新增按鍵增加資料-->
                                                        <asp:Literal ID="Literal51" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckAll" runat="server" CssClass="chk2" onclick="javascript:if(this.checked){$('.chk2').checkCheckboxes();}else{$('.chk2').unCheckCheckboxes();}" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckItem" runat="server" CssClass="chk2" />
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                                            CommandName="Edit" Text="<%$ Resources:WebResources, Edit %>" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                                                            CommandName="Update" Text="<%$ Resources:WebResources, Save %>" />
                                                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                                            CommandName="Cancel" Text="<%$ Resources:WebResources, Cancel %>" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel2_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel2_Click" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:WebResources, CommissionRate %>" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCommissionRate" runat="server" Text='<%# Bind("佣金比率") %>' Width="80px"></asp:TextBox>%
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblCommissionRate" runat="server" Text='<%# Bind("佣金比率", "{0:N}%") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtCommissionRate" runat="server" Width="80"></asp:TextBox>%
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:WebResources, StartMonth %>" ItemStyle-Wrap="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Bind("起始日期") %>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("起始日期") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="80"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                                                        
                                                 <asp:TemplateField HeaderText="<%$ Resources:WebResources, EndMonth %>" ItemStyle-Wrap="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Bind("結束日期") %>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("結束日期") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtEndDate" runat="server" Width="80"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>                                                                                                     
                                            </Columns>
                                        </cc1:ExGridView>
                                    </div>
                                </div>
                            </ContentTemplate>                          
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server">
                    <HeaderTemplate>
                        <span><!--金額級距-->
                        <asp:Literal ID="Literal42" runat="server" Text="<%$ Resources:WebResources, Bracket %>"></asp:Literal>
                        </span>
                    </HeaderTemplate>
                    <ContentTemplate>
                 
                                <div class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd3_Click" /><asp:Button
                                            ID="Button6" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                    </div>
                                    <div class="GridScrollBar">
                                        <cc1:ExGridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowEditing="GridView2_RowEditing"
                                            OnRowUpdating="GridView2_RowUpdating">
                                            <EmptyDataTemplate>
                                                <tr>                                                            
                                                    <th scope="col">
                                                        <!--級距項次-->
                                                        <asp:Literal ID="Literal42" runat="server" Text="<%$ Resources:WebResources, BracketItems %>"></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--起-金額級距-->
                                                        <asp:Literal ID="Literal43" runat="server" Text="<%$ Resources:WebResources, BracketStart %>"></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--訖-金額級距-->
                                                        <asp:Literal ID="Literal44" runat="server" Text="<%$ Resources:WebResources, BracketEnd %>"></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--佣金比率-->
                                                        <asp:Literal ID="Literal45" runat="server" Text="<%$ Resources:WebResources, CommissionRate %>"></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--開始日期-->
                                                         <asp:Literal ID="Literal46" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--結束日期-->
                                                         <asp:Literal ID="Literal47" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>
                                                    </th>
                                                </tr>
                                                <tr id="trEmptyData" runat="server">
                                                    <td colspan="6" class="tdEmptyData">
                                                        <!--請點選新增按鍵增加資料-->
                                                        <asp:Literal ID="Literal51" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckAll" runat="server" CssClass="chk3" onclick="javascript:if(this.checked){$('.chk3').checkCheckboxes();}else{$('.chk3').unCheckCheckboxes();}" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckItem" runat="server" CssClass="chk3" />
                                                    </ItemTemplate>                                                                                                        
                                                </asp:TemplateField>
                                                 <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                                            CommandName="Edit" Text="<%$ Resources:WebResources, Edit %>" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                                                            CommandName="Update" Text="<%$ Resources:WebResources, Save %>" />
                                                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                                            CommandName="Cancel" Text="<%$ Resources:WebResources, Cancel %>" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel3_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel3_Click" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>                                                
                                                <asp:BoundField DataField="級距項次" HeaderText="級距項次" ReadOnly="true" />
                                                                                                                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, BracketStart %>" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtBracketStart" runat="server" Text='<%# Bind("起金額級距") %>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblBracketStart" runat="server" Text='<%# Bind("起金額級距") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtBracketStart" runat="server" Width="80"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, BracketEnd %>" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtBracketEnd" runat="server" Text='<%# Bind("訖金額級距") %>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblBracketEnd" runat="server" Text='<%# Bind("訖金額級距") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtBracketEnd" runat="server" Width="80"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, CommissionRate %>" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCommissionRate" runat="server" Text='<%# Bind("佣金比率") %>' Width="40px"></asp:TextBox>%
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblCommissionRate" runat="server" Text='<%# Bind("佣金比率", "{0:N}%") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtCommissionRate" runat="server" Width="40"></asp:TextBox>%
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, StartDate %>">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Bind("開始日期") %>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("開始日期", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="80"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, EndDate %>">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Bind("結束日期") %>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("結束日期", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtEndDate" runat="server" Width="80"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>                                                                                                                                               
                                            </Columns>
                                        </cc1:ExGridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                      
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel5" runat="server">
                    <HeaderTemplate>
                        <span><!--商品編號設定-->
                        <asp:Literal ID="Literal47" runat="server" Text="<%$ Resources:WebResources, ProductCodeAssignment %>"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>
                      
                                <div class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <asp:Button ID="Button7" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd4_Click" /><asp:Button
                                            ID="Button8" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                    </div>
                                    <div class="GridScrollBar">
                                        <cc1:ExGridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            OnRowCancelingEdit="GridView3_RowCancelingEdit" OnRowEditing="GridView3_RowEditing"
                                            OnRowUpdating="GridView3_RowUpdating">
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <th scope="col">
                                                        <!--商品編號-->
                                                        <asp:Literal ID="Literal47" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--商品名稱-->
                                                        <asp:Literal ID="Literal48" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                                    </th>                                                            
                                                </tr>
                                                <tr id="trEmptyData" runat="server">
                                                    <td colspan="2" class="tdEmptyData">
                                                        <!--請點選新增按鍵增加資料-->
                                                        <asp:Literal ID="Literal51" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckAll" runat="server" CssClass="chk4" onclick="javascript:if(this.checked){$('.chk4').checkCheckboxes();}else{$('.chk4').unCheckCheckboxes();}" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckItem" runat="server" CssClass="chk4" />
                                                    </ItemTemplate>                                                     
                                                </asp:TemplateField>
                                                 <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                                            CommandName="Edit" Text="<%$ Resources:WebResources, Edit %>" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                                                            CommandName="Update" Text="<%$ Resources:WebResources, Save %>" />
                                                        &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                                            CommandName="Cancel" Text="<%$ Resources:WebResources, Cancel %>" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel4_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel4_Click" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtProductCode" runat="server" Text='<%# Bind("商品編號") %>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblProductCode" runat="server" Text='<%# Bind("商品編號", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtProductCode" runat="server" Width="80"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>       
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductName %>">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtProductName" runat="server" Text='<%# Bind("商品名稱") %>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtProductName" runat="server" Width="80"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>       
                                                                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, AccountingSubject %>">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAccountingSubject" runat="server" Text='<%# Bind("會計科目") %>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblAccountingSubject" runat="server" Text='<%# Bind("會計科目") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAccountingSubject" runat="server" Width="80"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>       
                                               
                                            </Columns>
                                        </cc1:ExGridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        
                </asp:TabPanel>
             <asp:TabPanel ID="TabPanel6" runat="server">
                    <HeaderTemplate>
                        <span><!--合作店組設定-->
                        <asp:Literal ID="Literal48" runat="server" Text="<%$ Resources:WebResources, CooperationStoreSettings %>"></asp:Literal>                                                                
                        </span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td class="tdval">
                                    <table>
                                        <tr>
                                            <td class="tdcen">
                                                <!--未選擇-->
                                                <asp:Literal ID="Literal49" runat="server" Text="<%$ Resources:WebResources, Nonselect %>"></asp:Literal>
                                            </td>
                                            <td class="tdcen">
                                            </td>
                                            <td class="tdcen">
                                                <!--已選擇-->
                                                <asp:Literal ID="Literal50" runat="server" Text="<%$ Resources:WebResources, Selected %>"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdcen">                                          
                                                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubZone_SelectedIndexChanged2">
                                                    <asp:ListItem Text="<%$ Resources:WebResources, DropDownListPrompt %>"></asp:ListItem>
                                                    <asp:ListItem Value="北">北一區</asp:ListItem>
                                                    <asp:ListItem Value="中">中一區</asp:ListItem>
                                                    <asp:ListItem Value="南">南一區</asp:ListItem>
                                                </asp:DropDownList>                                                       
                                            </td>
                                            <td class="tdcen">
                                            </td>
                                            <td class="tdcen">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdListBox" rowspan="5">                                            
                                                <asp:ListBox ID="ListBox3" runat="server" Height="327px" SelectionMode="Multiple"
                                                    Width="259px"></asp:ListBox>
                                            </td>
                                            <td class="tdBtn">
                                            </td>
                                            <td rowspan="5" class="tdListBox">                                                   
                                                <asp:ListBox ID="ListBox4" runat="server" Height="327px" SelectionMode="Multiple"
                                                    Width="259px"></asp:ListBox>                                                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdBtn">                                               
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/next.png" OnClick="btnAdd_Click2" />                                                       
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdBtn">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdBtn">                                                    
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/previous.png" OnClick="btnBack_Click2" />                                         
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
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel7" runat="server">
                    <HeaderTemplate>
                        <span><!--信用卡手續費-->
                        <asp:Literal ID="Literal16" runat="server" Text="信用卡手續費"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>                          
                        <div class="SubEditBlock">
                            <div class="SubEditCommand">
                                <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd5_Click" /><asp:Button
                                    ID="Button10" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                            </div>
                            <div class="GridScrollBar">
                                <cc1:ExGridView ID="GridView4" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    OnRowCancelingEdit="GridView4_RowCancelingEdit" OnRowEditing="GridView4_RowEditing"
                                    OnRowUpdating="GridView4_RowUpdating">
                                    <EmptyDataTemplate>
                                        <tr>
                                            <th scope="col">
                                                <!--項次-->
                                                <asp:Literal ID="Literal47" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                            </th>
                                             <th scope="col">
                                                <!--信用卡別-->
                                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, TypeOfCreditCard%>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--手續費-->
                                                <asp:Literal ID="Literal48" runat="server" Text="<%$ Resources:WebResources, ServiceCharges %>"></asp:Literal>
                                            </th>                                                            
                                        </tr>
                                        <tr id="trEmptyData" runat="server">
                                            <td colspan="2" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal51" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckAll" runat="server" CssClass="chk5" onclick="javascript:if(this.checked){$('.chk5').checkCheckboxes();}else{$('.chk5').unCheckCheckboxes();}" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckItem" runat="server" CssClass="chk5" />
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                                    CommandName="Edit" Text="<%$ Resources:WebResources, Edit %>" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                                                    CommandName="Update" Text="<%$ Resources:WebResources, Save %>" />
                                                &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                                    CommandName="Cancel" Text="<%$ Resources:WebResources, Cancel %>" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel5_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel5_Click" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="<%$ Resources:WebResources, Items %>">
                                            <EditItemTemplate>
                                              <asp:Label ID="Label13" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                              <asp:Label ID="Label11" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="<%$ Resources:WebResources, TypeOfCreditCard %>" >
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlCreditType" runat="server" >
                                                <asp:ListItem>VISA</asp:ListItem>
                                                <asp:ListItem>MASTER</asp:ListItem>
                                                <asp:ListItem>AE</asp:ListItem>
                                                <asp:ListItem>JCB</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("信用卡別") %>'></asp:Label>
                                        </ItemTemplate>  
                                        <FooterTemplate>
                                             <asp:DropDownList ID="ddlCreditType" runat="server" >
                                                <asp:ListItem Text="<%$ Resources:WebResources, DropDownListPrompt %>"></asp:ListItem>
                                                <asp:ListItem>VISA</asp:ListItem>
                                                <asp:ListItem>MASTER</asp:ListItem>
                                                <asp:ListItem>AE</asp:ListItem>
                                                <asp:ListItem>JCB</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>                                                 
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="<%$ Resources:WebResources, ServiceCharges %>" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                              <asp:TextBox ID="lblFee" runat="server" Text='<%# Bind("手續費") %>' Width="80"></asp:TextBox>%
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                              <asp:Label ID="lblFee" runat="server" Text='<%# Eval("手續費") %>'></asp:Label>%
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFee" runat="server" Width="80"></asp:TextBox>%
                                            </FooterTemplate>
                                        </asp:TemplateField>                                                    
                                    </Columns>
                                </cc1:ExGridView>
                            </div>
                        </div>
                    </ContentTemplate>                           
                </asp:TabPanel>
            </asp:TabContainer> 
            </ContentTemplate>
            </asp:UpdatePanel>                   
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
    </div>
    </form>
</body>
</html>
