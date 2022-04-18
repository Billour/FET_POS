<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON12.aspx.cs" Inherits="VSS_CON12_CON12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品退倉設定作業(門市)-->                       
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousing %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="Button1" runat="server" 
                            Text="<%$ Resources:WebResources, QueryEdit %>" onclick="Button1_Click" 
                            Width="75px"/>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉單號-->
                            <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></dx:ASPxLabel>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                    <dx:ListEditItem Text="COR2010072101" />
                                    <dx:ListEditItem Text="COR2010072102" />
                                    <dx:ListEditItem Text="COR2010072103" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉日期-->
                             <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, WarehousedDate %>"></dx:ASPxLabel>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="lblReturnDate" runat="server"> </asp:Label>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--狀態-->
                            <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></dx:ASPxLabel>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                             <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Label ID="Label2" runat="server" Text="00 未存檔"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉起日-->
                             <dx:ASPxLabel ID="Literal5" runat="server" Text="退倉起日"></dx:ASPxLabel>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label6" runat="server" Text="2010/07/01"></dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉訖日-->
                             <dx:ASPxLabel ID="Literal6" runat="server" Text="退倉訖日"></dx:ASPxLabel>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label8" runat="server" Text="2010/07/01"></dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期-->
                            <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></dx:ASPxLabel>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label3" runat="server" Text="10/08/31 15:00"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員-->
                            <dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></dx:ASPxLabel>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label4" runat="server" Text="64591 李家駿"></dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="GridScrollBar" style="height: auto">
                        <dx:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次" 
                        Width="100%" AutoGenerateColumns="False" 
                        EnableRowsCache="False">
                            <Columns>
                                <dx:GridViewDataTextColumn runat="server" FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn runat="server" FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn runat="server" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn runat="server" FieldName="廠商編號" Caption="<%$ Resources:WebResources, SupplierNo %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn runat="server" FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn runat="server" FieldName="庫存數量" Caption="<%$ Resources:WebResources, StockQuantity %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn runat="server" FieldName="實際退倉數量" Caption="<%$ Resources:WebResources, ActualRetunedQuantity %>">
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="TextBox3" runat="server" Text='<%# Bind("實際退倉數量") %>'></dx:ASPxTextBox>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager PageSize="5" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </dx:ASPxGridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" /></td>
                        <td>&nbsp;</td>
                        <td><dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                        <td>&nbsp;</td>
                        <td><dx:ASPxButton ID="Button4" runat="server" Text="列印簽收單" /></td>
                    </tr>
                </table>
            </div>
        </div>
    
</asp:Content>