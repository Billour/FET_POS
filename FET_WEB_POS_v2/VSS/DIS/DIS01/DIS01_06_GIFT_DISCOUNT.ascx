<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DIS01_06_GIFT_DISCOUNT.ascx.cs" Inherits="GIFT_DISCOUNT" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<dx:ASPxCallbackPanel ID="ac6" runat="server" ClientInstanceName="ac6" OnCallback="ac6_Callback">
    <PanelCollection>
        <dx:PanelContent>
            <cc:ASPxGridView ID="gvSet" ClientInstanceName="gvSet" runat="server" Width="100%"
                AutoGenerateColumns="false" KeyFieldName="PRODNO"
                OnRowInserting="gvSet_RowInserting" 
                OnRowValidating="gvSet_RowValidating" 
                OnStartRowEditing="gvSet_StartRowEditing"
                OnRowUpdating="gvSet_RowUpdating" 
                OnPageIndexChanged="gvSet_PageIndexChanged">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <div style="text-align: center">
                                <input type="checkbox" onclick="gvSet.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
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
                            <uc1:PopupControl ID="txtSetProdNo" runat="server" PopupControlName="ProductsPopup"
                                Text='<%#Bind("PRODNO") %>' IsValidation="true"
                                ValidationGroup='<%# Container.ValidationGroup %>' KeyFieldValue1="extrasale" OnClientTextChanged="function(s,e){ getPRODINFO(s,e,'PRODNAME1');}" />
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="商品名稱">
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtSetProdName" runat="server" Text='<%# Bind("PRODNAME") %>'
                                ClientInstanceName="PRODNAME1" ReadOnly="true" Width="100%">
                                <ReadOnlyStyle Border-BorderStyle="None">
                                </ReadOnlyStyle>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AMT" Caption="贈品金額">
                        <PropertiesTextEdit Width="100">
                            <ValidationSettings SetFocusOnError="true">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                <RegularExpression ValidationExpression="\d*" ErrorText="請輸入正整數" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnSetProdAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                        OnClick="btnSetProdAdd_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton SkinID="DeleteBtn" ID="btnSetProdDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                        CausesValidation="false" OnClick="btnSetProdDelete_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnImport4" runat="server" Text="<%$ Resources:WebResources, Import %>" CausesValidation="false">
                                    </dx:ASPxButton>
                                    <cc:ASPxPopupControl ID="ASPxPopupControl5" runat="server" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" PopupElementID="btnImport4" ContentUrl="~/VSS/DIS/DIS01/DIS01_Gift_Import.aspx"
                                        Width="640" Height="550" LoadingPanelID="lp" HeaderText="贈品上傳" onOKScript="onOK6">
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
                                    <dx:ASPxButton ID="btnSetTemplate" runat="server" Text="Template" CausesValidation="false" OnClick="btnSetTemplate_Click">
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