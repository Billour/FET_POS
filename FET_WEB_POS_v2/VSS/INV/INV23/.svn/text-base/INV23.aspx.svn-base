<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeFile="INV23.aspx.cs" Inherits="VSS_INV_INV23" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--倉別設定作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, WarehousingSettings %>"></asp:Literal>
        </div>
        
        <div class="seperate"></div>     
           
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div>

                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="LOC_ID"
                        Width="100%" AutoGenerateColumns="true" EnableRowsCache="true" EnableCallBacks="false" 
                        OnRowInserting="gvMaster_RowInserting"
                        OnHtmlRowCreated="gvMaster_HtmlRowCreated" 
                        OnCellEditorInitialize="gvMaster_CellEditorInitialize"
                        OnRowValidating="gvMaster_RowValidating"
                        OnPageIndexChanged="gvMaster_PageIndexChanged" 
                        OnRowUpdating="gvMaster_RowUpdating" >
                        <Columns>
                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0" Caption=" ">
                                <EditButton Visible="true">
                                </EditButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="ITEMNO" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                                VisibleIndex="0" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="STOCK_NAME" runat="server" Caption="<%$ Resources:WebResources, WarehouseName %>"
                                VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center"
                                PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-SetFocusOnError="true"
                                PropertiesTextEdit-MaxLength="50" PropertiesTextEdit-ValidationSettings-ErrorText="必填欄位">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataCheckColumn FieldName="SALES_FLAG" HeaderStyle-HorizontalAlign="Center"
                                CellStyle-HorizontalAlign="Center" Caption="<%$ Resources:WebResources, Marketable %>"
                                VisibleIndex="2">
                                <DataItemTemplate>
                                    <dx:ASPxCheckBox runat="server" ID="chkSFLAG" ReadOnly="true">
                                    </dx:ASPxCheckBox>
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="CHECK_IMEI_TYPE_NAME" HeaderStyle-HorizontalAlign="Center"
                                CellStyle-HorizontalAlign="Center" Caption="<%$ Resources:WebResources, VerifyImei %>"
                                VisibleIndex="3">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataDateColumn FieldName="MODI_DTM" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                VisibleIndex="12">
                                <PropertiesDateEdit DisplayFormatInEditMode="true" DisplayFormatString="yyyy/MM/dd HH:mm:ss"
                                    DropDownButton-Enabled="false" DropDownButton-Visible="false">
                                    <DropDownButton Enabled="False" Visible="False">
                                    </DropDownButton>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                </PropertiesDateEdit>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn FieldName="MODI_USER_NAME" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                VisibleIndex="13">
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                    <ValidationSettings>
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                OnClick="btnNew_Click" Visible="true" ClientInstanceName="btnNew">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <SettingsPager PageSize="5" />
                        <SettingsEditing Mode="Inline" />
                        <SettingsBehavior AllowFocusedRow="true" />  
                        <Settings ShowTitlePanel="true" />                      
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    </cc:ASPxGridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
