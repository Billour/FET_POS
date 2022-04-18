<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ORD12.aspx.cs" Inherits="VSS_ORD_ORD12" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--預訂貨作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PrePurchaseOrder %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) { document.location='ORD02.aspx';return false;
                                }" />
                    </dx:ASPxButton>
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
                    <!--訂單編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, BookOrderNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" Text="PR2101-10070101"></asp:Label>
                        </ContentTemplate>
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
                </td>
                <td class="tdval">
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
    <div class="seperate">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script language="javascript">
                    g_VisibleIndex = -1;</script>

            <div id="divContent" runat="server" class="SubEditBlock">
                <cc:ASPxGridView ID="gvMasterDV" runat="server" KeyFieldName="項次" ClientInstanceName="gvMasterDV"
                    AutoGenerateColumns="False" Width="98%" SettingsEditing-Mode="Inline" OnHtmlRowCreated="gvMasterDV_HtmlRowCreated"
                    OnRowUpdating="gvMasterDV_RowUpdating" OnInitNewRow="gvMasterDV_InitNewRow" OnRowInserting="gvMasterDV_RowInserting"
                    OnRowCommand="gvMasterDV_RowCommand">
                    <SettingsEditing Mode="Inline" />
                    <Columns>
                        <dx:GridViewCommandColumn ButtonType="Button" ShowSelectCheckbox="True" VisibleIndex="0"
                            HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <HeaderTemplate>
                                <input type="checkbox" onclick="gvMasterDV.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataButtonEditColumn VisibleIndex="0">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>">
                                </dx:ASPxButton>
                            </DataItemTemplate>
                            <EditItemTemplate>
                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>">
                                </dx:ASPxButton>
                            </EditItemTemplate>
                        </dx:GridViewDataButtonEditColumn>
                        <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                            VisibleIndex="2">
                            <EditItemTemplate>
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[項次]") %>'>
                                </dx:ASPxLabel>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>">
                            <EditItemTemplate>
                                <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  Text='<%#Eval("[商品編號]")  %>'/>
                                <%--<table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100PX" Text='<%#Eval("[商品編號]")  %>'>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton2" runat="server" SkinID="PopupButton">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                                <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                    PopupElementID="ASPxButton2" TargetElementID="ASPxTextBox1" LoadingPanelID="lp1">
                                </cc:ASPxPopupControl>
                                <dx:ASPxLoadingPanel ID="lp1" runat="server">
                                </dx:ASPxLoadingPanel>--%>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                            <EditItemTemplate>
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#Eval("[商品名稱]") %>'>
                                </dx:ASPxLabel>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="網購需求量" Caption="<%$ Resources:WebResources, EconomicOrderQuantity %>">
                            <EditItemTemplate>
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#Eval("[網購需求量]") %>'>
                                </dx:ASPxLabel>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="門市庫存量" Caption="<%$ Resources:WebResources, StockQuantity %>">
                            <EditItemTemplate>
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#Eval("[門市庫存量]") %>'>
                                </dx:ASPxLabel>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="在途量" Caption="<%$ Resources:WebResources, OnOrderQuantity %>">
                            <EditItemTemplate>
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#Eval("[在途量]") %>'>
                                </dx:ASPxLabel>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="預訂量" Caption="<%$ Resources:WebResources, PreOrderQuantity %>">
                            <EditItemTemplate>
                                <div align="left">
                                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="30PX" Text='<%#Eval("[預訂量]") %>'>
                                    </dx:ASPxTextBox>
                                </div>
                            </EditItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
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
                                    <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="搭配量" Caption="<%$ Resources:WebResources, CollocationQuantity %>">
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
                                </Templates>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                <SettingsDetail IsDetailGrid="true" />
                            </cc:ASPxGridView>
                            </div>
                        </DetailRow>
                        <TitlePanel>
                            <table align="left">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btnNew_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" AutoPostBack="false" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                        <EmptyDataRow>
                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                        </EmptyDataRow>
                    </Templates>
                    <Settings ShowTitlePanel="True" />
                    <SettingsDetail ShowDetailRow="true" ShowDetailButtons="False" />
                </cc:ASPxGridView>
                <br />
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition" id="showBtnFooter" runat="server">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" 
                                OnClick="btnSave_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
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
                            <dx:GridViewDataCheckColumn>
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="需求量" Caption="<%$ Resources:WebResources, QuantityDemanded %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="門市庫存量" Caption="<%$ Resources:WebResources, StoreStockQuantity %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="訂購量" Caption="<%$ Resources:WebResources, StoreStockQuantity %>">
                                <DataItemTemplate>
                                    <div align="left">
                                        <dx:ASPxTextBox ID="ASPxTextBox11" Text='<%# Bind("訂購量")%>' runat="server" Width="30px">
                                        </dx:ASPxTextBox>
                                    </div>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </cc:ASPxGridView>
                </div>
                <div class="btnPosition">
                    <asp:Button ID="OkButton" runat="server" Text="<%$ Resources:WebResources, Ok %>" />
                    <asp:Button ID="CancelButton" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
