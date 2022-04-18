<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV26_inputIMEIData.aspx.cs" Inherits="VSS_INV_INV26_inputIMEIData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IMEI輸入</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="func">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <div class="criteria">
                        <table>
                            <tr>
                                <td>
                                    <!--商品編號-->
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                                </td>
                                <td>
                                    <asp:Label ID="lbPRODNO" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <!--商品名稱-->
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                                </td>
                                <td>
                                    <asp:Label ID="lbPRODNAME" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="seperate"></div>
                    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="SID"
                        Width="100%" 
                        OnPageIndexChanged="grid_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, IMEI %>"
                                ReadOnly="true" />
                        </Columns>
                        <Styles>
                            <EditFormColumnCaption Wrap="False">
                            </EditFormColumnCaption>
                        </Styles>
                        <SettingsEditing EditFormColumnCount="4" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    </cc:ASPxGridView>
                    <div class="seperate"></div>
                    <div class="btnPosition">
                        <table cellpadding="0" cellspacing="0" border="0" align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Ok %>">
                                        <ClientSideEvents Click="function(s, e) { hidePopupWindow();  }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
