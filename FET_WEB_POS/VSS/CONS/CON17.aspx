<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON17.aspx.cs" Inherits="VSS_CONS_CON17" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
    <div class="func">
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
                         <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：</td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt"><!--廠商名稱-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：</td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                             <!--結算狀態-->
                                <asp:Literal ID="Literal3" runat="server" Text="結算狀態"></asp:Literal>：</td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem Value="">-請選擇-</asp:ListItem>
                                <asp:ListItem>未請款</asp:ListItem>
                                <asp:ListItem>已請款</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt"><!--結算月份-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, SettlementMonth %>"></asp:Literal>：</td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt"></td>         
                        <td class="tdval"></td>
                        <td class="tdtxt">
                           <%-- <!--日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Date %>"></asp:Literal>：--%>
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
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Staff %>"></asp:Literal>：--%>
                        </td>
                        <td class="tdval">
                           <%-- <asp:Label ID="Label3" runat="server" Text="12345 王大寶"></asp:Label>--%>
                        </td>
                    </tr>                    
                </table>
            </div>   
            <div class="seperate"></div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" />
            </div>
            <div class="seperate"></div>      
            <div class="GridScrollBar" style="height: auto">
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" 
                    CssClass="mGrid" onrowcommand="gvMaster_RowCommand" OnRowDataBound="gvMaster_OnRowDataBound" OnRowCreated="gvMaster_RowCreated"> 
                    <EmptyDataTemplate>
                        <tr>                           
                             <th scope="col">
                                <!--廠商代號-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                            </th>
                             <th scope="col">
                                <!--廠商名稱-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--結算月份-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, SettlementMonth %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--結算起日-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, SettlementStartDate %>"></asp:Literal>
                            </th>                           
                            <th scope="col">
                                <!--結算訖日-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SettlementEndDate %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--結算金額-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, SettlementAmount %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--結算狀態-->
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SettlementStatus %>"></asp:Literal>
                            </th>
                             <th scope="col">
                                <!--更新人員-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                            </th>                            
                             <th scope="col">
                                <!--更新日期-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="9" class="tdEmptyData">
                                <!--此無明細資料-->
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button id="btnPay" runat="server" Text="請款" CommandName="Pay"/>
                            </ItemTemplate>    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="廠商代號">                    
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("廠商代號") %>' CommandName="Select" CommandArgument='<%# Bind("廠商代號") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:BoundField DataField="廠商代號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" ReadOnly="true" />--%>
                        <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" ReadOnly="true" />
                        <asp:BoundField DataField="結算月份" HeaderText="<%$ Resources:WebResources, SettlementMonth %>" ReadOnly="true" />
                        <asp:BoundField DataField="結算起日" HeaderText="<%$ Resources:WebResources, SettlementStartDate %>" ReadOnly="true" DataFormatString="{0:yyyy/MM/dd}" />
                        <asp:BoundField DataField="結算訖日" HeaderText="<%$ Resources:WebResources, SettlementEndDate %>" ReadOnly="true" DataFormatString="{0:yyyy/MM/dd}" />
                        <asp:BoundField DataField="結算金額" HeaderText="<%$ Resources:WebResources, SettlementAmount %>" ReadOnly="true" />
                        <asp:BoundField DataField="結算狀態" HeaderText="<%$ Resources:WebResources, SettlementStatus %>" ReadOnly="true" />
                        <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" /> 
                        <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true" DataFormatString="{0:yyyy/MM/dd}" />                                                
                    </Columns>
                </asp:GridView>
            </div>
             <div class="seperate">
            </div>
            <div class="GridScrollBar" style="height: auto">
                <asp:Panel ID="Panel1" runat="server" Visible="false">    
                <asp:FormView ID="FormView1" runat="server" DefaultMode="ReadOnly" Width="100%">
                    <ItemTemplate>
                    <table class="mGrid" width="100%">
                        <tr>
                            <td class="tdtxt"><!--結算金額-->
                            <asp:Literal ID="Literal33" runat="server" Text="<%$ Resources:WebResources, SettlementAmount %>"></asp:Literal>：</td>
                            <td colspan="5"><asp:Label ID="Label1" runat="server" Text='<%# Eval("結算金額") %>'></asp:Label></td>
                        </tr>
                        <tr>
                    	    <td class="tdtxt"><!--銷貨總額-->
                    	    <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, GrossSales %>"></asp:Literal>：</td>
                    	    <td class="tdval"><asp:Label ID="Label2" runat="server" Text='<%# Eval("銷貨總額") %>'></asp:Label></td>
                    	    <td class="tdtxt"><!--銷貨金額-->
                    	    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, SalesAmount %>"></asp:Literal>：</td>
                    	    <td class="tdval"><asp:Label ID="Label4" runat="server" Text='<%# Eval("銷貨金額") %>'></asp:Label></td>
                    	    <td class="tdtxt"><!--銷貨稅額-->
                    	    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, SalesTax %>"></asp:Literal>：</td>
                    	    <td class="tdval"><asp:Label ID="Label5" runat="server" Text='<%# Eval("銷貨稅額") %>'></asp:Label></td>
                        </tr>	
                        <tr>
                            <td class="tdtxt"><!--佣金總額-->
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, GrossCommission %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label6" runat="server" Text='<%# Eval("佣金總額") %>'></asp:Label></td>
                            <td class="tdtxt"><!--佣金金額-->
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, CommissionAmount %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label7" runat="server" Text='<%# Eval("佣金金額") %>'></asp:Label></td>
                            <td class="tdtxt"><!--佣金稅額-->
                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, CommissionTax %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label8" runat="server" Text='<%# Eval("佣金稅額") %>'></asp:Label></td>
                        </tr>	
                        <tr>
                            <td class="tdtxt"><!--進貨總額-->
                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, TotalPurchasingAmount %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label9" runat="server" Text='<%# Eval("進貨總額") %>'></asp:Label></td>
                            <td class="tdtxt"><!--進貨金額-->
                            <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, PurchasingAmount %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label10" runat="server" Text='<%# Eval("進貨金額") %>'></asp:Label></td>
                            <td class="tdtxt"><!--進貨稅額-->
                            <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, PurchasingTax %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label11" runat="server" Text='<%# Eval("進貨稅額") %>'></asp:Label></td>
                        </tr>	
                        <tr>
                            <td class="tdtxt"><!--期末庫存總額-->
                            <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, TotalClosingInventoryAmount %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label12" runat="server" Text='<%# Eval("期末庫存總額") %>'></asp:Label></td>
                            <td class="tdtxt"><!--期末庫存金額-->
                            <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, ClosingInventoryAmount %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label13" runat="server" Text='<%# Eval("期末庫存金額") %>'></asp:Label></td>
                            <td class="tdtxt"><!--期末庫存稅額-->
                            <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, ClosingInventoryTax %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label14" runat="server" Text='<%# Eval("期末庫存稅額") %>'></asp:Label></td>
                        </tr>	
                        <tr>
                            <td class="tdtxt"><!--退倉總額-->
                            <asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, TotalReturnWarehousingAmount %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label15" runat="server" Text='<%# Eval("退倉總額") %>'></asp:Label></td>
                            <td class="tdtxt"><!--退倉金額-->
                            <asp:Literal ID="Literal30" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingAmount %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label16" runat="server" Text='<%# Eval("退倉金額") %>'></asp:Label></td>
                            <td class="tdtxt"><!--退倉稅額-->
                            <asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingTax %>"></asp:Literal>：</td>
                            <td class="tdval"><asp:Label ID="Label17" runat="server" Text='<%# Eval("退倉稅額") %>'></asp:Label></td>
                        </tr>
                    </table>
                        
                    </ItemTemplate>
                
                </asp:FormView>
                
                </asp:Panel>
            </div>                          
        </div>
    </div>
    </form>
</body>
</html>
