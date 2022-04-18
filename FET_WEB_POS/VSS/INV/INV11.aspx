<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV11.aspx.cs" Inherits="VSS_INV11_INV11" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        門市盤點作業
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="盤點查詢作業" OnClientClick="document.location='INV10.aspx';return false;" />
                    </td>
                </tr>
            </table>          
           
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--作業類型-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ActivityType %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="2">
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="True">列印</asp:ListItem>
                            <asp:ListItem>盤點輸入</asp:ListItem>
                        </asp:RadioButtonList>  
                        </td>
                </tr>
                <tr>                 
                    <td class="tdtxt">
                        <!--盤點型態-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockTakingMethod %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="True">重盤</asp:ListItem>
                            <asp:ListItem>全盤</asp:ListItem>
                            <asp:ListItem>關帳日盤點</asp:ListItem>
                        </asp:RadioButtonList>                       
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--盤點單號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label1" runat="server" Text="SC2101-1007002"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--盤點人員-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CountedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label2" runat="server" Text="王小明"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label6" runat="server" Text="10/07/12 15:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--盤點日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="2010/07/14"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--門市名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="2101 遠企"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label7" runat="server" Text="64591 李家駿"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Ok %>" onclick="Button5_Click" />
        </div>
        <div class="seperate"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <div class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--項次-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--倉別-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Warehouse %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        商品編號
                                    </th>
                                    <th scope="col">
                                        商品名稱
                                    </th>
                                    <th scope="col">
                                        單位
                                    </th>
                                    <th scope="col">
                                        帳上庫存
                                    </th>
                                    <th scope="col">
                                        門市盤點量
                                    </th>
                                    <th scope="col">
                                        盤差量
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="8" class="tdEmptyData">
                                        <!--此無明細資料-->
                                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true" />
                                <asp:BoundField DataField="倉別" HeaderText="<%$ Resources:WebResources, Warehouse %>" ReadOnly="true" />
                                <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />
                                <asp:BoundField DataField="單位" HeaderText="<%$ Resources:WebResources, Unit %>" ReadOnly="true" />
                                <asp:BoundField DataField="帳上庫存" HeaderText="<%$ Resources:WebResources, BookInventory %>" ReadOnly="true" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, PhysicalInventory %>">
                                    <ItemTemplate>
                                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("門市盤點量") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:BoundField DataField="盤差量" HeaderText="<%$ Resources:WebResources, DifferenceQuantity %>" ReadOnly="true" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <div class="btnPosition" id="gridBtn" runat="server">
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
        </div>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
