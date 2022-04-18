<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD13_Import.aspx.cs" Inherits="VSS_ORD_ORD13_ORD13_Import" %>

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
        }
        function Import(s, e) {
            var rtn = confirm('匯入資料後，會將原本畫面上的資料清除，是否要執行匯入動作?');
            if (!rtn) {
                e.processOnServer = false;
            }
            else {
                hidePopupWindow();
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
                        <dx:ASPxButton ID="btnImport" OnClick="btnImport_Click" runat="server" Text="<%$ Resources:WebResources, Import %>">
                        <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s, e);}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster"
            AutoGenerateColumns="False" Width="100%" KeyFieldName="SID"
            onpageindexchanged="gvMaster_PageIndexChanged">
            <Columns>
                <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="F1" Caption="<%$ Resources:WebResources, StoreNo %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="F2" Caption="<%$ Resources:WebResources, StoreName %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="F3" Caption="卡片群組">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="F7" Caption="<%$ Resources:WebResources, StartDate %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="F8" Caption="<%$ Resources:WebResources, EndDate %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="F4" Caption="安全庫存量">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="F5" Caption="最低庫存量">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="F6" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red">
                </dx:GridViewDataTextColumn>
            </Columns>
           <SettingsPager PageSize="10"></SettingsPager>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoRecordsImported %>" />
        </cc:ASPxGridView>   
        <div class="seperate"></div>
        <div class="btnPosition" runat="server" id="Div1" >
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxButton Visible="false" ID="btnUpdate" runat="server" Text="上傳確認" 
                            onclick="btnUpdate_Click">
                            <ClientSideEvents Click="function(s, e) { Import(s, e); }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton Visible="false" ID="btnCalcel" runat="server" AutoPostBack="False"
                            Text="<%$ Resources:WebResources, Cancel %>">
                            <ClientSideEvents Click="function(s, e) { hidePopupWindow();}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
      
    </form>
</body>
</html>
