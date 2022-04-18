<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON17.aspx.cs" Inherits="VSS_CONS_CON17" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">      

        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--外部廠商月結作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="外部廠商月結作業"></asp:Literal>
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
                        <td class="tdtxt"><!--廠商代號-->
                         <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>：</td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt"><!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal16" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></dx:ASPxLabel>：</td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox2" runat="server"></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                             <!--結算狀態-->
                                <dx:ASPxLabel ID="Literal3" runat="server" Text="結算狀態"></dx:ASPxLabel>：</td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="DropDownList2" runat="server">
                                <Items>
                                    <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                    <dx:ListEditItem Text="未請款" />
                                    <dx:ListEditItem Text="已請款" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt"><!--結算月份-->
                        <dx:ASPxLabel ID="Literal18" runat="server" Text="<%$ Resources:WebResources, SettlementMonth %>"></dx:ASPxLabel>：</td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox4" runat="server"></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt"></td>         
                        <td class="tdval"></td>
                        <td class="tdtxt">
                           <%-- <!--日期-->
                            <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Date %>"></dx:ASPxLabel>：--%>
                        </td>
                        <td class="tdval">
                           <%-- <asp:Label ID="Label1" runat="server" Text="2010/07/01 22:00"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt"></td>
                        <td class="tdval"></td>
                        <td class="tdtxt"></td>
                        <td class="tdval"></td>
                        <td class="tdtxt">
                          <%--  <!--人員-->
                            <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Staff %>"></dx:ASPxLabel>：--%>
                        </td>
                        <td class="tdval">
                           <%-- <asp:Label ID="Label3" runat="server" Text="12345 王大寶"></asp:Label>--%>
                        </td>
                    </tr>                    
                </table>
            </div>   
            <div class="seperate"></div>
            <div class="btnPosition">
                <table>
                    <tr>
                        <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                        <td><dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                        <td><dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" /></td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>      
            <div class="GridScrollBar" style="height: auto">
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="廠商代號" 
                Width="100%" AutoGenerateColumns="False" oncustombuttoncallback="gvMaster_CustomButtonCallback" 
                    oncustombuttoninitialize="gvMaster_CustomButtonInitialize" 
                    ondetailrowexpandedchanged="gvMaster_DetailRowExpandedChanged">
                    <Columns>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="btnPay" Text="請款">
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="廠商代號" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="廠商名稱" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>" VisibleIndex="2"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="結算月份" runat="server" Caption="<%$ Resources:WebResources, SettlementMonth %>" VisibleIndex="3"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="結算起日" VisibleIndex="4">
                            <PropertiesDateEdit DisplayFormatString="{0:yyyy/MM/dd}">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="結算訖日" VisibleIndex="5">
                            <PropertiesDateEdit DisplayFormatString="{0:yyyy/MM/dd}">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="結算金額" runat="server" Caption="<%$ Resources:WebResources, SettlementAmount %>" VisibleIndex="6"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="結算狀態" runat="server" Caption="<%$ Resources:WebResources, SettlementStatus %>" VisibleIndex="7"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" Caption="<%$ Resources:WebResources, ModifiedBy %>" VisibleIndex="8"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="更新日期" VisibleIndex="9">
                            <PropertiesDateEdit DisplayFormatString="{0:yyyy/MM/dd}">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <asp:FormView ID="FormView1" runat="server" DefaultMode="ReadOnly" Width="100%">
                                <ItemTemplate>
                                <table class="mGrid" width="100%">
                                    <tr>
                                        <td class="tdtxt"><!--結算金額-->
                                        <dx:ASPxLabel ID="Literal33" runat="server" Text="<%$ Resources:WebResources, SettlementAmount %>"></dx:ASPxLabel>：</td>
                                        <td colspan="5"><asp:Label ID="Label1" runat="server" Text='<%# Eval("結算金額") %>'></asp:Label></td>
                                    </tr>
                                    <tr>
                    	                <td class="tdtxt"><!--銷貨總額-->
                    	                <dx:ASPxLabel ID="Literal32" runat="server" Text="<%$ Resources:WebResources, GrossSales %>"></dx:ASPxLabel>：</td>
                    	                <td class="tdval"><asp:Label ID="Label2" runat="server" Text='<%# Eval("銷貨總額") %>'></asp:Label></td>
                    	                <td class="tdtxt"><!--銷貨金額-->
                    	                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, SalesAmount %>"></dx:ASPxLabel>：</td>
                    	                <td class="tdval"><asp:Label ID="Label4" runat="server" Text='<%# Eval("銷貨金額") %>'></asp:Label></td>
                    	                <td class="tdtxt"><!--銷貨稅額-->
                    	                <dx:ASPxLabel ID="Literal19" runat="server" Text="<%$ Resources:WebResources, SalesTax %>"></dx:ASPxLabel>：</td>
                    	                <td class="tdval"><asp:Label ID="Label5" runat="server" Text='<%# Eval("銷貨稅額") %>'></asp:Label></td>
                                    </tr>	
                                    <tr>
                                        <td class="tdtxt"><!--佣金總額-->
                                        <dx:ASPxLabel ID="Literal20" runat="server" Text="<%$ Resources:WebResources, GrossCommission %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label6" runat="server" Text='<%# Eval("佣金總額") %>'></asp:Label></td>
                                        <td class="tdtxt"><!--佣金金額-->
                                        <dx:ASPxLabel ID="Literal21" runat="server" Text="<%$ Resources:WebResources, CommissionAmount %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label7" runat="server" Text='<%# Eval("佣金金額") %>'></asp:Label></td>
                                        <td class="tdtxt"><!--佣金稅額-->
                                        <dx:ASPxLabel ID="Literal22" runat="server" Text="<%$ Resources:WebResources, CommissionTax %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label8" runat="server" Text='<%# Eval("佣金稅額") %>'></asp:Label></td>
                                    </tr>	
                                    <tr>
                                        <td class="tdtxt"><!--進貨總額-->
                                        <dx:ASPxLabel ID="Literal23" runat="server" Text="<%$ Resources:WebResources, TotalPurchasingAmount %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label9" runat="server" Text='<%# Eval("進貨總額") %>'></asp:Label></td>
                                        <td class="tdtxt"><!--進貨金額-->
                                        <dx:ASPxLabel ID="Literal24" runat="server" Text="<%$ Resources:WebResources, PurchasingAmount %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label10" runat="server" Text='<%# Eval("進貨金額") %>'></asp:Label></td>
                                        <td class="tdtxt"><!--進貨稅額-->
                                        <dx:ASPxLabel ID="Literal25" runat="server" Text="<%$ Resources:WebResources, PurchasingTax %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label11" runat="server" Text='<%# Eval("進貨稅額") %>'></asp:Label></td>
                                    </tr>	
                                    <tr>
                                        <td class="tdtxt"><!--期末庫存總額-->
                                        <dx:ASPxLabel ID="Literal26" runat="server" Text="<%$ Resources:WebResources, TotalClosingInventoryAmount %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label12" runat="server" Text='<%# Eval("期末庫存總額") %>'></asp:Label></td>
                                        <td class="tdtxt"><!--期末庫存金額-->
                                        <dx:ASPxLabel ID="Literal27" runat="server" Text="<%$ Resources:WebResources, ClosingInventoryAmount %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label13" runat="server" Text='<%# Eval("期末庫存金額") %>'></asp:Label></td>
                                        <td class="tdtxt"><!--期末庫存稅額-->
                                        <dx:ASPxLabel ID="Literal28" runat="server" Text="<%$ Resources:WebResources, ClosingInventoryTax %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label14" runat="server" Text='<%# Eval("期末庫存稅額") %>'></asp:Label></td>
                                    </tr>	
                                    <tr>
                                        <td class="tdtxt"><!--退倉總額-->
                                        <dx:ASPxLabel ID="Literal29" runat="server" Text="<%$ Resources:WebResources, TotalReturnWarehousingAmount %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label15" runat="server" Text='<%# Eval("退倉總額") %>'></asp:Label></td>
                                        <td class="tdtxt"><!--退倉金額-->
                                        <dx:ASPxLabel ID="Literal30" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingAmount %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label16" runat="server" Text='<%# Eval("退倉金額") %>'></asp:Label></td>
                                        <td class="tdtxt"><!--退倉稅額-->
                                        <dx:ASPxLabel ID="Literal31" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingTax %>"></dx:ASPxLabel>：</td>
                                        <td class="tdval"><asp:Label ID="Label17" runat="server" Text='<%# Eval("退倉稅額") %>'></asp:Label></td>
                                    </tr>
                                </table>
                                    
                                </ItemTemplate>
                            
                            </asp:FormView>
                        </DetailRow>
                    </Templates>
                    <SettingsPager PageSize="5" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
                
            </div>
                             
        </div>
    
</asp:Content>