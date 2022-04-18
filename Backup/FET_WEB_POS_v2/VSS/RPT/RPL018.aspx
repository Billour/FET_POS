<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL018.aspx.cs"  MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL018" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
   
    <div class="titlef" align="left">
            <!--寄售明細表-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL018 %>"></asp:Literal>
    </div>
    
      <div class="seperate"></div>

    
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                                          
                <tr>
                 <td class="tdtxt">
                            <!--訂單日期-->
                           
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SettlementMonth %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <table style="width: 250px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <div style="width: 120px;">
                                            <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server">
                                                <ValidationSettings CausesValidation="false">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                       
            </tr>
             <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                 <table style="width: 250px">
                                <tr>
                                    <td>
                                          <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                          <td>
                                          <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px"></dx:ASPxTextBox>
                                          </td>
                                     
                                     <td>
                                          <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                     </td>
                                     <td>     
                                          <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100px"></dx:ASPxTextBox>
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
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" >
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" 
                                AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                             <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" /></dx:ASPxButton></td><td> <dx:ASPxButton ID="btnExport" runat="server" Text="匯出">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
  <div class="seperate"></div>       
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
            Width="100%"  >
            <Columns>

                <dx:GridViewDataTextColumn FieldName="合作模式" runat="server" Caption="<%$ Resources:WebResources, ModalityForCooperation %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="廠商代號" runat="server" Caption="門市編號"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName%>" />
                 <dx:GridViewDataTextColumn FieldName="結算起日" Caption="<%$ Resources:WebResources, SettlementStartDate %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="結算訖日" Caption="<%$ Resources:WebResources, SettlementEndDate %>" />
                <dx:GridViewDataColumn FieldName="本期進貨總額" Caption="<%$ Resources:WebResources, TotalPurchases %>" />
                <dx:GridViewDataColumn FieldName="進項稅額(發票)" Caption="<%$ Resources:WebResources, IinputTax %>" />
                <dx:GridViewDataColumn FieldName="期末庫存總額" Caption="<%$ Resources:WebResources, EndTotalPurchases %>" />
                <dx:GridViewDataColumn FieldName="進項稅額(折讓)" Caption="<%$ Resources:WebResources, DinputTax %>" />
                <dx:GridViewDataColumn FieldName="本期退倉總額" Caption="<%$ Resources:WebResources, TTotalReturnWarehousingAmount %>" />
                <dx:GridViewDataColumn FieldName="退倉稅額" Caption="<%$ Resources:WebResources, ReturnWarehousingTax %>" />
                <dx:GridViewDataColumn FieldName="銷貨金額(未稅）" Caption="<%$ Resources:WebResources, TSalesAmount %>" />
                 <dx:GridViewDataColumn FieldName="銷貨(稅額）" Caption="銷貨(稅額）" />
                <dx:GridViewDataColumn FieldName="佣金(未稅）" Caption="<%$ Resources:WebResources, CommissionFreeTax %>" />
                <dx:GridViewDataColumn FieldName="佣金(稅額）" Caption="<%$ Resources:WebResources, CommissionTax %>" />
                <dx:GridViewDataColumn FieldName="結算金額" Caption="<%$ Resources:WebResources, SettlementAmount %>" />  
                 <dx:GridViewDataColumn FieldName="結算(Y/N)" Caption="<%$ Resources:WebResources, ConfirmSettlement %>" />

                               
		
               

            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="10">
            </SettingsPager>
        </cc:ASPxGridView>
        
        
        <div class="seperate">
        </div>


</asp:Content>
