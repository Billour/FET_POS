<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LEA02.aspx.cs" Inherits="VSS_LEA02_LEA02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <asp:Literal ID="Literal10" runat="server" Text="可租賃設備查詢"></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--類別-->
                    <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <dx:ASPxRadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow" Border-BorderStyle="None">
                        <Items>
                            <dx:ListEditItem Value="1" Text="漫遊租賃" />
                            <dx:ListEditItem Value="2" Text="維修租賃" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--區域-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, District %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList2" runat="server" Width="100">
                        <Items>
                            <dx:ListEditItem Value="-請選擇-" Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Value="北區" Text="北區" />
                            <dx:ListEditItem Value="中區" Text="中區" />
                            <dx:ListEditItem Value="南區" Text="南區" />
                            <dx:ListEditItem Value="東區" Text="東區" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--庫存地點-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList1" runat="server" Width="100">
                        <Items>
                            <dx:ListEditItem Value="-請選擇-" Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Value="地點1" Text="地點1" />
                            <dx:ListEditItem Value="地點2" Text="地點2" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="right">
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="SubEditBlock">
                <div class="GridScrollBar" style="height: auto">
                    <dx:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                        KeyFieldName="庫存地點" OnDetailRowExpandedChanged="gvMaster_DetailRowExpandedChanged"
                        OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                        <Columns>
                            <dx:GridViewDataColumn VisibleIndex="0" Caption="<%$ Resources:WebResources, StorageLocation %>"
                                FieldName="庫存地點">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="手機類型" Caption="<%$ Resources:WebResources, MobileType %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="庫存量" Caption="<%$ Resources:WebResources, StockQuantity %>"
                                VisibleIndex="2" />
                        </Columns>
                        <Templates>
                            <DetailRow>
                                <cc:ASPxGridView ID="detailGrid" runat="server" ClientInstanceName="detailGrid" Width="100%"
                                    OnBeforePerformDataSelect="detailGrid_DataSelect" OnPageIndexChanged="detailGrid_PageIndexChanged"
                                    OnBeforeColumnSortingGrouping="detailGrid_BeforeColumnSortingGrouping" AutoGenerateColumns="False"
                                    KeyFieldName="手機序號">
                                    <Columns>
                                        <dx:GridViewDataTextColumn VisibleIndex="0">
                                            <DataItemTemplate>
                                                <table cellpadding="0" cellspacing="0" border="0" align="left">
                                                    <tr>
                                                        <td align="right">
                                                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Reserve %>"
                                                                OnClick="ASPxButton1_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <dx:ASPxButton ID="btnAdd2" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                AutoPostBack="false">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataColumn FieldName="庫存地點" Caption="<%$ Resources:WebResources, StorageLocation %>"
                                            VisibleIndex="1" />
                                        <dx:GridViewDataColumn FieldName="手機類型" Caption="<%$ Resources:WebResources, MobileType %>"
                                            VisibleIndex="2" />
                                        <dx:GridViewDataColumn FieldName="手機序號" Caption="<%$ Resources:WebResources, MobileIdentityNumber %>"
                                            VisibleIndex="3" />
                                        <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>"
                                            VisibleIndex="4" />
                                    </Columns>
                                    <Settings ShowFooter="false" />
                                    <SettingsDetail IsDetailGrid="true" />
                                    <SettingsPager PageSize="5">
                                    </SettingsPager>
                                    <Templates>
                                        <EmptyDataRow>
                                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                        </EmptyDataRow>
                                    </Templates>
                                    <Styles>
                                        <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                        </TitlePanel>
                                    </Styles>
                                </cc:ASPxGridView>
                            </DetailRow>
                            <EmptyDataRow>
                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                            </EmptyDataRow>
                        </Templates>
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    </dx:ASPxGridView>
                </div>
                <div class="seperate">
                </div>
                <div class="GridScrollBar" style="height: auto">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
