<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ORD01.aspx.cs" Inherits="VSS_ORD01_ORD01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div >
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--訂貨作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderPlacement %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, OrderSearch %>"
                            AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
	document.location='ORD02.aspx';
}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--訂單編號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Label ID="lblOrderNo" runat="server" Text="SO2101-10070101"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="tdtxt">
                            <!--訂單日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="lblOrderDate" runat="server">2010/07/01 22:00</asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--狀態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label2" runat="server" Text="正式"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--備註-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                        </td>
                        <td colspan="3" class="tdval" rowspan="2">
                            <asp:TextBox ID="txtMemo" runat="server" Width="98%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--更新日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                        <td class="tdtxt">
                            <!--更新人員-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="SubEditCommand" style="display: none">
            <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                OnClick="btnNew_Click" Enabled="false" Visible="false" />
            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                Enabled="false" />
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="divContent" runat="server" class="SubEditBlock">
                    <cc:ASPxGridView ID="drMasterDV" Width="100%" runat="server" ClientInstanceName="drMasterDV"
                        AutoGenerateColumns="False" OnPageIndexChanged="drMasterDV_PageIndexChanged"
                        OnRowValidating="drMasterDV_RowValidating" KeyFieldName="項次" OnHtmlCommandCellPrepared="drMasterDV_HtmlCommandCellPrepared"
                        OnHtmlRowCreated="drMasterDV_HtmlRowCreated" OnCustomButtonCallback="drMasterDV_CustomButtonCallback">
                        <Columns>
                            <%-- <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton  Text="<%$ Resources:WebResources, ThrowIn %>"
                                        ID="btnSelect">
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>--%>
                            <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="建議訂購量" Caption="<%$ Resources:WebResources, RecommendedOrderQuantity %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="網購需求量" Caption="<%$ Resources:WebResources, EconomicOrderQuantity %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="門市庫存量" Caption="<%$ Resources:WebResources, StockQuantity %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="在途量" Caption="<%$ Resources:WebResources, OnOrderQuantity %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="當日訂購量" Caption="<%$ Resources:WebResources, IntraDayOrderQuantity %>">
                                <DataItemTemplate>
                                    <div align="left">
                                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="30px" Text='<%#BIND("當日訂購量") %>'>
                                        </dx:ASPxTextBox>
                                    </div>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="9" FieldName="當日總訂購量" Caption="<%$ Resources:WebResources, IntraDayTotalOrderQuantity %>">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Templates>
                            <DetailRow>
                                <cc:ASPxGridView ID="gvDetailDV" Width="80%" runat="server" ClientInstanceName="gvDetailDV"
                                    AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableRowsCache="true">
                                    <Columns>
                                        <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="搭配量" Caption="搭配數量">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="訂購量" Caption="<%$ Resources:WebResources, OrderQuantity %>">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <TitlePanel>
                                            <div align="left">
                                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, CollocationProduct %>"></asp:Literal>
                                            </div>
                                        </TitlePanel>
                                        <EmptyDataRow>
                                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                        </EmptyDataRow>
                                    </Templates>
                                    <SettingsDetail IsDetailGrid="true" />
                                </cc:ASPxGridView>
                            </DetailRow>
                            <EmptyDataRow>
                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                            </EmptyDataRow>
                        </Templates>
                        <SettingsPager PageSize="5">
                        </SettingsPager>
                        <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                    </cc:ASPxGridView>
                </div>
                </div>
                <div class="seperate">
                </div>
                <div class="btnPosition">
                    <table align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                    OnClick="btnSave_Click" Visible="false">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                    Visible="false">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, Transfer %>">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button runat="server" ID="HiddenTargetControlForModalPopup" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopup" runat="server" TargetControlID="HiddenTargetControlForModalPopup"
            PopupControlID="ModalPopupPanel" BackgroundCssClass="messagemodalbackground"
            DropShadow="true" OkControlID="OkButton" CancelControlID="CancelButton" />
        <asp:Panel ID="ModalPopupPanel" runat="server">
            <div class="window" style="left: 10px; top: 10px; z-index: 10; background-color: White">
                <div class="titlebar">
                    <!--網購商品訂貨-->
                    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, EconomicOrder %>"></asp:Literal>
                </div>
                <div class="content" style="background-color: #ffffff;">
                    <div class="GridScrollBar" style="height: 192px">
                        <cc:ASPxGridView ID="GridView1" runat="server" ClientInstanceName="GridView1" AutoGenerateColumns="False"
                            Width="100%">
                            <Columns>
                                <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="需求量" Caption="<%$ Resources:WebResources, QuantityDemanded %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="門市庫存量" Caption="<%$ Resources:WebResources, StoreStockQuantity %>">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                </EmptyDataRow>
                            </Templates>
                        </cc:ASPxGridView>
                    </div>
                    <div class="btnPosition">
                        <asp:Button ID="OkButton" runat="server" Text="<%$ Resources:WebResources, Ok %>" />
                        <asp:Button ID="CancelButton" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
