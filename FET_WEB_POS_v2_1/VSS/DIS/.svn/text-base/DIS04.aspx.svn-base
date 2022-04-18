<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DIS04.aspx.cs" Inherits="VSS_DIS_DIS04" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

        <div class="titlef">
            <table width="100%">
                <tr>
                    <td align="left">
                        <!--商品關聯性查詢-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, GroupRelationshipManagement %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--促銷代號-->
                        <asp:Literal ID="lblRelationshipNo" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtRelationshipNo" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--促銷名稱-->
                         <asp:Literal ID="lblRelationshipName" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtRelationshipName" runat="server"></dx:ASPxTextBox>
                    </td>
                   
                </tr>
               
                <tr>
                    <td class="tdtxt">
                        <!--商品類型-->
                        <asp:Literal ID="lblProductType" runat="server" Text="<%$ Resources:WebResources, ProductType1 %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="2">
                       <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="ASPxButton1" runat="server" Text="..." Width="10px"></dx:ASPxButton></td>
                                
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="ASPxButton2" runat="server" Text="..." Width="10px"></dx:ASPxButton></td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                   <td class="tdtxt"></td>
                    <td class="tdval"></td>
                </tr>
               <tr>
                <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="Literal2" runat="server" Text="商品料號"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="2">
                       <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="ASPxButton3" runat="server" Text="..." Width="10px"></dx:ASPxButton></td>
                                
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100px" ></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="ASPxButton4" runat="server" Text="..." Width="10px"></dx:ASPxButton></td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
               </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click"></dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="SubEditBlock">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" runat="server" Width="100%" KeyFieldName="促銷代號"  EnableCallBacks="False" 
                            onfocusedrowchanged="gvMaster_FocusedRowChanged" onpageindexchanged="gvMaster_PageIndexChanged">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                                     <DataItemTemplate>
                                        <%#Container.ItemIndex + 1%>
                                     </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="flag" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="促銷代號" Caption="<%$ Resources:WebResources, PromotionCode %>" />
                                <dx:GridViewDataTextColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>" />
                                
                                <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                                <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                            </Columns>
                            <SettingsPager PageSize="5"></SettingsPager>
                            <SettingsBehavior AllowFocusedRow="true"   ProcessFocusedRowChangedOnServer="True"/>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </cc:ASPxGridView>
                    </div>
                    <div class="seperate"></div>
                    <div id="Div_Dt">
                        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" AutoPostBack="True" Visible="false"
                            OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged" ActiveTabIndex="0">
                            <TabPages>
                                <dx:TabPage Text="群組一">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail1" runat="server" AutoGenerateColumns="true" Width="100%">
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsBehavior AllowFocusedRow="true" />
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                
                                <dx:TabPage Text="群組二">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail2" runat="server" AutoGenerateColumns="true" Width="100%">
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsBehavior AllowFocusedRow="true" />
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                
                                <dx:TabPage Text="群組三">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail3" runat="server" AutoGenerateColumns="true" Width="100%">
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsBehavior AllowFocusedRow="true" />
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                
                                <dx:TabPage Text="群組四">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail4" runat="server" AutoGenerateColumns="true" Width="100%">
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsBehavior AllowFocusedRow="true" />
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                
                                <dx:TabPage Text="群組五">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail5" runat="server" AutoGenerateColumns="true" Width="100%">
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsBehavior AllowFocusedRow="true" />
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                
                                <dx:TabPage Text="群組六">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail6" runat="server" AutoGenerateColumns="true" Width="100%">
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsBehavior AllowFocusedRow="true" />
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
         <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server"  
         EnableViewState="False" PopupElementID="ASPxButton3" TargetElementID="ASPxTextBox3" LoadingPanelID="lp1">                
     </cc:ASPxPopupControl>
     
       <cc:ASPxPopupControl ID="productsPopup1" SkinID="ProductsPopup" runat="server"  
         EnableViewState="False" PopupElementID="ASPxButton4" TargetElementID="ASPxTextBox4" LoadingPanelID="lp1">                
     </cc:ASPxPopupControl>
     
     <cc:ASPxPopupControl ID="ProductsType" SkinID="ProductsType" runat="server"  
         EnableViewState="False" PopupElementID="ASPxButton1" TargetElementID="ASPxTextBox1" LoadingPanelID="lp1">                
     </cc:ASPxPopupControl>
      <cc:ASPxPopupControl ID="ProductsType1" SkinID="ProductsType" runat="server"  
         EnableViewState="False" PopupElementID="ASPxButton2" TargetElementID="ASPxTextBox2" LoadingPanelID="lp1">                
     </cc:ASPxPopupControl>
     <dx:ASPxLoadingPanel ID="lp1" runat="server"></dx:ASPxLoadingPanel>
     <%--<dx:ASPxLoadingPanel ID="lp2" runat="server"></dx:ASPxLoadingPanel>--%>
     
</asp:Content>