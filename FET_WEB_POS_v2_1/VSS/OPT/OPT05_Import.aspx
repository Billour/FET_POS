<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT05_Import.aspx.cs" Inherits="VSS_OPT_OPT05_Import" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>總部發票匯入</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>"></asp:Literal>：
                        </td>
                        <td align="left">
                            <dx:ASPxUploadControl ID="FileUpload" runat="server">
                            </dx:ASPxUploadControl>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" 
                                OnClick="btnImport_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>--%>
                    <%--<div class="GridScrollBar" style="height: auto">--%>
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="門市編號"
                            Width="100%" OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared" Settings-ShowTitlePanel="true" >
                            <Columns>
                                <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
                                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                                <dx:GridViewDataColumn FieldName="用途" Caption="<%$ Resources:WebResources, use %>" />
                                <dx:GridViewDataColumn FieldName="所屬年月起" Caption="<%$ Resources:WebResources, YearMonthStart %>" />
                                <dx:GridViewDataColumn FieldName="所屬年月訖" Caption="<%$ Resources:WebResources, YearMonthEnd %>" />
                                <dx:GridViewDataColumn FieldName="字軌" Caption="<%$ Resources:WebResources, WordTracks %>" />
                                <dx:GridViewDataColumn FieldName="起始編號" Caption="<%$ Resources:WebResources, StartingNumber %>" />
                                <dx:GridViewDataColumn FieldName="終止編號" Caption="<%$ Resources:WebResources, EndNumber %>" />
                                <dx:GridViewDataColumn FieldName="異常原因"
                                    Caption="<%$ Resources:WebResources, ErrorDescription %>" />
                            </Columns>
                            
                            <SettingsPager PageSize="5">
                            </SettingsPager>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </cc:ASPxGridView>
                        
                    <%--</div>--%>
                    <div class="seperate">
                        <table  align="right">
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
                    <div class="seperate">
                    </div>
                <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnImport" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>--%>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                    <ContentTemplate>--%>
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
                                        <ClientSideEvents Click="function(s, e) {
                                      window.close();e.returnValue =false;          
                                    }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
