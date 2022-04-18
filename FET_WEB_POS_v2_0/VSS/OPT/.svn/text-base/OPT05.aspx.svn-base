<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT05.aspx.cs" Inherits="VSS_OPT_OPT05"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

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
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--總部發票設定作業 -->
                    <asp:Literal ID="Literal1" runat="server" Text="總部發票設定作業 "></asp:Literal>
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
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="TextBox2" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false" SkinID="PopupButton" />
                            </td>
                        </tr>
                    </table>
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
            OnRowUpdating="grid_RowUpdating" OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared"
            StylesFilterControl-EnableDefaultAppearance="False">
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
                    <DataItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("項次") %>' Width="40px"></asp:Label>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="<%$ Resources:WebResources, StoreNo %>"
                    VisibleIndex="4">
                    <EditItemTemplate>
                        <asp:TextBox ID="Label211" runat="server" Text='<%# Bind("門市編號") %>' Width="60px"></asp:TextBox></EditItemTemplate>
                    <DataItemTemplate>
                        <asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("門市編號") %>' OnClick="StoreNo_Click"
                            Width="60px" CommandName="Select" OnCommand="CommandButton_Click" CommandArgument='<%# Eval("門市編號") %>'>
                        </asp:LinkButton>
                    </DataItemTemplate>
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
                <dx:GridViewDataComboBoxColumn FieldName="用途" runat="server" Caption="用途" VisibleIndex="6">
                    <EditItemTemplate>
                        <asp:DropDownList ID="RadioButtonList1" runat="server" Width="60px" Text='<%# Bind("用途") %>'>
                            <asp:ListItem>離線</asp:ListItem>
                            <asp:ListItem>連線</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <DataItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("用途") %>' Width="40px"></asp:Label></DataItemTemplate>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="所屬年月(起)" runat="server" Caption="所屬年月(起)" ReadOnly="true"
                    VisibleIndex="7">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="所屬年月(訖)" runat="server" Caption="所屬年月(訖)" ReadOnly="true"
                    VisibleIndex="8">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="字軌" runat="server" Caption="字軌" ReadOnly="true"
                    VisibleIndex="9">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="起始編號" runat="server" Caption="起始編號" ReadOnly="true"
                    VisibleIndex="10">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="終止編號" runat="server" Caption="終止編號" ReadOnly="true"
                    VisibleIndex="11">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="目前編號" runat="server" Caption="目前編號" ReadOnly="true"
                    VisibleIndex="12">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="發票張數" runat="server" Caption="發票張數" ReadOnly="true"
                    VisibleIndex="13">
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
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="Import" PopupButtonID="btnImport"
                                    TargetControlID="HiddenField1" Width="630" Height="400" NavigateUrl="~/VSS/OPT/OPT05_Import.aspx" />
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <Styles>
                <EditFormColumnCaption Wrap="False">
                </EditFormColumnCaption>
            </Styles>
            <SettingsBehavior AllowFocusedRow="true" />
            <SettingsEditing Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
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
                <dx:GridViewDataTextColumn FieldName="機台號碼" runat="server" Caption="機台號碼" VisibleIndex="3"
                    ReadOnly="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="起始編號" runat="server" Caption="起始編號" VisibleIndex="4"
                    ReadOnly="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="終止編號" runat="server" Caption="終止編號" VisibleIndex="5"
                    ReadOnly="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="張數" runat="server" Caption="張數" VisibleIndex="6"
                    ReadOnly="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="發票分配日期" runat="server" Caption="發票分配日期" VisibleIndex="7"
                    ReadOnly="true">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>
    </div>
    </div>
    <cc:ASPxPopupControl ID="storesPopup2" ClientInstanceName="StoresPopup" SkinID="StoresPopup"
        runat="server" EnableViewState="False" PopupElementID="btnChooseProduct" TargetElementID="TextBox2">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
</asp:Content>
