<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ORD04.aspx.cs" Inherits="VSS_ORD04_ORD04" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--調整訂單作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderAdjustment %>"></asp:Literal>
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
                        <dx:aspxcombobox id="ddlArea" runat="server" width="120px">
                            <Items>
                                <dx:ListEditItem Text="全選" Value="0" />
                                <dx:ListEditItem Text="北一區" Value="1" />
                                <dx:ListEditItem Text="北二區" Value="2" />
                                <dx:ListEditItem Text="中一區" Value="3" />
                                <dx:ListEditItem Text="南一區" Value="4" />
                            </Items>
                        </dx:aspxcombobox>
                    </td>
                    <td class="tdtxt">
                        <!--訂單狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:aspxcombobox id="ASPxComboBox1" runat="server" width="120px">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="0" />
                                <dx:ListEditItem Text="預訂" Value="1" />
                                <dx:ListEditItem Text="正式" Value="2" />
                                <dx:ListEditItem Text="已轉入" Value="3" />
                                <dx:ListEditItem Text="已成單" Value="4" />
                                <dx:ListEditItem Text="未驗收" Value="5" />
                                <dx:ListEditItem Text="部分驗收" Value="6" />
                                <dx:ListEditItem Text="已結案" Value="7" />
                                <dx:ListEditItem Text="已傳輸" Value="8" />
                            </Items>
                        </dx:aspxcombobox>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label6" runat="server" Text="10/07/12 15:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table style="width: 250px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxtextbox id="txtStoreNoStart" runat="server" width="150px">
                                    </dx:aspxtextbox>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxtextbox id="txtStoreNoEnd" runat="server" width="150px">
                                    </dx:aspxtextbox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label7" runat="server" Text="64591 李家駿"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table style="width: 250px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxtextbox id="txtPrdtNoStart" runat="server" width="150px">
                                    </dx:aspxtextbox>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxtextbox id="txtPrdtNoEnd" runat="server" width="150px">
                                    </dx:aspxtextbox>
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
                        <!--訂單編號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table style="width: 250px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxtextbox id="txtOrdNoStart" runat="server" width="150px">
                                    </dx:aspxtextbox>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxtextbox id="txtOrdNoEnd" runat="server" width="150px">
                                    </dx:aspxtextbox>
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
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table style="width: 240px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxdateedit id="txtOrdDateStart" runat="server">
                                    </dx:aspxdateedit>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:aspxdateedit id="txtOrdDateEnd" runat="server">
                                    </dx:aspxdateedit>
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
        <div class="seperate"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <table align="center">
                    <tr>
                        <td>
                            <dx:aspxbutton id="btnSearch" onclick="btnSearch_Click" runat="server" text="<%$ Resources:WebResources, Search %>">
                                            </dx:aspxbutton>
                        </td>
                        <td>
                            <dx:aspxbutton id="btnReset" runat="server" text="<%$ Resources:WebResources, Reset %>">
                                            </dx:aspxbutton>
                        </td>
                    </tr>
                </table>
                <div class="seperate"></div>
                <div id="divSummary" runat="server" style="display: none">
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
                </div>
                <div class="SubEditBlock">
                    <cc:aspxgridview id="gvMasterDV" runat="server" clientinstancename="gvMasterDV" autogeneratecolumns="False"
                        width="100%" keyfieldname="訂單編號" settingsediting-mode="Inline" onrowupdating="gvMasterDV_RowUpdating"
                        onpageindexchanged="gvMasterDV_PageIndexChanged" settings-showtitlepanel="false">
                        <Columns>
                            <dx:GridViewCommandColumn ButtonType="Button">
                                <EditButton Visible="true" Text="編輯">
                                </EditButton>
                                <UpdateButton Text="更新">
                                </UpdateButton>
                                <CancelButton Text="取消">
                                </CancelButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="訂單日期" Caption="<%$ Resources:WebResources, OrderDate %>">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("訂單日期") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="訂單編號" Caption="<%$ Resources:WebResources, OrderNo %>">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("訂單編號") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="訂單狀態" Caption="<%$ Resources:WebResources, OrderStatus %>">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("訂單狀態") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("門市編號") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("門市名稱") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="訂購量" Caption="<%$ Resources:WebResources, OrderQuantity %>">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("訂購量") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="庫存量" Caption="<%$ Resources:WebResources, StockQuantity %>">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("庫存量") %>'>
                                    </dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="業助調整數量" Caption="<%$ Resources:WebResources, AssistantAdjustment %>">
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
                    </cc:aspxgridview>
                </div>
            </ContentTemplate>
            <%--      <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>
