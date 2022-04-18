<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON11.aspx.cs" Inherits="VSS_CON11_CON11" %>

<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品退倉查詢(門市) -->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSearch_Store %>"></asp:Literal>
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
                        <dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                    <td class="tdtxt" nowrap="nowrap">
                     <!--狀態-->
                         <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Status %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="DropDownList1" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="已存檔" />
                                <dx:ListEditItem Text="轉單中" />
                                <dx:ListEditItem Text="已成單" />
                                <dx:ListEditItem Text="已驗退" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdval"></td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉起日-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="退倉起日"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="StartDateFrom" runat="server" ></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="StartDateTo" runat="server" ></dx:ASPxDateEdit></td>
                            </tr>
                        </table>                        
                    </td>
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉訖日-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="退倉訖日"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="EndDateFrom" runat="server" ></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="EndDateTo" runat="server" ></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt"> </td>
                    <td class="tdval"> </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--實際退倉日期-->
                        <dx:ASPxLabel ID="Literal9" runat="server" Text="實際退倉日期"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ></dx:ASPxDateEdit></td>
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

        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="退倉單號" 
                    Width="100%" AutoGenerateColumns="False" 
                    EnableRowsCache="False" 
                    ondetailrowexpandedchanged="gvMaster_DetailRowExpandedChanged" 
                    onhtmldatacellprepared="gvMaster_HtmlDataCellPrepared">
                    <Columns>
                        <dx:GridViewDataHyperLinkColumn FieldName="退倉單號" runat="server" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></dx:GridViewDataHyperLinkColumn>
                        <dx:GridViewDataTextColumn FieldName="退倉起日" runat="server" Caption="退倉起日"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="退倉訖日" runat="server" Caption="退倉訖日"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="退倉日期" runat="server" Caption="<%$ Resources:WebResources, WarehousedDate %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" Caption="<%$ Resources:WebResources, ModifiedBy %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" Caption="<%$ Resources:WebResources, ModifiedDate %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="狀態" runat="server" Caption="<%$ Resources:WebResources, Status %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="備註" runat="server" Caption="<%$ Resources:WebResources, Remark %>"></dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                                 <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                                     Width="100%" EnableRowsCache="true">                                                  
                                    <Columns>                                    
                                        <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="廠商編號" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="廠商名稱" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="庫存數量" runat="server" Caption="<%$ Resources:WebResources, StockQuantity %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="實際退倉數量" runat="server" Caption="<%$ Resources:WebResources, ActualRetunedQuantity %>"></dx:GridViewDataTextColumn>                      
                                    </Columns>
                                    <Settings ShowFooter="false" />                                 
                                    <SettingsDetail IsDetailGrid="true" />
                                    <SettingsPager PageSize="5"></SettingsPager>                                
                                    <Templates>
                                        <TitlePanel>
                                            退倉單號：<asp:Label ID="Label5" runat="server" Text="COR2010073001" ></asp:Label>
                                        </TitlePanel>                                
                                    </Templates>
                                    <Styles>
                                        <TitlePanel Font-Size="Small" HorizontalAlign="Left"></TitlePanel>
                                    </Styles>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                        </DetailRow>               
                    </Templates>
                    <SettingsPager PageSize="5" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                </cc:ASPxGridView>                  
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>