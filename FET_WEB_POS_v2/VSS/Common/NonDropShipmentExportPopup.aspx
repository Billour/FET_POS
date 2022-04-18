<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NonDropShipmentExportPopup.aspx.cs" Inherits="VSS_Common_NonDropShipmentExportPopup" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Non-DropShipment主配商品檔匯入</title>
    <script>
        function DisabledAndRunButton(s, e) {
          var file = document.getElementById("FileUpload1").value;

            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file))) {
                alert("匯入的檔案不正確,須為Excel檔!!");
                var t = document.getElementById("FileUpload1");
                t.outerHTML = t.outerHTML;
                e.processOnServer = false;
            }     
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
            <div class="criteria">
                <table>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="seperate"></div>
            
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClick="btnImport_Click">
                                <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s, e);}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="resetButton" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnUpdate"  runat="server" Text="上傳" onclick="btnUpdate_Click"></dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCalcel" runat="server" AutoPostBack="False" Text="<%$ Resources:WebResources, Cancel %>">
                                <ClientSideEvents Click="function(s, e) {hidePopupWindow();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="seperate"></div>
            <div class="GridScrollBar" style="height: 370px;Width:620px;"  >
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server"  Width="100%" KeyFieldName="SID" AutoGenerateColumns="False"
                onhtmldatacellprepared="gvMasterDV_HtmlDataCellPrepared"  OnPageIndexChanged="gvMasterDV_PageIndexChanged">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="STORENO" Caption="<%$ Resources:WebResources, StoreNo %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ASSIGN_QTY" Caption="<%$ Resources:WebResources, ActivePicking %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="EXCEPTIOB_CAUSE" Caption="<%$ Resources:WebResources, ErrorDescription %>"></dx:GridViewDataTextColumn>
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoRecordsImported %>" />
                 <SettingsPager PageSize="10" />
            </cc:ASPxGridView>
            </div>
            <div class="seperate"></div>
            
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        
                    </tr>
                </table>
            </div>
            
        </div>
    </form>
</body>
</html>
