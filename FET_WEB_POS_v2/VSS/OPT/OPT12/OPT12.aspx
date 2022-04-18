<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OPT12.aspx.cs" Inherits="VSS_OPT_OPT12" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        //比較起訖累點點數，結束累點點數不可小於開始累點點數
        function chkPoint(s, e) {
            //debugger;
            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty != null) {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    //e.isValid = false;
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                else if (iQty < 0) {
                    //e.isValid = false;
                    e.errorText = '累點點數不允許小於0，請重新輸入';
                    return false;
                }
                else if (Qty.indexOf(".") > 0) {
                    //e.isValid = false;
                    e.errorText = '累點點數不允許輸入小數點，請重新輸入';
                    return false;
                }
            }
            
            var x = SPoint.GetValue();
            var y = EPoint.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if ((x != "" && y != "")) {

                e.isValid = (Number(x) <= Number(y));
                if (!e.isValid) {
                    alert("[累點點數訖]不允許小於[累點點數起]，請重新輸入!!");
                    s.SetValue(null);
                    return false;
                }
            }
            else {
                return true;
            }
        }

        //比較起訖累點金額，結束累點金額不可小於開始累點金額
        function chkCurrency(s, e) {
            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty != null) {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                else if (iQty < 0) {
                    e.isValid = false;
                    e.errorText = '累點金額不允許小於0，請重新輸入';
                    return false;
                }
                else if (Qty.indexOf(".") > 0) {
                    e.isValid = false;
                    e.errorText = '累點金額不允許輸入小數點，請重新輸入';
                    return false;
                }
            }
            
            var x = SCurrency.GetValue();
            var y = ECurrency.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if ((x != "" && y != "")) {

                e.isValid = (Number(x) <= Number(y));
                if (!e.isValid) {
                    alert("[累點金額訖]不允許小於[累點金額起]，請重新輸入!!");
                    s.SetValue(null);
                    return false;
                }
            }
            else {
                return true;
            }
        }

        function getAccuInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getInfo(_gvSender.GetText(), getAccuInfo_OnOK);
        }
        function getAccuInfo_OnOK(returnData) {
            if (returnData != '') {
                ACCU_NAME.SetValue(returnData);
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
            else {
                ACCU_NAME.SetValue(null);
            }
        }

        function getProdInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getInfo(_gvSender.GetText(), getProdInfo_OnOK);
        }
        function getProdInfo_OnOK(returnData) {
            if (returnData != '') {
                PRODNAME.SetValue(returnData);
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
            else {
                PRODNAME.SetValue(null);
            }
        }

        function getPRODINFO2(s, e) {
            this.s = s;
            // alert(s.GetText());
            this.EventArgs = e;
            this.Sender = s;
            if (s.GetText() != '')
                PageMethods.getPRODINFOExtraSale(Sender.GetText(), getPRODINFO_OnOK2);
        }

        function getPRODINFO_OnOK2(returnData) {
            var fName = "popupPRODNO";

            //lblAccuName = getClientInstance('TxtBox', s.name.replace(fName, "txttPRODNAME"));
            if (returnData == '') {
                EventArgs.processOnServer = false;
                alert("折扣料號不存在!");
                Sender.Focus();
                ACCU_NAME.SetText('');
            }
            else {
                if (returnData == "fail") {
                    EventArgs.processOnServer = false;
                    alert("折扣料號不允許設定!");
                    Sender.Focus();
                    ACCU_NAME.SetText('');
                }
                else {
                    if (Sender.GetText() == '')
                        ACCU_NAME.SetText(''); //txtActivityName.SetText('');
                    else
                        ACCU_NAME.SetText(returnData);
                }

            }
        }

        function getPRODINFO1(s, e) {
            // alert(s.GetText());
            this.EventArgs = e;
            this.Sender = s;
            if (s.GetText() != '')
                PageMethods.getPRODINFO(Sender.GetText(), getPRODINFO_OnOK1);
        }

        function getPRODINFO_OnOK1(returnData) {
            var fName = "PopupControl1";
            // txtPRODNAME = getClientInstance('TxtBox', Sender.name.replace(fName, "txtActivityName"));
            // txtActivityName.SetText(retnrData)
            if (returnData == '') {
                EventArgs.processOnServer = false;
                alert("折扣料號不存在!");
                Sender.Focus();
            }
            else {
                //0 品名 1 I MEM_FLAGE
                if (Sender.GetText() == '')
                    ACCU_NAME.SetText('');
                else
                    ACCU_NAME.SetText(returnData);

            }
        }

        function CheckQty(s, e) {

            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty != null) {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                else if (iQty <= 0) {
                    e.isValid = false;
                    if (s.name.indexOf("txtAccuCurrency") > 0) {
                        e.errorText = '累點金額不允許小於0，請重新輸入';
                    }
                    else {
                        e.errorText = '累點點數不允許小於0，請重新輸入';
                    }
                    return false;
                }
                else if (Qty.indexOf(".") > 0) {
                    e.isValid = false;
                    if (s.name.indexOf("txtAccuCurrency") > 0) {
                        e.errorText = '累點金額不允許輸入小數點，請重新輸入';
                    }
                    else {
                        e.errorText = '累點點數不允許輸入小數點，請重新輸入';
                    }
                    return false;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--HappyGo點數累點設定-->
        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, HappyGoSetPointsAccumulated %>"></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--累點名稱-->
                    <asp:Literal ID="lblAccuName" runat="server" Text="<%$ Resources:WebResources, NameOfAccumulatedPoints %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <dx:ASPxTextBox ID="txtAccuName" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--開始日期-->
                    <asp:Literal ID="lblSDate" runat="server" Text="<%$ Resources:WebResources, Startdate %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="lblSDateS" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtSDateS" runat="server" ClientInstanceName="txtSDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="lblSDateE" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtSDateE" runat="server" ClientInstanceName="txtEDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
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
                    <asp:Literal ID="lblDividablePoint" runat="server" Text="<%$ Resources:WebResources, PointsAccumulatedPoints %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="lblDividablePointS" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtDividablePointS" runat="server" ClientInstanceName="SPoint" Width="50">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s, e){ chkPoint(s, e); }" />
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="lblDividablePointE" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtDividablePointE" runat="server" ClientInstanceName="EPoint" Width="50">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s, e){ chkPoint(s, e); }" />
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
                    <asp:Literal ID="lblAccuCurrency" runat="server" Text="<%$ Resources:WebResources, AmountAccumulatedPoints %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="lblAccuCurrencyS" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAccuCurrencyS" runat="server" ClientInstanceName="SCurrency" Width="50">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" />
                                    </ValidationSettings>
                                    <ClientSideEvents  Validation="function(s, e){ chkCurrency(s, e); }" />
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="lblAccuCurrencyE" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAccuCurrencyE" runat="server" ClientInstanceName="ECurrency" Width="50">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" />
                                    </ValidationSettings>
                                    <ClientSideEvents  Validation="function(s, e){ chkCurrency(s, e); }" />
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
                    <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div>
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" AutoPostBack="true"
            OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged">
            <TabPages>
                <dx:TabPage Text="<%$ Resources:WebResources, TiredPointSetting %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="ACCU_ID"
                                    Width="100%"
                                    OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" 
                                    OnPageIndexChanged="gvMaster_PageIndexChanged" 
                                    OnRowInserting="gvMaster_RowInserting"
                                    OnRowUpdating="gvMaster_RowUpdating" 
                                    OnHtmlEditFormCreated="gvMaster_HtmlEditFormCreated" 
                                    OnRowValidating="gvMaster_RowValidating" 
                                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" 
                                    OnStartRowEditing="gvMaster_StartRowEditing" >
                                    <Columns>
                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowSelectCheckbox="True">
                                            <HeaderTemplate>
                                                <div style="text-align: center">
                                                    <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                                </div>
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button">
                                            <EditButton Visible="true">
                                            </EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>">
                                            <DataItemTemplate>
                                                <%#Container.ItemIndex + 1%>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <%-- 折扣料號- --%>
                                        <dx:GridViewDataTextColumn FieldName="ACCU_NO">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ACCU_NAME">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, NameOfAccumulatedPoints %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="S_DATE" HeaderStyle-HorizontalAlign="Center"
                                            Caption="<%$ Resources:WebResources, startdate %>">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span>
                                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, startdate %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                            <PropertiesDateEdit>
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" ErrorText="必填欄位"></RequiredField>
                                                </ValidationSettings>
                                            </PropertiesDateEdit>
                                            <EditItemTemplate>
                                                <dx:ASPxDateEdit MinDate="2011-01-05" ID="txtSDate" Value='<%# Bind("S_DATE") %>'
                                                    runat="server" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                                    EditFormat="Custom" EditFormatString="yyyy/MM/dd">
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="開始日期不允許為空值，請重新輸入" />
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </EditItemTemplate>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, ENDDATE %>" />
                                        <dx:GridViewDataTextColumn FieldName="ACCU_CURRENCY">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AmountAccumulatedPoints %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DIVIDABLE_POINT">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PointsAccumulatedPoints %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnAddNewM" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                            OnClick="btnAddNewM_Click" />
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnDeleteM" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                            OnClick="btnDeleteM_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                        <EditForm>
                                            <table width="90%">
                                                <tr>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--折扣料號-->
                                                        <asp:Literal ID="lblAccuNo" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <%--<dx:ASPxComboBox ID="ddlDiscount" runat="server" Width="170px" DropDownWidth="550"
                                                            DropDownStyle="DropDownList" TextField="PRODNO" ValueField="PRODNAME" ValueType="System.String"
                                                            TextFormatString="{0}" AutoPostBack="true" OnValueChanged="ddlDiscount_ValueChanged"
                                                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" CallbackPageSize="30" MaxLength="11"
                                                            ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                                            <Columns>
                                                                <dx:ListBoxColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                                                                    Width="50%" />
                                                                <dx:ListBoxColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, DiscountName %>"
                                                                    Width="50%" />
                                                            </Columns>
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                        <uc1:PopupControl ID="ddlDiscount" runat="server" PopupControlName="ProductsPopup" Text=""
                                                            AutoPostBack="false" KeyFieldValue1="extrasale"
                                                              IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                                             OnClientTextChanged="function(s,e){ getPRODINFO2(s,e);}" />
                                                        --%>
                                                        <uc1:PopupControl ID="ddlDiscount" runat="server" PopupControlName="DiscountPopup" 
                                                            AutoPostBack="false" SetClientValidationEvent="getPRODINFO1(s,e);" />
                                                    </td>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--開始日期-->
                                                        <asp:Literal ID="lblSDate" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <dx:ASPxDateEdit 
                                                            ID="txtSDate" runat="server" Value='<%# Bind("S_DATE") %>'>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <asp:Literal ID="lblEDate" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <dx:ASPxDateEdit ID="txtEDate" runat="server" Value='<%# Bind("E_DATE") %>'>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--累點名稱-->
                                                        <asp:Literal ID="lblAccuName" runat="server" Text="<%$ Resources:WebResources, NameOfAccumulatedPoints %>">
                                                        </asp:Literal>：
                                                        <%--<dx:ASPxTextBox ID="lblAccuName" ClientInstanceName="lblAccuName" runat="server" Text="<%$ Resources:WebResources, NameOfAccumulatedPoints %>" 
                                                            Enabled="false" Width="170px">
                                                            </dx:ASPxTextBox>--%>
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <dx:ASPxTextBox ID="txtAccuName" runat="server" Text='<%# Bind("ACCU_NAME") %>' ClientInstanceName="ACCU_NAME"
                                                            ReadOnly="true" Border-BorderStyle="None" BackColor="#EFEFEF" Width="100%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--累點金額-->
                                                        <asp:Literal ID="lblAccuCurrency" runat="server" Text="<%$ Resources:WebResources, AmountAccumulatedPoints %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <dx:ASPxTextBox ID="txtAccuCurrency" runat="server" Width="100%" Text='<%# Bind("ACCU_CURRENCY") %>'
                                                            HorizontalAlign="Right" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="8">
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串不為數字格式，請重新輸入" />
                                                            </ValidationSettings>
                                                            <ClientSideEvents Validation="function(s,e){ CheckQty(s, e); }" />
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td class="tdtxt" nowrap="nowrap">
                                                        <!--累點點數-->
                                                        <asp:Literal ID="lblDividablePoint" runat="server" Text="<%$ Resources:WebResources, PointsAccumulatedPoints %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" nowrap="nowrap">
                                                        <dx:ASPxTextBox ID="txtDividablePoint" runat="server" Width="100%" Text='<%# Bind("DIVIDABLE_POINT") %>'
                                                            HorizontalAlign="Right" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="8">
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串不為數字格式，請重新輸入" />
                                                            </ValidationSettings>
                                                            <ClientSideEvents Validation="function(s,e){ CheckQty(s, e); }" />
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
                                    <Settings ShowTitlePanel="true" />
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
                                    KeyFieldName="SID" Width="100%"
                                    OnHtmlRowPrepared="gvCondition_HtmlRowPrepared"
                                    OnPageIndexChanged="gvCondition_PageIndexChanged"
                                    OnRowInserting="gvCondition_RowInserting" 
                                    OnRowUpdating="gvCondition_RowUpdating"
                                    OnRowValidating="gvCondition_RowValidating" 
                                    OnStartRowEditing="gvCondition_StartRowEditing">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                            <HeaderTemplate>
                                                <input type="checkbox" onclick="gvCondition.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button">
                                            <EditButton Visible="true">
                                            </EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                                            <DataItemTemplate>
                                                <%#Container.ItemIndex + 1%>
                                            </DataItemTemplate>
                                            <EditItemTemplate>
                                                &nbsp;</EditItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PRODNO">
                                            <HeaderCaptionTemplate>
                                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                                                </dx:ASPxLabel>
                                            </HeaderCaptionTemplate>
                                            <EditItemTemplate>
                                                <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup" KeyFieldValue1="extrasale"
                                                    Text='<%# Bind("PRODNO") %>' IsValidation="true" ValidationGroup="<%# Container.ValidationGroup %>"
                                                    SetClientValidationEvent="getProdInfo" />
                                            </EditItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">
                                            <EditItemTemplate>
                                                <dx:ASPxTextBox ID="txtProdName" runat="server" Text='<%# Eval("PRODNAME") %>' ClientInstanceName="PRODNAME"
                                                    ReadOnly="true" Border-BorderStyle="None" Width="100%">
                                                </dx:ASPxTextBox>
                                            </EditItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnAddNewC" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                            OnClick="btnAddNewC_Click" />
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnDeleteC" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                            OnClick="btnDeleteC_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                    </Templates>
                                    <SettingsEditing Mode="Inline" />
                                    <SettingsPager PageSize="10">
                                    </SettingsPager>
                                    <Settings ShowTitlePanel="true" />
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
</asp:Content>
