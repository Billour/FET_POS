<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV08.aspx.cs" Inherits="VSS_INV_INV08" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">


        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            var x = CHK_SDATE.GetValue();
            var y = CHK_EDATE.GetValue();
//            if (x == '' || x == null ) {
//                alert("[驗收日期起值]不允許空白，請重新輸入!");
//                _gvEventArgs.processOnServer = false;
//                _gvSender.Focus();
//                return;
//            }

//            if (y == '' || y == null) {
//                alert("[驗收日期訖值]不允許空白，請重新輸入!");
//                _gvEventArgs.processOnServer = false;
//                _gvSender.Focus();
//                return;
//            }
            if (y != '' && x != '') {
                if (x > y) {
                    alert("[驗收日期訖值]不允許小於[驗收日期起值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    _gvSender.Focus();
                    return;
                }
            }

            var xx = ORD_SDATE.GetValue();
            var yy = ORD_EDATE.GetValue();

            if (xx != '' && yy != '') {
                if (xx > yy) {
                    alert("[訂單日期訖值]不允許小於[訂單日期起值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    _gvSender.Focus();
                    return;
                }
            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<OBJECT ID="BarcodePrint"
CLASSID="CLSID:5BF61033-4F10-4492-84F4-2052AB55CFFF"
CODEBASE="BarcodePrint.CAB#version=1,0,0,0">
</OBJECT>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div class="titlef">
                    <!--進貨驗收作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReceivingInspection %>"></asp:Literal>
                </div>
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--PO/OE_NO-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, POOENO %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="txtPOEONO" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單/主配日期：-->
                                <asp:Literal ID="Literal9" runat="server" Text="訂單/主配日期："></asp:Literal>
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <div style="width: 120px;">
                                                <dx:ASPxDateEdit ID="ORD_SDATE" runat="server" ClientInstanceName="ORD_SDATE">
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <div style="width: 120px;">
                                                <dx:ASPxDateEdit ID="ORD_EDATE" runat="server" ClientInstanceName="ORD_EDATE">
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單狀態-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="txtSTATUS" runat="server">
                                    <Items>
                                        <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                        <dx:ListEditItem Text="未驗收" Value="00" />
                                        <dx:ListEditItem Text="部分驗收" Value="50" />
                                        <dx:ListEditItem Text="已結案" Value="70" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單/主配編號-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="txtORDERNO" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--驗收日期-->
                              <%--  <span style="color: Red">*</span>--%>
                                <asp:Literal ID="Literal6" runat="server" Text=" <%$ Resources:WebResources, AcceptanceDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <div style="width: 120px;">
                                                <dx:ASPxDateEdit ID="CHK_SDATE" runat="server" ClientInstanceName="CHK_SDATE">
                                                 <%--   <ValidationSettings>
                                                        <RequiredField IsRequired="true" ErrorText="驗收日期不允許空白，請重新輸入!!" />
                                                    </ValidationSettings>--%>
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <div style="width: 120px;">
                                                <dx:ASPxDateEdit ID="CHK_EDATE" runat="server" ClientInstanceName="CHK_EDATE">
                                                <%--    <ValidationSettings>
                                                        <RequiredField IsRequired="true" ErrorText="驗收日期不允許空白，請重新輸入!!" />
                                                    </ValidationSettings>--%>
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--供貨商-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Supplier %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxComboBox ID="Supplier" runat="server">
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--商品料號-->
                                <asp:Literal ID="Literal10" runat="server" Text=" <%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="txtPROD" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="seperate">
                </div>
                <div class="btnPosition">
                    <table align="center" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                    OnClick="btnSearch_Click">
                                    <ClientSideEvents Click="function(s, e) {  CheckDate(s, e); }" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                    SkinID="ResetButton" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="seperate">
                </div>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PO_OE_NO"
                    Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnPageIndexChanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="PO_OE_NO" Caption="<%$ Resources:WebResources, POOENO %>">
                            <DataItemTemplate>
                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server">
                                </dx:ASPxHyperLink>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="ORDER_NO" Caption="<%$ Resources:WebResources, OrderNoOrDistributionNo %>" />
                        <dx:GridViewDataTextColumn FieldName="INV_APPROVE_NO" Caption="<%$ Resources:WebResources, ReceivingNo %>">
                            <DataItemTemplate>
                                <dx:ASPxHyperLink ID="ASPxHyperLink2" runat="server">
                                </dx:ASPxHyperLink>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, SToreno %>" />
                        <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, STorename %>" />
                        <dx:GridViewDataColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, OrderStatus %>" />
                        <dx:GridViewDataColumn FieldName="ORDDATE" Caption="訂單/主配日期" />
                        <dx:GridViewDataColumn FieldName="CHECK_IN_DTM" Caption="<%$ Resources:WebResources, AcceptanceDate %>" />
                        <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                        <dx:GridViewDataColumn FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                </cc:ASPxGridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
