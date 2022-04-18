<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="SAL06.aspx.cs" Inherits="VSS_SAL_SAL06" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--交易暫存清單-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TemporaryTransactionList %>"></asp:Literal>
        </div>
        <div>
            <div class="func">
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--申請日期-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ApplicationDate %>"></asp:Literal>：
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
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="DropDownList1" runat="server" ValueType="System.String" AutoResizeWithContainer="true">
                                    <Items>
                                        <dx:ListEditItem Text="已審核" Value="已審核" />
                                        <dx:ListEditItem Text="待審核" Value="待審核" Selected="true" />
                                        <dx:ListEditItem Text="拒絕" Value="拒絕" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--服務屬性-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ServiceNature %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="DropDownList3" runat="server" ValueType="System.String" AutoResizeWithContainer="true">
                                </dx:ASPxComboBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--服務類別-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ServiceClass %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="DropDownList4" runat="server" ValueType="System.String" AutoResizeWithContainer="true">
                                </dx:ASPxComboBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--銷售人員-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="DropDownList2" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                                    SelectedIndex="0">
                                    <Items>
                                        <dx:ListEditItem Text="請選擇-" Value="王大明" />
                                        <dx:ListEditItem Text="王大明" Value="王大明" />
                                        <dx:ListEditItem Text="陳明真" Value="陳明真" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                    </table>
                </div>
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
                                <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>"
                                    VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="申請日期" Caption="<%$ Resources:WebResources, ApplicationDate %>"
                                    VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="服務屬性" Caption="<%$ Resources:WebResources, ServiceNature %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="服務類別" Caption="<%$ Resources:WebResources, ServiceClass %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="應收總金額" Caption="<%$ Resources:WebResources, AmountReceivable %>"
                                    VisibleIndex="6" />
                                <dx:GridViewDataColumn FieldName="客戶門號" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                    VisibleIndex="7" />
                                <dx:GridViewDataColumn FieldName="銷售人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                    VisibleIndex="8" />
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
                    
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
