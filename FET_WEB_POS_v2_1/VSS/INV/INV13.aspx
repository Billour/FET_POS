<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV13.aspx.cs" Inherits="VSS_INV_INV13"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                
                <tr>
                    <td align="left">
                        <!--無訂單進貨資料查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, NoPurchaseOrderDataEntrySearch %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%">
                <tr>
                    <!--進貨日期-->
                    <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources,ReceivedDate %>">：
                        </asp:Literal>
                    </td>
                    <td nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ReceivedStartDate" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ReceivedEndDate" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div class="seperate">
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
                                <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
        </div>
       <cc:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
            KeyFieldName="項次" OnHtmlRowCreated="gvMaster_HtmlRowCreated1">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="項次" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                    VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="進貨單號" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ReceivingNoteNumber %>"
                    VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="進貨日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ReceivedDate %>"
                    VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="備註" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Remark %>"
                    VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    VisibleIndex="4">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="進貨單號" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ReceivingNoteNumber %>"
                    VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="進貨日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ReceivedDate %>"
                    VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="備註" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Remark %>"
                    VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                    VisibleIndex="5">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="Inline" />
            <SettingsPager PageSize="5" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
            <Templates>
                <DetailRow>
                    <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="detailGrid" Width="100%"
                        KeyFieldName="項次" >
                        <Columns>
                            <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                VisibleIndex="0" />
                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="單位" Caption="<%$ Resources:WebResources, Unit %>"
                                VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>"
                                VisibleIndex="3" />
                            <dx:GridViewDataColumn FieldName="總金額" Caption="<%$ Resources:WebResources, TotalAmount %>"
                                VisibleIndex="4" />
                        </Columns>
                    </cc:ASPxGridView>
                </DetailRow>
            </Templates>
            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
        </cc:ASPxGridView>
    </div>
</asp:Content>
