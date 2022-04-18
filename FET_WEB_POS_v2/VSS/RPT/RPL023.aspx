<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL023.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL023" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
   
    <div class="titlef" align="left">
            <!--禮券入帳明細表-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL023 %>"></asp:Literal>
    </div>
    
      <div class="seperate"></div>

    
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                                          
                <tr>
                 <td class="tdtxt">
                            <!--交易日期：-->
                           
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
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
                        
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                            <table style="width: 250px">
                                <tr>
                                    
                                          <td>
                                          <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px"></dx:ASPxTextBox>
                                          </td>
                                     
                                   
                                </tr>
                            </table>
                </td>
                        
                       
            </tr>
             <tr>
             <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                            <table style="width: 250px">
                                <tr>
                                    
                                          <td>
                                          <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100px"></dx:ASPxTextBox>
                                          </td>
                                     
                                   
                                </tr>
                            </table>
                </td>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, MaterialNo %>"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                            <table style="width: 250px">
                                <tr>
                                    
                                          <td>
                                          <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px"></dx:ASPxTextBox>
                                          </td>
                                     
                                   
                                </tr>
                            </table>
                </td>
               
                
                </tr>
                 <tr>
                    <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ServiceType %>"></asp:Literal>：
                    </td>
                     
                    <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="120px">
                                <Items>


                                    <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                                    <dx:ListEditItem Text="銷貨" Value="1" />
                                    <dx:ListEditItem Text="退貨" Value="1" />
                                
                                                           
                                    
                                  
                                </Items>
                            </dx:ASPxComboBox>
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
                <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="門市編號"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                <dx:GridViewDataTextColumn FieldName="交易日期" runat="server" Caption="<%$ Resources:WebResources, TradeDate %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="機台編號" Caption="<%$ Resources:WebResources, CashRegisterNo %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="交易序號" Caption="<%$ Resources:WebResources, TransactionNo %>" />
                <dx:GridViewDataColumn FieldName="發票號碼" Caption="<%$ Resources:WebResources, InvoiceNo %>" />
                <dx:GridViewDataColumn FieldName="服務類型" Caption="<%$ Resources:WebResources, ServiceType %>" />
                <dx:GridViewDataTextColumn FieldName="料號" runat="server" Caption="<%$ Resources:WebResources, ProductNo %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName%>" />
                <dx:GridViewDataTextColumn FieldName="數量" runat="server" Caption="<%$ Resources:WebResources, Quantity %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="未稅金額" Caption="<%$ Resources:WebResources, FreeTaxAmount %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="稅額" Caption="<%$ Resources:WebResources, Tax %>" />
                <dx:GridViewDataColumn FieldName="應收金額" Caption="<%$ Resources:WebResources, AmountReceivable %>" />
                <dx:GridViewDataColumn FieldName="付款方式" Caption="<%$ Resources:WebResources, PaymentMethod %>" />
                <dx:GridViewDataColumn FieldName="信用卡手續費" Caption="<%$ Resources:WebResources, CreditCardFees %>" />
                <dx:GridViewDataColumn FieldName="備註" Caption="<%$ Resources:WebResources, Remark %>" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="10">
            </SettingsPager>
        </cc:ASPxGridView>
        
        
        <div class="seperate">
        </div>


</asp:Content>

