<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL025.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL025" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
   
    <div class="titlef" align="left">
            <!--門市Aging Report-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL025 %>"></asp:Literal>
    </div>
    
      <div class="seperate"></div>

    
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                                          
                <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                 <table style="width: 250px">
                                <tr>
                                    <td>
                                          <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                          <td>
                                          <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100px"></dx:ASPxTextBox>
                                          </td>
                                     
                                     <td>
                                          <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                     </td>
                                     <td>     
                                          <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100px"></dx:ASPxTextBox>
                                     </td>
                                     </tr>
                                     </table>
                </td>
                 <td class="tdtxt">
                            <!--交易日期：-->
                           
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>：
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, AdjustmentStock %>"></asp:Literal>：
                </td>
                 
                <td class="tdval">
               
                 <table style="width: 250px">
                                <tr>
                                  
                                          <td>
                                          <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px"></dx:ASPxTextBox>
                                          </td>
                                     
                                     
                                     <td>     
                                          <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100px"></dx:ASPxTextBox>
                                     </td>
                                     </tr>
                                     </table>
                </td>
                
                </tr>
                 <tr>
                    <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, AdjustmentReason %>"></asp:Literal>：
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
                       <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductNo %>"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                 <table style="width: 250px">
                                <tr>
                                    <td>
                                          <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                          <td>
                                          <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px"></dx:ASPxTextBox>
                                          </td>
                                     
                                     <td>
                                          <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                     </td>
                                     <td>     
                                          <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="100px"></dx:ASPxTextBox>
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
          
                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, AdjustmentStore %>" runat="server" Caption="<%$ Resources:WebResources, AdjustmentStore %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, AdjuestmentNo %>" Caption="<%$ Resources:WebResources, AdjuestmentNo %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductNo %>" Caption="<%$ Resources:WebResources, ProductNo %>" />
                <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName%>" />
                <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, AdjustmentStock %>" Caption="<%$ Resources:WebResources, AdjustmentStock %>" />
                <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, AdjuestmentQuantity %>" runat="server" Caption="<%$ Resources:WebResources, AdjuestmentQuantity %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, AdjustmentReason %>" Caption="<%$ Resources:WebResources, AdjustmentReason %>" />
                <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, AccountingSubject %>" runat="server" Caption="<%$ Resources:WebResources, AccountingSubject %>"></dx:GridViewDataTextColumn>
              
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="10">
            </SettingsPager>
        </cc:ASPxGridView>
        
        
        <div class="seperate">
        </div>


</asp:Content>
