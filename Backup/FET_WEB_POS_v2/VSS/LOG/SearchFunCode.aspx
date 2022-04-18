<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchFunCode.aspx.cs" Inherits="VSS_LOG_SearchFunCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>選擇功能代碼</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!--功能代碼-->
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, FunctionCode %>" />：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="80"></asp:TextBox>
                        </td>
                        <td>
                            <!--功能名稱-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FunctionName %>" />：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" Width="80"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                        </td>                       
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
            </div>
            <div class="seperate">
            </div>
            <div class="GridScrollBar" style="height: 214px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--功能代碼-->
                                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, FunctionCode %>" />
                                    </th>
                                    <th scope="col">
                                        <!--功能名稱-->
                                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FunctionName %>" />
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="2" class="tdEmptyData">
                                        <!--無明細資料-->
                                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="功能代碼" HeaderText="<%$ Resources:WebResources, FunctionCode %>" />
                                <asp:BoundField DataField="功能名稱" HeaderText="<%$ Resources:WebResources, FunctionName %>" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Button ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" Visible="false" OnClientClick="window.close();return false;" />
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Cancel %>" Visible="false" OnClientClick="window.close();return false;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
