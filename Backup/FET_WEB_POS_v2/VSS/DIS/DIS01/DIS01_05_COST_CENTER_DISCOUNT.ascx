<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DIS01_05_COST_CENTER_DISCOUNT.ascx.cs" Inherits="COST_CENTER_DISCOUNT" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<script language="javascript" type="text/javascript">

    function EnabledbtnAdd(s, e) {
        var Value = cbDisType.GetValue();
        if (Value == "6" || Value == "7") {
            //**2011/04/08 Tina：【加價購】和【贈品設定】只能有一個成本中心
            if (gvCCD.pageRowCount >= 1) {
                if (typeof(btnCCDAdd) != "undefined") btnCCDAdd.SetEnabled(false);
            }
        }
    }

    function ModifyCostCenterPopupURL() {
        var url = PopupCCDiscount.GetContentUrl();
        var s = url.split('?');
        if (s.length > 1) {
            var ordDisType = s[1];
            var newDisType = "cbDisType=" + cbDisType.GetValue();
            url = url.replace(ordDisType, newDisType);
        }
        //PopupCCDiscount.SetContentUrl(url);

        //**2011/04/27 Tina：將URL傳遞的參數加密。
        ModifyPopupURLByEncrypt(url, PopupCCDiscount);

        event.returnValue = false;
        return false;
    }

</script>

<dx:ASPxCallbackPanel ID="ac5" runat="server" ClientInstanceName="ac5" OnCallback="ac5_Callback">
    <PanelCollection>
        <dx:PanelContent>
            <cc:ASPxGridView ID="gvCCD" ClientInstanceName="gvCCD" runat="server" Width="100%"
                AutoGenerateColumns="false" KeyFieldName="COSTCENTER_DIS_ID"
                OnRowInserting="gvCCD_RowInserting" 
                OnCellEditorInitialize="gvCCD_CellEditorInitialize"
                OnRowValidating="gvCCD_RowValidating" 
                OnRowUpdating="gvCCD_RowUpdating" 
                OnStartRowEditing="gvCCD_StartRowEditing" 
                OnPageIndexChanged="gvCCD_PageIndexChanged">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <div style="text-align: center">
                                <input type="checkbox" onclick="gvCCD.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </div>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn ButtonType="Button">
                        <HeaderCaptionTemplate>
                        </HeaderCaptionTemplate>
                        <EditButton Visible="true">
                        </EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="COST_CENTER_NO" Caption="成本中心">
                        <EditItemTemplate>
                            <uc1:PopupControl ID="txtCCDNo" runat="server" PopupControlName="CostCenterPopup"
                                TextBoxClientInstanceName="txtCCDNo"    
                                Text='<%#BIND("COST_CENTER_NO") %>' OnClientTextChanged="function(s,e){ getProd_CategInfo(s,e); }"
                                IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>' />
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="PROD_CATEG" Caption="商品分類">
                        <DataItemTemplate>
                            <dx:ASPxLabel ID="lblPROD_CATEGNAME" runat="server" Text='<%# BIND("PROD_CATEGNAME") %>'>
                            </dx:ASPxLabel>
                        </DataItemTemplate>
                        <PropertiesComboBox ValueType="System.String" ClientInstanceName="cbPROD_CATEG">
                            <ClientSideEvents ValueChanged="function(s,e){ getAccountInfo(s, e); }" />
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="ACCOUNTCODE" Caption="會計科目">
                        <%--<PropertiesTextEdit ClientInstanceName="ACCOUNTCODE" MaxLength="23">
                            <ValidationSettings SetFocusOnError="true">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                <RegularExpression ValidationExpression="\d{23}" ErrorText="格式錯誤" />
                            </ValidationSettings>
                        </PropertiesTextEdit>--%>
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtAccountCode" runat="server" Width="120px" MaxLength="6" ClientInstanceName="ACCOUNTCODE"
                                Text='<%# GetAccountCode((string)Eval("ACCOUNTCODE")) %>'>
                                <ValidationSettings SetFocusOnError="true">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    <RegularExpression ValidationExpression="\d{6}" ErrorText="格式錯誤" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AMT" Caption="金額">
                        <PropertiesTextEdit Width="100" Style-HorizontalAlign="Right">
                            <ValidationSettings SetFocusOnError="true">
                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                <RegularExpression ValidationExpression="\d*" ErrorText="請輸入正整數" />
                            </ValidationSettings>
                            <Style HorizontalAlign="Right"></Style>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="REMARK" Caption="備註">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnCCDAdd" runat="server" Text="<%$ Resources:WebResources, Add %>" ClientInstanceName="btnCCDAdd"
                                        OnClick="btnCCDAdd_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton SkinID="DeleteBtn" ID="btnCCDDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                        CausesValidation="false" OnClick="btnCCDDelete_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnImport3" runat="server" Text="<%$ Resources:WebResources, Import %>" CausesValidation="false" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) { ModifyCostCenterPopupURL(); }" />
                                    </dx:ASPxButton>
                                    <cc:ASPxPopupControl ID="ASPxPopupControl4" ClientInstanceName="PopupCCDiscount" runat="server" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" PopupElementID="btnImport3" ContentUrl="~/VSS/DIS/DIS01/DIS01_Cost_Import.aspx?cbDisType=aa"
                                        Width="640" Height="400" LoadingPanelID="lp" HeaderText="成本中心上傳" onOKScript="onOK5">
                                        <ContentStyle>
                                            <Paddings Padding="4px"></Paddings>
                                        </ContentStyle>
                                    </cc:ASPxPopupControl>
                                    <dx:ASPxLoadingPanel ID="lp" runat="server">
                                    </dx:ASPxLoadingPanel>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCCDTemplate" runat="server" Text="Template" CausesValidation="false" OnClick="btnCCDTemplate_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <Settings ShowTitlePanel="True" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager PageSize="10"></SettingsPager>
                <ClientSideEvents EndCallback="function(s,e){ EnabledbtnAdd(s,e); }" />
            </cc:ASPxGridView>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>