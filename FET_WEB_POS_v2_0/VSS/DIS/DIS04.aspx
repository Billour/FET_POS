﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DIS04.aspx.cs" Inherits="VSS_DIS04_DIS04" %>

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
        <div>
            <table width="100%" class="titlef">
                <tr>
                    <td align="left" style="width: 99%">
                        <!--群組關聯性設定查詢-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, GroupRelationshipManagement %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <div class="btnPosition">
                            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>" />
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
                        <!--群組關連代號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, RelationshipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--群組關連名稱-->
                         <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, RelationshipName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--有效期間-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, EffectiveDuration %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" isAutoPostBackCheck="False" />
                        &nbsp;<asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" isAutoPostBackCheck="False" />
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox3" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" isAutoPostBackCheck="False" />
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--類 別-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:RadioButtonList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>商品類別</asp:ListItem>
                            <asp:ListItem>商品編號</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        <!--維護人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    </td>
                </tr>
                </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnQuery_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            Visible="False">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--狀態-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--群組關連代號-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, RelationshipNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--群組關連名稱-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, RelationshipName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--有效日期(起)-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ValidStartDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--有效日期(迄)-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ValidEndDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--類別-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--人員-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Staff %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--日期-->
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Date %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="8" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
                                <asp:BoundField DataField="群組關連代號" HeaderText="<%$ Resources:WebResources, RelationshipNo %>" />
                                <asp:BoundField DataField="群組關連名稱" HeaderText="<%$ Resources:WebResources, RelationshipName %>" />
                                <asp:BoundField DataField="有效日期(起)" HeaderText="<%$ Resources:WebResources, ValidStartDate %>" />
                                <asp:BoundField DataField="有效日期(迄)" HeaderText="<%$ Resources:WebResources, ValidEndDate %>" />
                                <asp:BoundField DataField="類別" HeaderText="<%$ Resources:WebResources, Category %>" />
                                <asp:BoundField DataField="人員" HeaderText="<%$ Resources:WebResources, Staff %>" />
                                <asp:BoundField DataField="日期" HeaderText="<%$ Resources:WebResources, Date %>" />
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