<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrantPermissions.aspx.cs" Inherits="VSS_LOG_ChooseFunctions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>選擇功能清單</title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 
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
                            <!--角色名稱-->
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, RoleName %>" />：
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>AC</asp:ListItem>
                            <asp:ListItem>CB</asp:ListItem>
                            <asp:ListItem>AC</asp:ListItem>
                        </asp:DropDownList>
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
                                        <!--項次-->
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Items %>" />
                                    </th>
                                    <th scope="col">
                                        <!--模組名稱-->
                                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ModuleName %>" />
                                    </th>
                                    <th scope="col">
                                        <!--功能名稱-->
                                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FunctionName %>" />
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="3" class="tdEmptyData">                                        
                                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:if(this.checked){$('.GridScrollBar').checkCheckboxes();}else{$('.GridScrollBar').unCheckCheckboxes();}"  />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" />
                                <asp:BoundField DataField="模組名稱" HeaderText="<%$ Resources:WebResources, ModuleName %>" />
                                <asp:BoundField DataField="功能名稱" HeaderText="<%$ Resources:WebResources, FunctionName %>" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                
                <asp:Button ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClick="btnOk_Click" />
                <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Cancel %>"  OnClientClick="window.close();return false;" />
                   
            </div>
        </div>
    </div>
    </form>
</body>
</html>
