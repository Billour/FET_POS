<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL016.aspx.cs"  MasterPageFile="~/MasterPage.master"  Inherits="VSS_RPT_RPL016" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[訂單日期起值]不允許大於[訂單日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }

            if (ASPxDateEdit1.GetText() != '' && ASPxDateEdit2.GetText() != '') {
                if (ASPxDateEdit1.GetValue() > ASPxDateEdit2.GetValue()) {
                    alert("[收回日期起值]不允許大於[收回日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
   
    <div class="titlef" align="left">
            <!--交易取消憑證回收狀態表-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL016 %>"></asp:Literal>
    </div>
    
      <div class="seperate"></div>

    
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                                          
                <tr>
                 <td class="tdtxt">
                            <!--訂單日期-->
                           
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CancelTradeDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <table style="width: 250px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" ClientInstanceName="txtOrdDateStart">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="false" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="txtOrdDateEnd">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="false" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt">
                            <!--憑證類型：-->
                            <asp:Literal ID="Literal7" runat="server" Text="憑證類型"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="120px">
                                <Items>
                                    <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                                    <dx:ListEditItem Text="回收發票" Value="RECYCLE" />
                                    <dx:ListEditItem Text="折讓單" Value="PAYOFF" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
            </tr>
             <tr>
                <td class="tdtxt">
                    <!--門市編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                              <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox1" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td> 
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>     
                                <uc1:PopupControl ID="ASPxTextBox2" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                         </tr>
                     </table>
                </td>
                <td class="tdtxt">
                            <!--收回日期-->
                           
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, RegainDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <table style="width: 250px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ClientInstanceName="ASPxDateEdit1">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="false" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ClientInstanceName="ASPxDateEdit2">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="false" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                
                </tr>
                          
         
        </table>
    </div>
     <div class="seperate">
        </div>
        
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                                OnClick="btnSearch_Click">
                                <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                OnClick="btnReset_Click" AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                                <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click">
                                <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
  <div class="seperate"></div>       
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
            Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged" >
            <Columns>
           
                <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="門市編號">
                   
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, StoreName %>" Caption="<%$ Resources:WebResources, StoreName %>" />
                 <dx:GridViewDataTextColumn FieldName="交易取消日期" Caption="<%$ Resources:WebResources, CancelTradeDate %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="交易序號" Caption="<%$ Resources:WebResources, TransactionNo %>" />
                <dx:GridViewDataColumn FieldName="憑證類型" Caption="<%$ Resources:WebResources, CertType %>" />
                <dx:GridViewDataColumn FieldName="原開立發票號碼" Caption="<%$ Resources:WebResources, OldInvoiceNo %>" />
                <dx:GridViewDataColumn FieldName="客戶統編" Caption="<%$ Resources:WebResources, CUnifiedBusinessNo%>" />
                <dx:GridViewDataColumn FieldName="折讓單編號" Caption="<%$ Resources:WebResources, DiscountListNo %>" />
                <dx:GridViewDataColumn FieldName="未稅金額" Caption="<%$ Resources:WebResources, FreeTaxAmount %>" />
                <dx:GridViewDataColumn FieldName="稅額" Caption="<%$ Resources:WebResources, Tax %>" />
                 <dx:GridViewDataColumn FieldName="含稅金額" Caption="<%$ Resources:WebResources, HaveTaxAmount %>" />
                <dx:GridViewDataColumn FieldName="回收日期" Caption="<%$ Resources:WebResources, RegainDate %>" />
                <dx:GridViewDataColumn FieldName="折讓單申報否" Caption="<%$ Resources:WebResources, CheckDiscount %>" />
                <dx:GridViewDataColumn FieldName="原交易日期" Caption="<%$ Resources:WebResources, OldTradeDate %>" />  
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="10">
            </SettingsPager>
        </cc:ASPxGridView>
        
        
        <div class="seperate">
            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
            </dx:ASPxGridViewExporter>
        </div>


</asp:Content>

