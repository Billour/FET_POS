<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OPT05.aspx.cs" Inherits="VSS_OPT_OPT05" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" src="../../ClientUtility/Common.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

    <script type="text/javascript" language="javascript">
        function imeicheckbox(con) {
            if (con.checked) {
                openwindow("SAL01_inputIMEIData.aspx");
            }
        }

        function checkID() {
            var vID = document.getElementById("tbInvoiceNo").value;
            if (vID.length != 8 && vID > 0) {
                openwindow("SAL01_checkIDNumber.aspx", 300, 200);
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <input type="hidden" id="hdNo" runat="server" class="hdNo" />
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--總部發票設定作業 -->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InvoiceSettingHQ %> "></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--門市編號 -->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                </td>
                <td class="tdval" width="80px">
                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" />
                </td>
                <td class="tdtxt">
                    <!--門市名稱 -->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server" Width="80px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--狀態 -->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList2" runat="server" Width="100">
                        <Items>
                            <dx:ListEditItem Value="-請選擇-" Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Value="有效" Text="有效" />
                            <dx:ListEditItem Value="尚未生效" Text="尚未生效" />
                            <dx:ListEditItem Value="已過期" Text="已過期" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!-- 所屬年月 -->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, YearMonth %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="transferOutStartDate" runat="server" Width="100px">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    <div>
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
    <div id="Div1" class="SubEditBlock" style="text-align: left;">
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
            Settings-ShowTitlePanel="true" Width="100%" OnHtmlRowPrepared="grid_HtmlRowPrepared"
            OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged"
            OnRowUpdating="grid_RowUpdating" OnRowInserting="grid_RowInserting" OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared"
            StylesFilterControl-EnableDefaultAppearance="False" EnableCallBacks="False" OnFocusedRowChanged="gvMaster_FocusedRowChanged">
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
                    ReadOnly="true" VisibleIndex="3">
                    <EditItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("項次") %>' Width="40px"></asp:Label>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="<%$ Resources:WebResources, StoreNo %>"
                    VisibleIndex="4">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreNo %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="Label211" runat="server" Text='<%# Bind("門市編號") %>' Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="門市名稱" runat="server" Caption="<%$ Resources:WebResources, StoreName %>"
                    VisibleIndex="5">
                    <EditItemTemplate>
                        <asp:TextBox ID="Label5" runat="server" Text='<%# Bind("門市名稱") %>' Width="60px"></asp:TextBox></EditItemTemplate>
                    <DataItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("門市名稱") %>' Width="80px"></asp:Label></DataItemTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="用途" runat="server" Caption="<%$ Resources:WebResources, Use %>"
                    VisibleIndex="6">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Use %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <PropertiesComboBox>
                        <Items>
                            <dx:ListEditItem Text="離線" Value="離線" />
                            <dx:ListEditItem Text="連線" Value="連線" />
                        </Items>
                    </PropertiesComboBox>
                    <DataItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("用途") %>' Width="40px"></asp:Label></DataItemTemplate>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="所屬年月(起)" runat="server" Caption="<%$ Resources:WebResources, YearMonthStart %>"
                    ReadOnly="true" VisibleIndex="7">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, YearMonthStart %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="所屬年月(訖)" runat="server" Caption="<%$ Resources:WebResources, YearMonthEnd %>"
                    ReadOnly="true" VisibleIndex="8">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, YearMonthEnd %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="字軌" runat="server" Caption="<%$ Resources:WebResources, WordTracks %>"
                    ReadOnly="true" VisibleIndex="9">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, WordTracks %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="起始編號" runat="server" Caption="<%$ Resources:WebResources, StartingNumber %>"
                    ReadOnly="true" VisibleIndex="10">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StartingNumber %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="終止編號" runat="server" Caption="<%$ Resources:WebResources, EndNumber %>"
                    ReadOnly="true" VisibleIndex="11">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, EndNumber %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="目前編號" runat="server" Caption="<%$ Resources:WebResources, TheCurrentNumber %>"
                    ReadOnly="true" VisibleIndex="12">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="發票張數" runat="server" Caption="<%$ Resources:WebResources, InvoiceNumberOfSheets %>"
                    ReadOnly="true" VisibleIndex="13">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                    VisibleIndex="14" ReadOnly="true">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    VisibleIndex="15" ReadOnly="true">
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
                                <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, add %>"
                                    OnClick="btnAdd_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, delete %>">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                </dx:ASPxButton>
                                <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                    CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl="~/VSS/OPT/OPT05_Import.aspx"
                                    Width="640" Height="400" LoadingPanelID="lp">
                                    <ContentStyle>
                                        <Paddings Padding="4px"></Paddings>
                                    </ContentStyle>
                                </cc:ASPxPopupControl>
                                <dx:ASPxLoadingPanel ID="lp" runat="server">
                                </dx:ASPxLoadingPanel>
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <Styles>
                <EditFormColumnCaption Wrap="False">
                </EditFormColumnCaption>
            </Styles>
            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
            <StylesFilterControl EnableDefaultAppearance="False">
            </StylesFilterControl>
            <SettingsEditing Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
            <Settings ShowTitlePanel="True"></Settings>
        </cc:ASPxGridView>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" Width="70%"
            Visible="false" OnHtmlRowPrepared="grid_HtmlRowPrepared" OnHtmlRowCreated="grid_HtmlRowCreated"
            OnPageIndexChanged="grid_PageIndexChanged" OnRowUpdating="grid_RowUpdating">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                    VisibleIndex="2" ReadOnly="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="機台號碼" runat="server" Caption="<%$ Resources:WebResources, CashRegisterNo %>"
                    VisibleIndex="3" ReadOnly="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="起始編號" runat="server" Caption="<%$ Resources:WebResources, StartingNumber %>"
                    VisibleIndex="4" ReadOnly="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="終止編號" runat="server" Caption="<%$ Resources:WebResources, EndNumber %>"
                    VisibleIndex="5" ReadOnly="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="張數" runat="server" Caption="<%$ Resources:WebResources, NumberOfSheets %>"
                    VisibleIndex="6" ReadOnly="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="發票分配日期" runat="server" Caption="<%$ Resources:WebResources, TheDateOfTheInvoiceDistribution %>"
                    VisibleIndex="7" ReadOnly="true">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>
    </div>
</asp:Content>
