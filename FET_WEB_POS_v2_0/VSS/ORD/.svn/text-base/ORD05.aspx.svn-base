<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ORD05.aspx.cs" Inherits="VSS_ORD05_ORD05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--一搭一查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TwoForOneOfferSearch %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                            AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
	document.location='ORD06.aspx';return false;
}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--主商品料號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PrimaryProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="150px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--搭配日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TieinSaleDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <table style="width: 200px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="postbackDate_TextBox1" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;<asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="postbackDate_TextBox2" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMasterDV" runat="server" ClientInstanceName="gvMasterDV" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="商品編號" SettingsEditing-Mode="Inline" OnPageIndexChanged="gvMasterDV_PageIndexChanged">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="商品編號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"
                                    VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"
                                    VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="數量" runat="server" Caption="<%$ Resources:WebResources, Quantity %>"
                                    VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="搭配日期" runat="server" Caption="<%$ Resources:WebResources, TieinSaleDate %>"
                                    VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="人員" runat="server" Caption="<%$ Resources:WebResources, Staff %>"
                                    VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="日期" runat="server" Caption="<%$ Resources:WebResources, Date %>"
                                    VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                             <Templates>
                            <TitlePanel>
                               
                            </TitlePanel>
                            <EmptyDataRow>
                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                            </EmptyDataRow>
                        </Templates>
                            <SettingsPager PageSize="5">
                            </SettingsPager>
                        </cc:ASPxGridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
