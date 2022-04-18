<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT04.aspx.cs" Inherits="VSS_OPT_OPT04"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    禮券設定作業
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--公司別-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, CompyCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="ddlCompany" runat="server" Width="100px">
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, status %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="DropDownList2" runat="server" Width="100">
                            <Items>
                                <dx:ListEditItem Value="-請選擇-" Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Value="有效" Text="有效" />
                                <dx:ListEditItem Value="尚未生效" Text="尚未生效" />
                                <dx:ListEditItem Value="已過期" Text="已過期" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
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
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
            Settings-ShowTitlePanel="true" Width="100%" OnHtmlRowPrepared="grid_HtmlRowPrepared"
            OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged"
            OnRowUpdating="grid_RowUpdating">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                    <EditButton Visible="true">
                    </EditButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                    ReadOnly="true" VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="狀態" runat="server" Caption="<%$ Resources:WebResources, Status %>"
                    ReadOnly="true" VisibleIndex="3">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="公司別" runat="server" Caption="<%$ Resources:WebResources, CompyCode %>"
                    VisibleIndex="4">
                    <EditItemTemplate>
                        <dx:ASPxComboBox ID="ddlCompany" runat="server" Width="80px" Text='<%#BIND("[公司別]")  %>'>
                            <Items>
                                <dx:ListEditItem Value="0" Text="遠東百貨" Selected="true" />
                                <dx:ListEditItem Value="1" Text="愛買" />
                                <dx:ListEditItem Value="2" Text="Sogo" />
                            </Items>
                        </dx:ASPxComboBox>
                    </EditItemTemplate>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn FieldName="禮券名稱" runat="server" Caption="禮券名稱" VisibleIndex="5">
                    <EditItemTemplate>
                        <dx:ASPxComboBox runat="server" ID="ddlCuponName" Text='<%#BIND("[禮券名稱]")  %>' Width="80px">
                            <Items>
                                <dx:ListEditItem Value="0" Text="酬賓禮券" Selected="true" />
                                <dx:ListEditItem Value="1" Text="高級禮券" />
                            </Items>
                        </dx:ASPxComboBox>
                    </EditItemTemplate>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="手續費" runat="server" Caption="<%$ Resources:WebResources, ServiceCharges %>"
                    ReadOnly="true" VisibleIndex="6">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("手續費") %>' Width="25px"></asp:TextBox>%
                    </EditItemTemplate>
                    <DataItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("手續費") %>' Width="25px"></asp:Label>%
                    </DataItemTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="是否輸入禮券序號" runat="server" Caption="是否輸入禮券序號"
                    VisibleIndex="7">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </EditItemTemplate>
                    <DataItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </DataItemTemplate>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="開始日期" runat="server" ReadOnly="true" Caption="<%$ Resources:WebResources, StartDate %>"
                    VisibleIndex="7">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="結束日期" runat="server" Caption="<%$ Resources:WebResources, EndDate %>"
                    VisibleIndex="8">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                    VisibleIndex="9">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    VisibleIndex="10">
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
                                <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, add %>"
                                    Visible="true" OnClick="btnAdd_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, delete %>"
                                    Visible="true" />
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>
    </div>
</asp:Content>
