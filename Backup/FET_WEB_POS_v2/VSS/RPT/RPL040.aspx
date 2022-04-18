<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL040.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL040" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtSDate.GetText() != '' && txtEDate.GetText() != '') {
                if (txtSDate.GetValue() > txtEDate.GetValue()) {
                    alert("[銷售日期起值]不允許大於[銷售日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
    <div class="titlef" align="left">
        <!--POS discount-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL040 %>"></asp:Literal>
    </div>
    
    <div class="seperate"></div>
    
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
             <tr>
                  <td class="tdtxt">
                        <!--銷售日期-->
                        <asp:Literal ID="lblTRADE_DATE" runat="server" Text="<%$ Resources:WebResources, SalesDate %>"></asp:Literal>：
                  </td>
                 <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblTRADE_DATE_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTRADE_DATE_S" runat="server" ClientInstanceName="txtSDate">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="lblTRADE_DATE_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTRADE_DATE_E" runat="server" ClientInstanceName="txtEDate">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
             </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
        
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
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false" OnClick="btnReset_Click">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click">
                    <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
    </table>
                
    <div class="seperate"></div>

    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server"
        Width="100%" onpageindexchanged="gvMaster_PageIndexChanged"  >
        <Columns>
        <dx:GridViewDataTextColumn FieldName="商品料號" Caption="商品料號" />
        <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
        <dx:GridViewDataColumn FieldName="總數量" Caption="總數量" />
        <dx:GridViewDataColumn FieldName="總金額" Caption="<%$ Resources:WebResources, TotalAmount%>" />
        <dx:GridViewDataTextColumn FieldName="會計科目" Caption="<%$ Resources:WebResources, AccountingSubject %>" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10"></SettingsPager>
    </cc:ASPxGridView>

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster" ></dx:ASPxGridViewExporter>   

</asp:Content>