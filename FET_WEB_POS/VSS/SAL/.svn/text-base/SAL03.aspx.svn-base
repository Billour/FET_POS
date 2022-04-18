<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL03.aspx.cs" Inherits="VSS_SAL_SAL03" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        </style>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=no,scrollbars=no,location=no,toolbar=no,status=no');
        }
        function imeicheckbox(con) {
            if (con.checked) {
                openwindow("SAL01_inputIMEIData.aspx");
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--換貨作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, GoodsExchanging %>" />
                </td>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClientClick="document.location='SAL02.aspx';return false;" />
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
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
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
        <!--
        <div class="SubEditBlock">
            <div class="GridScrollBar">
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                原交易日期
                            </th>
                            <th scope="col">
                                原交易序號
                            </th>
                            <th scope="col">
                                類別
                            </th>
                            <th scope="col">
                                商品編號
                            </th>
                            <th scope="col">
                                商品名稱
                            </th>
                            <th scope="col">
                                數量
                            </th>
                            <th scope="col">
                                單價
                            </th>
                            <th scope="col">
                                總價
                            </th>
                            <th scope="col">
                                IMEI
                            </th>
                            <th scope="col">
                                備註
                            </th>
                        </tr>
                        <tr>
                            <td colspan="10" class="tdEmptyData">
                                此無明細資料
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="原交易日期" HeaderText="原交易日期" />
                        <asp:BoundField DataField="原交易序號" HeaderText="原交易序號" />
                        <asp:BoundField DataField="類別" HeaderText="類別" />
                        <asp:BoundField DataField="商品編號" HeaderText="商品編號" />
                        <asp:BoundField DataField="商品名稱" HeaderText="商品名稱" />
                        <asp:BoundField DataField="數量" HeaderText="數量" />
                        <asp:BoundField DataField="單價" HeaderText="單價" />
                        <asp:BoundField DataField="總價" HeaderText="總價" />
                        <asp:BoundField DataField="IMEI" HeaderText="IMEI" />
                        <asp:BoundField DataField="備註" HeaderText="備註" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        -->
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Add %>"
                            OnClick="btnSearch_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                        <asp:Button ID="Button3" runat="server" Text="組合促銷" OnClientClick="openwindow('SAL01_choosePromotions.aspx',700,420);return false;" />
                        <asp:Button ID="Button6" runat="server" Text="HappyGo折抵" OnClientClick="openwindow('../CheckOut/CheckOutHG.aspx',400,420);return false;" />
                        <asp:Button ID="Button7" runat="server" Text="店長折扣" OnClientClick="openwindow('../CheckOut/CheckOutSM.aspx',450,200);return false;" />
                    </div>
                    <div class="GridScrollBar">
                        <asp:GridView ID="gvMasterEdit" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMasterEdit_RowCancelingEdit" OnRowEditing="gvMasterEdit_RowEditing"
                            OnRowUpdating="gvMasterEdit_RowUpdating" OnRowDataBound="gvMasterEdit_RowDataBound">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        類別
                                    </th>
                                    <th scope="col">
                                        商品編號
                                    </th>
                                    <th scope="col">
                                        商品名稱
                                    </th>
                                    <th scope="col">
                                        數量
                                    </th>
                                    <th scope="col">
                                        單價
                                    </th>
                                    <th scope="col">
                                        總價
                                    </th>
                                    <th scope="col">
                                        IMEI
                                    </th>
                                    <th scope="col">
                                        促銷名稱
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="8" class="tdEmptyData">
                                        Choose add Button
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="類別" HeaderText="類別" />
                                <asp:TemplateField HeaderText="商品編號" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("商品編號") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("商品編號") %>' Width="70%"></asp:TextBox>
                                        <asp:Button ID="Button9" runat="server" Text="選" OnClientClick="openwindow('SAL01_searchProductNo.aspx');return false;" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="商品名稱" HeaderText="商品名稱" />
                                <asp:TemplateField HeaderText="數量">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("數量") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="單價" HeaderText="單價" />
                                <asp:BoundField DataField="總價" HeaderText="總價" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <itemtemplate> <asp:CheckBox ID="cbIMEI" runat="server"    /></itemtemplate>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="IMEI" HeaderText="IMEI" />
                                <asp:BoundField DataField="促銷名稱" HeaderText="促銷名稱" />
                            </Columns>
                        </asp:GridView>
                    </div>
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnConfirm" runat="server" Text="換貨商品確認" OnClick="btnConfirm_Click" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnCash" runat="server" Text="現金" OnClientClick="openwindow('../CheckOut/CheckOutCash.aspx',200,100);"
                            OnClick="Button1_Click" />
                        <asp:Button ID="btnCredit" runat="server" Text="信用卡" OnClientClick="openwindow('../CheckOut/CheckOutCredit.aspx',300,300);return false;" />
                        <asp:Button ID="btnOffLineCredit" runat="server" Text="離線信用卡" OnClientClick="openwindow('../CheckOut/CheckOutCreditUnline.aspx',300,200);return false;" />
                        <asp:Button ID="btnDivCredit" runat="server" Text="分期付款" OnClientClick="openwindow('../CheckOut/CheckOutCreditStage.aspx',300,300);return false;" />
                        <asp:Button ID="btnCP" runat="server" Text="禮券" OnClientClick="openwindow('../CheckOut/CheckOutGift.aspx',300,150);return false;" />
                        <asp:Button ID="btnVisaDebit" runat="server" Text="金融卡" OnClientClick="openwindow('../CheckOut/CheckOutDebitCard.aspx',300,300);return false;" />
                        <asp:Button ID="btnHappyGo" runat="server" Text="HappyGo" OnClientClick="openwindow('../CheckOut/CheckOutHG.aspx?Type=2',400,300);return false;" />
                        <asp:Button ID="btnPayDEL" runat="server" Text="刪除" />
                    </div>
                    <div class="GridScrollBar">
                        <asp:GridView ID="gvSecond" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowDataBound="gvSecond_RowDataBound">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        付款方式
                                    </th>
                                    <th scope="col">
                                        金額
                                    </th>
                                    <th scope="col">
                                        付款明細
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="8" class="tdEmptyData">
                                        choose add button
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckItem" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="付款方式" HeaderText="付款方式" />
                                <asp:BoundField DataField="金額" HeaderText="金額" />
                                <asp:BoundField DataField="付款明細" HeaderText="付款明細" />
                            </Columns>
                        </asp:GridView>
                    </div>
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
            <asp:Button ID="Button8" runat="server" Text="換貨結帳" OnClick="Button8_Click" />
            <asp:Button ID="Button9" runat="server" Text="換貨取消" />
        </div>
    </div>
    </form>
</body>
</html>
