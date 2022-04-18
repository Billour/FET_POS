<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="OPT05a.aspx.cs" Inherits="VSS_OPT_OPT05a" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">  
    <div>                
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品訂單查詢-->
                    <asp:Literal ID="Literal1" runat="server" Text="門市離線發票設定作業"></asp:Literal>
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
                        門市編號：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        門市名稱：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        狀態：
                    </td>
                    <td class="tdval">                        
                        <dx:ASPxComboBox ID="statusComboBox" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="有效" />
                                <dx:ListEditItem Text="尚未生效" />
                                <dx:ListEditItem Text="已過期" />
                            </Items>                           
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        所屬年月：
                    </td>
                    <td class="tdval" colspan="3">
                       <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                        <tr>
                        <td>起</td>
                        <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                        <td>&nbsp;</td>
                        <td>訖</td>
                        <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>
                        </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
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
                <td><dx:ASPxButton ID="Button1" runat="server" Text="查詢" OnClick="btnSearch_Click" /></td>
                <td>&nbsp;</td>
                <td><dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                    <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm); }" />
                </dx:ASPxButton></td>
                </tr>
            </table>   
        </div>
        <div class="seperate"></div>   
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="門市編號" Width="100%"           
            OnPageIndexChanged="grid_PageIndexChanged" Settings-ShowTitlePanel="true">                
            <Columns>                                                            
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="grid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>">
                    <DataItemTemplate>
                        <asp:LinkButton ID="commandButton" runat="server" OnCommand="CommandButton_Click" Text='<%# Eval("項次") %>' 
                            CommandName="Select" CommandArgument='<%# Eval("項次") %>' />
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>                
                <dx:GridViewDataColumn FieldName="用途" Caption="用途" />
                <dx:GridViewDataColumn FieldName="所屬年月(起)" Caption="所屬年月(起)" />
                <dx:GridViewDataColumn FieldName="所屬年月(訖)" Caption="所屬年月(訖)" />
                <dx:GridViewDataColumn FieldName="字軌" Caption="字軌" />
                <dx:GridViewDataColumn FieldName="起始編號" Caption="起始編號" />
                <dx:GridViewDataColumn FieldName="終止編號" Caption="終止編號" />
                <dx:GridViewDataColumn FieldName="已使用編號" Caption="已使用編號" />
                <dx:GridViewDataColumn FieldName="發票張數" Caption="發票張數" />                                  
                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true" />                                                                  
                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" />                
            </Columns>            
            <Styles>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
            </Styles>  
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
            <SettingsEditing Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>    
                                       
        <div class="seperate"></div>
        
        <cc:ASPxGridView ID="detailGrid" ClientInstanceName="grid" runat="server" KeyFieldName="項次" Width="100%" Visible="false"             
            OnPageIndexChanged="detailGrid_PageIndexChanged">                
            <Columns>                                                                         
                <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" />                                    
                <dx:GridViewDataColumn FieldName="機台號碼" Caption="<%$ Resources:WebResources, CashRegisterNo %>" />
                <dx:GridViewDataTextColumn Caption="起始編號">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Text='<%# Bind("起始編號") %>'></dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="終止編號">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Text='<%# Bind("終止編號") %>'></dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>                
                <dx:GridViewDataColumn FieldName="已使用編號" Caption="已使用編號" />                         
                <dx:GridViewDataColumn FieldName="張數" Caption="張數" />                                  
                <dx:GridViewDataColumn FieldName="發票分配日期" Caption="發票分配日期" />  
            </Columns>            
            <Styles>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
            </Styles>                  
            <SettingsEditing Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>    
        <div class="seperate"></div>              
        <div class="btnPosition" id="showFooterBtn" runat="server" visible="false">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                <td><dx:ASPxButton ID="SaveButton" runat="server" Text="<%$ Resources:WebResources, Save %>" /></td>
                <td>&nbsp;</td>
                <td><dx:ASPxButton ID="CancelButton" runat="server" Text="<%$ Resources:WebResources, Cancel %>" /></td>
                </tr>
            </table>     
        </div>    
</asp:Content>