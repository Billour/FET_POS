<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="CON09.aspx.cs" Inherits="VSS_CONS_CON09" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register Src="../../../Controls/ExportExcelData.ascx" TagName="ExportExcelData"
    TagPrefix="uc3" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1.Export, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkDate(s, e)
        {
            var sd = B_DATE_S.GetText(); //退倉起(起)日
            var ed = B_DATE_E.GetText(); //退倉起(迄)日
            if (sd == '' || ed == '')
            {
               
            } else
            {
                if ((new Date(sd)) > (new Date(ed)))
                {
                    alert('【退倉起(迄)日】不可小於【退倉起(起)日】');
                    e.processOnServer = false;
                    return false;
                }
            }
            var esd = E_DATE_S.GetText(); //退倉迄(起)日
            var eed = E_DATE_E.GetText(); //退倉迄(迄)日
            if (esd == '' || eed == '')
            {
             
            } else
            {
                if ((new Date(esd)) > (new Date(eed)))
                {
                    alert('【退倉迄(迄)日】不可小於【退倉迄(起)日】');
                    e.processOnServer = false;
                    return false;
                }
            }

        }
     
    </script>

    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品退倉查詢-->
                     <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSearch %>"></asp:Literal>

                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <uc1:PopupControl ID="txtRtnNo" runat="server" PopupControlName="OddNumberPopup"
                            KeyFieldValue1="CSM_RTNM" KeyFieldValue2="RTNNO" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td nowrap="nowrap">
                                    <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="RTN_STATUS" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="" Selected="true" />
                                <dx:ListEditItem Text="已存檔" Value="10" />
                                <dx:ListEditItem Text="已傳輸" Value="50" />
                                <dx:ListEditItem Text="已完成" Value="60" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--廠商編號-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td nowrap="nowrap">
                        <uc1:PopupControl ID="txtSuppNo" runat="server" KeyFieldValue2="SUPP_NO" PopupControlName="ConsignmentVendorsPopup" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品編號-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td class="tdval" nowrap="nowrap">
                                    <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup3"
                                        KeyFieldValue1="consignmentsale" KeyFieldValue2="PRODNO" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <%--退倉起日--%>
                    <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>">
                        </asp:Literal>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table>
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, start %>">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="B_DATE_S" ClientInstanceName="B_DATE_S" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, end %>">
                                    </asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="B_DATE_E" ClientInstanceName="B_DATE_E" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
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
                    <%--退倉訖日--%>
                    <td class="tdtxt" nowrap="nowrap">
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="E_DATE_S" ClientInstanceName="E_DATE_S" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, end %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="E_DATE_E" ClientInstanceName="E_DATE_E" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            Height="28px" OnClick="btnSearch_Click" >
                                 <ClientSideEvents Click="function(s,e){checkDate(s,e);}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                            Height="28px" OnClick="btnExport_Click">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="CSM_RTNM_UUID"
                Width="100%" AutoGenerateColumns="False" OnPageIndexChanged="gvMaster_PageIndexChanged"
                OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                <Columns>
                    <dx:GridViewDataColumn Visible="false" FieldName="CSM_RTNM_UUID" VisibleIndex="0" />
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="1">
                        <HeaderTemplate>
                            <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewCommandColumn> 
                    <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/CONS/CON10/CON10.aspx?dno={0}" 
                        FieldName="RTNNO" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"
                         VisibleIndex="1">
                    </dx:GridViewDataHyperLinkColumn>
                    <dx:GridViewDataTextColumn runat="server" FieldName="B_DATE" Caption="<%$ Resources:WebResources, ReturnWarehousingStartDate %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn runat="server" FieldName="E_DATE" Caption="<%$ Resources:WebResources, ReturnWarehousingEndDate %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn runat="server" FieldName="STATUS" Caption="<%$ Resources:WebResources, Status %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn runat="server" FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn runat="server" FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <DetailRow>
                        <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                            Width="100%" EnableRowsCache="true" OnPageIndexChanged="gvDetail_PageIndexChanged">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="SUPP_NO" Caption="<%$ Resources:WebResources, SupplierNo %>" />
                                <dx:GridViewDataColumn FieldName="SUPP_NAME" Caption="<%$ Resources:WebResources, SupplierName %>" />
                                <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" />
                                <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>" />
                                <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" />
                                <dx:GridViewDataColumn FieldName="STOCKQTY" Caption="<%$ Resources:WebResources, StockQty %>" />
                                <dx:GridViewDataColumn FieldName="RTNQTY" Caption="<%$ Resources:WebResources, ActualRetunedQuantity %>" />
                            </Columns>
                            <Settings ShowFooter="false" />
                            <SettingsDetail IsDetailGrid="true" />
                            <SettingsPager PageSize="5" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            <Styles>
                                <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                </TitlePanel>
                            </Styles>
                        </cc:ASPxGridView>
                    </DetailRow>
                </Templates>
                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                <SettingsPager PageSize="5" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
            </cc:ASPxGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
