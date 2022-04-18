<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="INV05_Import.aspx.cs" Inherits="VSS_INV_INV05_Import" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退倉設定匯入</title>
    <script>   

        function DisabledAndRunButton(s, e) {
            var file = document.form1.FileUpload.value;
            //var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            var extIndex = file.lastIndexOf('.');  
            var extension;
            if (extIndex != -1) { 
                extension = file.substr(extIndex+1, file.length);
                //if (!(reOKFiles.test(file))) {
                if (extension.toLowerCase() != "xls") {
                    alert("匯入的檔案不正確,須為Excel檔!!");
                    btnCommit.ClientEnabled = false;
                    var t = document.getElementById('FileUpload');
                    t.outerHTML = t.outerHTML;
                } 
                else {
                    if (s.GetEnabled()) {
                        s.SendPostBack('Click');
                        s.SetEnabled(false);
                    }
                }
            }
            else {
                alert("匯入的檔案不正確,須為Excel檔!!");
                btnCommit.ClientEnabled = false;
                var t = document.getElementById('FileUpload');
                t.outerHTML = t.outerHTML;
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
                    <td align="right">
                        <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>" ></dx:ASPxLabel>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload" runat="server"  Width="400"/>
                    </td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClick="btnImport_Click" >
                            <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s);}" />
                        </dx:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                     <td>
                        <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" 
            ActiveTabIndex="0">
            <TabPages>
                <dx:TabPage Text="<%$ Resources:WebResources, Product %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <cc:ASPxGridView ID="gvProduct" ClientInstanceName="gvProduct" runat="server" KeyFieldName="PRODNO"
                                Width="100%" AutoGenerateColumns="False" 
                                OnHtmlRowPrepared="gvProduct_HtmlRowPrepared" 
                                OnPageIndexChanged="gvProduct_PageIndexChanged" >
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" ReadOnly="true" Caption="<%$ Resources:WebResources, ProductCode %>"
                                        VisibleIndex="0">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductName %>"
                                        VisibleIndex="1">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="異常原因" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ErrorDescription %>"
                                        VisibleIndex="2">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                </Columns>                                   
                                <SettingsPager PageSize="10"></SettingsPager>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>"></SettingsText>
                            </cc:ASPxGridView>

                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>

                <dx:TabPage Text="<%$ Resources:WebResources, Store %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <cc:ASPxGridView ID="gvStore" ClientInstanceName="gvStore" runat="server" Width="100%"
                                KeyFieldName="門市編號" AutoGenerateColumns="False" 
                                OnHtmlRowPrepared="gvStore_HtmlRowPrepared" 
                                OnPageIndexChanged="gvStore_PageIndexChanged" >
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, StoreNo %>"
                                        VisibleIndex="0">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                        <CellStyle HorizontalAlign="Left"></CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="門市名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, StoreName %>"
                                        VisibleIndex="1">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="異常原因" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ErrorDescription %>"
                                        VisibleIndex="2">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                </Columns>                                    
                                <SettingsPager PageSize="10"></SettingsPager>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>"></SettingsText>
                            </cc:ASPxGridView>

                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>

        <div class="seperate"></div>

        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnCommit" ClientInstanceName="btnCommit" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>" 
                            OnClick="btnCommit_Click">
                        </dx:ASPxButton>
                    </td>
                    <td><dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false" >
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