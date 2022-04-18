<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LEA06.aspx.cs" Inherits="VSS_LEA_LEA06" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
        <!--租賃收費明細表查詢-->
        <asp:Literal ID="Literal11" runat="server" Text="租賃收費明細表查詢"></asp:Literal>
    </div>
    
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                </td>
                <td class="tdval">
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
                <td class="tdtxt">
                    <!--庫存地點-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ComStoreNo" runat="server" Width="100" />
                </td>
                <td class="tdtxt">
                    <!--出國時間-->
                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, DurationAbroad %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="1">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                起
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="SRDATE" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;訖
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ERDATE" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnClear" SkinID="ResetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <div class="SubEditBlock">
        <div class="GridScrollBar" style="height: auto">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
                        AutoGenerateColumns="False" IsClearStatus="True"
                        OnPageIndexChanged="gvMaster_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="NO" VisibleIndex="0" />
                            <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, MobileLocation %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="CUST_NAME" Caption="<%$ Resources:WebResources, CustomerName %>"
                                VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="MSISDN" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                VisibleIndex="3" />
                            <dx:GridViewDataColumn FieldName="REAL_RECEV_DATE" Caption="實際領取日" VisibleIndex="4" />
                            <dx:GridViewDataColumn FieldName="REAL_RETURN_DTM" Caption="實際歸還日" VisibleIndex="5" />
                            <dx:GridViewDataColumn FieldName="RENT_AMT" Caption="<%$ Resources:WebResources, Rent %>"
                                VisibleIndex="6" />
                            <dx:GridViewDataColumn FieldName="IND_AMT" Caption="<%$ Resources:WebResources, Compensation %>"
                                VisibleIndex="7" />
                            <dx:GridViewDataColumn FieldName="IND_ERR" Caption="<%$ Resources:WebResources, ReasonForCompensation %>"
                                VisibleIndex="8" />
                            <dx:GridViewDataColumn FieldName="AMT1" Caption="<%$ Resources:WebResources, DiscountAmount %>"
                                VisibleIndex="9" />
                            <dx:GridViewDataColumn FieldName="SAMT" Caption="<%$ Resources:WebResources, TotalAmount %>"
                                VisibleIndex="10" />
                            <dx:GridViewDataColumn FieldName="RMARK" Caption="<%$ Resources:WebResources, Remark %>"
                                VisibleIndex="11" />
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table cellpadding="0" cellspacing="0" align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
