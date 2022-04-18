<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL02_searchDiscountNo.aspx.cs"
    Inherits="VSS_SAL_SAL02_searchDiscountNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>促銷代碼查詢</title>

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
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            促銷代碼：
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="TextBox1" runat="server">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            促銷名稱：
                        </td>
                        <td>
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
                        KeyFieldName="促銷代碼" Width="100%">
                        <Columns>
                            <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                                <DataItemTemplate>
                                    <dx:ASPxRadioButton ID="radioChoose" runat="server" ClientInstanceName="rc1" GroupName="radioChoose">
                                        <ClientSideEvents Init="OnInit" />
                                    </dx:ASPxRadioButton>
                                </DataItemTemplate>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataColumn FieldName="促銷代碼" Caption="促銷代碼" VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="促銷名稱" Caption="促銷名稱" VisibleIndex="2" />
                            <dx:GridViewDataDateColumn FieldName="開始日期" Caption="開始日期" VisibleIndex="3">
                                <PropertiesDateEdit DisplayFormatString="{0:yyyy/MM/dd}">
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataDateColumn FieldName="結束日期" Caption="結束日期" VisibleIndex="4">
                                <PropertiesDateEdit DisplayFormatString="{0:yyyy/MM/dd}">
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
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
