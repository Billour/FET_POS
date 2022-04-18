<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OPT01.aspx.cs" Inherits="VSS_OPT_OPT01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">
        function chkSDate(s, e) {
            e.isValid = true;
            var x = txtSDate_S.GetValue();
            var y = txtSDate_E.GetValue();

            var dvalue = s.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[開始起日訖值]不允許小於[開始起日起值]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }

        function chkEDate(s, e) {
            e.isValid = true;
            var x = txtEDate_S.GetValue();
            var y = txtEDate_E.GetValue();

            var dvalue = s.GetValue(); 
            
            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[結束訖日起值]不允許大於[結束訖日訖值]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--支付方式設定作業 -->
        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources,PaymentMethodSettings %>"></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--支付方式-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PaymentMethod2 %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbPayMode" runat="server" SelectedIndex="0" AllowMouseWheel="False">
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--會計科目-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtAccountName" runat="server" Width="250px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--狀態-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbStatus" runat="server" SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="" />
                            <dx:ListEditItem Text="有效" Value="有效" />
                            <dx:ListEditItem Text="尚未生效" Value="尚未生效" />
                            <dx:ListEditItem Text="已過期" Value="已過期" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                    <td align="right">
                        <!--開始日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSDate_S" runat="server" ClientInstanceName="txtSDate_S" EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkSDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSDate_E" runat="server" ClientInstanceName="txtSDate_E" EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkSDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right">
                        <!--結束日期-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtEDate_S" runat="server" ClientInstanceName="txtEDate_S" EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkEDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtEDate_E" runat="server" ClientInstanceName="txtEDate_E" EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkEDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
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
                    <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate"></div>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PAYMENT_METHOD_ID"
                Width="100%"
                OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                OnRowInserting="gvMaster_RowInserting" 
                OnRowUpdating="gvMaster_RowUpdating" 
                OnPageIndexChanged="gvMaster_PageIndexChanged"
                OnStartRowEditing="gvMaster_StartRowEditing" 
                OnInitNewRow="gvMaster_InitNewRow"
                OnRowValidating="gvMaster_RowValidating" 
                onhtmlrowprepared="gvMaster_HtmlRowPrepared" 
                oncelleditorinitialize="gvMaster_CellEditorInitialize">
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" ShowSelectCheckbox="true">
                            <HeaderTemplate >
                                <div style="text-align: center">
                                    <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0" Caption=" ">
                        <EditButton Visible="true">
                        </EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="ITEMNO" runat="server" Caption="<%$ Resources:WebResources, Items %>" ReadOnly="true" VisibleIndex="1">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STATUS" runat="server" Caption="<%$ Resources:WebResources, Status %>" ReadOnly="true" VisibleIndex="2">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="PAY_MODE_ID" runat="server" VisibleIndex="3">
                        <HeaderCaptionTemplate>
                            <span style="text-align:right; color: Red">*</span>
                            <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PaymentMethod2 %>" Width="90px"></dx:ASPxLabel>
                        </HeaderCaptionTemplate>
                        <DataItemTemplate>
                            <dx:ASPxLabel ID="lblPAY_MODE_NAME" runat="server" Text='<%# BIND("PAY_MODE_NAME") %>'>
                            </dx:ASPxLabel>
                        </DataItemTemplate>
                        <PropertiesComboBox ValueType="System.String">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="ACC1" runat="server" VisibleIndex="4">
                        <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject1 %>">
                        </dx:ASPxLabel>
                        </HeaderCaptionTemplate>
                        <PropertiesTextEdit MaxLength="2">
                            <ValidationSettings>
                                <RegularExpression ValidationExpression="^\d{2}$" ErrorText="輸入非數值，請重新輸入。"/>
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ACC2" runat="server" VisibleIndex="5">
                        <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject2 %>">
                        </dx:ASPxLabel>
                        </HeaderCaptionTemplate>
                        <PropertiesTextEdit MaxLength="3">
                            <ValidationSettings>
                                <RegularExpression ValidationExpression="^\d{3}$"   ErrorText="輸入非數值，請重新輸入。"/>
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ACC3" runat="server" VisibleIndex="6">
                        <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject3 %>">
                        </dx:ASPxLabel>
                        </HeaderCaptionTemplate>
                        <PropertiesTextEdit MaxLength="4" Width="40" >
                            <ValidationSettings>
                                <RegularExpression ValidationExpression="^\d{4}$"   ErrorText="輸入非數值，請重新輸入。"/>
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ACC4" runat="server" VisibleIndex="7">
                        <HeaderCaptionTemplate>
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject4 %>">
                        </dx:ASPxLabel>
                        </HeaderCaptionTemplate>
                        <PropertiesTextEdit MaxLength="6" Width="60">
                            <ValidationSettings>
                                <RegularExpression ValidationExpression="^\d{6}$"   ErrorText="輸入非數值，請重新輸入。"/>
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ACC5" runat="server" VisibleIndex="8">
                        <HeaderCaptionTemplate>
                             <span style="color: Red">*</span>
                            <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject5 %>">
                            </dx:ASPxLabel>
                        </HeaderCaptionTemplate>
                        <PropertiesTextEdit MaxLength="4">
                            <ValidationSettings>
                                <RegularExpression ValidationExpression="^\d{4}$"   ErrorText="輸入非數值，請重新輸入。"/>
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ACC6" runat="server" VisibleIndex="9">
                        <HeaderCaptionTemplate>
                            <span style="color: Red">*</span>
                            <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Subject6 %>">
                            </dx:ASPxLabel>
                        </HeaderCaptionTemplate>
                        <PropertiesTextEdit MaxLength="4">
                            <ValidationSettings>
                                <RegularExpression ValidationExpression="^\d{4}$"   ErrorText="輸入非數值，請重新輸入。"/>
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="S_DATE" runat="server" Caption="<%$ Resources:WebResources, startdate %>"
                        VisibleIndex="10">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="E_DATE" runat="server" Caption="<%$ Resources:WebResources, EndDate %>"
                        VisibleIndex="11">
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="MODI_DTM" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                        VisibleIndex="12">
                        <PropertiesDateEdit DisplayFormatInEditMode="true" DisplayFormatString="yyyy/MM/dd HH:mm:ss"
                            DropDownButton-Enabled="false" DropDownButton-Visible="false">
                            <DropDownButton Enabled="False" Visible="False">
                            </DropDownButton>
                            <ReadOnlyStyle>
                                <Border BorderStyle="None" />
                            </ReadOnlyStyle>
                        </PropertiesDateEdit>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="MODI_USER_NAME" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                        VisibleIndex="13">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PAYMENT_METHOD_ID"  Visible="false"></dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, add %>"
                                        Visible="true" OnClick=" btnAdd_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                        Visible="true" OnClick="btnDelete_Click" SkinID="DeleteBtn">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <SettingsPager PageSize="10" />
                <SettingsEditing Mode="Inline" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                <Settings ShowTitlePanel="True" />
            </cc:ASPxGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
