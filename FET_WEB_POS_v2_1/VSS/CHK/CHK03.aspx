<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CHK03.aspx.cs" Inherits="VSS_CHK_CHK03" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
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
                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ></dx:ASPxDateEdit>
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
                        <dx:ASPxComboBox ID="DropDownList1" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="王大寶" />
                                <dx:ListEditItem Text="王二寶" />
                                <dx:ListEditItem Text="王三寶" />
                                <dx:ListEditItem Text="王四寶" />
                                <dx:ListEditItem Text="王小寶" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <div runat="server" visible="false">
            <div class="SubEditCommand">
                <table align="center" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td><dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click" /></td>
                        <td><dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" /></td>
                    </tr>
                </table>
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
                                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit>                                                
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
                                                <dx:ASPxDateEdit ID="startDateEdit" runat="server"></dx:ASPxDateEdit>                                                
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
                                                <dx:ASPxDateEdit ID="endDateEdit" runat="server"></dx:ASPxDateEdit>         
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
                                                <dx:ASPxDateEdit ID="modifiedDateEdit" runat="server"></dx:ASPxDateEdit>         
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
                                           <dx:ASPxDateEdit ID="startDateEdit" runat="server"></dx:ASPxDateEdit>
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
                                            <dx:ASPxDateEdit ID="endDateEdit" runat="server"></dx:ASPxDateEdit>
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
</asp:Content>
