<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="INV28.aspx.cs" Inherits="VSS_INV_INV28" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">      
         function checkDate(s, e) {
             var x = txtSDate.GetValue();
             var y = txtEDate.GetValue();

             if (x == null) { x = ""; }
             if (y == null) { y = ""; }

             if (x != "" && y != "") {

                 e.isValid = (x <= y);
                 if (!e.isValid) {
                     alert("展示日訖不允許小於展示日起，請重新輸入!");
                     s.SetValue(null);
                 }
             }
         }


         function checkPRODEvent(s, e, fName) {

             var txtS_PRODNO = getClientInstance('TxtBox', s.name.replace(fName, "txtS_PRODNO"));
             var txtE_PRODNO = getClientInstance('TxtBox', s.name.replace(fName, "txtE_PRODNO"));

             var x = txtS_PRODNO.GetText();
             var y = txtE_PRODNO.GetText();

             if (x == null) { x = ""; }
             if (y == null) { y = ""; }

             if (x != "" && y != "") {
                 e.isValid = (x <= y);
                 if (!e.isValid) {
                     alert("商品料號訖不允許小於商品料號起，請重新輸入!!");
                     s.SetValue(null);
                     e.processOnServer = false;
                 }
             }
         }

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">      
    <div class="titlef">
        <!--門市拆封IMEI設定-->
        <asp:Literal ID="Literal1" runat="server" Text="門市拆封IMEI設定"></asp:Literal>
    </div> 
    <div>
        <div class="criteria">
            <table>                    
                <tr>
                    <td class="tdtxt">
                        <!--展示起日-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ExhibitionStartDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ClientInstanceName="txtSDate">
                                        <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ClientInstanceName="txtEDate" >
                                        <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
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
                                <td><uc1:PopupControl ID="txtS_PRODNO" runat="server" PopupControlName="ProductsPopup" OnClientTextChanged="function(s,e){ checkPRODEvent(s,e, 'txtS_PRODNO'); }"  /></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><uc1:PopupControl ID="txtE_PRODNO" runat="server" PopupControlName="ProductsPopup" OnClientTextChanged="function(s,e){ checkPRODEvent(s,e, 'txtE_PRODNO'); }"  /></td>
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
                    <td><dx:ASPxButton ID="resetButton" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>">
                    </dx:ASPxButton></td>
                </tr>
            </table>       
        </div>
        <div class="seperate"></div>
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" 
            KeyFieldName="SEAL_OFF_PROD_ID" Width="100%" EnableCallBacks="False"            
            OnPageIndexChanged="grid_PageIndexChanged" 
            onfocusedrowchanged="grid_FocusedRowChanged">                
            <Columns>                
                <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                    <DataItemTemplate>
                        <asp:LinkButton ID="commandButton" runat="server" Text='<%# BIND("STORE_NO") %>' 
                            CommandName="Select" CommandArgument='<%# BIND("STORE_NO") %>' />                        
                    </DataItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataTextColumn>                      
                <dx:GridViewDataColumn FieldName="SEAL_OFF_PROD_ID" Visible="false" >
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="SEAL_OFF_STORE_ID" Visible="false" >
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataColumn>     
                <dx:GridViewDataColumn FieldName="IMEI_QTY" Visible="false" >
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataColumn>                        
                <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>" >
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="PRODNO"  Caption="<%$ Resources:WebResources, ProductCode %>">            
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">                                    
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataColumn>
                <dx:GridViewDataDateColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, ExhibitionStartDate %>">            
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, ExhibitionEndDate %>">                                                                                         
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataColumn FieldName="SEAL_OFF_QTY" Caption="<%$ Resources:WebResources, OpenedQuantity %>">                             
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Right"></CellStyle>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="DISCOUNT_TYPE" Caption="<%$ Resources:WebResources, DiscountMethod %>">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Left"></CellStyle>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="DISCOUNT_PRICE" Caption="<%$ Resources:WebResources, AmountOrPercentage %>">                
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <CellStyle HorizontalAlign="Right"></CellStyle>
                </dx:GridViewDataColumn>
            </Columns>            
            <Styles>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
            </Styles>   
            <SettingsPager PageSize="5"></SettingsPager>
            <SettingsEditing EditFormColumnCount="4" />
            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>                        
        <div class="seperate"></div>                    
        <cc:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" KeyFieldName="SEAL_OFF_IMEI_ID" 
            Width="100%"  Settings-ShowTitlePanel="true" Visible="false" EnableCallBacks="false"            
            OnPageIndexChanged="detailGrid_PageIndexChanged" 
            OnRowInserting="detailGrid_RowInserting" 
            OnRowUpdating="detailGrid_RowUpdating"
            OnRowValidating="detailGrid_RowValidating" 
            OnPreRender="gvDetail_PreRender" 
            onstartrowediting="detailGrid_StartRowEditing"  >                
            <Columns>                
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="detailGrid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                    <EditButton Visible="True"></EditButton>
                </dx:GridViewCommandColumn>                                                   
                <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, IMEI %>" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                    <EditItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td><dx:ASPxTextBox ID="imeiTextBox" runat="server" Text='<%# Bind("IMEI") %>' Width="200px"
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="20">
                                    <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                       </ValidationSettings>
                                </dx:ASPxTextBox></td>
                            </tr>
                        </table>                                            
                         <dx:ASPxLoadingPanel ID="lp2" runat="server"></dx:ASPxLoadingPanel>
                    </EditItemTemplate>                      
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="MODI_USER_NAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" >
                   <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None"  />
                </dx:GridViewDataTextColumn>                           
            </Columns>
            <Templates>
                <TitlePanel>
                    <table cellpadding="0" cellspacing="0" align="left">
                        <tr>
                            <td><dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>" AutoPostBack="false" OnClick="addButton_Click">
                                </dx:ASPxButton>
                            </td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" OnClick="deleteButton_Click" >
                            <ClientSideEvents Click = "function(s,e){if (!confirm('系統將刪除勾選之資料，確認刪除？')){e.processOnServer=false;}}"/>
                            </dx:ASPxButton></td>
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
            <SettingsPager PageSize="5"></SettingsPager>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>      
    </div>
</asp:Content>