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
    <div class="func">       
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
                <asp:Button ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClick="btnImport_Click" />
                <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowDataBound="gvMaster_RowDataBound">
                            <Columns>                                                                                    
                                <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                                <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />
                                <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />  
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />    
                                <asp:BoundField DataField="主動配貨" HeaderText="主動配貨" />                                     
                                <%--<asp:TemplateField HeaderText="<%$ Resources:WebResources, Ratio %>" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("比率") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="98" ForeColor="Red"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>    --%>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ErrorDescription %>">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("異常原因") %>' ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("異常原因") %>' ForeColor="Red"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>                                                                    
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="seperate">
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnImport" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate"></div>
            <div class="btnPosition">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="上傳" Visible="false" OnClientClick="window.close();return false;" />
                        <asp:Button ID="btnCalcel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" Visible="false" OnClientClick="window.close();return false;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
