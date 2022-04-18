<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ORD13.aspx.cs" Inherits="VSS_ORD_ORD13" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--卡片安全量暨捕貨量設定-->
                    <asp:Literal ID="Literal1" runat="server" Text="卡片安全量暨捕貨量設定"></asp:Literal>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td>
                    <!--門市編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width: 180px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!--門市名稱-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtStoreName" runat="server" Width="120">
                    </dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click">
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMasterDV" Settings-ShowTitlePanel="true" runat="server" KeyFieldName="項次"
        ClientInstanceName="gvMasterDV" AutoGenerateColumns="False" Width="100%" SettingsEditing-Mode="Inline"
        OnPageIndexChanged="gvMasterDV_PageIndexChanged" OnRowUpdating="gvMasterDV_RowUpdating"
        OnRowInserting="gvMasterDV_RowInserting" EnableCallBacks="False" OnFocusedRowChanged="gvMasterDV_FocusedRowChanged"
        OnHtmlRowPrepared="gvMasterDV_HtmlRowPrepared" OnHtmlRowCreated="gvMasterDV_HtmlRowCreated"
        OnStartRowEditing="gvMasterDV_StartRowEditing">
        <ClientSideEvents RowDblClick="function(s, e) {
                                    gvMasterDV.StartEditRow(e.visibleIndex);
                                    }" />
        <SettingsEditing Mode="Inline" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                <HeaderTemplate>
                    <input type="checkbox" onclick="gvMasterDV.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn ButtonType="Button">
                <HeaderCaptionTemplate>
                </HeaderCaptionTemplate>
                <EditButton Visible="true">
                </EditButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>">
                <EditItemTemplate>
                    &nbsp;</EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>">
                <EditItemTemplate>
                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup"
                        Text='<%#Eval("[門市編號]") %>' />
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>"
                ReadOnly="true">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                ReadOnly="true">
                <EditItemTemplate>
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#Eval("[更新人員]") %>'>
                    </dx:ASPxLabel>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                ReadOnly="true">
                <EditItemTemplate>
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text='<%#Eval("[更新日期]") %>'>
                    </dx:ASPxLabel>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <TitlePanel>
                <table align="left">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { gvMasterDV.AddNewRow(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                </dx:ASPxButton>
                                
                                 <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True"
                                     AllowResize="True" CloseAction="CloseButton" 
                                     PopupElementID="btnImport" HeaderText="卡片安全量暨補貨量匯入"
                                     ContentUrl="~/VSS/ORD/ORD13_Import.aspx" Width="640" Height="400" LoadingPanelID="lp">
                                     <ContentStyle>
                                         <Paddings Padding="4px"></Paddings>
                                     </ContentStyle>
                                 </cc:ASPxPopupControl>
                                <dx:ASPxLoadingPanel ID="lp" runat="server"></dx:ASPxLoadingPanel>  
                        </td>
                    </tr>
                </table>
            </TitlePanel>
        </Templates>
        <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
        <SettingsPager PageSize="5">
        </SettingsPager>
        <Settings ShowTitlePanel="True" />
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
    <div id="divDetails">
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%"
            Visible="false">
            <TabPages>
                <dx:TabPage Text="卡片群組設定">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl1" runat="server">
                            <div>
                                <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                                    Width="100%" OnPageIndexChanged="gvDetail_PageIndexChanged" KeyFieldName="項次"
                                    AutoGenerateColumns="False" OnRowInserting="gvDetail_RowInserting" OnRowUpdating="gvDetail_RowUpdating">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input type="checkbox" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button">
                                            <EditButton Visible="true">
                                            </EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                            HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="卡片群組" Caption="卡片群組" HeaderStyle-HorizontalAlign="Center">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="安全庫存量" Caption="安全庫存量" HeaderStyle-HorizontalAlign="Center"
                                            ReadOnly="true">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="最低庫存量" Caption="最低庫存量" HeaderStyle-HorizontalAlign="Center"
                                            ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="補貨量" Caption="補貨量" HeaderStyle-HorizontalAlign="Center"
                                            ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="已補貨量" Caption="己補貨量" HeaderStyle-HorizontalAlign="Center"
                                            ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsPager PageSize="5">
                                    </SettingsPager>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                            AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) { gvDetail.AddNewRow(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
</asp:Content>
