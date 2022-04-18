<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="OPT18.aspx.cs" Inherits="VSS_OPT_OPT18" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        _s = null;
        _e = null;
        function CheckStoreNO(s, e) {
            _s = s;
            _e = e;
            var StoreNO = s.GetValue();
            PageMethods.getStoreInfo(StoreNO, getStoreInfo_OnOK);
        }

        function getStoreInfo_OnOK(returnData)
        {
            if (returnData == '')
            {
                alert('門市編號不存在，請重新輸入');
                _s.SetText(null);
                txtStoreName.SetText(null);
                _e.processOnServer = false;
            }
            else
            {
                txtStoreName.SetText(returnData);
            }
        }


        function onOK()
        {
            __doPostBack('<%= txtBatchNO.UniqueID %>', 'AAA');
        }

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <input type="hidden" id="txtBatchNO" runat="server" class="txtBatchNO" />
    <div>
        <div class="titlef">
            <!--門市特殊客訢處理折扣設定 -->
            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, DiscountStoreManagerSettings %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="lblStoreNo" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" SetClientValidationEvent="CheckStoreNO" />
                    </td>
                    <td class="tdtxt">
                        <!--門市名稱-->
                        <asp:Literal ID="lblStoreName" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtStoreName" runat="server" ClientInstanceName="txtStoreName">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--折扣月份-->
                        <asp:Literal ID="lblDiscountsMonth" runat="server" Text="<%$ Resources:WebResources, DiscountsMonth %>"></asp:Literal>：
                    </td>
                    <td colspan="5">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 240px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtDiscountsMonth_S" runat="server" EditFormatString="yyyy/MM"
                                        ClientInstanceName="txtSMonth">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkMonth(s, e); }" />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtDiscountsMonth_E" runat="server" EditFormatString="yyyy/MM"
                                        ClientInstanceName="txtEMonth">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkMonth(s, e); }" />
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
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" CausesValidation="false" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="SSD_ID"
                    Width="100%" EnableCallBacks="False" 
                    OnPageIndexChanged="gvMaster_PageIndexChanged"
                    OnRowInserting="gvMaster_RowInserting" 
                    OnRowUpdating="gvMaster_RowUpdating" 
                    OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                    OnFocusedRowChanged="gvMaster_FocusedRowChanged" 
                    OnRowValidating="gvMaster_RowValidating"
                    OnStartRowEditing="gvMaster_StartRowEditing" 
                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                    OnInitNewRow="gvMaster_OnInitNewRow">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <div style="text-align: center">
                                    <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button">
                            <EditButton Visible="True">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                            <EditFormSettings Visible="false" />
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreNo %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" Text='<%# Bind("STORE_NO") %>'
                                    IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>' />
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreName %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="YYMM" Caption="<%$ Resources:WebResources, DiscountsMonth %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, DiscountsMonth %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="txtYYYYMM" runat="server" Text='<%# Bind("YYMM") %>' EditFormatString="yyyy/MM"
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                     PopupHorizontalAlign ="Center" PopupVerticalAlign="Middle">
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                    <ClientSideEvents ValueChanged="function(s, e){ chkIsMonth(s, e); }" />
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="DIS_AMT" Caption="<%$ Resources:WebResources, TotalDiscount %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TotalDiscount %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtDiscountQuota" runat="server" Width="100%" Text='<%# Bind("DIS_AMT") %>'
                                    HorizontalAlign="Right" MaxLength="7" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入值需為正整數，請重新輸入" />
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                            <EditFormSettings Visible="false" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table cellpadding="0" cellspacing="0" align="left">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddNewM" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            AutoPostBack="false" CausesValidation="false">
                                            <ClientSideEvents Click="function(s, e) { gvMaster.AddNewRow();  }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDeleteM" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                            CausesValidation="false" OnClick="btnDeleteM_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>"
                                            CausesValidation="false" />
                                        <dx:ASPxTextBox ID="txtBatchNO1" runat="server" Width="170px" ClientVisible="false">
                                        </dx:ASPxTextBox>
                                        <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                            CloseAction="CloseButton" ContentUrl="~/VSS/OPT/OPT18/OPT18_Import.aspx" PopupHorizontalAlign="Center"
                                            PopupVerticalAlign="WindowCenter" ShowFooter="false" onOKScript="onOK" Width="800px"
                                            Height="550px" TargetElementID="txtBatchNO1" Modal="true" HeaderText="門市特殊客訴處理折扣設定Excel上傳"
                                            ClientInstanceName="dataImportPopup" PopupElementID="btnImport" LoadingPanelID="lp">
                                        </cc:ASPxPopupControl>
                                        <dx:ASPxLoadingPanel ID="lp" runat="server">
                                        </dx:ASPxLoadingPanel>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lblError" runat="server" ForeColor="Red">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <Styles>
                        <TitlePanel HorizontalAlign="Left">
                        </TitlePanel>
                        <EditFormColumnCaption Wrap="False">
                        </EditFormColumnCaption>
                    </Styles>
                    <SettingsPager PageSize="5">
                    </SettingsPager>
                    <Settings ShowTitlePanel="true" />
                    <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="True" />
                    <SettingsEditing EditFormColumnCount="3" Mode="EditFormAndDisplayRow" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
                <div class="seperate">
                </div>
                <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" Width="100%"
                    KeyFieldName="SSDD_ID" OnPageIndexChanged="gvDetail_PageIndexChanged" OnRowInserting="gvDetail_RowInserting"
                    OnRowUpdating="gvDetail_RowUpdating" OnRowValidating="gvDetail_RowValidating"
                    OnHtmlRowPrepared="gvDetail_HtmlRowPrepared" OnHtmlEditFormCreated="gvDetail_HtmlEditFormCreated"
                    OnCommandButtonInitialize="gvDetail_CommandButtonInitialize" OnStartRowEditing="gvDetail_StartRowEditing">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <div style="text-align: center">
                                    <input type="checkbox" id="checkbox2" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                            <EditButton Visible="True">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                            ReadOnly="true">
                            <EditFormSettings Visible="false" />
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="ROLE_NAME" Caption="<%$ Resources:WebResources, Role %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Role %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxComboBox ID="ddlRole" runat="server" SelectedIndex="0" ValueType="System.String"
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="DIS_AMT" Caption="<%$ Resources:WebResources, Amount %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Amount %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtMoneyAmt" runat="server" HorizontalAlign="Right" Text='<%# BIND("DIS_AMT") %>'
                                    MaxLength="7" Width="100px" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入值需為正整數，請重新輸入" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="DIS_RATE" Caption="<%$ Resources:WebResources, Ratio %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Ratio %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtDisRate" MaxLength="3" runat="server" Width="40px" Text='<%#BIND("[DIS_RATE]")%>'
                                                HorizontalAlign="Right" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                                 <ValidationSettings>
                                                    <RegularExpression ValidationExpression="\d*" ErrorText="輸入值需為正整數，請重新輸入" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            %
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="Literal8" runat="server" Text='<%# Bind("DIS_RATE") %>'>
                                </dx:ASPxLabel>
                                <span>%</span>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="DIS_AMT_UBOND" Caption="<%$ Resources:WebResources, LimitTheAmountOfDiscount %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, LimitTheAmountOfDiscount %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtDisUpbon" runat="server" HorizontalAlign="Right" Text='<%# Eval("DIS_AMT_UBOND") %>'
                                    ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="7"
                                    Width="100">
                                    <ValidationSettings>
                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入值需為正整數，請重新輸入" />
                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table cellpadding="0" cellspacing="0" align="left">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddNewD" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            CausesValidation="false" OnClick="btnAddNewD_Click" ClientEnabled="true">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDeleteD" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                            CausesValidation="false" OnClick="btnDeleteD_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lblErrorD" runat="server" ForeColor="Red">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <Styles>
                        <TitlePanel HorizontalAlign="Left">
                        </TitlePanel>
                        <EditFormColumnCaption Wrap="False">
                        </EditFormColumnCaption>
                    </Styles>
                    <SettingsPager PageSize="5">
                    </SettingsPager>
                    <Settings ShowTitlePanel="true" />
                    <SettingsEditing EditFormColumnCount="3" Mode="EditFormAndDisplayRow" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
