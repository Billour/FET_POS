<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DIS01_Store_Import.aspx.cs"
    Inherits="VSS_DIS_DIS01_Store_Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>指定門市上傳</title>

    <script>
        function DisabledAndRunButton(s, e) {
            var file = document.form1.FileUpload.value;
            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file))) {
                alert("匯入的檔案不正確,須為Excel檔!!");
                var t = document.getElementById('FileUpload');
                t.outerHTML = t.outerHTML;
                e.processOnServer = false;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <input type="hidden" id="hdUploadBatchNo" runat="server" />
    <div class="func">
        <div class="criteria">
            <table>
                <tr>
                    <td align="right">
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload" runat="server" Width="450" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0">
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
                        <dx:ASPxButton ID="btnReset" OnClick="btnReset_Click" runat="server" Text="<%$ Resources:WebResources, Reset %>">
                            <ClientSideEvents Click="function(s, e) {
                                    file = document.form1.FileUpload.value;
                                    reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
                                    if (file != '' && !(reOKFiles.test(file)))
                                    {                               
                                          var   t=document.getElementById('FileUpload'); 
                                          t.outerHTML=t.outerHTML ;
                                          e.processOnServer=false;
                                    }
                                    else {
                                        if (s.GetEnabled()) {
                                            s.SendPostBack('Click');
                                            s.SetEnabled(false);
                                        }
                                    }
                                }"></ClientSideEvents>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvStore" ClientInstanceName="gvStore" runat="server" KeyFieldName="STORENO"
            Width="100%" AutoGenerateColumns="False" 
            OnHtmlRowPrepared="gvProduct_HtmlRowPrepared" 
            onpageindexchanged="gvStore_PageIndexChanged">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="STORENO" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, StoreNo %>"
                    VisibleIndex="0">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="STORENAME" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, StoreName %>"
                    VisibleIndex="1">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ZONENAME" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ByDistrict %>"
                    VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RESULT" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ErrorDescription %>"
                    VisibleIndex="4">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="10">
            </SettingsPager>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>"></SettingsText>
        </cc:ASPxGridView>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>"
                            OnClick="btnCommit_Click" />
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                            <ClientSideEvents Click="function(s, e) { hidePopupWindow(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
