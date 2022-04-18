<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON03.aspx.cs" Inherits="VSS_CON03_CON03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
   </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div class="titlef">
        <!--寄銷商品查詢作業(總部)-->
        <asp:Literal ID="Literal1" runat="server" Text="寄銷商品查詢作業(總部)"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem Selected="True">ALL</asp:ListItem>
                            <asp:ListItem>AC1</asp:ListItem>
                            <asp:ListItem>AC2</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                    </td>
                    <td class="tdval">
                        <table>
                            <tr>
                                <td><asp:TextBox ID="TextBox4" runat="server" Width="100"></asp:TextBox></td>
                                <td><dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>" ClientSideEvents-Click="function(){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                       <!--上架日期-->
                       <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupportStartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table>
                            <tr>
                                <td><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="SupportStartDateFrom" runat="server"></dx:ASPxDateEdit></td>
                                <td><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="SupportStartDateTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--下架日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SupportExpiryDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table>
                            <tr>
                                <td><asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="SupportExpiryDateFrom" runat="server"></dx:ASPxDateEdit></td>
                                <td><asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="SupportExpiryDateTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--商品類別-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlProductCategory" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>3G Handset</asp:ListItem>
                            <asp:ListItem>SIM Card</asp:ListItem>
                            <asp:ListItem>3G Accessory</asp:ListItem>
                            <asp:ListItem>On Line Recharge</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--停止訂購日-->
                        <asp:Literal ID="Literal12" runat="server" Text="停止訂購日"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table>
                            <tr>
                                <td><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                                <td><asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table>
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server" class="SubEditBlock">
                    <div class="SubEditCommand" style="text-align:left">
                        <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                    </div>
                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
                    Width="100%" AutoGenerateColumns="False"
                    EnableRowsCache="False">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="廠商編號" Caption="<%$ Resources:WebResources, SupplierNo %>" />
                            <dx:GridViewDataHyperLinkColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                <PropertiesHyperLinkEdit NavigateUrlFormatString="~/VSS/CONS/CON04.aspx?No={0}">
                                </PropertiesHyperLinkEdit>
                            </dx:GridViewDataHyperLinkColumn>
                            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
                            <dx:GridViewDataTextColumn FieldName="商品類別" Caption="<%$ Resources:WebResources, ProductCategory %>" />
                            <dx:GridViewDataTextColumn FieldName="上架日期" Caption="<%$ Resources:WebResources, SupportStartDate %>" />
                            <dx:GridViewDataTextColumn FieldName="下架日期" Caption="<%$ Resources:WebResources, SupportExpiryDate %>" />
                            <dx:GridViewDataTextColumn FieldName="停止訂購日" Caption="停止訂購日" />
                            <dx:GridViewDataTextColumn FieldName="人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                            <dx:GridViewDataTextColumn FieldName="日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                        </Columns>
                        <SettingsPager PageSize="10" />
                        <SettingsEditing Mode="Inline" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    </cc:ASPxGridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>