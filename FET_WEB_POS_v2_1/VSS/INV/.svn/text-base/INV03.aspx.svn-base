<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV03.aspx.cs" Inherits="VSS_INV_INV03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--庫存查詢作業-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, StockSearch %>"></asp:Literal>
                    </td>
                    <td align="right">
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                    </td>
                    <td>
                        <div style="width: 220px;">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" SelectedIndex="1">
                                <Items>
                                    <dx:ListEditItem Text="ALL" Value="ALL" />
                                    <dx:ListEditItem Text="北一區" Selected="True" Value="北一區" />
                                    <dx:ListEditItem Text="北二區" Value="北二區" />
                                    <dx:ListEditItem Text="中二區" Value="中二區" />
                                    <dx:ListEditItem Text="南一區" Value="南一區" />
                                </Items>
                                <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </div>
                    </td>
                    <td>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="TextBox4" runat="server" Text="2101" Width="100px">
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server" Text="遠企" Width="100px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Litera20" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px">
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="TextBox1" runat="server" Width="100px">
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="TextBox2" runat="server" Width="100px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Warehouse %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ASPxComboBox3" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" />
                                <dx:ListEditItem Text="銷售倉" Selected="true" />
                                <dx:ListEditItem Text="展示倉" />
                                <dx:ListEditItem Text="維修倉" />
                                <dx:ListEditItem Text="租賃倉" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="區域別"
            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
            OnPageIndexChanged="gvMaster_PageIndexChanged">
            <Columns>
                <dx:GridViewDataColumn FieldName="區域別" Caption="<%$ Resources:WebResources, ByDistrict %>"
                    CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>"
                    CellStyle-HorizontalAlign="Right" />
                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>"
                    CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataColumn FieldName="商品類別" Caption="<%$ Resources:WebResources, ProductCategory %>"
                    CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                    CellStyle-HorizontalAlign="Right" />
                <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                    CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataColumn FieldName="出貨倉別" Caption="<%$ Resources:WebResources, Warehouse %>"
                    CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>"
                    CellStyle-HorizontalAlign="Right" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsDetail ShowDetailRow="false" AllowOnlyOneMasterRowExpanded="false" />
            <SettingsPager PageSize="10">
            </SettingsPager>
        </cc:ASPxGridView>
    </div>
</asp:Content>
