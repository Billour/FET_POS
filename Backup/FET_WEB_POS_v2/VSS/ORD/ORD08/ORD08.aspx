<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ORD08.aspx.cs" Inherits="VSS_ORD_ORD08" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type= "text/javascript">
        _SenderM = null;
        _EventM = null;
        
        _SenderD = null;
        _EventD = null;

        _SenderW = null;
        _EventW = null;
       
        _MasterButtonClick = false;
        _DtailButtonClick = false;

        function getProductInfo(s, e) {
            _SenderM = s;
            _EventM = e;

            if (s.GetText() != '') {
                PageMethods.getProductInfo(_SenderM.GetText(), getProductInfo_OnOK);
            }
        }

        function getProductInfo_OnOK(returnData) {

            if (_SenderM.GetErrorText() == 'Invalid value') {
                var fName = "2_txtProd_txtControl";
                var txtProdNo = getClientInstance('TxtBox', _SenderM.name);
                var txtProdName = getClientInstance('TxtBox', _SenderM.name.replace(fName, "3_txtProdName"));
                var txtATR = getClientInstance('TxtBox', _SenderM.name.replace(fName, "4_txtATR"));

                var values = returnData.split(';');
                var PRODNAME = values[0];
                var ATRQTY = values[1];
                var ERROR = values[2];
                
                if (ERROR != '') {
                    txtProdNo.SetText(null);
                    if (txtProdName.GetInputElement() != null) txtProdName.SetText(null);
                    if (txtATR.GetInputElement() != null) txtATR.SetText("0");
                    _SenderM.errorText = ERROR;
                    _MasterButtonClick = false;
                    alert(ERROR);
                }
                else {
                    if (txtProdName.GetInputElement() != null) txtProdName.SetText(PRODNAME);
                    if (txtATR.GetInputElement() != null) txtATR.SetText(ATRQTY);
                    _SenderM.errorText = "";
                    if (_MasterButtonClick) {
                        _MasterButtonClick = false;
                        gvMaster.UpdateEdit();
                    }
                }
                
                onoffControl(fName, _SenderM.name, ATRQTY);
            }

        }

        function onoffControl(fName, objName, key) {
            var cbAutoFlag = getClientInstance('CheckBox', objName.replace(fName, "5_chkADF"));
            var txtDISQty = getClientInstance('TxtBox', objName.replace(fName, "6_txtDisQty"));
            var txtRemark = getClientInstance('TxtBox', objName.replace(fName, "7_txtREMARK"));

            if (key == "0") {
                cbAutoFlag.SetEnabled(false);
                txtDISQty.SetEnabled(false);
                txtRemark.SetEnabled(false);
            }
            else {
                cbAutoFlag.SetEnabled(true);
                if (cbAutoFlag.GetStateInput() != null) cbAutoFlag.SetChecked(false);
                txtDISQty.SetEnabled(false);
                txtRemark.SetEnabled(true);
            }
           
        }

        function getStoreInfo(s, e) {
            _SenderD = s;
            _EventD = e;
            if (s.GetText() != '') {
                PageMethods.getStoreInfo(_SenderD.GetText(), getStoreInfo_OnOK);
            }
        }

        function getStoreInfo_OnOK(returnData) {

            if (_SenderD.GetErrorText() == 'Invalid value')
            {
                var fName = "3_txtSTORE_NO_txtControl";
                var txtSTORE_NO = getClientInstance('TxtBox', _SenderD.name);
                var txtLOC_ID = getClientInstance('TxtBox', _SenderD.name.replace(fName, "2_txtLOC_ID"));
                var txtSTORENAME = getClientInstance('TxtBox', _SenderD.name.replace(fName, "4_txtSTORENAME"));
                var hdWEIGHT = getClientInstance('TxtBox', _SenderD.name.replace(fName, "3_hdWEIGHT"));
                //var cbAutoFlag = getClientInstance('CheckBox', _SenderD.name.replace(fName, "5_chkADF"));

                if (returnData != '')
                {
                    var values = returnData.split(';');
                    var iStore = values[0];
                    var iWeightCount = values[1];
                    var STORENAME = values[2];
                    var LOC_ID = values[3];
                    var WEIGHT = values[4];

                    if (iStore == "0")
                    {


                        if (txtSTORENAME.GetInputElement() != null) txtSTORENAME.SetText(STORENAME);
                        if (txtLOC_ID.GetInputElement() != null) txtLOC_ID.SetText(LOC_ID);
                        if (hdWEIGHT.GetInputElement() != null) hdWEIGHT.SetText(WEIGHT);
                        _SenderD.errorText = "";
                        if (_DtailButtonClick)
                        {
                            _DtailButtonClick = false;
                            gvDetail.UpdateEdit();
                        }


                    } else
                    {

                        if (txtSTORENAME.GetInputElement() != null) txtSTORENAME.SetText(null);
                        if (txtLOC_ID.GetInputElement() != null) txtLOC_ID.SetText(null);
                        if (hdWEIGHT.GetInputElement() != null) hdWEIGHT.SetText(0);
                        _SenderD.SetText(null);
                        _SenderD.errorText = "error";
                        _DtailButtonClick = false;
                        var strError;
                        //取得該門市的狀態：1 暫停營業, 2 已關閉
                        switch (iStore)
                        {
                            case "1":
                                strError = "此門市暫停營業!!";
                                break;
                            case "2":
                                strError = "此門市已關閉!!";
                                break;
                        }

                        alert(strError);
                    }
                }
            }


        }

        function checkWeightInfo(s, e) {
            _SenderW = s;
            _EventW = e;

            var fName = "5_chkADF";
            var cbAutoFlag = getClientInstance('CheckBox', _SenderW.name.replace(fName, "5_chkADF"));
            var txtDISQty = getClientInstance('TxtBox', _SenderW.name.replace(fName, "6_txtDisQty"));
            if (s.GetChecked()) {  //勾選【自動分配】
                if (typeof(gvDetail) == "undefined" || gvDetail.pageRowCount <= 0) {
                    alert('請先指定分配門市，再設定自動分配');
                    s.SetChecked(false);
                    e.processOnServer = false;
                }
                else {
                    PageMethods.checkWeightInfo(s.GetChecked(), checkWeightInfo_OnOK);
                }
            }
            else {
                txtDISQty.SetEnabled(false);
            }
            
        }

        function checkWeightInfo_OnOK(returnData) {
            var fName = "5_chkADF";
            
            var cbAutoFlag = getClientInstance('CheckBox', _SenderW.name.replace(fName, "5_chkADF"));
            var txtDISQty = getClientInstance('TxtBox', _SenderW.name.replace(fName, "6_txtDisQty"));

            if (_SenderW.GetChecked()) {  //勾選【自動分配】
                if (returnData) {
                    txtDISQty.SetEnabled(_SenderW.GetChecked());
                }
                else {
                    txtDISQty.SetEnabled(false);
                    _SenderW.SetChecked(false);
                    alert('請先完成權重佔比分配作業，才允許使用自動分配功能');
                }
                _EventW.processOnServer = false;
            }
            else {
                //txtDISQty.SetText("0");
                txtDISQty.SetEnabled(false);
                //_EventW.processOnServer = true;
            }
        }

        function CheckDIS_QTY(s, e) {
            var fName = "6_txtDisQty";
            var txtDISQty = s.GetText();
            var txtATR = getClientInstance('TxtBox', s.name.replace(fName, "4_txtATR"));
            var intATR = Number(txtATR.GetValue());

            var intDISQ = 0;

            if (txtDISQty != '') {
                intDISQ = Number(txtDISQty);
                if (isNaN(intDISQ)) {
                    e.isValid = false;
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    e.processOnServer = false;  
                    return false;
                }
                else if (intDISQ <= 0) {
                    e.isValid = false;
                    e.errorText = '主配量不允許小於等於0，請重新輸入';
                    e.processOnServer = false;  
                    return false;
                }
                else if (intATR - intDISQ < 0) {
                    e.isValid = false;
                    e.errorText = '主配量不得大於ATR量，請重新輸入';
                    e.processOnServer = false;  
                    return false;
                }
            }
            else {
                s.SetText("0");
            }

        }

        function CheckASSIGN_QTY(s, e) {
            var ASSIGN_QTY = s.GetText();

            var intASSIGN_QTY = 0;

            if (ASSIGN_QTY != '') {
                intASSIGN_QTY = Number(ASSIGN_QTY);
                if (isNaN(intASSIGN_QTY)) {
                    e.isValid = false;
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    e.processOnServer = false;  
                    return false;
                }
                else if (intASSIGN_QTY < 0) {
                    e.isValid = false;
                    e.errorText = '主動配貨量不允許小於0，請重新輸入';
                    e.processOnServer = false;  
                    return false;
                }
            }
            else {
                s.SetText("0");
            }

        }
        
        function onOK() {
            __doPostBack('<%= txtBatchNO.UniqueID %>', 'AAA');
        }


        function Import(s, e) {
            if (gvMaster.IsEditing())
                gvMaster.CancelEdit(); 

            var rtn = confirm('匯入資料後，會將原本畫面上的資料清除，是否要執行匯入動作?');
            if (rtn) {
                ORD08ImportPopup.Show();
            }
            else {
                ORD08ImportPopup.Hide();
            }

        }


        function checkProdNo(s, e) {

            _MasterButtonClick = true;
            var fName = "1_btnSave";

            var IsValidate = true;
            var cbAutoFlag = getClientInstance('CheckBox', s.name.replace(fName, "5_chkADF"));
            var txtDISQty = getClientInstance('TxtBox', s.name.replace(fName, "6_txtDisQty"));
            if (cbAutoFlag.GetChecked() && !txtDISQty.isValid) {  //勾選【自動分配】，才要檢查主配量合不合格
                IsValidate = false;
            }

            if (IsValidate) {
                if (_SenderM != null) {
                    if (_SenderM.GetErrorText() == 'Invalid value') {
                        var txtProdNo = getClientInstance('TxtBox', s.name.replace(fName, "2_txtProd_txtControl"));
                        txtProdNo.TextChanged.FireEvent(txtProdNo, null);

                        //_SenderM.TextChanged.FireEvent(_SenderM, null);
                        //_SenderM.OnValidation();
                        //_SenderM.Validation.FireEvent(_SenderM, _EventM);
                    }
                    else {
                        if (_SenderM.GetErrorText() == "") {
                            _MasterButtonClick = false;
                            gvMaster.UpdateEdit();
                        }
                    }
                }
                else {
                    var txtProdNo = getClientInstance('TxtBox', s.name.replace(fName, "2_txtProd_txtControl"));
                    txtProdNo.TextChanged.FireEvent(txtProdNo, null);
                }
            }

        }


        function checkStoreNo(s,e) {

            _DtailButtonClick = true;
            
            if (_SenderD != null) {
                if (_SenderD.GetErrorText() == 'Invalid value') {
                    var fName = "1_btnSave";
                    var txtStoreNo = getClientInstance('TxtBox', s.name.replace(fName, "3_txtSTORE_NO_txtControl"));
                    txtStoreNo.TextChanged.FireEvent(txtStoreNo, null);
                    
                    //_SenderD.TextChanged.FireEvent(_SenderD, null);
                    //_SenderD.OnValidation();
                    //_SenderD.Validation.FireEvent(_SenderD, _EventD);
                } else {
                    if (_SenderD.GetErrorText() == "") {
                        _DtailButtonClick = false;
                        gvDetail.UpdateEdit();
                    }
                }
            }
            else {
                var fName = "1_btnSave";
                var txtStoreNo = getClientInstance('TxtBox', s.name.replace(fName, "3_txtSTORE_NO_txtControl"));
                txtStoreNo.TextChanged.FireEvent(txtStoreNo, null);
            }
        }
        
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <input type="hidden" ID="txtBatchNO" runat="server" />

        <div class="titlef">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <!--Non-DropShipment主配作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, NonDropShipmentProductDistribution %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="btnQueryEdit" runat="server" UseSubmitBehavior="false" CausesValidation="false" 
                            Text="<%$ Resources:WebResources, QueryEdit %>" onclick="btnQueryEdit_Click">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div>
            <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
                <ContentTemplate>        
                    <div class="criteria">
                        <table>
                            <tr>
                                <td class="tdtxt">
                                    <!--主配單號-->
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DistributionNo %>"></asp:Literal>：
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="lblNDS_MID" runat="server" Text="" ReadOnly="true" Border-BorderStyle="None"> </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hdNDS_MID_UUID" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                                </td>
                                <td class="tdtxt"></td>
                                <td class="tdval"></td>
                                <td class="tdtxt">
                                    <!--狀態-->
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="lblNDS_Status" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt">&nbsp;</td>
                                <td class="tdval" colspan="3">&nbsp;</td>
                                <td class="tdtxt">
                                    <!--更新日期-->
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="lblUpDate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt"></td>
                                <td class="tdval"></td>
                                <td class="tdtxt"></td>
                                <td class="tdval"></td>
                                <td class="tdtxt">
                                    <!--更新人員-->
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="lblUpUser" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                    <div class="seperate"></div>
            
                    <div id="Div1" runat="server" class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="HQ_ORDER_D"
                            Width="99%" EnableCallBacks="false" 
                            OnPageIndexChanged="gvMaster_PageIndexChanged"
                            OnRowUpdating="gvMaster_RowUpdating" 
                            OnRowInserting="gvMaster_RowInserting" 
                            OnFocusedRowChanged="gvMaster_FocusedRowChanged"
                            OnRowValidating="gvMaster_RowValidating" 
                            OnInitNewRow="gvMaster_InitNewRow"
                            OnStartRowEditing="gvMaster_StartRowEditing" 
                            onhtmlrowprepared="gvMaster_HtmlRowPrepared" 
                            onhtmlrowcreated="gvMaster_HtmlRowCreated" 
                            onprerender="gvMaster_PreRender" 
                            oncommandbuttoninitialize="gvMaster_CommandButtonInitialize">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn VisibleIndex="1" Caption=" ">
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                    <DataItemTemplate>
                                        <%--<dx:ASPxButton ID="btnEdit" runat="server" Text="編輯" AutoPostBack="false" 
                                            UseSubmitBehavior="false" Native="True">
                                            <ClientSideEvents Click="function(s,e) { gvMaster.StartEditRow(Number(hdID.GetText())); }" />
                                         </dx:ASPxButton>  
                                         <dx:ASPxTextBox ID="hdID" runat="server" Text='<%# Container.VisibleIndex %>' ClientInstanceName="hdID" ClientVisible="false"></dx:ASPxTextBox>
                                         --%>    
                                         <input type="button" value="編輯" onclick="gvMaster.StartEditRow(<%# Container.VisibleIndex %>);"/>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnSave" runat="server" Text="儲存" AutoPostBack="false" 
                                                        UseSubmitBehavior="false" Native="True" ValidationSettings-ValidationGroup="gvMasterGroup">
                                                        <ClientSideEvents Click="function(s,e) { checkProdNo(s,e); }" />
                                                    </dx:ASPxButton>                                                
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnCancel" runat="server" Text="取消" AutoPostBack="false" 
                                                        UseSubmitBehavior="false" Native="True" ValidationSettings-ValidationGroup="gvMasterGroup">
                                                        <ClientSideEvents Click="function(s,e) { gvMaster.CancelEdit(); }" />
                                                    </dx:ASPxButton>                                                
                                                </td>
                                            </tr>
                                        </table>
                                         <%--<input type="button" value="儲存" onclick="checkProdNo();"/>
                                         <input type="button" value="取消" onclick="btnCancel_Click('gvMaster');"/>--%>
                                    </EditItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="2">
                                    <EditItemTemplate>
                                        <uc1:PopupControl ID="txtProd" runat="server" PopupControlName="ProductsPopup" Text='<%# BIND("PRODNO") %>'
                                            IsValidation="true" ValidationGroup="gvMasterGroup"
                                            OnClientTextChanged="function(s,e) { getProductInfo(s,e); }" />
                                    </EditItemTemplate>                                   
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="3">
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtProdName" runat="server" ReadOnly="true" Text='<%# Bind("PRODNAME") %>' >
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ATR_QTY" Caption="<%$ Resources:WebResources, AtrQuantity %>" VisibleIndex="4">
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtATR" runat="server" ReadOnly="true" Text='<%# Bind("ATR_QTY") %>'>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AUTO_DIS_FLAG" Caption="<%$ Resources:WebResources, AutomaticallyAssigned %>" VisibleIndex="5">
                                    <DataItemTemplate>
                                        <dx:ASPxCheckBox ID="lblchkADF" runat="server"></dx:ASPxCheckBox>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <dx:ASPxCheckBox ID="chkADF" runat="server" >
                                            <ClientSideEvents CheckedChanged="function(s,e) { checkWeightInfo(s,e); }" />
                                        </dx:ASPxCheckBox>
                                        <dx:ASPxTextBox ID="hdADF" runat="server" Text='<%# Bind("AUTO_DIS_FLAG") %>' ClientVisible="false"></dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DIS_QTY" Caption="<%$ Resources:WebResources, DistributionQuantity %>" VisibleIndex="6">
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="txtDisQty" runat="server" HorizontalAlign="Right" Text='<%# Bind("DIS_QTY") %>' ReadOnly="true" Border-BorderStyle="None" Width="50">
                                        </dx:ASPxTextBox>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtDisQty" runat="server" HorizontalAlign="Right" Text='<%# Bind("DIS_QTY") %>'
                                            ValidationSettings-ValidationGroup="gvMasterGroup" Width="50" MaxLength="9">
                                            <ValidationSettings>
                                                <RegularExpression ValidationExpression="\d*" />
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) { CheckDIS_QTY(s, e); }" />
                                        </dx:ASPxTextBox>
                                    </EditItemTemplate>                                                                       
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="REMARK" Caption="<%$ Resources:WebResources, Remark %>" VisibleIndex="7">
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtREMARK" runat="server" Text='<%# Bind("REMARK") %>' Width="100" MaxLength="50"></dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <table align="left">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnAddM" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                   UseSubmitBehavior="false" OnClick="btnAddM_Click">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnDeleteM" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                   UseSubmitBehavior="false" OnClick="btnDeleteM_Click" >
                                                   <ClientSideEvents Click="function(s,e) { e.processOnServer = confirm('確定刪除所選的項目?'); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) { Import(s, e); }" />
                                                </dx:ASPxButton>
                                                <dx:ASPxTextBox ID="txtBatchNO1" runat="server" Width="170px" ClientVisible="false"></dx:ASPxTextBox>
                                                <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server"
                                                    AllowDragging="True" AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/Common/NonDropShipmentExportPopup.aspx"
                                                    PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false" onOKScript="onOK"
                                                    Width="650px" Height="520px" TargetElementID="txtBatchNO1" PopupElementID="btnImport"
                                                    HeaderText="資料匯入" ClientInstanceName="ORD08ImportPopup" LoadingPanelID="lp">    
                                                </cc:ASPxPopupControl>
                                                <dx:ASPxLoadingPanel ID="lp" runat="server"></dx:ASPxLoadingPanel> 
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                            </Templates>
                            <SettingsEditing Mode="Inline" />
                            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
                            <SettingsPager PageSize="5" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            <Settings ShowTitlePanel="True" />
                        </cc:ASPxGridView>

                        <div class="seperate"></div>
                        
                        <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" Enabled="false"
                            KeyFieldName="HQ_ORDER_STORE" Width="99%"  EnableCallBacks="false"
                            OnRowInserting="gvDetail_RowInserting"
                            OnRowUpdating="gvDetail_RowUpdating" 
                            onpageindexchanged="gvDetail_PageIndexChanged" 
                            oninitnewrow="gvDetail_InitNewRow" 
                            onrowvalidating="gvDetail_RowValidating" 
                            onhtmlrowcreated="gvDetail_HtmlRowCreated" 
                            onprerender="gvDetail_PreRender" 
                            onstartrowediting="gvDetail_StartRowEditing" 
                            oncommandbuttoninitialize="gvDetail_CommandButtonInitialize">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn VisibleIndex="1" Caption=" ">
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                    <DataItemTemplate>
                                       <%-- <dx:ASPxButton ID="btnEdit" runat="server" Text="編輯" AutoPostBack="false" 
                                            UseSubmitBehavior="false" Native="True">
                                            <ClientSideEvents Click="function(s,e) { gvDetail.StartEditRow(Number(hdID.GetText())); }" />
                                         </dx:ASPxButton> 
                                         <dx:ASPxTextBox ID="hdID" runat="server" Text='<%# Container.VisibleIndex %>' ClientInstanceName="hdID" ClientVisible="false"></dx:ASPxTextBox>
                                         --%>
                                         <input type="button" value="編輯" onclick="gvDetail.StartEditRow(<%# Container.VisibleIndex %>);"/>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnSave" runat="server" Text="儲存" AutoPostBack="false" 
                                                        UseSubmitBehavior="false" Native="True" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                                        <ClientSideEvents Click="function(s,e) { checkStoreNo(s,e); }" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnCancel" runat="server" Text="取消" AutoPostBack="false" 
                                                        UseSubmitBehavior="false" Native="True" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                                        <ClientSideEvents Click="function(s,e) { gvDetail.CancelEdit(); }" />
                                                    </dx:ASPxButton>                                                
                                                </td>
                                            </tr>
                                        </table>
                                         <%--<input type="button" value="儲存" onclick="checkStoreNo();"/>
                                         <input type="button" value="取消" onclick="btnCancel_Click('gvDetail');"/>--%>
                                    </EditItemTemplate>                                   
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="LOC_ID" Caption="<%$ Resources:WebResources, ShipmentWarehouse %>">
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtLOC_ID" runat="server" Text='<%# BIND("LOC_ID") %>' ReadOnly="true" Border-BorderStyle="None" ></dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                                    <EditItemTemplate>
                                        <uc1:PopupControl ID="txtSTORE_NO" runat="server" PopupControlName="StoresPopup" Text='<%# BIND("STORE_NO") %>' 
                                            IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                            OnClientTextChanged="function(s,e) { getStoreInfo(s,e); }"  />
                                        <dx:ASPxTextBox ID="hdWEIGHT" runat="server" Text='<%# BIND("WEIGHT") %>' ClientVisible="false"></dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>">
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtSTORENAME" runat="server" Text='<%# BIND("STORENAME") %>'  ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ASSIGN_QTY" Caption="<%$ Resources:WebResources, DistributionQuantity %>">
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="lblASSIGN_QTY" runat="server" Text='<%# BIND("ASSIGN_QTY") %>' ReadOnly="true" Border-BorderStyle="None" ></dx:ASPxTextBox>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtASSIGN_QTY" runat="server" Text='<%# BIND("ASSIGN_QTY") %>' HorizontalAlign="Right" Width="50"  MaxLength="9"
                                            ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                            <ValidationSettings>
                                                <RegularExpression ValidationExpression="\d*" />
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) { CheckASSIGN_QTY(s,e); } " />
                                        </dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <table align="left">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnAddD" Enabled="true" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                    UseSubmitBehavior="false" CausesValidation="false" OnClick="btnAddD_Click">
                                                </dx:ASPxButton>
                                            </td>
                                            <%--區域別--%>
                                            <td>
                                                 <dx:ASPxComboBox ID="ddlZone" runat="server" Width="80px"></dx:ASPxComboBox>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnConfirm" runat="server" Text="<%$ Resources:WebResources, Confirm %>"
                                                    UseSubmitBehavior="false" CausesValidation="false" OnClick="btnConfirm_Click">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnDeleteD" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                    UseSubmitBehavior="false" CausesValidation="false" OnClick="btnDeleteD_Click">
                                                        <ClientSideEvents Click="function(s,e) { e.processOnServer = confirm('確定刪除所選的項目?'); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                            </Templates>
                            <Settings ShowTitlePanel="True" />
                            <SettingsPager PageSize="5"></SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </cc:ASPxGridView>

                    </div>
                    
                    <div class="seperate"></div>
                    
                    <div class="btnPosition" id="divShow" runat="server" visible="false">
                        <table cellpadding="0" cellspacing="0" border="0" align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnSave" runat="server" UseSubmitBehavior="false" 
                                        Text="<%$ Resources:WebResources, Save %>" onclick="btnSave_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" 
                                        AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                                            <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm); }" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnExport" runat="server" UseSubmitBehavior="false" 
                                        Text="<%$ Resources:WebResources, Export %>" onclick="btnExport_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="bntCommitUpload" runat="server"  UseSubmitBehavior="false"
                                        Text="<%$ Resources:WebResources, CommitUpload %>" 
                                        onclick="bntCommitUpload_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnDelete" SkinID="DeleteBtn" runat="server" UseSubmitBehavior="false" 
                                        Text="<%$ Resources:WebResources, Delete %>" onclick="btnDelete_Click">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
