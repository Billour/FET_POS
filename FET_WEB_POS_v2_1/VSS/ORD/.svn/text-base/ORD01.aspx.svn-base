<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ORD01.aspx.cs" Inherits="VSS_ORD_ORD01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--訂貨作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderPlacement %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
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
                            <asp:Literal ID="Literal4" runat="server" Text="訊息"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label2" runat="server" Text="當日總訂購量已出貨"></asp:Label>
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
                        OnHtmlRowCreated="drMasterDV_HtmlRowCreated" OnCustomButtonCallback="drMasterDV_CustomButtonCallback"
                        OnRowCommand="drMasterDV_RowCommand">
                        <Columns>
                            <dx:GridViewDataButtonEditColumn VisibleIndex="0">
                                <DataItemTemplate>
                                    <dx:ASPxButton ID="ASPxButton3" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>">
                                    </dx:ASPxButton>
                                </DataItemTemplate>
                            </dx:GridViewDataButtonEditColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                CellStyle-HorizontalAlign="Center">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                CellStyle-HorizontalAlign="Right">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                CellStyle-HorizontalAlign="Left">
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="建議訂購量" Caption="<%$ Resources:WebResources, RecommendedOrderQuantity %>"
                                CellStyle-HorizontalAlign="Right">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="網購需求量" Caption="<%$ Resources:WebResources, EconomicOrderQuantity %>"
                                CellStyle-HorizontalAlign="Right">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="門市庫存量" Caption="<%$ Resources:WebResources, StockQuantity %>"
                                CellStyle-HorizontalAlign="Right">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="在途量" Caption="<%$ Resources:WebResources, OnOrderQuantity %>"
                                CellStyle-HorizontalAlign="Right">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="當日訂購量" Caption="<%$ Resources:WebResources, IntraDayOrderQuantity %>">
                                <DataItemTemplate>
                                    <div align="right">
                                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="30px" Text='<%#BIND("當日訂購量") %>'
                                            CellStyle-HorizontalAlign="Right">
                                        </dx:ASPxTextBox>
                                    </div>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="9" FieldName="當日總訂購量" Caption="<%$ Resources:WebResources, IntraDayTotalOrderQuantity %>"
                                CellStyle-HorizontalAlign="Right">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="10" FieldName="驗收量" Caption="<%$ Resources:WebResources, InspectionQuantity %>"
                                CellStyle-HorizontalAlign="Right">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
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
                                        <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="訂購量" Caption="<%$ Resources:WebResources, OrderQuantity %>">
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
                        <SettingsPager PageSize="10">
                        </SettingsPager>
                        <SettingsDetail ShowDetailRow="true" ShowDetailButtons="False" />
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
                                <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, Transfer %>" >
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
        
    </div>
</asp:Content>
