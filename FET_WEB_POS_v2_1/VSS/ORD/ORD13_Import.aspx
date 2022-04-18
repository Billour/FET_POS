<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD13_Import.aspx.cs" Inherits="VSS_ORD_ORD13_Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>卡片安全量暨補貨量匯入</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div >
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
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnImport" OnClick="btnImport_Click" runat="server" Text="<%$ Resources:WebResources, Import %>">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s, e){ form1.reset(); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
   <%--         <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>--%>
                    <div class="GridScrollBar" style="height: auto">
                    </div>
                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster"
                        AutoGenerateColumns="False" Width="100%">
                        <SettingsEditing Mode="Inline" />
                        <Columns>
                            <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="卡片群組" Caption="卡片群組">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="安全庫存量" Caption="安全庫存量">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="最低庫存量" Caption="最低庫存量">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="異常原因" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoRecordsImported %>" />
                    </cc:ASPxGridView>                    
                <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnImport" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>--%>
            <div class="seperate">
            </div>
            <div class="btnPosition" runat="server" id="Div1" visible="false">
               <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                    <ContentTemplate>--%>
                    
                        <table align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnUpdate" runat="server" Text="上傳確認" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
	                                        hidePopupWindow();
                                        }" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCalcel" runat="server" AutoPostBack="False"
                                        Text="<%$ Resources:WebResources, Cancel %>">
                                        <ClientSideEvents Click="function(s, e) {
	                                    hidePopupWindow();}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                   <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
