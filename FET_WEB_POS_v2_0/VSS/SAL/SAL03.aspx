<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SAL03.aspx.cs" Inherits="VSS_SAL_SAL03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
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
    <div class="func">
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--交易序號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />：
                    </td>
                    <td class="tdval">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Label ID="Label3" runat="server" Text="00-未存檔"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
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
                        <!--更新日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>" />：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label5" runat="server" Text="2010/07/01 22:00"></asp:Label>
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
                        <!--更新人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>" />：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
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
                    <div class="SubEditCommand" style="text-align: left">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                        OnClick="btnSearch_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, MixPromotion %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('SAL01_choosePromotions.aspx',700,420);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="Button6" runat="server" Text="HappyGo折抵" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutHG.aspx',400,470);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="Button7" runat="server" Text="店長折扣" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutSM.aspx',450,200);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <dx:ASPxGridView ID="gvMasterEdit" runat="server" ClientInstanceName="gvMasterEdit"
                        KeyFieldName="項次" AutoGenerateColumns="False" Width="100%" OnRowInserting="gvMasterEdit_RowInserting">
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
                            <dx:GridViewDataColumn FieldName="類別" Caption="<%$ Resources:WebResources, Category %>"
                                VisibleIndex="1">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                                <EditFormSettings VisibleIndex="1" />
                                <EditFormSettings Caption="類別" VisibleIndex="1" />
                                <EditItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="選" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e){openwindow('SAL01_searchProductNo.aspx',400,350);return false;}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                VisibleIndex="2">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                                <EditFormSettings VisibleIndex="2" />
                                <EditFormSettings Caption="商品編號" VisibleIndex="2" />
                                <EditItemTemplate>
                                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server">
                                    </dx:ASPxTextBox>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="3">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                                <EditFormSettings VisibleIndex="3" />
                                <EditFormSettings Caption="商品名稱" VisibleIndex="3" />
                                <EditItemTemplate>
                                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server">
                                    </dx:ASPxTextBox>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>"
                                VisibleIndex="4">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                                <EditFormSettings VisibleIndex="4" />
                                <EditFormSettings Caption="數量" VisibleIndex="4" />
                                <EditItemTemplate>
                                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server">
                                    </dx:ASPxTextBox>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="單價" Caption="<%$ Resources:WebResources, UnitPrice %>"
                                VisibleIndex="5">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                                <EditFormSettings VisibleIndex="5" />
                                <EditFormSettings Caption="單價" VisibleIndex="5" />
                                <EditItemTemplate>
                                    <dx:ASPxTextBox ID="ASPxTextBox5" runat="server">
                                    </dx:ASPxTextBox>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="總價" Caption="<%$ Resources:WebResources, TotalPrice %>"
                                VisibleIndex="6">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                                <EditFormSettings VisibleIndex="6" />
                                <EditFormSettings Caption="總價" VisibleIndex="6" />
                                <EditItemTemplate>
                                    <dx:ASPxTextBox ID="ASPxTextBox6" runat="server">
                                    </dx:ASPxTextBox>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="" VisibleIndex="7">
                                <DataItemTemplate>
                                    <dx:ASPxCheckBox ID="chkIMEI" runat="server" Checked="<%# Bind('CHECK') %>" Enabled="false">
                                    </dx:ASPxCheckBox>
                                </DataItemTemplate>
                                <EditFormSettings Caption="IMEI" VisibleIndex="7" />
                                <EditItemTemplate>
                                    <dx:ASPxCheckBox ID="chkIMEI1" runat="server" ClientInstanceName="chkIMEI1" AutoPostBack="false">
                                        <ClientSideEvents CheckedChanged="function(s,e){imeicheckbox(s);}" />
                                    </dx:ASPxCheckBox>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>"
                                VisibleIndex="8">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                                <EditFormSettings VisibleIndex="8" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>"
                                VisibleIndex="9">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                                <EditFormSettings VisibleIndex="9" />
                                <EditFormSettings Caption="促銷名稱" VisibleIndex="9" />
                                <EditItemTemplate>
                                    <dx:ASPxTextBox ID="ASPxTextBox9" runat="server">
                                    </dx:ASPxTextBox>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                        <SettingsEditing Mode="EditForm" />
                        <SettingsEditing EditFormColumnCount="4" />
                    </dx:ASPxGridView>
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
                                    應退/應補 差額 ：
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
                    <asp:Button ID="btnConfirm" runat="server" Text="換貨商品確認" OnClick="btnConfirm_Click" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnConfirm" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <div class="SubEditCommand" style="text-align: left">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnCash" runat="server" Text="<%$ Resources:WebResources, Cash %>"
                                        OnClick="Button1_Click" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){openwindow('../CheckOut/CheckOutCash.aspx',300,200);}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCredit" runat="server" Text="<%$ Resources:WebResources, CreditCard %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutCredit.aspx',300,300);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnOffLineCredit" runat="server" Text="<%$ Resources:WebResources, OffLineCreditCard %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutCreditUnline.aspx',300,200);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnDivCredit" runat="server" Text="<%$ Resources:WebResources, Instalment %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutCreditStage.aspx',300,300);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCP" runat="server" Text="<%$ Resources:WebResources, Coupon %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutGift.aspx',300,150);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnVisaDebit" runat="server" Text="<%$ Resources:WebResources, BankCard %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){openwindow('../CheckOut/CheckOutDebitCard.aspx',300,300);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnHappyGo" runat="server" Text="<%$ Resources:WebResources, HappyGo %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){openwindow('../CheckOut/CheckOutHG.aspx?Type=2',300,320);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnPayDEL" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <dx:ASPxGridView ID="gvSecond" runat="server" AutoGenerateColumns="false" Width="100%"
                        ClientInstanceName="gvSecond" KeyFieldName="付款方式">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <input type="checkbox" onclick="gvSecond.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="付款方式" Caption="<%$ Resources:WebResources, PaymentMethod %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>"
                                VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="付款明細" Caption="<%$ Resources:WebResources, PaymentStatement %>"
                                VisibleIndex="3" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                    </dx:ASPxGridView>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="Button8" runat="server" Text="換貨結帳" OnClick="Button8_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button9" runat="server" Text="換貨取消">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
