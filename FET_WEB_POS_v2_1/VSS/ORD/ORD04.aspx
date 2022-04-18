<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ORD04.aspx.cs" Inherits="VSS_ORD_ORD04" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
        <tr>
            <td align="left">
                <!--調整訂單作業-->
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderAdjustment %>"></asp:Literal>
            </td>
        </tr>
    </table>
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
                                <dx:ListEditItem Text="全選" Value="0" Selected="true" />
                                <dx:ListEditItem Text="北一區" Value="1" />
                                <dx:ListEditItem Text="北二區" Value="2" />
                                <dx:ListEditItem Text="中一區" Value="3" />
                                <dx:ListEditItem Text="南一區" Value="4" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--訂單編號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="2">
                        <table style="width: 250px">
                            <td>
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtOrdNoStart" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtOrdNoEnd" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </table>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <tr>
                        <td class="tdtxt">
                            <!--門市編號-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtbox1" runat="server" Text="" Width="120px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--訂單日期-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <table style="width: 250px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <div style="width: 120px;">
                                            <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server">
                                                <ValidationSettings CausesValidation="false">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt">
                            <!-- 更新日期 -->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Literal ID="Literal6" runat="server" Text=""></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--商品編號-->
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Text="" Width="120px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--訂單狀態-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                                <Items>
                                    <dx:ListEditItem Text="ALL" Value="0" />
                                    <dx:ListEditItem Text="預訂" Value="1" />
                                    <dx:ListEditItem Text="正式" Value="2" />
                                    <dx:ListEditItem Text="已轉入" Value="3" />
                                    <dx:ListEditItem Text="已成單" Value="4" />
                                    <dx:ListEditItem Text="未驗收" Value="5" />
                                    <dx:ListEditItem Text="部分驗收" Value="6" />
                                    <dx:ListEditItem Text="已結案" Value="7" />
                                    <dx:ListEditItem Text="已傳輸" Value="8" Selected="true" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            <!-- 更新人員 -->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Literal ID="Literal8" runat="server" Text="64591 李家駿"></asp:Literal>
                        </td>
                    </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="<%$ Resources:WebResources, Search %>" >
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" 
                                AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                             <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
                <div class="seperate">
                </div>
                <%--<div id="divSummary" runat="server" style="display: none">
                    <table border="0">
                        <tr>
                            <td>
                                <!--商品編號-->
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:ListBox ID="ProductListBox" runat="server" DataTextField="商品名稱" DataValueField="商品編號"
                                    AutoPostBack="true" OnSelectedIndexChanged="ProductListBox_SelectedIndexChanged">
                                </asp:ListBox>
                            </td>
                            <td>
                                <table style="height: 65px; border-collapse: collapse" border="1" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>
                                                <!--可分配量-->
                                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, AllocatableQuantity %>"></asp:Literal>
                                            </th>
                                            <th>
                                                <!--門市調整量-->
                                                <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, StoreAdjustment %>"></asp:Literal>
                                            </th>
                                            <th>
                                                <!--總調整量-->
                                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, TotalAdjustments %>"></asp:Literal>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="10"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAdjustmentQty" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalAdjustmentQty" runat="server" Text="0"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>--%>
                <cc:ASPxGridView ID="gvDetailDV" runat="server" Visible="false" KeyFieldName="商品料號"
                    ClientInstanceName="gvDetailDV" AutoGenerateColumns="False" Width="100%" SettingsEditing-Mode="Inline"
                    Settings-ShowTitlePanel="true">
                    <SettingsEditing Mode="Inline" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>"
                            CellStyle-HorizontalAlign="Right">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                            CellStyle-HorizontalAlign="Left">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="可分配量" Caption="<%$ Resources:WebResources, AllocatableQuantity %>"
                            CellStyle-HorizontalAlign="Right">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="總訂貨量" Caption="<%$ Resources:WebResources, AllocatableQuantity %>"
                            CellStyle-HorizontalAlign="Right">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="總調整量" Caption="<%$ Resources:WebResources, TotalAdjustments %>"
                            CellStyle-HorizontalAlign="Right">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </cc:ASPxGridView>
                <div class="seperate">
                </div>
                <div class="SubEditBlock">
                    <cc:ASPxGridView ID="gvMasterDV" runat="server" ClientInstanceName="gvMasterDV" AutoGenerateColumns="False"
                        Width="100%" KeyFieldName="訂單編號" SettingsEditing-Mode="Inline" OnRowUpdating="gvMasterDV_RowUpdating" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                        OnPageIndexChanged="gvMasterDV_PageIndexChanged" Settings-ShowTitlePanel="false">
                        <Columns>
                            <dx:GridViewCommandColumn ButtonType="Button" Caption=" ">
                                <EditButton Visible="true">
                                </EditButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="訂單日期" Caption="<%$ Resources:WebResources, OrderDate %>"
                                CellStyle-HorizontalAlign="Right">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("訂單日期") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="訂單編號" Caption="<%$ Resources:WebResources, OrderNo %>"
                                CellStyle-HorizontalAlign="Right">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("訂單編號") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="訂單狀態" Caption="<%$ Resources:WebResources, OrderStatus %>"
                                CellStyle-HorizontalAlign="Left">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("訂單狀態") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>"
                                CellStyle-HorizontalAlign="Right">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("門市編號") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>"
                                CellStyle-HorizontalAlign="Left">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("門市名稱") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="訂購量" Caption="<%$ Resources:WebResources, OrderQuantity %>"
                                CellStyle-HorizontalAlign="Right">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("訂購量") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="庫存量" Caption="<%$ Resources:WebResources, StockQuantity %>"
                                CellStyle-HorizontalAlign="Right">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("庫存量") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="業助調整數量" Caption="<%$ Resources:WebResources, AssistantAdjustment %>"
                                CellStyle-HorizontalAlign="Right">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="備註" Caption="<%$ Resources:WebResources, Remark %>">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Templates>
                            <EmptyDataRow>
                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                            </EmptyDataRow>
                        </Templates>
                        <SettingsPager PageSize="5">
                        </SettingsPager>
                    </cc:ASPxGridView>
                </div>
           
    </div>
</asp:Content>
