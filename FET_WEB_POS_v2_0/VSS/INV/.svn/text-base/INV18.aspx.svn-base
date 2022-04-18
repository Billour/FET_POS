<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV18.aspx.cs" Inherits="VSS_INV_INV18" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=200,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   

    <div>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--庫存調整查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="庫存調整查詢作業"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div  style="text-align:left;">
                <table>                    
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox9" runat="server" Width="80px"></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>：
                        </td>
                        <td class="" nowrap="nowrap">
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
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整門市-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox16" runat="server" Width="80px"></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                         <table cellpadding="0" cellspacing="0" border="0">
                         <tr>
                            <td><asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                            <td><dx:ASPxTextBox ID="TextBox14" runat="server" Width="80px"></dx:ASPxTextBox></td>
                            <td><asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,300);return false;" /></td>
                            <td>&nbsp;</td>
                            <td><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                            <td><dx:ASPxTextBox ID="TextBox15" runat="server" Width="80px"></dx:ASPxTextBox></td>
                            <td><asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,300);return false;" /></td>
                        </tr>
                        </table>
                        </td>                        
                        
                    </tr>
                </table>
                </div>
                </div>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
               <table align="center" cellpadding="0" cellspacing="0" border="0">
                 <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
              </table>       
            </div>
            <div class="seperate">
            </div>
                             
                    <div class="SubEditBlock">
                       
                         <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="調整單號" Width="100%"              
                             OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnPageIndexChanged="gvMaster_PageIndexChanged" >   
                            <Columns>
                             
                                <dx:GridViewDataHyperLinkColumn  PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/INV/INV18_1.aspx?dno={0}" FieldName="調整單號" Caption="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"  PropertiesHyperLinkEdit-Style-Font-Underline="true"></dx:GridViewDataHyperLinkColumn>
                                 
                                <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>"  />
                                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                                <dx:GridViewDataColumn FieldName="調整日期" Caption="<%$ Resources:WebResources, AdjustmentDate %>" />
                                <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>" />
                                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                            </Columns>
                                                        
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            <SettingsPager PageSize="5"></SettingsPager>   
                          
                        </cc:ASPxGridView>     
                    </div>
               
           
            </div>
        </div>
    
 </asp:Content>