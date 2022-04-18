<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG05.aspx.cs" Inherits="VSS_LOG_LOG05"
    MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--功能清單設定-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, FunctionList %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--系統別-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, System %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                <dx:ListEditItem Value="1" Text="OnLine" />
                                <dx:ListEditItem Value="2" Text="OffLine" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--功能代碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, FunctionCode %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--功能狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, FunctionStatus %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList1" runat="server" >
                            <Items>
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                <dx:ListEditItem Value="1" Text="有效" />
                                <dx:ListEditItem Value="2" Text="已失效" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--模組名稱-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModuleName %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList4" runat="server">
                            <Items>
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                <dx:ListEditItem Value="1" Text="模組一" />
                                <dx:ListEditItem Value="2" Text="模組二" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--功能名稱-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, FunctionName %>" />：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
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
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
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
            <div id="Div2" class="SubEditBlock" style="text-align: left;">
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="功能代碼"
                    Settings-ShowTitlePanel="true" Width="100%" OnHtmlRowPrepared="grid_HtmlRowPrepared"
                    OnHtmlRowCreated="grid_HtmlRowCreated" 
                    OnPageIndexChanged="grid_PageIndexChanged" onrowupdating="grid_RowUpdating">
                    <Columns>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="狀態" runat="server" Caption="<%$ Resources:WebResources, Status %>"
                            VisibleIndex="2">
                            <EditItemTemplate>
                                <dx:ASPxComboBox ID="ddlStatus" runat="server" Width="60px">
                                    <Items>
                                        <dx:ListEditItem Value="0" Text="有效" Selected="true" />
                                        <dx:ListEditItem Value="1" Text="已失效" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </EditItemTemplate>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="系統別" runat="server" Caption="<%$ Resources:WebResources, System %>"
                            VisibleIndex="3">
                            <EditItemTemplate>
                                <dx:ASPxComboBox ID="ddlSystem" runat="server" Width="80px">
                                    <Items>
                                        <dx:ListEditItem Value="0" Text="OnLine" Selected="true" />
                                        <dx:ListEditItem Value="1" Text="OffLine" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </EditItemTemplate>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="模組名稱" runat="server" Caption="<%$ Resources:WebResources, ModuleName %>"
                            VisibleIndex="4">
                            <EditItemTemplate>
                                <dx:ASPxComboBox ID="ddlModuleName" runat="server" Width="60px">
                                     <Items>
                                        <dx:ListEditItem Value="0" Text="模組一" Selected="true" />
                                        <dx:ListEditItem Value="1" Text="模組二" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </EditItemTemplate>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="功能代碼" runat="server" Caption="<%$ Resources:WebResources, FunctionCode %>"
                            VisibleIndex="5">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="功能名稱" runat="server" Caption="<%$ Resources:WebResources, FunctionName %>"
                            VisibleIndex="6">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="URL" runat="server" Caption="<%$ Resources:WebResources, Url %>"
                            VisibleIndex="7">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                            VisibleIndex="8">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                            VisibleIndex="9">
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
                                        <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>" Visible="true" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                            </div> </div>
                        </TitlePanel>                       
                    </Templates>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                </cc:ASPxGridView>
            </div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    </td>
                </tr>
            </table>
        </div>
    
</asp:Content>
