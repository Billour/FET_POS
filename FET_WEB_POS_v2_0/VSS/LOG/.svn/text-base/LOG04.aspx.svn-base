<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG04.aspx.cs" Inherits="VSS_LOG_LOG04"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
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
                            <Items>
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                <dx:ListEditItem Value="1" Text="參數一" />
                                <dx:ListEditItem Value="2" Text="參數二" />
                            </Items>
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
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="參數分類" Settings-ShowTitlePanel="true"
                Width="100%" OnHtmlRowPrepared="grid_HtmlRowPrepared" OnHtmlRowCreated="grid_HtmlRowCreated"
                OnPageIndexChanged="grid_PageIndexChanged" OnRowUpdating="grid_RowUpdating">
                <Columns>
                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                        <EditButton Visible="true">
                        </EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="參數分類" runat="server" Caption="<%$ Resources:WebResources, ParameterCategory %>"
                        VisibleIndex="2">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
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
