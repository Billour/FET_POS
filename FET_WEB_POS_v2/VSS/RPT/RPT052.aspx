<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPT052.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPT052" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--庫存日報表-->
        <asp:Literal ID="Literal1" runat="server" Text="寄銷進銷存明細表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
          <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal8" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                 <table style="width: 250px">
                                <tr>
                                    <td>
                                          <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                          <td>
                                          <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100px"></dx:ASPxTextBox>
                                          </td>
                                     
                                     <td>
                                          <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                     </td>
                                     <td>     
                                          <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px"></dx:ASPxTextBox>
                                     </td>
                                     </tr>
                                     </table>
                </td>
                
                
                </tr>
                 <tr>
                <td class="tdtxt">
                    
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SalesBackDate %>"></asp:Literal>：
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
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, SaveClass %>"></asp:Literal>：
                    </td>
                     
                    <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="120px">
                                <Items>


                                    <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                                    <dx:ListEditItem Text="現金" Value="1" />
                                    <dx:ListEditItem Text="信用卡" Value="1" />
                                
                                                           
                                    
                                  
                                </Items>
                            </dx:ASPxComboBox>
                    </td>
                    </tr>
                   <tr>
               <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                     
                    <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                                <Items>


                                    <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                                    <dx:ListEditItem Text="999999" Value="1" />
                                    <dx:ListEditItem Text="100000" Value="1" />
                                
                                                           
                                    
                                  
                                </Items>
                            </dx:ASPxComboBox>
                    </td>
                    <td class="tdval">
                            ~<dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="120px">
                                <Items>


                                    <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                                    <dx:ListEditItem Text="999999" Value="1" />
                                    <dx:ListEditItem Text="100000" Value="1" />
                                
                                                           
                                    
                                  
                                </Items>
                            </dx:ASPxComboBox>
                    </td>
               </tr>     
                <tr>
               <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductNo %>"></asp:Literal>：
                    </td>
                     
                    
                    <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox4" runat="server" Width="120px">
                                <Items>


                                    <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                                    <dx:ListEditItem Text="999999" Value="1" />
                                    <dx:ListEditItem Text="100000" Value="1" />
                                
                                                           
                                    
                                  
                                </Items>
                            </dx:ASPxComboBox>
                    </td>
                      <td class="tdval">
                            ~<dx:ASPxComboBox ID="ASPxComboBox5" runat="server" Width="120px">
                                <Items>


                                    <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                                    <dx:ListEditItem Text="999999" Value="1" />
                                    <dx:ListEditItem Text="100000" Value="1" />
                                
                                                           
                                    
                                  
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
                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>">
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
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" align="left">
            <tr>
                <td>
                    <asp:Literal ID="Literal14" runat="server" Text=" <%$ Resources:WebResources, PrintDate %>"></asp:Literal>：
                    <asp:Label ID="Label2" runat="server" Text="	2010/11/10 12:00	～	2010/11/11 12:00	"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
               <div class="criteria">    
   <table width="100%" align="left">
                    <tr>
                        <td>
                             <asp:Literal ID="Literal13" runat="server" Text=" 門市編號"></asp:Literal>：
                             <asp:Label ID="Lbl1" runat="server" Text="	SSSS	～	SSSS	"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                       
                         <td>
                             <asp:Literal ID="Literal2" runat="server" Text=" <%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                             <asp:Label ID="Label1" runat="server" Text="	2010/11/1	～	2010/11/17	"></asp:Label>
                        </td>
                         <td>
                             <asp:Literal ID="Literal16" runat="server" Text=" <%$ Resources:WebResources, PrintDate %>"></asp:Literal>：
                             <asp:Label ID="Labelx" runat="server" Text="	2010/11/10 12:00	～	2010/11/11 12:00	"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td>
                             <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                             <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        </td>
                         <td>
                             <asp:Literal ID="Literal18" runat="server" Text=" <%$ Resources:WebResources, PrintPerson %>"></asp:Literal>：
                             <asp:Label ID="Label4" runat="server" Text="Kevin"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td>
                             <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, ProductNo %> "></asp:Literal>：
                             <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                        </td>
                         <td>
                             <asp:Literal ID="Literal20" runat="server" Text=" <%$ Resources:WebResources, Page %>"></asp:Literal>：
                             <asp:Label ID="Label6" runat="server" Text="	1 / 10	"></asp:Label>
                        </td>
                    </tr>
                </table>   
                </div>   
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%">
        <Columns>
    

             <dx:GridViewDataColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, StoreName %>" runat="server" Caption="<%$ Resources:WebResources, StoreName %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName%>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductNo %>" Caption="<%$ Resources:WebResources, ProductNo %>" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductName %>" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataColumn FieldName="期初" Caption="期初" />
            <dx:GridViewDataTextColumn FieldName="進貨" runat="server" Caption="進貨">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="銷貨" Caption="銷貨"/>
            <dx:GridViewDataColumn FieldName="銷退" Caption="銷退" />
            <dx:GridViewDataColumn FieldName="移出" Caption="移出" />
            <dx:GridViewDataColumn FieldName="撥入" Caption="撥入" />
            <dx:GridViewDataTextColumn FieldName="退倉" runat="server" Caption="退倉"/>
            <dx:GridViewDataTextColumn FieldName="期末" Caption="期末" />
            

        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
</asp:Content>

