<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="INV27.aspx.cs" Inherits="VSS_INV_INV27" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">    
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--總部拆封商品設定-->
                    <asp:Literal ID="Literal1" runat="server" Text="總部拆封商品設定"></asp:Literal>
                </td>
                <td align="right">&nbsp;</td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--拆封日期-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OpenedDate %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                            <td>&nbsp;</td>
                            <td><asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>
                        </tr>
                    </table>                                    
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品料號-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td><asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                            <td><dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox></td>
                            <td><dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>" AutoPostBack="false" SkinID="PopupButton" /></td>
                            <td>&nbsp;</td>
                            <td><asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                            <td><dx:ASPxTextBox ID="TextBox4" runat="server"></dx:ASPxTextBox></td>
                            <td><dx:ASPxButton ID="btnChooseProduct1" runat="server" Text="<%$ Resources:WebResources, Choose %>" AutoPostBack="false" SkinID="PopupButton" /></td>
                        </tr>                     
                    </table>                                                                               
                </td>
                <td class="tdtxt">                
                </td>
                <td class="tdval">                  
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>                        
        </table>
    </div>
    <div class="btnPosition">
        <table align="center">
            <tr>
                <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click" />
                </td>
                <td>&nbsp;</td>
                <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false">
                    <ClientSideEvents Click="function(s,e) { resetForm(aspnetForm); }" />
                </dx:ASPxButton></td>
            </tr>
        </table>                
    </div>
    <div class="seperate"></div>    
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="商品料號" Width="100%"  Settings-ShowTitlePanel="true"            
            OnHtmlRowPrepared="grid_HtmlRowPrepared" OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged"
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
                <dx:GridViewDataColumn FieldName="拆封日期" Caption="<%$ Resources:WebResources, OpenedDate %>"  VisibleIndex="2"/>
                <dx:GridViewDataDateColumn FieldName="展示起日" Caption="<%$ Resources:WebResources, ExhibitionStartDate %>"  VisibleIndex="3"/>            
                <dx:GridViewDataDateColumn FieldName="展示訖日" Caption="<%$ Resources:WebResources, ExhibitionEndDate %>" VisibleIndex="4" />                    
                <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="5">
                    <DataItemTemplate>
                        <asp:LinkButton ID="commandButton" runat="server" OnCommand="CommandButton_Click" Text='<%# Eval("商品料號") %>' 
                            CommandName="Select" CommandArgument='<%# Eval("商品料號") %>' />                        
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>                                        
                <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"  VisibleIndex="6" />                  
                <dx:GridViewDataColumn FieldName="拆封數量" Caption="<%$ Resources:WebResources, OpenedQuantity %>"  VisibleIndex="7" />                             
                <dx:GridViewDataColumn FieldName="折扣方式" Caption="<%$ Resources:WebResources, DiscountMethod %>"  VisibleIndex="8" />
                <dx:GridViewDataColumn FieldName="金額/占比" Caption="<%$ Resources:WebResources, AmountOrPercentage %>"  VisibleIndex="9" />
                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" VisibleIndex="10" />
                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true" VisibleIndex="11" />            
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
            <SettingsEditing EditFormColumnCount="4" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>            
        <div class="seperate"></div>
        <dx:ASPxPageControl ID="tabPage" ClientInstanceName="tabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True" Width="100%" Visible="false">
            <TabPages>
                <dx:TabPage Text="<%$ Resources:WebResources, SpecifyStore %>">
                    <ContentCollection>
                        <dx:ContentControl runat="server">                            
                            <cc:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" KeyFieldName="門市編號" Width="100%"  Settings-ShowTitlePanel="true"            
                                OnPageIndexChanged="grid_PageIndexChanged"
                                OnRowInserting="grid_RowInserting" OnRowUpdating="detailGrid_RowUpdating">                
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
                                    <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />                                    
                                    <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />                                                                        
                                    <dx:GridViewDataColumn FieldName="區域別" Caption="<%$ Resources:WebResources, ByDistrict %>" />                                                      
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td><dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e){ detailGrid.AddNewRow(); }" />
                                                </dx:ASPxButton></td>
                                                <td>&nbsp;</td>
                                                <td><dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" /></td>
                                                <td>&nbsp;</td>
                                                <td><dx:ASPxComboBox ID="districtComboBox" runat="server" Width="100">
                                                        <Items>                                                                                                                
                                                            <dx:ListEditItem Text="區域" Value="區域" Selected="true" />
                                                            <dx:ListEditItem Text="ALL" Value="ALL" />
                                                            <dx:ListEditItem Text="北一區" Value="北一區" />
                                                            <dx:ListEditItem Text="中一區" Value="中一區" />
                                                            <dx:ListEditItem Text="南一區" Value="南一區" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </td>                          
                                                <td><dx:ASPxButton ID="Button6" runat="server" Text="<%$ Resources:WebResources, DivideDistrict %>" /></td>
                                            </tr>
                                        </table>                         
                                    </TitlePanel>
                                </Templates>
                                <Styles>
                                    <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
                                </Styles>   
                                <SettingsEditing Mode="Inline" />
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>                                                        
                        </dx:ContentControl>
                    </ContentCollection>                
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>           
        <div class="seperate"></div>
        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td><dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" Visible="false" /></td>
                    <td><dx:ASPxButton ID="btnDiscard" runat="server" Text="<%$ Resources:WebResources, Discard %>" Visible="false" /></td>
                    <td><dx:ASPxButton ID="Button8" runat="server" Text="<%$ Resources:WebResources, Reset %>" Visible="false" /></td>
                </tr>            
            </table>                                            
        </div>
        
    <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server"  
         EnableViewState="False" PopupElementID="btnChooseProduct" TargetElementID="TextBox3">                
    </cc:ASPxPopupControl>
    
</asp:Content>