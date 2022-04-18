<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DIS01_02_STORE_DISCOUNT.ascx.cs" Inherits="STORE_DISCOUNT" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<dx:ASPxCallbackPanel ID="ac2" runat="server" ClientInstanceName="ac2" OnCallback="ac2_Callback">
    <PanelCollection>
        <dx:PanelContent>
            <cc:ASPxGridView ID="gvStore" ClientInstanceName="gvStore" runat="server" Width="100%"
                AutoGenerateColumns="false" KeyFieldName="STORE_NO"
                OnRowInserting="gvStore_RowInserting"
                OnRowValidating="gvStore_RowValidating"
                OnRowUpdating="gvStore_RowUpdating" 
                OnPreRender="gvStore_PreRender" 
                OnInitNewRow="gvStore_InitNewRow" 
                OnStartRowEditing="gvStore_StartRowEditing" 
                OnPageIndexChanged="gvStore_PageIndexChanged">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <div style="text-align: center">
                                 <input type="checkbox" onclick="gvStore.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </div>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn ButtonType="Button">
                        <HeaderCaptionTemplate>
                        </HeaderCaptionTemplate>
                        <EditButton Visible="true">
                        </EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                        <EditItemTemplate>
                            <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" Text='<%# Bind("STORE_NO") %>'
                                SetClientValidationEvent="getStoreInfo" IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>' />
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>">
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtStoreName" runat="server" Text='<%# Bind("STORENAME") %>'
                                ClientInstanceName="STORENAME" ReadOnly="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                <ReadOnlyStyle Border-BorderStyle="None">
                                </ReadOnlyStyle>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ZONE_NAME" Caption="<%$ Resources:WebResources, ByDistrict %>">
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtZone" runat="server" Text='<%# Bind("ZONE_NAME") %>' ClientInstanceName="ZONE"
                                ReadOnly="true">
                                <ReadOnlyStyle Border-BorderStyle="None">
                                </ReadOnlyStyle>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DIS_USE_COUNT" Caption="折扣上限次數">
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtDisUseCount" runat="server" Text='<%# Bind("DIS_USE_COUNT") %>'
                                ClientInstanceName="DisUseCount" ClientSideEvents-Init="InitDisUseCount" Width="100">
                                <ValidationSettings SetFocusOnError="true">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式，請重新輸入" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnStoreAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                        OnClick="btnStoreAdd_Click" ClientSideEvents-Init="InitDisUseCount">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton SkinID="DeleteBtn" ID="btnStoreDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                       CausesValidation="false" OnClick="btnStoreDelete_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnImport1" runat="server" Text="<%$ Resources:WebResources, Import %>" CausesValidation="false">
                                    </dx:ASPxButton>
                                    <cc:ASPxPopupControl ID="ASPxPopupControl2" runat="server" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" PopupElementID="btnImport1" ContentUrl="~/VSS/DIS/DIS01/DIS01_Store_Import.aspx"
                                        Width="640" Height="400" LoadingPanelID="lp" HeaderText="指定門市上傳" onOKScript="onOK2">
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
                                    <dx:ASPxButton ID="btnStoreTemplate" runat="server" Text="Template" CausesValidation="false" OnClick="btnStoreTemplate_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnTimes" ClientInstanceName="btnTimes" runat="server" Text="均分次數"
                                        ClientVisible="false" OnClick="btnTimes_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                  <%--  <dx:ASPxLabel ID="lblRemainingTimes" ClientInstanceName="lblRemainingTimes" runat="server"
                                        Text="剩餘數量：0" ClientVisible="false">
                                    </dx:ASPxLabel>--%>
                                    
                                    <dx:ASPxTextBox ID="lblRemainingTimes" runat="server" Text="剩餘數量：0" ClientInstanceName="lblRemainingTimes" 
                                        ClientVisible="false" BackColor="#ffcc99" Border-BorderColor="#ffcc99"></dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <Settings ShowTitlePanel="True" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager PageSize="10"></SettingsPager>
                <ClientSideEvents EndCallback="InitDisUseCount" />
            </cc:ASPxGridView>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>