<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="OPT17.aspx.cs" Inherits="VSS_OPT_OPT17" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--門市手開發票號碼設定-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HandInvoiceNumberSet %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup"  />
                       <%-- <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox2" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        SkinID="PopupButton">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server" EnableViewState="False"
                            PopupElementID="Button1" TargetElementID="TextBox2">
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </cc:ASPxPopupControl>--%>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox1" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--所屬年月-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, YearMonth %>"></asp:Literal>：
                    </td>
                    <td nowrap="nowrap">
                         <table cellpadding="0" cellspacing="0" border="0">
                            <tr>                         
                            <td><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                            <td>&nbsp;</td>
                            <td><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>                            
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm);}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server">

                    <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="項次"
                        AutoGenerateColumns="False" Width="100%" Settings-ShowTitlePanel="true"
                        OnPageIndexChanged="gvMaster_PageIndexChanged" 
                        onrowinserting="gvMaster_RowInserting" onrowupdating="gvMaster_RowUpdating">
                        <SettingsPager PageSize="5">
                        </SettingsPager>
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center"
                                ButtonType="Button">
                                <HeaderTemplate>
                                    <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);"
                                        title="Select/Unselect all rows on the page" />
                                </HeaderTemplate>
                               <%-- <UpdateButton Text="儲存">
                                </UpdateButton>
                                <CancelButton Text="取消">
                                </CancelButton>--%>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewCommandColumn VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" ButtonType="Button">
                                <EditButton Visible="true">
                                </EditButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                VisibleIndex="2" ReadOnly="true">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>"
                                VisibleIndex="3" ReadOnly="true">
                                <EditFormSettings Visible="False" />
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>"
                                VisibleIndex="4" ReadOnly="true">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="發票格式" Caption="<%$ Resources:WebResources, InvoiceFormat %>" VisibleIndex="5">
                                <PropertiesComboBox>
                                    <Items>
                                        <dx:ListEditItem Text="手開二聯式" Value="手開二聯式" />
                                        <dx:ListEditItem Text="手開三聯式" Value="手開三聯式" />
                                    </Items>
                                </PropertiesComboBox>
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataDateColumn FieldName="所屬年月起" Caption="<%$ Resources:WebResources, YearMonthStart %>" VisibleIndex="6">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataDateColumn FieldName="所屬年月訖" Caption="<%$ Resources:WebResources, YearMonthEnd %>" VisibleIndex="7">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataColumn FieldName="字軌" Caption="<%$ Resources:WebResources, WordTracks %>" VisibleIndex="8">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="起始編號" Caption="<%$ Resources:WebResources, StartingNumber %>" VisibleIndex="9">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="終止編號" Caption="<%$ Resources:WebResources, EndNumber %>" VisibleIndex="10">
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="發票張數" Caption="<%$ Resources:WebResources, InvoiceNumberOfSheets %>" VisibleIndex="11" ReadOnly="true">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                VisibleIndex="12" ReadOnly="true">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                VisibleIndex="13" ReadOnly="true">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                OnClick="btnAdd_Click">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <SettingsEditing EditFormColumnCount="4" />
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
