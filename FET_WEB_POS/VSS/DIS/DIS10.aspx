<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DIS10.aspx.cs" Inherits="VSS_DIS10_DIS10" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div class="titlef">
            <table width="100%">
                <tr>
                    <td align="left">
                        <!--ERP與類別屬性對應查詢-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ObjectPropertiesMapping %>"></asp:Literal> 
                    </td>
                    <td align="right">
                        <div class="btnPosition">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
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
                        <!--商品類別-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal> ：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品屬性-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductAttribute %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--維護人員-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>王大寶</asp:ListItem>
                            <asp:ListItem>王二寶</asp:ListItem>
                            <asp:ListItem>王小寶</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--ERP Attribute-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ErpAttribute %>"></asp:Literal>：
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
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                </tr>
                </table>
        </div>
        <div class="seperate">
        </div>
         <div class="btnPosition">
            <asp:Button ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnQuery_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></div>
        <div class="seperate"></div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="div1" runat="server" visible="false" >
                        <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" OnClick="btnExport_Click" />
                    </div>
                    <div id="div2" runat="server" visible="false"  class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--商品類別-->
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal> 
                                    </th>
                                    <th scope="col">
                                        <!--商品屬性-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductAttribute %>"></asp:Literal> 
                                    </th>
                                    <th scope="col">
                                        <!--ERP Attribute-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ErpAttribute %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--生效日(起)-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, EffectiveStartDate %>"></asp:Literal> 
                                    </th>
                                    <th scope="col">
                                        <!--生效日(迄)-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, EffectiveEndDate %>"></asp:Literal> 
                                    </th>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal> 
                                    </th>
                                    <th scope="col">
                                        <!--維護人員-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal> 
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="7" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal> 
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="商品類別" HeaderText="<%$ Resources:WebResources, ProductCategory %>" />
                                <asp:BoundField DataField="商品屬性" HeaderText="<%$ Resources:WebResources, ProductAttribute %>" />
                                <asp:BoundField DataField="ERP Attribute" HeaderText="<%$ Resources:WebResources, ErpAttribute %>" />
                                <asp:BoundField DataField="生效日(起)" HeaderText="<%$ Resources:WebResources, EffectiveStartDate %>" />
                                <asp:BoundField DataField="生效日(迄)" HeaderText="<%$ Resources:WebResources, EffectiveEndDate %>" />
                                <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
                                <asp:BoundField DataField="維護人員" HeaderText="<%$ Resources:WebResources, MaintainedBy %>" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
        </div>
        <div class="txt">
        </div>
        <br />
        <div class="btnPosition">
        </div>
        <div class="txt">
        </div>
        <br />
        <div class="btnPosition">
        </div>
    </div>
    </form>
</body>
</html>
