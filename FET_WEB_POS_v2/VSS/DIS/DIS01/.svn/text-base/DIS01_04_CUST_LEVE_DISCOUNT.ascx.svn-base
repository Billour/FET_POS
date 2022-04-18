<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DIS01_04_CUST_LEVE_DISCOUNT.ascx.cs" Inherits="CUST_LEVE_DISCOUNT" %>

 <script language="javascript" type="text/javascript">

     $(function() {
         //客戶對象 GridView切換
         rbCustomer_ValueChanged();
     });

     function rbCustomer_ValueChanged() {
         if (rbCustomer.GetValue() == "1") {
             $('.divGV1').show();
             $('.divGV2').hide();
         }
         else {
             $('.divGV1').hide();
             $('.divGV2').show();
         }
     }
 </script>
 
<table width="100%">
    <tr>
        <td align="left">
            <dx:ASPxRadioButtonList ID="rbCustomer" ClientInstanceName="rbCustomer" runat="server" RepeatDirection="Horizontal">
                <Items>
                    <dx:ListEditItem Value="1" Selected="true" Text="客戶等級" />
                    <dx:ListEditItem Value="2" Text="名單" />
                </Items>
                <ClientSideEvents ValueChanged="function(s,e) { rbCustomer_ValueChanged(); }" />
            </dx:ASPxRadioButtonList>
           <%-- <asp:RadioButtonList ID="rbCustomer" runat="server"
                RepeatDirection="Horizontal" AutoPostBack="true">
                <asp:ListItem Value="1" Selected="True" Text="客戶等級"></asp:ListItem>
                <asp:ListItem Value="2" Text="名單"></asp:ListItem>
            </asp:RadioButtonList>--%>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <div class="divGV1">
                <cc:ASPxGridView ID="gv1" ClientInstanceName="gv1" runat="server" Width="100%" AutoGenerateColumns="false"
                    KeyFieldName="CUST_LEVEL_ID"
                    OnRowInserting="gv1_RowInserting"
                    OnRowValidating="gv1_RowValidating" 
                    OnRowUpdating="gv1_RowUpdating" 
                    OnStartRowEditing="gv1_StartRowEditing" 
                    onpageindexchanged="gv1_PageIndexChanged">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <div style="text-align: center">
                                    <input type="checkbox" onclick="gv1.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button">
                            <HeaderCaptionTemplate>
                            </HeaderCaptionTemplate>
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="ARPB_S" Caption="ARPB金額(起)">
                            <PropertiesTextEdit MaxLength="9">
                                <ValidationSettings SetFocusOnError="true">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    <RegularExpression ValidationExpression="\d*" ErrorText="格式錯誤" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ARPB_E" Caption="ARPB金額(訖)">
                            <PropertiesTextEdit MaxLength="9">
                                <ValidationSettings SetFocusOnError="true">
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    <RegularExpression ValidationExpression="\d*" ErrorText="格式錯誤" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btngv1Add" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btngv1Add_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton SkinID="DeleteBtn" ID="btngv1Delete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                           CausesValidation="false" OnClick="btngv1Delete_Click">
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
                </cc:ASPxGridView>
            </div>
            
            <div class="divGV2" style="display: none">
                <dx:ASPxCallbackPanel ID="ac4" runat="server" ClientInstanceName="ac4" OnCallback="ac4_Callback">
                    <PanelCollection>
                        <dx:PanelContent>
                            <cc:ASPxGridView ID="gv2" ClientInstanceName="gv2" runat="server" Width="100%" AutoGenerateColumns="false"
                                KeyFieldName="CUST_LEVEL_ID"
                                OnRowInserting="gv2_RowInserting" 
                                OnRowValidating="gv2_RowValidating" 
                                OnStartRowEditing="gv2_StartRowEditing"
                                OnRowUpdating="gv2_RowUpdating" 
                                OnPageIndexChanged="gv2_PageIndexChanged">
                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <div style="text-align: center">
                                                <input type="checkbox" onclick="gv2.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </div>
                                        </HeaderTemplate>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewCommandColumn ButtonType="Button">
                                        <HeaderCaptionTemplate>
                                        </HeaderCaptionTemplate>
                                        <EditButton Visible="true">
                                        </EditButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="MSISDN" Caption="客戶門號">
                                        <PropertiesTextEdit MaxLength="10" Width="300">
                                            <ValidationSettings SetFocusOnError="true">
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                                <RegularExpression ValidationExpression="^(0)(9)([0-9]{8})$" ErrorText="格式不符" />
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btngv2Add" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                        OnClick="btngv2Add_Click">
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <dx:ASPxButton SkinID="DeleteBtn" ID="btngv2Delete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                       CausesValidation="false" OnClick="btngv2Delete_Click">
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnImport7" runat="server" Text="<%$ Resources:WebResources, Import %>" CausesValidation="false" AutoPostBack="false">
                                                    </dx:ASPxButton>
                                                    <cc:ASPxPopupControl ID="ASPxPopupControl8" runat="server" AllowDragging="True" AllowResize="True"
                                                        CloseAction="CloseButton" PopupElementID="btnImport7" ContentUrl="~/VSS/DIS/DIS01/DIS01_Customer_Import.aspx"
                                                        Width="640" Height="400" LoadingPanelID="lp" HeaderText="客戶門號上傳" onOKScript="onOK4">
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
                                                    <dx:ASPxButton ID="btnCustomerTemplate" runat="server" Text="Template" CausesValidation="false" 
                                                        OnClick="btnCustomerTemplate_Click">
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
                            </cc:ASPxGridView>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </div>
        </td>
    </tr>
</table>