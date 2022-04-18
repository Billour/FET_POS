<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG04.aspx.cs" Inherits="VSS_LOG_LOG04"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--系統參數設定-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SystemParametersSetting %>" />
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--參數代碼-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ParameterCode %>" />：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td class="tdtxt">
                    <!--參數名稱-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ParameterName %>" />：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--參數分類-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ParameterCategory %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList2" runat="server" Width="80px">
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
                    <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="SubEditBlock">
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="參數分類"
            Settings-ShowTitlePanel="true" Width="100%" OnHtmlRowPrepared="grid_HtmlRowPrepared"
            OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged"
            OnRowUpdating="grid_RowUpdating"              
            oncelleditorinitialize="gvMaster_CellEditorInitialize">
            <Columns>
                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                    <EditButton Visible="true">
                    </EditButton>
                </dx:GridViewCommandColumn>
                <%--<dx:GridViewDataComboBoxColumn FieldName="參數分類" runat="server" Caption="<%$ Resources:WebResources, ParameterCategory %>"
                    VisibleIndex="2">
                    <EditItemTemplate>
                        <dx:ASPxComboBox ID="DropDownList4" runat="server" Width="100px" Text='<%#BIND("[參數分類]")  %>'>
                            <Items>
                            
                                <dx:ListEditItem Value="1" Text="系統管理" />
                                <dx:ListEditItem Value="2" Text="基本資料設定" />
                                <dx:ListEditItem Value="3" Text="日結管理" />
                                <dx:ListEditItem Value="4" Text="庫存管理" />
                                <dx:ListEditItem Value="5" Text="訂貨管理" />
                                <dx:ListEditItem Value="6" Text="寄銷管理" />
                                <dx:ListEditItem Value="7" Text="預購管理" />
                                <dx:ListEditItem Value="8" Text="銷售管理" />
                                <dx:ListEditItem Value="9" Text="租賃管理" />
                                <dx:ListEditItem Value="10" Text="商品管理" />
                                
                            </Items>
                        </dx:ASPxComboBox>
                    </EditItemTemplate>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataComboBoxColumn>--%>
                <%--<dx:GridViewCommandColumn FieldName="參數分類" runat="server" Caption="<%$ Resources:WebResources, ParameterCategory %>"
                    VisibleIndex="2">
                </dx:GridViewCommandColumn>--%>
                <dx:GridViewDataComboBoxColumn FieldName="參數分類" runat="server" Caption="<%$ Resources:WebResources, ParameterCategory %>"
                    VisibleIndex="2">
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="參數代碼" runat="server" Caption="<%$ Resources:WebResources, ParameterCode %>"
                    VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="參數名稱" runat="server" Caption="<%$ Resources:WebResources, ParameterName %>"
                    VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="值" runat="server" Caption="<%$ Resources:WebResources, ParameterValue %>"
                    VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="備註說明" runat="server" Caption="<%$ Resources:WebResources, RemarkAndDescription %>"
                    VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                    VisibleIndex="7">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    VisibleIndex="8">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
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
                                    OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>
    </div>
</asp:Content>
