<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON09.aspx.cs" Inherits="VSS_CONS_CON09" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品退倉查詢-->
                    <dx:aspxlabel id="Literal1" runat="server" text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSearch %>">
                    </dx:aspxlabel>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <dx:aspxlabel id="Literal2" runat="server" text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxcombobox id="DropDownList24" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Selected="true" />
                                <dx:ListEditItem Text="COR2010072101" />
                                <dx:ListEditItem Text="COR2010072102" />
                                <dx:ListEditItem Text="COR2010072103" />
                            </Items>
                        </dx:aspxcombobox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <dx:aspxlabel id="Literal3" runat="server" text="<%$ Resources:WebResources, StoreNo %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td>
                                    <dx:aspxtextbox id="ASPxTextBox1" runat="server" width="100">
                                    </dx:aspxtextbox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:aspxbutton id="btnChooseStore" runat="server" text="<%$ Resources:WebResources, Choose %>"
                                        autopostback="false" skinid="PopupButton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <dx:aspxlabel id="Literal4" runat="server" text="<%$ Resources:WebResources, Status %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxcombobox id="DropDownList1" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="已存檔" />
                                <dx:ListEditItem Text="轉單中" />
                                <dx:ListEditItem Text="已成單" />
                                <dx:ListEditItem Text="已驗退" />
                            </Items>
                        </dx:aspxcombobox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--廠商編號-->
                        <dx:aspxlabel id="Literal5" runat="server" text="<%$ Resources:WebResources, SupplierNo %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxcombobox id="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Selected="true" />
                                <dx:ListEditItem Text="AC1" />
                                <dx:ListEditItem Text="AC2" />
                                <dx:ListEditItem Text="AP1" />
                            </Items>
                        </dx:aspxcombobox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品編號-->
                        <dx:aspxlabel id="Literal6" runat="server" text="<%$ Resources:WebResources, ProductCode %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td>
                                    <dx:aspxtextbox id="TextBox4" runat="server" width="100">
                                    </dx:aspxtextbox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:aspxbutton id="btnChooseProduct" runat="server" text="<%$ Resources:WebResources, Choose %>"
                                        autopostback="false" skinid="PopupButton" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>">
                        </asp:Literal>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table>
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, start %>">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxdateedit id="ReturnWarehousingStartDateFrom" runat="server">
                                    </dx:aspxdateedit>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, end %>">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxdateedit id="ReturnWarehousingStartDateTo" runat="server">
                                    </dx:aspxdateedit>
                                </td>
                            </tr>
                        </table>
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
                        <dx:aspxlabel id="Literal7" runat="server" text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval">
                        <table>
                            <tr>
                                <td>
                                    <dx:aspxlabel id="ASPxLabel1" runat="server" text="<%$ Resources:WebResources, start %>">
                                    </dx:aspxlabel>
                                </td>
                                <td>
                                    <dx:aspxdateedit id="ReturnWarehousingEndDateFrom" runat="server">
                                    </dx:aspxdateedit>
                                </td>
                                <td>
                                    <dx:aspxlabel id="ASPxLabel2" runat="server" text="<%$ Resources:WebResources, end %>">
                                    </dx:aspxlabel>
                                </td>
                                <td>
                                    <dx:aspxdateedit id="ReturnWarehousingEndDateTo" runat="server">
                                    </dx:aspxdateedit>
                                </td>
                            </tr>
                        </table>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:aspxbutton id="btnSearch" runat="server" text="<%$ Resources:WebResources, Search %>" 
                            onclick="btnSearch_Click" />
                    </td>
                    <td>
                        <dx:aspxbutton id="btnClear" runat="server" text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                    <td>
                        <dx:aspxbutton id="Button2" runat="server" text="<%$ Resources:WebResources, Export %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
    </div>
    <cc:aspxgridview id="gvMaster" clientinstancename="gvMaster" runat="server" keyfieldname="退倉單號"
        width="100%" autogeneratecolumns="False" enablerowscache="False" ondetailrowexpandedchanged="gvMaster_DetailRowExpandedChanged"
        onhtmldatacellprepared="gvMaster_HtmlDataCellPrepared" onhtmlrowcreated="gvMaster_HtmlRowCreated">
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                <HeaderTemplate>
                    <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataHyperLinkColumn FieldName="退倉單號" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>" VisibleIndex="1">
            </dx:GridViewDataHyperLinkColumn>
            <dx:GridViewDataTextColumn runat="server" FieldName="退倉起日" Caption="<%$ Resources:WebResources, ReturnWarehousingStartDate %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn runat="server" FieldName="退倉訖日" Caption="<%$ Resources:WebResources, ReturnWarehousingEndDate %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn runat="server" FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn runat="server" FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn runat="server" FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <DetailRow>
                <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                    Width="100%" EnableRowsCache="true">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="廠商編號" Caption="<%$ Resources:WebResources, SupplierNo %>" />
                        <dx:GridViewDataColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName %>" />
                        <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
                        <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                        <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />
                        <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
                        <dx:GridViewDataColumn FieldName="庫存數量" Caption="<%$ Resources:WebResources, StockQty %>" />
                        <dx:GridViewDataColumn FieldName="實際退倉數量" Caption="<%$ Resources:WebResources, ActualRetunedQuantity %>" />
                    </Columns>
                    <Settings ShowFooter="false" />
                    <SettingsDetail IsDetailGrid="true" />
                    <SettingsPager PageSize="5">
                    </SettingsPager>
                    <Templates>
                        <TitlePanel>
                            <!--移撥單號-->
                            
                            
                            <asp:Literal ID="TransferSlipNo" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                            <asp:Label ID="Label5" runat="server" Text="ST2013-100712001"></asp:Label>
                        </TitlePanel>
                    </Templates>
                    <Styles>
                        <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                        </TitlePanel>
                    </Styles>
                </cc:ASPxGridView>
            </DetailRow>
            <EmptyDataRow>
                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
            </EmptyDataRow>
        </Templates>
        <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
        <SettingsPager PageSize="5" />
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
    </cc:aspxgridview>
    <cc:aspxpopupcontrol id="productsPopup" skinid="ProductsPopup" runat="server" popupelementid="btnChooseProduct"
        targetelementid="TextBox4" loadingpanelid="lp">
    </cc:aspxpopupcontrol>
    <cc:aspxpopupcontrol id="StoresPopup" skinid="StoresPopup" runat="server" popupelementid="btnChooseStore"
        targetelementid="ASPxTextBox1" loadingpanelid="lp">
    </cc:aspxpopupcontrol>
    <dx:aspxloadingpanel id="lp" runat="server" clientinstancename="lp">
    </dx:aspxloadingpanel>
</asp:Content>
