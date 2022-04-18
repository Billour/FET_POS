<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="INV28.aspx.cs" Inherits="VSS_INV_INV28" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeicheckbox(con) {
            if (con.checked) {
                openwindow("SAL01_inputIMEIData.aspx");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">      
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--門市拆封IMEI設定-->
                    <asp:Literal ID="Literal1" runat="server" Text="門市拆封IMEI設定"></asp:Literal>
                </td>
            </tr>
        </table>
    </div> 
    <div>
        <div class="criteria">
            <table>                    
                <tr>
                    <td class="tdtxt">
                        <!--拆封日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OpenedDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Date="2010/07/01" /></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Date="2010/07/01" /></td>
                            </tr>                            
                        </table>                                                      
                    </td>                                                              
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" style="width:350px">
                            <tr>
                                <td><asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="productCodeTextBox1" runat="server"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="chooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>" AutoPostBack="false" SkinID="PopupButton" /></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="productCodeTextBox2" runat="server"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="chooseButton2" runat="server" Text="<%$ Resources:WebResources, Choose %>" AutoPostBack="false" SkinID="PopupButton" /></td>
                            </tr>                            
                        </table>                                                                                                      
                    </td>   
                                 
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td><dx:ASPxButton ID="searchButton" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="SearchButton_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e) { resetForm(aspnetForm); }" />
                    </dx:ASPxButton></td>
                </tr>
            </table>       
        </div>
        <div class="seperate"></div>
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="門市編號" Width="100%"           
            OnPageIndexChanged="grid_PageIndexChanged">                
            <Columns>                
                <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, StoreNo %>">
                    <DataItemTemplate>
                        <asp:LinkButton ID="commandButton" runat="server" OnCommand="CommandButton_Click" Text='<%# Eval("門市編號") %>' 
                            CommandName="Select" CommandArgument='<%# Eval("門市編號") %>' />                        
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>                                                
                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ExhibitionStartDate %>" />            
                <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ExhibitionEndDate %>" />                                    
                <dx:GridViewDataDateColumn FieldName="展示起日" Caption="<%$ Resources:WebResources, ExhibitionStartDate %>" />            
                <dx:GridViewDataDateColumn FieldName="展示訖日" Caption="<%$ Resources:WebResources, ExhibitionEndDate %>" />                                                                                         
                <dx:GridViewDataColumn FieldName="拆封數量" Caption="<%$ Resources:WebResources, OpenedQuantity %>" />                             
                <dx:GridViewDataColumn FieldName="折扣方式" Caption="<%$ Resources:WebResources, DiscountMethod %>" />
                <dx:GridViewDataColumn FieldName="金額/佔比" Caption="<%$ Resources:WebResources, AmountOrPercentage %>" />                
            </Columns>            
            <Styles>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
            </Styles>   
            <SettingsPager PageSize="5"></SettingsPager>
            <SettingsEditing EditFormColumnCount="4" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>                        
        <div class="seperate"></div>                    
        <cc:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" KeyFieldName="IMEI" Width="100%"  Settings-ShowTitlePanel="true"            
            OnPageIndexChanged="detailGrid_PageIndexChanged"
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
                <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, IMEI %>">
                    <EditItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td><dx:ASPxTextBox ID="imeiTextBox" runat="server"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="chooseButton" runat="server" SkinID="PopupButton" AutoPostBack="false"></dx:ASPxButton></td>
                            </tr>
                        </table>                                            
                        <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True"
                             AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/SAL/SAL01_inputIMEIData.aspx"    
                             PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
                             Width="500px" Height="400px" FooterText="Try to resize the control using the resize grip or the control's edges"
                             HeaderText="IMEI輸入"
                              
                             EnableHierarchyRecreation="true"
                             PopupElementID="chooseButton" TargetElementID="imeiTextBox" LoadingPanelID="lp2">
                             <ContentStyle>
                                 <Paddings Padding="2px"></Paddings>
                             </ContentStyle>
                         </cc:ASPxPopupControl>
                         <dx:ASPxLoadingPanel ID="lp2" runat="server"></dx:ASPxLoadingPanel>
                    </EditItemTemplate>  
                    
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true">
                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true">
                   <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                </dx:GridViewDataTextColumn>                           
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
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel ID="Label5" runat="server" Text="門市編號:GA00001"></dx:ASPxLabel></td>
                        </tr>
                    </table>                         
                </TitlePanel>
            </Templates>
            <Styles>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
            </Styles>                
            <SettingsEditing  Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>                                                                                          
    </div>
        
    <cc:ASPxPopupControl ID="productsPopup1" SkinID="ProductsPopup" runat="server"  
     EnableViewState="False" PopupElementID="chooseButton1" TargetElementID="productCodeTextBox1" LoadingPanelID="lp">                
    </cc:ASPxPopupControl>
    <cc:ASPxPopupControl ID="productsPopup2" SkinID="ProductsPopup" runat="server"  
     EnableViewState="False" PopupElementID="chooseButton2" TargetElementID="productCodeTextBox2" LoadingPanelID="lp">                
    </cc:ASPxPopupControl>
    
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp"></dx:ASPxLoadingPanel>
</asp:Content>