<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON15.aspx.cs" Inherits="VSS_CONS_CON15" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品盤點查詢作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductInventorySearch %>"></asp:Literal>
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--盤點單號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--盤點日期-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dateFrom" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dateTo" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--盤點狀態-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, InventoryStatus %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList1" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Selected="true" />
                                <dx:ListEditItem Text="未盤點" />
                                <dx:ListEditItem Text="已盤點" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="盤點單號"
            Width="100%" AutoGenerateColumns="False">
            <Columns>
                <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, InventoryNo %>">
                    <DataItemTemplate>
                        <asp:LinkButton ID="commandButton" runat="server" OnCommand="CommandButton_Click"
                            Text='<%# Eval("盤點單號") %>' CommandName="Select" CommandArgument='<%# Eval("盤點單號") %>' />
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="盤點日期" runat="server" Caption="<%$ Resources:WebResources, InventoryDate %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="盤點狀態" Caption="<%$ Resources:WebResources, InventoryStatus %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="人員" runat="server" Caption="<%$ Resources:WebResources, CountedBy %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="日期" runat="server" Caption="<%$ Resources:WebResources,ModifiedDate %>">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="5" />
            <SettingsEditing Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>
        <div class="seperate">
        </div>
        <div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false">
            <cc:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" KeyFieldName="商品編號"
                Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableRowsCache="False">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="商品編號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="廠商編號" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="廠商名稱" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="庫存量" runat="server" Caption="<%$ Resources:WebResources, StockQuantity %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="門市盤點量" runat="server" Caption="<%$ Resources:WebResources,PhysicalInventory %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="盤差量" runat="server" Caption="<%$ Resources:WebResources, DifferenceQuantity %>">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <div style="text-align: left">
                            <asp:Label ID="Label5" runat="server" Text="盤點單號:STC2010072801" ForeColor="White"
                                Font-Size="Small"></asp:Label>
                        </div>
                    </TitlePanel>
                </Templates>
                <SettingsPager PageSize="5" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
            </cc:ASPxGridView>
        </div>
    </div>
</asp:Content>
