<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON12.aspx.cs" Inherits="VSS_CONS_CON12" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        //檢查實際退倉數量
        function CheckRtnQtyOnTextChanged(s, e)
        {
        
            _gvEventArgs = e;
            _gvSender = s;
            _qtyName = s.name;
            var fName = "6_txtRTNQTY";
            txtSTOCKQTY = getClientInstance('Label', s.name.replace(fName, "5_txtSTOCKQTY"));
            var iSTOCKQTY = txtSTOCKQTY.outerText;

            Qty = s.GetValue();
            var iQty = 0;
            //       
            //            iTmpQty = 0;
            ////            if (Qty == null || Qty == "")
            ////            {
            ////                e.isValid = false;
            ////                s.SetValue(null);
            ////            }
            ////            else
            ////            {               

            //                iQty = Number(Qty);
            //                if (isNaN(iQty))
            //                {
            //                  s.SetValue('');
            //                    alert('輸入字串不為數字格式，請重新輸入');
            //                      e.isValid = false;
            //                   // 
            //                    //s.SetValue(iTmpQty);
            //                    //return false;
            //                }
            //                else if (iQty < 0)
            //                {
            //                   s.SetValue('');
            //                    alert('實際退倉數量需不允許小於0，請重新輸入');
            //                     e.isValid = false;
            //                    //
            //                    //s.SetValue(iTmpQty);
            //                   // return false;
            //                }



            //}
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品退倉設定作業(門市)-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousing %>"></asp:Literal>
                    <dx:ASPxLabel ID="lbStatus" ClientInstanceName="lbStatus" runat="server" ClientVisible="false" />
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='CON11.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--寄銷退倉單號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="comRtnNo" runat="server" OnSelectedIndexChanged="comRtnNo_SelectedIndexChanged"
                            AutoPostBack="true" EnableClientSideAPI="false">
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉日期-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, WarehousedDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:Label ID="lblReturnDate" runat="server"> </asp:Label>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Status1" runat="server" Text="" />
                        <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Label ID="Label2" runat="server" Text="00 未存檔"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉起日-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="lbRTNM_BDate" runat="server">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉訖日-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="lbRTNM_EDate" runat="server">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新日期-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="lbModifiedDate" runat="server">
                        </dx:ASPxLabel>
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
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新人員-->
                        <dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="lbModifiedBy" runat="server">
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>
   
                <div class="GridScrollBar" style="height: auto">
                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="SEQNO"
                   OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                    OnPageIndexChanged="gvMaster_PageIndexChanged"
                        Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <dx:GridViewDataTextColumn runat="server" FieldName="SEQNO" Caption="<%$ Resources:WebResources, Items %>"
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn runat="server" FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"
                                VisibleIndex="1">
                                <DataItemTemplate>
                                    <dx:ASPxLabel ID="txtPRODNO" runat="server" Text='<%# Bind("PRODNO") %>' />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn runat="server" FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn runat="server" FieldName="SUPPNO" Caption="<%$ Resources:WebResources, SupplierNo %>"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn runat="server" FieldName="SUPPNAME" Caption="<%$ Resources:WebResources, SupplierName %>"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn runat="server" FieldName="STOCKQTY" Caption="<%$ Resources:WebResources, StockQuantity %>"
                                VisibleIndex="5">
                                <DataItemTemplate>
                                    <dx:ASPxLabel ID="txtSTOCKQTY" runat="server" Text='<%# Bind("STOCKQTY") %>' />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn runat="server" FieldName="RTNQTY" Caption="<%$ Resources:WebResources, ActualRetunedQuantity %>"
                                VisibleIndex="6">
                                <HeaderCaptionTemplate>
                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ActualRetunedQuantity %>">
                                    </dx:ASPxLabel>
                                </HeaderCaptionTemplate>
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtRTNQTY" Width="50px" MaxLength="9" runat="server" Text='<%# Bind("RTNQTY") %>'>
                                        <ClientSideEvents TextChanged="function(s,e){ CheckRtnQtyOnTextChanged(s, e); }" />
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            <RegularExpression ValidationExpression="\d*" ErrorText="請輸入正整數" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager PageSize="5" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    </cc:ASPxGridView>
                </div>
                
     
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                            OnClick="btnSave_Click">
                        <clientsideevents click="function (s, e) {e.processOnServer = confirm('退倉單儲存後不可再修改，請確認');}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                         <dx:ASPxButton ID="btnPrint" runat="server" Text="<%$ Resources:WebResources, PrintReceipt %>"
                                UseSubmitBehavior="false" CausesValidation="false"    OnClick="btnPrint_Click" />
                    </td>
                </tr>
            </table>
        </div>
               </ContentTemplate>
        <%--    <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            </Triggers>--%>
            
        </asp:UpdatePanel>
    </div>
       <iframe id="fDownload" style="display: none"  src="" runat="server"></iframe>
</asp:Content>


