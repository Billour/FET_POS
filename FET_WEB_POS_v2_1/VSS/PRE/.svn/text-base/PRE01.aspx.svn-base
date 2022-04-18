<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PRE01.aspx.cs" Inherits="VSS_PRE_PRE01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--預購作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderOperation1 %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" />
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--預購單號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PreOrderSheetNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label9" runat="server"></asp:Label>
                </td>
                <td class="tdtxt">
                    <span style="color: Red">*</span><!--客戶身份證號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CustomerIdentifyNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox7" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--狀態-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label1" runat="server" Text="00-未存檔"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--發票號碼-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lbInvoceNo" runat="server" />
                </td>
                <td class="tdtxt">
                    <span style="color: Red">*</span><!--客戶姓名-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--更新日期-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label3" runat="server" Text="2010/07/01 22:00"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--發票抬頭-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--客戶門號-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox4" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--維護人員-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--統一編號-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox5" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <span style="color: Red">*</span><!--聯絡電話-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox8" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--啟用類型-->
                    <span style="color: Red">*</span><asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ActivationType %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList1" runat="server" Width="100">
                        <Items>
                            <dx:ListEditItem Value="新啟用" Text="新啟用" Selected="true" />
                            <dx:ListEditItem Value="續約" Text="續約" />
                            <dx:ListEditItem Value="MNP" Text="MNP" />
                            <dx:ListEditItem Value="其他" Text="其他" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--e-Mail-->
                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Email %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <dx:ASPxTextBox ID="TextBox6" runat="server" Width="98%">
                    </dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="SubEditBlock">
        <div class="GridScrollBar" style="height: auto">
            <cc:ASPxGridView ClientInstanceName="gvMaster" ID="gvMaster" runat="server" Width="100%"
                KeyFieldName="商品料號">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <HeaderTemplate>
                            <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="2"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="預收款說明" Caption="預收款說明" VisibleIndex="3"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="預購金額" Caption="<%$ Resources:WebResources, PreOrderAmount %>" VisibleIndex="4"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>" VisibleIndex="5"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="總價" Caption="<%$ Resources:WebResources, TotalPrice %>" VisibleIndex="6"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="備註" Caption="<%$ Resources:WebResources, RemarksLimitedTo50Chars %>"></dx:GridViewDataColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                            <tr>
                                <td align="right">
                                    <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                        OnClick="Button1_Click" />
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                    <FooterRow>
                        <div class="txt" align="left">
                            <!--應收總金額-->
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>"></asp:Literal>：<asp:Label
                                ID="Label5" runat="server" Text="10000"></asp:Label></div>
                    </FooterRow>
                    <EmptyDataRow>
                        <!--choose add button-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                    </EmptyDataRow>
                </Templates>
                <Settings ShowTitlePanel="True" ShowFooter="True" />
            </cc:ASPxGridView>
        </div>
    </div>
    <div class="seperate">
    </div>
    <div>
        <div class="SubEditBlock">
            <div class="GridScrollBar" style="height: auto">
                <cc:ASPxGridView ID="gvMaster2" ClientInstanceName="gvMaster2" runat="server" Width="100%"
                    KeyFieldName="付款方式">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <input type="checkbox" onclick="gvMaster2.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="付款方式" Caption="<%$ Resources:WebResources, PaymentMethod %>" VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>" VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="付款明細" Caption="<%$ Resources:WebResources, PaymentStatement %>" VisibleIndex="3" />
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table cellpadding="0" cellspacing="0" border="0" align="left">
                                <tr>
                                    <td align="right">
                                        <dx:ASPxButton ID="btnCash" runat="server" Text="<%$ Resources:WebResources, Cash %>"
                                            OnClick="Button3_Click">
                                            <ClientSideEvents Click="function(s, e) {
	                                                openwindow('../CheckOut/CheckOutCash.aspx',100,100);
                                                }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <dx:ASPxButton ID="btnCredit" runat="server" Text="<%$ Resources:WebResources, CreditCard %>"
                                            OnClick="btnCredit_Click">
                                            <ClientSideEvents Click="function(s, e) {
                                                    openwindow('../CheckOut/CheckOutCredit.aspx',300,300);
                                                }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <dx:ASPxButton ID="btnVisaDebit" runat="server" Text="<%$ Resources:WebResources, BankCard %>">
                                            <ClientSideEvents Click="function(s, e) {
	                                                openwindow('../CheckOut/CheckOutDebitCard.aspx',300,300);return false;
                                                }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <dx:ASPxButton ID="btnOffLineCredit" runat="server" Text="<%$ Resources:WebResources, OffLineCreditCard %>">
                                            <ClientSideEvents Click="function(s, e) {
	                                                openwindow('../CheckOut/CheckOutCreditUnline.aspx',300,200);
                                                }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <dx:ASPxButton ID="btnPayDEL" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                        <FooterRow>
                            <div align="left">
                                <!--訂金金額-->
                                <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, DepositPaid %>"></asp:Literal>：<asp:Label
                                    ID="Label2" runat="server" Text="10000"></asp:Label>
                            </div>
                        </FooterRow>
                        <EmptyDataRow>
                            <!--choose add button-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                        </EmptyDataRow>
                    </Templates>
                    <Settings ShowTitlePanel="True" />
                </cc:ASPxGridView>
            </div>
        </div>
    </div>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="right">
                    <dx:ASPxButton ID="Button6" runat="server" Text="<%$ Resources:WebResources, CheckOut %>" 
                        OnClick="Button6_Click" />
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton ID="Button7" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton ID="Button8" runat="server" Text="<%$ Resources:WebResources, CancelPreOrder %>" />
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton ID="Button10" runat="server" Text="<%$ Resources:WebResources, PrintPreOrderVoucher %>" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
