<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ORD02_1.aspx.cs" Inherits="VSS_ORD_ORD02_ORD02_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--訂貨作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderPlacement %>"></asp:Literal>
                    <dx:ASPxLabel ID="lbStoreNo" ClientInstanceName="lbStoreNo" runat="server" ClientVisible="false" />
                    <dx:ASPxLabel ID="lbCheck" ClientInstanceName="lbCheck" runat="server" ClientVisible="false" />
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="False" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e) { document.location='../ORD02/ORD02.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--訂單編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdtxt">
                    <!--訂單日期-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="lblOrderDate" runat="server" ClientInstanceName="lblOrderDate">
                    </dx:ASPxLabel>
                </td>
                <td class="tdtxt">
                    <!--更新日期-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblUpdDateTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--備註-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                </td>
                <td colspan="3" class="tdval">
                    <dx:ASPxTextBox ID="txtMemo" runat="server" Width="98%" Border-BorderStyle="None"
                        ReadOnly="true">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--更新人員-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblUpdUser" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <div id="divContent" runat="server" class="SubEditBlock">
                <cc:ASPxGridView ID="drMasterDV" Width="100%" runat="server" ClientInstanceName="drMasterDV"
                    KeyFieldName="ORDER_D_TEMP_ID" AutoGenerateColumns="False" OnPageIndexChanged="drMasterDV_PageIndexChanged"
                    OnHtmlCommandCellPrepared="drMasterDV_HtmlCommandCellPrepared" OnRowCommand="drMasterDV_RowCommand"
                    OnHtmlRowCreated="drMasterDV_HtmlRowCreated">
                    <Columns>
                        <dx:GridViewDataButtonEditColumn VisibleIndex="0">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btnOnetoone" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>">
                                </dx:ASPxButton>
                            </DataItemTemplate>
                        </dx:GridViewDataButtonEditColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblITEMNO" runat="server" Text='<%# Bind("ITEMNO") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblPRODNO" runat="server" Text='<%# Bind("PRODNO") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="ORDQTY" Caption="<%$ Resources:WebResources, OrderQuantity %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="APPROVEQTY" Caption="<%$ Resources:WebResources, ApprovedTheAmount %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="CHECK_IN_QTY" Caption="<%$ Resources:WebResources, InspectionQuantity %>">
                        </dx:GridViewDataTextColumn>
                     
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetailDV" Width="80%" runat="server" ClientInstanceName="gvDetailDV"
                                 AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                                EnableRowsCache="true">
                                <Columns>
                                    <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="ORDQTY" Caption="<%$ Resources:WebResources, OrderQuantity %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="APPROVEQTY" Caption="<%$ Resources:WebResources, ApprovedTheAmount %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="CHECK_IN_QTY" Caption="<%$ Resources:WebResources, InspectionQuantity %>">
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
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                    <SettingsDetail ShowDetailRow="true" ShowDetailButtons="False" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
            </div>
            <div class="seperate">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
