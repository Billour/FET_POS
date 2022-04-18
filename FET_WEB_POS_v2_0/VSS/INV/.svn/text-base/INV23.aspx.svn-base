<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV23.aspx.cs" Inherits="VSS_INV23_INV23" %>

<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--倉別設定作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, WarehousingSettings %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="seperate">
            </div>
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
                Width="100%" AutoGenerateColumns="true" EnableRowsCache="true" OnRowInserting="gvMaster_RowInserting"
                Settings-ShowTitlePanel="true">
                <Columns>
                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0">
                        <EditButton Visible="false" Text="編輯">
                        </EditButton>
                        <UpdateButton Text="更新">
                        </UpdateButton>
                        <CancelButton Text="取消">
                        </CancelButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="項次" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                        VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" >
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                       
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="倉別名稱" runat="server" Caption="<%$ Resources:WebResources, WarehouseName %>"
                        VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" >
                   
                      
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxTextBox ID="WarehouseName" runat="server" >
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center"
                        Caption="<%$ Resources:WebResources, Marketable %>" VisibleIndex="3" >
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <DataItemTemplate>
                            <dx:ASPxCheckBox ID="CheckItem" runat="server" />
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataCheckColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" 
                        Caption="<%$ Resources:WebResources, VerifyImei %>" VisibleIndex="4">
                        <DataItemTemplate>
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                <Items>
                                    <dx:ListEditItem Text="不控管" Selected="true" />
                                    <dx:ListEditItem Text="銷售時記錄" />
                                    <dx:ListEditItem Text="銷售時確認" />
                                    <dx:ListEditItem Text="庫存異動控管" />
                                </Items>
                            </dx:ASPxComboBox>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                <Items>
                                    <dx:ListEditItem Text="不控管" Selected="true" />
                                    <dx:ListEditItem Text="銷售時記錄" />
                                    <dx:ListEditItem Text="銷售時確認" />
                                    <dx:ListEditItem Text="庫存異動控管" />
                                </Items>
                            </dx:ASPxComboBox>
                        </EditItemTemplate>
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                        VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                     
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                        VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsPager PageSize="5" />
                <SettingsEditing Mode="Inline" />
                <Templates>
                    <TitlePanel>
                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                        OnClick="btnNew_Click" Visible="true">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
            </cc:ASPxGridView>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                            OnClick="btnSave_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                            OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </div>
</asp:Content>
