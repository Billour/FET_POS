<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PRE03.aspx.cs" Inherits="VSS_PRE_PRE03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--預購活動設定作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderActivitySetting %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--活動代號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ActivityNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--活動名稱-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ActivityName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox8" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--狀態-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label1" runat="server" Text="00-未存檔"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--有效期間-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, EffectiveDuration %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="1">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="postbackDate_TextBox1" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="postbackDate_TextBox2" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--更新日期-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label3" runat="server" Text="2010/07/01 22:00"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--訂金-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Deposit %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>不需要</asp:ListItem>
                        <asp:ListItem>需要</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="tdtxt">
                    <!--最低預購訂金-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, MinimumPreOrderDeposit %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox4" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--維護人員-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div>
        <div class="SubEditBlock">
            <div class="GridScrollBar" style="height: auto">
                <dx:ASPxGridView ID="gvMaster" runat="server" Width="100%">
                    <Columns>
                        <dx:GridViewDataCheckColumn VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </DataItemTemplate>
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>"
                            VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                            VisibleIndex="2" />
                    </Columns>
                    <Templates>
                        <EmptyDataRow>
                            <!--choose add button-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                        </EmptyDataRow>
                        <TitlePanel>
                            <table cellpadding="0" cellspacing="0" border="0" align="left">
                                <tr>
                                    <td align="right">
                                        <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="Button1_Click" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <Settings ShowTitlePanel="True" />
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="right">
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
