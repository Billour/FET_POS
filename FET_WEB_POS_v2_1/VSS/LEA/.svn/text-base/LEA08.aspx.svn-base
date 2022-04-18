<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEA08.aspx.cs" Inherits="VSS_LEA_LEA08"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--手機庫存分類設定-->
                        <asp:Literal ID="Literal2" runat="server" Text="手機庫存分類設定"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--產品類別-->
                            <asp:Literal ID="Literal13" runat="server" Text="產品類別"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="DropDownList1" runat="server" Width="150px">
                                <Items>
                                    <dx:ListEditItem Value="0" Text="ALL" Selected="true" />
                                    <dx:ListEditItem Value="1" Text="手機類" />
                                    <dx:ListEditItem Value="1" Text="其他類" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--產品名稱-->
                            <asp:Literal ID="Literal1" runat="server" Text="產品名稱"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="DropDownList2" runat="server" Width="150px">
                                <Items>
                                    <dx:ListEditItem Value="0" Text="ALL" Selected="true" />
                                    <dx:ListEditItem Value="1" Text="Nokia 6230" />
                                    <dx:ListEditItem Value="2" Text="HTC Desire HD" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td align="right">
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="left">
                        <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <div class="seperate">
            </div>
            <dx:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%" ClientInstanceName="gvMaster" KeyFieldName="產品類別"
                Settings-ShowTitlePanel="true">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="產品類別" HeaderStyle-HorizontalAlign="Center">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="產品名稱" HeaderStyle-HorizontalAlign="Center">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="手機序號" HeaderStyle-HorizontalAlign="Center">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="分類" HeaderStyle-HorizontalAlign="Center">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" align="left">
                            <tr>
                                <td class="tdtxt">
                                    手機分類設定：
                                </td>
                                <td class="tdval">
                                    <dx:ASPxComboBox ID="DropDownList3" runat="server" Width="150px">
                                        <Items>
                                            <dx:ListEditItem Value="0" Text="漫遊租賃" />
                                            <dx:ListEditItem Value="1" Text="維修租賃" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                     <EmptyDataRow>
                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                </EmptyDataRow>
                </Templates>
                
            </dx:ASPxGridView>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td align="right">
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Save %>" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="left">
                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>
