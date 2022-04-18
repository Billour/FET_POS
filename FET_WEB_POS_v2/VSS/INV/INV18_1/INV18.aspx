<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV18.aspx.cs" Inherits="VSS_INV_INV18" ValidateRequest="false" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register src="../../../Controls/ExportExcelData.ascx" tagname="ExportExcelData" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">

        function chkDateEvent(s, e) {
            var x = txtSDate.GetText();
            var y = txtEDate.GetText();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != '' && y != '') {
                e.isValid = (x <= y);
                if (!e.isValid) {
                    s.SetText(null);
                    alert('調整日訖不允許小於調整日起，請重新輸入');
                    e.processOnServer = false;
                }
            }
        }


        function checkPRODEvent(s, e, fName) {

            var txtS_PRODNO = getClientInstance('TxtBox', s.name.replace(fName, "txtS_PRODNO"));
            var txtE_PRODNO = getClientInstance('TxtBox', s.name.replace(fName, "txtE_PRODNO"));
            
            var x = txtS_PRODNO.GetText();
            var y = txtE_PRODNO.GetText();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {
                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("商品料號訖不允許小於商品料號起，請重新輸入!!");
                    s.SetValue(null);
                    e.processOnServer = false;
                }
            }
       }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div>
        <div class="titlef">
            <!--庫存調整查詢作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentSerch %>"></asp:Literal>
        </div>

        <div style="text-align: left;">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap" >
                        <!--調整單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtADJNO" runat="server" Width="80px">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--調整日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>：
                    </td>
                    <td class="" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtS_DATE" runat="server" ClientInstanceName="txtSDate" >
                                        <ClientSideEvents ValueChanged="function(s,e) { chkDateEvent(s,e);}" />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtE_DATE" runat="server" ClientInstanceName="txtEDate">
                                        <ClientSideEvents ValueChanged="function(s,e) { chkDateEvent(s,e);}" />
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--調整門市名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AdjustmentStoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtSTOREName" runat="server" Width="80px">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品料號-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="txtS_PRODNO" runat="server" PopupControlName="ProductsPopup" OnClientTextChanged="function(s,e){ checkPRODEvent(s,e, 'txtS_PRODNO'); }" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                     <uc1:PopupControl ID="txtE_PRODNO" runat="server" PopupControlName="ProductsPopup" OnClientTextChanged="function(s,e){ checkPRODEvent(s,e, 'txtE_PRODNO'); }" />
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
                            OnClick="btnSearch_Click" >  
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
        
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="ADJNO"
                Width="100%" 
                OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                OnPageIndexChanged="gvMaster_PageIndexChanged"
                OnHtmlRowPrepared="gvMaster_HtmlRowPrepared">
                <Columns>
                     <dx:GridViewDataTextColumn FieldName="ADJNO" Caption="<%$ Resources:WebResources, StockAdjustmentNoteNo %>">
                    <DataItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("ADJNO") %>'></asp:HyperLink>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" />
                    <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>" />
                    <dx:GridViewDataColumn FieldName="ADJDATE" Caption="<%$ Resources:WebResources, AdjustmentDate %>" />
                    <dx:GridViewDataColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, Status %>" />
                    <dx:GridViewDataColumn FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                    <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                        AutoPostBack="false" CausesValidation="false" onclick="btnExport_Click">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsPager PageSize="5"></SettingsPager>
                <Settings ShowTitlePanel="true" />
            </cc:ASPxGridView>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
