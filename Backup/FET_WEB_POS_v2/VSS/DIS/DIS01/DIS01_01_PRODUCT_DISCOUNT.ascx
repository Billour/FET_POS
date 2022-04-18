<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DIS01_01_PRODUCT_DISCOUNT.ascx.cs" Inherits="PRODUCT_DISCOUNT" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<dx:ASPxCallbackPanel ID="ac1" runat="server" ClientInstanceName="ac1" OnCallback="ac1_Callback">
    <PanelCollection>
        <dx:PanelContent>
            <cc:ASPxGridView ID="gvProd" ClientInstanceName="gvProd" runat="server" Width="100%"
                KeyFieldName="PRODNO"
                OnRowInserting="gvProd_RowInserting" 
                OnRowValidating="gvProd_RowValidating" 
                OnRowUpdating="gvProd_RowUpdating"
                OnStartRowEditing="gvProd_StartRowEditing" 
                OnPageIndexChanged="gvProd_PageIndexChanged">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <div style="text-align: center">
                                <input type="checkbox" onclick="gvProd.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </div>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn ButtonType="Button">
                        <HeaderCaptionTemplate>
                        </HeaderCaptionTemplate>
                        <EditButton Visible="true">
                        </EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="商品料號">
                        <EditItemTemplate>
                            <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup"
                                Text='<%#Bind("PRODNO") %>' IsValidation="true"
                                ValidationGroup='<%# Container.ValidationGroup %>' KeyFieldValue1="extrasale" OnClientTextChanged="function(s,e){ getPRODINFO(s,e,'PRODNAME');}" />
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="商品名稱">
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtProdName" runat="server" Text='<%# Bind("PRODNAME") %>'
                                ClientInstanceName="PRODNAME" ReadOnly="true">
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
                                    <dx:ASPxButton ID="btnProdAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                        OnClick="btnProdAdd_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton SkinID="DeleteBtn" ID="btnProdDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                        CausesValidation="false" OnClick="btnProdDelete_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" CausesValidation="false">
                                    </dx:ASPxButton>
                                    <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl='~/VSS/DIS/DIS01/DIS01_Import.aspx'
                                        Width="640" Height="600" LoadingPanelID="lp" HeaderText="指定商品上傳" onOKScript="onOK1">
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
                                    <dx:ASPxButton ID="btnProdTemplate" runat="server" Text="Template" CausesValidation="false" OnClick="btnProdTemplate_Click">
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