<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="DIS05.aspx.cs" Inherits="VSS_DIS_DIS05" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript">
           
       function getProdInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getProdInfo(_gvSender.GetText(), getProdInfo_OnOK);
        }
        function getProdInfo_OnOK(returnData) {
            if (returnData == 0) {
                alert('商品料號不存在!!');
                _gvSender.SetValue(null);
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
        }
    </script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">  
    <div class="func">
    
        <div class="titlef">
            <!--促銷商品設定查詢-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionMixSearch %>"></asp:Literal>
        </div>
        
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--促銷代碼-->
                        <asp:Literal ID="lblPromotionCode" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtPromoNo" runat="server" MaxLength="20"></dx:ASPxTextBox>
                    </td>
                
                    <td class="tdtxt">
                        <!--促銷名稱-->
                        <asp:Literal ID="lblPromotionName" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtPromoName" runat="server" MaxLength="100"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--開始日期-->
                        <asp:Literal ID="lblStartDate" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                       <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtBDate_S" runat="server" ClientInstanceName="txtSDate">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                             
                                <td><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtBDate_E" runat="server" ClientInstanceName="txtEDate">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                               </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品型態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductType %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlProdConfigType" runat="server" ValueType="System.String">
                            <Items>
                                <dx:ListEditItem Text="All" Value="" Selected="true" />
                                <dx:ListEditItem Text="指定商品" Value="1" />
                                <dx:ListEditItem Text="一般商品" Value="2" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                
                    <td class="tdtxt">
                        <!--變價規則-->
                        <asp:Literal ID="Literal10" runat="server" Text="變價規則"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlNeedToPricing" runat="server" ValueType="System.String">
                            <Items>
                                <dx:ListEditItem Text="All" Value="" Selected="true" />
                                <dx:ListEditItem Text="變價" Value="Y" />
                                <dx:ListEditItem Text="不變價" Value="N" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="lblProductCode" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" >
                      <table cellpadding="0" cellspacing="0" border="0" >
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td>
                                    <uc1:PopupControl ID="txtProdNo_S" runat="server" PopupControlName="ProductsPopup" SetClientValidationEvent="getProdInfo" Width="150"   />
                                </td>
                                <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><uc1:PopupControl ID="txtProdNo_E" runat="server" PopupControlName="ProductsPopup"  SetClientValidationEvent="getProdInfo"  Width="150" /></td>
                            </tr>
                        </table>
                    </td>
               
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="lblProductName" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtProdName" runat="server" MaxLength="100"></dx:ASPxTextBox>
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
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                         </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>        
        
        <div class="seperate"></div>
        
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="UUID" 
                            Width="100%"  EnableCallBacks="False" 
                            onfocusedrowchanged="gvMaster_FocusedRowChanged" 
                            onpageindexchanged="gvMaster_PageIndexChanged" 
                            onhtmlrowprepared="gvMaster_HtmlRowPrepared">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                                    <DataItemTemplate>
                                         <%#Container.ItemIndex + 1%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PROMO_NO" Caption="<%$ Resources:WebResources, PromotionCode %>" />
                                <dx:GridViewDataTextColumn FieldName="PROMO_NAME" Caption="<%$ Resources:WebResources, PromotionName %>" />
                                <dx:GridViewDataColumn FieldName="B_DATE" Caption="<%$ Resources:WebResources, StartDate %>" />
                                <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>" />
                                <dx:GridViewDataTextColumn FieldName="PROD_ADDED_DATE" Caption="預計上架日" />
                                <dx:GridViewDataTextColumn FieldName="PROD_CONFIG_TYPE" Caption="<%$ Resources:WebResources, ProductType %>" >
                                   <DataItemTemplate>
                                       <dx:ASPxLabel ID="lblProdConfigType" runat="server" Text='<%# Bind("PROD_CONFIG_TYPE") %>'></dx:ASPxLabel>
                                   </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PROMO_SUBSIDY" Caption="Promotion Subsidy" />
                                <dx:GridViewDataTextColumn FieldName="NEED_TO_PRICING" Caption="變價規則">
                                   <DataItemTemplate>
                                       <dx:ASPxLabel ID="lblNeedToPricing" runat="server" Text='<%# Bind("NEED_TO_PRICING") %>'></dx:ASPxLabel>
                                   </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="REMARK" Caption="<%$ Resources:WebResources, Remark %>" />
                                <dx:GridViewDataTextColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                                <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                            </Columns>
                            <SettingsPager PageSize="5"></SettingsPager>
                            <Settings ShowTitlePanel="false" />
                            <SettingsBehavior AllowFocusedRow="true"  ProcessFocusedRowChangedOnServer="True"/>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </cc:ASPxGridView> 
                    </div>
                    
                    <div class="seperate"></div>

                    <div id="Div_Dt">
                        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" AutoPostBack="True" 
                            Width="100%" Visible="False"
                            onactivetabchanged="ASPxPageControl1_ActiveTabChanged">
                            <TabPages>
                            
                                <dx:TabPage Text="商品類別">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail1" ClientInstanceName="gvDetail" runat="server" 
                                                    Width="100%" KeyFieldName="UUID"
                                                    OnPageIndexChanged="gvDetail1_PageIndexChanged" 
                                                    OnHtmlDataCellPrepared="gvDetail1_HtmlDataCellPrepared">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn>
                                                            <HeaderTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                                                                    <tr>
                                                                        <td colspan="3" style="border-bottom:1px #8E8E8E  solid">Handset</td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td width="30%">2G</td>
                                                                        <td width="30%">3G</td>
                                                                        <td width="40%">3.5G</td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            
                                                            <DataItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                                                                    <tr>
                                                                        <td align="center" width="30%">
                                                                            <dx:ASPxLabel ID="lbl2G" runat="server" Text='<%#BIND("A")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                        <td align="center" width="30%">
                                                                            <dx:ASPxLabel ID="lbl3G" runat="server" Text='<%#BIND("B")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                        <td align="center" width="40%">
                                                                            <dx:ASPxLabel ID="lbl35G" runat="server" Text='<%#BIND("C")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        
                                                        <dx:GridViewDataTextColumn>
                                                            <HeaderTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                                                                    <tr>
                                                                        <td colspan="2" style="border-bottom:1px #8E8E8E  solid">Broadband</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="50%">Netbook</td>
                                                                        <td width="50%">Datacard</td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            
                                                            <DataItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                                                                    <tr>
                                                                        <td align="center" width="50%">
                                                                            <dx:ASPxLabel ID="lblNetbook" runat="server" Text='<%#BIND("D")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                        <td align="center" width="50%">
                                                                            <dx:ASPxLabel ID="lblDatacard" runat="server" Text='<%#BIND("E")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Other" CellStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <dx:ASPxLabel ID="lblOther" runat="server" Text='<%#BIND("F")  %>'></dx:ASPxLabel>
                                                            </DataItemTemplate>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="SUB_SUBSIDY" Caption="補貼金額" />
                                                    </Columns>
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                    <Settings ShowTitlePanel="false" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                
                                <dx:TabPage Text="促銷商品">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail2" ClientInstanceName="gvDetail2" runat="server" 
                                                    Width="100%" KeyFieldName="UUID"
                                                    OnPageIndexChanged="gvDetail2_PageIndexChanged">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                                                             <DataItemTemplate>
                                                                <%#Container.ItemIndex + 1%>
                                                             </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="PROD_NO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                                        <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" />
                                                        <dx:GridViewDataTextColumn FieldName="PROMO_PROD_GROUP" Caption="商品群組" />
                                                        <dx:GridViewDataTextColumn FieldName="AMOUNT" Caption="<%$ Resources:WebResources, PromotionPrice %>" />
                                                    </Columns>
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                    <Settings ShowTitlePanel="false" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>

                                <dx:TabPage Text="指定門市">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail3" ClientInstanceName="gvDetail3" runat="server" 
                                                    Width="100%" KeyFieldName="UUID" 
                                                    OnPageIndexChanged="gvDetail3_PageIndexChanged">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" />
                                                        <dx:GridViewDataTextColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>" />
                                                    </Columns>
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                    <Settings ShowTitlePanel="false" />
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
        
    </div>
  
</asp:Content>