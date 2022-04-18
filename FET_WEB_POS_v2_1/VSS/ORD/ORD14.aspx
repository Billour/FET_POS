<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ORD14.aspx.cs" Inherits="VSS_ORD_ORD14" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef" align="left">
            <!--卡片群組設定-->
            <asp:Literal ID="Literal1" runat="server" Text="卡片群組設定"></asp:Literal>
    </div>
    
    <div class="seperate"></div>
    
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--卡片群組-->
                    <asp:Literal ID="Literal2" runat="server" Text="卡片群組"></asp:Literal>：
                </td>
                <td class="tdval">
                   <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--商品料號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
     </div>

    <div class="seperate"></div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="SubEditBlock">
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="卡片群組" EnableCallBacks="False" 
                    Width="100%" onfocusedrowchanged="gvMaster_FocusedRowChanged" 
                    onhtmlrowprepared="gvMaster_HtmlRowPrepared">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                            <EditButton Visible="true"></EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="2">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="卡片群組" Caption="卡片群組">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>">
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Value='<%# Eval("開始日期") %>'></dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>">
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Value='<%# Eval("結束日期") %>'></dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataTextColumn>
                     </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) { gvMaster.AddNewRow(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" AutoPostBack="false" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                     <SettingsBehavior AllowFocusedRow="True"  ProcessFocusedRowChangedOnServer="True" />
                     <SettingsPager PageSize="5"></SettingsPager>
                    <Settings ShowTitlePanel="True" />
                    <SettingsEditing EditFormColumnCount="3" Mode="EditFormAndDisplayRow" />
                     <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
                <br />
            </div>
            
            <div class="seperate"></div>
            
            <div id="Div_Dt">
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%" Visible="false">
                    <TabPages>
                        <dx:TabPage Text="商品設定">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <div>
                                        <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server"
                                         Width="100%" KeyFieldName="商品料號">
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                    <HeaderTemplate>
                                                        <input type="checkbox" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                    </HeaderTemplate>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                                    <EditButton Visible="true"></EditButton>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="2">
                                                    <DataItemTemplate>
                                                        <%#Container.ItemIndex + 1%>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Templates>
                                                <TitlePanel>
                                                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td>
                                                                  <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>" AutoPostBack="false">
                                                                    <ClientSideEvents Click="function(s, e) { gvDetail.AddNewRow(); }" />
                                                                  </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" AutoPostBack="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </TitlePanel>
                                            </Templates>
                                            <Settings ShowTitlePanel="True" />
                                            <SettingsEditing EditFormColumnCount="3" Mode="EditFormAndDisplayRow" />
                                            <SettingsPager PageSize="5"></SettingsPager>
                                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        </cc:ASPxGridView>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
