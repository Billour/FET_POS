<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SAL13.aspx.cs" Inherits="VSS_SAL_SAL13" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register Src="../../../Controls/DISItemChargesAndApply.ascx" TagName="DISItemChargesAndApply" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../../ClientUtility/Common.js"></script>

    <script type="text/javascript" language="javascript">
        
        function checkStoreNO(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() == '') {
                e.errorText = '門市編號不允許空值，請重新輸入';
                return false;
            }
            if (s.GetText() != '')
                PageMethods.wmCheckStoreNO(_gvSender.GetText(), wmCheckStoreNO_OnOK);
        }
        
        function wmCheckStoreNO_OnOK(returnData) {
            if (returnData != '') {
                //popSTORENO.SetValue(returnData);
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
            else {
                alert("[門市編號]不存在，請重新輸入");
            }
        }
        
        function chkSearch(s, e) {
            //alert('search');
        }

        function chkMSISDN(s, e) {
            var Qty = s.GetValue();
            var iQty = 0;

            if (Qty != null) {
                iQty = Number(Qty);
                if (Qty.length != 10 && Qty > 0) {
                    e.isValid = false;
                    e.errorText = '門號碼數不為10碼';
                    return false;
                }
                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '格式不正確';
                    return false;
                }

            }
        }
        
        function getPRODINFO2(s, e) {
            this.s = s;
            this.EventArgs = e;
            this.Sender = s;
            if (s.GetText() != '')
                PageMethods.getPRODINFOExtraSale(Sender.GetText(), getPRODINFO_OnOK2);
        }

        function getPRODINFO_OnOK2(returnData) {
            if (returnData == '') {
                alert("[商品料號]不存在，請重新輸入");
                popPRODNO.Focus();
                popPRODNO.SetText('');
            }
            else {
                if (returnData == "fail") {
                    alert("[商品料號]不存在，請重新輸入");
                    popPRODNO.Focus();
                    popPRODNO.SetText('');
                    EventArgs.processOnServer = false;
                }
                else {
                    if (Sender.GetText() == '')
                        popPRODNO.SetText('');
                }

            }
        }

        function getPromoteInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getPromoteInfo(s.GetText(), onSuccess);
        }

        function onSuccess(returnData, userContext, methodName) {

           if (methodName == "getPromoteInfo") {
                if (returnData != '') {
                    //PROMOTENAME.SetValue(returnData);
                }
                else {
                    //PROMOTENAME.SetValue(null);
                    alert("[促銷代號]不存在，請重新輸入");
                }
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--折扣優惠查詢-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DiscountProductSearch %>"></asp:Literal>
        </div>
        
        <div class="criteria">
            <table id="tableSearchPanl">
                <tr>
                    <td class="tdtxt">
                        <!--費率-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Rates %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxCheckBox ID="chkVOICE" runat="server" Text="Voice" />
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="chkDATA" runat="server" Text="Data" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--申辦類型-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, BidType %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxCheckBox ID="chkGA" runat="server" Text="GA" />
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="chkLOYALTY" runat="server" Text="Loyalty" />
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="chk223" runat="server" Text="2轉3" />
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="chkMNP" runat="server" Text="MNP" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="popSTORENO"  runat="server"  PopupControlName="StoresPopup" IsValidation="true"
                        SetClientValidationEvent="checkStoreNO" />
                    </td>
                    <td class="tdtxt">
                        <!--客戶門號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtMSISDN" runat="server" ClientInstanceName="txtMSISDN" MaxLength="10" CssClass="tbSpanWidth" >
                         <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" />
                         </ValidationSettings>
                         <ClientSideEvents  Validation="function(s, e){ chkMSISDN(s, e); }" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--ARPB金額-->
                        <asp:Literal ID="Literal6"  runat="server" Text="<%$ Resources:WebResources, ARPBamount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtARPB" MaxLength="9" runat='server' CssClass="tbSpanWidth" >
                        <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*"  ErrorText="請輸入數字"/>
                         </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--促銷代號-->
                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="txtMM" runat="server" PopupControlName="PromotionsPopupOnly" SetClientValidationEvent="getPromoteInfo" 
								                         Text='<%# Bind("PROMOTE_CODE") %>' KeyFieldValue1="NoDeadline" />
                    </td>
                    
                    <td class="tdtxt">
                        <!--促銷名稱-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtMMNAME" runat='server' CssClass="tbSpanWidth" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="popPRODNO" runat="server" PopupControlName="ProductsPopup" TextBoxClientInstanceName="popPRODNO"
                                                            AutoPostBack="false" KeyFieldValue1="extrasale"
                                                             OnClientTextChanged="function(s,e){ getPRODINFO2(s,e);}" />
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtPRODNAME" runat="server"  CssClass="tbSpanWidth" />
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click"  >
                            <ClientSideEvents Click="function(s, e) { chkSearch(s, e); }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                    </td>
                    <td>

                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="DISCOUNT_MASTER_ID"
            Width="100%" EnableCallBacks="False"  
            OnPageIndexChanged="grid_PageIndexChanged" 
            OnFocusedRowChanged="grid_OnFocusedRowChanged">
            <Columns>
                <dx:GridViewDataColumn FieldName="ROW_NO" Caption="<%$ Resources:WebResources, items %>" />
                <dx:GridViewDataColumn FieldName="DISCOUNT_CODE" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" />
                <dx:GridViewDataColumn FieldName="DISCOUNT_NAME" Caption="<%$ Resources:WebResources, DiscountName %>" />
                <dx:GridViewDataColumn FieldName="DISCOUNT_MONEY" Caption="<%$ Resources:WebResources, DiscountAmount %>" />
                <dx:GridViewDataColumn FieldName="DISCOUNT_RATE" Caption="<%$ Resources:WebResources, MerchandiseDiscountRate %>" />
                <dx:GridViewDataColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, EffectiveStartDate %>" />
                <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EffectiveEndDate %>" />
                <dx:GridViewDataColumn FieldName="DIS_USE_COUNT" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
            <SettingsPager PageSize="5"></SettingsPager>
        </cc:ASPxGridView>
        
        <div class="seperate"></div>
        
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" 
            ActiveTabIndex="0">
            <TabPages>
                
                <dx:TabPage Text="<%$ Resources:WebResources, RatesHostTypes %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                               <uc5:DISItemChargesAndApply ID="DISItemChargesAndApply1"  runat="server" />
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                
                <dx:TabPage Text="<%$ Resources:WebResources, DesignatedGoods %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PROD_DIS_ID"
                                    Width="50%" OnPageIndexChanged="gvMaster_PageIndexChanged">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                        <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, productname %>" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                
                <dx:TabPage Text="<%$ Resources:WebResources, SpecifyStore %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvStore" ClientInstanceName="gvStore" runat="server" KeyFieldName="STORE_DIS_ID"
                                    Width="50%" OnPageIndexChanged="gvStore_PageIndexChanged">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" />
                                        <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>" />
                                        <dx:GridViewDataColumn FieldName="ZONE" Caption="<%$ Resources:WebResources, ByDistrict %>" />
                                        <dx:GridViewDataColumn FieldName="DIS_USE_COUNT" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                
                <dx:TabPage Text="<%$ Resources:WebResources, PromotionCode %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvPromo" ClientInstanceName="gvPromo" runat="server" KeyFieldName="PROM_DIS_ID"
                                    Width="50%" OnPageIndexChanged="gvPromo_PageIndexChanged">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="PROMOTION_CODE" Caption="<%$ Resources:WebResources, PromotionCode %>" />
                                        <dx:GridViewDataColumn FieldName="PROMO_NAME" Caption="<%$ Resources:WebResources, Promotionname %>" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                
                <dx:TabPage Text="<%$ Resources:WebResources, TargetCustomers %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <table width="100%">
                                    <tr>
                                        <td align="left">                                        
                                            <asp:RadioButtonList ID="rbCustomer" runat="server" 
                                                OnSelectedIndexChanged="rbCustomer_SelectedIndexChanged"
                                                RepeatDirection="Horizontal" AutoPostBack="true">
                                                <asp:ListItem Value="CustLevel" Selected="True">客戶等級</asp:ListItem>
                                                <asp:ListItem Value="List">名單</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="trgv1" runat="server">
                                           <cc:ASPxGridView ID="gvARPB" ClientInstanceName="gvARPB" runat="server" KeyFieldName="CUST_LEVEL_ID"
                                            Width="50%" OnPageIndexChanged="gvARPB_PageIndexChanged">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="ROW_NO" Caption="項次" Visible="false" />
                                                    <dx:GridViewDataColumn FieldName="ARPB_S" Caption="ARPB金額(起)" />
                                                    <dx:GridViewDataColumn FieldName="ARPB_E" Caption="ARPB金額(訖)" />
                                                </Columns>
                                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                            </cc:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr id="trgv2" runat="server" visible="false">
                                        <td>
                                            <cc:ASPxGridView ID="gvMsisdn" ClientInstanceName="gvMsisdn" runat="server" KeyFieldName="CUST_LEVEL_ID"
                                            Width="10%" OnPageIndexChanged="gvMsisdn_PageIndexChanged">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="ROW_NO" Caption="項次" Visible="false" />
                                                    <dx:GridViewDataColumn FieldName="MSISDN" Caption="客戶門號" />
                                                </Columns>
                                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                            </cc:ASPxGridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                
                <dx:TabPage Visible="false" Text="<%$ Resources:WebResources, CostCenter %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvCostCenter" ClientInstanceName="gvCostCenter" runat="server" KeyFieldName="商品料號"
                                    Width="50%" OnPageIndexChanged="gvCostCenter_PageIndexChanged">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="COST_CENTER_NAME" Caption="成本中心" />
                                        <dx:GridViewDataColumn FieldName="PROD_CATEG" Caption="商品分類" />
                                        <dx:GridViewDataColumn FieldName="ACCOUNTCODE" Caption="會計科目" />
                                        <dx:GridViewDataColumn FieldName="AMT" Caption="金額" />
                                        <dx:GridViewDataColumn FieldName="REMARK" Caption="備註" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                
                <dx:TabPage Text="贈品設定">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvGifDisc" ClientInstanceName="gvGifDisc" runat="server" KeyFieldName="PROD_DIS_ID"
                                    Width="50%" OnPageIndexChanged="gvGifDisc_PageIndexChanged">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                        <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, productname %>" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                
                <dx:TabPage Text="加價購">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvAddIn" ClientInstanceName="gvAddIn" runat="server" KeyFieldName="PROD_DIS_ID"
                                    Width="50%" OnPageIndexChanged="gvAddIn_PageIndexChanged">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                        <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, productname %>" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                
            </TabPages>
        </dx:ASPxPageControl>
    </div>
</asp:Content>
