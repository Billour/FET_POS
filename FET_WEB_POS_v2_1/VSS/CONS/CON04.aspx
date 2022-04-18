<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON04.aspx.cs" Inherits="VSS_CONS_CON04" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window", "width:500px;height:450px,resizable=1,scrollbars=1'");
        }            
    </script>  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品維護作業(總部)-->
                    <asp:Literal ID="Literal110" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductMaintenanceHQ %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" AutoPostBack="false" ClientSideEvents-Click="function(){document.location='CON03.aspx';return false;}" />
                </td>
            </tr>
        </table>
    
            
    </div>
   
        <div class="criteria">
            <table>                
                <tr>
                    <td align="right">
                        <!--廠商編號-->
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ddlSupplierNo" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="AC1" />
                                <dx:ListEditItem Text="AC2" />
                            </Items>
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">
                        <!--狀態-->
                        <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Status %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblStatus" runat="server" Text="00 未存檔"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--商品編號-->
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtProductCode" runat="server">
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">
                        <!--日期-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Date %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="Label3" runat="server" Text="10/07/12 15:00"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--商品名稱-->
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtProductName" runat="server">
                            <ValidationSettings CausesValidation="false">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="right">
                        <!--人員-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Staff %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="Label4" runat="server" Text="64591 李家駿"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--商品類別-->
                        <dx:ASPxLabel ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ddlProductCategory" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="3G Handset" />
                                <dx:ListEditItem Text="SIM Card" />
                                <dx:ListEditItem Text="3G Accessory" />
                                <dx:ListEditItem Text="On Line Recharge" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <!--上下架日期-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, SupportDateRange %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td><span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="SupportDateRangeFrom" runat="server">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit></td>
                                <td><dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="SupportDateRangeTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <!--停止訂購日期-->
                        <dx:ASPxLabel ID="Literal9" runat="server" Text="<%$ Resources:WebResources, OrderEndDate %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right">
                        <!--會計科目-->
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal25" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>"></dx:ASPxLabel>：
                    </td>
                    <td colspan="3">                                                
                        <table>
                            <tr>
                                <td><dx:ASPxLabel ID="Literal26" runat="server" Text="<%$ Resources:WebResources, Subject1 %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxLabel ID="Literal27" runat="server" Text="<%$ Resources:WebResources, Subject2 %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxLabel ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Subject3 %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxLabel ID="Literal29" runat="server" Text="<%$ Resources:WebResources, Subject4 %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxLabel ID="Literal30" runat="server" Text="<%$ Resources:WebResources, Subject5 %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxLabel ID="Literal31" runat="server" Text="<%$ Resources:WebResources, Subject6 %>"></dx:ASPxLabel></td>
                            </tr>
                            <tr>
                                <td><dx:ASPxTextBox ID="txtAcct1" runat="server" Width="40">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox></td>
                                <td><dx:ASPxTextBox ID="txtAcct2" runat="server" Width="40">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox></td>
                                <td><dx:ASPxTextBox ID="txtAcct3" runat="server" Width="50">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox></td>
                                <td><dx:ASPxTextBox ID="txtAcct4" runat="server" Width="50">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox></td>
                                <td><dx:ASPxTextBox ID="txtAcct5" runat="server" Width="40">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox></td>
                                <td><dx:ASPxTextBox ID="txtAcct6" runat="server" Width="40">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right">
                        <!--單位-->
                        <dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Unit %>"></dx:ASPxLabel>：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtUnit" runat="server" Width="40"></dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
            <TabPages>
                <dx:TabPage>
                <TabTemplate>
                <table width="70px">
                <tr><td>
                <span style="color: Red">*</span>
                </td><td><dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, CommissionRate %>"></dx:ASPxLabel>
               </td> </tr></table></TabTemplate>
                    <ContentCollection>
                        <dx:ContentControl>

                        <div >
                            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="佣金比率"
                            Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                            EnableRowsCache="False" onrowinserting="gvMaster_RowInserting" 
                                onrowupdating="gvMaster_RowUpdating">
                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                        <HeaderTemplate>
                                            <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button">
                                        <EditButton Visible="True">
                                        </EditButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="佣金比率" 
                                        Caption="<%$ Resources:WebResources, CommissionRate %>" VisibleIndex="2">
                                        <PropertiesTextEdit DisplayFormatString="{0:N}%">
                                        </PropertiesTextEdit>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCommissionRate" runat="server" Text='<%# Eval("[佣金比率]") %>'></asp:TextBox>%
                                        </EditItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="起始月份" 
                                        Caption="<%$ Resources:WebResources, StartMonthOfTheFirstDay %>" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="結束月份" 
                                        Caption="結束月份" VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table align="left">
                                            <tr>
                                                <td><dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click" /></td>
                                                <td><dx:ASPxButton ID="Button5" runat="server" Text="<%$ Resources:WebResources, Delete %>" /></td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                </Templates>
                                <SettingsPager PageSize="5" />
                                <SettingsEditing Mode="Inline" />
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                            </cc:ASPxGridView>
                        </div>

                        </dx:ContentControl>
                    </ContentCollection>

                </dx:TabPage>
                
                <dx:TabPage Text="<%$ Resources:WebResources, ProductAmount %>">
                    <ContentCollection>
                        <dx:ContentControl>

                        <div >
                            <cc:ASPxGridView ID="GridView1" ClientInstanceName="GridView1" runat="server" 
                            Width="100%" AutoGenerateColumns="False"
                            EnableRowsCache="False">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                                    <dx:GridViewDataTextColumn FieldName="生效日期" Caption="<%$ Resources:WebResources, EffectiveDate %>" />
                                    <dx:GridViewDataTextColumn FieldName="失效日期" Caption="<%$ Resources:WebResources, ExpiryDate %>" />
                                    <dx:GridViewDataTextColumn FieldName="商品金額" Caption="<%$ Resources:WebResources, ProductAmount %>" />
                                </Columns>
                                <SettingsPager PageSize="5" />
                                <SettingsEditing Mode="Inline" />
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                        </div>

                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>

        <div class="seperate"></div>

        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td><dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Import %>" ClientSideEvents-Click="function(){openwindow('con04_1.aspx');return false;}" AutoPostBack="false" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>
        </div>
    
    
</asp:Content>