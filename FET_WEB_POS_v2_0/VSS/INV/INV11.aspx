<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV11.aspx.cs" Inherits="VSS_INV11_INV11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title><</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="func">
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                    <tr>
                        <td align="left">
                            門市盤點作業
                        </td>
                        <td align="right">
                            <dx:ASPxButton ID="Button3" runat="server" Text="盤點查詢作業" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){ document.location='INV10.aspx'; }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--作業類型-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ActivityType %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="2">
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True">列印</asp:ListItem>
                                <asp:ListItem>盤點輸入</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點型態-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockTakingMethod %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True">重盤</asp:ListItem>
                                <asp:ListItem>全盤</asp:ListItem>
                                <asp:ListItem>關帳日盤點</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="tdtxt">
                            &nbsp;
                        </td>
                        <td class="tdval">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點單號-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="Label1" runat="server" Text="SC2101-1007002">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點人員-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CountedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="Label2" runat="server" Text="王小明">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label6" runat="server" Text="10/07/12 15:00">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="Label3" runat="server" Text="2010/07/14">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--門市名稱-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="Label4" runat="server" Text="2101 遠企">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label7" runat="server" Text="64591 李家駿">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="seperate">
        </div>
        <table>
            <tr>
                <td>
                    <dx:ASPxButton ID="Button5" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                        OnClick="Button5_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
        Width="100%" AutoGenerateColumns="false" EnableRowsCache="true" Visible="true">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="項次" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                VisibleIndex="0">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="倉別" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Warehouse %>"
                VisibleIndex="1">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="商品編號" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductCode %>"
                VisibleIndex="2">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductName %>"
                VisibleIndex="3">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="單位" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Unit %>"
                VisibleIndex="4">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="帳上庫存" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, BookInventory %>"
                VisibleIndex="4">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="門市盤點量" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, PhysicalInventory %>"
                VisibleIndex="4">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <DataItemTemplate>
                    <dx:ASPxTextBox ID="txtPhysicyInventory" runat="server" Text='<%# Bind("[門市盤點量]") %>'>
                    </dx:ASPxTextBox>
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="盤差量" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, DifferenceQuantity %>"
                VisibleIndex="4">
                <PropertiesTextEdit>
                    <ReadOnlyStyle>
                        <Border BorderStyle="None" />
                    </ReadOnlyStyle>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsPager PageSize="5" />
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                        OnClick="btnSave_Click" Visible="false" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                        OnClick="btnCancel_Click" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    </div>
</asp:Content>
