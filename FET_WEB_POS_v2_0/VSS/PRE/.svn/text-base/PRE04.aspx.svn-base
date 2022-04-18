<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PRE04.aspx.cs" Inherits="VSS_PRE_PRE04" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderActivitySearch %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    活動代號：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    活動名稱：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    狀態：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddlStatus" runat="server" Width="100">
                        <Items>
                            <dx:ListEditItem Value="已生效" Text="已生效" Selected="true" />
                            <dx:ListEditItem Value="尚未生效" Text="尚未生效" />
                            <dx:ListEditItem Value="已過期" Text="已過期" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    有效期間：
                </td>
                <td class="tdval" colspan="1">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    訂金：
                </td>
                <td class="tdval">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem>不需要</asp:ListItem>
                        <asp:ListItem>需要</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="tdtxt">
                    最低預購訂金：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
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
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查詢" OnClick="ASPxButton1_Click">
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="清空">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div>
        <div class="SubEditBlock">
            <div class="GridScrollBar" style="height: auto">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                    KeyFieldName="活動代號">
                    <Columns>
                        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" VisibleIndex="0">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="狀態" Caption="狀態" VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="活動代號" Caption="活動代號" VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="活動名稱" Caption="活動名稱" VisibleIndex="3" />
                        <dx:GridViewDataColumn FieldName="有效期間起始" Caption="有效期間(起)" VisibleIndex="4" />
                        <dx:GridViewDataColumn FieldName="有效期間結束" Caption="有效期間(訖)" VisibleIndex="5" />
                    </Columns>
                    <Templates>
                        <EmptyDataRow>
                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                        </EmptyDataRow>
                    </Templates>
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
</asp:Content>
