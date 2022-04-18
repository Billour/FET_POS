<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OPT15.aspx.cs" Inherits="VSS_OPT_OPT15" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <input type="hidden" id="hdNo" runat="server" class="hdNo" />

    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--HG點數兌換-來店禮-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoPointsExchangeStoreGift %>"></asp:Literal>
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
                        <!--開始日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StartdATE %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
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
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--活動代號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox3" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox4" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--活動名稱-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="折扣料號"
                            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                            OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnPageIndexChanged="gvMaster_PageIndexChanged"
                            OnRowInserting="gvMaster_RowInserting" OnRowUpdating="gvMaster_RowUpdating" EnableCallBacks="False" 
                            onfocusedrowchanged="gvMaster_FocusedRowChanged">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn ButtonType="Button">
                                    <EditButton Visible="true"></EditButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                                    <DataItemTemplate>
                                        <%#Container.ItemIndex + 1%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="折扣料號" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>">
<%--                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lbtnActivityNo" runat="server" Text='<%# Bind("折扣料號") %>' OnClick="lbtnActivityNo_Click" />
                                    </DataItemTemplate>
--%>                                
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="折扣名稱" Caption="<%$ Resources:WebResources, DiscountName %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="類別" Caption="<%$ Resources:WebResources, Category %>">
                                    <PropertiesComboBox ValueType="System.String">
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="點數" Caption="<%$ Resources:WebResources, Points %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn FieldName="名單檢核" Caption="<%$ Resources:WebResources, NameListVerification %>">
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn FieldName="兌換次數" Caption="兌換次數">
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
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, ImportList %>"
                                                    OnClick="ASPxButton1_Click" />
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
                                            <td class="tdtxt" nowrap="nowrap">
                                            </td>
                                            <td class="tdval" nowrap="nowrap">
                                            </td>
                                        </tr>
                                        <tr>
                                            <%--類別--%>
                                            <td class="tdtxt" nowrap="nowrap">
                                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                                            </td>
                                            <td class="tdval" nowrap="nowrap">
                                                <dx:ASPxComboBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                    ID="ASPxComboBox1" runat="server" ValueType="System.String" Value='<%# Eval("類別") %>'>
                                                    <Items>
                                                        <dx:ListEditItem Text="商品" Value="商品" />
                                                        <dx:ListEditItem Text="點數" Value="點數" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <%--商品料號--%>
                                            <td class="tdtxt" nowrap="nowrap">
                                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                                            </td>
                                            <td class="tdval" nowrap="nowrap">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtProductCode" runat="server" Width="120px" Text='<%#Eval("[商品料號]") %>'>
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
                                            <%--點數--%>
                                            <td class="tdtxt" nowrap="nowrap">
                                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Points %>"></asp:Literal>：
                                            </td>
                                            <td class="tdval" nowrap="nowrap">
                                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100%" Text='<%# Eval("點數") %>'>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <%--名單檢核--%>
                                            <td class="tdtxt" nowrap="nowrap">
                                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NameListVerification %>"></asp:Literal>：
                                            </td>
                                            <td class="tdval" nowrap="nowrap">
                                                <dx:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormCellEditor"
                                                    ColumnID="名單檢核">
                                                </dx:ASPxGridViewTemplateReplacement>
                                            </td>
                                            <%--兌換次數--%>
                                            <td class="tdtxt" nowrap="nowrap">
                                                <asp:Literal ID="Literal14" runat="server" Text="兌換次數"></asp:Literal>：
                                            </td>
                                            <td class="tdval" nowrap="nowrap">
                                                <dx:ASPxTextBox ID="ASPxTextBox2" ValidationSettings-RequiredField-IsRequired="true"
                                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' runat="server"
                                                    Width="100%" Text='<%# Eval("兌換次數") %>'>
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
                            <SettingsBehavior AllowFocusedRow="true"  ProcessFocusedRowChangedOnServer="True" />
                            <SettingsEditing />
                            <SettingsPager PageSize="5"></SettingsPager>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            <Settings ShowTitlePanel="True" />
                        </cc:ASPxGridView>
                    </div>
                    <div class="seperate">
                    </div>
                    <div id="Div_Dt">
                        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%"
                            Visible="false">
                            <TabPages>
                                <dx:TabPage Text="<%$ Resources:WebResources, SpecifyStore %>">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                                                    Width="100%" OnPageIndexChanged="detailGrid_PageIndexChanged" KeyFieldName="項次"
                                                    AutoGenerateColumns="False" OnRowInserting="gvDetail_RowInserting" OnRowUpdating="gvDetail_RowUpdating">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <input type="checkbox" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
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
                                                        <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>"
                                                            HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="區域別" Caption="<%$ Resources:WebResources, ByDistrict %>"
                                                            HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsEditing />
                                                    <SettingsPager PageSize="5">
                                                    </SettingsPager>
                                                    <Templates>
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
                                                        <TitlePanel>
                                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnAdd_detail" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                            OnClick="btnAdd_Click_dt" />
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                                                            <asp:ListItem Text="區域" Value="區域" />
                                                                            <asp:ListItem Text="ALL" Value="ALL" />
                                                                            <asp:ListItem Text="北一區" Value="北一區" />
                                                                            <asp:ListItem Text="中一區" Value="中一區" />
                                                                            <asp:ListItem Text="南一區" Value="南一區" />
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="Button5" runat="server" Text="<%$ Resources:WebResources, Confirm %>">
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </TitlePanel>
                                                        <EmptyDataRow>
                                                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                                        </EmptyDataRow>
                                                    </Templates>
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="seperate">
        </div>
    </div>
</asp:Content>
