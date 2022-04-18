<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ORD12_view.aspx.cs" Inherits="VSS_ORD_ORD12_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, BookOrderDetail %>"></asp:Literal>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--預訂單編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, BookOrderNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lblOrderNo" runat="server" Text="PR2101-10070101"></asp:Label>
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
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <div id="divContent" runat="server" class="SubEditBlock">
                <cc:ASPxGridView ID="drMasterDV" Width="100%" runat="server" ClientInstanceName="drMasterDV"
                    AutoGenerateColumns="False" KeyFieldName="項次" OnHtmlRowCreated="drMasterDV_HtmlRowCreated"
                    OnHtmlCommandCellPrepared="drMasterDV_HtmlCommandCellPrepared" OnRowCommand="drMasterDV_RowCommand">
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
                        <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="預訂量" Caption="<%$ Resources:WebResources, PreOrderQuantity %>" CellStyle-HorizontalAlign="Right">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="總部核准量" Caption="<%$ Resources:WebResources, ApprovedTheAmount %>" CellStyle-HorizontalAlign="Right">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="實際成單數量" Caption="<%$ Resources:WebResources, IntoSingleRealNumber %>" CellStyle-HorizontalAlign="Right">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="失敗原因" Caption="<%$ Resources:WebResources, Failure %>" CellStyle-HorizontalAlign="Right">
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="seperate">
    </div>
</asp:Content>
