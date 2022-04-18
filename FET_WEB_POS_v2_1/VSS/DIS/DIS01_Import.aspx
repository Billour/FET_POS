<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DIS01_Import.aspx.cs" Inherits="VSS_DIS_DIS01_Import" %>

<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>指定商品上傳</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div class="criteria">
            <table>
                <tr>
                    <td align="right">
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>">
                        </dx:ASPxLabel>
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
            <table align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>"
                           OnClick="btnImport_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvProduct" ClientInstanceName="gvProduct" runat="server" KeyFieldName="商品料號"
            Width="100%" AutoGenerateColumns="False" OnHtmlRowPrepared="gvProduct_HtmlRowPrepared">
            
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
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" >
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
