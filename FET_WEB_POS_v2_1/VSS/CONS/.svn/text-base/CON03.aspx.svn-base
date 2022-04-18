<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON03.aspx.cs" Inherits="VSS_CONS_CON03" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品查詢作業(總部)-->
                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductSearchHQ %>"></asp:Literal>
                </td>
            </tr>
        </table>
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
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                <dx:ListEditItem Value="1" Text="ALL" />
                                <dx:ListEditItem Value="2" Text="AC1" />
                                <dx:ListEditItem Value="3" Text="AC2" />
                            </Items>
                        </dx:ASPxComboBox>
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
                        <uc1:PopupControl ID="SelectProduct1" runat="server" PopupControlName="ProductsPopup"  />

                        <%--<table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        AutoPostBack="false" SkinID="PopupButton" />
                                </td>
                            </tr>
                        </table>--%>
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
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="SupportStartDateFrom" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="SupportStartDateTo" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
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
                                <td>
                                    <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="SupportExpiryDateFrom" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="SupportExpiryDateTo" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--商品類別-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlProductCategory" runat="server">
                            <Items>
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                <dx:ListEditItem Value="1" Text="3G Handset" />
                                <dx:ListEditItem Value="2" Text="SIM Card" />
                                <dx:ListEditItem Value="3" Text="3G Accessory" />
                                <dx:ListEditItem Value="4" Text="On Line Recharge" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--停止訂購日-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, OrderEndDay %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
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
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server" class="SubEditBlock">
                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
                        Width="100%" AutoGenerateColumns="False" EnableRowsCache="False" Settings-ShowTitlePanel="true">
                        <Columns>
                            <dx:GridViewDataHyperLinkColumn FieldName="廠商編號" Caption="<%$ Resources:WebResources, SupplierNo %>">
                                <PropertiesHyperLinkEdit NavigateUrlFormatString="~/VSS/CONS/CON04.aspx?No={0}">
                                </PropertiesHyperLinkEdit>
                            </dx:GridViewDataHyperLinkColumn>
                            <dx:GridViewDataTextColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
                            <dx:GridViewDataTextColumn FieldName="商品類別" Caption="<%$ Resources:WebResources, ProductCategory %>" />
                            <dx:GridViewDataTextColumn FieldName="上架日期" Caption="<%$ Resources:WebResources, SupportStartDate %>" />
                            <dx:GridViewDataTextColumn FieldName="下架日期" Caption="<%$ Resources:WebResources, SupportExpiryDate %>" />
                            <dx:GridViewDataTextColumn FieldName="停止訂購日" Caption="<%$ Resources:WebResources, OrderEndDay %>" />
                            <dx:GridViewDataTextColumn FieldName="人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                            <dx:GridViewDataTextColumn FieldName="日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                    align="left" />
                                </div>
                            </TitlePanel>
                        </Templates>
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
<%--    <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" PopupElementID="btnChooseProduct"
        TargetElementID="ASPxTextBox1" LoadingPanelID="lp">
    </cc:ASPxPopupControl>
    <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
--%></asp:Content>
