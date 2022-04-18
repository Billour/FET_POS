<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OPT12.aspx.cs" Inherits="VSS_OPT_OPT12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=230,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--HappyGo點數累點設定-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, HappyGoSetPointsAccumulated %>"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--累點名稱-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, NameOfAccumulatedPoints %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <dx:ASPxTextBox ID="TextBox1" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--開始日期-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Startdate %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--累點點數-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, PointsAccumulatedPoints %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="TextBox2" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="TextBox3" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--累點金額-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, AmountAccumulatedPoints %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="TextBox4" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="TextBox5" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
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
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
            <TabPages>
                <dx:TabPage Text="<%$ Resources:WebResources, TiredPointSetting %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
                                    Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                                    OnPageIndexChanged="gvMaster_PageIndexChanged" AutoGenerateColumns="False" OnRowInserting="gvMaster_RowInserting"
                                    OnRowUpdating="gvMaster_RowUpdating" Settings-ShowTitlePanel="true">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
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
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                            <DataItemTemplate>
                                                <%#Container.ItemIndex + 1%>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <%-- 折扣料號- --%>
                                        <dx:GridViewDataTextColumn FieldName="累點代號" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="累點名稱" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, NameOfAccumulatedPoints %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="開始日期" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, STARTDATE %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataDateColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, ENDDATE %>"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <dx:GridViewDataTextColumn FieldName="累點金額" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AmountAccumulatedPoints %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="累點點數" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PointsAccumulatedPoints %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                            HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                            HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
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
                                                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                        <EditForm>
                                            <table width="90%">
                                                <tr>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--折扣料號-->
                                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                            ID="ASPxTextBox11" runat="server" Width="100%" Text='<%# Eval("累點代號") %>'>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--開始日期-->
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
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--累點名稱-->
                                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, NameOfAccumulatedPoints %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                            ID="TextBox4" runat="server" Width="100%" Text='<%# Eval("累點名稱") %>'>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--累點金額-->
                                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, AmountAccumulatedPoints %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                            ID="txtMemo" runat="server" Width="100%" Text='<%# Eval("累點金額") %>'>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--累點點數-->
                                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, PointsAccumulatedPoints %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <dx:ASPxTextBox ID="ASPxTextBox1" ValidationSettings-RequiredField-IsRequired="true"
                                                            ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' runat="server"
                                                            Width="100%" Text='<%# Eval("累點點數") %>'>
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
                                    <SettingsPager PageSize="10">
                                    </SettingsPager>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="排外條件">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvCondition" ClientInstanceName="gvCondition" runat="server"
                                    KeyFieldName="商品料號" Width="100%" OnHtmlRowPrepared="gvCondition_HtmlRowPrepared"
                                    OnHtmlRowCreated="gvCondition_HtmlRowCreated" OnPageIndexChanged="gvCondition_PageIndexChanged"
                                    AutoGenerateColumns="False" OnRowInserting="gvCondition_RowInserting" OnRowUpdating="gvCondition_RowUpdating"
                                    Settings-ShowTitlePanel="true">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input type="checkbox" onclick="gvCondition.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button">
                                            <EditButton Visible="true">
                                            </EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                            HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品料號" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtProductCode" runat="server" Width="68px" Text='<%#Eval("[商品料號]") %>'>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ChooseProductCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                ClientSideEvents-Click="function(s,e){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EditItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                            HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <EditForm>
                                            <table width="60%" align="center">
                                                <tr>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--商品料號-->
                                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                                        ID="TextBox4" runat="server" Width="100%" Text='<%# Eval("商品料號") %>'>
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
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--商品名稱-->
                                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <asp:Literal ID="Literal11" runat="server" Text='<%# Eval("商品名稱") %>'></asp:Literal>
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
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnAddNew_2" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                            OnClick="btnAddNew_Click2" />
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnDelete_2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <SettingsEditing />
                                    <SettingsPager PageSize="10">
                                    </SettingsPager>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
    <div class="seperate">
    </div>
</asp:Content>
