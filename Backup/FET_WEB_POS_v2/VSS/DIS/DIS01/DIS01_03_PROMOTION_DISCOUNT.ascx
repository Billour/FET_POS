<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DIS01_03_PROMOTION_DISCOUNT.ascx.cs" Inherits="PROMOTION_DISCOUNT" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<dx:ASPxCallbackPanel ID="ac3" runat="server" ClientInstanceName="ac3" OnCallback="ac3_Callback">
    <PanelCollection>
        <dx:PanelContent>
            <cc:ASPxGridView ID="gvPromo" ClientInstanceName="gvPromo" runat="server" Width="100%"
                AutoGenerateColumns="false" KeyFieldName="PROMO_NO"
                OnRowInserting="gvPromo_RowInserting"
                OnRowValidating="gvPromo_RowValidating"
                OnRowUpdating="gvPromo_RowUpdating" 
                OnStartRowEditing="gvPromo_StartRowEditing" 
                OnPageIndexChanged="gvPromo_PageIndexChanged">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <div style="text-align: center">
                                <input type="checkbox" onclick="gvPromo.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </div>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn ButtonType="Button">
                        <HeaderCaptionTemplate>
                        </HeaderCaptionTemplate>
                        <EditButton Visible="true">
                        </EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="PROMO_NO" Caption="促銷代號">
                        <EditItemTemplate>
                            <uc1:PopupControl ID="txtPromoNo" runat="server" PopupControlName="PromotionsPopupOnly"
                                Text='<%#Bind("PROMO_NO") %>' SetClientValidationEvent="getPromoInfo" IsValidation="true"
                                ValidationGroup='<%# Container.ValidationGroup %>' />
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PROMO_NAME" Caption="促銷名稱">
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtPromoName" runat="server" Text='<%# Bind("PROMO_NAME") %>'
                                ClientInstanceName="PROMONAME" ReadOnly="true" Width="100%">
                                <ReadOnlyStyle Border-BorderStyle="None">
                                </ReadOnlyStyle>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnPromoAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                        OnClick="btnPromoAdd_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton SkinID="DeleteBtn" ID="btnPromoDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                       CausesValidation="false" OnClick="btnPromoDelete_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnImport2" runat="server" Text="<%$ Resources:WebResources, Import %>" CausesValidation="false">
                                    </dx:ASPxButton>
                                    <cc:ASPxPopupControl ID="ASPxPopupControl3" runat="server" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" PopupElementID="btnImport2" ContentUrl="~/VSS/DIS/DIS01/DIS01_Permissions_Import.aspx"
                                        Width="640" Height="400" LoadingPanelID="lp" HeaderText="指定促銷上傳" onOKScript="onOK3">
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
                                    <dx:ASPxButton ID="btnPromoTemplate" runat="server" Text="Template" CausesValidation="false" OnClick="btnPromoTemplate_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <Settings ShowTitlePanel="True" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager PageSize="10"></SettingsPager>
            </cc:ASPxGridView>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>