<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON13.aspx.cs" Inherits="VSS_CONS_CON13" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function getSUPPINFO(s, e) {
            this.s = s;
            this.Sender = s;

            if (s.GetText() != '')
                PageMethods.getSuppInfo(Sender.GetText(), getSUPPINFO_OnOK);
        }

        function getSUPPINFO_OnOK(returnData) {

            if (returnData == '') {
                SuppName.SetText(null);
            }
            else {
                //廠商名稱∩總金額底限
                var DataArray = returnData.split("∩");
                SuppName.SetText(DataArray[1]);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品進貨驗收查詢作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventoryExaminationSearch %>"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" colspan="1">
                        <!--訂單/主配編號-->
                        <dx:ASPxLabel ID="Literal9" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <%-- <dx:ASPxComboBox ID="ORDER_ID" runat="server">
                        </dx:ASPxComboBox>--%>
                        <uc1:PopupControl ID="ORDER_ID" runat="server" PopupControlName="OddNumberPopup"
                            KeyFieldValue1="VW_CON13_SELECT" KeyFieldValue2="ORDNO" />
                    </td>
                    <td class="tdtxt">
                        <!--出貨編號-->
                        <dx:ASPxLabel ID="Literal23" runat="server" Text="<%$ Resources:WebResources, DeliveryOrderNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="OENO" runat="server" Width="130">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--驗收狀態-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReceiveStatus %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="STATUS" runat="server">
                            <Items>
                                <dx:ListEditItem Value="0" Text="全部" Selected="true" />
                                <dx:ListEditItem Value="1" Text="待進貨" />
                                <dx:ListEditItem Value="2" Text="已驗收" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <dx:ASPxLabel ID="Literal24" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="PRODNO" runat="server" PopupControlName="ProductsPopup" />
                    </td>
                    <td class="tdtxt">
                        <!--進貨日期-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ReceivedDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ORDERDTS" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ORDERDTE" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <%--  <dx:ASPxComboBox ID="SuppNo" ClientInstanceName="SuppNo" ClientSideEvents-ValueChanged="getSUPPINFO"
                            runat="server" />--%>
                        <uc1:PopupControl ID="SuppNo" runat="server" PopupControlName="ConsignmentVendorsPopup"
                            SetClientValidationEvent="getSUPPINFO" KeyFieldValue2="SUPP_NO" />
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, SupplierName %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="SuppName" ClientInstanceName="SuppName" ReadOnly="true" runat="server"
                            Width="110">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
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
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" SkinID="ResetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="ORDNO"
                    Width="100%" AutoGenerateColumns="False" EnableRowsCache="False" OnPageIndexChanged="gvMaster_PageIndexChanged"
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ORDNO" runat="server" Caption="<%$ Resources:WebResources, OrderNo %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="OENO" runat="server" Caption="<%$ Resources:WebResources, DeliveryOrderNo %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SUPP_NAME" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STATUSNAME" runat="server" Caption="<%$ Resources:WebResources, ReceiveStatus %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ORDER_DATE" runat="server" Caption="<%$ Resources:WebResources, ReceivedDate %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ORDER_USER" runat="server" Caption="<%$ Resources:WebResources, ReceivedBy %>">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                                Width="100%" EnableRowsCache="true" OnPageIndexChanged="gvDetail_PageIndexChanged">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="OENO" runat="server" Caption="<%$ Resources:WebResources, DeliveryOrderNo %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="IN_QTY" runat="server" Caption="<%$ Resources:WebResources, InspectionQuantity %>">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFooter="false" />
                                <SettingsDetail IsDetailGrid="true" />
                                <SettingsPager PageSize="5">
                                </SettingsPager>
                                <Styles>
                                    <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsPager PageSize="10" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                </cc:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
    </div>
</asp:Content>
