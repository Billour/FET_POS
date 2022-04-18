<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON02.aspx.cs" Inherits="VSS_CONS_CON02" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window", "width:500px;height:450px");
        }            
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" class="titlef">
            <tr>
                <td align="left" style="width: 79%">
                    <!--外部廠商維護作業(總部)-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, SupplierInformationMaintenanceHQ %>"></asp:Literal>
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxButton ID="importButton" runat="server" Text="<%$ Resources:WebResources, Import %>" AutoPostBack="false" />
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(){document.location='CON01.aspx';return false;}">
                                    </ClientSideEvents>
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <cc:ASPxPopupControl ID="dataImportPopup" runat="server" AllowDragging="True"
         AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/CONS/con01_1.aspx"    
         PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
         Width="700px" Height="600px" FooterText="Try to resize the control using the resize grip or the control's edges"
         HeaderText="<%$ Resources:WebResources, DataImport %>"
         EnableHierarchyRecreation="True" PopupElementID="importButton" LoadingPanelID="lp">      
     </cc:ASPxPopupControl>
    
    <div class="criteria">
        <table border="0">
            <tr>
                <td style="width:13%">
                    <!--廠商類別-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>">
                    </dx:ASPxLabel>：
                </td>
                <td><div style="width:160px;">                    
                    <dx:ASPxComboBox ID="vendorTypeComboBox" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                        AutoPostBack="true">
                        <Items>
                            <dx:ListEditItem Text="<%$ Resources:WebResources, DropDownListPrompt %>" Value="" />
                            <dx:ListEditItem Text="寄售廠商" Value="1" />
                            <dx:ListEditItem Text="外部廠商" Value="2" />                            
                        </Items>
                        <ValidationSettings CausesValidation="false">                                                
                            <RequiredField IsRequired="true" />                                                       
                        </ValidationSettings>                                                
                    </dx:ASPxComboBox>   
                    </div>                                     
                </td>
                <td align="right" style="width:12%">
                    <!--遠傳聯絡窗口-->
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, FETContact %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                               <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="EmployeesPopup" />
                                                        
                               <%-- <table cellpadding="0" cellspacing="0" border="0" style="width:120px">
                                    <tr>
                                        <td><dx:ASPxTextBox ID="txtFETOwner" runat="server" OnTextChanged="txtFETOwner_TextChanged"
                                                Width="100px">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td><dx:ASPxButton ID="btnChooseEmp" runat="server" SkinID="PopupButton" AutoPostBack="false" /></td>
                                    </tr>
                                </table>    --%>                                                        
                            </td>                            
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Department %>"></asp:Literal>：                                
                            </td>
                            <td align="right">
                                <dx:ASPxLabel ID="lblDepartment" runat="server" Width="40px"></dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width:10%">
                    <!--狀態-->
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, Status %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxLabel ID="lblStatus" runat="server" Text="00-未存檔"></dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <!--廠商編號-->
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxLabel ID="lblSupplierNo" runat="server">
                    </dx:ASPxLabel>
                </td>
                <td align="right">
                    <!--廠商代碼-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:WebResources, SupplierCode %>">
                    </dx:ASPxLabel>：
                </td>
                <td align="left">
                    <div style="width:110px;">
                    <dx:ASPxTextBox ID="txtSupplierCode" runat="server">
                        <ValidationSettings CausesValidation="false">                                                
                            <RequiredField IsRequired="true" />                                                       
                        </ValidationSettings>       
                    </dx:ASPxTextBox>
                    </div>
                </td>
                <td>
                    <!--更新日期-->
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="2010/07/01 22:00">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <!--廠商名稱-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="<%$ Resources:WebResources, SupplierName %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="3">
                    <div style="width:220px;">
                    <dx:ASPxTextBox ID="txtSupplierName" runat="server">
                        <ValidationSettings CausesValidation="false">                                                
                            <RequiredField IsRequired="true" />                                                       
                        </ValidationSettings>       
                    </dx:ASPxTextBox>
                    </div>
                </td>
                <td>
                    <!--維護人員-->
                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="12345 王大寶"></dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <!--公司地址-->
                    <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="<%$ Resources:WebResources, CompanyAddress %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="3">
                    <dx:ASPxTextBox ID="txtAddress" runat="server"></dx:ASPxTextBox>
                </td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td>
                    <!--聯絡人-->
                    <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="<%$ Resources:WebResources, Contact  %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtContact" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td align="right">
                    <!--聯絡電話-->
                    <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="<%$ Resources:WebResources, ContactTelephone  %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtPhone" runat="server"></dx:ASPxTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <!--合作起訖日-->
                    <dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, CooperationDateRange %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="2">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="CooperationDateRangeFrom" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="CooperationDateRangeTo" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <!--合約號碼-->
                    <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ContractNo %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtContractNo" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td align="right">
                    <!--結算日-->
                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal15" runat="server" Text="<%$ Resources:WebResources, SettlementDate %>">
                    </dx:ASPxLabel>：
                </td>
                <td>

                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxRadioButtonList ID="cutoffDateRadioButtonList" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="cutoffDateRadioButtonList_SelectedIndexChanged" AutoPostBack="true">
                                    <Items>
                                        <dx:ListEditItem Text="<%$ Resources:WebResources, EndOfDay %>" Value="<%$ Resources:WebResources, EndOfDay %>" />
                                        <dx:ListEditItem Text="" Value="Day" />
                                    </Items>
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxRadioButtonList>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="cutoffDayTextBox" runat="server" Enabled="false">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>日</td>
                        </tr>
                    </table>

                    
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <!--統一編號-->
                    <dx:ASPxLabel ID="Literal17" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>">
                    </dx:ASPxLabel>：
                </td>
                <td nowrap="nowrap">
                    <dx:ASPxTextBox ID="txtUnifiedBusinessNo" runat="server"></dx:ASPxTextBox>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <!--負責人-->
                    <dx:ASPxLabel ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Owner %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtOwner" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td align="right">
                    <!--電話號碼-->
                    <dx:ASPxLabel ID="Literal19" runat="server" Text="<%$ Resources:WebResources, Telephone %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtOwnerPhone" runat="server"></dx:ASPxTextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <!--傳真-->
                    <dx:ASPxLabel ID="Literal20" runat="server" Text="<%$ Resources:WebResources, Fax %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtFax" runat="server"></dx:ASPxTextBox>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <!--電子信箱-->
                    <dx:ASPxLabel ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Email %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="3">
                    <dx:ASPxTextBox ID="txtEmail" runat="server" Width="98%"></dx:ASPxTextBox>
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
                    <dx:ASPxLabel ID="Literal23" runat="server" Text="<%$ Resources:WebResources, MinimumOrderAmount %>">
                    </dx:ASPxLabel>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtMinAmt" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6"></td>
            </tr>
            <tr>
                <td>
                    <!--會計科目-->
                    <dx:ASPxLabel ID="Literal25" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="3">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="Literal26" runat="server" Text="<%$ Resources:WebResources, Subject1 %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="Literal27" runat="server" Text="<%$ Resources:WebResources, Subject2 %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Subject3 %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="Literal29" runat="server" Text="<%$ Resources:WebResources, Subject4 %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="Literal30" runat="server" Text="<%$ Resources:WebResources, Subject5 %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="Literal31" runat="server" Text="<%$ Resources:WebResources, Subject6 %>">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct1" runat="server" Width="40">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct2" runat="server" Width="40">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct3" runat="server" Width="50">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct4" runat="server" Width="50">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct5" runat="server" Width="40">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct6" runat="server" Width="40">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <!--備註-->
                    <dx:ASPxLabel ID="Literal32" runat="server" Text="<%$ Resources:WebResources, Remark %>">
                    </dx:ASPxLabel>：
                </td>
                <td colspan="3" nowrap="nowrap">
                    <dx:ASPxTextBox ID="TextBox19" runat="server" Width="98%" TextMode="MultiLine">
                    </dx:ASPxTextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div>
        <div>
            <dx:ASPxPageControl ID="TabContainer1" runat="server" Width="100%">
                <TabPages>
                    <dx:TabPage Name="TabPanel1" Text='<%$ Resources:WebResources, CommissionRateSetting %>'>
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="佣金比率"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableRowsCache="False"
                                        OnRowInserting="gvMaster_RowInserting" OnRowUpdating="gvMaster_RowUpdating">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="佣金比率" PropertiesTextEdit-DisplayFormatString="{0:N}%"
                                                Caption="<%$ Resources:WebResources, CommissionRate %>" VisibleIndex="2">
                                                <PropertiesTextEdit DisplayFormatString="{0:N}%">
                                                </PropertiesTextEdit>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCommissionRate" runat="server" Text='<%# Eval("[佣金比率]") %>' Width="80px"></asp:TextBox>%
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn FieldName="起始月份" Caption="起始月份(當月的第一天)" VisibleIndex="3">
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="結束月份" Caption="結束月份(當月的最後一天)" VisibleIndex="4">
                                            </dx:GridViewDataDateColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAdd1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd1_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <SettingsPager PageSize="5" />
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel2" Text='<%$ Resources:WebResources, CooperationStoreSettings %>'>
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
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
                                                    <asp:ListItem Value="all">all</asp:ListItem>
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
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
            <dx:ASPxPageControl ID="TabContainer2" runat="server" Width="100%">
                <TabPages>
                    <dx:TabPage Name="TabPanel3" Text="<%$ Resources:WebResources, Prorate %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="GridView1" ClientInstanceName="GridView1" runat="server" KeyFieldName="佣金比率"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableRowsCache="False"
                                        OnRowInserting="GridView1_RowInserting" OnRowUpdating="GridView1_RowUpdating">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="GridView1.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="佣金比率" runat="server" Caption="<%$ Resources:WebResources, CommissionRate %>"
                                                PropertiesTextEdit-DisplayFormatString="{0:N}%">
                                                <PropertiesTextEdit DisplayFormatString="{0:N}%">
                                                </PropertiesTextEdit>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCommissionRate" runat="server" Text='<%# Eval("[佣金比率]") %>' Width="80px"></asp:TextBox>%
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn FieldName="起始日期" runat="server" Caption="<%$ Resources:WebResources, StartMonth %>">
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="結束日期" runat="server" Caption="<%$ Resources:WebResources, EndMonth %>">
                                            </dx:GridViewDataDateColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd2_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <SettingsPager PageSize="5" />
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel4" Text="<%$ Resources:WebResources, Bracket %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="GridView2" ClientInstanceName="GridView2" runat="server" KeyFieldName="級距項次"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableRowsCache="False"
                                        OnRowInserting="GridView2_RowInserting" OnRowUpdating="GridView2_RowUpdating">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="GridView2.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="級距項次" runat="server" Caption="<%$ Resources:WebResources, BracketItems %>">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="起金額級距" runat="server" Caption="<%$ Resources:WebResources, BracketStart %>">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="訖金額級距" runat="server" Caption="<%$ Resources:WebResources, BracketEnd %>">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="佣金比率" runat="server" Caption="<%$ Resources:WebResources, CommissionRate %>"
                                                PropertiesTextEdit-DisplayFormatString="{0:N}%">
                                                <PropertiesTextEdit DisplayFormatString="{0:N}%">
                                                </PropertiesTextEdit>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCommissionRate" runat="server" Text='<%# Eval("[佣金比率]") %>' Width="40px"></asp:TextBox>%
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn FieldName="開始日期" runat="server" Caption="<%$ Resources:WebResources, StartDate %>">
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="結束日期" runat="server" Caption="<%$ Resources:WebResources, EndDate %>">
                                            </dx:GridViewDataDateColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="Button5" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd3_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="Button6" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <SettingsPager PageSize="5" />
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel5" Text="<%$ Resources:WebResources, ProductCodeAssignment %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="GridView3" ClientInstanceName="GridView3" runat="server" KeyFieldName="商品編號"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableRowsCache="False"
                                        OnRowInserting="GridView3_RowInserting" OnRowUpdating="GridView3_RowUpdating">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="GridView3.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="商品編號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="會計科目" runat="server" Caption="<%$ Resources:WebResources, AccountingSubject %>">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn FieldName="開始日期" runat="server" Caption="<%$ Resources:WebResources, StartMonth %>">
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="結束日期" runat="server" Caption="<%$ Resources:WebResources, EndMonth %>">
                                            </dx:GridViewDataDateColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="Button7" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd4_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="Button8" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <SettingsPager PageSize="5" />
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel6" Text="<%$ Resources:WebResources, CooperationStoreSettings %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
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
                                                                <asp:ListItem Value="all">all</asp:ListItem>
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
                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/previous.png"
                                                                OnClick="btnBack_Click2" />
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
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="TabPanel7" Text="<%$ Resources:WebResources, CreditCardFees %>">
                        <ContentCollection>
                            <dx:ContentControl>
                                <div>
                                    <cc:ASPxGridView ID="GridView4" ClientInstanceName="GridView4" runat="server" KeyFieldName="項次"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableRowsCache="False"
                                        OnRowInserting="GridView4_RowInserting" OnRowUpdating="GridView4_RowUpdating">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="GridView4.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="信用卡別" runat="server" Caption="<%$ Resources:WebResources, TypeOfCreditCard %>">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        <dx:ListEditItem Text="<%$ Resources:WebResources, DropDownListPrompt %>" Value="<%$ Resources:WebResources, DropDownListPrompt %>" />
                                                        <dx:ListEditItem Text="VISA" Value="VISA" />
                                                        <dx:ListEditItem Text="MASTER" Value="MASTER" />
                                                        <dx:ListEditItem Text="AE" Value="AE" />
                                                        <dx:ListEditItem Text="JCB" Value="JCB" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="手續費" runat="server" Caption="<%$ Resources:WebResources, ServiceCharges %>"
                                                PropertiesTextEdit-DisplayFormatString="{0:N}%">
                                                <PropertiesTextEdit DisplayFormatString="{0:N}%">
                                                </PropertiesTextEdit>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="lblFee" runat="server" Text='<%# Eval("[手續費]") %>' Width="80"></asp:TextBox>%
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="Button9" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd5_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="Button10" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <SettingsPager PageSize="5" />
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </div>
    </div>
    <div class="seperate"></div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" 
                        OnClick="btnSave_Click" EnableClientSideAPI="false" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                </td>
            </tr>
        </table>
    </div>
<%--    <cc:ASPxPopupControl ID="employeesPopup" SkinID="EmployeesPopup" runat="server" PopupElementID="btnChooseEmp"
        TargetElementID="txtFETOwner" LoadingPanelID="lp">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>--%>
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
</asp:Content>
