<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT02.aspx.cs" Inherits="VSS_OPT_OPT02"
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
                    <!--信用卡手續費設定作業 -->
                    <asp:Literal ID="Literal1" runat="server" Text="信用卡手續費設定作業 "></asp:Literal>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        Visible="false" />
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        信用卡別：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlCardType" runat="server" />
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        狀態：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                <dx:ListEditItem Value="1" Text="有效" />
                                <dx:ListEditItem Value="2" Text="尚未生效" />
                                <dx:ListEditItem Value="3" Text="已過期" />
                            </Items>
                        </dx:ASPxComboBox>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <div id="Div2" class="SubEditBlock" style="text-align: left;">
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
                                <dx:GridViewDataTextColumn FieldName="信用卡別" runat="server" Caption="<%$ Resources:WebResources, TypeOfCreditCard %>"
                                    ReadOnly="true" VisibleIndex="4">
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                    <EditItemTemplate>
                                        <dx:ASPxComboBox ID="dropdown1" runat="server" Width="80px" Text='<%#BIND("[信用卡別]")  %>'>
                                            <Items>
                                                <dx:ListEditItem Value="0" Text="MASTER" Selected="true" />
                                                <dx:ListEditItem Value="1" Text="VISA" />
                                                <dx:ListEditItem Value="2" Text="AE" />
                                                <dx:ListEditItem Value="3" Text="JCB" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </EditItemTemplate>
                                    <DataItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%#BIND("[信用卡別]")  %>' Width="40px"></asp:Label></DataItemTemplate>
                                    <FooterTemplate>
                                        <dx:ASPxComboBox ID="dropdown1" runat="server" Width="80px" ReadOnly="true">
                                            <Items>
                                                <dx:ListEditItem Value="0" Text="MASTER" Selected="true" />
                                                <dx:ListEditItem Value="1" Text="VISA" />
                                                <dx:ListEditItem Value="2" Text="AE" />
                                                <dx:ListEditItem Value="3" Text="JCB" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </FooterTemplate>
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="手續費" runat="server" Caption="<%$ Resources:WebResources, ServiceCharges %>"
                                    ReadOnly="true" VisibleIndex="5">
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("手續費") %>' Width="40px"></asp:TextBox>%
                                    </EditItemTemplate>
                                    <DataItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("手續費") %>'></asp:Label>%
                                    </DataItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("手續費") %>' Width="40px" ReadOnly="true"></asp:TextBox>%
                                    </FooterTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="開始日期" runat="server" Caption="<%$ Resources:WebResources, startdate %>"
                                    VisibleIndex="6">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="結束日期" runat="server" Caption="<%$ Resources:WebResources, EndDate %>"
                                    VisibleIndex="7">
                                </dx:GridViewDataDateColumn>
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
                                    VisibleIndex="8">
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
                            </Columns>
                            <SettingsPager PageSize="5" />
                            <SettingsEditing Mode="Inline" />
                            <Templates>
                                <TitlePanel>
                                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, add %>"
                                                    Visible="true" OnClick=" btnAdd_Click">
                                                </dx:ASPxButton>
                                            </td>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                                </EmptyDataRow>
                            </Templates>
                        </cc:ASPxGridView>
                    </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
