<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01.aspx.cs" Inherits="VSS_SA01_SA01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeicheckbox(con) {
            if (con.checked) {
                openwindow("SAL01_inputIMEIData.aspx", 720, 300);
            }
        }

        function checkID() {
            var vID = document.getElementById("tbInvoiceNo").value;
            if (vID.length != 8 && vID > 0) {
                openwindow("SAL01_checkIDNumber.aspx", 300, 200);
                return false;
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--銷售作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Selling %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClientClick="document.location='SAL02.aspx';return false;" />
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, UnclearedTradeListing %>" OnClientClick="document.location='SAL05.aspx';return false;" />
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
                        <asp:TextBox ID="tbInvoiceNo" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
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
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--備註-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:TextBox ID="TextBox8" runat="server" CssClass="tbWidthFormat"></asp:TextBox>
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
            <div class="SubEditCommand">
                <asp:Button ID="btnItemAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                    OnClick="btnItemAdd_Click" />
                <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, SalesFromPreOrder %>"
                    OnClientClick="openwindow('SAL01_inputPreOrderNumber.aspx',520,380);return false;" />
                <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, MixPromotion %>"
                    OnClientClick="openwindow('SAL01_choosePromotions.aspx',700,420);return false;" />
                <asp:Button ID="Button6" runat="server" Text="HappyGo折抵" OnClientClick="openwindow('../CheckOut/CheckOutHG.aspx',400,420);return false;" />
                <asp:Button ID="Button7" runat="server" Text="店長折扣" OnClientClick="openwindow('../CheckOut/CheckOutSM.aspx',450,200);return false;" />
            </div>
            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                OnRowDataBound="gvMaster_RowDataBound">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col">
                            &nbsp;
                        </th>
                        <th scope="col">
                            <!--類別-->
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--商品編號-->
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--商品名稱-->
                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--數量-->
                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Quantity %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--單價-->
                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, UnitPrice %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--總價-->
                            <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, TotalPrice %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--IMEI-->
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, Imei %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--促銷名稱-->
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td colspan="9" style="height: 50px; text-align: center; vertical-align: middle">
                            <!--請新增資料-->
                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckAll" runat="server" CssClass="Class1" onclick="javascript:if(this.checked){$('.Class1').checkCheckboxes();}else{$('.Class1').unCheckCheckboxes();}"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckItem" runat="server" CssClass="Class1"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, Category %>">
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("類別") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("商品編號") %>'></asp:TextBox>
                            <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                OnClientClick="openwindow('SAL01_searchProductNo.aspx');return false;" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="Label99" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, Quantity %>">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("數量") %>' Width="20px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, UnitPrice %>">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("單價") %>' Width="40px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="總價" HeaderText="<%$ Resources:WebResources, TotalPrice %>" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbIMEI" runat="server" Checked='<%#Eval("CHECK").ToString() == "1" ? true:false %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, Imei %>">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("IMEI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, PromotionName %>">
                        <ItemTemplate>
                            <asp:Label ID="TextBox7" runat="server" Text='<%# Bind("促銷名稱") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="txt">
            <!--應收總金額-->
            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>"></asp:Literal>：
            <asp:Label ID="Label2" runat="server" /></div>
        <div class="seperate">
        </div>
        <asp:Label ID="Label8" Text="<%$ Resources:WebResources, DiscountDetail %>" Visible="false"
            runat="server" />
        <div class="SubEditBlock">
            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col">
                            <!--項次-->
                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--折扣料號-->
                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--折扣名稱-->
                            <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--數量-->
                            <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, Quantity %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--單價-->
                            <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, UnitPrice %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--總價-->
                            <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, TotalPrice %>"></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td colspan="6" class="tdEmptyData">
                            <!--請選擇支付方式-->
                            <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" />
                    <asp:BoundField DataField="折扣料號" HeaderText="<%$ Resources:WebResources, PartNumberOfDiscount %>" />
                    <asp:BoundField DataField="折扣名稱" HeaderText="<%$ Resources:WebResources, DiscountName %>" />
                    <asp:BoundField DataField="數量" HeaderText="<%$ Resources:WebResources, Quantity %>" />
                    <asp:BoundField DataField="單價" HeaderText="<%$ Resources:WebResources, UnitPrice %>" />
                    <asp:BoundField DataField="總價" HeaderText="<%$ Resources:WebResources, TotalPrice %>" />
                </Columns>
            </asp:GridView>
            <table cellpadding="0" width="100%">
                <tr>
                    <td style="width: 70%">
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <div class="SubEditCommand">
                <asp:Button ID="btnCash" runat="server" Text="<%$ Resources:WebResources, Cash %>"
                    OnClientClick="openwindow('../CheckOut/CheckOutCash.aspx',300,200);" OnClick="btnCash_Click" />
                <asp:Button ID="btnCredit" runat="server" Text="<%$ Resources:WebResources, CreditCard %>"
                    OnClientClick="openwindow('../CheckOut/CheckOutCredit.aspx',300,300);return false;" />
                <asp:Button ID="btnOffLineCredit" runat="server" Text="<%$ Resources:WebResources, OffLineCreditCard %>"
                    OnClientClick="openwindow('../CheckOut/CheckOutCreditUnline.aspx',300,200);return false;" />
                <asp:Button ID="btnDivCredit" runat="server" Text="<%$ Resources:WebResources, Instalment %>"
                    OnClientClick="openwindow('../CheckOut/CheckOutCreditStage.aspx',300,300);return false;" />
                <asp:Button ID="btnCP" runat="server" Text="<%$ Resources:WebResources, Coupon %>"
                    OnClientClick="openwindow('../CheckOut/CheckOutGift.aspx',300,150);return false;" />
                <asp:Button ID="btnVisaDebit" runat="server" Text="<%$ Resources:WebResources, BankCard %>"
                    OnClientClick="openwindow('../CheckOut/CheckOutDebitCard.aspx',300,300);return false;" />
                <asp:Button ID="btnHappyGo" runat="server" Text="<%$ Resources:WebResources, HappyGo %>"
                    OnClientClick="openwindow('../CheckOut/CheckOutHG2.aspx?Type=2',300,320);return false;" />
                <asp:Button ID="btnPayDEL" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            </div>
            <asp:GridView ID="gvCheckOut" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col">
                            <!--付款方式-->
                            <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--金額-->
                            <asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, Amount %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--付款明細-->
                            <asp:Literal ID="Literal30" runat="server" Text="<%$ Resources:WebResources, PaymentStatement %>"></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td colspan="8" class="tdEmptyData">
                            <!--請選擇支付方式-->
                            <asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckAll" runat="server" CssClass="Class2" onclick="javascript:if(this.checked){$('.Class2').checkCheckboxes();}else{$('.Class2').unCheckCheckboxes();}"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckItem" runat="server" CssClass="Class2"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="付款方式" HeaderText="<%$ Resources:WebResources, PaymentMethod %>" />
                    <asp:BoundField DataField="金額" HeaderText="<%$ Resources:WebResources, Amount %>" />
                    <asp:BoundField DataField="付款明細" HeaderText="<%$ Resources:WebResources, PaymentStatement %>" />
                </Columns>
            </asp:GridView>
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
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnOrderCheckOut" runat="server" Text="<%$ Resources:WebResources, CheckOut %>"
                OnClick="btnOrderCheckOut_Click" OnClientClick="return checkID();" />
            <asp:Button ID="btnOrderCancel" runat="server" Text="<%$ Resources:WebResources, CancelTransaction %>" />
            <asp:Button ID="btnReprint" runat="server" Text="<%$ Resources:WebResources, ReprintSalesSlip %>" />
        </div>
    </div>
    </form>
</body>
</html>
