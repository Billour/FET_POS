<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="OPT10.aspx.cs" Inherits="VSS_OPT_OPT10" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server"> 
    <div>                
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                   <!--商品主檔設定-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductDataManagement %>"></asp:Literal>
                </td>
                <td align="right">                        
                </td>
            </tr>
        </table>                
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--商品類別-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                        <Items>
                            <dx:ListEditItem Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Text="3G Handset" />
                            <dx:ListEditItem Text="SIM Card" />
                            <dx:ListEditItem Text="3G Accessory" />
                            <dx:ListEditItem Text="On Line Recharge" />
                        </Items>                            
                    </dx:ASPxComboBox>
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
                     <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                        <Items>
                            <dx:ListEditItem Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Text="有效" />
                            <dx:ListEditItem Text="已過期" />
                            <dx:ListEditItem Text="未生效" />                                
                        </Items>
                   </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品編號-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--商品名稱-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server"></dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--檢核IMEI-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, VerifyImei %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox3" runat="server">
                        <Items>
                            <dx:ListEditItem Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Text="不控管" />
                            <dx:ListEditItem Text="銷售時記錄" />
                            <dx:ListEditItem Text="銷售時確認" />
                            <dx:ListEditItem Text="庫存異動控管" />                      
                        </Items>
                    </dx:ASPxComboBox>                        
                </td>
            </tr>
        </table>
    </div>
    
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
        <tr>
        <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
        <td>&nbsp;</td>
        <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
        </tr>
        </table>
    </div>
    <div class="seperate"></div>
    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="商品編號" Width="100%"  Settings-ShowTitlePanel="true"            
        OnPageIndexChanged="grid_PageIndexChanged"
        OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating">                           
        <Columns>                                  
            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                <HeaderTemplate>
                    <input type="checkbox" onclick="grid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button">
                <EditButton Visible="True"></EditButton>
            </dx:GridViewCommandColumn>                                    
            <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>" ReadOnly="true">
                <EditFormSettings Visible="false" />                
            </dx:GridViewDataColumn>                                           
            <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true">
                <EditFormSettings Visible="false" />
            </dx:GridViewDataColumn>            
            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />                
            <dx:GridViewDataComboBoxColumn FieldName="商品類別" Caption="<%$ Resources:WebResources, ProductCategory %>">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="-請選擇-" />
                        <dx:ListEditItem Text="3G Handset" Value="3G Handset" />
                        <dx:ListEditItem Text="SIM Card" Value="SIM Card" />
                        <dx:ListEditItem Text="3G Accessory" Value="3G Accessory" />
                        <dx:ListEditItem Text="On Line Recharge" Value="On Line Recharge" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataColumn FieldName="單位" Caption="<%$ Resources:WebResources, Unit %>" />                
            <dx:GridViewDataColumn FieldName="單機價格" Caption="<%$ Resources:WebResources, StandAlonePrice %>" />                        
            <dx:GridViewDataDateColumn FieldName="有效日期1" Caption="<%$ Resources:WebResources, ValidStartDate %>" />            
            <dx:GridViewDataDateColumn FieldName="有效日期2" Caption="<%$ Resources:WebResources, ValidEndDate %>" />
            <dx:GridViewDataCheckColumn FieldName="扣庫存" Caption="扣庫存" />            
            <dx:GridViewDataComboBoxColumn FieldName="檢核IMEI" Caption="<%$ Resources:WebResources, VerifyImei %>">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="-請選擇-" />
                        <dx:ListEditItem Text="不控管" Value="不控管" />
                        <dx:ListEditItem Text="銷售時記錄" Value="銷售時記錄" />
                        <dx:ListEditItem Text="銷售時確認" Value="銷售時確認" />
                        <dx:ListEditItem Text="庫存異動控管" Value="庫存異動控管" />                        
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>            
            <dx:GridViewDataCheckColumn FieldName="自訂價格" Caption="<%$ Resources:WebResources, CustomPrice %>" />            
            <dx:GridViewDataColumn FieldName="科目1" Caption="<%$ Resources:WebResources, Subject1 %>" />
            <dx:GridViewDataColumn FieldName="科目2" Caption="<%$ Resources:WebResources, Subject2 %>" />
            <dx:GridViewDataColumn FieldName="科目3" Caption="<%$ Resources:WebResources, Subject3 %>" />
            <dx:GridViewDataColumn FieldName="科目4" Caption="<%$ Resources:WebResources, Subject4 %>" />
            <dx:GridViewDataColumn FieldName="科目5" Caption="<%$ Resources:WebResources, Subject5 %>" />
            <dx:GridViewDataColumn FieldName="科目6" Caption="<%$ Resources:WebResources, Subject6 %>" />            
            <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true">
                <EditFormSettings Visible="false" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true">
                <EditFormSettings Visible="false" />
            </dx:GridViewDataColumn>            
        </Columns>
        <Templates>
            <TitlePanel>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td><dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e) { grid.AddNewRow(); }" />
                            </dx:ASPxButton></td>
                        <td>&nbsp;</td>
                        <td><dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" /></td>
                    </tr>
                </table>                         
            </TitlePanel>
        </Templates>           
        <Styles>
            <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
        </Styles>              
        <SettingsPager PageSize="8"></SettingsPager>
        <SettingsEditing EditFormColumnCount="5" Mode="EditFormAndDisplayRow" />
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>                                                                        
</asp:Content>