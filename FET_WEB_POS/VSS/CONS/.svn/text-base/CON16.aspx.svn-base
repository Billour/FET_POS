<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON16.aspx.cs" Inherits="VSS_CON16_CON16" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品盤點作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductSockTaking %>"></asp:Literal>
                    </td>
                     <td align="right">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='CON15.aspx';return false;" />
                    </td>
                    
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">作業類型：</td>
                        <td class="tdval" colspan="2">
                            <asp:RadioButton ID="RadioButton4" runat="server" GroupName="TYPE" 
                            Text="列印(空白盤點單)" />
                            <asp:RadioButton ID="RadioButton5" runat="server" GroupName="TYPE" Text="盤點輸入" />
                        </td>
                        <td class="tdval"> 
                            </td>
                        <td class="tdtxt">
                        <!--狀態-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：</td>
                        <td class="tdval">
                            <asp:Label ID="Label1" runat="server" Text="00 未存檔"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">盤點型態：</td>
                        <td class="tdval">
                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="InventoryType" 
                                Text="<%$ Resources:WebResources, Recount %>" AutoPostBack="false" 
                                oncheckedchanged="RadioButton1_CheckedChanged" />
                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="InventoryType" 
                                Text="<%$ Resources:WebResources, CountAll %>" AutoPostBack="false" 
                                oncheckedchanged="RadioButton1_CheckedChanged" />
                        </td>
                        <td class="tdtxt">
                            <!--盤點單號-->
                        </td>
                        <td class="tdval">
                        
                        </td>
                        <td class="tdtxt">
                           
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：</td>
                        <td class="tdval">
                            <asp:Label ID="Label8" runat="server" Text="2010/07/12 15:00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--盤點日期-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                                    <asp:Label ID="lblOrderNo" runat="server" />
                        </td>
                        <td class="tdtxt">
                            <!--盤點人員-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, CountedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label6" runat="server">王小明</asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--日期-->
                            &nbsp;<asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：</td>
                        <td class="tdval">
                            <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--門市名稱-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label5" runat="server" ></asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--廠商名稱-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label7" runat="server">遠企門市</asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--人員-->
                            &nbsp;</td>
                        <td class="tdval">
                            &nbsp;</td>
                    </tr>
                </table>
            </div>         
              <div class="btnPosition">
                <asp:Button ID="btnOk" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClick="btnOk_Click" />        
            </div>
            <div class="seperate"></div> 
                    <div class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                     <th scope="col">
                                        <!--SupplierNo-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                    </th>
                                     <th scope="col">
                                        <!--SupplierNo-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品編號-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                   
                                    <th scope="col">
                                        <!--庫存量-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市盤點量-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, PhysicalInventory %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--盤差量-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, DifferenceQuantity %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="8" class="tdEmptyData">
                                        <!--此無明細資料-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="廠商編號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" ReadOnly="true" />
                                <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" ReadOnly="true" />
                                <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />
                                <asp:BoundField DataField="庫存量" HeaderText="<%$ Resources:WebResources, StockQuantity %>" ReadOnly="true" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, PhysicalInventory %>">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("門市盤點量") %>' Width="100px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("門市盤點量") %>' Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="盤差量" HeaderText="<%$ Resources:WebResources, DifferenceQuantity %>" ReadOnly="true" />
                            </Columns>
                        </asp:GridView>
                    </div>
                     <div class="seperate">
                    </div>
                    <div class="btnPosition">
                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" CausesValidation="False" />
                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    </div>                          
        </div>
    </div>
    </form>
</body>
</html>
