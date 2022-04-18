<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON21.aspx.cs" Inherits="VSS_CON21_CON21" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                    <!--寄銷商品撥入作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferIn %>"></asp:Literal>
                </td>
               
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="1">
                        <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                       <dx:ASPxLabel ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox2" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <dx:ASPxLabel ID="Literal18" runat="server" 
                            Text="<%$ Resources:WebResources, TransferStatus %>"></dx:ASPxLabel>：                    
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Selected="true" />
                                <dx:ListEditItem Text="在途中" />
                                <dx:ListEditItem Text="已撥入" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">                        
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="TransferOutDateFrom" runat="server"></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="TransferOutDateTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                  <td class="tdtxt">
                        <dx:ASPxLabel ID="Literal19" runat="server" 
                            Text="<%$ Resources:WebResources, Transferfrom %>"></dx:ASPxLabel>：</td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                       
                       <dx:ASPxLabel ID="Literal17" runat="server" 
                            Text="<%$ Resources:WebResources, TransferInDate %>"></dx:ASPxLabel>：
                    </td>
                   <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt"><dx:ASPxLabel ID="Literal3" runat="server" 
                            Text="<%$ Resources:WebResources, Transferto %>"></dx:ASPxLabel>：</td>
                    <td class="tdval">2101</td>   
                </tr>              
            </table>
        </div>
       
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
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" 
            runat="server" KeyFieldName="移撥單號" 
                Width="100%" AutoGenerateColumns="False" 
                EnableRowsCache="False" 
            ondetailrowexpandedchanged="gvMaster_DetailRowExpandedChanged">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="移撥單號" runat="server" Caption="<%$ Resources:WebResources, TransferSlipNo %>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="移出門市" runat="server" Caption="<%$ Resources:WebResources,TransferFrom%>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="移出時間" runat="server" Caption="移出時間"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="撥入門市" runat="server" Caption="<%$ Resources:WebResources,TransferTo%>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="撥入時間" runat="server" Caption="撥入時間"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="移撥狀態" runat="server" Caption="<%$ Resources:WebResources,TransferStatus%>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" Caption="<%$ Resources:WebResources,ModifiedBy%>"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" Caption="<%$ Resources:WebResources,ModifiedDate%>"></dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                                 <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                                     Width="100%" EnableRowsCache="true">                                                  
                                    <Columns>                                    
                                        <dx:GridViewDataTextColumn FieldName="商品類別" runat="server" 
                                        Caption="<%$ Resources:WebResources, ProductCategory %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" 
                                        Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" 
                                        Caption="<%$ Resources:WebResources, ProductName %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="撥出數量" runat="server" 
                                        Caption="<%$ Resources:WebResources, TransferredOutQuantity %>"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="撥入數量" runat="server" 
                                        Caption="<%$ Resources:WebResources, TransferredInQuantity %>">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="txtTransferredInQuantity" runat="server" Text='' Width="80px"></dx:ASPxTextBox>
                                        </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFooter="false" />                                 
                                    <SettingsDetail IsDetailGrid="true" />
                                    <SettingsPager PageSize="5"></SettingsPager>                                
                                    <Templates>
                                        <TitlePanel>
                                            移撥單號：<asp:Label ID="Label5" runat="server" Text="CST2010070101" ></asp:Label>
                                        </TitlePanel>                                
                                    </Templates>
                                    <Styles>
                                        <TitlePanel Font-Size="Small" HorizontalAlign="Left"></TitlePanel>
                                    </Styles>
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                </cc:ASPxGridView>
                                <div class="btnPosition">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Save %>" />
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, ConfrimTransferIn %>" />
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                        </DetailRow>               
                    </Templates>
                    <SettingsPager PageSize="15" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>        
    </div>
</asp:Content>