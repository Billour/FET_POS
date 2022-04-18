<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="CHK06.aspx.cs" Inherits="VSS_CHK_CHK06" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function CheckRequired(s, e) {

            var IsNotNull = true;

            if ($(".FileUpload1").val() == "" && $(".FileUpload2").val() == "") {
                alert("合庫入帳檔案 及 NCCC信用卡入帳檔案 不允許為空");
                e.processOnServer = false;
                s.SetEnabled(true);
                IsNotNull = false;
                return IsNotNull;
            }

            if (IsNotNull) {
                //if (confirm('資料若有重複，則點擊【資料儲存】後會被忽略，是否確定要匯入？')) {
                    DisabledAndRunButton(s);
               // }
            }

        }

        function DisabledAndRunButton(s) {
            if (s.GetEnabled()) {
                s.SendPostBack('Click');
                s.SetEnabled(false);
            }
        }
      
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<input type="hidden" id="hdUploadBatchNo" runat="server" />
<input type="hidden" id="hdIsSave" runat="server" />

        <div class="titlef">
            <!--總部對帳作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HeadquartersReconciliation%>"></asp:Literal>
        </div>
       
        <div class="criteria">
            <table id="Tab1" >
                <tr>
                    <td align="right">
                        <!--交易日期區間-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ReconciliationDateInterval %>"></asp:Literal>：
                    </td>
                    <td colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtTRADE_DATE_S" runat="server" ClientInstanceName="txtSDate" EditFormatString="yyyy/MM/dd">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtTRADE_DATE_E" runat="server" ClientInstanceName="txtEDate" EditFormatString="yyyy/MM/dd">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>                                
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
                    <td colspan="3" align="left">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" CssClass="FileUpload1" />
                    </td>
                    <td align="left" width="250px">
                        <dx:ASPxLabel ID="lblBankAccountedError" runat="server" Visible="false"
                            Font-Bold="True" ForeColor="Red" Text="合庫入帳檔案格式有誤，請確認檔案格式內容!!"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--NCCC信用卡入帳檔案路徑-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, NcccCreditFilePath %>"></asp:Literal>：
                    </td>
                    <td colspan="3" align="left">
                        <asp:FileUpload ID="FileUpload2" runat="server" Width="60%" CssClass="FileUpload2" />
                    </td>
                    <td align="left" width="250px">
                        <dx:ASPxLabel ID="lblNcccCreditError" runat="server" Visible="false"
                            Font-Bold="True" ForeColor="Red" Text="NCCC信用卡入帳入帳檔案格式有誤，請確認檔案格式內容!!"></dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>            
                    <td>
                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClick="btnImport_Click">
                            <ClientSideEvents Click="function(s, e) {  if(ASPxClientEdit.ValidateEditorsInContainer(null)) { CheckRequired(s, e); } }" />
                        </dx:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
       </div>
        
        <div class="seperate"></div>   
        
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="BANK_CASH_M_ID" 
            Width="100%" EnableCallBacks="false"         
            OnPageIndexChanged="gvMaster_PageIndexChanged" 
            onfocusedrowchanged="gvMaster_FocusedRowChanged" 
            onhtmldatacellprepared="gvMaster_HtmlDataCellPrepared">                
            <Columns>                                                           
                <dx:GridViewDataColumn FieldName="TRADE_DATE" Caption="<%$ Resources:WebResources, ReconciliationDate %>" />                                  
                <dx:GridViewDataTextColumn FieldName="BANK_CASH_IN" Caption="<%$ Resources:WebResources, CooperativeBankAccounted %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="POS_CASH_IN" Caption="<%$ Resources:WebResources, PosCash %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="NCCC_CC_IN" Caption="<%$ Resources:WebResources, NcccCredit %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="POS_CC_IN" Caption="<%$ Resources:WebResources, PosCredit %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataColumn FieldName="EXCEPTION_CAUSE" Caption="<%$ Resources:WebResources, ErrorDescription%>" CellStyle-HorizontalAlign="Left" />            
            </Columns>
            <Templates>
                <TitlePanel>
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:WebResources, TheTotalAmountOf%>" 
                    ForeColor="Black" Font-Size="Small"></asp:Label>                
                </TitlePanel>
            </Templates>
            <Styles>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>    
                <TitlePanel HorizontalAlign="Left"></TitlePanel>            
            </Styles>  
            <Settings ShowTitlePanel="true" />
            <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
            <SettingsPager PageSize="5"></SettingsPager>             
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>  
        
         <div class="seperate"></div>
         
         <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" 
            runat="server" KeyFieldName="BANK_CASH_D_ID" Width="100%"           
            OnPageIndexChanged="gvDetail_PageIndexChanged" 
            onhtmldatacellprepared="gvDetail_HtmlDataCellPrepared">                
            <Columns>                                                           
                <dx:GridViewDataColumn FieldName="TRADE_DATE" Caption="<%$ Resources:WebResources, ReconciliationDate %>" />                  
                <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" />                             
                <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>"  />
                <dx:GridViewDataTextColumn FieldName="BANK_CASH_IN" Caption="<%$ Resources:WebResources, CooperativeBankAccounted %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="POS_CASH_IN" Caption="<%$ Resources:WebResources, PosCash %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="NCCC_CC_IN" Caption="<%$ Resources:WebResources, NcccCredit %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataTextColumn FieldName="POS_CC_IN" Caption="<%$ Resources:WebResources, PosCredit %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <dx:GridViewDataColumn FieldName="EXCEPTION_CAUSE" Caption="<%$ Resources:WebResources, ErrorDescription%>" CellStyle-HorizontalAlign="Left" />            
            </Columns>
            <Templates>
                <TitlePanel>
                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:WebResources, DetailedListOfExceptions %>"
                     ForeColor="Black" Font-Size="Small"></asp:Label>                                  
                </TitlePanel>                
            </Templates>
            <Styles>
                <TitlePanel HorizontalAlign="Left"></TitlePanel>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
            </Styles>   
            <Settings ShowTitlePanel="true" />
            <SettingsPager PageSize="5"></SettingsPager>            
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>   
        
       <div class="seperate"></div> 
             
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>            
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, DataStorage %>" onclick="btnSave_Click" >
                         <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s);}" />
                    </dx:ASPxButton>
                </td>
                <td>&nbsp;</td>
                <td><dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" /></td>
            </tr>
            </table>
        </div>

</asp:Content>    

