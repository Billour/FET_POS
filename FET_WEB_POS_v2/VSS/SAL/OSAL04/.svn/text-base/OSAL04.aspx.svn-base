<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="OSAL04.aspx.cs" Inherits="VSS_SAL_OSAL04" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">   
        var ETCProdNo = "";
        var ETCProdNoExist = false;
        var etcAmt = 0;
        
        function CheckgvMaster(s, e) {
            if (gvMaster != undefined) {
                for (var i = 0; i < gvMaster.pageRowCount; i++) {
                    var gvRowName = "ctl00_MainContentPlaceHolder_gvMaster_cell" + i;
                    var hdIMEI = window.document.all[gvRowName + "_2_hdIMEIFlag"]; 
                    var txtIMEI = window.document.all[gvRowName + "_7_txtIMEI"]; 
                    
                    if (hdIMEI.GetText() != "1" && txtIMEI.GetText() == "") {
                        e.processOnServer = false;
                        alert("請輸入IMEI!!");
                        txtIMEI.SetFocus();
                        return false;
                    }
                }
            }
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip"></div>
   
    <div class="func1">
        <div class="titlef">
            <!--舊POS銷售作廢作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, SalesCancel %>" />
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" width="40">
                        <!--作廢序號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CancelNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" width="60">
                        <dx:ASPxLabel ID="lblCancelNo" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt" width="40">
                        <!--作廢日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, CancelDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblCancelDate" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--交易序號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lbSALE_NO" runat="server" />
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lbSALE_STATUS" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--交易日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lbTran_Date" ClientInstanceName="TRAN_DATE" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lbMODI_DTM" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="lblMODI_USER" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lbMODI_USER" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--備註-->
                        <asp:Literal ID="lblREMARK" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <dx:ASPxLabel ID="lblREMARK_VALUE" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="ID"
                        AutoGenerateColumns="False" Width="100%" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                        OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="ITEM_TYPE_NAME" Caption="<%$ Resources:WebResources, Category %>" VisibleIndex="0"/>
                            <dx:GridViewDataColumn FieldName="PROMO_NAME" Caption="<%$ Resources:WebResources, PromotionName %>" VisibleIndex="1"/>
                            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="2">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="hdIMEIFlag" runat="server" Text='' ClientVisible="false"></dx:ASPxTextBox>
                                    <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup" KeyFieldValue1="salehouse"
                                        IsValidation="true" Text='<%# Bind("[PRODNO]") %>' OnClientTextChanged="function(s, e) { getPRODINFO(s,e); }" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="3">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtPRODNAME" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[PRODNAME]") %>'
                                        runat="server">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="QUANTITY" Caption="<%$ Resources:WebResources, Quantity %>" VisibleIndex="4">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="hdOldQUANTITY" runat="server" ClientVisible="false" Text='<%# Bind("[QUANTITY]") %>'>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtQUANTITY" AutoPostBack="false" ClientSideEvents-TextChanged='function(s,e){ checkSaleQty(s,e);getPROD_IMEI_COUNT(s,e,"6_txtQUANTITY");}'
                                        Text='<%# Bind("[QUANTITY]") %>' runat="server" Width="50px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UNIT_PRICE" Caption="<%$ Resources:WebResources, UnitPrice %>" VisibleIndex="5">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtUNIT_PRICE" Text='<%# Bind("[UNIT_PRICE]") %>' ReadOnly="true"
                                        Border-BorderStyle="None" runat="server" Width="80px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="TOTAL_AMOUNT" Caption="<%$ Resources:WebResources, TotalPrice %>" VisibleIndex="6">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtTOTAL_AMOUNT" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[TOTAL_AMOUNT]") %>'
                                        runat="server" Width="40px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI_QTY" Caption="<%$ Resources:WebResources, Imei %>" VisibleIndex="7">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtIMEI" Text='<%# Bind("[IMEI]") %>'
                                        runat="server" Width="40px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                        <Settings ShowTitlePanel="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                    </cc:ASPxGridView>
                    <div class="criteria" id="divpay" runat="server">
                        <table>
                            <tr>
                                <td class="tdtxt">
                                    <!--已收總金額-->
                                    <asp:Literal ID="Literal22" runat="server" Text="已收總金額"></asp:Literal>：
                                </td>
                                <td class="tdval">
                                    <dx:ASPxLabel ID="lbTOTAL_AMOUNT" ClientInstanceName="lbTOTAL_AMOUNT" runat="server"
                                        Text="">
                                    </dx:ASPxLabel>
                                    <!--HappyGo折抵資訊 -->
                                    <!--兌換點數 -->
                                    <dx:ASPxTextBox ID="hdHG_REDEEM_POINT" ClientInstanceName="HG_REDEEM_POINT" runat="server"
                                        ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <!--HappyGo卡號 -->
                                    <dx:ASPxTextBox ID="hdHG_CARD_NO" ClientInstanceName="HG_CARD_NO" runat="server"
                                        ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <!--兌點金額 -->
                                    <dx:ASPxTextBox ID="hdTOTAL_AMOUNT" ClientInstanceName="TOTAL_AMOUNT" runat="server"
                                        ClientVisible="false">
                                    </dx:ASPxTextBox>
                                    <!--剩餘點數 -->
                                    <dx:ASPxTextBox ID="hdHG_LEFT_POINT" ClientInstanceName="HG_LEFT_POINT" runat="server"
                                        ClientVisible="false">
                                    </dx:ASPxTextBox>
                                </td>
                                <td class="tdtxt">
                                    &nbsp;
                                </td>
                                <td class="tdval">
                                    &nbsp;
                                </td>
                                <td class="tdtxt">
                                    &nbsp;
                                </td>
                                <td class="tdval">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <div style="text-align: left">
                        <table cellpadding="0" width="100%">
                            <tr>
                                <td style="width: 70%">
                                    <!--應退總金額-->
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, TotalRefundDue%>"></asp:Literal>：
                                    <dx:ASPxLabel ID="lbPayAmount" ClientInstanceName="lbPayAmount" runat="server">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="seperate">
                </div>
                <div class="btnPosition">
                    <table cellpadding="0" cellspacing="0" border="0" align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnConfirmCancel" ClientInstanceName ="btnConfirmCancel1" runat="server" Text="<%$ Resources:WebResources, ConfirmCancel %>"
                                    OnClick="btnConfirmCancel_Click" ClientSideEvents-Click="function(s,e){                                              
                                                 if (!confirm('確定作廢?'))
                                                    e.processOnServer=false;
                                                 else {
                                                  
                                                        s.SendPostBack('Click');
                                                        s.SetEnabled(false);
                                                    
                                                 }
                                             }" UseSubmitBehavior="false">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>   
</asp:Content>
