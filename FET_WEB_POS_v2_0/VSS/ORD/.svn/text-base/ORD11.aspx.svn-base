<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ORD11.aspx.cs" Inherits="VSS_ORD11_ORD11" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
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
                            <table style="width: 159px">
                                <tr>
                                    <td>
                                        <dx:ASPxTextBox ID="txtParNoStart" runat="server" Width="120px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ChooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                            SkinID="PopupButton">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="ProductSearch" PopupButtonID="ChooseButton1"
                                TargetControlID="txtParNoStart" Width="500" Height="500" NavigateUrl="~/VSS/ORD/ORD01_searchProductNo.aspx" />
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
                                    <%--     <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, CollocationProduct %>"></asp:Literal>
    --%>
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
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                </EmptyDataRow>
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
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="TextBox1" runat="server" Text='<%# Eval("[商品料號]") %>' Width="80px">
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="選" Width="20px" SkinID="PopupButton">
                                                        <ClientSideEvents Click="function(s,e){window.open('../ORD/ORD01_searchProductNo.aspx',640,400);return false;}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn ReadOnly="true" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <%-- <dx:GridViewDataComboBoxColumn FieldName="銷售基準" Caption="<%$ Resources:WebResources, SalesBase %>">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="半個月" Value="0" />
                                            <dx:ListEditItem Text="一個月" Value="1" />
                                            <dx:ListEditItem Text="指定期間" Value="2" />
                                        </Items>
                                    </PropertiesComboBox>
                                    
                                    <EditFormCaptionStyle Wrap="False">
                                    </EditFormCaptionStyle>
                                </dx:GridViewDataComboBoxColumn>--%>
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
                                <dx:GridViewDataTextColumn FieldName="訂貨天數上限" Caption="訂貨天數上限">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="訂貨天數下限" Caption="訂貨天數下限">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataColumn FieldName="安全係數" Caption="<%$ Resources:WebResources, SafeOrderCoefficient %>">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Templates>
                                <DetailRow>
                                    <cc:ASPxGridView ID="gvDetailDV" Width="80%" runat="server" ClientInstanceName="gvDetailDV"
                                        AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableRowsCache="true">
                                        <Columns>
                                            <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="搭配量" Caption="搭配數量">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="訂購量" Caption="<%$ Resources:WebResources, OrderQuantity %>">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabe2" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <div align="left">
                                                    <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, CollocationProduct %>"></asp:Literal>
                                                </div>
                                            </TitlePanel>
                                        </Templates>
                                        <SettingsDetail IsDetailGrid="true" />
                                    </cc:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            
                            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                        </cc:ASPxGridView>
                        <div class="seperate">
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
                <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                </Triggers>--%>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
