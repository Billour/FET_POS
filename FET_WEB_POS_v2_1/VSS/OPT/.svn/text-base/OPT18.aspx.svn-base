<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="OPT18.aspx.cs" Inherits="VSS_OPT_OPT18" EnableEventValidation="false" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--門市特殊客訢處理折扣設定 -->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, DiscountStoreManagerSettings %>"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" />
                    </td>
                    <td class="tdtxt">
                        <!--門市名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="storeNameTextBox" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--折扣月份-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, DiscountsMonth %>"></asp:Literal>：
                    </td>
                    <td colspan="5">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 240px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="searchButton" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="SearchButton_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="項次"
            Width="100%" Settings-ShowTitlePanel="true" OnPageIndexChanged="grid_PageIndexChanged"
            OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="grid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button">
                    <EditButton Visible="True">
                    </EditButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                    ReadOnly="true">
                    <EditFormSettings Visible="false" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>"
                    ReadOnly="true">
                    <EditFormSettings Visible="false" />
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreNo %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>"
                    ReadOnly="true">
                    <EditFormSettings Visible="false" />
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreName %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="折扣月份" Caption="<%$ Resources:WebResources, DiscountsMonth %>">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, DiscountsMonth %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="折扣總額" Caption="<%$ Resources:WebResources, TotalDiscount %>">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TotalDiscount %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                    ReadOnly="true">
                    <EditFormSettings Visible="false" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    ReadOnly="true">
                    <EditFormSettings Visible="false" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <TitlePanel>
                    <table cellpadding="0" cellspacing="0" align="left">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) { grid.AddNewRow(); }" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="ImportButton" runat="server" Text="<%$ Resources:WebResources, Import %>" 
                                 CausesValidation="false">
                                </dx:ASPxButton>
                               <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                    CloseAction="CloseButton" PopupElementID="ImportButton" ContentUrl="~/VSS/OPT/OPT18_Import.aspx"
                                    Width="640" Height="400" LoadingPanelID="lp" HeaderText="門市特殊客訴處理折扣設定Excel上傳">
                                    <ContentStyle>
                                        <Paddings Padding="4px"></Paddings>
                                    </ContentStyle>
                                </cc:ASPxPopupControl>
                                 <%--<cc:ASPxPopupControl ID="ASPxPopupControl2" runat="server"
                                AllowDragging="True" AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/OPT/OPT18_Import.aspx"
                                PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
                                Width="400px" Height="350px"
                                 PopupElementID="btnImport" LoadingPanelID="lp">            
                                </cc:ASPxPopupControl>--%>
                                <dx:ASPxLoadingPanel ID="lp" runat="server">
                                </dx:ASPxLoadingPanel>
                                
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <Styles>
                <TitlePanel HorizontalAlign="Left">
                </TitlePanel>
                <EditFormColumnCaption Wrap="False">
                </EditFormColumnCaption>
            </Styles>
            <SettingsPager PageSize="5">
            </SettingsPager>
            <SettingsEditing EditFormColumnCount="3" Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" KeyFieldName="項次"
            Width="100%" Settings-ShowTitlePanel="true" OnPageIndexChanged="grid_PageIndexChanged"
            OnRowInserting="detailGrid_RowInserting" OnRowUpdating="detailGrid_RowUpdating">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="detailGrid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button">
                    <EditButton Visible="True">
                    </EditButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                    ReadOnly="true">
                    <EditFormSettings Visible="false" />
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="角色" Caption="<%$ Resources:WebResources, Role %>">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Role %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <PropertiesComboBox>
                        <Items>
                            <dx:ListEditItem Text="店員" Value="店員" Selected="true" />
                            <dx:ListEditItem Text="店長" Value="店長" />
                        </Items>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Amount %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="比率" Caption="<%$ Resources:WebResources, Ratio %>">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Ratio %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <PropertiesTextEdit DisplayFormatString="{0}%">
                    </PropertiesTextEdit>
                    <EditItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="ratioTextBox" runat="server" HorizontalAlign="Right" Text='<%# Bind("比率") %>'>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    %
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="折扣上限金額" Caption="<%$ Resources:WebResources, LimitTheAmountOfDiscount %>">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, LimitTheAmountOfDiscount %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataColumn>
            </Columns>
            <Templates>
                <TitlePanel>
                    <table cellpadding="0" cellspacing="0" align="left">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e) { detailGrid.AddNewRow(); }" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <Styles>
                <TitlePanel HorizontalAlign="Left">
                </TitlePanel>
                <EditFormColumnCaption Wrap="False">
                </EditFormColumnCaption>
            </Styles>
            <SettingsPager PageSize="5">
            </SettingsPager>
            <SettingsEditing EditFormColumnCount="3" Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>
    </div>
</asp:Content>
