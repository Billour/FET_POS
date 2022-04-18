<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_inputPreOrderNumber.aspx.cs"
    Inherits="VSS_SAL_SAL01_inputPreOrderNumber" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>預購單號查詢</title>

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
        <div class="criteria">
            <div class="seperate">
            </div>
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--預購單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderNumber %>" />：
                    </td>
                    <td class="tdval" colspan="3">
                        <dx:ASPxTextBox ID="TextBox2" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--客戶身分證號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, CustomersIdentityNumber %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="tbID" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--客戶門號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="tbMSISDN" runat="server">
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
                            AutoPostBack="false" UseSubmitBehavior="false">
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
                    Width="100%">
                    <Columns>
                        <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                            <DataItemTemplate>
                                <dx:ASPxRadioButton ID="radioChoose" runat="server" ClientInstanceName="rc1" GroupName="radioChoose">
                                    <ClientSideEvents Init="OnInit" />
                                </dx:ASPxRadioButton>
                            </DataItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn FieldName="預購單號" Caption="<%$ Resources:WebResources, PreOrderNumber %>"
                            VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="客戶身分證號" Caption="<%$ Resources:WebResources, CustomersIdentityNumber %>"
                            VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="客戶姓名" Caption="<%$ Resources:WebResources, CustomerName %>"
                            VisibleIndex="3" />
                        <dx:GridViewDataColumn FieldName="客戶門號" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                            VisibleIndex="4" />
                        <dx:GridViewDataColumn FieldName="預購金額" Caption="<%$ Resources:WebResources, PreOrderCash %>"
                            VisibleIndex="5" />
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                </dx:ASPxGridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" 
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e){window.close();return false;}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
