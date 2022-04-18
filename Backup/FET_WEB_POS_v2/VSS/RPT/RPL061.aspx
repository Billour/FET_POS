<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL061.aspx.cs" Inherits="VSS_RPT_RPL061" MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtSDate.GetText() != '' && txtEDate.GetText() != '') {
                if (txtSDate.GetValue() > txtEDate.GetValue()) {
                    alert("[調出日期起值]不允許大於[調出日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--門市調撥明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="門市調撥明細表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
                <td class="tdtxt">
                    
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OutDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtSDate" ClientInstanceName="txtSDate" runat="server" EditFormatString="yyyy/MM/dd">
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtEDate" ClientInstanceName="txtEDate" runat="server" EditFormatString="yyyy/MM/dd">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
               <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="移入門市"></asp:Literal>：
               </td>
               <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtToStoreNO_S" runat="server" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtToStoreNO_E" runat="server" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
               </td>
             </tr>     
             <tr>
               <td class="tdtxt">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                     
               <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtProdNo_S" runat="server" PopupControlName="ProductsPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtProdNo_E" runat="server" PopupControlName="ProductsPopup" />
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
                 OnClick="btnReset_Clicked">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出"
                onclick="btnExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
        Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged" 
        ondetailrowexpandedchanged="gvMaster_DetailRowExpandedChanged">
        <Columns>
            <dx:GridViewDataColumn FieldName="STDATE" Caption="<%$ Resources:WebResources, OutDate %>" />
            <dx:GridViewDataTextColumn FieldName="STNO" runat="server" Caption="調撥單號" />
            <dx:GridViewDataTextColumn FieldName="FROM_STORENAME" Caption="<%$ Resources:WebResources, OutputStore %>" />
            <dx:GridViewDataColumn FieldName="TO_STORENAME" Caption="移入門市" />
            <dx:GridViewDataColumn FieldName="FROM_EMPNAME" Caption="調出人員" />
            <dx:GridViewDataColumn FieldName="TO_EMPNAME" Caption="移入人員" />
            <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataColumn FieldName="TRANOUTQTY" Caption="調出數量" />
            <dx:GridViewDataColumn FieldName="TRANINQTY" Caption="移入數量" />   
            <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>" CellStyle-Wrap="True" />             
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster" ></dx:ASPxGridViewExporter>
    <div class="seperate">
    </div>
</asp:Content>
