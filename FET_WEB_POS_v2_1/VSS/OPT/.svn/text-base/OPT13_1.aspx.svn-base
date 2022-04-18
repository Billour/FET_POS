<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OPT13_1.aspx.cs" Inherits="VSS_OPT_OPT13_1" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <% if (DesignMode)
       { %>

    <script type="text/javascript" src="../../ASPxScriptIntelliSense.js"></script>

    <% } %>
    
    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <input type="hidden" id="hdNo" runat="server" class="hdNo" />
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--HG活動兌點限制－促銷活動-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoRedeemPointsForEvent %>"></asp:Literal>
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
                    <td class="tdtxt" nowrap="nowrap">
                        <!--折扣料號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox7" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--折扣名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox8" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開始日期-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品料號-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  />
                                </td>
                                <%--<td>
                                    <dx:ASPxTextBox ID="txtProductCode1" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        AutoPostBack="false" SkinID="PopupButton" />
                                </td>--%>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="ProductsPopup"  />
                                </td>
                                <%--<td>
                                    <dx:ASPxTextBox ID="txtProductCode2" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnChooseProduct1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        AutoPostBack="false" SkinID="PopupButton" />
                                </td>--%>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox11" runat="server">
                        </dx:ASPxTextBox>
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
        <div>
            <div id="Div1" class="SubEditBlock">
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="折扣料號"
                    Width="100%" Settings-ShowTitlePanel="true" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnPageIndexChanged="gvMaster_PageIndexChanged"
                    AutoGenerateColumns="False" OnRowInserting="gvMaster_RowInserting" 
                    OnRowUpdating="gvMaster_RowUpdating" EnableCallBacks="False" 
                    onfocusedrowchanged="gvMaster_FocusedRowChanged">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <HeaderTemplate>
                                <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button">
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                            ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None"></Border>
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="折扣料號" HeaderStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[折扣料號]") %>'>
                                </dx:ASPxLabel>
                            </EditItemTemplate>
<%--                            <DataItemTemplate>
                                <asp:LinkButton ID="lbtnActivityNo" runat="server" Text='<%# Bind("折扣料號") %>' OnClick="lbtnActivityNo_Click" />
                            </DataItemTemplate>
