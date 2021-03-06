<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT16.aspx.cs" Inherits="VSS_OPT_OPT16" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script>
        function DisabledAndRunButton(s, e) {            
            var file = document.form1.FileUpload1.value;
            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file)))
            {
                  alert("匯入的檔案不正確,須為Excel檔!!");
                  var   t=document.getElementById('FileUpload1'); 
                  t.outerHTML=t.outerHTML ;
                  e.processOnServer=false;
            }      //return false;
            else {
                if (s.GetEnabled()) {
                    s.SendPostBack('Click');
                    s.SetEnabled(false);
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

    <input type="hidden" id="hdUploadBatchNo" runat="server" />
    <div>
<%--        <div class="titlef">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <!--HappyGo兌點名單上傳-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoPointListUpload %>"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
--%>        
        <div class="criteria">
            <table>
                <tr>
                    <td style="width: 200px" align="right">
                        <!--檔案路徑-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, FilePath %>"></asp:Literal>：
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="FileUpload1" runat="server" Enabled="false" />
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" 
                            OnClick="btnImport_Click">
                            <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s, e);}" />
                            </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" 
                            Text="<%$ Resources:WebResources, Reset %>" onclick="btnClear_Click">                      
                            <ClientSideEvents Click = "function(s, e) {
                                    file = document.form1.FileUpload1.value;
                                    reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
                                    if (file != '' && !(reOKFiles.test(file)))
                                    {
                                          var   t=document.getElementById('FileUpload1'); 
                                          t.outerHTML=t.outerHTML ;
                                          e.processOnServer=false;
                                    }
                                    else {
                                        if (s.GetEnabled()) {
                                            s.SendPostBack('Click');
                                            s.SetEnabled(false);
                                        }
                                    }
                                }">
                            </ClientSideEvents>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
            Width="100%" KeyFieldName="SID" 
            onhtmldatacellprepared="gvMaster_HtmlDataCellPrepared" 
            onpageindexchanged="gvMaster_PageIndexChanged">
            <Columns>
               <dx:GridViewDataDateColumn FieldName="ACTIVITY_NO" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"/>
                <dx:GridViewDataDateColumn FieldName="MSISDN" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>" />
                <dx:GridViewDataDateColumn FieldName="EXCEPTIOB_CAUSE" Caption="<%$ Resources:WebResources, ErrorDescription %>" />
            </Columns>    
            <Settings ShowTitlePanel="false" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoRecordsImported %>" />
            <SettingsPager PageSize="10"></SettingsPager>
        </cc:ASPxGridView>
        
        <div class="seperate">
                <table align="right">
                    <tr>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SuccessfulPlots %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Literal ID="lblSuccess" runat="server" Text=""></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Plots %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, FailedPlots %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Literal ID="lblFail" runat="server" Text=""></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Plots %>"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="seperate">
            </div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnUpload" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>"
                            OnClick="btnUpload_Click"></dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false">
                              <ClientSideEvents Click="function(s, e) {hidePopupWindow();}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    </form>
</body>
</html>
