<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DIS01.aspx.cs" Inherits="VSS_DIS_DIS01" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register Src="../../Controls/DISGridViewPanel.ascx" TagName="DISGridViewPanel"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/DISItemChargesAndApply.ascx" TagName="DISItemChargesAndApply"
    TagPrefix="uc5" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--折扣設定作業-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, DiscountSetOperations %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='DIS02.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, Category %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" Width="170px">
                        <Items>
                            <dx:ListEditItem Text="一般" Value="一般" />
                            <dx:ListEditItem Text="舊機回收" Value="舊機回收" />
                            <dx:ListEditItem Text="租賃" Value="租賃" />
                            <dx:ListEditItem Text="店長折扣" Value="店長折扣" />
                            <dx:ListEditItem Text="HappyGo折扣" Value="HappyGo折扣" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120px" align="right">
                    &nbsp;
                </td>
                <td width="170px" align="left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="40px" align="right">
                    &nbsp;
                </td>
                <td width="120px" align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, DiscountName %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="40px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="<%$ Resources:WebResources, Status %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="120px" align="left">
                    <dx:ASPxLabel ID="lblStatus" runat="server" Text="00-未存檔">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, DiscountAmount %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="<%$ Resources:WebResources, MerchandiseDiscountRate %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="40px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="<%$ Resources:WebResources, Date %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="120px" align="left">
                    <dx:ASPxLabel ID="lblDate" runat="server" Text="2010/07/01 22:00">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct1" runat="server" Width="40">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct2" runat="server" Width="40">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct3" runat="server" Width="50">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct4" runat="server" Width="50">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct5" runat="server" Width="40">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct6" runat="server" Width="40">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120px" align="right">
                    <!--折扣上限次數-->
                    <asp:Literal ID="LimitTheNumberDiscount" runat="server" Text="<%$ Resources:WebResources, LimitTheNumberDiscount %>"></asp:Literal>：
                </td>
                <td width="170px" align="left">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" SelectedIndex="3" ValueType="System.String"
                                    Width="60px">
                                    <Items>
                                        <dx:ListEditItem Text="無" Value="無" />
                                        <dx:ListEditItem Text="指定" Value="指定" />
                                        <dx:ListEditItem Text="總量" Value="總量" />
                                        <dx:ListEditItem Selected="True" Text="均分" Value="均分" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100px" Text="10">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="40px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="<%$ Resources:WebResources, Staff %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="120px" align="left">
                    <dx:ASPxLabel ID="lblStaff" runat="server" Text="12345 王大寶">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:WebResources, EffectiveDuration %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <table>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="SupportStartDateFrom" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="SupportStartDateTo" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120px" align="right">
                    &nbsp;
                </td>
                <td width="170px" align="left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="40px" align="right">
                    &nbsp;
                </td>
                <td width="120px" align="left">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="seperate">
        <asp:UpdatePanel ID="upTab" runat="server">
            <ContentTemplate>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" Width="100%" AutoPostBack="True"
                    OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged" ActiveTabIndex="0">
                    <TabPages>
                        <dx:TabPage Text="<%$ Resources:WebResources, RatesHostTypes %>">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <div>
                                        <uc5:DISItemChargesAndApply ID="DISItemChargesAndApply1" runat="server" />
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, DesignatedGoods %>">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <cc:ASPxGridView ID="gvMaster1" ClientInstanceName="gvMaster1" runat="server" KeyFieldName="商品料號"
                                        Width="50%" Settings-ShowTitlePanel="true">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvMaster1.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"
                                                        Text='<%# BIND("[商品料號]") %>' />
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, productname %>" />
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd1_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                                            </dx:ASPxButton>
                                                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                                                CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl="~/VSS/DIS/DIS01_Import.aspx"
                                                                Width="640" Height="400" LoadingPanelID="lp" HeaderText="指定商品上傳">
                                                                <ContentStyle>
                                                                    <Paddings Padding="4px"></Paddings>
                                                                </ContentStyle>
                                                            </cc:ASPxPopupControl>
                                                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                                                            </dx:ASPxLoadingPanel>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnTemplate" runat="server" Text="Template">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <SettingsEditing Mode="Inline" />
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, SpecifyStore %>">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <cc:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
                                        KeyFieldName="門市編號" Width="50%" Settings-ShowTitlePanel="true">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="ASPxGridView1.SelectAllRowsOnPage(this.checked);"
                                                        title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>">
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup"
                                                        Text='<%# BIND("[門市編號]") %>' />
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                                            <dx:GridViewDataColumn FieldName="區域別" Caption="<%$ Resources:WebResources, ByDistrict %>" />
                                            <dx:GridViewDataColumn FieldName="折扣上限次數" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>" />
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd2_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnImport1" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                                            </dx:ASPxButton>
                                                            <cc:ASPxPopupControl ID="ASPxPopupControl2" runat="server" AllowDragging="True" AllowResize="True"
                                                                CloseAction="CloseButton" PopupElementID="btnImport1" ContentUrl="~/VSS/DIS/DIS01_Store_Import.aspx"
                                                                Width="640" Height="400" LoadingPanelID="lp" HeaderText="指定門市上傳">
                                                                <ContentStyle>
                                                                    <Paddings Padding="4px"></Paddings>
                                                                </ContentStyle>
                                                            </cc:ASPxPopupControl>
                                                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                                                            </dx:ASPxLoadingPanel>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnTemplate" runat="server" Text="Template">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnTimes" runat="server" Text="均分次數">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="lblRemainingTimes" runat="server" Text="剩餘數量：1">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <SettingsEditing Mode="Inline" />
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, SpecifyPromotions %>">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <cc:ASPxGridView ID="ASPxGridView10" ClientInstanceName="ASPxGridView10" runat="server"
                                        KeyFieldName="促銷代號" Width="50%" Settings-ShowTitlePanel="true">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="ASPxGridView10.SelectAllRowsOnPage(this.checked);"
                                                        title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="促銷代號" Caption="<%$ Resources:WebResources, PromotionCode %>">
                                                <EditItemTemplate>
                                                    <%--<uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="PromotionPopup"
                                                        Text='<%# BIND("[促銷代號]") %>' />--%>
                                                    <uc1:PopupControl ID="PopupControl233" runat="server" PopupControlName="PromotionsPopup"
                                                        Text='<%# BIND("[促銷代號]") %>' />
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>" />
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd3_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                                            </dx:ASPxButton>
                                                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                                                CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl="~/VSS/DIS/DIS01_Permissions_Import.aspx"
                                                                Width="640" Height="400" LoadingPanelID="lp" HeaderText="指定促銷上傳">
                                                                <ContentStyle>
                                                                    <Paddings Padding="4px"></Paddings>
                                                                </ContentStyle>
                                                            </cc:ASPxPopupControl>
                                                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                                                            </dx:ASPxLoadingPanel>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnTemplate" runat="server" Text="Template">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <SettingsEditing Mode="Inline" />
                                    </cc:ASPxGridView>
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
                                                    <asp:RadioButtonList ID="rbCustomer" runat="server" OnSelectedIndexChanged="rbCustomer_SelectedIndexChanged"
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
                                                <td>
                                                    <uc2:DISGridViewPanel ID="DISGridViewPanelCustomer1" runat="server" />
                                                    <uc2:DISGridViewPanel ID="DISGridViewPanelCustomer2" runat="server" Visible="false" />
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
                                    <cc:ASPxGridView ID="ASPxGridView3" ClientInstanceName="ASPxGridView3" runat="server"
                                        KeyFieldName="會計科目" Width="50%" Settings-ShowTitlePanel="true">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="ASPxGridView3.SelectAllRowsOnPage(this.checked);"
                                                        title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="成本中心" Caption="<%$ Resources:WebResources, CostCenter %>">
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="CostCenterPopup"
                                                        Text='<%# BIND("[成本中心]") %>' />
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataDateColumn FieldName="商品分類" Caption="<%$ Resources:WebResources, ProductCategory1 %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxComboBox ID="DROPDOWNLIST" runat="server">
                                                        <Items>
                                                            <dx:ListEditItem Value="0" Text="手機類" />
                                                            <dx:ListEditItem Value="1" Text="配件類" />
                                                            <dx:ListEditItem Value="2" Text="3C" />
                                                            <dx:ListEditItem Value="3" Text="新啟用" />
                                                            <dx:ListEditItem Value="4" Text="續約" />
                                                            <dx:ListEditItem Value="5" Text="通路" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataColumn FieldName="會計科目" Caption="<%$ Resources:WebResources, AccountingSubject %>" />
                                            <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>" />
                                            <dx:GridViewDataColumn FieldName="備註" Caption="<%$ Resources:WebResources, Remark %>" />
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd4_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnImport1" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                                            </dx:ASPxButton>
                                                            <cc:ASPxPopupControl ID="ASPxPopupControl2" runat="server" AllowDragging="True" AllowResize="True"
                                                                CloseAction="CloseButton" PopupElementID="btnImport1" ContentUrl="~/VSS/DIS/DIS01_Cost_Import.aspx"
                                                                Width="640" Height="400" LoadingPanelID="lp" HeaderText="成本中心上傳">
                                                                <ContentStyle>
                                                                    <Paddings Padding="4px"></Paddings>
                                                                </ContentStyle>
                                                            </cc:ASPxPopupControl>
                                                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                                                            </dx:ASPxLoadingPanel>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnTemplate" runat="server" Text="Template">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <SettingsEditing Mode="Inline" />
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="贈品設定">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <cc:ASPxGridView ID="ASPxGridView4" ClientInstanceName="ASPxGridView4" runat="server"
                                        KeyFieldName="商品料號" Width="50%" Settings-ShowTitlePanel="true">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="ASPxGridView4.SelectAllRowsOnPage(this.checked);"
                                                        title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"
                                                        Text='<%# BIND("[商品料號]") %>' />
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, productname %>" />
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd5_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                                            </dx:ASPxButton>
                                                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                                                CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl="~/VSS/DIS/DIS01_Import.aspx"
                                                                Width="640" Height="400" LoadingPanelID="lp" HeaderText="贈品上傳">
                                                                <ContentStyle>
                                                                    <Paddings Padding="4px"></Paddings>
                                                                </ContentStyle>
                                                            </cc:ASPxPopupControl>
                                                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                                                            </dx:ASPxLoadingPanel>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnTemplate" runat="server" Text="Template">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <SettingsEditing Mode="Inline" />
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="加價購">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <cc:ASPxGridView ID="ASPxGridView5" ClientInstanceName="ASPxGridView5" runat="server"
                                        KeyFieldName="商品料號" Width="50%" Settings-ShowTitlePanel="true">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="ASPxGridView5.SelectAllRowsOnPage(this.checked);"
                                                        title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"
                                                        Text='<%# BIND("[商品料號]") %>' />
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, productname %>" />
                                            <dx:GridViewDataColumn FieldName="折扣金額" Caption="<%$ Resources:WebResources, productname %>" />
                                            <dx:GridViewDataColumn FieldName="單機價" Caption="<%$ Resources:WebResources, productname %>" />
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAdd6_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                                            </dx:ASPxButton>
                                                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                                                CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl="~/VSS/DIS/DIS01_Import.aspx"
                                                                Width="640" Height="400" LoadingPanelID="lp" HeaderText="加價購商品上傳">
                                                                <ContentStyle>
                                                                    <Paddings Padding="4px"></Paddings>
                                                                </ContentStyle>
                                                            </cc:ASPxPopupControl>
                                                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                                                            </dx:ASPxLoadingPanel>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnTemplate" runat="server" Text="Template">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <SettingsEditing Mode="Inline" />
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="存檔">
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
