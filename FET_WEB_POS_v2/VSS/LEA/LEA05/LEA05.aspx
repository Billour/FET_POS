<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LEA05.aspx.cs" Inherits="VSS_LEA_LEA05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        function chkgvList(s, e) {
            var AMT = 0;
            var AMT_CHAR = "";
            for (var i = 0; i < gvList.pageRowCount; i++) {
                if (gvList.GetDataRow(i).children[0].childNodes[0].status == true) {
                    if (gvList.GetDataRow(i).children[1].childNodes[0].nodeValue != "") {
                        AMT = AMT + Number(gvList.GetDataRow(i).children[2].childNodes[0].nodeValue);
                        AMT_CHAR = AMT_CHAR + gvList.GetDataRow(i).children[1].childNodes[0].nodeValue + "^" + gvList.GetDataRow(i).children[2].childNodes[0].nodeValue + "#"
                    }
                }
            }
            lblIND_AMT.SetValue(AMT);
            lblIND_AMT1.SetValue(AMT_CHAR);
        }

        function checkData(s, e) {
            //debugger;
            if (isNaN(lblEARNEST_AMT.GetValue())) {
                if (Number(lblEARNEST_AMT.GetValue()) > 9999999) {
                    alert('應收保證金輸入錯誤!!');
                    e.processOnServer = false;
                    return false;
                }
            }
            if (isNaN(lblRENT_AMT.GetValue())) {
                if (Number(lblRENT_AMT.GetValue()) > 9999999) {
                    alert('應付租金輸入錯誤!!');
                    e.processOnServer = false;
                    return false;
                }
            }
            if (isNaN(lblIND_AMT.GetValue())) {
                if (Number(lblIND_AMT.GetValue()) > 9999999) {
                    alert('賠償金額輸入錯誤!!');
                    e.processOnServer = false;
                    return false;
                }
            }
        }
        
        function checkDate1(s, e) {
            //debugger;
            if (txtPRE_E_DATE.GetValue() != null) {
                var DateStamp = new Date();
                var CurrentDate = DateStamp.getFullYear() + "/" + (DateStamp.getMonth() + 1) + "/" + DateStamp.getDate();  // 透過 JavaScript 產生今天日期
                var EDate = txtPRE_E_DATE.GetValue();  // 租賃時間(迄)
                if (((Date.parse(EDate)).valueOf()) < (Date.parse(CurrentDate)).valueOf()) {
                    alert('租賃時間(迄)輸入錯誤!!');
                    e.processOnServer = false;
                    return false;
                }

                if (txtPRE_S_DATE.GetValue() != null) {
                    var SDate = txtPRE_S_DATE.GetValue();  // 租賃時間(起)
                    if (((Date.parse(EDate)).valueOf()) < (Date.parse(SDate)).valueOf()) {
                        alert('租賃時間(迄)輸入錯誤!!');
                        e.processOnServer = false;
                        return false;
                    }
                } else {
                    alert('租賃時間(起)輸入錯誤!!');
                    e.processOnServer = false;
                    return false;
                }
            }
        }
        
        function checkDate2(s, e) {
            //debugger;
            if (txtREAL_RETURN_DTM.GetValue() != null) {
                var DateStamp = new Date();
                var CurrentDate = DateStamp.getFullYear() + "/" + (DateStamp.getMonth() + 1) + "/" + DateStamp.getDate();  // 透過 JavaScript 產生今天日期
                var EDate = txtREAL_RETURN_DTM.GetValue();  // 租賃時間(迄)
                if (((Date.parse(EDate)).valueOf()) < (Date.parse(CurrentDate)).valueOf()) {
                    alert('實際歸還日輸入錯誤!!');
                    e.processOnServer = false;
                    return false;
                }

                if (txtREAL_RECEV_DATE.GetValue() != null) {
                    var SDate = txtREAL_RECEV_DATE.GetValue();  // 租賃時間(起)
                    if (((Date.parse(EDate)).valueOf()) < (Date.parse(SDate)).valueOf()) {
                        alert('實際歸還日輸入錯誤!!');
                        e.processOnServer = false;
                        return false;
                    }
                } else {
                    alert('實際領取日輸入錯誤!!');
                    e.processOnServer = false;
                    return false;
                }
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
        <!--設備租賃作業-->
        <asp:Literal ID="Literal11" runat="server" Text="設備租賃作業"></asp:Literal>
    </div>
    
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                </td>
                <td class="tdval">
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
                    <!--租賃單號-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, LeaseOrderNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Literal ID="lblRENT_SHEET_NO" runat="server" Text=""></asp:Literal>
                </td>
                <td class="tdtxt">
                    <!--預約日期-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ReservationDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Literal ID="lblBOOKING_DATE" runat="server" Text=""></asp:Literal>
                </td>
                <td class="tdtxt">
                    <!--設定狀態-->
                    <asp:Literal ID="Literal19" runat="server" Text="設定狀態"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Literal ID="lblSTUTS" runat="server" Text=""></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--手機類型-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, MobileType %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Literal ID="lblDEVICE_TYPE" runat="server" Text=""></asp:Literal>
                </td>
                <td class="tdtxt">
                    <!--手機序號-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, MobileIdentityNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="lblIMEI" runat="server" Text="">
                    </dx:ASPxLabel>
                </td>
                <td class="tdtxt">
                    <!--更新日期-->
                    <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="txtUpdateTime" runat="server" Text="">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--庫存地點-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="lblSTORE_NO" runat="server" Text="" />
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
                <td class="tdtxt">
                    <!--維護人員-->
                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="lblMOUSER" runat="server" Text="">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--客戶門號-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtMSISDN" runat="server" MaxLength="10">
                        <ValidationSettings>
                            <RegularExpression ErrorText="客戶門號輸入錯誤" ValidationExpression="^[0-9]{10}" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--客戶姓名-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtCUST_NAME" runat="server" MaxLength="10">
                        <ValidationSettings>
                            <RequiredField IsRequired="True" ErrorText="客戶姓名不可為空值" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--客戶等級-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, CustomerGrade %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtCUST_LEVEL" runat="server" MaxLength="1">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--性別-->
                    <%--<asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Gender %>"></asp:Literal>：--%>
                </td>
                <td class="tdval">
                    <%--  <dx:ASPxRadioButtonList ID="txtSEX" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <Items>
                            <dx:ListEditItem Value="M" Text="男" Selected="true" />
                            <dx:ListEditItem Value="F" Text="女" />
                        </Items>
                    </dx:ASPxRadioButtonList>--%>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                </td>
                <td class="tdval">
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
                    <!--租賃時間-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, RentTime %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="txtPRE_S_DATE" ClientInstanceName="txtPRE_S_DATE" runat="server"
                                EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ 
                                        txtREAL_RECEV_DATE.SetValue(s.GetValue());
                                        lblEARNEST_AMT.SetValue(lblEARNEST_AMT1.GetValue());
                                }" />
                                </dx:ASPxDateEdit>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="txtPRE_E_DATE" ClientInstanceName="txtPRE_E_DATE" runat="server"
                                EditFormatString="yyyy/MM/dd">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                    <%--<dx:ASPxDateEdit ID="txtPRE_S_DATE" runat="server">
                    </dx:ASPxDateEdit>--%>
                </td>
                <td class="tdtxt">
                    <%--  <!--預定歸還日-->
                    <asp:Literal ID="Literal16" runat="server" Text="預定歸還日"></asp:Literal>：--%>
                </td>
                <td class="tdval">
                    <%--  <dx:ASPxDateEdit ID="txtPRE_E_DATE" runat="server">
                    </dx:ASPxDateEdit>--%>
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
                    <!--領取方式-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CollectionMethod %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxRadioButtonList ID="txtRECEIVE_TYPE" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <Items>
                            <dx:ListEditItem Value="1" Text="親至門市" Selected="True" />
                            <dx:ListEditItem Value="2" Text="快遞送貨" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td class="tdval">
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--地址-->
                    <%--<asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Address %>"></asp:Literal>
                    ：--%>
                </td>
                <td class="tdval" colspan="3">
                    <%-- <dx:ASPxTextBox ID="txtCUST_ADDR" runat="server" Width="98%">
                    </dx:ASPxTextBox>--%>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--出國時間-->
                    <%-- <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, DurationAbroad %>"></asp:Literal>：--%>
                </td>
                <td class="tdval" colspan="1">
                    <%--<table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="txtDEPARTURE_DTM" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="txtARRIVAL_DTM" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>--%>
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
                    <!--實際領取日-->
                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ActualCollectionDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxDateEdit ID="txtREAL_RECEV_DATE" ClientInstanceName="txtREAL_RECEV_DATE"
                        runat="server"  EditFormatString="yyyy/MM/dd">
                        
                        <ClientSideEvents ValueChanged="function(s, e){ 
                                if (txtREAL_RECEV_DATE.GetValue() != null && txtREAL_RETURN_DTM.GetValue() != null) {
                                    var date1 = Date.parse(txtREAL_RECEV_DATE.GetValue());
                                    var date2 = Date.parse(txtREAL_RETURN_DTM.GetValue());
                                    lblRENT_AMT.SetValue((Math.ceil((date2 - date1) / (24 * 60 * 60 * 1000))+1)*Math.ceil(lblRENT_AMT1.GetValue()));
                                }
                                if (txtREAL_RECEV_DATE.GetValue()!=null)
                                {
                                    lblEARNEST_AMT.SetValue(lblEARNEST_AMT1.GetValue());
                                }else
                                {
                                    lblEARNEST_AMT.SetValue('');
                                    lblRENT_AMT.SetValue('');
                                }                                        
                                }" />
                    </dx:ASPxDateEdit>
                </td>
                <td class="tdtxt">
                    <!--應收保證金：-->
                    <asp:Literal ID="Literal25" runat="server" Text="應收保證金"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="lblEARNEST_AMT" ClientInstanceName="lblEARNEST_AMT" runat="server"
                        ReadOnly="true" Border-BorderStyle="None" Text="" />
                    <dx:ASPxLabel ID="lblEARNEST_AMT1" ClientInstanceName="lblEARNEST_AMT1" runat="server"
                        Text="" ClientVisible="false" />
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--實際歸還日-->
                    <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, ActualReturnDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxDateEdit ID="txtREAL_RETURN_DTM" ClientInstanceName="txtREAL_RETURN_DTM"
                        runat="server">
                        <ClientSideEvents ValueChanged="function(s, e){ 
                            if (txtREAL_RECEV_DATE.GetValue() != null && txtREAL_RETURN_DTM.GetValue() != null) {
                                var date1 = Date.parse(txtREAL_RECEV_DATE.GetValue());
                                var date2 = Date.parse(txtREAL_RETURN_DTM.GetValue());
                                lblRENT_AMT.SetValue((Math.ceil((date2 - date1) / (24 * 60 * 60 * 1000))+1)*Math.ceil(lblRENT_AMT1.GetValue()));
                            }
                            if (s.GetValue() != null) {
                                txtIS_IND_FLAG.SetEnabled(true);
                            } else {
                                txtIS_IND_FLAG.SetEnabled(false);
                                txtIS_IND_FLAG.SetValue('0');
                                gvList.SetVisible(false);
                                btnOK.SetVisible(false);
                                lblRENT_AMT.SetValue('');
                            }
                                }" />
                    </dx:ASPxDateEdit>
                </td>
                <td class="tdtxt">
                    <!--應付租金-->
                    <asp:Literal ID="Literal22" runat="server" Text="應付租金"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="lblRENT_AMT" ClientInstanceName="lblRENT_AMT" runat="server"
                        Text="" ReadOnly="true" Border-BorderStyle="None" />
                    <dx:ASPxLabel ID="lblRENT_AMT1" ClientInstanceName="lblRENT_AMT1" runat="server"
                        Text="" ClientVisible="false" />
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
                    <!--是否有賠償-->
                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, CompensationRequired %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxRadioButtonList ID="txtIS_IND_FLAG" ClientInstanceName="txtIS_IND_FLAG" runat="server"
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <Items>
                            <dx:ListEditItem Text="否" Value="0" Selected="true"></dx:ListEditItem>
                            <dx:ListEditItem Text="是" Value="1"></dx:ListEditItem>
                        </Items>
                        <ClientSideEvents SelectedIndexChanged="function(s, e){ 
                            if (s.GetValue() != '0') {
                                gvList.SetVisible(true);
                                btnOK.SetVisible(true);
                            } else {
                                gvList.SetVisible(false);
                                btnOK.SetVisible(false);
                            }
                                }" />
                    </dx:ASPxRadioButtonList>
                </td>
                <td class="tdtxt">
                    <!--賠償金額-->
                    <asp:Literal ID="Literal26" runat="server" Text="賠償金額"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="lblIND_AMT" ClientInstanceName="lblIND_AMT" runat="server" Text="0"
                        ReadOnly="true" Border-BorderStyle="None" />
                    <dx:ASPxTextBox ID="lblIND_AMT1" ClientInstanceName="lblIND_AMT1" runat="server"
                        Text="0" ReadOnly="true" ClientVisible="false" />
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr id="showList" runat="server">
                <td colspan="6">
                    <table width="35%">
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="gvList" runat="server" Width="40%" AutoGenerateColumns="False"
                                    KeyFieldName="IND_ITEM_NAME" ClientVisible="false" ClientInstanceName="gvList">
                                    <Columns>
                                        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" VisibleIndex="0">
                                            <HeaderTemplate>
                                                <input type="checkbox" runat="server" onclick="gvList.SelectAllRowsOnPage(this.checked);"
                                                    title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataColumn FieldName="IND_ITEM_NAME" Caption="賠償項目" VisibleIndex="1" />
                                        <dx:GridViewDataColumn FieldName="IND_UNIT_PRICE" Caption="金額" VisibleIndex="2" />
                                    </Columns>
                                    <Templates>
                                        <EmptyDataRow>
                                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                                        </EmptyDataRow>
                                    </Templates>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <dx:ASPxButton ID="btnOK" ClientInstanceName="btnOK" Width="50px" runat="server"
                                    ClientVisible="false" Text="<%$ Resources:WebResources, OK %>" AutoPostBack="false"
                                    CausesValidation="false">
                                    <ClientSideEvents Click="function(s, e){ 
                                        chkgvList(s, e)
                                    }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, SaveReservation %>"
                        OnClick="btnSave_Click" Width="100px" Wrap="False">
                        <ClientSideEvents Click="function(s,e){ checkData(s,e); checkDate1(s,e); checkDate2(s,e); }" />
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnReserCancel" CausesValidation="false" runat="server" Text="<%$ Resources:WebResources, CancelReservation %>"
                        OnClick="btnReserCancel_Click" Width="100px" Wrap="False" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnCheck" Width="100px" OnClick="btnCheck_Click" runat="server"
                        Text="設備領取" Wrap="False">
                        <ClientSideEvents Click="function(s,e){ checkData(s,e); checkDate1(s,e); checkDate2(s,e); }" />
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnReturn" Width="100px" OnClick="btnReturn_Click" runat="server"
                        Text="設備歸還" Wrap="False">
                        <ClientSideEvents Click="function(s,e){ checkData(s,e); checkDate2(s,e); }" />
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnCancel" Width="100px" CausesValidation="false" runat="server"
                        Text="<%$ Resources:WebResources, Cancel %>" Wrap="False" 
                        onclick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <div class="GridScrollBar" style="height: auto">
        <asp:Panel ID="Panel2" runat="server" Visible="false">
         
                    <table class="mGrid" width="60%">
                        <tr>
                            <td align="center">
                                <!--修改記錄-->
                                <asp:Literal ID="Literal40" runat="server" Text="修改記錄"></asp:Literal>
                            </td>
                            <td colspan="3"></td>
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
                                <asp:Literal ID="ModiDtm" runat="server" Text=""></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="ModiEmpNo" runat="server" Text=""></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="ModiUser" runat="server" Text=""></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="ModiRecord" runat="server" Text=""></asp:Literal>
                            </td>
                        </tr>
                    </table>
              
        </asp:Panel>
    </div>
</asp:Content>
