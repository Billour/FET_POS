<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ORD11.aspx.cs" Inherits="VSS_ORD_ORD11" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--商品建議訂購量設定-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductSuggestOrderAmountSetting %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--商品編號-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td>
                            <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  />
                            <%--<table style="width: 159px">
                                <tr>
                                    <td>
                                        <dx:ASPxTextBox ID="txtParNoStart" runat="server" Width="120px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ChooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                            SkinID="PopupButton" AutoPostBack="false">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <cc:ASPxPopupControl ID="productsPopup1" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                PopupElementID="ChooseButton1" TargetElementID="txtParNoStart" LoadingPanelID="lp1">
                            </cc:ASPxPopupControl>
                            <dx:ASPxLoadingPanel ID="lp1" runat="server">
                            </dx:ASPxLoadingPanel>--%>
                        </td>
                        <td class="tdtxt">
                            <!--商品名稱-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox4" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
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
                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="Div1" runat="server" class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" KeyFieldName="商品料號" ClientInstanceName="gvMaster"
                            runat="server" Width="100%" OnPageIndexChanged="gvMasterDV_PageIndexChanged"
                            OnRowInserting="gvMasterDV_RowInserting" OnRowUpdating="gvMasterDV_RowUpdating"
                            OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnStartRowEditing="gvMaster_StartRowEditing"
                            OnHtmlEditFormCreated="gvMaster_HtmlEditFormCreated" Settings-ShowTitlePanel="true">
                            <SettingsPager PageSize="5">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Templates>
                                <TitlePanel>
                                    <table align="left">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="<%$ Resources:WebResources, Add %>">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                            </Templates>
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True">
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn ButtonType="Button">
                                    <EditButton Visible="true" Text="<%$ Resources:WebResources, Edit %>">
                                    </EditButton>
                                    <UpdateButton Text="<%$ Resources:WebResources, Save %>">
                                    </UpdateButton>
                                    <CancelButton Text="<%$ Resources:WebResources, Cancel %>">
                                    </CancelButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                    <EditItemTemplate>
                                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" Text='<%# Eval("[商品料號]") %>' />
                                        <%--<table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="TextBox1" runat="server" Text='<%# Eval("[商品料號]") %>' Width="80px">
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" SkinID="PopupButton">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <cc:ASPxPopupControl ID="productsPopup2" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                            PopupElementID="ASPxButton1" TargetElementID="TextBox1" LoadingPanelID="lp2">
                                        </cc:ASPxPopupControl>
                                        <dx:ASPxLoadingPanel ID="lp2" runat="server">
                                        </dx:ASPxLoadingPanel>--%>
                                    </EditItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn ReadOnly="true" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="銷售基準" Caption="<%$ Resources:WebResources, SalesBase %>">
                                    <EditItemTemplate>
                                        <dx:ASPxComboBox ID="ASPxComboBox1" AutoPostBack="True" runat="server" Value='<%# Eval("[銷售基準]") %>'
                                            OnSelectedIndexChanged="ASPxComboBox1_SelectedIndexChanged">
                                            <ClientSideEvents />
                                            <Items>
                                                <dx:ListEditItem Text="半個月" Value="0" />
                                                <dx:ListEditItem Text="一個月" Value="1" />
                                                <dx:ListEditItem Text="指定期間" Value="2" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataColumn FieldName="安全係數" Caption="<%$ Resources:WebResources, SafeOrderCoefficient %>">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </cc:ASPxGridView>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
