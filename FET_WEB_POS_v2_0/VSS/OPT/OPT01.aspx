<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OPT01.aspx.cs" Inherits="VSS_OPT_OPT01" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--支付方式設定作業 -->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources,PaymentMethodSettings %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--支付方式-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PaymentMethod2 %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                <Items>
                                    <dx:ListEditItem Text="ALL" Selected="true" />
                                    <dx:ListEditItem Text="信用卡" />
                                    <dx:ListEditItem Text="信用卡分期" />
                                    <dx:ListEditItem Text="禮券" />
                                    <dx:ListEditItem Text="現金" />
                                    <dx:ListEditItem Text="金融卡" />
                                    <dx:ListEditItem Text="HappyGo" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--會計科目-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox1" runat="server" Width="80px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--狀態-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                <Items>
                                    <dx:ListEditItem Text="請選擇" Selected="true" />
                                    <dx:ListEditItem Text="有效" />
                                    <dx:ListEditItem Text="尚未生效" />
                                    <dx:ListEditItem Text="已過期" />
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
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
                Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                OnPageIndexChanged="gvMaster_PageIndexChanged" OnRowInserting="gvMaster_RowInserting"
                OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" Settings-ShowTitlePanel="true">
                <Columns>
                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0">
                        <EditButton Visible="true" Text="編輯">
                        </EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                        ReadOnly="true" VisibleIndex="1">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="狀態" runat="server" Caption="<%$ Resources:WebResources, Status %>"
                        ReadOnly="true" VisibleIndex="2">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="支付方式" runat="server" Caption="<%$ Resources:WebResources, PaymentMethod2 %>"
                        ReadOnly="true" VisibleIndex="3">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <DataItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("支付方式") %>' Width="40px"></asp:Label></DataItemTemplate>
                        <FooterTemplate>
                            <dx:ASPxComboBox ID="dropdown1" runat="server" Width="80px" ReadOnly="true">
                                <Items>
                                    <dx:ListEditItem Value="0" Text="信用卡" Selected="true" />
                                    <dx:ListEditItem Value="1" Text="信用卡分期" />
                                    <dx:ListEditItem Value="2" Text="禮券" />
                                    <dx:ListEditItem Value="3" Text="現金" />
                                    <dx:ListEditItem Value="4" Text="金融卡" />
                                    <dx:ListEditItem Value="5" Text="HappyGo" />
                                </Items>
                            </dx:ASPxComboBox>
                        </FooterTemplate>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="科目1" runat="server" Caption="科目1" ReadOnly="true"
                        VisibleIndex="4">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                        <DataItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("科目1") %>'></asp:Label>
                        </DataItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("科目1") %>' Width="40px" ReadOnly="true"></asp:TextBox>
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="科目2" runat="server" Caption="科目2" ReadOnly="true"
                        VisibleIndex="5">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                        <DataItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("科目2") %>'></asp:Label>
                        </DataItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("科目2") %>' Width="40px" ReadOnly="true"></asp:TextBox>
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="科目3" runat="server" Caption="科目3" ReadOnly="true"
                        VisibleIndex="6">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                        <DataItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("科目3") %>'></asp:Label>
                        </DataItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("科目3") %>' Width="40px" ReadOnly="true"></asp:TextBox>
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="科目4" runat="server" Caption="科目4" ReadOnly="true"
                        VisibleIndex="7">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                        <DataItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("科目4") %>'></asp:Label>
                        </DataItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("科目4") %>' Width="40px" ReadOnly="true"></asp:TextBox>
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="科目5" runat="server" Caption="科目5" ReadOnly="true"
                        VisibleIndex="8">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                        <DataItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("科目5") %>'></asp:Label>
                        </DataItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("科目5") %>' Width="40px" ReadOnly="true"></asp:TextBox>
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="科目6" runat="server" Caption="科目6" ReadOnly="true"
                        VisibleIndex="9">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                        <DataItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("科目6") %>'></asp:Label>
                        </DataItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("科目6") %>' Width="40px" ReadOnly="true"></asp:TextBox>
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="起始日期" runat="server" Caption="<%$ Resources:WebResources, startdate %>"
                        VisibleIndex="10">
                        <PropertiesDateEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="結束日期" runat="server" Caption="<%$ Resources:WebResources, EndDate %>"
                        VisibleIndex="11">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                        VisibleIndex="12">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                        VisibleIndex="13">
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
                            </tr>
                        </table>
                    </TitlePanel>
                    <EmptyDataRow>
                        <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                    </EmptyDataRow>
                </Templates>
            </cc:ASPxGridView>
       
</asp:Content>
