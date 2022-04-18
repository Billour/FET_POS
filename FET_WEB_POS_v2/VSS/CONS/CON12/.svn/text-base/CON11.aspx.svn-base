<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON11.aspx.cs" Inherits="VSS_CONS_CON11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
  <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkDate(s, e)
        {
            var sd = lbB_DATE_S.GetText(); //退倉起(起)日
            var ed = lbB_DATE_E.GetText(); //退倉起(迄)日
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
       
            var esd = lbE_DATE_S.GetText(); //退倉迄(起)日
            var eed = lbE_DATE_E.GetText(); //退倉迄(迄)日
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

            var rsd = lbRTNDATE_S.GetText(); //實際退倉日期(起)日
            var red = lbRTNDATE_E.GetText(); //實際退倉日期(迄)日
            if (rsd == '' || red == '')
            {
                
            } else
            {
                if ((new Date(rsd)) > (new Date(red)))
                {
                    alert('【實際退倉日期(迄)日】不可小於【實際退倉日期(起)日】');
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
                    <!--寄銷商品退倉查詢(門市) -->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSearch_Store %>"></asp:Literal>
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
                        <dx:ASPxTextBox ID="txtRtnNo" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Status %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="combRTN_STATUS" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="" Selected="true" />
                                <dx:ListEditItem Text="已存檔" Value="10" />
                                <dx:ListEditItem Text="已傳輸" Value="50" />
                                <dx:ListEditItem Text="已完成" Value="60" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉起日-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 240px">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="lbB_DATE_S" ClientInstanceName="lbB_DATE_S" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="lbB_DATE_E" ClientInstanceName="lbB_DATE_E" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉訖日-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingendDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 240px">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="lbE_DATE_S" ClientInstanceName="lbE_DATE_S" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="lbE_DATE_E" ClientInstanceName="lbE_DATE_E" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--實際退倉日期-->
                        <dx:ASPxLabel ID="Literal9" runat="server" Text="<%$ Resources:WebResources, RearyReturnWarehousingDay %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 240px">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="lbRTNDATE_S" ClientInstanceName="lbRTNDATE_S" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="lbRTNDATE_E" ClientInstanceName="lbRTNDATE_E" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
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
                            OnClick="btnSearch_Click" >
                             <ClientSideEvents Click="function(s,e){checkDate(s,e);}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="CSM_RTNM_UUID"
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated" Width="100%" AutoGenerateColumns="False">
                    <Columns>
                        <dx:GridViewDataColumn Visible="false" FieldName="CSM_RTNM_UUID" VisibleIndex="0" />
                        <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/CONS/CON12/CON12.aspx?dno={0}"
                            FieldName="RTNNO" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"
                            VisibleIndex="1">
                        </dx:GridViewDataHyperLinkColumn>
                        <dx:GridViewDataTextColumn FieldName="B_DATE" runat="server" Caption="<%$ Resources:WebResources, ReturnWarehousingstartDate %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="E_DATE" runat="server" Caption="<%$ Resources:WebResources, ReturnWarehousingendDate %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="RTNDATE" runat="server" Caption="<%$ Resources:WebResources, WarehousedDate %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_USER" runat="server" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" runat="server" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STATUS" runat="server" Caption="<%$ Resources:WebResources, Status %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="REMARK" runat="server" Caption="<%$ Resources:WebResources, Remark %>">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                                Width="100%" >
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="SEQNO" runat="server" Caption="<%$ Resources:WebResources, Items %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="SUPP_NO" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="SUPP_NAME" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="STOCKQTY" runat="server" Caption="<%$ Resources:WebResources, StockQuantity %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="RTNQTY" runat="server" Caption="<%$ Resources:WebResources, ActualRetunedQuantity %>">
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
                    <SettingsPager PageSize="5" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                </cc:ASPxGridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
