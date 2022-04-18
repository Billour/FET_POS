<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SAL05.aspx.cs" Inherits="VSS_SAL_SAL05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="func1">
        <div class="titlef" style="text-align: left">
            <!--交易未結清單-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, UnclearedTradeListing %>" />
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--申請日期-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ApplicationDate %>" />：
                        </td>
                        <td class="tdval" colspan="3" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--狀 態-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="DropDownList1" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                                SelectedIndex="0">
                                <Items>
                                    <dx:ListEditItem Text="未結帳" Value="未結帳" />
                                    <dx:ListEditItem Text="巳取消" Value="巳取消" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--服務類別-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ServiceClass %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="DropDownList4" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                                SelectedIndex="0">
                                <Items>
                                    <dx:ListEditItem Text="-請選擇-" Value="-請選擇-" />
                                    <dx:ListEditItem Text="全球卡" Value="全球卡" />
                                    <dx:ListEditItem Text="換補卡" Value="換補卡" />
                                    <dx:ListEditItem Text="2轉3" Value="2轉3" />
                                    <dx:ListEditItem Text="新啟用" Value="新啟用" />
                                    <dx:ListEditItem Text="續約" Value="續約" />
                                    <dx:ListEditItem Text="代收" Value="代收" />
                                    <dx:ListEditItem Text="維修" Value="維修" />
                                    <dx:ListEditItem Text="網購" Value="網購" />
                                    <dx:ListEditItem Text="預購" Value="預購" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--客戶門號-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--銷售人員-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="DropDownList2" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                                SelectedIndex="0">
                                <Items>
                                    <dx:ListEditItem Text="王大明" Value="王大明" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </div>
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="divContent" runat="server" class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="項次"
                            AutoGenerateColumns="False" Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged"
                            OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>"
                                    VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="申請日期" Caption="<%$ Resources:WebResources, ApplicationDate %>"
                                    VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="服務類別" Caption="<%$ Resources:WebResources, ServiceClass %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="客戶門號" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="應收總金額" Caption="<%$ Resources:WebResources, AmountReceivable %>"
                                    VisibleIndex="6" />
                                <dx:GridViewDataColumn FieldName="銷售人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                    VisibleIndex="7" />
                                <dx:GridViewDataTextColumn Caption="折扣" VisibleIndex="8">
                                    <DataItemTemplate>
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="折扣">
                                        </dx:ASPxButton>
                                        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ContentUrl="~/VSS/SAL/SAL05_DiscountDetail.aspx"
                                            PopupElementID="ASPxButton1" AllowDragging="True" AllowResize="True" CloseAction="CloseButton"
                                            PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
                                            Width="500px" Height="280px" HeaderText="折扣明細" EnableHierarchyRecreation="True">
                                        </dx:ASPxPopupControl>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <DetailRow>
                                    <cc:ASPxGridView ID="detailGrid" runat="server" ClientInstanceName="detailGrid" Settings-ShowTitlePanel="true"
                                        KeyFieldName="項次" Width="100%" OnBeforePerformDataSelect="detailGrid_DataSelect"
                                        OnPageIndexChanged="detailGrid_PageIndexChanged" EnableRowsCache="true">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                                VisibleIndex="0" />
                                            <dx:GridViewDataColumn FieldName="促銷代號" Caption="<%$ Resources:WebResources, PromotionCode %>"
                                                VisibleIndex="1" />
                                            <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>"
                                                VisibleIndex="2" />
                                            <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                                VisibleIndex="3" />
                                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                                VisibleIndex="4" />
                                            <dx:GridViewDataColumn FieldName="卡片序號(SIM)" Caption="<%$ Resources:WebResources, SimCardSerialNumber %>"
                                                VisibleIndex="5" />
                                            <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>" />
                                        </Columns>
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <Settings ShowFooter="false" />
                                        <SettingsDetail IsDetailGrid="true" />
                                        <SettingsPager PageSize="5">
                                        </SettingsPager>
                                    </cc:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <SettingsPager PageSize="5" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                        </cc:ASPxGridView>
                        <div class="seperate">
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="btnPosition" id="showFooter" runat="server" visible="true">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="combinedPaymentButton" runat="server" Text="<%$ Resources:WebResources, ConsolidatedCheckout %>" 
                            onclick="combinedPaymentButton_Click">                           
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, CancelTransaction %>" >
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
