<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SAL07.aspx.cs" Inherits="VSS_SAL07_SAL07" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="func">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                        <tr>
                            <td align="left">
                                <!--促銷商品價格查詢-->
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionPriceEnquiry %>" />
                            </td>
                            <td align="right">
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--專案類型-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProjectType %>" />：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="cbProjectType" runat="server" Width="120">
                                    <Items>
                                        <dx:ListEditItem Value="ALL" Text="ALL" Selected="true" />
                                        <dx:ListEditItem Value="現行" Text="現行" />
                                        <dx:ListEditItem Value="過期專案" Text="過期專案" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--商品廠牌-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductBrand %>" />：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="TextBox7" runat="server" CssClass="tbSpanWidth">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxCheckBox ID="CheckBox1" runat="server" Text="含過期料號">
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--促銷類型-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, PromotionType %>" />：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="AutoCompleteComboBox" runat="server" Width="120">
                                    <Items>
                                        <dx:ListEditItem Value="ALL" Text="ALL" Selected="true" />
                                        <dx:ListEditItem Value="啟用" Text="啟用" />
                                        <dx:ListEditItem Value="續約" Text="續約" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--商品料號-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="TextBox3" runat="server" CssClass="tbSpanWidth">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxCheckBox ID="CheckBox2" runat="server" Text="只查詢單品(不含促銷)">
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--促銷代號-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="TextBox2" runat="server" CssClass="tbSpanWidth">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--商品名稱-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="TextBox5" runat="server" CssClass="tbSpanWidth">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxCheckBox ID="CheckBox3" runat="server" Text="庫存量>0">
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--促銷名稱-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="TextBox6" runat="server" CssClass="tbSpanWidth">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--促銷價-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PromotionPrice %>" />：
                            </td>
                            <td class="tdval" colspan="3" nowrap="nowrap">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>" />
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="TextBox8" runat="server" CssClass="tbSpanWidth">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>" />
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="TextBox9" runat="server" CssClass="tbSpanWidth">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
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
                                <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm); }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="seperate">
                </div>
                <div class="SubEditBlock">
                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="促銷代號"
                        AutoGenerateColumns="False" OnPageIndexChanged="gvMaster_PageIndexChanged" Width="100%">
                        <SettingsPager PageSize="5">
                        </SettingsPager>
                        <Columns>
                            <dx:GridViewDataColumn FieldName="促銷代號" Caption="<%$ Resources:WebResources, PromotionCode %>"
                                VisibleIndex="0" />
                            <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>"
                                VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>"
                                VisibleIndex="3" />
                            <dx:GridViewDataColumn FieldName="促銷類別" Caption="<%$ Resources:WebResources, PromotionType %>"
                                VisibleIndex="4" />
                            <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                VisibleIndex="5" />
                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="6" />
                            <dx:GridViewDataColumn FieldName="商品群" Caption="<%$ Resources:WebResources, ProductGroup %>"
                                VisibleIndex="7" />
                            <dx:GridViewDataColumn FieldName="促銷價" Caption="<%$ Resources:WebResources, PromotionPrice %>"
                                VisibleIndex="8" />
                            <dx:GridViewDataColumn FieldName="庫存量" Caption="<%$ Resources:WebResources, StockQuantity %>"
                                VisibleIndex="9" />
                        </Columns>
                        <Templates>
                            <EmptyDataRow>
                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                            </EmptyDataRow>
                        </Templates>
                    </cc:ASPxGridView>
                </div>
                <div class="seperate">
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
