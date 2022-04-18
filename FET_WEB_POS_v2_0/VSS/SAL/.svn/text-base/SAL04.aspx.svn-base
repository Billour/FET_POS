<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SAL04.aspx.cs" Inherits="VSS_SAL04_SAL04" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=no,scrollbars=no,location=no,toolbar=no,status=no');
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="func">
        <div class="titlef" style="text-align: left">
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
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
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
                    <td class="tdval" nowrap="nowrap">
                        AK00001
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--日期-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Date %>" />：
                    </td>
                    <td class="tdval">
                        2010/06/18 16:00
                    </td>
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
                    <td class="tdval" nowrap="nowrap">
                        64591 李家駿
                    </td>
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
                    <td class="tdtxt" nowrap="nowrap">
                        HG卡號：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="Label13" runat="server" Text="N1234567890123456"></asp:Label>
                    </td>
                    <td>
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
            <dx:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="項次"
                AutoGenerateColumns="False" Width="100%">
                <Styles Header-HorizontalAlign="Center">
                </Styles>
                <Columns>
                    <dx:GridViewDataColumn FieldName="類別" Caption="<%$ Resources:WebResources, Category %>"
                        VisibleIndex="1">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                        VisibleIndex="2">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                        VisibleIndex="3">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>"
                        VisibleIndex="4">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="單價" Caption="<%$ Resources:WebResources, UnitPrice %>"
                        VisibleIndex="5">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="總價" Caption="<%$ Resources:WebResources, TotalPrice %>"
                        VisibleIndex="6">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>"
                        VisibleIndex="7">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>"
                        VisibleIndex="8">
                    </dx:GridViewDataColumn>
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
            </dx:ASPxGridView>
        </div>
        <div class="txt" style="text-align: left">
            <!--已付總金額-->
            <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, TotalAmountPaid %>" />：
            <asp:Label ID="Label2" runat="server" Text="14290"></asp:Label>元</div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" AutoGenerateColumns="False"
                Width="100%" KeyFieldName="項次">
                <Styles Header-HorizontalAlign="Center">
                </Styles>
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
        <div class="txt">
            <asp:Label ID="Label33" runat="server" Height="20"></asp:Label>
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvCheckOut" runat="server" AutoGenerateColumns="false" Width="100%"
                ClientInstanceName="gvCheckOut" KeyFieldName="付款方式">
                <Styles Header-HorizontalAlign="Center">
                </Styles>
                <Columns>
                    <dx:GridViewDataColumn FieldName="付款方式" Caption="<%$ Resources:WebResources, PaymentMethod %>"
                        VisibleIndex="1" />
                    <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>"
                        VisibleIndex="2" />
                    <dx:GridViewDataColumn FieldName="付款明細" Caption="<%$ Resources:WebResources, PaymentStatement %>"
                        VisibleIndex="3" />
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
            </cc:ASPxGridView>
            <div class="txt" style="text-align: left">
                <!--應退總金額-->
                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, TotalRefundDue %>" />：
                <asp:Label ID="lbPayAmount" runat="server" Text="14290"></asp:Label>元</div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnVoid" runat="server" Text="確定作廢" OnClick="btnVoid_Click">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
