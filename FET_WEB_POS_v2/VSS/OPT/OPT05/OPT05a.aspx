<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OPT05a.aspx.cs" Inherits="VSS_OPT_OPT05_OPT05a" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckStoreNO(s, e) {
            var StoreNO = s.GetValue();
            if (StoreNO == null || StoreNO == "") {
                lblStoreName.SetText(null);
                txtStoreName.SetText(null);
                btnAdd.SetEnabled(false);
                if (gvMaster.IsEditing()) gvMaster.CancelEdit();
                e.isValid = false;
                e.errorText = '門市編號不允許空白，請重新輸入';
                e.processOnServer = false;
            }
            else {
                PageMethods.getStoreInfo(StoreNO, getStoreInfo_OnOK);
            }
        }

        function getStoreInfo_OnOK(returnData) {
            if (returnData == '') {
                txtStoreName.SetText(null);
                lblStoreName.SetText(null);
                lblStoreName.SetValue(null);
                if (gvMaster.IsEditing()) gvMaster.CancelEdit();
                alert('門市編號不存在，請重新輸入');
                btnAdd.SetEnabled(false);
                btnDelete.SetEnabled(false);
            }
            else {
                txtStoreName.SetText(returnData);
                lblStoreName.SetText(returnData);
                lblStoreName.SetValue(returnData);
                btnAdd.SetEnabled(true);
                btnDelete.SetEnabled(true);
            }
        }

        function upperText(s, e) {
            if (s.GetText() != '') {
                s.SetText(s.GetText().toUpperCase());
                var str = s.GetText();
                var formatStr = "A,B,C,D,E,F,G,H,I,J,K,L,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                for (var i = 0; i < str.length; i++) {
                    if (formatStr.indexOf(str.substr(i, 1)) < 0) {
                        s.SetText(null);
                        return false;
                    }
                }
            }
        }

        _EditIndex = null;
        function gvMasterEdit(s, e, EditIndex) {
            _EditIndex = EditIndex;
            gvMaster.GetRowValues(EditIndex, 'USE_TYPE;S_USE_YM;E_USE_YM;CURRENT_NO', OnGetRowValues);
        }

        function OnGetRowValues(values) {
            //[0] = USE_TYPE    使用用途： 連線, 離線, 手開
            //[1] = S_USE_YM    所屬年月_起
            //[2] = E_USE_YM    所屬年月_迄
            //[3] = CURRENT_NO  目前編號

            var d = new Date();
            var m_names = new Array("01", "02", "03", "04", "05", "07", "07", "08", "09", "10", "11", "12");
            var curr_month = d.getMonth();
            var curr_year = d.getFullYear();
            var YM = curr_year + "/" + m_names[curr_month];

            if ((values[0] == "3" || values[0] == "4") && (YM < values[1]) && (values[3] == null || values[3] == '')) {
                gvMaster.StartEditRow(_EditIndex);
            }

        }

        function CheckLength(s, e) {
            var Qty = s.GetText();
            if (Qty != '') {
                var iQty = 0;
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串格式錯誤，請重新輸入';
                }
                else if (Qty.length < 8) {
                    e.isValid = false;
                    e.errorText = '編號必須為8碼，請重新輸入';
                }

                if (!e.isValid) {
                    e.processOnServer = false;
                }
            }
        }

        function ChangedFocusedIndex(s, e) {
            if (gvDetail.GetFocusedRowIndex() < 0) {
                var str = s.name.split('cell')[1].split('_');
                gvDetail.SetFocusedRowIndex(str[0]);
                e.processOnServer = true;
            }
        }

        function CheckbtnAdd(s, e) {
            if (lblStoreName.GetText() != '') {
                document.getElementById('checkbox1').checked = false;
                CheckAll_onclick();
                gvMaster.AddNewRow();
            }
            else {
                if (gvMaster.IsEditing()) gvMaster.CancelEdit();
                e.processOnServer = false;
                alert('請輸入門市編號');
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--門市離線發票設定作業-->
        <asp:Literal ID="Literal1" runat="server" Text="門市離線發票設定作業"></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <span style="color: Red">*</span>
                    門市編號：
                </td>
                <td class="tdval">
                     <uc1:PopupControl ID="txtStroeNo" runat="server" PopupControlName="StoresPopup" SetClientValidationEvent="CheckStoreNO" Enabled="false" />
                     <dx:ASPxTextBox ID="lblStoreName" runat="server" ClientInstanceName="lblStoreName" ClientVisible="false"></dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    門市名稱：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtStoreName" runat="server" ClientInstanceName="txtStoreName">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    所屬年月：
                </td>
                <td class="tdval" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0" style="width: 240px">
                        <tr>
                            <td>
                                起
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtSDate" runat="server" Width="100px" EditFormat="Custom" EditFormatString="yyyy/MM"
                                    ClientInstanceName="txtSMonth">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkMonth(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                訖
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtEDate" runat="server" EditFormat="Custom" EditFormatString="yyyy/MM"
                                    ClientInstanceName="txtEMonth">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkMonth(s, e); }" />
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
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false">
                    </dx:ASPxButton>
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
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="ASSIGN_ID"
        Width="100%" EnableCallBacks="false" 
        OnRowInserting="gvMaster_RowInserting" 
        OnRowCommand="gvMaster_RowCommand" 
        OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
        OnRowValidating="gvMaster_RowValidating"
        OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
        OnPageIndexChanged="gvMaster_PageIndexChanged" 
        OnRowUpdating="gvMaster_RowUpdating" 
        onprerender="gvMaster_PreRender" 
        oninitnewrow="gvMaster_InitNewRow" 
        onstartrowediting="gvMaster_StartRowEditing" >
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="True" ButtonType="Button" VisibleIndex="0">
                <HeaderStyle HorizontalAlign="Center" />
                <HeaderTemplate>
                    <div style="text-align: center">
                        <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                    </div>
                </HeaderTemplate>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn ButtonType="Button" Caption=" " VisibleIndex="1">
                <EditButton Visible="True"></EditButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="ItemIndex" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="2">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <DataItemTemplate>
                    <dx:ASPxLabel ID="lblItemIndex" runat="server" Text='<%#Container.ItemIndex + 1%>'>
                    </dx:ASPxLabel>
                    <dx:ASPxButton CausesValidation="false" ID="btnCommand" runat="server"
                        Text='<%#Container.ItemIndex + 1%>' CommandName="Select" UseSubmitBehavior="false" />
                </DataItemTemplate>
                <EditItemTemplate>
                    &nbsp;
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <%-- 用途 --%>
            <dx:GridViewDataDateColumn FieldName="USE_TYPE" UnboundType="String" Caption="<%$ Resources:WebResources, Use %>">
                <DataItemTemplate>
                    <asp:Label ID="labUseTypeName" runat="server" Text='<%# Bind("USE_TYPE_NAME") %>'></asp:Label>
                </DataItemTemplate>
                <EditItemTemplate>
                    手開
                </EditItemTemplate>
            </dx:GridViewDataDateColumn>
            <%-- 發票格式 --%>
            <dx:GridViewDataColumn FieldName="INVOICE_TYPE_ID" Caption="<%$ Resources:WebResources, InvoiceFormat %>">
                <DataItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("INVOICE_TYPE_NAME") %>'></asp:Label>
                </DataItemTemplate>
                <EditItemTemplate>
                    <dx:ASPxComboBox ID="ddlInvoiceType" runat="server" Value='<%# Bind("INVOICE_TYPE_ID") %>'
                        ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                        <Items>
                            <dx:ListEditItem Text="手開二聯式發票" Value="03" />
                            <dx:ListEditItem Text="手開三聯式發票" Value="04" />
                        </Items>
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </EditItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataTextColumn FieldName="S_USE_YM" Caption="<%$ Resources:WebResources, YearMonthStart %>">
                <EditItemTemplate>
                    <dx:ASPxDateEdit ID="txtS_DATE" runat="server" Text='<%# Bind("S_USE_YM") %>' EditFormatString="yyyy/MM" PopupHorizontalAlign="Center" PopupVerticalAlign="Middle"
                        ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                        <ValidationSettings>
                            <RequiredField IsRequired="True" ErrorText="必填欄位" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="E_USE_YM" Caption="<%$ Resources:WebResources, YearMonthEnd %>">
                <EditItemTemplate>
                    <dx:ASPxDateEdit ID="txtE_DATE" runat="server" Text='<%# Bind("E_USE_YM") %>' EditFormatString="yyyy/MM" PopupHorizontalAlign="Center" PopupVerticalAlign="Middle"
                        ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="LEADER_CODE" Caption="<%$ Resources:WebResources, WordTracks %>">
                <EditItemTemplate>
                    <dx:ASPxTextBox ID="txtLEADER_CODE" Text='<%# Bind("LEADER_CODE") %>' runat="server"
                        Width="50px" MaxLength="2" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                        </ValidationSettings>
                        <ClientSideEvents TextChanged="function(s,e){ upperText(s,e); }" />
                    </dx:ASPxTextBox>
                </EditItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataTextColumn FieldName="INIT_NO" Caption="<%$ Resources:WebResources, StartingNumber %>">
                <EditItemTemplate>
                    <dx:ASPxTextBox ID="txtINIT_NO" Text='<%# Bind("INIT_NO") %>' runat="server" Width="100px"
                        MaxLength="8" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串格式錯誤，請重新輸入" />
                        </ValidationSettings>
                        <ClientSideEvents Validation="function(s,e) { CheckLength(s, e); } " />
                    </dx:ASPxTextBox>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="END_NO" Caption="<%$ Resources:WebResources, EndNumber %>">
                <EditItemTemplate>
                    <dx:ASPxTextBox ID="txtEND_NO" Text='<%# Bind("END_NO") %>' runat="server" Width="100px"
                        MaxLength="8" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串格式錯誤，請重新輸入" />
                        </ValidationSettings>
                        <ClientSideEvents Validation="function(s,e) { CheckLength(s, e); } " />
                    </dx:ASPxTextBox>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CURRENT_NO" runat="server" Caption="<%$ Resources:WebResources, TheCurrentNumber1 %>">
                <EditItemTemplate>
                    &nbsp;
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="SHEET_COUNT" runat="server" Caption="<%$ Resources:WebResources, InvoiceNumberOfSheets %>">
                <EditItemTemplate>
                    &nbsp;
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="MODI_DTM" runat="server" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                <EditItemTemplate>
                    &nbsp;
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="EMPNAME" runat="server" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                <EditItemTemplate>
                    &nbsp;
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="STORE_NO" Visible="false">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <TitlePanel>
                <table cellpadding="0" cellspacing="0" border="0" align="left">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnAdd" ClientInstanceName="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                UseSubmitBehavior="false" AutoPostBack="false" OnClick="btnAdd_Click" CausesValidation="false">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDelete" ClientInstanceName="btnDelete" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                OnClick="btnDelete_Click" CausesValidation="false" UseSubmitBehavior="false" />
                        </td>
                    </tr>
                </table>
            </TitlePanel>
        </Templates>
        <SettingsBehavior AllowFocusedRow="True" />
        <SettingsEditing Mode="Inline" />
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        <Settings ShowTitlePanel="True"></Settings>
        <SettingsPager PageSize="5">
        </SettingsPager>
       <%-- <ClientSideEvents RowDblClick="function(s, e) { document.getElementById('checkbox1').checked=false; CheckAll_onclick(); gvMasterEdit(s, e, e.visibleIndex); }" />--%>
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" Width="100%"
                Visible="false" KeyFieldName="HOST_NO" EnableViewState="true" OnPageIndexChanged="gvDetail_PageIndexChanged"
                OnHtmlRowCreated="gvDetail_HtmlRowCreated">
                <Columns>
                    <dx:GridViewDataTextColumn runat="server" Caption="<%$ Resources:WebResources, Items %>">
                        <DataItemTemplate>
                            <%#Container.ItemIndex + 1%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="HOST_NO" runat="server" Caption="<%$ Resources:WebResources, CashRegisterNo %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="START_NO" runat="server" Caption="<%$ Resources:WebResources, StartingNumber %>">
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="txtSTART_NO" runat="server" Text='<%# Bind("START_NO") %>' MaxLength="8"
                                OnTextChanged="txtSTART_NO_TextChanged" AutoPostBack="true" Width="100px" ValidationSettings-ValidationGroup="ModifyNo" >
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="\d{8}" ErrorText="輸入字串格式錯誤，請重新輸入" />
                                </ValidationSettings>
                                <ClientSideEvents Validation="function(s,e) { CheckLength(s, e); } " />
                            </dx:ASPxTextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="END_NO" runat="server" Caption="<%$ Resources:WebResources, EndNumber %>">
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="txtEND_NO" runat="server" Text='<%# Bind("END_NO") %>' MaxLength="8"
                                OnTextChanged="txtEND_NO_TextChanged" AutoPostBack="true" Width="100px" ValidationSettings-ValidationGroup="ModifyNo">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="\d{8}" ErrorText="輸入字串格式錯誤，請重新輸入" />
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="function(s,e) { ChangedFocusedIndex(s,e); }" Validation="function(s,e) { CheckLength(s, e); } " />
                            </dx:ASPxTextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CURRENT_NO" runat="server" Caption="<%$ Resources:WebResources, TheCurrentNumber %>"
                        ReadOnly="true">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SHEET_COUNT" runat="server" Caption="<%$ Resources:WebResources, NumberOfSheets %>"
                        ReadOnly="true">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="MODI_DTM" runat="server" Caption="<%$ Resources:WebResources, TheDateOfTheInvoiceDistribution %>"
                        ReadOnly="true">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OLDSTART_NO" Visible="false">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="OLDEND_NO" Visible="false">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="true" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsPager PageSize="5">
                </SettingsPager>
            </cc:ASPxGridView>
            <div class="seperate">
            </div>
            <div class="btnPosition" id="showFooterBtn" runat="server" visible="false">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxTextBox ID="hdError" runat="server" ClientVisible="false">
                            </dx:ASPxTextBox>
                            <dx:ASPxButton ID="btnSave" ClientInstanceName="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                ValidationGroup="ModifyNo" UseSubmitBehavior="false" OnClick="btnSave_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                CausesValidation="false" UseSubmitBehavior="false" OnClick="btnCancel_Click">
                                <ClientSideEvents Click="function(s,e) { gvDetail.SetFocusedRowIndex(-1); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
