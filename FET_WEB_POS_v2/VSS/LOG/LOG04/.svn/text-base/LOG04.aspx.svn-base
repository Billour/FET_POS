<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG04.aspx.cs" Inherits="VSS_LOG_LOG04" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div>
            <div class="titlef">
                <!--系統參數設定-->
                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SystemParametersSetting %>" />
            </div>
            
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--參數代碼-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ParameterCode %>" />：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--參數名稱-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ParameterName %>" />：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox2" runat="server" ></dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--參數分類-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ParameterCategory %>" />：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            &nbsp;
                        </td>
                        <td class="tdval">
                            &nbsp;
                        </td>
                        <td class="tdtxt">
                            &nbsp;
                        </td>
                        <td class="tdval">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="btnPosition">
                <table align="center" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                                OnClick="btnSearch_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="seperate"></div>
            
            <div class="SubEditBlock">
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PARA_ID" Width="100%" 
                    OnPageIndexChanged="grid_PageIndexChanged"
                    OnRowUpdating="gvMaster_RowUpdating" 
                    OnRowInserting="gvMaster_RowInserting" 
                    oncelleditorinitialize="gvMaster_CellEditorInitialize" 
                    OnStartRowEditing="gvMaster_StartRowEditing" 
                    OnInitNewRow="gvMaster_InitNewRow" 
                    OnRowValidating="gvMaster_RowValidating">
                    <Columns>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0">
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="SYS_PARA_TYPE_ID" runat="server" Caption="<%$ Resources:WebResources, ParameterCategory %>"
                            VisibleIndex="1" >
                            <DataItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("SYS_PARA_TYPE_NAME") %>'></asp:Label>
                            </DataItemTemplate>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="PARA_KEY" runat="server" Caption="<%$ Resources:WebResources, ParameterCode %>"
                            VisibleIndex="2" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"  PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="必填欄位" 
                            PropertiesTextEdit-MaxLength="50">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PARA_NAME" runat="server" Caption="<%$ Resources:WebResources, ParameterName %>"
                            VisibleIndex="3" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"  PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="必填欄位" 
                            PropertiesTextEdit-MaxLength="250">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PARA_VALUE" runat="server" Caption="<%$ Resources:WebResources, ParameterValue %>"
                            VisibleIndex="4" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-RequiredField-ErrorText="必填欄位" 
                            PropertiesTextEdit-MaxLength="250">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PARA_DESC" runat="server" Caption="<%$ Resources:WebResources, RemarkAndDescription %>"
                            VisibleIndex="5" PropertiesTextEdit-MaxLength="250">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                            VisibleIndex="6">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_USER" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                            VisibleIndex="7">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
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
                                            OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <Settings ShowTitlePanel="true" />
                    <SettingsPager PageSize="10" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
