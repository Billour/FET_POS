<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON06.aspx.cs" Inherits="VSS_CONS_CON06" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品訂貨作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentOrderPlacement %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        OnClick="Button1_Click" Width="75px" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--訂單編號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="lblOrderNo" runat="server" Text="101900073">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--訂單日期-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="lblOrderDate" runat="server" Text="2010/07/01 22:00">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Label2" runat="server" Text="00 未存檔">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--廠商編號-->
                        <dx:ASPxLabel ID="Literal16" runat="server" Text="<%$ Resources:WebResources,SupplierNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="DropDownList1" ValueType="System.String" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                                <dx:ListEditItem Text="AC1" Value="10000" />
                                <dx:ListEditItem Text="AC2" Value="20000" />
                                <dx:ListEditItem Text="AP1" Value="30000" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal17" runat="server" Text="<%$ Resources:WebResources,SupplierName %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                       <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ConsignmentVendorsPopup"  />

                       <%-- <table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox1" runat="server" Width="100">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        AutoPostBack="false" SkinID="PopupButton" />
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新日期-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources,ModifiedDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Label3" runat="server" Text="10/07/12 15:00">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--備註-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources,Remark %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td colspan="3" class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtMemo" runat="server" Width="99%">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新人員-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Label4" runat="server" Text="64591 李家駿">
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1">
                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
                        Settings-ShowFooter="true" Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                        EnableRowsCache="False" OnRowInserting="gvMaster_RowInserting" OnRowUpdating="gvMaster_RowUpdating">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                <HeaderTemplate>
                                    <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewCommandColumn>
                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button">
                                <EditButton Visible="True">
                                </EditButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="項次" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                                VisibleIndex="2">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" VisibleIndex="3" Caption="<%$ Resources:WebResources, ProductCode %>">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                                <EditCellStyle HorizontalAlign="Left" />
                                <HeaderCaptionTemplate>
                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="txtProductCode" runat="server" Text='<%# Eval("[商品料號]") %>' Width="68px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ChooseProductCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                    ClientSideEvents-Click="function(s,e){openwindowByEncrypt('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="4">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="商品類別" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductCategory %>"
                                VisibleIndex="5">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="建議訂購量" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, RecommendedOrderQuantity %>"
                                VisibleIndex="6">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="實際訂購量" runat="server" VisibleIndex="7">
                                <HeaderCaptionTemplate>
                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ActualOrderQuantity %>">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="單價" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, UnitPrice %>"
                                VisibleIndex="8">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="總價" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, TotalPrice %>"
                                VisibleIndex="9">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="總價" SummaryType="Sum" DisplayFormat="c" />
                        </TotalSummary>
                        <SettingsPager PageSize="10" />
                        <SettingsEditing Mode="Inline" />
                        <Templates>
                            <TitlePanel>
                                <table align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnSaleToOrder" runat="server" Text="<%$ Resources:WebResources, SalesToOrder %>"
                                                OnClick="btnSaleToOrder_Click" />
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                OnClick="AddButton_Click" />
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                AutoPostBack="False" />
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                            <FooterRow>
                            
                                <table align="right" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <!--最低訂單金額-->
                                            <dx:ASPxLabel ID="Literal20" runat="server" Text="<%$ Resources:WebResources, MinimumOrderAmount %>">
                                            </dx:ASPxLabel>
                                            ：<dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="0">
                                            </dx:ASPxLabel>
                                            元
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <!--訂單總價-->
                                            <dx:ASPxLabel ID="Literal18" runat="server" Text="<%$ Resources:WebResources, OrderAmount %>">
                                            </dx:ASPxLabel>
                                            ：<dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="0">
                                            </dx:ASPxLabel>元
                                        </td>
                                    </tr>
                                </table>
                            </FooterRow>
                        </Templates>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <Settings ShowFooter="True" ShowTitlePanel="True" />
                    </cc:ASPxGridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" 
                            OnClick="btnSave_Click" />
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Discard %>" />
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
<%--    <cc:ASPxPopupControl ID="ASPxPopupControl1" SkinID="ConsignmentVendorsPopup" runat="server"
        PopupElementID="ASPxButton1" TargetElementID="TextBox1" LoadingPanelID="lp">
    </cc:ASPxPopupControl>
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
--%></asp:Content>
