<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="INV29.aspx.cs" Inherits="VSS_INV_INV29" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    <script>
        function CheckRequired(s, e) {
            s.SetEnabled = false;
            var IsNotNull = true;
            $(".required").each(function() {
                if ($(this).val() == "") {
                    alert("IMEI 上傳檔案路徑 不允許為空");
                    e.processOnServer = false;
                    s.SetEnabled = true;
                    IsNotNull = false;
                    return IsNotNull;
                }
            });
            if (IsNotNull) {
                if (!confirm('資料若有重複，則點擊【資料儲存】後會被忽略，是否確定要匯入？')) {
                    e.processOnServer = false;
                    s.SetEnabled = true;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<input type="hidden" id="hdUploadBatchNo" runat="server" />
<input type="hidden" id="hdIsSave" runat="server" />

        <div class="titlef">
            <!--IMEI 上傳作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HeadquartersIMEI%>"></asp:Literal>
        </div>
        <div class="criteria">
            <table id="Tab1" >
                <tr>
                    <td align="right">
                        <!--IMEI檔案路徑-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, IMEIFilePath %>"></asp:Literal>：
                    </td>
                    <td >
                        <asp:FileUpload ID="FileUpload" runat="server" Width ="600" CssClass="required" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>            
                    <td><dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" 
                          OnClick="btnImport_Click"  >
                          <ClientSideEvents Click="function(s, e) {  CheckRequired(s, e); }" />
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
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="grid" runat="server" KeyFieldName="IMEI" Width="100%"   
            OnPageIndexChanged="grid_PageIndexChanged">                
            <Columns>                                                           
                <%--<dx:GridViewDataColumn FieldName="IVRCODE" Caption="<%$ Resources:WebResources, IMEIIvrcode %>" />                                  
                <dx:GridViewDataTextColumn FieldName="CHANNEL" Caption="<%$ Resources:WebResources, IMEIChannel %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />--%>
                <dx:GridViewDataTextColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, IMEIImei %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />
                <%--<dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, IMEIProdno %>" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:N0}" />--%>
                <dx:GridViewDataColumn FieldName="ERROR" Caption="<%$ Resources:WebResources, Status%>" CellStyle-HorizontalAlign="Left" />            
            </Columns>
            <Templates>
                <TitlePanel>
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:WebResources, IMEIComparison%>" ForeColor="White" Font-Size="Small"></asp:Label>                
                </TitlePanel>
            </Templates>
            <Styles>
                <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>    
                <TitlePanel HorizontalAlign="Left"></TitlePanel>            
            </Styles>  
            <SettingsPager PageSize="5"></SettingsPager>          
            <Settings ShowTitlePanel="true" />   
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </cc:ASPxGridView>                           
         <div class="seperate"></div>
       <div class="seperate"></div>       
       <%-- <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>            
                <td><dx:ASPxButton ID="btnSave" OnClick="btnSave_Click" runat="server" Text="<%$ Resources:WebResources, DataStorage %>" /></td>
                <td>&nbsp;</td>
                <td><dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" /></td>
            </tr>
            </table>
        </div>--%>

</asp:Content>    

