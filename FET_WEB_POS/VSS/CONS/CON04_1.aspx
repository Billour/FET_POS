<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON04_1.aspx.cs" Inherits="VSS_CON04_1" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                     <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <asp:Button ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>" />
                    </td>
                </tr>
              <tr>
                    <td class="tdval" colspan="2">
                        <!--寄銷佣金/租金資料-->
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="寄銷廠商佣金" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Import %>" />
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
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Exit %>" OnClientClick="javascript:window.close();" />
                    </td>
                </tr>
               
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="upTab" runat="server">
            <ContentTemplate>
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                    Width="100%" CssClass="visoft__tab_xpie7" AutoPostBack="True" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                    <asp:TabPanel ID="TabPanel4" runat="server">
                        <HeaderTemplate>
                            <span>
                                <!--寄銷商品資料-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductInformation %>"></asp:Literal></span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label5" runat="server" Text="資料筆數：0 筆"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label6" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                            <br />
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="mGrid" HeaderStyle-Wrap="false" RowStyle-Wrap="false">                                    
                                    <EmptyDataTemplate>                                 
                                        <tr>
                                            <th scope="col" nowrap="nowrap">
                                                <!--廠商代號-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--商品代號-->
                                                <asp:Literal ID="Literal12" runat="server" Text="商品代號"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--商品類別-->
                                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--商品名稱-->
                                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--上架日-->
                                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, SupportStartDate %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--下架日-->
                                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, SupportExpiryDate %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--停止訂購日-->
                                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, OrderEndDate %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--科目1-->
                                                <asp:Literal ID="Literal30" runat="server" Text="科目1"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--科目2-->
                                                <asp:Literal ID="Literal31" runat="server" Text="科目2"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--科目3-->
                                                <asp:Literal ID="Literal32" runat="server" Text="科目3"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--科目4-->
                                                <asp:Literal ID="Literal33" runat="server" Text="科目4"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--科目5-->
                                                <asp:Literal ID="Literal34" runat="server" Text="科目5"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--科目6-->
                                                <asp:Literal ID="Literal35" runat="server" Text="科目6"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--單位-->
                                                <asp:Literal ID="Literal39" runat="server" Text="<%$ Resources:WebResources, Unit %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--佣金比率-->
                                                <asp:Literal ID="Literal36" runat="server" Text="<%$ Resources:WebResources, CommissionRate %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--起始月份-->
                                                <asp:Literal ID="Literal37" runat="server" Text="<%$ Resources:WebResources, StartMonth %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--結束月份-->
                                                <asp:Literal ID="Literal38" runat="server" Text="<%$ Resources:WebResources, EndMonth %>"></asp:Literal>
                                            </th>                                            
                                        </tr>
                                        <tr>
                                            <td colspan="17" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>                                      
                                        <asp:BoundField DataField="廠商代號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                                        <asp:BoundField DataField="商品代號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                                        <asp:BoundField DataField="商品類別" HeaderText="<%$ Resources:WebResources, ProductCategory %>" />
                                        <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                                        <asp:BoundField DataField="上架日" HeaderText="<%$ Resources:WebResources, SupportStartDate %>" />
                                        <asp:BoundField DataField="下架日" HeaderText="<%$ Resources:WebResources, SupportExpiryDate %>" />
                                        <asp:BoundField DataField="停止訂購日" HeaderText="<%$ Resources:WebResources, OrderEndDate %>" />
                                        <asp:BoundField DataField="科目1" HeaderText="科目1" />
                                        <asp:BoundField DataField="科目2" HeaderText="科目2" />
                                        <asp:BoundField DataField="科目3" HeaderText="科目3" />
                                        <asp:BoundField DataField="科目4" HeaderText="科目4" />
                                        <asp:BoundField DataField="科目5" HeaderText="科目5" />
                                        <asp:BoundField DataField="科目6" HeaderText="科目6" />
                                         <asp:BoundField DataField="單位" HeaderText="<%$ Resources:WebResources, Unit %>" />                                       
                                        <asp:BoundField DataField="佣金比率" HeaderText="<%$ Resources:WebResources, CommissionRate %>" />
                                        <asp:BoundField DataField="起始月份" HeaderText="<%$ Resources:WebResources, StartMonth %>" />
                                        <asp:BoundField DataField="結束月份" HeaderText="<%$ Resources:WebResources, EndMonth %>" />                                        
                                    </Columns>
                                </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel2" runat="server">
                        <HeaderTemplate>
                            <span>
                                <!--寄銷佣金/租金資料-->
                                <asp:Literal ID="Literal8" runat="server" Text="寄銷廠商佣金"></asp:Literal></span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label7" runat="server" Text="資料筆數：0 筆"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label8" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                            <br />
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                    <EmptyDataTemplate>
                                        <tr>
                                            <th scope="col">
                                                <!--廠商代號-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--佣金比率-->
                                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CommissionRate %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--起始月份-->
                                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, StartMonth %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--結束月份-->
                                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, EndMonth %>"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="廠商代號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                                        <asp:BoundField DataField="佣金比率" HeaderText="<%$ Resources:WebResources, CommissionRate %>" />
                                        <asp:BoundField DataField="起始月份" HeaderText="<%$ Resources:WebResources, StartMonth %>" />
                                        <asp:BoundField DataField="結束月份" HeaderText="<%$ Resources:WebResources, EndMonth %>" />
                                    </Columns>
                                </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
