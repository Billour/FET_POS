<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SAL01.aspx.cs" Inherits="VSS_SA01_SA01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeicheckbox(con) {
            if (con.GetChecked()) {
                openwindow("SAL01_inputIMEIData.aspx", 520, 300);
            }
        }

        function checkID() {
            var vID = document.getElementById("<%=tbInvoiceNo.ClientID %>" + "_I").value;
            if (vID.length != 8 && vID.length > 0) {
                openwindow("SAL01_checkIDNumber.aspx", 300, 200);
                return false;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--銷售作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Selling %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ document.location='SAL02.aspx'; return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, UnclearedTradeListing %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ document.location='SAL05.aspx'; return false;}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--交易序號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lbTSNo" runat="server" />
                    </td>
                    <td class="tdtxt">
                        <!--單據類別-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SalesSlipType %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <!--發票/收據-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, InvoiceOrReceipt %>"></asp:Literal>：
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label runat="server" ID="lbStatus" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--交易日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label runat="server" ID="lbUpdate" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--發票號碼-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lbInvoiceNo" runat="server" />
                    </td>
                    <td class="tdtxt">
                        <!--統一編號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="tbInvoiceNo" runat="server" CssClass="tbWidthFormat" ClientInstanceName="tbInvoiceNo">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label6" runat="server" Text="64591 李家駿"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--發票抬頭-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" CssClass="tbWidthFormat">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--備註-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" CssClass="tbWidthFormat">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        HG卡號：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="N1234567890123456"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        HG剩餘點數：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>
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
        <div class="SubEditBlock">
            <div class="SubEditCommand" style="text-align: left">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnItemAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                OnClick="btnItemAdd_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnItemDel" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton5" runat="server" Text="預收款轉銷售"
                                AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){ openwindow('SAL01_inputPreOrderNumber.aspx',520,380);return false;}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton6" runat="server" Text="<%$ Resources:WebResources, MixPromotion %>"
                                AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){ openwindow('SAL01_choosePromotions.aspx',700,420);return false;}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton7" runat="server" Text="HappyGo折抵" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutHG.aspx',400,470);return false;}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton8" runat="server" Text="店長折扣" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutSM.aspx',450,200);return false;}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="商品編號"
                        AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="類別" Caption="<%$ Resources:WebResources, Category %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="3" />
                            <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>"
                                VisibleIndex="4" />
                            <dx:GridViewDataColumn FieldName="單價" Caption="<%$ Resources:WebResources, UnitPrice %>"
                                VisibleIndex="5" />
                            <dx:GridViewDataColumn FieldName="總價" Caption="<%$ Resources:WebResources, TotalPrice %>"
                                VisibleIndex="6" />
                            <dx:GridViewDataColumn Caption="" VisibleIndex="7">
                                <DataItemTemplate>
                                    <dx:ASPxCheckBox ID="chkIMEI" runat="server" Checked="<%# Bind('CHECK') %>" AutoPostBack="false">
                                        <ClientSideEvents CheckedChanged="function(s,e){imeicheckbox(s);}" />
                                    </dx:ASPxCheckBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>"
                                VisibleIndex="8" />
                            <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>"
                                VisibleIndex="9" />
                        </Columns>
                        <Templates>
                            <EmptyDataRow>
                                <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                            </EmptyDataRow>
                        </Templates>
                    </cc:ASPxGridView>
                    <div class="txt" style="text-align: left">
                        <!--應收總金額-->
                        <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>"></asp:Literal>：
                        <asp:Label ID="Label2" runat="server" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnItemAdd" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnItemDel" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div style="text-align: left">
                        <asp:Label ID="Label8" Text="<%$ Resources:WebResources, DiscountDetail %>" runat="server" />
                    </div>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvDetail" runat="server" AutoGenerateColumns="False" Width="100%"
                            KeyFieldName="項次" Visible="false">
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
                    <div class="seperate">
                    </div>
                    <div class="SubEditBlock">
                        <div class="SubEditCommand" style="text-align: left">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnCash" runat="server" Text="<%$ Resources:WebResources, Cash %>"
                                            OnClick="btnCash_Click" AutoPostBack="false">
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
                                        <dx:ASPxButton ID="btnDivCredit" runat="server" Text="<%$ Resources:WebResources, Instalment %>"
                                            AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutCreditStage.aspx',300,300);return false;}" />
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
                                        <dx:ASPxButton ID="btnCP" runat="server" Text="<%$ Resources:WebResources, Coupon %>"
                                            AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutGift.aspx',300,150);return false;}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnOffLineCredit" runat="server" Text="<%$ Resources:WebResources, OffLineCreditCard %>"
                                            AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutCreditUnline.aspx',300,200);return false;}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnPayDEL" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <dx:ASPxGridView ID="gvCheckOut" runat="server" AutoGenerateColumns="false" Width="100%"
                            ClientInstanceName="gvCheckOut" KeyFieldName="付款方式">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvCheckOut.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
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
                        <div style="text-align: left">
                            <table cellpadding="0" width="100%">
                                <tr>
                                    <td style="width: 70%">
                                        <!--應付總金額-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, TotalAmountDue%>"></asp:Literal>：
                                        <asp:Label ID="lbPayAmount" runat="server" />
                                    </td>
                                    <td>
                                        <!--找零-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Change %>"></asp:Literal>：<asp:Label
                                            runat="server" ID="lbChange"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCash" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnOrderCheckOut" runat="server" Text="<%$ Resources:WebResources, CheckOut %>"
                                OnClick="btnOrderCheckOut_Click">
                                <ClientSideEvents Click="function(s,e){checkID();}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnOrderCancel" runat="server" Text="<%$ Resources:WebResources, CancelTransaction %>">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReprint" runat="server" Text="<%$ Resources:WebResources, ReprintSalesSlip %>">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
