<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT03.aspx.cs" Inherits="VSS_OPT_OPT03"
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
                    信用卡分期設定作業
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    發卡銀行：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ddlCardBank" runat="server" Width="80px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    成本中心：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddlCostCenter" runat="server" Width="80px">
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    狀態：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList2" runat="server" Width="80px">
                        <Items>
                            <dx:ListEditItem Value="請選擇" Text="請選擇" Selected="true" />
                            <dx:ListEditItem Value="有效" Text="有效" />
                            <dx:ListEditItem Value="尚未生效" Text="尚未生效" />
                            <dx:ListEditItem Value="已過期" Text="已過期" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    分期期數：
                </td>
                <td class="tdval" colspan="5" nowrap="nowrap">
                    <dx:ASPxTextBox ID="TextBox1" runat="server" Width="80px">
                    </dx:ASPxTextBox>
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
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
        Settings-ShowTitlePanel="true" Width="100%" 
        OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged"
        OnRowUpdating="grid_RowUpdating" OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared">
        <Columns>
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
            <dx:GridViewDataTextColumn FieldName="狀態" runat="server" Caption="<%$ Resources:WebResources, status %>"
                ReadOnly="true" VisibleIndex="3">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn FieldName="發卡銀行" runat="server" Caption="發卡銀行" VisibleIndex="4">
                <DataItemTemplate>
                    <asp:LinkButton ID="lbtn1" runat="server" Text='<%# Bind("發卡銀行") %>' OnClick="Card_Click"
                        Width="80px"></asp:LinkButton>
                </DataItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCard" runat="server" Text='<%# Bind("發卡銀行") %>' Width="80px"></asp:TextBox>
                </EditItemTemplate>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn FieldName="分期期數" runat="server" Caption="分期期數" VisibleIndex="5">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("分期期數") %>' Width="40px"></asp:TextBox>
                </EditItemTemplate>
                <DataItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("分期期數") %>' Width="40px"></asp:Label>
                </DataItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txt1" runat="server" Width="40px"></asp:TextBox>
                </FooterTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="分期利率" runat="server" Caption="分期利率" VisibleIndex="6"
                EditCellStyle-HorizontalAlign="Right">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("分期利率") %>' Width="40px"></asp:TextBox>%
                </EditItemTemplate>
                <DataItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("分期利率") %>'></asp:Label>%
                </DataItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("分期利率") %>' Width="40px" ReadOnly="true"></asp:TextBox>%
                </FooterTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="開始日期" runat="server" Caption="<%$ Resources:WebResources, StartDate %>" VisibleIndex="7">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="結束日期" runat="server" Caption="<%$ Resources:WebResources, Enddate %>" VisibleIndex="8">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                VisibleIndex="9" ReadOnly="true">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                VisibleIndex="10" ReadOnly="true">
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
                            <dx:ASPxButton ID="btnAdd1" runat="server" Text="<%$ Resources:WebResources, add %>"
                                OnClick="btnAdd1_Click" />
                        </td>
                    </tr>
                </table>
            </TitlePanel>
        </Templates>
        <SettingsEditing Mode="Inline" />
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        <SettingsPager PageSize="5">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
    <div id="Div2" style="text-align: left;" visible="false">
        <div visible="false">
        </div>
        <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" Width="40%"
            Settings-ShowTitlePanel="true" KeyFieldName="項次" Visible="false" 
            OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged"
            OnRowUpdating="grid_RowUpdating">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="10%">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                    <EditButton Visible="true">
                    </EditButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                    ReadOnly="true" VisibleIndex="3" Width="20px">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="成本中心" runat="server" Caption="成本中心" VisibleIndex="4"
                    Width="80px">
                    <EditItemTemplate>
                        <dx:ASPxComboBox ID="ddlCostCenter" runat="server" Width="80px" Text='<%# Bind("成本中心") %>'>
                            <Items>
                                <dx:ListEditItem Value="0" Text="行銷部" Selected="true" />
                                <dx:ListEditItem Value="1" Text="通路管理部" />
                                <dx:ListEditItem Value="2" Text="HRS" />
                            </Items>
                        </dx:ASPxComboBox>
                    </EditItemTemplate>
                    <DataItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("成本中心") %>' Width="80px"></asp:Label></DataItemTemplate>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="成本中心拆帳比率" runat="server" Caption="成本中心拆帳比率"
                    VisibleIndex="5" Width="60px" HeaderStyle-HorizontalAlign="Left">
                    <EditItemTemplate>
                        <asp:TextBox ID="Label3" runat="server" Text='<%# Bind("成本中心拆帳比率") %>' Width="20px"></asp:TextBox>%</EditItemTemplate>
                    <DataItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("成本中心拆帳比率") %>' Width="20px"></asp:Label>%</DataItemTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <TitlePanel>
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnAdd2" runat="server" Text="<%$ Resources:WebResources, add %>"
                                    OnClick=" btnAdd2_Click">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnDelete2" runat="server" Text="<%$ Resources:WebResources, delete %>">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <SettingsEditing Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>
    </div>
</asp:Content>
