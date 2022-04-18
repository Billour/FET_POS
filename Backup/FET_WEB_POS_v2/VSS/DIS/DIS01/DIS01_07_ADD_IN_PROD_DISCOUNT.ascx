<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DIS01_07_ADD_IN_PROD_DISCOUNT.ascx.cs" Inherits="ADD_IN_PROD_DISCOUNT" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<dx:ASPxCallbackPanel ID="ac7" runat="server" ClientInstanceName="ac7" OnCallback="ac7_Callback">
    <PanelCollection>
        <dx:PanelContent>
            <cc:ASPxGridView ID="gvAddProd" ClientInstanceName="gvAddProd" runat="server" Width="100%"
                AutoGenerateColumns="false" KeyFieldName="PRODNO"
                OnRowInserting="gvAddProd_RowInserting"
                OnRowValidating="gvAddProd_RowValidating" 
                OnStartRowEditing="gvAddProd_StartRowEditing"
                OnRowUpdating="gvAddProd_RowUpdating" 
                OnPageIndexChanged="gvAddProd_PageIndexChanged">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <div style="text-align: center">
                                <input type="checkbox" onclick="gvAddProd.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
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
                            <uc1:PopupControl ID="txtAddProdNo" runat="server" PopupControlName="ProductsPopup"
                                Text='<%#Bind("PRODNO") %>' IsValidation="true"
                                ValidationGroup='<%# Container.ValidationGroup %>' KeyFieldValue1="extrasale" OnClientTextChanged="function(s,e){ getPRODINFO(s,e,'PRODNAME2');}" />
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="商品名稱">
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtAddProdName" runat="server" Text='<%# Bind("PRODNAME") %>'
                                ClientInstanceName="PRODNAME2" ReadOnly="true" Width="100%">
                                <ReadOnlyStyle Border-BorderStyle="None">
                                </ReadOnlyStyle>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DIS_AMT" Caption="折扣金額" EditCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit>
                            <ValidationSettings SetFocusOnError="true">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                <RegularExpression ValidationExpression="\d*" ErrorText="格式錯誤" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="UNIT_PRICE" Caption="單機售價" EditCellStyle-HorizontalAlign="Right" CellStyle-HorizontalAlign="Right">
                        <%--<PropertiesTextEdit>
                            <ValidationSettings SetFocusOnError="true">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                <RegularExpression ValidationExpression="\d*" ErrorText="格式錯誤" />
                            </ValidationSettings>
                        </PropertiesTextEdit>--%>
                        <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtUNIT_PRICE" runat="server" Text='<%# Bind("UNIT_PRICE") %>'
                                ClientInstanceName="UNIT_PRICE" Width="100px" HorizontalAlign="Right" ClientEnabled="false">
                                <ValidationSettings SetFocusOnError="true">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    <RegularExpression ValidationExpression="\d*" ErrorText="格式錯誤" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnAddProdAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                        OnClick="btnAddProdAdd_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton SkinID="DeleteBtn" ID="btnAddProdDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                      CausesValidation="false"  OnClick="btnAddProdDelete_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnImport5" runat="server" Text="<%$ Resources:WebResources, Import %>" CausesValidation="false">
                                    </dx:ASPxButton>
                                    <cc:ASPxPopupControl ID="ASPxPopupControl6" runat="server" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" PopupElementID="btnImport5" ContentUrl="~/VSS/DIS/DIS01/DIS01_Import.aspx"
                                        Width="640" Height="550" LoadingPanelID="lp" HeaderText="加價購商品上傳" onOKScript="onOK7">
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
                                    <dx:ASPxButton ID="btnAddProdTemplate" runat="server" Text="Template" CausesValidation="false" OnClick="btnAddProdTemplate_Click">
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