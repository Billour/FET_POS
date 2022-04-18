<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LEA01.aspx.cs" Inherits="VSS_LEA_LEA01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--設備租賃設定-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, EquipmentLeasing %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--類別-->
                    <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <dx:ASPxRadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow" Border-BorderStyle="None">
                        <Items>
                            <dx:ListEditItem Value="1" Text="漫遊租賃" />
                            <dx:ListEditItem Value="2" Text="維修租賃" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--產品類別-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList2" runat="server" Width="100">
                        <Items>
                            <dx:ListEditItem Value="-請選擇-" Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Value="魔拖羅拉" Text="魔拖羅拉" />
                            <dx:ListEditItem Value="紅打電" Text="紅打電" />
                            <dx:ListEditItem Value="HOKIA" Text="HOKIA" />
                            <dx:ListEditItem Value="其他" Text="其他" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--產品名稱-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList1" runat="server" Width="100">
                        <Items>
                            <dx:ListEditItem Value="-請選擇-" Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Value="產品1" Text="產品1" />
                            <dx:ListEditItem Value="產品2" Text="產品2" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--狀態-->
                    <asp:Literal ID="Literal3" runat="server" Text="設定狀態"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label1" runat="server" Text="00-未存檔"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--外部廠商代碼-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, OutsideFirmNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList3" runat="server" Width="100">
                        <Items>
                            <dx:ListEditItem Value="-請選擇-" Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Value="廠商1" Text="廠商1" />
                            <dx:ListEditItem Value="廠商2" Text="廠商2" />
                            <dx:ListEditItem Value="廠商3" Text="廠商3" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--外部廠商名稱-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OutsideFirmName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label6" runat="server" Text="名稱1"></asp:Label>
                </td>
                <td class="tdtxt">
                    <!--更新日期-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label3" runat="server" Text="2010/07/01 22:00"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--租金料號-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PartNumberOfRent %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--日租金額-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, DailyRent %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--維護人員-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--保證金料號-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, PartNumberOfRentDeposit %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox5" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--保證金-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, RentDeposit %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox6" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--賠償金料號-->
                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, PartNumberOfCompensation %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox7" runat="server">
                    </dx:ASPxTextBox>
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
                    <!--有效期間-->
                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, EffectiveDuration %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="1">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="postbackDate_TextBox1" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="postbackDate_TextBox2" runat="server">
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
                <td class="tdtxt">
                    <!--備註-->
                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <dx:ASPxMemo ID="TextBox8" runat="server" Width="98%">
                    </dx:ASPxMemo>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div>
        <dx:ASPxPageControl ID="TabContainer1" ClientInstanceName="tabPage" runat="server"
            ActiveTabIndex="0" EnableHierarchyRecreation="True" Width="100%">
            <TabPages>
                <dx:TabPage Text="<%$ Resources:WebResources, CompensationItems %>">
                    <ContentCollection>
                        <dx:ContentControl ID="TabPanel1" runat="server">
                            <div id="Div1" runat="server" class="SubEditBlock">
                                <div class="GridScrollBar" style="height: auto">
                                    <cc:ASPxGridView Width="100%" ID="gvMaster" ClientInstanceName="gvMaster" runat="server"
                                        AutoGenerateColumns="False" KeyFieldName="ID" OnRowInserting="gvMaster_RowInserting"
                                        OnRowUpdating="gvMaster_RowUpdating1" >
                                        <SettingsEditing Mode="Inline" />
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                                <EditButton Visible="True">
                                                </EditButton>
                                                <HeaderTemplate>
                                                    &nbsp;
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="Name" Caption="<%$ Resources:WebResources, CompensationItems %>"
                                                VisibleIndex="2">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Amount" Caption="<%$ Resources:WebResources, Amount %>"
                                                VisibleIndex="3">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table cellpadding="0" cellspacing="0" border="0" align="left">
                                                    <tr>
                                                        <td align="right">
                                                            <dx:ASPxButton ID="btnAdd1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) { gvMaster.AddNewRow(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <dx:ASPxButton AutoPostBack="false" ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <Settings ShowTitlePanel="true" />
                                    </cc:ASPxGridView>
                                </div>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="<%$ Resources:WebResources, DiscountItems %>">
                    <ContentCollection>
                        <dx:ContentControl ID="TabPanel2" runat="server">
                            <div class="SubEditBlock">
                                <div class="GridScrollBar" style="height: auto">
                                    <cc:ASPxGridView ID="gvDiscountItem" ClientInstanceName="gvDiscountItem" runat="server"
                                        AutoGenerateColumns="False" KeyFieldName="折扣料號" OnCustomCallback="gvDiscountItem_CustomCallback"
                                        OnRowInserting="gvDiscountItem_RowInserting" OnRowUpdated="gvDiscountItem_RowUpdated"
                                        OnRowUpdating="gvDiscountItem_RowUpdating1" Width="100%">
                                        <SettingsEditing Mode="Inline" />
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvDiscountItem.SelectAllRowsOnPage(this.checked);"
                                                        title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                                <EditButton Visible="True">
                                                </EditButton>
                                                <HeaderTemplate>
                                                    &nbsp;
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="折扣料號" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                                                VisibleIndex="2">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="折扣名稱" Caption="<%$ Resources:WebResources, DiscountName %>"
                                                VisibleIndex="3">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="折扣金額" Caption="<%$ Resources:WebResources, DiscountAmount %>"
                                                VisibleIndex="4">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="折扣比率" Caption="<%$ Resources:WebResources, DiscountRate %>"
                                                VisibleIndex="5">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="成本中心" Caption="<%$ Resources:WebResources, CostCenter %>"
                                                VisibleIndex="6">
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="會計科目" Caption="<%$ Resources:WebResources, AccountingSubject %>"
                                                VisibleIndex="7">
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table cellpadding="0" cellspacing="0" border="0" align="left">
                                                    <tr>
                                                        <td align="right">
                                                            <dx:ASPxButton ID="btnAdd2" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                AutoPostBack="false">
                                                                <ClientSideEvents Click="function(s, e) { gvDiscountItem.AddNewRow(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <dx:ASPxButton ID="Button4" AutoPostBack="false" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <Settings ShowTitlePanel="true" />
                                    </cc:ASPxGridView>
                                </div>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="租賃手機庫存">
                    <ContentCollection>
                        <dx:ContentControl ID="TabPanel3" runat="server">
                            <div class="SubEditBlock">
                                <div class="GridScrollBar" style="height: auto">
                                    <cc:ASPxGridView ID="gvMobileStock" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="門市代號" Caption="門市代號" VisibleIndex="0" />
                                            <dx:GridViewDataColumn FieldName="門市名稱" Caption="門市名稱" VisibleIndex="1" />
                                            <dx:GridViewDataColumn FieldName="手機序號" Caption="手機序號" VisibleIndex="2" />
                                        </Columns>
                                        <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                                            </EmptyDataRow>
                                        </Templates>
                                    </cc:ASPxGridView>
                                </div>
                                <div class="seperate">
                                </div>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
    <div class="seperate">
    </div>
    <div class="GridScrollBar" style="height: auto">
        <asp:Panel ID="Panel1" runat="server" Visible="false">
        </asp:Panel>
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="right">
                    <dx:ASPxButton ID="btnSave" runat="server" Text="存檔" OnClick="btnSave_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" />
                </td>
            </tr>
        </table>
    </div>
    <div class="GridScrollBar" style="height: auto">
        <asp:Panel ID="Panel2" runat="server" Visible="false">
            <asp:FormView ID="FormView1" runat="server" DefaultMode="ReadOnly" Width="60%">
                <ItemTemplate>
                    <table class="mGrid" width="60%">
                        <tr>
                            <td align="center">
                                <!--修改記錄-->
                                <asp:Literal ID="Literal40" runat="server" Text="修改記錄"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <!--更新日期-->
                                <asp:Literal ID="Literal33" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                            </td>
                            <td align="center">
                                <!--工號-->
                                <asp:Literal ID="Literal30" runat="server" Text="工號"></asp:Literal>
                            </td>
                            <td align="center">
                                <!--姓名-->
                                <asp:Literal ID="Literal31" runat="server" Text="姓名"></asp:Literal>
                            </td>
                            <td align="center">
                                <!--說明-->
                                <asp:Literal ID="Literal34" runat="server" Text="說明"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Literal ID="Literal32" runat="server" Text="2010/09/30"></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="Literal7" runat="server" Text="60736"></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="Literal19" runat="server" Text="王小明"></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="Literal35" runat="server" Text="保證金料號修改"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </asp:Panel>
    </div>
</asp:Content>
