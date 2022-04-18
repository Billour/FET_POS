<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="OPT18.aspx.cs" Inherits="VSS_OPT_OPT18" enableEventValidation="false" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">  
    <div class="titlef">
        <!--門市店長折扣設定-->
        <asp:Literal ID="Literal1" runat="server" Text="門市店長折扣設定"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">                                            
                        <table cellpadding="0" cellspacing="0" border="0" style="width:120px"> 
                            <tr>
                                <td>                          
                                    <dx:ASPxTextBox ID="storeNoTextBox" runat="server" Width="100px">                                                                
                                    </dx:ASPxTextBox>
                                </td>      
                                <td>&nbsp;</td>                          
                                <td>
                                    <dx:ASPxButton ID="chooseButton1" runat="server" 
                                        Text="<%$ Resources:WebResources, Choose %>" SkinID="PopupButton">                                                         
                                   </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--門市名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="storeNameTextBox" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">&nbsp;</td>
                    <td class="tdval">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--折扣月份-->
                        <asp:Literal ID="Literal4" runat="server" Text="折扣月份"></asp:Literal>：
                    </td>
                    <td colspan="5">                        
                       <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                            <td><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                            <td>&nbsp;</td>
                            <td><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                       </table>                                                          
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
             <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                <td><dx:ASPxButton ID="searchButton" runat="server" Text="<%$ Resources:WebResources, Search %>"
                    OnClick="SearchButton_Click" /></td>
                <td>&nbsp;</td>
                <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
                           
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="項次" Width="100%"  Settings-ShowTitlePanel="true"            
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
                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" ReadOnly="true">
                    <EditFormSettings Visible="false" />
                </dx:GridViewDataTextColumn>                                           
                <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" ReadOnly="true">
                    <EditFormSettings Visible="false" />
                </dx:GridViewDataTextColumn>            
                <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" ReadOnly="true">
                    <EditFormSettings Visible="false" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="折扣月份" Caption="折扣月份" />            
                <dx:GridViewDataColumn FieldName="折扣總額" Caption="折扣總額" />
                <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true">
                    <EditFormSettings Visible="false" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true">
                    <EditFormSettings Visible="false" />
                </dx:GridViewDataTextColumn>            
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
                <TitlePanel HorizontalAlign="Left"></TitlePanel>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
            </Styles>              
            <SettingsPager PageSize="5"></SettingsPager>
            <SettingsEditing EditFormColumnCount="3" Mode="EditFormAndDisplayRow" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>                                               
        <div class="seperate"></div>                
         <cc:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" KeyFieldName="項次" Width="100%"  Settings-ShowTitlePanel="true"            
            OnPageIndexChanged="grid_PageIndexChanged"
            OnRowInserting="detailGrid_RowInserting" OnRowUpdating="detailGrid_RowUpdating">                
            <Columns>                
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="detailGrid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button">
                    <EditButton Visible="True"></EditButton>
                </dx:GridViewCommandColumn>                                
                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" ReadOnly="true">                    
                    <EditFormSettings Visible="false" />
                </dx:GridViewDataTextColumn>                           
                <dx:GridViewDataComboBoxColumn FieldName="角色" Caption="<%$ Resources:WebResources, Role %>">
                   <PropertiesComboBox>
                    <Items>
                       <dx:ListEditItem Text="-請選擇-" />
                        <dx:ListEditItem Text="店員" Value="店員" />
                        <dx:ListEditItem Text="店長" Value="店長" />                        
                    </Items>
                   </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>" />            
                <dx:GridViewDataTextColumn FieldName="比率" Caption="<%$ Resources:WebResources, Ratio %>">
                    <PropertiesTextEdit DisplayFormatString="{0}%"></PropertiesTextEdit>
                    <EditItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                        <td><dx:ASPxTextBox ID="ratioTextBox" runat="server" HorizontalAlign="Right" Text='<%# Bind("比率") %>'></dx:ASPxTextBox></td>
                        <td>%</td>
                        </tr>
                        </table>
                    </EditItemTemplate>                    
                </dx:GridViewDataTextColumn>                                                
                <dx:GridViewDataColumn FieldName="折扣上限金額" Caption="折扣上限金額" />
                                                          
            </Columns>
            <Templates>
                <TitlePanel>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td><dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { detailGrid.AddNewRow(); }" />
                                </dx:ASPxButton>
                            </td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" /></td>
                        </tr>
                    </table>                         
                </TitlePanel>
            </Templates>
            <Styles>
                <TitlePanel HorizontalAlign="Left"></TitlePanel>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
            </Styles>   
            <SettingsPager PageSize="5"></SettingsPager>
            <SettingsEditing EditFormColumnCount="3" Mode="EditFormAndDisplayRow" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>                      
    </div>
    
    <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server"  
         EnableViewState="False" PopupElementID="chooseButton1" TargetElementID="storeNoTextBox">         
     </cc:ASPxPopupControl>
</asp:Content>

