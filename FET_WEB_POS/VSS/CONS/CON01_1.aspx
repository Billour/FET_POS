<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON01_1.aspx.cs" Inherits="VSS_CON01_1" %>

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
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="廠商資料" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
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
                        <!--合作店組資料-->
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="合作店組資料" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <asp:Button ID="Button6" runat="server" Text="<%$ Resources:WebResources, Exit %>" />
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
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--總額抽成-->
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="<%$ Resources:WebResources, Prorate %>" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--金額級距-->
                        <asp:CheckBox ID="CheckBox6" runat="server" Text="<%$ Resources:WebResources, Bracket %>" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--外部廠商商品資料-->
                        <asp:CheckBox ID="CheckBox7" runat="server" Text="外部廠商商品資料" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--信用卡手續費-->
                        <asp:CheckBox ID="CheckBox8" runat="server" Text="信用卡手續費" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
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
                    <asp:TabPanel ID="TabPanel1" runat="server">
                        <HeaderTemplate>
                            <span>
                                <!--廠商資料-->
                                <asp:Literal ID="Literal7" runat="server" Text="廠商資料"></asp:Literal></span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label1" runat="server" Text="資料筆數：0 筆"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                            <br />
                            <%--<div class="GridScrollBar">--%>
                                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid" HeaderStyle-Wrap="false" RowStyle-Wrap="false">
                                    <EmptyDataTemplate>
                                        <tr>
                                            <th scope="col" nowrap="nowrap">
                                                <!--廠商類別-->
                                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--廠商代號-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--廠商名稱-->
                                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--公司地址-->
                                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, CompanyAddress %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--聯絡人-->
                                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Contact %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--聯絡電話-->
                                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--合作起日-->
                                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, CooperationStartDate %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--合作迄日-->
                                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, CooperationEndDate %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--合約號碼-->
                                                <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ContractNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--結算日-->
                                                <asp:Literal ID="Literal21" runat="server" Text="結算日"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--統一編號-->
                                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--負責人-->
                                                <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, Owner %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--電話號碼-->
                                                <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, Telephone %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--傳真-->
                                                <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, Fax %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--電子信箱-->
                                                <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, Email %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--總金額底限-->
                                                <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, MinimumTotalAmount %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--總金額底限勾選-->
                                                <asp:Literal ID="Literal28" runat="server" Text="總金額底限勾選"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--備註-->
                                                <asp:Literal ID="Literal29" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>
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
                                        </tr>
                                        <tr>
                                            <td colspan="24" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="廠商類別" HeaderText="<%$ Resources:WebResources, SupplierCategory %>" />
                                        <asp:BoundField DataField="廠商代號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                                        <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" />
                                        <asp:BoundField DataField="公司地址" HeaderText="<%$ Resources:WebResources, CompanyAddress %>" />
                                        <asp:BoundField DataField="聯絡人" HeaderText="<%$ Resources:WebResources, Contact %>" />
                                        <asp:BoundField DataField="聯絡電話" HeaderText="<%$ Resources:WebResources, ContactTelephone %>" />
                                        <asp:BoundField DataField="合作起日" HeaderText="<%$ Resources:WebResources, CooperationStartDate %>" />
                                        <asp:BoundField DataField="合作訖日" HeaderText="<%$ Resources:WebResources, CooperationEndDate %>" />
                                        <asp:BoundField DataField="合約號碼" HeaderText="<%$ Resources:WebResources, ContractNo %>" />
                                        <asp:BoundField DataField="結算日" HeaderText="結算日" />
                                        <asp:BoundField DataField="統一編號" HeaderText="<%$ Resources:WebResources, UnifiedBusinessNo %>" />
                                        <asp:BoundField DataField="負責人" HeaderText="<%$ Resources:WebResources, Owner %>" />
                                        <asp:BoundField DataField="電話號碼" HeaderText="<%$ Resources:WebResources, Telephone %>" />
                                        <asp:BoundField DataField="傳真" HeaderText="<%$ Resources:WebResources, Fax %>" />
                                        <asp:BoundField DataField="電子信箱" HeaderText="<%$ Resources:WebResources, Email %>" />
                                        <asp:BoundField DataField="總金額底限" HeaderText="<%$ Resources:WebResources, MinimumTotalAmount %>" />
                                        <asp:BoundField DataField="總金額底限勾選" HeaderText="總金額底限勾選" />
                                        <asp:BoundField DataField="備註" HeaderText="<%$ Resources:WebResources, Remark %>" />
                                        <asp:BoundField DataField="科目1" HeaderText="科目1" />
                                        <asp:BoundField DataField="科目2" HeaderText="科目2" />
                                        <asp:BoundField DataField="科目3" HeaderText="科目3" />
                                        <asp:BoundField DataField="科目4" HeaderText="科目4" />
                                        <asp:BoundField DataField="科目5" HeaderText="科目5" />
                                        <asp:BoundField DataField="科目6" HeaderText="科目6" />
                                    </Columns>
                                </asp:GridView>
                           <%-- </div>--%>
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
                          <%--  <div class="GridScrollBar">--%>
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
                           <%-- </div>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel3" runat="server">
                        <HeaderTemplate>
                            <span>
                                <!--寄銷合作店組資料-->
                                <asp:Literal ID="Literal9" runat="server" Text="合作店組資料"></asp:Literal></span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label3" runat="server" Text="資料筆數：0 筆"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label4" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                            <br />
                           <%-- <div class="GridScrollBar">--%>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                    <EmptyDataTemplate>
                                        <tr>
                                            <th scope="col">
                                                <!--廠商代號-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--門市代號-->
                                                <asp:Literal ID="Literal12" runat="server" Text="門市代號"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="廠商代號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                                        <asp:BoundField DataField="門市代號" HeaderText="門市代號" />
                                    </Columns>
                                </asp:GridView>
                           <%-- </div>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
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
                            <%--<div class="GridScrollBar">--%>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="mGrid" HeaderStyle-Wrap="false" RowStyle-Wrap="false">
                                    <EmptyDataTemplate>                                 
                                        <tr>
                                            <th scope="col" nowrap="nowrap">
                                                <!--廠商代號-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col" nowrap="nowrap">
                                                <!--商品代號-->
                                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
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
                                        <asp:BoundField DataField="商品代號" HeaderText="商品代號" />
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
                            <%--</div>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel5" runat="server">
                        <HeaderTemplate>
                            <span>
                                <!--總額抽成-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Prorate %>"></asp:Literal></span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label9" runat="server" Text="資料筆數：0 筆"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label10" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                            <br />
                           <%-- <div class="GridScrollBar">--%>
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
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
                           <%-- </div>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel6" runat="server">
                        <HeaderTemplate>
                            <span>
                                <!--金額級距-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Bracket %>"></asp:Literal></span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label11" runat="server" Text="資料筆數：0 筆"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label12" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                            <br />
                           <%-- <div class="GridScrollBar">--%>
                                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                    <EmptyDataTemplate>
                                        <tr>
                                            <th scope="col">
                                                <!--廠商代號-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--級距項次-->
                                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, BracketItems %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--起-金額級距-->
                                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, BracketStart %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--訖-金額級距-->
                                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, BracketEnd %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--佣金比率-->
                                                <asp:Literal ID="Literal40" runat="server" Text="<%$ Resources:WebResources, CommissionRate %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--起始月份-->
                                                <asp:Literal ID="Literal41" runat="server" Text="<%$ Resources:WebResources, StartMonth %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--結束月份-->
                                                <asp:Literal ID="Literal42" runat="server" Text="<%$ Resources:WebResources, EndMonth %>"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="24" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="廠商代號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                                        <asp:BoundField DataField="級距項次" HeaderText="<%$ Resources:WebResources, BracketItems %>" />
                                        <asp:BoundField DataField="起-金額級距" HeaderText="<%$ Resources:WebResources, BracketStart %>" />
                                        <asp:BoundField DataField="訖-金額級距" HeaderText="<%$ Resources:WebResources, BracketEnd %>" />
                                        <asp:BoundField DataField="佣金比率" HeaderText="<%$ Resources:WebResources, CommissionRate %>" />
                                        <asp:BoundField DataField="起始月份" HeaderText="<%$ Resources:WebResources, StartMonth %>" />
                                        <asp:BoundField DataField="結束月份" HeaderText="<%$ Resources:WebResources, EndMonth %>" />
                                    </Columns>
                                </asp:GridView>
                           <%-- </div>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel7" runat="server">
                        <HeaderTemplate>
                            <span>
                                <!--外部廠商商品資料-->
                                <asp:Literal ID="Literal13" runat="server" Text="外部廠商商品資料"></asp:Literal></span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label13" runat="server" Text="資料筆數：0 筆"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label14" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                            <br />
                           <%-- <div class="GridScrollBar">--%>
                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                    <EmptyDataTemplate>
                                        <tr>
                                            <th scope="col">
                                                <!--廠商代號-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--商品料號-->
                                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="廠商代號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                                        <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                                    </Columns>
                                </asp:GridView>
                            <%--</div>--%>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel8" runat="server">
                        <HeaderTemplate>
                            <span>
                                <!--信用卡手續費-->
                                <asp:Literal ID="Literal14" runat="server" Text="信用卡手續費"></asp:Literal></span>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label15" runat="server" Text="資料筆數：0 筆"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label16" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></asp:Label>
                            <br />
                            <%--<div class="GridScrollBar">--%>
                                <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                    <EmptyDataTemplate>
                                        <tr>
                                            <th scope="col">
                                                <!--項次-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--信用卡別-->
                                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TypeOfCreditCard %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--手續費-->
                                                <asp:Literal ID="Literal13" runat="server" Text="手續費"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="tdEmptyData">
                                                <!--請點選新增按鍵增加資料-->
                                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="廠商代號" HeaderText="<%$ Resources:WebResources, Items %>" />
                                        <asp:BoundField DataField="佣金比率" HeaderText="<%$ Resources:WebResources, TypeOfCreditCard %>" />
                                        <asp:BoundField DataField="手續費" HeaderText="手續費" />
                                    </Columns>
                                </asp:GridView>
                            <%--</div>--%>
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
