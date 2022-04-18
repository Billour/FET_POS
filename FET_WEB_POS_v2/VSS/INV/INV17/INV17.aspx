<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV17.aspx.cs" Inherits="VSS_INV_INV17" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

   <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
   <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
   
    <script type="text/javascript">

        function chkDate(e) {
            var x = txtSDate.GetText();
            var y = txtEDate.GetText();
            if (x != '' && y != '') {
                if (x > y) {
                    e.isValid = false;
                    if (!e.isValid) {
                        alert('訖 日期不可小於 起 日期!!');
                        e.processOnServer = false;
                    }
                }
            }
        }

        function CheckAll_clear() {
            for (var i = 0; i < gvMaster.pageRowCount; i++) {
                gvMaster.SelectRowOnPage(i + gvMaster.visibleStartIndex, false);
            }
        }
        function dataInit(s, e) {
            alert('init');
        }
        function chkDate2(s, e) {
            var selectedDate = s.date;
            alert('test');
            if (selectedDate == null || selectedDate == false) return;
        }
        function CheckRequiredField(s, e) {
            var Value = s.GetValue();
            if (Value == null || Value == "") {
                e.isValid = false;
                e.errorText = '【欄位名稱】不允許空白，請重新輸入';
                return false;
            }
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--關帳日設定-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ClosingDateSettings %>"></asp:Literal>
        </div>
        <div>
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--關帳年月-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ClosingYearMonth %>"></asp:Literal>：
                    </td>
                    <td nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSDate" ClientInstanceName="txtSDate" EditFormatString="yyyy/MM" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtEDate" ClientInstanceName="txtEDate" EditFormatString="yyyy/MM" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="CUT_OFF_DATE_ID"
                    Width="100%" AutoGenerateColumns="False" 
                    OnPageIndexChanged="gvMaster_PageIndexChanged" 
                    OnRowUpdating="gvMaster_RowUpdating"
                    OnRowInserting="gvMaster_RowInserting" 
                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                    OnStartRowEditing="gvMaster_StartRowEditing" 
                    OnRowValidating="gvMaster_RowValidating"
                    OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" 
                    OnInitNewRow="gvMaster_InitNewRow">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ShowSelectCheckbox="true">
                            <HeaderTemplate>
                                <div style="text-align: center">
                                   <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1" Caption=" ">
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="CUT_YYMM" Caption="<%$ Resources:WebResources, ClosingYearMonth %>"
                            VisibleIndex="2">
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" EditFormatString="yyyy/MM" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                    Text='<%# Bind("CUT_YYMM") %>'>
                                    <ValidationSettings SetFocusOnError="True">
                                        <RequiredField IsRequired="True" ErrorText="必填欄位"></RequiredField>
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s,e){ CheckRequiredField(s, e);  }" />
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CUT_OFF_DATE" Caption="<%$ Resources:WebResources, ClosingDate %>"
                            VisibleIndex="3">
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" EditFormatString="yyyy/MM/dd"
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' Text='<%# Bind("CUT_OFF_DATE") %>'>
                                    <ValidationSettings SetFocusOnError="True">
                                        <RequiredField IsRequired="True" ErrorText="必填欄位"></RequiredField>
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s,e){ CheckRequiredField(s, e); }" />
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_USER" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                            VisibleIndex="4">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                            VisibleIndex="5">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btnNew_Click" Visible="true">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                            Visible="true" OnClick="btnDelete_Click" SkinID="DeleteBtn">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <Settings ShowTitlePanel="True" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsPager PageSize="10" />
                </cc:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
