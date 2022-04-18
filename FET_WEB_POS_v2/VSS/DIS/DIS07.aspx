<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DIS07.aspx.cs" Inherits="VSS_DIS_DIS07" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div class="titlef">
            <table width="100%">
                <tr>
                    <td align="left" style="width: 99%">
                        <!--補貼金額查詢-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, AllowanceSearch %>"></asp:Literal>
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
                        <!--群組代碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, GroupID %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--群組名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, GroupName %>"></asp:Literal>：
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
                <tr>
                    <td class="tdtxt">
                        <!--基準補貼群組-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, BaseGroup %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        <!--維護人員-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
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
                        <!--生效日-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, EffectiveDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                         <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" ></dx:ASPxDateEdit>
                    </td>
                </tr>
                </table>
        </div>
                
        <div class="seperate"></div>
        <div class="btnPosition">
            <asp:Button ID="btnQuery" runat="server" 
                Text="<%$ Resources:WebResources, Search %>" OnClick="btnQuery_Click" 
                />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="div1" runat="server" visible="false" >
                        <asp:Button ID="btnExport" runat="server" 
                            Text="<%$ Resources:WebResources, Export %>" OnClick="btnExport_Click" />
                    </div>
                    <div id="div2" runat="server" visible="false" class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--群組代碼-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, GroupID %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--群組名稱-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, GroupName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--補貼金額-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, AllowanceAmount %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--生效日(起)-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, EffectiveStartDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--生效日(迄)-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, EffectiveEndDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--基準補貼群組-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, BaseGroup %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--備註-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--維護人員-->
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="9" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="群組代碼" HeaderText="<%$ Resources:WebResources, GroupID %>" />
                                <asp:BoundField DataField="群組名稱" HeaderText="<%$ Resources:WebResources, GroupName %>" />
                                <asp:BoundField DataField="補貼金額" HeaderText="<%$ Resources:WebResources, AllowanceAmount %>" />
                                <asp:BoundField DataField="生效日(起)" HeaderText="<%$ Resources:WebResources, EffectiveStartDate %>" />
                                <asp:BoundField DataField="生效日(迄)" HeaderText="<%$ Resources:WebResources, EffectiveEndDate %>" />
                                <asp:BoundField DataField="基準補貼群組" HeaderText="<%$ Resources:WebResources, BaseGroup %>" />
                                <asp:BoundField DataField="備註" HeaderText="<%$ Resources:WebResources, Remark %>" />
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
