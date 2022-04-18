<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="TSAL031.aspx.cs" Inherits="VSS_SAL_TSAL031" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        //�P�_�ӫ~�Ƹ��O�_�s�b
        function getProdInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getProdInfo(_gvSender.GetText(), getProdInfo_OnOK);
        }
        
        function getProdInfo_OnOK(returnData) {
            if (returnData == 0) {
                alert('�ӫ~�Ƹ����s�b!!');
                _gvSender.SetValue(null);
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>       
        <div class="titlef">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <!--���f�@�~-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, GoodsExchanging %>" />(��)
                    </td>
                </tr>
            </table>
        </div>
         
        <div class="criteria"> 
            <table>
                <tr>
                    <td class="tdtxt">
                        �����s���G
                    </td>
                    <td class="tdval" width="80px">
                        <uc1:PopupControl ID="pcSTORENO" runat="server" ClientInstanceName="pcSTORENO" PopupControlName="StoresPopup" />
                    </td>
                    <td class="tdtxt">
                        <!--������-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TradeDate %>" />�G
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtS_Date" runat="server">
                                        <ValidationSettings CausesValidation="false" ErrorText="�п�J���">
                                            <RequiredField IsRequired="true" ErrorText="�������" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtE_Date" runat="server">
                                        <ValidationSettings CausesValidation="false" ErrorText="�п�J���">
                                            <RequiredField IsRequired="true" ErrorText="�������" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--���x-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CashRegister %>" />�G
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtMACHINE_ID" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="2" >
                          
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--�Ȥ����-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />�G
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtMSISDN" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="10">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--���A-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />�G
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="cbSTATUS" runat="server" ValueType="System.String" AutoResizeWithContainer="true">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value=""  />
                                <%--<dx:ListEditItem Text="�����b" Value="1" />--%>
                                <dx:ListEditItem Text="�w���b" Value="2" Selected="true"/>
                                <dx:ListEditItem Text="�w�@�o" Value="3" />
                                <%--<dx:ListEditItem Text="���@�o" Value="4" />--%>
                                <%--<dx:ListEditItem Text="���f�@�o" Value="5" />--%>
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--����Ǹ�-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />�G
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtSALE_NO" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="20">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--�P�P�N�X-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />�G
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="txtPROMOTION_CODE" runat="server" PopupControlName="PromotionsPopupOnly" KeyFieldValue1="NoDeadline" />
                    </td>
                    <td class="tdtxt">
                        <!--�P��H��-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>" />�G
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="cbSALE_PERSON" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                            SelectedIndex="0">
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--�o�����X-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>" />�G
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtInv_No" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="10">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--�ӫ~�s��-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />�G
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="pcPRODNO" runat="server" PopupControlName="ProductsPopup" SetClientValidationEvent="getProdInfo" />
                    </td>
                    <td class="tdtxt">
                        <!--�I�ڤ覡-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>" />�G
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="cbPAY_METHOD" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                            SelectedIndex="0">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="" />
                                <dx:ListEditItem Text="�{��" Value="1" />
                                <dx:ListEditItem Text="�H�Υd" Value="2" />
                                <%--<dx:ListEditItem Text="���u�H�Υd" Value="3" />--%>
                                <%--<dx:ListEditItem Text="�����I��" Value="4" />--%>
                                <dx:ListEditItem Text="§��" Value="5" />
                                <dx:ListEditItem Text="���ĥd" Value="6" />
                                <dx:ListEditItem Text="Happy GO" Value="7" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton" />
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="POSUUID_MASTER"
                        AutoGenerateColumns="False" Width="100%" 
                        OnPageIndexChanged="gvMaster_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                                <DataItemTemplate>
                                    <input type="radio" name="radioButton" />
                                </DataItemTemplate>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataColumn FieldName="ITEMS" Caption="<%$ Resources:WebResources, Items %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="SALE_STATUS_NAME" Caption="<%$ Resources:WebResources, Status %>"
                                VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="TRADE_DATE" Caption="<%$ Resources:WebResources, TradeDate %>"
                                VisibleIndex="3" />
                            <dx:GridViewDataColumn FieldName="SALE_NO" Caption="<%$ Resources:WebResources, TransactionNo %>"
                                VisibleIndex="4" />
                            <dx:GridViewDataColumn FieldName="MACHINE_ID" Caption="<%$ Resources:WebResources, CashRegister %>"
                                VisibleIndex="5" />
                            <dx:GridViewDataColumn FieldName="MSISDN" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                VisibleIndex="6" />
                            <dx:GridViewDataColumn FieldName="INVOICE_NO" Caption="<%$ Resources:WebResources, InvoiceNo %>"
                                VisibleIndex="7" />
                            <dx:GridViewDataColumn FieldName="SALE_TOTAL_AMOUNT" Caption="<%$ Resources:WebResources, Amount %>"
                                VisibleIndex="8" />
                            <dx:GridViewDataColumn FieldName="PAY_METHOD" Caption="<%$ Resources:WebResources, PaymentMethod %>"
                                VisibleIndex="9" />
                            <dx:GridViewDataColumn FieldName="SALE_PERSON_NAME" Caption="<%$ Resources:WebResources, SalesClerk %>"
                                VisibleIndex="10" />
                            <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                VisibleIndex="11" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                        <ClientSideEvents
                            FocusedRowChanged="function(s, e) {
                               if(s.GetFocusedRowIndex() == -1)
                                    return;
                               var row = s.GetRow(s.GetFocusedRowIndex());
                               
                                if(__aspxIE)
                                    row.cells[0].childNodes[0].checked = true;
                                else
                                    row.cells[0].childNodes[1].checked = true;
                            }" />  
                        <SettingsPager PageSize="10"></SettingsPager>
                        <SettingsBehavior AllowFocusedRow="true"  />
                    </cc:ASPxGridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <div class="seperate"></div>
        
        <div class="btnPosition" id="showFooter" runat="server" visible="true">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnChangeProd" runat="server" Text="���f���Ӭd��" 
                            AutoPostBack="true" onclick="btnChangeProd_Click">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
