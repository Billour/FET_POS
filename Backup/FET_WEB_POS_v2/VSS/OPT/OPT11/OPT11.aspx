<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="OPT11.aspx.cs" Inherits="VSS_OPT_OPT11" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function getProductInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getProductInfo(s.GetText(), onSuccess);
        }

        function onSuccess(returnData, userContext, methodName) {
           if (methodName == "getProductInfo") {
                if (returnData != '') {
                }
                else {
                    alert("折扣料號不存在或不屬於HappyGo折扣!");
                    _gvSender.SetValue(null);
                    _gvSender.Focus();
                }
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--HappyGo點數兌換設定-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoPointConversionSet %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table width="100%">
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--類別-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="" Selected="true" />
                                <dx:ListEditItem Text="銷售" Value="1" />
                                <dx:ListEditItem Text="代收" Value="2" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開始日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSDate_S" runat="server" ClientInstanceName="txtSDate" EditFormat="Custom"
                                        EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSDate_E" runat="server" ClientInstanceName="txtEDate" EditFormat="Custom"
                                        EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--兌點名稱-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, RedemptionName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox4" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--兌點點數-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, PointsAgainstThePoint %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtMemo1" runat="server" Width="100">
                                        <ValidationSettings>
                                            <RegularExpression ValidationExpression="^\d{1,38}" ErrorText="輸入字串非數字格式，請重新輸入" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtMemo2" runat="server" Width="100">
                                        <ValidationSettings>
                                            <RegularExpression ValidationExpression="^\d{1,38}" ErrorText="輸入字串非數字格式，請重新輸入" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--兌換金額-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, RedemptionAmount %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtMemo3" runat="server" Width="100">
                                        <ValidationSettings>
                                            <RegularExpression ValidationExpression="^\d{1,9}" ErrorText="輸入字串非數字格式，請重新輸入" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtMemo4" runat="server" Width="100">
                                        <ValidationSettings>
                                            <RegularExpression ValidationExpression="^\d{1,9}" ErrorText="輸入字串非數字格式，請重新輸入" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                    </td>
                    <td class="tdval" nowrap="nowrap" style="color: Red">
                        <asp:Literal ID="MESSGER" runat="server" Text=""></asp:Literal>
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
                        <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="CONVERT_ID"
                            Width="100%" EnableCallBacks="false" 
                            OnPageIndexChanged="gvMaster_PageIndexChanged" 
                            OnStartRowEditing ="gvMaster_StartRowEditing"
                            OnRowInserting="gvMaster_RowInserting" 
                            OnRowValidating="gvMaster_RowValidating"
                            OnRowUpdating="gvMaster_RowUpdating" 
                            OnInitNewRow="gvMaster_InitNewRow" 
                            OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                            OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" >
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                        </div>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                    <EditButton Visible="true">
                                    </EditButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="2" ReadOnly="true">
                                    <DataItemTemplate>
                                        <%#Container.ItemIndex + 1%>
                                    </DataItemTemplate>
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <%-- 類別- --%>
                                <dx:GridViewDataComboBoxColumn FieldName="HG_EXCHANGE_TYPE" HeaderStyle-HorizontalAlign="Center"
                                    VisibleIndex="3" Caption="<%$ Resources:WebResources, Category %>" PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true"
                                    PropertiesComboBox-ValidationSettings-ErrorText="必填欄位" >
                                    <HeaderCaptionTemplate>
                                        <span style="color: Red">*</span>
                                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Category %>">
                                        </dx:ASPxLabel>
                                    </HeaderCaptionTemplate>
                                    <PropertiesComboBox ValueType="System.String">
                                        <Items>
                                            <dx:ListEditItem Text="銷售" Value="1" />
                                            <dx:ListEditItem Text="代收" Value="2" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# BIND("[HG_EXCHANGE_TYPE_NAME]") %>'>
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="CONVERT_NO" HeaderStyle-HorizontalAlign="Center"
                                    Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"
                                    PropertiesTextEdit-ValidationSettings-ErrorText="必填欄位">
                                    <HeaderCaptionTemplate>
                                        <span style="color: Red">*</span>
                                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>">
                                        </dx:ASPxLabel>
                                    </HeaderCaptionTemplate>
                                    <EditItemTemplate>
                                        <uc1:PopupControl ID="CONVERT_NO" runat="server" PopupControlName="DiscountPopup"
                                            AutoPostBack="false" IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                            Text='<%# Bind("CONVERT_NO") %>' Width="150" OnClientTextChanged="function(s,e){ getProductInfo(s,e);}" />
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CONVERT_NAME" HeaderStyle-HorizontalAlign="Right"
                                    VisibleIndex="5" Caption="<%$ Resources:WebResources, AgainstThePointName %>">
                                    <HeaderCaptionTemplate>
                                        <span style="color: Red">*</span>
                                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AgainstThePointName %>">
                                        </dx:ASPxLabel>
                                    </HeaderCaptionTemplate>
                                    <PropertiesTextEdit MaxLength="25" Style-HorizontalAlign="Left">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="S_DATE" HeaderStyle-HorizontalAlign="Center"
                                    Caption="<%$ Resources:WebResources, startdate %>">
                                    <HeaderCaptionTemplate>
                                        <span style="color: Red">*</span>
                                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, startdate %>">
                                        </dx:ASPxLabel>
                                    </HeaderCaptionTemplate>
                                    <EditItemTemplate>
                                        <dx:ASPxDateEdit MinDate='<%# DateTime.Today.AddDays(1) %>' ID="txtSDate" Value='<%# Bind("S_DATE") %>'
                                            runat="server" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                            EditFormat="Custom" EditFormatString="yyyy/MM/dd">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </EditItemTemplate>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>"
                                    HeaderStyle-HorizontalAlign="Center" EditCellStyle-HorizontalAlign="Right">
                                    <EditItemTemplate>
                                        <dx:ASPxDateEdit MinDate='<%# DateTime.Today.AddDays(1) %>' ID="txtEDate" Value='<%# Bind("E_DATE") %>'
                                            runat="server" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                            EditFormat="Custom" EditFormatString="yyyy/MM/dd">
                                        </dx:ASPxDateEdit>
                                    </EditItemTemplate>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="DIVIDABLE_POINT" HeaderStyle-HorizontalAlign="Right"
                                    Width="100" Caption="<%$ Resources:WebResources, Points %>">
                                    <HeaderCaptionTemplate>
                                        <span style="color: Red">*</span>
                                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Points %>">
                                        </dx:ASPxLabel>
                                    </HeaderCaptionTemplate>
                                    <PropertiesTextEdit MaxLength="10" Style-HorizontalAlign="Left">
                                        <ValidationSettings>
                                            <RegularExpression ValidationExpression="^\d{1,38}" ErrorText="輸入字串非數字格式，請重新輸入" />
                                            <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CONVERT_CURRENCY" HeaderStyle-HorizontalAlign="Right"
                                    Width="100" Caption="<%$ Resources:WebResources, RedemptionAmount %>">
                                    <HeaderCaptionTemplate>
                                        <span style="color: Red">*</span>
                                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionAmount %>">
                                        </dx:ASPxLabel>
                                    </HeaderCaptionTemplate>
                                    <PropertiesTextEdit MaxLength="10" Style-HorizontalAlign="Left">
                                        <ValidationSettings>
                                            <RegularExpression ValidationExpression="\d{1,7}" ErrorText="輸入字串非數字格式，請重新輸入" />
                                            <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MODI_USER_NAME" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                    ReadOnly="true">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="UDTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                    HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("[UDTM]") %>'>
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Eval("[UDTM]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                    OnClick="btnAddNew_Click" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnDelete" SkinID="DeleteBtn"  runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                    OnClick="btnDelete_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                            </Templates>
                            <SettingsPager PageSize="10"></SettingsPager>
                            <Settings ShowTitlePanel="true" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            <SettingsEditing Mode="Inline" />
                        </cc:ASPxGridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
