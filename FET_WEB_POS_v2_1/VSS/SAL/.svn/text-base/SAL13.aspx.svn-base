<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SAL13.aspx.cs" Inherits="VSS_SAL_SAL13" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeicheckbox(con) {
            if (con.checked) {
                openwindow("SAL01_inputIMEIData.aspx");
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--折扣優惠查詢-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DiscountProductSearch %>"></asp:Literal>
                    </td>
                    <td align="right">
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td class="tdtxt">
                        <!--費率-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Rates %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxCheckBox ID="CheckBox1" runat="server" Text="Voice" />
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="CheckBox2" runat="server" Text="Data" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--申辦類型-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, BidType %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxCheckBox ID="CheckBox3" runat="server" Text="GA" />
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="CheckBox4" runat="server" Text="Loyalty" />
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="CheckBox5" runat="server" Text="2轉3" />
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="CheckBox6" runat="server" Text="MNP" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server" CssClass="tbSpanWidth" />
                    </td>
                    <td class="tdtxt">
                        <!--客戶門號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox2" runat="server" CssClass="tbSpanWidth" />
                    </td>
                    <td class="tdtxt">
                        <!--ARPB金額-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ARPBamount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat='server' CssClass="tbSpanWidth" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--促銷代號-->
                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" CssClass="tbSpanWidth" />
                    </td>
                    
                    <td class="tdtxt">
                        <!--促銷名稱-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox4" runat='server' CssClass="tbSpanWidth" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox3" runat="server" CssClass="tbSpanWidth" />
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox6" runat="server" CssClass="tbSpanWidth" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="折扣料號"
            Width="100%" OnHtmlRowPrepared="grid_HtmlRowPrepared" OnHtmlRowCreated="grid_HtmlRowCreated"
            OnPageIndexChanged="grid_PageIndexChanged">
            <Columns>
                <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, items %>" />
                <dx:GridViewDataColumn FieldName="折扣料號" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" />
                <dx:GridViewDataColumn FieldName="折扣名稱" Caption="<%$ Resources:WebResources, DiscountName %>" />
                <dx:GridViewDataColumn FieldName="折扣金額" Caption="<%$ Resources:WebResources, DiscountAmount %>" />
                <dx:GridViewDataColumn FieldName="商品折扣比率" Caption="<%$ Resources:WebResources, MerchandiseDiscountRate %>" />
                <dx:GridViewDataColumn FieldName="生效起日" Caption="<%$ Resources:WebResources, EffectiveStartDate %>" />
                <dx:GridViewDataColumn FieldName="生效訖日" Caption="<%$ Resources:WebResources, EffectiveEndDate %>" />
                <dx:GridViewDataColumn FieldName="折扣上限次數" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="5">
            </SettingsPager>
        </cc:ASPxGridView>
        <div class="seperate">
        </div>
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
            <TabPages>
                
                <dx:TabPage Text="<%$ Resources:WebResources, RatesHostTypes %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <table width="100%" align="left">
                                    <tr>
                                        <td align="left">
                                            費率：
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem> Voice</asp:ListItem>
                                                <asp:ListItem>Data</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            GA：
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:CheckBoxList ID="CheckBoxList3" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>Postpaid FET-2G</asp:ListItem>
                                                <asp:ListItem>Postpaid FET-3G</asp:ListItem>
                                                <asp:ListItem>Prepaid</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Loyalty：
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:CheckBoxList ID="CheckBoxList4" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>FET-2G</asp:ListItem>
                                                <asp:ListItem>FET-3G</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            2轉3：
                                        </td>
                                        <td align="left" colspan="4">
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                                                            <asp:ListItem>FET-2G =&gt; FET-3G (Voice)</asp:ListItem>
                                                            <asp:ListItem>KGT-2G =&gt; KGT-3G (Voice)</asp:ListItem>
                                                            <asp:ListItem>FET Prepaid =&gt; FET-3G Postpaid (Voice)</asp:ListItem>
                                                            <asp:ListItem>New Cash =&gt; FET-3G Postpaid (Voice)</asp:ListItem>
                                                            <asp:ListItem>KGT Prepaid =&gt; FET-3G Postpaid (Voice)</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td valign="top">
                                                        <asp:CheckBoxList ID="CheckBoxList6" runat="server">
                                                            <asp:ListItem>FET-2G =&gt; FET-3G (Data)</asp:ListItem>
                                                            <asp:ListItem>KGT-2G =&gt; KGT-3G (Data)</asp:ListItem>
                                                            <asp:ListItem>FET Prepaid =&gt; FET-3G Postpaid (Data)</asp:ListItem>
                                                            <asp:ListItem>KGT Prepaid =&gt; FET-3G Postpaid (Data)</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            MNP：
                                        </td>
                                        <td align="left" colspan="4">
                                            &nbsp;<asp:CheckBox ID="CheckBox7" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="<%$ Resources:WebResources, DesignatedGoods %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號"
                                    Width="50%">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                        <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, productname %>" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="<%$ Resources:WebResources, SpecifyStore %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                            <cc:ASPxGridView ID="ASPxGridView1" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號"
                                    Width="50%">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
                                        <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                                        <dx:GridViewDataColumn FieldName="區域別" Caption="<%$ Resources:WebResources, ByDistrict %>" />
                                        <dx:GridViewDataColumn FieldName="折扣上限次數" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="<%$ Resources:WebResources, PromotionCode %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                            <cc:ASPxGridView ID="ASPxGridView2" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號"
                                    Width="50%">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="促銷代號" Caption="<%$ Resources:WebResources, PromotionCode %>" />
                                        <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, Promotionname %>" />
                                    </Columns>
                                    
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="<%$ Resources:WebResources, TargetCustomers %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:RadioButtonList ID="rbCustomer" runat="server" 
                                            OnSelectedIndexChanged="rbCustomer_SelectedIndexChanged"
                                            RepeatDirection="Horizontal" AutoPostBack="true">
                                            <asp:ListItem Value="客戶等級" Selected="True">客戶等級</asp:ListItem>
                                            <asp:ListItem Value="名單">名單</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td id="trgv1" runat="server">
                                       <cc:ASPxGridView ID="ASPxGridView7" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
                                        Width="50%">
                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="項次" Caption="項次" Visible="false" />
                                                <dx:GridViewDataColumn FieldName="ARPB金額(起)" Caption="ARPB金額(起)" />
                                                <dx:GridViewDataColumn FieldName="ARPB金額(訖)" Caption="ARPB金額(訖)" />
                                            </Columns>
                                            
                                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        </cc:ASPxGridView>
                                    </td>
                                </tr>
                                <tr id="trgv2" runat="server" visible="false">
                                    <td>
                                        <cc:ASPxGridView ID="ASPxGridView6" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
                                        Width="10%">
                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="項次" Caption="項次" Visible="false" />
                                                <dx:GridViewDataColumn FieldName="客戶門號" Caption="客戶門號" />
                                            </Columns>
                                            
                                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        </cc:ASPxGridView>
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="<%$ Resources:WebResources, CostCenter %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                            <cc:ASPxGridView ID="ASPxGridView3" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號"
                                    Width="50%">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="成本中心" Caption="成本中心" />
                                        <dx:GridViewDataColumn FieldName="商品分類" Caption="商品分類" />
                                        <dx:GridViewDataColumn FieldName="會計科目" Caption="會計科目" />
                                        <dx:GridViewDataColumn FieldName="金額" Caption="金額" />
                                        <dx:GridViewDataColumn FieldName="備註" Caption="備註" />
                                    </Columns>
                                    
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="贈品設定">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                            <cc:ASPxGridView ID="ASPxGridView4" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號"
                                    Width="50%">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                        <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, productname %>" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="加價購">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                            <cc:ASPxGridView ID="ASPxGridView5" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號"
                                    Width="50%">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                        <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, productname %>" />
                                    </Columns>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
</asp:Content>
