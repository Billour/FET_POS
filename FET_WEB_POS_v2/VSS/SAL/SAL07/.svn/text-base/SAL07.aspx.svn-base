<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SAL07.aspx.cs" Inherits="VSS_SAL_SAL07_SAL07" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
    
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/ExportExcelData.ascx" TagName="ExportExcelData" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/DISItemChargesAndApply.ascx" TagName="DISItemChargesAndApply" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        function getProdInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getProdInfo(_gvSender.GetText(), getProdInfo_OnOK);
        }

        function getStoreInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() == '') {
                //alert("門市編號不允許空值，請重新輸入");
            } else {
                PageMethods.getStoreInfo(_gvSender.GetText(), getStoreInfo_OnOK);
            }
        }

        function getStoreInfo_OnOK(returnData) {
            if (returnData == "") {
                alert('門市編號不存在，請重新輸入');
                _gvSender.SetValue(null);
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
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

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

   <div>
       <div class="titlef">
            <!--促銷商品價格查詢-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionPriceEnquiry %>" />
        </div>  
       
       <div class="criteria"> 
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--專案類型-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProjectType %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="cbProjectType" runat="server" Width="120">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="0"  />
                                <dx:ListEditItem Text="現行專案" Value="1" Selected="true"/>
                                <dx:ListEditItem Text="過期專案" Value="2" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品廠牌-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductBrand %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtProductName1" runat="server" CssClass="tbSpanWidth" 
                            MaxLength="30">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品型號-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources,ProductModel %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtProductName2" runat="server" CssClass="tbSpanWidth" 
                            MaxLength="20">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品料號-->
                        <asp:Literal ID="ltProductCode" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtProdNO" runat="server" CssClass="tbSpanWidth" 
                            MaxLength="10">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--促銷價-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources,PromotionPrice %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" align="left">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="PromotPrice_start" runat="server" Width="75" MaxLength="9" >
                                        <ValidationSettings>
                                            <RegularExpression ErrorText="請輸入數字" ValidationExpression="^-?\d+$" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="PromotPrice_end" runat="server" Width="75" MaxLength="9">
                                          <ValidationSettings>
                                            <RegularExpression ErrorText="請輸入數字" ValidationExpression="^-?\d+$" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                               
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--促銷代號-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtPromoNO" runat="server" CssClass="tbSpanWidth" MaxLength="20">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtProductName3" runat="server" CssClass="tbSpanWidth" MaxLength="50">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxCheckBox ID="CheckBox3" runat="server" Text="庫存量>0">
                        </dx:ASPxCheckBox>
                        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Text="只查有效料號">
                        </dx:ASPxCheckBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--促銷名稱-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap" colspan="5">
                        <table cellpadding="0" cellspacing="0" align="left">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources,FrontText %>"
                                        Width="80">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources,VoiceRates %>"
                                        Width="80">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="<%$ Resources:WebResources,VoiceMonthLimit %>"
                                        Width="120">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:WebResources,BonusRates %>"
                                        Width="80">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="<%$ Resources:WebResources,BonusMonthLimit %>"
                                        Width="120">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="<%$ Resources:WebResources,PrepaymentAmount %>"
                                        Width="80">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtPromoName1" runat="server" Width="80">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPromoName2" runat="server" Width="80">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPromoName3" runat="server" Width="120">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPromoName4" runat="server" Width="80">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPromoName5" runat="server" Width="120">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPromoName6" runat="server" Width="80">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
       </div>
    
       <div class="seperate"></div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" LoadingPanelID="lp">
                        </dx:ASPxButton>
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

        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0"  >
            <TabPages>
                <dx:TabPage Text="促銷商品">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                     OnClick="ASPxButton1_Click" AutoPostBack="false" CausesValidation="false">
                                </dx:ASPxButton>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="UUID"
                                            AutoGenerateColumns="False" Width="100%" EnableCallBacks="False" 
                                            OnPageIndexChanged="gvMaster_PageIndexChanged"
                                            OnFocusedRowChanged="gvMaster_FocusedRowChanged">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="0">
                                                    <DataItemTemplate>
                                                        <%#Container.ItemIndex + 1%>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="PROMO_NO" Caption="<%$ Resources:WebResources, PromotionCode %>" VisibleIndex="1" />
                                                <dx:GridViewDataColumn FieldName="PROMO_NAME" Caption="<%$ Resources:WebResources, PromotionName %>" VisibleIndex="2" />
                                                <dx:GridViewDataColumn FieldName="B_DATE" Caption="<%$ Resources:WebResources, StartDate %>" VisibleIndex="3" />
                                                <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>" VisibleIndex="4" />
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />                                            
                                            <SettingsPager PageSize="20"></SettingsPager>
                                            <Settings ShowTitlePanel="false"></Settings>
                                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        </cc:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                    <ContentTemplate>
                                   
                                        <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" KeyFieldName="PROD_NO"
                                            AutoGenerateColumns="False" Width="100%"
                                            onpageindexchanged="gvDetail_PageIndexChanged" 
                                            onhtmlrowprepared="gvDetail_HtmlRowPrepared">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="0">
                                                    <DataItemTemplate>
                                                        <%#Container.ItemIndex + 1%>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="PROD_NO" Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="1" />
                                                <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="2" />
                                                <dx:GridViewDataColumn FieldName="PROMO_PROD_GROUP" Caption="<%$ Resources:WebResources, ProductGroup %>" VisibleIndex="3" />
                                                <dx:GridViewDataColumn FieldName="AMOUNT" Caption="<%$ Resources:WebResources, PromotionPrice %>" VisibleIndex="4" />
                                                <dx:GridViewDataColumn FieldName="ONHANDQTY" Caption="<%$ Resources:WebResources, StockQuantity %>" VisibleIndex="5" />
                                            </Columns>
                                            <Templates>
                                                <TitlePanel>
                                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="<%$ Resources:WebResources, PromotionDetail %>">
                                                    </dx:ASPxLabel>
                                                </TitlePanel>
                                            </Templates>
                                            <Settings ShowTitlePanel="True" />
                                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        </cc:ASPxGridView>
                                        
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvMaster" EventName="FocusedRowChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                
                <dx:TabPage Text="折扣查詢">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <table width="100%">
                                    <tr>
                                        <td class="tdtxt">促銷代號：</td>
                                        <td class="tdval" colspan="4">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <dx:ASPxLabel ID="lblPromoCode" runat="server" Text="">
                                                    </dx:ASPxLabel>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="gvMaster" EventName="FocusedRowChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdtxt">促銷名稱：</td>
                                        <td class="tdval" colspan="4">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <dx:ASPxLabel ID="lblPromoName" runat="server" Text="">
                                                    </dx:ASPxLabel>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="gvMaster" EventName="FocusedRowChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdtxt">費率：</td>
                                        <td class="tdval" colspan="4">
                                            <uc5:DISItemChargesAndApply ID="DISItemChargesAndApply1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdtxt">門市編號：</td>
                                        <td colspan="4">
                                            <uc1:PopupControl ID="txtStoreNo2" runat="server" IsValidation="false" PopupControlName="StoresPopup" SetClientValidationEvent="getStoreInfo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdtxt">商品料號：</td>
                                        <td class="tdval" colspan="4">
                                            <uc1:PopupControl ID="txtProdNo2" runat="server" IsValidation="false" 
                                                PopupControlName="ProductsPopup" SetClientValidationEvent="getProdInfo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdtxt" >客戶門號：</td>
                                        <td class="tdval">
                                            <dx:ASPxTextBox ID="txtPhoneNo" runat="server" Width="100px" MaxLength="10">
                                                <ValidationSettings>
                                                    <RegularExpression ErrorText="格式不正確" ValidationExpression="^[0-9]{10}" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="tdtxt">ARPB金額：</td>
                                        <td class="tdval">
                                            <dx:ASPxTextBox ID="txtARPB" runat="server" Width="100px" MaxLength="9">
                                                <ValidationSettings>
                                                   <RegularExpression ErrorText="請輸入數字" ValidationExpression="^-?\d+$" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="btnPosition">
                                <table align="center" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnSearchD" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                                OnClick="btnSearchD_Click" >
                                                <%--<ClientSideEvents Click="
                                                function(s,e){
                                                    if($('#ctl00_MainContentPlaceHolder_ASPxPageControl1_txtStoreNo2_txtControl_I').val() == ''){
                                                        alert('門市編號不允許空值，請重新輸入');
                                                       e.processOnServer = false;
                                                    }
                                                }
                                                " />--%>
                                                </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton5" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                              OnClick="ASPxButton5_Click">
                                               
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="seperate"></div>
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <cc:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="ASPxGridView1"
                                            KeyFieldName="DISCOUNT_MASTER_ID" Width="100%" AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="0">
                                                    <DataItemTemplate>
                                                        <%#Container.ItemIndex + 1%>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="DISCOUNT_CODE" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                                                    VisibleIndex="1" />
                                                <dx:GridViewDataColumn FieldName="DISCOUNT_NAME" Caption="<%$ Resources:WebResources, DiscountName %>"
                                                    VisibleIndex="2" />
                                                <dx:GridViewDataColumn FieldName="DISCOUNT_MONEY" Caption="<%$ Resources:WebResources, DiscountAmount %>"
                                                    VisibleIndex="3" />
                                                <dx:GridViewDataColumn Caption="<%$ Resources:WebResources, GiftAndIncreasesPriceBuys %>" VisibleIndex="4" FieldName="GIFT" />
                                            </Columns>
                                            <SettingsPager PageSize="5"></SettingsPager>
                                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        </cc:ASPxGridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSearchD" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>

    </div>
    
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Width="150px">       
    </dx:ASPxLoadingPanel>

    <uc3:ExportExcelData ID="ExportExcelData1" runat="server" />
    
</asp:Content>

