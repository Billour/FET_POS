<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON19.aspx.cs" Inherits="VSS_CONS_CON19" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品移出查詢作業-->
                    <asp:Literal ID="Literal01" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferOutSearch %>">
                    </asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>               
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Productcode %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox2" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--移撥狀態-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList1" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Selected="true" />
                                <dx:ListEditItem Text="在途" />
                                <dx:ListEditItem Text="已撥入" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移出日期-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dateFrom" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, end %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dateTo" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval"></td>
                    <td class="tdtxt">
                        <!--撥入門市-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TransferTo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox7" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="移撥單號"
            Width="100%" AutoGenerateColumns="False" EnableRowsCache="False" OnDetailRowExpandedChanged="gvMaster_DetailRowExpandedChanged">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="移撥單號" runat="server" Caption="<%$ Resources:WebResources, TransferSlipNo %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="移出門市" runat="server" Caption="<%$ Resources:WebResources, TransferFrom %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="移出日期" runat="server" Caption="<%$ Resources:WebResources, TransferOutDate %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="撥入門市" runat="server" Caption="<%$ Resources:WebResources, TransferTo %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="撥入日期" runat="server" Caption="<%$ Resources:WebResources, TransferInDate %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="移撥狀態" runat="server" Caption="<%$ Resources:WebResources, TransferStatus %>">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="移出人員" runat="server" Caption="<%$ Resources:WebResources, TransferOutBy %>">
                </dx:GridViewDataTextColumn>
                
            </Columns>
            <Templates>
                <DetailRow>
                    <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                        Width="100%" EnableRowsCache="true">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="商品類別" runat="server" Caption="<%$ Resources:WebResources, ProductCategory %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="撥出數量" runat="server" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>">
                            </dx:GridViewDataTextColumn>
                            
                        </Columns>
                        <Settings ShowFooter="false" />
                        <SettingsDetail IsDetailGrid="true" />
                        <SettingsPager PageSize="5">
                        </SettingsPager>
                        <Templates>
                            <TitlePanel>
                            <!--移撥單號-->
                            <asp:Literal ID="literal33" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>
                                ：<asp:Label ID="Label5" runat="server" Text="CST2010070101"></asp:Label>
                            </TitlePanel>
                        </Templates>
                        <Styles>
                            <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                            </TitlePanel>
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
    </div>
</asp:Content>
