<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON13.aspx.cs" Inherits="VSS_CONS_CON13" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品進貨驗收查詢作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventoryExaminationSearch %>"></asp:Literal>
                </td>                
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" colspan="1">
                        <!--訂單/主配編號-->
                        <dx:ASPxLabel ID="Literal9" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="orderno1" runat="server">
                        <Items>
                        <dx:ListEditItem Text="ALL" Selected="true" />
                        <dx:ListEditItem Text="CAAC12010072800" />
                        <dx:ListEditItem Text="CAAC12010072801" />
                        <dx:ListEditItem Text="CAAC12010072802" />
                        <dx:ListEditItem Text="CAAC12010072803" />
                        <dx:ListEditItem Text="CAAC12010072804" />
                        <dx:ListEditItem Text="CAAC12010072805" />
                        </Items>
                        </dx:ASPxComboBox>
                        
                    </td>
                    <td class="tdtxt">
                        <!--出貨編號-->
                        <dx:ASPxLabel ID="Literal23" runat="server" Text="<%$ Resources:WebResources, DeliveryOrderNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server" Width="110"></dx:ASPxTextBox>
                       
                        </td>                    
                    <td class="tdtxt">
                        <!--驗收狀態-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReceiveStatus %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">       
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Text="全部" Selected="true" />
                               
                                <dx:ListEditItem Text="待進貨" />
                                <dx:ListEditItem Text="已驗收" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>                    
                </tr>
                <tr>
                    <td class="tdtxt"> 
                        <!--商品編號-->
                        <dx:ASPxLabel ID="Literal24" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  />

                        <%--<table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        AutoPostBack="false" SkinID="PopupButton" />
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                
                    <td class="tdtxt">
                        <!--進貨日期-->
                         <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ReceivedDate %>"></dx:ASPxLabel>：
                    </td>
                    <td >
                        <table>
                            <tr>
                                <td><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="dateForm" runat="server"></dx:ASPxDateEdit></td>
                                <td><dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="dateTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>                    
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="AutoCompleteDropDownList1" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="AC1" />
                                <dx:ListEditItem Text="AC2" />
                                <dx:ListEditItem Text="AC3" />
                                <dx:ListEditItem Text="AC4" />
                                <dx:ListEditItem Text="AC5" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox4" runat="server" Width="110"></dx:ASPxTextBox>
                       
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>                    
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Examine %>" Visible="false" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="訂單編號" 
        Width="100%" AutoGenerateColumns="False" 
        EnableRowsCache="False" 
            ondetailrowexpandedchanged="gvMaster_DetailRowExpandedChanged">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="訂單編號" runat="server" Caption="<%$ Resources:WebResources, OrderNo %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="出貨編號" runat="server" Caption="<%$ Resources:WebResources, DeliveryOrderNo %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="廠商名稱" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="驗收狀態" runat="server" Caption="<%$ Resources:WebResources, ReceiveStatus %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="進貨日期" runat="server" Caption="<%$ Resources:WebResources, ReceivedDate %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="人員" runat="server" Caption="<%$ Resources:WebResources, ReceivedBy %>"></dx:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <DetailRow>
                         <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                             Width="100%" EnableRowsCache="true">                                                  
                            <Columns>                                    
                                <dx:GridViewDataTextColumn FieldName="出貨編號" runat="server" Caption="<%$ Resources:WebResources, DeliveryOrderNo %>"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="商品編號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="驗收量" runat="server" Caption="<%$ Resources:WebResources, InspectionQuantity %>"></dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFooter="false" />                                 
                            <SettingsDetail IsDetailGrid="true" />
                            <SettingsPager PageSize="5"></SettingsPager>                                
                            <Templates>
                                <TitlePanel>
                                <!--訂單編號-->
                                
                                    <asp:Literal ID="orderno" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                                    <asp:Label ID="Label5" runat="server" Text="101900074" ></asp:Label>
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
            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>
        
        <div class="seperate"></div>
    </div>
<%--    <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" PopupElementID="btnChooseProduct"
        TargetElementID="TextBox4" LoadingPanelID="lp">
    </cc:ASPxPopupControl>
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
--%></asp:Content>