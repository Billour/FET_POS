<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT05_Import.aspx.cs" Inherits="VSS_OPT_OPT05_OPT05_Import" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>總部發票匯入</title>
    <script>
        function DisabledAndRunButton(s, e) {
            var file = document.getElementById("FileUpload1").value;

            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file))) {
                alert("匯入的檔案不正確,須為Excel檔!!");

                var t = document.getElementById("FileUpload1");
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div class="func">
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>"></asp:Literal>：
                        </td>
                        <td align="left">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
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
                            <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="門市編號"
                Width="100%" EnableCallBacks="false" 
                OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared" 
                Settings-ShowTitlePanel="true">
                <Columns>
                    <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" />
                    <dx:GridViewDataColumn FieldName="STORE_NAME" Caption="<%$ Resources:WebResources, StoreName %>" />
                    <dx:GridViewDataColumn FieldName="USE_TYPE" Caption="<%$ Resources:WebResources, use %>" />
                    <dx:GridViewDataColumn FieldName="S_USE_YM" Caption="<%$ Resources:WebResources, YearMonthStart %>" />
                    <dx:GridViewDataColumn FieldName="E_USE_YM" Caption="<%$ Resources:WebResources, YearMonthEnd %>" />
                    <dx:GridViewDataColumn FieldName="LEADER_CODE" Caption="<%$ Resources:WebResources, WordTracks %>" />
                    <dx:GridViewDataColumn FieldName="INIT_NO" Caption="<%$ Resources:WebResources, StartingNumber %>" />
                    <dx:GridViewDataColumn FieldName="END_NO" Caption="<%$ Resources:WebResources, EndNumber %>" />
                    <dx:GridViewDataColumn FieldName="EXCEPTION_CAUSE" Caption="<%$ Resources:WebResources, ErrorDescription %>" />
                </Columns>
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <Settings ShowTitlePanel="True"></Settings>
            </cc:ASPxGridView>
            <div class="seperate">
                <table align="right">
                    <tr>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SuccessfulPlots %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Literal ID="Literal6" runat="server" Text=""></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Plots %>"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, FailedPlots %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Literal ID="Literal7" runat="server" Text=""></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Plots %>"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <div class="seperate"></div>
            
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>"
                                Visible="false" OnClick="btnCommit_Click">
                                <ClientSideEvents Click="function(s, e) {
                                      window.close();e.returnValue =false;          
                                    }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCalcel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                Visible="false">
                                 <ClientSideEvents Click="function(s, e) {hidePopupWindow();}" />
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
