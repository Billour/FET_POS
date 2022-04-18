<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LEA07.aspx.cs" Inherits="VSS_LEA_LEA07" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--設備租賃設定查詢作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="設備租賃設定查詢作業"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--類別-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">漫遊租賃</asp:ListItem>
                                <asp:ListItem Value="2">維修租賃</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            <!--產品類別-->
                            <asp:Literal ID="Literal3" runat="server" Text="產品類別"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">產品類別1</asp:ListItem>
                                <asp:ListItem Value="2">產品類別2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            <!--產品名稱-->
                            <asp:Literal ID="Literal5" runat="server" Text="產品名稱"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList4" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">產品名稱1</asp:ListItem>
                                <asp:ListItem Value="2">產品名稱2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--外部廠商代碼-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, OutsideFirmNo %>"></asp:Literal>：
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="TextBox1" runat="server">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--外部廠商名稱-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, OutsideFirmName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox2" runat="server">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td align="right">
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                                OnClick="btnSearch_Click" />
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <dx:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <dx:GridViewDataCheckColumn VisibleIndex="0">
                                    <DataItemTemplate>
                                        <input id="Radio" type="radio" name="SameRadio" />
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataColumn FieldName="類別" Caption="<%$ Resources:WebResources, Category %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="產品類別" Caption="產品類別" VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="產品名稱" Caption="產品名稱" VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="外部廠商代碼" Caption="<%$ Resources:WebResources, OutsideFirmNo %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="外部廠商名稱" Caption="<%$ Resources:WebResources, OutsideFirmName %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>"
                                    VisibleIndex="6" />
                                <dx:GridViewDataColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, StartDate %>"
                                    VisibleIndex="7" />
                                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                    VisibleIndex="8" />
                                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                    VisibleIndex="9" />
                            </Columns>
                            <Templates>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                                </EmptyDataRow>
                            </Templates>
                        </dx:ASPxGridView>
                        <div class="seperate">
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <dx:ASPxButton ID="btnSure" runat="server" Text="<%$ Resources:WebResources, Ok %>" 
                    OnClick="btnSure_Click" />
            </div>
        </div>
    </div>
</asp:Content>
