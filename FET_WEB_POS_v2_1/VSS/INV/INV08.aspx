<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV08.aspx.cs" Inherits="VSS_INV_INV08" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--進貨驗收作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReceivingInspection %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--PO/OE_NO-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, POOENO %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="txt1" runat="server" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--供貨商-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Supplier %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                <Items>
                                    <dx:ListEditItem Text="請選擇" Selected="true" />
                                    <dx:ListEditItem Text="供貨商1" />
                                    <dx:ListEditItem Text="供貨商2" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--訂單狀態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                <Items>
                                    <dx:ListEditItem Text="請選擇" Selected="true" />
                                    <dx:ListEditItem Text="未驗收" />
                                    <dx:ListEditItem Text="部分驗收" />
                                    <dx:ListEditItem Text="已結案" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--訂單/主配編號-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox6" runat="server" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--驗收日期-->
                            <span style="color: Red">*</span><asp:Literal ID="Literal6" runat="server" Text=" <%$ Resources:WebResources, AcceptanceDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <div style="width:120px;">
                                            <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                                <ValidationSettings CausesValidation="false">                                                
                                                    <RequiredField IsRequired="true" />                                                       
                                                </ValidationSettings>                                                
                                            </dx:ASPxDateEdit>
                                        </div>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <div style="width:120px;">
                                            <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                                <ValidationSettings CausesValidation="false">                                                
                                                    <RequiredField IsRequired="true" />                                                       
                                                </ValidationSettings>                                                
                                            </dx:ASPxDateEdit>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--商品料號-->
                            <asp:Literal ID="Literal10" runat="server" Text=" <%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="seperate"></div>
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
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PO/OE_NO"
            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
            OnPageIndexChanged="gvMaster_PageIndexChanged" OnCustomUnboundColumnData="gvMaster_CustomUnboundColumnData">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="PO/OE_NO">
                    <DataItemTemplate>
                        <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server">
                        </dx:ASPxHyperLink>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="訂單編號" Caption="<%$ Resources:WebResources, OrderNoOrDistributionNo %>" />
                <dx:GridViewDataTextColumn FieldName="驗收單編號">
                    <DataItemTemplate>
                        <dx:ASPxHyperLink ID="ASPxHyperLink2" runat="server">
                        </dx:ASPxHyperLink>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, SToreno %>" />
                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, STorename %>" />
                <dx:GridViewDataColumn FieldName="訂單狀態" Caption="<%$ Resources:WebResources, OrderStatus %>" />
                <dx:GridViewDataColumn FieldName="驗收日期" Caption="<%$ Resources:WebResources, AcceptanceDate %>" />
                <dx:GridViewDataColumn FieldName="人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                <dx:GridViewDataColumn FieldName="日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="5">
            </SettingsPager>
        </cc:ASPxGridView>
    </div>
</asp:Content>
