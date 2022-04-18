<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SAL031.aspx.cs" Inherits="VSS_SAL031_SAL031" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "商品編號查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');

        }
        function openwindow2(url, width, height) {
            window.open(url, "促銷代碼查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');

        }
        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    換貨作業
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
        <table>
            <tr>
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
                                <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--狀態-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList2" runat="server" ValueType="System.String" AutoResizeWithContainer="true">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="已結帳" Value="已結帳" Selected="true" />
                            <dx:ListEditItem Text="已作廢" Value="已作廢" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--機台-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CashRegister %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server" CssClass="tbWidthFormat">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--客戶門號-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox8" runat="server" CssClass="tbWidthFormat">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--付款方式-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                        SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="-請選擇-" Value="-請選擇-" />
                            <dx:ListEditItem Text="現金" Value="現金" />
                            <dx:ListEditItem Text="信用卡" Value="信用卡" />
                            <dx:ListEditItem Text="禮券" Value="禮券" />
                            <dx:ListEditItem Text="金融卡" Value="金融卡" />
                            <dx:ListEditItem Text="HappyGo" Value="HappyGo" />
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
                    <dx:ASPxTextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--促銷代碼-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                </td>
                <td class="tdval">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="TextBox3" runat="server" CssClass="tbWidthFormat" Width="110">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){openwindow2('SAL02_searchDiscountNo.aspx',450,350);return false;}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--銷售人員-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList3" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                        SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="-請選擇-" Value="-請選擇-" />
                            <dx:ListEditItem Text="劉光俊" Value="劉光俊" />
                            <dx:ListEditItem Text="林雅玲" Value="林雅玲" />
                            <dx:ListEditItem Text="游惠貞" Value="游惠貞" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--發票號碼-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox5" runat="server" CssClass="tbWidthFormat">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--商品編號-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                </td>
                <td class="tdval">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="TextBox4" runat="server" CssClass="tbWidthFormat" Width="110">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){openwindow2('SAL01_searchProductNo.aspx',450,350);return false;}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){resetForm(aspnetForm);}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="項次"
                AutoGenerateColumns="False" Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
                <SettingsPager PageSize="5">
                </SettingsPager>
                <Columns>
                    <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                        <DataItemTemplate>
                            <dx:ASPxRadioButton ID="radioChoose" runat="server" ClientInstanceName="rc1" GroupName="radioChoose">
                                <ClientSideEvents Init="OnInit" />
                            </dx:ASPxRadioButton>
                        </DataItemTemplate>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                        VisibleIndex="1" />
                    <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>"
                        VisibleIndex="2" />
                    <dx:GridViewDataColumn FieldName="交易日期" Caption="<%$ Resources:WebResources, TradeDate %>"
                        VisibleIndex="3" />
                    <dx:GridViewDataColumn FieldName="交易序號" Caption="<%$ Resources:WebResources, TransactionNo %>"
                        VisibleIndex="4" />
                    <dx:GridViewDataColumn FieldName="機台" Caption="<%$ Resources:WebResources, CashRegister %>"
                        VisibleIndex="5" />
                    <dx:GridViewDataColumn FieldName="客戶門號" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                        VisibleIndex="6" />
                    <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>"
                        VisibleIndex="7" />
                    <dx:GridViewDataColumn FieldName="現金" Caption="<%$ Resources:WebResources, Cash %>"
                        VisibleIndex="8" />
                    <dx:GridViewDataColumn FieldName="信用卡" Caption="<%$ Resources:WebResources, CreditCard %>"
                        VisibleIndex="9" />
                    <dx:GridViewDataColumn FieldName="禮券" Caption="<%$ Resources:WebResources, Coupon %>"
                        VisibleIndex="10" />
                    <dx:GridViewDataColumn FieldName="金融卡" Caption="<%$ Resources:WebResources, BankCard %>"
                        VisibleIndex="11" />
                    <dx:GridViewDataColumn FieldName="HG" Caption="HG" VisibleIndex="12" />
                    <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                        VisibleIndex="13" />
                    <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                        VisibleIndex="14" />
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
            </cc:ASPxGridView>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, View %>">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button21" runat="server" Text="<%$ Resources:WebResources, ExchangeGoods %>"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e){document.location='SAL03.aspx';return false;}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
