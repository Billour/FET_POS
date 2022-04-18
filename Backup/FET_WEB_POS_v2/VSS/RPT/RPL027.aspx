<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL027.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL027" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
   
    <div class="titlef" align="left">
            <!--總部庫存進銷存明細表-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL027 %>"></asp:Literal>
    </div>
    
      <div class="seperate"></div>

    
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
        <tr>
                <td class="tdtxt">
                    <!--調整門市-->
                    <asp:Literal ID="Literal6" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                 <table style="width: 250px">
                                <tr>
                                    <td width="100px">
                                    <uc1:PopupControl ID="txtSTORE_NO" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                                       
                                    </td>
 
                                     
                                     </tr>
                                     </table>
                </td>
                <td class="tdtxt">
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
                                    <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" EditFormatString="yyyy/MM/dd">
                                       
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" EditFormatString="yyyy/MM/dd">
                                
                                </dx:ASPxDateEdit>
                            </td>
                            
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                <%--<div align="left">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, PrintDate %>"></asp:Literal>：<asp:Label ID="date" runat="server" Text="2010/11/17"></asp:Label><br />
                     
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PrintTime %>"></asp:Literal>：<asp:Label ID="Label1" runat="server" Text="13:00"></asp:Label><br />
                      <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Page %>"></asp:Literal>：<asp:Label ID="Label2" runat="server" Text="1"></asp:Label>
                </div>--%>
                </td>
            </tr>
            
                 <tr>
                     <td class="tdtxt">
                   
                    <asp:Literal ID="Literal2" runat="server" Text="交易型態"></asp:Literal>：
                </td>
          
                <td class="tdval">
                   <dx:ASPxComboBox ID="ddlINV_TRAN_TYPE" runat="server" Width="120px">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="" Selected="true" />
                                <dx:ListEditItem Text="進貨" Value="CI" />
                                <dx:ListEditItem Text="銷售" Value="SA" />
                                <%--<dx:ListEditItem Text="退貨" Value="SR" />
                                <dx:ListEditItem Text="換貨" Value="CG" />--%>
                                <dx:ListEditItem Text="銷退" Value="SR" /> 
                                <dx:ListEditItem Text="移出" Value="SO" />
                                <dx:ListEditItem Text="撥入" Value="SI" />
                                <dx:ListEditItem Text="調整" Value="AI" />
                                <dx:ListEditItem Text="驗退" Value="RI" />
                                <dx:ListEditItem Text="退倉" Value="RO" />
                                <dx:ListEditItem Text="移倉" Value="TO" />
                            </Items>
                        </dx:ASPxComboBox>
                </td>
                    
                       <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                 
                <td class="tdval">
                 <table style="width: 250px">
                                <tr>
                                    <td>
                                          <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                          <td width="130px">
                                          <uc1:PopupControl ID="txtPRODNO_S" runat="server" IsValidation="false" 
                                                PopupControlName="ProductsPopup"  />
                                        
                                          </td>
                                     
                                     <td>
                                          <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                     </td>
                                     <td width="130px">     
                                      <uc1:PopupControl ID="txtPRODNO_E" runat="server" IsValidation="false" 
                                                PopupControlName="ProductsPopup"  />
                                          
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
                            <dx:ASPxButton ID="btnSearch" runat="server" 
                                Text="<%$ Resources:WebResources, Search %>" onclick="btnSearch_Click" >
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"/></td><td> 
                            <dx:ASPxButton ID="btnExport" runat="server" 
                                Text="<%$ Resources:WebResources, Export %>" onclick="btnExport_Click" >
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
  <div class="seperate"></div>       
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
            Width="100%" onpageindexchanged="gvMaster_PageIndexChanged" >
            <Columns>
            	
                <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, StoreName %>" Caption="<%$ Resources:WebResources, StoreName %>" />
                <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCode %>" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName%>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="日期" Caption="<%$ Resources:WebResources, TradeDate %>" />
                <dx:GridViewDataColumn FieldName="交易型態" Caption="交易型態" />
                <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>" />
                <dx:GridViewDataTextColumn FieldName="交易序號" runat="server" Caption="<%$ Resources:WebResources, TransactionNo %>"></dx:GridViewDataTextColumn>
              
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="10">
            </SettingsPager>
        </cc:ASPxGridView>
         <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster" ></dx:ASPxGridViewExporter>   
        
        <div class="seperate">
        </div>


</asp:Content>

