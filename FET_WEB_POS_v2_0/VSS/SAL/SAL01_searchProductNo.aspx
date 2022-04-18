<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_searchProductNo.aspx.cs"
    Inherits="VSS_SAL_SAL01_searchProductNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品編號查詢</title>

    <script type="text/javascript">
        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }
    </script>

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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--商品編號-->
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox1" runat="server">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--商品名稱-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox6" runat="server">
                            </dx:ASPxTextBox>
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
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                OnClick="btnSearch_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){form1.reset();}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <dx:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" AutoGenerateColumns="False"
                        KeyFieldName="商品編號" Width="100%">
                        <Columns>
                            <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                                <DataItemTemplate>
                                    <dx:ASPxRadioButton ID="radioChoose" runat="server" ClientInstanceName="rc1" GroupName="radioChoose">
                                        <ClientSideEvents Init="OnInit" />
                                    </dx:ASPxRadioButton>
                                </DataItemTemplate>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="庫存" Caption="<%$ Resources:WebResources, StockQuantity %>"
                                VisibleIndex="3" />
                            <dx:GridViewDataColumn FieldName="價格" Caption="<%$ Resources:WebResources, Price %>"
                                VisibleIndex="4" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                        <Settings ShowVerticalScrollBar="false" VerticalScrollableHeight="150" VerticalScrollBarStyle="Standard" />
                    </dx:ASPxGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, OK %>"
                                AutoPostBack="false" Visible="false">
                                <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                AutoPostBack="false" Visible="false">
                                <ClientSideEvents Click="function(s, e){window.close();return false;}" />
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
