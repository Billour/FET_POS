<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT18_Import.aspx.cs" Inherits="VSS_OPT_OPT18_Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script>
        function DisabledAndRunButton(s, e)
        {
            var file = document.form1.FileUpload1.value;
            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file)))
            {
                alert("匯入的檔案不正確,須為Excel檔!!");
                var t = document.getElementById('FileUpload1');
                t.outerHTML = t.outerHTML;
                e.processOnServer = false;
            }      //return false;
            else
            {
                if (s.GetEnabled())
                {
                    s.SendPostBack('Click');
                    s.SetEnabled(false);
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <input type="hidden" id="hdUploadBatchNo" runat="server" />
            <div>
            
        <%--        <div class="titlef">
                    <!--門市店長折扣Excel上傳 -->
                    <asp:Literal ID="Literal5" runat="server" Text="門市店長折扣Excel上傳"></asp:Literal>
                </div>
        --%>
             <div class="criteria">
            <table>
                <tr>
                    <td align="right">
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="450" />
                    </td>
                </tr>
            </table>
        </div>
                
                <div class="seperate"></div>
                
                <div class="btnPosition">
                    <table cellpadding="0" cellspacing="0" border="0" align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClick="btnImport_Click">
                                    <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s,e);}" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                 <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>" />
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div class="seperate"></div>
                
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="SID"
                    Width="100%" OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared" 
                onpageindexchanged="gvMaster_PageIndexChanged" >
                    <Columns>
                        <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" HeaderStyle-HorizontalAlign="center" />
                        <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>" HeaderStyle-HorizontalAlign="center" />
                        <dx:GridViewDataColumn FieldName="ROLE_ID" Caption="<%$ Resources:WebResources, Role %>" HeaderStyle-HorizontalAlign="center" />
                        <dx:GridViewDataColumn FieldName="YYMM" Caption="<%$ Resources:WebResources, DiscountsMonth %>" HeaderStyle-HorizontalAlign="center" />
                        <dx:GridViewDataColumn FieldName="DIS_AMT" Caption="<%$ Resources:WebResources, TotalDiscount %>" HeaderStyle-HorizontalAlign="center" />
                        <dx:GridViewDataColumn FieldName="ROLE_DIS_AMT" Caption="<%$ Resources:WebResources, DiscountAmount %>" HeaderStyle-HorizontalAlign="center" />
                        <dx:GridViewDataColumn FieldName="ROLE_DIS_RATE" Caption="折扣比例" HeaderStyle-HorizontalAlign="center" />
                        <dx:GridViewDataColumn FieldName="ROLE_DIS_AMT_UBOND" Caption="折扣上限" HeaderStyle-HorizontalAlign="center" />           
                        <dx:GridViewDataColumn FieldName="EXCEPTIOB_CAUSE" Caption="<%$ Resources:WebResources, ErrorDescription %>" HeaderStyle-HorizontalAlign="center" >
                        </dx:GridViewDataColumn>
                    </Columns>
                    <Settings ShowTitlePanel="false" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsPager PageSize="10"></SettingsPager>
                </cc:ASPxGridView>
                    
                <div class="seperate">
                    <table align="right" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td><dx:ASPxLabel id="lblSuccess" runat="server"></dx:ASPxLabel></td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel id="lblFail" runat="server"></dx:ASPxLabel></td>
                        </tr>
                    </table>
                
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
