<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ORD12.aspx.cs" Inherits="VSS_ORD_ORD12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--預訂貨作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PrePurchaseOrder %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, OrderSearch %>"
                            AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) { document.location='ORD01.aspx';return false;
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
                                    <asp:Label ID="lblOrderNo" runat="server" Text="PR2101-10070101"></asp:Label>
                                </ContentTemplate>
                                <%--  <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                </Triggers>--%>
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
                            <asp:Literal ID="Literal4" runat="server" Visible="false" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label2" runat="server" Text="預訂" Visible="false"></asp:Label>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>

                <script language="javascript">
                    g_VisibleIndex = -1;</script>

                <div id="divContent" runat="server" class="SubEditBlock">
                    <div class="GridScrollBar" style="height: 300px;">
                        <cc:ASPxGridView ID="gvMasterDV" runat="server" KeyFieldName="項次" ClientInstanceName="gvMasterDV"
                            AutoGenerateColumns="False" Width="98%" SettingsEditing-Mode="Inline" OnHtmlRowCreated="gvMasterDV_HtmlRowCreated"
                            OnRowUpdating="gvMasterDV_RowUpdating" OnInitNewRow="gvMasterDV_InitNewRow" OnRowInserting="gvMasterDV_RowInserting">
                            <SettingsEditing Mode="Inline" />
                            <Columns>
                                <%--<dx:GridViewDataTextColumn VisibleIndex="0" Caption=" ">
                                    <HeaderTemplate>
                                        <dx:ASPxCheckBox ID="ASPxCheckBox1" ClientInstanceName="ASPxCheckBox1" AutoPostBack="true"
                                            runat="server" OnCheckedChanged="ASPxCheckBox1_CheckedChanged">
                                        </dx:ASPxCheckBox>
                                        
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <dx:ASPxCheckBox ID="ASPxCheckBox1" ClientInstanceName="ASPxCheckBox1" runat="server">
                                        </dx:ASPxCheckBox>
                                    </EditItemTemplate>
                                    <DataItemTemplate>
                                        <dx:ASPxCheckBox ID="ASPxCheckBox1" ClientInstanceName="ASPxCheckBox1" runat="server">
                                        </dx:ASPxCheckBox>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvMasterDV.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewCommandColumn>
                                <%--<dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                    <EditButton Visible="true" Text="編輯">
                                    </EditButton>
                                    <UpdateButton Text="更新">
                                    </UpdateButton>
                                    <CancelButton Text="取消">
                                    </CancelButton>
                                </dx:GridViewCommandColumn>--%>
                                <%--<dx:GridViewDataTextColumn VisibleIndex="1" Width="50px">
                                    <DataItemTemplate>
                                        <dx:ASPxButton ID="ASPxButton6" Width="50px" AutoPostBack="false" runat="server"
                                            Text="搭贈">
                                            <ClientSideEvents Click="function(s,e)  
                                            {      
                                               var vIndex = s.name.replace(gvMasterDV.name+'_cell','');
                                               vIndex = vIndex.replace('_1_ASPxButton6','');                                        
                                               gvMasterDV.ShowDetailRow=true;
                                               gvMasterDV.ExpandDetailRow(parseInt(vIndex));                                        
                                             }" />
                                        </dx:ASPxButton>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="2">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[項次]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                    <EditItemTemplate>
                                        <table>
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
                                        </dx:ASPxLoadingPanel>
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
                                                <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                </EmptyDataRow>
                            </Templates>
                           <%-- <Images>
                                <DetailCollapsedButton  Url="../../Images/next.png">
                                </DetailCollapsedButton>
                                <DetailExpandedButton Url="../../Images/previous.png"></DetailExpandedButton>
                            </Images>--%>
                            <SettingsDetail ShowDetailRow="true" ShowDetailButtons="true" AllowOnlyOneMasterRowExpanded="true" />
                        </cc:ASPxGridView>
                    </div>
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
                            <%--  <td>
                                <dx:aspxbutton id="Button1" runat="server" text="<%$ Resources:WebResources, Transfer %>">
                                </dx:aspxbutton>
                            </td>--%>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <%-- <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            </Triggers>--%>
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
    </div>
</asp:Content>
