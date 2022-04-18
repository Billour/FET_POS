<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ORD02.aspx.cs" Inherits="VSS_ORD_ORD02" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--訂單查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderSearch %>"></asp:Literal>
                    </td>
                    <td align="right">
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div align="left">
                <table width="100%">
                    <tr>
                        <td class="tdtxt">
                            <!--訂單日期-->
                            <span style="color: Red">*</span><asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" align="left">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                        </asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit runat="server" ID="postbackDate_TextBox1">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, End %>">
                                        </asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit runat="server" ID="postbackDate_TextBox2">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--商品編號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td>
                            <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  />
                            <%--<table sytle="widht:100px">
                                <tr>
                                    <td>
                                        <dx:ASPxTextBox ID="txtParNoStart" runat="server" Width="60px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ChooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                            AutoPostBack="false" SkinID="PopupButton" />
                                    </td>
                                </tr>
                            </table>
                            <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                PopupElementID="ChooseButton1" TargetElementID="txtParNoStart" LoadingPanelID="lp1">
                            </cc:ASPxPopupControl>
                            <dx:ASPxLoadingPanel ID="lp1" runat="server">
                            </dx:ASPxLoadingPanel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                                OnClick="btnSearch_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <cc:ASPxGridView ID="gvMasterDV" runat="server" ClientInstanceName="gvMasterDV" AutoGenerateColumns="False"
                Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="訂單日期" Caption="<%$ Resources:WebResources, OrderDate %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="0" Visible="false" FieldName="區域" Caption="<%$ Resources:WebResources, District %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="1" Visible="false" FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="2" Visible="false" FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="訂單編號" Caption="<%$ Resources:WebResources, OrderNo %>">
                        <DataItemTemplate>
                            <div align="left">
                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text='<%#BIND("訂單編號") %>' NavigateUrl='~/VSS/ORD/ORD01_view.aspx'>
                                </dx:ASPxHyperLink>
                            </div>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="預訂單號" Caption="<%$ Resources:WebResources, BookOrderNumber %>">
                        <DataItemTemplate>
                            <div align="left">
                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text='<%#BIND("預訂單號") %>' NavigateUrl='~/VSS/ORD/ORD12_view.aspx'>
                                </dx:ASPxHyperLink>
                            </div>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="人員" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="日期" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <EmptyDataRow>
                        <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                    </EmptyDataRow>
                    <TitlePanel>
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, CollocationProduct %>"></asp:Literal>
                    </TitlePanel>
                </Templates>
            </cc:ASPxGridView>
            <div class="seperate">
            </div>
        </div>
    </div>
</asp:Content>
