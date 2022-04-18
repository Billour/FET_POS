<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SAL11.aspx.cs" Inherits="VSS_SAL_SA11" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        var mx;
        var my;
        jQuery(document).mousemove(function(event) {
            mx = event.pageX;
            my = event.pageY;
        });

        function show(content) {
            var tip = $("#tooltip");
            tip.html(content);
            tip.css({
                display: "",
                left: mx - 150,
                top: my,
                position: "absolute",
                background: "#FFFFFF"
            });

        }
        function hide() {
            var tip = $("#tooltip");
            tip.css("display", "none");

        }
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeicheckbox(con) {
            if (con.GetChecked()) {
                openwindow("SAL01_inputIMEIData.aspx", 720, 300);
            }
        }

        function checkID() {
            var vID = document.getElementById("<%=tbInvoiceNo.ClientID %>" + "_I").value;
            if (vID.length != 8 && vID.length > 0) {
                openwindow("SAL01_checkIDNumber.aspx", 300, 200);
                return false;
            }
        }
        function redirect(url) {
            document.location = url;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip">
    </div>    
    <cc:ASPxPopupControl ID="ASPxPopupControl2" runat="server" AllowDragging="True"
         AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/SAL/SAL11_Transaction.aspx"    
         PopupHorizontalAlign="Center" PopupVerticalAlign="Middle" PopupHorizontalOffset="400" PopupVerticalOffset="50"
         Width="300px" Height="180px" HeaderText="交易補登選項" ShowOnPageLoad="true"
         EnableHierarchyRecreation="True">     
     </cc:ASPxPopupControl> 
        
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
        <tr>
            <td align="left">
                <!--交易補登-->
                <asp:Literal ID="Literal1" runat="server" Text="交易補登"></asp:Literal>
            </td>
            <td align="right">
                <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, UnclearedTradeListing %>" 
                    AutoPostBack="false">
                    <ClientSideEvents Click="function(s, e){ document.location='SAL05.aspx'; return false;}" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
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
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, InvoiceOrReceipt %>"></asp:Literal>
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
                <td class="tdtxt">
                    <!--統一編號-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="tbInvoiceNo" runat="server">
                    </dx:ASPxTextBox>
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
                    <!--發票日期-->
                    <asp:Literal ID="Literal32" runat="server" Text="發票日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                    </dx:ASPxDateEdit>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="tdtxt">
                    <!--更新人員-->
                    <asp:Literal ID="Literal34" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label5" runat="server" Text="64591 李家駿"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--發票格式-->
                    *<asp:Literal ID="Literal33" runat="server" Text="發票格式"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" SelectedIndex="0"
                        RepeatDirection="Horizontal" Border-BorderStyle="None">
                        <Items>
                            <dx:ListEditItem Text="二聯式" Value="二聯式" />
                            <dx:ListEditItem Text="三聯式" Value="三聯式" />
                            <dx:ListEditItem Text="無" Value="無" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td class="tdtxt">
                    <!--發票抬頭-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server" CssClass="tbWidthFormat">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--發票號碼-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox8" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--備註-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <dx:ASPxTextBox ID="TextBox6" runat="server" CssClass="tbWidthFormat">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    HG卡號：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox9" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
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
        <div class="SubEditCommand" style="text-align: left; background-color: GrayText">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnItemAdd" runat="server" Text="<%$ Resources:WebResources, Add %>" 
                            OnClick="btnItemAdd_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button5" runat="server" Text="<%$ Resources:WebResources, Delete %>" >
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, MixPromotion %>"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e){ openwindow('SAL01_choosePromotions1.aspx',700,420);return false;}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="商品編號"
                    AutoGenerateColumns="False" Width="100%" OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="類別" Caption="<%$ Resources:WebResources, Category %>"
                            VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>"
                            VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                            VisibleIndex="3">
                            <DataItemTemplate>
                                <uc1:PopupControl ID="PopupControl_Products" runat="server" PopupControlName="ProductsPopup" />
                               <%-- <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtprodno" runat="server" Text='<%# Bind("[商品編號]") %>' Width="100px">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnprodno" runat="server" Text="<%$ Resources:WebResources, choose %>" AutoPostBack="false"></dx:ASPxButton>
                                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ContentUrl="SAL01_searchProductNo.aspx"
                                                PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" Width="500px"
                                                Height="420px" HeaderText="選擇商品料號" TargetElementID="txtprodno" PopupElementID="btnprodno">
                                            </cc:ASPxPopupControl>
                                        </td>
                                    </tr>
                                </table>--%>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                            VisibleIndex="4" />
                        <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>"
                            VisibleIndex="5">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="ASPxTextBox14" Text='<%# Bind("[數量]") %>' runat="server" Width="50px">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="單價" Caption="<%$ Resources:WebResources, UnitPrice %>"
                            VisibleIndex="6">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="ASPxTextBox15" Text='<%# Bind("[單價]") %>' runat="server" Width="50px">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="總價" Caption="<%$ Resources:WebResources, TotalPrice %>"
                            VisibleIndex="7" />
                        <dx:GridViewDataColumn Caption="" VisibleIndex="8">
                            <DataItemTemplate>
                                <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                </dx:ASPxImage>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>"
                            VisibleIndex="9">
                            <DataItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="lbl1" runat="server" Text='<%# Bind("[IMEI]") %>' Width="20px"
                                                Enabled="false" DisabledStyle-Font-Underline="true">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                             <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="InputIMEIData"/>
                                        </td>
                                        <%--<td>
                                            <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="60px" Text="">
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
                        </dx:GridViewDataColumn>
                    </Columns>                    
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                </cc:ASPxGridView>
                <div class="txt" style="text-align: left">
                    <!--應收總金額-->
                    <asp:Literal ID="Literal222" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>"></asp:Literal>：
                    <asp:Label ID="Label2" runat="server" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnItemAdd" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="text-align: left">
                    <asp:Label ID="Label8" Text="<%$ Resources:WebResources, DiscountDetail %>" Visible="false"
                        runat="server" />
                </div>
                <div class="SubEditBlock">
                    <cc:ASPxGridView ID="gvDetail" runat="server" AutoGenerateColumns="False" Width="100%"
                        KeyFieldName="項次" Visible="false" ClientInstanceName="gvDetail">
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
                    <div class="SubEditCommand" style="text-align: left; background-color: GrayText">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnCash" runat="server" Text="<%$ Resources:WebResources, Cash %>"
                                        OnClick="btnCash_Click" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){openwindow('../CheckOut/CheckOutCash.aspx',300,200);}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnOffLineCredit" runat="server" Text="<%$ Resources:WebResources, OffLineCreditCard %>"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){ openwindow('../CheckOut/CheckOutCreditUnline.aspx',300,200);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnPayDEL" runat="server" Text="<%$ Resources:WebResources, Delete %>" >
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
                        <dx:ASPxButton ID="btnOrderCancel" runat="server" Text="<%$ Resources:WebResources, CancelTransaction %>" >
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
