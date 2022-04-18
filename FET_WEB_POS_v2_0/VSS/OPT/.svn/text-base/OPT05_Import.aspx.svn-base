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
                            <dx:ASPxUploadControl ID="FileUpload" runat="server"></dx:ASPxUploadControl>
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
                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClick="btnImport_Click"></dx:ASPxButton>
                         </td>
                        <td>&nbsp;</td>
                        <td>
                            <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                        </td>
                    </tr>
                </table>    
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="GridScrollBar" style="height: auto">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
                            KeyFieldName="門市編號" Width="100%" 
                            onhtmldatacellprepared="gvMaster_HtmlDataCellPrepared" >
                             <Columns>                       
                                <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
                                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />  
                                <dx:GridViewDataColumn FieldName="用途" Caption="用途"/>
                                <dx:GridViewDataColumn FieldName="所屬年月起" Caption="所屬年月(起)"/>
                                <dx:GridViewDataColumn FieldName="所屬年月訖" Caption="所屬年月(訖)"/>
                                <dx:GridViewDataColumn FieldName="字軌" Caption="字軌"/>
                                <dx:GridViewDataColumn FieldName="起始編號" Caption="起始編號" />
                                <dx:GridViewDataColumn FieldName="終止編號" Caption="終止編號" />
                                <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ErrorDescription %>" Caption="異常原因" />
                            </Columns>
                              <SettingsPager PageSize="5"></SettingsPager>               
                              <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" /> 
                        </cc:ASPxGridView>
                    
                      <%--  <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowDataBound="gvMaster_RowDataBound">
                            <Columns>                                                                                    
                                <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                                <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />                                
                                <asp:TemplateField HeaderText="用途">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("用途") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="所屬年月(起)">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("所屬年月起") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>     
                                <asp:TemplateField HeaderText="所屬年月(訖)">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("所屬年月訖") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="字軌">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("字軌") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="起始編號">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("起始編號") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="終止編號">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("終止編號") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ErrorDescription %>">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("異常原因") %>' ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("異常原因") %>' ForeColor="Red"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>                                                                    
                            </Columns>
                        </asp:GridView>--%>
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
                        <table cellpadding="0" cellspacing="0" border="0" align="center">
                        <tr>
                            <td>
                                 <dx:ASPxButton ID="btnCommit" runat="server" 
                                     Text="<%$ Resources:WebResources, CommitUpload %>" Visible="false" 
                                     onclick="btnCommit_Click">
                                   <ClientSideEvents Click="function(s, e) {
                                      window.close();e.returnValue =false;          
                                    }" /> 
                                 </dx:ASPxButton>
                             </td>
                            <td>&nbsp;</td>
                            
                            <td>
                                 <dx:ASPxButton ID="btnCalcel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" Visible="false">
                                   <ClientSideEvents Click="function(s, e) {
                                      window.close();e.returnValue =false;          
                                    }" /> 
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
