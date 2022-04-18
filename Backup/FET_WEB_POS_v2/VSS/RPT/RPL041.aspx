<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL041.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL041" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
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
        <!--POS門市手機銷量表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL041 %>"></asp:Literal>
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
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="lblTRADE_DATE_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTRADE_DATE_E" runat="server" ClientInstanceName="txtEDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
             </tr>
            
            <tr>
                <td class="tdtxt">
                    <!--門市編號-->
                    <asp:Literal ID="lblSTORE_NO" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblSTORE_NO_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTORE_NO_S" runat="server" PopupControlName="StoresPopup"  />
                            </td>
                            <td>
                                  <asp:Literal ID="lblSTORE_NO_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>     
                                  <uc1:PopupControl ID="txtSTORE_NO_E" runat="server" PopupControlName="StoresPopup"  />
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
        </tr>
    </table>
    
    <div class="seperate"></div>

    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
            Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged"  >
            <Columns>
                <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="門市編號">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, StoreName %>" Caption="<%$ Resources:WebResources, StoreName %>" />
                <dx:GridViewDataColumn FieldName="手機料號" Caption="手機料號" />
                <dx:GridViewDataColumn FieldName="手機名稱" Caption="手機名稱" />
                <dx:GridViewDataColumn FieldName="單機銷貨數" Caption="單機銷貨數" />
                <dx:GridViewDataColumn FieldName="單機均價" Caption="單機均價" />
                <dx:GridViewDataColumn FieldName="促銷銷貨數" Caption="促銷銷貨數" />
                <dx:GridViewDataColumn FieldName="促銷均價" Caption="促銷均價" />
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