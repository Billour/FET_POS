<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ORD10.aspx.cs" Inherits="VSS_ORD_ORD10" Title="權重佔比分配" ValidateRequest="false" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/ecmascript">
        function onOK() {
            __doPostBack('<%= txtBatchNO.UniqueID %>', 'AAA');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <input type="hidden" id="txtBatchNO" runat="server" class="txtBatchNO" />

    <div>
        <div class="titlef">
            <!--權重佔比分配-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, WeightRatingAssignment %>"></asp:Literal>
        </div>
        <div>
            <div>
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--區域-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, District %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="cbZone" ClientInstanceName="cbZone" SelectedIndex="0" runat="server"
                                Width="100px" TextField="ZONE_NAME" ValueField="ZONE">
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <uc2:PopupControl ID="SelectStore1" runat="server" PopupControlName="StoresPopup" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" ClientInstanceName="btnSearch" OnClick="btnSearch_Click"
                                runat="server" Text="<%$ Resources:WebResources, Search %>">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"/>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <div id="divContent" runat="server">
                    <cc:ASPxGridView ID="gvMaster" KeyFieldName="ITEMS" ClientInstanceName="gvMaster" runat="server" Width="100%"
                        OnPageIndexChanged ="gvMaster_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="<%$ Resources:WebResources, Items %>">
                                <DataItemTemplate>
                                    <%#Container.ItemIndex + 1%>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn FieldName="ZONE_NAME" Caption="<%$ Resources:WebResources, District %>">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="WEIGHT" Caption="<%$ Resources:WebResources, Ratio %>">
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>"
                                                AutoPostBack="false" CausesValidation="false" >
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                                AutoPostBack="false" CausesValidation="false" OnClick="btnExport_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                                <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                            CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl='~/VSS/ORD/ORD10/ORD10_Import.aspx'
                                            Width="640" Height="500" LoadingPanelID="lp" HeaderText="權重佔比分配上傳" onOKScript="onOK">
                                            <ContentStyle>
                                                <Paddings Padding="4px"></Paddings>
                                            </ContentStyle>
                                        </cc:ASPxPopupControl>
                                <dx:ASPxLoadingPanel ID="lp" runat="server">
                                </dx:ASPxLoadingPanel>
                            </TitlePanel>
                        </Templates>
                        <Settings ShowTitlePanel="True" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    </cc:ASPxGridView>
                </div>
                <div class="seperate"></div>
                <div id="Div03" style="width: 90%; text-align: right;" visible="false">
                    <!--比率統計-->
                    <asp:Literal ID="Literal01" runat="server" Text="比率統計"></asp:Literal>
                    <asp:Literal ID="Literal02" runat="server" Text="："></asp:Literal>
                    <asp:Literal ID="Literal03" runat="server" Text="100%"></asp:Literal>
                </div>
                <div class="seperate">
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
