<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV10.aspx.cs" Inherits="VSS_INV10_INV10" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    
    <div>
   <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--盤點查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventorySearch %>"></asp:Literal>                        
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, StockTaking %>"  AutoPostBack="false" >
                         <ClientSideEvents Click="function(s, e){ document.location='INV11.aspx'; }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox>
                        </td>
               
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                               <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="transferOutStartDate" runat="server"></dx:ASPxDateEdit></td>
                                 <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server"></dx:ASPxDateEdit></td>
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
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                  <table align="center" cellpadding="0" cellspacing="0" border="0">
                 <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
              </table>    
            </div>
            <div class="seperate"></div>
          
                    <div class="SubEditBlock">
                        <div class="GridScrollBar" style="height: auto">
                           <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="盤點單號" Width="100%"              
                             OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnPageIndexChanged="gvMaster_PageIndexChanged" > 
                              
                                <Columns>                                    
                                     <dx:GridViewDataHyperLinkColumn  PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/INV/INV11.aspx?InventoryNo={0}" FieldName="盤點單號" Caption="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"  PropertiesHyperLinkEdit-Style-Font-Underline="true"></dx:GridViewDataHyperLinkColumn>
                                 
                                    
                                    <dx:GridViewDataColumn FieldName="盤點日期" Caption="盤點日期" />
                                    <dx:GridViewDataColumn FieldName="盤點類型" Caption="盤點類型" />
                                    <dx:GridViewDataColumn FieldName="盤點狀態" Caption="盤點狀態" />
                                    <dx:GridViewDataColumn FieldName="盤點人員" Caption="盤點人員" />
                                    <dx:GridViewDataColumn FieldName="更新人員" Caption="更新人員" />
                                    <dx:GridViewDataColumn FieldName="更新日期" Caption="更新日期" />
                                </Columns>
                                
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                <SettingsPager PageSize="5"></SettingsPager>   
                            
                            </cc:ASPxGridView>
                        </div>
                    </div>
                </div>
    
        <div class="seperate"></div>
      </div>
    </div>
</asp:Content>