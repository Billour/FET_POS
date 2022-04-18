<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DIS04.aspx.cs" Inherits="VSS_DIS_DIS04" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
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
                        <dx:ASPxTextBox ID="txtRelationshipNo" runat="server" MaxLength="20"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--促銷名稱-->
                         <asp:Literal ID="lblRelationshipName" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtRelationshipName" runat="server" MaxLength="100"></dx:ASPxTextBox>
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
                                <td><uc1:PopupControl ID="ASPxTextBox1" runat="server" PopupControlName="ProductCategory" Width="150px" /></td>
                                
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><uc1:PopupControl ID="ASPxTextBox2" runat="server" PopupControlName="ProductCategory" Width="150px" /></td>
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
                                <td><uc1:PopupControl ID="ASPxTextBox3" runat="server" PopupControlName="ProductsPopup" Width="150px" /></td>
                                
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><uc1:PopupControl ID="ASPxTextBox4" runat="server" PopupControlName="ProductsPopup" Width="150px" /></td>
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
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
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
                        <cc:ASPxGridView ID="gvMaster" runat="server" Width="100%" KeyFieldName="PROMO_NO" EnableCallBacks="False"
                            OnFocusedRowChanged="gvMaster_FocusedRowChanged" OnPageIndexChanged="gvMaster_PageIndexChanged">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="ITEM_NO" Caption="<%$ Resources:WebResources, Items %>">
                                     <DataItemTemplate>
                                        <%#Container.ItemIndex + 1%>
                                     </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="FLAG" Visible="false" />--%>
                                <dx:GridViewDataTextColumn FieldName="PROMO_NO" Caption="<%$ Resources:WebResources, PromotionCode %>" />
                                <dx:GridViewDataTextColumn FieldName="PROMO_NAME" Caption="<%$ Resources:WebResources, PromotionName %>" />
                                
                                <dx:GridViewDataTextColumn FieldName="MODI_USER_NAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                                <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                            </Columns>
                            <SettingsPager PageSize="5"></SettingsPager>
                            <SettingsBehavior AllowFocusedRow="true"   ProcessFocusedRowChangedOnServer="True"/>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </cc:ASPxGridView>
                    </div>
                    <div class="seperate"></div>
                    <div id="Div_Dt">
                        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" AutoPostBack="True" Visible="false"
                            OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged" ActiveTabIndex="0" >
                            <TabPages>
                                <dx:TabPage Text="群組一">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail1" runat="server" AutoGenerateColumns="true" Width="100%" OnPageIndexChanged="gvDetail_PageIndexChanged">
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
                                                <cc:ASPxGridView ID="gvDetail2" runat="server" AutoGenerateColumns="true" Width="100%" OnPageIndexChanged="gvDetail_PageIndexChanged">
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
                                                <cc:ASPxGridView ID="gvDetail3" runat="server" AutoGenerateColumns="true" Width="100%" OnPageIndexChanged="gvDetail_PageIndexChanged">
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
                                                <cc:ASPxGridView ID="gvDetail4" runat="server" AutoGenerateColumns="true" Width="100%" OnPageIndexChanged="gvDetail_PageIndexChanged">
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
                                                <cc:ASPxGridView ID="gvDetail5" runat="server" AutoGenerateColumns="true" Width="100%" OnPageIndexChanged="gvDetail_PageIndexChanged">
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
                                                <cc:ASPxGridView ID="gvDetail6" runat="server" AutoGenerateColumns="true" Width="100%" OnPageIndexChanged="gvDetail_PageIndexChanged">
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
        
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>