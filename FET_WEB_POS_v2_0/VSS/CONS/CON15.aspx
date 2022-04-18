<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON15.aspx.cs" Inherits="VSS_CON15_CON15" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品盤點查詢作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductInventorySearch %>"></asp:Literal>
                </td>
                <td align="right">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--盤點單號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server"></dx:ASPxTextBox>
                    </td>
                   <td class="tdtxt">
                        <!--門市編號-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" >
                        <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--盤點狀態-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, InventoryStatus %>"></dx:ASPxLabel>：
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
                <tr>
                    <td class="tdtxt">
                        <!--盤點日期-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></dx:ASPxLabel>：
                    </td>
                      <td class="tdval" colspan="2">
                          <table>
                              <tr>
                                  <td><dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                  <td><dx:ASPxDateEdit ID="dateFrom" runat="server"></dx:ASPxDateEdit></td>
                                  <td><dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                  <td><dx:ASPxDateEdit ID="dateTo" runat="server"></dx:ASPxDateEdit></td>
                              </tr>
                          </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table>
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>            
        </div>
        <div class="seperate">
        </div>
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>--%>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次" 
                Width="100%" AutoGenerateColumns="False" 
                EnableRowsCache="False">
                    <Columns>
                        <dx:GridViewDataHyperLinkColumn FieldName="盤點單號" runat="server" Caption="<%$ Resources:WebResources, InventoryNo %>">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="CON16.aspx?dno={0}">
                            </PropertiesHyperLinkEdit>
                        </dx:GridViewDataHyperLinkColumn>
                        <dx:GridViewDataTextColumn FieldName="盤點日期" runat="server" Caption="<%$ Resources:WebResources, InventoryDate %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="盤點狀態" runat="server" Caption="<%$ Resources:WebResources, InventoryStatus %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="人員" runat="server" Caption="盤點人員"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="日期" runat="server" Caption="<%$ Resources:WebResources,ModifiedDate %>"></dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="5" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                </cc:ASPxGridView>
                
                <div class="seperate"></div>
               <%-- <div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false">
                    <div class="SubEditCommand">
                        <asp:Label ID="Label5" runat="server" Text="盤點單號:STC2010072801" ForeColor="White"></asp:Label>
                    </div>
                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        Visible="False">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                    <!--商品編號->
                                     <dx:ASPxLabel ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></dx:ASPxLabel>
                                </th>
                                <th scope="col">
                                    <!--商品名稱-->
                                     <dx:ASPxLabel ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></dx:ASPxLabel>
                                </th>
                               <th scope="col">
                                    <!--廠商編號-->
                                     <dx:ASPxLabel ID="Literal13" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>
                                </th>
                                <th scope="col">
                                    <!--廠商名稱-->
                                     <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></dx:ASPxLabel>
                                </th>
                                <th scope="col">
                                    <!--庫存量-->
                                    <dx:ASPxLabel ID="Literal12" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>"></dx:ASPxLabel>
                                </th>
                                <th scope="col">
                                    <!--門市盤點量-->
                                    <dx:ASPxLabel ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PhysicalInventory %>"></dx:ASPxLabel>
                                </th>
                                <th scope="col">
                                    <!--盤差量-->
                                    <dx:ASPxLabel ID="Literal10" runat="server" Text="<%$ Resources:WebResources, DifferenceQuantity %>"></dx:ASPxLabel>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="9" class="tdEmptyData">
                                    <!--此無明細資料-->
                                    <dx:ASPxLabel ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                             <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources ,ProductName %>">
                                <EditItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:BoundField DataField="廠商編號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" ReadOnly="true" />
                            <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" ReadOnly="true" />
                            <asp:BoundField DataField="庫存量" HeaderText="<%$ Resources:WebResources, StockQuantity %>" />
                            <asp:BoundField DataField="門市盤點量" HeaderText="<%$ Resources:WebResources, PhysicalInventory %>" />
                            <asp:BoundField DataField="盤差量" HeaderText="<%$ Resources:WebResources, DifferenceQuantity %>" />
                        </Columns>
                    </asp:GridView>
                </div>--%>
           <%-- </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>
        
        
    </div>
</asp:Content>