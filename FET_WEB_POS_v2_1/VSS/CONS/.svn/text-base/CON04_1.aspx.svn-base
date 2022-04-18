<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON04_1.aspx.cs" Inherits="VSS_CONS_CON04_1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        <!--資料匯入作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DataImport %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdval">
                        <!--匯入檔案名稱-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ImportFileName %>"></asp:Literal>：
                    </td>
                    <td colspan="5">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" />
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--寄銷商品資料-->
                        <asp:CheckBox ID="CheckBox4" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductInformation %>" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox2" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <dx:ASPxButton Width="50px" ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>" />
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--寄銷佣金/租金資料-->
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="<%$ Resources:WebResources, ConsignmentCommission %>" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <dx:ASPxButton Width="50px" ID="Button5" runat="server" Text="<%$ Resources:WebResources, Import %>" />
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <dx:ASPxButton Width="50px" ID="Button1" runat="server" Text="<%$ Resources:WebResources, Exit %>">
                            <ClientSideEvents Click="function(s, e) {
	                            window.close();
                            }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
            <TabPages>
                <dx:TabPage Text="<%$ Resources:WebResources, ConsignmentProductInformation %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <cc:ASPxGridView ClientInstanceName="GridView3" ID="GridView3" runat="server" Width="100%"
                                KeyFieldName="廠商代號">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="廠商代號" Caption="<%$ Resources:WebResources, SupplierNo %>"
                                        VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="商品代號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                        VisibleIndex="1">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="商品類別" Caption="<%$ Resources:WebResources, ProductCategory %>"
                                        VisibleIndex="2">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                        VisibleIndex="3">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="上架日" Caption="<%$ Resources:WebResources, SupportStartDate %>"
                                        VisibleIndex="4">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="下架日" Caption="<%$ Resources:WebResources, SupportExpiryDate %>"
                                        VisibleIndex="5">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="停止訂購日" Caption="<%$ Resources:WebResources, OrderEndDate %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="科目1" Caption="<%$ Resources:WebResources, Subject1 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="科目2" Caption="<%$ Resources:WebResources, Subject2 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="科目3" Caption="<%$ Resources:WebResources, Subject3 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="科目4" Caption="<%$ Resources:WebResources, Subject4 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="科目5" Caption="<%$ Resources:WebResources, Subject5 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="科目6" Caption="<%$ Resources:WebResources, Subject6 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="單位" Caption="<%$ Resources:WebResources, Unit %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="佣金比率" Caption="<%$ Resources:WebResources, CommissionRate %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="起始月份" Caption="<%$ Resources:WebResources, StartMonth %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="結束月份" Caption="<%$ Resources:WebResources, EndMonth %>">
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="資料筆數：0 筆"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label6" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                    <EmptyDataRow>
                                        <!--choose add button-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </EmptyDataRow>
                                </Templates>
                                <Settings ShowTitlePanel="True" ShowFooter="True" />
                            </cc:ASPxGridView>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="<%$ Resources:WebResources, ConsignmentCommission %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <cc:ASPxGridView ClientInstanceName="GridView1" ID="GridView1" runat="server" Width="100%"
                                KeyFieldName="廠商代號">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="廠商代號" Caption="<%$ Resources:WebResources, SupplierNo %>"
                                        VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="佣金比率" Caption="<%$ Resources:WebResources, CommissionRate %>"
                                        VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="起始月份" Caption="<%$ Resources:WebResources, StartMonth %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="結束月份" Caption="<%$ Resources:WebResources, EndMonth %>">
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="資料筆數：0 筆"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label6" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                    <EmptyDataRow>
                                        <!--choose add button-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </EmptyDataRow>
                                </Templates>
                                <Settings ShowTitlePanel="True" ShowFooter="True" />
                            </cc:ASPxGridView>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
