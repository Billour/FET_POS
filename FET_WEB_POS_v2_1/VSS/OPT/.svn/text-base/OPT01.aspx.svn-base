<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OPT01.aspx.cs" Inherits="VSS_OPT_OPT01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" SelectedIndex="0" AllowMouseWheel="False">
                            <Items>
                                <dx:ListEditItem Text="ALL" />
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
                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" SelectedIndex="0">
                            <Items>
                                <dx:ListEditItem Text="請選擇" />
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
            Width="100%" Settings-ShowTitlePanel="true" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
            OnRowInserting="gvMaster_RowInserting" OnRowUpdating="gvMaster_RowUpdating" OnStartRowEditing="gvMaster_StartRowEditing"
            OnInitNewRow="gvMaster_InitNewRow" OnPageIndexChanged="gvMaster_PageIndexChanged">
            <Columns>
                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0" Caption=" ">
                    <EditButton Visible="true">
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
                <dx:GridViewDataComboBoxColumn FieldName="支付方式" runat="server" VisibleIndex="3">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PaymentMethod2 %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <PropertiesComboBox>
                        <Items>
                            <dx:ListEditItem Value="0" Text="ALL" Selected="true" />
                            <dx:ListEditItem Value="1" Text="信用卡" />
                            <dx:ListEditItem Value="2" Text="信用卡分期" />
                            <dx:ListEditItem Value="3" Text="禮券" />
                            <dx:ListEditItem Value="4" Text="現金" />
                            <dx:ListEditItem Value="5" Text="金融卡" />
                            <dx:ListEditItem Value="6" Text="HappyGo" />
                        </Items>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="科目1" runat="server"  VisibleIndex="4" >
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject1 %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="科目2" runat="server" 
                    VisibleIndex="5">
                     <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject2 %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="科目3" runat="server" 
                    VisibleIndex="6">
                     <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject3 %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="科目4" runat="server" 
                    VisibleIndex="7">
                     <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject4 %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="科目5" runat="server" Caption="<%$ Resources:WebResources, Subject5 %>"
                    VisibleIndex="8">
                     <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject5 %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="科目6" runat="server" Caption="<%$ Resources:WebResources, Subject6 %>"
                    VisibleIndex="9">
                     <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject6 %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="起始日期" runat="server" Caption="<%$ Resources:WebResources, startdate %>"
                    VisibleIndex="10">
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
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
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
            </Templates>
        </cc:ASPxGridView>
</asp:Content>
