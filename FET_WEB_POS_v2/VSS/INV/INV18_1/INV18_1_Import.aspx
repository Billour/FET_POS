<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV18_1_Import.aspx.cs" Inherits="VSS_INV_INV18_1_Import"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script>
        function DisabledAndRunButton(s, e) {
            var file = document.form1.FileUpload.value;
            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file)))
            {
                  alert("匯入的檔案不正確,須為Excel檔!!");
                  var t = document.getElementById('FileUpload'); 
                  t.outerHTML=t.outerHTML ;
                  e.processOnServer=false;
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
                            <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="seperate"></div>

            <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" OnPageIndexChanged="gvMaster_PageIndexChanged"
                AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="F9" Caption="<%$ Resources:WebResources, IMEIIvrcode %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="F1" Caption="<%$ Resources:WebResources, ProductCode %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="F2" Caption="<%$ Resources:WebResources, ProductName %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="F3" Caption="<%$ Resources:WebResources, StockQuantity %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="F4" Caption="<%$ Resources:WebResources, AdjuestmentQuantity %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="F6" Caption="<%$ Resources:WebResources, ReasonForAdjustment %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="F8" Caption="IMEI">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="F10" Caption="<%$ Resources:WebResources, ErrorDescription %>"
                        CellStyle-ForeColor="Red">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoRecordsImported %>" />
            </cc:ASPxGridView>
            
            <div class="seperate"></div>
            
            <div class="btnPosition" runat="server" id="Div1">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton Enabled="false" ID="btnUpdate" runat="server" Text="上傳確認" OnClick="btnUpdate_Click">
                                <ClientSideEvents Click="function(s, e) {
	                                        hidePopupWindow();
                                        }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCalcel" runat="server" AutoPostBack="False" Text="<%$ Resources:WebResources, Cancel %>">
                                <ClientSideEvents Click="function(s, e) {
	                                    hidePopupWindow();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </form>
</body>
</html>
