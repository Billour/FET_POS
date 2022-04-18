<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD03_reports.aspx.cs" Inherits="VSS_ORD03_ORD03_reports" %>

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
        <div class="titlef">
            <!--訂單報表-->
             <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderReport %>"></asp:Literal>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:WebResources, OrderDetailsSearch %>"></asp:Label>
        <!--報表產出時間-->
        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReportCreatedDate %>"></asp:Literal>：<asp:Label ID="Label2" runat="server" Text="2010/08/01 12:29:41"></asp:Label>
        <div class="SubEditBlock">
            <div class="GridScrollBar">
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--訂單日期-->
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--時間-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Time %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--區域-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, District %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--門市編號-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--門市名稱-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--訂單編號-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--項次-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--OE_NO-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, OENO %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--商品編號-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--商品名稱-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--門市調整數量-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, StoreAdjustment %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--業助調整數量-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AssistantAdjustment %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--實際訂購數量-->
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ActualOrderQuantity %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--商品型態-->
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductType %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--配送商-->
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Distributor %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--備註-->
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--狀態-->
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--退回原因-->
                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, ReasonForReturn %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="18" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="訂單日期" HeaderText="<%$ Resources:WebResources, OrderDate %>" />
                        <asp:BoundField DataField="時間" HeaderText="<%$ Resources:WebResources, Time %>" />
                        <asp:BoundField DataField="區域" HeaderText="<%$ Resources:WebResources, District %>" />
                        <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                        <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />
                        <asp:BoundField DataField="訂單編號" HeaderText="<%$ Resources:WebResources, OrderNo %>" />
                        <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" />
                        <asp:BoundField DataField="OE_NO" HeaderText="<%$ Resources:WebResources, OENO %>" />
                        <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                        <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                        <asp:BoundField DataField="門市調整數量" HeaderText="<%$ Resources:WebResources, StoreAdjustment %>" />
                        <asp:BoundField DataField="業助調整數量" HeaderText="<%$ Resources:WebResources, AssistantAdjustment %>" />
                        <asp:BoundField DataField="實際訂購數量" HeaderText="<%$ Resources:WebResources, ActualOrderQuantity %>" />
                        <asp:BoundField DataField="商品型態" HeaderText="<%$ Resources:WebResources, ProductType %>" />
                        <asp:BoundField DataField="配送商" HeaderText="<%$ Resources:WebResources, Distributor %>" />
                        <asp:BoundField DataField="備註" HeaderText="<%$ Resources:WebResources, Remark %>" />
                        <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
                        <asp:BoundField DataField="退回原因" HeaderText="<%$ Resources:WebResources, ReasonForReturn %>" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
