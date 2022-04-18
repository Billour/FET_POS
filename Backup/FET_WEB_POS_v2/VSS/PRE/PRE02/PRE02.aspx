<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PRE02.aspx.cs" Inherits="VSS_PRE_PRE02" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register Src="../../../Controls/ExportExcelData.ascx" TagName="ExportExcelData"
    TagPrefix="uc3" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        //檢查預收款日期
        function checkDate(s, e)
        {
            var sd = deTradeDate_S.GetText(); //預收款日期(起)日
            var ed = deTradeDate_E.GetText(); //預收款日期(迄)日
            if (sd == '' || ed == '')
            {

            } else
            {
                if ((new Date(sd)) > (new Date(ed)))
                {
                    alert('【預收款日期(迄)日】不可小於【預收款日期(起)日】');
                    e.processOnServer = false;
                    return false;
                }
            }
        }
        //檢查資料是否為自己門市
        function RowChanged(s, e)
        {

            if (s.GetFocusedRowIndex() == -1)
                return;
            var row = s.GetRow(s.GetFocusedRowIndex());

            if (__aspxIE)
                row.cells[0].childNodes[0].checked = true;
            else
                row.cells[0].childNodes[1].checked = true;
            var fId = "1_9_txtSTORE_NO";
            fId = s.GetFocusedRowIndex() + "_9_txtSTORE_NO_I";
            var txtSTORENO = $('input[id$="' + fId + '"]').val();
            if (txtLogSTORE_NO.GetValue() != txtSTORENO && txtLogSTORE_NO.GetValue() != "HQ")
                btnView.SetEnabled(false);
            else
                btnView.SetEnabled(true);

        }
        function QryCheckedChanged(s, e)
        {
            //chkStoreQuery.GetValue();
            //pcPreNo.SetValue(''); //預收款單號

            if (chkStoreQuery.GetValue())
            {
                //txtUniNo.SetValue(''); //統一編號
                //txtCustId.SetValue(''); //客戶身份證號
                //txtPhone.SetValue(''); //聯絡電話

                txtCustName.SetValue(''); //客戶姓名
                comPreStatus.SetSelectedIndex("0"); //狀態   
                comSalePerson.SetSelectedIndex("0"); //銷售人員  
                deTradeDate_S.SetValue(null); //預購日期-起
                deTradeDate_E.SetValue(null); //預購日期-迄
                txtMsisdn.SetValue(''); //客戶門號
                txtInvoiceNo.SetValue(''); //發票號碼

                txtCustName.SetEnabled(false); //客戶姓名

                comPreStatus.SetEnabled(false); //狀態

                comSalePerson.SetEnabled(false); //銷售人員

                deTradeDate_S.SetEnabled(false); //預購日期-起

                deTradeDate_E.SetEnabled(false); //預購日期-迄

                txtMsisdn.SetEnabled(false); //客戶門號

                txtInvoiceNo.SetEnabled(false); //發票號碼

            }
            else
            {
                txtCustName.SetEnabled(true); //客戶姓名

                comPreStatus.SetEnabled(true); //狀態

                comSalePerson.SetEnabled(true); //銷售人員

                deTradeDate_S.SetEnabled(true); //預購日期-起

                deTradeDate_E.SetEnabled(true); //預購日期-迄

                txtMsisdn.SetEnabled(true); //客戶門號

                txtInvoiceNo.SetEnabled(true); //發票號碼
            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--預購查詢作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderSearch %>"></asp:Literal>
        <dx:ASPxTextBox ID="txtLogSTORE_NO" ClientInstanceName="txtLogSTORE_NO" runat="server"
            ClientVisible="false" />
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval" colspan="2">
                    <dx:ASPxCheckBox ID="chkStoreQuery" ClientInstanceName="chkStoreQuery" ClientSideEvents-CheckedChanged="function(s, e) {QryCheckedChanged(s, e);}"
                        Width="150px" runat="server" Text="跨門市查詢(限單筆)" ClientSideEvents-Init="function(s, e) {QryCheckedChanged(s, e);}">
                    </dx:ASPxCheckBox>
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
                    <!--門市代號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <uc1:PopupControl ID="pcSTORENO" runat="server" ClientInstanceName="pcSTORENO" PopupControlName="StoresPopup" />
                </td>
                <td class="tdtxt">
                    <!--客戶姓名-->
                    &nbsp;<asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtCustName" ClientInstanceName="txtCustName" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--狀態-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="comPreStatus" ClientInstanceName="comPreStatus" runat="server"
                        Width="100">
                        <Items>
                            <dx:ListEditItem Value="" Text="ALL" Selected="true" />
                            <dx:ListEditItem Value="1" Text="未存檔" />
                            <dx:ListEditItem Value="2" Text="已結帳" />
                            <dx:ListEditItem Value="3" Text="已作廢" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--預收款單號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, PreOrderSheetNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <%--      <dx:ASPxTextBox ID="pcPreNo" ClientInstanceName="pcPreNo" runat="server">
                    </dx:ASPxTextBox>--%>
                    <uc1:PopupControl ID="pcPreNo" ClientInstanceName="pcPreNo" runat="server" PopupControlName="OddNumberPopup" />
                </td>
                <td class="tdtxt">
                    <!--客戶身份證號-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, CustomerIdentifyNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtCustId" ClientInstanceName="txtCustId" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--銷售人員-->
                    &nbsp;<asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="comSalePerson" ClientInstanceName="comSalePerson" runat="server"
                        AutoResizeWithContainer="true" Width="100">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--預購日期-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PreOrderDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="deTradeDate_S" ClientInstanceName="deTradeDate_S" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="deTradeDate_E" ClientInstanceName="deTradeDate_E" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--客戶門號-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtMsisdn" ClientInstanceName="txtMsisdn" runat="server">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--統一編號-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtUniNo" ClientInstanceName="txtUniNo" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--聯絡電話-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtPhone" ClientInstanceName="txtPhone" runat="server">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--發票號碼-->
                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtInvoiceNo" ClientInstanceName="txtInvoiceNo" runat="server">
                    </dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="right">
                    <dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnQuery_Click">
                        <ClientSideEvents Click="function(s,e){checkDate(s,e);}" />
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>">
                    </dx:ASPxButton>
                </td>
                <td align="left">
                    <dx:ASPxButton ID="btnExportSubmit" ClientInstanceName="btnExportSubmit" runat="server"
                        Text="<%$ Resources:WebResources, Export %>" Height="28px" OnClick="btnExport_Click"
                        ClientVisible="false">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnPageIndexChanged="gvMaster_PageIndexChanged" KeyFieldName="PREPAY_NO">
                    <Columns>
                        <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                            <DataItemTemplate>
                                <input type="radio" name="radioButton" />
                            </DataItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn FieldName="ITEMS" Caption="<%$ Resources:WebResources, Items %>"
                            VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="PREPAY_STATUS_NAME" Caption="<%$ Resources:WebResources, Status %>"
                            VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="TRADE_DATE" Caption="<%$ Resources:WebResources, PreOrderDate %>"
                            VisibleIndex="3" />
                        <dx:GridViewDataColumn FieldName="PREPAY_NO" Caption="<%$ Resources:WebResources, PreOrderSheetNo %>"
                            VisibleIndex="4" />
                        <dx:GridViewDataColumn FieldName="CUST_NAME" Caption="<%$ Resources:WebResources, CustomerName %>"
                            VisibleIndex="5" />
                        <dx:GridViewDataColumn FieldName="MSISDN" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                            VisibleIndex="6" />
                        <dx:GridViewDataColumn FieldName="CONTACT_PHONE" Caption="<%$ Resources:WebResources, ContactTelephone %>"
                            VisibleIndex="7" />
                        <dx:GridViewDataColumn FieldName="AR_AMOUNT" Caption="<%$ Resources:WebResources, PreOrderAmount %>"
                            VisibleIndex="8" />
                        <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>"
                            VisibleIndex="9">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblSTORE_NO" runat="server" Text='<%# Bind("STORE_NO") %>' />
                                <dx:ASPxTextBox ID="txtSTORE_NO" runat="server" ClientVisible="false" Text='<%#BIND("STORE_NO")  %>'
                                    Border-BorderStyle="None" ReadOnly="true">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn FieldName="STORE_NAME" Caption="<%$ Resources:WebResources, StoreName %>"
                            VisibleIndex="10" />
                        <dx:GridViewDataColumn FieldName="SALE_PERSON_NAME" Caption="<%$ Resources:WebResources, SalesClerk %>"
                            VisibleIndex="11">
                        </dx:GridViewDataColumn>
                    </Columns>
                    <Templates>
                        <EmptyDataRow>
                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                        </EmptyDataRow>
                        <TitlePanel>
                            <table cellpadding="0" cellspacing="0" border="0" align="left">
                                <tr>
                                    <td align="right">
                                        <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                            Height="28px" UseSubmitBehavior="false" ClientSideEvents-Click="
                                                function(s,e){
                                                   btnExportSubmit.SendPostBack('Click');
                                                }
                                                ">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <ClientSideEvents FocusedRowChanged="function(s, e) {RowChanged(s, e);}" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Settings ShowTitlePanel="true" />
                    <SettingsPager PageSize="10" />
                </cc:ASPxGridView>
                <div class="seperate">
                </div>
                <div class="btnPosition">
                    <table cellpadding="0" cellspacing="0" border="0" align="center">
                        <tr>
                            <td align="right">
                                <dx:ASPxButton ID="btnView" ClientInstanceName="btnView" runat="server" OnClick="btnView_Click"
                                    Text="<%$ Resources:WebResources, View %>">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
