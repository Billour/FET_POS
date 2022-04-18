<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CHK03.aspx.cs" Inherits="VSS_CHK_CHK03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="titlef">
            <!--保全收款作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, CashCollectionProcess %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--收款日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, CollectionDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        <!--處理人員-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProcessedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList4" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>王大寶</asp:ListItem>
                            <asp:ListItem>王二寶</asp:ListItem>
                            <asp:ListItem>王小寶</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <div runat="server" id="DIV1" visible="false">
            <div class="SubEditCommand">
                <asp:Button ID="Button11" runat="server" Text="<%$ Resources:WebResources, Add %>" />
                <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            </div>
            <div class="criteria">
                <table>
                    <tr style="background-color: #780C0C; color: White; text-align: Left; font-size: 12pt">
                        <td>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </td>
                        <td>
                            <!--現金-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Cash %>"></asp:Literal>
                        </td>
                        <td>
                            <!--其他-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Other %>"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </td>
                        <td class="tdtxt">
                            <div class="SubEditBlock">
                                <asp:DetailsView ID="gvMaster" runat="server" AutoGenerateRows="False" Height="50px"
                                    Width="295px" OnItemUpdating="gvMaster_ItemUpdating" OnModeChanging="gvMaster_ModeChanging">
                                    <Fields>
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, CollectionDate %>">
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("收款日期") %>'></asp:TextBox>
                                            </InsertItemTemplate>
                                            <EditItemTemplate>
                                                <cc1:postbackDate_TextBox ID="postbackDate_TextBox3" runat="server" Text='<%# Bind("收款日期") %>'
                                                    ImageUrl="~/Icon/calendar.jpg" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("收款日期") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="保全人員" HeaderText="<%$ Resources:WebResources, SecuritiesCustodyClerk %>" />
                                        <asp:BoundField DataField="封條號碼" HeaderText="<%$ Resources:WebResources, SealNo %>" />
                                        <asp:BoundField DataField="收款金額" HeaderText="<%$ Resources:WebResources, ReceiptAmount %>" />
                                        <asp:BoundField DataField="處理人員" HeaderText="<%$ Resources:WebResources, ProcessedBy %>" />
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, DateRangeClosing %>">
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, Start %>">
                                            <EditItemTemplate>
                                                <cc1:postbackDate_TextBox ID="postbackDate_TextBox31" runat="server" Text='<%# Bind("起") %>'
                                                    ImageUrl="~/Icon/calendar.jpg" />
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("[起]") %>'></asp:TextBox>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("[起]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, End %>">
                                            <EditItemTemplate>
                                                <cc1:postbackDate_TextBox ID="postbackDate_TextBox32" runat="server" Text='<%# Bind("訖") %>'
                                                    ImageUrl="~/Icon/calendar.jpg" />
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("[訖]") %>'></asp:TextBox>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("[訖]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ModifiedDate %>">
                                            <EditItemTemplate>
                                                <cc1:postbackDate_TextBox ID="postbackDate_TextBox33" runat="server" Text='<%# Bind("更新日期") %>'
                                                    ImageUrl="~/Icon/calendar.jpg" />
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("更新日期") %>'></asp:TextBox>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("更新日期") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="<%$ Resources:WebResources, Save %>" />
                                                &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="<%$ Resources:WebResources, Cancel %>" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="<%$ Resources:WebResources, Edit %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Fields>
                                </asp:DetailsView>
                            </div>
                        </td>
                        <td class="tdtxt">
                            <asp:DetailsView ID="gvMaster0" runat="server" AutoGenerateRows="False" Height="50px"
                                Width="295px" OnItemUpdating="gvMaster0_ItemUpdating" OnModeChanging="gvMaster0_ModeChanging">
                                <Fields>
                                    <asp:TemplateField>
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="封條號碼" HeaderText="<%$ Resources:WebResources, SealNo %>" />
                                    <asp:BoundField DataField="收款金額" HeaderText="<%$ Resources:WebResources, ReceiptAmount %>" />
                                    <asp:TemplateField HeaderText="處理人員">
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("處理人員") %>'></asp:TextBox>
                                        </InsertItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList5" runat="server">
                                                <asp:ListItem>-請選擇-</asp:ListItem>
                                                <asp:ListItem Selected="True">王大寶</asp:ListItem>
                                                <asp:ListItem>王二寶</asp:ListItem>
                                                <asp:ListItem>王小寶</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("處理人員") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, CollectionDate %>">
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, Start %>">
                                        <EditItemTemplate>
                                            <cc1:postbackDate_TextBox ID="postbackDate_TextBox35" runat="server" Text='<%# Bind("起") %>'
                                                ImageUrl="~/Icon/calendar.jpg" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("[起]") %>'></asp:TextBox>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("[起]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, End %>">
                                        <EditItemTemplate>
                                            <cc1:postbackDate_TextBox ID="postbackDate_TextBox36" runat="server" Text='<%# Bind("訖") %>'
                                                ImageUrl="~/Icon/calendar.jpg" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("[訖]") %>'></asp:TextBox>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("[訖]") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <InsertItemTemplate>
                                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:Button ID="Button12" runat="server" CausesValidation="True" CommandName="Update"
                                                Text="<%$ Resources:WebResources, Save %>" />
                                            &nbsp;<asp:Button ID="Button13" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="<%$ Resources:WebResources, Cancel %>" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="Button14" runat="server" CausesValidation="False" CommandName="Edit"
                                                Text="<%$ Resources:WebResources, Edit %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
                <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
