<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD10_Import.aspx.cs" Inherits="VSS_ORD_ORD10_Import" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script>
        function DisabledAndRunButton(s, e) {
            var file = document.form1.FileUpload.value;
            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file))) {
                alert("匯入的檔案不正確,須為Excel檔!!");
                var t = document.getElementById('FileUpload');
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td>
                            <span style="red">*</span><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>"
                                OnClick="btnImport_Click">
                                <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s, e);}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"> 
                        </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
           
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="F1"  Width="100%" 
                OnPageIndexChanged ="gvMaster_PageIndexChanged"
                OnHtmlFooterCellPrepared="gvMaster_HtmlFooterCellPrepared">
                <Columns>
                    <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="WEIGHT" Caption="<%$ Resources:WebResources, Ratio %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>"
                        CellStyle-ForeColor="Red">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <FooterCell>
                        <dx:ASPxLabel ID="lblFooterTotal" runat="server">
                        </dx:ASPxLabel>
                    </FooterCell>
                </Templates>
                <SettingsPager PageSize="10"></SettingsPager>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>"></SettingsText>
                <Settings ShowFooter="true" />
            </cc:ASPxGridView>
            <div class="seperate"></div>
            <div class="btnPosition" runat="server" id="Div1">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton Enabled="false" ID="btnUpdate" runat="server" Text="上傳確認" OnClick="btnUpdate_Click">
                                <ClientSideEvents Click="function(s, e) { hidePopupWindow(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCalcel" runat="server" AutoPostBack="False" Text="<%$ Resources:WebResources, Cancel %>">
                                <ClientSideEvents Click="function(s, e) { hidePopupWindow();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
