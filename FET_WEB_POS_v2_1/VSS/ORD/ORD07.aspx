<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ORD07.aspx.cs" Inherits="VSS_ORD_ORD07" %>

<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "商品料號查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--Non-DropShipment主配查詢作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, NonDropShipmentProductDistributionSearch %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--主配單號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DistributionNo %>"></asp:Literal>:
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="120px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--出貨倉別-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ShipmentWarehouse %>"></asp:Literal>:
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ddlArea" runat="server" Width="120px">
                                <Items>
                                    <dx:ListEditItem Text="請選擇" Value="0" Selected="true"/>
                                    <dx:ListEditItem Text="Retail-北" Value="1" />
                                    <dx:ListEditItem Text="Retail-中" Value="2" />
                                    <dx:ListEditItem Text="Retail-南" Value="3" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            <!--狀 態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                                <Items>
                                    <dx:ListEditItem Text="請選擇" Value="0" Selected="true" />
                                    <dx:ListEditItem Text="已存檔" Value="1" />
                                    <dx:ListEditItem Text="巳刪除" Value="2" />
                                    <dx:ListEditItem Text="已上傳" Value="3" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--主配日期-->
                            <span style="color: Red">*</span><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, DistributionDate %>"></asp:Literal>:
                        </td>
                        <td class="tdval" colspan="3">
                            <table style="width: 200px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt">
                            商品料號：
                        </td>
                        <td class="tdval">
                            <uc2:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  />
                           <%-- <table style="width: 100px">
                                <tr>
                                    <td>
                                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="110px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, choose %>" AutoPostBack="false" SkinID="PopupButton">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <cc:ASPxPopupControl ID="ASPxPopupControl2" SkinID="ProductsPopup" runat="server"
                                EnableViewState="False" PopupElementID="ASPxButton1" TargetElementID="ASPxTextBox2"
                                LoadingPanelID="lp1">
                            </cc:ASPxPopupControl>
                            <dx:ASPxLoadingPanel ID="lp1" runat="server">
                            </dx:ASPxLoadingPanel>--%>
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
                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div>
                        <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" AutoGenerateColumns="False"
                            Width="100%" OnPageIndexChanged="gvMasterDV_PageIndexChanged" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                            OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                            <SettingsEditing Mode="Inline" />
                            <SettingsPager PageSize="10" AlwaysShowPager="True">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="主配單號" Caption="<%$ Resources:WebResources, DistributionNo %>">
                                    <DataItemTemplate>
                                        <asp:HyperLink ID="hlkdno1" runat="server" Text='<%# Bind("[主配單號]") %>'></asp:HyperLink>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="主配日期" Caption="<%$ Resources:WebResources, DistributionDate %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="出貨倉別" Caption="<%$ Resources:WebResources, ShipmentWarehouse %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                </EmptyDataRow>
                            </Templates>
                        </cc:ASPxGridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
        </div>
    </div>
</asp:Content>
