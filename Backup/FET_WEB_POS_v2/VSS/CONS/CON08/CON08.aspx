<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON08.aspx.cs" Inherits="VSS_CONS_CON08" ValidateRequest="false" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function DisabledAndRunButton(s, e) {
            var file = document.getElementById("ctl00_MainContentPlaceHolder_FileUpload").value
            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file))) {
                alert("匯入的檔案不正確,須為Excel檔!!");
                e.processOnServer = false;
            }
            else {
                if (s.GetEnabled()) {
                    s.SendPostBack('Click');
                    s.SetEnabled(false);
                }
            }
        }

        _s = null;
        _fName = null;
        _lName = null;
        function changeSupp(s, e) {
            _s = s;
            _fName = '0_PopupControl1_txtControl';
            _lName = "1_txtSuppName";

            if (s.GetText() != '') {
                PageMethods.getSUPPINFO(_s.GetText(), ReturnOK);
            }
        }

        _s = null;
        _fName = null;
        _lName = null;
        function changeStore(s, e) {
            _s = s;
            _fName = '2_PopupControl1_txtControl';
            _lName = "3_txtStoreName";
            if (s.GetText() != '') {
                PageMethods.getStoreInfo(_s.GetText(), ReturnOK);
            }
        }

        _s = null;
        _fName = null;
        _lName = null;
        function changePROD(s, e) {
            _s = s;
            _fName = '4_PopupControl1_txtControl';
            _lName = "5_txtProdName";

            if (s.GetText() != '') {
                PageMethods.getPRODINFO(_s.GetText(), ReturnOK);
            }
        }



        function ReturnOK(returnValue) {
            var fName = _fName;
            var txtSName = getClientInstance('TxtBox', _s.name.replace(fName, _lName));
            if (returnValue != '') {
                txtSName.SetText(returnValue);
            } else {
                txtSName.SetText('');
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品主配作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductDistribution %>"></asp:Literal>
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table id="Tab1">
            <tr>
                <td class="tdval">
                    <!--匯入檔案名稱-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ImportFilePath %>"></asp:Literal>：
                </td>
                <td colspan="5">
                    <asp:FileUpload ID="FileUpload" runat="server" Width="60%" />
                </td>
            </tr>
        </table>
    </div>
    <div class="btnPosition">
        <table align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>"
                        OnClick="btnImport_Click">
                        <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s, e);}" />
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                        SkinID="ResetButton">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" KeyFieldName="PRODNO" ClientInstanceName="gvMaster"
        runat="server" Width="100%" EnableCallBacks="false" OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared" OnHtmlRowCreated ="gvMaster_HtmlRowCreated"
        OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataColumn FieldName="SUPPNO" runat="server"  Width="100px" Caption="<%$ Resources:WebResources, SupplierNo %>">
                <DataItemTemplate>
                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ConsignmentVendorsPopup"
                        KeyFieldValue1="ConsignmentSale" IsValidation="true" KeyFieldValue2="SUPP_NO"  Width="100px"
                        Text='<%# Bind("SUPPNO") %>' OnClientTextChanged="function(s,e) { changeSupp(s, e); }" />
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="SUPPNAME" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>" Width="150px">
                <DataItemTemplate>
                    <dx:ASPxTextBox ID="txtSuppName" runat="server" ReadOnly="true" Text='<%# Bind("SUPPNAME") %>' Width="150px"
                        Border-BorderStyle="None">
                    </dx:ASPxTextBox>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="STORENO" runat="server" Caption="<%$ Resources:WebResources, StoreNo %>" Width="90px">
                <DataItemTemplate>
                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" Width="90px"
                        IsValidation="true" Text='<%# Bind("STORENO") %>' OnClientTextChanged="function(s,e) { changeStore(s, e); }" />
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="STORENAME" runat="server" Caption="<%$ Resources:WebResources, StoreName %>" Width="150px">
                <DataItemTemplate>
                    <dx:ASPxTextBox ID="txtStoreName" runat="server" ReadOnly="true" Text='<%# Bind("STORENAME") %>' Width="150px"
                        Border-BorderStyle="None">
                    </dx:ASPxTextBox>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" Width="120px">
                <DataItemTemplate>
                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" Width="120px"
                        IsValidation="true" KeyFieldValue1="ConsignmentSale" Text='<%# Bind("PRODNO") %>'
                        OnClientTextChanged="function(s,e) { changePROD(s, e); }" />
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>" Width="150px">
                <DataItemTemplate>
                    <dx:ASPxTextBox ID="txtProdName" runat="server" ReadOnly="true" Text='<%# Bind("PRODNAME") %>' Width="150px"
                        Border-BorderStyle="None">
                    </dx:ASPxTextBox>
                    <dx:ASPxLabel ID="txtUUID" runat="server" Text='<%# Bind("UUID") %>' Enabled="false"
                        DisabledStyle-Font-Underline="true" Visible="false">
                    </dx:ASPxLabel>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="DIS_QTY" Caption="<%$ Resources:WebResources, ActualOrderQuantity %>" Width="70px">
                <DataItemTemplate>
                    <dx:ASPxTextBox ID="DIS_QTY" runat="server" Text='<%# Bind("DIS_QTY") %>' Width="70px">
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="^\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                            <RequiredField IsRequired="True" ErrorText="必填欄位" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataTextColumn FieldName="ERR_DESC" runat="server" Caption="<%$ Resources:WebResources, ErrorDescription %>" Width="150px">
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table align="center">
            <tr>
                <td>
                    <dx:ASPxButton Enabled="false" ID="btnCommitUpload" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>"
                        OnClick="btnCommitUpload_Click">
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="btnCalcel" runat="server" AutoPostBack="False" Text="<%$ Resources:WebResources, Cancel %>" SkinID="ResetButton">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
