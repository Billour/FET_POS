<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="TSAL031.aspx.cs" Inherits="VSS_SAL_TSAL031" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        //判斷商品料號是否存在
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

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>       
        <div class="titlef">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <!--換貨作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, GoodsExchanging %>" />(測)
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
                    <td class="tdval" width="80px">
                        <uc1:PopupControl ID="pcSTORENO" runat="server" ClientInstanceName="pcSTORENO" PopupControlName="StoresPopup" />
                    </td>
                    <td class="tdtxt">
                        <!--交易日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TradeDate %>" />：
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtS_Date" runat="server">
                                        <ValidationSettings CausesValidation="false" ErrorText="請輸入日期">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtE_Date" runat="server">
                                        <ValidationSettings CausesValidation="false" ErrorText="請輸入日期">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--機台-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CashRegister %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtMACHINE_ID" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="2" >
                          
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--客戶門號-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtMSISDN" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="10">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="cbSTATUS" runat="server" ValueType="System.String" AutoResizeWithContainer="true">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value=""  />
                                <%--<dx:ListEditItem Text="未結帳" Value="1" />--%>
                                <dx:ListEditItem Text="已結帳" Value="2" Selected="true"/>
                                <dx:ListEditItem Text="已作廢" Value="3" />
                                <%--<dx:ListEditItem Text="跨月作廢" Value="4" />--%>
                                <%--<dx:ListEditItem Text="換貨作廢" Value="5" />--%>
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--交易序號-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtSALE_NO" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="20">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--促銷代碼-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="txtPROMOTION_CODE" runat="server" PopupControlName="PromotionsPopupOnly" KeyFieldValue1="NoDeadline" />
                    </td>
                    <td class="tdtxt">
                        <!--銷售人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="cbSALE_PERSON" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                            SelectedIndex="0">
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--發票號碼-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtInv_No" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="10">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="pcPRODNO" runat="server" PopupControlName="ProductsPopup" SetClientValidationEvent="getProdInfo" />
                    </td>
                    <td class="tdtxt">
                        <!--付款方式-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="cbPAY_METHOD" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                            SelectedIndex="0">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="" />
                                <dx:ListEditItem Text="現金" Value="1" />
                                <dx:ListEditItem Text="信用卡" Value="2" />
                                <%--<dx:ListEditItem Text="離線信用卡" Value="3" />--%>
                                <%--<dx:ListEditItem Text="分期付款" Value="4" />--%>
                                <dx:ListEditItem Text="禮券" Value="5" />
                                <dx:ListEditItem Text="金融卡" Value="6" />
                                <dx:ListEditItem Text="Happy GO" Value="7" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton" />
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="POSUUID_MASTER"
                        AutoGenerateColumns="False" Width="100%" 
                        OnPageIndexChanged="gvMaster_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                                <DataItemTemplate>
                                    <input type="radio" name="radioButton" />
                                </DataItemTemplate>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataColumn FieldName="ITEMS" Caption="<%$ Resources:WebResources, Items %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="SALE_STATUS_NAME" Caption="<%$ Resources:WebResources, Status %>"
                                VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="TRADE_DATE" Caption="<%$ Resources:WebResources, TradeDate %>"
                                VisibleIndex="3" />
                            <dx:GridViewDataColumn FieldName="SALE_NO" Caption="<%$ Resources:WebResources, TransactionNo %>"
                                VisibleIndex="4" />
                            <dx:GridViewDataColumn FieldName="MACHINE_ID" Caption="<%$ Resources:WebResources, CashRegister %>"
                                VisibleIndex="5" />
                            <dx:GridViewDataColumn FieldName="MSISDN" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                VisibleIndex="6" />
                            <dx:GridViewDataColumn FieldName="INVOICE_NO" Caption="<%$ Resources:WebResources, InvoiceNo %>"
                                VisibleIndex="7" />
                            <dx:GridViewDataColumn FieldName="SALE_TOTAL_AMOUNT" Caption="<%$ Resources:WebResources, Amount %>"
                                VisibleIndex="8" />
                            <dx:GridViewDataColumn FieldName="PAY_METHOD" Caption="<%$ Resources:WebResources, PaymentMethod %>"
                                VisibleIndex="9" />
                            <dx:GridViewDataColumn FieldName="SALE_PERSON_NAME" Caption="<%$ Resources:WebResources, SalesClerk %>"
                                VisibleIndex="10" />
                            <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                VisibleIndex="11" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                        <ClientSideEvents
                            FocusedRowChanged="function(s, e) {
                               if(s.GetFocusedRowIndex() == -1)
                                    return;
                               var row = s.GetRow(s.GetFocusedRowIndex());
                               
                                if(__aspxIE)
                                    row.cells[0].childNodes[0].checked = true;
                                else
                                    row.cells[0].childNodes[1].checked = true;
                            }" />  
                        <SettingsPager PageSize="10"></SettingsPager>
                        <SettingsBehavior AllowFocusedRow="true"  />
                    </cc:ASPxGridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <div class="seperate"></div>
        
        <div class="btnPosition" id="showFooter" runat="server" visible="true">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnChangeProd" runat="server" Text="換貨明細查詢" 
                            AutoPostBack="true" onclick="btnChangeProd_Click">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
