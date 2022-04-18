<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DISGridViewPanel.ascx.cs"
    Inherits="DISGridViewPanel" %>
<%@ Register Src="PopupControl.ascx" TagName="PopupControl" TagPrefix="uc" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<div>
    <input type="hidden" id="hdItemName" runat="server" class="hdItemName" />
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
        AutoGenerateColumns="true" OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared"
        OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
        OnCancelRowEditing="gvMaster_CancelRowEditing">
        <Columns>
        </Columns>
        <Templates>
            <TitlePanel>
                <table align="left" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                OnClick="btnAdd_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                            </dx:ASPxButton>
                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl="~/VSS/DIS/DIS01_Corresponding_Import.aspx"
                                Width="640" Height="400" LoadingPanelID="lp" HeaderText="客戶對應名單上傳">
                                <ContentStyle>
                                    <Paddings Padding="4px"></Paddings>
                                </ContentStyle>
                            </cc:ASPxPopupControl>
                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                            </dx:ASPxLoadingPanel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnTemplate" runat="server" Text="Template">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnTimes" runat="server" Text="均分次數" Visible="false">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxLabel ID="lblRemainingTimes" runat="server" Text="剩餘數量：1" Visible="false">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </TitlePanel>
        </Templates>
        <Settings ShowTitlePanel="True" />
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsEditing Mode="Inline" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
</div>
