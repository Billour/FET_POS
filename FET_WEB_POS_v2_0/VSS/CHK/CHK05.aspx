<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="CHK05.aspx.cs" Inherits="VSS_CHK05_CHK05" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div class="titlef">
        <!--繳大鈔-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DollarBills %>"></asp:Literal>
    </div>
   
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--交易日期-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxDateEdit ID="tradeDate" runat="server"></dx:ASPxDateEdit>                        
                </td>
                <td class="tdtxt">
                    <!--機台編號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CashRegisterNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList1" runat="server">
                        <Items>
                            <dx:ListEditItem Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Text="01" />
                            <dx:ListEditItem Text="02" />
                            <dx:ListEditItem Text="03" />
                            <dx:ListEditItem Text="04" />
                            <dx:ListEditItem Text="05" />
                        </Items>                           
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate"></div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                OnClick="btnSearch_Click" /></td>
                <td>&nbsp;</td>
                <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false">
                    <ClientSideEvents Click="function(s,e) { resetForm(aspnetForm); }" />
                </dx:ASPxButton></td>
            </tr>
        </table>                                
    </div>
    <div class="seperate"></div>
                                                            
    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="批次" Width="100%"  Settings-ShowTitlePanel="true"            
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
            <dx:GridViewDataColumn FieldName="交易日期" Caption="<%$ Resources:WebResources, TradeDate %>" ReadOnly="true" />                           
            <dx:GridViewDataComboBoxColumn FieldName="機台編號" Caption="<%$ Resources:WebResources, CashRegisterNo %>">
               <PropertiesComboBox>
                <Items>
                   <dx:ListEditItem Text="-請選擇-" />
                    <dx:ListEditItem Text="01" Value="01" />
                    <dx:ListEditItem Text="02" Value="02" />
                    <dx:ListEditItem Text="03" Value="03" />
                    <dx:ListEditItem Text="04" Value="04" />
                    <dx:ListEditItem Text="05" Value="015" />
                </Items>
               </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataColumn FieldName="批次" Caption="<%$ Resources:WebResources, BatchNo %>" ReadOnly="true" />            
            <dx:GridViewDataColumn FieldName="繳大鈔" Caption="繳大鈔" />                                                
            <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" />
            <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true" />            
        </Columns>
        <Templates>
            <TitlePanel>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td><dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="AddButton_Click" /></td>
                        <td>&nbsp;</td>
                        <td><dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" /></td>
                    </tr>
                </table>                         
            </TitlePanel>
        </Templates>
        <Styles>
            <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
        </Styles>   
        <SettingsPager PageSize="10"></SettingsPager>
        <SettingsEditing EditFormColumnCount="4" />        
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>                                                             
                
    
</asp:Content>
