<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="CHK06.aspx.cs" Inherits="VSS_CHK_CHK06" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--總部對帳作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HeadquartersReconciliation%>"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table id="Tab1" >
                <tr>
                    <td align="right">
                        <!--對帳日期區間-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ReconciliationDateInterval %>"></asp:Literal>：
                    </td>
                    <td colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>                                
                            </tr>
                        </table>                        
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right">
                        <!--合庫入帳檔案路徑-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CooperativeBankAccountedFilePath %>"></asp:Literal>：
                    </td>
                    <td colspan="4">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--NCCC信用卡入帳檔案路徑-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, NcccCreditFilePath %>"></asp:Literal>：
                    </td>
                    <td colspan="4">
                        <asp:FileUpload ID="FileUpload2" runat="server" Width="60%" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>            
                    <td><dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Import %>" 
                    OnClick="Button1_Click" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false">
                    <ClientSideEvents Click="function(s,e) { resetForm(aspnetForm); }" />
                </dx:ASPxButton></td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>   
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="對帳日期" Width="100%"  Settings-ShowTitlePanel="true"            
            OnPageIndexChanged="grid_PageIndexChanged">                
            <Columns>                                                           
                <dx:GridViewDataColumn FieldName="對帳日期" Caption="<%$ Resources:WebResources, ReconciliationDate %>" />                                  
                <dx:GridViewDataTextColumn FieldName="合庫入帳" Caption="<%$ Resources:WebResources, CooperativeBankAccounted %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="POS現金帳" Caption="<%$ Resources:WebResources, PosCash %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="NCCC信用卡入帳" Caption="<%$ Resources:WebResources, NcccCredit %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="POS信用卡帳" Caption="<%$ Resources:WebResources, PosCredit %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataColumn FieldName="異常原因" Caption="<%$ Resources:WebResources, ErrorDescription%>" CellStyle-HorizontalAlign="Left" />            
            </Columns>
            <Templates>
                <TitlePanel>
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:WebResources, TheTotalAmountOf%>" ForeColor="White" Font-Size="Small"></asp:Label>                
                </TitlePanel>
            </Templates>
            <Styles>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>    
                <TitlePanel HorizontalAlign="Left"></TitlePanel>            
            </Styles>  
            <SettingsPager PageSize="5"></SettingsPager>             
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>                           
         <div class="seperate"></div>
         <cc:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" KeyFieldName="門市編號" Width="100%"  Settings-ShowTitlePanel="true"            
            OnPageIndexChanged="detailGrid_PageIndexChanged">                
            <Columns>                                                           
                <dx:GridViewDataColumn FieldName="對帳日期" Caption="<%$ Resources:WebResources, ReconciliationDate %>" />                  
                <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />                             
                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>"  />
                <dx:GridViewDataTextColumn FieldName="合庫入帳" Caption="<%$ Resources:WebResources, CooperativeBankAccounted %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="POS現金帳" Caption="<%$ Resources:WebResources, PosCash %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="NCCC信用卡入帳" Caption="<%$ Resources:WebResources, NcccCredit %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="POS信用卡帳" Caption="<%$ Resources:WebResources, PosCredit %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataColumn FieldName="異常原因" Caption="<%$ Resources:WebResources, ErrorDescription%>" CellStyle-HorizontalAlign="Left" />            
            </Columns>
            <Templates>
                <TitlePanel>
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:WebResources, DetailedListOfExceptions %>" ForeColor="White" Font-Size="Small"></asp:Label>                                  
                </TitlePanel>                
            </Templates>
            <Styles>
                <TitlePanel HorizontalAlign="Left"></TitlePanel>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
            </Styles>   
            <SettingsPager PageSize="5"></SettingsPager>            
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>   
       <div class="seperate"></div>       
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>            
                <td><dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, DataStorage %>" /></td>
                <td>&nbsp;</td>
                <td><dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Cancel %>" /></td>
            </tr>
            </table>
        </div>

</asp:Content>    

