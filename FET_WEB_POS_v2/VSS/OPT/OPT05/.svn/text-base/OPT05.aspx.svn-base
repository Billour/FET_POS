<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OPT05.aspx.cs" Inherits="VSS_OPT_OPT05_OPT05" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function CheckStoreNO(s, e) {
            var StoreNO = s.GetText();
            if (StoreNO != '') {
                PageMethods.getStoreInfo(StoreNO, getStoreInfo_OnOK);
            }
        }
        
        function upperText(s, e) {
            if (s.GetText() != '') {
                s.SetText(s.GetText().toUpperCase());
                var str = s.GetText();
                var formatStr = "A,B,C,D,E,F,G,H,I,J,K,L,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                for (var i = 0; i < str.length; i++) {
                    if (formatStr.indexOf(str.substr(i, 1)) < 0) {
                        s.SetText(null);
                        return false;
                    }
                }
            }
        }
        
        function getStoreInfo_OnOK(returnData) {
            if (returnData == '') {
                alert('門市編號不存在，請重新輸入');
                txtStoreName.SetText(null);
                return false;
            }
            else {
                txtStoreName.SetText(returnData);
            }
        }

        function CheckStoreNO2(s, e) {
            var StoreNO = s.GetValue();
            PageMethods.getStoreInfo(StoreNO, getStoreInfo_OnOK2);
        }

        function getStoreInfo_OnOK2(returnData) {
            if (returnData == '') {
                alert('門市編號不存在，請重新輸入');
                txtStoreName2.SetValue(null);
                return false;
            }
            else {
                txtStoreName2.SetValue(returnData);
            }
        }
        
        function CheckRequiredField(s, e) {
            var Value = s.GetValue();
            if (Value == null || Value == "") {
                e.isValid = false;
                e.errorText = '【欄位名稱】不允許空白，請重新輸入';
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--總部發票設定作業 -->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InvoiceSettingHQ %> "></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--門市編號 -->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                </td>
                <td class="tdval" width="80px">
                    <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" SetClientValidationEvent="CheckStoreNO" />
                </td>
                <td class="tdtxt">
                    <!--門市名稱 -->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtStoreName" runat="server" Width="80px" ClientInstanceName="txtStoreName">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--用途 -->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Use %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddlUSE_TYPE" runat="server" Width="100">
                      <Items>
                            <dx:ListEditItem Value="" Text="ALL" Selected="true" />
                            <dx:ListEditItem Value="1" Text="連線" />
                            <dx:ListEditItem Value="2" Text="離線" />
                            <dx:ListEditItem Value="3" Text="手開二聯式" />
                            <dx:ListEditItem Value="4" Text="手開三聯式" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!-- 所屬年月 -->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, YearMonth %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtSDate" runat="server" Width="100px" EditFormat="Custom" EditFormatString="yyyy/MM"
                                    ClientInstanceName="txtSMonth">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkMonth(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtEDate" runat="server" EditFormat="Custom" EditFormatString="yyyy/MM"
                                    ClientInstanceName="txtEMonth">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkMonth(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table align="center" cellpadding="0" cellspacing="0" border="0">
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
    <div class="seperate"></div>
    <div id="Div1" class="SubEditBlock" style="text-align: left;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
            <asp:HiddenField ID="testStore" runat="server" />
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="ASSIGN_ID"
                    Width="100%" EnableCallBacks="false"
                    OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                    OnPageIndexChanged="gvMaster_PageIndexChanged"
                    OnRowUpdating="gvMaster_RowUpdating" 
                    OnRowInserting="gvMaster_RowInserting" 
                    OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared"
                    OnCellEditorInitialize="gvMaster_CellEditorInitialize" 
                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                    OnRowValidating="gvMaster_RowValidating" 
                    onrowcommand="gvMaster_RowCommand" 
                    onstartrowediting="gvMaster_StartRowEditing" >
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                            <HeaderTemplate>
                                <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1" Caption=" ">
                            <EditButton Visible='true'></EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn runat="server" FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="3"
                         PropertiesTextEdit-Style-HorizontalAlign="Right" ReadOnly="true">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STORE_NO" runat="server" VisibleIndex="4">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <DataItemTemplate>
                                 <asp:Label ID="test" runat="server" Text='<%# Bind("STORE_NO") %>'></asp:Label>
                                 <asp:LinkButton ID="Label1" runat="server" Text='<%# Bind("STORE_NO") %>' CommandName="Select"
                                    CommandArgument='<%#Container.ItemIndex%>' >
                                  </asp:LinkButton>
                            </DataItemTemplate>     
                            <EditItemTemplate>
                                <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" ValidationGroup='<%# Container.ValidationGroup %>'
                                    Text='<%# Bind("STORE_NO") %>' IsValidation="true" SetClientValidationEvent="CheckStoreNO2" />
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn UnboundType="String" FieldName="STORENAME" runat="server" Caption="<%$ Resources:WebResources, StoreName %>" VisibleIndex="5">
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="Label1" runat="server" Text='<%# Bind("STORENAME") %>' ClientInstanceName="txtStoreName2" readonly="true" Border-BorderStyle = "None">
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="USE_TYPE" VisibleIndex="6" Width="50">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Use %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <PropertiesComboBox ValueType="System.String">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("USE_TYPE_NAME") %>'></asp:Label>
                            </DataItemTemplate>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="S_USE_YM" VisibleIndex="7">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, YearMonthStart %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="txtS_USE_YM" runat="server" Text='<%# Bind("S_USE_YM") %>' EditFormatString="yyyy/MM" >
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="E_USE_YM" VisibleIndex="8">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, YearMonthEnd %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="txtE_USE_YM" runat="server" Text='<%# Bind("E_USE_YM") %>' EditFormatString="yyyy/MM" >
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="LEADER_CODE" VisibleIndex="9">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, WordTracks %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <PropertiesTextEdit MaxLength="2">
                                     <ValidationSettings>
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                     </ValidationSettings>
                                     <ClientSideEvents TextChanged="function(s,e){ upperText(s,e); }" />
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="INIT_NO" VisibleIndex="10">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StartingNumber %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <PropertiesTextEdit MaxLength="8">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                    <RegularExpression ValidationExpression="\d{8}" ErrorText="起始編號應為8碼數字，請重新輸入" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="END_NO" VisibleIndex="11">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, EndNumber %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate> 
                            <PropertiesTextEdit MaxLength="8">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                    <RegularExpression ValidationExpression="\d{8}" ErrorText="終止編號應為8碼數字，請重新輸入" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CURRENT_NO" Caption="<%$ Resources:WebResources, TheCurrentNumber %>" VisibleIndex="12" ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Right" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SHEET_COUNT" Caption="<%$ Resources:WebResources, InvoiceNumberOfSheets %>" VisibleIndex="13" ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Right" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" VisibleIndex="14" ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" VisibleIndex="15" ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, add %>"
                                            OnClick="btnAdd_Click" CausesValidation="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, delete %>"
                                            OnClick="btnDelete_Click" CausesValidation="false">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>"
                                            CausesValidation="false">
                                        </dx:ASPxButton>
                                        <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                            CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl="~/VSS/OPT/OPT05/OPT05_Import.aspx"
                                            Width="900" Height="500" LoadingPanelID="lp" HeaderText="總部發票檔匯入">
                                            <ContentStyle>
                                                <Paddings Padding="4px"></Paddings>
                                            </ContentStyle>
                                        </cc:ASPxPopupControl>
                                        <dx:ASPxLoadingPanel ID="lp" runat="server">
                                        </dx:ASPxLoadingPanel>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <SettingsBehavior AllowFocusedRow="true" />
                    <StylesFilterControl EnableDefaultAppearance="False"></StylesFilterControl>
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <Settings ShowTitlePanel="True"></Settings>
                </cc:ASPxGridView>
                <div class="seperate"></div>
                <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" Width="100%" Visible="false">
                    <Columns>
                        <dx:GridViewDataTextColumn runat="server" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="2">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="HOST_NO" runat="server" Caption="<%$ Resources:WebResources, CashRegisterNo %>" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="START_NO" runat="server" Caption="<%$ Resources:WebResources, StartingNumber %>" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="END_NO" runat="server" Caption="<%$ Resources:WebResources, EndNumber %>" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SHEET_COUNT" runat="server" Caption="<%$ Resources:WebResources, NumberOfSheets %>" VisibleIndex="6" ReadOnly="true">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" runat="server" Caption="<%$ Resources:WebResources, TheDateOfTheInvoiceDistribution %>" VisibleIndex="7" ReadOnly="true">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
