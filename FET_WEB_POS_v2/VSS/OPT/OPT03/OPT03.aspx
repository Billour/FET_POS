<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT03.aspx.cs" Inherits="VSS_OPT_OPT03" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckDate(s, e, fName) {

            var txtSDate = getClientInstance('TxtBox', s.name.replace(fName, "7_txtSDATE"));
            var txtEDate = getClientInstance('TxtBox', s.name.replace(fName, "8_txtEDATE"));

            var x = txtSDate.GetValue();
            var y = txtEDate.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[結束日期]必須大於[開始日期]，請重新輸入!");
                    s.SetValue(null);
                    return false;
                }
            }
            else {
                return true;
            }

        }

        function chkSDate(s, e) {
            e.isValid = true;
            var x = txtSDate_S.GetValue();
            var y = txtSDate_E.GetValue();

            var dvalue = s.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[開始起日訖值]不允許小於[開始起日起值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {

                var Sx = txtEDate_S.GetValue();
                var Sy = txtEDate_E.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue > Sx) || (Sy != "" && dvalue > Sy)) {
                    e.isValid = false;
                    alert("[開始日期]不允許大於[結束日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }

        }

        function chkEDate(s, e) {
            e.isValid = true;
            var x = txtEDate_S.GetValue();
            var y = txtEDate_E.GetValue();

            var dvalue = s.GetValue(); 
            
            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[結束訖日起值]不允許大於[結束訖日訖值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {
                var Sx = txtSDate_S.GetValue();
                var Sy = txtSDate_E.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue < Sx) || (Sy != "" && dvalue < Sy)) {
                    e.isValid = false;
                    alert("[結束日期]不允許小於[開始日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }


        function CheckRate(s, e) {

            var Rate = s.GetValue();
            var iRate = 0;
            if (Rate != null) {
                iRate = Number(Rate);
                if (isNaN(iRate)) {
                    e.isValid = false;
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                else if (iRate <= 0) {
                    e.isValid = false;
                    if (s.name.indexOf("txtSEQMENT_RATE") > 0) {
                        e.errorText = '分期利率不允許小於等於0，請重新輸入';
                    }
                    else if (s.name.indexOf("txtSETTLEMENT_RATE") > 0) {
                        e.errorText = '成本中心拆帳比率不允許小於等於0，請重新輸入';
                    }
                    else
                    {
                        e.errorText = '分期期數不允許小於等於0，請重新輸入';
                    }
                    return false;
                }
            }
        }
        function CheckRateKeydown(s, e)
        {
            var sValue = s.GetValue();
            if (sValue != null && sValue != '')
            {
                var regex = /^-?\d+\.?\d*?$/;
                if (sValue.match(regex) == null)
                {

                    s.SetText(sValue.substr(0, sValue.length - 1));
                    event.returnValue = false;
                    return;
                }
                else
                {
                    var splitNumber = sValue.split('.');
                    if (splitNumber.length == 2)
                    {
                        if (splitNumber[1].length > 1 && (event.keyCode >= 49 && event.keyCode <= 105 || event.keyCode == 190))
                        {
                            event.returnValue = false;
                            return;
                        }

                    }
                }
            } 
        }
        function gvMasterFocusedRowChanged(s, e) {

            if (s.IsEditing()) {
                if (s.focusedRowIndex > -1) {
                    if (confirm('是否確定要離開編輯模式?')) {
                        e.processOnServer = true;
                    }
                    else {
                        s.SetFocusedRowIndex(-1);
                        e.processOnServer = false;
                        return false;
                    }
                }
            }
            else {
                e.processOnServer = true;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
        <!--信用卡分期設定作業 -->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, CreditCardInstallmentSetting %> "></asp:Literal>
    </div>
    
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--發卡銀行-->                            
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, IssuingBank %> "></asp:Literal>：
                </td>
                <td class="tdval">                    
                    <dx:ASPxComboBox ID="ddlCardBank" runat="server" ></dx:ASPxComboBox>
                </td>
                <td class="tdtxt">                         
                     <!--成本中心-->                           
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CostCenter %> "></asp:Literal>：
                </td>
                <td class="tdval">
                    <%--<dx:ASPxComboBox ID="ddlCostCenter" runat="server"></dx:ASPxComboBox>--%>
                    <dx:ASPxComboBox ID="ddlCostCenter" runat="server" Width="170px" DropDownWidth="100"
                        DropDownStyle="DropDownList" TextField="COST_CENTER_NO" ValueField="COST_CENTER_NO" ValueType="System.String"
                        TextFormatString="{0}" IncrementalFilteringMode="StartsWith" CallbackPageSize="30" MaxLength="5">
                       <%-- <Columns>
                            <dx:ListBoxColumn FieldName="COST_CENTER_NO" Caption="<%$ Resources:WebResources, CostCenter %>"
                                Width="50%" />
                            <dx:ListBoxColumn FieldName="COST_CENTER_NO" Caption="<%$ Resources:WebResources, CostCenter %>"
                                Width="50%" />
                        </Columns>--%>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                     <!--狀態-->                    
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, status %> "></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddlStatus" runat="server" Width="80px">
                        <Items>
                            <dx:ListEditItem Value="" Text="ALL" Selected="true" />
                            <dx:ListEditItem Value="有效" Text="有效" />
                            <dx:ListEditItem Value="尚未生效" Text="尚未生效" />
                            <dx:ListEditItem Value="已過期" Text="已過期" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--分期期數-->                            
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, InstallmentsQty %> "></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtPaySeqment" runat="server" MaxLength="2">
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式，請重新輸入" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td align="right">
                    <!--開始日期-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                        <tr>
                            <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                            <td>
                                <dx:ASPxDateEdit ID="txtSDate_S" runat="server" ClientInstanceName="txtSDate_S" EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkSDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                            <td>
                                <dx:ASPxDateEdit ID="txtSDate_E" runat="server" ClientInstanceName="txtSDate_E" EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkSDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="right">
                    <!--結束日期-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                        <tr>
                            <td><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                            <td>
                                <dx:ASPxDateEdit ID="txtEDate_S" runat="server" ClientInstanceName="txtEDate_S" EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkEDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                            <td>
                                <dx:ASPxDateEdit ID="txtEDate_E" runat="server" ClientInstanceName="txtEDate_E" EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkEDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
            
    <div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            UseSubmitBehavior="false"  OnClick="btnSearch_Click" >
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
            
        <asp:UpdatePanel ID="UpdatePanel1" runat="Server">
            <ContentTemplate>      
          
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="INSTELLMENT_ID"
                    Width="100%" EnableCallBacks="false"  AutoGenerateColumns="false"   
                    OnRowInserting="gvMaster_RowInserting" 
                    OnRowUpdating="gvMaster_RowUpdating" 
                    OnPageIndexChanged="gvMaster_PageIndexChanged"
                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                    onfocusedrowchanged="gvMaster_FocusedRowChanged" 
                    OnInitNewRow="gvMaster_InitNewRow"
                    OnRowValidating ="gvMaster_RowValidating" 
                    onhtmlrowcreated="gvMaster_HtmlRowCreated" 
                    oncancelrowediting="gvMaster_CancelRowEditing" 
                    onstartrowediting="gvMaster_StartRowEditing">                 
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ShowSelectCheckbox="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <div style="text-align: center">
                                    <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1" Caption=" ">
                            <EditButton Visible="true"></EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn runat="server" FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="2" ReadOnly="true"
                         PropertiesTextEdit-Style-HorizontalAlign="Right">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STATUS" runat="server" Caption="<%$ Resources:WebResources, status %>" VisibleIndex="3">
                            <EditItemTemplate><dx:ASPxTextBox ID="txtSTATUS" runat="server" Border-BorderStyle="None" ReadOnly="true"></dx:ASPxTextBox></EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="BANK_ID" runat="server" Caption="發卡銀行" VisibleIndex="4">
                            <DataItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("BANK_NAME") %>'></asp:Label>
                            </DataItemTemplate>
                            <EditItemTemplate>
                                <dx:ASPxComboBox ID ="ddlBankID" runat ="server" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' >
                                   <ValidationSettings>
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </EditItemTemplate>
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="發卡銀行"></dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PAY_SEQMENT" runat="server" Caption="分期期數" VisibleIndex="5">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="分期期數"></dx:ASPxLabel>
                            </HeaderCaptionTemplate>                  
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtPAY_SEQMENT" runat="server" MaxLength="2" Width="40" 
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' HorizontalAlign="Right">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" />
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s, e){ CheckRate(s, e); }"  />
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SEQMENT_RATE" runat="server" Caption="分期利率" VisibleIndex="6">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="分期利率"></dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <CellStyle HorizontalAlign="Right"></CellStyle>
                            <DataItemTemplate>
                                <table>
                                    <tr  align="right">
                                        <td align="right"><dx:ASPxLabel ID="lblSEQMENT_RATE" runat="server" Text='<%# Bind("SEQMENT_RATE") %>'></dx:ASPxLabel></td>
                                        <td align="left">%</td>
                                    </tr>
                                </table>
                            </DataItemTemplate>                   
                            <EditItemTemplate>
                                <table>
                                    <tr align="right">
                                        <td align="right">
                                            <dx:ASPxTextBox ID="txtSEQMENT_RATE" runat="server" MaxLength="5" Width="100"  
                                                ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' HorizontalAlign="Right">
                                                <ValidationSettings>
                                                    <RegularExpression ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$" ErrorText="輸入字串不為數字格式或格式不正確，請重新輸入"/>
                                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e){ CheckRate(s, e); }"  />
                                                <ClientSideEvents   KeyDown="function(s,e) {CheckRateKeydown(s, e); }" />                                               
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td align="left">%</td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartDate %>" VisibleIndex="7">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="開始日期"></dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                 <dx:ASPxDateEdit ID="txtSDATE" runat="server" EditFormatString="yyyy/MM/dd" MinDate='<%# DateTime.Today.AddDays(0) %>'
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                    <ClientSideEvents ValueChanged="function(s, e){ CheckDate(s, e, '7_txtSDATE'); }"  />
                                 </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, Enddate %>" VisibleIndex="8">
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="txtEDATE" runat="server" EditFormatString="yyyy/MM/dd" MinDate='<%# DateTime.Today.AddDays(0) %>'>
                                    <ClientSideEvents ValueChanged="function(s, e){ CheckDate(s, e, '8_txtEDATE'); }"  />
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_USER_NAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddM" runat="server" Text="<%$ Resources:WebResources, add %>"
                                            UseSubmitBehavior="false" OnClick="btnAddM_Click" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDeleteM" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                                            UseSubmitBehavior="false"  OnClick="btnDeleteM_Click" />
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="false" />
                    <SettingsPager PageSize="5"></SettingsPager>
                    <Settings ShowTitlePanel="True"></Settings>
                    <ClientSideEvents FocusedRowChanged="function(s,e) { gvMasterFocusedRowChanged(s,e); }" />
                </cc:ASPxGridView>

                <div class="seperate"></div>

                <div style="text-align: left;">
                    <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" 
                        Width="40%" KeyFieldName="SETTLEMENT_ID" Visible="false" EnableCallBacks="false" 
                        OnPageIndexChanged="gvDetail_PageIndexChanged"
                        OnRowUpdating="gvDetail_RowUpdating" 
                        OnRowInserting="gvDetail_RowInserting" 
                        onrowvalidating="gvDetail_RowValidating" 
                        onhtmlrowcreated="gvDetail_HtmlRowCreated" 
                        onstartrowediting="gvDetail_StartRowEditing" 
                        oninitnewrow="gvDetail_InitNewRow">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="10%">
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <input type="checkbox" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1" Caption=" ">
                                <EditButton Visible="true"></EditButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn runat="server" FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="2" ReadOnly="true" >
                                <PropertiesTextEdit>
                                    <ReadOnlyStyle>
                                        <Border BorderStyle="None" />
                                    </ReadOnlyStyle>
                                </PropertiesTextEdit>
                                <DataItemTemplate>
                                    <%#Container.ItemIndex + 1%>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="COST_CENTER_NO" runat="server" Caption="<%$ Resources:WebResources, CostCenter %>" VisibleIndex="4">
                                <EditItemTemplate>
                                    <%--<dx:ASPxComboBox ID="ddlCostCenterNo" runat="server" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' >
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>--%>
                                    <dx:ASPxComboBox ID="ddlCostCenterNo" runat="server" Width="170px" DropDownWidth="100"
                                        DropDownStyle="DropDownList" TextField="COST_CENTER_NO" ValueField="COST_CENTER_NO" ValueType="System.String"
                                        TextFormatString="{0}" IncrementalFilteringMode="StartsWith" CallbackPageSize="30" MaxLength="5"
                                        ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </EditItemTemplate>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="SETTLEMENT_RATE" runat="server" Caption="<%$ Resources:WebResources, CostCenterSplitOffRatio %>" VisibleIndex="5">
                                <DataItemTemplate>
                                    <table>
                                        <tr>
                                            <td align="right"><dx:ASPxLabel ID="lblSETTLEMENT_RATE" runat="server" Text='<%# Bind("SETTLEMENT_RATE") %>' Width="100"></dx:ASPxLabel></td>
                                            <td align="left">%</td>
                                        </tr>
                                    </table>
                                </DataItemTemplate>                        
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td align="right">
                                                <dx:ASPxTextBox ID="txtSETTLEMENT_RATE" runat="server" Text='<%# Bind("SETTLEMENT_RATE") %>' Width="100" MaxLength="5" 
                                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' HorizontalAlign="Right">
                                                    <ValidationSettings>
                                                        <RegularExpression ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$" ErrorText="輸入字串不為數字格式或格式不正確，請重新輸入"/>
                                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                    </ValidationSettings>
                                                    <ClientSideEvents Validation="function(s, e){ CheckRate(s, e); }"  />
                                                     <ClientSideEvents   KeyDown="function(s,e) {CheckRateKeydown(s, e); }" /> 
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td align="left">%</td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="COST_CENTER_NO" runat="server" Visible="false"></dx:GridViewDataComboBoxColumn>
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnAddD" runat="server" Text="<%$ Resources:WebResources, add %>"
                                                UseSubmitBehavior="false" OnClick="btnAddD_Click">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnDeleteD" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, delete %>"
                                                UseSubmitBehavior="false" CausesValidation="false" OnClick="btnDeleteD_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <SettingsEditing Mode="Inline" />
                        <Settings ShowTitlePanel="true" />
                        <SettingsPager PageSize="5"></SettingsPager>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    </cc:ASPxGridView>
                </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>

</asp:Content>
