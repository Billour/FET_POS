<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON09.aspx.cs" Inherits="VSS_CON09_CON09" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   

    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品退倉查詢(總部)-->
                    <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSearch %>"></dx:ASPxLabel>
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
                        <dx:ASPxTextBox ID="TextBox2" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table>
                            <tr>
                                <td><dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="Button3" runat="server" Text="選" ClientSideEvents-Click="function(s,e){openwindow('../INV/INV18_3.aspx',640,300);return false;}" /></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></dx:ASPxLabel>：
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
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--廠商編號-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Selected="true" />
                                <dx:ListEditItem Text="AC1" />
                                <dx:ListEditItem Text="AC2" />
                                <dx:ListEditItem Text="AP1" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品編號-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table>
                            <tr>
                                <td><dx:ASPxTextBox ID="TextBox6" runat="server"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="Button4" runat="server" Text="選" ClientSideEvents-Click="function(s,e){openwindow('../SAL/SAL01_searchProductNo.aspx',640,300);return false;}" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <dx:ASPxLabel ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        <table>
                            <tr>
                                <td>起</td>
                                <td><dx:ASPxDateEdit ID="ReturnWarehousingStartDateFrom" runat="server"></dx:ASPxDateEdit></td>
                                <td>訖</td>
                                <td><dx:ASPxDateEdit ID="ReturnWarehousingStartDateTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table>
                            <tr>
                                <td>起</td>
                                <td><dx:ASPxDateEdit ID="ReturnWarehousingEndDateFrom" runat="server"></dx:ASPxDateEdit></td>
                                <td>訖</td>
                                <td><dx:ASPxDateEdit ID="ReturnWarehousingEndDateTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table>
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                    <td><dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Export %>" /></td>
                </tr>
            </table>            
        </div>
        <div class="seperate">
        </div>
    </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="退倉單號" 
        Width="100%" AutoGenerateColumns="False" 
        EnableRowsCache="False" 
        ondetailrowexpandedchanged="gvMaster_DetailRowExpandedChanged" 
        onhtmldatacellprepared="gvMaster_HtmlDataCellPrepared" 
        onhtmlrowcreated="gvMaster_HtmlRowCreated">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewDataHyperLinkColumn FieldName="退倉單號" VisibleIndex="1">
                </dx:GridViewDataHyperLinkColumn>
                <dx:GridViewDataTextColumn runat="server" FieldName="退倉起日" Caption="<%$ Resources:WebResources, ReturnWarehousingStartDate %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn runat="server" FieldName="退倉訖日" Caption="<%$ Resources:WebResources, ReturnWarehousingEndDate %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn runat="server" FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn runat="server" FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn runat="server" FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                </dx:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <DetailRow>
                         <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                             Width="100%" EnableRowsCache="true">                                                  
                            <Columns>                                    
                                <dx:GridViewDataColumn FieldName="廠商編號" Caption="<%$ Resources:WebResources, SupplierNo %>" />                            
                                <dx:GridViewDataColumn FieldName="廠商名稱" Caption="廠商名稱" />  
                                <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />  
                                <dx:GridViewDataColumn FieldName="門市名稱" Caption="門市名稱" />  
                                <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />  
                                <dx:GridViewDataColumn FieldName="商品名稱" Caption="商品名稱" />  
                                <dx:GridViewDataColumn FieldName="庫存數量" Caption="庫存數量" />  
                                <dx:GridViewDataColumn FieldName="實際退倉數量" Caption="實際退倉數量" />                       
                            </Columns>
                            <Settings ShowFooter="false" />                                 
                            <SettingsDetail IsDetailGrid="true" />
                            <SettingsPager PageSize="5"></SettingsPager>                                
                            <Templates>
                                <TitlePanel>
                                    移撥單號：<asp:Label ID="Label5" runat="server" Text="ST2013-100712001" ></asp:Label>
                                </TitlePanel>                                
                            </Templates>
                            <Styles>
                                <TitlePanel Font-Size="Small" HorizontalAlign="Left"></TitlePanel>
                            </Styles>
                        </cc:ASPxGridView>
                </DetailRow>
                <EmptyDataRow>
                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                 </EmptyDataRow>                 
            </Templates>
            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
            <SettingsPager PageSize="5" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView> 
</asp:Content>