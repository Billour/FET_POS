<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SAL03.aspx.cs" Inherits="VSS_SAL_SAL03" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=no,scrollbars=no,location=no,toolbar=no,status=no');
        }
        function imeicheckbox(con) {
            if (con.GetChecked()) {
                openwindow("SAL01_inputIMEIData.aspx", 720, 300);
            }
        }
    </script>

    <script type="text/javascript">
        var secondsRemaining = 5;
        var countdownTimer;

        function decreaseTime() {
            secondsRemaining--;
            if (secondsRemaining < 0) {
                printPopup.Hide();
                return true;
            }
            countdownTimer = setTimeout(decreaseTime, 1000);
        }           
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip">
    </div>
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--換貨作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, GoodsExchanging %>" />
                </td>
                <td align="right">
                    <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e){ document.location='SAL02.aspx'; return false;}" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="func1">
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--交易序號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />：
                    </td>
                    <td class="tdval">
                      <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                            <ContentTemplate>--%>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            <%--</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </td>
                    <td class="tdtxt">
                        <!--單據類別-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SalesSlipType %>" />：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label2" runat="server" Text="發票/收據"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--狀 態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>" />：
                    </td>
                    <td class="tdval">
                      <%--  <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline">
                            <ContentTemplate>--%>
                                <asp:Label ID="Label3" runat="server" Text="00-未存檔"></asp:Label>
                            <%--</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--交易日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TradeDate %>" />：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label12" runat="server" Text="2010/07/07"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>" />：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label5" runat="server" Text="2010/07/01 22:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--發票號碼-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>" />：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lbInvoiceNo" runat="server" />
                    </td>
                    <td class="tdtxt">
                        <!--統一編號-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox3" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>" />：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--發票抬頭-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox4" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--備註-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" CssClass="tbWidthFormat">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        HG卡號：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label13" runat="server" Text="N1234567890123456"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        HG剩餘點數：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label14" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <cc:ASPxGridView ID="gvMasterEdit" runat="server" ClientInstanceName="gvMasterEdit"
                        KeyFieldName="項次" AutoGenerateColumns="False" Width="100%" OnRowInserting="gvMasterEdit_RowInserting"
                        OnHtmlRowPrepared="gvMasterEdit_HtmlRowPrepared" Settings-ShowTitlePanel="true">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center"
                                ButtonType="Button">
                                <HeaderTemplate>
                                    <input type="checkbox" onclick="gvMasterEdit.SelectAllRowsOnPage(this.checked);"
                                        title="Select/Unselect all rows on the page" />
                                </HeaderTemplate>
                                <UpdateButton Text="儲存">
                                </UpdateButton>
                                <CancelButton Text="取消">
                                </CancelButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="status" Caption=" " VisibleIndex="1" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="類別" Caption="<%$ Resources:WebResources, Category %>"
                                VisibleIndex="2" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>"
                                VisibleIndex="3" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                VisibleIndex="4">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                                <EditFormSettings VisibleIndex="4" />
                                <EditFormSettings Caption="商品編號" VisibleIndex="4" />
                                <EditItemTemplate>
                                    <uc1:PopupControl ID="PopupControl0" runat="server" PopupControlName="ProductsPopup" />
                                    <%--<table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, choose %>" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e){openwindow('SAL01_searchProductNo.aspx',400,350);return false;}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>--%>
                                </EditItemTemplate>
                                <DataItemTemplate>
                                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" />
                                    <%-- <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="txtprodno" runat="server">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnprodno" runat="server" Text="<%$ Resources:WebResources, choose %>" AutoPostBack="false">
                                                    <%--                                                    <ClientSideEvents Click="function(s,e){openwindow('SAL01_searchProductNo.aspx',400,350);return false;}" />

                                                </dx:ASPxButton>
                                                <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ContentUrl="SAL01_searchProductNo.aspx"
                                                    PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" Width="500px"
                                                    Height="420px" HeaderText="選擇商品料號" TargetElementID="txtprodno" PopupElementID="btnprodno">
                                                </cc:ASPxPopupControl>
                                            </td>
                                        </tr>
                                    </table>--%>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="5" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>"
                                VisibleIndex="6" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="單價" Caption="<%$ Resources:WebResources, UnitPrice %>"
                                VisibleIndex="7" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="總價" Caption="<%$ Resources:WebResources, TotalPrice %>"
                                VisibleIndex="8" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="9" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                </PropertiesTextEdit>
                                <DataItemTemplate>
                                    <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                    </dx:ASPxImage>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>"
                                VisibleIndex="10" Width="20%">
                                <EditItemTemplate>
                                    <uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="InputIMEIData"
                                        Text='<%# Bind("[IMEI]") %>' />
                                    <%--<table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="40px" Text='<%# Bind("[IMEI]") %>'
                                                    Visible="true">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, choose %>" Visible="true" AutoPostBack="false"
                                                    SkinID="PopupButton" ClientSideEvents-Click="function(s,e){openwindow('../SAL/SAL01_inputIMEIData.aspx',500,400);return false;}">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>--%>
                                </EditItemTemplate>
                                <DataItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbl1" runat="server" Text='<%# Bind("[IMEI]") %>' Width="20px"
                                                    Enabled="false" DisabledStyle-Font-Underline="true">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <uc1:PopupControl ID="PopupControl3" runat="server" PopupControlName="InputIMEIData" />
                                            </td>
                                            <%--<td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="40px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, choose %>" Visible="true" AutoPostBack="false"
                                                    SkinID="PopupButton" ClientSideEvents-Click="function(s,e){openwindow('../SAL/SAL01_inputIMEIData.aspx',500,400);return false;}">
                                                </dx:ASPxButton>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                OnClick="btnSearch_Click">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, MixPromotion %>"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e){ openwindow('SAL01_choosePromotions.aspx',700,420);return false;}" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="Button6" runat="server" Text="HappyGo折抵" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutHG.aspx',400,470);return false;}" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="Button7" runat="server" Text="店長折扣" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutSM.aspx',450,200);return false;}" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                        <SettingsEditing Mode="Inline" />
                        <SettingsEditing EditFormColumnCount="4" />
                    </cc:ASPxGridView>
                    <div class="criteria" id="divpay" runat="server">
                        <table>
                            <tr>
                                <td class="tdtxt">
                                    應收總金額 ：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="tdtxt">
                                    原交易金額 ：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="Label7" runat="server" Text="3000"></asp:Label>
                                </td>
                                <td class="tdtxt">
                                    應退差額 ：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="Label8" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Label ID="Label15" Text="<%$ Resources:WebResources, DiscountDetail %>" Visible="false"
                        runat="server" />
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="項次" Visible="false">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="折扣料號" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                                    VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="折扣名稱" Caption="<%$ Resources:WebResources, DiscountName %>"
                                    VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="單價" Caption="<%$ Resources:WebResources, UnitPrice %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="總價" Caption="<%$ Resources:WebResources, TotalPrice %>"
                                    VisibleIndex="6" />
                            </Columns>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                        </cc:ASPxGridView>
                    </div>
                </div>
                <div class="seperate">
                </div>
                <div class="btnPosition">
                    <table align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnConfirm" runat="server" Text="<%$ Resources:WebResources, ExchangeGoodsConfirm %>"
                                    OnClick="btnConfirm_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnConfirm" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <%--        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
            <ContentTemplate>--%>
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvSecond" runat="server" AutoGenerateColumns="false" Width="100%"
                ClientInstanceName="gvSecond" KeyFieldName="付款方式" Settings-ShowTitlePanel="true"
                OnHtmlRowPrepared="gvSecond_HtmlRowPrepared">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <input type="checkbox" onclick="gvSecond.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="status" Caption=" " VisibleIndex="1">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="付款方式" Caption="<%$ Resources:WebResources, PaymentMethod %>"
                        VisibleIndex="2" />
                    <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>"
                        VisibleIndex="3" />
                    <dx:GridViewDataColumn FieldName="付款明細" Caption="<%$ Resources:WebResources, PaymentStatement %>"
                        VisibleIndex="4" />
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                <Templates>
                    <TitlePanel>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnCash" runat="server" Text="<%$ Resources:WebResources, Cash %>"
                                        OnClick="Button1_Click" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){openwindow('../CheckOut/CheckOutCash.aspx',300,200);}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCredit" runat="server" Text="<%$ Resources:WebResources, CreditCard %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutCredit.aspx?Type=2',300,300);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnDivCredit" runat="server" Text="<%$ Resources:WebResources, Instalment %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutCreditStage.aspx',300,300);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnVisaDebit" runat="server" Text="<%$ Resources:WebResources, BankCard %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){openwindow('../CheckOut/CheckOutDebitCard.aspx?Type=2',300,300);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnHappyGo" runat="server" Text="<%$ Resources:WebResources, HappyGo %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){openwindow('../CheckOut/CheckOutHG3.aspx',300,320);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCP" runat="server" Text="<%$ Resources:WebResources, Coupon %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutGift.aspx',300,150);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnOffLineCredit" runat="server" Text="<%$ Resources:WebResources, OffLineCreditCard %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutCreditUnline.aspx',300,200);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnPayDEL" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
            </cc:ASPxGridView>
            <div class="criteria" runat="server" id="divCheckOut">
                <table>
                    <tr>
                        <td class="tdtxt">
                            應付總金額 ：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label9" runat="server"></asp:Label>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="tdtxt">
                            找零 ：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="Button8" runat="server" Text="<%$ Resources:WebResources, ExchangeGoodsCheckOut %>"
                            OnClick="Button8_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button9" runat="server" Text="<%$ Resources:WebResources, ExchangeGoodsCancel %>">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <dx:ASPxPopupControl ClientInstanceName="productExchangePopup" Width="250px" Height="150px"
        ID="ProductExchangePopup" ShowFooter="false" HeaderText="換貨結帳" runat="server" Enabled="false"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" EnableHierarchyRecreation="True">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <asp:Panel ID="Panel2" runat="server">
                    <table width="100%" style="height: 100%">
                        <tr>
                            <td valign="middle" align="left">
                                <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" ClientInstanceName="productExchangeOptions"
                                    runat="server" RepeatDirection="Vertical">
                                    <Items>
                                        <dx:ListEditItem Text="確認原交易發票已收回" Value="1" Selected="true" />
                                        <dx:ListEditItem Text="客戶原發票遺失開立折讓當單" Value="2" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="okButton" runat="server" Text="確認" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e){                                                        
                                                        if(productExchangeOptions.GetSelectedIndex()==1) 
                                                        {
                                                            $('#messageSpan').text('折讓單列印中...');
                                                        }
                                                        else 
                                                        {
                                                            $('#messageSpan').text('發票列印中...');
                                                        }
                                                        productExchangePopup.Hide();
                                                        printPopup.Show();
                                                    }" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="cancelButton" runat="server" Text="取消" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e){
                                                        productExchangePopup.Hide();
                                                    }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ClientInstanceName="printPopup" Width="250px" Height="150px"
        ID="PrintPopup" ShowFooter="false" HeaderText="列印" runat="server"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" EnableHierarchyRecreation="True">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <asp:Panel ID="Panel1" runat="server">
                    <table width="100%" style="height: 100%">
                        <tr>
                            <td valign="middle" align="center">
                                <span id="messageSpan">發票列印中....</span>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents Shown="function(s, e) {
                        // 倒數計時，5秒後自動關閉    
                        secondsRemaining = 5;                  
                        countdownTimer = setTimeout('decreaseTime()', 1000);        
                    }" />
    </dx:ASPxPopupControl>
</asp:Content>