--%>                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="折扣名稱" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <HeaderCaptionTemplate>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, DiscountName %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="開始日期" HeaderStyle-HorizontalAlign="Center"
                            ReadOnly="true">
                            <PropertiesDateEdit ReadOnlyStyle-Border-BorderStyle="None" DropDownButton-Visible="false">
                                <DropDownButton Visible="False">
                                </DropDownButton>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None"></Border>
                                </ReadOnlyStyle>
                            </PropertiesDateEdit>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StartDate %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>">
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn FieldName="名單檢核" Caption="<%$ Resources:WebResources, NameListVerification %>">
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="折抵方式">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionMethod %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="折抵上限" Caption="<%$ Resources:WebResources, RedemptionLimit %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionLimit %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btnAdd_Click" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                    </td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="匯入名單" OnClick="ASPxButton1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                        <EditForm>
                            <table width="90%">
                                <tr>
                                    <%--折扣料號--%>
                                    <td class="tdtxt" nowrap="nowrap">
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>：
                                    </td>
                                    <td class="tdval" nowrap="nowrap">
                                        <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                            ID="ASPxTextBox11" runat="server" Width="100%" Text='<%# Eval("折扣料號") %>'>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <%--折扣名稱--%>
                                    <td class="tdtxt" nowrap="nowrap">
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>：
                                    </td>
                                    <td class="tdval" nowrap="nowrap">
                                        <asp:Literal ID="Literal11" runat="server" Text='<%# Eval("折扣名稱") %>'></asp:Literal>
                                    </td>
                                    <td class="tdtxt" nowrap="nowrap">
                                    </td>
                                    <td class="tdval" nowrap="nowrap">
                                    </td>
                                </tr>
                                <tr>
                                    <%--開始日期--%>
                                    <td class="tdtxt" nowrap="nowrap">
                                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                                    </td>
                                    <td class="tdval" nowrap="nowrap">
                                        <dx:ASPxDateEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                            ID="ASPxDateEdit1" runat="server" Value='<%# Eval("開始日期") %>'>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td class="tdtxt" nowrap="nowrap">
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>：
                                    </td>
                                    <td class="tdval" nowrap="nowrap">
                                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Value='<%# Eval("結束日期") %>'>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <%--名單檢核--%>
                                    <td class="tdtxt" nowrap="nowrap">
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NameListVerification %>"></asp:Literal>：
                                    </td>
                                    <td class="tdval" nowrap="nowrap">
                                        <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" runat="server"
                                            ReplacementType="EditFormCellEditor" ColumnID="名單檢核">
                                        </dx:ASPxGridViewTemplateReplacement>
                                    </td>
                                </tr>
                                <tr>
                                    <%--折抵方式--%>
                                    <td class="tdtxt" nowrap="nowrap">
                                        <asp:Literal ID="Literal14" runat="server" Text="折抵方式"></asp:Literal>：
                                    </td>
                                    <td class="tdval" nowrap="nowrap">
                                        <dx:ASPxTextBox ID="ASPxTextBox2" ValidationSettings-RequiredField-IsRequired="true"
                                            ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' runat="server"
                                            Width="100%" Text='<%# Eval("折抵方式") %>'>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <%--折抵上限--%>
                                    <td class="tdtxt" nowrap="nowrap">
                                        <asp:Literal ID="Literal6" runat="server" Text="折抵上限"></asp:Literal>：
                                    </td>
                                    <td class="tdval" nowrap="nowrap">
                                        <dx:ASPxTextBox ID="ASPxTextBox1" ValidationSettings-RequiredField-IsRequired="true"
                                            ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' runat="server"
                                            Width="100%" Text='<%# Eval("折抵上限") %>'>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                    runat="server">
                                </dx:ASPxGridViewTemplateReplacement>
                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                    runat="server">
                                </dx:ASPxGridViewTemplateReplacement>
                            </div>
                        </EditForm>
                    </Templates>
                    <SettingsEditing />
                    <SettingsBehavior AllowFocusedRow="True" 
                        ProcessFocusedRowChangedOnServer="True" />
                    <SettingsPager PageSize="5">
                    </SettingsPager>
                    <Settings ShowTitlePanel="True"></Settings>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
            </div>
            <div class="seperate">
            </div>
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%"
                Visible="false">
                <TabPages>
                    <%--促銷設定--%>
                    <dx:TabPage Text="促銷設定">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl4" runat="server">
                                <div class="SubEditBlock">
                                    <cc:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="ASPxGridView1"
                                        Settings-ShowTitlePanel="true" Width="100%" OnRowInserting="gvDetail3_RowInserting"
                                        OnRowUpdating="gvDetail3_RowUpdating" KeyFieldName="商品料號" AutoGenerateColumns="False">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="ASPxGridView1.SelectAllRowsOnPage(this.checked);"
                                                        title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                                                ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <DataItemTemplate>
                                                    <%#Container.ItemIndex + 1%>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="商品料號" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, PromotionName %>">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsEditing />
                                        <Settings ShowTitlePanel="True"></Settings>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAddDetail_3" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                AutoPostBack="False" ClientSideEvents-Click="function(s, e) {ASPxGridView1.AddNewRow();}" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelDetail_3" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            <EditForm>
                                                <table width="80%" align="center">
                                                    <tr>
                                                        <%--促銷料號--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <table align="left">
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="ASPxTextBox3" ValidationSettings-RequiredField-IsRequired="true"
                                                                            ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' runat="server"
                                                                            Width="68px" Text='<%#Eval("[商品料號]") %>'>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                            ClientSideEvents-Click="function(s,e){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}">
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--促銷名稱--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <asp:Literal ID="Literal11" runat="server" Text='<%# Eval("商品名稱") %>'></asp:Literal>
                                                        </td>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
                                                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
                                                </div>
                                            </EditForm>
                                        </Templates>
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <%--兌點設定--%>
                    <dx:TabPage Text="兌點設定">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <div class="SubEditBlock">
                                    <cc:ASPxGridView ID="gvDetail1" runat="server" ClientInstanceName="gvDetail1" Settings-ShowTitlePanel="true"
                                        Width="100%" OnRowInserting="gvDetail1_RowInserting" OnRowUpdating="gvDetail1_RowUpdating"
                                        KeyFieldName="項次" AutoGenerateColumns="False">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvDetail1.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                                                ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <DataItemTemplate>
                                                    <%#Container.ItemIndex + 1%>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="兌點名稱">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionName %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="點數">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Points %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="兌換金額">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionAmount %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsEditing />
                                        <Settings ShowTitlePanel="True"></Settings>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAddDetail_1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                AutoPostBack="False" ClientSideEvents-Click="function(s, e) {gvDetail1.AddNewRow();}" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelDetail_1" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            <EditForm>
                                                <table width="80%" align="center">
                                                    <tr>
                                                        <%--兌點名稱--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, RedemptionName %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                                ID="ASPxTextBox1" runat="server" Width="100%" Text='<%# Eval("兌點名稱") %>'>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <%--點數--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Points %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                                ID="ASPxTextBox4" runat="server" Width="100%" Text='<%# Eval("點數") %>'>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <%--兌換金額--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, RedemptionAmount %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                                ID="ASPxTextBox5" runat="server" Width="100%" Text='<%# Eval("兌換金額") %>'>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
                                                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
                                                </div>
                                            </EditForm>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                        </Templates>
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <%--指定門市--%>
                    <dx:TabPage Text="指定門市">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">
                                <div class="SubEditBlock">
                                    <cc:ASPxGridView ID="gvDetail2" runat="server" ClientInstanceName="gvDetail2" Settings-ShowTitlePanel="true"
                                        Width="100%" OnRowInserting="gvDetail2_RowInserting" OnRowUpdating="gvDetail2_RowUpdating"
                                        KeyFieldName="門市編號" AutoGenerateColumns="False">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvDetail2.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                                                ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <DataItemTemplate>
                                                    <%#Container.ItemIndex + 1%>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="門市編號" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreNo %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <EditItemTemplate>
                                                    <table align="left">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxTextBox ID="txtStoreNo" runat="server" Width="68px" Text='<%#Eval("[門市編號]") %>'>
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="ChooseStoreNoButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                    AutoPostBack="false" ClientSideEvents-Click="function(s,e){openwindow('../OPT/OPT13_choseStore.aspx',500,400);return false;}">
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>"
                                                HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="區域別" Caption="<%$ Resources:WebResources, ByDistrict %>"
                                                HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsEditing />
                                        <Settings ShowTitlePanel="True"></Settings>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAddDetail_2" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                AutoPostBack="False" ClientSideEvents-Click="function(s, e) {gvDetail2.AddNewRow();}" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelDetail_2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DropDownList1" runat="server" Enabled="True">
                                                                <asp:ListItem Text="區域" Value="區域" />
                                                                <asp:ListItem Text="ALL" Value="ALL" />
                                                                <asp:ListItem Text="北一區" Value="北一區" />
                                                                <asp:ListItem Text="中一區" Value="中一區" />
                                                                <asp:ListItem Text="南一區" Value="南一區" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnSubmit" runat="server" Text="<%$ Resources:WebResources, SubmitDistrict %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            <EditForm>
                                                <table width="80%" align="center">
                                                    <tr>
                                                        <%--門市編號--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <table align="left">
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                                            ID="txtStoreCode" runat="server" Width="68px" Text='<%#Eval("[門市編號]") %>'>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="ChooseStoreCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                            ClientSideEvents-Click="function(s,e){openwindow('../SAL/SAL01_chooseStore.aspx',500,400);return false;}">
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--門市名稱--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <asp:Literal ID="Literal11" runat="server" Text='<%# Eval("門市名稱") %>'></asp:Literal>
                                                        </td>
                                                        <%--區域別--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("區域別") %>'></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
                                                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
                                                </div>
                                            </EditForm>
                                        </Templates>
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <%--加購價--%>
                    <dx:TabPage Text="加購價">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">
                                <div class="SubEditBlock">
                                    <cc:ASPxGridView ID="gvDetail3" runat="server" ClientInstanceName="gvDetail3" Settings-ShowTitlePanel="true"
                                        Width="100%" OnRowInserting="gvDetail3_RowInserting" OnRowUpdating="gvDetail3_RowUpdating"
                                        KeyFieldName="商品料號" AutoGenerateColumns="False">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvDetail3.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                                                ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <DataItemTemplate>
                                                    <%#Container.ItemIndex + 1%>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="商品料號">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                                HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="兌換點數" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionPoints %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="加購價" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AdditionalCharges %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsEditing />
                                        <Settings ShowTitlePanel="True"></Settings>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAddDetail_3" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                AutoPostBack="False" ClientSideEvents-Click="function(s, e) {gvDetail3.AddNewRow();}" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelDetail_3" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            <EditForm>
                                                <table width="80%" align="center">
                                                    <tr>
                                                        <%--商品料號--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <table align="left">
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtProductCode" ValidationSettings-RequiredField-IsRequired="true"
                                                                            ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' runat="server"
                                                                            Width="120px" Text='<%#Eval("[商品料號]") %>'>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="ChooseProductCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                            ClientSideEvents-Click="function(s,e){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}">
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <%--商品名稱--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <asp:Literal ID="Literal11" runat="server" Text='<%# Eval("商品名稱") %>'></asp:Literal>
                                                        </td>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--兌換點數--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, RedemptionPoints %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                                ID="ASPxTextBox1" runat="server" Width="100%" Text='<%# Eval("兌換點數") %>'>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <%--加購價--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, AdditionalCharges %>"></asp:Literal>：
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                                ID="ASPxTextBox4" runat="server" Width="100%" Text='<%# Eval("加購價") %>'>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
                                                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                        runat="server">
                                                    </dx:ASPxGridViewTemplateReplacement>
                                                </div>
                                            </EditForm>
                                        </Templates>
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
            <div class="seperate">
            </div>
        </div>
    </div>
<%--    <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
        PopupElementID="btnChooseProduct" TargetElementID="txtProductCode1">
    </cc:ASPxPopupControl>
    <cc:ASPxPopupControl ID="productsPopup1" SkinID="ProductsPopup" runat="server" EnableViewState="False"
        PopupElementID="btnChooseProduct1" TargetElementID="txtProductCode2">
    </cc:ASPxPopupControl>
--%></asp:Content>
