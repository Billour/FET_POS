<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD14.aspx.cs" Inherits="VSS_ORD_ORD14_ORD14"
    MasterPageFile="~/MasterPage.master" ValidateRequest="true" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--卡片群組設定-->
        <asp:Literal ID="Literal1" runat="server" Text="卡片群組設定"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--卡片群組-->
                    <asp:Literal ID="Literal2" runat="server" Text="卡片群組"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--商品料號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="170px">
                    </dx:ASPxTextBox>
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
                    <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>">
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="SubEditBlock">

                <script type="text/javascript">
                    function getPRODINFO(s, e) {
                        this.EventArgs = e;
                        this.Sender = s;
                        var fName = "3_pcPRODNO_txtControl";
                        txtPRODNAME = getClientInstance('TxtBox', Sender.name.replace(fName, "4_txtPRODNAME"));
                        if (s.GetText() != '')
                            PageMethods.getPRODINFO(Sender.GetText(), getPRODINFO_OnOK);
                        else
                            txtPRODNAME.SetText('');
                        EventArgs.processOnServer = false;
                    }

                    function getPRODINFO_OnOK(returnData) {
                        var fName = "3_pcPRODNO_txtControl";
                        txtPRODNAME = getClientInstance('TxtBox', Sender.name.replace(fName, "4_txtPRODNAME"));
                        if (returnData == '') {
                            EventArgs.processOnServer = false;
                            alert("商品料號不存在，請重新輸入!");
                            Sender.Focus();
                            txtPRODNAME.SetText('');
                        }
                        else {
                            //0 品名 1 I MEM_FLAGE
                            txtPRODNAME.SetText(returnData);
                        }
                    }

                    //不選取DISENABLED的CHECKBOX
                    function CheckAll_gvMaster(checked) {
                        var sTitle = 'ctl00_MainContentPlaceHolder_gvMaster_DXSelBtn';
                        for (var i = 0; i < gvMaster.pageRowCount; i++) {
                            var chk2 = document.getElementById(sTitle + (i + gvMaster.visibleStartIndex));
                            if (chk2 && !chk2.disabled) {
                                gvMaster.SelectRowOnPage(i + gvMaster.visibleStartIndex, checked);
                            }

                        }
                    }
                    function CheckAll_gvDetail(checked) {
                        var sTitle = 'ctl00_MainContentPlaceHolder_ASPxPageControl1_gvDetail_DXSelBtn';
                        for (var i = 0; i < gvDetail.pageRowCount; i++) {
                            var chk2 = document.getElementById(sTitle + (i + gvDetail.visibleStartIndex));
                            if (chk2 && !chk2.disabled) {
                                gvDetail.SelectRowOnPage(i + gvDetail.visibleStartIndex, checked);
                            }

                        }
                    }
                    function gvMasterAdd(s, e) {
                        gvMaster.AddNewRow();
                    }
                </script>

                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="SIM_GROUP_ID"
                    EnableCallBacks="False" Width="100%" OnFocusedRowChanged="gvMaster_FocusedRowChanged"
                    OnRowUpdating="gvMaster_RowUpdating" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                    OnInitNewRow="gvMaster_InitNewRow" OnRowInserting="gvMaster_RowInserting" OnRowValidating="gvMaster_RowValidating"
                    OnStartRowEditing="gvMaster_StartRowEditing" OnCellEditorInitialize="gvMaster_CellEditorInitialize"
                    OnPageIndexChanged="gvMaster_PageIndexChanged" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <input type="checkbox" onclick="if (typeof(gvMaster)!='undefined' ){ CheckAll_gvMaster(this.checked); }"
                                    title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                            VisibleIndex="2">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="SIM_GROUP_NAME" Caption="卡片群組">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn VisibleIndex="4" FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartDate %>">
                            <PropertiesDateEdit EditFormatString="yyyy/MM/dd" EditFormat="Custom" />
                            <EditCellStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn VisibleIndex="5" FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>">
                            <PropertiesDateEdit EditFormatString="yyyy/MM/dd" EditFormat="Custom" />
                            <EditCellStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btnNew_Click">
                                            <%--<ClientSideEvents Click="function(s, e) { gvMasterAdd(s, e) }" />--%>
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" SkinID="DeleteBtn" AutoPostBack="false" runat="server"
                                            Text="<%$ Resources:WebResources, Delete %>" OnClick="btnDelete_Click">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
                    <SettingsPager PageSize="5">
                    </SettingsPager>
                    <Settings ShowTitlePanel="True" />
                    <SettingsEditing EditFormColumnCount="5" Mode="EditFormAndDisplayRow" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
            </div>
            <div class="seperate">
            </div>
            <div id="Div_Dt">
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%"
                    Visible="false">
                    <TabPages>
                        <dx:TabPage Text="商品設定">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <div>
                                        <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" Width="100%"
                                            EnableCallBacks="False" KeyFieldName="SIM_GROUP_PROD" OnStartRowEditing="gvDetail_StartRowEditing"
                                            OnCommandButtonInitialize="gvDetail_CommandButtonInitialize" OnInitNewRow="gvDetail_InitNewRow"
                                            OnRowInserting="gvDetail_RowInserting" OnRowUpdating="gvDetail_RowUpdating" OnRowValidating="gvDetail_RowValidating"
                                            OnPageIndexChanged="gvDetail_PageIndexChanged" OnHtmlRowPrepared="gvDetail_HtmlRowPrepared">
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <HeaderTemplate>
                                                        <input type="checkbox" onclick="if(typeof(gvDetail)!='undefined'){CheckAll_gvDetail(this.checked);}"
                                                            title="Select/Unselect all rows on the page" />
                                                    </HeaderTemplate>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                                    <EditButton Visible="true">
                                                    </EditButton>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                                    VisibleIndex="2">
                                                    <DataItemTemplate>
                                                        <%#Container.ItemIndex + 1%>
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"
                                                    VisibleIndex="3">
                                                    <EditItemTemplate>
                                                        <uc1:PopupControl ID="pcPRODNO" runat="server" PopupControlName="ProductsPopup" Text='<%#BIND("PRODNO") %>'
                                                            SetClientValidationEvent="getPRODINFO(s,e);" />
                                                    </EditItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                                    VisibleIndex="4">
                                                    <EditItemTemplate>
                                                        <dx:ASPxTextBox ID="txtPRODNAME" Text='<%# Bind("PRODNAME") %>' Enabled="false" runat="server"
                                                            ReadOnly="true" Width="200" ForeColor="Blue">
                                                        </dx:ASPxTextBox>
                                                    </EditItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Templates>
                                                <TitlePanel>
                                                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                    AutoPostBack="false">
                                                                    <ClientSideEvents Click="function(s, e) { gvDetail.AddNewRow(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="btnDelete" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                                    AutoPostBack="false" OnClick="Button4_Click">
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </TitlePanel>
                                            </Templates>
                                            <Settings ShowTitlePanel="True" />
                                            <SettingsEditing EditFormColumnCount="4" Mode="EditFormAndDisplayRow" />
                                            <SettingsPager PageSize="5">
                                            </SettingsPager>
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
