<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD10_Import.aspx.cs" Inherits="VSS_ORD_ORD10_Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Non-DropShipment主配商品檔匯入</title>
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
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>"></asp:Literal>：
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
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="GridScrollBar" style="height: auto">
                    </div>
                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster"
                        AutoGenerateColumns="False" Width="100%" onhtmldatacellprepared="gvMasterDV_HtmlDataCellPrepared" 
                       >
                        <SettingsEditing Mode="Inline" />
                        <Columns>
                            <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="主動配貨" Caption="主動配貨">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="異常原因" Caption="<%$ Resources:WebResources, ErrorDescription %>">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoRecordsImported %>" />
                    </cc:ASPxGridView>
                    <div class="seperate">
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnImport" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <table align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnUpdate" Visible="false" runat="server" Text="上傳" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
	                                        hidePopupWindow();
                                        }" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCalcel" Visible="false" runat="server" AutoPostBack="False"
                                        Text="<%$ Resources:WebResources, Cancel %>">
                                        <ClientSideEvents Click="function(s, e) {
	                                    hidePopupWindow();}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
