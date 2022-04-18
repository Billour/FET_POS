<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="TSAL05.aspx.cs" Inherits="VSS_SAL_TSAL05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="func1">
        <div class="titlef" style="text-align: left">
            <!--交易未結清單-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, UnclearedTradeListing %>" />
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--申請日期-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ApplicationDate %>" />：
                        </td>
                        <td class="tdval" colspan="3" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtS_Date" ClientInstanceName="txtS_Date" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtE_Date" ClientInstanceName="txtE_Date" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--狀 態-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="cbSTATUS" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                                SelectedIndex="0">
                                <Items>
                                    <dx:ListEditItem Text="未結" Value="1" />
                                    <dx:ListEditItem Text="已結" Value="2" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--服務類別-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ServiceClass %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="cbSERVICE_TYPE" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                                SelectedIndex="0">
                                <Items>
                                    <dx:ListEditItem Text="-請選擇-" Value="" />
                                    <dx:ListEditItem Text="新啟用" Value="1" />
                                    <dx:ListEditItem Text="續約" Value="2" />
                                    <dx:ListEditItem Text="代收" Value="3" />
                                    <dx:ListEditItem Text="服務異動" Value="4" />
                                    <dx:ListEditItem Text="線上儲值" Value="5" />
                                    <dx:ListEditItem Text="維修" Value="6" />
                                    <dx:ListEditItem Text="網購" Value="10" />
                                    
                                    <%--<dx:ListEditItem Text="新啟用" Value="1" />
                                    <dx:ListEditItem Text="續約" Value="2" />
                                    <dx:ListEditItem Text="2轉3" Value="3" />
                                    <dx:ListEditItem Text="換補卡" Value="4" />
                                    <dx:ListEditItem Text="代收" Value="5" />
                                    <dx:ListEditItem Text="維修" Value="6" />
                                    <dx:ListEditItem Text="網購" Value="7" />
                                    <dx:ListEditItem Text="預購" Value="8" />
                                    <dx:ListEditItem Text="MNP(IA)" Value="9" />
                                    <dx:ListEditItem Text="特殊授權(IA)" Value="10" />
                                    <dx:ListEditItem Text="變更促代-換貨(SSI)" Value="11" />
                                    <dx:ListEditItem Text="變更促代-不換貨(SSI)" Value="12" />--%>
                                    
                                 
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--客戶門號-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="txtMSISDN" runat="server" Width="170px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--銷售人員-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="cbSALE_PERSON" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                                SelectedIndex="0">
                               <%-- <ClientSideEvents SelectedIndexChanged="
                                function(s,e)
                                  {
                                     if(s.GetValue()==GV_OPERATOR)
                                     {
                                        combinedPaymentButton.SetEnabled(true);
                                        btnCancelTran.SetEnabled(true);
                                     }else
                                     {                                       
                                        combinedPaymentButton.SetEnabled(false);
                                        btnCancelTran.SetEnabled(false);
                                     }
                                  }" />--%>
                                <Items>
                                    <dx:ListEditItem Text="陳文筆" Value="SAL05" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">

                <script type="text/javascript">
                    function checkDate(s, e) {
                        var s_date = txtS_Date.GetText();
                        var e_date = txtE_Date.GetText();

                        if (s_date != "" || e_date != "") {
                            if (s_date != "" && isNaN(Date.parse(s_date))) {
                                alert('日期格式錯誤，請重新輸入');
                                e.processOnServer = false;
                            } else if (e_date != "" && isNaN(Date.parse(e_date))) {
                                alert('日期格式錯誤，請重新輸入');
                                e.processOnServer = false;
                            } else if (s_date != "" && e_date != "" && Date.parse(s_date) > Date.parse(e_date)) {
                                alert('申請訖日必須大於起日，請重新輸入');
                                e.processOnServer = false;
                            }
                        }
                    }
                </script>

                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                OnClick="btnSearch_Click">
                                <ClientSideEvents Click="function(s,e){checkDate(s,e);}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                AutoPostBack="false" OnClick="btnClear_Click">
                                <ClientSideEvents Click="function(s,e){resetForm(aspnetForm);}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="divContent" runat="server" class="SubEditBlock">

                        <script type="text/javascript">
                            var selectAll = false;
                            function OnGridSelectionChanged(s, e) {
                                this.s = s;
                                this.e = e;
                                this.grid = gvMaster;
                                if (!selectAll)
                                    grid.GetSelectedFieldValues('SERVICE_TYPE', OnGridSelectionComplete);
                            }

                            function OnGridSelectionComplete(values) {
                                var payCount = 0; //付款
                                var olrCount = 0// OLR on line Recharge
                                var otherCount = 0;
                                for (var i = 0; i < values.length; i++) {
                                    if (values[i] == '3') //付款系統而來的資料
                                        payCount++;
                                    else if (values[i] == '5')
                                        olrCount++;
                                    else otherCount++;

                                    if ((payCount > 0 && otherCount > 0) ||
                                        (olrCount > 0 && otherCount > 0) ||
                                        (payCount > 0 && olrCount > 0)) //付款資料不能合併其他類型帳款
                                    {
                                        var idx = e.visibleIndex < 0 ? i : e.visibleIndex;
                                        alert('帳單代收交易與銷售交易不允許合併結帳!');
                                        if (selectAll) {//全選時
                                            for (var k = 0; k < grid.pageRowCount; k++) {
                                                grid.SelectRowOnPage(k + grid.visibleStartIndex, false);
                                            }

                                            var chk = document.getElementById("checkbox1");
                                            chk.checked = false;

                                        } else grid.SelectRowOnPage(idx, false); //單筆選                   
                                        break;
                                    }
                                }
                                selectAll = false;
                            }

                            //不選取DISENABLED的CHECKBOX
                            function CheckAll_onclick() {
                                this.grid = gvMaster;
                                selectAll = true;
                                var chk = document.getElementById("checkbox1");
                                for (var i = 0; i < grid.pageRowCount; i++) {
                                    if (grid.GetRow(i + grid.visibleStartIndex).attributes["canSelect"].value == "true") {
                                        if (chk.checked) {
                                            grid.SelectRowOnPage(i + grid.visibleStartIndex, true);
                                        } else {
                                            grid.SelectRowOnPage(i + grid.visibleStartIndex, false);
                                        }
                                    }
                                }
                                grid.GetSelectedFieldValues('SERVICE_TYPE', OnGridSelectionComplete);
                            }
                        </script>

                        <cc:ASPxGridView ID="gvMaster" runat="server" EnableCallBacks="false" ClientInstanceName="gvMaster"
                            KeyFieldName="POSUUID_DETAIL" AutoGenerateColumns="False" Width="100%" IsClearStatus="false" 
                            OnPageIndexChanged="gvMaster_PageIndexChanged"
                            OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" 
                            OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                            OnSelectionChanged="gvMaster_SelectionChanged" 
                            OnCommandButtonInitialize="gvMaster_CommandButtonInitialize">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="ITEMS" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="STATUS_NAME" Caption="<%$ Resources:WebResources, Status %>"
                                    VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="APPLY_DATE" Caption="<%$ Resources:WebResources, ApplicationDate %>"
                                    VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="SERVICE_TYPENAME" Caption="<%$ Resources:WebResources, ServiceClass %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="MSISDN" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="TOTAL_AMOUNT" Caption="<%$ Resources:WebResources, AmountReceivable %>"
                                    VisibleIndex="6" />
                                <dx:GridViewDataColumn FieldName="SALE_PERSON" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                    VisibleIndex="7" />
                                <dx:GridViewDataTextColumn Caption="折扣" VisibleIndex="8">
                                    <DataItemTemplate>
                                        <dx:ASPxButton ID="btnDiscount" runat="server" Text="折扣">
                                        </dx:ASPxButton>
                                        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ContentUrl='<%#"~/VSS/SAL/TSAL05/TSAL05_DiscountDetail.aspx?" + TransferURL("POSUUID_DETAIL="+ Convert.ToString(Eval("[POSUUID_DETAIL]")))%>'
                                            PopupElementID="btnDiscount" AllowDragging="True" AllowResize="True" CloseAction="CloseButton"
                                            PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
                                            Width="500px" Height="280px" HeaderText="折扣明細" EnableHierarchyRecreation="True">
                                        </dx:ASPxPopupControl>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                               
                            </Columns>
                            <Templates>
                                <DetailRow>
                                    <cc:ASPxGridView ID="detailGrid" runat="server" ClientInstanceName="detailGrid" Settings-ShowTitlePanel="true"
                                        KeyFieldName="ID" Width="100%"
                                        OnPageIndexChanged="detailGrid_PageIndexChanged" EnableRowsCache="true">
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="ITEMS" Caption="<%$ Resources:WebResources, Items %>"
                                                VisibleIndex="0" />
                                            <dx:GridViewDataColumn FieldName="PROMOTION_CODE" Caption="<%$ Resources:WebResources, PromotionCode %>"
                                                VisibleIndex="1" />
                                            <dx:GridViewDataColumn FieldName="PROMO_NAME" Caption="<%$ Resources:WebResources, PromotionName %>"
                                                VisibleIndex="2" />
                                            <dx:GridViewDataColumn FieldName="MSISDN" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                                VisibleIndex="3" />
                                            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"
                                                VisibleIndex="4" />
                                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                                VisibleIndex="5" />
                                            <dx:GridViewDataColumn FieldName="SIM_CARD_NO" Caption="<%$ Resources:WebResources, SimCardSerialNumber %>"
                                                VisibleIndex="6" />
                                            <dx:GridViewDataColumn FieldName="AMOUNT" Caption="<%$ Resources:WebResources, Amount %>" />
                                        </Columns>
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <Settings ShowFooter="false" />
                                        <SettingsDetail IsDetailGrid="true" />
                                        <SettingsPager PageSize="5">
                                        </SettingsPager>
                                    </cc:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <SettingsPager PageSize="10" />
                            <SettingsBehavior ProcessSelectionChangedOnServer="false" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                            <ClientSideEvents SelectionChanged="function(s,e){OnGridSelectionChanged(s,e);}" />
                        </cc:ASPxGridView>
                        <div class="seperate">
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelTran" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="combinedPaymentButton" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="btnPosition" id="showFooter" runat="server" visible="true">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="combinedPaymentButton" ClientInstanceName="combinedPaymentButton"
                            runat="server" Text="<%$ Resources:WebResources, ConsolidatedCheckout %>" OnClick="combinedPaymentButton_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancelTran" ClientInstanceName="btnCancelTran" runat="server"
                            Text="<%$ Resources:WebResources, CancelTransaction %>" OnClick="btnCancelTran_Click"
                            Style="height: 21px">
                            <ClientSideEvents Click="function(s,e)
                            {                             
                               if (!confirm('您確定要取消所勾選的交易資料嗎？')){e.processOnServer=false;}                               
                            }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
