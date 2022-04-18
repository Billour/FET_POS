<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrantEmployeePermissions.aspx.cs"
    Inherits="VSS_LOG_GrantEmployeePermissions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>選擇員工清單</title>

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
                            <!--區域別-->
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>" />：
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="DropDownList1" runat="server">
                                <Items>
                                    <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                    <dx:ListEditItem Value="1" Text="北一區" />
                                    <dx:ListEditItem Value="2" Text="中一區" />
                                    <dx:ListEditItem Value="3" Text="南一區" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <!--門市編號-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StoreNo %>" />：
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="DropDownList2" runat="server">
                                <Items>
                                    <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                    <dx:ListEditItem Value="1" Text="2101" />
                                    <dx:ListEditItem Value="2" Text="2102" />
                                    <dx:ListEditItem Value="3" Text="2103" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!--員工編號-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>" />：
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="DropDownList3" runat="server">
                                <Items>
                                    <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                    <dx:ListEditItem Value="1" Text="AC" />
                                    <dx:ListEditItem Value="2" Text="CB" />
                                    <dx:ListEditItem Value="3" Text="DC" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <!--員工姓名-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, EmployeeName %>" />：
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="DropDownList4" runat="server">
                                <Items>
                                    <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                    <dx:ListEditItem Value="1" Text="陳建國"></dx:ListEditItem>
                                    <dx:ListEditItem Value="2" Text="王大為"></dx:ListEditItem>
                                </Items>
                            </dx:ASPxComboBox>
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
            <div class="GridScrollBar" style="height: auto">
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
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:if(this.checked){$('#form1').checkCheckboxes();}else{$('#form1').unCheckCheckboxes();}" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </ItemTemplate>
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
                <table align="center" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnOk" runat="server" Text="<%$ Resources:WebResources, Ok %>" >
                            <ClientSideEvents Click="function(s, e) {
                              window.close();e.returnValue =false;          
                            }" />
                            </dx:ASPxButton>
                                <%--OnClick="btnOk_Click" />--%>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                                <ClientSideEvents Click="function(s, e) {
                              window.close();e.returnValue =false;          
                            }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
