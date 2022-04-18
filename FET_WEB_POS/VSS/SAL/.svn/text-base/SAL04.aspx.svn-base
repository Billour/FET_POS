<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL04.aspx.cs" Inherits="VSS_SAL04_SAL04" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=no,scrollbars=no,location=no,toolbar=no,status=no');
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div class="titlef">
            <!--銷售作廢作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, SalesCancel %>" /></div>
        <div class="criteria">
            <table>
                <tr>
                <td class="tdtxt" nowrap="nowrap">
                        <!--作廢序號-->
                        <asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, CancelNo %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="Label6" runat="server" Text="1234-01-100912345" Visible="False"></asp:Label>
                    </td>
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                </tr>
                <tr>
                     <td class="tdtxt" nowrap="nowrap">
                        <!--作廢日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CancelDate %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="Label1" runat="server" Text="2010/07/01"></asp:Label>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--單據類別-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SalesSlipType %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="Label4" runat="server" Text="發票/收據"></asp:Label>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="Label5" runat="server" Text="已結案"></asp:Label>
                    </td>
                </tr>
                <tr>
                     </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--原交易序號-->
                        <asp:Literal ID="Literal6" runat="server" Text="原交易序號" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">AK00001</td>
                    <td></td>
                    <td></td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--日期-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Date %>" />：
                    </td>
                    <td class="tdval">2010/06/18 16:00</td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--發票號碼-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="lbInvoiceNo" runat="server" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--統一編號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="TextBox2" runat="server" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Staff %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">64591 李家駿</td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--發票抬頭-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>" />：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="lbInvoiceTitle" runat="server" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--備註-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Remark %>" />：
                    </td>
                    <td class="tdval" colspan="2" nowrap="nowrap">
                        <asp:Label ID="lblRemark" runat="server" />
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">HG卡號：</td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="Label13" runat="server" Text="N1234567890123456"></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col" nowrap="nowrap">
                            <!--類別-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Category %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--商品編號-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--商品名稱-->
                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--數量-->
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Quantity %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--單價-->
                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, UnitPrice %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--總價-->
                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, TotalPrice %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--IMEI-->
                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Imei %>" />
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--促銷名稱-->
                             <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />
                        </th>
                    </tr>
                    <tr>
                        <td colspan="8" class="tdEmptyData">
                            <!--此無明細資料-->
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="類別" HeaderText="<%$ Resources:WebResources, Category %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="數量" HeaderText="<%$ Resources:WebResources, Quantity %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="單價" HeaderText="<%$ Resources:WebResources, UnitPrice %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="總價" HeaderText="<%$ Resources:WebResources, TotalPrice %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="IMEI" HeaderText="<%$ Resources:WebResources, Imei %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="促銷名稱" HeaderText="<%$ Resources:WebResources, PromotionName %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                </Columns>
            </asp:GridView>
        </div>
        <div class="txt">
            <!--已付總金額-->
            <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, TotalAmountPaid %>" />：
            <asp:Label ID="Label2" runat="server" Text="14290"></asp:Label>元</div>
        <div class="seperate">
        </div>

        <div class="SubEditBlock">
            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col" nowrap="nowrap">
                            <!--項次-->
                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--折扣料號-->
                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--折扣名稱-->
                            <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--數量-->
                            <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, Quantity %>"></asp:Literal>
                        </th>
                        <th scope="col" nowrap="nowrap">
                            <!--單價-->
                            <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, UnitPrice %>"></asp:Literal>
                        </th>
                        <th scope="col" nowrap="nowrap">
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
                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="折扣料號" HeaderText="<%$ Resources:WebResources, PartNumberOfDiscount %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="折扣名稱" HeaderText="<%$ Resources:WebResources, DiscountName %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="數量" HeaderText="<%$ Resources:WebResources, Quantity %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="單價" HeaderText="<%$ Resources:WebResources, UnitPrice %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    <asp:BoundField DataField="總價" HeaderText="<%$ Resources:WebResources, TotalPrice %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                </Columns>
            </asp:GridView>
        </div>
        <div class="txt">
            <asp:Label ID="Label33" runat="server" Height="20"></asp:Label>
        </div>
        <div class="seperate">
        </div>

        <div class="SubEditBlock">
            <div class="GridScrollBar">
                <asp:GridView ID="gvCheckOut" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                    <Columns>
                        <asp:BoundField DataField="付款方式" HeaderText="<%$ Resources:WebResources, PaymentMethod %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                        <asp:BoundField DataField="金額" HeaderText="<%$ Resources:WebResources, Amount %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                        <asp:BoundField DataField="付款明細" HeaderText="<%$ Resources:WebResources, PaymentStatement %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="txt">
                <!--應退總金額-->
                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, TotalRefundDue %>" />：
                <asp:Label ID="lbPayAmount" runat="server" Text="14290"></asp:Label>元</div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnVoid" runat="server" Text="<%$ Resources:WebResources, ConfirmCancel %>" OnClick="btnVoid_Click" />
        </div>
    </div>
    </form>
</body>
</html>
