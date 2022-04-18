<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV23.aspx.cs" Inherits="VSS_INV23_INV23" %>

<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
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
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--倉別設定作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, WarehousingSettings %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="Div1" runat="server" class="SubEditBlock" visible="true">
                        <div class="SubEditCommand">
                            <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                Visible="true" OnClick="btnAdd_Click" />
                        </div>
                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvMaster_PageIndexChanging"
                            EnableTheming="True" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False"
                            CssClass="mGrid">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--項次-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--倉別名稱-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, WarehouseName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--可銷售-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Marketable %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--檢核IMEI-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, VerifyImei %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新人員-->
                                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="6" class="tdEmptyData">
                                        <!--此無明細資料-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <FooterTemplate>
                                        <asp:Button ID="btnSave" runat="server" CommandName="Save" OnClick="btnCancel_Click"
                                            Text="<%$ Resources:WebResources, Save %>" />
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="<%$ Resources:WebResources, Cancel %>" />
                                    </FooterTemplate>
                                    <FooterStyle Width="30px" />
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>"
                                    ReadOnly="true" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, WarehouseName %>" HeaderStyle-Width="60px"
                                    ItemStyle-Width="60px" FooterStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("倉別名稱") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtb2" runat="server" Width="40px"></asp:TextBox>
                                    </FooterTemplate>
                                    <FooterStyle Width="60px" />
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="可銷售" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox101" runat="server" Enabled="true" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CheckBox1011" runat="server" Enabled="true" />
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, VerifyImei %>">
                                    <ItemTemplate>
                                        <asp:Label ID="Label91" runat="server" Text='<%# Bind("檢核IMEI") %>'></asp:Label>
                                        <asp:DropDownList ID="DropDownList111" runat="server">
                                            <asp:ListItem>不控管</asp:ListItem>
                                            <asp:ListItem>銷售時記錄</asp:ListItem>
                                            <asp:ListItem>銷售時確認</asp:ListItem>
                                            <asp:ListItem>庫存異動控管</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="DropDownList111111" runat="server">
                                            <asp:ListItem>不控管</asp:ListItem>
                                            <asp:ListItem>銷售時記錄</asp:ListItem>
                                            <asp:ListItem>銷售時確認</asp:ListItem>
                                            <asp:ListItem>庫存異動控管</asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" />
                                <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
                            </Columns>
                        </cc1:ExGridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                    OnClick="btnSave_Click" />
                <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, CANCEL %>" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
