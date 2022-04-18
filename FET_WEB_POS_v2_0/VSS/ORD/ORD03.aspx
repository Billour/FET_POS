<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ORD03.aspx.cs" Inherits="VSS_ORD03_ORD03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--訂單報表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderReport %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--區域別-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlArea" runat="server" Width="120px">
                            <Items>
                                <dx:ListEditItem Text="全選" Value="0" />
                                <dx:ListEditItem Text="北一區" Value="1" />
                                <dx:ListEditItem Text="北二區" Value="2" />
                                <dx:ListEditItem Text="中一區" Value="3" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--訂單狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlOrdStatus" runat="server" Width="50px">
                            <Items>
                                <dx:ListEditItem Text="全選" Value="0" />
                                <dx:ListEditItem Text="完成" Value="1" />
                                <dx:ListEditItem Text="作廢" Value="2" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--訂單編號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table style="width: 250px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtOrdNoStart" runat="server" Width="150px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtOrdNoEnd" runat="server" Width="150px">
                                    </dx:ASPxTextBox>
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
                        <!--店組代碼-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreCategoryCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table style="width: 250px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtStoreNoStart" runat="server" Width="150px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtStoreNoEnd" runat="server" Width="150px">
                                    </dx:ASPxTextBox>
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
                        <!--訂單日期-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table style="width: 250px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" Width="180px">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" Width="180px">
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
                        <!--料號-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, MaterialNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table style="width: 250px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtParNoStart" runat="server" Width="150px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtParNoEnd" runat="server" Width="150px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
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
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
    </div>
</asp:Content>
